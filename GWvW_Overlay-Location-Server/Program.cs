using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using GWvW_Overlay_Location_Server.AnetAPI;
using GWvW_Overlay_Location_Server.AnetAPI.Resources;

namespace GWvW_Overlay_Location_Server
{
    static class Program
    {

        private static ServiceHost _host;
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        static void Main()
        {
            Console.ReadLine();
            CreateService();
            Console.WriteLine("Server Started");
            Console.ReadLine();
            _host.Close();
            /*
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new GWvWOverlayLocationServerService() 
            };
            ServiceBase.Run(ServicesToRun);
            */
        }

        private static void CreateService()
        {
            _host = new ServiceHost(typeof(LocationService));
            _host.Open();
            _host.Faulted += (s, e) => CreateService();
        }

    }
}
