<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmJobChangeApproval.aspx.cs" Inherits="ImpexCube.frmJobChangeApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <table>
                    <tr>
                        <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                            font-size: large">
                            Job Change Approval
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Job No
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlJobno" runat="server" CssClass="textbox200">                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Request Raised By
                        </td>
                        <td>
                            <asp:TextBox ID="txtRequestBy" runat="server" CssClass="textbox200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Reason for Change
                        </td>
                        <td>
                            <asp:TextBox ID="txtReason" runat="server" CssClass="textbox200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize">
                            Remarks
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontsize" colspan="2">
                            <asp:CheckBox ID="chkJobCreation" runat="server" CssClass="radio-area1" Text="Job Creation"
                                Height="20px" Width="71px" />&nbsp;
                            <asp:CheckBox ID="chkImpExp" runat="server" CssClass="radio-area1" Height="18px"
                                Width="83px" />&nbsp;
                            <asp:CheckBox ID="chkShipment" runat="server" CssClass="radio-area1" Text="Shipment"
                                Height="16px" Width="81px" />&nbsp;
                            <asp:CheckBox ID="chkInvoice" runat="server" CssClass="radio-area1" Text="Invoice"
                                Height="16px" Width="81px" />&nbsp;
                            <asp:CheckBox ID="chkProduct" runat="server" CssClass="radio-area1" Text="Product"
                                Height="16px" Width="81px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <asp:Button ID="btnSave" runat="server" CssClass="masterbutton" OnClick="btnSave_Click"
                                Text="Save" OnClientClick="javascript:return validate();"></asp:Button>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="masterbutton">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
