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
    using System.Collections.Generic;
    using System.Linq;

    using IngressTracker.DataModel;
    using IngressTracker.DataModel.Models;
    using IngressTracker.Extensions;
    using IngressTracker.Properties;
    using IngressTracker.Services.Interfaces;

    using NHibernate;

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

        /// <summary>
        /// The statistic progress service.
        /// </summary>
        private readonly IStatisticProgressService statisticProgressService;

        /// <summary>
        /// The badge progresses.
        /// </summary>
        private IDictionary<Badge, BadgeProgress> badgeProgresses = new Dictionary<Badge, BadgeProgress>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BadgeProgressService"/> class.
        /// </summary>
        /// <param name="loginService">
        /// The login service.
        /// </param>
        /// <param name="statisticProgressService">
        /// The statistic Progress Service.
        /// </param>
        /// <param name="databaseSession">
        /// The database Session.
        /// </param>
        public BadgeProgressService(
            ILoginService loginService, 
            IStatisticProgressService statisticProgressService, 
            ISession databaseSession)
        {
            this.loginService = loginService;
            this.statisticProgressService = statisticProgressService;
            this.databaseSession = databaseSession;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get next.
        /// </summary>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <param name="level">
        /// The level.
        /// </param>
        /// <param name="weekMode">
        /// The week Mode.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public DateTime? GetNext(int count, BadgeLevel level, bool weekMode)
        {
            this.UpdateProgressCache();

            var badgeDates =
                this.badgeProgresses.Values.Where(x => x.CurrentLevel < level)
                    .Select(
                        x =>
                        new StatisticProgress(x.CurrentValue, x.MonthValue, x.WeekValue)
                            {
                                NextTarget =
                                    x.Badge.GetNextTarget(
                                        level)
                            })
                    .Select(x => weekMode ? x.NextTargetDateWeek : x.NextTargetDateMonth)
                    .Where(x => x.HasValue)
                    .OrderBy(x => x.GetValueOrDefault())
                    .ToList();

            if (badgeDates.Count < count)
            {
                // not enough to predict
                return null;
            }

            return badgeDates.Take(count).Max();
        }

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

            this.UpdateProgressCache();

            if (this.badgeProgresses.ContainsKey(badge))
            {
                return this.badgeProgresses[badge];
            }

            return this.QueryBadgeProgress(badge);
        }

        /// <summary>
        /// The refresh.
        /// </summary>
        public void Refresh()
        {
            this.badgeProgresses = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The query badge progress.
        /// </summary>
        /// <param name="badge">
        /// The badge.
        /// </param>
        /// <returns>
        /// The <see cref="BadgeProgress"/>.
        /// </returns>
        private BadgeProgress QueryBadgeProgress(Badge badge)
        {
            return new BadgeProgress(
                badge, 
                this.statisticProgressService.GetStatisticProgress(this.loginService.Agent, badge.Statistic));
        }

        /// <summary>
        /// The update progress cache.
        /// </summary>
        private void UpdateProgressCache()
        {
            if (this.badgeProgresses == null)
            {
                this.badgeProgresses =
                    this.databaseSession.QueryOver<Badge>()
                        .Where(x => !x.Awardable)
                        .List()
                        .Select(this.QueryBadgeProgress)
                        .ToDictionary(x => x.Badge);
            }
        }

        #endregion
    }
}