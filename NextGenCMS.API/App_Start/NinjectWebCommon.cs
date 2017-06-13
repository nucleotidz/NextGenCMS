[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NextGenCMS.API.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NextGenCMS.API.App_Start.NinjectWebCommon), "Stop")]

namespace NextGenCMS.API.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using NextGenCMS.BL.interfaces;
    using NextGenCMS.BL.classes;
    using NextGenCMS.DL.interfaces;
    using NextGenCMS.DL.classes;
    using NextGenCMS.APIHelper.interfaces;
    using NextGenCMS.APIHelper.classes;
    using NextGenCMS.UnitOfWork.Interfaces;
    using NextGenCMS.UnitOfWork.Classes;
    using NextGenCMS.ORM;
    using System.Data.Entity;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork<NextgenModel>>().InRequestScope();
            kernel.Bind<NextgenModel>().ToSelf().InRequestScope();
            kernel.Bind<DbContext>().To<NextgenModel>();
            kernel.Bind<IAuthentication>().To<Authentication>();
            kernel.Bind<IAuthenticationRepository>().To<AuthenticationRepository>();
            kernel.Bind<IAPIHelper>().To<APIHelper>();
            kernel.Bind<IFolderNext>().To<Folder>();
            kernel.Bind<IFile>().To<File>();
            kernel.Bind<ISearchBL>().To<SearchBL>();
            kernel.Bind<IWorkflowBL>().To<WorkflowBL>();
            kernel.Bind<IAdministration>().To<Administration>();
            kernel.Bind<IAdministrationRepository>().To<AdministrationRepository>();
            kernel.Bind<IWorkflowReport>().To<WorkflowReport>();
            kernel.Bind<IFileRepository>().To<FileRepository>();
        }
    }
}
