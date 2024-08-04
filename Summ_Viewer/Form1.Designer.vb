<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lbFilenameList = New System.Windows.Forms.ListView()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbServerSelect = New System.Windows.Forms.ComboBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AkelPadFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotepadFolderSelectTSMItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AkelPadFolderSelectSMItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotepadplusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FolderSelectSMitem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.FILEToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SavedFileFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SaveCopyOfSelectedFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckOnDatalogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbAlarmFail = New System.Windows.Forms.ComboBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.txtTestNum = New System.Windows.Forms.TextBox()
        Me.btnSelFile = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.lbRowsNum = New System.Windows.Forms.Label()
        Me.lbLVtotRows = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lbDGVtotalRows = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Chart2 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.OFD = New System.Windows.Forms.OpenFileDialog()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbTestNameTot = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbTestNumTot = New System.Windows.Forms.Label()
        Me.lbTestNameCnt = New System.Windows.Forms.Label()
        Me.lbTestnumcnt = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(13, 61)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(147, 23)
        Me.txtSearch.TabIndex = 0
        '
        'lbFilenameList
        '
        Me.lbFilenameList.Alignment = System.Windows.Forms.ListViewAlignment.[Default]
        Me.lbFilenameList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbFilenameList.BackColor = System.Drawing.Color.Black
        Me.lbFilenameList.ForeColor = System.Drawing.Color.White
        Me.lbFilenameList.FullRowSelect = True
        Me.lbFilenameList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lbFilenameList.HideSelection = False
        Me.lbFilenameList.Location = New System.Drawing.Point(3, 3)
        Me.lbFilenameList.Name = "lbFilenameList"
        Me.lbFilenameList.Size = New System.Drawing.Size(1006, 532)
        Me.lbFilenameList.TabIndex = 3
        Me.lbFilenameList.UseCompatibleStateImageBehavior = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbServerSelect)
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 33)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(170, 98)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SERVER SELECT"
        '
        'cmbServerSelect
        '
        Me.cmbServerSelect.FormattingEnabled = True
        Me.cmbServerSelect.Location = New System.Drawing.Point(13, 22)
        Me.cmbServerSelect.Name = "cmbServerSelect"
        Me.cmbServerSelect.Size = New System.Drawing.Size(147, 21)
        Me.cmbServerSelect.TabIndex = 6
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.FILEToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1044, 24)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AkelPadFolderToolStripMenuItem, Me.ExitToolStripMenuItem, Me.FolderSelectSMitem1, Me.ExitToolStripMenuItem2})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(86, 20)
        Me.FileToolStripMenuItem.Text = "APP FOLDER"
        '
        'AkelPadFolderToolStripMenuItem
        '
        Me.AkelPadFolderToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NotepadFolderSelectTSMItem, Me.AkelPadFolderSelectSMItem, Me.NotepadplusToolStripMenuItem})
        Me.AkelPadFolderToolStripMenuItem.Name = "AkelPadFolderToolStripMenuItem"
        Me.AkelPadFolderToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.AkelPadFolderToolStripMenuItem.Text = "Select App To Open Text File"
        '
        'NotepadFolderSelectTSMItem
        '
        Me.NotepadFolderSelectTSMItem.Name = "NotepadFolderSelectTSMItem"
        Me.NotepadFolderSelectTSMItem.Size = New System.Drawing.Size(136, 22)
        Me.NotepadFolderSelectTSMItem.Text = "Notepad"
        '
        'AkelPadFolderSelectSMItem
        '
        Me.AkelPadFolderSelectSMItem.Name = "AkelPadFolderSelectSMItem"
        Me.AkelPadFolderSelectSMItem.Size = New System.Drawing.Size(136, 22)
        Me.AkelPadFolderSelectSMItem.Text = "AkelPad"
        '
        'NotepadplusToolStripMenuItem
        '
        Me.NotepadplusToolStripMenuItem.Name = "NotepadplusToolStripMenuItem"
        Me.NotepadplusToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.NotepadplusToolStripMenuItem.Text = "Notepad++"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.ExitToolStripMenuItem.Text = "STDFViewer Folder"
        '
        'FolderSelectSMitem1
        '
        Me.FolderSelectSMitem1.Name = "FolderSelectSMitem1"
        Me.FolderSelectSMitem1.Size = New System.Drawing.Size(222, 22)
        Me.FolderSelectSMitem1.Text = "Select Folder To Save File"
        '
        'ExitToolStripMenuItem2
        '
        Me.ExitToolStripMenuItem2.Name = "ExitToolStripMenuItem2"
        Me.ExitToolStripMenuItem2.Size = New System.Drawing.Size(222, 22)
        Me.ExitToolStripMenuItem2.Text = "Exit"
        '
        'FILEToolStripMenuItem1
        '
        Me.FILEToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SavedFileFolderToolStripMenuItem})
        Me.FILEToolStripMenuItem1.Name = "FILEToolStripMenuItem1"
        Me.FILEToolStripMenuItem1.Size = New System.Drawing.Size(40, 20)
        Me.FILEToolStripMenuItem1.Text = "FILE"
        '
        'SavedFileFolderToolStripMenuItem
        '
        Me.SavedFileFolderToolStripMenuItem.Name = "SavedFileFolderToolStripMenuItem"
        Me.SavedFileFolderToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.SavedFileFolderToolStripMenuItem.Text = "OPEN SAVED FILE FOLDER"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveCopyOfSelectedFilesToolStripMenuItem, Me.CheckOnDatalogToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(220, 48)
        '
        'SaveCopyOfSelectedFilesToolStripMenuItem
        '
        Me.SaveCopyOfSelectedFilesToolStripMenuItem.Name = "SaveCopyOfSelectedFilesToolStripMenuItem"
        Me.SaveCopyOfSelectedFilesToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.SaveCopyOfSelectedFilesToolStripMenuItem.Text = "Save copy of selected file(s)"
        '
        'CheckOnDatalogToolStripMenuItem
        '
        Me.CheckOnDatalogToolStripMenuItem.Name = "CheckOnDatalogToolStripMenuItem"
        Me.CheckOnDatalogToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.CheckOnDatalogToolStripMenuItem.Text = "Check Datalog "
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.cmbAlarmFail)
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Controls.Add(Me.txtTestNum)
        Me.GroupBox2.Controls.Add(Me.btnSelFile)
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Location = New System.Drawing.Point(188, 34)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(394, 97)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "FAIL/ALARM PARAMETERS FROM DATALOG"
        '
        'cmbAlarmFail
        '
        Me.cmbAlarmFail.FormattingEnabled = True
        Me.cmbAlarmFail.Location = New System.Drawing.Point(13, 21)
        Me.cmbAlarmFail.Name = "cmbAlarmFail"
        Me.cmbAlarmFail.Size = New System.Drawing.Size(141, 21)
        Me.cmbAlarmFail.TabIndex = 10
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(293, 51)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(94, 30)
        Me.Button3.TabIndex = 9
        Me.Button3.Text = "SUMMARIZE"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'txtTestNum
        '
        Me.txtTestNum.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTestNum.Location = New System.Drawing.Point(13, 61)
        Me.txtTestNum.Name = "txtTestNum"
        Me.txtTestNum.Size = New System.Drawing.Size(141, 20)
        Me.txtTestNum.TabIndex = 9
        '
        'btnSelFile
        '
        Me.btnSelFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelFile.Location = New System.Drawing.Point(175, 16)
        Me.btnSelFile.Name = "btnSelFile"
        Me.btnSelFile.Size = New System.Drawing.Size(105, 30)
        Me.btnSelFile.TabIndex = 3
        Me.btnSelFile.Text = "Select File(*.txt)"
        Me.btnSelFile.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(293, 16)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(94, 30)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "Execute"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 137)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1020, 582)
        Me.TabControl1.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lbRowsNum)
        Me.TabPage1.Controls.Add(Me.lbLVtotRows)
        Me.TabPage1.Controls.Add(Me.lbFilenameList)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1012, 556)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "SERVER SUMMARY"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lbRowsNum
        '
        Me.lbRowsNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbRowsNum.AutoSize = True
        Me.lbRowsNum.Location = New System.Drawing.Point(7, 538)
        Me.lbRowsNum.Name = "lbRowsNum"
        Me.lbRowsNum.Size = New System.Drawing.Size(184, 13)
        Me.lbRowsNum.TabIndex = 5
        Me.lbRowsNum.Text = "NUMBER OF SELECTED ROW/S: 0"
        '
        'lbLVtotRows
        '
        Me.lbLVtotRows.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbLVtotRows.AutoSize = True
        Me.lbLVtotRows.Location = New System.Drawing.Point(784, 539)
        Me.lbLVtotRows.Name = "lbLVtotRows"
        Me.lbLVtotRows.Size = New System.Drawing.Size(144, 13)
        Me.lbLVtotRows.TabIndex = 4
        Me.lbLVtotRows.Text = "TOTAL NUMBER OF FILES:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.lbDGVtotalRows)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.DataGridView1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1012, 556)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TEST FAILURE RAW DATA"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lbDGVtotalRows
        '
        Me.lbDGVtotalRows.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbDGVtotalRows.AutoSize = True
        Me.lbDGVtotalRows.Location = New System.Drawing.Point(866, 539)
        Me.lbDGVtotalRows.Name = "lbDGVtotalRows"
        Me.lbDGVtotalRows.Size = New System.Drawing.Size(85, 13)
        Me.lbDGVtotalRows.TabIndex = 1
        Me.lbDGVtotalRows.Text = "TOTAL ROWS: "
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 539)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "FILENAME: "
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.Location = New System.Drawing.Point(3, 6)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1006, 530)
        Me.DataGridView1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.ComboBox2)
        Me.TabPage3.Controls.Add(Me.ComboBox1)
        Me.TabPage3.Controls.Add(Me.Chart2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1012, 556)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "CHART"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(851, 214)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox2.TabIndex = 4
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(851, 176)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 3
        '
        'Chart2
        '
        ChartArea3.Name = "ChartArea1"
        Me.Chart2.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Me.Chart2.Legends.Add(Legend3)
        Me.Chart2.Location = New System.Drawing.Point(25, 55)
        Me.Chart2.Margin = New System.Windows.Forms.Padding(2)
        Me.Chart2.Name = "Chart2"
        Series3.ChartArea = "ChartArea1"
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        Me.Chart2.Series.Add(Series3)
        Me.Chart2.Size = New System.Drawing.Size(960, 390)
        Me.Chart2.TabIndex = 2
        Me.Chart2.Text = "Chart2"
        '
        'OFD
        '
        Me.OFD.FileName = "OpenFileDialog1"
        '
        'Timer2
        '
        Me.Timer2.Interval = 500
        '
        'Chart1
        '
        ChartArea4.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea4)
        Legend4.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend4)
        Me.Chart1.Location = New System.Drawing.Point(0, 0)
        Me.Chart1.Name = "Chart1"
        Series4.ChartArea = "ChartArea1"
        Series4.Legend = "Legend1"
        Series4.Name = "Series1"
        Me.Chart1.Series.Add(Series4)
        Me.Chart1.Size = New System.Drawing.Size(300, 300)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.lbTestNameTot)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.lbTestNumTot)
        Me.GroupBox3.Controls.Add(Me.lbTestNameCnt)
        Me.GroupBox3.Controls.Add(Me.lbTestnumcnt)
        Me.GroupBox3.Location = New System.Drawing.Point(589, 34)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(436, 97)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "COUNTERS"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(325, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Total Alarm: 0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(325, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Total Failed: 0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(158, 70)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Total Yield: %"
        '
        'lbTestNameTot
        '
        Me.lbTestNameTot.AutoSize = True
        Me.lbTestNameTot.Location = New System.Drawing.Point(158, 43)
        Me.lbTestNameTot.Name = "lbTestNameTot"
        Me.lbTestNameTot.Size = New System.Drawing.Size(130, 13)
        Me.lbTestNameTot.TabIndex = 3
        Me.lbTestNameTot.Text = "Distinct TestNameTotal: 0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Total Tested: 0"
        '
        'lbTestNumTot
        '
        Me.lbTestNumTot.AutoSize = True
        Me.lbTestNumTot.Location = New System.Drawing.Point(158, 20)
        Me.lbTestNumTot.Name = "lbTestNumTot"
        Me.lbTestNumTot.Size = New System.Drawing.Size(127, 13)
        Me.lbTestNumTot.TabIndex = 2
        Me.lbTestNumTot.Text = "Distinct TestNum Total: 0"
        '
        'lbTestNameCnt
        '
        Me.lbTestNameCnt.AutoSize = True
        Me.lbTestNameCnt.Location = New System.Drawing.Point(6, 43)
        Me.lbTestNameCnt.Name = "lbTestNameCnt"
        Me.lbTestNameCnt.Size = New System.Drawing.Size(125, 13)
        Me.lbTestNameCnt.TabIndex = 1
        Me.lbTestNameCnt.Text = "Distinct TestName Cnt: 0"
        '
        'lbTestnumcnt
        '
        Me.lbTestnumcnt.AutoSize = True
        Me.lbTestnumcnt.Location = New System.Drawing.Point(6, 20)
        Me.lbTestnumcnt.Name = "lbTestnumcnt"
        Me.lbTestnumcnt.Size = New System.Drawing.Size(119, 13)
        Me.lbTestnumcnt.TabIndex = 0
        Me.lbTestnumcnt.Text = "Distinct TestNum Cnt: 0"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1044, 731)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtSearch As TextBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents lbFilenameList As ListView
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer1 As Timer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AkelPadFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FolderSelectSMitem1 As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SaveCopyOfSelectedFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FILEToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SavedFileFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents NotepadFolderSelectTSMItem As ToolStripMenuItem
    Friend WithEvents AkelPadFolderSelectSMItem As ToolStripMenuItem
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Button2 As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents OFD As OpenFileDialog
    Friend WithEvents btnSelFile As Button
    Friend WithEvents NotepadplusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CheckOnDatalogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Label3 As Label
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents txtTestNum As TextBox
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Chart2 As DataVisualization.Charting.Chart
    Friend WithEvents lbDGVtotalRows As Label
    Friend WithEvents lbLVtotRows As Label
    Friend WithEvents lbRowsNum As Label
    Friend WithEvents cmbServerSelect As ComboBox
    Friend WithEvents cmbAlarmFail As ComboBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents lbTestNumTot As Label
    Friend WithEvents lbTestNameCnt As Label
    Friend WithEvents lbTestnumcnt As Label
    Friend WithEvents lbTestNameTot As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox2 As ComboBox
End Class
