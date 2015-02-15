// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScreenBase.cs" company="Simon Walker">
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
//   The screen base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ScreenBase
{
    using Caliburn.Micro;

    using NHibernate;

    /// <summary>
    /// The screen base.
    /// </summary>
    public abstract class ScreenBase : Screen
    {
        #region Fields

        /// <summary>
        /// The database session.
        /// </summary>
        private readonly ISession databaseSession;

        /// <summary>
        /// The display name.
        /// </summary>
        private string displayName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ScreenBase"/> class.
        /// </summary>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <param name="databaseSession">
        /// The database session.
        /// </param>
        protected ScreenBase(string displayName, ISession databaseSession)
        {
            this.displayName = displayName;
            this.databaseSession = databaseSession;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return this.displayName;
            }

            set
            {
                if (this.displayName != value)
                {
                    this.displayName = value;
                    this.NotifyOfPropertyChange(() => this.DisplayName);
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the database session.
        /// </summary>
        protected ISession DatabaseSession
        {
            get
            {
                return this.databaseSession;
            }
        }

        #endregion
    }
}