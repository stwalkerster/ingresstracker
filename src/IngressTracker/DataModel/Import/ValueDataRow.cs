// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValueDataRow.cs" company="Simon Walker">
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
//   The importer value data row.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.DataModel.Import
{
    using System;

    /// <summary>
    /// The importer value data row.
    /// </summary>
    public class ValueDataRow
    {
        #region Public Properties

        /// <summary>
        /// Gets the data value.
        /// </summary>
        public long DataValue
        {
            get
            {
                long dataValue;
                return long.TryParse(this.Value, out dataValue) ? dataValue : 0;
            }
        }

        /// <summary>
        /// Gets or sets the statistic.
        /// </summary>
        public string Statistic { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ValueDataRow)obj);
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Statistic != null ? this.Statistic.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ this.Timestamp.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Value.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                "Statistic: ¦{0}¦, Timestamp: ¦{1}¦, Value: ¦{2}¦", 
                this.Statistic, 
                this.Timestamp, 
                this.Value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected bool Equals(ValueDataRow other)
        {
            return string.Equals(this.Statistic, other.Statistic) && this.Timestamp.Equals(other.Timestamp)
                   && this.Value == other.Value;
        }

        #endregion
    }
}