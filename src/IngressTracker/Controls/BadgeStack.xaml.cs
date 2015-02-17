// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BadgeStack.xaml.cs" company="Simon Walker">
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
//   Interaction logic for BadgeStack.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Controls
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for BadgeStack.xaml
    /// </summary>
    public partial class BadgeStack
    {
        #region Static Fields

        /// <summary>
        /// The badge count property.
        /// </summary>
        public static readonly DependencyProperty BadgeCountProperty = DependencyProperty.Register(
            "BadgeCount", 
            typeof(int), 
            typeof(BadgeStack), 
            new PropertyMetadata(default(int)));

        /// <summary>
        /// The badge style property.
        /// </summary>
        public static readonly DependencyProperty BadgeStyleProperty = DependencyProperty.Register(
            "BadgeStyle", 
            typeof(Style), 
            typeof(BadgeStack), 
            new PropertyMetadata(default(Style)));

        /// <summary>
        /// The text property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", 
            typeof(string), 
            typeof(BadgeStack), 
            new PropertyMetadata(default(string)));

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BadgeStack"/> class.
        /// </summary>
        public BadgeStack()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the badge count.
        /// </summary>
        public int BadgeCount
        {
            get
            {
                return (int)this.GetValue(BadgeCountProperty);
            }

            set
            {
                this.SetValue(BadgeCountProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the badge style.
        /// </summary>
        public Style BadgeStyle
        {
            get
            {
                return (Style)this.GetValue(BadgeStyleProperty);
            }

            set
            {
                this.SetValue(BadgeStyleProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return (string)this.GetValue(TextProperty);
            }

            set
            {
                this.SetValue(TextProperty, value);
            }
        }

        #endregion
    }
}