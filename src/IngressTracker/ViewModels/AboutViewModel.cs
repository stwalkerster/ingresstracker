// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AboutViewModel.cs" company="Simon Walker">
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
//   The about view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Caliburn.Micro;

    using IngressTracker.DataModel;
    using IngressTracker.Properties;
    using IngressTracker.ViewModels.Interfaces;

    /// <summary>
    /// The about view model.
    /// </summary>
    public class AboutViewModel : Screen, IAboutViewModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="AboutViewModel"/> class.
        /// </summary>
        public AboutViewModel()
        {
            this.LibraryLicences = new List<LibraryLicence>
                                       {
                                           new LibraryLicence
                                               {
                                                   Name = "Ingress Tracker", 
                                                   Licence =
                                                       string.Format(
                                                           Resources.LicMIT, 
                                                           2015, 
                                                           "Simon Walker")
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "Caliburn.Micro", 
                                                   Licence =
                                                       string.Format(
                                                           Resources.LicMIT, 
                                                           2010, 
                                                           "Blue Spire Consulting, Inc.")
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "StyleCop MSBuild", 
                                                   Licence =
                                                       string.Format(
                                                           Resources.LicMIT, 
                                                           2012, 
                                                           "Adam Ralph (adam@adamralph.com)")
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "Fluent.Ribbon", 
                                                   Licence =
                                                       string.Format(
                                                           Resources.LicMIT, 
                                                           "2009-2014", 
                                                           "Degtyarev Daniel, Rikker Serg, Bastian Schmidt (https://github.com/fluentribbon/Fluent.Ribbon)")
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "SQLite", 
                                                   Licence =
                                                       string.Format(Resources.LicPD, "SQLite")
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "StyleCop", 
                                                   Licence =
                                                       string.Format(Resources.LicMSPL)
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "CommonServiceHelper", 
                                                   Licence =
                                                       string.Format(Resources.LicMSPL)
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "Castle Windsor", 
                                                   Licence =
                                                       string.Format(Resources.LicApache2)
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "log4net", 
                                                   Licence =
                                                       string.Format(Resources.LicApache2)
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "NHibernate", 
                                                   Licence =
                                                       string.Format(Resources.LicLGPL2)
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "Fluent NHibernate", 
                                                   Licence =
                                                       string.Format(
                                                           Resources.LicBSD, 
                                                           "2008-2009", 
                                                           "James Gregory and contributors")
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "CsvHelper", 
                                                   Licence =
                                                       string.Format(
                                                           "{0}\r\n\r\n{1}", 
                                                           string.Format(Resources.LicMSPL), 
                                                           string.Format(Resources.LicApache2))
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "Fluent Validation", 
                                                   Licence = string.Format(Resources.LicApache2)
                                               }, 
                                           new LibraryLicence
                                               {
                                                   Name = "Extended WPF Toolkit Community Edition", 
                                                   Licence = string.Format(Resources.LicMSPL)
                                               }, 
                                       };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the copyright.
        /// </summary>
        public string Copyright
        {
            get
            {
                object[] attributes = this.GetType()
                    .Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

                if (attributes.Length > 0)
                {
                    var attribute = attributes[0] as AssemblyCopyrightAttribute;
                    if (attribute != null)
                    {
                        return attribute.Copyright;
                    }
                }

                return "Copyright © " + DateTime.Now.Year;
            }
        }

        /// <summary>
        /// Gets or sets the library licences.
        /// </summary>
        public IEnumerable<LibraryLicence> LibraryLicences { get; set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public string Version
        {
            get
            {
                return string.Format("{1} : {0}", Assembly.GetExecutingAssembly().GetName().Version, Resources.Version);
            }
        }

        #endregion
    }
}