Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Collections 'For arraylist
Imports System.ComponentModel
Imports System.Timers

Imports System.Runtime.InteropServices
Imports System.Diagnostics
'Imports Interop.office

Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Windows.Forms.DataVisualization.Charting
'Imports Microsoft ' Interop.Excel

'Imports Microsoft.Office
'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Excel

Public Class Form1

    Private summaryTable As DataTable

    Public tmpTable As DataTable

    Dim tempFolder, DestFolder, txtPadFolder, STDF_Viewer, txtApp, ffd As String

    Dim fbd As FolderBrowserDialog

    Dim bolNotepad, bolAkelPad As Boolean

    Private Declare Function SetForegroundWindow Lib "User32.dll" (ByVal handle As IntPtr) As Boolean

    Private m_intMarqueeCounter As Integer = 1
    Private m_bolMarqueeIncrementUp As Boolean = True


    Private m_lstFileNames As New List(Of String)
    Private dctNameToLocation As New Dictionary(Of String, String)

    Dim counter As Integer
    Dim timer As Timer = New Timer
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged


        If txtSearch.Text.Substring(0) = "I" Or txtSearch.Text.Substring(0) = "i" Then
            rbIflex.Checked = True
        ElseIf txtSearch.Text.Substring(0) = "U" Or txtSearch.Text.Substring(0) = "u" Then
            rbUflex.Checked = True

        End If


    End Sub

    Private Sub txtSearch_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown

        TabControl1.SelectedIndex = 0

        lbFilenameList.Items.Clear()

        If txtSearch.Text.Count > 0 Then

            If e.KeyCode = Keys.Enter Then

                Me.Text = strAppText + "   " + "[ Loading files... ]"

                Dim t As String = Trim(txtSearch.Text).ToUpper
                lbFilenameList.Columns.Clear()
                lbFilenameList.Items.Clear()
                'listAllSumm(t)
                fileList(t)
            End If
        Else
            Exit Sub
        End If

        Me.Text = strAppText

        If Not lbFilenameList.Items.Count = 0 Then

            Button1.Enabled = True

        End If

    End Sub

    'TEST COMMENT
    'ANOTHER TEST

    Public Sub fileList(ByVal s As String)
        'This was solved the issue for ->Item with Same Key has already been added
        'whenever the search string is repeated twice.
        dctNameToLocation.Clear()
        m_lstFileNames = GetListofTraceViewFileNames(s)

        lbFilenameList.View = View.Details
        lbFilenameList.Columns.Add("FILENAME")
        lbFilenameList.Columns.Add("SIZE")
        lbFilenameList.Columns.Add("CREATION DATE")
        For Each file In m_lstFileNames
            'load all filenames except STDF files
            'If Not file.Contains(".stdf") Then
            'load selected file list to dictionary.
            dctNameToLocation.Add(IO.Path.GetFileName(file), file)
            Dim fileSize = file.Length()
            Dim item As ListViewItem = lbFilenameList.Items.Add(FileIO.FileSystem.GetName(file))


            item.SubItems.Add(Format(FileIO.FileSystem.GetFileInfo(file).Length / 1024, "0").ToString + " KB")

            'item.SubItems.Add(Format(FileIO.FileSystem.GetFileInfo(file).Length / 1024, "0.00 Kb"))
            item.SubItems.Add(FileIO.FileSystem.GetFileInfo(file).CreationTime)

            'End If

        Next
        For Each column As ColumnHeader In lbFilenameList.Columns
            column.Width = -1
        Next

    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        timer.Interval = 500

        AddHandler timer.Elapsed, AddressOf TimerEvent

        timer.AutoReset = True

        timer.Enabled = False

        Button1.Enabled = False

        If Not lbFilenameList.Items.Count = 0 Then

            Button1.Enabled = True

        End If

        Me.Text = strAppText

        ' Populate ComboBox1 with filter options
        ComboBox1.Items.Add("Select Filter")
        ComboBox1.Items.Add("Top 3")
        ComboBox1.Items.Add("Top 5")
        ComboBox1.Items.Add("Top 10")
        ComboBox1.SelectedIndex = 0 ' Set default to "Select Filter"

        'Me.MaximizeBox = False
        rbIflex.Checked = True

        Me.Text = strAppText
        Me.txtSearch.CharacterCasing = CharacterCasing.Upper
        Me.txtSearch.Select()

        Button2.Enabled = False

        'AkelFolder = "C:\Users\sterc\scoop\apps\akelpad\4.9.8\AkelPad.exe"
        txtPadFolder = My.Settings.txtPadFolder
        STDF_Viewer = My.Settings.STDFViewer
        fd = My.Settings.SaveFileFolder

        txtApp = My.Settings.txtApplication

        tempFolder = Application.StartupPath & "\" & "Saved Summary"

        If Not Directory.Exists(tempFolder) Then
            Directory.CreateDirectory(tempFolder)
        End If

        Timer2.Enabled = False

        login2Server()

    End Sub

    'Private handle As IntPtr


    Private Sub lbFilenameList_DoubleClick(sender As Object, e As EventArgs) Handles lbFilenameList.DoubleClick

        DestFolder = Application.StartupPath + "\Saved Summary\"
        Dim fname As String = lbFilenameList.SelectedItems(0).Text
        Dim sourceFile As String = m_strDefaultTraceviewPath + fname

        If System.IO.File.Exists(DestFolder + fname) = True Then
            'If file(s) is already exist inside the folder, will proceed to open
            'using selected app.
            GoTo proc

        Else
            ' Cursor = Cursors.WaitCursor
            Me.Text = strAppText + "[ Getting file ready. . . ]"

            Dim destLoc As String = DestFolder + fname

            Form2.Show()
            File.Copy(sourceFile, Path.Combine(DestFolder, fname), True)

            ' Cursor = Cursors.Default

            lbFilenameList.SelectedItems(0).Selected = False

            Form2.Close()
            Timer1.Enabled = False
            Me.Text = strAppText

        End If

proc:
        Dim apploc As String = txtPadFolder + txtApp
        Dim apploc2 As String = STDF_Viewer + "\STDF-Viewer.exe" '
        Dim f As String = DestFolder + fname

        If f.Contains(".txt") Or f.Contains(".sum") Or f.Contains(".summary") Then

            Process.Start(apploc, f)

        ElseIf f.Contains(".stdf") Then

            Dim processName As Process() = Process.GetProcessesByName("STDF-Viewer")
            If processName.Length = 0 Then
                'Start application here

                Process.Start(STDF_Viewer + "\STDF-Viewer.exe", f)

                SendKeys.Send("^(o)")

            Else

            End If

        End If

    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        DeleteFilesFromFolder("Saved Summary")
    End Sub

    Private Sub rbIflex_CheckedChanged(sender As Object, e As EventArgs) Handles rbIflex.CheckedChanged
        If rbIflex.Checked = True Then
            m_strDefaultTraceviewPath = "\\10.83.133.10\iflex\iflex_summary\"
            rbUflex.Checked = False
        Else
            m_strDefaultTraceviewPath = "\\10.83.133.10\uflex\uflex_summary\"
            rbIflex.Checked = False
        End If
    End Sub


    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        Dim fbd As New FolderBrowserDialog

        With fbd
            .SelectedPath = Application.StartupPath
            .Description = "Browse STDF Viewer folder"
            If .ShowDialog = DialogResult.OK Then
                STDF_Viewer = (.SelectedPath)
            End If
            My.Settings.STDFViewer = STDF_Viewer
            My.Settings.Save()
        End With

    End Sub




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not lbFilenameList.Items.Count = 0 Then

            Button1.Enabled = True

        End If

        lbFilenameList.Items.Clear()

        Me.Text = strAppText + "[ Loading files. . . ]"
        TabControl1.SelectedIndex = 0

        Dim t As String = Trim(txtSearch.Text).ToUpper
        lbFilenameList.Columns.Clear()
        lbFilenameList.Items.Clear()
        'listAllSumm(t)
        fileList(t)
        'End If
        Me.Text = strAppText


    End Sub

    Private Sub ExitToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem2.Click
        Application.Exit()
    End Sub

    Private Sub FolderSelectSMitem1_Click(sender As Object, e As EventArgs) Handles FolderSelectSMitem1.Click

        fbd = New FolderBrowserDialog

        With fbd
            .SelectedPath = "Desktop"
            .Description = "Browse File folder to save Datalogs and STDF file(s)."
            If .ShowDialog = DialogResult.OK Then
                SaveFileFolder = (.SelectedPath)
            End If
            My.Settings.SaveFileFolder = SaveFileFolder
            My.Settings.Save()
        End With

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click



        timer.Enabled = True
        timer.Start()

        'Timer2.Enabled = True
        'Timer2.Interval = 1000
        'Timer2.Start()

        'Me.Text = strAppText + "   " + "[ Start searching for failing parameters from datalog...] "

        If Button2.Text = "Execute" Then
            Form2.TextBox1.Text = Console.ReadLine
            Form2.Show()
            mtfSearcher()

            Button2.Text = "DGVToXL"
            timer.Enabled = False
            Form2.Close()
        ElseIf Button2.Text = "DGVToXL" Then

            expttoXL(fd)

            Button2.Text = "Execute"
            Button2.Enabled = False
        End If


    End Sub

    Private Sub NotepadFolderSelectTSMItem_Click(sender As Object, e As EventArgs) Handles NotepadFolderSelectTSMItem.Click

        NotepadFolderSelectTSMItem.Checked = True
        If NotepadFolderSelectTSMItem.Checked = True Then
            AkelPadFolderSelectSMItem.Checked = False
            NotepadplusToolStripMenuItem.Checked = False
            txtApp = "\Notepad.exe"
        End If
        txtPadFolder = "C:\WINDOWS\system32"
        My.Settings.txtPadFolder = txtPadFolder
        My.Settings.txtApplication = txtApp
        My.Settings.Save()
    End Sub

    Private Sub AkelPadFolderSelectSMItem_Click(sender As Object, e As EventArgs) Handles AkelPadFolderSelectSMItem.Click

        Dim fbd As New FolderBrowserDialog

        AkelPadFolderSelectSMItem.Checked = True

        If AkelPadFolderSelectSMItem.Checked = True Then
            NotepadFolderSelectTSMItem.Checked = False
            NotepadplusToolStripMenuItem.Checked = False
            txtApp = "\AkelPad.exe"
        End If

        With fbd
            .SelectedPath = "c:\Users"
            .Description = "Browse AkelPad folder"
            If .ShowDialog = DialogResult.OK Then
                txtPadFolder = (.SelectedPath)
            End If
            My.Settings.txtPadFolder = txtPadFolder
            My.Settings.txtApplication = txtApp
            My.Settings.Save()
        End With
    End Sub



    Private Sub SaveCopyOfSelectedFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveCopyOfSelectedFilesToolStripMenuItem.Click

        SaveFilesCopyToFolder()
        Process.Start("explorer.exe", My.Settings.SaveFileFolder)

    End Sub

    Private Sub btnSelFile_Click(sender As Object, e As EventArgs) Handles btnSelFile.Click

        Button2.Text = "Execute"
        OFD.Title = "Please select a datalog file"
        OFD.InitialDirectory = My.Settings.SaveFileFolder
        OFD.Filter = "*.txt|txt files"
        OFD.FileName = "*.txt"
        'OFD.OpenFile()

        If OFD.ShowDialog() = DialogResult.OK Then
            ffd = OFD.FileName.ToString.Trim
            Button2.Enabled = True
        End If

    End Sub

    Private Sub lbFilenameList_MouseDown(sender As Object, e As MouseEventArgs) Handles lbFilenameList.MouseDown

        lbFilenameList.ContextMenuStrip = ContextMenuStrip1

    End Sub

    Private Sub NotepadplusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NotepadplusToolStripMenuItem.Click
        Dim fbd As New FolderBrowserDialog

        NotepadplusToolStripMenuItem.Checked = True

        If NotepadplusToolStripMenuItem.Checked = True Then
            NotepadFolderSelectTSMItem.Checked = False
            AkelPadFolderSelectSMItem.Checked = False
            txtApp = "\notepad++.exe"
        End If

        With fbd
            .SelectedPath = Application.StartupPath
            .Description = "Browse Notepad++ aap folder"
            If .ShowDialog = DialogResult.OK Then
                txtPadFolder = (.SelectedPath)
            End If
            My.Settings.txtPadFolder = txtPadFolder
            My.Settings.txtApplication = txtApp
            My.Settings.Save()
        End With
    End Sub

    Private Sub CheckOnDatalogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckOnDatalogToolStripMenuItem.Click
        DestFolder = My.Settings.SaveFileFolder + "\"
        Dim fname As String = lbFilenameList.SelectedItems(0).Text
        Dim sourceFile As String = m_strDefaultTraceviewPath + fname

        If System.IO.File.Exists(DestFolder + fname) = True Then
            'If file(s) is already exist inside the folder, will proceed to open
            'using selected app.
            GoTo proc

        Else
            ' Cursor = Cursors.WaitCursor
            Me.Text = strAppText + "[ Getting file ready. . . Please wait ]"

            Dim destLoc As String = DestFolder + fname

            Form2.Show()
            File.Copy(sourceFile, Path.Combine(DestFolder, fname), True)

            ' Cursor = Cursors.Default

            lbFilenameList.SelectedItems(0).Selected = False

            Form2.Close()
            Timer1.Enabled = False
            Me.Text = strAppText

        End If

proc:
        ffd = My.Settings.SaveFileFolder + "\" + fname
        mtfSearcher()
        Button2.Text = "DGVToXL"
        Button2.Enabled = True
    End Sub

    Private Sub SavedFileFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SavedFileFolderToolStripMenuItem.Click
        Process.Start("explorer.exe", My.Settings.SaveFileFolder)
    End Sub


    Public Sub mtfSearcher()
        ' Switch to the desired tab
        TabControl1.SelectedIndex = 1

        ' Create a new DataTable and define columns only once
        tmpTable = New DataTable

        ' Define columns for the DataTable
        tmpTable.Columns.Add("DTRptGenerated", GetType(String))
        tmpTable.Columns.Add("DeviceID", GetType(String))
        tmpTable.Columns.Add("Lotno", GetType(String))
        tmpTable.Columns.Add("JobName", GetType(String))
        tmpTable.Columns.Add("PgmName", GetType(String))
        tmpTable.Columns.Add("PgmFolderPath", GetType(String))
        tmpTable.Columns.Add("TesterName", GetType(String))
        tmpTable.Columns.Add("IGXLVer", GetType(String))
        tmpTable.Columns.Add("PartType", GetType(String))
        tmpTable.Columns.Add("PackageType", GetType(String))
        tmpTable.Columns.Add("TestCode", GetType(String))
        tmpTable.Columns.Add("TestTemperature", GetType(String))
        tmpTable.Columns.Add("HdlrType", GetType(String))
        tmpTable.Columns.Add("HdlrID", GetType(String))
        tmpTable.Columns.Add("LBused", GetType(String))
        tmpTable.Columns.Add("SktsUsed", GetType(String))
        tmpTable.Columns.Add("DevNum", GetType(String))
        tmpTable.Columns.Add("RowNum", GetType(String))
        tmpTable.Columns.Add("TestNum", GetType(String))
        tmpTable.Columns.Add("Site", GetType(String))
        tmpTable.Columns.Add("TestName", GetType(String))
        tmpTable.Columns.Add("AlrmOrFail", GetType(String))
        tmpTable.Columns.Add("Test_Data", GetType(String))

        ' Define the search string and split it into words
        Dim s As String = " (F) "
        Dim words As String() = s.Split(New Char() {CChar(vbTab), ";"c})

        ' Process each word
        For Each word As String In words
            m_SearchParamData(word, ffd, tmpTable)
        Next

        ' Update DataGridView if the DataTable is not empty
        If tmpTable.Rows.Count > 0 Then
            DataGridView1.DataSource = tmpTable
            dgvColAutoFit(DataGridView1)
        Else
            MessageBox.Show("I think the DataTable is empty!!!")
        End If

        ' Calculate distinct and total counts in a single iteration
        Dim distinctTestNumCount As Integer = 0
        Dim distinctTestNameCount As Integer = 0
        Dim totalTestNumCount As Integer = 0
        Dim totalTestNameCount As Integer = 0

        Dim distinctTestNums As New HashSet(Of String)()
        Dim distinctTestNames As New HashSet(Of String)()

        For Each row As DataRow In tmpTable.Rows
            Dim testNum As String = row.Field(Of String)("TestNum")
            Dim testName As String = row.Field(Of String)("TestName")

            If Not String.IsNullOrEmpty(testNum) Then
                totalTestNumCount += 1
                distinctTestNums.Add(testNum)
            End If

            If Not String.IsNullOrEmpty(testName) Then
                totalTestNameCount += 1
                distinctTestNames.Add(testName)
            End If
        Next

        distinctTestNumCount = distinctTestNums.Count
        distinctTestNameCount = distinctTestNames.Count

        ' Update UI labels
        Me.Label5.Text = distinctTestNumCount.ToString()
        Me.Label7.Text = distinctTestNameCount.ToString()
        Me.Label9.Text = totalTestNumCount.ToString()
        Me.Label11.Text = totalTestNameCount.ToString()

        ' Update the form's title and enable UI elements
        Me.Text = $"{strAppText} [ Data has been successfully loaded. ]"
        Me.TextBox1.Enabled = True
        Me.Button4.Enabled = True

        ' Dispose of the DataTable
        tmpTable.Dispose()
    End Sub


    Sub CountAlarmsAndFailsBySite(ByVal dt As DataTable, ByVal testName As String)
        ' Create a dictionary to store the counts for each site
        Dim siteCounts As New Dictionary(Of String, (Alarms As Integer, Fails As Integer))

        ' Iterate through the DataTable rows
        For Each row As DataRow In dt.Rows
            ' Check if the TestName matches the desired test name
            If row("TestName").ToString() = testName Then
                ' Get the Site and AlrmOrFail values
                Dim site As String = row("Site").ToString()
                Dim alarmOrFail As String = row("AlrmOrFail").ToString()

                ' Initialize the counts for the site if it doesn't exist in the dictionary
                If Not siteCounts.ContainsKey(site) Then
                    siteCounts(site) = (0, 0)
                End If

                ' Increment the alarm or fail count based on the AlrmOrFail value
                If alarmOrFail = "Alarm" Then
                    siteCounts(site) = (siteCounts(site).Alarms + 1, siteCounts(site).Fails)
                ElseIf alarmOrFail = "Fail" Then
                    siteCounts(site) = (siteCounts(site).Alarms, siteCounts(site).Fails + 1)
                End If
            End If
        Next

        ' Display the results for each site
        For Each kvp As KeyValuePair(Of String, (Alarms As Integer, Fails As Integer)) In siteCounts
            Console.WriteLine($"Site: {kvp.Key}, Alarms: {kvp.Value.Alarms}, Fails: {kvp.Value.Fails}")
        Next
    End Sub

    Function CreateSummaryDataTable() As DataTable

        ' Create the new DataTable
        Dim dtSummary As New DataTable("Summary")
        dtSummary.Columns.Add("Test Number", GetType(String))
        dtSummary.Columns.Add("Test Name", GetType(String))
        dtSummary.Columns.Add("Total Alarms", GetType(Integer))
        dtSummary.Columns.Add("Total Fails", GetType(Integer))
        dtSummary.Columns.Add("Site 0 Alarms", GetType(Integer))
        dtSummary.Columns.Add("Site 0 Fails", GetType(Integer))
        dtSummary.Columns.Add("Site 1 Alarms", GetType(Integer))
        dtSummary.Columns.Add("Site 1 Fails", GetType(Integer))
        dtSummary.Columns.Add("Site 3 Alarms", GetType(Integer))
        dtSummary.Columns.Add("Site 3 Fails", GetType(Integer))

        ' Group the data from the tmpTable by Test Number and Test Name
        Dim testGroups = tmpTable.AsEnumerable().GroupBy(Function(row) New With {
        Key .TestNumber = row("TestNum").ToString(),
        Key .TestName = row("TestName").ToString()
    })

        ' Iterate through the groups and calculate the counts
        For Each grp In testGroups
            Dim totalAlarms = grp.Where(Function(row) row("AlrmOrFail").ToString() = "Alarm").Count()
            Dim totalFails = grp.Where(Function(row) row("AlrmOrFail").ToString() = "Fail").Count()

            ' Initialize site-specific counts
            Dim site0Alarms = grp.Where(Function(row) row("Site").ToString() = "'0" AndAlso row("AlrmOrFail").ToString() = "Alarm").Count()
            Dim site0Fails = grp.Where(Function(row) row("Site").ToString() = "'0" AndAlso row("AlrmOrFail").ToString() = "Fail").Count()
            Dim site1Alarms = grp.Where(Function(row) row("Site").ToString() = "'1" AndAlso row("AlrmOrFail").ToString() = "Alarm").Count()
            Dim site1Fails = grp.Where(Function(row) row("Site").ToString() = "'1" AndAlso row("AlrmOrFail").ToString() = "Fail").Count()
            Dim site3Alarms = grp.Where(Function(row) row("Site").ToString() = "'3" AndAlso row("AlrmOrFail").ToString() = "Alarm").Count()
            Dim site3Fails = grp.Where(Function(row) row("Site").ToString() = "'3" AndAlso row("AlrmOrFail").ToString() = "Fail").Count()

            ' Add a new row to the summary DataTable
            dtSummary.Rows.Add(grp.Key.TestNumber, grp.Key.TestName, totalAlarms, totalFails, site0Alarms, site0Fails, site1Alarms, site1Fails, site3Alarms, site3Fails)
        Next

        ' Return the summary DataTable
        Return dtSummary
    End Function

    Private Sub CreateOrUpdateChartSeries()
        ' Clear existing series (if any)
        Chart3.Series.Clear()

        ' Create series for Total Alarms
        Dim seriesAlarms As New Series("Total Alarms")
        seriesAlarms.ChartType = SeriesChartType.Column
        Chart3.Series.Add(seriesAlarms) ' Add to chart immediately

        ' Create series for Total Fails
        Dim seriesFails As New Series("Total Fails")
        seriesFails.ChartType = SeriesChartType.Column
        Chart3.Series.Add(seriesFails) ' Add to chart immediately

        ' Now populate the series with data from summaryTable
        For Each row As DataRow In summaryTable.Rows
            Chart3.Series("Total Alarms").Points.AddXY(row("Test Number").ToString(), CInt(row("Total Alarms")))
            Chart3.Series("Total Fails").Points.AddXY(row("Test Number").ToString(), CInt(row("Total Fails")))
        Next

        ' Update chart appearance
        Chart3.ChartAreas(0).RecalculateAxesScale()
        Chart3.Invalidate()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim txtFind As String = TextBox1.Text
        Dim testNameExists As Boolean = tmpTable.AsEnumerable().Any(Function(row) row("TestName").ToString() = txtFind)

        If testNameExists Then
            CountAlarmsAndFailsBySite(tmpTable, txtFind)
        Else
            If MsgBox($"Test name not found. Do you want to retry?", MsgBoxStyle.YesNo, "Test Name Not Found") = MsgBoxResult.Yes Then
                TextBox1.Clear()
                TextBox1.Focus()
            End If
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Me.Button3.Enabled = True
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        summaryTable = CreateSummaryDataTable()

        ' After populating summaryTable:
        CreateOrUpdateChartSeries() ' Create or update the chart series

        ' Clear existing series
        Chart3.Series.Clear()

        ' Create a new series for Total Alarms
        Dim seriesAlarms As New Series("Total Alarms")
        seriesAlarms.ChartType = SeriesChartType.Column

        ' Create a new series for Total Fails
        Dim seriesFails As New Series("Total Fails")
        seriesFails.ChartType = SeriesChartType.Column

        ' Populate the series with data from the summary table
        For Each row As DataRow In summaryTable.Rows
            seriesAlarms.Points.AddXY(row("Test Number").ToString(), CInt(row("Total Alarms")))
            seriesFails.Points.AddXY(row("Test Number").ToString(), CInt(row("Total Fails")))
        Next

        ' Add the series to the chart
        Chart3.Series.Add(seriesAlarms)
        Chart3.Series.Add(seriesFails)

        ' Optionally, set chart title and other properties
        Chart3.Titles.Clear()
        Chart3.Titles.Add("Main Test Failure (MTF)")
        Chart3.ChartAreas(0).AxisX.Title = "Test Number"
        Chart3.ChartAreas(0).AxisY.Title = "Total Count"

        ' Ensure all test names are displayed on the X-axis with dynamic adjustments
        Dim numLabels As Integer = summaryTable.Rows.Count
        If numLabels > 20 Then
            Chart3.ChartAreas(0).AxisX.LabelStyle.Angle = -90 ' Rotate labels vertically for many labels
            Chart3.ChartAreas(0).AxisX.LabelStyle.Font = New Font("Arial", 8) ' Smaller font for many labels
        ElseIf numLabels > 10 Then
            Chart3.ChartAreas(0).AxisX.LabelStyle.Angle = -45 ' Rotate labels at an angle for moderate number of labels
            Chart3.ChartAreas(0).AxisX.LabelStyle.Font = New Font("Arial", 10)
        Else
            Chart3.ChartAreas(0).AxisX.LabelStyle.Angle = 0 ' No rotation for few labels
            Chart3.ChartAreas(0).AxisX.LabelStyle.Font = New Font("Arial", 12)
        End If

        Chart3.ChartAreas(0).AxisX.Interval = 1 ' Display every label
        Chart3.ChartAreas(0).AxisX.LabelStyle.IsStaggered = False
        Chart3.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        Chart3.ChartAreas(0).AxisX.LabelStyle.TruncatedLabels = False

        Chart3.ChartAreas(0).AxisX.ScaleView.Zoomable = True
        Chart3.ChartAreas(0).AxisX.ScrollBar.Enabled = True
        Chart3.ChartAreas(0).AxisX.ScrollBar.Size = 14

        ' Optionally adjust other properties to improve visualization
        Chart3.ChartAreas(0).AxisX.IsLabelAutoFit = False

        'DISPLAY SUMMARY
        DataGridView1.DataSource = summaryTable
    End Sub

    Private Sub FilterAndUpdateChart()
        Dim selectedFilter As String = ComboBox1.SelectedItem.ToString()
        Dim view As List(Of DataRow)  ' Change the type here to List(Of DataRow)
        view = summaryTable.AsEnumerable().ToList() 'Initialize the variable

        ' Apply filtering based on the selected option
        Select Case selectedFilter
            Case "Top 3"
                view = view.OrderByDescending(Function(row) CInt(row("Total Fails"))).Take(3).ToList()
            Case "Top 5"
                view = view.OrderByDescending(Function(row) CInt(row("Total Fails"))).Take(5).ToList()
            Case "Top 10"
                view = view.OrderByDescending(Function(row) CInt(row("Total Fails"))).Take(10).ToList()
            Case Else
                ' No filtering needed 
        End Select

        'Clear Chart 
        Chart3.Series("Total Alarms").Points.Clear()
        Chart3.Series("Total Fails").Points.Clear()
        ' Populate chart with (potentially) filtered data
        For Each row In view
            Chart3.Series("Total Alarms").Points.AddXY(row("Test Number"), CInt(row("Total Alarms")))
            Chart3.Series("Total Fails").Points.AddXY(row("Test Number"), CInt(row("Total Fails")))
        Next

        ' Update chart appearance
        Chart3.ChartAreas(0).RecalculateAxesScale()
        Chart3.Invalidate()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If summaryTable IsNot Nothing Then
            FilterAndUpdateChart()
        End If
    End Sub

    Public Sub m_SearchParamData(ByVal strtofind As String, ByVal strpath As String, ByVal tmptable As DataTable)
        Dim lines() As String = File.ReadAllLines(strpath)
        Dim intTotalLines As Integer = lines.Length

        ' Pre-compile the regex pattern
        Dim strParamtofind As String = "\s\s\([FA]\)\s[-+0-9]"
        Dim paramRegex As Regex = New Regex(strParamtofind, RegexOptions.Compiled)

        ' Pre-declare and initialize variables
        Dim strTemp() As String
        Dim DTrptGenerated As String = String.Empty
        Dim xlpgmName As String = String.Empty
        Dim xlJobName As String = String.Empty
        Dim CustLotID As String = String.Empty
        Dim Station As String = String.Empty
        Dim PartType As String = String.Empty
        Dim PgmFolderPath As String = String.Empty
        Dim IGXLver As String = String.Empty
        Dim DeviceID As String = String.Empty
        Dim PackageType As String = String.Empty
        Dim TestCode As String = String.Empty
        Dim TestTemp As String = String.Empty
        Dim hdlrType As String = String.Empty
        Dim hdlrID As String = String.Empty
        Dim ldBrdID As String = String.Empty
        Dim socketIDs As String = String.Empty
        Dim DevNum As String = String.Empty
        Dim TestNum As String = String.Empty
        Dim Site As String = String.Empty
        Dim TestName As String = String.Empty
        Dim Test_Data As String = String.Empty
        Dim AlrmOrFail As String = String.Empty
        Dim Rownum As Integer

        Dim rows As New List(Of DataRow)

        ' Process each line
        For intCounter As Integer = 0 To intTotalLines - 1
            Dim strline As String = lines(intCounter)

            'Date and time report
            If strline.Contains("Datalog report") Then
                strline = lines(intCounter + 1)
                strTemp = strline.Split(" "c)
                DTrptGenerated = strTemp(0) & " " & strTemp(1)
            End If

            ' Various metadata
            Select Case True
                Case strline.Contains("      Prog Name:")
                    xlpgmName = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("       Job Name:")
                    xlJobName = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("            Lot:")
                    CustLotID = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("      Node Name:")
                    Station = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("      Part Type:")
                    PartType = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("        AuxFile:")
                    PgmFolderPath = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("       ExecType:")
                    IGXLver = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("       FamilyID:")
                    DeviceID = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("        PkgType:")
                    PackageType = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("       TestCode:")
                    TestCode = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("        TstTemp:")
                    TestTemp = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("       HandType:")
                    hdlrType = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("         HandID:")
                    hdlrID = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("         LoadID:")
                    ldBrdID = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("         ContID:")
                    socketIDs = strline.Split(":"c)(1).TrimStart()
                Case strline.Contains("    Device#: ")
                    DevNum = "'" & strline.Split(":"c)(1).Trim()
            End Select

            ' Process test data
            If paramRegex.IsMatch(strline) Then
                Dim tmp As String = paramRegex.Match(strline).Value
                strTemp = strline.Split(" "c)
                TestNum = strTemp(1)
                Test_Data = strline
                Site = "'" & strTemp(4) & strTemp(5) & strTemp(6) & strTemp(7)

                TestName = String.Join(" ", strTemp.Skip(8).Take(8)).Trim()
                AlrmOrFail = If(strTemp.Contains("(F)"), "Fail", "Alarm")
                Rownum = intCounter

                ' Add to rows list
                Dim newRow As DataRow = tmptable.NewRow()
                newRow.ItemArray = New Object() {
                DTrptGenerated, DeviceID, CustLotID, xlJobName, xlpgmName, PgmFolderPath, Station, IGXLver,
                PartType, PackageType, TestCode, TestTemp, hdlrType, hdlrID, ldBrdID, socketIDs,
                DevNum, Rownum, TestNum, Site, TestName, AlrmOrFail, Test_Data
            }
                rows.Add(newRow)
            End If
        Next

        ' Batch add rows to DataTable
        If rows.Count > 0 Then
            tmptable.BeginLoadData()
            For Each row As DataRow In rows
                tmptable.Rows.Add(row)
            Next
            tmptable.EndLoadData()
        End If

        Me.Text = $"{strAppText} [ Done search. Fetching data To DGV...Please wait. ]"
    End Sub




    Private Sub Form1_MouseHover(sender As Object, e As EventArgs) Handles Me.MouseHover
        Me.Text = strAppText
    End Sub

    Sub expttoXL(ByVal strSavePath As String)

        Dim m_Date As String = Format(Now, "dd-MMM-yyyy")
        Dim m_Time As String = Format(Now, "HH-mm-ss")

        'verfying the datagridview having data or not
        If ((DataGridView1.Columns.Count = 0) Or (DataGridView1.Rows.Count = 0)) Then
            Exit Sub
        End If

        'Creating dataset to export
        Dim dset As New DataSet
        'add table to dataset
        dset.Tables.Add()
        'add column to that table
        For i As Integer = 0 To DataGridView1.ColumnCount - 1
            dset.Tables(0).Columns.Add(DataGridView1.Columns(i).HeaderText)
        Next
        'add rows to the table
        Dim dr1 As DataRow
        For i As Integer = 0 To DataGridView1.RowCount - 1
            dr1 = dset.Tables(0).NewRow
            For j As Integer = 0 To DataGridView1.Columns.Count - 1
                dr1(j) = DataGridView1.Rows(i).Cells(j).Value
            Next
            dset.Tables(0).Rows.Add(dr1)
        Next

        'Dim ExcelApp As Object
        Dim ExcelApp As Excel.Application
        Dim misValue As Object = System.Reflection.Missing.Value
        'ExcelApp = CreateObject("Excel.Application")
        ExcelApp = New Excel.Application
        Dim wBook As Excel.Workbook
        Dim wSheet As Excel.Worksheet
        'wSheet = Nothing
        'Create chart objects
        'Dim oChart As Excel.Chart
        'Dim MyCharts As Excel.ChartObjects '= CType(wSheet, Excel.ChartObjects)
        'Dim MyCharts1 As Excel.ChartObject
        'MyCharts = CType(wSheet, Excel.ChartObjects)

        ''set chart location
        'MyCharts1 = MyCharts.Add(500, 30, 400, 250)
        'oChart = MyCharts1.Chart

        wBook = ExcelApp.Workbooks.Add()
        wSheet = wBook.ActiveSheet()
        wSheet.Name = "Raw Data"

        Dim dt As System.Data.DataTable = dset.Tables(0)
        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        For Each dc In dt.Columns
            colIndex = colIndex + 1
            ExcelApp.Cells(1, colIndex) = dc.ColumnName
        Next

        For Each dr In dt.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In dt.Columns
                colIndex = colIndex + 1
                ExcelApp.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)

            Next
        Next

        With wSheet
            ' Set the column headers and desired formatting for the spreadsheet.
            .Columns().ColumnWidth = 15
            .Range("A1:W1").Font.Bold = True
            .Range("A1:W1").Font.Size = 11
            .Range("A1:W1").Font.Color = RGB(255, 255, 255)
            .Range("A1:W1").Interior.Color = RGB(0, 51, 102)
        End With

        Dim xlsRange As Excel.Range = wSheet.UsedRange

        xlsRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous
        xlsRange.Borders.Weight = Excel.XlBorderWeight.xlThin

        wSheet.Columns.AutoFit()

        Dim lRow As Long = wSheet.Range("R" & wSheet.Rows.Count).End(Excel.XlDirection.xlUp).Row
        '' first get range of cells from sheet 1 that will be used by pivot
        Dim xlRange As Excel.Range = CType(wSheet, Excel.Worksheet).Range("A1:V" + CStr(lRow))

        '' create second sheet
        If ExcelApp.Application.Sheets.Count() < 2 Then
            wSheet = CType(wBook.Worksheets.Add(), Excel.Worksheet)
        Else
            wSheet = ExcelApp.Worksheets(2)
        End If

        wSheet.Name = "MTF Summary"

        'Dim devID As String = xlRange.Cells("B2").ToString()
        '' specify first cell for pivot table on the second sheet
        Dim xlRange2 As Excel.Range = CType(wSheet, Excel.Worksheet).Range("E3")
        'Create range for chart
        Dim xlRange3 As Excel.Range = CType(wSheet, Excel.Worksheet).Range("U1", "U" + CStr(lRow))
        '' Create pivot cache and table
        Dim ptCache As Excel.PivotCache = wBook.PivotCaches.Add(Excel.XlPivotTableSourceType.xlDatabase, xlRange)
        ' Create pivot cache and table
        Dim ptTable As Excel.PivotTable = wSheet.PivotTables.Add(PivotCache:=ptCache, TableDestination:=xlRange2, TableName:="Pivot Table")

        ' create Pivot Field, note that pivot field name is the same as column name in sheet 1
        Dim ptField As Excel.PivotField = ptTable.PivotFields("TestName")

        'Add each MTF on Rows
        ptField = ptTable.PivotFields("TestName")
        With ptField
            .Orientation = Excel.XlPivotFieldOrientation.xlRowField
            .Position = 1
        End With

        'Add each MTF on added Rows > optional
        'ptField = ptTable.PivotFields("TestName")
        'With ptField
        '    .Orientation = Excel.XlPivotFieldOrientation.xlRowField
        '    .Position = 2

        'End With

        ' add each Sites on Column
        ptField = ptTable.PivotFields("Site")
        With ptField
            .Orientation = Excel.XlPivotFieldOrientation.xlColumnField
            '.Position = 1
        End With

        ' add each Sites on Column
        ptField = ptTable.PivotFields("AlrmOrFail")
        With ptField
            .Orientation = Excel.XlPivotFieldOrientation.xlColumnField
            '.Position = 1
        End With

        'Count failing Test Parameters
        ptField = ptTable.AddDataField(ptTable.PivotFields("TestNum"), "TEST FAILURE SUMM", Excel.XlConsolidationFunction.xlCount)

        'Test number
        ptField = ptTable.PivotFields("TestNum")
        With ptField
            .Orientation = Excel.XlPivotFieldOrientation.xlRowField
            '.Calculation = Excel.XlPivotFieldCalculation.xlPercentOfColumn
            '.Position = 1
        End With

        'ptField = ptTable.PivotFields("TestName")
        'With ptField
        '    .Orientation = Excel.XlPivotFieldOrientation.xlColumnField
        '    .Calculation = Excel.XlPivotFieldCalculation.xlPercentOfColumn
        '    '.Position = 1
        'End With
        'chart continuition

        'With oChart
        '    'set data range for chart
        '    Dim chartRange As Excel.Range
        '    chartRange = wSheet.Range("U1", "U" + CStr(lRow)) 'xlRange3
        '    .SetSourceData(chartRange)
        '    'set how you want to draw chart i.e column wise or row wise
        '    .PlotBy = Excel.XlRowCol.xlColumns
        '    'set data lables for bars
        '    .ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowNone)
        '    'set legend to be displayed or not
        '    .HasLegend = True
        '    'set legend location
        '    .Legend.Position = Excel.XlLegendPosition.xlLegendPositionRight
        '    'select chart type
        '    '.ChartType = Excel.XlChartType.xl3DBarClustered
        '    'chart title
        '    .HasTitle = True
        '    .ChartTitle.Text = "MTF"
        '    'set titles for Axis values and categories
        '    Dim xlAxisCategory, xlAxisValue As Excel.Axes
        '    xlAxisCategory = CType(oChart.Axes(, Excel.XlAxisGroup.xlPrimary), Excel.Axes)
        '    xlAxisCategory.Item(Excel.XlAxisType.xlCategory).HasTitle = True
        '    xlAxisCategory.Item(Excel.XlAxisType.xlCategory).AxisTitle.Characters.Text = "TestName"
        '    xlAxisValue = CType(oChart.Axes(, Excel.XlAxisGroup.xlPrimary), Excel.Axes)
        '    xlAxisValue.Item(Excel.XlAxisType.xlValue).HasTitle = True
        '    xlAxisValue.Item(Excel.XlAxisType.xlValue).AxisTitle.Characters.Text = "TestName Count"
        'End With


        'Put some details needed on the Pivot table summary
        Dim DevITMS As String = "ITMSDeviceName: "
        wBook.Sheets("MTF Summary").Cells(3, 1).value = DevITMS
        wBook.Sheets("MTF Summary").Cells(3, 2).value = wBook.Sheets("Raw Data").cells(2, 9).value 'Cells(row,col)

        Dim CustDev As String = "Cust Device: "
        wBook.Sheets("MTF Summary").Cells(4, 1).value = CustDev
        wBook.Sheets("MTF Summary").Cells(4, 2).value = wBook.Sheets("Raw Data").cells(2, 2).value 'Cells(row,col)

        Dim Testpgm As String = "Test Pgm Name: "
        wBook.Sheets("MTF Summary").Cells(5, 1).value = Testpgm
        wBook.Sheets("MTF Summary").Cells(5, 2).value = wBook.Sheets("Raw Data").cells(2, 4).value 'Cells(row,col)

        Dim Lotno As String = "Lot Number: "
        wBook.Sheets("MTF Summary").Cells(6, 1).value = Lotno
        wBook.Sheets("MTF Summary").Cells(6, 2).value = wBook.Sheets("Raw Data").cells(2, 3).value 'Cells(row,col)

        Dim Station As String = "Test Station: "
        wBook.Sheets("MTF Summary").Cells(7, 1).value = Station
        wBook.Sheets("MTF Summary").Cells(7, 2).value = wBook.Sheets("Raw Data").cells(2, 7).value 'Cells(row,col)

        Dim TestMode As String = "Test Code: "
        wBook.Sheets("MTF Summary").Cells(8, 1).value = TestMode
        wBook.Sheets("MTF Summary").Cells(8, 2).value = wBook.Sheets("Raw Data").cells(2, 11).value 'Cells(row,col)

        Dim TestTemp As String = "Test Temperature: "
        wBook.Sheets("MTF Summary").Cells(9, 1).value = TestTemp
        wBook.Sheets("MTF Summary").Cells(9, 2).value = wBook.Sheets("Raw Data").cells(2, 12).value 'Cells(row,col)

        Dim DataGen As String = "Datalog Generated: "
        wBook.Sheets("MTF Summary").Cells(10, 1).value = DataGen
        wBook.Sheets("MTF Summary").Cells(10, 2).value = wBook.Sheets("Raw Data").cells(2, 1).value 'Cells(row,col)

        '***************************************************
        'Format the table
        Dim sh2Range As Excel.Range = wSheet.Range("A3:B10")

        sh2Range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous
        sh2Range.Borders.Weight = Excel.XlBorderWeight.xlThin

        With wSheet
            .Range("A3:A10").HorizontalAlignment = Excel.Constants.xlRight
            .Range("B3:B10").HorizontalAlignment = Excel.Constants.xlLeft
            .Range("A3:A10").Font.Bold = True
            .Range("A3:A10").Interior.Color = RGB(221, 235, 237)
            .Range("B3:B10").Font.Italic = True
        End With

        wSheet.Columns.AutoFit()

        'We'll save the file
        Dim strFileName As String = strSavePath & "\DataSearchResult-" & m_Date & "-" & m_Time & ".xlsx"
        Dim blnFileOpen As Boolean = False
        Try
            Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
            fileTemp.Close()
        Catch ex As Exception
            blnFileOpen = False
        End Try

        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If

        wBook.SaveAs(strFileName)
        ExcelApp.Workbooks.Open(strFileName)
        ExcelApp.Visible = True

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        'counter = counter + 1
        'Me.Label3.Text = strAppText + " [ Loading failure list in " + CStr(counter) + "sec ]"

    End Sub

    Private Sub TimerEvent(ByVal source As Object, ByVal e As ElapsedEventArgs)

        Console.WriteLine("Event Raised at {0:HH:mm:ss.fff}", e.SignalTime)

    End Sub
    'Private Sub releaseObject(ByVal obj As Object)
    '    Try
    '        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
    '        obj = Nothing
    '    Catch ex As Exception
    '        obj = Nothing
    '        MessageBox.Show("Exception Occured while releasing object " + ex.ToString())
    '    Finally
    '        GC.Collect()
    '    End Try
    'End Sub
    Private Sub Charting()



    End Sub


End Class
