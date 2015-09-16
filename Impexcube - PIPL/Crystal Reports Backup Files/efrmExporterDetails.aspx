<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="efrmExporterDetails.aspx.cs" Inherits="ImpexCube.efrmExporterDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: left;
        }
        .style2
        {
            font-family: Verdana;
            font-size: 8pt;
            font-weight: 700;
            text-align: left;
        }
    </style>
    <script type="text/javascript">
        function DisableBuyer() {
            var chkBuyer = document.getElementById('ContentPlaceHolder1_cbBuyer').checked;
            if (chkBuyer == true) {
                document.getElementById('ContentPlaceHolder1_txtBuyer').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtBuyerAddress').disabled = false;
                return false;
            }
            else {
                document.getElementById('ContentPlaceHolder1_txtBuyer').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtBuyerAddress').disabled = true;
                return false;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td colspan="5" align="center">
                <asp:Label ID="lblExporterDetails" runat="server" Text="Exporter Details" Style="font-weight: 700"></asp:Label>
                &nbsp;
            </td>
            <td align="center" rowspan="16" valign="top">
                <table>
                    <tr>
                        <td colspan="2" align="center">
                            &nbsp; &nbsp; &nbsp; Job Details
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label42" runat="server" CssClass="fontsizehistory" Text="Job No"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlJobnoExporter" runat="server" AutoPostBack="True" CssClass="ddl100"
                                Height="20px" Width="130px" OnSelectedIndexChanged="ddlJobnoExporter_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="Label32" runat="server" CssClass="fontsizehistory" Text="Job Date"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblJobReceivedDate" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label33" runat="server" CssClass="fontsizehistory" Text="Mode"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lblMode" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label34" runat="server" CssClass="fontsizehistory" Text="Custom"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lblCustom" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label35" runat="server" CssClass="fontsizehistory" Text="SB Type"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lblBEType" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label36" runat="server" CssClass="fontsizehistory" Text="Doc Filling"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lblDocFillingStatus" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label37" runat="server" CssClass="fontsizehistory" Text="SB No"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lblBENo" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label38" runat="server" CssClass="fontsizehistory" Text="SB Date"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lblBEDate" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label39" runat="server" CssClass="fontsizehistory" Text="Job Appr."></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lblJobApprovedBy" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label40" runat="server" CssClass="fontsizehistory" Text="Duty Pay. Date"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lblDutyPaymentDate" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label41" runat="server" CssClass="fontsizehistory" Text="Oversea Date"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="lblOverseaDate" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblmsg" runat="server" Style="font-weight: 700" CssClass="fontsize"></asp:Label>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblExporter" runat="server" Text="Exporter" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtExporter" runat="server" CssClass="textbox400"></asp:TextBox>
                <asp:ImageButton ID="ibConsignee0" runat="server" Height="18px" 
                    ImageUrl="~/Content/Images/Search1.png" 
                    onclientclick=" popupwindow('frmPopUpConsigner.aspx?mode=Exp');" />
            </td>
            <td>
                <asp:Button ID="BbtnExportNew" runat="server" Text="New" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblExporterRef" runat="server" Text="Exporter Ref No/Date" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtExporterRefNo" runat="server" CssClass="textbox200"></asp:TextBox>
                <asp:TextBox ID="txtExporterRefDate" runat="server" CssClass="textbox75"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExporterRefDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td>
                <asp:Label ID="lblExporterType" runat="server" Text="Exporter Type" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlExporterType" runat="server" CssClass="ddl200" 
                    OnSelectedIndexChanged="ddlExporterType_SelectedIndexChanged">
                    <asp:ListItem Text="~Select~" Value="~Select~"></asp:ListItem>
                    <asp:ListItem Text="Manufacturer Exporter" Value="Manufacturer Exporter"></asp:ListItem>
                    <asp:ListItem Text="Merchant Exporter" Value="Merchant Exporter"></asp:ListItem>
                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblExporterAddress" runat="server" Text="Address" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtExporterAddress" runat="server" TextMode="MultiLine" 
                    CssClass="textboxMulti200"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblSBNo" runat="server" Text="SB No/Date" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSBNo" runat="server" CssClass="textbox200" 
                    OnTextChanged="txtSBNo_TextChanged"></asp:TextBox>
                <asp:TextBox ID="txtSBDate" runat="server" CssClass="textbox75"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSBDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblBranchSNo" runat="server" Text="Branch SNo" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtBranchSNo" runat="server" CssClass="textbox75"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblRbiNo" runat="server" Text="RBI Appr.No & Date" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRbiNo" runat="server" CssClass="textbox200"></asp:TextBox>
                <asp:TextBox ID="txtRbiDate" runat="server" CssClass="textbox75"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtRbiDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblStateProvince" runat="server" Text="State/Province" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtStateProvince" runat="server" CssClass="textbox200"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblGRWaived" runat="server" Text="GR Waived" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="cbGRWaived" runat="server" Text="GR No" CssClass="fontsize" />
               <asp:TextBox ID="txtWavierNo" runat="server" CssClass="textbox140"></asp:TextBox>
                <asp:TextBox ID="txtWavierNoExtn" runat="server" CssClass="textbox75"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtWavierNoExtn"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblIECode" runat="server" Text="IE Code No" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtIECode" runat="server" CssClass="textbox200"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblRbiWavierNo" runat="server" Text="RBI Wavier No" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRbiWavierNo" runat="server" CssClass="textbox200"></asp:TextBox>
                <asp:TextBox ID="txtRbiWavierExtn" runat="server" CssClass="textbox75"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtRbiWavierExtn"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblBankDealer" runat="server" Text="Bank/Dealer" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBankDealer" runat="server" TextMode="MultiLine" 
                    CssClass="textboxMulti200"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConsginee" runat="server" Text="Consignee" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtConsignee" runat="server" CssClass="textbox200" 
                    OnTextChanged="txtConsignee_TextChanged"></asp:TextBox>
                <asp:ImageButton ID="ibConsignee" runat="server" Height="18px" 
                    ImageUrl="~/Content/Images/Search1.png" 
                    onclientclick="popupwindow('frmPopUpConsigner.aspx?mode=ExpCnsr');" />
                <asp:Button ID="btnConsigneeNew" runat="server" Text="New" Visible="False" />
            </td>
            <td>
                <asp:Label ID="lblAccNo" runat="server" Text="A/C No" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAccNo" runat="server" CssClass="textbox200"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConsigneeAddress" runat="server" Text="Address" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtConsigneeAddress" runat="server" CssClass="textboxMulti200" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblBankDealerCode" runat="server" Text="Bank/Dealer Code" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBankDealerCode" runat="server" CssClass="textbox200"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCosigneeCountry" runat="server" Text="Cons Country" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtCosigneeCountry" runat="server" CssClass="textbox200"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblEpzCode" runat="server" Text="EPZ Code" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEpzCode" runat="server" CssClass="textbox75"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblBuyer" runat="server" Text="Buyer" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:CheckBox ID="cbBuyer" runat="server" CssClass="fontsize" onChange="javascript:return DisableBuyer();" />
                <asp:TextBox ID="txtBuyer" runat="server" CssClass="textbox175"></asp:TextBox>
                <asp:ImageButton ID="ibBuyer" runat="server" Height="18px" 
                    ImageUrl="~/Content/Images/Search1.png" 
                    onclientclick="popupwindow('frmPopUpConsigner.aspx?mode=ExpBuyer');" />
            </td>
            <td>
                <asp:Label ID="lblNotify" runat="server" Text="Notify" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNotify" runat="server" CssClass="textbox200"></asp:TextBox>
                <asp:ImageButton ID="ibNotify" runat="server" Height="18px" 
                    ImageUrl="~/Content/Images/Search1.png" 
                    onclientclick="popupwindow('frmPopUpConsigner.aspx?mode=Notify');" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblBuyerAddress" runat="server" Text="Address" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtBuyerAddress" runat="server" TextMode="MultiLine" 
                    CssClass="textboxMulti200"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblAddress" runat="server" Text="Address" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAddressExtn" runat="server" TextMode="MultiLine" 
                    CssClass="textboxMulti200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="stylebutton" OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="stylebutton" OnClick="btnUpdate_Click" />
                &nbsp;
                <asp:Button ID="btnReturn" runat="server" Text="Back To Job Creation" CssClass="stylebutton"
                    Width="134px" OnClick="btnReturn_Click" />
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="stylebutton" OnClick="btnCancel_Click" />
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="stylebutton" OnClick="btnClose_Click" />
                &nbsp;
                <asp:Button ID="btnForward" runat="server" Text="Go To Shipment" CssClass="stylebutton"
                    Width="134px" OnClick="btnForward_Click" />
            </td>
            <td align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5" align="center">
                &nbsp;
            </td>
            <td align="center">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
