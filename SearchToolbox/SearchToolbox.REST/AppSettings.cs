namespace SearchToolbox.REST
{
    /// <summary>
    /// App settings class
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Name of the web api
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name of the web api
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Reference to logging information class
        /// </summary>
        public Logging Logging { get; set; }
    }

    /// <summary>
    /// Class to contain logging information
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// Path to log to
        /// </summary>
        public string LogFilePath { get; set; }
    
        /// <summary>
        /// Name of log file
        /// </summary>
        public string LogFileName { get; set; }
    }
}
