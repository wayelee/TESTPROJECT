using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;

namespace CERMapping
{
    static class Program
    {
        private static LicenseInitializer m_AOLicenseInitializer = new CERMapping.LicenseInitializer();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ESRI License Initializer generated code.
            m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngine },
            new esriLicenseExtensionCode[] { esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst });
            if (!RuntimeManager.Bind(ProductCode.Engine))
            {
                if (!RuntimeManager.Bind(ProductCode.Desktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
            //ESRI License Initializer generated code.
            //Do not make any call to ArcObjects after ShutDownApplication()
            m_AOLicenseInitializer.ShutdownApplication();
        }
    }
}
