// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RawValueViewModel.cs" company="Simon Walker">
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
//   The raw value view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using IngressTracker.DataModel.Models;
    using IngressTracker.Properties;
    using IngressTracker.ScreenBase;
    using IngressTracker.Services.Interfaces;
    using IngressTracker.ViewModels.Interfaces;

    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// The raw value view model.
    /// </summary>
    public class RawValueViewModel : EditableDataScreenBase<ValueEntry>, IRawValueViewModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="RawValueViewModel"/> class.
        /// </summary>
        /// <param name="databaseSession">
        /// The database session.
        /// </param>
        /// <param name="loginService">
        /// The login service.
        /// </param>
        public RawValueViewModel(ISession databaseSession, ILoginService loginService)
            : base(Resources.RawValuesView, databaseSession, loginService)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the agents.
        /// </summary>
        public IEnumerable<User> Agents { get; private set; }

        /// <summary>
        /// Gets the statistics.
        /// </summary>
        public IEnumerable<Stat> Statistics { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The refresh data.
        /// </summary>
        protected override void RefreshDataAsync()
        {
            base.RefreshDataAsync();

            this.Agents = this.DatabaseSession.Query<User>().ToList();
            this.NotifyOfPropertyChange(() => this.Agents);

            this.Statistics = this.DatabaseSession.Query<Stat>().ToList();
            this.NotifyOfPropertyChange(() => this.Statistics);
        }

        /// <summary>
        /// The get data.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{ValueEntry}"/>.
        /// </returns>
        protected override IEnumerable<ValueEntry> GetData()
        {
            var agent = this.LoginService.Agent;

            return this.DatabaseSession.QueryOver<ValueEntry>().Where(x => x.Agent == agent).List();
        }

        #endregion
    }
}