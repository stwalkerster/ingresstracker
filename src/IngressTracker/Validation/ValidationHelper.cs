// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationHelper.cs" company="Simon Walker">
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
//   The validation helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Validation
{
    using System;
    using System.Linq;
    using System.Reflection;

    using FluentValidation;
    using FluentValidation.Attributes;

    /// <summary>
    /// The validation helper.
    /// </summary>
    public class ValidationHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get validator.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="IValidator"/>.
        /// </returns>
        public static IValidator GetValidator(Type type)
        {
            var customAttributes = type.GetCustomAttributes(typeof(ValidatorAttribute), true);
            var attribute = customAttributes.FirstOrDefault();

            if (attribute == null)
            {
                return null;
            }

            var validator = ((ValidatorAttribute)attribute).ValidatorType;

            return (IValidator)Activator.CreateInstance(validator);
        }

        #endregion
    }
}