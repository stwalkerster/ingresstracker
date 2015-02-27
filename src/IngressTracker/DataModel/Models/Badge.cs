// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Badge.cs" company="Simon Walker">
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
//   The badge.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.DataModel.Models
{
    using IngressTracker.Persistence;

    /// <summary>
    /// The badge.
    /// </summary>
    public class Badge : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether awardable.
        /// </summary>
        public virtual bool Awardable { get; set; }

        /// <summary>
        /// Gets or sets the black.
        /// </summary>
        public virtual long? Black { get; set; }

        /// <summary>
        /// Gets or sets the bronze.
        /// </summary>
        public virtual long? Bronze { get; set; }

        /// <summary>
        /// Gets or sets the gold.
        /// </summary>
        public virtual long? Gold { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the platinum.
        /// </summary>
        public virtual long? Platinum { get; set; }

        /// <summary>
        /// Gets or sets the silver.
        /// </summary>
        public virtual long? Silver { get; set; }

        /// <summary>
        /// Gets or sets the statistic.
        /// </summary>
        public virtual Stat Statistic { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public virtual string Description { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}