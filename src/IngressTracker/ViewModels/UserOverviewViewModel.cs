// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserOverviewViewModel.cs" company="Simon Walker">
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
//   The user overview view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using System;
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
    /// The user overview view model.
    /// </summary>
    public class UserOverviewViewModel : ScreenBase, IUserOverviewViewModel
    {
        #region Fields

        /// <summary>
        /// The badge progress service.
        /// </summary>
        private readonly IBadgeProgressService badgeProgressService;

        /// <summary>
        /// The level progress service.
        /// </summary>
        private readonly ILevelProgressService levelProgressService;

        /// <summary>
        /// The access points.
        /// </summary>
        private long accessPoints;

        /// <summary>
        /// The black count.
        /// </summary>
        private int blackCount;

        /// <summary>
        /// The bronze count.
        /// </summary>
        private int bronzeCount;

        /// <summary>
        /// The gold count.
        /// </summary>
        private int goldCount;

        /// <summary>
        /// The level.
        /// </summary>
        private int level;

        /// <summary>
        /// The max AP.
        /// </summary>
        private int maxAp;

        /// <summary>
        /// The platinum count.
        /// </summary>
        private int platinumCount;

        /// <summary>
        /// The silver count.
        /// </summary>
        private int silverCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="UserOverviewViewModel"/> class.
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
        /// <param name="levelProgressService">
        /// The level Progress Service.
        /// </param>
        public UserOverviewViewModel(
            ISession databaseSession, 
            ILoginService loginService, 
            IBadgeProgressService badgeProgressService, 
            ILevelProgressService levelProgressService)
            : base(Resources.UserOverviewView, databaseSession, loginService)
        {
            this.badgeProgressService = badgeProgressService;
            this.levelProgressService = levelProgressService;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the access points.
        /// </summary>
        public long AccessPoints
        {
            get
            {
                return this.accessPoints;
            }

            set
            {
                if (value == this.accessPoints)
                {
                    return;
                }

                this.accessPoints = value;
                this.NotifyOfPropertyChange(() => this.AccessPoints);
            }
        }

        /// <summary>
        /// Gets the agent name.
        /// </summary>
        public string AgentName
        {
            get
            {
                return this.LoginService.Agent.AgentName;
            }
        }

        /// <summary>
        /// Gets or sets the black count.
        /// </summary>
        public int BlackCount
        {
            get
            {
                return this.blackCount;
            }

            set
            {
                if (value == this.blackCount)
                {
                    return;
                }

                this.blackCount = value;
                this.NotifyOfPropertyChange(() => this.BlackCount);
            }
        }

        /// <summary>
        /// Gets or sets the bronze count.
        /// </summary>
        public int BronzeCount
        {
            get
            {
                return this.bronzeCount;
            }

            set
            {
                if (value == this.bronzeCount)
                {
                    return;
                }

                this.bronzeCount = value;
                this.NotifyOfPropertyChange(() => this.BronzeCount);
            }
        }

        /// <summary>
        /// Gets or sets the gold count.
        /// </summary>
        public int GoldCount
        {
            get
            {
                return this.goldCount;
            }

            set
            {
                if (value == this.goldCount)
                {
                    return;
                }

                this.goldCount = value;
                this.NotifyOfPropertyChange(() => this.GoldCount);
            }
        }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        public int Level
        {
            get
            {
                return this.level;
            }

            set
            {
                if (value == this.level)
                {
                    return;
                }

                this.level = value;
                this.NotifyOfPropertyChange(() => this.Level);
            }
        }

        /// <summary>
        /// Gets or sets the max AP.
        /// </summary>
        public int MaxAP
        {
            get
            {
                return this.maxAp;
            }

            set
            {
                if (value == this.maxAp)
                {
                    return;
                }

                this.maxAp = value;
                this.NotifyOfPropertyChange(() => this.MaxAP);
            }
        }

        /// <summary>
        /// Gets or sets the platinum count.
        /// </summary>
        public int PlatinumCount
        {
            get
            {
                return this.platinumCount;
            }

            set
            {
                if (value == this.platinumCount)
                {
                    return;
                }

                this.platinumCount = value;
                this.NotifyOfPropertyChange(() => this.PlatinumCount);
            }
        }

        /// <summary>
        /// Gets or sets the silver count.
        /// </summary>
        public int SilverCount
        {
            get
            {
                return this.silverCount;
            }

            set
            {
                if (value == this.silverCount)
                {
                    return;
                }

                this.silverCount = value;
                this.NotifyOfPropertyChange(() => this.SilverCount);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The refresh data.
        /// </summary>
        public void RefreshData()
        {
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

            this.AccessPoints = value == null ? 0 : value.Value.Value;

            var currentLevel = this.levelProgressService.GetCurrentLevel(
                this.AccessPoints, 
                this.SilverCount, 
                this.GoldCount, 
                this.PlatinumCount, 
                this.BlackCount);

            var nextLevel = this.levelProgressService.GetNextLevel(currentLevel);

            this.Level = currentLevel.LevelNumber;
            this.MaxAP = nextLevel.AccessPoints;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on initialize.
        /// </summary>
        protected override void OnInitialize()
        {
            base.OnInitialize();

            this.RefreshData();
        }

        #endregion
    }
}