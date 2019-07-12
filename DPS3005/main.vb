Public Class main

    Dim WithEvents tmp_DPS3005 As New DPS3005
    Dim statePort As Boolean
    Dim flag_send As Boolean
    Dim temp_time As Boolean

    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        tmp_DPS3005.PortListComboBox(cb_port)
        statePort = False
        forms_reandaring_port()

    End Sub


    Private Sub b_connect_Click(sender As Object, e As EventArgs) Handles b_connect.Click
        statePort = tmp_DPS3005.Open(cb_port.Text)
        forms_reandaring_port()
    End Sub

    Private Sub b_disconnect_Click(sender As Object, e As EventArgs) Handles b_disconnect.Click
        statePort = tmp_DPS3005.Close()
        forms_reandaring_port()
    End Sub

    Sub forms_reandaring_port()
        If (statePort) Then
            b_connect.Enabled = False
            b_disconnect.Enabled = True
        Else
            b_connect.Enabled = True
            b_disconnect.Enabled = False
        End If

        t_loop.Enabled = Not b_connect.Enabled
        cb_loop.Checked = Not b_connect.Enabled

    End Sub




    Private Sub B_look_on_Click(sender As Object, e As EventArgs) Handles B_look_on.Click
        temp_time = t_loop.Enabled
        Try
            t_loop.Enabled = False
            tmp_DPS3005.look_enable()
            t_loop.Enabled = temp_time
        Catch ex As Exception
            MsgBox("Errore input")
            t_loop.Enabled = temp_time
        End Try
    End Sub

    Private Sub B_look_off_Click(sender As Object, e As EventArgs) Handles B_look_off.Click
        temp_time = t_loop.Enabled
        Try
            t_loop.Enabled = False
            tmp_DPS3005.look_disable()
            t_loop.Enabled = temp_time
        Catch ex As Exception
            MsgBox("Errore input")
            t_loop.Enabled = temp_time
        End Try
    End Sub

    Private Sub Bon_Click(sender As Object, e As EventArgs) Handles Bon.Click
        temp_time = t_loop.Enabled
        Try
            t_loop.Enabled = False
            tmp_DPS3005.enable()
            t_loop.Enabled = temp_time
        Catch ex As Exception
            MsgBox("Errore input")
            t_loop.Enabled = temp_time
        End Try
    End Sub

    Private Sub Boff_Click(sender As Object, e As EventArgs) Handles Boff.Click
        temp_time = t_loop.Enabled
        Try
            t_loop.Enabled = False
            tmp_DPS3005.disable()
            t_loop.Enabled = temp_time
        Catch ex As Exception
            MsgBox("Errore input")
            t_loop.Enabled = temp_time
        End Try
    End Sub

    Dim var As Decimal
    Private Sub b_Tension_Click(sender As Object, e As EventArgs) Handles b_Tension.Click
        temp_time = t_loop.Enabled
        Try
            t_loop.Enabled = False
            var = Me.v_Tension.Text
            tmp_DPS3005.set_tension(var)
            t_loop.Enabled = temp_time
        Catch ex As Exception
            MsgBox("Errore input")
            t_loop.Enabled = temp_time
        End Try
    End Sub

    Private Sub b_Current_Click(sender As Object, e As EventArgs) Handles b_Current.Click
        Try
            var = Me.v_Current.Text
            tmp_DPS3005.set_current(var)
        Catch ex As Exception
            MsgBox("Errore input")
        End Try

    End Sub

    Private Sub b_info_Click(sender As Object, e As EventArgs) Handles b_info.Click
        tmp_DPS3005.get_info()
    End Sub

    Private Sub t_loop_Tick(sender As Object, e As EventArgs) Handles t_loop.Tick
        tmp_DPS3005.get_info()
    End Sub
    Private Sub cb_loop_CheckedChanged(sender As Object, e As EventArgs) Handles cb_loop.CheckedChanged
        t_loop.Enabled = cb_loop.Checked
    End Sub

    Dim tmps As String = ""
    Private Sub tmp_DPS3005_DataReceived(data As infoDPS3005) Handles tmp_DPS3005.DataReceived
        tmps = "U-Out " & data.U_Out.ToString & vbCrLf
        tmps &= "I-Out " & data.I_Out.ToString & vbCrLf
        tmps &= "P-Out " & data.P_Out.ToString & vbCrLf
        tmps &= "U-in " & data.I_Out.ToString & vbCrLf
        tmps &= "Model " & data.Model & vbCrLf
        tmps &= "Firmware " & data.Firmware & vbCrLf

        Me.Invoke(Sub()
                      tb_out.Text = tmps
                  End Sub)
    End Sub
End Class
