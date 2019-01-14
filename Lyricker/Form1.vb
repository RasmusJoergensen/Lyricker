
Imports System.Runtime.InteropServices

Public Class Lyricker

   <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> lParam As String) As Int32
    End Function

    Const EM_SETCUEBANNER As Integer = &H1501

     Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New Point(CInt((Screen.PrimaryScreen.WorkingArea.Width / 2) - (Me.Width / 2)),
                                CInt((Screen.PrimaryScreen.WorkingArea.Height / 2) - (Me.Height / 2)))
        SendMessage(TextBoxFilePath.Handle, EM_SETCUEBANNER, 0, "Path name")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnChooseChartFile.Click

        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.Filter = "chart files (*.chart)|*.chart" '|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 1 'select first file type (.txt) as default

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            TextBoxFilePath.Text = openFileDialog1.FileName
        End If
    End Sub

    Private Sub  BtnWRITE_Click(sender As Object, e As EventArgs) Handles BtnWRITE.Click

        Dim path As String = ""

        path = TextBoxFilePath.Text

        If path.Length <= 0 Then
            MsgBox("Please set a path name")
            Return
        End If

        If Not My.Computer.FileSystem.FileExists(path) Then
            MsgBox("File does not exist" + vbNewLine + "Please check that the path is correct")
            Return
        End If

        If Not My.Computer.FileSystem.GetName(path).Contains(".chart") Then
            MsgBox("File must be a .chart file")
            Return
        End If

        If TextboxLyrics.Text.Length = 0 'User can still write only spaces " " to break the program :(
            MsgBox("Please enter lyrics")
            Return
        End If

        Try
            writeLyrics(path)
       Catch ex As Exception
            MsgBox("Something went wrong :(" & vbNewLine & vbNewLine & "Error message: """ & ex.Message & """")
            Me.Close
        End Try
    End Sub

    Private sub writeLyrics(path As String)

        Dim lyrics As String = TextboxLyrics.Text

        'Make all words end with +
        lyrics = Replace(lyrics, " ", "+")      
        lyrics = Replace(lyrics, vbLf, "+")

        'TextBoxLyrics.Text = lyrics

        '*****************************************************************************************************************
        ' Get all syllables in the lyrics box and put them in an arraylist

        Dim syllables As ArrayList = New ArrayList()
        Dim syllablesCount As Integer = Len(lyrics) - Len(lyrics.Replace("-", "")) _
                                      + Len(lyrics) - Len(lyrics.Replace("+", "")) + 1 'Get the total number of syllables

        Dim a As Integer = 0    'Start index
        Dim b As Integer        '(-) End index if not last syllable in word 
        Dim c As Integer        '(+) End index if last syllable in word 

        For i As Integer = 1 To syllablesCount - 1
            b = InStr(a + 1, lyrics, "-") 'Find index of next (-)
            c = InStr(a + 1, lyrics, "+") 'Find index of next (+)

            If (c < b and c <> 0) Or b = 0 Then 'If it is the last syllable in a word OR (if b==0 then the rest of all the words in the lyrics have one syllable)
                syllables.Add(Mid(lyrics, a + 1, c - a - 1))  'c-a-1 is the length of the syllable. We subtract 1 to not get the (+) that signifies the last syllable in a word
                a = c
            Else 'If its not the last syllable in a word 
                syllables.Add(Mid(lyrics, a + 1, b - a))  'b-a is the length of the syllable plus the (-) that signifies the not last syllable in a word
                a = b
            End If
        Next i
        syllables.Add(Mid(lyrics, a + 1)) 'Add the very last syllable

        '*****************************************************************************************************************

        '*****************************************************************************************************************
        ' Replace all empty lyric events with their respective syllable
        Dim chartText() As String = System.IO.File.ReadAllLines(path)

        Dim syllableNo As Integer
        Dim lyricStart As Integer
        For i As Integer = LBound(chartText) To UBound(chartText)
            If not InStr(chartText(i), "= E ""lyric") = 0 ' If a lyric event is on this line
                lyricStart = InStr(chartText(i), "c") + 1 'Find the start index of where we want to insert the syllable
                chartText(i) = LSet(chartText(i), lyricStart) 'Cut the string to end after "lyric" (to override existing lyrics in case they uhh exist)
                Try
                    chartText(i) = Replace(chartText(i), "lyric ", "lyric " & syllables.Item(syllableNo) & """") 'Add the syllable
                Catch ex As System.ArgumentOutOfRangeException
                    printToFile(path, chartText)
                    MsgBox("Lyrics have been written but there's too few syllables to fill out all the lyric events in the chart. You may be missing some lyrics, have a word that should be split up, or you have too many lyric events")
                    Exit Sub
                End Try
                syllableNo += 1
            End If
        Next i
        '*****************************************************************************************************************

        if Not printToFile(path, chartText)
            Exit Sub
        End If
        If syllableNo < syllables.Count
            MsgBox("Some of the lyrics have been written. The rest couldn't because there's too many syllables in the text, compared to lyric events. You may have a word that should not be split up, or you have too few lyric events")
        Else
            MsgBox("Lyrics written successfully")
        End If
    End Sub

    Function printToFile(path As String, chartText() As String) As Boolean
        Try
            IO.File.WriteAllLines(path, chartText) 'Overwrite the .chart file
        Catch ex As Exception
            MsgBox("Unable to write to file" & vbNewLine & vbNewLine & "Error message: """ & ex.Message & """")
            Return False
        End Try
        Return True
    End Function
End Class