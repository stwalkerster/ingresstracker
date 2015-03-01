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
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    using IngressTracker.Interfaces;
    using IngressTracker.Persistence.Interfaces;
    using IngressTracker.Persistence.Proxy;
    using IngressTracker.Properties;
    using IngressTracker.Services.Interfaces;

    using NHibernate;

    /// <summary>
    /// The data screen base.
    /// </summary>
    /// <typeparam name="T">
    /// A data class
    /// </typeparam>
    public abstract class DataScreenBase<T> : ScreenBase, IDataOperations
        where T : class, IDataEntity
    {
        #region Fields

        /// <summary>
        /// The deleted items.
        /// </summary>
        private readonly List<T> deletedItems;

        /// <summary>
        /// The DataItems.
        /// </summary>
        private ObservableCollection<T> dataItems;

        /// <summary>
        /// The selected item.
        /// </summary>
        private T selectedItem;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="DataScreenBase{T}"/> class. 
        /// Initialises a new instance of the <see cref="StaticDataScreen{T}"/> class.
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
        protected DataScreenBase(string displayName, ISession databaseSession, ILoginService loginService)
            : base(displayName, databaseSession, loginService)
        {
            this.deletedItems = new List<T>();
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
        /// The add record.
        /// </summary>
        public void AddRecord()
        {
            this.DataItems.Add(DataBindingFactory.Create<T>());
        }

        /// <summary>
        /// The can close.
        /// </summary>
        /// <param name="callback">
        /// The callback.
        /// </param>
        public override void CanClose(Action<bool> callback)
        {
            if (!this.ConfirmDataLoss())
            {
                callback(false);
            }

            base.CanClose(callback);
        }

        /// <summary>
        /// The delete record.
        /// </summary>
        public void DeleteRecord()
        {
            if (!this.deletedItems.Contains(this.SelectedItem))
            {
                this.deletedItems.Add(this.SelectedItem);
            }

            this.DataItems.Remove(this.SelectedItem);
        }

        /// <summary>
        /// The refresh data.
        /// </summary>
        public virtual void RefreshData()
        {
            if (this.ConfirmDataLoss())
            {
                this.deletedItems.Clear();
                this.DatabaseSession.Clear();
                var enumerable = this.GetData();
                this.DataItems = new ObservableCollection<T>(enumerable);
            }
        }

        /// <summary>
        /// The save.
        /// </summary>
        public void Save()
        {
            try
            {
                // handle the deletes
                foreach (var deletedItem in this.deletedItems)
                {
                    this.DatabaseSession.Delete(deletedItem);
                }

                this.deletedItems.Clear();

                // handle the saves
                foreach (var dataItem in this.DataItems)
                {
                    this.DatabaseSession.SaveOrUpdate(dataItem);
                }

                this.DatabaseSession.Flush();
            }
            catch (Exception e)
            {
                var errorSavingTransaction = string.Format(
                    "Error saving transaction\r\n\r\n{0}\r\n\r\n{1}", 
                    e.Message, 
                    e.StackTrace);
                MessageBox.Show(errorSavingTransaction);
            }

            this.RefreshData();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get data.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        protected virtual IEnumerable<T> GetData()
        {
            return this.DatabaseSession.QueryOver<T>().List();
        }

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

        /// <summary>
        /// The confirm data loss.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ConfirmDataLoss()
        {
            if (this.DatabaseSession.IsDirty() || this.deletedItems.Count > 0)
            {
                var messageBoxResult = MessageBox.Show(
                    Resources.UnsavedChanges, 
                    Resources.UnsavedChangesTitle, 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Warning);
                if (messageBoxResult != MessageBoxResult.Yes)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}