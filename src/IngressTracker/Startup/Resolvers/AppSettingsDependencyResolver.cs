// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSettingsDependencyResolver.cs" company="Simon Walker">
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
//   The app settings dependency resolver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IngressTracker.Startup.Resolvers
{
    using System.ComponentModel;
    using System.Configuration;
    using System.Linq;

    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;

    /// <summary>
    /// The app settings dependency resolver.
    /// </summary>
    public class AppSettingsDependencyResolver : ISubDependencyResolver
    {
        #region Public Methods and Operators

        /// <summary>
        /// Returns true if the resolver is able to satisfy this dependency.
        /// </summary>
        /// <param name="context">
        /// Creation context, which is a resolver itself
        /// </param>
        /// <param name="contextHandlerResolver">
        /// Parent resolver - normally the IHandler implementation
        /// </param>
        /// <param name="model">
        /// Model of the component that is requesting the dependency
        /// </param>
        /// <param name="dependency">
        /// The dependency model
        /// </param>
        /// <returns>
        /// <c>true</c> if the dependency can be satisfied
        /// </returns>
        public bool CanResolve(
            CreationContext context, 
            ISubDependencyResolver contextHandlerResolver, 
            ComponentModel model, 
            DependencyModel dependency)
        {
            return ConfigurationManager.AppSettings.AllKeys.Contains(dependency.DependencyKey)
                   && TypeDescriptor.GetConverter(dependency.TargetType).CanConvertFrom(typeof(string));
        }

        /// <summary>
        /// Should return an instance of a service or property values as
        ///               specified by the dependency model instance. 
        ///               It is also the responsibility of <see cref="T:Castle.MicroKernel.IDependencyResolver"/>
        ///               to throw an exception in the case a non-optional dependency 
        ///               could not be resolved.
        /// </summary>
        /// <param name="context">
        /// Creation context, which is a resolver itself
        /// </param>
        /// <param name="contextHandlerResolver">
        /// Parent resolver - normally the IHandler implementation
        /// </param>
        /// <param name="model">
        /// Model of the component that is requesting the dependency
        /// </param>
        /// <param name="dependency">
        /// The dependency model
        /// </param>
        /// <returns>
        /// The dependency resolved value or null
        /// </returns>
        public object Resolve(
            CreationContext context, 
            ISubDependencyResolver contextHandlerResolver, 
            ComponentModel model, 
            DependencyModel dependency)
        {
            return
                TypeDescriptor.GetConverter(dependency.TargetType)
                    .ConvertFrom(ConfigurationManager.AppSettings[dependency.DependencyKey]);
        }

        #endregion
    }
}