using System;

namespace WebX.Logging {
    
    using static System.Console;
    
    using WebX.Enum;
    
    /// <summary>
    /// Implements the <see cref="Logger"/> abstract class.
    /// This logger represents a Console logger. All logs given to this class will be output to the console.
    /// </summary>
    public class ConsoleLogger: Logger {
        
        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="loggerFormatString">The format string used to write each log entry.</param>
        public ConsoleLogger(string loggerFormatString = WebXLogFormat) {
            LoggerInstances.Add(this);
        }
        #endregion
        
        /// <inheritdoc cref="Logger.WriteLog(WebX.Enum.LogLevel,string)"/>
        /// <remarks>
        /// Must be overridden, must call <see cref="Logger.WriteLog(WebX.Enum.LogLevel,string)"/>!
        /// </remarks>
        public override void WriteLog(LogLevel logLevel, string logMessage) {
            base.WriteLog(logLevel, logMessage);
            WriteLine(FormatLogLine(logLevel, logMessage));
        }
    }
}