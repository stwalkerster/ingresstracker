﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersistenceFacility.cs" company="Simon Walker">
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
//   The persistence facility.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Startup.Facilities
{
    using Castle.MicroKernel.Facilities;
    using Castle.MicroKernel.Registration;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using IngressTracker.Persistence;
    using IngressTracker.Persistence.Interfaces;
    using IngressTracker.Persistence.Proxy;

    using NHibernate;
    using NHibernate.Cfg;

    using Settings = IngressTracker.Properties.Settings;

    /// <summary>
    /// The persistence facility.
    /// </summary>
    public class PersistenceFacility : AbstractFacility
    {
        #region Methods

        /// <summary>
        /// Initialise the persistence facility.
        /// </summary>
        protected override void Init()
        {
            var dataBindingInterceptor = new DataBindingInterceptor();

            var config =
                Fluently.Configure()
                    .Database(this.SetupDatabase)
                    .Mappings(a => a.FluentMappings.AddFromAssemblyOf<EntityBase>())
                    .Cache(x => x.Not.UseSecondLevelCache())
                    .ExposeConfiguration(
                        configuration => this.ConfigurePersistence(configuration, dataBindingInterceptor))
                    .BuildConfiguration();

            this.Kernel.Register(
                Component.For<ISessionFactoryProvider>().ImplementedBy<SessionFactoryProvider>(), 
                Component.For<Configuration>().Instance(config));

            var sessionFactory = this.Kernel.Resolve<ISessionFactoryProvider>().SessionFactory;
            dataBindingInterceptor.SessionFactory = sessionFactory;

            var sessionRegistraction =
                Component.For<ISession>()
                    .UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                    .LifestyleTransient();

            this.Kernel.Register(
                Component.For<ISessionFactory>().UsingFactoryMethod(k => sessionFactory), 
                sessionRegistraction);
        }

        /// <summary>
        /// The configure persistence.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="dataBindingInterceptor">
        /// Interceptor to enable INPC notifications
        /// </param>
        private void ConfigurePersistence(Configuration config, DataBindingInterceptor dataBindingInterceptor)
        {
            // This is a fix for a wierd exception.
            // {"Column 'ReservedWord' does not belong to table ReservedWords."}
            // http://stackoverflow.com/questions/1061128/mysql-nhibernate-how-fix-the-error-column-reservedword-does-not-belong-to
            config.Properties.Add("hbm2ddl.keywords", "none");

            config.SetInterceptor(dataBindingInterceptor);
        }

        /// <summary>
        /// The setup database.
        /// </summary>
        /// <returns>
        /// The <see cref="IPersistenceConfigurer"/>.
        /// </returns>
        private IPersistenceConfigurer SetupDatabase()
        {
            return
                MySQLConfiguration.Standard.ConnectionString(
                    string.Format(
                        "Server={0};Database={1};Uid={2};Pwd={3};", 
                        Settings.Default.mysqlhost, 
                        Settings.Default.mysqlschema, 
                        Settings.Default.mysqluser, 
                        Settings.Default.mysqlpassword));
        }

        #endregion
    }
}