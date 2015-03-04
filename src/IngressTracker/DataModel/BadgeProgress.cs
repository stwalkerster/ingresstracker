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
        /// The current value.
        /// </summary>
        private readonly ValueEntry currentValue;
        
        /// <summary>
        /// The month value.
        /// </summary>
        private readonly ValueEntry monthValue;

        /// <summary>
        /// The week value.
        /// </summary>
        private readonly ValueEntry weekValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BadgeProgress"/> class.
        /// </summary>
        /// <param name="badge">
        /// The badge.
        /// </param>
        /// <param name="currentValue">
        /// The value Entry.
        /// </param>
        /// <param name="weekValue">
        /// The week Value.
        /// </param>
        /// <param name="monthValue">
        /// The month Value.
        /// </param>
        public BadgeProgress(Badge badge, ValueEntry currentValue, ValueEntry weekValue, ValueEntry monthValue)
        {
            this.badge = badge;
            this.currentValue = currentValue;
            this.weekValue = weekValue;
            this.monthValue = monthValue;
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

                if (this.badge.Black <= this.CurrentPosition)
                {
                    return BadgeLevel.Black;
                }

                if (this.badge.Platinum <= this.CurrentPosition)
                {
                    return BadgeLevel.Platinum;
                }

                if (this.badge.Gold <= this.CurrentPosition)
                {
                    return BadgeLevel.Gold;
                }

                if (this.badge.Silver <= this.CurrentPosition)
                {
                    return BadgeLevel.Silver;
                }

                if (this.badge.Bronze <= this.CurrentPosition)
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
                return this.currentValue == null ? 0 : this.currentValue.Value.GetValueOrDefault(0);
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

        /// <summary>
        /// Gets the next target date month.
        /// </summary>
        public DateTime? NextTargetDateMonth
        {
            get
            {
                if (this.monthValue == null || this.NextTarget == null)
                {
                    return null;
                }

                var prediction = this.PredictValue(this.monthValue);

                if (prediction == null)
                {
                    return null;
                }

                return this.monthValue.Timestamp.Add(prediction.Value);
            }
        }

        /// <summary>
        /// Gets the next target date week.
        /// </summary>
        public DateTime? NextTargetDateWeek
        {
            get
            {
                if (this.weekValue == null || this.NextTarget == null)
                {
                    return null;
                }

                var prediction = this.PredictValue(this.weekValue);

                if (prediction == null)
                {
                    return null;
                }

                return this.weekValue.Timestamp.Add(prediction.Value);
            }
        }

        /// <summary>
        /// The predict value.
        /// </summary>
        /// <param name="historical">
        /// The historical.
        /// </param>
        /// <returns>
        /// The <see cref="TimeSpan"/> from the historical position.
        /// </returns>
        private TimeSpan? PredictValue(ValueEntry historical)
        {
            // Okay. Maths time.
            // y = mx + b.
            // Let x = deltaTimeTicks, 0
            long deltaTimeTicks = (this.currentValue.Timestamp - historical.Timestamp).Ticks;

            // let y = deltaValue, 0
            long deltaValue = this.currentValue.Value.GetValueOrDefault(0) - historical.Value.GetValueOrDefault(0);

            if (deltaValue == 0)
            {
                // urm... nothing's happened. Return null so we don't get a nullrefexception.
                return null;
            }

            long requiredDelta = this.NextTarget.GetValueOrDefault(0) - this.currentValue.Value.GetValueOrDefault(0);

            // y = mx + b. deltas - point is (0,0). 0 = 0m + b, b = 0.
            // y = mx, m = y/x
            decimal m = (decimal)deltaValue / deltaTimeTicks;

            // y = mx + b, requiredValue / m = prediction
            return new TimeSpan((long)Math.Ceiling(requiredDelta / m));
        }

        #endregion
    }
}