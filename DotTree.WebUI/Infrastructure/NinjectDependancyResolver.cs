using DotTree.Domain.Abstract;
using DotTree.Domain.Entities;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DotTree.Domain.Concrete;

namespace DotTree.WebUI.Infrastructure
{
    public class NinjectDependancyResolver :IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependancyResolver(IKernel kernelParam)
        {
            this.kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // add bindings
            kernel.Bind<IPersonRepository>().To<EFPersonRepository>();
        }
    }
}