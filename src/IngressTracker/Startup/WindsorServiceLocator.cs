// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindsorServiceLocator.cs" company="Simon Walker">
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
//   The windsor service locator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IngressTracker.Startup
{
    using System;
    using System.Collections.Generic;

    using Castle.Windsor;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// The windsor service locator.
    /// </summary>
    internal class WindsorServiceLocator : ServiceLocatorImplBase
    {
        #region Fields

        /// <summary>
        /// The container.
        /// </summary>
        private readonly IWindsorContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="WindsorServiceLocator"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public WindsorServiceLocator(IWindsorContainer container)
        {
            this.container = container;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The do get all instances.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{Object}"/>.
        /// </returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return (object[])this.container.ResolveAll(serviceType);
        }

        /// <summary>
        /// The do get instance.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (key != null)
            {
                return this.container.Resolve(key, serviceType);
            }

            return this.container.Resolve(serviceType);
        }

        #endregion
    }
}