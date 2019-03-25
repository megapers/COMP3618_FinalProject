using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SearchToolbox.REST
{
    /// <summary>
    /// Main class for the web api
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method for the web api
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        /// <summary>
        /// Method used to start and run the web api
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
