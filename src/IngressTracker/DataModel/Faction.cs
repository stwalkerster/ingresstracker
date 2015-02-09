// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Faction.cs" company="Simon Walker">
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
//   The faction.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.DataModel
{
    using System.Collections.Generic;
    using System.Windows.Media;

    /// <summary>
    /// The faction.
    /// </summary>
    public class Faction
    {
        #region Static Fields

        /// <summary>
        /// The enlightened.
        /// </summary>
        public static readonly Faction Enlightened = new Faction(
            "Enlightened", 
            "ENL", 
            Brushes.Green, 
            Brushes.LightGreen);

        /// <summary>
        /// The item collection.
        /// </summary>
        public static readonly List<Faction> ItemCollection = new List<Faction> { Enlightened, Resistance };

        /// <summary>
        /// The resistance.
        /// </summary>
        public static readonly Faction Resistance = new Faction("Resistance", "RES", Brushes.Blue, Brushes.LightBlue);

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="Faction"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <param name="colour">
        /// The colour.
        /// </param>
        /// <param name="backgroundColour">
        /// The background colour.
        /// </param>
        private Faction(string name, string code, Brush colour, Brush backgroundColour)
        {
            this.Name = name;
            this.Code = code;
            this.Colour = colour;
            this.BackgroundColour = backgroundColour;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the background colour.
        /// </summary>
        public Brush BackgroundColour { get; private set; }

        /// <summary>
        /// Gets the code.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the colour.
        /// </summary>
        public Brush Colour { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}