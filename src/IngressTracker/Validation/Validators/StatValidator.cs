// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatValidator.cs" company="Simon Walker">
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
//   The stat validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IngressTracker.Validation.Validators
{
    using FluentValidation;

    using IngressTracker.DataModel.Models;

    /// <summary>
    /// The stat validator.
    /// </summary>
    internal class StatValidator : AbstractValidator<Stat>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="StatValidator"/> class.
        /// </summary>
        public StatValidator()
        {
            this.RuleFor(x => x.Description).NotNull();
            this.RuleFor(x => x.Category).NotNull();
        }

        #endregion
    }
}