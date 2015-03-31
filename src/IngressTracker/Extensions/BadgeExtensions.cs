// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BadgeExtensions.cs" company="Simon Walker">
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
//   The badge extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IngressTracker.Extensions
{
    using IngressTracker.DataModel;
    using IngressTracker.DataModel.Models;

    /// <summary>
    /// The badge extensions.
    /// </summary>
    public static class BadgeExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get target.
        /// </summary>
        /// <param name="badge">
        /// The badge.
        /// </param>
        /// <param name="level">
        /// The level.
        /// </param>
        /// <returns>
        /// The <see cref="long?"/>.
        /// </returns>
        public static long? GetNextTarget(this Badge badge, BadgeLevel level)
        {
            if (level == BadgeLevel.Locked)
            {
                return badge.Bronze;
            }

            if (level == BadgeLevel.Bronze)
            {
                return badge.Silver;
            }

            if (level == BadgeLevel.Silver)
            {
                return badge.Gold;
            }

            if (level == BadgeLevel.Gold)
            {
                return badge.Platinum;
            }

            if (level == BadgeLevel.Platinum)
            {
                return badge.Black;
            }

            return null;
        }

        #endregion
    }
}