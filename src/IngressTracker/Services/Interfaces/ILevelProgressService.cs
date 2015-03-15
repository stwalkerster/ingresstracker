// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILevelProgressService.cs" company="Simon Walker">
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
//   The LevelProgressService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Services.Interfaces
{
    using IngressTracker.DataModel.Models;

    /// <summary>
    /// The LevelProgressService interface.
    /// </summary>
    public interface ILevelProgressService
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get current level.
        /// </summary>
        /// <param name="ap">
        /// The AP.
        /// </param>
        /// <param name="silver">
        /// The silver.
        /// </param>
        /// <param name="gold">
        /// The gold.
        /// </param>
        /// <param name="platinum">
        /// The platinum.
        /// </param>
        /// <param name="black">
        /// The black.
        /// </param>
        /// <returns>
        /// The <see cref="Level"/>.
        /// </returns>
        Level GetCurrentLevel(long ap, int silver, int gold, int platinum, int black);

        /// <summary>
        /// The get next level.
        /// </summary>
        /// <param name="currentLevel">
        /// The current Level.
        /// </param>
        /// <returns>
        /// The <see cref="Level"/>.
        /// </returns>
        Level GetNextLevel(Level currentLevel);

        #endregion
    }
}