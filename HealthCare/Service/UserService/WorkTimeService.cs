/***********************************************************************
 * Module:  WorkTimeService.cs
 * Purpose: Definition of the Class Service.WorkTimeService
 ***********************************************************************/

using HealthCare.Util;
using Model.Users;
using Repository.AppointmentRepository;
using Repository.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.UserService
{
    public class WorkTimeService : IWorkTimeService
    {
        public IWorkTimeRepository _repository;
        public IAppointmentRepository _appointmentRepository;
        public IAbsenceRepository _absenceRepository;

        public WorkTimeService(IWorkTimeRepository repository, IAppointmentRepository appointmentRepository, IAbsenceRepository absenceRepository)
        {
            _repository = repository;
            _appointmentRepository = appointmentRepository;
            _absenceRepository = absenceRepository;
        }
        public WorkTime Create(WorkTime entity)
           => (!IsDesiredWorkTimeInForbiddenRangeOfExistingWorkTime(entity) && !IsDesiredWorkTimeInAbsenceRange(entity)) ? _repository.Create(entity) : null;

        private bool IsDesiredWorkTimeInAbsenceRange(WorkTime entity)
            => AbsenceConverter.ConvertAbsenceListToDateTimeList(_absenceRepository.GetEmployeesFutureAbsence(entity.staff).ToList())
                                                                .Where(x => x.Date <= entity.EndDate.Date && x.Date >= entity.StartDate.Date).Count() > 0;


        private bool IsDesiredWorkTimeInForbiddenRangeOfExistingWorkTime(WorkTime entity)
         => GetWorkTime(entity.staff).Where(x => ((entity.StartDate.Date <= x.StartDate.Date && entity.EndDate.Date >= x.StartDate.Date) ||
                                                 (entity.StartDate.Date >= x.StartDate.Date && entity.StartDate.Date <= x.EndDate.Date)) &&
                                                 AreWorkDaysMatched(x, entity)).Count() > 0;

        private bool AreWorkDaysMatched(WorkTime existing, WorkTime newWorkTime)
        {
            foreach (DateTime date in GetMutualDates(existing, newWorkTime))
                if (CheckIfMutualDaysAreMatched(date, existing) && CheckIfMutualDaysAreMatched(date, newWorkTime))
                    return true;

           return false;
        }

        private bool CheckIfMutualDaysAreMatched(DateTime date, WorkTime forCheck)
            => forCheck.workDay.Where(x => (int)x.Day == (int)date.Date.DayOfWeek).Count() > 0;

        private IEnumerable<DateTime> GetMutualDates(WorkTime existing, WorkTime newWorkTime)
        {
            List<DateTime> retVal = new List<DateTime>();
            DateTime start = existing.StartDate.Date;
            while(start <= existing.EndDate.Date)
            {
                if (start >= newWorkTime.StartDate.Date && start <= newWorkTime.EndDate.Date)
                    retVal.Add(start);
                start = start.AddDays(1);
            }
            return retVal;
        }

        public IEnumerable<WorkTime> GetFutureWorkTime() => _repository.GetFutureWorkTime();

        public IEnumerable<WorkTime> GetWorkTime(User user) => _repository.GetWorkTimes(user);

        private bool DoesStaffHaveAppointmentThatDay(User staff, DateTime date) => _appointmentRepository.GetAppointmentForDoctorForDate(staff, date).Count() > 0;
        public WorkTime Delete(WorkTime entity)
            => (WorkTimeConverter.ConvertWorkTimeToEmployeeWorkDay(entity).Where(x => DoesStaffHaveAppointmentThatDay(entity.staff, x.DateFrom.Date)).Count() == 0) ? _repository.Delete(entity) : null;

        public EmployeeWorkDay GetEmployeeWorkDay(User staff, DateTime date)
        {
            List<EmployeeWorkDay> employeeWorkDayList = WorkTimeConverter.ConvertWorkTimeListToEmployeeWorkDayList(GetWorkTime(staff).ToList()).ToList();

            foreach (EmployeeWorkDay employeeWorkDay in employeeWorkDayList)
                if (employeeWorkDay.DateFrom.Date == date.Date)
                    return employeeWorkDay;

            return null;
        }

        public WorkTime GetFromID(int id) => _repository.GetFromID(id);

        public WorkTime UpdateWorkTimeWhileOverridingExisting(IEnumerable<WorkTime> oldWorkTimes, WorkTime newWorkTime)
        {
            if (DoesStaffHaveAppointmentInMutualDays(oldWorkTimes, newWorkTime))
                return null;

            oldWorkTimes.ToList().ForEach(x => _repository.Delete(x));
            CreateNewWorkTimes(oldWorkTimes, newWorkTime).ToList().ForEach(x => _repository.Create(x));

            return newWorkTime;
        }

        private bool DoesStaffHaveAppointmentInMutualDays(IEnumerable<WorkTime> oldWorkTimes, WorkTime newWorkTime)
        {
            foreach (var x in oldWorkTimes)
                foreach (var v in GetMutualDates(x, newWorkTime))
                    if (DoesStaffHaveAppointmentThatDay(x.staff, v.Date))
                        return true;

            return false;
        }

        private IEnumerable<WorkTime> CreateNewWorkTimes(IEnumerable<WorkTime> oldWorkTimes, WorkTime newWorkTime)
        {
            List<WorkTime> retVal = new List<WorkTime>();
            retVal.Add(newWorkTime);
            retVal.AddRange(MoveOldWorkTimesDateRange(oldWorkTimes.Where(x => !IsOldWorkTimeOverridenByNewWorkTime(x, newWorkTime)), newWorkTime));
            return retVal;
        }

        private IEnumerable<WorkTime> MoveOldWorkTimesDateRange(IEnumerable<WorkTime> oldWorkTimes, WorkTime newWorkTime)
        {
            List<WorkTime> retVal = new List<WorkTime>();
            foreach (var v in oldWorkTimes)
                retVal.AddRange(MoveOldWorkTime(v, newWorkTime));

            return retVal;
        }

        private IEnumerable<WorkTime> MoveOldWorkTime(WorkTime oldWorkTime, WorkTime newWorkTime)
        {
            List<WorkTime> retVal = new List<WorkTime>();
            if (oldWorkTime.EndDate.Date <= newWorkTime.EndDate.Date)
                retVal.Add(new WorkTime() { staff = oldWorkTime.staff, workDay = oldWorkTime.workDay, StartDate = oldWorkTime.StartDate, EndDate = newWorkTime.StartDate.Date.AddDays(-1)});
            else
                retVal.Add(new WorkTime() { staff = oldWorkTime.staff, workDay = oldWorkTime.workDay, StartDate = newWorkTime.EndDate.Date.AddDays(1), EndDate = oldWorkTime.EndDate});
            
            if (oldWorkTime.EndDate.Date >= newWorkTime.EndDate.Date && oldWorkTime.StartDate.Date <= newWorkTime.StartDate.Date)
                retVal.Add(new WorkTime() { staff = oldWorkTime.staff, workDay = oldWorkTime.workDay, StartDate = oldWorkTime.StartDate, EndDate = newWorkTime.StartDate.Date.AddDays(-1) });

            return retVal;
        }

        private bool IsOldWorkTimeOverridenByNewWorkTime(WorkTime oldWorkTime, WorkTime newWorkTime)
            => oldWorkTime.StartDate.Date >= newWorkTime.StartDate.Date && oldWorkTime.EndDate.Date <= newWorkTime.EndDate.Date;
    }
}