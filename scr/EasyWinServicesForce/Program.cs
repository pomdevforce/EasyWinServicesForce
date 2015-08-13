namespace EasyWinServicesForce
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Atlas;
    using EasyWinServicesForce.IoC;
    using EasyWinServicesForce.Service;
    using Common.Logging;

    class Program
    {
        //C:\myservice.exe /console  to run
        static void Main(string[] args)
        {
            Type type = typeof(Program);
            ILog Log = LogManager.GetLogger(type);
            try
            {
                var configuration = Host.UseAppConfig<ServicesHost>();
                configuration.WithRegistrations(Boostrapper.Config);
                if (args != null && args.Any())
                    configuration = configuration.WithArguments(args);
                Host.Start(configuration);
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception during startup. ", ex);
                Console.ReadLine();
            }

        }
    }
}
