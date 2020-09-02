using Domain;
using Domain.Entities;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {

            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book
                {
                    Name = "ewqeqweqwe",
                    Author = "Ya",
                    Price = 20,
                    Description = "Opisanie",
                },
                new Book
                {
                    Name = "ttwet",
                    Author = "Ya",
                    Price = 10000,
                    Description = "Descr",
                },
                new Book
                {
                    Name = "qweqwe",
                    Author = "Ya",
                    Price = 30000000,
                    Description = "Opisanie",
                },
            });
            kernel.Bind<IBookRepository>().ToConstant(mock.Object);
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}