<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class main
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cb_port = New System.Windows.Forms.ComboBox()
        Me.b_connect = New System.Windows.Forms.Button()
        Me.b_disconnect = New System.Windows.Forms.Button()
        Me.B_look_on = New System.Windows.Forms.Button()
        Me.B_look_off = New System.Windows.Forms.Button()
        Me.b_Tension = New System.Windows.Forms.Button()
        Me.b_Current = New System.Windows.Forms.Button()
        Me.Boff = New System.Windows.Forms.Button()
        Me.Bon = New System.Windows.Forms.Button()
        Me.v_Tension = New System.Windows.Forms.TextBox()
        Me.v_Current = New System.Windows.Forms.TextBox()
        Me.b_info = New System.Windows.Forms.Button()
        Me.tb_out = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.t_loop = New System.Windows.Forms.Timer(Me.components)
        Me.cb_loop = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Port"
        '
        'cb_port
        '
        Me.cb_port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_port.FormattingEnabled = True
        Me.cb_port.Location = New System.Drawing.Point(44, 15)
        Me.cb_port.Name = "cb_port"
        Me.cb_port.Size = New System.Drawing.Size(102, 21)
        Me.cb_port.TabIndex = 1
        '
        'b_connect
        '
        Me.b_connect.Location = New System.Drawing.Point(166, 11)
        Me.b_connect.Name = "b_connect"
        Me.b_connect.Size = New System.Drawing.Size(78, 27)
        Me.b_connect.TabIndex = 2
        Me.b_connect.Text = "Connect"
        Me.b_connect.UseVisualStyleBackColor = True
        '
        'b_disconnect
        '
        Me.b_disconnect.Location = New System.Drawing.Point(250, 11)
        Me.b_disconnect.Name = "b_disconnect"
        Me.b_disconnect.Size = New System.Drawing.Size(78, 27)
        Me.b_disconnect.TabIndex = 3
        Me.b_disconnect.Text = "Disconnect"
        Me.b_disconnect.UseVisualStyleBackColor = True
        '
        'B_look_on
        '
        Me.B_look_on.Location = New System.Drawing.Point(150, 164)
        Me.B_look_on.Name = "B_look_on"
        Me.B_look_on.Size = New System.Drawing.Size(78, 29)
        Me.B_look_on.TabIndex = 4
        Me.B_look_on.Text = "Look ON"
        Me.B_look_on.UseVisualStyleBackColor = True
        '
        'B_look_off
        '
        Me.B_look_off.Location = New System.Drawing.Point(234, 164)
        Me.B_look_off.Name = "B_look_off"
        Me.B_look_off.Size = New System.Drawing.Size(78, 29)
        Me.B_look_off.TabIndex = 6
        Me.B_look_off.Text = "Look OFF"
        Me.B_look_off.UseVisualStyleBackColor = True
        '
        'b_Tension
        '
        Me.b_Tension.Location = New System.Drawing.Point(250, 67)
        Me.b_Tension.Name = "b_Tension"
        Me.b_Tension.Size = New System.Drawing.Size(78, 29)
        Me.b_Tension.TabIndex = 7
        Me.b_Tension.Text = "ok"
        Me.b_Tension.UseVisualStyleBackColor = True
        '
        'b_Current
        '
        Me.b_Current.Location = New System.Drawing.Point(250, 109)
        Me.b_Current.Name = "b_Current"
        Me.b_Current.Size = New System.Drawing.Size(78, 29)
        Me.b_Current.TabIndex = 8
        Me.b_Current.Text = "ok"
        Me.b_Current.UseVisualStyleBackColor = True
        '
        'Boff
        '
        Me.Boff.Location = New System.Drawing.Point(71, 164)
        Me.Boff.Name = "Boff"
        Me.Boff.Size = New System.Drawing.Size(47, 29)
        Me.Boff.TabIndex = 10
        Me.Boff.Text = "off"
        Me.Boff.UseVisualStyleBackColor = True
        '
        'Bon
        '
        Me.Bon.Location = New System.Drawing.Point(18, 164)
        Me.Bon.Name = "Bon"
        Me.Bon.Size = New System.Drawing.Size(47, 29)
        Me.Bon.TabIndex = 9
        Me.Bon.Text = "on"
        Me.Bon.UseVisualStyleBackColor = True
        '
        'v_Tension
        '
        Me.v_Tension.Location = New System.Drawing.Point(89, 72)
        Me.v_Tension.Name = "v_Tension"
        Me.v_Tension.Size = New System.Drawing.Size(145, 20)
        Me.v_Tension.TabIndex = 11
        '
        'v_Current
        '
        Me.v_Current.Location = New System.Drawing.Point(89, 114)
        Me.v_Current.Name = "v_Current"
        Me.v_Current.Size = New System.Drawing.Size(145, 20)
        Me.v_Current.TabIndex = 12
        '
        'b_info
        '
        Me.b_info.Location = New System.Drawing.Point(358, 166)
        Me.b_info.Name = "b_info"
        Me.b_info.Size = New System.Drawing.Size(144, 27)
        Me.b_info.TabIndex = 13
        Me.b_info.Text = "info"
        Me.b_info.UseVisualStyleBackColor = True
        '
        'tb_out
        '
        Me.tb_out.Location = New System.Drawing.Point(358, 11)
        Me.tb_out.Multiline = True
        Me.tb_out.Name = "tb_out"
        Me.tb_out.Size = New System.Drawing.Size(255, 127)
        Me.tb_out.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Set tension"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Set current"
        '
        't_loop
        '
        Me.t_loop.Interval = 1000
        '
        'cb_loop
        '
        Me.cb_loop.AutoSize = True
        Me.cb_loop.Location = New System.Drawing.Point(532, 172)
        Me.cb_loop.Name = "cb_loop"
        Me.cb_loop.Size = New System.Drawing.Size(70, 17)
        Me.cb_loop.TabIndex = 17
        Me.cb_loop.Text = "Loop (1s)"
        Me.cb_loop.UseVisualStyleBackColor = True
        '
        'main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(629, 222)
        Me.Controls.Add(Me.cb_loop)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tb_out)
        Me.Controls.Add(Me.b_info)
        Me.Controls.Add(Me.v_Current)
        Me.Controls.Add(Me.v_Tension)
        Me.Controls.Add(Me.Boff)
        Me.Controls.Add(Me.Bon)
        Me.Controls.Add(Me.b_Current)
        Me.Controls.Add(Me.b_Tension)
        Me.Controls.Add(Me.B_look_off)
        Me.Controls.Add(Me.B_look_on)
        Me.Controls.Add(Me.b_disconnect)
        Me.Controls.Add(Me.b_connect)
        Me.Controls.Add(Me.cb_port)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "main"
        Me.Text = "Remato contro RD DPS3005"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cb_port As System.Windows.Forms.ComboBox
    Friend WithEvents b_connect As System.Windows.Forms.Button
    Friend WithEvents b_disconnect As System.Windows.Forms.Button
    Friend WithEvents B_look_on As System.Windows.Forms.Button
    Friend WithEvents B_look_off As System.Windows.Forms.Button
    Friend WithEvents b_Tension As System.Windows.Forms.Button
    Friend WithEvents b_Current As System.Windows.Forms.Button
    Friend WithEvents Boff As System.Windows.Forms.Button
    Friend WithEvents Bon As System.Windows.Forms.Button
    Friend WithEvents v_Tension As System.Windows.Forms.TextBox
    Friend WithEvents v_Current As System.Windows.Forms.TextBox
    Friend WithEvents b_info As System.Windows.Forms.Button
    Friend WithEvents tb_out As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents t_loop As System.Windows.Forms.Timer
    Friend WithEvents cb_loop As System.Windows.Forms.CheckBox

End Class
