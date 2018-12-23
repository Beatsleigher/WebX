namespace WebX.Enum {
    
    /// <summary>
    /// Defines the different types of request lists applicable to any given site.
    /// Request lists define lists of blocks of IPs or individual IPs (including IPv6) to white- or blacklist.
    ///
    /// This feature is useful if the server is open to the WWW and a website should only be available to the intranet.
    /// </summary>
    public enum RequestListType {
        
        /// <summary>
        /// The request list type is a whitelist.
        /// Only allow certain blocks of IPs or individual IPs.
        /// </summary>
        Whitelist,
        
        /// <summary>
        /// The request list type is a blacklist.
        /// Block requests from certain blocks of IPs or individual IPs.
        /// </summary>
        Blacklist,
        
        /// <summary>
        /// No request list type. Allow all, disallow none.
        /// </summary>
        None
        
    }
}
