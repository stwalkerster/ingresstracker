// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LevelValidator.cs" company="Simon Walker">
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
//   The level validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IngressTracker.Validation.Validators
{
    using FluentValidation;

    using IngressTracker.DataModel.Models;

    /// <summary>
    /// The level validator.
    /// </summary>
    internal class LevelValidator : AbstractValidator<Level>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="LevelValidator"/> class.
        /// </summary>
        public LevelValidator()
        {
            this.RuleFor(x => x.LevelNumber).NotNull().GreaterThan(0);
            this.RuleFor(x => x.AccessPoints).NotNull();
            this.RuleFor(x => x.SilverBadges).NotNull();
            this.RuleFor(x => x.GoldBadges).NotNull();
            this.RuleFor(x => x.PlatinumBadges).NotNull();
            this.RuleFor(x => x.BlackBadges).NotNull();
        }

        #endregion
    }
}