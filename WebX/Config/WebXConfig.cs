namespace WebX.Config {
    
    /// <summary>
    /// Basic application config. 
    /// </summary>
    public class WebXConfig {
        
        /// <summary>
        /// Gets or sets the main configuration directory.
        /// </summary>
        public string ConfigDir { get; set; }
        
        /// <summary>
        /// Gets or sets the root WWW directory.
        /// This is where the websites are stored.
        /// </summary>
        public string WwwRoot { get; set; }
        
        /// <summary>
        /// Gets or sets the WebX scripting directory.
        /// This is where scripts are stored which manipulate WebX and its behaviour.
        /// </summary>
        public string ScriptDir { get; set; }
        
    }
}