namespace CERMapping
{
    partial class FrmMain
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
            //Ensures that any ESRI libraries that have been used are unloaded in the correct order. 
            //Failure to do this may result in random crashes on exit due to the operating system unloading 
            //the libraries in the incorrect order. 
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.dotNetBarManagerMain = new DevComponents.DotNetBar.DotNetBarManager(this.components);
            this.dockSiteBottom = new DevComponents.DotNetBar.DockSite();
            this.barAttributeTable = new DevComponents.DotNetBar.Bar();
            this.panelTable = new DevComponents.DotNetBar.PanelDockContainer();
            this.GridTable = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataSettable = new System.Data.DataSet();
            this.AttributeTable = new System.Data.DataTable();
            this.FileRecTable = new System.Data.DataTable();
            this.ColumnID = new System.Data.DataColumn();
            this.ColumnFileName = new System.Data.DataColumn();
            this.ColumnFilePath = new System.Data.DataColumn();
            this.ColumnRecTime = new System.Data.DataColumn();
            this.zdFileTable = new System.Data.DataTable();
            this.ID = new System.Data.DataColumn();
            this.szfilename = new System.Data.DataColumn();
            this.szfilenames = new System.Data.DataColumn();
            this.uiTaskCode = new System.Data.DataColumn();
            this.uiObjCode = new System.Data.DataColumn();
            this.uiFileTypes = new System.Data.DataColumn();
            this.bindingNavigatorEx2 = new DevComponents.DotNetBar.Controls.BindingNavigatorEx(this.components);
            this.axToolbarfield = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.btnNOsel = new DevComponents.DotNetBar.LabelItem();
            this.btnTable = new DevComponents.DotNetBar.ButtonItem();
            this.btnselectras = new DevComponents.DotNetBar.ButtonItem();
            this.btnselectallras = new DevComponents.DotNetBar.ButtonItem();
            this.btnswitchras = new DevComponents.DotNetBar.ButtonItem();
            this.btnselclear = new DevComponents.DotNetBar.ButtonItem();
            this.btnshowall = new DevComponents.DotNetBar.ButtonItem();
            this.btnshowsel = new DevComponents.DotNetBar.ButtonItem();
            this.btnAddField = new DevComponents.DotNetBar.ButtonItem();
            this.btndelectfield = new DevComponents.DotNetBar.ButtonItem();
            this.btnFieldCalculator = new DevComponents.DotNetBar.ButtonItem();
            this.btnAddXYField = new DevComponents.DotNetBar.ButtonItem();
            this.btnExport = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemhide = new DevComponents.DotNetBar.ButtonItem();
            this.btnshowallout = new DevComponents.DotNetBar.ButtonItem();
            this.btnshowselout = new DevComponents.DotNetBar.ButtonItem();
            this.dockTable = new DevComponents.DotNetBar.DockContainerItem();
            this.dockSite10 = new DevComponents.DotNetBar.DockSite();
            this.barMain = new DevComponents.DotNetBar.Bar();
            this.panelMap = new DevComponents.DotNetBar.PanelDockContainer();
            this.axMapControlHide = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axMapCtlMain = new ESRI.ArcGIS.Controls.AxMapControl();
            this.contextMenuMap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuMapPopupSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolzoomin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolzoomout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolpan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolextent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolzoominfixed = new System.Windows.Forms.ToolStripMenuItem();
            this.toolzoomoutfixed = new System.Windows.Forms.ToolStripMenuItem();
            this.toolpreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolnextview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolselset = new System.Windows.Forms.ToolStripMenuItem();
            this.panelLayout = new DevComponents.DotNetBar.PanelDockContainer();
            this.axPageLayoutCtlMain = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.panelRulerVertical = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rulerCtlVertical = new Lyquidity.UtilityLibrary.Controls.RulerControl();
            this.panelRulerHorizontal = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rulerCtlHorizontal = new Lyquidity.UtilityLibrary.Controls.RulerControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.panelScene = new DevComponents.DotNetBar.PanelDockContainer();
            this.axSceneCtlMain = new ESRI.ArcGIS.Controls.AxSceneControl();
            this.dockMap = new DevComponents.DotNetBar.DockContainerItem();
            this.dockLayout = new DevComponents.DotNetBar.DockContainerItem();
            this.dockScene = new DevComponents.DotNetBar.DockContainerItem();
            this.dockSiteLeft = new DevComponents.DotNetBar.DockSite();
            this.barLeft = new DevComponents.DotNetBar.Bar();
            this.panelLayer = new DevComponents.DotNetBar.PanelDockContainer();
            this.axTOCCtlLayer = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.dockLayers = new DevComponents.DotNetBar.DockContainerItem();
            this.dockSiteRight = new DevComponents.DotNetBar.DockSite();
            this.barModel = new DevComponents.DotNetBar.Bar();
            this.panelDockContainer5 = new DevComponents.DotNetBar.PanelDockContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpRandomGen = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.txtThirdParam = new DevComponents.Editors.DoubleInput();
            this.txtSecondParam = new DevComponents.Editors.DoubleInput();
            this.txtFirstParam = new DevComponents.Editors.DoubleInput();
            this.grpGeneral = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbSubdivisionCount = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbiOne = new DevComponents.Editors.ComboItem();
            this.cmbiTwo = new DevComponents.Editors.ComboItem();
            this.cmbiThree = new DevComponents.Editors.ComboItem();
            this.cmbiFour = new DevComponents.Editors.ComboItem();
            this.cmbMappingType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbiPlat = new DevComponents.Editors.ComboItem();
            this.cmbiSphere = new DevComponents.Editors.ComboItem();
            this.cmbTritype = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbiForward = new DevComponents.Editors.ComboItem();
            this.cmbiBackword = new DevComponents.Editors.ComboItem();
            this.cmbiSubdivision = new DevComponents.Editors.ComboItem();
            this.lblSubdivisionCount = new DevComponents.DotNetBar.LabelX();
            this.lblMappingType = new DevComponents.DotNetBar.LabelX();
            this.lblTriType = new DevComponents.DotNetBar.LabelX();
            this.lblThirdParam = new DevComponents.DotNetBar.LabelX();
            this.lblSecondParam = new DevComponents.DotNetBar.LabelX();
            this.lblFirstParam = new DevComponents.DotNetBar.LabelX();
            this.lblModelType = new DevComponents.DotNetBar.LabelX();
            this.cmbModelType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbiCrater = new DevComponents.Editors.ComboItem();
            this.cmbiEllipse = new DevComponents.Editors.ComboItem();
            this.cmbiPyramid = new DevComponents.Editors.ComboItem();
            this.cmbiTetra = new DevComponents.Editors.ComboItem();
            this.btnTextureFilename = new DevComponents.DotNetBar.ButtonX();
            this.btnOutputFilename = new DevComponents.DotNetBar.ButtonX();
            this.lblTextureFilename = new DevComponents.DotNetBar.LabelX();
            this.lblOutputFilename = new DevComponents.DotNetBar.LabelX();
            this.txtTextureFilename = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtOutputFilename = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkRandomGen = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnGenerate = new DevComponents.DotNetBar.ButtonX();
            this.grpInputExisting = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelXCrater = new DevComponents.DotNetBar.LabelX();
            this.labelXStone = new DevComponents.DotNetBar.LabelX();
            this.txtExistingNonCrater = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnChooseNonCrater = new DevComponents.DotNetBar.ButtonX();
            this.txtExistingCrater = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnChooseExisingModel = new DevComponents.DotNetBar.ButtonX();
            this.chkInputExisting = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.dockContainerItem6 = new DevComponents.DotNetBar.DockContainerItem();
            this.barRight = new DevComponents.DotNetBar.Bar();
            this.panel3D = new DevComponents.DotNetBar.PanelDockContainer();
            this.treeViewTemplate = new System.Windows.Forms.TreeView();
            this.dock3D = new DevComponents.DotNetBar.DockContainerItem();
            this.barDataRv = new DevComponents.DotNetBar.Bar();
            this.panelDockContainer1 = new DevComponents.DotNetBar.PanelDockContainer();
            this.dataGridRv = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.序号DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.文件名DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.存储路径DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.接收时间DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolopenfile = new System.Windows.Forms.ToolStripMenuItem();
            this.tooltransandopen = new System.Windows.Forms.ToolStripMenuItem();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnStartListen = new DevComponents.DotNetBar.ButtonItem();
            this.btnStopListen = new DevComponents.DotNetBar.ButtonItem();
            this.txtFolder = new DevComponents.DotNetBar.TextBoxItem();
            this.btnBrowse = new DevComponents.DotNetBar.ButtonItem();
            this.panelDockContainer2 = new DevComponents.DotNetBar.PanelDockContainer();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.datagridzdFile = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.szfilenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.szfilenamesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiTaskCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiObjCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiFileTypesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datagridLocal = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panelExSearch = new DevComponents.DotNetBar.PanelEx();
            this.panelRecords = new DevComponents.DotNetBar.PanelEx();
            this.gridRecords = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.panelRemote = new DevComponents.DotNetBar.PanelEx();
            this.lblfiletype = new DevComponents.DotNetBar.LabelX();
            this.lblSaveFloder = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmbfiletype = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.comboItem10 = new DevComponents.Editors.ComboItem();
            this.comboItem11 = new DevComponents.Editors.ComboItem();
            this.comboItem12 = new DevComponents.Editors.ComboItem();
            this.lblStationID = new DevComponents.DotNetBar.LabelX();
            this.txtSaveFloder = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSaveFloder = new DevComponents.DotNetBar.ButtonX();
            this.RichTBoxSQL = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.cmbStationID = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.panelLocal = new DevComponents.DotNetBar.PanelEx();
            this.btnSetSQL = new DevComponents.DotNetBar.ButtonX();
            this.lblsqlset = new DevComponents.DotNetBar.LabelX();
            this.panelDataSource = new DevComponents.DotNetBar.PanelEx();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.btnDownLoadAll = new DevComponents.DotNetBar.ButtonItem();
            this.btnSeekSite = new DevComponents.DotNetBar.ButtonItem();
            this.RadioBoxService = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.RadioBoxLocal = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtRecorders = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dockDataRv = new DevComponents.DotNetBar.DockContainerItem();
            this.dockDataSearch = new DevComponents.DotNetBar.DockContainerItem();
            this.dockSite8 = new DevComponents.DotNetBar.DockSite();
            this.dockSite5 = new DevComponents.DotNetBar.DockSite();
            this.dockSite6 = new DevComponents.DotNetBar.DockSite();
            this.dockSite7 = new DevComponents.DotNetBar.DockSite();
            this.contextMenuBar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.popupBarCommon = new System.Windows.Forms.ToolStripMenuItem();
            this.popupBarLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.popupBar3D = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.popupBarEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.popupBarTinEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.popupBarGeoReference = new System.Windows.Forms.ToolStripMenuItem();
            this.popupBarGeoAdjust = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.popupBarEffects = new System.Windows.Forms.ToolStripMenuItem();
            this.popupBarSunalt = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.popupBarCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain = new DevComponents.DotNetBar.Bar();
            this.menuFile = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemNewDoc_Click = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemOpenDoc = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemSaveDoc = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemSaveAs = new DevComponents.DotNetBar.ButtonItem();
            this.btnItemExportMxd = new DevComponents.DotNetBar.ButtonItem();
            this.btnCheckUpdate = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemExit = new DevComponents.DotNetBar.ButtonItem();
            this.toolcreatfile = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemNewFeature = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemCreateShapeFile = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemnewFeatureLayer = new DevComponents.DotNetBar.ButtonItem();
            this.menuShpFromFile = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemDEMToTIN = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemTINToDEM = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemPointToLine = new DevComponents.DotNetBar.ButtonItem();
            this.btnmxdRAR = new DevComponents.DotNetBar.ButtonItem();
            this.btnRasterExportBatch = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemCustomSet = new DevComponents.DotNetBar.ButtonItem();
            this.menuView = new DevComponents.DotNetBar.ButtonItem();
            this.btnbarleft = new DevComponents.DotNetBar.ButtonItem();
            this.btnbarright = new DevComponents.DotNetBar.ButtonItem();
            this.btnmap = new DevComponents.DotNetBar.ButtonItem();
            this.btnlayout = new DevComponents.DotNetBar.ButtonItem();
            this.btn3d = new DevComponents.DotNetBar.ButtonItem();
            this.menuData = new DevComponents.DotNetBar.ButtonItem();
            this.btnIFLI_RPACPA = new DevComponents.DotNetBar.ButtonItem();
            this.btn_CreateFromXML = new DevComponents.DotNetBar.ButtonItem();
            this.btn_CreateLabel = new DevComponents.DotNetBar.ButtonItem();
            this.btn_CreateImagePath = new DevComponents.DotNetBar.ButtonItem();
            this.btnMapSymbol = new DevComponents.DotNetBar.ButtonItem();
            this.btnIFLI_MTRPPLAN = new DevComponents.DotNetBar.ButtonItem();
            this.btn_CreatParkPoint = new DevComponents.DotNetBar.ButtonItem();
            this.btnMapParkSymbol = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem54 = new DevComponents.DotNetBar.ButtonItem();
            this.btnIFLI_GMP = new DevComponents.DotNetBar.ButtonItem();
            this.btnCreatGmpPoint = new DevComponents.DotNetBar.ButtonItem();
            this.btnINMapSymbol = new DevComponents.DotNetBar.ButtonItem();
            this.menuBtnRaster = new DevComponents.DotNetBar.ButtonItem();
            this.menuBtnRasterPan = new DevComponents.DotNetBar.ButtonItem();
            this.menuBtnRasterRotate = new DevComponents.DotNetBar.ButtonItem();
            this.menuBtnRasterMirror = new DevComponents.DotNetBar.ButtonItem();
            this.menuBtnFlip = new DevComponents.DotNetBar.ButtonItem();
            this.menuBtnRasterZ = new DevComponents.DotNetBar.ButtonItem();
            this.btn_RasterClip = new DevComponents.DotNetBar.ButtonItem();
            this.menuBtnRasterResample = new DevComponents.DotNetBar.ButtonItem();
            this.menuBtnTransform = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.btnNEDtoENU = new DevComponents.DotNetBar.ButtonItem();
            this.btnNEDtoENUShape = new DevComponents.DotNetBar.ButtonItem();
            this.btntoolmatrix = new DevComponents.DotNetBar.ButtonItem();
            this.menuProcess = new DevComponents.DotNetBar.ButtonItem();
            this.btn_surfaceop = new DevComponents.DotNetBar.ButtonItem();
            this.btnprofile = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemCreateCenterline = new DevComponents.DotNetBar.ButtonItem();
            this.menuBtnVisibility = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem36 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem37 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem38 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem39 = new DevComponents.DotNetBar.ButtonItem();
            this.btnMenuAmizuth = new DevComponents.DotNetBar.ButtonItem();
            this.btnProBackWard = new DevComponents.DotNetBar.ButtonItem();
            this.btnSkyLinePara = new DevComponents.DotNetBar.ButtonItem();
            this.btnJSCamLabel = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem19 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem20 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem40 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem41 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem21 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem42 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem22 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem23 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem24 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemCenterlineInsideReport = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem25 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem26 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem27 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem28 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem29 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemWeldAlignToCenterline = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemGeneratePDFReport = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemExportToCAD = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem30 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem31 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem32 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem33 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem34 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem35 = new DevComponents.DotNetBar.ButtonItem();
            this.menuMapping = new DevComponents.DotNetBar.ButtonItem();
            this.btnAddText = new DevComponents.DotNetBar.ButtonItem();
            this.btnAddNorthArrow = new DevComponents.DotNetBar.ButtonItem();
            this.btnAddLegend = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem10 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem11 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem12 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem17 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem18 = new DevComponents.DotNetBar.ButtonItem();
            this.btnAddGrid = new DevComponents.DotNetBar.ButtonItem();
            this.btnAddScallBar = new DevComponents.DotNetBar.ButtonItem();
            this.btnAddScallText = new DevComponents.DotNetBar.ButtonItem();
            this.btnExportMap = new DevComponents.DotNetBar.ButtonItem();
            this.btnPrintMap = new DevComponents.DotNetBar.ButtonItem();
            this.btnLandExpMap = new DevComponents.DotNetBar.ButtonItem();
            this.menu3DAnalyst = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.btnvector = new DevComponents.DotNetBar.ButtonItem();
            this.menuToolVector = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem43 = new DevComponents.DotNetBar.ButtonItem();
            this.btnillum = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem44 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem45 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem46 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem47 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem48 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem49 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem50 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem51 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem52 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem53 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemMerge = new DevComponents.DotNetBar.ButtonItem();
            this.barContexMenuEditor = new DevComponents.DotNetBar.Bar();
            this.axToolbarCtlMenuEditor = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.controlContainerItem20 = new DevComponents.DotNetBar.ControlContainerItem();
            this.barGeoAdjust = new DevComponents.DotNetBar.Bar();
            this.cmbAdjustMethod = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbiAffine = new DevComponents.Editors.ComboItem();
            this.cbiProjective = new DevComponents.Editors.ComboItem();
            this.cbiSimilarity = new DevComponents.Editors.ComboItem();
            this.axToolbarControlSpatialAdjust = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.controlContainerItem14 = new DevComponents.DotNetBar.ControlContainerItem();
            this.controlContainerItem13 = new DevComponents.DotNetBar.ControlContainerItem();
            this.barCustom = new DevComponents.DotNetBar.Bar();
            this.axToolbarControlCustom = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.controlContainerItem19 = new DevComponents.DotNetBar.ControlContainerItem();
            this.barTerrain = new DevComponents.DotNetBar.Bar();
            this.btnRandomTerrain = new DevComponents.DotNetBar.ButtonItem();
            this.btnRandomTexture = new DevComponents.DotNetBar.ButtonItem();
            this.btnRandomModel = new DevComponents.DotNetBar.ButtonItem();
            this.barCommon = new DevComponents.DotNetBar.Bar();
            this.axToolbarControlCommon = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.controlContainerItem3 = new DevComponents.DotNetBar.ControlContainerItem();
            this.bar3D = new DevComponents.DotNetBar.Bar();
            this.axToolbarControlScene = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.controlContainerItem6 = new DevComponents.DotNetBar.ControlContainerItem();
            this.barSunAlt = new DevComponents.DotNetBar.Bar();
            this.cmbSunAltOriImg = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtSunAltXmlFile = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSetXmlFile = new DevComponents.DotNetBar.ButtonX();
            this.axToolbarSunAltitude = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.controlContainerItem16 = new DevComponents.DotNetBar.ControlContainerItem();
            this.controlContainerItem17 = new DevComponents.DotNetBar.ControlContainerItem();
            this.controlContainerItem18 = new DevComponents.DotNetBar.ControlContainerItem();
            this.controlContainerItem15 = new DevComponents.DotNetBar.ControlContainerItem();
            this.barGeoReference = new DevComponents.DotNetBar.Bar();
            this.cmbTargetRasterLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.axToolbarControlRectifyRaster = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.lblTargetRasterLayer = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem14 = new DevComponents.DotNetBar.ButtonItem();
            this.controlContainerItem11 = new DevComponents.DotNetBar.ControlContainerItem();
            this.controlContainerItem12 = new DevComponents.DotNetBar.ControlContainerItem();
            this.barLayout = new DevComponents.DotNetBar.Bar();
            this.axToolbarControlLayout = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.controlContainerItem4 = new DevComponents.DotNetBar.ControlContainerItem();
            this.barEditor = new DevComponents.DotNetBar.Bar();
            this.axToolbarControlEdit = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.controlContainerItem5 = new DevComponents.DotNetBar.ControlContainerItem();
            this.barEffects = new DevComponents.DotNetBar.Bar();
            this.cmBoxEffectsLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.controlContainerItem23 = new DevComponents.DotNetBar.ControlContainerItem();
            this.sliderItemTransparency = new DevComponents.DotNetBar.SliderItem();
            this.sliderItemContrast = new DevComponents.DotNetBar.SliderItem();
            this.sliderItemBrightness = new DevComponents.DotNetBar.SliderItem();
            this.barTinEditor = new DevComponents.DotNetBar.Bar();
            this.axToolbarControlTinEdit = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.cmbTargetTinLayer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboBoxExCrater = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboBoxExNonCrater = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.buttonItemAddTin = new DevComponents.DotNetBar.ButtonItem();
            this.controlContainerItem7 = new DevComponents.DotNetBar.ControlContainerItem();
            this.lblTinTargetLayer = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem8 = new DevComponents.DotNetBar.ControlContainerItem();
            this.lblCrater = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem9 = new DevComponents.DotNetBar.ControlContainerItem();
            this.lblNonCrater = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem10 = new DevComponents.DotNetBar.ControlContainerItem();
            this.dockSite3 = new DevComponents.DotNetBar.DockSite();
            this.txtExistingModel = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonItemSelect = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemClearSelection = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemVisible = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemUnVisible = new DevComponents.DotNetBar.ButtonItem();
            this.panelDockContainer4 = new DevComponents.DotNetBar.PanelDockContainer();
            this.panelDockContainer3 = new DevComponents.DotNetBar.PanelDockContainer();
            this.buttonItem15 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
            this.controlContainerItem2 = new DevComponents.DotNetBar.ControlContainerItem();
            this.axToolbarControl2 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem();
            this.styleManagerMain = new DevComponents.DotNetBar.StyleManager(this.components);
            this.dockSite9 = new DevComponents.DotNetBar.DockSite();
            this.dockContainerItem4 = new DevComponents.DotNetBar.DockContainerItem();
            this.dockContainerItem3 = new DevComponents.DotNetBar.DockContainerItem();
            this.barStatus = new DevComponents.DotNetBar.Bar();
            this.progressBarXMain = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.lblInfo = new DevComponents.DotNetBar.LabelItem();
            this.cmbiSubdivisionThree = new DevComponents.Editors.ComboItem();
            this.cmbiSubdivisionTwo = new DevComponents.Editors.ComboItem();
            this.cmbiSubdivisionOne = new DevComponents.Editors.ComboItem();
            this.cmbiSubdivisionZero = new DevComponents.Editors.ComboItem();
            this.cmiAffine = new DevComponents.Editors.ComboItem();
            this.cmiProjective = new DevComponents.Editors.ComboItem();
            this.cmiSimilarity = new DevComponents.Editors.ComboItem();
            this.cmiRubberSheet = new DevComponents.Editors.ComboItem();
            this.cmiEdgeSnap = new DevComponents.Editors.ComboItem();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dockContainerItem1 = new DevComponents.DotNetBar.DockContainerItem();
            this.textBoxItem1 = new DevComponents.DotNetBar.TextBoxItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.textBoxItem2 = new DevComponents.DotNetBar.TextBoxItem();
            this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
            this.textBoxItem3 = new DevComponents.DotNetBar.TextBoxItem();
            this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
            this.dockContainerItem5 = new DevComponents.DotNetBar.DockContainerItem();
            this.axLicenseControl2 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axToolbarControlEffects = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.dockSiteBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barAttributeTable)).BeginInit();
            this.barAttributeTable.SuspendLayout();
            this.panelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSettable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttributeTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileRecTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zdFileTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorEx2)).BeginInit();
            this.bindingNavigatorEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarfield)).BeginInit();
            this.dockSite10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barMain)).BeginInit();
            this.barMain.SuspendLayout();
            this.panelMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControlHide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapCtlMain)).BeginInit();
            this.contextMenuMap.SuspendLayout();
            this.panelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutCtlMain)).BeginInit();
            this.panelRulerVertical.SuspendLayout();
            this.panelRulerHorizontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.panelScene.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneCtlMain)).BeginInit();
            this.dockSiteLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barLeft)).BeginInit();
            this.barLeft.SuspendLayout();
            this.panelLayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCCtlLayer)).BeginInit();
            this.dockSiteRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barModel)).BeginInit();
            this.barModel.SuspendLayout();
            this.panelDockContainer5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpRandomGen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtThirdParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecondParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstParam)).BeginInit();
            this.grpGeneral.SuspendLayout();
            this.grpInputExisting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barRight)).BeginInit();
            this.barRight.SuspendLayout();
            this.panel3D.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barDataRv)).BeginInit();
            this.barDataRv.SuspendLayout();
            this.panelDockContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRv)).BeginInit();
            this.contextMenuStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelDockContainer2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridzdFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagridLocal)).BeginInit();
            this.panelExSearch.SuspendLayout();
            this.panelRecords.SuspendLayout();
            this.panelRemote.SuspendLayout();
            this.panelLocal.SuspendLayout();
            this.panelDataSource.SuspendLayout();
            this.dockSite7.SuspendLayout();
            this.contextMenuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barContexMenuEditor)).BeginInit();
            this.barContexMenuEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarCtlMenuEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barGeoAdjust)).BeginInit();
            this.barGeoAdjust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlSpatialAdjust)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barCustom)).BeginInit();
            this.barCustom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlCustom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barTerrain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barCommon)).BeginInit();
            this.barCommon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlCommon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar3D)).BeginInit();
            this.bar3D.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlScene)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barSunAlt)).BeginInit();
            this.barSunAlt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarSunAltitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barGeoReference)).BeginInit();
            this.barGeoReference.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlRectifyRaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barLayout)).BeginInit();
            this.barLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlLayout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditor)).BeginInit();
            this.barEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEffects)).BeginInit();
            this.barEffects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barTinEditor)).BeginInit();
            this.barTinEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlTinEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barStatus)).BeginInit();
            this.barStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlEffects)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn1
            // 
            this.gridColumn1.HeaderText = "站点";
            // 
            // gridColumn2
            // 
            this.gridColumn2.HeaderText = "记录数";
            // 
            // dotNetBarManagerMain
            // 
            this.dotNetBarManagerMain.AllowUserBarCustomize = false;
            this.dotNetBarManagerMain.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
            this.dotNetBarManagerMain.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC);
            this.dotNetBarManagerMain.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
            this.dotNetBarManagerMain.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV);
            this.dotNetBarManagerMain.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlX);
            this.dotNetBarManagerMain.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.dotNetBarManagerMain.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlY);
            this.dotNetBarManagerMain.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Del);
            this.dotNetBarManagerMain.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Ins);
            this.dotNetBarManagerMain.BottomDockSite = this.dockSiteBottom;
            this.dotNetBarManagerMain.EnableFullSizeDock = false;
            this.dotNetBarManagerMain.FillDockSite = this.dockSite10;
            this.dotNetBarManagerMain.LeftDockSite = this.dockSiteLeft;
            this.dotNetBarManagerMain.ParentForm = this;
            this.dotNetBarManagerMain.RightDockSite = this.dockSiteRight;
            this.dotNetBarManagerMain.ShowCustomizeContextMenu = false;
            this.dotNetBarManagerMain.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.dotNetBarManagerMain.ToolbarBottomDockSite = this.dockSite8;
            this.dotNetBarManagerMain.ToolbarLeftDockSite = this.dockSite5;
            this.dotNetBarManagerMain.ToolbarRightDockSite = this.dockSite6;
            this.dotNetBarManagerMain.ToolbarTopDockSite = this.dockSite7;
            this.dotNetBarManagerMain.TopDockSite = this.dockSite3;
            this.dotNetBarManagerMain.ItemClick += new System.EventHandler(this.dotNetBarManagerMain_ItemClick);
            // 
            // dockSiteBottom
            // 
            this.dockSiteBottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSiteBottom.Controls.Add(this.barAttributeTable);
            this.dockSiteBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSiteBottom.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.barAttributeTable, 944, 184)))}, DevComponents.DotNetBar.eOrientation.Vertical);
            this.dockSiteBottom.Location = new System.Drawing.Point(201, 416);
            this.dockSiteBottom.Name = "dockSiteBottom";
            this.dockSiteBottom.Size = new System.Drawing.Size(944, 187);
            this.dockSiteBottom.TabIndex = 3;
            this.dockSiteBottom.TabStop = false;
            // 
            // barAttributeTable
            // 
            this.barAttributeTable.AccessibleDescription = "DotNetBar Bar (barAttributeTable)";
            this.barAttributeTable.AccessibleName = "DotNetBar Bar";
            this.barAttributeTable.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.barAttributeTable.AutoSyncBarCaption = true;
            this.barAttributeTable.CanHide = true;
            this.barAttributeTable.CloseSingleTab = true;
            this.barAttributeTable.Controls.Add(this.panelTable);
            this.barAttributeTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.barAttributeTable.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.barAttributeTable.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockTable});
            this.barAttributeTable.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.barAttributeTable.Location = new System.Drawing.Point(0, 3);
            this.barAttributeTable.Name = "barAttributeTable";
            this.barAttributeTable.Size = new System.Drawing.Size(944, 184);
            this.barAttributeTable.Stretch = true;
            this.barAttributeTable.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barAttributeTable.TabIndex = 0;
            this.barAttributeTable.TabStop = false;
            this.barAttributeTable.Visible = false;
            this.barAttributeTable.ItemClick += new System.EventHandler(this.barAttributeTable_ItemClick);
            // 
            // panelTable
            // 
            this.panelTable.Controls.Add(this.GridTable);
            this.panelTable.Controls.Add(this.bindingNavigatorEx2);
            this.panelTable.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelTable.Location = new System.Drawing.Point(3, 23);
            this.panelTable.Name = "panelTable";
            this.panelTable.Size = new System.Drawing.Size(938, 158);
            this.panelTable.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelTable.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelTable.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelTable.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelTable.Style.GradientAngle = 90;
            this.panelTable.TabIndex = 0;
            // 
            // GridTable
            // 
            this.GridTable.AllowUserToAddRows = false;
            this.GridTable.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridTable.DataMember = "AttributeTable";
            this.GridTable.DataSource = this.dataSettable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridTable.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridTable.EnableHeadersVisualStyles = false;
            this.GridTable.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.GridTable.Location = new System.Drawing.Point(0, 27);
            this.GridTable.Name = "GridTable";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GridTable.RowHeadersWidth = 20;
            this.GridTable.RowTemplate.Height = 23;
            this.GridTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridTable.Size = new System.Drawing.Size(938, 131);
            this.GridTable.TabIndex = 2;
            this.GridTable.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GridTable_CellBeginEdit);
            this.GridTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridTable_CellClick);
            this.GridTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridTable_CellDoubleClick);
            this.GridTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridTable_CellEndEdit);
            this.GridTable.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridTable_CellMouseDown);
            this.GridTable.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridTable_CellMouseUp);
            this.GridTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridTable_CellValueChanged);
            this.GridTable.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridTable_RowHeaderMouseClick);
            this.GridTable.SelectionChanged += new System.EventHandler(this.GridTable_SelectionChanged);
            this.GridTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridTable_KeyDown);
            this.GridTable.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GridTable_KeyUp);
            // 
            // dataSettable
            // 
            this.dataSettable.DataSetName = "NewDataSet";
            this.dataSettable.Tables.AddRange(new System.Data.DataTable[] {
            this.AttributeTable,
            this.FileRecTable,
            this.zdFileTable});
            // 
            // AttributeTable
            // 
            this.AttributeTable.TableName = "AttributeTable";
            // 
            // FileRecTable
            // 
            this.FileRecTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.ColumnID,
            this.ColumnFileName,
            this.ColumnFilePath,
            this.ColumnRecTime});
            this.FileRecTable.TableName = "FileRecTable";
            // 
            // ColumnID
            // 
            this.ColumnID.ColumnName = "序号";
            // 
            // ColumnFileName
            // 
            this.ColumnFileName.ColumnName = "文件名";
            // 
            // ColumnFilePath
            // 
            this.ColumnFilePath.ColumnName = "存储路径";
            // 
            // ColumnRecTime
            // 
            this.ColumnRecTime.ColumnName = "接收时间";
            // 
            // zdFileTable
            // 
            this.zdFileTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.ID,
            this.szfilename,
            this.szfilenames,
            this.uiTaskCode,
            this.uiObjCode,
            this.uiFileTypes});
            this.zdFileTable.TableName = "zdFileTable";
            // 
            // ID
            // 
            this.ID.ColumnName = "ID";
            // 
            // szfilename
            // 
            this.szfilename.ColumnName = "szfilename";
            // 
            // szfilenames
            // 
            this.szfilenames.ColumnName = "szfilenames";
            // 
            // uiTaskCode
            // 
            this.uiTaskCode.Caption = "uiTaskCode";
            this.uiTaskCode.ColumnName = "uiTaskCode";
            // 
            // uiObjCode
            // 
            this.uiObjCode.ColumnName = "uiObjCode";
            // 
            // uiFileTypes
            // 
            this.uiFileTypes.ColumnName = "uiFileTypes";
            // 
            // bindingNavigatorEx2
            // 
            this.bindingNavigatorEx2.AntiAlias = true;
            this.bindingNavigatorEx2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.bindingNavigatorEx2.Controls.Add(this.axToolbarfield);
            this.bindingNavigatorEx2.CountLabel = this.btnNOsel;
            this.bindingNavigatorEx2.CountLabelFormat = "{0} out of {0} isselected";
            this.bindingNavigatorEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.bindingNavigatorEx2.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.bindingNavigatorEx2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnTable,
            this.buttonItemhide,
            this.btnshowallout,
            this.btnshowselout,
            this.btnNOsel});
            this.bindingNavigatorEx2.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigatorEx2.Name = "bindingNavigatorEx2";
            this.bindingNavigatorEx2.Size = new System.Drawing.Size(938, 27);
            this.bindingNavigatorEx2.Stretch = true;
            this.bindingNavigatorEx2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bindingNavigatorEx2.TabIndex = 1;
            this.bindingNavigatorEx2.TabStop = false;
            this.bindingNavigatorEx2.Text = "bindingNavigatorEx2";
            // 
            // axToolbarfield
            // 
            this.axToolbarfield.Location = new System.Drawing.Point(39, -1);
            this.axToolbarfield.Name = "axToolbarfield";
            this.axToolbarfield.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarfield.OcxState")));
            this.axToolbarfield.Size = new System.Drawing.Size(99, 28);
            this.axToolbarfield.TabIndex = 1;
            // 
            // btnNOsel
            // 
            this.btnNOsel.Name = "btnNOsel";
            this.btnNOsel.Text = "{0} out of {0} isselected";
            // 
            // btnTable
            // 
            this.btnTable.Image = ((System.Drawing.Image)(resources.GetObject("btnTable.Image")));
            this.btnTable.Name = "btnTable";
            this.btnTable.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnselectras,
            this.btnselectallras,
            this.btnswitchras,
            this.btnselclear,
            this.btnshowall,
            this.btnshowsel,
            this.btnAddField,
            this.btndelectfield,
            this.btnFieldCalculator,
            this.btnAddXYField,
            this.btnExport});
            this.btnTable.Text = "buttonItem2";
            this.btnTable.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btnTable_PopupOpen);
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // btnselectras
            // 
            this.btnselectras.Name = "btnselectras";
            this.btnselectras.Text = "选择";
            this.btnselectras.Tooltip = "根据属性选择";
            this.btnselectras.Click += new System.EventHandler(this.btnselectras_Click);
            // 
            // btnselectallras
            // 
            this.btnselectallras.Name = "btnselectallras";
            this.btnselectallras.Text = "全选";
            this.btnselectallras.Visible = false;
            // 
            // btnswitchras
            // 
            this.btnswitchras.Name = "btnswitchras";
            this.btnswitchras.Text = "反选";
            this.btnswitchras.Visible = false;
            // 
            // btnselclear
            // 
            this.btnselclear.Name = "btnselclear";
            this.btnselclear.Text = "清除选择";
            this.btnselclear.Visible = false;
            // 
            // btnshowall
            // 
            this.btnshowall.BeginGroup = true;
            this.btnshowall.Name = "btnshowall";
            this.btnshowall.Text = "显示所有记录";
            this.btnshowall.Click += new System.EventHandler(this.btnshowall_Click);
            // 
            // btnshowsel
            // 
            this.btnshowsel.Name = "btnshowsel";
            this.btnshowsel.Text = "显示选中记录";
            this.btnshowsel.Click += new System.EventHandler(this.btnshowsel_Click);
            // 
            // btnAddField
            // 
            this.btnAddField.BeginGroup = true;
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Text = "添加字段";
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // btndelectfield
            // 
            this.btndelectfield.Name = "btndelectfield";
            this.btndelectfield.Text = "删除字段";
            this.btndelectfield.Click += new System.EventHandler(this.btndelectfield_Click);
            // 
            // btnFieldCalculator
            // 
            this.btnFieldCalculator.Name = "btnFieldCalculator";
            this.btnFieldCalculator.Text = "字段计算";
            this.btnFieldCalculator.Click += new System.EventHandler(this.btnFieldCalculator_Click);
            // 
            // btnAddXYField
            // 
            this.btnAddXYField.Name = "btnAddXYField";
            this.btnAddXYField.Text = "添加XY字段";
            this.btnAddXYField.Click += new System.EventHandler(this.btnAddXYField_Click);
            // 
            // btnExport
            // 
            this.btnExport.BeginGroup = true;
            this.btnExport.Name = "btnExport";
            this.btnExport.Text = "导出...";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // buttonItemhide
            // 
            this.buttonItemhide.Name = "buttonItemhide";
            this.buttonItemhide.Text = "buttonItemhide";
            // 
            // btnshowallout
            // 
            this.btnshowallout.Checked = true;
            this.btnshowallout.Image = ((System.Drawing.Image)(resources.GetObject("btnshowallout.Image")));
            this.btnshowallout.Name = "btnshowallout";
            this.btnshowallout.Text = "buttonItem2";
            this.btnshowallout.Tooltip = "显示所有记录";
            this.btnshowallout.Click += new System.EventHandler(this.btnshowallout_Click);
            // 
            // btnshowselout
            // 
            this.btnshowselout.Image = ((System.Drawing.Image)(resources.GetObject("btnshowselout.Image")));
            this.btnshowselout.Name = "btnshowselout";
            this.btnshowselout.Text = "buttonItem3";
            this.btnshowselout.Tooltip = "显示选中记录";
            this.btnshowselout.Click += new System.EventHandler(this.btnshowselout_Click);
            // 
            // dockTable
            // 
            this.dockTable.Control = this.panelTable;
            this.dockTable.Name = "dockTable";
            // 
            // dockSite10
            // 
            this.dockSite10.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite10.Controls.Add(this.barMain);
            this.dockSite10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockSite10.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.barMain, 635, 152)))}, DevComponents.DotNetBar.eOrientation.Horizontal);
            this.dockSite10.Location = new System.Drawing.Point(201, 264);
            this.dockSite10.Name = "dockSite10";
            this.dockSite10.Size = new System.Drawing.Size(635, 152);
            this.dockSite10.TabIndex = 9;
            this.dockSite10.TabStop = false;
            // 
            // barMain
            // 
            this.barMain.AccessibleDescription = "DotNetBar Bar (barMain)";
            this.barMain.AccessibleName = "DotNetBar Bar";
            this.barMain.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.barMain.AlwaysDisplayDockTab = true;
            this.barMain.CanCustomize = false;
            this.barMain.CanDockBottom = false;
            this.barMain.CanDockLeft = false;
            this.barMain.CanDockRight = false;
            this.barMain.CanDockTab = false;
            this.barMain.CanDockTop = false;
            this.barMain.CanMove = false;
            this.barMain.CanUndock = false;
            this.barMain.Controls.Add(this.panelMap);
            this.barMain.Controls.Add(this.panelLayout);
            this.barMain.Controls.Add(this.panelScene);
            this.barMain.DockTabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Top;
            this.barMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.barMain.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockMap,
            this.dockLayout,
            this.dockScene});
            this.barMain.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.barMain.Location = new System.Drawing.Point(0, 0);
            this.barMain.Name = "barMain";
            this.barMain.SelectedDockTab = 0;
            this.barMain.Size = new System.Drawing.Size(635, 152);
            this.barMain.Stretch = true;
            this.barMain.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barMain.TabIndex = 0;
            this.barMain.TabNavigation = true;
            this.barMain.TabStop = false;
            this.barMain.Text = "数据视图";
            this.barMain.DockTabChange += new DevComponents.DotNetBar.DotNetBarManager.DockTabChangeEventHandler(this.bar3_DockTabChange);
            // 
            // panelMap
            // 
            this.panelMap.Controls.Add(this.axMapControlHide);
            this.panelMap.Controls.Add(this.axMapCtlMain);
            this.panelMap.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelMap.Location = new System.Drawing.Point(3, 28);
            this.panelMap.Name = "panelMap";
            this.panelMap.Size = new System.Drawing.Size(629, 121);
            this.panelMap.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelMap.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelMap.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelMap.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelMap.Style.GradientAngle = 90;
            this.panelMap.TabIndex = 0;
            // 
            // axMapControlHide
            // 
            this.axMapControlHide.Location = new System.Drawing.Point(108, 89);
            this.axMapControlHide.Name = "axMapControlHide";
            this.axMapControlHide.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControlHide.OcxState")));
            this.axMapControlHide.Size = new System.Drawing.Size(253, 71);
            this.axMapControlHide.TabIndex = 1;
            this.axMapControlHide.Visible = false;
            // 
            // axMapCtlMain
            // 
            this.axMapCtlMain.ContextMenuStrip = this.contextMenuMap;
            this.axMapCtlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapCtlMain.Location = new System.Drawing.Point(0, 0);
            this.axMapCtlMain.Name = "axMapCtlMain";
            this.axMapCtlMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapCtlMain.OcxState")));
            this.axMapCtlMain.Size = new System.Drawing.Size(629, 121);
            this.axMapCtlMain.TabIndex = 0;
            this.axMapCtlMain.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapCtlMain_OnMouseDown);
            this.axMapCtlMain.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapCtlMain_OnMouseUp);
            this.axMapCtlMain.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapCtlMain_OnMouseMove);
            this.axMapCtlMain.OnDoubleClick += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnDoubleClickEventHandler(this.axMapCtlMain_OnDoubleClick);
            this.axMapCtlMain.OnSelectionChanged += new System.EventHandler(this.axMapCtlMain_OnSelectionChanged);
            this.axMapCtlMain.OnViewRefreshed += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnViewRefreshedEventHandler(this.axMapCtlMain_OnViewRefreshed);
            this.axMapCtlMain.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterScreenDrawEventHandler(this.axMapCtlMain_OnAfterScreenDraw);
            this.axMapCtlMain.OnAfterDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterDrawEventHandler(this.axMapCtlMain_OnAfterDraw);
            this.axMapCtlMain.OnKeyUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnKeyUpEventHandler(this.axMapCtlMain_OnKeyUp);
            this.axMapCtlMain.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapCtlMain_OnMapReplaced);
            // 
            // contextMenuMap
            // 
            this.contextMenuMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMapPopupSelect,
            this.toolzoomin,
            this.toolzoomout,
            this.toolpan,
            this.toolextent,
            this.toolzoominfixed,
            this.toolzoomoutfixed,
            this.toolpreview,
            this.toolnextview,
            this.toolselset});
            this.contextMenuMap.Name = "contextMenuStrip2";
            this.contextMenuMap.Size = new System.Drawing.Size(125, 224);
            // 
            // menuMapPopupSelect
            // 
            this.menuMapPopupSelect.Image = ((System.Drawing.Image)(resources.GetObject("menuMapPopupSelect.Image")));
            this.menuMapPopupSelect.Name = "menuMapPopupSelect";
            this.menuMapPopupSelect.Size = new System.Drawing.Size(124, 22);
            this.menuMapPopupSelect.Text = "选择";
            this.menuMapPopupSelect.Click += new System.EventHandler(this.menuMapPopupSelect_Click);
            // 
            // toolzoomin
            // 
            this.toolzoomin.Image = ((System.Drawing.Image)(resources.GetObject("toolzoomin.Image")));
            this.toolzoomin.Name = "toolzoomin";
            this.toolzoomin.Size = new System.Drawing.Size(124, 22);
            this.toolzoomin.Text = "放大";
            this.toolzoomin.Click += new System.EventHandler(this.toolzoomin_Click);
            // 
            // toolzoomout
            // 
            this.toolzoomout.Image = ((System.Drawing.Image)(resources.GetObject("toolzoomout.Image")));
            this.toolzoomout.Name = "toolzoomout";
            this.toolzoomout.Size = new System.Drawing.Size(124, 22);
            this.toolzoomout.Text = "缩小";
            this.toolzoomout.Click += new System.EventHandler(this.toolzoomout_Click);
            // 
            // toolpan
            // 
            this.toolpan.Image = ((System.Drawing.Image)(resources.GetObject("toolpan.Image")));
            this.toolpan.Name = "toolpan";
            this.toolpan.Size = new System.Drawing.Size(124, 22);
            this.toolpan.Text = "漫游";
            this.toolpan.Click += new System.EventHandler(this.toolpan_Click);
            // 
            // toolextent
            // 
            this.toolextent.Image = ((System.Drawing.Image)(resources.GetObject("toolextent.Image")));
            this.toolextent.Name = "toolextent";
            this.toolextent.Size = new System.Drawing.Size(124, 22);
            this.toolextent.Text = "全图";
            this.toolextent.Click += new System.EventHandler(this.toolextent_Click);
            // 
            // toolzoominfixed
            // 
            this.toolzoominfixed.Image = ((System.Drawing.Image)(resources.GetObject("toolzoominfixed.Image")));
            this.toolzoominfixed.Name = "toolzoominfixed";
            this.toolzoominfixed.Size = new System.Drawing.Size(124, 22);
            this.toolzoominfixed.Text = "中心放大";
            this.toolzoominfixed.Click += new System.EventHandler(this.toolzoominfixed_Click);
            // 
            // toolzoomoutfixed
            // 
            this.toolzoomoutfixed.Image = ((System.Drawing.Image)(resources.GetObject("toolzoomoutfixed.Image")));
            this.toolzoomoutfixed.Name = "toolzoomoutfixed";
            this.toolzoomoutfixed.Size = new System.Drawing.Size(124, 22);
            this.toolzoomoutfixed.Text = "中心缩小";
            this.toolzoomoutfixed.Click += new System.EventHandler(this.toolzoomoutfixed_Click);
            // 
            // toolpreview
            // 
            this.toolpreview.Image = ((System.Drawing.Image)(resources.GetObject("toolpreview.Image")));
            this.toolpreview.Name = "toolpreview";
            this.toolpreview.Size = new System.Drawing.Size(124, 22);
            this.toolpreview.Text = "前一视图";
            this.toolpreview.Click += new System.EventHandler(this.toolpreview_Click);
            // 
            // toolnextview
            // 
            this.toolnextview.Image = ((System.Drawing.Image)(resources.GetObject("toolnextview.Image")));
            this.toolnextview.Name = "toolnextview";
            this.toolnextview.Size = new System.Drawing.Size(124, 22);
            this.toolnextview.Text = "后一视图";
            this.toolnextview.Click += new System.EventHandler(this.toolnextview_Click);
            // 
            // toolselset
            // 
            this.toolselset.Image = ((System.Drawing.Image)(resources.GetObject("toolselset.Image")));
            this.toolselset.Name = "toolselset";
            this.toolselset.Size = new System.Drawing.Size(124, 22);
            this.toolselset.Text = "选择集";
            this.toolselset.Click += new System.EventHandler(this.toolselset_Click);
            // 
            // panelLayout
            // 
            this.panelLayout.Controls.Add(this.axPageLayoutCtlMain);
            this.panelLayout.Controls.Add(this.panelRulerVertical);
            this.panelLayout.Controls.Add(this.panelRulerHorizontal);
            this.panelLayout.Controls.Add(this.axLicenseControl1);
            this.panelLayout.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelLayout.Location = new System.Drawing.Point(3, 28);
            this.panelLayout.Name = "panelLayout";
            this.panelLayout.Size = new System.Drawing.Size(629, 121);
            this.panelLayout.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelLayout.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelLayout.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelLayout.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelLayout.Style.GradientAngle = 90;
            this.panelLayout.TabIndex = 2;
            // 
            // axPageLayoutCtlMain
            // 
            this.axPageLayoutCtlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPageLayoutCtlMain.Location = new System.Drawing.Point(23, 25);
            this.axPageLayoutCtlMain.Name = "axPageLayoutCtlMain";
            this.axPageLayoutCtlMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutCtlMain.OcxState")));
            this.axPageLayoutCtlMain.Size = new System.Drawing.Size(606, 96);
            this.axPageLayoutCtlMain.TabIndex = 0;
            this.axPageLayoutCtlMain.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutCtlMain_OnMouseDown);
            this.axPageLayoutCtlMain.OnMouseUp += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseUpEventHandler(this.axPageLayoutCtlMain_OnMouseUp);
            this.axPageLayoutCtlMain.OnDoubleClick += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnDoubleClickEventHandler(this.axPageLayoutCtlMain_OnDoubleClick);
            this.axPageLayoutCtlMain.OnAfterDraw += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnAfterDrawEventHandler(this.axPageLayoutCtlMain_OnAfterDraw);
            this.axPageLayoutCtlMain.OnExtentUpdated += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnExtentUpdatedEventHandler(this.axPageLayoutCtlMain_OnExtentUpdated);
            this.axPageLayoutCtlMain.OnFocusMapChanged += new System.EventHandler(this.axPageLayoutCtlMain_OnFocusMapChanged);
            this.axPageLayoutCtlMain.OnPageLayoutReplaced += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnPageLayoutReplacedEventHandler(this.axPageLayoutCtlMain_OnPageLayoutReplaced);
            // 
            // panelRulerVertical
            // 
            this.panelRulerVertical.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelRulerVertical.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelRulerVertical.Controls.Add(this.rulerCtlVertical);
            this.panelRulerVertical.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelRulerVertical.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelRulerVertical.Location = new System.Drawing.Point(0, 25);
            this.panelRulerVertical.Name = "panelRulerVertical";
            this.panelRulerVertical.Size = new System.Drawing.Size(23, 96);
            // 
            // 
            // 
            this.panelRulerVertical.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelRulerVertical.Style.BackColorGradientAngle = 90;
            this.panelRulerVertical.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelRulerVertical.Style.BorderBottomWidth = 1;
            this.panelRulerVertical.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelRulerVertical.Style.BorderLeftWidth = 1;
            this.panelRulerVertical.Style.BorderRightWidth = 1;
            this.panelRulerVertical.Style.BorderTopWidth = 1;
            this.panelRulerVertical.Style.CornerDiameter = 4;
            this.panelRulerVertical.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.panelRulerVertical.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.panelRulerVertical.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelRulerVertical.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.panelRulerVertical.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.panelRulerVertical.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.panelRulerVertical.TabIndex = 7;
            // 
            // rulerCtlVertical
            // 
            this.rulerCtlVertical.ActualSize = true;
            this.rulerCtlVertical.DivisionMarkFactor = 5;
            this.rulerCtlVertical.Divisions = 10;
            this.rulerCtlVertical.ForeColor = System.Drawing.Color.Black;
            this.rulerCtlVertical.Location = new System.Drawing.Point(0, 41);
            this.rulerCtlVertical.MajorInterval = 100;
            this.rulerCtlVertical.MiddleMarkFactor = 3;
            this.rulerCtlVertical.MouseTrackingOn = false;
            this.rulerCtlVertical.Name = "rulerCtlVertical";
            this.rulerCtlVertical.Orientation = Lyquidity.UtilityLibrary.Controls.enumOrientation.orVertical;
            this.rulerCtlVertical.RulerAlignment = Lyquidity.UtilityLibrary.Controls.enumRulerAlignment.raBottomOrRight;
            this.rulerCtlVertical.ScaleMode = Lyquidity.UtilityLibrary.Controls.enumScaleMode.smPixels;
            this.rulerCtlVertical.Size = new System.Drawing.Size(23, 125);
            this.rulerCtlVertical.StartValue = 0D;
            this.rulerCtlVertical.TabIndex = 0;
            this.rulerCtlVertical.Text = "rulerControl2";
            this.rulerCtlVertical.VerticalNumbers = false;
            this.rulerCtlVertical.ZoomFactor = 1D;
            // 
            // panelRulerHorizontal
            // 
            this.panelRulerHorizontal.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelRulerHorizontal.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelRulerHorizontal.Controls.Add(this.rulerCtlHorizontal);
            this.panelRulerHorizontal.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelRulerHorizontal.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRulerHorizontal.Location = new System.Drawing.Point(0, 0);
            this.panelRulerHorizontal.Name = "panelRulerHorizontal";
            this.panelRulerHorizontal.Size = new System.Drawing.Size(629, 25);
            // 
            // 
            // 
            this.panelRulerHorizontal.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelRulerHorizontal.Style.BackColorGradientAngle = 90;
            this.panelRulerHorizontal.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelRulerHorizontal.Style.BorderBottomWidth = 1;
            this.panelRulerHorizontal.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelRulerHorizontal.Style.BorderLeftWidth = 1;
            this.panelRulerHorizontal.Style.BorderRightWidth = 1;
            this.panelRulerHorizontal.Style.BorderTopWidth = 1;
            this.panelRulerHorizontal.Style.CornerDiameter = 4;
            this.panelRulerHorizontal.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.panelRulerHorizontal.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.panelRulerHorizontal.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelRulerHorizontal.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.panelRulerHorizontal.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.panelRulerHorizontal.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.panelRulerHorizontal.TabIndex = 6;
            // 
            // rulerCtlHorizontal
            // 
            this.rulerCtlHorizontal.ActualSize = true;
            this.rulerCtlHorizontal.DivisionMarkFactor = 5;
            this.rulerCtlHorizontal.Divisions = 10;
            this.rulerCtlHorizontal.ForeColor = System.Drawing.Color.Black;
            this.rulerCtlHorizontal.Location = new System.Drawing.Point(127, 0);
            this.rulerCtlHorizontal.MajorInterval = 100;
            this.rulerCtlHorizontal.MiddleMarkFactor = 3;
            this.rulerCtlHorizontal.MouseTrackingOn = false;
            this.rulerCtlHorizontal.Name = "rulerCtlHorizontal";
            this.rulerCtlHorizontal.Orientation = Lyquidity.UtilityLibrary.Controls.enumOrientation.orHorizontal;
            this.rulerCtlHorizontal.RulerAlignment = Lyquidity.UtilityLibrary.Controls.enumRulerAlignment.raBottomOrRight;
            this.rulerCtlHorizontal.ScaleMode = Lyquidity.UtilityLibrary.Controls.enumScaleMode.smPixels;
            this.rulerCtlHorizontal.Size = new System.Drawing.Size(234, 25);
            this.rulerCtlHorizontal.StartValue = 0D;
            this.rulerCtlHorizontal.TabIndex = 0;
            this.rulerCtlHorizontal.Text = "rulerControl1";
            this.rulerCtlHorizontal.VerticalNumbers = false;
            this.rulerCtlHorizontal.ZoomFactor = 1D;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(189, 106);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // panelScene
            // 
            this.panelScene.Controls.Add(this.axSceneCtlMain);
            this.panelScene.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelScene.Location = new System.Drawing.Point(3, 28);
            this.panelScene.Name = "panelScene";
            this.panelScene.Size = new System.Drawing.Size(629, 121);
            this.panelScene.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelScene.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelScene.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelScene.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelScene.Style.GradientAngle = 90;
            this.panelScene.TabIndex = 3;
            // 
            // axSceneCtlMain
            // 
            this.axSceneCtlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSceneCtlMain.Location = new System.Drawing.Point(0, 0);
            this.axSceneCtlMain.Name = "axSceneCtlMain";
            this.axSceneCtlMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSceneCtlMain.OcxState")));
            this.axSceneCtlMain.Size = new System.Drawing.Size(629, 121);
            this.axSceneCtlMain.TabIndex = 0;
            this.axSceneCtlMain.OnDoubleClick += new ESRI.ArcGIS.Controls.ISceneControlEvents_Ax_OnDoubleClickEventHandler(this.axSceneCtlMain_OnDoubleClick);
            this.axSceneCtlMain.OnKeyDown += new ESRI.ArcGIS.Controls.ISceneControlEvents_Ax_OnKeyDownEventHandler(this.axSceneCtlMain_OnKeyDown);
            this.axSceneCtlMain.OnKeyUp += new ESRI.ArcGIS.Controls.ISceneControlEvents_Ax_OnKeyUpEventHandler(this.axSceneCtlMain_OnKeyUp);
            // 
            // dockMap
            // 
            this.dockMap.CanClose = DevComponents.DotNetBar.eDockContainerClose.No;
            this.dockMap.Control = this.panelMap;
            this.dockMap.Name = "dockMap";
            this.dockMap.Text = "数据";
            // 
            // dockLayout
            // 
            this.dockLayout.CanClose = DevComponents.DotNetBar.eDockContainerClose.No;
            this.dockLayout.Control = this.panelLayout;
            this.dockLayout.Name = "dockLayout";
            this.dockLayout.Text = "布局";
            // 
            // dockScene
            // 
            this.dockScene.CanClose = DevComponents.DotNetBar.eDockContainerClose.No;
            this.dockScene.Control = this.panelScene;
            this.dockScene.Name = "dockScene";
            this.dockScene.Text = "三维";
            this.dockScene.GotFocus += new System.EventHandler(this.dockScene_GotFocus);
            this.dockScene.VisibleChanged += new System.EventHandler(this.dockScene_VisibleChanged);
            // 
            // dockSiteLeft
            // 
            this.dockSiteLeft.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSiteLeft.Controls.Add(this.barLeft);
            this.dockSiteLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSiteLeft.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.barLeft, 198, 339)))}, DevComponents.DotNetBar.eOrientation.Horizontal);
            this.dockSiteLeft.Location = new System.Drawing.Point(0, 264);
            this.dockSiteLeft.Name = "dockSiteLeft";
            this.dockSiteLeft.Size = new System.Drawing.Size(201, 339);
            this.dockSiteLeft.TabIndex = 0;
            this.dockSiteLeft.TabStop = false;
            // 
            // barLeft
            // 
            this.barLeft.AccessibleDescription = "DotNetBar Bar (barLeft)";
            this.barLeft.AccessibleName = "DotNetBar Bar";
            this.barLeft.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.barLeft.AutoSyncBarCaption = true;
            this.barLeft.CanHide = true;
            this.barLeft.CloseSingleTab = true;
            this.barLeft.Controls.Add(this.panelLayer);
            this.barLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.barLeft.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.barLeft.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockLayers});
            this.barLeft.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.barLeft.Location = new System.Drawing.Point(0, 0);
            this.barLeft.Name = "barLeft";
            this.barLeft.Size = new System.Drawing.Size(198, 339);
            this.barLeft.Stretch = true;
            this.barLeft.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barLeft.TabIndex = 0;
            this.barLeft.TabStop = false;
            this.barLeft.Text = "图层";
            this.barLeft.VisibleChanged += new System.EventHandler(this.barLeft_VisibleChanged);
            // 
            // panelLayer
            // 
            this.panelLayer.Controls.Add(this.axTOCCtlLayer);
            this.panelLayer.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelLayer.Location = new System.Drawing.Point(3, 23);
            this.panelLayer.Name = "panelLayer";
            this.panelLayer.Size = new System.Drawing.Size(192, 313);
            this.panelLayer.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelLayer.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelLayer.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelLayer.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelLayer.Style.GradientAngle = 90;
            this.panelLayer.TabIndex = 0;
            // 
            // axTOCCtlLayer
            // 
            this.axTOCCtlLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCCtlLayer.Location = new System.Drawing.Point(0, 0);
            this.axTOCCtlLayer.Name = "axTOCCtlLayer";
            this.axTOCCtlLayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCCtlLayer.OcxState")));
            this.axTOCCtlLayer.Size = new System.Drawing.Size(192, 313);
            this.axTOCCtlLayer.TabIndex = 0;
            this.axTOCCtlLayer.OnMouseUp += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseUpEventHandler(this.axTOCCtlLayer_OnMouseUp);
            this.axTOCCtlLayer.OnDoubleClick += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnDoubleClickEventHandler(this.axTOCCtlLayer_OnDoubleClick);
            // 
            // dockLayers
            // 
            this.dockLayers.Control = this.panelLayer;
            this.dockLayers.Name = "dockLayers";
            this.dockLayers.Text = "图层";
            // 
            // dockSiteRight
            // 
            this.dockSiteRight.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSiteRight.Controls.Add(this.barModel);
            this.dockSiteRight.Controls.Add(this.barRight);
            this.dockSiteRight.Controls.Add(this.barDataRv);
            this.dockSiteRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSiteRight.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.barModel, 306, 61))),
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.barRight, 306, 61))),
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.barDataRv, 306, 24)))}, DevComponents.DotNetBar.eOrientation.Vertical);
            this.dockSiteRight.Location = new System.Drawing.Point(836, 264);
            this.dockSiteRight.Name = "dockSiteRight";
            this.dockSiteRight.Size = new System.Drawing.Size(309, 152);
            this.dockSiteRight.TabIndex = 1;
            this.dockSiteRight.TabStop = false;
            this.dockSiteRight.Visible = false;
            // 
            // barModel
            // 
            this.barModel.AccessibleDescription = "DotNetBar Bar (barModel)";
            this.barModel.AccessibleName = "DotNetBar Bar";
            this.barModel.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.barModel.AutoSyncBarCaption = true;
            this.barModel.CloseSingleTab = true;
            this.barModel.Controls.Add(this.panelDockContainer5);
            this.barModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.barModel.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.barModel.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItem6});
            this.barModel.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.barModel.Location = new System.Drawing.Point(3, 0);
            this.barModel.Name = "barModel";
            this.barModel.Size = new System.Drawing.Size(306, 61);
            this.barModel.Stretch = true;
            this.barModel.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barModel.TabIndex = 1;
            this.barModel.TabStop = false;
            this.barModel.Text = "模型生成";
            this.barModel.Visible = false;
            // 
            // panelDockContainer5
            // 
            this.panelDockContainer5.Controls.Add(this.panel1);
            this.panelDockContainer5.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelDockContainer5.Location = new System.Drawing.Point(3, 23);
            this.panelDockContainer5.Name = "panelDockContainer5";
            this.panelDockContainer5.Size = new System.Drawing.Size(300, 35);
            this.panelDockContainer5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer5.Style.GradientAngle = 90;
            this.panelDockContainer5.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grpRandomGen);
            this.panel1.Controls.Add(this.chkRandomGen);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Controls.Add(this.grpInputExisting);
            this.panel1.Controls.Add(this.chkInputExisting);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 35);
            this.panel1.TabIndex = 16;
            // 
            // grpRandomGen
            // 
            this.grpRandomGen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRandomGen.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpRandomGen.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grpRandomGen.Controls.Add(this.buttonX1);
            this.grpRandomGen.Controls.Add(this.txtThirdParam);
            this.grpRandomGen.Controls.Add(this.txtSecondParam);
            this.grpRandomGen.Controls.Add(this.txtFirstParam);
            this.grpRandomGen.Controls.Add(this.grpGeneral);
            this.grpRandomGen.Controls.Add(this.lblThirdParam);
            this.grpRandomGen.Controls.Add(this.lblSecondParam);
            this.grpRandomGen.Controls.Add(this.lblFirstParam);
            this.grpRandomGen.Controls.Add(this.lblModelType);
            this.grpRandomGen.Controls.Add(this.cmbModelType);
            this.grpRandomGen.Controls.Add(this.btnTextureFilename);
            this.grpRandomGen.Controls.Add(this.btnOutputFilename);
            this.grpRandomGen.Controls.Add(this.lblTextureFilename);
            this.grpRandomGen.Controls.Add(this.lblOutputFilename);
            this.grpRandomGen.Controls.Add(this.txtTextureFilename);
            this.grpRandomGen.Controls.Add(this.txtOutputFilename);
            this.grpRandomGen.DisabledBackColor = System.Drawing.Color.Empty;
            this.grpRandomGen.Location = new System.Drawing.Point(163, 14);
            this.grpRandomGen.Name = "grpRandomGen";
            this.grpRandomGen.Size = new System.Drawing.Size(0, 282);
            // 
            // 
            // 
            this.grpRandomGen.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpRandomGen.Style.BackColorGradientAngle = 90;
            this.grpRandomGen.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpRandomGen.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpRandomGen.Style.BorderBottomWidth = 1;
            this.grpRandomGen.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpRandomGen.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpRandomGen.Style.BorderLeftWidth = 1;
            this.grpRandomGen.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpRandomGen.Style.BorderRightWidth = 1;
            this.grpRandomGen.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpRandomGen.Style.BorderTopWidth = 1;
            this.grpRandomGen.Style.CornerDiameter = 4;
            this.grpRandomGen.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpRandomGen.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grpRandomGen.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpRandomGen.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpRandomGen.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpRandomGen.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpRandomGen.TabIndex = 12;
            this.grpRandomGen.Text = "随机生成";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(152, 176);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 25);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 2;
            this.buttonX1.Text = "buttonX1";
            // 
            // txtThirdParam
            // 
            // 
            // 
            // 
            this.txtThirdParam.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtThirdParam.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtThirdParam.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtThirdParam.Increment = 0.1D;
            this.txtThirdParam.Location = new System.Drawing.Point(108, 105);
            this.txtThirdParam.Name = "txtThirdParam";
            this.txtThirdParam.ShowUpDown = true;
            this.txtThirdParam.Size = new System.Drawing.Size(80, 20);
            this.txtThirdParam.TabIndex = 11;
            // 
            // txtSecondParam
            // 
            // 
            // 
            // 
            this.txtSecondParam.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtSecondParam.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSecondParam.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtSecondParam.Increment = 1D;
            this.txtSecondParam.Location = new System.Drawing.Point(109, 72);
            this.txtSecondParam.Name = "txtSecondParam";
            this.txtSecondParam.ShowUpDown = true;
            this.txtSecondParam.Size = new System.Drawing.Size(80, 20);
            this.txtSecondParam.TabIndex = 11;
            // 
            // txtFirstParam
            // 
            // 
            // 
            // 
            this.txtFirstParam.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtFirstParam.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFirstParam.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtFirstParam.Increment = 1D;
            this.txtFirstParam.Location = new System.Drawing.Point(109, 42);
            this.txtFirstParam.Name = "txtFirstParam";
            this.txtFirstParam.ShowUpDown = true;
            this.txtFirstParam.Size = new System.Drawing.Size(80, 20);
            this.txtFirstParam.TabIndex = 11;
            // 
            // grpGeneral
            // 
            this.grpGeneral.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpGeneral.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grpGeneral.Controls.Add(this.cmbSubdivisionCount);
            this.grpGeneral.Controls.Add(this.cmbMappingType);
            this.grpGeneral.Controls.Add(this.cmbTritype);
            this.grpGeneral.Controls.Add(this.lblSubdivisionCount);
            this.grpGeneral.Controls.Add(this.lblMappingType);
            this.grpGeneral.Controls.Add(this.lblTriType);
            this.grpGeneral.DisabledBackColor = System.Drawing.Color.Empty;
            this.grpGeneral.Location = new System.Drawing.Point(7, 211);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(303, 151);
            // 
            // 
            // 
            this.grpGeneral.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpGeneral.Style.BackColorGradientAngle = 90;
            this.grpGeneral.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpGeneral.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpGeneral.Style.BorderBottomWidth = 1;
            this.grpGeneral.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpGeneral.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpGeneral.Style.BorderLeftWidth = 1;
            this.grpGeneral.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpGeneral.Style.BorderRightWidth = 1;
            this.grpGeneral.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpGeneral.Style.BorderTopWidth = 1;
            this.grpGeneral.Style.CornerDiameter = 4;
            this.grpGeneral.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpGeneral.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grpGeneral.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpGeneral.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpGeneral.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpGeneral.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpGeneral.TabIndex = 10;
            this.grpGeneral.Text = "通用参数";
            // 
            // cmbSubdivisionCount
            // 
            this.cmbSubdivisionCount.DisplayMember = "Text";
            this.cmbSubdivisionCount.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubdivisionCount.FormattingEnabled = true;
            this.cmbSubdivisionCount.ItemHeight = 14;
            this.cmbSubdivisionCount.Items.AddRange(new object[] {
            this.cmbiOne,
            this.cmbiTwo,
            this.cmbiThree,
            this.cmbiFour});
            this.cmbSubdivisionCount.Location = new System.Drawing.Point(121, 93);
            this.cmbSubdivisionCount.Name = "cmbSubdivisionCount";
            this.cmbSubdivisionCount.Size = new System.Drawing.Size(121, 20);
            this.cmbSubdivisionCount.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSubdivisionCount.TabIndex = 1;
            // 
            // cmbiOne
            // 
            this.cmbiOne.Text = "1";
            // 
            // cmbiTwo
            // 
            this.cmbiTwo.Text = "2";
            // 
            // cmbiThree
            // 
            this.cmbiThree.Text = "3";
            // 
            // cmbiFour
            // 
            this.cmbiFour.Text = "4";
            // 
            // cmbMappingType
            // 
            this.cmbMappingType.DisplayMember = "Text";
            this.cmbMappingType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMappingType.FormattingEnabled = true;
            this.cmbMappingType.ItemHeight = 14;
            this.cmbMappingType.Items.AddRange(new object[] {
            this.cmbiPlat,
            this.cmbiSphere});
            this.cmbMappingType.Location = new System.Drawing.Point(121, 54);
            this.cmbMappingType.Name = "cmbMappingType";
            this.cmbMappingType.Size = new System.Drawing.Size(121, 20);
            this.cmbMappingType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbMappingType.TabIndex = 1;
            // 
            // cmbiPlat
            // 
            this.cmbiPlat.Text = "平铺";
            // 
            // cmbiSphere
            // 
            this.cmbiSphere.Text = "球体";
            // 
            // cmbTritype
            // 
            this.cmbTritype.DisplayMember = "Text";
            this.cmbTritype.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTritype.FormattingEnabled = true;
            this.cmbTritype.ItemHeight = 14;
            this.cmbTritype.Items.AddRange(new object[] {
            this.cmbiForward,
            this.cmbiBackword,
            this.cmbiSubdivision});
            this.cmbTritype.Location = new System.Drawing.Point(121, 14);
            this.cmbTritype.Name = "cmbTritype";
            this.cmbTritype.Size = new System.Drawing.Size(121, 20);
            this.cmbTritype.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbTritype.TabIndex = 1;
            // 
            // cmbiForward
            // 
            this.cmbiForward.Text = "前向三角化";
            // 
            // cmbiBackword
            // 
            this.cmbiBackword.Text = "后向三角化";
            // 
            // cmbiSubdivision
            // 
            this.cmbiSubdivision.Text = "三角细化";
            // 
            // lblSubdivisionCount
            // 
            // 
            // 
            // 
            this.lblSubdivisionCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSubdivisionCount.Location = new System.Drawing.Point(1, 93);
            this.lblSubdivisionCount.Name = "lblSubdivisionCount";
            this.lblSubdivisionCount.Size = new System.Drawing.Size(95, 25);
            this.lblSubdivisionCount.TabIndex = 0;
            this.lblSubdivisionCount.Text = "网格细化次数:";
            // 
            // lblMappingType
            // 
            // 
            // 
            // 
            this.lblMappingType.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMappingType.Location = new System.Drawing.Point(1, 52);
            this.lblMappingType.Name = "lblMappingType";
            this.lblMappingType.Size = new System.Drawing.Size(95, 25);
            this.lblMappingType.TabIndex = 0;
            this.lblMappingType.Text = "纹理坐标映射:";
            // 
            // lblTriType
            // 
            // 
            // 
            // 
            this.lblTriType.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTriType.Location = new System.Drawing.Point(1, 14);
            this.lblTriType.Name = "lblTriType";
            this.lblTriType.Size = new System.Drawing.Size(95, 25);
            this.lblTriType.TabIndex = 0;
            this.lblTriType.Text = "三角化方式:";
            // 
            // lblThirdParam
            // 
            // 
            // 
            // 
            this.lblThirdParam.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblThirdParam.Location = new System.Drawing.Point(7, 103);
            this.lblThirdParam.Name = "lblThirdParam";
            this.lblThirdParam.Size = new System.Drawing.Size(95, 25);
            this.lblThirdParam.TabIndex = 5;
            this.lblThirdParam.Text = "labelX1";
            // 
            // lblSecondParam
            // 
            // 
            // 
            // 
            this.lblSecondParam.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSecondParam.Location = new System.Drawing.Point(7, 72);
            this.lblSecondParam.Name = "lblSecondParam";
            this.lblSecondParam.Size = new System.Drawing.Size(95, 25);
            this.lblSecondParam.TabIndex = 5;
            this.lblSecondParam.Text = "labelX1";
            // 
            // lblFirstParam
            // 
            // 
            // 
            // 
            this.lblFirstParam.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblFirstParam.Location = new System.Drawing.Point(7, 40);
            this.lblFirstParam.Name = "lblFirstParam";
            this.lblFirstParam.Size = new System.Drawing.Size(95, 25);
            this.lblFirstParam.TabIndex = 5;
            this.lblFirstParam.Text = "labelX1";
            // 
            // lblModelType
            // 
            // 
            // 
            // 
            this.lblModelType.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblModelType.Location = new System.Drawing.Point(7, 3);
            this.lblModelType.Name = "lblModelType";
            this.lblModelType.Size = new System.Drawing.Size(95, 25);
            this.lblModelType.TabIndex = 0;
            this.lblModelType.Text = "模型类型:";
            // 
            // cmbModelType
            // 
            this.cmbModelType.DisplayMember = "Text";
            this.cmbModelType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbModelType.FormattingEnabled = true;
            this.cmbModelType.ItemHeight = 14;
            this.cmbModelType.Items.AddRange(new object[] {
            this.cmbiCrater,
            this.cmbiEllipse,
            this.cmbiPyramid,
            this.cmbiTetra});
            this.cmbModelType.Location = new System.Drawing.Point(107, 3);
            this.cmbModelType.Name = "cmbModelType";
            this.cmbModelType.Size = new System.Drawing.Size(108, 20);
            this.cmbModelType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbModelType.TabIndex = 4;
            // 
            // cmbiCrater
            // 
            this.cmbiCrater.Text = "撞击坑";
            // 
            // cmbiEllipse
            // 
            this.cmbiEllipse.Text = "椭球";
            // 
            // cmbiPyramid
            // 
            this.cmbiPyramid.Text = "石块-四棱台";
            // 
            // cmbiTetra
            // 
            this.cmbiTetra.Text = "石块-四面体";
            // 
            // btnTextureFilename
            // 
            this.btnTextureFilename.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTextureFilename.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTextureFilename.Location = new System.Drawing.Point(238, 142);
            this.btnTextureFilename.Name = "btnTextureFilename";
            this.btnTextureFilename.Size = new System.Drawing.Size(72, 25);
            this.btnTextureFilename.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTextureFilename.TabIndex = 3;
            this.btnTextureFilename.Text = "...";
            // 
            // btnOutputFilename
            // 
            this.btnOutputFilename.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOutputFilename.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOutputFilename.Location = new System.Drawing.Point(238, 176);
            this.btnOutputFilename.Name = "btnOutputFilename";
            this.btnOutputFilename.Size = new System.Drawing.Size(72, 25);
            this.btnOutputFilename.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOutputFilename.TabIndex = 3;
            this.btnOutputFilename.Text = "...";
            // 
            // lblTextureFilename
            // 
            // 
            // 
            // 
            this.lblTextureFilename.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTextureFilename.Location = new System.Drawing.Point(7, 140);
            this.lblTextureFilename.Name = "lblTextureFilename";
            this.lblTextureFilename.Size = new System.Drawing.Size(95, 25);
            this.lblTextureFilename.TabIndex = 0;
            this.lblTextureFilename.Text = "纹理贴图路径:";
            // 
            // lblOutputFilename
            // 
            // 
            // 
            // 
            this.lblOutputFilename.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblOutputFilename.Location = new System.Drawing.Point(7, 173);
            this.lblOutputFilename.Name = "lblOutputFilename";
            this.lblOutputFilename.Size = new System.Drawing.Size(95, 25);
            this.lblOutputFilename.TabIndex = 0;
            this.lblOutputFilename.Text = "输出3DS路径:";
            // 
            // txtTextureFilename
            // 
            this.txtTextureFilename.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtTextureFilename.Border.Class = "TextBoxBorder";
            this.txtTextureFilename.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTextureFilename.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtTextureFilename.Location = new System.Drawing.Point(109, 142);
            this.txtTextureFilename.Name = "txtTextureFilename";
            this.txtTextureFilename.Size = new System.Drawing.Size(111, 20);
            this.txtTextureFilename.TabIndex = 2;
            // 
            // txtOutputFilename
            // 
            this.txtOutputFilename.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtOutputFilename.Border.Class = "TextBoxBorder";
            this.txtOutputFilename.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOutputFilename.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtOutputFilename.Location = new System.Drawing.Point(109, 176);
            this.txtOutputFilename.Name = "txtOutputFilename";
            this.txtOutputFilename.Size = new System.Drawing.Size(111, 20);
            this.txtOutputFilename.TabIndex = 2;
            // 
            // chkRandomGen
            // 
            // 
            // 
            // 
            this.chkRandomGen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkRandomGen.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkRandomGen.Checked = true;
            this.chkRandomGen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRandomGen.CheckValue = "Y";
            this.chkRandomGen.Location = new System.Drawing.Point(48, 79);
            this.chkRandomGen.Name = "chkRandomGen";
            this.chkRandomGen.Size = new System.Drawing.Size(100, 25);
            this.chkRandomGen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkRandomGen.TabIndex = 13;
            this.chkRandomGen.Text = "随机生成：";
            // 
            // btnGenerate
            // 
            this.btnGenerate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGenerate.Location = new System.Drawing.Point(429, 431);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(0, 25);
            this.btnGenerate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "增加";
            // 
            // grpInputExisting
            // 
            this.grpInputExisting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpInputExisting.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpInputExisting.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grpInputExisting.Controls.Add(this.labelXCrater);
            this.grpInputExisting.Controls.Add(this.labelXStone);
            this.grpInputExisting.Controls.Add(this.txtExistingNonCrater);
            this.grpInputExisting.Controls.Add(this.btnChooseNonCrater);
            this.grpInputExisting.Controls.Add(this.txtExistingCrater);
            this.grpInputExisting.Controls.Add(this.btnChooseExisingModel);
            this.grpInputExisting.DisabledBackColor = System.Drawing.Color.Empty;
            this.grpInputExisting.Enabled = false;
            this.grpInputExisting.Location = new System.Drawing.Point(154, 309);
            this.grpInputExisting.Name = "grpInputExisting";
            this.grpInputExisting.Size = new System.Drawing.Size(0, 127);
            // 
            // 
            // 
            this.grpInputExisting.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpInputExisting.Style.BackColorGradientAngle = 90;
            this.grpInputExisting.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpInputExisting.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpInputExisting.Style.BorderBottomWidth = 1;
            this.grpInputExisting.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpInputExisting.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpInputExisting.Style.BorderLeftWidth = 1;
            this.grpInputExisting.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpInputExisting.Style.BorderRightWidth = 1;
            this.grpInputExisting.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpInputExisting.Style.BorderTopWidth = 1;
            this.grpInputExisting.Style.CornerDiameter = 4;
            this.grpInputExisting.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpInputExisting.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grpInputExisting.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpInputExisting.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpInputExisting.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpInputExisting.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpInputExisting.TabIndex = 11;
            this.grpInputExisting.Text = "导入";
            // 
            // labelXCrater
            // 
            // 
            // 
            // 
            this.labelXCrater.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXCrater.Location = new System.Drawing.Point(8, 14);
            this.labelXCrater.Name = "labelXCrater";
            this.labelXCrater.Size = new System.Drawing.Size(58, 25);
            this.labelXCrater.TabIndex = 7;
            this.labelXCrater.Text = "撞击坑：";
            // 
            // labelXStone
            // 
            // 
            // 
            // 
            this.labelXStone.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXStone.Location = new System.Drawing.Point(8, 56);
            this.labelXStone.Name = "labelXStone";
            this.labelXStone.Size = new System.Drawing.Size(48, 25);
            this.labelXStone.TabIndex = 6;
            this.labelXStone.Text = "石块：";
            // 
            // txtExistingNonCrater
            // 
            this.txtExistingNonCrater.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtExistingNonCrater.Border.Class = "TextBoxBorder";
            this.txtExistingNonCrater.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtExistingNonCrater.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtExistingNonCrater.Location = new System.Drawing.Point(65, 59);
            this.txtExistingNonCrater.Name = "txtExistingNonCrater";
            this.txtExistingNonCrater.Size = new System.Drawing.Size(135, 20);
            this.txtExistingNonCrater.TabIndex = 4;
            // 
            // btnChooseNonCrater
            // 
            this.btnChooseNonCrater.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChooseNonCrater.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChooseNonCrater.Location = new System.Drawing.Point(205, 59);
            this.btnChooseNonCrater.Name = "btnChooseNonCrater";
            this.btnChooseNonCrater.Size = new System.Drawing.Size(75, 25);
            this.btnChooseNonCrater.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnChooseNonCrater.TabIndex = 5;
            this.btnChooseNonCrater.Text = "...";
            // 
            // txtExistingCrater
            // 
            this.txtExistingCrater.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtExistingCrater.Border.Class = "TextBoxBorder";
            this.txtExistingCrater.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtExistingCrater.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtExistingCrater.Location = new System.Drawing.Point(66, 13);
            this.txtExistingCrater.Name = "txtExistingCrater";
            this.txtExistingCrater.Size = new System.Drawing.Size(135, 20);
            this.txtExistingCrater.TabIndex = 2;
            // 
            // btnChooseExisingModel
            // 
            this.btnChooseExisingModel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChooseExisingModel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChooseExisingModel.Location = new System.Drawing.Point(137, 23);
            this.btnChooseExisingModel.Name = "btnChooseExisingModel";
            this.btnChooseExisingModel.Size = new System.Drawing.Size(75, 25);
            this.btnChooseExisingModel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnChooseExisingModel.TabIndex = 3;
            this.btnChooseExisingModel.Text = "...";
            // 
            // chkInputExisting
            // 
            // 
            // 
            // 
            this.chkInputExisting.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkInputExisting.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkInputExisting.Location = new System.Drawing.Point(53, 343);
            this.chkInputExisting.Name = "chkInputExisting";
            this.chkInputExisting.Size = new System.Drawing.Size(100, 25);
            this.chkInputExisting.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkInputExisting.TabIndex = 14;
            this.chkInputExisting.Text = "导入：";
            // 
            // dockContainerItem6
            // 
            this.dockContainerItem6.Control = this.panelDockContainer5;
            this.dockContainerItem6.Name = "dockContainerItem6";
            this.dockContainerItem6.Text = "模型生成";
            // 
            // barRight
            // 
            this.barRight.AccessibleDescription = "DotNetBar Bar (barRight)";
            this.barRight.AccessibleName = "DotNetBar Bar";
            this.barRight.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.barRight.AutoHide = true;
            this.barRight.AutoSyncBarCaption = true;
            this.barRight.CanHide = true;
            this.barRight.CloseSingleTab = true;
            this.barRight.Controls.Add(this.panel3D);
            this.barRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.barRight.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.barRight.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dock3D});
            this.barRight.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.barRight.Location = new System.Drawing.Point(3, 64);
            this.barRight.Name = "barRight";
            this.barRight.Size = new System.Drawing.Size(306, 61);
            this.barRight.Stretch = true;
            this.barRight.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barRight.TabIndex = 0;
            this.barRight.TabStop = false;
            this.barRight.Text = "地图模板";
            this.barRight.Visible = false;
            this.barRight.VisibleChanged += new System.EventHandler(this.barRight_VisibleChanged);
            // 
            // panel3D
            // 
            this.panel3D.Controls.Add(this.treeViewTemplate);
            this.panel3D.DisabledBackColor = System.Drawing.Color.Empty;
            this.panel3D.Location = new System.Drawing.Point(3, 23);
            this.panel3D.Name = "panel3D";
            this.panel3D.Size = new System.Drawing.Size(300, 35);
            this.panel3D.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panel3D.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panel3D.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panel3D.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panel3D.Style.GradientAngle = 90;
            this.panel3D.TabIndex = 0;
            // 
            // treeViewTemplate
            // 
            this.treeViewTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTemplate.Location = new System.Drawing.Point(0, 0);
            this.treeViewTemplate.Name = "treeViewTemplate";
            this.treeViewTemplate.Size = new System.Drawing.Size(300, 35);
            this.treeViewTemplate.TabIndex = 16;
            this.treeViewTemplate.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewTemplate_NodeMouseDoubleClick);
            // 
            // dock3D
            // 
            this.dock3D.Control = this.panel3D;
            this.dock3D.Name = "dock3D";
            this.dock3D.Text = "地图模板";
            // 
            // barDataRv
            // 
            this.barDataRv.AccessibleDescription = "DotNetBar Bar (barDataRv)";
            this.barDataRv.AccessibleName = "DotNetBar Bar";
            this.barDataRv.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.barDataRv.AutoSyncBarCaption = true;
            this.barDataRv.CloseSingleTab = true;
            this.barDataRv.Controls.Add(this.panelDockContainer1);
            this.barDataRv.Controls.Add(this.panelDockContainer2);
            this.barDataRv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.barDataRv.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.barDataRv.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockDataRv,
            this.dockDataSearch});
            this.barDataRv.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.barDataRv.Location = new System.Drawing.Point(3, 128);
            this.barDataRv.Name = "barDataRv";
            this.barDataRv.SelectedDockTab = 0;
            this.barDataRv.Size = new System.Drawing.Size(306, 83);
            this.barDataRv.Stretch = true;
            this.barDataRv.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barDataRv.TabIndex = 1;
            this.barDataRv.TabStop = false;
            this.barDataRv.Text = "数据接收";
            this.barDataRv.Visible = false;
            // 
            // panelDockContainer1
            // 
            this.panelDockContainer1.Controls.Add(this.dataGridRv);
            this.panelDockContainer1.Controls.Add(this.bar1);
            this.panelDockContainer1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelDockContainer1.Location = new System.Drawing.Point(3, 23);
            this.panelDockContainer1.Name = "panelDockContainer1";
            this.panelDockContainer1.Size = new System.Drawing.Size(300, 32);
            this.panelDockContainer1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer1.Style.GradientAngle = 90;
            this.panelDockContainer1.TabIndex = 0;
            // 
            // dataGridRv
            // 
            this.dataGridRv.AllowUserToAddRows = false;
            this.dataGridRv.AutoGenerateColumns = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridRv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridRv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序号DataGridViewTextBoxColumn,
            this.文件名DataGridViewTextBoxColumn,
            this.存储路径DataGridViewTextBoxColumn,
            this.接收时间DataGridViewTextBoxColumn});
            this.dataGridRv.ContextMenuStrip = this.contextMenuStrip3;
            this.dataGridRv.DataMember = "FileRecTable";
            this.dataGridRv.DataSource = this.dataSettable;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridRv.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridRv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridRv.EnableHeadersVisualStyles = false;
            this.dataGridRv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridRv.Location = new System.Drawing.Point(0, 27);
            this.dataGridRv.Name = "dataGridRv";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridRv.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridRv.RowTemplate.Height = 23;
            this.dataGridRv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridRv.Size = new System.Drawing.Size(300, 5);
            this.dataGridRv.TabIndex = 2;
            // 
            // 序号DataGridViewTextBoxColumn
            // 
            this.序号DataGridViewTextBoxColumn.DataPropertyName = "序号";
            this.序号DataGridViewTextBoxColumn.HeaderText = "序号";
            this.序号DataGridViewTextBoxColumn.Name = "序号DataGridViewTextBoxColumn";
            this.序号DataGridViewTextBoxColumn.Width = 40;
            // 
            // 文件名DataGridViewTextBoxColumn
            // 
            this.文件名DataGridViewTextBoxColumn.DataPropertyName = "文件名";
            this.文件名DataGridViewTextBoxColumn.HeaderText = "文件名";
            this.文件名DataGridViewTextBoxColumn.Name = "文件名DataGridViewTextBoxColumn";
            this.文件名DataGridViewTextBoxColumn.Width = 150;
            // 
            // 存储路径DataGridViewTextBoxColumn
            // 
            this.存储路径DataGridViewTextBoxColumn.DataPropertyName = "存储路径";
            this.存储路径DataGridViewTextBoxColumn.HeaderText = "存储路径";
            this.存储路径DataGridViewTextBoxColumn.Name = "存储路径DataGridViewTextBoxColumn";
            this.存储路径DataGridViewTextBoxColumn.Width = 200;
            // 
            // 接收时间DataGridViewTextBoxColumn
            // 
            this.接收时间DataGridViewTextBoxColumn.DataPropertyName = "接收时间";
            this.接收时间DataGridViewTextBoxColumn.HeaderText = "接收时间";
            this.接收时间DataGridViewTextBoxColumn.Name = "接收时间DataGridViewTextBoxColumn";
            this.接收时间DataGridViewTextBoxColumn.Width = 150;
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolopenfile,
            this.tooltransandopen});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(137, 48);
            // 
            // toolopenfile
            // 
            this.toolopenfile.Name = "toolopenfile";
            this.toolopenfile.Size = new System.Drawing.Size(136, 22);
            this.toolopenfile.Text = "打开";
            // 
            // tooltransandopen
            // 
            this.tooltransandopen.Name = "tooltransandopen";
            this.tooltransandopen.Size = new System.Drawing.Size(136, 22);
            this.tooltransandopen.Text = "打开并转换";
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnStartListen,
            this.btnStopListen,
            this.txtFolder,
            this.btnBrowse});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(300, 27);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btnStartListen
            // 
            this.btnStartListen.Name = "btnStartListen";
            this.btnStartListen.Text = "Start";
            // 
            // btnStopListen
            // 
            this.btnStopListen.Name = "btnStopListen";
            this.btnStopListen.Text = "Stop";
            // 
            // txtFolder
            // 
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.TextBoxWidth = 200;
            this.txtFolder.WatermarkColor = System.Drawing.SystemColors.GrayText;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Text = "浏览";
            // 
            // panelDockContainer2
            // 
            this.panelDockContainer2.Controls.Add(this.panelEx1);
            this.panelDockContainer2.Controls.Add(this.panelExSearch);
            this.panelDockContainer2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelDockContainer2.Location = new System.Drawing.Point(3, 23);
            this.panelDockContainer2.Name = "panelDockContainer2";
            this.panelDockContainer2.Size = new System.Drawing.Size(300, 32);
            this.panelDockContainer2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer2.Style.GradientAngle = 90;
            this.panelDockContainer2.TabIndex = 2;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.datagridzdFile);
            this.panelEx1.Controls.Add(this.datagridLocal);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 178);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(300, 0);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 4;
            // 
            // datagridzdFile
            // 
            this.datagridzdFile.AllowUserToAddRows = false;
            this.datagridzdFile.AutoGenerateColumns = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridzdFile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.datagridzdFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridzdFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.szfilenameDataGridViewTextBoxColumn,
            this.szfilenamesDataGridViewTextBoxColumn,
            this.uiTaskCodeDataGridViewTextBoxColumn,
            this.uiObjCodeDataGridViewTextBoxColumn,
            this.uiFileTypesDataGridViewTextBoxColumn});
            this.datagridzdFile.DataMember = "zdFileTable";
            this.datagridzdFile.DataSource = this.dataSettable;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datagridzdFile.DefaultCellStyle = dataGridViewCellStyle8;
            this.datagridzdFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datagridzdFile.EnableHeadersVisualStyles = false;
            this.datagridzdFile.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.datagridzdFile.Location = new System.Drawing.Point(0, 0);
            this.datagridzdFile.MultiSelect = false;
            this.datagridzdFile.Name = "datagridzdFile";
            this.datagridzdFile.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridzdFile.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.datagridzdFile.RowTemplate.Height = 23;
            this.datagridzdFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagridzdFile.Size = new System.Drawing.Size(300, 0);
            this.datagridzdFile.TabIndex = 5;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 40;
            // 
            // szfilenameDataGridViewTextBoxColumn
            // 
            this.szfilenameDataGridViewTextBoxColumn.DataPropertyName = "szfilename";
            this.szfilenameDataGridViewTextBoxColumn.HeaderText = "szfilename";
            this.szfilenameDataGridViewTextBoxColumn.Name = "szfilenameDataGridViewTextBoxColumn";
            this.szfilenameDataGridViewTextBoxColumn.ReadOnly = true;
            this.szfilenameDataGridViewTextBoxColumn.Width = 150;
            // 
            // szfilenamesDataGridViewTextBoxColumn
            // 
            this.szfilenamesDataGridViewTextBoxColumn.DataPropertyName = "szfilenames";
            this.szfilenamesDataGridViewTextBoxColumn.HeaderText = "szfilenames";
            this.szfilenamesDataGridViewTextBoxColumn.Name = "szfilenamesDataGridViewTextBoxColumn";
            this.szfilenamesDataGridViewTextBoxColumn.ReadOnly = true;
            this.szfilenamesDataGridViewTextBoxColumn.Width = 200;
            // 
            // uiTaskCodeDataGridViewTextBoxColumn
            // 
            this.uiTaskCodeDataGridViewTextBoxColumn.DataPropertyName = "uiTaskCode";
            this.uiTaskCodeDataGridViewTextBoxColumn.HeaderText = "uiTaskCode";
            this.uiTaskCodeDataGridViewTextBoxColumn.Name = "uiTaskCodeDataGridViewTextBoxColumn";
            this.uiTaskCodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.uiTaskCodeDataGridViewTextBoxColumn.Width = 80;
            // 
            // uiObjCodeDataGridViewTextBoxColumn
            // 
            this.uiObjCodeDataGridViewTextBoxColumn.DataPropertyName = "uiObjCode";
            this.uiObjCodeDataGridViewTextBoxColumn.HeaderText = "uiObjCode";
            this.uiObjCodeDataGridViewTextBoxColumn.Name = "uiObjCodeDataGridViewTextBoxColumn";
            this.uiObjCodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.uiObjCodeDataGridViewTextBoxColumn.Width = 80;
            // 
            // uiFileTypesDataGridViewTextBoxColumn
            // 
            this.uiFileTypesDataGridViewTextBoxColumn.DataPropertyName = "uiFileTypes";
            this.uiFileTypesDataGridViewTextBoxColumn.HeaderText = "uiFileTypes";
            this.uiFileTypesDataGridViewTextBoxColumn.Name = "uiFileTypesDataGridViewTextBoxColumn";
            this.uiFileTypesDataGridViewTextBoxColumn.ReadOnly = true;
            this.uiFileTypesDataGridViewTextBoxColumn.Width = 80;
            // 
            // datagridLocal
            // 
            this.datagridLocal.AllowUserToAddRows = false;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridLocal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.datagridLocal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridLocal.ContextMenuStrip = this.contextMenuStrip3;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datagridLocal.DefaultCellStyle = dataGridViewCellStyle11;
            this.datagridLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datagridLocal.EnableHeadersVisualStyles = false;
            this.datagridLocal.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.datagridLocal.Location = new System.Drawing.Point(0, 0);
            this.datagridLocal.MultiSelect = false;
            this.datagridLocal.Name = "datagridLocal";
            this.datagridLocal.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridLocal.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.datagridLocal.RowTemplate.Height = 23;
            this.datagridLocal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagridLocal.Size = new System.Drawing.Size(300, 0);
            this.datagridLocal.TabIndex = 4;
            // 
            // panelExSearch
            // 
            this.panelExSearch.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExSearch.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelExSearch.Controls.Add(this.panelRecords);
            this.panelExSearch.Controls.Add(this.panelRemote);
            this.panelExSearch.Controls.Add(this.panelLocal);
            this.panelExSearch.Controls.Add(this.panelDataSource);
            this.panelExSearch.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelExSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExSearch.Location = new System.Drawing.Point(0, 0);
            this.panelExSearch.Name = "panelExSearch";
            this.panelExSearch.Size = new System.Drawing.Size(300, 178);
            this.panelExSearch.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExSearch.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelExSearch.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExSearch.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExSearch.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelExSearch.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExSearch.Style.GradientAngle = 90;
            this.panelExSearch.TabIndex = 0;
            // 
            // panelRecords
            // 
            this.panelRecords.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelRecords.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelRecords.Controls.Add(this.gridRecords);
            this.panelRecords.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelRecords.Location = new System.Drawing.Point(360, 0);
            this.panelRecords.Name = "panelRecords";
            this.panelRecords.Size = new System.Drawing.Size(288, 178);
            this.panelRecords.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelRecords.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelRecords.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelRecords.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelRecords.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelRecords.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelRecords.Style.GradientAngle = 90;
            this.panelRecords.TabIndex = 23;
            this.panelRecords.Visible = false;
            // 
            // gridRecords
            // 
            this.gridRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRecords.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.gridRecords.Location = new System.Drawing.Point(0, 0);
            this.gridRecords.Name = "gridRecords";
            // 
            // 
            // 
            this.gridRecords.PrimaryGrid.AllowRowInsert = true;
            this.gridRecords.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.gridRecords.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.gridRecords.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.gridRecords.Size = new System.Drawing.Size(288, 178);
            this.gridRecords.TabIndex = 0;
            this.gridRecords.Text = "superGridControl1";
            // 
            // panelRemote
            // 
            this.panelRemote.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelRemote.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelRemote.Controls.Add(this.lblfiletype);
            this.panelRemote.Controls.Add(this.lblSaveFloder);
            this.panelRemote.Controls.Add(this.labelX2);
            this.panelRemote.Controls.Add(this.cmbfiletype);
            this.panelRemote.Controls.Add(this.lblStationID);
            this.panelRemote.Controls.Add(this.txtSaveFloder);
            this.panelRemote.Controls.Add(this.btnSaveFloder);
            this.panelRemote.Controls.Add(this.RichTBoxSQL);
            this.panelRemote.Controls.Add(this.cmbStationID);
            this.panelRemote.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelRemote.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelRemote.Location = new System.Drawing.Point(72, 0);
            this.panelRemote.Name = "panelRemote";
            this.panelRemote.Size = new System.Drawing.Size(288, 178);
            this.panelRemote.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelRemote.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelRemote.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelRemote.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelRemote.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelRemote.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelRemote.Style.GradientAngle = 90;
            this.panelRemote.TabIndex = 20;
            // 
            // lblfiletype
            // 
            // 
            // 
            // 
            this.lblfiletype.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblfiletype.Location = new System.Drawing.Point(3, 13);
            this.lblfiletype.Name = "lblfiletype";
            this.lblfiletype.Size = new System.Drawing.Size(69, 25);
            this.lblfiletype.TabIndex = 12;
            this.lblfiletype.Text = "查询类型：";
            // 
            // lblSaveFloder
            // 
            // 
            // 
            // 
            this.lblSaveFloder.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSaveFloder.Location = new System.Drawing.Point(3, 124);
            this.lblSaveFloder.Name = "lblSaveFloder";
            this.lblSaveFloder.Size = new System.Drawing.Size(69, 25);
            this.lblSaveFloder.TabIndex = 14;
            this.lblSaveFloder.Text = "存储路径：";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(3, 79);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(69, 25);
            this.labelX2.TabIndex = 7;
            this.labelX2.Text = "查询条件：";
            // 
            // cmbfiletype
            // 
            this.cmbfiletype.DisplayMember = "Text";
            this.cmbfiletype.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbfiletype.FormattingEnabled = true;
            this.cmbfiletype.ItemHeight = 14;
            this.cmbfiletype.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5,
            this.comboItem6,
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem10,
            this.comboItem11,
            this.comboItem12});
            this.cmbfiletype.Location = new System.Drawing.Point(78, 10);
            this.cmbfiletype.Name = "cmbfiletype";
            this.cmbfiletype.Size = new System.Drawing.Size(199, 20);
            this.cmbfiletype.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbfiletype.TabIndex = 13;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "导航点确定结果文件";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "路径单元规划控制参数文件";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "导航相机原始图像文件";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "全景相机原始图像文件";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "避障相机原始图像文件";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "Dom文件";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "巡视器全景相机原始彩色复原图像文件";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "100厘米两级DEM地形文件";
            // 
            // comboItem9
            // 
            this.comboItem9.Text = "10厘米量级DEM地形文件";
            // 
            // comboItem10
            // 
            this.comboItem10.Text = "1厘米量级DEM地形文件";
            // 
            // comboItem11
            // 
            this.comboItem11.Text = "1厘米以下高分辨率DEM地形文件";
            // 
            // comboItem12
            // 
            this.comboItem12.Text = "着陆点定位文件";
            // 
            // lblStationID
            // 
            // 
            // 
            // 
            this.lblStationID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblStationID.Location = new System.Drawing.Point(3, 44);
            this.lblStationID.Name = "lblStationID";
            this.lblStationID.Size = new System.Drawing.Size(69, 25);
            this.lblStationID.TabIndex = 17;
            this.lblStationID.Text = "站点信息：";
            // 
            // txtSaveFloder
            // 
            this.txtSaveFloder.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtSaveFloder.Border.Class = "TextBoxBorder";
            this.txtSaveFloder.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSaveFloder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSaveFloder.Location = new System.Drawing.Point(78, 124);
            this.txtSaveFloder.Name = "txtSaveFloder";
            this.txtSaveFloder.Size = new System.Drawing.Size(147, 20);
            this.txtSaveFloder.TabIndex = 15;
            // 
            // btnSaveFloder
            // 
            this.btnSaveFloder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveFloder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveFloder.Location = new System.Drawing.Point(231, 124);
            this.btnSaveFloder.Name = "btnSaveFloder";
            this.btnSaveFloder.Size = new System.Drawing.Size(46, 23);
            this.btnSaveFloder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSaveFloder.TabIndex = 16;
            this.btnSaveFloder.Text = "浏览";
            // 
            // RichTBoxSQL
            // 
            // 
            // 
            // 
            this.RichTBoxSQL.BackgroundStyle.Class = "RichTextBoxBorder";
            this.RichTBoxSQL.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RichTBoxSQL.Location = new System.Drawing.Point(78, 74);
            this.RichTBoxSQL.Name = "RichTBoxSQL";
            this.RichTBoxSQL.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fnil\\fcharset" +
                "0 Microsoft Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\lang2052\\f0\\fs17\\par\r\n}\r\n";
            this.RichTBoxSQL.Size = new System.Drawing.Size(199, 39);
            this.RichTBoxSQL.TabIndex = 6;
            // 
            // cmbStationID
            // 
            this.cmbStationID.DisplayMember = "Text";
            this.cmbStationID.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStationID.FormattingEnabled = true;
            this.cmbStationID.ItemHeight = 14;
            this.cmbStationID.Location = new System.Drawing.Point(78, 41);
            this.cmbStationID.Name = "cmbStationID";
            this.cmbStationID.Size = new System.Drawing.Size(199, 20);
            this.cmbStationID.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbStationID.TabIndex = 18;
            // 
            // panelLocal
            // 
            this.panelLocal.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelLocal.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelLocal.Controls.Add(this.btnSetSQL);
            this.panelLocal.Controls.Add(this.lblsqlset);
            this.panelLocal.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelLocal.Location = new System.Drawing.Point(72, 0);
            this.panelLocal.Name = "panelLocal";
            this.panelLocal.Size = new System.Drawing.Size(288, 178);
            this.panelLocal.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelLocal.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelLocal.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelLocal.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelLocal.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelLocal.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelLocal.Style.GradientAngle = 90;
            this.panelLocal.TabIndex = 21;
            this.panelLocal.Visible = false;
            // 
            // btnSetSQL
            // 
            this.btnSetSQL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSetSQL.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSetSQL.Image = global::CERMapping.Properties.Resources.GenericCalculator16;
            this.btnSetSQL.Location = new System.Drawing.Point(112, 22);
            this.btnSetSQL.Name = "btnSetSQL";
            this.btnSetSQL.Size = new System.Drawing.Size(18, 23);
            this.btnSetSQL.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSetSQL.TabIndex = 11;
            // 
            // lblsqlset
            // 
            // 
            // 
            // 
            this.lblsqlset.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblsqlset.Location = new System.Drawing.Point(6, 21);
            this.lblsqlset.Name = "lblsqlset";
            this.lblsqlset.Size = new System.Drawing.Size(100, 25);
            this.lblsqlset.TabIndex = 5;
            this.lblsqlset.Text = "查询条件设置：";
            // 
            // panelDataSource
            // 
            this.panelDataSource.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelDataSource.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelDataSource.Controls.Add(this.btnSearch);
            this.panelDataSource.Controls.Add(this.RadioBoxService);
            this.panelDataSource.Controls.Add(this.RadioBoxLocal);
            this.panelDataSource.Controls.Add(this.txtRecorders);
            this.panelDataSource.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelDataSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelDataSource.Location = new System.Drawing.Point(0, 0);
            this.panelDataSource.Name = "panelDataSource";
            this.panelDataSource.Size = new System.Drawing.Size(72, 178);
            this.panelDataSource.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDataSource.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelDataSource.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelDataSource.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelDataSource.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelDataSource.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelDataSource.Style.GradientAngle = 90;
            this.panelDataSource.TabIndex = 22;
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(11, 76);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(50, 60);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnDownLoadAll,
            this.btnSeekSite});
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "查询";
            // 
            // btnDownLoadAll
            // 
            this.btnDownLoadAll.GlobalItem = false;
            this.btnDownLoadAll.Name = "btnDownLoadAll";
            this.btnDownLoadAll.Text = "下载全部";
            // 
            // btnSeekSite
            // 
            this.btnSeekSite.GlobalItem = false;
            this.btnSeekSite.Name = "btnSeekSite";
            this.btnSeekSite.Text = "查询站点";
            // 
            // RadioBoxService
            // 
            // 
            // 
            // 
            this.RadioBoxService.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RadioBoxService.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.RadioBoxService.Checked = true;
            this.RadioBoxService.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RadioBoxService.CheckValue = "Y";
            this.RadioBoxService.Location = new System.Drawing.Point(11, 11);
            this.RadioBoxService.Name = "RadioBoxService";
            this.RadioBoxService.Size = new System.Drawing.Size(50, 25);
            this.RadioBoxService.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.RadioBoxService.TabIndex = 10;
            this.RadioBoxService.Text = "远程";
            // 
            // RadioBoxLocal
            // 
            // 
            // 
            // 
            this.RadioBoxLocal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RadioBoxLocal.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.RadioBoxLocal.Location = new System.Drawing.Point(11, 42);
            this.RadioBoxLocal.Name = "RadioBoxLocal";
            this.RadioBoxLocal.Size = new System.Drawing.Size(50, 25);
            this.RadioBoxLocal.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.RadioBoxLocal.TabIndex = 8;
            this.RadioBoxLocal.Text = "本地";
            // 
            // txtRecorders
            // 
            this.txtRecorders.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtRecorders.Border.Class = "TextBoxBorder";
            this.txtRecorders.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRecorders.Enabled = false;
            this.txtRecorders.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtRecorders.Location = new System.Drawing.Point(11, 142);
            this.txtRecorders.Name = "txtRecorders";
            this.txtRecorders.Size = new System.Drawing.Size(50, 20);
            this.txtRecorders.TabIndex = 19;
            // 
            // dockDataRv
            // 
            this.dockDataRv.Control = this.panelDockContainer1;
            this.dockDataRv.Name = "dockDataRv";
            this.dockDataRv.Text = "数据接收";
            this.dockDataRv.Visible = false;
            // 
            // dockDataSearch
            // 
            this.dockDataSearch.Control = this.panelDockContainer2;
            this.dockDataSearch.Name = "dockDataSearch";
            this.dockDataSearch.Text = "数据查询";
            this.dockDataSearch.Visible = false;
            // 
            // dockSite8
            // 
            this.dockSite8.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite8.Location = new System.Drawing.Point(0, 603);
            this.dockSite8.Name = "dockSite8";
            this.dockSite8.Size = new System.Drawing.Size(1145, 0);
            this.dockSite8.TabIndex = 7;
            this.dockSite8.TabStop = false;
            // 
            // dockSite5
            // 
            this.dockSite5.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite5.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite5.Location = new System.Drawing.Point(0, 264);
            this.dockSite5.Name = "dockSite5";
            this.dockSite5.Size = new System.Drawing.Size(0, 339);
            this.dockSite5.TabIndex = 4;
            this.dockSite5.TabStop = false;
            // 
            // dockSite6
            // 
            this.dockSite6.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite6.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite6.Location = new System.Drawing.Point(1145, 264);
            this.dockSite6.Name = "dockSite6";
            this.dockSite6.Size = new System.Drawing.Size(0, 339);
            this.dockSite6.TabIndex = 5;
            this.dockSite6.TabStop = false;
            // 
            // dockSite7
            // 
            this.dockSite7.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite7.ContextMenuStrip = this.contextMenuBar;
            this.dockSite7.Controls.Add(this.menuMain);
            this.dockSite7.Controls.Add(this.barContexMenuEditor);
            this.dockSite7.Controls.Add(this.barTerrain);
            this.dockSite7.Controls.Add(this.barCustom);
            this.dockSite7.Controls.Add(this.barGeoAdjust);
            this.dockSite7.Controls.Add(this.bar3D);
            this.dockSite7.Controls.Add(this.barCommon);
            this.dockSite7.Controls.Add(this.barSunAlt);
            this.dockSite7.Controls.Add(this.barGeoReference);
            this.dockSite7.Controls.Add(this.barLayout);
            this.dockSite7.Controls.Add(this.barEffects);
            this.dockSite7.Controls.Add(this.barEditor);
            this.dockSite7.Controls.Add(this.barTinEditor);
            this.dockSite7.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite7.Location = new System.Drawing.Point(0, 0);
            this.dockSite7.Name = "dockSite7";
            this.dockSite7.Size = new System.Drawing.Size(1145, 264);
            this.dockSite7.TabIndex = 6;
            this.dockSite7.TabStop = false;
            // 
            // contextMenuBar
            // 
            this.contextMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.popupBarCommon,
            this.popupBarLayout,
            this.popupBar3D,
            this.toolStripSeparator1,
            this.popupBarEditor,
            this.popupBarTinEditor,
            this.popupBarGeoReference,
            this.popupBarGeoAdjust,
            this.toolStripSeparator2,
            this.popupBarEffects,
            this.popupBarSunalt,
            this.toolStripSeparator3,
            this.popupBarCustom});
            this.contextMenuBar.Name = "contextMenuStrip1";
            this.contextMenuBar.Size = new System.Drawing.Size(149, 242);
            this.contextMenuBar.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenubar_Opening);
            // 
            // popupBarCommon
            // 
            this.popupBarCommon.CheckOnClick = true;
            this.popupBarCommon.Name = "popupBarCommon";
            this.popupBarCommon.Size = new System.Drawing.Size(148, 22);
            this.popupBarCommon.Text = "地图工具";
            this.popupBarCommon.Click += new System.EventHandler(this.popupBarCommon_Click);
            // 
            // popupBarLayout
            // 
            this.popupBarLayout.CheckOnClick = true;
            this.popupBarLayout.Name = "popupBarLayout";
            this.popupBarLayout.Size = new System.Drawing.Size(148, 22);
            this.popupBarLayout.Text = "布局工具";
            this.popupBarLayout.Click += new System.EventHandler(this.popupBarLayout_Click);
            // 
            // popupBar3D
            // 
            this.popupBar3D.CheckOnClick = true;
            this.popupBar3D.Name = "popupBar3D";
            this.popupBar3D.Size = new System.Drawing.Size(148, 22);
            this.popupBar3D.Text = "三维工具";
            this.popupBar3D.Click += new System.EventHandler(this.popupBar3D_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // popupBarEditor
            // 
            this.popupBarEditor.CheckOnClick = true;
            this.popupBarEditor.Name = "popupBarEditor";
            this.popupBarEditor.Size = new System.Drawing.Size(148, 22);
            this.popupBarEditor.Text = "编辑工具";
            this.popupBarEditor.Click += new System.EventHandler(this.popupBarEditor_Click);
            // 
            // popupBarTinEditor
            // 
            this.popupBarTinEditor.CheckOnClick = true;
            this.popupBarTinEditor.Name = "popupBarTinEditor";
            this.popupBarTinEditor.Size = new System.Drawing.Size(148, 22);
            this.popupBarTinEditor.Text = "TIN编辑";
            this.popupBarTinEditor.Visible = false;
            this.popupBarTinEditor.Click += new System.EventHandler(this.popupBarTinEditor_Click);
            // 
            // popupBarGeoReference
            // 
            this.popupBarGeoReference.CheckOnClick = true;
            this.popupBarGeoReference.Name = "popupBarGeoReference";
            this.popupBarGeoReference.Size = new System.Drawing.Size(148, 22);
            this.popupBarGeoReference.Text = "栅格处理";
            this.popupBarGeoReference.Click += new System.EventHandler(this.popupBarGeoReference_Click);
            // 
            // popupBarGeoAdjust
            // 
            this.popupBarGeoAdjust.CheckOnClick = true;
            this.popupBarGeoAdjust.Name = "popupBarGeoAdjust";
            this.popupBarGeoAdjust.Size = new System.Drawing.Size(148, 22);
            this.popupBarGeoAdjust.Text = "矢量校正";
            this.popupBarGeoAdjust.Click += new System.EventHandler(this.popupBarGeoAdjust_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // popupBarEffects
            // 
            this.popupBarEffects.CheckOnClick = true;
            this.popupBarEffects.Name = "popupBarEffects";
            this.popupBarEffects.Size = new System.Drawing.Size(148, 22);
            this.popupBarEffects.Text = "图层效果";
            this.popupBarEffects.Visible = false;
            this.popupBarEffects.Click += new System.EventHandler(this.popupBarEffects_Click);
            // 
            // popupBarSunalt
            // 
            this.popupBarSunalt.CheckOnClick = true;
            this.popupBarSunalt.Name = "popupBarSunalt";
            this.popupBarSunalt.Size = new System.Drawing.Size(148, 22);
            this.popupBarSunalt.Text = "高度角测量";
            this.popupBarSunalt.Visible = false;
            this.popupBarSunalt.Click += new System.EventHandler(this.popupBarSunalt_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(145, 6);
            // 
            // popupBarCustom
            // 
            this.popupBarCustom.CheckOnClick = true;
            this.popupBarCustom.Name = "popupBarCustom";
            this.popupBarCustom.Size = new System.Drawing.Size(148, 22);
            this.popupBarCustom.Text = "自定义工具栏";
            this.popupBarCustom.Click += new System.EventHandler(this.popupBarCustom_Click);
            // 
            // menuMain
            // 
            this.menuMain.AccessibleDescription = "DotNetBar Bar (menuMain)";
            this.menuMain.AccessibleName = "DotNetBar Bar";
            this.menuMain.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.menuMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuMain.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.menuMain.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.menuMain.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.menuFile,
            this.toolcreatfile,
            this.menuView,
            this.menuData,
            this.menuProcess,
            this.buttonItem19,
            this.menuMapping,
            this.menu3DAnalyst});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.MenuBar = true;
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1145, 26);
            this.menuMain.Stretch = true;
            this.menuMain.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.menuMain.TabIndex = 0;
            this.menuMain.TabStop = false;
            this.menuMain.Text = "主菜单";
            // 
            // menuFile
            // 
            this.menuFile.Name = "menuFile";
            this.menuFile.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemNewDoc_Click,
            this.buttonItemOpenDoc,
            this.buttonItemSaveDoc,
            this.buttonItemSaveAs,
            this.btnItemExportMxd,
            this.btnCheckUpdate,
            this.buttonItemExit});
            this.menuFile.Text = "文件";
            // 
            // buttonItemNewDoc_Click
            // 
            this.buttonItemNewDoc_Click.Name = "buttonItemNewDoc_Click";
            this.buttonItemNewDoc_Click.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlN);
            this.buttonItemNewDoc_Click.Text = "新建工作空间";
            this.buttonItemNewDoc_Click.Click += new System.EventHandler(this.buttonItemNewDoc_Click_Click);
            // 
            // buttonItemOpenDoc
            // 
            this.buttonItemOpenDoc.Name = "buttonItemOpenDoc";
            this.buttonItemOpenDoc.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlO);
            this.buttonItemOpenDoc.Text = "打开工作空间";
            this.buttonItemOpenDoc.Click += new System.EventHandler(this.buttonItemOpenDoc_Click);
            // 
            // buttonItemSaveDoc
            // 
            this.buttonItemSaveDoc.Name = "buttonItemSaveDoc";
            this.buttonItemSaveDoc.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS);
            this.buttonItemSaveDoc.Text = "保存工作空间";
            this.buttonItemSaveDoc.Click += new System.EventHandler(this.buttonItemSaveDoc_Click);
            // 
            // buttonItemSaveAs
            // 
            this.buttonItemSaveAs.Name = "buttonItemSaveAs";
            this.buttonItemSaveAs.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
            this.buttonItemSaveAs.Text = "另存工作空间";
            this.buttonItemSaveAs.Click += new System.EventHandler(this.buttonItemSaveAs_Click);
            // 
            // btnItemExportMxd
            // 
            this.btnItemExportMxd.BeginGroup = true;
            this.btnItemExportMxd.Name = "btnItemExportMxd";
            this.btnItemExportMxd.Text = "导出工作空间";
            this.btnItemExportMxd.Click += new System.EventHandler(this.btnItemExportMxd_Click);
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.BeginGroup = true;
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Text = "检查更新";
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // buttonItemExit
            // 
            this.buttonItemExit.BeginGroup = true;
            this.buttonItemExit.Name = "buttonItemExit";
            this.buttonItemExit.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.AltF4);
            this.buttonItemExit.Text = "退出";
            this.buttonItemExit.Click += new System.EventHandler(this.buttonItemExit_Click);
            // 
            // toolcreatfile
            // 
            this.toolcreatfile.Name = "toolcreatfile";
            this.toolcreatfile.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem3,
            this.buttonItemDEMToTIN,
            this.buttonItemTINToDEM,
            this.buttonItemPointToLine,
            this.btnmxdRAR,
            this.btnRasterExportBatch,
            this.buttonItemCustomSet});
            this.toolcreatfile.Text = "工具";
            // 
            // buttonItem3
            // 
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemNewFeature,
            this.buttonItemCreateShapeFile,
            this.buttonItemnewFeatureLayer,
            this.menuShpFromFile});
            this.buttonItem3.Text = "创建文件";
            // 
            // buttonItemNewFeature
            // 
            this.buttonItemNewFeature.Name = "buttonItemNewFeature";
            this.buttonItemNewFeature.Text = "创建数据库";
            this.buttonItemNewFeature.Click += new System.EventHandler(this.buttonItemNewFeature_Click);
            // 
            // buttonItemCreateShapeFile
            // 
            this.buttonItemCreateShapeFile.Name = "buttonItemCreateShapeFile";
            this.buttonItemCreateShapeFile.Text = "创建Shape文件";
            this.buttonItemCreateShapeFile.Click += new System.EventHandler(this.buttonItemCreateShapeFile_Click);
            // 
            // buttonItemnewFeatureLayer
            // 
            this.buttonItemnewFeatureLayer.Name = "buttonItemnewFeatureLayer";
            this.buttonItemnewFeatureLayer.Text = "创建mulitpatch";
            this.buttonItemnewFeatureLayer.Click += new System.EventHandler(this.buttonItemnewFeatureLayer_Click);
            // 
            // menuShpFromFile
            // 
            this.menuShpFromFile.Name = "menuShpFromFile";
            this.menuShpFromFile.Text = "从文件创建点图层";
            this.menuShpFromFile.Click += new System.EventHandler(this.menuShpFromFile_Click);
            // 
            // buttonItemDEMToTIN
            // 
            this.buttonItemDEMToTIN.Name = "buttonItemDEMToTIN";
            this.buttonItemDEMToTIN.Text = "DEM转TIN";
            this.buttonItemDEMToTIN.Click += new System.EventHandler(this.buttonItemDEMToTIN_Click);
            // 
            // buttonItemTINToDEM
            // 
            this.buttonItemTINToDEM.Name = "buttonItemTINToDEM";
            this.buttonItemTINToDEM.Text = "TIN转DEM";
            this.buttonItemTINToDEM.Click += new System.EventHandler(this.buttonItemTINToDEM_Click);
            // 
            // buttonItemPointToLine
            // 
            this.buttonItemPointToLine.Name = "buttonItemPointToLine";
            this.buttonItemPointToLine.Text = "点转线";
            this.buttonItemPointToLine.Click += new System.EventHandler(this.buttonItemPointToLine_Click);
            // 
            // btnmxdRAR
            // 
            this.btnmxdRAR.Name = "btnmxdRAR";
            this.btnmxdRAR.Text = "数据打包";
            this.btnmxdRAR.Click += new System.EventHandler(this.btnmxdRAR_Click);
            // 
            // btnRasterExportBatch
            // 
            this.btnRasterExportBatch.Name = "btnRasterExportBatch";
            this.btnRasterExportBatch.Text = "栅格数据批量导出";
            this.btnRasterExportBatch.Click += new System.EventHandler(this.btnRasterExportBatch_Click);
            // 
            // buttonItemCustomSet
            // 
            this.buttonItemCustomSet.BeginGroup = true;
            this.buttonItemCustomSet.Name = "buttonItemCustomSet";
            this.buttonItemCustomSet.Text = "自定义工具设置";
            this.buttonItemCustomSet.Click += new System.EventHandler(this.buttonItemCustomSet_Click);
            // 
            // menuView
            // 
            this.menuView.Name = "menuView";
            this.menuView.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnbarleft,
            this.btnbarright,
            this.btnmap,
            this.btnlayout,
            this.btn3d});
            this.menuView.Text = "视图";
            // 
            // btnbarleft
            // 
            this.btnbarleft.AutoCheckOnClick = true;
            this.btnbarleft.Checked = true;
            this.btnbarleft.Name = "btnbarleft";
            this.btnbarleft.Text = "图层管理窗口";
            this.btnbarleft.CheckedChanged += new System.EventHandler(this.btnbarleft_CheckedChanged);
            // 
            // btnbarright
            // 
            this.btnbarright.AutoCheckOnClick = true;
            this.btnbarright.Name = "btnbarright";
            this.btnbarright.Text = "地图模板窗口";
            this.btnbarright.CheckedChanged += new System.EventHandler(this.btnbarright_CheckedChanged);
            this.btnbarright.Click += new System.EventHandler(this.btnbarright_Click);
            // 
            // btnmap
            // 
            this.btnmap.AutoCheckOnClick = true;
            this.btnmap.Checked = true;
            this.btnmap.Name = "btnmap";
            this.btnmap.Text = "二维地图窗口";
            this.btnmap.CheckedChanged += new System.EventHandler(this.btnmap_CheckedChanged);
            // 
            // btnlayout
            // 
            this.btnlayout.AutoCheckOnClick = true;
            this.btnlayout.Name = "btnlayout";
            this.btnlayout.Text = "布局窗口";
            this.btnlayout.CheckedChanged += new System.EventHandler(this.btnlayout_CheckedChanged);
            // 
            // btn3d
            // 
            this.btn3d.AutoCheckOnClick = true;
            this.btn3d.Name = "btn3d";
            this.btn3d.Text = "三维窗口";
            this.btn3d.CheckedChanged += new System.EventHandler(this.btn3d_CheckedChanged);
            // 
            // menuData
            // 
            this.menuData.Name = "menuData";
            this.menuData.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnIFLI_RPACPA,
            this.btnIFLI_MTRPPLAN,
            this.btnIFLI_GMP,
            this.menuBtnRaster,
            this.menuBtnTransform});
            this.menuData.Text = "数据管理模块";
            // 
            // btnIFLI_RPACPA
            // 
            this.btnIFLI_RPACPA.Name = "btnIFLI_RPACPA";
            this.btnIFLI_RPACPA.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btn_CreateFromXML,
            this.btn_CreateLabel,
            this.btn_CreateImagePath,
            this.btnMapSymbol});
            this.btnIFLI_RPACPA.Text = "检测记录管理";
            // 
            // btn_CreateFromXML
            // 
            this.btn_CreateFromXML.Name = "btn_CreateFromXML";
            this.btn_CreateFromXML.Text = "创建检查记录";
            this.btn_CreateFromXML.Click += new System.EventHandler(this.btn_CreateFromXML_Click);
            // 
            // btn_CreateLabel
            // 
            this.btn_CreateLabel.Name = "btn_CreateLabel";
            this.btn_CreateLabel.Text = "删除检查记录";
            this.btn_CreateLabel.Click += new System.EventHandler(this.btn_CreateLabel_Click);
            // 
            // btn_CreateImagePath
            // 
            this.btn_CreateImagePath.Name = "btn_CreateImagePath";
            this.btn_CreateImagePath.Text = "记录查询";
            this.btn_CreateImagePath.Click += new System.EventHandler(this.btn_CreateImagePath_Click);
            // 
            // btnMapSymbol
            // 
            this.btnMapSymbol.Name = "btnMapSymbol";
            this.btnMapSymbol.Text = "从模板地图导入符号";
            this.btnMapSymbol.Visible = false;
            this.btnMapSymbol.Click += new System.EventHandler(this.btnMapSymbol_Click);
            // 
            // btnIFLI_MTRPPLAN
            // 
            this.btnIFLI_MTRPPLAN.Name = "btnIFLI_MTRPPLAN";
            this.btnIFLI_MTRPPLAN.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btn_CreatParkPoint,
            this.btnMapParkSymbol,
            this.buttonItem54});
            this.btnIFLI_MTRPPLAN.Text = "检测数据导入";
            // 
            // btn_CreatParkPoint
            // 
            this.btn_CreatParkPoint.Name = "btn_CreatParkPoint";
            this.btn_CreatParkPoint.Text = "内检测数据导入";
            this.btn_CreatParkPoint.Click += new System.EventHandler(this.btn_CreatParkPoint_Click);
            // 
            // btnMapParkSymbol
            // 
            this.btnMapParkSymbol.Name = "btnMapParkSymbol";
            this.btnMapParkSymbol.Text = "外检测数据导入";
            this.btnMapParkSymbol.Click += new System.EventHandler(this.btnMapParkSymbol_Click);
            // 
            // buttonItem54
            // 
            this.buttonItem54.Name = "buttonItem54";
            this.buttonItem54.Text = "中线数据导入";
            this.buttonItem54.Click += new System.EventHandler(this.buttonItem54_Click);
            // 
            // btnIFLI_GMP
            // 
            this.btnIFLI_GMP.Name = "btnIFLI_GMP";
            this.btnIFLI_GMP.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnCreatGmpPoint,
            this.btnINMapSymbol});
            this.btnIFLI_GMP.Text = "检测数据查询";
            this.btnIFLI_GMP.Visible = false;
            // 
            // btnCreatGmpPoint
            // 
            this.btnCreatGmpPoint.Name = "btnCreatGmpPoint";
            this.btnCreatGmpPoint.Text = "条件查询";
            this.btnCreatGmpPoint.Click += new System.EventHandler(this.btnCreatGmpPoint_Click);
            // 
            // btnINMapSymbol
            // 
            this.btnINMapSymbol.Name = "btnINMapSymbol";
            this.btnINMapSymbol.Text = "SQL查询";
            this.btnINMapSymbol.Click += new System.EventHandler(this.btnINMapSymbol_Click);
            // 
            // menuBtnRaster
            // 
            this.menuBtnRaster.BeginGroup = true;
            this.menuBtnRaster.Name = "menuBtnRaster";
            this.menuBtnRaster.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.menuBtnRasterPan,
            this.menuBtnRasterRotate,
            this.menuBtnRasterMirror,
            this.menuBtnFlip,
            this.menuBtnRasterZ,
            this.btn_RasterClip,
            this.menuBtnRasterResample});
            this.menuBtnRaster.Text = "栅格处理";
            this.menuBtnRaster.Visible = false;
            // 
            // menuBtnRasterPan
            // 
            this.menuBtnRasterPan.Name = "menuBtnRasterPan";
            this.menuBtnRasterPan.Text = "栅格平移";
            this.menuBtnRasterPan.Click += new System.EventHandler(this.menuBtnRasterPan_Click);
            // 
            // menuBtnRasterRotate
            // 
            this.menuBtnRasterRotate.Name = "menuBtnRasterRotate";
            this.menuBtnRasterRotate.Text = "栅格旋转";
            this.menuBtnRasterRotate.Click += new System.EventHandler(this.menuBtnRasterRotate_Click);
            // 
            // menuBtnRasterMirror
            // 
            this.menuBtnRasterMirror.Name = "menuBtnRasterMirror";
            this.menuBtnRasterMirror.Text = "水平镜像";
            this.menuBtnRasterMirror.Click += new System.EventHandler(this.menuBtnRasterMirror_Click);
            // 
            // menuBtnFlip
            // 
            this.menuBtnFlip.Name = "menuBtnFlip";
            this.menuBtnFlip.Text = "垂直翻转";
            this.menuBtnFlip.Click += new System.EventHandler(this.menuBtnFlip_Click);
            // 
            // menuBtnRasterZ
            // 
            this.menuBtnRasterZ.BeginGroup = true;
            this.menuBtnRasterZ.Name = "menuBtnRasterZ";
            this.menuBtnRasterZ.Text = "Z值变换";
            this.menuBtnRasterZ.Click += new System.EventHandler(this.menuBtnRasterZ_Click);
            // 
            // btn_RasterClip
            // 
            this.btn_RasterClip.Name = "btn_RasterClip";
            this.btn_RasterClip.Text = "栅格裁切";
            this.btn_RasterClip.Click += new System.EventHandler(this.btn_RasterClip_Click);
            // 
            // menuBtnRasterResample
            // 
            this.menuBtnRasterResample.Name = "menuBtnRasterResample";
            this.menuBtnRasterResample.Text = "栅格重采样";
            this.menuBtnRasterResample.Click += new System.EventHandler(this.menuBtnRasterResample_Click);
            // 
            // menuBtnTransform
            // 
            this.menuBtnTransform.Name = "menuBtnTransform";
            this.menuBtnTransform.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2,
            this.buttonItem5,
            this.btnNEDtoENU,
            this.btnNEDtoENUShape,
            this.btntoolmatrix});
            this.menuBtnTransform.Text = "矢量处理";
            this.menuBtnTransform.Visible = false;
            // 
            // buttonItem2
            // 
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "矢量几何校正";
            this.buttonItem2.Visible = false;
            // 
            // buttonItem5
            // 
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Text = "栅格几何校正";
            this.buttonItem5.Visible = false;
            // 
            // btnNEDtoENU
            // 
            this.btnNEDtoENU.Name = "btnNEDtoENU";
            this.btnNEDtoENU.Text = "北东地<->东北天(栅格)";
            this.btnNEDtoENU.Visible = false;
            this.btnNEDtoENU.Click += new System.EventHandler(this.btnNEDtoENU_Click);
            // 
            // btnNEDtoENUShape
            // 
            this.btnNEDtoENUShape.Name = "btnNEDtoENUShape";
            this.btnNEDtoENUShape.Text = "北东地<->东北天(矢量)";
            this.btnNEDtoENUShape.Click += new System.EventHandler(this.btnNEDtoENUShape_Click);
            // 
            // btntoolmatrix
            // 
            this.btntoolmatrix.Name = "btntoolmatrix";
            this.btntoolmatrix.Text = "通用变换";
            this.btntoolmatrix.Click += new System.EventHandler(this.btntoolmatrix_Click);
            // 
            // menuProcess
            // 
            this.menuProcess.Name = "menuProcess";
            this.menuProcess.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btn_surfaceop,
            this.btnprofile,
            this.buttonItemCreateCenterline,
            this.menuBtnVisibility,
            this.btnMenuAmizuth,
            this.btnProBackWard,
            this.btnSkyLinePara,
            this.btnJSCamLabel});
            this.menuProcess.Text = "中线分析";
            // 
            // btn_surfaceop
            // 
            this.btn_surfaceop.Name = "btn_surfaceop";
            this.btn_surfaceop.Text = "中线检查";
            this.btn_surfaceop.Click += new System.EventHandler(this.btn_surfaceop_Click);
            // 
            // btnprofile
            // 
            this.btnprofile.Name = "btnprofile";
            this.btnprofile.Text = "测量点位检查";
            this.btnprofile.Click += new System.EventHandler(this.btnprofile_Click);
            // 
            // buttonItemCreateCenterline
            // 
            this.buttonItemCreateCenterline.Name = "buttonItemCreateCenterline";
            this.buttonItemCreateCenterline.Text = "控制点生成中线";
            this.buttonItemCreateCenterline.Click += new System.EventHandler(this.buttonItemCreateCenterline_Click);
            // 
            // menuBtnVisibility
            // 
            this.menuBtnVisibility.Name = "menuBtnVisibility";
            this.menuBtnVisibility.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem36,
            this.buttonItem37,
            this.buttonItem38,
            this.buttonItem39});
            this.menuBtnVisibility.Text = "数据统计";
            this.menuBtnVisibility.Visible = false;
            this.menuBtnVisibility.Click += new System.EventHandler(this.menuBtnVisibility_Click);
            // 
            // buttonItem36
            // 
            this.buttonItem36.Name = "buttonItem36";
            this.buttonItem36.Text = "管道测量长度统计";
            // 
            // buttonItem37
            // 
            this.buttonItem37.Name = "buttonItem37";
            this.buttonItem37.Text = "三桩间距、偏移量统计";
            // 
            // buttonItem38
            // 
            this.buttonItem38.Name = "buttonItem38";
            this.buttonItem38.Text = "三桩数量统计";
            // 
            // buttonItem39
            // 
            this.buttonItem39.Name = "buttonItem39";
            this.buttonItem39.Text = "测点数统计";
            // 
            // btnMenuAmizuth
            // 
            this.btnMenuAmizuth.BeginGroup = true;
            this.btnMenuAmizuth.Name = "btnMenuAmizuth";
            this.btnMenuAmizuth.Text = "高度角测量";
            this.btnMenuAmizuth.Visible = false;
            this.btnMenuAmizuth.Click += new System.EventHandler(this.btnMenuAmizuth_Click);
            // 
            // btnProBackWard
            // 
            this.btnProBackWard.Name = "btnProBackWard";
            this.btnProBackWard.Text = "路径反投影";
            this.btnProBackWard.Visible = false;
            this.btnProBackWard.Click += new System.EventHandler(this.btnProBackWard_Click);
            // 
            // btnSkyLinePara
            // 
            this.btnSkyLinePara.Name = "btnSkyLinePara";
            this.btnSkyLinePara.Text = "天际线分析";
            this.btnSkyLinePara.Visible = false;
            this.btnSkyLinePara.Click += new System.EventHandler(this.btnSkyLinePara_Click);
            // 
            // btnJSCamLabel
            // 
            this.btnJSCamLabel.Name = "btnJSCamLabel";
            this.btnJSCamLabel.Text = "监视相机标注";
            this.btnJSCamLabel.Visible = false;
            this.btnJSCamLabel.Click += new System.EventHandler(this.btnJSCamLabel_Click);
            // 
            // buttonItem19
            // 
            this.buttonItem19.Name = "buttonItem19";
            this.buttonItem19.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem20,
            this.buttonItem21,
            this.buttonItem22,
            this.buttonItem30,
            this.buttonItem31,
            this.buttonItem32,
            this.buttonItem33,
            this.buttonItem34,
            this.buttonItem35});
            this.buttonItem19.Text = "_数据对齐与分析";
            // 
            // buttonItem20
            // 
            this.buttonItem20.Name = "buttonItem20";
            this.buttonItem20.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem40,
            this.buttonItem41});
            this.buttonItem20.Text = "内检测数据检查";
            this.buttonItem20.Tooltip = "添加文本";
            // 
            // buttonItem40
            // 
            this.buttonItem40.Name = "buttonItem40";
            this.buttonItem40.Text = "管节长度分析";
            // 
            // buttonItem41
            // 
            this.buttonItem41.Name = "buttonItem41";
            this.buttonItem41.Text = "内检测数据质量分析";
            // 
            // buttonItem21
            // 
            this.buttonItem21.Name = "buttonItem21";
            this.buttonItem21.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem42});
            this.buttonItem21.Text = "外检测数据检查";
            this.buttonItem21.Tooltip = "添加指北针";
            // 
            // buttonItem42
            // 
            this.buttonItem42.Name = "buttonItem42";
            this.buttonItem42.Text = "数据检查";
            // 
            // buttonItem22
            // 
            this.buttonItem22.Name = "buttonItem22";
            this.buttonItem22.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem23,
            this.buttonItem24,
            this.buttonItemCenterlineInsideReport,
            this.buttonItem25,
            this.buttonItem26,
            this.buttonItem27,
            this.buttonItem28,
            this.buttonItem29,
            this.buttonItemWeldAlignToCenterline,
            this.buttonItemGeneratePDFReport,
            this.buttonItemExportToCAD});
            this.buttonItem22.Text = "数据对齐分析";
            this.buttonItem22.Tooltip = "添加图例";
            // 
            // buttonItem23
            // 
            this.buttonItem23.Name = "buttonItem23";
            this.buttonItem23.Text = "竣工测量与焊缝数据对比";
            // 
            // buttonItem24
            // 
            this.buttonItem24.Name = "buttonItem24";
            this.buttonItem24.Text = "内检测(惯性导航/IMU)数据与管道中线对齐";
            this.buttonItem24.Click += new System.EventHandler(this.buttonItem24_Click);
            // 
            // buttonItemCenterlineInsideReport
            // 
            this.buttonItemCenterlineInsideReport.Name = "buttonItemCenterlineInsideReport";
            this.buttonItemCenterlineInsideReport.Text = "中线_内检测对齐报告";
            this.buttonItemCenterlineInsideReport.Click += new System.EventHandler(this.buttonItemCenterlineInsideReport_Click);
            // 
            // buttonItem25
            // 
            this.buttonItem25.Name = "buttonItem25";
            this.buttonItem25.Text = "内检测(里程)数据与管道中线对齐";
            this.buttonItem25.Click += new System.EventHandler(this.buttonItem25_Click);
            // 
            // buttonItem26
            // 
            this.buttonItem26.Name = "buttonItem26";
            this.buttonItem26.Text = "内检测数据（原始，按里程）间对齐分析";
            this.buttonItem26.Click += new System.EventHandler(this.buttonItem26_Click);
            // 
            // buttonItem27
            // 
            this.buttonItem27.Name = "buttonItem27";
            this.buttonItem27.Text = "外检测数据与管道中线对齐";
            this.buttonItem27.Click += new System.EventHandler(this.buttonItem27_Click);
            // 
            // buttonItem28
            // 
            this.buttonItem28.Name = "buttonItem28";
            this.buttonItem28.Text = "两次外检测结果对比";
            this.buttonItem28.Click += new System.EventHandler(this.buttonItem28_Click);
            // 
            // buttonItem29
            // 
            this.buttonItem29.Name = "buttonItem29";
            this.buttonItem29.Text = "两次内外检测结果对比";
            // 
            // buttonItemWeldAlignToCenterline
            // 
            this.buttonItemWeldAlignToCenterline.Name = "buttonItemWeldAlignToCenterline";
            this.buttonItemWeldAlignToCenterline.Text = "焊缝对齐到中线";
            this.buttonItemWeldAlignToCenterline.Click += new System.EventHandler(this.buttonItemWeldAlignToCenterline_Click);
            // 
            // buttonItemGeneratePDFReport
            // 
            this.buttonItemGeneratePDFReport.Name = "buttonItemGeneratePDFReport";
            this.buttonItemGeneratePDFReport.Text = "生成 PDF";
            this.buttonItemGeneratePDFReport.Click += new System.EventHandler(this.buttonItemGeneratePDFReport_Click);
            // 
            // buttonItemExportToCAD
            // 
            this.buttonItemExportToCAD.Name = "buttonItemExportToCAD";
            this.buttonItemExportToCAD.Text = "导出 CAD";
            this.buttonItemExportToCAD.Click += new System.EventHandler(this.buttonItemExportToCAD_Click);
            // 
            // buttonItem30
            // 
            this.buttonItem30.Name = "buttonItem30";
            this.buttonItem30.Text = "地图网格";
            this.buttonItem30.Tooltip = "添加地图网格";
            this.buttonItem30.Visible = false;
            // 
            // buttonItem31
            // 
            this.buttonItem31.Name = "buttonItem31";
            this.buttonItem31.Text = "图形比例尺";
            this.buttonItem31.Tooltip = "添加图形比例尺";
            this.buttonItem31.Visible = false;
            // 
            // buttonItem32
            // 
            this.buttonItem32.Name = "buttonItem32";
            this.buttonItem32.Text = "文字比例尺";
            this.buttonItem32.Tooltip = "添加文字比例尺";
            this.buttonItem32.Visible = false;
            // 
            // buttonItem33
            // 
            this.buttonItem33.Name = "buttonItem33";
            this.buttonItem33.Text = "导出地图";
            this.buttonItem33.Visible = false;
            // 
            // buttonItem34
            // 
            this.buttonItem34.Name = "buttonItem34";
            this.buttonItem34.Text = "打印地图";
            this.buttonItem34.Visible = false;
            // 
            // buttonItem35
            // 
            this.buttonItem35.Name = "buttonItem35";
            this.buttonItem35.Text = "着陆点定位图";
            this.buttonItem35.Visible = false;
            // 
            // menuMapping
            // 
            this.menuMapping.Name = "menuMapping";
            this.menuMapping.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAddText,
            this.btnAddNorthArrow,
            this.btnAddLegend,
            this.btnAddGrid,
            this.btnAddScallBar,
            this.btnAddScallText,
            this.btnExportMap,
            this.btnPrintMap,
            this.btnLandExpMap});
            this.menuMapping.Text = "数据对齐与分析_";
            this.menuMapping.Visible = false;
            // 
            // btnAddText
            // 
            this.btnAddText.Name = "btnAddText";
            this.btnAddText.Text = "内检测数据检查";
            this.btnAddText.Tooltip = "添加文本";
            this.btnAddText.Click += new System.EventHandler(this.btnAddText_Click);
            // 
            // btnAddNorthArrow
            // 
            this.btnAddNorthArrow.Name = "btnAddNorthArrow";
            this.btnAddNorthArrow.Text = "外检测数据检查";
            this.btnAddNorthArrow.Tooltip = "添加指北针";
            this.btnAddNorthArrow.Click += new System.EventHandler(this.btnAddNorthArrow_Click);
            // 
            // btnAddLegend
            // 
            this.btnAddLegend.Name = "btnAddLegend";
            this.btnAddLegend.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem9,
            this.buttonItem10,
            this.buttonItem11,
            this.buttonItem12,
            this.buttonItem13,
            this.buttonItem17,
            this.buttonItem18});
            this.btnAddLegend.Text = "数据对齐分析";
            this.btnAddLegend.Tooltip = "添加图例";
            this.btnAddLegend.Click += new System.EventHandler(this.btnAddLegend_Click);
            // 
            // buttonItem9
            // 
            this.buttonItem9.Name = "buttonItem9";
            this.buttonItem9.Text = "竣工测量与焊缝数据对比";
            // 
            // buttonItem10
            // 
            this.buttonItem10.Name = "buttonItem10";
            this.buttonItem10.Text = "内检测(惯性导航/IMU)数据与管道中线对齐";
            // 
            // buttonItem11
            // 
            this.buttonItem11.Name = "buttonItem11";
            this.buttonItem11.Text = "内检测(里程)数据与管道中线对齐";
            // 
            // buttonItem12
            // 
            this.buttonItem12.Name = "buttonItem12";
            this.buttonItem12.Text = "内检测数据（原始，按里程）间对齐分析";
            // 
            // buttonItem13
            // 
            this.buttonItem13.Name = "buttonItem13";
            this.buttonItem13.Text = "外检测数据与管道中线对齐";
            this.buttonItem13.Click += new System.EventHandler(this.buttonItem13_Click);
            // 
            // buttonItem17
            // 
            this.buttonItem17.Name = "buttonItem17";
            this.buttonItem17.Text = "两次外检测结果对比";
            // 
            // buttonItem18
            // 
            this.buttonItem18.Name = "buttonItem18";
            this.buttonItem18.Text = "两次内外检测结果对比";
            this.buttonItem18.Click += new System.EventHandler(this.buttonItem18_Click);
            // 
            // btnAddGrid
            // 
            this.btnAddGrid.Name = "btnAddGrid";
            this.btnAddGrid.Text = "地图网格";
            this.btnAddGrid.Tooltip = "添加地图网格";
            this.btnAddGrid.Visible = false;
            this.btnAddGrid.Click += new System.EventHandler(this.btnAddGrid_Click);
            // 
            // btnAddScallBar
            // 
            this.btnAddScallBar.Name = "btnAddScallBar";
            this.btnAddScallBar.Text = "图形比例尺";
            this.btnAddScallBar.Tooltip = "添加图形比例尺";
            this.btnAddScallBar.Visible = false;
            this.btnAddScallBar.Click += new System.EventHandler(this.btnAddScallBar_Click);
            // 
            // btnAddScallText
            // 
            this.btnAddScallText.Name = "btnAddScallText";
            this.btnAddScallText.Text = "文字比例尺";
            this.btnAddScallText.Tooltip = "添加文字比例尺";
            this.btnAddScallText.Visible = false;
            this.btnAddScallText.Click += new System.EventHandler(this.btnAddScallText_Click);
            // 
            // btnExportMap
            // 
            this.btnExportMap.Name = "btnExportMap";
            this.btnExportMap.Text = "导出地图";
            this.btnExportMap.Visible = false;
            this.btnExportMap.Click += new System.EventHandler(this.btnExportMap_Click);
            // 
            // btnPrintMap
            // 
            this.btnPrintMap.Name = "btnPrintMap";
            this.btnPrintMap.Text = "打印地图";
            this.btnPrintMap.Visible = false;
            this.btnPrintMap.Click += new System.EventHandler(this.btnPrintMap_Click);
            // 
            // btnLandExpMap
            // 
            this.btnLandExpMap.Name = "btnLandExpMap";
            this.btnLandExpMap.Text = "着陆点定位图";
            this.btnLandExpMap.Visible = false;
            this.btnLandExpMap.Click += new System.EventHandler(this.btnLandExpMap_Click);
            // 
            // menu3DAnalyst
            // 
            this.menu3DAnalyst.Name = "menu3DAnalyst";
            this.menu3DAnalyst.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem4,
            this.btnillum,
            this.buttonItemMerge});
            this.menu3DAnalyst.Text = "成果输出";
            this.menu3DAnalyst.Visible = false;
            // 
            // buttonItem4
            // 
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnvector,
            this.menuToolVector,
            this.buttonItem43});
            this.buttonItem4.Text = "成果输出";
            // 
            // btnvector
            // 
            this.btnvector.Name = "btnvector";
            this.btnvector.Text = "对齐成果数据库输出";
            this.btnvector.Click += new System.EventHandler(this.btnvector_Click);
            // 
            // menuToolVector
            // 
            this.menuToolVector.Name = "menuToolVector";
            this.menuToolVector.Text = "管道工程图输出";
            this.menuToolVector.Click += new System.EventHandler(this.menuToolVector_Click);
            // 
            // buttonItem43
            // 
            this.buttonItem43.Name = "buttonItem43";
            this.buttonItem43.Text = "原始数据输出";
            // 
            // btnillum
            // 
            this.btnillum.Name = "btnillum";
            this.btnillum.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem44,
            this.buttonItem45,
            this.buttonItem46,
            this.buttonItem47,
            this.buttonItem48,
            this.buttonItem49,
            this.buttonItem50,
            this.buttonItem51,
            this.buttonItem52,
            this.buttonItem53});
            this.btnillum.Text = "报告输出";
            this.btnillum.Click += new System.EventHandler(this.btnillum_Click);
            // 
            // buttonItem44
            // 
            this.buttonItem44.Name = "buttonItem44";
            this.buttonItem44.Text = "单次中线数据分析报告";
            // 
            // buttonItem45
            // 
            this.buttonItem45.Name = "buttonItem45";
            this.buttonItem45.Text = "单次内检测数据分析报告";
            // 
            // buttonItem46
            // 
            this.buttonItem46.Name = "buttonItem46";
            this.buttonItem46.Text = "单次外检测报告";
            // 
            // buttonItem47
            // 
            this.buttonItem47.Name = "buttonItem47";
            this.buttonItem47.Text = "竣工焊缝数据与中线对齐报告";
            // 
            // buttonItem48
            // 
            this.buttonItem48.Name = "buttonItem48";
            this.buttonItem48.Text = "内检测（IMU）与管道中线的对齐融合报告";
            // 
            // buttonItem49
            // 
            this.buttonItem49.Name = "buttonItem49";
            this.buttonItem49.Text = "内检测（里程）与管道中线的对齐融合报告";
            // 
            // buttonItem50
            // 
            this.buttonItem50.Name = "buttonItem50";
            this.buttonItem50.Text = "两次内检测对比报告";
            // 
            // buttonItem51
            // 
            this.buttonItem51.Name = "buttonItem51";
            this.buttonItem51.Text = "外检测与中线的融合报告";
            // 
            // buttonItem52
            // 
            this.buttonItem52.Name = "buttonItem52";
            this.buttonItem52.Text = "两次外检测结果对比报告";
            // 
            // buttonItem53
            // 
            this.buttonItem53.Name = "buttonItem53";
            this.buttonItem53.Text = "内外检测数据对比报告";
            // 
            // buttonItemMerge
            // 
            this.buttonItemMerge.Name = "buttonItemMerge";
            this.buttonItemMerge.Text = "地形融合";
            this.buttonItemMerge.Visible = false;
            this.buttonItemMerge.Click += new System.EventHandler(this.buttonItemMerge_Click);
            // 
            // barContexMenuEditor
            // 
            this.barContexMenuEditor.AccessibleDescription = "DotNetBar Bar (barContexMenuEditor)";
            this.barContexMenuEditor.AccessibleName = "DotNetBar Bar";
            this.barContexMenuEditor.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barContexMenuEditor.Controls.Add(this.axToolbarCtlMenuEditor);
            this.barContexMenuEditor.DockLine = 1;
            this.barContexMenuEditor.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barContexMenuEditor.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.barContexMenuEditor.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barContexMenuEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItem20});
            this.barContexMenuEditor.Location = new System.Drawing.Point(0, 27);
            this.barContexMenuEditor.Name = "barContexMenuEditor";
            this.barContexMenuEditor.Size = new System.Drawing.Size(1238, 33);
            this.barContexMenuEditor.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barContexMenuEditor.TabIndex = 12;
            this.barContexMenuEditor.TabStop = false;
            this.barContexMenuEditor.Text = "bar2";
            this.barContexMenuEditor.Visible = false;
            // 
            // axToolbarCtlMenuEditor
            // 
            this.axToolbarCtlMenuEditor.Location = new System.Drawing.Point(10, 2);
            this.axToolbarCtlMenuEditor.Name = "axToolbarCtlMenuEditor";
            this.axToolbarCtlMenuEditor.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarCtlMenuEditor.OcxState")));
            this.axToolbarCtlMenuEditor.Size = new System.Drawing.Size(1225, 28);
            this.axToolbarCtlMenuEditor.TabIndex = 0;
            // 
            // controlContainerItem20
            // 
            this.controlContainerItem20.AllowItemResize = false;
            this.controlContainerItem20.Control = this.axToolbarCtlMenuEditor;
            this.controlContainerItem20.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem20.Name = "controlContainerItem20";
            // 
            // barGeoAdjust
            // 
            this.barGeoAdjust.AccessibleDescription = "DotNetBar Bar (barGeoAdjust)";
            this.barGeoAdjust.AccessibleName = "DotNetBar Bar";
            this.barGeoAdjust.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barGeoAdjust.CanDockLeft = false;
            this.barGeoAdjust.CanDockRight = false;
            this.barGeoAdjust.CanHide = true;
            this.barGeoAdjust.Controls.Add(this.cmbAdjustMethod);
            this.barGeoAdjust.Controls.Add(this.axToolbarControlSpatialAdjust);
            this.barGeoAdjust.DockLine = 2;
            this.barGeoAdjust.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barGeoAdjust.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.barGeoAdjust.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barGeoAdjust.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItem14,
            this.controlContainerItem13});
            this.barGeoAdjust.Location = new System.Drawing.Point(1195, 61);
            this.barGeoAdjust.Name = "barGeoAdjust";
            this.barGeoAdjust.Size = new System.Drawing.Size(0, 28);
            this.barGeoAdjust.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barGeoAdjust.TabIndex = 8;
            this.barGeoAdjust.TabStop = false;
            this.barGeoAdjust.Text = "矢量校正";
            this.barGeoAdjust.Visible = false;
            // 
            // cmbAdjustMethod
            // 
            this.cmbAdjustMethod.DisplayMember = "Text";
            this.cmbAdjustMethod.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAdjustMethod.FormattingEnabled = true;
            this.cmbAdjustMethod.ItemHeight = 17;
            this.cmbAdjustMethod.Items.AddRange(new object[] {
            this.cbiAffine,
            this.cbiProjective,
            this.cbiSimilarity});
            this.cmbAdjustMethod.Location = new System.Drawing.Point(10, 2);
            this.cmbAdjustMethod.Name = "cmbAdjustMethod";
            this.cmbAdjustMethod.Size = new System.Drawing.Size(121, 23);
            this.cmbAdjustMethod.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbAdjustMethod.TabIndex = 1;
            this.cmbAdjustMethod.SelectedIndexChanged += new System.EventHandler(this.cmbAdjustMethod_SelectedIndexChanged);
            this.cmbAdjustMethod.Enter += new System.EventHandler(this.cmbAdjustMethod_Enter);
            // 
            // cbiAffine
            // 
            this.cbiAffine.Text = "空间转换 - 仿射";
            // 
            // cbiProjective
            // 
            this.cbiProjective.Text = "空间转换 - 投影";
            // 
            // cbiSimilarity
            // 
            this.cbiSimilarity.Text = "空间转换 - 相似";
            // 
            // axToolbarControlSpatialAdjust
            // 
            this.axToolbarControlSpatialAdjust.Location = new System.Drawing.Point(135, 2);
            this.axToolbarControlSpatialAdjust.Name = "axToolbarControlSpatialAdjust";
            this.axToolbarControlSpatialAdjust.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControlSpatialAdjust.OcxState")));
            this.axToolbarControlSpatialAdjust.Size = new System.Drawing.Size(398, 28);
            this.axToolbarControlSpatialAdjust.TabIndex = 1;
            // 
            // controlContainerItem14
            // 
            this.controlContainerItem14.AllowItemResize = false;
            this.controlContainerItem14.Control = this.cmbAdjustMethod;
            this.controlContainerItem14.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem14.Name = "controlContainerItem14";
            // 
            // controlContainerItem13
            // 
            this.controlContainerItem13.AllowItemResize = false;
            this.controlContainerItem13.Control = this.axToolbarControlSpatialAdjust;
            this.controlContainerItem13.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem13.Name = "controlContainerItem13";
            // 
            // barCustom
            // 
            this.barCustom.AccessibleDescription = "DotNetBar Bar (barCustom)";
            this.barCustom.AccessibleName = "DotNetBar Bar";
            this.barCustom.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barCustom.CanHide = true;
            this.barCustom.Controls.Add(this.axToolbarControlCustom);
            this.barCustom.DockLine = 2;
            this.barCustom.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barCustom.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.barCustom.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barCustom.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItem19});
            this.barCustom.Location = new System.Drawing.Point(216, 61);
            this.barCustom.Name = "barCustom";
            this.barCustom.Size = new System.Drawing.Size(977, 33);
            this.barCustom.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barCustom.TabIndex = 11;
            this.barCustom.TabStop = false;
            this.barCustom.Text = "自定义工具栏";
            this.barCustom.Visible = false;
            // 
            // axToolbarControlCustom
            // 
            this.axToolbarControlCustom.Location = new System.Drawing.Point(10, 2);
            this.axToolbarControlCustom.Name = "axToolbarControlCustom";
            this.axToolbarControlCustom.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControlCustom.OcxState")));
            this.axToolbarControlCustom.Size = new System.Drawing.Size(964, 28);
            this.axToolbarControlCustom.TabIndex = 2;
            // 
            // controlContainerItem19
            // 
            this.controlContainerItem19.AllowItemResize = false;
            this.controlContainerItem19.Control = this.axToolbarControlCustom;
            this.controlContainerItem19.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem19.Name = "controlContainerItem19";
            // 
            // barTerrain
            // 
            this.barTerrain.AccessibleDescription = "DotNetBar Bar (barTerrain)";
            this.barTerrain.AccessibleName = "DotNetBar Bar";
            this.barTerrain.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barTerrain.CanHide = true;
            this.barTerrain.DockLine = 2;
            this.barTerrain.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barTerrain.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.barTerrain.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barTerrain.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnRandomTerrain,
            this.btnRandomTexture,
            this.btnRandomModel});
            this.barTerrain.Location = new System.Drawing.Point(0, 61);
            this.barTerrain.Name = "barTerrain";
            this.barTerrain.Size = new System.Drawing.Size(214, 27);
            this.barTerrain.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barTerrain.TabIndex = 6;
            this.barTerrain.TabStop = false;
            this.barTerrain.Text = "地形纹理生成";
            this.barTerrain.Visible = false;
            // 
            // btnRandomTerrain
            // 
            this.btnRandomTerrain.Name = "btnRandomTerrain";
            this.btnRandomTerrain.Text = "随机地形";
            this.btnRandomTerrain.Click += new System.EventHandler(this.btnRandomTerrain_Click);
            // 
            // btnRandomTexture
            // 
            this.btnRandomTexture.Name = "btnRandomTexture";
            this.btnRandomTexture.Text = "随机纹理";
            this.btnRandomTexture.Click += new System.EventHandler(this.btnRandomTexture_Click);
            // 
            // btnRandomModel
            // 
            this.btnRandomModel.Name = "btnRandomModel";
            this.btnRandomModel.Text = "随机加入模型";
            this.btnRandomModel.Click += new System.EventHandler(this.btnRandomModel_Click);
            // 
            // barCommon
            // 
            this.barCommon.AccessibleDescription = "DotNetBar Bar (barCommon)";
            this.barCommon.AccessibleName = "DotNetBar Bar";
            this.barCommon.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barCommon.CanHide = true;
            this.barCommon.Controls.Add(this.axToolbarControlCommon);
            this.barCommon.DockLine = 3;
            this.barCommon.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barCommon.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.barCommon.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barCommon.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItem3});
            this.barCommon.Location = new System.Drawing.Point(510, 95);
            this.barCommon.Name = "barCommon";
            this.barCommon.Size = new System.Drawing.Size(716, 33);
            this.barCommon.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barCommon.TabIndex = 1;
            this.barCommon.TabStop = false;
            this.barCommon.Text = "工具";
            // 
            // axToolbarControlCommon
            // 
            this.axToolbarControlCommon.Location = new System.Drawing.Point(10, 2);
            this.axToolbarControlCommon.Name = "axToolbarControlCommon";
            this.axToolbarControlCommon.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControlCommon.OcxState")));
            this.axToolbarControlCommon.Size = new System.Drawing.Size(703, 28);
            this.axToolbarControlCommon.TabIndex = 0;
            // 
            // controlContainerItem3
            // 
            this.controlContainerItem3.AllowItemResize = false;
            this.controlContainerItem3.Control = this.axToolbarControlCommon;
            this.controlContainerItem3.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem3.Name = "controlContainerItem3";
            // 
            // bar3D
            // 
            this.bar3D.AccessibleDescription = "DotNetBar Bar (bar3D)";
            this.bar3D.AccessibleName = "DotNetBar Bar";
            this.bar3D.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.bar3D.CanDockLeft = false;
            this.bar3D.CanDockRight = false;
            this.bar3D.CanHide = true;
            this.bar3D.Controls.Add(this.axToolbarControlScene);
            this.bar3D.DockLine = 3;
            this.bar3D.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.bar3D.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.bar3D.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.bar3D.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItem6});
            this.bar3D.Location = new System.Drawing.Point(0, 95);
            this.bar3D.Name = "bar3D";
            this.bar3D.Size = new System.Drawing.Size(508, 33);
            this.bar3D.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.bar3D.TabIndex = 4;
            this.bar3D.TabStop = false;
            this.bar3D.Text = "三维";
            // 
            // axToolbarControlScene
            // 
            this.axToolbarControlScene.Location = new System.Drawing.Point(10, 2);
            this.axToolbarControlScene.Name = "axToolbarControlScene";
            this.axToolbarControlScene.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControlScene.OcxState")));
            this.axToolbarControlScene.Size = new System.Drawing.Size(495, 28);
            this.axToolbarControlScene.TabIndex = 1;
            this.axToolbarControlScene.OnMouseDown += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnMouseDownEventHandler(this.axToolbarControlScene_OnMouseDown);
            this.axToolbarControlScene.OnItemClick += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnItemClickEventHandler(this.axToolbarControlScene_OnItemClick);
            // 
            // controlContainerItem6
            // 
            this.controlContainerItem6.AllowItemResize = false;
            this.controlContainerItem6.Control = this.axToolbarControlScene;
            this.controlContainerItem6.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem6.Name = "controlContainerItem6";
            // 
            // barSunAlt
            // 
            this.barSunAlt.AccessibleDescription = "DotNetBar Bar (barSunAlt)";
            this.barSunAlt.AccessibleName = "DotNetBar Bar";
            this.barSunAlt.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barSunAlt.CanHide = true;
            this.barSunAlt.Controls.Add(this.cmbSunAltOriImg);
            this.barSunAlt.Controls.Add(this.txtSunAltXmlFile);
            this.barSunAlt.Controls.Add(this.btnSetXmlFile);
            this.barSunAlt.Controls.Add(this.axToolbarSunAltitude);
            this.barSunAlt.DockLine = 4;
            this.barSunAlt.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barSunAlt.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.barSunAlt.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barSunAlt.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItem16,
            this.controlContainerItem17,
            this.controlContainerItem18,
            this.controlContainerItem15});
            this.barSunAlt.Location = new System.Drawing.Point(0, 129);
            this.barSunAlt.Name = "barSunAlt";
            this.barSunAlt.Size = new System.Drawing.Size(358, 33);
            this.barSunAlt.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barSunAlt.TabIndex = 9;
            this.barSunAlt.TabStop = false;
            this.barSunAlt.Text = "地平线高度角测量";
            this.barSunAlt.Visible = false;
            // 
            // cmbSunAltOriImg
            // 
            this.cmbSunAltOriImg.DisplayMember = "Text";
            this.cmbSunAltOriImg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSunAltOriImg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSunAltOriImg.FormattingEnabled = true;
            this.cmbSunAltOriImg.ItemHeight = 17;
            this.cmbSunAltOriImg.Location = new System.Drawing.Point(10, 4);
            this.cmbSunAltOriImg.Name = "cmbSunAltOriImg";
            this.cmbSunAltOriImg.Size = new System.Drawing.Size(121, 23);
            this.cmbSunAltOriImg.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSunAltOriImg.TabIndex = 8;
            this.cmbSunAltOriImg.DropDown += new System.EventHandler(this.cmbSunAltOriImg_DropDown);
            this.cmbSunAltOriImg.SelectedIndexChanged += new System.EventHandler(this.cmbSunAltOriImg_SelectedIndexChanged);
            this.cmbSunAltOriImg.Enter += new System.EventHandler(this.cmbSunAltOriImg_Enter);
            // 
            // txtSunAltXmlFile
            // 
            this.txtSunAltXmlFile.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtSunAltXmlFile.Border.Class = "TextBoxBorder";
            this.txtSunAltXmlFile.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSunAltXmlFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSunAltXmlFile.Location = new System.Drawing.Point(135, 4);
            this.txtSunAltXmlFile.Name = "txtSunAltXmlFile";
            this.txtSunAltXmlFile.ReadOnly = true;
            this.txtSunAltXmlFile.Size = new System.Drawing.Size(100, 23);
            this.txtSunAltXmlFile.TabIndex = 8;
            this.txtSunAltXmlFile.TextChanged += new System.EventHandler(this.txtSunAltXmlFile_TextChanged);
            // 
            // btnSetXmlFile
            // 
            this.btnSetXmlFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSetXmlFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSetXmlFile.Location = new System.Drawing.Point(239, 3);
            this.btnSetXmlFile.Name = "btnSetXmlFile";
            this.btnSetXmlFile.Size = new System.Drawing.Size(29, 25);
            this.btnSetXmlFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSetXmlFile.TabIndex = 8;
            this.btnSetXmlFile.Text = "...";
            this.btnSetXmlFile.Tooltip = "设置XML文件";
            this.btnSetXmlFile.Click += new System.EventHandler(this.btnSetXmlFile_Click);
            // 
            // axToolbarSunAltitude
            // 
            this.axToolbarSunAltitude.Location = new System.Drawing.Point(272, 2);
            this.axToolbarSunAltitude.Name = "axToolbarSunAltitude";
            this.axToolbarSunAltitude.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarSunAltitude.OcxState")));
            this.axToolbarSunAltitude.Size = new System.Drawing.Size(83, 28);
            this.axToolbarSunAltitude.TabIndex = 8;
            // 
            // controlContainerItem16
            // 
            this.controlContainerItem16.AllowItemResize = false;
            this.controlContainerItem16.Control = this.cmbSunAltOriImg;
            this.controlContainerItem16.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem16.Name = "controlContainerItem16";
            // 
            // controlContainerItem17
            // 
            this.controlContainerItem17.AllowItemResize = false;
            this.controlContainerItem17.Control = this.txtSunAltXmlFile;
            this.controlContainerItem17.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem17.Name = "controlContainerItem17";
            // 
            // controlContainerItem18
            // 
            this.controlContainerItem18.AllowItemResize = false;
            this.controlContainerItem18.Control = this.btnSetXmlFile;
            this.controlContainerItem18.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem18.Name = "controlContainerItem18";
            // 
            // controlContainerItem15
            // 
            this.controlContainerItem15.AllowItemResize = false;
            this.controlContainerItem15.Control = this.axToolbarSunAltitude;
            this.controlContainerItem15.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem15.Name = "controlContainerItem15";
            // 
            // barGeoReference
            // 
            this.barGeoReference.AccessibleDescription = "DotNetBar Bar (barGeoReference)";
            this.barGeoReference.AccessibleName = "DotNetBar Bar";
            this.barGeoReference.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barGeoReference.CanHide = true;
            this.barGeoReference.Controls.Add(this.cmbTargetRasterLayer);
            this.barGeoReference.Controls.Add(this.axToolbarControlRectifyRaster);
            this.barGeoReference.DockLine = 4;
            this.barGeoReference.DockOffset = 369;
            this.barGeoReference.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barGeoReference.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.barGeoReference.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barGeoReference.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblTargetRasterLayer,
            this.controlContainerItem11,
            this.controlContainerItem12});
            this.barGeoReference.Location = new System.Drawing.Point(369, 129);
            this.barGeoReference.Name = "barGeoReference";
            this.barGeoReference.Size = new System.Drawing.Size(692, 33);
            this.barGeoReference.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barGeoReference.TabIndex = 7;
            this.barGeoReference.TabStop = false;
            this.barGeoReference.Text = "栅格处理";
            this.barGeoReference.Visible = false;
            // 
            // cmbTargetRasterLayer
            // 
            this.cmbTargetRasterLayer.DisplayMember = "Text";
            this.cmbTargetRasterLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTargetRasterLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetRasterLayer.FormattingEnabled = true;
            this.cmbTargetRasterLayer.ItemHeight = 17;
            this.cmbTargetRasterLayer.Location = new System.Drawing.Point(66, 4);
            this.cmbTargetRasterLayer.Name = "cmbTargetRasterLayer";
            this.cmbTargetRasterLayer.Size = new System.Drawing.Size(194, 23);
            this.cmbTargetRasterLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbTargetRasterLayer.TabIndex = 1;
            this.cmbTargetRasterLayer.SelectedIndexChanged += new System.EventHandler(this.cmbTargetRasterLayer_SelectedIndexChanged);
            this.cmbTargetRasterLayer.Click += new System.EventHandler(this.cmbTargetRasterLayer_Click);
            this.cmbTargetRasterLayer.Enter += new System.EventHandler(this.cmbTargetRasterLayer_Enter);
            // 
            // axToolbarControlRectifyRaster
            // 
            this.axToolbarControlRectifyRaster.Location = new System.Drawing.Point(264, 2);
            this.axToolbarControlRectifyRaster.Name = "axToolbarControlRectifyRaster";
            this.axToolbarControlRectifyRaster.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControlRectifyRaster.OcxState")));
            this.axToolbarControlRectifyRaster.Size = new System.Drawing.Size(425, 28);
            this.axToolbarControlRectifyRaster.TabIndex = 1;
            // 
            // lblTargetRasterLayer
            // 
            this.lblTargetRasterLayer.Name = "lblTargetRasterLayer";
            this.lblTargetRasterLayer.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem14});
            this.lblTargetRasterLayer.Text = "栅格图层";
            // 
            // buttonItem1
            // 
            this.buttonItem1.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem1.Image")));
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "buttonItem1";
            // 
            // buttonItem14
            // 
            this.buttonItem14.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem14.Image")));
            this.buttonItem14.Name = "buttonItem14";
            this.buttonItem14.Text = "buttonItem14";
            // 
            // controlContainerItem11
            // 
            this.controlContainerItem11.AllowItemResize = false;
            this.controlContainerItem11.Control = this.cmbTargetRasterLayer;
            this.controlContainerItem11.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem11.Name = "controlContainerItem11";
            // 
            // controlContainerItem12
            // 
            this.controlContainerItem12.AllowItemResize = false;
            this.controlContainerItem12.Control = this.axToolbarControlRectifyRaster;
            this.controlContainerItem12.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem12.Name = "controlContainerItem12";
            // 
            // barLayout
            // 
            this.barLayout.AccessibleDescription = "DotNetBar Bar (barLayout)";
            this.barLayout.AccessibleName = "DotNetBar Bar";
            this.barLayout.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barLayout.CanHide = true;
            this.barLayout.Controls.Add(this.axToolbarControlLayout);
            this.barLayout.DockLine = 5;
            this.barLayout.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barLayout.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.barLayout.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barLayout.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItem4});
            this.barLayout.Location = new System.Drawing.Point(0, 163);
            this.barLayout.Name = "barLayout";
            this.barLayout.Size = new System.Drawing.Size(1094, 33);
            this.barLayout.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barLayout.TabIndex = 2;
            this.barLayout.TabStop = false;
            this.barLayout.Text = "布局";
            // 
            // axToolbarControlLayout
            // 
            this.axToolbarControlLayout.Location = new System.Drawing.Point(10, 2);
            this.axToolbarControlLayout.Name = "axToolbarControlLayout";
            this.axToolbarControlLayout.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControlLayout.OcxState")));
            this.axToolbarControlLayout.Size = new System.Drawing.Size(1081, 28);
            this.axToolbarControlLayout.TabIndex = 1;
            this.axToolbarControlLayout.OnItemClick += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnItemClickEventHandler(this.axToolbarControlLayout_OnItemClick);
            // 
            // controlContainerItem4
            // 
            this.controlContainerItem4.AllowItemResize = false;
            this.controlContainerItem4.Control = this.axToolbarControlLayout;
            this.controlContainerItem4.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem4.Name = "controlContainerItem4";
            // 
            // barEditor
            // 
            this.barEditor.AccessibleDescription = "DotNetBar Bar (barEditor)";
            this.barEditor.AccessibleName = "DotNetBar Bar";
            this.barEditor.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barEditor.CanHide = true;
            this.barEditor.Controls.Add(this.axToolbarControlEdit);
            this.barEditor.DockLine = 6;
            this.barEditor.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barEditor.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.barEditor.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItem5});
            this.barEditor.Location = new System.Drawing.Point(733, 197);
            this.barEditor.Name = "barEditor";
            this.barEditor.Size = new System.Drawing.Size(833, 33);
            this.barEditor.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barEditor.TabIndex = 3;
            this.barEditor.TabStop = false;
            this.barEditor.Text = "编辑";
            this.barEditor.Visible = false;
            // 
            // axToolbarControlEdit
            // 
            this.axToolbarControlEdit.Location = new System.Drawing.Point(10, 2);
            this.axToolbarControlEdit.Name = "axToolbarControlEdit";
            this.axToolbarControlEdit.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControlEdit.OcxState")));
            this.axToolbarControlEdit.Size = new System.Drawing.Size(820, 28);
            this.axToolbarControlEdit.TabIndex = 1;
            // 
            // controlContainerItem5
            // 
            this.controlContainerItem5.AllowItemResize = false;
            this.controlContainerItem5.Control = this.axToolbarControlEdit;
            this.controlContainerItem5.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem5.Name = "controlContainerItem5";
            // 
            // barEffects
            // 
            this.barEffects.AccessibleDescription = "DotNetBar Bar (barEffects)";
            this.barEffects.AccessibleName = "DotNetBar Bar";
            this.barEffects.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barEffects.CanHide = true;
            this.barEffects.Controls.Add(this.cmBoxEffectsLayer);
            this.barEffects.DockLine = 6;
            this.barEffects.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barEffects.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.barEffects.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barEffects.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItem23,
            this.sliderItemTransparency,
            this.sliderItemContrast,
            this.sliderItemBrightness});
            this.barEffects.Location = new System.Drawing.Point(0, 197);
            this.barEffects.Name = "barEffects";
            this.barEffects.Size = new System.Drawing.Size(731, 28);
            this.barEffects.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barEffects.TabIndex = 10;
            this.barEffects.TabStop = false;
            this.barEffects.Text = "图层效果设置";
            this.barEffects.Visible = false;
            // 
            // cmBoxEffectsLayer
            // 
            this.cmBoxEffectsLayer.DisplayMember = "Text";
            this.cmBoxEffectsLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmBoxEffectsLayer.FormattingEnabled = true;
            this.cmBoxEffectsLayer.ItemHeight = 17;
            this.cmBoxEffectsLayer.Location = new System.Drawing.Point(10, 2);
            this.cmBoxEffectsLayer.Name = "cmBoxEffectsLayer";
            this.cmBoxEffectsLayer.Size = new System.Drawing.Size(196, 23);
            this.cmBoxEffectsLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmBoxEffectsLayer.TabIndex = 11;
            this.cmBoxEffectsLayer.SelectedIndexChanged += new System.EventHandler(this.cmBoxEffectsLayer_SelectedIndexChanged);
            this.cmBoxEffectsLayer.Click += new System.EventHandler(this.cmBoxEffectsLayer_Click);
            this.cmBoxEffectsLayer.Enter += new System.EventHandler(this.cmBoxEffectsLayer_Enter);
            // 
            // controlContainerItem23
            // 
            this.controlContainerItem23.AllowItemResize = false;
            this.controlContainerItem23.Control = this.cmBoxEffectsLayer;
            this.controlContainerItem23.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem23.Name = "controlContainerItem23";
            // 
            // sliderItemTransparency
            // 
            this.sliderItemTransparency.Enabled = false;
            this.sliderItemTransparency.Name = "sliderItemTransparency";
            this.sliderItemTransparency.Text = "透明度";
            this.sliderItemTransparency.Value = 0;
            this.sliderItemTransparency.ValueChanged += new System.EventHandler(this.sliderItemTransparency_ValueChanged);
            // 
            // sliderItemContrast
            // 
            this.sliderItemContrast.Enabled = false;
            this.sliderItemContrast.Minimum = -100;
            this.sliderItemContrast.Name = "sliderItemContrast";
            this.sliderItemContrast.Text = "对比度";
            this.sliderItemContrast.Value = 0;
            this.sliderItemContrast.ValueChanged += new System.EventHandler(this.sliderItemContrast_ValueChanged);
            // 
            // sliderItemBrightness
            // 
            this.sliderItemBrightness.Enabled = false;
            this.sliderItemBrightness.Minimum = -100;
            this.sliderItemBrightness.Name = "sliderItemBrightness";
            this.sliderItemBrightness.Text = "亮度";
            this.sliderItemBrightness.Value = 0;
            this.sliderItemBrightness.ValueChanged += new System.EventHandler(this.sliderItemBrightness_ValueChanged);
            // 
            // barTinEditor
            // 
            this.barTinEditor.AccessibleDescription = "DotNetBar Bar (barTinEditor)";
            this.barTinEditor.AccessibleName = "DotNetBar Bar";
            this.barTinEditor.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barTinEditor.CanHide = true;
            this.barTinEditor.Controls.Add(this.axToolbarControlTinEdit);
            this.barTinEditor.Controls.Add(this.cmbTargetTinLayer);
            this.barTinEditor.Controls.Add(this.comboBoxExCrater);
            this.barTinEditor.Controls.Add(this.comboBoxExNonCrater);
            this.barTinEditor.DockLine = 7;
            this.barTinEditor.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barTinEditor.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.barTinEditor.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barTinEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemAddTin,
            this.controlContainerItem7,
            this.lblTinTargetLayer,
            this.controlContainerItem8,
            this.lblCrater,
            this.controlContainerItem9,
            this.lblNonCrater,
            this.controlContainerItem10});
            this.barTinEditor.Location = new System.Drawing.Point(0, 231);
            this.barTinEditor.Name = "barTinEditor";
            this.barTinEditor.Size = new System.Drawing.Size(991, 33);
            this.barTinEditor.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.barTinEditor.TabIndex = 5;
            this.barTinEditor.TabStop = false;
            this.barTinEditor.Text = "Tin编辑";
            this.barTinEditor.Visible = false;
            // 
            // axToolbarControlTinEdit
            // 
            this.axToolbarControlTinEdit.Location = new System.Drawing.Point(63, 2);
            this.axToolbarControlTinEdit.Name = "axToolbarControlTinEdit";
            this.axToolbarControlTinEdit.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControlTinEdit.OcxState")));
            this.axToolbarControlTinEdit.Size = new System.Drawing.Size(352, 28);
            this.axToolbarControlTinEdit.TabIndex = 1;
            this.axToolbarControlTinEdit.OnMouseDown += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnMouseDownEventHandler(this.axToolbarControlTinEdit_OnMouseDown);
            // 
            // cmbTargetTinLayer
            // 
            this.cmbTargetTinLayer.DisplayMember = "Text";
            this.cmbTargetTinLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTargetTinLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetTinLayer.FormattingEnabled = true;
            this.cmbTargetTinLayer.ItemHeight = 17;
            this.cmbTargetTinLayer.Location = new System.Drawing.Point(493, 4);
            this.cmbTargetTinLayer.Name = "cmbTargetTinLayer";
            this.cmbTargetTinLayer.Size = new System.Drawing.Size(121, 23);
            this.cmbTargetTinLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbTargetTinLayer.TabIndex = 1;
            this.cmbTargetTinLayer.SelectedIndexChanged += new System.EventHandler(this.cmbTargetTinLayer_SelectedIndexChanged);
            this.cmbTargetTinLayer.Enter += new System.EventHandler(this.cmbTargetTinLayer_Enter);
            // 
            // comboBoxExCrater
            // 
            this.comboBoxExCrater.DisplayMember = "Text";
            this.comboBoxExCrater.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExCrater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExCrater.FormattingEnabled = true;
            this.comboBoxExCrater.ItemHeight = 17;
            this.comboBoxExCrater.Location = new System.Drawing.Point(686, 4);
            this.comboBoxExCrater.Name = "comboBoxExCrater";
            this.comboBoxExCrater.Size = new System.Drawing.Size(121, 23);
            this.comboBoxExCrater.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExCrater.TabIndex = 15;
            this.comboBoxExCrater.SelectedIndexChanged += new System.EventHandler(this.comboBoxExCrater_SelectedIndexChanged);
            // 
            // comboBoxExNonCrater
            // 
            this.comboBoxExNonCrater.DisplayMember = "Text";
            this.comboBoxExNonCrater.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExNonCrater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExNonCrater.FormattingEnabled = true;
            this.comboBoxExNonCrater.ItemHeight = 17;
            this.comboBoxExNonCrater.Location = new System.Drawing.Point(867, 4);
            this.comboBoxExNonCrater.Name = "comboBoxExNonCrater";
            this.comboBoxExNonCrater.Size = new System.Drawing.Size(121, 23);
            this.comboBoxExNonCrater.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExNonCrater.TabIndex = 15;
            this.comboBoxExNonCrater.SelectedIndexChanged += new System.EventHandler(this.comboBoxExNonCrater_SelectedIndexChanged);
            this.comboBoxExNonCrater.TextChanged += new System.EventHandler(this.comboBoxExNonCrater_TextChanged);
            // 
            // buttonItemAddTin
            // 
            this.buttonItemAddTin.Name = "buttonItemAddTin";
            this.buttonItemAddTin.Text = "添加Tin";
            this.buttonItemAddTin.Click += new System.EventHandler(this.buttonItemAddTin_Click);
            // 
            // controlContainerItem7
            // 
            this.controlContainerItem7.AllowItemResize = false;
            this.controlContainerItem7.Control = this.axToolbarControlTinEdit;
            this.controlContainerItem7.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem7.Name = "controlContainerItem7";
            // 
            // lblTinTargetLayer
            // 
            this.lblTinTargetLayer.Name = "lblTinTargetLayer";
            this.lblTinTargetLayer.Text = "Tin目标图层";
            // 
            // controlContainerItem8
            // 
            this.controlContainerItem8.AllowItemResize = false;
            this.controlContainerItem8.Control = this.cmbTargetTinLayer;
            this.controlContainerItem8.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem8.Name = "controlContainerItem8";
            // 
            // lblCrater
            // 
            this.lblCrater.Name = "lblCrater";
            this.lblCrater.Text = "撞击坑图层";
            // 
            // controlContainerItem9
            // 
            this.controlContainerItem9.AllowItemResize = false;
            this.controlContainerItem9.Control = this.comboBoxExCrater;
            this.controlContainerItem9.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem9.Name = "controlContainerItem9";
            // 
            // lblNonCrater
            // 
            this.lblNonCrater.Name = "lblNonCrater";
            this.lblNonCrater.Text = "障碍图层";
            // 
            // controlContainerItem10
            // 
            this.controlContainerItem10.AllowItemResize = false;
            this.controlContainerItem10.Control = this.comboBoxExNonCrater;
            this.controlContainerItem10.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem10.Name = "controlContainerItem10";
            // 
            // dockSite3
            // 
            this.dockSite3.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite3.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite3.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite3.Location = new System.Drawing.Point(0, 264);
            this.dockSite3.Name = "dockSite3";
            this.dockSite3.Size = new System.Drawing.Size(1145, 0);
            this.dockSite3.TabIndex = 2;
            this.dockSite3.TabStop = false;
            // 
            // txtExistingModel
            // 
            this.txtExistingModel.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtExistingModel.Border.Class = "TextBoxBorder";
            this.txtExistingModel.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtExistingModel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtExistingModel.Location = new System.Drawing.Point(21, 21);
            this.txtExistingModel.Name = "txtExistingModel";
            this.txtExistingModel.Size = new System.Drawing.Size(100, 21);
            this.txtExistingModel.TabIndex = 2;
            // 
            // buttonItemSelect
            // 
            this.buttonItemSelect.Name = "buttonItemSelect";
            this.buttonItemSelect.Text = "图层可选";
            this.buttonItemSelect.Click += new System.EventHandler(this.buttonItemSelect_Click);
            // 
            // buttonItemClearSelection
            // 
            this.buttonItemClearSelection.Name = "buttonItemClearSelection";
            this.buttonItemClearSelection.Text = "图层不可选";
            this.buttonItemClearSelection.Click += new System.EventHandler(this.buttonItemClearSelection_Click);
            // 
            // buttonItemVisible
            // 
            this.buttonItemVisible.Name = "buttonItemVisible";
            this.buttonItemVisible.Text = "三维可见";
            this.buttonItemVisible.Click += new System.EventHandler(this.buttonItemVisible_Click);
            // 
            // buttonItemUnVisible
            // 
            this.buttonItemUnVisible.Name = "buttonItemUnVisible";
            this.buttonItemUnVisible.Text = "三维不可见";
            this.buttonItemUnVisible.Click += new System.EventHandler(this.buttonItemUnVisible_Click);
            // 
            // panelDockContainer4
            // 
            this.panelDockContainer4.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelDockContainer4.Location = new System.Drawing.Point(3, 23);
            this.panelDockContainer4.Name = "panelDockContainer4";
            this.panelDockContainer4.Size = new System.Drawing.Size(623, 70);
            this.panelDockContainer4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer4.Style.GradientAngle = 90;
            this.panelDockContainer4.TabIndex = 0;
            // 
            // panelDockContainer3
            // 
            this.panelDockContainer3.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelDockContainer3.Location = new System.Drawing.Point(3, 23);
            this.panelDockContainer3.Name = "panelDockContainer3";
            this.panelDockContainer3.Size = new System.Drawing.Size(623, 70);
            this.panelDockContainer3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer3.Style.GradientAngle = 90;
            this.panelDockContainer3.TabIndex = 2;
            // 
            // buttonItem15
            // 
            this.buttonItem15.Name = "buttonItem15";
            this.buttonItem15.Text = "buttonItem15";
            // 
            // buttonItem16
            // 
            this.buttonItem16.Name = "buttonItem16";
            this.buttonItem16.Text = "buttonItem16";
            // 
            // controlContainerItem2
            // 
            this.controlContainerItem2.AllowItemResize = false;
            this.controlContainerItem2.Control = this.axToolbarControl2;
            this.controlContainerItem2.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem2.Name = "controlContainerItem2";
            // 
            // axToolbarControl2
            // 
            this.axToolbarControl2.Location = new System.Drawing.Point(203, 3);
            this.axToolbarControl2.Name = "axToolbarControl2";
            this.axToolbarControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl2.OcxState")));
            this.axToolbarControl2.Size = new System.Drawing.Size(179, 28);
            this.axToolbarControl2.TabIndex = 11;
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = false;
            this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            // 
            // styleManagerMain
            // 
            this.styleManagerMain.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
            this.styleManagerMain.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // dockSite9
            // 
            this.dockSite9.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite9.Location = new System.Drawing.Point(0, 0);
            this.dockSite9.Name = "dockSite9";
            this.dockSite9.Size = new System.Drawing.Size(0, 0);
            this.dockSite9.TabIndex = 0;
            this.dockSite9.TabStop = false;
            // 
            // dockContainerItem4
            // 
            this.dockContainerItem4.Control = this.panelDockContainer4;
            this.dockContainerItem4.Name = "dockContainerItem4";
            this.dockContainerItem4.Text = "dockContainerItem4";
            // 
            // dockContainerItem3
            // 
            this.dockContainerItem3.Control = this.panelDockContainer3;
            this.dockContainerItem3.Name = "dockContainerItem3";
            this.dockContainerItem3.Text = "dockContainerItem3";
            // 
            // barStatus
            // 
            this.barStatus.AntiAlias = true;
            this.barStatus.Controls.Add(this.progressBarXMain);
            this.barStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barStatus.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.barStatus.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblInfo});
            this.barStatus.Location = new System.Drawing.Point(0, 603);
            this.barStatus.Name = "barStatus";
            this.barStatus.Size = new System.Drawing.Size(1145, 19);
            this.barStatus.Stretch = true;
            this.barStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barStatus.TabIndex = 10;
            this.barStatus.TabStop = false;
            this.barStatus.Text = "bar1";
            // 
            // progressBarXMain
            // 
            // 
            // 
            // 
            this.progressBarXMain.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarXMain.Dock = System.Windows.Forms.DockStyle.Right;
            this.progressBarXMain.Location = new System.Drawing.Point(1000, 0);
            this.progressBarXMain.Name = "progressBarXMain";
            this.progressBarXMain.Size = new System.Drawing.Size(145, 19);
            this.progressBarXMain.TabIndex = 0;
            this.progressBarXMain.Text = "progressBarX1";
            // 
            // lblInfo
            // 
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Width = 400;
            // 
            // cmbiSubdivisionThree
            // 
            this.cmbiSubdivisionThree.Text = "3";
            // 
            // cmbiSubdivisionTwo
            // 
            this.cmbiSubdivisionTwo.Text = "2";
            // 
            // cmbiSubdivisionOne
            // 
            this.cmbiSubdivisionOne.Text = "1";
            // 
            // cmbiSubdivisionZero
            // 
            this.cmbiSubdivisionZero.Text = "0";
            // 
            // cmiAffine
            // 
            this.cmiAffine.Text = "空间转换 - 仿射变换";
            // 
            // cmiProjective
            // 
            this.cmiProjective.Text = "空间转换 - 透视变换";
            // 
            // cmiSimilarity
            // 
            this.cmiSimilarity.Text = "空间转换 - 相似变换";
            // 
            // cmiRubberSheet
            // 
            this.cmiRubberSheet.Text = "橡皮拉伸";
            // 
            // cmiEdgeSnap
            // 
            this.cmiEdgeSnap.Text = "边界匹配";
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 17;
            this.comboBoxEx1.Location = new System.Drawing.Point(95, 4);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(121, 23);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 1;
            // 
            // dockContainerItem1
            // 
            this.dockContainerItem1.Name = "dockContainerItem1";
            this.dockContainerItem1.Text = "dockContainerItem1";
            // 
            // textBoxItem1
            // 
            this.textBoxItem1.Name = "textBoxItem1";
            this.textBoxItem1.TextBoxWidth = 200;
            this.textBoxItem1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            // 
            // buttonItem6
            // 
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.Text = "浏览";
            // 
            // textBoxItem2
            // 
            this.textBoxItem2.Name = "textBoxItem2";
            this.textBoxItem2.TextBoxWidth = 200;
            this.textBoxItem2.WatermarkColor = System.Drawing.SystemColors.GrayText;
            // 
            // buttonItem7
            // 
            this.buttonItem7.Name = "buttonItem7";
            this.buttonItem7.Text = "浏览";
            // 
            // textBoxItem3
            // 
            this.textBoxItem3.Name = "textBoxItem3";
            this.textBoxItem3.TextBoxWidth = 200;
            this.textBoxItem3.WatermarkColor = System.Drawing.SystemColors.GrayText;
            // 
            // buttonItem8
            // 
            this.buttonItem8.Name = "buttonItem8";
            this.buttonItem8.Text = "浏览";
            // 
            // dockContainerItem5
            // 
            this.dockContainerItem5.Name = "dockContainerItem5";
            this.dockContainerItem5.Text = "dockContainerItem5";
            // 
            // axLicenseControl2
            // 
            this.axLicenseControl2.Enabled = true;
            this.axLicenseControl2.Location = new System.Drawing.Point(0, 0);
            this.axLicenseControl2.Name = "axLicenseControl2";
            this.axLicenseControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl2.OcxState")));
            this.axLicenseControl2.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl2.TabIndex = 13;
            // 
            // axToolbarControlEffects
            // 
            this.axToolbarControlEffects.Location = new System.Drawing.Point(214, 2);
            this.axToolbarControlEffects.Name = "axToolbarControlEffects";
            this.axToolbarControlEffects.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControlEffects.OcxState")));
            this.axToolbarControlEffects.Size = new System.Drawing.Size(227, 28);
            this.axToolbarControlEffects.TabIndex = 10000;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 622);
            this.Controls.Add(this.dockSite10);
            this.Controls.Add(this.dockSiteRight);
            this.Controls.Add(this.dockSiteBottom);
            this.Controls.Add(this.dockSiteLeft);
            this.Controls.Add(this.dockSite3);
            this.Controls.Add(this.dockSite5);
            this.Controls.Add(this.dockSite6);
            this.Controls.Add(this.dockSite7);
            this.Controls.Add(this.dockSite8);
            this.Controls.Add(this.barStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "数据对齐软件系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyUp);
            this.dockSiteBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barAttributeTable)).EndInit();
            this.barAttributeTable.ResumeLayout(false);
            this.panelTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSettable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttributeTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileRecTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zdFileTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorEx2)).EndInit();
            this.bindingNavigatorEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarfield)).EndInit();
            this.dockSite10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barMain)).EndInit();
            this.barMain.ResumeLayout(false);
            this.panelMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControlHide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapCtlMain)).EndInit();
            this.contextMenuMap.ResumeLayout(false);
            this.panelLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutCtlMain)).EndInit();
            this.panelRulerVertical.ResumeLayout(false);
            this.panelRulerHorizontal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.panelScene.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axSceneCtlMain)).EndInit();
            this.dockSiteLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barLeft)).EndInit();
            this.barLeft.ResumeLayout(false);
            this.panelLayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCCtlLayer)).EndInit();
            this.dockSiteRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barModel)).EndInit();
            this.barModel.ResumeLayout(false);
            this.panelDockContainer5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.grpRandomGen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtThirdParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecondParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstParam)).EndInit();
            this.grpGeneral.ResumeLayout(false);
            this.grpInputExisting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barRight)).EndInit();
            this.barRight.ResumeLayout(false);
            this.panel3D.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barDataRv)).EndInit();
            this.barDataRv.ResumeLayout(false);
            this.panelDockContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRv)).EndInit();
            this.contextMenuStrip3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelDockContainer2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datagridzdFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagridLocal)).EndInit();
            this.panelExSearch.ResumeLayout(false);
            this.panelRecords.ResumeLayout(false);
            this.panelRemote.ResumeLayout(false);
            this.panelLocal.ResumeLayout(false);
            this.panelDataSource.ResumeLayout(false);
            this.dockSite7.ResumeLayout(false);
            this.contextMenuBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.menuMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barContexMenuEditor)).EndInit();
            this.barContexMenuEditor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarCtlMenuEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barGeoAdjust)).EndInit();
            this.barGeoAdjust.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlSpatialAdjust)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barCustom)).EndInit();
            this.barCustom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlCustom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barTerrain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barCommon)).EndInit();
            this.barCommon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlCommon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar3D)).EndInit();
            this.bar3D.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlScene)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barSunAlt)).EndInit();
            this.barSunAlt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarSunAltitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barGeoReference)).EndInit();
            this.barGeoReference.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlRectifyRaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barLayout)).EndInit();
            this.barLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlLayout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditor)).EndInit();
            this.barEditor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEffects)).EndInit();
            this.barEffects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barTinEditor)).EndInit();
            this.barTinEditor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlTinEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barStatus)).EndInit();
            this.barStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControlEffects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.DotNetBarManager dotNetBarManagerMain;
        private DevComponents.DotNetBar.DockSite dockSiteBottom;
        private DevComponents.DotNetBar.DockSite dockSiteLeft;
        private DevComponents.DotNetBar.DockSite dockSiteRight;
        private DevComponents.DotNetBar.DockSite dockSite3;
        private DevComponents.DotNetBar.DockSite dockSite5;
        private DevComponents.DotNetBar.DockSite dockSite6;
        private DevComponents.DotNetBar.DockSite dockSite7;
        private DevComponents.DotNetBar.DockSite dockSite8;
        private DevComponents.DotNetBar.Bar barLeft;
        private DevComponents.DotNetBar.PanelDockContainer panelLayer;
        private DevComponents.DotNetBar.DockContainerItem dockLayers;
        private DevComponents.DotNetBar.StyleManager styleManagerMain;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCCtlLayer;
        private DevComponents.DotNetBar.DockSite dockSite9;
        private DevComponents.DotNetBar.DockSite dockSite10;
        private DevComponents.DotNetBar.Bar barMain;
        private DevComponents.DotNetBar.PanelDockContainer panelScene;
        private DevComponents.DotNetBar.PanelDockContainer panelMap;
        private DevComponents.DotNetBar.PanelDockContainer panelLayout;
        private DevComponents.DotNetBar.DockContainerItem dockMap;
        private DevComponents.DotNetBar.DockContainerItem dockLayout;
        private DevComponents.DotNetBar.DockContainerItem dockScene;
        private DevComponents.DotNetBar.Bar barRight;
        private DevComponents.DotNetBar.PanelDockContainer panel3D;
        private DevComponents.DotNetBar.DockContainerItem dock3D;
        private ESRI.ArcGIS.Controls.AxSceneControl axSceneCtlMain;
        private ESRI.ArcGIS.Controls.AxMapControl axMapCtlMain;
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutCtlMain;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private DevComponents.DotNetBar.PanelDockContainer panelDockContainer4;
        private DevComponents.DotNetBar.PanelDockContainer panelDockContainer3;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem4;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem3;
        private DevComponents.DotNetBar.Bar barAttributeTable;
        private DevComponents.DotNetBar.PanelDockContainer panelTable;
        private DevComponents.DotNetBar.DockContainerItem dockTable;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem14;
        private DevComponents.DotNetBar.ButtonItem buttonItem15;
        private DevComponents.DotNetBar.ButtonItem buttonItem16;
        private DevComponents.DotNetBar.Bar menuMain;
        private DevComponents.DotNetBar.ButtonItem menuFile;
        private DevComponents.DotNetBar.ButtonItem menuView;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem2;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl2;
        private DevComponents.DotNetBar.Bar barTerrain;
        private DevComponents.DotNetBar.ButtonItem btnRandomTerrain;
        private DevComponents.DotNetBar.Bar barTinEditor;
        private DevComponents.DotNetBar.ButtonItem buttonItemAddTin;
        private DevComponents.DotNetBar.Bar bar3D;
        private DevComponents.DotNetBar.Bar barEditor;
        private DevComponents.DotNetBar.Bar barLayout;
        private DevComponents.DotNetBar.Bar barCommon;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControlCommon;
        private DevComponents.DotNetBar.ButtonItem btnRandomTexture;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControlLayout;
        private DevComponents.DotNetBar.Bar barStatus;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarXMain;
        private DevComponents.DotNetBar.LabelItem lblInfo;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControlEdit;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem5;
        private DevComponents.DotNetBar.ButtonItem buttonItemSelect;
        private DevComponents.DotNetBar.ButtonItem buttonItemClearSelection;
        private DevComponents.DotNetBar.ButtonItem buttonItemVisible;
        private DevComponents.DotNetBar.ButtonItem buttonItemUnVisible;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControlScene;
        private DevComponents.DotNetBar.ButtonItem toolcreatfile;
        private DevComponents.DotNetBar.ButtonItem buttonItemCustomSet;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControlTinEdit;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbTargetTinLayer;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem8;
        private DevComponents.DotNetBar.ButtonItem buttonItemDEMToTIN;
        private DevComponents.DotNetBar.ButtonItem buttonItemTINToDEM;
        private DevComponents.DotNetBar.Controls.GroupPanel panelRulerVertical;
        private Lyquidity.UtilityLibrary.Controls.RulerControl rulerCtlVertical;
        private DevComponents.DotNetBar.Controls.GroupPanel panelRulerHorizontal;
        private Lyquidity.UtilityLibrary.Controls.RulerControl rulerCtlHorizontal;
        private DevComponents.DotNetBar.Controls.BindingNavigatorEx bindingNavigatorEx2;
        private DevComponents.DotNetBar.LabelItem btnNOsel;
        private DevComponents.DotNetBar.ButtonItem btnTable;
        private DevComponents.DotNetBar.ButtonItem btnAddField;
        private DevComponents.DotNetBar.ButtonItem btnExport;
        private DevComponents.DotNetBar.Controls.TextBoxX txtExistingModel;
        private DevComponents.Editors.ComboItem cmbiSubdivisionThree;
        private DevComponents.Editors.ComboItem cmbiSubdivisionTwo;
        private DevComponents.Editors.ComboItem cmbiSubdivisionOne;
        private DevComponents.Editors.ComboItem cmbiSubdivisionZero;
        private DevComponents.DotNetBar.ButtonItem btnRandomModel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExNonCrater;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExCrater;
        private DevComponents.DotNetBar.LabelItem lblTinTargetLayer;
        private DevComponents.DotNetBar.LabelItem lblCrater;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem9;
        private DevComponents.DotNetBar.LabelItem lblNonCrater;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem10;
        private DevComponents.DotNetBar.ButtonItem buttonItemMerge;
        private DevComponents.DotNetBar.ButtonItem buttonItemNewFeature;
        private DevComponents.DotNetBar.ButtonItem buttonItemnewFeatureLayer;
        private DevComponents.DotNetBar.ButtonItem buttonItemNewDoc_Click;
        private DevComponents.DotNetBar.ButtonItem buttonItemOpenDoc;
        private DevComponents.DotNetBar.ButtonItem buttonItemSaveDoc;
        private DevComponents.DotNetBar.ButtonItem buttonItemSaveAs;
        private DevComponents.DotNetBar.ButtonItem buttonItemExit;
        private DevComponents.DotNetBar.Bar barGeoReference;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbTargetRasterLayer;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControlRectifyRaster;
        private DevComponents.DotNetBar.LabelItem lblTargetRasterLayer;
        private DevComponents.DotNetBar.ButtonItem btnselectras;
        private DevComponents.DotNetBar.ButtonItem btnselectallras;
        private DevComponents.DotNetBar.ButtonItem btnswitchras;
        private DevComponents.DotNetBar.ButtonItem btnselclear;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarfield;
        private DevComponents.DotNetBar.ButtonItem buttonItemhide;
        private DevComponents.DotNetBar.Controls.DataGridViewX GridTable;
        private System.Data.DataSet dataSettable;
        private System.Data.DataTable AttributeTable;
        private DevComponents.DotNetBar.ButtonItem btnshowall;
        private DevComponents.DotNetBar.ButtonItem btnshowsel;
        private DevComponents.DotNetBar.ButtonItem buttonItemCreateShapeFile;
        private DevComponents.DotNetBar.ButtonItem btndelectfield;
        private DevComponents.DotNetBar.ButtonItem btnbarleft;
        private DevComponents.DotNetBar.Bar barGeoAdjust;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControlSpatialAdjust;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem13;
        private DevComponents.Editors.ComboItem cmiAffine;
        private DevComponents.Editors.ComboItem cmiProjective;
        private DevComponents.Editors.ComboItem cmiSimilarity;
        private DevComponents.Editors.ComboItem cmiRubberSheet;
        private DevComponents.Editors.ComboItem cmiEdgeSnap;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbAdjustMethod;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem14;
        private DevComponents.Editors.ComboItem cbiAffine;
        private DevComponents.Editors.ComboItem cbiProjective;
        private DevComponents.Editors.ComboItem cbiSimilarity;
        private DevComponents.DotNetBar.ButtonItem btn_surfaceop;
        private DevComponents.DotNetBar.ButtonItem menu3DAnalyst;
        private DevComponents.DotNetBar.ButtonItem btnillum;
        private DevComponents.DotNetBar.ButtonItem btnprofile;
        private System.Windows.Forms.ContextMenuStrip contextMenuBar;
        private System.Windows.Forms.ToolStripMenuItem popupBarCommon;
        private System.Windows.Forms.ToolStripMenuItem popupBarLayout;
        private System.Windows.Forms.ToolStripMenuItem popupBar3D;
        private System.Windows.Forms.ToolStripMenuItem popupBarEditor;
        private System.Windows.Forms.ToolStripMenuItem popupBarTinEditor;
        private System.Windows.Forms.ToolStripMenuItem popupBarGeoReference;
        private System.Windows.Forms.ToolStripMenuItem popupBarGeoAdjust;
        private DevComponents.DotNetBar.ButtonItem btnbarright;
        private DevComponents.DotNetBar.ButtonItem btnmap;
        private DevComponents.DotNetBar.ButtonItem btnlayout;
        private DevComponents.DotNetBar.ButtonItem btn3d;
        private DevComponents.DotNetBar.ButtonItem btn_RasterClip;
        private DevComponents.DotNetBar.ButtonItem btnshowallout;
        private DevComponents.DotNetBar.ButtonItem btnshowselout;
        private System.Windows.Forms.ContextMenuStrip contextMenuMap;
        private System.Windows.Forms.ToolStripMenuItem toolzoomin;
        private System.Windows.Forms.ToolStripMenuItem toolzoomout;
        private System.Windows.Forms.ToolStripMenuItem toolpan;
        private System.Windows.Forms.ToolStripMenuItem toolextent;
        private System.Windows.Forms.ToolStripMenuItem toolzoominfixed;
        private System.Windows.Forms.ToolStripMenuItem toolzoomoutfixed;
        private System.Windows.Forms.ToolStripMenuItem toolpreview;
        private System.Windows.Forms.ToolStripMenuItem toolnextview;
        private System.Windows.Forms.ToolStripMenuItem toolselset;
        private System.Windows.Forms.ToolStripMenuItem menuMapPopupSelect;
        private DevComponents.DotNetBar.ButtonItem btn_CreateFromXML;
        private DevComponents.DotNetBar.ButtonItem buttonItemPointToLine;
        private DevComponents.DotNetBar.ButtonItem btnvector;
        private DevComponents.DotNetBar.ButtonItem btn_CreateLabel;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonItem btntoolmatrix;
        private DevComponents.DotNetBar.ButtonItem btnMenuAmizuth;
        private DevComponents.DotNetBar.Bar barSunAlt;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarSunAltitude;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSunAltXmlFile;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSunAltOriImg;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem16;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem17;
        private DevComponents.DotNetBar.ButtonX btnSetXmlFile;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem18;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem15;
        private System.Windows.Forms.ToolStripMenuItem popupBarSunalt;
        private DevComponents.DotNetBar.Bar barEffects;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControlEffects;
        private DevComponents.DotNetBar.SliderItem sliderItemTransparency;
        private DevComponents.DotNetBar.SliderItem sliderItemContrast;
        private DevComponents.DotNetBar.SliderItem sliderItemBrightness;
        private System.Windows.Forms.ToolStripMenuItem popupBarEffects;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem11;
        private System.Windows.Forms.TreeView treeViewTemplate;
        private DevComponents.DotNetBar.ButtonItem btn_CreateImagePath;
        private DevComponents.DotNetBar.ButtonItem btnIFLI_RPACPA;
        private DevComponents.DotNetBar.ButtonItem btnIFLI_MTRPPLAN;
        private DevComponents.DotNetBar.ButtonItem btn_CreatParkPoint;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ButtonItem menuToolVector;
        private DevComponents.DotNetBar.ButtonItem menuShpFromFile;
        private DevComponents.DotNetBar.ButtonItem btnIFLI_GMP;
        private DevComponents.DotNetBar.ButtonItem btnCreatGmpPoint;
        private DevComponents.DotNetBar.ButtonItem btnMapSymbol;
        private DevComponents.DotNetBar.ButtonItem btnMapParkSymbol;
        private DevComponents.DotNetBar.ButtonItem btnINMapSymbol;
        private DevComponents.DotNetBar.ButtonItem menuData;
        private DevComponents.DotNetBar.ButtonItem menuProcess;
        private DevComponents.DotNetBar.ButtonItem menuBtnTransform;
        private DevComponents.DotNetBar.ButtonItem menuBtnRaster;
        private DevComponents.DotNetBar.ButtonItem menuBtnVisibility;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ButtonItem btnNEDtoENU;
        private DevComponents.DotNetBar.ButtonItem menuBtnRasterPan;
        private DevComponents.DotNetBar.ButtonItem menuBtnRasterRotate;
        private DevComponents.DotNetBar.ButtonItem menuBtnRasterMirror;
        private DevComponents.DotNetBar.ButtonItem menuBtnFlip;
        private DevComponents.DotNetBar.ButtonItem menuBtnRasterZ;
        private DevComponents.DotNetBar.ButtonItem menuBtnRasterResample;
        private DevComponents.DotNetBar.ButtonItem menuMapping;
        private DevComponents.DotNetBar.ButtonItem btnAddText;
        private DevComponents.DotNetBar.ButtonItem btnAddNorthArrow;
        private DevComponents.DotNetBar.ButtonItem btnAddLegend;
        private DevComponents.DotNetBar.ButtonItem btnAddGrid;
        private DevComponents.DotNetBar.ButtonItem btnAddScallBar;
        private DevComponents.DotNetBar.ButtonItem btnAddScallText;
        private DevComponents.DotNetBar.ButtonItem btnExportMap;
        private DevComponents.DotNetBar.ButtonItem btnPrintMap;
        private DevComponents.DotNetBar.Bar barDataRv;
        private DevComponents.DotNetBar.PanelDockContainer panelDockContainer1;
        private DevComponents.DotNetBar.DockContainerItem dockDataRv;
        private DevComponents.DotNetBar.PanelDockContainer panelDockContainer2;
        private DevComponents.DotNetBar.DockContainerItem dockDataSearch;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnStartListen;
        private DevComponents.DotNetBar.ButtonItem btnStopListen;
        private DevComponents.DotNetBar.TextBoxItem txtFolder;
        private DevComponents.DotNetBar.ButtonItem btnBrowse;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridRv;
        private DevComponents.DotNetBar.PanelEx panelExSearch;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx RichTBoxSQL;
        private DevComponents.DotNetBar.LabelX lblsqlset;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.Controls.CheckBoxX RadioBoxService;
        private DevComponents.DotNetBar.Controls.CheckBoxX RadioBoxLocal;
        private System.Data.DataTable FileRecTable;
        private System.Data.DataColumn ColumnID;
        private System.Data.DataColumn ColumnFileName;
        private System.Data.DataColumn ColumnFilePath;
        private System.Data.DataColumn ColumnRecTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序号DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文件名DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 存储路径DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 接收时间DataGridViewTextBoxColumn;
        private DevComponents.DotNetBar.ButtonX btnSetSQL;
        private DevComponents.DotNetBar.LabelX lblfiletype;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbfiletype;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.Editors.ComboItem comboItem10;
        private DevComponents.Editors.ComboItem comboItem11;
        private System.Data.DataTable zdFileTable;
        private System.Data.DataColumn ID;
        private System.Data.DataColumn szfilename;
        private System.Data.DataColumn szfilenames;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSaveFloder;
        private DevComponents.DotNetBar.LabelX lblSaveFloder;
        private DevComponents.DotNetBar.TextBoxItem textBoxItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private DevComponents.DotNetBar.TextBoxItem textBoxItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem7;
        private DevComponents.DotNetBar.TextBoxItem textBoxItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem8;
        private DevComponents.DotNetBar.ButtonX btnSaveFloder;
        private System.Data.DataColumn uiTaskCode;
        private System.Data.DataColumn uiObjCode;
        private System.Data.DataColumn uiFileTypes;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem toolopenfile;
        private System.Windows.Forms.ToolStripMenuItem tooltransandopen;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.DataGridViewX datagridzdFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn szfilenameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn szfilenamesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiTaskCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiObjCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiFileTypesDataGridViewTextBoxColumn;
        private DevComponents.DotNetBar.Controls.DataGridViewX datagridLocal;
        private DevComponents.DotNetBar.ButtonItem btnmxdRAR;
        private DevComponents.DotNetBar.ButtonItem btnProBackWard;
        private DevComponents.DotNetBar.ButtonItem btnJSCamLabel;
        private DevComponents.DotNetBar.ButtonItem btnSkyLinePara;
        private DevComponents.DotNetBar.Bar barModel;
        //private DevComponents.DotNetBar.DockContainerItem dockContainerItem2;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem5;
        private DevComponents.DotNetBar.PanelDockContainer panelDockContainer5;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem6;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.GroupPanel grpRandomGen;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.Editors.DoubleInput txtThirdParam;
        private DevComponents.Editors.DoubleInput txtSecondParam;
        private DevComponents.Editors.DoubleInput txtFirstParam;
        private DevComponents.DotNetBar.Controls.GroupPanel grpGeneral;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSubdivisionCount;
        private DevComponents.Editors.ComboItem cmbiOne;
        private DevComponents.Editors.ComboItem cmbiTwo;
        private DevComponents.Editors.ComboItem cmbiThree;
        private DevComponents.Editors.ComboItem cmbiFour;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbMappingType;
        private DevComponents.Editors.ComboItem cmbiPlat;
        private DevComponents.Editors.ComboItem cmbiSphere;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbTritype;
        private DevComponents.Editors.ComboItem cmbiForward;
        private DevComponents.Editors.ComboItem cmbiBackword;
        private DevComponents.Editors.ComboItem cmbiSubdivision;
        private DevComponents.DotNetBar.LabelX lblSubdivisionCount;
        private DevComponents.DotNetBar.LabelX lblMappingType;
        private DevComponents.DotNetBar.LabelX lblTriType;
        private DevComponents.DotNetBar.LabelX lblThirdParam;
        private DevComponents.DotNetBar.LabelX lblSecondParam;
        private DevComponents.DotNetBar.LabelX lblFirstParam;
        private DevComponents.DotNetBar.LabelX lblModelType;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbModelType;
        private DevComponents.Editors.ComboItem cmbiCrater;
        private DevComponents.Editors.ComboItem cmbiEllipse;
        private DevComponents.Editors.ComboItem cmbiPyramid;
        private DevComponents.Editors.ComboItem cmbiTetra;
        private DevComponents.DotNetBar.ButtonX btnTextureFilename;
        private DevComponents.DotNetBar.ButtonX btnOutputFilename;
        private DevComponents.DotNetBar.LabelX lblTextureFilename;
        private DevComponents.DotNetBar.LabelX lblOutputFilename;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTextureFilename;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOutputFilename;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkRandomGen;
        private DevComponents.DotNetBar.ButtonX btnGenerate;
        private DevComponents.DotNetBar.Controls.GroupPanel grpInputExisting;
        private DevComponents.DotNetBar.LabelX labelXCrater;
        private DevComponents.DotNetBar.LabelX labelXStone;
        private DevComponents.DotNetBar.Controls.TextBoxX txtExistingNonCrater;
        private DevComponents.DotNetBar.ButtonX btnChooseNonCrater;
        private DevComponents.DotNetBar.Controls.TextBoxX txtExistingCrater;
        private DevComponents.DotNetBar.ButtonX btnChooseExisingModel;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkInputExisting;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControlHide;
        private DevComponents.DotNetBar.Bar barCustom;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControlCustom;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem19;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem popupBarCustom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem7;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem3;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmBoxEffectsLayer;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem23;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbStationID;
        private DevComponents.DotNetBar.LabelX lblStationID;
        private DevComponents.DotNetBar.Bar barContexMenuEditor;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarCtlMenuEditor;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem20;
        private DevComponents.DotNetBar.ButtonItem btnFieldCalculator;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem4;
        private DevComponents.DotNetBar.ButtonItem btnCheckUpdate;
        private DevComponents.DotNetBar.ButtonItem btnRasterExportBatch;
        private DevComponents.DotNetBar.ButtonItem btnNEDtoENUShape;
        private DevComponents.Editors.ComboItem comboItem12;
        private DevComponents.DotNetBar.ButtonItem btnLandExpMap;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem12;
        private DevComponents.DotNetBar.ButtonItem btnItemExportMxd;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRecorders;
        private DevComponents.DotNetBar.PanelEx panelRemote;
        private DevComponents.DotNetBar.PanelEx panelLocal;
        private DevComponents.DotNetBar.PanelEx panelDataSource;
        private DevComponents.DotNetBar.ButtonItem btnDownLoadAll;
        private DevComponents.DotNetBar.PanelEx panelRecords;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl gridRecords;
        private DevComponents.DotNetBar.ButtonItem btnSeekSite;
        private DevComponents.DotNetBar.ButtonItem btnAddXYField;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl2;
        private DevComponents.DotNetBar.ButtonItem buttonItem9;
        private DevComponents.DotNetBar.ButtonItem buttonItem10;
        private DevComponents.DotNetBar.ButtonItem buttonItem11;
        private DevComponents.DotNetBar.ButtonItem buttonItem12;
        private DevComponents.DotNetBar.ButtonItem buttonItem13;
        private DevComponents.DotNetBar.ButtonItem buttonItem17;
        private DevComponents.DotNetBar.ButtonItem buttonItem18;
        private DevComponents.DotNetBar.ButtonItem buttonItem19;
        private DevComponents.DotNetBar.ButtonItem buttonItem20;
        private DevComponents.DotNetBar.ButtonItem buttonItem21;
        private DevComponents.DotNetBar.ButtonItem buttonItem22;
        private DevComponents.DotNetBar.ButtonItem buttonItem23;
        private DevComponents.DotNetBar.ButtonItem buttonItem24;
        private DevComponents.DotNetBar.ButtonItem buttonItem25;
        private DevComponents.DotNetBar.ButtonItem buttonItem26;
        private DevComponents.DotNetBar.ButtonItem buttonItem27;
        private DevComponents.DotNetBar.ButtonItem buttonItem28;
        private DevComponents.DotNetBar.ButtonItem buttonItem29;
        private DevComponents.DotNetBar.ButtonItem buttonItem30;
        private DevComponents.DotNetBar.ButtonItem buttonItem31;
        private DevComponents.DotNetBar.ButtonItem buttonItem32;
        private DevComponents.DotNetBar.ButtonItem buttonItem33;
        private DevComponents.DotNetBar.ButtonItem buttonItem34;
        private DevComponents.DotNetBar.ButtonItem buttonItem35;
        private DevComponents.DotNetBar.ButtonItem buttonItem36;
        private DevComponents.DotNetBar.ButtonItem buttonItem37;
        private DevComponents.DotNetBar.ButtonItem buttonItem38;
        private DevComponents.DotNetBar.ButtonItem buttonItem39;
        private DevComponents.DotNetBar.ButtonItem buttonItem40;
        private DevComponents.DotNetBar.ButtonItem buttonItem41;
        private DevComponents.DotNetBar.ButtonItem buttonItem42;
        private DevComponents.DotNetBar.ButtonItem buttonItem43;
        private DevComponents.DotNetBar.ButtonItem buttonItem44;
        private DevComponents.DotNetBar.ButtonItem buttonItem45;
        private DevComponents.DotNetBar.ButtonItem buttonItem46;
        private DevComponents.DotNetBar.ButtonItem buttonItem47;
        private DevComponents.DotNetBar.ButtonItem buttonItem48;
        private DevComponents.DotNetBar.ButtonItem buttonItem49;
        private DevComponents.DotNetBar.ButtonItem buttonItem50;
        private DevComponents.DotNetBar.ButtonItem buttonItem51;
        private DevComponents.DotNetBar.ButtonItem buttonItem52;
        private DevComponents.DotNetBar.ButtonItem buttonItem53;
        private DevComponents.DotNetBar.ButtonItem buttonItem54;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.ButtonItem buttonItemCreateCenterline;
        private DevComponents.DotNetBar.ButtonItem buttonItemCenterlineInsideReport;
        private DevComponents.DotNetBar.ButtonItem buttonItemGeneratePDFReport;
        private DevComponents.DotNetBar.ButtonItem buttonItemExportToCAD;
        private DevComponents.DotNetBar.ButtonItem buttonItemWeldAlignToCenterline;
    }
}

