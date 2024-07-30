Imports System.IO
'Imports Microsoft.Win32
Imports System.Text.RegularExpressions
Imports System.Linq
Imports System.Data.SqlClient
'Imports Microsoft.Office.Interop
Imports System.Xml
Imports System.Security.Cryptography
'Imports Microsoft.Office.Interop.Excel


Module mdlFunction

    'Public tmpTable As DataTable
    Public fd As String = String.Empty

    'Public folderDir As String = SaveFileFolder + "\summToSearch" 'Search file
    Public SaveFileFolder As String
    Public strAppText As String = "SUMMARY SEARCHER V1.1"
    Public Sub LB_Copy2Clipboard(ByVal LB As ListBox)
        'set listbox1 to multisimple
        'LB.SelectionMode = SelectionMode.MultiSimple
        'Select all items from the list.
        'For i As Integer = 0 To LB.Items.Count - 1
        ' LB.SetSelected(i, True)
        'Next
        'Copy to cliboard
        Dim copy_buffer As New System.Text.StringBuilder
        For Each item As Object In LB.SelectedItems
            copy_buffer.AppendLine(item.ToString)
        Next
        If copy_buffer.Length > 0 Then
            Clipboard.SetText(copy_buffer.ToString)
        End If
        LB.SelectedItems.Clear()
    End Sub
    Public strCurrentPath As String
    Public m_strDefaultTraceviewPath As String = "\\10.83.133.10\uflex\uflex_summary\" '"C:\Users\sterc\OneDrive\Documents\Traceview\Saved_TraceView\U35\" 
    '"C:\Users\sterc\Desktop\Traceview\Complete" '

    Public Function GetListofTraceViewFileNames(ByVal strSN As String) As List(Of String)
        'Files are retrieved and arranged in increasing order of Data(Oldest first)
        'This is needed to handle incomplete traceviews.....
        'Displaying must be done in reverse order i.e., Latest First

        Dim strTraceDirectory As String = GetTraceViewPath(strSN)

        Dim orderedFiles As List(Of FileInfo) = GetFilesInFolder(strTraceDirectory, strSN)
        orderedFiles.Sort(Function(x, y) x.LastWriteTime.CompareTo(y.LastWriteTime))
        Dim lstFiles As New List(Of String)
        For Each f As System.IO.FileInfo In orderedFiles
            If Regex.IsMatch(f.FullName.ToString, strSN) Then
                lstFiles.Add(f.FullName.ToString)
            End If
        Next
        Return lstFiles
    End Function

    Private Function GetFilesInFolder(ByVal baseDirectory As String, ByVal whereSNis As String) As List(Of FileInfo)
        Dim retlstFiles As New List(Of FileInfo)

        ' Get files from current directory
        Dim rawFiles As FileInfo() = New System.IO.DirectoryInfo(baseDirectory).GetFiles()
        For Each f As FileInfo In rawFiles
            If Regex.IsMatch(f.FullName.ToString, whereSNis) Then
                retlstFiles.Add(f)
            End If
        Next

        'Get files from further subdirectories
        'Dim subDirectories As DirectoryInfo() = New System.IO.DirectoryInfo(baseDirectory).GetDirectories()
        'For Each d As DirectoryInfo In subDirectories
        ' Dim newBaseDirectory As String = d.FullName
        ' retlstFiles.AddRange(GetFilesInFolder(newBaseDirectory, whereSNis))
        ' Next

        Return retlstFiles
    End Function

    Public Function GetTraceViewPath(ByVal strSN As String) As String
        strCurrentPath = m_strDefaultTraceviewPath '= Item("TraceViewSavePath", m_strDefaultTraceviewPath)

        If strCurrentPath.ToUpper <> m_strDefaultTraceviewPath.ToUpper Then
            Return strCurrentPath
        Else
            Return strCurrentPath
        End If
    End Function

    Dim s As String


    Public Sub deleteFile(ByVal fPath As String)
        Dim FileToDelete As String

        FileToDelete = fPath

        If System.IO.File.Exists(FileToDelete) = True Then

            System.IO.File.Delete(FileToDelete)
            MsgBox("File Deleted")

        End If
    End Sub

    Public Sub DeleteFilesFromFolder(ByVal Folder As String)
        If Directory.Exists(Folder) Then
            For Each _file As String In Directory.GetFiles(Folder)
                File.Delete(_file)
            Next
            For Each _folder As String In Directory.GetDirectories(Folder)

                DeleteFilesFromFolder(_folder)
            Next

        End If
        'MsgBox("All Traceview/s has been deleted", vbOKOnly, "Delete Saved Traceviews")
    End Sub








    'Public Sub FindString(ByVal rtb As RichTextBox)
    '    Dim searchstring As String = "Passed"
    '    Dim index As Integer = rtb.Find(searchstring, 0, RichTextBoxFinds.None)
    '    While index <> -1
    '        rtb.Select(index, searchstring.Length)
    '        rtb.SelectionColor = Color.Lime
    '        rtb.SelectionFont = New Font(rtb.Font, FontStyle.Bold)
    '        index = rtb.Find(searchstring, index + searchstring.Length, RichTextBoxFinds.None)
    '    End While
    'End Sub

    Public Sub strFindrtbWord(ByVal rtb As RichTextBox)
        Dim str As String = "Passed"
        Dim startText As Integer = 0
        Dim endText As Integer
        endText = rtb.Text.LastIndexOf(str)

        rtb.SelectAll()
        rtb.SelectionBackColor = Color.Black

        While startText < endText
            rtb.Find(str, startText, rtb.TextLength, RichTextBoxFinds.MatchCase)
            rtb.SelectionColor = Color.Lime

            startText = rtb.Text.LastIndexOf(str, startText) + 1
        End While

    End Sub

    Public Sub m_deleteTxtFiles(ByVal folderpath As String)
        For Each path As String In IO.Directory.GetFiles(folderpath, "*.txt")
            Try
                System.IO.File.Delete(path)
            Catch ex As Exception
                MessageBox.Show(path, "Unable to Delete File")
            End Try
        Next
    End Sub

    Public Function g_compSN(ByVal txtbx As TextBox) As String

        Dim count As String = txtbx.Text


        For Each item As Object In txtbx.Text
            s = (s + "'" & item.ToString) & "'"
            If (txtbx.Text((count - 1)) <> item) Then
                s = (s + ",")

            End If

        Next
        g_compSN = s

    End Function






    Public Function login2Server() As Boolean
        Try
            Dim ret As Long

            ret = Shell("NET USE \\10.83.175.32 /user:JCIM-SG\adi_oft adi_oft", AppWinStyle.MinimizedFocus, True, -1)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function disConnect2Server() As Boolean
        Try

            Dim ret As Long

            ret = Shell("NET USE \\10.96.150.72 /delete")

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Sub SaveFilesCopyToFolder()

        Dim DestFolder = My.Settings.SaveFileFolder
        Form1.Text = strAppText + "   [ Please wait while copying files to your folder. . . ]"
        For x = 0 To Form1.lbFilenameList.SelectedItems.Count() - 1
            Dim fname As String = Form1.lbFilenameList.SelectedItems(x).Text
            Dim sourceFile As String = m_strDefaultTraceviewPath + fname
            If Form1.lbFilenameList.SelectedItems.Count() Then

                File.Copy(sourceFile, Path.Combine(DestFolder, fname), True)

            End If

        Next x

        MsgBox("Selected datalog(s) and STDF files has been saved to destination folder.")
        Form1.Text = strAppText
    End Sub

    'Public Sub strmCopy(ByVal from As String, ByVal dest As String)

    '    Dim CF As New IO.FileStream(from, IO.FileMode.Open)
    '    Dim CT As New IO.FileStream(dest, IO.FileMode.Create)
    '    Dim f As Long = CF.Length - 1
    '    Dim buffer(1024) As Byte
    '    Dim byteCFead As Integer
    '    While CF.Position < f
    '        byteCFead = (CF.Read(buffer, 0, 1024))
    '        CT.Write(buffer, 0, byteCFead)
    '        'Form1.ProgressBar1.Value = CInt(CF.Position / f * 100)
    '        Application.DoEvents()
    '    End While
    '    CT.Flush()
    '    CT.Close()
    '    CF.Close()

    'End Sub

    Public Sub dgvColAutoFit(ByVal dgv As DataGridView)

        ' Set your desired AutoSize Mode:
        dgv.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(9).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(10).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(11).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(12).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(13).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(14).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(15).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(16).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(17).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(18).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(19).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(20).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(21).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgv.Columns(22).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        ' Now that DataGridView has calculated it's Widths; we can now store each column Width values.
        For i As Integer = 0 To dgv.Columns.Count - 1
            ' Store Auto Sized Widths:
            Dim colw As Integer = dgv.Columns(i).Width

            ' Remove AutoSizing:
            dgv.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.None

            ' Set Width to calculated AutoSize value:
            dgv.Columns(i).Width = colw
        Next

    End Sub


End Module
