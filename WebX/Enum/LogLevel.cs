using System;

namespace WebX.Enum {
    
    /// <summary>
    /// Contains the different logging levels applicable to both the application itself
    /// and the different websites being managed by WebX.
    /// Log levels can be added to one another, like flags. 
    /// </summary>
    [Flags]
    public enum LogLevel {
        
        /// <summary>
        /// Basic logging, the default for the application.
        /// This log level is ideal for most applications.
        /// </summary>
        Basic,
        
        /// <summary>
        /// Display only catastrophic failures, such as those which will definitely
        /// cause a connection to be terminated, or even worse: the application to restart.
        /// </summary>
        CatastrophicOnly,
        
        /// <summary>
        /// Debug-level information.
        /// Debug-level information is like a chatty teenager or middle-aged wife.
        /// They won't shut up. Applying this log level will tell you just about everything
        /// going on during its life.
        /// </summary>
        Debug,
        
        /// <summary>
        /// Display only (minor) errors.
        /// Useful for tracking down errors without the rest of the bloat.
        /// </summary>
        ErrorsOnly,
        
        /// <summary>
        /// Display only warnings.
        /// </summary>
        WarningsOnly,
        
        /// <summary>
        /// Verbose logging.
        /// Includes basic information, and some more in-depth information.
        /// </summary>
        Verbose
        
    }
}