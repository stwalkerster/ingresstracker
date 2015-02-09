// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataScreen.cs" company="Simon Walker">
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
//   The data screen.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker
{
    using System.Collections.ObjectModel;

    using Caliburn.Micro;

    using IngressTracker.Interfaces;

    /// <summary>
    /// The data screen.
    /// </summary>
    /// <typeparam name="T">
    /// The type of data shown
    /// </typeparam>
    public abstract class DataScreen<T> : Screen, IDataScreen
    {
        #region Fields

        /// <summary>
        /// The DataItems.
        /// </summary>
        private ObservableCollection<T> dataItems;

        /// <summary>
        /// The display name.
        /// </summary>
        private string displayName;

        /// <summary>
        /// The selected item.
        /// </summary>
        private T selectedItem;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="DataScreen{T}"/> class. 
        /// </summary>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        protected DataScreen(string displayName)
        {
            this.displayName = displayName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the DataItems.
        /// </summary>
        public ObservableCollection<T> DataItems
        {
            get
            {
                return this.dataItems;
            }

            set
            {
                if (value != this.dataItems)
                {
                    this.dataItems = value;
                    this.NotifyOfPropertyChange(() => this.DataItems);
                }
            }
        }

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
        /// Gets or sets the selected item.
        /// </summary>
        public T SelectedItem
        {
            get
            {
                return this.selectedItem;
            }

            set
            {
                if (!object.Equals(this.selectedItem, value))
                {
                    this.selectedItem = value;
                    this.NotifyOfPropertyChange(() => this.SelectedItem);
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The refresh data.
        /// </summary>
        public abstract void RefreshData();

        #endregion

        #region Methods

        /// <summary>
        /// The on view loaded.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            this.RefreshData();
        }

        #endregion
    }
}