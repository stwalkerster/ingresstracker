// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataBindingFactory.cs" company="Simon Walker">
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
//   The data binding factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Persistence.Proxy
{
    using System;
    using System.ComponentModel;

    using Castle.DynamicProxy;

    /// <summary>
    /// The data binding factory.
    /// </summary>
    public static class DataBindingFactory
    {
        #region Static Fields

        /// <summary>
        /// The proxy generator.
        /// </summary>
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        #endregion

        #region Interfaces

        /// <summary>
        /// The MarkerInterface interface.
        /// </summary>
        public interface IMarkerInterface
        {
            #region Public Properties

            /// <summary>
            /// Gets the type name.
            /// </summary>
            string TypeName { get; }

            #endregion
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <typeparam name="T">
        /// The type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object Create(Type type)
        {
            return ProxyGenerator.CreateClassProxy(
                type, 
                new[] { typeof(INotifyPropertyChanged), typeof(IMarkerInterface) }, 
                new NotifyPropertyChangedInterceptor(type.FullName));
        }

        #endregion

        /// <summary>
        /// The notify property changed interceptor.
        /// </summary>
        public class NotifyPropertyChangedInterceptor : IInterceptor
        {
            #region Fields

            /// <summary>
            /// The type name.
            /// </summary>
            private readonly string typeName;

            /// <summary>
            /// The subscribers.
            /// </summary>
            private PropertyChangedEventHandler subscribers = delegate { };

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initialises a new instance of the <see cref="NotifyPropertyChangedInterceptor"/> class.
            /// </summary>
            /// <param name="typeName">
            /// The type name.
            /// </param>
            public NotifyPropertyChangedInterceptor(string typeName)
            {
                this.typeName = typeName;
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// The intercept.
            /// </summary>
            /// <param name="invocation">
            /// The invocation.
            /// </param>
            public void Intercept(IInvocation invocation)
            {
                if (invocation.Method.DeclaringType == typeof(IMarkerInterface))
                {
                    invocation.ReturnValue = this.typeName;
                    return;
                }

                if (invocation.Method.DeclaringType == typeof(INotifyPropertyChanged))
                {
                    var propertyChangedEventHandler = (PropertyChangedEventHandler)invocation.Arguments[0];
                    if (invocation.Method.Name.StartsWith("add_"))
                    {
                        this.subscribers += propertyChangedEventHandler;
                    }
                    else
                    {
                        this.subscribers -= propertyChangedEventHandler;
                    }

                    return;
                }

                invocation.Proceed();

                if (invocation.Method.Name.StartsWith("set_"))
                {
                    var propertyName = invocation.Method.Name.Substring(4);
                    this.subscribers(invocation.InvocationTarget, new PropertyChangedEventArgs(propertyName));
                }
            }

            #endregion
        }
    }
}