using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Quartz.DependencyInjection.NetCore;
using QuartzDependencyInjectionTests.Helpers;
using System;

namespace QuartzDependencyInjectionTests
{
    [TestClass]
    public class JobOptionsConfiguratorTests
    {
        [TestMethod]
        public void JobOptionsConfiguratorMissingArgument()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new JobOptionsConfigurator(null));
        }

        [TestMethod]
        public void JobOptionsConfiguratorNotExistingJob()
        {
            Assert.ThrowsException<NotSupportedException>(() =>
            {
                GetConfigurator().Configure<DummyJob2>(x =>
                {
                    x.ConfigureJobDetail((builder, type) => builder.Build());
                    x.ConfigureTrigger((builder, type) => builder.Build());
                });
            });
        }

        [TestMethod]
        public void JobOptionsConfiguratorBadJobConfiguration()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                GetConfigurator().Configure<DummyJob>(x => { });
            });

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                GetConfigurator().Configure<DummyJob>(x =>
                {
                    x.ConfigureJobDetail((builder, type) => builder.Build());
                });
            });

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                GetConfigurator().Configure<DummyJob>(x =>
                {
                    x.ConfigureTrigger((builder, type) => builder.Build());
                });
            });
        }

        [TestMethod]
        public void JobOptionsConfiguratorConfigurationOk()
        {
            GetConfigurator().Configure<DummyJob>(x =>
            {
                x.ConfigureJobDetail((builder, type) => builder.Build());
                x.ConfigureTrigger((builder, type) => builder.Build());
            });

            Assert.IsTrue(true);
        }

        private JobOptionsConfigurator GetConfigurator()
        {
            var builder = new Mock<IApplicationBuilder>();
            builder.Setup(x => x.ApplicationServices)
                .Returns(new ServiceCollection().AddQuartz(x => x.AddJob<DummyJob>()).BuildServiceProvider());

            return new JobOptionsConfigurator(builder.Object);
        }
    }
}
