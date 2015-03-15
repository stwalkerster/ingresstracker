// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScreenBase.cs" company="Simon Walker">
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
//   The screen base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ScreenBase
{
    using Caliburn.Micro;

    using IngressTracker.Services.Interfaces;

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
        /// The login service.
        /// </summary>
        private readonly ILoginService loginService;

        /// <summary>
        /// The display name.
        /// </summary>
        private string displayName;

        /// <summary>
        /// The background in progress.
        /// </summary>
        private bool backgroundInProgress;

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
        /// <param name="loginService">
        /// The login Service.
        /// </param>
        protected ScreenBase(string displayName, ISession databaseSession, ILoginService loginService)
        {
            this.displayName = displayName;
            this.databaseSession = databaseSession;
            this.loginService = loginService;
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

        /// <summary>
        /// Gets or sets a value indicating whether background in progress.
        /// </summary>
        public bool BackgroundInProgress
        {
            get
            {
                return this.backgroundInProgress;
            }

            set
            {
                if (!value.Equals(this.backgroundInProgress))
                {
                    this.backgroundInProgress = value;
                    this.NotifyOfPropertyChange(() => this.BackgroundInProgress);
                }
            }
        }

        /// <summary>
        /// Gets the login service.
        /// </summary>
        public ILoginService LoginService
        {
            get
            {
                return this.loginService;
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