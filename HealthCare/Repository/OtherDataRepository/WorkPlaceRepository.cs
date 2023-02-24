using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.OtherDataRepository
{
    public class WorkPlaceRepository : IWorkPlaceRepository
    {
        public JSONStream<WorkPlace> _stream;

        public WorkPlaceRepository(JSONStream<WorkPlace> stream)
        {
            _stream = stream;
        }
        public IEnumerable<WorkPlace> GetAll() => _stream.GetAll();
    }
}
