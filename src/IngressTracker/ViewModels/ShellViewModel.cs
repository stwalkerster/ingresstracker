// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShellViewModel.cs" company="Simon Walker">
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
//   The shell view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using Caliburn.Micro;

    using IngressTracker.DataModel;
    using IngressTracker.Interfaces;
    using IngressTracker.ScreenBase.Interfaces;
    using IngressTracker.Services.Interfaces;
    using IngressTracker.ViewModels.Interfaces;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// The shell view model.
    /// </summary>
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShellViewModel
    {
        #region Fields

        /// <summary>
        /// The login service.
        /// </summary>
        private readonly ILoginService loginService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        /// <param name="loginService">
        /// The login service.
        /// </param>
        public ShellViewModel(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether can add record.
        /// </summary>
        public bool CanAddRecord
        {
            get
            {
                bool allowed = true;

                if (!(this.ActiveItem is ICanAddRecord))
                {
                    return false;
                }

                var sds = this.ActiveItem as IStaticDataScreen;
                if (sds != null)
                {
                    allowed &= sds.UserAllowedEdit;
                }

                return allowed;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can delete.
        /// </summary>
        public bool CanDelete
        {
            get
            {
                bool allowed = true;

                if (!(this.ActiveItem is ICanDelete))
                {
                    return false;
                }

                var sds = this.ActiveItem as IStaticDataScreen;
                if (sds != null)
                {
                    allowed &= sds.UserAllowedEdit;
                }

                return allowed;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can open badge awards.
        /// </summary>
        public bool CanOpenBadgeAwards
        {
            get
            {
                return this.loginService.LoginComplete;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can open badge static.
        /// </summary>
        public bool CanOpenBadgeStatic
        {
            get
            {
                return this.loginService.LoginComplete;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can open category static.
        /// </summary>
        public bool CanOpenCategoryStatic
        {
            get
            {
                return this.loginService.LoginComplete;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can open level static.
        /// </summary>
        public bool CanOpenLevelStatic
        {
            get
            {
                return this.loginService.LoginComplete;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can open login.
        /// </summary>
        public bool CanOpenLogin
        {
            get
            {
                return !this.loginService.LoginComplete;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can open raw values.
        /// </summary>
        public bool CanOpenRawValues
        {
            get
            {
                return this.loginService.LoginComplete;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can open stat static.
        /// </summary>
        public bool CanOpenStatStatic
        {
            get
            {
                return this.loginService.LoginComplete;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can open user static.
        /// </summary>
        public bool CanOpenUserStatic
        {
            get
            {
                if (!this.loginService.LoginComplete)
                {
                    return false;
                }

                if (this.loginService.Agent.StaticDataAdmin || this.loginService.Agent.AccessToAllAgents)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can refresh.
        /// </summary>
        public bool CanRefreshData
        {
            get
            {
                return this.ActiveItem is IDataScreen;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can save.
        /// </summary>
        public bool CanSave
        {
            get
            {
                bool allowed = true;

                if (!(this.ActiveItem is ICanSave))
                {
                    return false;
                }

                var sds = this.ActiveItem as IStaticDataScreen;
                if (sds != null)
                {
                    allowed &= sds.UserAllowedEdit;
                }

                return allowed;
            }
        }

        /// <summary>
        /// Gets the current agent faction.
        /// </summary>
        public Faction CurrentAgentFaction
        {
            get
            {
                if (this.loginService.Agent == null)
                {
                    return null;
                }

                return this.loginService.Agent.Faction;
            }
        }

        /// <summary>
        /// Gets the current agent name.
        /// </summary>
        public string CurrentAgentName
        {
            get
            {
                if (this.loginService.Agent == null)
                {
                    return string.Empty;
                }

                return this.loginService.Agent.AgentName;
            }
        }

        /// <summary>
        /// Gets a value indicating whether data tools enabled.
        /// </summary>
        public bool DataToolsEnabled
        {
            get
            {
                return this.CanDelete || this.CanSave || this.CanAddRecord || this.CanRefreshData;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add record.
        /// </summary>
        public void AddRecord()
        {
            if (this.CanAddRecord)
            {
                ((ICanAddRecord)this.ActiveItem).AddRecord();
            }
        }

        /// <summary>
        /// The close window.
        /// </summary>
        /// <param name="window">
        /// The window.
        /// </param>
        public void CloseWindow(IScreen window)
        {
            window.TryClose();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        public void Delete()
        {
            if (this.CanDelete)
            {
                ((ICanDelete)this.ActiveItem).DeleteRecord();
            }
        }

        /// <summary>
        /// The open badge awards.
        /// </summary>
        public void OpenBadgeAwards()
        {
            var window = ServiceLocator.Current.GetInstance<IBadgeAwardViewModel>();
            this.ActivateItem(window);
        }

        /// <summary>
        /// The open badge static.
        /// </summary>
        public void OpenBadgeStatic()
        {
            var window = ServiceLocator.Current.GetInstance<IBadgeStaticViewModel>();
            this.ActivateItem(window);
        }

        /// <summary>
        /// The open category static.
        /// </summary>
        public void OpenCategoryStatic()
        {
            var window = ServiceLocator.Current.GetInstance<ICategoryStaticViewModel>();
            this.ActivateItem(window);
        }

        /// <summary>
        /// The open level static.
        /// </summary>
        public void OpenLevelStatic()
        {
            var window = ServiceLocator.Current.GetInstance<ILevelStaticViewModel>();
            this.ActivateItem(window);
        }

        /// <summary>
        /// The open user static.
        /// </summary>
        public void OpenLogin()
        {
            var window = ServiceLocator.Current.GetInstance<ILoginViewModel>();
            this.ActivateItem(window);
        }

        /// <summary>
        /// The open raw values.
        /// </summary>
        public void OpenRawValues()
        {
            var window = ServiceLocator.Current.GetInstance<IRawValueViewModel>();
            this.ActivateItem(window);
        }

        /// <summary>
        /// The open stat static.
        /// </summary>
        public void OpenStatStatic()
        {
            var window = ServiceLocator.Current.GetInstance<IStatisticsStaticViewModel>();
            this.ActivateItem(window);
        }

        /// <summary>
        /// The open user static.
        /// </summary>
        public void OpenUserStatic()
        {
            var window = ServiceLocator.Current.GetInstance<IUserStaticViewModel>();
            this.ActivateItem(window);
        }

        /// <summary>
        /// The refresh.
        /// </summary>
        public void RefreshData()
        {
            if (this.CanRefreshData)
            {
                ((IDataScreen)this.ActiveItem).RefreshData();
            }
        }

        /// <summary>
        /// The save.
        /// </summary>
        public void Save()
        {
            if (this.CanSave)
            {
                ((ICanSave)this.ActiveItem).Save();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on activation processed.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="success">
        /// The success.
        /// </param>
        protected override void OnActivationProcessed(IScreen item, bool success)
        {
            base.OnActivationProcessed(item, success);

            this.NotifyOfPropertyChange(() => this.CanAddRecord);
            this.NotifyOfPropertyChange(() => this.CanDelete);
            this.NotifyOfPropertyChange(() => this.CanRefreshData);
            this.NotifyOfPropertyChange(() => this.CanSave);
            this.NotifyOfPropertyChange(() => this.DataToolsEnabled);
        }

        #endregion
    }
}