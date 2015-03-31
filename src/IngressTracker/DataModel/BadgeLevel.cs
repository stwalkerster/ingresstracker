// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BadgeLevel.cs" company="Simon Walker">
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
//   The badge level.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.DataModel
{
    using System;

    using IngressTracker.Properties;

    /// <summary>
    /// The badge level.
    /// </summary>
    public class BadgeLevel : DbType<BadgeLevel>, IComparable<BadgeLevel>
    {
        #region Static Fields

        /// <summary>
        /// The black.
        /// </summary>
        public static readonly BadgeLevel Black = new BadgeLevel("K", Resources.Black, 5);

        /// <summary>
        /// The bronze.
        /// </summary>
        public static readonly BadgeLevel Bronze = new BadgeLevel("B", Resources.Bronze, 1);

        /// <summary>
        /// The gold.
        /// </summary>
        public static readonly BadgeLevel Gold = new BadgeLevel("G", Resources.Gold, 3);

        /// <summary>
        /// The locked.
        /// </summary>
        public static readonly BadgeLevel Locked = new BadgeLevel("L", Resources.Locked, 0);

        /// <summary>
        /// The platinum.
        /// </summary>
        public static readonly BadgeLevel Platinum = new BadgeLevel("P", Resources.Platinum, 4);

        /// <summary>
        /// The silver.
        /// </summary>
        public static readonly BadgeLevel Silver = new BadgeLevel("S", Resources.Silver, 2);

        #endregion

        #region Fields

        /// <summary>
        /// The position.
        /// </summary>
        private readonly int position;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises static members of the <see cref="BadgeLevel"/> class.
        /// </summary>
        static BadgeLevel()
        {
            BadgeLevel.AddItem(Locked);
            BadgeLevel.AddItem(Bronze);
            BadgeLevel.AddItem(Silver);
            BadgeLevel.AddItem(Gold);
            BadgeLevel.AddItem(Platinum);
            BadgeLevel.AddItem(Black);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="BadgeLevel"/> class.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="position">
        /// The position.
        /// </param>
        private BadgeLevel(string code, string description, int position)
            : base(code)
        {
            this.position = position;
            this.Description = description;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The &gt;.
        /// </summary>
        /// <param name="first">
        /// The first.
        /// </param>
        /// <param name="second">
        /// The second.
        /// </param>
        /// <returns>boolean value</returns>
        public static bool operator >(BadgeLevel first, BadgeLevel second)
        {
            return first.CompareTo(second) > 0;
        }

        /// <summary>
        /// The &lt;.
        /// </summary>
        /// <param name="first">
        /// The first.
        /// </param>
        /// <param name="second">
        /// The second.
        /// </param>
        /// <returns>boolean value</returns>
        public static bool operator <(BadgeLevel first, BadgeLevel second)
        {
            return first.CompareTo(second) < 0;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: 
        ///     Value Meaning Less than zero 
        ///         This object is less than the <paramref name="other"/> parameter.
        ///     Zero 
        ///         This object is equal to <paramref name="other"/>.
        ///     Greater than zero
        ///         This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">
        /// An object to compare with this object.
        /// </param>
        public int CompareTo(BadgeLevel other)
        {
            if (other == null)
            {
                return 1;
            }

            if (this.position < other.position)
            {
                return -1;
            }

            if (this.position == other.position)
            {
                return 0;
            }

            if (this.position > other.position)
            {
                return 1;
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return this.Description;
        }

        #endregion
    }
}