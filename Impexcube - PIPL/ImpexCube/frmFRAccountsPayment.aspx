<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmFRAccountsPayment.aspx.cs" Inherits="ImpexCube.frmFRAccountsPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="div80">
                <table width="100%">
                    <tr>
                        <td colspan="4" class="center">
                            <asp:Label ID="Label1" runat="server" Text="Accounts Approval "></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblJobNo1" runat="server" CssClass="fontsize" Text="Job No"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblJobNo" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" CssClass="fontsize" Text="Fund Req No"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFundRequestNo" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" CssClass="fontsize" Text="Mode of Payment"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblModeOfPayment" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="Fund Request Date"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFundReqDate" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" CssClass="fontsize" Text="Required Date"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblRequiredDate" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label8" runat="server" CssClass="fontsize" Text="Customer Name"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCustomerName" runat="server" CssClass="fontsize2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="fontsize" Text="Req. Amount"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblReqAmount" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" CssClass="fontsize" Text="Req. By"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblReqBy" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" CssClass="fontsize" Text="Remarks"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblRemark" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label27" runat="server" CssClass="fontsize" Text="Purpose For"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblPurpose" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCFS" runat="server" CssClass="fontsize" Text="CFS Name" Visible="False"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblCFSName" runat="server" CssClass="fontsize" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblShipping" runat="server" CssClass="fontsize" Text="Shipping Name"
                                Visible="False"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblShippingName" runat="server" CssClass="fontsize" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="center">
                            <asp:Label ID="Label11" runat="server" Text="Approver"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" CssClass="fontsize" Text="Approved"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:CheckBox ID="chkApproved" runat="server" Enabled="false" />
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="Approved Date"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblApprovalDate" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label21" runat="server" CssClass="fontsize" Text="Approved Amount"></asp:Label>
                        </td>
                        <td colspan="1">
                            <asp:Label ID="lblApprovedAmount" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label26" runat="server" CssClass="fontsize" Text="Amount From"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblApprovedAmountFrom" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" CssClass="fontsize" Text="Approval Remarks"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblApprovalRemark" runat="server" CssClass="fontsize"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div class="saparate">
                    <table width="100%">
                        <tr>
                            <td colspan="4" class="center">
                                <asp:Label ID="Label15" runat="server" Text="Account Manager"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" CssClass="fontsize" Text="Pay"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:CheckBox ID="chkComplete" runat="server" Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td colspan="3">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label18" runat="server" CssClass="fontsize" Text="Payment Status"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblPaymentStatus" runat="server" CssClass="fontsize"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label16" runat="server" CssClass="fontsize" Text="Payment Amount"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblPaymentAmount" runat="server" CssClass="fontsize"></asp:Label>
                                <asp:Label ID="lblActual" runat="server" CssClass="fontsize" Visible="false"></asp:Label>
                                <asp:Label ID="lblPaid" runat="server" CssClass="fontsize" Visible="false"></asp:Label>
                                <asp:Label ID="lblBalance" runat="server" CssClass="fontsize" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label17" runat="server" CssClass="fontsize" Text="Payment Mode"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblPaymentMode" runat="server" CssClass="fontsize"></asp:Label>
                            </td>
                        </tr>
                        <tr id="rwBankName" runat="server">
                            <td>
                                <asp:Label ID="Label24" runat="server" CssClass="fontsize" Text="BankName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBankName" runat="server" CssClass="fontsize"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label25" runat="server" CssClass="fontsize" Text="Drawn in Favour of"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDrewBank" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="rwPaymentMode" runat="server">
                            <td>
                                <asp:Label ID="Label22" runat="server" CssClass="fontsize" Text="Cheque No/DD No"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtChqDDNo" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label23" runat="server" CssClass="fontsize" Text="Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtChqDDdate" runat="server" CssClass="textbox150" OnKeyPress="javascript:return false;"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtChqDDdate"
                                    Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label19" runat="server" CssClass="fontsize" Text="Remarks"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtAccountRemarks" runat="server" CssClass="textbox400" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="center" align="center">
                                &nbsp;&nbsp; &nbsp;
                                <asp:Button ID="btnSave" runat="server" Text="Save" Width="60px" Height="26px" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" Height="26px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="div32">
                <table>
                    <tr style="height: 25px">
                        <td class="center">
                            Approval List
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <div class="grid_scroll">
                                <asp:GridView ID="gvPendingrequest" runat="server" AutoGenerateSelectButton="True"
                                    CellPadding="4" CssClass="table-wrapper" GridLines="None" Font-Size="9pt" Width="400px"
                                    OnSelectedIndexChanged="gvPendingrequest_SelectedIndexChanged" >
                                    <RowStyle CssClass="table-header light" Font-Size="9pt" />
                                    <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" Font-Size="9pt" />
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" Font-Size="9pt" />
                                    <AlternatingRowStyle BackColor="#E7E7FF" Font-Size="9pt" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div class="div100" id="divOverAll" runat="server">
                <table width="100%">
                    <tr>
                        <td align="center" class="style1">
                            <asp:Label ID="Label20" runat="server" Text="History" CssClass="header"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <div class="grid_scroll-2">
                                <asp:GridView ID="gvApprovedList" runat="server" Width="869px" Font-Size="10pt" GridLines="Both">
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                <input type="hidden" id="hdnApprovalAmount" runat="server" />
                <input type="hidden" id="hdnPayBalance" runat="server" />
                <input type="hidden" id="hdnPayPaid" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
