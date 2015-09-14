using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GWvW_Overlay_Location_Server
{
    // ReSharper disable once InconsistentNaming
    public partial class GWvWOverlayLocationServerService : ServiceBase
    {
        private ServiceHost _host;
        public static NotifyIcon DesktopNotification;

        public GWvWOverlayLocationServerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            DesktopNotification = notifyIcon;
            CreateService();
        }

        private void CreateService()
        {
            _host = new ServiceHost(typeof(LocationService));
            _host.Open();
            _host.Faulted += (s, e) => CreateService();
        }

        protected override void OnStop()
        {
            _host.Close();
        }
    }
}
