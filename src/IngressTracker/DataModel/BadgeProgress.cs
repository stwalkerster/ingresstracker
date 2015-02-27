// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BadgeProgress.cs" company="Simon Walker">
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
//   The badge progress.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.DataModel
{
    using System;

    using IngressTracker.DataModel.Models;

    /// <summary>
    /// The badge progress.
    /// </summary>
    public class BadgeProgress
    {
        #region Fields

        /// <summary>
        /// The badge.
        /// </summary>
        private readonly Badge badge;

        /// <summary>
        /// The current position.
        /// </summary>
        private readonly long currentPosition;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BadgeProgress"/> class.
        /// </summary>
        /// <param name="badge">
        /// The badge.
        /// </param>
        /// <param name="currentPosition">
        /// The current position.
        /// </param>
        public BadgeProgress(Badge badge, long currentPosition)
        {
            this.badge = badge;
            this.currentPosition = currentPosition;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the badge.
        /// </summary>
        public Badge Badge
        {
            get
            {
                return this.badge;
            }
        }

        /// <summary>
        /// Gets the current level.
        /// </summary>
        public BadgeLevel CurrentLevel
        {
            get
            {
                if (this.badge.Awardable)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (this.badge.Black <= this.currentPosition)
                {
                    return BadgeLevel.Black;
                }

                if (this.badge.Platinum <= this.currentPosition)
                {
                    return BadgeLevel.Platinum;
                }

                if (this.badge.Gold <= this.currentPosition)
                {
                    return BadgeLevel.Gold;
                }

                if (this.badge.Silver <= this.currentPosition)
                {
                    return BadgeLevel.Silver;
                }

                if (this.badge.Bronze <= this.currentPosition)
                {
                    return BadgeLevel.Bronze;
                }

                return BadgeLevel.Locked;
            }
        }

        /// <summary>
        /// Gets the current position.
        /// </summary>
        public long CurrentPosition
        {
            get
            {
                return this.currentPosition;
            }
        }

        /// <summary>
        /// Gets the next level.
        /// </summary>
        public BadgeLevel NextLevel
        {
            get
            {
                if (this.badge.Awardable)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (this.CurrentLevel == BadgeLevel.Locked)
                {
                    return BadgeLevel.Bronze;
                }

                if (this.CurrentLevel == BadgeLevel.Bronze)
                {
                    return BadgeLevel.Silver;
                }

                if (this.CurrentLevel == BadgeLevel.Silver)
                {
                    return BadgeLevel.Gold;
                }

                if (this.CurrentLevel == BadgeLevel.Gold)
                {
                    return BadgeLevel.Platinum;
                }

                if (this.CurrentLevel == BadgeLevel.Platinum)
                {
                    return BadgeLevel.Black;
                }

                if (this.CurrentLevel == BadgeLevel.Black)
                {
                    return null;
                }

                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets the last target.
        /// </summary>
        public long? LastTarget
        {
            get
            {
                if (this.badge.Awardable)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (this.CurrentLevel == BadgeLevel.Locked)
                {
                    return 0;
                }

                if (this.CurrentLevel == BadgeLevel.Bronze)
                {
                    return this.Badge.Bronze;
                }

                if (this.CurrentLevel == BadgeLevel.Silver)
                {
                    return this.Badge.Silver;
                }

                if (this.CurrentLevel == BadgeLevel.Gold)
                {
                    return this.Badge.Gold;
                }

                if (this.CurrentLevel == BadgeLevel.Platinum)
                {
                    return this.Badge.Platinum;
                }

                if (this.CurrentLevel == BadgeLevel.Black)
                {
                    return this.Badge.Black;
                }

                throw new ArgumentOutOfRangeException();

            }
        }

        /// <summary>
        /// Gets the next target.
        /// </summary>
        public long? NextTarget
        {
            get
            {
                if (this.badge.Awardable)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (this.CurrentLevel == BadgeLevel.Locked)
                {
                    return this.Badge.Bronze;
                }

                if (this.CurrentLevel == BadgeLevel.Bronze)
                {
                    return this.Badge.Silver;
                }

                if (this.CurrentLevel == BadgeLevel.Silver)
                {
                    return this.Badge.Gold;
                }

                if (this.CurrentLevel == BadgeLevel.Gold)
                {
                    return this.Badge.Platinum;
                }

                if (this.CurrentLevel == BadgeLevel.Platinum)
                {
                    return this.Badge.Black;
                }

                if (this.CurrentLevel == BadgeLevel.Black)
                {
                    return null;
                }

                throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}