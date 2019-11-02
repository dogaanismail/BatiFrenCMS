using BatiFren.Business.Abstract;
using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;
using System;
using System.Collections.Generic;

namespace BatiFren.Business.Concrete
{
    public class ModuleManager : IModuleService
    {
        private IModuleDal _moduleDal;
        public ModuleManager(IModuleDal moduleDal)
        {
            _moduleDal = moduleDal;
        }
        public void Add(Module module)
        {
            throw new NotImplementedException();
        }

        public void Delete(Module module)
        {
            throw new NotImplementedException();
        }

        public Module GetByModuleId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Module> GetList()
        {
            return _moduleDal.GetList();
        }

        public void Update(Module module)
        {
            throw new NotImplementedException();
        }
    }
}
