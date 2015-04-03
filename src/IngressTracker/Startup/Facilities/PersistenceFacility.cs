// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersistenceFacility.cs" company="Simon Walker">
//   Copyright (C) 2015 Simon Walker
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
    using System.Data.SQLite;
    using System.IO;

    using Castle.MicroKernel.Facilities;
    using Castle.MicroKernel.Registration;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using IngressTracker.Persistence;
    using IngressTracker.Persistence.Interfaces;
    using IngressTracker.Persistence.Proxy;

    using NHibernate;
    using NHibernate.Cfg;

    using Environment = System.Environment;

    /// <summary>
    /// The persistence facility.
    /// </summary>
    public class PersistenceFacility : AbstractFacility
    {
        #region Public Properties

        /// <summary>
        /// Gets the data file path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string DataFilePath
        {
            get
            {
                var appDataFolder = "IngressTracker";
#if DEBUG
                appDataFolder += "-DEBUG";
#endif

                var appDataPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                    appDataFolder);

                var appData = new DirectoryInfo(appDataPath);
                if (!appData.Exists)
                {
                    appData.Create();
                }

                var dataFilePath = Path.Combine(appDataPath, "data.s3db");
                return dataFilePath;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The configure persistence.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="dataBindingInterceptor">
        /// Interceptor to enable INPC notifications
        /// </param>
        public static void ConfigurePersistence(Configuration config, DataBindingInterceptor dataBindingInterceptor)
        {
            if (dataBindingInterceptor != null)
            {
                config.SetInterceptor(dataBindingInterceptor);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialise the persistence facility.
        /// </summary>
        protected override void Init()
        {
            this.Kernel.Register(Component.For<DataBindingInterceptor>());
            var dataBindingInterceptor = this.Kernel.Resolve<DataBindingInterceptor>();

            var config =
                Fluently.Configure()
                    .Database(this.SetupDatabase)
                    .Mappings(a => a.FluentMappings.AddFromAssemblyOf<EntityBase>())
                    .Cache(x => x.Not.UseSecondLevelCache())
                    .ExposeConfiguration(configuration => ConfigurePersistence(configuration, dataBindingInterceptor))
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
        /// The setup database.
        /// </summary>
        /// <returns>
        /// The <see cref="IPersistenceConfigurer"/>.
        /// </returns>
        private IPersistenceConfigurer SetupDatabase()
        {
            var dataFilePath = DataFilePath;
            if (!File.Exists(dataFilePath))
            {
                SQLiteConnection.CreateFile(dataFilePath);
            }

            var sb = new SchemaBuilder(dataFilePath);
            sb.UpgradeSchema();

            return new SQLiteConfiguration().UsingFile(dataFilePath);
        }

        #endregion
    }
}