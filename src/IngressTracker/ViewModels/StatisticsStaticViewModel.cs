// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatisticsStaticViewModel.cs" company="Simon Walker">
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
//   The stats static view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;

    using IngressTracker.DataModel.Models;
    using IngressTracker.Persistence.Interfaces;
    using IngressTracker.Properties;
    using IngressTracker.ScreenBase;
    using IngressTracker.Services.Interfaces;
    using IngressTracker.ViewModels.Interfaces;

    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// The stats static view model.
    /// </summary>
    public class StatisticsStaticViewModel : StaticDataScreen<Stat>, IStatisticsStaticViewModel
    {
        #region Fields

        /// <summary>
        /// The categories.
        /// </summary>
        private ObservableCollection<Category> categories;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="StatisticsStaticViewModel"/> class.
        /// </summary>
        /// <param name="databaseSession">
        /// The database session.
        /// </param>
        /// <param name="loginService">
        /// The login Service.
        /// </param>
        public StatisticsStaticViewModel(ISession databaseSession, ILoginService loginService)
            : base(Resources.StatStaticView, databaseSession, loginService)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the categories.
        /// </summary>
        public ObservableCollection<Category> Categories
        {
            get
            {
                return this.categories ?? new ObservableCollection<Category>();
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

            var queryable = this.DatabaseSession.Query<Category>();
            this.categories = new ObservableCollection<Category>(queryable);
            this.NotifyOfPropertyChange(() => this.Categories);
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
            var dataEntity = (Stat)entity;

            var messageBoxText = string.Format(
                Resources.DeleteStatisticConfirmation,
                dataEntity.Description);

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

        /// <summary>
        /// The do delete.
        /// </summary>
        /// <param name="deletedItem">
        /// The deleted item.
        /// </param>
        protected override void DoDelete(Stat deletedItem)
        {
            var badges = this.DatabaseSession.QueryOver<Badge>().Where(x => x.Statistic == deletedItem).List();

            foreach (var badge in badges)
            {
                badge.Statistic = null;
                badge.Awardable = true;
                this.DatabaseSession.Update(badge);
            }

            this.DatabaseSession.Delete(deletedItem);
        }

        #endregion
    }
}