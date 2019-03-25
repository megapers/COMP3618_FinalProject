using System.Collections.Generic;

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
        /// The authority for the connection token
        /// </summary>
        public string Authority { get; set; }
        /// <summary>
        /// Object containing the connection parameters for the web api
        /// </summary>
        public DBConnection DBConnection { get; set; }
        /// <summary>
        /// Object containing the identity server configuration
        /// </summary>
        public IdentityServer IdentityServer { get; set; }
    }
    /// <summary>
    /// Class containing the connection parameters for the web api
    /// </summary>
    public class DBConnection
    {
        /// <summary>
        /// Spiral server name
        /// </summary>
        public string SpiralServer { get; set; }
        /// <summary>
        /// SpiralClients server name
        /// </summary>
        public string SpiralClientsServer { get; set; }
    }
    /// <summary>
    /// Class containing the identity server configuration
    /// </summary>
    public class IdentityServer
    {
        /// <summary>
        /// List of identity server client details
        /// </summary>
        public List<IdentityServerClient> Clients { get; set; }
    }
    /// <summary>
    /// Class containing identity server user information
    /// </summary>
    public class IdentityServerClient
    {
        /// <summary>
        /// Username of the identity server client
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Display name of the identity server client
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// Password of the identity server client
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
