Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Me.TextBox1.Text = Console.ReadLine


        'Me.Timer1.Interval = 500
        'Me.Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Static i As Long
        i = i + 1
        Me.TextBox1.Text = Me.TextBox1.Text & "*"
        If i = 10 Then
            Me.TextBox1.Text = "Copying"
            i = 0
        End If
    End Sub

    Private Sub Form2_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Timer1.Enabled = False
    End Sub
End Class