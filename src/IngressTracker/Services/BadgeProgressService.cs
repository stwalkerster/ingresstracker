// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BadgeProgressService.cs" company="Simon Walker">
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
//   The badge progress service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IngressTracker.Services
{
    using System;

    using IngressTracker.DataModel;
    using IngressTracker.DataModel.Models;
    using IngressTracker.Properties;
    using IngressTracker.Services.Interfaces;

    using NHibernate;
    using NHibernate.Criterion;

    /// <summary>
    /// The badge progress service.
    /// </summary>
    public class BadgeProgressService : IBadgeProgressService
    {
        #region Fields

        /// <summary>
        /// The database session.
        /// </summary>
        private readonly ISession databaseSession;

        /// <summary>
        /// The login service.
        /// </summary>
        private readonly ILoginService loginService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BadgeProgressService"/> class.
        /// </summary>
        /// <param name="loginService">
        /// The login service.
        /// </param>
        /// <param name="databaseSession">
        /// The database session.
        /// </param>
        public BadgeProgressService(ILoginService loginService, ISession databaseSession)
        {
            this.loginService = loginService;
            this.databaseSession = databaseSession;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get progress.
        /// </summary>
        /// <param name="badge">
        /// The badge.
        /// </param>
        /// <returns>
        /// The <see cref="BadgeProgress"/>.
        /// </returns>
        public BadgeProgress GetProgress(Badge badge)
        {
            if (badge.Awardable)
            {
                throw new ArgumentOutOfRangeException("badge", Resources.BadgeIsAwardable);
            }

            var agent = this.loginService.Agent;
            var statistic = badge.Statistic;

            QueryOver<ValueEntry, ValueEntry> newestStat =
                QueryOver.Of<ValueEntry>()
                    .SelectList(p => p.SelectMax(x => x.Timestamp))
                    .Where(c => c.Agent == agent && c.Statistic == statistic);

            ValueEntry value =
                this.databaseSession.QueryOver<ValueEntry>()
                    .Where(c => c.Agent == agent && c.Statistic == statistic)
                    .WithSubquery.Where(x => x.Timestamp == newestStat.As<DateTime>())
                    .SingleOrDefault();

            if (value == null)
            {
                return new BadgeProgress(badge, null, null, null);
            }

            var weekTime = value.Timestamp.AddDays(-7);
            var monthTime = value.Timestamp.AddDays(-28);

            QueryOver<ValueEntry, ValueEntry> week =
                QueryOver.Of<ValueEntry>()
                    .SelectList(p => p.SelectMax(x => x.Timestamp))
                    .Where(c => c.Agent == agent && c.Statistic == statistic && c.Timestamp < weekTime);

            QueryOver<ValueEntry, ValueEntry> month =
                QueryOver.Of<ValueEntry>()
                    .SelectList(p => p.SelectMax(x => x.Timestamp))
                    .Where(c => c.Agent == agent && c.Statistic == statistic && c.Timestamp < monthTime);

            ValueEntry weekValue =
                this.databaseSession.QueryOver<ValueEntry>()
                    .Where(c => c.Agent == agent && c.Statistic == statistic)
                    .WithSubquery.Where(x => x.Timestamp == week.As<DateTime>())
                    .SingleOrDefault();

            ValueEntry monthValue =
                this.databaseSession.QueryOver<ValueEntry>()
                    .Where(c => c.Agent == agent && c.Statistic == statistic)
                    .WithSubquery.Where(x => x.Timestamp == month.As<DateTime>())
                    .SingleOrDefault();
            
            return new BadgeProgress(badge, value, weekValue, monthValue);
        }

        #endregion
    }
}