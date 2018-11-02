using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Controls;
using System.Collections;
using System.Drawing.Printing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geoprocessing;

namespace LibCerMap
{
    public partial class FrmPageSetup : Form
    {
        internal PageSetupDialog pPageSetupDialog;
        //declare a PrintDocument object named document, to be displayed in the print preview
        public System.Drawing.Printing.PrintDocument document = new System.Drawing.Printing.PrintDocument();

        private IPageLayoutControl3 m_pPageLayoutCtl = null;

        public FrmPageSetup(IPageLayoutControl3 pageCtl)
        {
            InitializeComponent();
            m_pPageLayoutCtl = pageCtl;
            
        }

        private void FrmPageSetup_Load(object sender, EventArgs e)
        {
            //页面设置初始化
            InitializePageSetupDialog(); //initialize the page setup dialog   

            cboPageSize.Items.Add("Letter - 8.5in x 11in.");
            cboPageSize.Items.Add("Legal - 8.5in x 14in.");
            cboPageSize.Items.Add("Tabloid - 11in x 17in.");
            cboPageSize.Items.Add("C - 17in x 22in.");
            cboPageSize.Items.Add("D - 22in x 34in.");
            cboPageSize.Items.Add("E - 34in x 44in.");
            cboPageSize.Items.Add("A5 - 148mm x 210mm.");
            cboPageSize.Items.Add("A4 - 210mm x 297mm.");
            cboPageSize.Items.Add("A3 - 297mm x 420mm.");
            cboPageSize.Items.Add("A2 - 420mm x 594mm.");
            cboPageSize.Items.Add("A1 - 594mm x 841mm.");
            cboPageSize.Items.Add("A0 - 841mm x 1189mm.");
            cboPageSize.Items.Add("Custom Page Size.");
            //cboPageSize.Items.Add("Same as Printer Form.");
            //初始化单位列表
            cmbPageUnit.Items.Clear();
            for (int i = 0; i < 13;i++ )
            {
                cmbPageUnit.Items.Add(((esriUnits)i).ToString().Replace("esri",""));
            }

            IPage page = m_pPageLayoutCtl.Page;
            if (page.FormID == esriPageFormID.esriPageFormSameAsPrinter)
            {
                panelCustom.Enabled = false;            
                UpdatePrintingDisplay();
            }
            else
            {
                chkUsePrinterPage.Checked = false;
                cboPageSize.SelectedIndex = (int)m_pPageLayoutCtl.Page.FormID;

                UpdatePrintPageDisplay();
            }

            chkUsePrinterPage.Checked = !panelCustom.Enabled;
            chkUsePrinterPage_CheckedChanged(null, null);
        }

        private void UpdatePageSizedisplay()
        {
            double dPageWidth = 0.0;
            double dPageHeight = 0.0;//页面的宽度和高度
            esriUnits unit = m_pPageLayoutCtl.Page.Units; ;
            IPage page = m_pPageLayoutCtl.Page;
            if (page.FormID == esriPageFormID.esriPageFormSameAsPrinter)
            {
                if (m_pPageLayoutCtl.Printer == null)
                {
                    page.QuerySize(out dPageWidth, out dPageHeight);
                }
                else
                {
                    ESRI.ArcGIS.Output.IPaper paper = m_pPageLayoutCtl.Printer.Paper;
                    paper.QueryPaperSize(out dPageWidth, out dPageHeight);
                    unit = paper.Units;
                }
            }
            else
            {
                page.QuerySize(out dPageWidth, out dPageHeight);
            }

            if (unit != page.Units)
            {
                IGPLinearUnit gpLinearUnit = new GPLinearUnitClass();
                gpLinearUnit.Units = page.Units;
                gpLinearUnit.Value = dPageWidth;
                dPageWidth = gpLinearUnit.ConvertValue(page.Units);
                gpLinearUnit.Value = dPageHeight;
                dPageHeight = gpLinearUnit.ConvertValue(page.Units);
            }
            txtPageWidth.Value = dPageWidth;
            txtPageHeight.Value = dPageHeight;

            if (m_pPageLayoutCtl.Page.Orientation == 1)
            {
                radioOritationPortrait.Checked = true;
            }
            else
            {
                radioOritationLandscape.Checked = true;
            }

            //设置单位列表
            cmbPageUnit.SelectedIndex = (int)page.Units;
            
        }


        private void UpdatePrintPageDisplay()
        {
            //显示页面大小
            switch (cboPageSize.SelectedIndex)
            {
                case 12:
                    panelExWH.Enabled = true;
                    break;
                
                default:
                    panelExWH.Enabled = false;
                    break;
            }
            UpdatePageSizedisplay();
        }

        private void UpdatePrintingDisplay()
        {
            if (m_pPageLayoutCtl.Printer != null)
            {
                //Get IPrinter interface through the PageLayoutControl's printer
                IPrinter printer = m_pPageLayoutCtl.Printer;

                //Determine the orientation of the printer's paper
                if (printer.Paper.Orientation == 1)
                {
                    radioOritationPortrait.Checked = true;
                }
                else
                {
                    radioOritationLandscape.Checked = true;
                }

                //Determine the printer name
               cmbPrinters.Text = printer.Paper.PrinterName;

            }
 
             UpdatePageSizedisplay();
        }

        private void InitializePageSetupDialog()
        {
            //create a new PageSetupDialog using constructor
            pPageSetupDialog = new PageSetupDialog();
            //initialize the dialog's PrinterSettings property to hold user defined printer settings
            pPageSetupDialog.PageSettings = new System.Drawing.Printing.PageSettings();
            //initialize dialog's PrinterSettings property to hold user set printer settings
            pPageSetupDialog.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            //do not show the network in the printer dialog
            pPageSetupDialog.ShowNetwork = false;
        }

        private void chkUsePrinterPage_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsePrinterPage.Checked)
            {
                m_pPageLayoutCtl.Page.FormID = esriPageFormID.esriPageFormSameAsPrinter;
                panelCustom.Enabled = false;
                btnPrinterPage.Enabled = true;
                cmbPrinters.Enabled = true;
            }
            else
            {
                m_pPageLayoutCtl.Page.FormID = (esriPageFormID)cboPageSize.SelectedIndex;
                panelCustom.Enabled = true;
                btnPrinterPage.Enabled = false;
                cmbPrinters.Enabled = false;
            }
            if (cboPageSize.SelectedIndex <0)
            {
                cboPageSize.SelectedIndex = 12;
            }
        }



        private void btnPrinterPage_Click(object sender, EventArgs e)
        {            
            //开始页面设置调用
            DialogResult result = pPageSetupDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                //set the printer settings of the preview document to the selected printer settings
                document.PrinterSettings = pPageSetupDialog.PrinterSettings;
                //set the page settings of the preview document to the selected page settings
                document.DefaultPageSettings = pPageSetupDialog.PageSettings;

                //due to a bug in PageSetupDialog the PaperSize has to be set explicitly by iterating through the
                //available PaperSizes in the PageSetupDialog finding the selected PaperSize
                int i;
                IEnumerator paperSizes = pPageSetupDialog.PrinterSettings.PaperSizes.GetEnumerator();
                paperSizes.Reset();
                for (i = 0; i < pPageSetupDialog.PrinterSettings.PaperSizes.Count; ++i)
                {
                    paperSizes.MoveNext();
                    if (((PaperSize)paperSizes.Current).Kind == document.DefaultPageSettings.PaperSize.Kind)
                    if (((PaperSize)paperSizes.Current).PaperName == document.DefaultPageSettings.PaperSize.PaperName)
                    {
                        document.DefaultPageSettings.PaperSize = ((PaperSize)paperSizes.Current);
                    }
                }

                IPaper paper = new PaperClass(); //create a paper object
                IPrinter printer = new EmfPrinterClass(); //create a printer object
                paper.Attach(pPageSetupDialog.PrinterSettings.GetHdevmode(pPageSetupDialog.PageSettings).ToInt32(), pPageSetupDialog.PrinterSettings.GetHdevnames().ToInt32());

                //pass the paper to the emf printer
                printer.Paper = paper;
                //set the page layout control's printer to the currently selected printer
                m_pPageLayoutCtl.Printer = printer;

                UpdatePageSizedisplay();
                m_pPageLayoutCtl.ActiveView.Refresh();
            }
        }

        
        private void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPageSize.SelectedIndex == 13)
                EnableOrientation(false);
            else
                EnableOrientation(true);
            //Set the page size
            m_pPageLayoutCtl.Page.FormID = (esriPageFormID)cboPageSize.SelectedIndex;

            UpdatePageSizedisplay();
            radioOritationLandscape_Click(null, null);
            radioOritationPortrait_Click(null, null);
            //Update printer page display
            UpdatePrintPageDisplay();
        }

        private void EnableOrientation(bool b)
        {
            radioOritationLandscape.Enabled = b;
            radioOritationLandscape.Enabled = b;
        }

        private void radioOritationLandscape_Click(object sender, EventArgs e)
        {
            if (radioOritationLandscape.Checked == true)
            {
                if (chkUsePrinterPage.Checked == false && cboPageSize.SelectedIndex == 12)
                {
                    SetCustomPage();
                }
                //Set the page orientation
                if (m_pPageLayoutCtl.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter)
                {
                    m_pPageLayoutCtl.Page.Orientation = 2;
                }
                
                //Update printer page display
                UpdatePrintPageDisplay();
            }
        }

        private void radioOritationPortrait_Click(object sender, EventArgs e)
        {
            if (radioOritationPortrait.Checked == true)
            {
                //Set the page orientation
                if(chkUsePrinterPage.Checked == false && cboPageSize.SelectedIndex == 12)
                {
                    SetCustomPage();
                }
                if (m_pPageLayoutCtl.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter)
                {
                    m_pPageLayoutCtl.Page.Orientation = 1;
                }
                //Update printer page display
                UpdatePrintPageDisplay();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (chkUsePrinterPage.Checked == false && cboPageSize.SelectedIndex == 12)
            {
                SetCustomPage();
            }
        }

        private void SetCustomPage()
        {
            m_pPageLayoutCtl.Page.FormID = esriPageFormID.esriPageFormCUSTOM;
            m_pPageLayoutCtl.Page.Units = (esriUnits)cmbPageUnit.SelectedIndex;
            m_pPageLayoutCtl.Page.PutCustomSize(txtPageWidth.Value, txtPageHeight.Value);

        }

    }
}
