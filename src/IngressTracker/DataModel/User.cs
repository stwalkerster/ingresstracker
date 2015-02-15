// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Simon Walker">
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
//   The user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.DataModel
{
    using IngressTracker.Persistence;

    /// <summary>
    /// The user.
    /// </summary>
    public class User : EntityBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public virtual string AgentName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether database admin.
        /// </summary>
        public virtual bool StaticDataAdmin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether access to all agents.
        /// </summary>
        public virtual bool AccessToAllAgents { get; set; }

        /// <summary>
        /// Gets or sets the database username.
        /// </summary>
        public virtual string DatabaseUsername { get; set; }

        /// <summary>
        /// Gets or sets the faction.
        /// </summary>
        public virtual Faction Faction
        {
            get
            {
                if (this.FactionCode == "ENL")
                {
                    return Faction.Enlightened;
                }

                if (this.FactionCode == "RES")
                {
                    return Faction.Resistance;
                }

                return null;
            }

            set
            {
                this.FactionCode = value.Code;
            }
        }

        /// <summary>
        /// Gets or sets the faction.
        /// </summary>
        public virtual string FactionCode { get; set; }

        #endregion
    }
}