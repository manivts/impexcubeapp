<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmFundRequestApproval.aspx.cs" Inherits="ImpexCube.frmFundRequestApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .hiddencol
        {
            display: none;
        }
        .style1
        {
            height: 25px;
        }
    </style>
    <script type="text/javascript">
        function isFloat(evt, val) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 46 && val.indexOf('.') == -1)
                return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function ValidateText() {
            var Amt = document.getElementById('<%= txtApprovedAmount.ClientID %>').value;
            if (document.getElementById('<%= chkApproved.ClientID %>').checked == 1) {
                if (document.getElementById('<%= txtApprovedAmount.ClientID %>').value == '' || Amt == 0.00) {
                    alert('Please fill the "Amount Approved" field ');
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="width100">
        <div class="div70">
            <table class="table100">
                <tr>
                    <td class="center" colspan="4">
                        <asp:Label ID="Label1" runat="server" CssClass="header" 
                            Text="Operational Manager Approval" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="Job No"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblJobNo" runat="server" CssClass="fontsize"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="fontsize" Text="Fund Req No"></asp:Label>
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
                        <asp:Label ID="Label3" runat="server" CssClass="fontsize" Text="Fund Request Date"></asp:Label>
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
                    <td colspan="3">
                        <asp:Label ID="lblReqAmount" runat="server" CssClass="fontsize"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" CssClass="fontsize" Text="Req. By"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblReqBy" runat="server" CssClass="fontsize"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="Purpose For"></asp:Label>
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
                        <asp:Label ID="lblCFSName" runat="server" CssClass="fontsize"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblShipping" runat="server" CssClass="fontsize" Text="Shipping Name"
                            Visible="False"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblShippingName" runat="server" CssClass="fontsize"></asp:Label>
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
                        <asp:Label ID="Label19" runat="server" CssClass="fontsize" Text="Reject"></asp:Label>
                    </td>
                    <td colspan="1">
                        <asp:CheckBox ID="chkReject" runat="server" AutoPostBack="True" Enabled="False" OnCheckedChanged="chkReject_CheckedChanged" />
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
                        <asp:Label ID="Label11" runat="server" CssClass="fontsize" Text="Approved"></asp:Label>
                    </td>
                    <td colspan="1">
                        <asp:CheckBox ID="chkApproved" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" CssClass="fontsize" Text="Approved Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtApprovedDate" runat="server" CssClass="textbox150" OnKeyPress="javascript:return false;"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtApprovedDate"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label15" runat="server" CssClass="fontsize" Text="Amount Approved"></asp:Label>
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="txtApprovedAmount" runat="server" CssClass="textbox150" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" CssClass="fontsize" Text="Amount From"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAmountFrom" runat="server" CssClass="ddl150">
                            <asp:ListItem>Party</asp:ListItem>
                            <asp:ListItem Selected="True">Office</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" CssClass="fontsize" Text="Approval Remarks"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtApprovalRemarks" runat="server" CssClass="textbox400" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="center" colspan="4">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="60px"
                            Height="26px" OnClientClick="javascript:return ValidateText();" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" Height="26px"
                            OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="div31">
            <table>
                <tr style="height: 30px">
                    <td class="center">
                        <asp:Label ID="Label16" runat="server" CssClass="header" Text="Pending List" 
                            Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <div class="grid_scroll">
                            <asp:GridView ID="gvFundRequest" runat="server" CellPadding="4" CssClass="table-wrapper"
                                GridLines="None" OnSelectedIndexChanged="gvFundRequest_SelectedIndexChanged"
                                Font-Size="10pt" AutoGenerateColumns="False">
                                <RowStyle CssClass="table-header light" />
                                <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                <AlternatingRowStyle BackColor="#E7E7FF" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:BoundField DataField="Request No" HeaderText="FR No" SortExpression="Request No"
                                        ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                    <asp:BoundField DataField="JobNo" HeaderText="Job No" ReadOnly="True" />
                                    <asp:BoundField DataField="Customer" HeaderText="Customer" ReadOnly="True" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" />
                                    <asp:BoundField DataField="Request Date" HeaderText="Req.Date" ReadOnly="True" />
                                </Columns>
                                <EditRowStyle BackColor="#000000" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="div31">
            <table>
                <tr>
                    <td class="center">
                        <asp:Label ID="Label20" runat="server" CssClass="header" Text="Approved List" 
                            Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="grid_scroll">
                            <asp:GridView ID="gvPendingrequest" runat="server" CellPadding="4" CssClass="table-wrapper"
                                GridLines="None" Font-Size="9pt" OnSelectedIndexChanged="gvPendingrequest_SelectedIndexChanged"
                                AutoGenerateColumns="false">
                                <RowStyle CssClass="table-header light" Font-Size="9pt" />
                                <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" Font-Size="9pt" />
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" Font-Size="9pt" />
                                <AlternatingRowStyle BackColor="#E7E7FF" Font-Size="9pt" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" HeaderText="Select" />
                                    <asp:BoundField DataField="Request No" HeaderText="FR.No" SortExpression="Request No"
                                        ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="JobNo" HeaderText="Job No" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Customer" HeaderText="Customer" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Request Date" HeaderText="Req.Date" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="div101" id="divHistory" runat="server" visible="false">
            <table width="100%" align="center">
                <tr>
                    <td align="center" class="style1">
                        <asp:Label ID="Label17" runat="server" Text="History" CssClass="header" 
                            Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="gvApprovedList" runat="server" AutoGenerateColumns="False" Width="869px"
                            Font-Size="10pt" GridLines="Both">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Request No" HeaderText="Request No" SortExpression="Request No"
                                    ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="JobNo" HeaderText="Job No" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Customer" HeaderText="Customer" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Request Date" HeaderText="Required Date" ReadOnly="True"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Request By" HeaderText="Request By" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Manager's Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManagerApproval" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accounts Manager's Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccountsApproval" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amount" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPayAmt" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
