using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WebX.Logging {
    
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    
    using WebX.Enum;
    
    /// <summary>
    /// Logger abstract class.
    /// Provides a basic structure for all logger classes in WebX.
    ///
    /// All specialisations of this class are intended to be singletons ONLY!
    /// Loggers are not meant to be initialised more than once during runtime.
    /// </summary>
    public abstract class Logger {
        
        #region Log Format String Consts
        /// <summary>
        /// The RegEx pattern all patterns must match against.
        /// Formatter vars must be initialised with a dollar ($) sign,
        /// the identifier must be between opening and closing curly brackets ({ & }).
        /// The identifier must consist of letters (can be upper or lowercase, numbers, and underscores).
        /// Identifiers are NOT case-sensitive. cur_date == CuR_DAte
        /// </summary>
        public const string FormatterVarRegex = @"^(\$\{[A-z0-9_]{1,16}\})$";
        
        /// <summary>
        /// Current date.
        /// Format: System and locale-dependant.
        /// </summary>
        public const string CurrentDate = "${cur_date}";
        
        /// <summary>
        /// Current time.
        /// Format: 24hr
        /// </summary>
        public const string CurrentTime = "${cur_time}";

        /// <summary>
        /// Unix timestamp.
        /// Depending on system architecture, either a 32-bit or 64-bit integer
        /// representing the seconds passed, since 01. Jan. 1970.
        /// </summary>
        public const string UnixTimestamp = "${unix_tstamp}";

        /// <summary>
        /// Single string containing the date and time.
        /// Date format: system and locale-dependant.
        /// Time format: 24-hr.
        /// </summary>
        public const string DateTime = "${cur_datetime}";

        /// <summary>
        /// The log level associated with the message.
        /// </summary>
        public const string LogLevel = "${log_lvl}";

        /// <summary>
        /// The log message.
        /// </summary>
        public const string LogMessage = "${log_msg}";

        /// <summary>
        /// The default format used by WebX.
        /// </summary>
        public const string WebXLogFormat = "[${cur_date}] [${cur_time}] :: --${log_lvl}-- ${log_msg}";
        #endregion
        
        #region Constructors
        /// <inheritdoc />
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="loggerFormatString">The logging format to use.</param>
        protected Logger(string loggerFormatString = WebXLogFormat): this() {
            LoggerFormatString = loggerFormatString;
        }

        /// <summary>
        /// Default constructor.
        /// All initialisations which aren't specific to any overloaded constructor go here.
        /// </summary>
        protected Logger() {
            if (LoggerInstances is default) {
                LoggerInstances = new List<Logger>();
            }
        }
        #endregion
        
        protected static List<Logger> LoggerInstances { get; set; }
        
        /// <summary>
        /// Gets or sets the Logger format string.
        /// This format string is used to generate the entire log message.
        /// A log message is generally one line, but it may be more. 
        /// </summary>
        public string LoggerFormatString { get; set; }

        /// <summary>
        /// Writes the passed log message.
        /// </summary>
        /// <param name="logLevel">The log level. <see cref="WebX.Enum.LogLevel"/></param>
        /// <param name="formatMessage">String formatter. Uses the .net syntax for formatting strings. <see cref="string.Format(string,object)"/></param>
        /// <param name="formatParams"><see cref="string.Format(string,object)"/></param>
        public void WriteLog(LogLevel logLevel, string formatMessage, params object[] formatParams) =>
            WriteLog(logLevel, string.Format(formatMessage, formatParams));

        /// <summary>
        /// Writes the passed log message.
        /// </summary>
        /// <param name="logLevel">The log level. <see cref="WebX.Enum.LogLevel"/></param>
        /// <param name="logMessage">The message to be logged.</param>
        public virtual void WriteLog(LogLevel logLevel, string logMessage) =>
            LoggerInstances.ForEach(logger => logger.WriteLog(logLevel, logMessage));

        /// <summary>
        /// Formats the entire log output, using the defined <see cref="LoggerFormatString"/>.
        /// </summary>
        /// <param name="logLevel">The level of the logging message.</param>
        /// <param name="logMessage">The log message.</param>
        /// <returns>A string containing the full log message to be printed.</returns>
        public string FormatLogLine(LogLevel logLevel, string logMessage) {
            var tmpString = LoggerFormatString;
            var matchCollection = Regex.Matches(tmpString, FormatterVarRegex);
            var currentCulture = CultureInfo.CurrentCulture;
            var unixEpoch = new DateTime(1970, 01, 01);

            // Get all matches and compare them against the known variables
            // Replace if possible.
            // ReSharper disable once HeapView.ObjectAllocation.Possible
            foreach (Match match in matchCollection) {
                switch (match.Value) {
                    case LogLevel:
                        // ReSharper disable once HeapView.BoxingAllocation
                        tmpString = tmpString.Replace(match.Value, logLevel.ToString());
                        break;
                    case DateTime:
                        tmpString = tmpString.Replace(match.Value,
                            System.DateTime.Now.ToString("f", currentCulture));
                        break;
                    case LogMessage:
                        tmpString = tmpString.Replace(match.Value, logMessage);
                        break;
                    case CurrentDate:
                        tmpString = tmpString.Replace(match.Value, 
                                            System.DateTime.Now.ToString("d", currentCulture));
                        break;
                    case CurrentTime:
                        tmpString = tmpString.Replace(match.Value, 
                                            System.DateTime.Now.ToString("T", currentCulture));
                        break;
                    case UnixTimestamp:
                        tmpString = tmpString.Replace(match.Value,
                            ((long)System.DateTime.Now.Subtract(unixEpoch).TotalSeconds).ToString());
                        break;
                    default:
                        tmpString = tmpString.Replace(match.Value, 
                            $"{ match.Value } is not a recognized formatter var! Do you need an upgrade?");
                        break;
                }
            }

            return tmpString;
        }

        protected List<string> GetLoggerFormatVars() {
            var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Static | 
                                                        BindingFlags.FlattenHierarchy);

            // ReSharper disable once HeapView.ObjectAllocation
            return fields.Where(field => field.IsInitOnly && field.IsLiteral).Select(field => field.Name).ToList();
        }

    }
}