﻿Imports System.IO.Ports
Imports System.Text

Public Class DPS3005

    Public baudRate As Integer = 9600
    Public parity As Boolean = False
    Public stopBits As Integer = 1
    Public databits As Integer = 8

    WithEvents sp As SerialPort
    Public Event DataReceivedRAW(data As Byte())
    Public Event DataReceived(data As infoDPS3005)


    Sub New()
        ConstructTable()
        sp = New SerialPort()

    End Sub
    Public Shared Function ConvertIntToByteArray(ByVal I As Integer) As Byte()
        Return BitConverter.GetBytes(I)
    End Function


    Public Function get_info() As Boolean
        Dim message() As Byte = New Byte() {1, 3, 0, 0, 0, 14}
        Return modbus_send(message)
    End Function



    Public Function look_enable() As Boolean
        Dim message() As Byte = New Byte() {1, 16, 0, 6, 0, 1, 2, 0, 1}
        Return modbus_send(message)
    End Function
    Public Function look_disable() As Boolean
        Dim message() As Byte = New Byte() {1, 16, 0, 6, 0, 1, 2, 0, 0}
        Return modbus_send(message)
    End Function

    Public Function enable() As Boolean
        Dim message() As Byte = New Byte() {1, 16, 0, 9, 0, 1, 2, 0, 1}
        Return modbus_send(message)
    End Function

    Public Function disable() As Boolean
        Dim message() As Byte = New Byte() {1, 16, 0, 9, 0, 1, 2, 0, 0}
        Return modbus_send(message)
    End Function


    Public Function set_tension(tension As Decimal) As Boolean
        Dim val As Integer
        val = tension * 100
        Dim valByte As Byte()
        valByte = ConvertIntToByteArray(Val)
        Dim message() As Byte = New Byte() {1, 16, 0, 0, 0, 1, 2, valByte(1), valByte(0)}
        Return modbus_send(message)
    End Function


    Public Function set_current(current As Decimal) As Boolean
        Dim val As Integer
        val = current * 1000
        Dim valByte As Byte()
        valByte = ConvertIntToByteArray(val)
        Dim message() As Byte = New Byte() {1, 16, 0, 1, 0, 1, 2, valByte(1), valByte(0)}
        Return modbus_send(message)
    End Function


#Region "Open / Close"

    Public Function Open(portName As String) As Boolean
        If Not (sp.IsOpen) Then
            sp.PortName = portName
            sp.BaudRate = baudRate
            sp.DataBits = databits
            sp.Parity = parity
            sp.StopBits = stopBits
            sp.ReadTimeout = 1000
            sp.WriteTimeout = 1000
            Try
                sp.Open()
                Return True
            Catch ex As Exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function


    Public Function Close() As Boolean
        If (sp.IsOpen) Then
            Try
                sp.Close()
                Return True
            Catch ex As Exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function


#End Region


    Public Sub PortListComboBox(ByRef cb As ComboBox)
        For Each port As String In SerialPort.GetPortNames()
            cb.Items.Add(port)
        Next
        If (cb.Items.Count = 0) Then
            MsgBox("Error serial port")
            End
        End If
        cb.SelectedIndex = 0
    End Sub




    Public Function modbus_send(message As Byte()) As Boolean
        If Not (sp.IsOpen) Then Return False
        Dim send_data() As Byte
        send_data = addCRC16(message)
        sp.Write(send_data, 0, send_data.Length)
        Return True
    End Function



    Private Sub sp_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles sp.DataReceived
        Dim lenbuff As Integer = sp.BytesToRead
        Dim array() As Byte = New Byte((lenbuff) - 1) {}
        Dim i As Integer = 0
        While (sp.BytesToRead > 0)
            array(i) = sp.ReadByte
            i = (i + 1)
        End While

        Try
            If array.Length < 3 Then
                RaiseEvent DataReceivedRAW(array)
            End If

            If (array(0) = 1 And array(1) = 3) Then
                RaiseEvent DataReceived(New infoDPS3005(array))
            End If
        Catch ex As Exception
            RaiseEvent DataReceivedRAW(array)
        End Try

    End Sub

    Public Function Bytes_To_String2(ByVal bytes_Input As Byte()) As String
        Dim strTemp As New StringBuilder(bytes_Input.Length * 2)
        For Each b As Byte In bytes_Input
            strTemp.Append(b.ToString("X02"))
        Next
        Return strTemp.ToString()
    End Function


#Region "CRC-16-DNP (DNP, IEC 870, ModBus)  Reversed "

    Public CRC_Table(512) As Byte
    Private Sub ConstructTable()
        CRC_Table(0) = &H0
        CRC_Table(1) = &HC1
        CRC_Table(2) = &H81
        CRC_Table(3) = &H40
        CRC_Table(4) = &H1
        CRC_Table(5) = &HC0
        CRC_Table(6) = &H80
        CRC_Table(7) = &H41
        CRC_Table(8) = &H1
        CRC_Table(9) = &HC0
        CRC_Table(10) = &H80
        CRC_Table(11) = &H41
        CRC_Table(12) = &H0
        CRC_Table(13) = &HC1
        CRC_Table(14) = &H81
        CRC_Table(15) = &H40
        CRC_Table(16) = &H1
        CRC_Table(17) = &HC0
        CRC_Table(18) = &H80
        CRC_Table(19) = &H41
        CRC_Table(20) = &H0
        CRC_Table(21) = &HC1
        CRC_Table(22) = &H81
        CRC_Table(23) = &H40
        CRC_Table(24) = &H0
        CRC_Table(25) = &HC1
        CRC_Table(26) = &H81
        CRC_Table(27) = &H40
        CRC_Table(28) = &H1
        CRC_Table(29) = &HC0
        CRC_Table(30) = &H80
        CRC_Table(31) = &H41
        CRC_Table(32) = &H1
        CRC_Table(33) = &HC0
        CRC_Table(34) = &H80
        CRC_Table(35) = &H41
        CRC_Table(36) = &H0
        CRC_Table(37) = &HC1
        CRC_Table(38) = &H81
        CRC_Table(39) = &H40
        CRC_Table(40) = &H0
        CRC_Table(41) = &HC1
        CRC_Table(42) = &H81
        CRC_Table(43) = &H40
        CRC_Table(44) = &H1
        CRC_Table(45) = &HC0
        CRC_Table(46) = &H80
        CRC_Table(47) = &H41
        CRC_Table(48) = &H0
        CRC_Table(49) = &HC1
        CRC_Table(50) = &H81
        CRC_Table(51) = &H40
        CRC_Table(52) = &H1
        CRC_Table(53) = &HC0
        CRC_Table(54) = &H80
        CRC_Table(55) = &H41
        CRC_Table(56) = &H1
        CRC_Table(57) = &HC0
        CRC_Table(58) = &H80
        CRC_Table(59) = &H41
        CRC_Table(60) = &H0
        CRC_Table(61) = &HC1
        CRC_Table(62) = &H81
        CRC_Table(63) = &H40
        CRC_Table(64) = &H1
        CRC_Table(65) = &HC0
        CRC_Table(66) = &H80
        CRC_Table(67) = &H41
        CRC_Table(68) = &H0
        CRC_Table(69) = &HC1
        CRC_Table(70) = &H81
        CRC_Table(71) = &H40
        CRC_Table(72) = &H0
        CRC_Table(73) = &HC1
        CRC_Table(74) = &H81
        CRC_Table(75) = &H40
        CRC_Table(76) = &H1
        CRC_Table(77) = &HC0
        CRC_Table(78) = &H80
        CRC_Table(79) = &H41
        CRC_Table(80) = &H0
        CRC_Table(81) = &HC1
        CRC_Table(82) = &H81
        CRC_Table(83) = &H40
        CRC_Table(84) = &H1
        CRC_Table(85) = &HC0
        CRC_Table(86) = &H80
        CRC_Table(87) = &H41
        CRC_Table(88) = &H1
        CRC_Table(89) = &HC0
        CRC_Table(90) = &H80
        CRC_Table(91) = &H41
        CRC_Table(92) = &H0
        CRC_Table(93) = &HC1
        CRC_Table(94) = &H81
        CRC_Table(95) = &H40
        CRC_Table(96) = &H0
        CRC_Table(97) = &HC1
        CRC_Table(98) = &H81
        CRC_Table(99) = &H40
        CRC_Table(100) = &H1
        CRC_Table(101) = &HC0
        CRC_Table(102) = &H80
        CRC_Table(103) = &H41
        CRC_Table(104) = &H1
        CRC_Table(105) = &HC0
        CRC_Table(106) = &H80
        CRC_Table(107) = &H41
        CRC_Table(108) = &H0
        CRC_Table(109) = &HC1
        CRC_Table(110) = &H81
        CRC_Table(111) = &H40
        CRC_Table(112) = &H1
        CRC_Table(113) = &HC0
        CRC_Table(114) = &H80
        CRC_Table(115) = &H41
        CRC_Table(116) = &H0
        CRC_Table(117) = &HC1
        CRC_Table(118) = &H81
        CRC_Table(119) = &H40
        CRC_Table(120) = &H0
        CRC_Table(121) = &HC1
        CRC_Table(122) = &H81
        CRC_Table(123) = &H40
        CRC_Table(124) = &H1
        CRC_Table(125) = &HC0
        CRC_Table(126) = &H80
        CRC_Table(127) = &H41
        CRC_Table(128) = &H1
        CRC_Table(129) = &HC0
        CRC_Table(130) = &H80
        CRC_Table(131) = &H41
        CRC_Table(132) = &H0
        CRC_Table(133) = &HC1
        CRC_Table(134) = &H81
        CRC_Table(135) = &H40
        CRC_Table(136) = &H0
        CRC_Table(137) = &HC1
        CRC_Table(138) = &H81
        CRC_Table(139) = &H40
        CRC_Table(140) = &H1
        CRC_Table(141) = &HC0
        CRC_Table(142) = &H80
        CRC_Table(143) = &H41
        CRC_Table(144) = &H0
        CRC_Table(145) = &HC1
        CRC_Table(146) = &H81
        CRC_Table(147) = &H40
        CRC_Table(148) = &H1
        CRC_Table(149) = &HC0
        CRC_Table(150) = &H80
        CRC_Table(151) = &H41
        CRC_Table(152) = &H1
        CRC_Table(153) = &HC0
        CRC_Table(154) = &H80
        CRC_Table(155) = &H41
        CRC_Table(156) = &H0
        CRC_Table(157) = &HC1
        CRC_Table(158) = &H81
        CRC_Table(159) = &H40
        CRC_Table(160) = &H0
        CRC_Table(161) = &HC1
        CRC_Table(162) = &H81
        CRC_Table(163) = &H40
        CRC_Table(164) = &H1
        CRC_Table(165) = &HC0
        CRC_Table(166) = &H80
        CRC_Table(167) = &H41
        CRC_Table(168) = &H1
        CRC_Table(169) = &HC0
        CRC_Table(170) = &H80
        CRC_Table(171) = &H41
        CRC_Table(172) = &H0
        CRC_Table(173) = &HC1
        CRC_Table(174) = &H81
        CRC_Table(175) = &H40
        CRC_Table(176) = &H1
        CRC_Table(177) = &HC0
        CRC_Table(178) = &H80
        CRC_Table(179) = &H41
        CRC_Table(180) = &H0
        CRC_Table(181) = &HC1
        CRC_Table(182) = &H81
        CRC_Table(183) = &H40
        CRC_Table(184) = &H0
        CRC_Table(185) = &HC1
        CRC_Table(186) = &H81
        CRC_Table(187) = &H40
        CRC_Table(188) = &H1
        CRC_Table(189) = &HC0
        CRC_Table(190) = &H80
        CRC_Table(191) = &H41
        CRC_Table(192) = &H0
        CRC_Table(193) = &HC1
        CRC_Table(194) = &H81
        CRC_Table(195) = &H40
        CRC_Table(196) = &H1
        CRC_Table(197) = &HC0
        CRC_Table(198) = &H80
        CRC_Table(199) = &H41
        CRC_Table(200) = &H1
        CRC_Table(201) = &HC0
        CRC_Table(202) = &H80
        CRC_Table(203) = &H41
        CRC_Table(204) = &H0
        CRC_Table(205) = &HC1
        CRC_Table(206) = &H81
        CRC_Table(207) = &H40
        CRC_Table(208) = &H1
        CRC_Table(209) = &HC0
        CRC_Table(210) = &H80
        CRC_Table(211) = &H41
        CRC_Table(212) = &H0
        CRC_Table(213) = &HC1
        CRC_Table(214) = &H81
        CRC_Table(215) = &H40
        CRC_Table(216) = &H0
        CRC_Table(217) = &HC1
        CRC_Table(218) = &H81
        CRC_Table(219) = &H40
        CRC_Table(220) = &H1
        CRC_Table(221) = &HC0
        CRC_Table(222) = &H80
        CRC_Table(223) = &H41
        CRC_Table(224) = &H1
        CRC_Table(225) = &HC0
        CRC_Table(226) = &H80
        CRC_Table(227) = &H41
        CRC_Table(228) = &H0
        CRC_Table(229) = &HC1
        CRC_Table(230) = &H81
        CRC_Table(231) = &H40
        CRC_Table(232) = &H0
        CRC_Table(233) = &HC1
        CRC_Table(234) = &H81
        CRC_Table(235) = &H40
        CRC_Table(236) = &H1
        CRC_Table(237) = &HC0
        CRC_Table(238) = &H80
        CRC_Table(239) = &H41
        CRC_Table(240) = &H0
        CRC_Table(241) = &HC1
        CRC_Table(242) = &H81
        CRC_Table(243) = &H40
        CRC_Table(244) = &H1
        CRC_Table(245) = &HC0
        CRC_Table(246) = &H80
        CRC_Table(247) = &H41
        CRC_Table(248) = &H1
        CRC_Table(249) = &HC0
        CRC_Table(250) = &H80
        CRC_Table(251) = &H41
        CRC_Table(252) = &H0
        CRC_Table(253) = &HC1
        CRC_Table(254) = &H81
        CRC_Table(255) = &H40
        CRC_Table(256) = &H0
        CRC_Table(257) = &HC0
        CRC_Table(258) = &HC1
        CRC_Table(259) = &H1
        CRC_Table(260) = &HC3
        CRC_Table(261) = &H3
        CRC_Table(262) = &H2
        CRC_Table(263) = &HC2
        CRC_Table(264) = &HC6
        CRC_Table(265) = &H6
        CRC_Table(266) = &H7
        CRC_Table(267) = &HC7
        CRC_Table(268) = &H5
        CRC_Table(269) = &HC5
        CRC_Table(270) = &HC4
        CRC_Table(271) = &H4
        CRC_Table(272) = &HCC
        CRC_Table(273) = &HC
        CRC_Table(274) = &HD
        CRC_Table(275) = &HCD
        CRC_Table(276) = &HF
        CRC_Table(277) = &HCF
        CRC_Table(278) = &HCE
        CRC_Table(279) = &HE
        CRC_Table(280) = &HA
        CRC_Table(281) = &HCA
        CRC_Table(282) = &HCB
        CRC_Table(283) = &HB
        CRC_Table(284) = &HC9
        CRC_Table(285) = &H9
        CRC_Table(286) = &H8
        CRC_Table(287) = &HC8
        CRC_Table(288) = &HD8
        CRC_Table(289) = &H18
        CRC_Table(290) = &H19
        CRC_Table(291) = &HD9
        CRC_Table(292) = &H1B
        CRC_Table(293) = &HDB
        CRC_Table(294) = &HDA
        CRC_Table(295) = &H1A
        CRC_Table(296) = &H1E
        CRC_Table(297) = &HDE
        CRC_Table(298) = &HDF
        CRC_Table(299) = &H1F
        CRC_Table(300) = &HDD
        CRC_Table(301) = &H1D
        CRC_Table(302) = &H1C
        CRC_Table(303) = &HDC
        CRC_Table(304) = &H14
        CRC_Table(305) = &HD4
        CRC_Table(306) = &HD5
        CRC_Table(307) = &H15
        CRC_Table(308) = &HD7
        CRC_Table(309) = &H17
        CRC_Table(310) = &H16
        CRC_Table(311) = &HD6
        CRC_Table(312) = &HD2
        CRC_Table(313) = &H12
        CRC_Table(314) = &H13
        CRC_Table(315) = &HD3
        CRC_Table(316) = &H11
        CRC_Table(317) = &HD1
        CRC_Table(318) = &HD0
        CRC_Table(319) = &H10
        CRC_Table(320) = &HF0
        CRC_Table(321) = &H30
        CRC_Table(322) = &H31
        CRC_Table(323) = &HF1
        CRC_Table(324) = &H33
        CRC_Table(325) = &HF3
        CRC_Table(326) = &HF2
        CRC_Table(327) = &H32
        CRC_Table(328) = &H36
        CRC_Table(329) = &HF6
        CRC_Table(330) = &HF7
        CRC_Table(331) = &H37
        CRC_Table(332) = &HF5
        CRC_Table(333) = &H35
        CRC_Table(334) = &H34
        CRC_Table(335) = &HF4
        CRC_Table(336) = &H3C
        CRC_Table(337) = &HFC
        CRC_Table(338) = &HFD
        CRC_Table(339) = &H3D
        CRC_Table(340) = &HFF
        CRC_Table(341) = &H3F
        CRC_Table(342) = &H3E
        CRC_Table(343) = &HFE
        CRC_Table(344) = &HFA
        CRC_Table(345) = &H3A
        CRC_Table(346) = &H3B
        CRC_Table(347) = &HFB
        CRC_Table(348) = &H39
        CRC_Table(349) = &HF9
        CRC_Table(350) = &HF8
        CRC_Table(351) = &H38
        CRC_Table(352) = &H28
        CRC_Table(353) = &HE8
        CRC_Table(354) = &HE9
        CRC_Table(355) = &H29
        CRC_Table(356) = &HEB
        CRC_Table(357) = &H2B
        CRC_Table(358) = &H2A
        CRC_Table(359) = &HEA
        CRC_Table(360) = &HEE
        CRC_Table(361) = &H2E
        CRC_Table(362) = &H2F
        CRC_Table(363) = &HEF
        CRC_Table(364) = &H2D
        CRC_Table(365) = &HED
        CRC_Table(366) = &HEC
        CRC_Table(367) = &H2C
        CRC_Table(368) = &HE4
        CRC_Table(369) = &H24
        CRC_Table(370) = &H25
        CRC_Table(371) = &HE5
        CRC_Table(372) = &H27
        CRC_Table(373) = &HE7
        CRC_Table(374) = &HE6
        CRC_Table(375) = &H26
        CRC_Table(376) = &H22
        CRC_Table(377) = &HE2
        CRC_Table(378) = &HE3
        CRC_Table(379) = &H23
        CRC_Table(380) = &HE1
        CRC_Table(381) = &H21
        CRC_Table(382) = &H20
        CRC_Table(383) = &HE0
        CRC_Table(384) = &HA0
        CRC_Table(385) = &H60
        CRC_Table(386) = &H61
        CRC_Table(387) = &HA1
        CRC_Table(388) = &H63
        CRC_Table(389) = &HA3
        CRC_Table(390) = &HA2
        CRC_Table(391) = &H62
        CRC_Table(392) = &H66
        CRC_Table(393) = &HA6
        CRC_Table(394) = &HA7
        CRC_Table(395) = &H67
        CRC_Table(396) = &HA5
        CRC_Table(397) = &H65
        CRC_Table(398) = &H64
        CRC_Table(399) = &HA4
        CRC_Table(400) = &H6C
        CRC_Table(401) = &HAC
        CRC_Table(402) = &HAD
        CRC_Table(403) = &H6D
        CRC_Table(404) = &HAF
        CRC_Table(405) = &H6F
        CRC_Table(406) = &H6E
        CRC_Table(407) = &HAE
        CRC_Table(408) = &HAA
        CRC_Table(409) = &H6A
        CRC_Table(410) = &H6B
        CRC_Table(411) = &HAB
        CRC_Table(412) = &H69
        CRC_Table(413) = &HA9
        CRC_Table(414) = &HA8
        CRC_Table(415) = &H68
        CRC_Table(416) = &H78
        CRC_Table(417) = &HB8
        CRC_Table(418) = &HB9
        CRC_Table(419) = &H79
        CRC_Table(420) = &HBB
        CRC_Table(421) = &H7B
        CRC_Table(422) = &H7A
        CRC_Table(423) = &HBA
        CRC_Table(424) = &HBE
        CRC_Table(425) = &H7E
        CRC_Table(426) = &H7F
        CRC_Table(427) = &HBF
        CRC_Table(428) = &H7D
        CRC_Table(429) = &HBD
        CRC_Table(430) = &HBC
        CRC_Table(431) = &H7C
        CRC_Table(432) = &HB4
        CRC_Table(433) = &H74
        CRC_Table(434) = &H75
        CRC_Table(435) = &HB5
        CRC_Table(436) = &H77
        CRC_Table(437) = &HB7
        CRC_Table(438) = &HB6
        CRC_Table(439) = &H76
        CRC_Table(440) = &H72
        CRC_Table(441) = &HB2
        CRC_Table(442) = &HB3
        CRC_Table(443) = &H73
        CRC_Table(444) = &HB1
        CRC_Table(445) = &H71
        CRC_Table(446) = &H70
        CRC_Table(447) = &HB0
        CRC_Table(448) = &H50
        CRC_Table(449) = &H90
        CRC_Table(450) = &H91
        CRC_Table(451) = &H51
        CRC_Table(452) = &H93
        CRC_Table(453) = &H53
        CRC_Table(454) = &H52
        CRC_Table(455) = &H92
        CRC_Table(456) = &H96
        CRC_Table(457) = &H56
        CRC_Table(458) = &H57
        CRC_Table(459) = &H97
        CRC_Table(460) = &H55
        CRC_Table(461) = &H95
        CRC_Table(462) = &H94
        CRC_Table(463) = &H54
        CRC_Table(464) = &H9C
        CRC_Table(465) = &H5C
        CRC_Table(466) = &H5D
        CRC_Table(467) = &H9D
        CRC_Table(468) = &H5F
        CRC_Table(469) = &H9F
        CRC_Table(470) = &H9E
        CRC_Table(471) = &H5E
        CRC_Table(472) = &H5A
        CRC_Table(473) = &H9A
        CRC_Table(474) = &H9B
        CRC_Table(475) = &H5B
        CRC_Table(476) = &H99
        CRC_Table(477) = &H59
        CRC_Table(478) = &H58
        CRC_Table(479) = &H98
        CRC_Table(480) = &H88
        CRC_Table(481) = &H48
        CRC_Table(482) = &H49
        CRC_Table(483) = &H89
        CRC_Table(484) = &H4B
        CRC_Table(485) = &H8B
        CRC_Table(486) = &H8A
        CRC_Table(487) = &H4A
        CRC_Table(488) = &H4E
        CRC_Table(489) = &H8E
        CRC_Table(490) = &H8F
        CRC_Table(491) = &H4F
        CRC_Table(492) = &H8D
        CRC_Table(493) = &H4D
        CRC_Table(494) = &H4C
        CRC_Table(495) = &H8C
        CRC_Table(496) = &H44
        CRC_Table(497) = &H84
        CRC_Table(498) = &H85
        CRC_Table(499) = &H45
        CRC_Table(500) = &H87
        CRC_Table(501) = &H47
        CRC_Table(502) = &H46
        CRC_Table(503) = &H86
        CRC_Table(504) = &H82
        CRC_Table(505) = &H42
        CRC_Table(506) = &H43
        CRC_Table(507) = &H83
        CRC_Table(508) = &H41
        CRC_Table(509) = &H81
        CRC_Table(510) = &H80
        CRC_Table(511) = &H40
    End Sub

    '
    Public Function addCRC16(ByRef data() As Byte) As Byte()

        Dim index As Int16
        Dim crcHigh As Int16 = &HFF
        Dim crcLow As Int16 = &HFF

        For iCount As Int16 = 0 To data.Length - 1
            index = crcHigh Xor data(iCount)
            crcHigh = crcLow Xor CRC_Table(index)
            crcLow = Convert.ToByte(CRC_Table(index + 256))
        Next

        Dim ReturnData(data.Length + 1) As Byte

        For i = 0 To data.Length - 1
            ReturnData(i) = data(i)
        Next

        ReturnData(data.Length) = crcHigh
        ReturnData(data.Length + 1) = crcLow
        Return ReturnData

    End Function


#End Region




End Class



Public Class infoDPS3005
    Public U_Out As Decimal
    Public I_Out As Decimal
    Public P_Out As Decimal
    Public U_in As Decimal
    Public Model As String
    Public Firmware As String


    Sub New(Data As Byte())
        U_Out = (256 * Data(7) + Data(8)) / 100
        I_Out = (256 * Data(9) + Data(10)) / 1000
        P_Out = (256 * Data(9) + Data(10)) / 1000
        U_in = (256 * Data(13) + Data(14)) / 100
        Model = (256 * Data(25) + Data(26)).ToString
        Firmware = ((256 * Data(27) + Data(28)) / 10).ToString
    End Sub

End Class