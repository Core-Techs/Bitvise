namespace CoreTechs.Bitvise.WebService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BitviseServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.BitviseServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // BitviseServiceProcessInstaller
            // 
            this.BitviseServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.BitviseServiceProcessInstaller.Password = null;
            this.BitviseServiceProcessInstaller.Username = null;
            // 
            // BitviseServiceInstaller
            // 
            this.BitviseServiceInstaller.Description = "CoreTechs Bitvise Service";
            this.BitviseServiceInstaller.DisplayName = "CoreTechs Bitvise Service";
            this.BitviseServiceInstaller.ServiceName = "CoreTechs.BitviseService";
            this.BitviseServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.BitviseServiceProcessInstaller,
            this.BitviseServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller BitviseServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller BitviseServiceInstaller;
    }
}