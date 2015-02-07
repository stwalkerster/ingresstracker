namespace IngressTracker.ViewModels
{
    using Caliburn.Micro;

    using Castle.Core.Logging;

    using IngressTracker.ViewModels.Interfaces;

    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShellViewModel
    {        
        #region Fields

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;
        
        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public ShellViewModel(ILogger logger)
        {
            this.logger = logger;
        }

        #endregion
    }
}