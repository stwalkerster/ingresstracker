// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BadgeStaticViewModel.cs" company="Simon Walker">
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
//   The badge static view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;

    using Caliburn.Micro;

    using IngressTracker.DataModel.Models;
    using IngressTracker.Persistence.Interfaces;
    using IngressTracker.Properties;
    using IngressTracker.ScreenBase;
    using IngressTracker.Services.Interfaces;
    using IngressTracker.ViewModels.Interfaces;

    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// The badge static view model.
    /// </summary>
    public class BadgeStaticViewModel : StaticDataScreen<Badge>, IBadgeStaticViewModel
    {
        #region Fields

        /// <summary>
        /// The stats.
        /// </summary>
        private ObservableCollection<Stat> stats;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BadgeStaticViewModel"/> class.
        /// </summary>
        /// <param name="databaseSession">
        /// The database session.
        /// </param>
        /// <param name="loginService">
        /// The login Service.
        /// </param>
        public BadgeStaticViewModel(ISession databaseSession, ILoginService loginService)
            : base(Resources.BadgeStaticView, databaseSession, loginService)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the statistics.
        /// </summary>
        public ObservableCollection<Stat> Statistics
        {
            get
            {
                return this.stats ?? new ObservableCollection<Stat>();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The refresh data.
        /// </summary>
        protected override void RefreshDataAsync()
        {
            base.RefreshDataAsync();

            var enumarable = this.DatabaseSession.Query<Stat>();
            this.stats = new ObservableCollection<Stat>(enumarable);
            this.NotifyOfPropertyChange(() => this.Statistics);
        }

        /// <summary>
        /// The pre delete guard.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool PreDeleteGuard(IDataEntity entity)
        {
            var dataEntity = (Badge)entity;

            var messageBoxText = string.Format(
                Resources.DeleteBadgeMessage,
                dataEntity.Name);

            var messageBoxResult = MessageBox.Show(
                messageBoxText,
                Resources.DeleteAreYouSure,
                MessageBoxButton.YesNo,
                MessageBoxImage.Exclamation,
                MessageBoxResult.No);

            if (messageBoxResult == MessageBoxResult.No)
            {
                return true;
            }

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                return false;
            }

            throw new NotSupportedException();
        }

        #endregion
    }
}