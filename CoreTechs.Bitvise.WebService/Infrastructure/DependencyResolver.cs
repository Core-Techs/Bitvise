using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;


namespace CoreTechs.Bitvise.WebService.Infrastructure
{
    public class DependencyResolver : IDependencyResolver
    {
        public void Dispose()
        {
            
        }

        public object GetService(Type type)
        {
            if (type == typeof (BitviseSSHServer))
                return new BitviseSSHServer();

            if (type == typeof (BitviseController))
                return new BitviseController(GetService<BitviseSSHServer>());

            return null;
        }

        public T GetService<T>()
        {
            return (T) GetService(typeof (T));
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new object[0];
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }
}