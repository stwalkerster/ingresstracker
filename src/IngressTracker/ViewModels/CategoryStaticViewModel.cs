// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryStaticViewModel.cs" company="Simon Walker">
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
//   The category static view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using System.Windows;

    using IngressTracker.DataModel.Models;
    using IngressTracker.Persistence.Interfaces;
    using IngressTracker.Properties;
    using IngressTracker.ScreenBase;
    using IngressTracker.Services.Interfaces;
    using IngressTracker.ViewModels.Interfaces;

    using NHibernate;

    /// <summary>
    /// The category static view model.
    /// </summary>
    public class CategoryStaticViewModel : StaticDataScreen<Category>, ICategoryStaticViewModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="CategoryStaticViewModel"/> class.
        /// </summary>
        /// <param name="databaseSession">
        /// The database session.
        /// </param>
        /// <param name="loginService">
        /// The login Service.
        /// </param>
        public CategoryStaticViewModel(ISession databaseSession, ILoginService loginService)
            : base(Resources.CategoryStaticView, databaseSession, loginService)
        {
        }

        /// <summary>
        /// The pre delete guard.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool PreDeleteGuard(IDataEntity entity)
        {
            var dataEntity = (Category)entity;

            if (this.DatabaseSession.QueryOver<Stat>().Where(x => x.Category == dataEntity).RowCount() > 0)
            {
                var messageBoxTextDenied = string.Format(
                Resources.CannotDeleteCategoryInUse,
                dataEntity.Name);

                MessageBox.Show(
                    messageBoxTextDenied,
                    Resources.CannotDeleteCategory,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return true;
            }

            return false;
        }

        #endregion
    }
}