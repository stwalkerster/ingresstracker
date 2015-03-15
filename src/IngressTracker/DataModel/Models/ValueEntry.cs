// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValueEntry.cs" company="Simon Walker">
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
//   The value entry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IngressTracker.DataModel.Models
{
    using System;

    using FluentValidation.Attributes;

    using IngressTracker.Persistence;
    using IngressTracker.Validation.Validators;

    /// <summary>
    /// The value entry.
    /// </summary>
    [Validator(typeof(ValueEntryValidator))]
    public class ValueEntry : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the statistic.
        /// </summary>
        public virtual Stat Statistic { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        public virtual DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User Agent { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public virtual long? Value { get; set; }

        #endregion
    }
}