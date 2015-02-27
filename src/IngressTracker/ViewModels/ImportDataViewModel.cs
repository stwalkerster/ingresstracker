// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImportDataViewModel.cs" company="Simon Walker">
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
//   The import data view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows;

    using CsvHelper;
    using CsvHelper.Configuration;

    using IngressTracker.DataModel.Import;
    using IngressTracker.DataModel.Models;
    using IngressTracker.Properties;
    using IngressTracker.ScreenBase;
    using IngressTracker.Services.Interfaces;
    using IngressTracker.ViewModels.Interfaces;

    using Microsoft.Win32;

    using NHibernate;

    /// <summary>
    /// The import data view model.
    /// </summary>
    public class ImportDataViewModel : ScreenBase, IImportDataViewModel
    {
        #region Fields

        /// <summary>
        /// The data.
        /// </summary>
        private IEnumerable<ValueDataRow> data;

        /// <summary>
        /// The file name.
        /// </summary>
        private string fileName;

        /// <summary>
        /// The stage.
        /// </summary>
        private int stage;

        /// <summary>
        /// The stat names.
        /// </summary>
        private IEnumerable<ImporterStatisticMapping> statNames;

        /// <summary>
        /// The stats.
        /// </summary>
        private IEnumerable<Stat> stats;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ImportDataViewModel"/> class.
        /// </summary>
        /// <param name="databaseSession">
        /// The database session.
        /// </param>
        /// <param name="loginService">
        /// The login service.
        /// </param>
        public ImportDataViewModel(ISession databaseSession, ILoginService loginService)
            : base(Resources.ImportDataView, databaseSession, loginService)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether can browse.
        /// </summary>
        public bool CanBrowse
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can open file.
        /// </summary>
        public bool CanOpenFile
        {
            get
            {
                return (!string.IsNullOrEmpty(this.FileName)) && File.Exists(this.fileName);
            }
        }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileName;
            }

            set
            {
                if (value != this.fileName)
                {
                    this.fileName = value;
                    this.NotifyOfPropertyChange(() => this.FileName);
                    this.NotifyOfPropertyChange(() => this.CanOpenFile);
                }
            }
        }

        /// <summary>
        /// Gets or sets the stage.
        /// </summary>
        public int Stage
        {
            get
            {
                return this.stage;
            }

            set
            {
                if (value != this.stage)
                {
                    this.stage = value;
                    this.NotifyOfPropertyChange(() => this.Stage);
                }
            }
        }

        /// <summary>
        /// Gets or sets the stat names.
        /// </summary>
        public IEnumerable<ImporterStatisticMapping> StatNames
        {
            get
            {
                return this.statNames;
            }

            set
            {
                if (this.statNames != value)
                {
                    this.statNames = value;
                    this.NotifyOfPropertyChange(() => this.StatNames);
                }
            }
        }

        /// <summary>
        /// Gets or sets the stats.
        /// </summary>
        public IEnumerable<Stat> Stats
        {
            get
            {
                return this.stats;
            }

            set
            {
                if (this.stats != value)
                {
                    this.stats = value;
                    this.NotifyOfPropertyChange(() => this.Stats);
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The assign mappings.
        /// </summary>
        public void AssignMappings()
        {
            var lookupCache = this.StatNames.ToDictionary(x => x.StatName, y => y.Stat);

            var rejectList = new List<ValueDataRow>();

            foreach (var valueDataRow in this.data)
            {
                try
                {
                    this.DatabaseSession.Save(
                        new ValueEntry
                            {
                                Agent = this.LoginService.Agent, 
                                Statistic = lookupCache[valueDataRow.Statistic], 
                                Value = valueDataRow.DataValue, 
                                Timestamp = valueDataRow.Timestamp
                            });
                }
                catch (Exception)
                {
                    rejectList.Add(valueDataRow);
                }
            }

            if (rejectList.Any())
            {
                this.DatabaseSession.Transaction.Rollback();
                MessageBox.Show("Some data rejected, transaction has been rolled back");
            }

            this.TryClose();
        }

        /// <summary>
        /// The browse.
        /// </summary>
        public void Browse()
        {
            var dialog = new OpenFileDialog { DefaultExt = ".csv", Filter = "Comma-separated Values (.csv)|*.csv" };
            var result = dialog.ShowDialog();
            if (result.GetValueOrDefault(false))
            {
                this.FileName = dialog.FileName;
            }
        }

        /// <summary>
        /// The open file.
        /// </summary>
        public void OpenFile()
        {
            try
            {
                var dataStream = new StreamReader(File.Open(this.fileName, FileMode.Open));

                var csvConfiguration = new CsvConfiguration { HasHeaderRecord = true };
                csvConfiguration.RegisterClassMap<ValueMap>();

                var csv = new CsvReader(dataStream, csvConfiguration);
                this.data = csv.GetRecords<ValueDataRow>().ToList();

                this.StatNames = this.data.Select(x => x.Statistic).Distinct().Select(
                    x =>
                        {
                            var matching = this.Stats.Where(s => s.Description == x).ToList();
                            if (matching.Count() == 1)
                            {
                                return new ImporterStatisticMapping(matching.First(), x);
                            }

                            return new ImporterStatisticMapping(x);
                        });

                this.Stage = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on initialize.
        /// </summary>
        protected override void OnInitialize()
        {
            base.OnInitialize();

            this.Stage = 0;

            this.Stats = this.DatabaseSession.QueryOver<Stat>().List();
        }

        #endregion
    }
}