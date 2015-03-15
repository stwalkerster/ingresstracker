// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LevelProgressViewModel.cs" company="Simon Walker">
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
//   The level progress view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using IngressTracker.DataModel;
    using IngressTracker.DataModel.Models;
    using IngressTracker.Properties;
    using IngressTracker.ScreenBase;
    using IngressTracker.Services.Interfaces;
    using IngressTracker.ViewModels.Interfaces;

    using NHibernate;
    using NHibernate.Criterion;

    /// <summary>
    /// The level progress view model.
    /// </summary>
    public class LevelProgressViewModel : DataScreenBase, ILevelProgressViewModel
    {
        #region Fields

        /// <summary>
        /// The badge progress service.
        /// </summary>
        private readonly IBadgeProgressService badgeProgressService;

        /// <summary>
        /// The levels.
        /// </summary>
        private IEnumerable<LevelProgress> levels;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="LevelProgressViewModel"/> class.
        /// </summary>
        /// <param name="databaseSession">
        /// The database session.
        /// </param>
        /// <param name="loginService">
        /// The login service.
        /// </param>
        /// <param name="badgeProgressService">
        /// The badge Progress Service.
        /// </param>
        public LevelProgressViewModel(
            ISession databaseSession, 
            ILoginService loginService, 
            IBadgeProgressService badgeProgressService)
            : base(Resources.LevelProgressView, databaseSession, loginService)
        {
            this.badgeProgressService = badgeProgressService;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the access points.
        /// </summary>
        public long AccessPoints { get; set; }

        /// <summary>
        /// Gets or sets the black count.
        /// </summary>
        public int BlackCount { get; set; }

        /// <summary>
        /// Gets or sets the bronze count.
        /// </summary>
        public int BronzeCount { get; set; }

        /// <summary>
        /// Gets or sets the gold count.
        /// </summary>
        public int GoldCount { get; set; }

        /// <summary>
        /// Gets the levels.
        /// </summary>
        public IEnumerable<LevelProgress> Levels
        {
            get
            {
                return this.levels;
            }
        }

        /// <summary>
        /// Gets or sets the platinum count.
        /// </summary>
        public int PlatinumCount { get; set; }

        /// <summary>
        /// Gets or sets the silver count.
        /// </summary>
        public int SilverCount { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The refresh data async.
        /// </summary>
        protected override void RefreshDataAsync()
        {
            var levelProgressList =
                this.DatabaseSession.QueryOver<Level>().List().Select(x => new LevelProgress(x, this.LoginService));

            var levels = new List<LevelProgress>();

            foreach (var levelProgress in levelProgressList)
            {
                LevelProgress progress = levelProgress;
                var lastLevel =
                    this.DatabaseSession.QueryOver<Level>()
                        .Where(x => x.LevelNumber == progress.Level - 1)
                        .Take(1)
                        .SingleOrDefault();

                if (lastLevel == null)
                {
                    progress.LastAccessPoints = 0;
                    progress.LastGold = 0;
                    progress.LastSilver = 0;
                    progress.LastPlatinum = 0;
                    progress.LastBlack = 0;
                }
                else
                {
                    progress.LastAccessPoints = lastLevel.AccessPoints;
                    progress.LastSilver = lastLevel.SilverBadges;
                    progress.LastGold = lastLevel.GoldBadges;
                    progress.LastPlatinum = lastLevel.PlatinumBadges;
                    progress.LastBlack = lastLevel.BlackBadges;
                }

                levels.Add(progress);
            }

            var badges =
                this.DatabaseSession.QueryOver<Badge>()
                    .Where(x => !x.Awardable)
                    .List()
                    .Select(x => this.badgeProgressService.GetProgress(x))
                    .ToList();

            var awardedBadges =
                this.DatabaseSession.QueryOver<BadgeAward>().Where(x => x.Agent == this.LoginService.Agent).List();

            this.BlackCount = badges.Count(x => x.CurrentLevel == BadgeLevel.Black)
                              + awardedBadges.Count(x => x.Level == BadgeLevel.Black);
            this.PlatinumCount = this.BlackCount + badges.Count(x => x.CurrentLevel == BadgeLevel.Platinum)
                                 + awardedBadges.Count(x => x.Level == BadgeLevel.Platinum);
            this.GoldCount = this.PlatinumCount + badges.Count(x => x.CurrentLevel == BadgeLevel.Gold)
                             + awardedBadges.Count(x => x.Level == BadgeLevel.Gold);
            this.SilverCount = this.GoldCount + badges.Count(x => x.CurrentLevel == BadgeLevel.Silver)
                               + awardedBadges.Count(x => x.Level == BadgeLevel.Silver);
            this.BronzeCount = this.SilverCount + badges.Count(x => x.CurrentLevel == BadgeLevel.Bronze)
                               + awardedBadges.Count(x => x.Level == BadgeLevel.Bronze);

            var agent = this.LoginService.Agent;

            Stat apstat = this.DatabaseSession.QueryOver<Stat>().Where(s => s.IsAccessPointsStat).SingleOrDefault();

            QueryOver<ValueEntry, ValueEntry> newestStat =
                QueryOver.Of<ValueEntry>()
                    .SelectList(p => p.SelectMax(x => x.Timestamp))
                    .Where(c => c.Agent == agent && c.Statistic == apstat);

            ValueEntry value =
                this.DatabaseSession.QueryOver<ValueEntry>()
                    .Where(c => c.Agent == agent && c.Statistic == apstat)
                    .WithSubquery.Where(x => x.Timestamp == newestStat.As<DateTime>())
                    .SingleOrDefault();

            this.AccessPoints = value == null ? 0 : value.Value.GetValueOrDefault();

            this.levels = levels;
        }

        /// <summary>
        /// The refresh data completed.
        /// </summary>
        protected override void RefreshDataCompleted()
        {
            this.NotifyOfPropertyChange(() => this.Levels);
        }

        #endregion
    }
}