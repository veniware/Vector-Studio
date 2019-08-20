<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSvgDialog
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
        Me.components = New System.ComponentModel.Container()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.pnlParams = New System.Windows.Forms.Panel()
        Me.txtMargin = New System.Windows.Forms.NumericUpDown()
        Me.txtSize = New System.Windows.Forms.NumericUpDown()
        Me.lblMargin = New System.Windows.Forms.Label()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.chkIdentificateShapes = New System.Windows.Forms.CheckBox()
        Me.chkGZip = New System.Windows.Forms.CheckBox()
        Me.cmbColorFormat = New System.Windows.Forms.ComboBox()
        Me.lblColorFormat = New System.Windows.Forms.Label()
        Me.lblFilename = New System.Windows.Forms.Label()
        Me.txtFilename = New System.Windows.Forms.TextBox()
        Me.cmdFilename = New System.Windows.Forms.Button()
        Me.tmrUpdatePreview = New System.Windows.Forms.Timer(Me.components)
        Me.pnlView = New Vector_Studio.DoubleBuffered()
        Me.pnlParams.SuspendLayout()
        CType(Me.txtMargin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.Color.Black
        Me.cmdCancel.Location = New System.Drawing.Point(633, 325)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.ForeColor = System.Drawing.Color.Black
        Me.cmdOK.Location = New System.Drawing.Point(552, 325)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'pnlParams
        '
        Me.pnlParams.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlParams.Controls.Add(Me.txtMargin)
        Me.pnlParams.Controls.Add(Me.txtSize)
        Me.pnlParams.Controls.Add(Me.lblMargin)
        Me.pnlParams.Controls.Add(Me.lblSize)
        Me.pnlParams.Controls.Add(Me.chkIdentificateShapes)
        Me.pnlParams.Controls.Add(Me.chkGZip)
        Me.pnlParams.Controls.Add(Me.cmbColorFormat)
        Me.pnlParams.Controls.Add(Me.lblColorFormat)
        Me.pnlParams.Location = New System.Drawing.Point(508, 12)
        Me.pnlParams.Name = "pnlParams"
        Me.pnlParams.Size = New System.Drawing.Size(200, 307)
        Me.pnlParams.TabIndex = 7
        '
        'txtMargin
        '
        Me.txtMargin.Location = New System.Drawing.Point(114, 32)
        Me.txtMargin.Maximum = New Decimal(New Integer() {32, 0, 0, 0})
        Me.txtMargin.Name = "txtMargin"
        Me.txtMargin.Size = New System.Drawing.Size(83, 22)
        Me.txtMargin.TabIndex = 11
        '
        'txtSize
        '
        Me.txtSize.Location = New System.Drawing.Point(114, 4)
        Me.txtSize.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.txtSize.Minimum = New Decimal(New Integer() {8, 0, 0, 0})
        Me.txtSize.Name = "txtSize"
        Me.txtSize.Size = New System.Drawing.Size(83, 22)
        Me.txtSize.TabIndex = 10
        Me.txtSize.Value = New Decimal(New Integer() {32, 0, 0, 0})
        '
        'lblMargin
        '
        Me.lblMargin.AutoSize = True
        Me.lblMargin.Location = New System.Drawing.Point(3, 34)
        Me.lblMargin.Name = "lblMargin"
        Me.lblMargin.Size = New System.Drawing.Size(47, 13)
        Me.lblMargin.TabIndex = 9
        Me.lblMargin.Text = "Margin:"
        '
        'lblSize
        '
        Me.lblSize.AutoSize = True
        Me.lblSize.Location = New System.Drawing.Point(3, 6)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(30, 13)
        Me.lblSize.TabIndex = 7
        Me.lblSize.Text = "Size:"
        '
        'chkIdentificateShapes
        '
        Me.chkIdentificateShapes.AutoSize = True
        Me.chkIdentificateShapes.Location = New System.Drawing.Point(6, 87)
        Me.chkIdentificateShapes.Name = "chkIdentificateShapes"
        Me.chkIdentificateShapes.Size = New System.Drawing.Size(123, 17)
        Me.chkIdentificateShapes.TabIndex = 6
        Me.chkIdentificateShapes.Text = "Identificate shapes"
        Me.chkIdentificateShapes.UseVisualStyleBackColor = True
        '
        'chkGZip
        '
        Me.chkGZip.AutoSize = True
        Me.chkGZip.Location = New System.Drawing.Point(6, 110)
        Me.chkGZip.Name = "chkGZip"
        Me.chkGZip.Size = New System.Drawing.Size(121, 17)
        Me.chkGZip.TabIndex = 4
        Me.chkGZip.Text = "G-zip compression"
        Me.chkGZip.UseVisualStyleBackColor = True
        '
        'cmbColorFormat
        '
        Me.cmbColorFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColorFormat.FormattingEnabled = True
        Me.cmbColorFormat.Items.AddRange(New Object() {"HEX", "RGB", "RGBA"})
        Me.cmbColorFormat.Location = New System.Drawing.Point(114, 60)
        Me.cmbColorFormat.Name = "cmbColorFormat"
        Me.cmbColorFormat.Size = New System.Drawing.Size(83, 21)
        Me.cmbColorFormat.TabIndex = 1
        '
        'lblColorFormat
        '
        Me.lblColorFormat.AutoSize = True
        Me.lblColorFormat.Location = New System.Drawing.Point(3, 63)
        Me.lblColorFormat.Name = "lblColorFormat"
        Me.lblColorFormat.Size = New System.Drawing.Size(75, 13)
        Me.lblColorFormat.TabIndex = 0
        Me.lblColorFormat.Text = "Color format:"
        '
        'lblFilename
        '
        Me.lblFilename.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFilename.AutoSize = True
        Me.lblFilename.Location = New System.Drawing.Point(12, 329)
        Me.lblFilename.Name = "lblFilename"
        Me.lblFilename.Size = New System.Drawing.Size(56, 13)
        Me.lblFilename.TabIndex = 8
        Me.lblFilename.Text = "Filename:"
        '
        'txtFilename
        '
        Me.txtFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFilename.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtFilename.ForeColor = System.Drawing.Color.Black
        Me.txtFilename.Location = New System.Drawing.Point(70, 326)
        Me.txtFilename.Name = "txtFilename"
        Me.txtFilename.ReadOnly = True
        Me.txtFilename.Size = New System.Drawing.Size(391, 22)
        Me.txtFilename.TabIndex = 9
        '
        'cmdFilename
        '
        Me.cmdFilename.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdFilename.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cmdFilename.ForeColor = System.Drawing.Color.Black
        Me.cmdFilename.Location = New System.Drawing.Point(467, 325)
        Me.cmdFilename.Name = "cmdFilename"
        Me.cmdFilename.Size = New System.Drawing.Size(35, 23)
        Me.cmdFilename.TabIndex = 10
        Me.cmdFilename.Text = "..."
        Me.cmdFilename.UseVisualStyleBackColor = False
        '
        'tmrUpdatePreview
        '
        Me.tmrUpdatePreview.Enabled = True
        Me.tmrUpdatePreview.Interval = 250
        '
        'pnlView
        '
        Me.pnlView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pnlView.Location = New System.Drawing.Point(12, 12)
        Me.pnlView.Name = "pnlView"
        Me.pnlView.Size = New System.Drawing.Size(490, 307)
        Me.pnlView.TabIndex = 6
        '
        'frmSvgDialog
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(720, 360)
        Me.Controls.Add(Me.cmdFilename)
        Me.Controls.Add(Me.txtFilename)
        Me.Controls.Add(Me.lblFilename)
        Me.Controls.Add(Me.pnlParams)
        Me.Controls.Add(Me.pnlView)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(640, 320)
        Me.Name = "frmSvgDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.pnlParams.ResumeLayout(False)
        Me.pnlParams.PerformLayout()
        CType(Me.txtMargin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents pnlView As Vector_Studio.DoubleBuffered
    Friend WithEvents pnlParams As System.Windows.Forms.Panel
    Friend WithEvents lblFilename As System.Windows.Forms.Label
    Friend WithEvents txtFilename As System.Windows.Forms.TextBox
    Friend WithEvents cmdFilename As System.Windows.Forms.Button
    Friend WithEvents cmbColorFormat As System.Windows.Forms.ComboBox
    Friend WithEvents lblColorFormat As System.Windows.Forms.Label
    Friend WithEvents chkGZip As System.Windows.Forms.CheckBox
    Friend WithEvents chkIdentificateShapes As System.Windows.Forms.CheckBox
    Friend WithEvents lblSize As System.Windows.Forms.Label
    Friend WithEvents lblMargin As System.Windows.Forms.Label
    Friend WithEvents txtMargin As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents tmrUpdatePreview As System.Windows.Forms.Timer
End Class
