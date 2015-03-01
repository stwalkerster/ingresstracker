// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnterStatsViewModel.cs" company="Simon Walker">
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
//   The enter stats view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IngressTracker.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    using IngressTracker.DataModel;
    using IngressTracker.DataModel.Models;
    using IngressTracker.Properties;
    using IngressTracker.ScreenBase;
    using IngressTracker.Services.Interfaces;
    using IngressTracker.ViewModels.Interfaces;

    using NHibernate;

    /// <summary>
    /// The enter stats view model.
    /// </summary>
    public class EnterStatsViewModel : ScreenBase, IEnterStatsViewModel
    {
        #region Fields

        /// <summary>
        /// The value entries.
        /// </summary>
        private ObservableCollection<NewValueEntryContainer> valueEntries;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="EnterStatsViewModel"/> class.
        /// </summary>
        /// <param name="databaseSession">
        /// The database session.
        /// </param>
        /// <param name="loginService">
        /// The login service.
        /// </param>
        public EnterStatsViewModel(ISession databaseSession, ILoginService loginService)
            : base(Resources.EnterStatsView, databaseSession, loginService)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the value entries.
        /// </summary>
        public IEnumerable<NewValueEntryContainer> ValueEntries
        {
            get
            {
                return this.valueEntries;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add record.
        /// </summary>
        public void AddRecord()
        {
            this.valueEntries = new ObservableCollection<NewValueEntryContainer>(
                this.DatabaseSession.QueryOver<Stat>()
                .OrderBy(x => x.DisplayOrder).Asc
                    .List()
                    .Select(
                        x => new NewValueEntryContainer { Agent = this.LoginService.Agent, Statistic = x, Timestamp = DateTime.Now }));

            foreach (var container in this.valueEntries)
            {
                NewValueEntryContainer nvec = container;
                var lastStatValue = this.DatabaseSession.QueryOver<ValueEntry>()
                    .Where(x => x.Agent == this.LoginService.Agent && x.Statistic == nvec.Statistic)
                    .OrderBy(x => x.Timestamp)
                    .Desc.Take(1).SingleOrDefault();

                container.OldValue = lastStatValue == null ? null : lastStatValue.Value;
            }

            this.NotifyOfPropertyChange(() => this.ValueEntries);
        }

        /// <summary>
        /// The refresh data.
        /// </summary>
        public void RefreshData()
        {
            this.AddRecord();
        }

        /// <summary>
        /// The save.
        /// </summary>
        public void Save()
        {
            if (this.valueEntries.Count(x => x.Value == null) != 0)
            {
                MessageBox.Show(Resources.NotAllStatsSpecified);
                return;
            }

            foreach (var valueEntry in this.ValueEntries)
            {
                this.DatabaseSession.Save(valueEntry.ValueEntry);
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

            this.AddRecord();
        }

        #endregion
    }
}