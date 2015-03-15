// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LevelProgressService.cs" company="Simon Walker">
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
//   The level progress service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Services
{
    using IngressTracker.DataModel.Models;
    using IngressTracker.Services.Interfaces;

    using NHibernate;

    /// <summary>
    /// The level progress service.
    /// </summary>
    public class LevelProgressService : ILevelProgressService
    {
        #region Fields

        /// <summary>
        /// The database session.
        /// </summary>
        private readonly ISession databaseSession;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="LevelProgressService"/> class.
        /// </summary>
        /// <param name="databaseSession">
        /// The database session.
        /// </param>
        public LevelProgressService(ISession databaseSession)
        {
            this.databaseSession = databaseSession;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get current level.
        /// </summary>
        /// <param name="ap">
        /// The AP.
        /// </param>
        /// <param name="silver">
        /// The silver.
        /// </param>
        /// <param name="gold">
        /// The gold.
        /// </param>
        /// <param name="platinum">
        /// The platinum.
        /// </param>
        /// <param name="black">
        /// The black.
        /// </param>
        /// <returns>
        /// The <see cref="Level"/>.
        /// </returns>
        public Level GetCurrentLevel(long ap, int silver, int gold, int platinum, int black)
        {
            var getLevel =
                this.databaseSession.QueryOver<Level>()
                    .Where(x => x.AccessPoints <= ap)
                    .Where(x => x.BlackBadges <= black)
                    .Where(x => x.PlatinumBadges <= platinum)
                    .Where(x => x.GoldBadges <= gold)
                    .Where(x => x.SilverBadges <= silver)
                    .OrderBy(x => x.LevelNumber)
                    .Desc;
            var level = getLevel.Take(1).SingleOrDefault();

            return level;
        }

        /// <summary>
        /// The get next level.
        /// </summary>
        /// <param name="currentLevel">
        /// The current Level.
        /// </param>
        /// <returns>
        /// The <see cref="Level"/>.
        /// </returns>
        public Level GetNextLevel(Level currentLevel)
        {
            var level =
                this.databaseSession.QueryOver<Level>()
                    .Where(x => x.LevelNumber > currentLevel.LevelNumber)
                    .Take(1)
                    .SingleOrDefault();

            return level;
        }

        #endregion
    }
}