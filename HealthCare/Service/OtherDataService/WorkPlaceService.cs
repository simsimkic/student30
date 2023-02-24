using Model.Users;
using Repository.OtherDataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.OtherDataService
{
    public class WorkPlaceService : IWorkPlaceService
    {
        public IWorkPlaceRepository _workPlaceRepository;

        public WorkPlaceService(IWorkPlaceRepository repository)
        {
            _workPlaceRepository = repository;
        }

        public IEnumerable<WorkPlace> GetAll() => _workPlaceRepository.GetAll();
    }
}
