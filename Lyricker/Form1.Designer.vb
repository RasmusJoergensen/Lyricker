<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Lyricker
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Lyricker))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxFilePath = New System.Windows.Forms.TextBox()
        Me.BtnChooseChartFile = New System.Windows.Forms.Button()
        Me.BtnWRITE = New System.Windows.Forms.Button()
        Me.TextboxLyrics = New System.Windows.Forms.RichTextBox()
        Me.ChkBxReplaceDefault = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 435)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Chart file:"
        '
        'TextBoxFilePath
        '
        Me.TextBoxFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TextBoxFilePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBoxFilePath.Location = New System.Drawing.Point(69, 432)
        Me.TextBoxFilePath.Name = "TextBoxFilePath"
        Me.TextBoxFilePath.Size = New System.Drawing.Size(472, 20)
        Me.TextBoxFilePath.TabIndex = 2
        '
        'BtnChooseChartFile
        '
        Me.BtnChooseChartFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.BtnChooseChartFile.Location = New System.Drawing.Point(547, 432)
        Me.BtnChooseChartFile.Name = "BtnChooseChartFile"
        Me.BtnChooseChartFile.Size = New System.Drawing.Size(48, 20)
        Me.BtnChooseChartFile.TabIndex = 3
        Me.BtnChooseChartFile.Text = "Set"
        Me.BtnChooseChartFile.UseVisualStyleBackColor = true
        '
        'BtnWRITE
        '
        Me.BtnWRITE.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnWRITE.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.BtnWRITE.Location = New System.Drawing.Point(233, 479)
        Me.BtnWRITE.Name = "BtnWRITE"
        Me.BtnWRITE.Size = New System.Drawing.Size(141, 30)
        Me.BtnWRITE.TabIndex = 4
        Me.BtnWRITE.Text = "Write lyrics"
        Me.BtnWRITE.UseVisualStyleBackColor = true
        '
        'TextboxLyrics
        '
        Me.TextboxLyrics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TextboxLyrics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextboxLyrics.DetectUrls = false
        Me.TextboxLyrics.Font = New System.Drawing.Font("Calibri", 16!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextboxLyrics.Location = New System.Drawing.Point(12, 12)
        Me.TextboxLyrics.Name = "TextboxLyrics"
        Me.TextboxLyrics.ShowSelectionMargin = true
        Me.TextboxLyrics.Size = New System.Drawing.Size(583, 393)
        Me.TextboxLyrics.TabIndex = 5
        Me.TextboxLyrics.Text = ""
        Me.TextboxLyrics.WordWrap = false
        '
        'ChkBxReplaceDefault
        '
        Me.ChkBxReplaceDefault.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ChkBxReplaceDefault.AutoSize = true
        Me.ChkBxReplaceDefault.Location = New System.Drawing.Point(380, 488)
        Me.ChkBxReplaceDefault.Name = "ChkBxReplaceDefault"
        Me.ChkBxReplaceDefault.Size = New System.Drawing.Size(154, 17)
        Me.ChkBxReplaceDefault.TabIndex = 6
        Me.ChkBxReplaceDefault.Text = "Also replace default events"
        Me.ChkBxReplaceDefault.UseVisualStyleBackColor = true
        '
        'Lyricker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(607, 521)
        Me.Controls.Add(Me.ChkBxReplaceDefault)
        Me.Controls.Add(Me.TextboxLyrics)
        Me.Controls.Add(Me.BtnWRITE)
        Me.Controls.Add(Me.BtnChooseChartFile)
        Me.Controls.Add(Me.TextBoxFilePath)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "Lyricker"
        Me.Text = "Lyricker"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxFilePath As TextBox
    Friend WithEvents BtnChooseChartFile As Button
    Friend WithEvents BtnWRITE As Button
    Friend WithEvents TextboxLyrics As RichTextBox
    Friend WithEvents ChkBxReplaceDefault As CheckBox
End Class
