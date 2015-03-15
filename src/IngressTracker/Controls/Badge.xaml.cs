// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Badge.xaml.cs" company="Simon Walker">
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
//   The badge.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Controls
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// The badge.
    /// </summary>
    public partial class Badge
    {
        #region Static Fields

        /// <summary>
        /// The fill property.
        /// </summary>
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill", 
            typeof(Brush), 
            typeof(Badge), 
            new PropertyMetadata(Brushes.Purple));

        /// <summary>
        /// The inner stroke property.
        /// </summary>
        public static readonly DependencyProperty InnerStrokeProperty = DependencyProperty.Register(
            "InnerStroke",
            typeof(Brush), 
            typeof(Badge),
            new PropertyMetadata(Brushes.Red));

        /// <summary>
        /// The stroke property.
        /// </summary>
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke",
            typeof(Brush), 
            typeof(Badge),
            new PropertyMetadata(Brushes.Magenta));

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="Badge"/> class.
        /// </summary>
        public Badge()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the fill.
        /// </summary>
        public Brush Fill
        {
            get
            {
                return (Brush)this.GetValue(FillProperty);
            }

            set
            {
                this.SetValue(FillProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the inner stroke.
        /// </summary>
        public Brush InnerStroke
        {
            get
            {
                return (Brush)this.GetValue(InnerStrokeProperty);
            }

            set
            {
                this.SetValue(InnerStrokeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the stroke.
        /// </summary>
        public Brush Stroke
        {
            get
            {
                return (Brush)this.GetValue(StrokeProperty);
            }

            set
            {
                this.SetValue(StrokeProperty, value);
            }
        }

        #endregion
    }
}