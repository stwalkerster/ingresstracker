// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BadgeProgressViewModel.cs" company="Simon Walker">
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
//   The badge progress view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using IngressTracker.DataModel;
    using IngressTracker.DataModel.Models;
    using IngressTracker.Properties;
    using IngressTracker.ScreenBase;
    using IngressTracker.Services.Interfaces;
    using IngressTracker.ViewModels.Interfaces;

    using NHibernate;

    /// <summary>
    /// The badge progress view model.
    /// </summary>
    public class BadgeProgressViewModel : DataScreenBase, IBadgeProgressViewModel
    {
        #region Fields

        /// <summary>
        /// The badge progress service.
        /// </summary>
        private readonly IBadgeProgressService badgeProgressService;

        /// <summary>
        /// The awarded badges.
        /// </summary>
        private IList<BadgeAward> awardedBadges;

        /// <summary>
        /// The badges.
        /// </summary>
        private IEnumerable<BadgeProgress> badges;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BadgeProgressViewModel"/> class.
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
        public BadgeProgressViewModel(
            ISession databaseSession, 
            ILoginService loginService, 
            IBadgeProgressService badgeProgressService)
            : base(Resources.BadgeProgressView, databaseSession, loginService)
        {
            this.badgeProgressService = badgeProgressService;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the awarded badges.
        /// </summary>
        public IList<BadgeAward> AwardedBadges
        {
            get
            {
                return this.awardedBadges;
            }
        }

        /// <summary>
        /// Gets the badges.
        /// </summary>
        public IEnumerable<BadgeProgress> Badges
        {
            get
            {
                return this.badges;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The refresh data async.
        /// </summary>
        protected override void RefreshDataAsync()
        {
            var agent = this.LoginService.Agent;

            this.badges =
                this.DatabaseSession.QueryOver<Badge>()
                    .Where(x => !x.Awardable)
                    .List()
                    .Select(x => this.badgeProgressService.GetProgress(x));

            this.awardedBadges = this.DatabaseSession.QueryOver<BadgeAward>().Where(x => x.Agent == agent).List();
        }

        /// <summary>
        /// The refresh data completed.
        /// </summary>
        protected override void RefreshDataCompleted()
        {
            this.NotifyOfPropertyChange(() => this.Badges);
            this.NotifyOfPropertyChange(() => this.AwardedBadges);
        }

        #endregion
    }
}