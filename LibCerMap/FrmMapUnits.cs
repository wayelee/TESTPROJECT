using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using DevComponents.DotNetBar;

namespace LibCerMap
{
    public partial class FrmMapUnits : OfficeForm
    {
        AxMapControl pMapControl;
        public int pDegreeMode;
        public FrmMapUnits(AxMapControl mapcontrol)
        {
            InitializeComponent();
            pMapControl = mapcontrol;
            this.EnableGlass = false;
        }

        private void FrmMapUnits_Load(object sender, EventArgs e)
        {
            //地图单位
            string mapunits = pMapControl.Map.MapUnits.ToString();
            string units = mapunits.Substring(4, mapunits.Length - 4);
            cmbMapUnits.Text = units;
            if (!(units == "UnknownUnits"))
            {
                cmbMapUnits.Enabled = false;
            }
            //显示单位
             mapunits = pMapControl.Map.MapUnits.ToString();
            units = mapunits.Substring(4, mapunits.Length - 4);
            cmbDisplayUnits.Text = units;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbMapUnits.Text == "UnKnownUnits")
            {
                pMapControl.Map.MapUnits = esriUnits.esriUnknownUnits;
                //pDegreeMode = 0;
            }
            if (cmbMapUnits.Text == "Inches")
            {
                pMapControl.Map.MapUnits = esriUnits.esriInches;
                //pDegreeMode = 0;
            }
            if (cmbMapUnits.Text == "Points")
            {
                pMapControl.Map.MapUnits = esriUnits.esriPoints;
                //pDegreeMode = 0;
            }
            if (cmbMapUnits.Text == "Feet")
            {
                pMapControl.Map.MapUnits = esriUnits.esriFeet;
                //pDegreeMode = 0;
            }
            if (cmbMapUnits.Text == "Yards")
            {
                pMapControl.Map.MapUnits = esriUnits.esriYards;
                //pDegreeMode = 0;
            }
            if (cmbMapUnits.Text == "Miles")
            {
                pMapControl.Map.MapUnits = esriUnits.esriMiles;
               // pDegreeMode = 0;
            }
            if (cmbMapUnits.Text == "NauticalMiles")
            {
                pMapControl.Map.MapUnits = esriUnits.esriNauticalMiles;
                //pDegreeMode = 0;
            }
            if (cmbMapUnits.Text == "Millimeters")
            {
                pMapControl.Map.MapUnits = esriUnits.esriMillimeters;
                //pDegreeMode = 0;
            }
            if (cmbMapUnits.Text == "Centimeters")
            {
                pMapControl.Map.MapUnits = esriUnits.esriCentimeters;
                //pDegreeMode = 0;
            }
            if (cmbMapUnits.Text == "Meters")
            {
                pMapControl.Map.MapUnits = esriUnits.esriMeters;
                //pDegreeMode = 0;
            }
            if (cmbMapUnits.Text == "Kilometers")
            {
                pMapControl.Map.MapUnits = esriUnits.esriKilometers;
                //pDegreeMode = 0;
            }
            if (cmbMapUnits.Text == "DecimalDegrees")
            {
                pMapControl.Map.MapUnits = esriUnits.esriDecimalDegrees;
            }
            if (cmbMapUnits.Text == "Decimeters")
            {
                pMapControl.Map.MapUnits = esriUnits.esriDecimeters;
               // pDegreeMode = 0;
            }

            if (cmbDisplayUnits.Text == "UnKnownUnits")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriUnknownUnits;
                pDegreeMode = 0;
            }
            if (cmbDisplayUnits.Text == "Inches")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriInches;
                pDegreeMode = 0;
            }
            if (cmbDisplayUnits.Text == "Points")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriPoints;
                pDegreeMode = 0;
            }
            if (cmbDisplayUnits.Text == "Feet")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriFeet;
                pDegreeMode = 0;
            }
            if (cmbDisplayUnits.Text == "Yards")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriYards;
                pDegreeMode = 0;
            }
            if (cmbDisplayUnits.Text == "Miles")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriMiles;
                pDegreeMode = 0;
            }
            if (cmbDisplayUnits.Text == "NauticalMiles")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriNauticalMiles;
                pDegreeMode = 0;
            }
            if (cmbDisplayUnits.Text == "Millimeters")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriMillimeters;
                pDegreeMode = 0;
            }
            if (cmbDisplayUnits.Text == "Centimeters")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriCentimeters;
                pDegreeMode = 0;
            }
            if (cmbDisplayUnits.Text == "Meters")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriMeters;
                pDegreeMode = 0;
            }
            if (cmbDisplayUnits.Text == "Kilometers")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriKilometers;
                pDegreeMode = 0;
            }
            if (cmbDisplayUnits.Text == "DecimalDegrees")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriDecimalDegrees;
                pDegreeMode = 1;
            }
            if (cmbDisplayUnits.Text == "DegreeaMinutesSecond")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriDecimalDegrees;
                pDegreeMode = 2;
            }
            if (cmbDisplayUnits.Text == "Decimeters")
            {
                pMapControl.Map.DistanceUnits = esriUnits.esriDecimeters;
                pDegreeMode = 0;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
     
        private void cmbunits_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      
    }
}
