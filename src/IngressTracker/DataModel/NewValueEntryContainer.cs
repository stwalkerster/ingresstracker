// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewValueEntryContainer.cs" company="Simon Walker">
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
//   The new value entry container.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.DataModel
{
    using System;
    using System.ComponentModel;

    using IngressTracker.DataModel.Models;
    using IngressTracker.Properties;

    /// <summary>
    /// The new value entry container.
    /// </summary>
    public class NewValueEntryContainer : IDataErrorInfo
    {
        #region Fields

        /// <summary>
        /// The value entry.
        /// </summary>
        private readonly ValueEntry valueEntry;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="NewValueEntryContainer"/> class.
        /// </summary>
        public NewValueEntryContainer()
        {
            this.valueEntry = new ValueEntry();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User Agent
        {
            get
            {
                return this.ValueEntry.Agent;
            }

            set
            {
                this.ValueEntry.Agent = value;
            }
        }

        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        public long? OldValue { get; set; }

        /// <summary>
        /// Gets or sets the statistic.
        /// </summary>
        public Stat Statistic
        {
            get
            {
                return this.ValueEntry.Statistic;
            }

            set
            {
                this.ValueEntry.Statistic = value;
            }
        }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        public DateTime Timestamp
        {
            get
            {
                return this.ValueEntry.Timestamp;
            }

            set
            {
                this.ValueEntry.Timestamp = value;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public long? Value
        {
            get
            {
                return this.ValueEntry.Value;
            }

            set
            {
                this.ValueEntry.Value = value;
            }
        }

        /// <summary>
        /// The value entry.
        /// </summary>
        public ValueEntry ValueEntry
        {
            get
            {
                return this.valueEntry;
            }
        }

        #endregion

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Value")
                {
                    if (this.Value < this.OldValue)
                    {
                        return Resources.NewValueLessThanOldValue;
                    }
                }

                return null;
            }
        }

        public string Error
        {
            get
            {
                if (this.Value < this.OldValue)
                {
                    return Resources.NewValueLessThanOldValue;
                }

                return null;
            }
        }
    }
}