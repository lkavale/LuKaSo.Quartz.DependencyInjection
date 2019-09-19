using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quartz.DependencyInjection.NetCore;
using QuartzDependencyInjectionTests.Helpers;
using System;

namespace QuartzDependencyInjectionTests
{
    [TestClass]
    public class JobOptionConfiguratorTests
    {
        [TestMethod]
        public void JobOptionConfiguratorOk()
        {
            Assert.IsNotNull(new JobOptionConfigurator(typeof(DummyJob)));
        }

        [TestMethod]
        public void JobOptionConfiguratorMissingArgument()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new JobOptionConfigurator(null));
        }

        [TestMethod]
        public void JobOptionConfiguratorNotJob()
        {
            Assert.ThrowsException<ArgumentException>(() => new JobOptionConfigurator(typeof(NotJob)));
        }
    }
}
