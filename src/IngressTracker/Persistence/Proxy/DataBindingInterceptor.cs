// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataBindingInterceptor.cs" company="Simon Walker">
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
//   The data binding intercepter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Persistence.Proxy
{
    using System;

    using NHibernate;

    /// <summary>
    /// The data binding interceptor.
    /// </summary>
    public class DataBindingInterceptor : EmptyInterceptor
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the session factory.
        /// </summary>
        public ISessionFactory SessionFactory { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get entity name.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string GetEntityName(object entity)
        {
            var markerInterface = entity as DataBindingFactory.IMarkerInterface;
            if (markerInterface != null)
            {
                return markerInterface.TypeName;
            }

            return base.GetEntityName(entity);
        }

        /// <summary>
        /// The instantiate.
        /// </summary>
        /// <param name="typeName">
        /// The typeName.
        /// </param>
        /// <param name="entityMode">
        /// The entity mode.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public override object Instantiate(string typeName, EntityMode entityMode, object id)
        {
            if (entityMode == EntityMode.Poco)
            {
                Type type = Type.GetType(typeName);
                if (type != null)
                {
                    var instance = DataBindingFactory.Create(type);
                    this.SessionFactory.GetClassMetadata(typeName).SetIdentifier(instance, id, entityMode);
                    return instance;
                }
            }

            return base.Instantiate(typeName, entityMode, id);
        }

        #endregion
    }
}