// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginViewModel.cs" company="Simon Walker">
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
//   The login view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    using Caliburn.Micro;

    using Castle.Windsor;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using IngressTracker.DataModel.Models;
    using IngressTracker.Persistence;
    using IngressTracker.Properties;
    using IngressTracker.Services.Interfaces;
    using IngressTracker.Startup.Facilities;
    using IngressTracker.ViewModels.Interfaces;

    using NHibernate.Exceptions;

    /// <summary>
    /// The login view model.
    /// </summary>
    public class LoginViewModel : Screen, ILoginViewModel
    {
        #region Fields

        /// <summary>
        /// The container.
        /// </summary>
        private readonly IWindsorContainer container;

        /// <summary>
        /// The database schema name.
        /// </summary>
        private readonly string databaseSchemaName;

        /// <summary>
        /// The database server hostname.
        /// </summary>
        private readonly string databaseServerHostname;

        /// <summary>
        /// The login service.
        /// </summary>
        private readonly ILoginService loginService;

        /// <summary>
        /// The agent grid visible.
        /// </summary>
        private bool agentGridVisible;

        /// <summary>
        /// The available agents.
        /// </summary>
        private ObservableCollection<User> availableAgents;

        /// <summary>
        /// The display name.
        /// </summary>
        private string displayName;

        /// <summary>
        /// The login grid visible.
        /// </summary>
        private bool loginGridVisible;

        /// <summary>
        /// The password.
        /// </summary>
        private string password;

        /// <summary>
        /// The selected available agent.
        /// </summary>
        private User selectedAvailableAgent;

        /// <summary>
        /// The username.
        /// </summary>
        private string username;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        /// <param name="databaseServerHostname">
        /// The database Server Hostname.
        /// </param>
        /// <param name="databaseSchemaName">
        /// The database Schema Name.
        /// </param>
        /// <param name="loginService">
        /// The login Service.
        /// </param>
        /// <param name="container">
        /// The container.
        /// </param>
        public LoginViewModel(
            string databaseServerHostname, 
            string databaseSchemaName, 
            ILoginService loginService, 
            IWindsorContainer container)
        {
            this.databaseServerHostname = databaseServerHostname;
            this.databaseSchemaName = databaseSchemaName;
            this.loginService = loginService;
            this.container = container;
            this.loginGridVisible = true;
            this.availableAgents = new ObservableCollection<User>();

            this.displayName = Resources.LoginView;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether agent grid visible.
        /// </summary>
        public bool AgentGridVisible
        {
            get
            {
                return this.agentGridVisible;
            }

            private set
            {
                if (!value.Equals(this.agentGridVisible))
                {
                    this.agentGridVisible = value;
                    this.NotifyOfPropertyChange(() => this.AgentGridVisible);
                }
            }
        }

        /// <summary>
        /// Gets the available agents.
        /// </summary>
        public ObservableCollection<User> AvailableAgents
        {
            get
            {
                return this.availableAgents;
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return this.displayName;
            }

            set
            {
                if (value != this.displayName)
                {
                    this.displayName = value;
                    this.NotifyOfPropertyChange(() => this.DisplayName);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether login grid visible.
        /// </summary>
        public bool LoginGridVisible
        {
            get
            {
                return this.loginGridVisible;
            }

            private set
            {
                if (!value.Equals(this.loginGridVisible))
                {
                    this.loginGridVisible = value;
                    this.NotifyOfPropertyChange(() => this.LoginGridVisible);
                }
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (value != this.password)
                {
                    this.password = value;
                    this.NotifyOfPropertyChange(() => this.Password);
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected available agent.
        /// </summary>
        public User SelectedAvailableAgent
        {
            get
            {
                return this.selectedAvailableAgent;
            }

            set
            {
                if (value != this.selectedAvailableAgent)
                {
                    this.selectedAvailableAgent = value;
                    this.NotifyOfPropertyChange(() => this.SelectedAvailableAgent);
                }
            }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                if (value != this.username)
                {
                    this.username = value;
                    this.NotifyOfPropertyChange(() => this.Username);
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The connect.
        /// </summary>
        public void Connect()
        {
            var sf =
                Fluently.Configure()
                    .Database(
                        () =>
                        MySQLConfiguration.Standard.ConnectionString(
                            string.Format(
                                "Server={0};Database={1};Uid={2};Pwd={3};", 
                                this.databaseServerHostname, 
                                this.databaseSchemaName, 
                                this.Username, 
                                this.Password)))
                    .Mappings(a => a.FluentMappings.AddFromAssemblyOf<EntityBase>())
                    .Cache(x => x.Not.UseSecondLevelCache())
                    .ExposeConfiguration(configuration => PersistenceFacility.ConfigurePersistence(configuration, null))
                    .BuildSessionFactory();

            var tempSession = sf.OpenSession();

            try
            {
                var collection = tempSession.QueryOver<User>().Where(x => x.DatabaseUsername == this.Username).List();

                // at least one user on this connection is allowed access to all agents.
                if (collection.Count(x => x.AccessToAllAgents) > 0)
                {
                    collection = tempSession.QueryOver<User>().List();
                }

                this.availableAgents = new ObservableCollection<User>(collection);

                if (collection.Count == 1)
                {
                    this.SelectedAvailableAgent = collection.First();
                    this.Login();
                }

                this.NotifyOfPropertyChange(() => this.AvailableAgents);

                this.LoginGridVisible = false;
                this.AgentGridVisible = true;
            }
            catch (GenericADOException ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// The login.
        /// </summary>
        public void Login()
        {
            this.loginService.Username = this.Username;
            this.loginService.Password = this.Password;
            this.loginService.Agent = this.SelectedAvailableAgent;
            this.loginService.AvailableAgents = this.AvailableAgents;

            this.container.AddFacility<PersistenceFacility>();

            this.loginService.LoginComplete = true;

            var shellViewModel = (ShellViewModel)this.Parent;

            shellViewModel.Refresh();

            if (shellViewModel.CanOpenUserOverview)
            {
                shellViewModel.OpenUserOverview();
            }

            this.TryClose();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on initialize.
        /// </summary>
        protected override void OnInitialize()
        {
            base.OnInitialize();

            if (this.loginService.AutoLoginHelper.Used)
            {
                this.Username = this.loginService.AutoLoginHelper.Username;
                this.Password = this.loginService.AutoLoginHelper.Password;

                this.Connect();
            }
        }

        #endregion
    }
}