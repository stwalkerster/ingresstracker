// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserStaticViewModel.cs" company="Simon Walker">
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
//   The user static view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    using IngressTracker.DataModel;
    using IngressTracker.Interfaces;
    using IngressTracker.Persistence.Proxy;
    using IngressTracker.Properties;
    using IngressTracker.ViewModels.Interfaces;

    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// The user static view model.
    /// </summary>
    public class UserStaticViewModel : DataScreen<User>, IUserStaticViewModel, ICanSave, ICanAddRecord, ICanDelete
    {
        #region Fields

        /// <summary>
        /// The database session.
        /// </summary>
        private readonly ISession databaseSession;

        /// <summary>
        /// The deleted items.
        /// </summary>
        private readonly List<User> deletedItems;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="UserStaticViewModel"/> class.
        /// </summary>
        /// <param name="databaseSession">
        /// The database Session.
        /// </param>
        public UserStaticViewModel(ISession databaseSession)
            : base(Resources.UserStaticView)
        {
            this.databaseSession = databaseSession;
            this.deletedItems = new List<User>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add record.
        /// </summary>
        public void AddRecord()
        {
            this.DataItems.Add(DataBindingFactory.Create<User>());
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
        public override void RefreshData()
        {
            if (this.databaseSession.IsDirty())
            {
                // prompt save changes
            }

            this.deletedItems.Clear();
            this.databaseSession.Clear();
            var enumerable = this.databaseSession.Query<User>();
            this.DataItems = new ObservableCollection<User>(enumerable);
        }

        /// <summary>
        /// The save.
        /// </summary>
        public void Save()
        {
            try
            {
                foreach (var deletedItem in this.deletedItems)
                {
                    this.databaseSession.Delete(deletedItem);
                }

                foreach (var user in this.DataItems)
                {
                    this.databaseSession.SaveOrUpdate(user);
                }

                this.databaseSession.Flush();
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
    }
}