// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LevelProgress.cs" company="Simon Walker">
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
//   The level progress.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.DataModel
{
    using IngressTracker.DataModel.Models;
    using IngressTracker.Services;
    using IngressTracker.Services.Interfaces;

    /// <summary>
    /// The level progress.
    /// </summary>
    public class LevelProgress
    {
        #region Fields

        /// <summary>
        /// The level.
        /// </summary>
        private readonly Level level;

        /// <summary>
        /// The login service.
        /// </summary>
        private readonly ILoginService loginService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="LevelProgress"/> class.
        /// </summary>
        /// <param name="level">
        /// The level.
        /// </param>
        /// <param name="loginService">
        /// The login Service.
        /// </param>
        public LevelProgress(Level level, ILoginService loginService)
        {
            this.level = level;
            this.loginService = loginService;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the access points.
        /// </summary>
        public int AccessPoints
        {
            get
            {
                return this.level.AccessPoints;
            }
        }

        /// <summary>
        /// Gets the black badges.
        /// </summary>
        public int BlackBadges
        {
            get
            {
                return this.level.BlackBadges;
            }
        }

        /// <summary>
        /// Gets the gold badges.
        /// </summary>
        public int GoldBadges
        {
            get
            {
                return this.level.GoldBadges;
            }
        }

        /// <summary>
        /// Gets the level.
        /// </summary>
        public int Level
        {
            get
            {
                return this.level.LevelNumber;
            }
        }

        /// <summary>
        /// Gets the platinum badges.
        /// </summary>
        public int PlatinumBadges
        {
            get
            {
                return this.level.PlatinumBadges;
            }
        }

        /// <summary>
        /// Gets the silver badges.
        /// </summary>
        public int SilverBadges
        {
            get
            {
                return this.level.SilverBadges;
            }
        }

        public ILoginService LoginService 
        {
            get
            {
                return this.loginService;
            }
        }

        public int LastAccessPoints { get; set; }

        public int LastGold { get; set; }

        public int LastSilver { get; set; }

        public int LastPlatinum { get; set; }

        public int LastBlack { get; set; }

        #endregion
    }
}