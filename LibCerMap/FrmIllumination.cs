using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;

using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmIllumination : OfficeForm
    {
        int n = 1;
        double azimuth = 0.0;
        double altitude = 0.0;
        int contrast = 0;

        IScene pScene = null;
        ISceneGraph pSceneGraph = null;
        AxSceneControl pscenecontrol = null;
        IVector3D pVector3D = null;

        public FrmIllumination(AxSceneControl scenecontrol)
        {
            InitializeComponent();
            this.EnableGlass = false;
            pscenecontrol = scenecontrol;
        }

        private void FrmIllumination_Load(object sender, EventArgs e)
        {
            pScene = pscenecontrol.Scene;
            pSceneGraph = pScene.SceneGraph;
            pVector3D = pSceneGraph.SunVector;
            //lblAziumth.Text = pVector3D.Azimuth.ToString();
            //lblaltitude.Text = pVector3D.Inclination.ToString();
            //lblcontrast.Text = pSceneGraph.Contrast.ToString();
            azimuth = pVector3D.Azimuth;
            altitude = pVector3D.Inclination;
            contrast = pSceneGraph.Contrast;
            ksAzimuth.Value = (decimal)getdegrees(pVector3D.Azimuth);
            ksAltitude.Value = (decimal)getdegrees(pVector3D.Inclination);
            trackcontrast.Value = pSceneGraph.Contrast;
            lblAziumth.Text = ksAzimuth.Value.ToString();
            lblaltitude.Text = ksAltitude.Value.ToString();
            lblcontrast.Text = trackcontrast.Value.ToString();
            n++;
        }

        private void ksAzimuth_ValueChanged(object sender, DevComponents.Instrumentation.ValueChangedEventArgs e)
        {
            if (n==1)
            {
                return;
            }
            else
            {
                lblAziumth.Text = ksAzimuth.Value.ToString();
                pVector3D.Azimuth = getradians(Convert.ToDouble(lblAziumth.Text));
                pSceneGraph.SunVector = pVector3D;
                pSceneGraph.RefreshViewers();
            }
        }

        private void ksAltitude_ValueChanged(object sender, DevComponents.Instrumentation.ValueChangedEventArgs e)
        {
            if (n == 1)
            {
                return;
            }
            else
            {
                lblaltitude.Text = ksAltitude.Value.ToString();
                pVector3D.Inclination = getradians(Convert.ToDouble(lblaltitude.Text));
                pSceneGraph.SunVector = pVector3D;
                pSceneGraph.RefreshViewers();
            }
        }

        private void trackcontrast_ValueChanged(object sender, EventArgs e)
        {
            if (n == 1)
            {
                return;
            }
            else
            {
                lblcontrast.Text = trackcontrast.Value.ToString();
                pSceneGraph.Contrast = Convert.ToInt32(lblcontrast.Text);
                pSceneGraph.RefreshViewers();
            }            
        }

        private double getradians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
        private double getdegrees(double radians)
        {
            return radians*180/Math.PI;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            pVector3D.Azimuth = azimuth;
            pVector3D.Inclination = altitude;
            pSceneGraph.Contrast = contrast;
            pSceneGraph.SunVector = pVector3D;
            ksAzimuth.Value = (decimal)getdegrees(pVector3D.Azimuth);
            ksAltitude.Value = (decimal)getdegrees(pVector3D.Inclination);
            trackcontrast.Value = pSceneGraph.Contrast;
            lblAziumth.Text = ksAzimuth.Value.ToString();
            lblaltitude.Text = ksAltitude.Value.ToString();
            lblcontrast.Text = trackcontrast.Value.ToString();
            pSceneGraph.RefreshViewers();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            pVector3D.Azimuth = azimuth;
            pVector3D.Inclination = altitude;
            pSceneGraph.Contrast = contrast;
            pSceneGraph.SunVector = pVector3D;
            pSceneGraph.RefreshViewers();
            this.Close();
        }
        
    }
}
