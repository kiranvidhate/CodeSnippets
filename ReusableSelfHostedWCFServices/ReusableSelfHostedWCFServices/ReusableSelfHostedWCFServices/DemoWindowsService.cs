using System;
using System.Configuration;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceModel;
using System.ServiceProcess;

namespace serviceDemo
{
    public class DemoWindowsService : ServiceBase
	{
		public ServiceHost serviceHost = null;
		public object tcpListener = true
			? new object()
			: null;

		public DemoWindowsService()
		{
			// Set the service name
			ServiceName = Constants.productName; // This must be identical to the System.ServiceProcess.ServiceBase.ServiceName set in the WindowsServiceInstaller class
		}

		public static void Main()
		{
            DemoWindowsService svc = new DemoWindowsService();
		}

		// Start the Windows service
		protected override void OnStart(string[] args)
		{
			if (serviceHost != null)
				serviceHost.Close();

            // Create a ServiceHost for the WindowsService type and provide the base address
			serviceHost = new ServiceHost(typeof(DemoService));

			// Open the ServiceHostBase to create listeners and start listening for messages
			serviceHost.Open();
        }

		// Stop the Windows service
		protected override void OnStop()
		{
			if (serviceHost != null)
			{
				serviceHost.Close();
				serviceHost = null;
			}
		}

		public static void StopService(string sStoppingMessage)
		{
			ServiceController scdemoService = new ServiceController("serviceDemo");

			try
			{
				scdemoService.Stop();
			}
			catch (Exception)
			{
				// Ignore exceptions
			}
		}
	}

	// Allow the service to be installed by the InstallUtil.exe tool
	[RunInstaller(true)]
	public class WindowsServiceInstaller : Installer
	{
		private ServiceProcessInstaller spiProcess;
		private ServiceInstaller siInstaller;

		public WindowsServiceInstaller()
		{
			spiProcess = new ServiceProcessInstaller();
			spiProcess.Account = ServiceAccount.LocalSystem;

			siInstaller = new ServiceInstaller();
			siInstaller.ServiceName = Constants.productName;
			siInstaller.StartType = ServiceStartMode.Automatic;

			Installers.Add(spiProcess);
			Installers.Add(siInstaller);
		}
	}
}
