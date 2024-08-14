using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ABCBrasil.Hackthon.Api
{
    /// <summary>
    /// Program.
    /// </summary>
    public class Program
    {
        protected Program()
        {
        }

        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// CreateHostBuilder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(opt =>
                {
                    opt.UseKestrel();
                    opt.UseStartup<Startup>();
                });
        }
    }
}