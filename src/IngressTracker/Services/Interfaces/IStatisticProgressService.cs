// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatisticProgressService.cs" company="Simon Walker">
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
//   The StatisticProgressService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IngressTracker.Services.Interfaces
{
    using IngressTracker.DataModel;
    using IngressTracker.DataModel.Models;

    /// <summary>
    /// The StatisticProgressService interface.
    /// </summary>
    public interface IStatisticProgressService
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get statistic progress.
        /// </summary>
        /// <param name="agent">
        /// The agent.
        /// </param>
        /// <param name="statistic">
        /// The statistic.
        /// </param>
        /// <returns>
        /// The <see cref="StatisticProgress"/>.
        /// </returns>
        StatisticProgress GetStatisticProgress(User agent, Stat statistic);

        #endregion
    }
}