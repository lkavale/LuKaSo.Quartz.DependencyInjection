using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Quartz.DependencyInjection.NetCore.Common
{
    /// <summary>
    /// Equality comparrer for ServiceDescriptor
    /// </summary>
    public class ServiceDescriptorEqualityComparer : IEqualityComparer<ServiceDescriptor>
    {
        public bool Equals(ServiceDescriptor x, ServiceDescriptor y)
        {
            return x.ServiceType == y.ServiceType
                && x.ImplementationType == y.ImplementationType
                && x.Lifetime == y.Lifetime;
        }

        public int GetHashCode(ServiceDescriptor obj)
        {
            unchecked
            {
                int hash = 13;

                hash = (hash * 7) + obj.ServiceType.GetHashCode();
                hash = (hash * 7) + obj.ImplementationType.GetHashCode();
                hash = (hash * 7) + obj.Lifetime.GetHashCode();

                return hash;
            }
        }
    }
}
