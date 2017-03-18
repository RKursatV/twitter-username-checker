Imports System.IO
Imports System.Net

Public Class Form1
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        For i = 1 To RichTextBox1.Lines.Length
            Label1.Text = "Current: " & RichTextBox1.Lines(i - 1) & " " & i & "/" & RichTextBox1.Lines.Length
            ProgressBar1.Value = i
            istek(RichTextBox1.Lines(i - 1))
            Label2.Text = String.Format("{0}%", ((ProgressBar1.Value / ProgressBar1.Maximum) * 100).ToString("F2"))
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CheckForIllegalCrossThreadCalls = False
        ProgressBar1.Maximum = RichTextBox1.Lines.Length
        BackgroundWorker1.RunWorkerAsync()

    End Sub
    Public Function istek(ByVal nickname As String)
        Try
            Dim request As WebRequest =
             WebRequest.Create("https://mobile.twitter.com/" & nickname)

            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            ' Get the response.
            Dim response As WebResponse = request.GetResponse
            '' Display the status.

            '' Get the stream containing content returned by the server.
            'Dim dataStream As Stream = response.GetResponseStream()
            '' Open the stream using a StreamReader for easy access.
            'Dim reader As New StreamReader(dataStream)
            '' Read the content.
            'Dim responseFromServer As String = reader.ReadToEnd()
            '' Display the content.
            '' RichTextBox2.Text += responseFromServer & vbCrLf

            'Dim regex As New Text.RegularExpressions.Regex("<\s*title[^>]*>(.*?)<\s*/\s*title>")
            'Dim results As New Text.StringBuilder
            'For Each m As Text.RegularExpressions.Match In regex.Matches(responseFromServer)
            '    results.Append(m.Value)
            'Next
            'If InStr(results.ToString, "on Twitter") = False Then
            '    If InStr(responseFromServer, "This account has been suspended.") = False Then
            '        RichTextBox3.Text += nickname & vbCrLf
            '    End If
            'End If

            ' Clean up the streams and the response.
            'reader.Close()
            response.Close()
        Catch ex As Exception
            RichTextBox3.Text += nickname & vbCrLf
        End Try


        Return True

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim openFile1 As New OpenFileDialog()

        ' Initialize the OpenFileDialog to look for RTF files.
        openFile1.DefaultExt = "*.txt"
        openFile1.Filter = "TXT Files|*.txt"

        ' Determine whether the user selected a file from the OpenFileDialog.
        If (openFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) _
            And (openFile1.FileName.Length > 0) Then

            ' Load the contents of the file into the RichTextBox.
            RichTextBox1.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText)
        End If

    End Sub
End Class
