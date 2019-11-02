using BatiFren.Entities;
using System.Collections.Generic;

namespace BatiFren.Business.Abstract
{
    public interface IModuleService
    {
        List<Module> GetList();
        Module GetByModuleId(int id);
        void Add(Module module);
        void Delete(Module module);
        void Update(Module module);
    }
}
