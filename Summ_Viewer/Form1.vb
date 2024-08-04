Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Collections 'For arraylist
Imports System.ComponentModel
Imports System.Timers

Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System.Diagnostics.Eventing.Reader
'Imports Interop.office

Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Diagnostics.Eventing
'Imports Microsoft ' Interop.Excel

'Imports Microsoft.Office
'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Excel

Public Class Form1

    Private summaryTable As DataTable

    Public tmpTable As DataTable

    Dim tempFolder, DestFolder, txtPadFolder, STDF_Viewer, txtApp, ffd, m_fname As String
    Dim totalTested As Integer

    Dim fbd As FolderBrowserDialog

    Dim bolNotepad, bolAkelPad As Boolean

    Private Declare Function SetForegroundWindow Lib "User32.dll" (ByVal handle As IntPtr) As Boolean

    Private m_intMarqueeCounter As Integer = 1
    Private m_bolMarqueeIncrementUp As Boolean = True


    Private m_lstFileNames As New List(Of String)
    Private dctNameToLocation As New Dictionary(Of String, String)

    Dim counter As Integer
    Dim timer As Timer = New Timer

    Dim word As String

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged


        If Not txtSearch.Text = "SEARCH" Then
            txtSearch.ForeColor = Color.Black
        End If

        If txtSearch.TextLength = 0 Then
            txtSearch.Text = "SEARCH"
            txtSearch.ForeColor = Color.Gray
            txtSearch.SelectAll()
        End If

        'If txtSearch.Text.Substring(0) = "I" Or txtSearch.Text.Substring(0) = "i" Then
        '    cmbServerSelect.SelectedIndex = 0
        'ElseIf txtSearch.Text.Substring(0) = "U" Or txtSearch.Text.Substring(0) = "u" Then
        '    cmbServerSelect.SelectedIndex = 1

        'End If


    End Sub

    Private Sub txtSearch_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown

        TabControl1.SelectedIndex = 0

        lbFilenameList.Items.Clear()

        If txtSearch.Text.Count > 0 Then

            If e.KeyCode = Keys.Enter Then

                Me.Text = strAppText + "   " + "[ Loading files. ]"

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

            'Button1.Enabled = True

        End If

        'lbLVtotRows.Text = "TOTAL NUMBER OF FILES: " + CStr(getLBxLastRow(ListView1))

    End Sub



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

        lbLVtotRows.Text = "TOTAL NUMBER OF FILES: " + CStr(getLBxLastRow(lbFilenameList))

    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Label3.Text = "NO FILENAME SELECTED"
        lbDGVtotalRows.Text = "TOTAL ROWS: 0"
        lbLVtotRows.Text = "TOTAL NUMBER OF FILES: 0"

        Dim servers() As String = New String() {"IFLEX SERVER", "UFLEX SERVER", "SYNTIANT SERVER"}
        cmbServerSelect.Items.AddRange(servers)
        cmbServerSelect.SelectedIndex = 0

        Dim failNtestnum() As String = New String() {"SEARCH ALARM/FAIL", "SEARCH TESTNUM"}
        cmbAlarmFail.Items.AddRange(failNtestnum)
        cmbAlarmFail.SelectedIndex = 0

        Dim Chart2Filters() As String = New String() {"Select Filter", "Top 3", "Top 5", "Top 10"}
        ComboBox1.Items.AddRange(Chart2Filters)
        ComboBox1.SelectedIndex = 0

        Dim Chart2xAxis() As String = New String() {"TestNum", "TesTName"}
        ComboBox2.Items.AddRange(Chart2xAxis)
        ComboBox2.SelectedIndex = 0

        Button3.Enabled = False
        'TabControl1.TabPages.Remove(TabPage3)

        'Button1.Enabled = False

        'If Not lbFilenameList.Items.Count = 0 Then

        '    Button1.Enabled = True

        'End If

        Me.Text = strAppText

        cmbAlarmFail.SelectedIndex = 0
        txtTestNum.Enabled = False

        txtSearch.Text = "SEARCH"
        txtSearch.ForeColor = Color.Gray


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

        'Timer2.Enabled = False

        ' login2Server()

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

    'Private Sub rbIflex_CheckedChanged(sender As Object, e As EventArgs)
    '    If rbIflex.Checked = True Then
    '        m_strDefaultTraceviewPath = "\\10.83.133.10\iflex\iflex_summary\"
    '        rbUflex.Checked = False
    '        rbSyntiant.Checked = False
    '        txtSearch.Select()
    '    ElseIf rbUflex.Checked = True Then
    '        m_strDefaultTraceviewPath = "\\10.83.133.10\uflex\uflex_summary\"
    '        rbIflex.Checked = False
    '        rbSyntiant.Checked = False
    '        txtSearch.Select()
    '        'ElseIf rbSyntiant.Checked = True Then
    '        '    txtSearch.Select()
    '        '    m_strDefaultTraceviewPath = "\\10.83.133.10\uflex\uflex_summary\Syntiant\"
    '        '    rbIflex.Checked = False
    '        '    rbUflex.Checked = False

    '    End If
    'End Sub


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




    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    '    If Not lbFilenameList.Items.Count = 0 Then

    '        Button1.Enabled = True

    '    End If

    '    lbFilenameList.Items.Clear()

    '    Me.Text = strAppText + "[ Loading files. . . ]"
    '    TabControl1.SelectedIndex = 0

    '    Dim t As String = Trim(txtSearch.Text).ToUpper
    '    lbFilenameList.Columns.Clear()
    '    lbFilenameList.Items.Clear()
    '    'listAllSumm(t)
    '    fileList(t)
    '    'End If
    '    Me.Text = strAppText


    'End Sub

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
        Dim strtestnum As String

        If cmbAlarmFail.SelectedIndex = 0 Then

            word = "\s+\([FA]\)\s[-+0-9N][\dx.][\dx.A ]"

        End If

        If cmbAlarmFail.SelectedIndex = 1 Then

            strtestnum = "^ " & txtTestNum.Text & " +[0-9]"
            word = strtestnum

        End If

        If Button2.Text = "Execute" Then
            Me.Text = strAppText + "   [ Please wait while searching for data from selected file. ]"
            mtfSearcher()

            If DataGridView1.Rows.Count > 1 Then
                Button3.Enabled = True
            Else
                button3.Enabled = False
            End If
            Button2.Text = "DGVToXL"

            Me.Text = strAppText
        ElseIf Button2.Text = "DGVToXL" Then

            Me.Text = strAppText + "   [ Please wait while exporting the data into Excel file. ]"

            expttoXL(fd)

            Me.Text = strAppText
            Button2.Text = "Execute"
            Button2.Enabled = False
            Button3.Enabled = False
        End If

        lbDGVtotalRows.Text = "TOTAL ROWS: " + CStr(getDGVLastRow(DataGridView1))

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

        Label3.Text = "SELECTED SUMM FNAME: " + GetFileName(ffd)
        Label3.BackColor = Color.LightYellow
        m_fname = GetFileName(ffd)
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

        'If rbIflex.Checked = True Then
        '    m_strDefaultTraceviewPath = "\\10.83.133.10\iflex\iflex_summary\"

        'ElseIf rbUflex.Checked = True Then
        '    m_strDefaultTraceviewPath = "\\10.83.133.10\uflex\uflex_summary\"

        'Else
        '    m_strDefaultTraceviewPath = "\\10.83.133.10\uflex\uflex_summary\Syntiant\"

        'End If

        Dim sourceFile As String = m_strDefaultTraceviewPath + fname



        If System.IO.File.Exists(DestFolder + fname) = True Then
            'If file(s) is already exist inside the folder, will proceed to open
            'using selected app.
            GoTo proc

        Else
            ' Cursor = Cursors.WaitCursor
            Me.Text = strAppText + "[ Getting file ready. . . Please wait ]"

            Dim destLoc As String = DestFolder + fname


            File.Copy(sourceFile, Path.Combine(DestFolder, fname), True)

            ' Cursor = Cursors.Default

            lbFilenameList.SelectedItems(0).Selected = False


            Timer1.Enabled = False
            Me.Text = strAppText

        End If

proc:
        ffd = My.Settings.SaveFileFolder + "\" + fname
        Label3.Text = "SELECTED SUMM FNAME: " + GetFileName(ffd)
        Label3.BackColor = Color.LightYellow
        m_fname = fname

        Dim strtestnum As String

        If cmbAlarmFail.SelectedIndex = 0 Then

            word = "\s+\([FA]\)\s[-+0-9N][\dx.][\dx.A ]"

        End If

        If cmbAlarmFail.SelectedIndex = 1 Then

            strtestnum = "^ " & txtTestNum.Text & " +[0-9]"
            word = strtestnum

        End If

        Me.Text = strAppText + "   [ Please wait while searching for data from selected file. ]"

        mtfSearcher()

        lbDGVtotalRows.Text = "TOTAL ROWS: " + CStr(getDGVLastRow(DataGridView1))
        Me.Text = strAppText
        Button2.Text = "DGVToXL"
        Button2.Enabled = True
    End Sub

    Private Sub SavedFileFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SavedFileFolderToolStripMenuItem.Click
        Process.Start("explorer.exe", My.Settings.SaveFileFolder)
    End Sub


    Sub mtfSearcher()


        TabControl1.SelectedIndex = 1

        tmpTable = New DataTable

        If tmpTable.Rows.Count > 0 Then
            tmpTable.Rows.Clear() 'clear all data from the table
        End If

        tmpTable.Columns.Add("DTRptGenerated", GetType(System.String), Nothing) ' time of report generated
        tmpTable.Columns.Add("DeviceID", GetType(System.String), Nothing) 'Device ID
        tmpTable.Columns.Add("Lotno", GetType(System.String), Nothing) 'Customer lot number
        tmpTable.Columns.Add("JobName", GetType(System.String), Nothing) 'Load filename
        tmpTable.Columns.Add("PgmName", GetType(System.String), Nothing) 'Excel pgm name
        tmpTable.Columns.Add("PgmFolderPath", GetType(System.String), Nothing) ' Folder path
        tmpTable.Columns.Add("TesterName", GetType(System.String), Nothing) 'what test station
        tmpTable.Columns.Add("IGXLVer", GetType(System.String), Nothing) 'IGXL version
        tmpTable.Columns.Add("PartType", GetType(System.String), Nothing) 'Device name
        tmpTable.Columns.Add("PackageType", GetType(System.String), Nothing) 'Package type
        tmpTable.Columns.Add("TestCode", GetType(System.String), Nothing) 'If FT or QA
        tmpTable.Columns.Add("TestTemperature", GetType(System.String), Nothing) 'what temperature
        tmpTable.Columns.Add("HdlrType", GetType(System.String), Nothing) 'Hdlr type
        tmpTable.Columns.Add("HdlrID", GetType(System.String), Nothing) 'Hdlr ID
        tmpTable.Columns.Add("LBused", GetType(System.String), Nothing) 'Loadboard used
        tmpTable.Columns.Add("SktsUsed", GetType(System.String), Nothing) 'Socket used
        tmpTable.Columns.Add("DevNum", GetType(System.String), Nothing) 'Device number
        tmpTable.Columns.Add("RowNum", GetType(System.String), Nothing) 'row number
        tmpTable.Columns.Add("TestNum", GetType(System.String), Nothing) 'What test number
        tmpTable.Columns.Add("Site", GetType(System.String), Nothing) 'what Site
        tmpTable.Columns.Add("TestName", GetType(System.String), Nothing) 'Test name
        tmpTable.Columns.Add("AlrmOrFail", GetType(System.String), Nothing) 'Alarm or Fail?
        tmpTable.Columns.Add("Test_Data", GetType(System.String), Nothing) 'Test number

        'Dim folderDir As String = SaveFileFolder + "\summToSearch"

        'Dim s As String = " (F) "

        'Dim words As String() = s.Split(New [Char]() {CChar(vbTab), ";"c}) 's.Split(New [Char]() {";"c})

        'Dim word As St = "\s+\([FA]\)\s[-+0-9N][\dx.][\dx.A]" 'pattern for fail and alarm
        'For Each word In words
        '    m_SearchParamData(word, ffd, tmpTable)
        'Next
        m_SearchParamData(word, ffd, tmpTable)
        'DataGridView1.DataSource = tmpTable




        If tmpTable IsNot Nothing AndAlso tmpTable.Rows.Count > 0 Then
            DataGridView1.DataSource = tmpTable
        Else
            MsgBox("I think the the Datatable is empty!!!")
        End If
        dgvColAutoFit(DataGridView1)
        Me.Text = strAppText + "   " + "[ Data has been Successfullyloaded. ]"

        tmpTable.Dispose()

        'UNIQUE VALUES
        Dim distinctTestNumCount As Integer = tmpTable.AsEnumerable().Select(Function(row) row.Field(Of String)("TestNum")).Distinct().Count()
        Dim distinctTestNameCount As Integer = tmpTable.AsEnumerable().Select(Function(row) row.Field(Of String)("TestName")).Distinct().Count()

        'TOTAL VALUES
        Dim totalTestNumCount As Integer = tmpTable.AsEnumerable().Select(Function(row) row.Field(Of String)("TestNum")).Count()
        Dim totalTestNameCount As Integer = tmpTable.AsEnumerable().Select(Function(row) row.Field(Of String)("TestName")).Count()

        lbTestnumcnt.Text = "Distinct TestNum Cnt: " + CStr(distinctTestNumCount)
        lbTestNameCnt.Text = "Distinct TestName Cnt: " + CStr(distinctTestNameCount)
        lbTestNumTot.Text = "Distinct TestNum Total: " + CStr(totalTestNumCount)
        lbTestNameTot.Text = "Distinct TestName Total: " + CStr(totalTestNameCount)

    End Sub


    Private Sub cmbServerSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbServerSelect.SelectedIndexChanged
        'cmbServerSelect.Items.Clear()



        Select Case cmbServerSelect.SelectedIndex

            Case 0
                m_strDefaultTraceviewPath = "\\10.83.133.10\iflex\iflex_summary\"
                txtSearch.Select()
            Case 1
                m_strDefaultTraceviewPath = "\\10.83.133.10\uflex\uflex_summary\"
                txtSearch.Select()
            Case 2
                m_strDefaultTraceviewPath = "\\10.83.133.10\uflex\uflex_summary\Syntiant\"
                txtSearch.Select()

        End Select

        'cmbServerSelect.Items.Clear()

    End Sub

    Public Sub m_SearchParamData(ByVal strtofind As String, ByVal strpath As String, ByVal tmptable As DataTable)
        Dim lines() As String = File.ReadAllLines(strpath)
        Dim intTotalLines As Integer = lines.Length

        ' Pre-compile the regex pattern
        Dim strParamtofind As String = strtofind '"\s\s\([FA]\)\s[-+0-9]"
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
                    PgmFolderPath = strline.Split(":"c)(1).TrimStart() _
                        + ":" + strline.Split(":"c)(2).TrimStart() 'revised
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

                If ContainsNumbers(strTemp(4) & strTemp(5) & strTemp(6) & strTemp(7)) Then
                    Dim mystr As String = strTemp(4) & strTemp(5) & strTemp(6) & strTemp(7)
                    If Regex.IsMatch(mystr, "0") Then
                        Site = "'" & mystr
                    Else
                        Site = mystr
                    End If

                ElseIf ContainsNumbers(strTemp(8)) Then
                    Dim mystr As String = strTemp(8)
                    If Regex.IsMatch(mystr, "0") Then
                        Site = "'" & mystr
                    Else
                        Site = mystr
                    End If

                ElseIf ContainsNumbers(strTemp(9)) Then
                    Dim mystr As String = strTemp(9)
                    If Regex.IsMatch(mystr, "0") Then
                        Site = "'" & mystr
                    Else
                        Site = mystr
                    End If
                End If
                'Site = "'" & strTemp(4) & strTemp(5) & strTemp(6) & strTemp(7)
                Dim strTestname As String = String.Join(" ", strTemp.Skip(6).Take(9)).Trim()
                If Regex.IsMatch(strTestname.Substring(0).Trim, "\d") Then
                    TestName = strTestname.Substring(1).TrimStart
                Else
                    TestName = strTestname
                End If

                If strTemp.Contains("(F)") Then
                    AlrmOrFail = "Fail"
                ElseIf strTemp.Contains("(A)") Then
                    AlrmOrFail = "Alarm"
                Else
                    AlrmOrFail = "Pass"
                End If
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

        'Label5.Text = "Total Tested: " & CInt(getTotalDev(strpath, intTotalLines))
        totalTested = getTotalDev(strpath, intTotalLines)

        Me.Text = $"{strAppText} [ Done search. Fetching data To DGV...Please wait. ]"
    End Sub

    Function getTotalDev(ByVal strpath As String, ByVal revTotalLines As Integer) As Integer
        Dim lineInrev() As String = File.ReadAllLines(strpath)

        Dim strTempInrev() As String
        Dim devCount As String
        Dim strTemp As String()
        For revCounter As Integer = (revTotalLines - 1) To 0 Step -1
            Dim revline As String = lineInrev(revCounter)


            If revline.Contains("Device#:") Then
                strTempInrev = revline.Split(":"c)
                If Regex.IsMatch(strTempInrev(1), "-") Then
                    devCount = strTempInrev(1)
                    strTemp = devCount.Split("-"c)
                    getTotalDev = CInt(strTemp(1))
                    If getTotalDev Then Exit For

                ElseIf Regex.IsMatch(strTempInrev(1), ",") Then
                    devCount = strTempInrev(1)
                    strTemp = devCount.Split(","c)
                    getTotalDev = CInt(strTemp(1))
                    If getTotalDev Then Exit For

                Else
                    getTotalDev = CInt(strTempInrev(1))
                    If getTotalDev Then Exit For

                End If

            End If
        Next

        Return getTotalDev

    End Function

    Private Function ContainsNumbers(input As String) As Boolean
        Return Regex.IsMatch(input, "[0-9]")
    End Function
    'Public Sub m_SearchParamData(ByVal strtofind As String, ByVal strpath As String, ByVal tmptable As DataTable)

    '    Dim strTemp() As String
    '    Dim lines() As String
    '    Dim strline As String = ""
    '    Dim filename As String = strpath
    '    'folderDir = ffd
    '    'Dim fileList = Directory.GetFiles(ffd, "*.txt", False)
    '    Dim str As String = String.Empty

    '    Dim DTrptGenerated As String = String.Empty
    '    Dim xlpgmName As String = String.Empty
    '    Dim xlJobName As String = String.Empty
    '    Dim CustLotID As String = String.Empty
    '    Dim Station As String = String.Empty
    '    Dim PartType As String = String.Empty
    '    Dim PgmFolderPath As String = String.Empty
    '    Dim IGXLver As String = String.Empty
    '    Dim DeviceID As String = String.Empty
    '    Dim PackageType As String = String.Empty
    '    Dim TestCode As String = String.Empty
    '    Dim TestTemp As String = String.Empty
    '    Dim hdlrType As String = String.Empty
    '    Dim hdlrID As String = String.Empty
    '    Dim ldBrdID As String = String.Empty
    '    Dim socketIDs As String = String.Empty
    '    Dim DevNum As String = String.Empty
    '    Dim TestNum As String = String.Empty
    '    Dim Site As String = String.Empty
    '    Dim TestName As String = String.Empty
    '    Dim Test_Data As String = String.Empty
    '    Dim PassAlrmOrFail As String = String.Empty
    '    Dim Rownum As Integer
    '    'Dim Channel As String = String.Empty
    '    'Dim mLow As String = String.Empty
    '    'Dim Measured As String = String.Empty
    '    'Dim mHigh As String = String.Empty


    '    'For Each fileName In fileList

    '    lines = File.ReadAllLines(filename)
    '    Dim intTotalLines As Integer = lines.Length

    '    Dim lastLine As String = File.ReadLines(filename).LastOrDefault

    '    'Dim strParamtofind As String = "\s\s\([FA]\)\s[-+0-9]" ' ")\s+([\d]*[.]?[\d]*)?([eE][-+]?[0-9]+)?" '\s+[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?
    '    ' Split string based on spaces.
    '    For intCounter = 0 To intTotalLines - 1
    '        strline = lines(intCounter)

    '        'Date and time report
    '        If Regex.IsMatch(strline, "Datalog report") Then

    '            strline = lines(intCounter + 1)
    '            strTemp = Regex.Split(strline, " ")
    '            DTrptGenerated = strTemp(0) + " " + strTemp(1)
    '        End If
    '        'Excel program
    '        If Regex.IsMatch(strline, "      Prog Name:") Then
    '            strTemp = Regex.Split(strline, ": ")
    '            xlpgmName = strTemp(1).TrimStart

    '        End If

    '        'Job name in excel
    '        If Regex.IsMatch(strline, "       Job Name:") Then

    '            strTemp = Regex.Split(strline, ": ")
    '            xlJobName = strTemp(1).TrimStart
    '        End If

    '        'Customer lot number
    '        If Regex.IsMatch(strline, "            Lot:") Then
    '            strTemp = Regex.Split(strline, ": ")
    '            CustLotID = strTemp(1).TrimStart
    '        End If

    '        'Station name
    '        If Regex.IsMatch(strline, "      Node Name:") Then
    '            strTemp = Regex.Split(strline, ": ")
    '            Station = strTemp(1).TrimStart

    '        End If

    '        'Part type
    '        If Regex.IsMatch(strline, "      Part Type:") Then
    '            strTemp = Regex.Split(strline, ": ")
    '            PartType = strTemp(1).TrimStart
    '        End If


    '        'Program Folder Path
    '        If Regex.IsMatch(strline, "        AuxFile:") Then

    '            strTemp = Regex.Split(strline, ": ")
    '            PgmFolderPath = strTemp(1).TrimStart

    '        End If

    '        'IGXL version
    '        If Regex.IsMatch(strline, "       ExecType:") Then

    '            strTemp = Regex.Split(strline, ": ")
    '            IGXLver = strTemp(1).TrimStart
    '        End If

    '        'Device ID
    '        If Regex.IsMatch(strline, "       FamilyID:") Then
    '            strTemp = Regex.Split(strline, ": ")
    '            DeviceID = strTemp(1).TrimStart

    '        End If

    '        'Package type
    '        If Regex.IsMatch(strline, "        PkgType:") Then

    '            strTemp = Regex.Split(strline, ": ")
    '            PackageType = strTemp(1).TrimStart
    '        End If

    '        'Test code
    '        If Regex.IsMatch(strline, "       TestCode:") Then
    '            strTemp = Regex.Split(strline, ": ")
    '            TestCode = strTemp(1).TrimStart
    '        End If

    '        'Test temperature
    '        If Regex.IsMatch(strline, "        TstTemp:") Then

    '            strTemp = Regex.Split(strline, ": ")
    '            TestTemp = strTemp(1).TrimStart

    '        End If

    '        'Handler type
    '        If Regex.IsMatch(strline, "       HandType:") Then

    '            strTemp = Regex.Split(strline, ": ")
    '            hdlrType = strTemp(1).TrimStart
    '        End If

    '        'Handler ID
    '        If Regex.IsMatch(strline, "         HandID:") Then
    '            strTemp = Regex.Split(strline, ": ")
    '            hdlrID = strTemp(1).TrimStart

    '        End If

    '        'Loadboard ID
    '        If Regex.IsMatch(strline, "         LoadID:") Then

    '            strTemp = Regex.Split(strline, ": ")
    '            ldBrdID = strTemp(1).TrimStart
    '        End If

    '        'Socket IDs
    '        If Regex.IsMatch(strline, "         ContID:") Then
    '            strTemp = Regex.Split(strline, ": ")
    '            socketIDs = strTemp(1).TrimStart
    '        End If

    '        'Socket IDs
    '        If Regex.IsMatch(strline, "    Device#: ") Then

    '            strTemp = Regex.Split(strline, ": ")
    '            DevNum = "'" + strTemp(1).Trim
    '        End If
    '        'Dim strParamtofind As String = "^ " + strtofind + " +[0-9]" '"\s\s\([FA]\)\s[-+0-9]" '"\s+\([FA]\)\s[-+0-9N][\dx.][\dx.A]"
    '        'Dim strParamtofind As String = strtofind
    '        If Regex.IsMatch(strline, strtofind) Then
    '            Dim tmp As String = Regex.Match(strline, strtofind).Value
    '            Dim fnd As Boolean = False
    '            Dim param As String = String.Empty
    '            Dim val As String = String.Empty
    '            For Each x As String In tmp.Split(" ")
    '                If Not String.IsNullOrWhiteSpace(x) Then
    '                    If Not fnd Then
    '                        param = x
    '                        fnd = True


    '                        'Test number
    '                        strTemp = Regex.Split(strline, " ")
    '                        TestNum = strTemp(1)
    '                        Test_Data = strline

    '                        'What site
    '                        Site = "'" + strTemp(3) + strTemp(4) + strTemp(5) + strTemp(6) + strTemp(7)
    '                        'Test name
    '                        Dim str1 As String
    '                        str1 = strTemp(8) + " " + strTemp(9) + " " + strTemp(10) + " " + strTemp(11) + " " +
    '                            strTemp(12) + " " + strTemp(13) + " " + strTemp(14) + " " + strTemp(15)
    '                        TestName = str1.Trim

    '                        'Is it alarm or fail?
    '                        If strTemp.Contains("(F)") Then
    '                            PassAlrmOrFail = "Fail"

    '                        ElseIf strTemp.Contains("(A)") Then
    '                            PassAlrmOrFail = "Alarm"

    '                        Else
    '                            PassAlrmOrFail = "Pass"
    '                        End If

    '                        'Row number in the datalog
    '                        Rownum = CStr(intCounter)



    '                    Else

    '                        'val = x
    '                    End If
    '                End If
    '            Next

    '            tmptable.Rows.Add(DTrptGenerated, DeviceID, CustLotID, xlJobName, xlpgmName, PgmFolderPath, Station, IGXLver, PartType, PackageType, TestCode, TestTemp, hdlrType, hdlrID, ldBrdID, socketIDs, DevNum, Rownum, TestNum, Site, TestName, PassAlrmOrFail, Test_Data)

    '        End If

    '        'tmptable.Rows.Add(DTrptGenerated, DeviceID, CustLotID, xlJobName, xlpgmName, PgmFolderPath, Station, IGXLver, PartType, PackageType, TestCode, TestTemp, hdlrType, hdlrID, ldBrdID, socketIDs, strParamtofind, DeviceID, TestNum, Site, TestName, Channel, mLow, Measured, mHigh)

    '    Next
    '    'pBar.PerformStep()
    '    'Next

    '    Charting()

    '    Me.Text = strAppText + "   " + "[ Done search. Fetching data To DGV...Please wait. ]"

    'End Sub


    Private Sub Form1_MouseHover(sender As Object, e As EventArgs) Handles Me.MouseHover
        Me.Text = strAppText
    End Sub

    Private Sub cmbAlarmFail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAlarmFail.SelectedIndexChanged

        Select Case cmbAlarmFail.SelectedIndex
            Case 0
                txtTestNum.Text = ""
                txtTestNum.Enabled = False
                Button2.Text = "Execute"
                txtTestNum.Clear()
            Case 1
                txtTestNum.Enabled = True
                Button2.Text = "Execute"
                txtTestNum.Select()
        End Select

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




        'Put some details needed on the Pivot table summary
        Dim fname As String = "File Name: "
        wBook.Sheets("MTF Summary").Cells(1, 1).value = fname
        wBook.Sheets("MTF Summary").Cells(1, 2).value = m_fname 'show the datalog filename

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
            .Range("A1:A10").HorizontalAlignment = Excel.Constants.xlRight
            .Range("B1:B10").HorizontalAlignment = Excel.Constants.xlLeft
            .Range("A1:A10").Font.Bold = True
            .Range("A3:A10").Interior.Color = RGB(221, 235, 237)
            .Range("B1:B10").Font.Italic = True
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



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        summaryTable = CreateSummaryDataTable()

        'updateChart()
        ' After populating summaryTable:
        'CreateOrUpdateChartSeries() ' Create or update the chart series

        ' Clear existing series
        Chart2.Series.Clear()

        ' Create a new series for Total Alarms
        Dim seriesAlarms As New Series("Total Alarms")
        seriesAlarms.ChartType = SeriesChartType.Column

        ' Create a new series for Total Fails
        Dim seriesFails As New Series("Total Fails")
        seriesFails.ChartType = SeriesChartType.Column
        Select Case ComboBox2.SelectedIndex
            Case 0
                For Each row As DataRow In summaryTable.Rows
                    seriesAlarms.Points.AddXY(row("Test Number").ToString(), CInt(row("Total Alarms")))
                    seriesFails.Points.AddXY(row("Test Number").ToString(), CInt(row("Total Fails")))
                Next
            Case 1
                For Each row As DataRow In summaryTable.Rows
                    seriesAlarms.Points.AddXY(row("Test Name").ToString(), CInt(row("Total Alarms")))
                    seriesFails.Points.AddXY(row("Test Name").ToString(), CInt(row("Total Fails")))
                Next
        End Select


        ' Add the series to the chart
        Chart2.Series.Add(seriesAlarms)
        Chart2.Series.Add(seriesFails)

        ' Optionally, set chart title and other properties
        Chart2.Titles.Clear()
        Chart2.Titles.Add("Main Test Failure (MTF)")
        Chart2.ChartAreas(0).AxisX.Title = "Test Number"
        Chart2.ChartAreas(0).AxisY.Title = "Total Count"

        ' Ensure all test names are displayed on the X-axis with dynamic adjustments
        Dim numLabels As Integer = summaryTable.Rows.Count
        If numLabels > 20 Then
            Chart2.ChartAreas(0).AxisX.LabelStyle.Angle = -90 ' Rotate labels vertically for many labels
            Chart2.ChartAreas(0).AxisX.LabelStyle.Font = New Font("Arial", 8) ' Smaller font for many labels
        ElseIf numLabels > 10 Then
            Chart2.ChartAreas(0).AxisX.LabelStyle.Angle = -45 ' Rotate labels at an angle for moderate number of labels
            Chart2.ChartAreas(0).AxisX.LabelStyle.Font = New Font("Arial", 10)
        Else
            Chart2.ChartAreas(0).AxisX.LabelStyle.Angle = 0 ' No rotation for few labels
            Chart2.ChartAreas(0).AxisX.LabelStyle.Font = New Font("Arial", 12)
        End If

        Chart2.ChartAreas(0).AxisX.Interval = 1 ' Display every label
        Chart2.ChartAreas(0).AxisX.LabelStyle.IsStaggered = False
        Chart2.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        Chart2.ChartAreas(0).AxisX.LabelStyle.TruncatedLabels = False

        Chart2.ChartAreas(0).AxisX.ScaleView.Zoomable = True
        Chart2.ChartAreas(0).AxisX.ScrollBar.Enabled = True
        Chart2.ChartAreas(0).AxisX.ScrollBar.Size = 14

        ' Optionally adjust other properties to improve visualization
        Chart2.ChartAreas(0).AxisX.IsLabelAutoFit = False

        'DISPLAY SUMMARY
        DataGridView1.DataSource = summaryTable
    End Sub

    Sub updateChart()
        ' After populating summaryTable:
        CreateOrUpdateChartSeries() ' Create or update the chart series

        ' Clear existing series
        Chart2.Series.Clear()

        ' Create a new series for Total Alarms
        Dim seriesAlarms As New Series("Total Alarms")
        seriesAlarms.ChartType = SeriesChartType.Column

        ' Create a new series for Total Fails
        Dim seriesFails As New Series("Total Fails")
        seriesFails.ChartType = SeriesChartType.Column

        ' Populate the series with data from the summary table
        Select Case ComboBox2.SelectedIndex
            Case 0
                For Each row As DataRow In summaryTable.Rows
                    seriesAlarms.Points.AddXY(row("Test Number").ToString(), CInt(row("Total Alarms")))
                    seriesFails.Points.AddXY(row("Test Number").ToString(), CInt(row("Total Fails")))
                Next
            Case 1
                For Each row As DataRow In summaryTable.Rows
                    seriesAlarms.Points.AddXY(row("Test Name").ToString(), CInt(row("Total Alarms")))
                    seriesFails.Points.AddXY(row("Test Name").ToString(), CInt(row("Total Fails")))
                Next
        End Select


        ' Add the series to the chart
        Chart2.Series.Add(seriesAlarms)
        Chart2.Series.Add(seriesFails)

        ' Optionally, set chart title and other properties
        Chart2.Titles.Clear()
        Chart2.Titles.Add("Main Test Failure (MTF)")

        Select Case ComboBox2.SelectedIndex
            Case 0
                Chart2.ChartAreas(0).AxisX.Title = "Test Number"
            Case 1
                Chart2.ChartAreas(0).AxisX.Title = "Test Name"
        End Select
        'Chart2.ChartAreas(0).AxisX.Title = "Test Number"
        Chart2.ChartAreas(0).AxisY.Title = "Total Count"

        ' Ensure all test names are displayed on the X-axis with dynamic adjustments
        Dim numLabels As Integer = summaryTable.Rows.Count
        If numLabels > 20 Then
            Chart2.ChartAreas(0).AxisX.LabelStyle.Angle = -90 ' Rotate labels vertically for many labels
            Chart2.ChartAreas(0).AxisX.LabelStyle.Font = New Font("Arial", 8) ' Smaller font for many labels
        ElseIf numLabels > 10 Then
            Chart2.ChartAreas(0).AxisX.LabelStyle.Angle = -45 ' Rotate labels at an angle for moderate number of labels
            Chart2.ChartAreas(0).AxisX.LabelStyle.Font = New Font("Arial", 10)
        Else
            Chart2.ChartAreas(0).AxisX.LabelStyle.Angle = 0 ' No rotation for few labels
            Chart2.ChartAreas(0).AxisX.LabelStyle.Font = New Font("Arial", 12)
        End If

        Chart2.ChartAreas(0).AxisX.Interval = 1 ' Display every label
        Chart2.ChartAreas(0).AxisX.LabelStyle.IsStaggered = False
        Chart2.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        Chart2.ChartAreas(0).AxisX.LabelStyle.TruncatedLabels = False

        Chart2.ChartAreas(0).AxisX.ScaleView.Zoomable = True
        Chart2.ChartAreas(0).AxisX.ScrollBar.Enabled = True
        Chart2.ChartAreas(0).AxisX.ScrollBar.Size = 14

        ' Optionally adjust other properties to improve visualization
        Chart2.ChartAreas(0).AxisX.IsLabelAutoFit = False
    End Sub
    Private Sub TimerEvent(ByVal source As Object, ByVal e As ElapsedEventArgs)

        Console.WriteLine("Event Raised at {0:HH:mm:ss.fff}", e.SignalTime)

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If summaryTable IsNot Nothing Then
            updateChart()
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If summaryTable IsNot Nothing Then
            If ComboBox2.SelectedIndex = 0 Then
                FilterByTestNum()
            End If
            If ComboBox2.SelectedIndex = 1 Then
                FilterByTestName()
            End If
            'FilterByTestName()
        End If
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
    'Private Sub Charting()
    '    With Chart2.Series(0)
    '        .Name = "MTF"
    '        .Font = New Font("Arial", 8, FontStyle.Italic)
    '        .BackGradientStyle = GradientStyle.TopBottom
    '        .Color = Color.Purple
    '        .BackSecondaryColor = Color.Magenta
    '        .IsValueShownAsLabel = True
    '        .LabelBackColor = Color.LightYellow
    '        .LabelForeColor = Color.Blue
    '        .Points.DataBind(tmpTable.DefaultView, "TestName", "TestNum", Nothing)
    '    End With


    'End Sub

    Private Sub lbFilenameList_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lbFilenameList.ItemSelectionChanged
        If lbFilenameList.SelectedItems.Count > 0 Then

            lbRowsNum.Text = "NUMBER OF SELECTED ROW/S: " + CStr(cntSelectedRow(lbFilenameList))

        End If
    End Sub

    Private Sub txtSearch_MouseHover(sender As Object, e As EventArgs) Handles txtSearch.MouseHover
        txtSearch.ResetText()
    End Sub

    Private Sub txtSearch_MouseClick(sender As Object, e As MouseEventArgs) Handles txtSearch.MouseClick
        If txtSearch.Text = "SEARCH" And txtSearch.TextLength > 0 Then
            txtSearch.Text = ""
            txtSearch.ForeColor = Color.Black
        ElseIf txtSearch.TextLength = 0 Then
            txtSearch.Text = "SEARCH"
        End If
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
        dtSummary.Columns.Add("Site 2 Alarms", GetType(Integer))
        dtSummary.Columns.Add("Site 2 Fails", GetType(Integer))
        dtSummary.Columns.Add("Site 3 Alarms", GetType(Integer))
        dtSummary.Columns.Add("Site 3 Fails", GetType(Integer))
        dtSummary.Columns.Add("Site 4 Alarms", GetType(Integer))
        dtSummary.Columns.Add("Site 4 Fails", GetType(Integer))
        dtSummary.Columns.Add("Site 5 Alarms", GetType(Integer))
        dtSummary.Columns.Add("Site 5 Fails", GetType(Integer))
        dtSummary.Columns.Add("Site 6 Alarms", GetType(Integer))
        dtSummary.Columns.Add("Site 6 Fails", GetType(Integer))
        dtSummary.Columns.Add("Site 7 Alarms", GetType(Integer))
        dtSummary.Columns.Add("Site 7 Fails", GetType(Integer))
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
            Dim site0Alarms = grp.Where(Function(row) row("Site").ToString() = "0" AndAlso row("AlrmOrFail").ToString() = "Alarm").Count()
            Dim site0Fails = grp.Where(Function(row) row("Site").ToString() = "0" AndAlso row("AlrmOrFail").ToString() = "Fail").Count()
            Dim site1Alarms = grp.Where(Function(row) row("Site").ToString() = "1" AndAlso row("AlrmOrFail").ToString() = "Alarm").Count()
            Dim site1Fails = grp.Where(Function(row) row("Site").ToString() = "1" AndAlso row("AlrmOrFail").ToString() = "Fail").Count()
            Dim site2Alarms = grp.Where(Function(row) row("Site").ToString() = "2" AndAlso row("AlrmOrFail").ToString() = "Alarm").Count()
            Dim site2Fails = grp.Where(Function(row) row("Site").ToString() = "2" AndAlso row("AlrmOrFail").ToString() = "Fail").Count()
            Dim site3Alarms = grp.Where(Function(row) row("Site").ToString() = "3" AndAlso row("AlrmOrFail").ToString() = "Alarm").Count()
            Dim site3Fails = grp.Where(Function(row) row("Site").ToString() = "3" AndAlso row("AlrmOrFail").ToString() = "Fail").Count()
            Dim site4Alarms = grp.Where(Function(row) row("Site").ToString() = "4" AndAlso row("AlrmOrFail").ToString() = "Alarm").Count()
            Dim site4Fails = grp.Where(Function(row) row("Site").ToString() = "4" AndAlso row("AlrmOrFail").ToString() = "Fail").Count()
            Dim site5Alarms = grp.Where(Function(row) row("Site").ToString() = "5" AndAlso row("AlrmOrFail").ToString() = "Alarm").Count()
            Dim site5Fails = grp.Where(Function(row) row("Site").ToString() = "5" AndAlso row("AlrmOrFail").ToString() = "Fail").Count()
            Dim site6Alarms = grp.Where(Function(row) row("Site").ToString() = "6" AndAlso row("AlrmOrFail").ToString() = "Alarm").Count()
            Dim site6Fails = grp.Where(Function(row) row("Site").ToString() = "6" AndAlso row("AlrmOrFail").ToString() = "Fail").Count()
            Dim site7Alarms = grp.Where(Function(row) row("Site").ToString() = "7" AndAlso row("AlrmOrFail").ToString() = "Alarm").Count()
            Dim site7Fails = grp.Where(Function(row) row("Site").ToString() = "7" AndAlso row("AlrmOrFail").ToString() = "Fail").Count()

            ' Add a new row to the summary DataTable
            dtSummary.Rows.Add(grp.Key.TestNumber, grp.Key.TestName, totalAlarms, totalFails,
                               site0Alarms, site0Fails, site1Alarms, site1Fails, site2Alarms, site2Fails,
                               site3Alarms, site3Fails, site4Alarms, site4Fails, site5Alarms, site5Fails,
                               site6Alarms, site6Fails, site7Alarms, site7Fails)
        Next

        Dim TotFails As Integer = dtSummary.AsEnumerable().Sum(Function(row) row.Field(Of Integer)("Total Fails"))
        Dim TotAlarms As Integer = dtSummary.AsEnumerable().Sum(Function(row) row.Field(Of Integer)("Total Alarms"))
        Dim TotBin1 As Integer = totalTested - (TotFails + TotAlarms)
        'get the yield in percentage
        Dim result As Decimal = CDec((TotBin1 * 100) / totalTested)
        Dim TotYld As Decimal = Math.Round(result, 2)

        Label5.Text = "Total Tested: " + CStr(totalTested)
        Label1.Text = "Total Failed: " + CStr(TotFails)
        Label2.Text = "Total Alarm: " + CStr(TotAlarms)
        Label4.Text = "OverAll Yield: " + CStr(TotYld) + "%"
        ' Return the summary DataTable
        Return dtSummary
    End Function

    Private Sub CreateOrUpdateChartSeries()
        ' Clear existing series (if any)
        Chart2.Series.Clear()

        ' Create series for Total Alarms
        Dim seriesAlarms As New Series("Total Alarms")
        seriesAlarms.ChartType = SeriesChartType.Column
        Chart2.Series.Add(seriesAlarms) ' Add to chart immediately

        ' Create series for Total Fails
        Dim seriesFails As New Series("Total Fails")
        seriesFails.ChartType = SeriesChartType.Column
        Chart2.Series.Add(seriesFails) ' Add to chart immediately

        ' Now populate the series with data from summaryTable
        Select Case ComboBox2.SelectedIndex
            Case 0
                For Each row As DataRow In summaryTable.Rows
                    Chart2.Series("Total Alarms").Points.AddXY(row("Test Number").ToString(), CInt(row("Total Alarms")))
                    Chart2.Series("Total Fails").Points.AddXY(row("Test Number").ToString(), CInt(row("Total Fails")))
                Next
            Case 1
                For Each row As DataRow In summaryTable.Rows
                    Chart2.Series("Total Alarms").Points.AddXY(row("Test Name").ToString(), CInt(row("Total Alarms")))
                    Chart2.Series("Total Fails").Points.AddXY(row("Test Name").ToString(), CInt(row("Total Fails")))
                Next
        End Select

        ' Update chart appearance
        Chart2.ChartAreas(0).RecalculateAxesScale()
        Chart2.Invalidate()
    End Sub

    Private Sub FilterByTestName()
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
        Chart2.Series("Total Alarms").Points.Clear()
        Chart2.Series("Total Fails").Points.Clear()

        ' Populate chart with (potentially) filtered data
        For Each row In view
            Chart2.Series("Total Alarms").Points.AddXY(row("Test Name"), CInt(row("Total Alarms")))
            Chart2.Series("Total Fails").Points.AddXY(row("Test Name"), CInt(row("Total Fails")))
        Next

        ' Update chart appearance
        Chart2.ChartAreas(0).RecalculateAxesScale()
        Chart2.Invalidate()
    End Sub

    Private Sub FilterByTestNum()
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
        Chart2.Series("Total Alarms").Points.Clear()
        Chart2.Series("Total Fails").Points.Clear()

        ' Populate chart with (potentially) filtered data
        For Each row In view
            Chart2.Series("Total Alarms").Points.AddXY(row("Test Number"), CInt(row("Total Alarms")))
            Chart2.Series("Total Fails").Points.AddXY(row("Test Number"), CInt(row("Total Fails")))
        Next

        ' Update chart appearance
        Chart2.ChartAreas(0).RecalculateAxesScale()
        Chart2.Invalidate()
    End Sub

End Class
