Option Strict On
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class Summ_Searcher

    Private _strSNToSearch As String
    Private _strBaseDirectoryToSearchFrom As String
    Private _lstTraceviewFiles As List(Of String)

    Private _thrAction As Thread
    Private _lstSubDirToSearch As List(Of String)
    Private _lstSearchedSubDir As List(Of String)
    Private _strCurrentDir As String

    Public Sub New(ByVal strStartingDirectory As String, ByVal strSN As String)
        _strBaseDirectoryToSearchFrom = strStartingDirectory
        _strSNToSearch = strSN

        Dim x As DirectoryInfo() = New System.IO.DirectoryInfo(_strBaseDirectoryToSearchFrom).GetDirectories()
        _lstSubDirToSearch = New List(Of String)
        For Each dir As DirectoryInfo In x
            _lstSubDirToSearch.Add(dir.FullName)
        Next
        _lstSearchedSubDir = New List(Of String)

        _thrAction = New Thread(AddressOf GetListOfTraceviewFiles)
        _thrAction.IsBackground = True
    End Sub

    Public Sub Start()
        If Not _thrAction.IsAlive Then
            _thrAction.Start()
        End If
    End Sub

    Public ReadOnly Property IsDone As Boolean
        Get
            If _thrAction.IsAlive Then
                Return False
            Else
                _thrAction.Join()
                Return True
            End If
        End Get
    End Property

    Public ReadOnly Property Directories As List(Of String)
        Get
            Return _lstSubDirToSearch
        End Get
    End Property

    Public ReadOnly Property SearchedDirectories As List(Of String)
        Get
            Return _lstSearchedSubDir
        End Get
    End Property

    Public ReadOnly Property CurrentDirectory As String
        Get
            Return _strCurrentDir
        End Get
    End Property

    Public Function GetFiles() As List(Of String)
        Return _lstTraceviewFiles
    End Function

    Private Sub GetListOfTraceviewFiles()
        Try
            Dim orderedFiles As List(Of FileInfo) = GetFilesInFolder(_strBaseDirectoryToSearchFrom, _strSNToSearch)
            orderedFiles.Sort(Function(x, y) x.LastWriteTime.CompareTo(y.LastWriteTime))
            _lstTraceviewFiles = New List(Of String)
            For Each f As System.IO.FileInfo In orderedFiles
                If Regex.IsMatch(f.FullName.ToString, _strSNToSearch) Then
                    _lstTraceviewFiles.Add(f.FullName.ToString)
                End If
            Next
        Catch ex As Exception
            Dim strMsg As String = ex.Message & vbCrLf & vbCrLf & ex.StackTrace
            MessageBox.Show("Something is wrong. Search will be terminated." & vbCrLf & strMsg, "Error Handler")
        End Try
    End Sub

    Private Function GetFilesInFolder(ByVal baseDirectory As String, ByVal whereSNis As String) As List(Of FileInfo)
        If _lstSubDirToSearch.Contains(baseDirectory) Then
            _strCurrentDir = baseDirectory
        End If

        Dim retlstFiles As New List(Of FileInfo)
        ' Get files from current directory
        Dim rawFiles As FileInfo() = New System.IO.DirectoryInfo(baseDirectory).GetFiles()
        For Each f As FileInfo In rawFiles
            If Regex.IsMatch(f.FullName.ToString, whereSNis) Then
                retlstFiles.Add(f)
            End If
        Next

        'Get files from further subdirectories
        Dim subDirectories As DirectoryInfo() = New System.IO.DirectoryInfo(baseDirectory).GetDirectories()
        For Each d As DirectoryInfo In subDirectories
            Dim newBaseDirectory As String = d.FullName
            retlstFiles.AddRange(GetFilesInFolder(newBaseDirectory, whereSNis))
        Next

        If _lstSubDirToSearch.Contains(baseDirectory) Then
            _lstSearchedSubDir.Add(baseDirectory)
        End If
        Return retlstFiles
    End Function
End Class
