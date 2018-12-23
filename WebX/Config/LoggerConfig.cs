using System;
using System.Collections.Generic;
using WebX.Enum;
using WebX.Logging;

namespace WebX.Config {
    
    using YamlDotNet.Serialization;
    
    /// <summary>
    /// Application-wide logger configuration.
    /// </summary>
    public class LoggerConfig {
        
        /// <summary>
        /// Gets or sets the log format.
        /// </summary>
        public string LogFormat { get; set; }
        
        /// <summary>
        /// Gets or sets the different types of loggers to use with
        /// in WebX.
        /// </summary>
        public List<string> LogType { get; set; }
        
        /// <summary>
        /// Gets or sets the application-wide log level.
        /// </summary>
        public LogLevel LogLevel { get; set; }
        
        /// <summary>
        /// Gets or sets the logging directory.
        /// </summary>
        public string LogDir { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether logs should be rotated.
        /// </summary>
        public bool RotateLogs { get; set; }
        
        /// <summary>
        /// Gets or sets the maximum number of rotations a log can go through, without being
        /// deleted.
        /// E.g.: 10: maximum 10 files before the last file is truncated.
        /// </summary>
        public uint MaxRotations { get; set; }
        
        /// <summary>
        /// Gets or sets the rotation threshold.
        /// Alpha-numerical, suffixed with B (Bytes), K (Kibibytes), M (Mebibytes), G (Gibibytes).
        /// </summary>
        public string RotateThreshold { get; set; }
        
    }
}