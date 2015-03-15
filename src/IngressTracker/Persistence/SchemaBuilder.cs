// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SchemaBuilder.cs" company="Simon Walker">
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
//   The schema builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace IngressTracker.Persistence
{
    using System;
    using System.Data.SQLite;
    using System.Diagnostics;
    using System.IO;

    using IngressTracker.Properties;

    using EnumerableExtensions = Caliburn.Micro.EnumerableExtensions;

    /// <summary>
    /// The schema builder.
    /// </summary>
    public class SchemaBuilder
    {
        #region Fields

        /// <summary>
        /// The connection.
        /// </summary>
        private readonly SQLiteConnection connection;

        /// <summary>
        /// The file.
        /// </summary>
        private readonly string file;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="SchemaBuilder"/> class.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        public SchemaBuilder(string file)
        {
            this.file = file;

            var connectionString = string.Format("Data Source={0};Version=3;", file);
            this.connection = new SQLiteConnection(connectionString);
            this.connection.Open();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get schema version.
        /// </summary>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public long GetSchemaVersion()
        {
            var cmd = new SQLiteCommand("PRAGMA user_version;", this.connection);
            return (long)cmd.ExecuteScalar();
        }

        /// <summary>
        /// The upgrade schema.
        /// </summary>
        public void UpgradeSchema()
        {
            var version = this.GetSchemaVersion();

            if (version < 1)
            {
                this.SchemaUpgrade(
                    version, 
                    () =>
                        {
                            this.PerformUpgradeStage(Resources.SchemaUpgrade0To1a);
                            this.PerformUpgradeStage(Resources.SchemaUpgrade0To1b);
                            this.PerformUpgradeStage(Resources.SchemaUpgrade0To1c);
                            this.PerformUpgradeStage(Resources.SchemaUpgrade0To1d);
                            this.PerformUpgradeStage(Resources.SchemaUpgrade0To1e);
                            this.PerformUpgradeStage(Resources.SchemaUpgrade0To1f);
                            this.PerformUpgradeStage(Resources.SchemaUpgrade0To1g);

                            EnumerableExtensions.Apply(
                                Resources.SchemaUpgrade0To1h.Split('\r', '\n'), 
                                this.PerformUpgradeStage);
                        });

                version = 1;
                this.SetSchemaVersion(version);
            }

            if (version < 2)
            {
                this.SchemaUpgrade(
                    version, 
                    () =>
                    EnumerableExtensions.Apply(Resources.SchemaUpgrade1To2.Split('\r', '\n'), this.PerformUpgradeStage));
                this.SetSchemaVersion(2);
                version = 2;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The perform upgrade stage.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        private void PerformUpgradeStage(string command)
        {
            var cmd = new SQLiteCommand(command, this.connection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// The schema upgrade.
        /// </summary>
        /// <param name="currentVersion">
        /// The current version.
        /// </param>
        /// <param name="task">
        /// The task.
        /// </param>
        private void SchemaUpgrade(long currentVersion, Action task)
        {
            var backupFileName = this.file + "-schema" + currentVersion;

            if (File.Exists(backupFileName))
            {
                File.Delete(backupFileName);
            }

            File.Copy(this.file, backupFileName);
            try
            {
                task();
            }
            catch (Exception ex)
            {
                this.connection.Close();

                // Force a GC run to close the file.
                GC.Collect();
                GC.WaitForPendingFinalizers();

                File.Delete(this.file);
                File.Move(backupFileName, this.file);

                var errorFile = this.file + "-error.txt";

                File.WriteAllText(errorFile, string.Format(Resources.SchemaUpgradeFailed, ex.Message));
                Process.Start(errorFile);

                Environment.Exit(1);
            }
        }

        /// <summary>
        /// The set schema version.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        private void SetSchemaVersion(long version)
        {
            var cmd = new SQLiteCommand("PRAGMA user_version = " + version + ";", this.connection);
            cmd.ExecuteNonQuery();
        }

        #endregion
    }
}