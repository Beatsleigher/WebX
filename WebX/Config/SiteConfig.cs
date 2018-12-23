using System;
using YamlDotNet.Serialization;

namespace WebX.Config {
    
    using System.Collections.Generic;
    using System.Net;
    
    using WebX.Enum;
    
    /// <summary>
    /// Defines the configuration for any given site. 
    /// </summary>
    public class SiteConfig {
        
        /// <summary>
        /// Defines the server names WebX will listen for and re-direct to this website.
        /// 
        /// </summary>
        public List<string> ServerNames { get; set; }
        
        public List<int> ListenPorts { get; set; }
        
        public RequestListType RequestListType { get; set; }
        
        public List<IPAddress> RequestList { get; set; }

        public SiteLogs SiteLogs { get; set; }
        
        public string MainFile { get; set; }
        
        public string DefaultFileType { get; set; }
        
        [YamlMember(Alias = "allow_exec")]
        public List<string> AllowedExecutables { get; set; }
        
        [YamlMember(Alias = "disallow_exec")]
        public List<string> ForbiddenExecutables { get; set; }
        
        public string DocumentRoot { get; set; }
        
        public string SiteLogRoot { get; set; }
        
    }

    public struct SiteLogs {
        
        /// <summary>
        /// Gets or sets the access log.
        /// </summary>
        public string AccessLog { get; set; }
        
        /// <summary>
        /// Gets or sets the error log. 
        /// </summary>
        public string ErrorLog { get; set; }
        
        /// <summary>
        /// Gets or sets the general log.
        /// </summary>
        public string GeneralLog { get; set; }
        
        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        public LogLevel LogLevel { get; set; }
        
    }
    
}
