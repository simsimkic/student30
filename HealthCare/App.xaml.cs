using Controller;
using Controller.AppointmenController;
using Controller.CommunicationControllers;
using Controller.DiagnosisController;
using Controller.DrugController;
using Controller.MedicalRecordControllers;
using Controller.OtherDataController;
using Controller.ReportController;
using Controller.RoomControllers;
using Controller.UserControllers;
using HealthCare.Controller.CommunicationControllers;
using HealthCare.Repository.CommunicationRepositories;
using HealthCare.Service.CommunicationServices;
using Model.Appointment;
using Model.Communication;
using Model.Diagnosis;
using Model.Drug;
using Model.MedicalRecords;
using Model.Rooms;
using Model.Users;
using Repository;
using Repository.AppointmentRepository;
using Repository.CommunicationRepositories;
using Repository.DiagnosisRepositories;
using Repository.DrugRepositories;
using Repository.MedicalRecordRepositories;
using Repository.OtherDataRepository;
using Repository.RoomRepositories;
using Repository.UserRepositories;
using Service.AppointmentService;
using Service.CommunicationServices;
using Service.DiagnosisService;
using Service.DrugService;
using Service.MedicalRecordServices;
using Service.OtherDataService;
using Service.ReportService;
using Service.RoomServices;
using Service.UserService;
using System;
using System.IO;
using System.Threading;
using System.Windows;

namespace HealthCare
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public User _currentUser;

        public static string configurationPath = @"../../Resources/Configuration/configuration.txt";

        public static int MaxDaysForSpecialistExamination;
        public static int MaxDaysForControlExamination;
        public static int MaxDaysForHospitalTreatment;
        public static int MinDaysForRenovationStart;
        public static int MinDrugAmountForNotification;
        public static int PercentOfMatchingForPotentialDiagnosis;
        public static int DefaultAppointmentExaminationDuration;
        public static int DefaultAppointmentSurgeryDuration;
        public static int MaxDaysForControlSpecialistExamination;
        public static int MaxTimePeriodToMakeAppointment;

        private const string NOTIFICATION_FILE = "../../FileStorage/Notifications.json";
        private const string MEDICALRECORD_FILE = "../../FileStorage/MedicalRecords.json";
        private const string SYMPTOMS_FILE = "../../FileStorage/Symptoms.json";
        private const string DIAGNOSIS_FILE = "../../FileStorage/Diagnosis.json";
        private const string ALLERGENS_FILE = @"../../FileStorage/Allergens.json";
        private const string INGREDIENTS_FILE = @"../../FileStorage/Ingredients.json";
        private const string INVENTORY_TYPE_FILE = @"../../FileStorage/InventoryType.json";
        private const string DRUG_FILE = @"../../FileStorage/Drugs.json";
        private const string INVENTORY_FILE = @"../../FileStorage/Inventories.json";
        private const string ROOM_FILE = @"../../FileStorage/Rooms.json";
        private const string INVENTORY_AMOUNT_FILE = @"../../FileStorage/InventoryAmount.json";
        private const string ROOM_SECTORS_FILE = @"../../FileStorage/RoomSectors.json";
        private const string APPOINTMENT_FILE = @"../../FileStorage/Appointment.json";
        private const string ARTICLE_FILE = @"../../FileStorage/Articles.json";
        private const string SOFTWARE_RATING_FILE = @"../../FileStorage/SoftwareRating.json";
        private const string REFFERALS_FILE = @"../../FileStorage/Refferals.json";
        private const string RENOVATION_FILE = @"../../FileStorage/Renovations.json";
        private const string QUESTIONS_FILE = @"../../FileStorage/Questions.json";

        private const string USER_FILE = @"../../FileStorage/Users.json";
        private const string SPECIALIZATION_FILE = @"../../FileStorage/Specializations.json";
        private const string WORK_PLACE_FILE = @"../../FileStorage/WorkPlaces.json";
        private const string WORK_TIME_FILE = @"../../FileStorage/WorkTimes.json";
        private const string ABSENCES_FILE = @"../../FileStorage/Absences.json";
        private const string FEEDBACKS_FILE = @"../../FileStorage/FeedBacks.json";


        public IAllergyController allergyController { get; private set; }
        public IIngredientController ingredientController { get; private set; }
        public IInventoryTypeController inventoryTypeController { get; private set; }
        public IDrugController drugControler { get; private set; }
        public ISectorController sectorController { get; private set; }
        public IRoomController roomController { get; private set; }
        public IInventoryController inventoryController { get; private set; }
        public ISpecializationController specializationController { get; private set; }
        public IWorkPlaceController workPlaceController { get; private set; }
        public IUserController userController { get; private set; }

        public INotificationController notificationController { get; private set; }
        public IRecordController medicalRecordController { get; private set; }
        public ICUDController<MedicalHistory> medicalHistoryController { get; private set; }
        public ICUDController<FamilyRiskFactor> familyMedicalHistoryController { get; private set; }
        public ICUDController<RiskFactor> riskFactorController { get; private set; }
        public ICUDController<RiskAllergies> riskAllergiesController { get; private set; }

        public ICUDController<Immunization> immunizationController { get; private set; }

        public ISymptomController symptomsController { get; private set; }
        public IDiagnosisController diagnosisController { get; private set; }

        public ITreatmentController treatmentController { get; private set; }
        public IWorkTimeController workTimeController { get; private set; }

        public IArticleController articleController { get; private set; }

        public ISoftwareRatingController softwareRatingController { get; private set; }
        public IAbsenceController absenceController { get; private set; }
        public IRefferalController refferalControler { get; private set; }
        public IAppointmentController appointmentController { get; private set; }
        public IReportController reportController { get; private set; }
        public IRenovateController renovateController { get; private set; }
        public IFeedBackController feedBackController { get; private set; }

        public IQuestionController questionController { get; private set; }
        public App()
        {
            initConfiguration();

            var feedBackRepository = new FeedBackRepository(new JSONStream<FeedBack>(FEEDBACKS_FILE));
            var feedBackService = new FeedBackService(feedBackRepository);
            feedBackController = new FeedBackController(feedBackService);

            var appointmentRepository = new AppointmentRepository(new JSONStream<Appointment>(APPOINTMENT_FILE));


            var renovationRepository = new RenovateRepository(new JSONStream<Renovate>(RENOVATION_FILE));
            var renovationService = new RenovateService(renovationRepository, appointmentRepository);
            renovateController = new RenovateController(renovationService);








            var workPlaceRepository = new WorkPlaceRepository(new JSONStream<WorkPlace>(WORK_PLACE_FILE));
            var workPlaceService = new WorkPlaceService(workPlaceRepository);
            workPlaceController = new WorkPlaceController(workPlaceService);

            SpecializationRepository specializationRepository = new SpecializationRepository(new JSONStream<Specialization>(SPECIALIZATION_FILE));
            var specializationService = new SpecializationService(specializationRepository);
            specializationController = new SpecializationController(specializationService);


            var inventoryAmountRepository = new InventoryAmountRepository(new JSONStream<InventoryAmount>(INVENTORY_AMOUNT_FILE));

            var inventoryRepository = new InventoryRepository(new JSONStream<Inventory>(INVENTORY_FILE));
            var inventoryService = new InventoryService(inventoryRepository, inventoryAmountRepository);
            inventoryController = new InventoryController(inventoryService);

            var roomRepository = new RoomRepository(new JSONStream<Room>(ROOM_FILE));
            var roomService = new RoomService(roomRepository, inventoryAmountRepository, inventoryService, renovationRepository, appointmentRepository);
            roomController = new RoomController(roomService);

            var sectorRepository = new SectorRepository(new JSONStream<Sector>(ROOM_SECTORS_FILE));
            var sectorService = new SectorService(sectorRepository);
            sectorController = new SectorController(sectorService);



            var inventoryTypeRepository = new InventoryTypeRepository(new JSONStream<InventoryType>(INVENTORY_TYPE_FILE));
            var inventoryTypeService = new InventoryTypeService(inventoryTypeRepository);
            inventoryTypeController = new InventoryTypeController(inventoryTypeService);

            var allergensRepository = new AllergyRepository(new JSONStream<Allergen>(ALLERGENS_FILE));
            var allergensService = new AllergyService(allergensRepository);
            allergyController = new AllergyController(allergensService);

            var ingredientRepository = new IngredientRepository(new JSONStream<Ingredient>(INGREDIENTS_FILE));
            var ingredientService = new IgredientService(ingredientRepository);
            ingredientController = new IngredientController(ingredientService);

            var userRepository = new UserRepository(new JSONStream<User>(USER_FILE), appointmentRepository);
            var absencesRepository = new AbsenceRepository(new JSONStream<Absence>(ABSENCES_FILE));

            var notificationRepository = new NotificationRepository(new JSONStream<Notification>(NOTIFICATION_FILE));
            var notificationService = new NotificationService(notificationRepository, userRepository);
            notificationController = new NotificationController(notificationService);

            var medicalRecordRepository = new RecordRepository(new JSONStream<MedicalRecord>(MEDICALRECORD_FILE));
            var medicalRecordService = new RecordService(medicalRecordRepository);
            medicalRecordController = new RecordController(medicalRecordService);

            var medicalHistoryService = new MedicalHistoryService(medicalRecordRepository);
            medicalHistoryController = new MedicalHistoryController(medicalHistoryService);

            var familyMedicalHistoryService = new FamilyRiskFactorService(medicalRecordRepository);
            familyMedicalHistoryController = new FamilyRiskFactorController(familyMedicalHistoryService);

            var riskFactorService = new RiskFactorService(medicalRecordRepository);
            riskFactorController = new RiskFactorController(riskFactorService);

            var riskAllergiesService = new RiskAllergyService(medicalRecordRepository);
            riskAllergiesController = new RiskAllergyController(riskAllergiesService);

            var immunizationService = new ImmunizationService(medicalRecordRepository);
            immunizationController = new ImmunizationController(immunizationService);

            var treatmentService = new TreatmentService(medicalRecordRepository);
            treatmentController = new TreatmentController(treatmentService);

            var symptomsRepository = new SymptomsRepository(new JSONStream<Symptom>(SYMPTOMS_FILE));
            var symptomsService = new SymptomService(symptomsRepository);
            symptomsController = new SymptomController(symptomsService);

            var diagnosisRepository = new DiagnosisRepository(new JSONStream<Diagnosis>(DIAGNOSIS_FILE));
            var diagnosisService = new DiagnosisService(diagnosisRepository);
            diagnosisController = new DiagnosisController(diagnosisService);

            var articleRepository = new ArticleRepository(new JSONStream<Article>(ARTICLE_FILE));
            var articleService = new ArticleService(articleRepository);
            articleController = new ArticleController(articleService);

            var softwareRatingRepository = new SoftwareRatingRepository(new JSONStream<SoftwareRating>(SOFTWARE_RATING_FILE));
            var softwareRatingService = new SoftwareRatingService(softwareRatingRepository);
            softwareRatingController = new SoftwareRatingController(softwareRatingService);

            var refferalRepository = new RefferalRepository(new JSONStream<Refferal>(REFFERALS_FILE));
            var refferalService = new RefferalService(refferalRepository, notificationService);
            refferalControler = new RefferalController(refferalService);

            var questionRepository = new QuestionRepository(new JSONStream<Question>(QUESTIONS_FILE));
            var questionService = new QuestionService(questionRepository);
            questionController = new QuestionController(questionService);



            var workTimeRepository = new WorkTimeRepository(new JSONStream<WorkTime>(WORK_TIME_FILE));
            var workTimeService = new WorkTimeService(workTimeRepository, appointmentRepository, absencesRepository);
            workTimeController = new WorkTimeController(workTimeService);

            var userService = new UserService(userRepository, workTimeRepository, absencesRepository, appointmentRepository);
            userController = new UserController(userService);

            var absencesService = new AbsenceService(absencesRepository, appointmentRepository, notificationService);
            absenceController = new AbsenceController(absencesService);

            var drugRepository = new DrugRepository(new JSONStream<Drug>(DRUG_FILE));
            var drugService = new DrugService(drugRepository, notificationService, userRepository);
            drugControler = new DrugController(drugService);

            var appointmentService = new AppointmentService(appointmentRepository, workTimeService, userService, notificationService, renovationService);
            appointmentController = new AppointmentController(appointmentService);

            var reportService = new ReportService(userService, workTimeService, appointmentService, medicalRecordService);
            reportController = new ReportController(reportService);

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var langCode = Secretary.Properties.Settings.Default.languageCodes;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langCode);
            base.OnStartup(e);
        }

        private void initConfiguration()
        {
            string line = File.ReadAllText(configurationPath);
            line = line.Replace("\n", "").Replace("\r", "");

            string[] path = line.Split(';');

            foreach (string keyValuePair in path)
            {
                string[] token = keyValuePair.Split('=');
                if (token[0] == "MaxDaysForSpecialistExamination")
                    MaxDaysForSpecialistExamination = Int32.Parse(token[1]);
                else if (token[0] == "MaxDaysForControlExamination")
                    MaxDaysForControlExamination = Int32.Parse(token[1]);
                else if (token[0] == "MaxDaysForHospitalTreatment")
                    MaxDaysForHospitalTreatment = Int32.Parse(token[1]);
                else if (token[0] == "MinDaysForRenovationStart")
                    MinDaysForRenovationStart = Int32.Parse(token[1]);
                else if (token[0] == "MinDrugAmountForNotification")
                    MinDrugAmountForNotification = Int32.Parse(token[1]);
                else if (token[0] == "PercentOfMatchingForPotentialDiagnosis")
                    PercentOfMatchingForPotentialDiagnosis = Int32.Parse(token[1]);
                else if (token[0] == "DefaultAppointmentExaminationDuration")
                    DefaultAppointmentExaminationDuration = Int32.Parse(token[1]);
                else if (token[0] == "DefaultAppointmentSurgeryDuration")
                    DefaultAppointmentSurgeryDuration = Int32.Parse(token[1]);
                else if (token[0] == "MaxDaysForControlSpecialistExamination")
                    MaxDaysForControlSpecialistExamination = Int32.Parse(token[1]);
                else if(token[0] == "MaxTimePeriodToMakeAppointment")
                    MaxTimePeriodToMakeAppointment = Int32.Parse(token[1]);
            }
        }
    }
}
