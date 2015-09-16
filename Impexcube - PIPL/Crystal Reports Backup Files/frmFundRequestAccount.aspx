<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmFundRequestAccount.aspx.cs" Inherits="ImpexCube.frmFundRequestAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .hiddencol
        {
            display: none;
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
            var amt = document.getElementById('<%= txtPaymentAmount.ClientID %>').value;
            if (document.getElementById('<%= chkComplete.ClientID %>').checked == 1) {
                if (document.getElementById('<%= txtPaymentAmount.ClientID %>').value == '' || amt == 0.00) {
                    alert('Please fill the Payment amount');
                    return false;
                }
            }
            if (document.getElementById('<%= txtPaymentAmount.ClientID %>').value == '' || amt == 0.00) {
                alert('Please fill the Payment amount');
                return false;
            }
            var mode = document.getElementById('<%= ddlPaymentMode.ClientID %>').options[document.getElementById("<%=ddlPaymentMode.ClientID%>").selectedIndex].text
            var status = document.getElementById('<%= ddlPaymentStatus.ClientID %>').options[document.getElementById("<%=ddlPaymentStatus.ClientID%>").selectedIndex].text;
            if (mode != "-Select-") {
                if (status != "-Select-") {
                    if (status == "Full Payment") {
                        var actamt = parseFloat(document.getElementById('<%= hdnApprovalAmount.ClientID %>').value).toFixed(2);
                        var balance = document.getElementById('<%= hdnPayBalance.ClientID %>').value;
                        if (balance == "") {
                            balance = "0";
                        }
                        var paid = document.getElementById('<%= hdnPayPaid.ClientID %>').value;
                        if (paid == "") {
                            paid = "0";
                        }
                        var payamt = document.getElementById('<%= txtPaymentAmount.ClientID %>').value;
                        var paybalance = parseInt(balance);
                        var paypaid = parseInt(paid);
                        var pay = parseInt(payamt);
                        var total = pay + paypaid;
                        var totalpay = parseFloat(total).toFixed(2);
                        if (totalpay == actamt) {
                            return true;
                        }
                        else if (totalpay > actamt) {
                            alert('Payment amount exceeds approval amount');
                            return false;
                        }
                    }
                }
                else if (status == "-Select-") {
                    alert('Please select payment status');
                    return false;
                }
            }
            else if (mode == "-Select-") {
                alert('Please select payment mode');
                return false;
            }
        }
    </script>
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
                            <asp:Label ID="lblCFS" runat="server" CssClass="fontsize" Text="CFS Name" 
                                Visible="False"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblCFSName" runat="server" CssClass="fontsize" Visible="False"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                    <td>
                        <asp:Label ID="lblShipping" runat="server" CssClass="fontsize" 
                            Text="Shipping Name" Visible="False"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblShippingName" runat="server" CssClass="fontsize" 
                            Visible="False"></asp:Label>
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
                                <asp:CheckBox ID="chkComplete" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label28" runat="server" CssClass="fontsize" 
                                    Text="Go to Accounts"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:CheckBox ID="chkAccounts" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label18" runat="server" CssClass="fontsize" Text="Payment Status"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlPaymentStatus" runat="server" CssClass="ddl150">
                                    <asp:ListItem>-Select-</asp:ListItem>
                                    <asp:ListItem Value="1">Full Payment</asp:ListItem>
                                    <asp:ListItem Value="2">Partial Payment</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label16" runat="server" CssClass="fontsize" Text="Payment Amount"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtPaymentAmount" runat="server" CssClass="textbox150" OnKeyPress="javascript:return isFloat(event);"
                                    OnTextChanged="txtPaymentAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
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
                                <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="ddl150" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem>-Select-</asp:ListItem>
                                    <asp:ListItem>Cash</asp:ListItem>
                                    <asp:ListItem>Cheque</asp:ListItem>
                                    <asp:ListItem>Demand Draft</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="rwBankName" runat="server">
                            <td>
                                <asp:Label ID="Label24" runat="server" CssClass="fontsize" Text="BankName"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="txtBankName" runat="server" CssClass="textbox150">
                                </asp:DropDownList>
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
                            <td colspan="4" class="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="60px"
                                    Height="26px" OnClientClick="javascript:return ValidateText();" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" Height="26px"
                                    OnClick="btnCancel_Click" />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="div32">
                <table>
                    <tr style="height: 25px">
                        <td class="center">
                            Pending Request
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
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
                            <asp:Label ID="lblResult" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <div class="separategrid" id="PaymentInfo" runat="server" visible="false">
                    <div style="text-align: center">
                        Partial Payment Balance</div>
                    <asp:GridView ID="gvPartialBalance" runat="server" CellPadding="4" CssClass="table-wrapper"
                        GridLines="None" Font-Size="8pt" AutoGenerateColumns="false">
                        <RowStyle CssClass="table-header light" />
                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <AlternatingRowStyle BackColor="#E7E7FF" />
                        <Columns>
                            <asp:BoundField DataField="Request No" HeaderText="FR.No" SortExpression="Request No"
                                ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Actual" HeaderText="Actual Amt" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Paid" HeaderText="Paid Amt" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Balance" HeaderText="Balance" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Mode" HeaderText="Mode" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="div100" id="divOverAll" runat="server" visible="false">
                <table width="100%">
                    <tr>
                        <td align="center" class="style1">
                            <asp:Label ID="Label20" runat="server" Text="History" CssClass="header"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <div class="grid_scroll-2">
                                <asp:GridView ID="gvApprovedList" runat="server" AutoGenerateColumns="False" Width="869px"
                                    Font-Size="10pt" GridLines="Both">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="Request No" HeaderText="Request No" SortExpression="Request No"
                                            ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="JobNo" HeaderText="Job No" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Customer" HeaderText="Customer" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Request Date" HeaderText="Request Date" ReadOnly="True"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Request By" HeaderText="Request By" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Manager" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblManagerApproval" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accounts Manager" ItemStyle-HorizontalAlign="Center">
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
