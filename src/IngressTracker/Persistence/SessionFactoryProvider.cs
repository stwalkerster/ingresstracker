// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionFactoryProvider.cs" company="Simon Walker">
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
//   The session factory provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Persistence
{
    using System;

    using IngressTracker.Persistence.Interfaces;

    using NHibernate;
    using NHibernate.Cfg;

    /// <summary>
    /// The session factory provider.
    /// </summary>
    public class SessionFactoryProvider : ISessionFactoryProvider
    {
        #region Fields

        /// <summary>
        /// The config.
        /// </summary>
        private readonly Configuration config;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="SessionFactoryProvider"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public SessionFactoryProvider(Configuration config)
        {
            this.config = config;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the session factory.
        /// </summary>
        public virtual ISessionFactory SessionFactory { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        public virtual void Initialize()
        {
            this.SessionFactory = this.config.BuildSessionFactory();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.SessionFactory.Close();
            }
        }

        #endregion
    }
}