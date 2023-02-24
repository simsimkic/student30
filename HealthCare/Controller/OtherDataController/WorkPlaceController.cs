using Model.Users;
using Service.OtherDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.OtherDataController
{
    public class WorkPlaceController : IWorkPlaceController
    {
        public IWorkPlaceService _specializationService;

        public WorkPlaceController(IWorkPlaceService service) 
        {
            _specializationService = service;
        }
        public IEnumerable<WorkPlace> GetAll() => _specializationService.GetAll();
    }
}
