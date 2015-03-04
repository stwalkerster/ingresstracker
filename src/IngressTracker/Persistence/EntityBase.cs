// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityBase.cs" company="Simon Walker">
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
//   The entity base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Persistence
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    using FluentValidation;

    using IngressTracker.Persistence.Interfaces;
    using IngressTracker.Validation;

    /// <summary>
    /// The entity base.
    /// </summary>
    public class EntityBase : IDatabaseEntity, IDataErrorInfo
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private IValidator validator;

        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public virtual int Id { get; protected set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public virtual string ObjectType
        {
            get
            {
                return this.GetType().ToString();
            }
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        public virtual string Error
        {
            get
            {
                // resolve the validator
                if (this.validator == null)
                {
                    this.validator = ValidationHelper.GetValidator(this.GetType());
                }

                // still null? no validator.
                if (this.validator == null)
                {
                    return null;
                }

                var validationResult = this.validator.Validate(this);

                if (!validationResult.Errors.Any())
                {
                    return null;
                }

                var aggregate = validationResult.Errors.Aggregate(
                    string.Empty,
                    (current, x) => current + x + Environment.NewLine);
                return aggregate.Trim(Environment.NewLine.ToCharArray());
            }
        }

        #endregion

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="columnName">
        /// The column name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public virtual string this[string columnName]
        {
            get
            {
                // resolve the validator
                if (this.validator == null)
                {
                    this.validator = ValidationHelper.GetValidator(this.GetType());
                }

                // still null? no validator.
                if (this.validator == null)
                {
                    return null;
                }

                var validationResult = this.validator.Validate(this);

                var validationFailure =
                    validationResult.Errors.FirstOrDefault(x => x.PropertyName == columnName);

                if (validationFailure == null)
                {
                    return null;
                }

                return validationFailure.ErrorMessage;
            }
        }
    }
}