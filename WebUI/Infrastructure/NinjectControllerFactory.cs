using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using System.Web.Routing;
using Domain.Concrete;
using Domain.Abstract;
using System.Configuration;

namespace WebUI.Infrastructure
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            //привязки
            ninjectKernel.Bind<ITaskRepository>().To<EFTaskRepository>();
            ninjectKernel.Bind<IPersonRepository>().To<EFPersonRepository>();
            ninjectKernel.Bind<IMessageRepository>().To<EFMessageRepository>();
            ninjectKernel.Bind<IRoleRepository>().To<EFRoleRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile=bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"]??"false")
            };
            ninjectKernel.Bind<IPostProcessor>()
                .To<EmailPostProcessor>().WithConstructorArgument("settings",emailSettings);
        }
    }
}