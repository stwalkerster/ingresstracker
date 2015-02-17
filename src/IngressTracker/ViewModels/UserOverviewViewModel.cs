// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserOverviewViewModel.cs" company="Simon Walker">
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
//   The user overview view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using IngressTracker.DataModel;
    using IngressTracker.Properties;
    using IngressTracker.ScreenBase;
    using IngressTracker.Services.Interfaces;
    using IngressTracker.ViewModels.Interfaces;

    using NHibernate;

    /// <summary>
    /// The user overview view model.
    /// </summary>
    public class UserOverviewViewModel : ScreenBase, IUserOverviewViewModel
    {
        #region Fields

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
        /// The platinum count.
        /// </summary>
        private int platinumCount;

        /// <summary>
        /// The silver count.
        /// </summary>
        private int silverCount;

        private int accessPoints;

        private int maxAp;

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
        public UserOverviewViewModel(ISession databaseSession, ILoginService loginService)
            : base(Resources.UserOverviewView, databaseSession, loginService)
        {
            this.level = 11;
            this.bronzeCount = 16;
            this.silverCount = 14;
            this.goldCount = 4;
            this.platinumCount = 3;
            this.blackCount = 2;
            this.accessPoints = 1000000;
            this.maxAp = 1200000;
        }

        #endregion

        #region Public Properties

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
        /// Gets the level.
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

        public int AccessPoints
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
        /// Gets the faction.
        /// </summary>
        public Faction Faction
        {
            get
            {
                return this.LoginService.Agent.Faction;
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
        /// Gets the level.
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
        /// Gets the level.
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
    }
}