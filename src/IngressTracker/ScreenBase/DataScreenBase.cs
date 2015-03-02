// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataScreenBase.cs" company="Simon Walker">
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
//   The data screen base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ScreenBase
{
    using System.ComponentModel;

    using IngressTracker.Interfaces;
    using IngressTracker.Services.Interfaces;

    using NHibernate;

    /// <summary>
    /// The data screen base.
    /// </summary>
    public abstract class DataScreenBase : ScreenBase, IDataScreen
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="DataScreenBase"/> class.
        /// </summary>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <param name="databaseSession">
        /// The database session.
        /// </param>
        /// <param name="loginService">
        /// The login service.
        /// </param>
        protected DataScreenBase(string displayName, ISession databaseSession, ILoginService loginService)
            : base(displayName, databaseSession, loginService)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether content loaded.
        /// </summary>
        public bool ContentLoaded { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The refresh data.
        /// </summary>
        public void RefreshData()
        {
            if (this.PreRefreshGuard())
            {
                return;
            }

            if (this.BackgroundInProgress)
            {
                return;
            }

            this.BackgroundInProgress = true;

            var bgw = new BackgroundWorker();
            bgw.DoWork += (sender, args) => this.RefreshDataAsync();
            bgw.RunWorkerCompleted += this.OnBackgroundWorkerCompleted;

            bgw.RunWorkerAsync();
        }

        /// <summary>
        /// Guard method for refresh
        /// </summary>
        /// <returns>true to abort refresh</returns>
        protected virtual bool PreRefreshGuard()
        {
            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on view ready.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);

            if (!this.ContentLoaded)
            {
                this.RefreshData();
            }
        }

        /// <summary>
        /// The refresh data async.
        /// </summary>
        protected abstract void RefreshDataAsync();

        /// <summary>
        /// The refresh data completed.
        /// </summary>
        protected virtual void RefreshDataCompleted()
        {
        }

        /// <summary>
        /// The on background worker completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnBackgroundWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            this.RefreshDataCompleted();
            this.BackgroundInProgress = false;
            this.ContentLoaded = true;
        }

        #endregion
    }
}