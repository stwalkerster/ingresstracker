// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationBootstrapper.cs" company="Simon Walker">
//   Copyright (C) 2014 Simon Walker
//   
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
//   documentation files (the "Software"), to deal in the Software without restriction, including without limitation
//   the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
//   to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//   
//   The above copyright notice and this permission notice shall be included in all copies or substantial portions of
//   the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//   THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//   TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//   SOFTWARE.
// </copyright>
// <summary>
//   The bootstrapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Startup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    using Caliburn.Micro;

    using Castle.Facilities.Logging;
    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using IngressTracker.Helpers;
    using IngressTracker.Startup.Resolvers;
    using IngressTracker.ViewModels.Interfaces;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// The bootstrapper.
    /// </summary>
    public class ApplicationBootstrapper : BootstrapperBase
    {
        #region Fields

        /// <summary>
        /// The container.
        /// </summary>
        private WindsorContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationBootstrapper"/> class.
        /// </summary>
        public ApplicationBootstrapper()
        {
            this.Initialize();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The configure.
        /// </summary>
        protected override void Configure()
        {
            this.container = new WindsorContainer();

            var commandLineArgs = Environment.GetCommandLineArgs();
            var autoLogin = new AutoLoginHelper();

            if (commandLineArgs.Count() == 3)
            {
                autoLogin.Username = commandLineArgs[1];
                autoLogin.Password = commandLineArgs[2];
            }

            this.container.Register(Component.For<AutoLoginHelper>().Instance(autoLogin).LifestyleSingleton());

            // add the container to itself. This is probably a Bad Idea(tm) but hey.
            this.container.Register(Component.For<IWindsorContainer>().Instance(this.container).LifestyleSingleton());

            this.container.Kernel.Resolver.AddSubResolver(new AppSettingsDependencyResolver());

            this.container.AddFacility<EventRegistrationFacility>();
            this.container.AddFacility<TypedFactoryFacility>();
            this.container.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("logger.config"));

            var viewModelRegistrations =
                Classes.FromThisAssembly()
                    .InNamespace("IngressTracker.ViewModels")
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient();

            var services =
                Classes.FromThisAssembly()
                    .InNamespace("IngressTracker.Services")
                    .WithServiceAllInterfaces()
                    .LifestyleSingleton();

            this.container.Register(
                Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifestyleSingleton(), 
                Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton(), 
                services, 
                viewModelRegistrations);

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(this.container));


        }

        /// <summary>
        /// The get all instances.
        /// </summary>
        /// <param name="service">
        /// The service.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{Object}"/>.
        /// </returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.container.ResolveAll(service).Cast<object>();
        }

        /// <summary>
        /// The get instance.
        /// </summary>
        /// <param name="service">
        /// The service.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return this.container.Resolve(service);
            }

            return this.container.Resolve(key, service);
        }

        /// <summary>
        /// The on startup.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            this.DisplayRootViewFor<IShellViewModel>();
        }

        #endregion
    }
}