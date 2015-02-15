﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValueEntryMap.cs" company="Simon Walker">
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
//   The value entry map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Persistence.Mappings
{
    using FluentNHibernate.Mapping;

    using IngressTracker.DataModel.Models;

    /// <summary>
    /// The value entry map.
    /// </summary>
    public class ValueEntryMap : ClassMap<ValueEntry>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ValueEntryMap"/> class.
        /// </summary>
        public ValueEntryMap()
        {
            this.Table("value");
            this.Id(x => x.Id).Column("id");
            this.Map(x => x.Value).Column("value");
            this.References(x => x.Statistic).Column("stat");
            this.References(x => x.Agent).Column("user");
            this.Map(x => x.Timestamp).Column("timestamp");
        }

        #endregion
    }
}