// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatisticProgress.cs" company="Simon Walker">
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
//   The statistic progress.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.DataModel
{
    using System;

    using IngressTracker.DataModel.Models;

    /// <summary>
    /// The statistic progress.
    /// </summary>
    public class StatisticProgress : ICloneable
    {
        #region Fields

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

        /// <summary>
        /// The month prediction.
        /// </summary>
        private DateTime? monthPrediction;

        /// <summary>
        /// The month prediction done.
        /// </summary>
        private bool monthPredictionDone;

        /// <summary>
        /// The next target.
        /// </summary>
        private long? nextTarget;

        /// <summary>
        /// The week prediction.
        /// </summary>
        private DateTime? weekPrediction;

        /// <summary>
        /// The week prediction done.
        /// </summary>
        private bool weekPredictionDone;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="StatisticProgress"/> class.
        /// </summary>
        /// <param name="currentValue">
        /// The current value.
        /// </param>
        /// <param name="monthValue">
        /// The month value.
        /// </param>
        /// <param name="weekValue">
        /// The week value.
        /// </param>
        public StatisticProgress(ValueEntry currentValue, ValueEntry monthValue, ValueEntry weekValue)
        {
            this.currentValue = currentValue;
            this.monthValue = monthValue;
            this.weekValue = weekValue;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current position.
        /// </summary>
        public long CurrentPosition
        {
            get
            {
                return this.CurrentValue == null ? 0 : this.CurrentValue.Value.GetValueOrDefault(0);
            }
        }

        /// <summary>
        /// Gets the current value.
        /// </summary>
        public ValueEntry CurrentValue
        {
            get
            {
                return this.currentValue;
            }
        }

        /// <summary>
        /// Gets the month value.
        /// </summary>
        public ValueEntry MonthValue
        {
            get
            {
                return this.monthValue;
            }
        }

        /// <summary>
        /// Gets or sets the next target.
        /// </summary>
        public virtual long? NextTarget
        {
            get
            {
                return this.nextTarget;
            }

            set
            {
                this.nextTarget = value;
                this.weekPredictionDone = false;
                this.monthPredictionDone = false;
            }
        }

        /// <summary>
        /// Gets the next target date month.
        /// </summary>
        public DateTime? NextTargetDateMonth
        {
            get
            {
                if (!this.monthPredictionDone)
                {
                    if (this.MonthValue == null || this.NextTarget == null)
                    {
                        this.monthPrediction = null;
                        this.monthPredictionDone = true;
                        return null;
                    }

                    var prediction = this.PredictValue(this.MonthValue);

                    if (prediction == null)
                    {
                        this.monthPrediction = null;
                        this.monthPredictionDone = true;
                        return null;
                    }

                    try
                    {
                        this.monthPrediction = this.MonthValue.Timestamp.Add(prediction.Value);
                        this.monthPredictionDone = true;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        this.monthPrediction = null;
                        this.monthPredictionDone = true;
                    }
                }

                return this.monthPrediction;
            }
        }

        /// <summary>
        /// Gets the next target date week.
        /// </summary>
        public DateTime? NextTargetDateWeek
        {
            get
            {
                if (!this.weekPredictionDone)
                {
                    if (this.WeekValue == null || this.NextTarget == null)
                    {
                        this.weekPrediction = null;
                        this.weekPredictionDone = true;
                        return null;
                    }

                    var prediction = this.PredictValue(this.WeekValue);

                    if (prediction == null)
                    {
                        this.weekPrediction = null;
                        this.weekPredictionDone = true;
                        return null;
                    }

                    try
                    {
                        this.weekPrediction = this.WeekValue.Timestamp.Add(prediction.Value);
                        this.weekPredictionDone = true;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        this.weekPrediction = null;
                        this.weekPredictionDone = true;
                    }
                }

                return this.weekPrediction;
            }
        }

        /// <summary>
        /// Gets the week value.
        /// </summary>
        public ValueEntry WeekValue
        {
            get
            {
                return this.weekValue;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The clone.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public virtual object Clone()
        {
            return new StatisticProgress(this.currentValue, this.monthValue, this.weekValue);
        }

        #endregion

        #region Methods

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
            long deltaTimeTicks = (this.CurrentValue.Timestamp - historical.Timestamp).Ticks;

            // let y = deltaValue, 0
            long deltaValue = this.CurrentValue.Value.GetValueOrDefault(0) - historical.Value.GetValueOrDefault(0);

            if (deltaValue == 0)
            {
                // urm... nothing's happened. Return null so we don't get a nullrefexception.
                return null;
            }

            long requiredDelta = this.NextTarget.GetValueOrDefault(0) - this.CurrentValue.Value.GetValueOrDefault(0);

            // y = mx + b. deltas - point is (0,0). 0 = 0m + b, b = 0.
            // y = mx, m = y/x
            decimal m = (decimal)deltaValue / deltaTimeTicks;

            // y = mx + b, requiredValue / m = prediction
            return new TimeSpan((long)Math.Ceiling(requiredDelta / m));
        }

        #endregion
    }
}