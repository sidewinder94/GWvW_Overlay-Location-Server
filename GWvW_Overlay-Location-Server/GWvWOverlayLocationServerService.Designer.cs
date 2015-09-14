using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace GWvW_Overlay_Location_Server
{
    partial class GWvWOverlayLocationServerService
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            this.notifyIcon = new NotifyIcon(this.components);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "GWvW Overlay Location Service";
            this.notifyIcon.Visible = true;
            // 
            // GWvWOverlayLocationServerService
            // 
            this.ServiceName = "GWvWOverlayLocationServerService";

        }

        #endregion

        private NotifyIcon notifyIcon;
    }
}
