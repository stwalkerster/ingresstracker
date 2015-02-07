// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShellViewModel.cs" company="Simon Walker">
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
//   The shell view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using Caliburn.Micro;

    using Castle.Core.Logging;

    using IngressTracker.ViewModels.Interfaces;

    /// <summary>
    /// The shell view model.
    /// </summary>
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShellViewModel
    {
        #region Fields

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public ShellViewModel(ILogger logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether can add record.
        /// </summary>
        public bool CanAddRecord
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can delete.
        /// </summary>
        public bool CanDelete
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can open badge static.
        /// </summary>
        public bool CanOpenBadgeStatic
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can open stat static.
        /// </summary>
        public bool CanOpenStatStatic
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can open user static.
        /// </summary>
        public bool CanOpenUserStatic
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can refresh.
        /// </summary>
        public bool CanRefreshData
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can save.
        /// </summary>
        public bool CanSave
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether data tools enabled.
        /// </summary>
        public bool DataToolsEnabled
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add record.
        /// </summary>
        public void AddRecord()
        {
        }

        /// <summary>
        /// The delete.
        /// </summary>
        public void Delete()
        {
        }

        /// <summary>
        /// The open badge static.
        /// </summary>
        public void OpenBadgeStatic()
        {
        }

        /// <summary>
        /// The open stat static.
        /// </summary>
        public void OpenStatStatic()
        {
        }

        /// <summary>
        /// The open user static.
        /// </summary>
        public void OpenUserStatic()
        {
        }

        /// <summary>
        /// The refresh.
        /// </summary>
        public void RefreshData()
        {
        }

        /// <summary>
        /// The save.
        /// </summary>
        public void Save()
        {
        }

        #endregion
    }
}