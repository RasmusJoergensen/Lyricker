
Imports System.Runtime.InteropServices

Public Class Lyricker

    Dim writer As System.IO.StreamWriter
    Dim chartText() As String

    Dim replaceDefault As Boolean

   <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> lParam As String) As Int32
    End Function

    Const EM_SETCUEBANNER As Integer = &H1501

    Const PHRASE_START_SPACE As Integer = 24
    Const PHRASE_END_SPACE As Integer = 48

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

        If Trim(TextboxLyrics.Text).Length = 0
            MsgBox("Please enter lyrics")
            Return
        End If

        Dim chartFile As String = My.Computer.FileSystem.ReadAllText(path) 'Store the original file for backup

        Try
            writeLyrics(path)
        Catch ex As Exception
            MsgBox("Something went wrong :(" & vbNewLine & vbNewLine & "Error message: """ & ex.Message & """")
            writer.Close()
            My.Computer.FileSystem.WriteAllText(path, chartFile, False) 'Write the backup to the file
            'Me.Close
        End Try
    End Sub

    Private sub writeLyrics(path As String)

        '*****************************************************************************************************************
        ' Write phrase_start and phrase_end and fill lyrics events
        '*****************************************************************************************************************

        chartText = System.IO.File.ReadAllLines(path)
        writer = My.Computer.FileSystem.OpenTextFileWriter(path, False)

        Dim syllables As new ArrayList()
        syllables = getSyllables()
        
        Dim syllableNo As Integer

        Dim stringToReplace As String = "lyric "
        Dim string2ndLastLetter As String = "c"

        For i As Integer = LBound(chartText) To UBound(chartText)

            'if(replaceDefault, InStr(chartText(lineNo), "= E ""Default") <> 0, False)
            If stringOnLine(i, "= E ""lyric ")

                stringToReplace = "lyric "
                string2ndLastLetter = "c"

            Else If stringOnLine(i, "= E ""Default") And replaceDefault

                stringToReplace = "Default"
                string2ndLastLetter = "l"

            Else 'If no phrase event on the line
                'Write the line without changing it, if it's not a phrase event
                'Already existing phrase events are skipped because new ones are already written in the above lines
                'This way we don't get duplicate phrase events and don't keep phrase events that are removed in the text box i.e. deleted a period
                If InStr(chartText(i), "phrase_") = 0
                    writer.WriteLine(chartText(i)) 'Just write the line without changing it
                Else
                    'Do nothing i.e. skip the line
                End If
                Continue For
            End If

            If syllableNo >= syllables.Count and InStr(chartText(i), "phrase_") = 0
                writer.WriteLine(chartText(i)) 'Just write the line without changing it 'TODO: write better logic so this line doesn't have to be at twp places
                syllableNo += 1
                Continue For
            End If

            ReplaceLine(i, stringToReplace, string2ndLastLetter, syllables, syllableNo, syllables.Count)
        Next i

        writer.Close()
        '*****************************************************************************************************************

        If syllableNo < syllables.Count
            MsgBox("Some of the lyrics have been written. The rest couldn't because there's too many syllables in the text, compared to lyric events. You may have a word that should not be split up, or you have too few lyric events")
        Else If syllableNo > syllables.Count
            MsgBox("Lyrics have been written but there's too few syllables to fill out all the lyric events in the chart. You may be missing some lyrics, have a word that should be split up, or you have too many lyric events")
        Else
            MsgBox("Lyrics written successfully")
        End If
    End Sub
    
    Private Function stringOnLine(lineNo As Integer, _event As String)

        If InStr(chartText(lineNo), _event) <> 0
            Return True
        End If
        Return False
    End Function

    Private sub ReplaceLine(i As Integer, stringToReplace As String, string2ndLastLetter As String, syllables As ArrayList, ByRef syllableNo As Integer, syllablesTotal As Integer)

        Dim lyricStartIndex As Integer
        Dim tickNumberEndIndex As Integer
        Dim tickNumber As Integer

        'Check if we should write a phrase_start event before the lyric event
                If syllableNo = 0 OrElse InStr(syllables.Item(syllableNo - 1), ".") <> 0 'If first syllable, or if a period is in the previous syllable
                    'Get the tick number of the current lyric event, so that we can add a phrase_start event a specified number of ticks BEFORE it
                    tickNumberEndIndex = InStr(chartText(i), "=") - 1
                    tickNumber = RSet(chartText(i), tickNumberEndIndex)
                    writer.WriteLine("  " & tickNumber - PHRASE_START_SPACE & " = E ""phrase_start""")
                End If
                
                'Write the lyric event with lyrics
                lyricStartIndex = InStr(chartText(i), string2ndLastLetter) + 1 'Find the start index of where we want to insert the syllable
                chartText(i) = LSet(chartText(i), lyricStartIndex) 'Cut the string to end after "lyric" (to override existing lyrics)
                chartText(i) = Replace(chartText(i), stringToReplace, "lyric " & Replace(syllables.Item(syllableNo), ".", "") & """") 'Add the syllable
                writer.WriteLine(chartText(i))

                'Check if we should write a phrase_end event after the lyric event
                If InStr(syllables.Item(syllableNo), ".") <> 0 'If a period is in the current syllable
                    
                    If syllableNo = syllablesTotal - 1 'Special case for last syllable
                        'Get the tick number of the current lyric event, so that we can add a phrase_end event a specified number of ticks AFTER it
                        tickNumberEndIndex = InStr(chartText(i), "=") - 1
                        tickNumber = RSet(chartText(i), tickNumberEndIndex)
                        writer.WriteLine("  " & tickNumber + PHRASE_END_SPACE & " = E ""phrase_end""")
                    Else
                    'Get the tick number of the NEXT lyric event, so that we can add a phrase_end event a specified number of ticks BEFORE it
                    For j As Integer = i + 1 To UBound(chartText)
                        If InStr(chartText(j), "= E """ & stringToReplace) <> 0       ' "Lyric"
                            tickNumberEndIndex = InStr(chartText(j), "=") - 1
                            tickNumber = RSet(chartText(j), tickNumberEndIndex)
                            Exit For
                        End If
                    Next j
                    writer.WriteLine("  " & tickNumber - PHRASE_END_SPACE & " = E ""phrase_end""")
                    End If
                End If
                syllableNo += 1
    End Sub

    Private Function getSyllables

        '*****************************************************************************************************************
        ' Get all syllables in the lyrics box and return them in an arraylist
        '*****************************************************************************************************************

        Dim lyrics As String = Trim(TextboxLyrics.Text)

        'Make all words end with +
        lyrics = Replace(lyrics, " ", "+")      
        lyrics = Replace(lyrics, vbLf, "+")

        'TextBoxLyrics.Text = lyrics


        Dim syllables As ArrayList = New ArrayList()
        Dim syllablesTotal As Integer = TotalSyllables(lyrics)

        Dim a As Integer = 0    'Start index
        Dim b As Integer        '(-) End index if not last syllable in word 
        Dim c As Integer        '(+) End index if last syllable in word 

        For i As Integer = 1 To syllablesTotal - 1
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
        return syllables
    End Function

    Private Function TotalSyllables(lyrics As String)
        Dim syllablesTotal As Integer = Len(lyrics) - Len(lyrics.Replace("-", "")) _
                                      + Len(lyrics) - Len(lyrics.Replace("+", "")) + 1
        Return syllablesTotal
    End Function


    Private Sub ChkBxReplaceDefault_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBxReplaceDefault.CheckedChanged
        replaceDefault = Not replaceDefault
    End Sub
End Class