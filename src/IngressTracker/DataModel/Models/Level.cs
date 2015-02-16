// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Level.cs" company="Simon Walker">
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
//   The level.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IngressTracker.DataModel.Models
{
    using IngressTracker.Persistence;

    /// <summary>
    /// The level.
    /// </summary>
    public class Level : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the access points.
        /// </summary>
        public virtual int AccessPoints { get; set; }

        /// <summary>
        /// Gets or sets the black badges.
        /// </summary>
        public virtual int BlackBadges { get; set; }

        /// <summary>
        /// Gets or sets the gold badges.
        /// </summary>
        public virtual int GoldBadges { get; set; }

        /// <summary>
        /// Gets or sets the level number.
        /// </summary>
        public virtual int LevelNumber { get; set; }

        /// <summary>
        /// Gets or sets the platinum badges.
        /// </summary>
        public virtual int PlatinumBadges { get; set; }

        /// <summary>
        /// Gets or sets the silver badges.
        /// </summary>
        public virtual int SilverBadges { get; set; }

        #endregion
    }
}