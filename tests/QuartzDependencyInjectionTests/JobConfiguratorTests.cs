using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quartz;
using Quartz.DependencyInjection.NetCore;
using QuartzDependencyInjectionTests.Helpers;
using System;

namespace QuartzDependencyInjectionTests
{
    [TestClass]
    public class JobConfiguratorTests
    {
        [TestMethod]
        public void JobConfiguratorWithoutSC()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new JobConfigurator(null));
        }

        [TestMethod]
        public void AddJobGeneric()
        {
            var services = new ServiceCollection();
            var configurator = new JobConfigurator(services);
            configurator.AddJob<DummyJob>();

            Assert.AreEqual(1, services.Count);
        }

        [TestMethod]
        public void AddJobGenericDoubled()
        {
            var services = new ServiceCollection();
            var configurator = new JobConfigurator(services);
            configurator.AddJob<DummyJob>();

            Assert.ThrowsException<ObjectAlreadyExistsException>(() => configurator.AddJob<DummyJob>());
        }

        [TestMethod]
        public void AddJobOk()
        {
            var services = new ServiceCollection();
            var configurator = new JobConfigurator(services);
            configurator.AddJob(typeof(DummyJob));

            Assert.AreEqual(1, services.Count);
        }

        [TestMethod]
        public void AddJobNotJob()
        {
            var configurator = new JobConfigurator(new ServiceCollection());
            Assert.ThrowsException<ArgumentException>(() => configurator.AddJob(typeof(NotJob)));
        }
    }
}
