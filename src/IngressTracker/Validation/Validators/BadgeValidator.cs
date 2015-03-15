// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BadgeValidator.cs" company="Simon Walker">
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
//   The badge validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IngressTracker.Validation.Validators
{
    using System;

    using FluentValidation;

    using IngressTracker.DataModel.Models;
    using IngressTracker.Properties;

    /// <summary>
    /// The badge validator.
    /// </summary>
    internal class BadgeValidator : AbstractValidator<Badge>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BadgeValidator"/> class.
        /// </summary>
        public BadgeValidator()
        {
            this.RuleFor(x => x.Name).NotNull();

            this.RuleFor(x => x.Bronze).Must((badge, val) => badge.Statistic == null || val != null).WithLocalizedMessage(() => Resources.BadgeValuesMustBeSpecifiedWithStatistic);
            this.RuleFor(x => x.Silver).Must((badge, val) => badge.Statistic == null || val != null).WithLocalizedMessage(() => Resources.BadgeValuesMustBeSpecifiedWithStatistic);
            this.RuleFor(x => x.Gold).Must((badge, val) => badge.Statistic == null || val != null).WithLocalizedMessage(() => Resources.BadgeValuesMustBeSpecifiedWithStatistic);
            this.RuleFor(x => x.Platinum).Must((badge, val) => badge.Statistic == null || val != null).WithLocalizedMessage(() => Resources.BadgeValuesMustBeSpecifiedWithStatistic);
            this.RuleFor(x => x.Black).Must((badge, val) => badge.Statistic == null || val != null).WithLocalizedMessage(() => Resources.BadgeValuesMustBeSpecifiedWithStatistic);

            this.RuleFor(x => x.Silver).GreaterThan(x => x.Bronze);
            this.RuleFor(x => x.Gold).GreaterThan(x => x.Silver);
            this.RuleFor(x => x.Platinum).GreaterThan(x => x.Gold);
            this.RuleFor(x => x.Black).GreaterThan(x => x.Platinum);
        }

        #endregion
    }
}