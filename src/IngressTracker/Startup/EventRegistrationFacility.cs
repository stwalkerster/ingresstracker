// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventRegistrationFacility.cs" company="Simon Walker">
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
//   The event registration facility.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Startup
{
    using Caliburn.Micro;

    using Castle.Core;
    using Castle.MicroKernel.Facilities;

    /// <summary>
    /// The event registration facility.
    /// </summary>
    public class EventRegistrationFacility : AbstractFacility
    {
        #region Fields

        /// <summary>
        /// The _event aggregator.
        /// </summary>
        private IEventAggregator eventAggregator;

        #endregion

        #region Methods

        /// <summary>
        /// The initialisation method.
        /// </summary>
        protected override void Init()
        {
            this.Kernel.ComponentCreated += this.ComponentCreated;
            this.Kernel.ComponentDestroyed += this.ComponentDestroyed;
        }

        /// <summary>
        /// The component created.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="instance">
        /// The instance.
        /// </param>
        private void ComponentCreated(ComponentModel model, object instance)
        {
            if (!(instance is IHandle))
            {
                return;
            }

            if (this.eventAggregator == null)
            {
                this.eventAggregator = this.Kernel.Resolve<IEventAggregator>();
            }

            this.eventAggregator.Subscribe(instance);
        }

        /// <summary>
        /// The component destroyed.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="instance">
        /// The instance.
        /// </param>
        private void ComponentDestroyed(ComponentModel model, object instance)
        {
            if (!(instance is IHandle))
            {
                return;
            }

            if (this.eventAggregator == null)
            {
                return;
            }

            this.eventAggregator.Unsubscribe(instance);
        }

        #endregion
    }
}