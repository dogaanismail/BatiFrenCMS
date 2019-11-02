using BatiFren.Business.Abstract;
using BatiFren.Business.Concrete;
using BatiFren.DataAccess.Abstract;
using BatiFren.DataAccess.Concrete.EntityFramework;
using Ninject.Modules;

namespace BatiFren.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMenuService>().To<MenuManager>().InSingletonScope();
            Bind<IMenuDal>().To<EfMenuDal>().InSingletonScope();

            Bind<IMessageService>().To<MessageManager>().InSingletonScope();
            Bind<IMessageDal>().To<EfMessageDal>().InSingletonScope();

            Bind<IModuleService>().To<ModuleManager>().InSingletonScope();
            Bind<IModuleDal>().To<EfModuleDal>().InSingletonScope();

            Bind<IPageDetailService>().To<PageDetailManager>().InSingletonScope();
            Bind<IPageDetailDal>().To<EfPageDetailDal>().InSingletonScope();

            Bind<IPageService>().To<PageManager>().InSingletonScope();
            Bind<IPageDal>().To<EfPageDal>().InSingletonScope();

            Bind<IRoleService>().To<RoleManager>().InSingletonScope();
            Bind<IRoleDal>().To<EfRoleDal>().InSingletonScope();

            Bind<IUserService>().To<UserManager>().InSingletonScope();
            Bind<IUserDal>().To<EfUserDal>().InSingletonScope();

            Bind<IUserPermissionService>().To<UserPermissionManager>().InSingletonScope();
            Bind<IUserPermissionDal>().To<EfUserPermissionDal>().InSingletonScope();

        }
    }
}
