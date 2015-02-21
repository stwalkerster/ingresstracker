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

    using IngressTracker.DataModel.Models;
    using IngressTracker.Properties;
    using IngressTracker.ScreenBase;
    using IngressTracker.Services.Interfaces;
    using IngressTracker.ViewModels.Interfaces;

    using NHibernate;
    using NHibernate.Exceptions;

    /// <summary>
    /// The login view model.
    /// </summary>
    public class LoginViewModel : ScreenBase, ILoginViewModel
    {
        #region Fields

        /// <summary>
        /// The available agents.
        /// </summary>
        private ObservableCollection<User> availableAgents;

        /// <summary>
        /// The selected available agent.
        /// </summary>
        private User selectedAvailableAgent;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        /// <param name="loginService">
        /// The login Service.
        /// </param>
        /// <param name="session">
        /// The session.
        /// </param>
        public LoginViewModel(ILoginService loginService, ISession session)
            : base(Resources.LoginView, session, loginService)
        {
            this.availableAgents = new ObservableCollection<User>();
        }

        #endregion

        #region Public Properties

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

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The login.
        /// </summary>
        public void Login()
        {
            this.LoginService.Agent = this.SelectedAvailableAgent;
            this.LoginService.AvailableAgents = this.AvailableAgents;

            this.LoginService.LoginComplete = true;

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

            try
            {
                var collection = this.DatabaseSession.QueryOver<User>().List();

                this.availableAgents = new ObservableCollection<User>(collection);

                if (collection.Count == 1)
                {
                    this.SelectedAvailableAgent = collection.First();
                    this.Login();
                }

                this.NotifyOfPropertyChange(() => this.AvailableAgents);
            }
            catch (GenericADOException ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        #endregion
    }
}