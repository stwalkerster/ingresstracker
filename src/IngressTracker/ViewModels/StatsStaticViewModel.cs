// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatsStaticViewModel.cs" company="Simon Walker">
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
//   The stats static view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using System.Collections.ObjectModel;

    using IngressTracker.DataModel;
    using IngressTracker.Properties;
    using IngressTracker.ViewModels.Interfaces;

    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// The stats static view model.
    /// </summary>
    public class StatsStaticViewModel : DataScreen<Stat>, IStatStaticViewModel
    {
        #region Fields

        /// <summary>
        /// The categories.
        /// </summary>
        private ObservableCollection<Category> categories;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="StatsStaticViewModel"/> class.
        /// </summary>
        /// <param name="databaseSession">
        /// The database session.
        /// </param>
        public StatsStaticViewModel(ISession databaseSession)
            : base(Resources.StatStaticView, databaseSession)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the categories.
        /// </summary>
        public ObservableCollection<Category> Categories
        {
            get
            {
                return this.categories ?? new ObservableCollection<Category>();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The refresh data.
        /// </summary>
        public override void RefreshData()
        {
            base.RefreshData();

            var queryable = this.DatabaseSession.Query<Category>();
            this.categories = new ObservableCollection<Category>(queryable);
            this.NotifyOfPropertyChange(() => this.Categories);
        }

        #endregion
    }
}