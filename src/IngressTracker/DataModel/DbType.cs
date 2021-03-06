﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbType.cs" company="Simon Walker">
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
//   The db type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.DataModel
{
    using System.Collections.Generic;

    using IngressTracker.Persistence.Interfaces;

    /// <summary>
    /// The database type.
    /// </summary>
    /// <typeparam name="T">
    /// the type
    /// </typeparam>
    public class DbType<T> : IDataEntity
        where T : DbType<T>
    {
        #region Static Fields

        /// <summary>
        /// The item collection.
        /// </summary>
        public static readonly List<T> ItemCollection = new List<T>();

        /// <summary>
        /// The lookup map.
        /// </summary>
        private static readonly Dictionary<string, T> LookupMap = new Dictionary<string, T>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="DbType{T}"/> class.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        protected DbType(string code)
        {
            this.Code = code;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the code.
        /// </summary>
        public string Code { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The lookup.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T Lookup(string code)
        {
            if (code == null)
            {
                return null;
            }

            if (LookupMap.ContainsKey(code))
            {
                return LookupMap[code];
            }

            return null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        protected static void AddItem(T item)
        {
            ItemCollection.Add(item);
            LookupMap.Add(item.Code, item);
        }

        #endregion
    }
}