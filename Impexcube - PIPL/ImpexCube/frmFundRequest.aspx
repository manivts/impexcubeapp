<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmFundRequest.aspx.cs" Inherits="ImpexCube.frmFundRequest" %>

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
            if (document.getElementById('<%= txtReqAmount.ClientID %>').value == "") {
                alert('Please fill the approval amount');
                return false;
            }
        }
        function validate() {

            try {
                var splitDate = document.getElementById('<%=txtRequiredDate.ClientID%>').value.split("/");
                var refDate = new Date(splitDate[2] + " " + splitDate[1] + " " + splitDate[0]);
                var todaydd = new Date();
                todaydd.setHours(0, 0, 0, 0)
                if (refDate < todaydd) {
                    alert('No Past Date, Please Select Correct Date');
                    document.getElementById('<%=txtRequiredDate.ClientID%>').focus();
                    document.getElementById('<%=txtRequiredDate.ClientID%>').value = "";

                    return false;
                }
            }
            catch (err) {
                alert(err.Message);
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="width100">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="div70">
                        <table class="table100">
                            <tr>
                                <td class="center" colspan="4">
                                    <asp:Label ID="Label1" runat="server" CssClass="header" Text="Fund Request"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    <asp:RadioButtonList ID="rbImpExp" runat="server" AutoPostBack="True" 
                                        CssClass="fontsize" Font-Bold="False" 
                                        onselectedindexchanged="rbImpExp_SelectedIndexChanged" 
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="Imp">Import</asp:ListItem>
                                        <asp:ListItem Value="Exp">Export</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="Job No"></asp:Label>
                                </td>
                                <td>
                                   <%-- <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="true" 
                                        CssClass="ddl150" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged">
                                    </asp:DropDownList>--%>
                                    <cc1:ComboBox ID="ddlJobNo"  runat="server" CssClass="ddl150" 
                                        OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" AutoPostBack="true" 
                                        AutoCompleteMode="Suggest">
                                    </cc1:ComboBox>


                                    
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" CssClass="fontsize" Text="Fund Req No"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFundRequest" runat="server" CssClass="fontsize"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" CssClass="fontsize" Text="Mode of Payment"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlModeOfPayment" runat="server" CssClass="ddl150" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlModeOfPayment_SelectedIndexChanged">
                                        <asp:ListItem>-Select-</asp:ListItem>
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>Demand Draft</asp:ListItem>
                                    </asp:DropDownList>
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
                                    <asp:Label ID="Label5" runat="server" CssClass="fontsize" Text="Req. Amount"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReqAmount" runat="server" CssClass="textbox150" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label12" runat="server" CssClass="fontsize" Text="Customer Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblImporter" runat="server" CssClass="fontsize1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" CssClass="fontsize" Text="Required Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRequiredDate" runat="server" CssClass="textbox150" onchange="javascript:return validate();"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRequiredDate"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" CssClass="fontsize" Text="Req. By"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtReqBy" runat="server" CssClass="textbox150" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" CssClass="fontsize" Text="Purpose For"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <%--<asp:TextBox ID="txtPurpose" runat="server" CssClass="textbox150"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlPurpose" runat="server" CssClass="ddl150" OnSelectedIndexChanged="ddlPurpose_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="cfs" class="cfs" runat="server">
                                <td>
                                    <asp:Label ID="lblCFSName" runat="server" CssClass="fontsize" Text="CFS Name"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <%--<asp:TextBox ID="txtPurpose" runat="server" CssClass="textbox150"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlCFSName" runat="server" CssClass="ddl150">                                        
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="Shipping" class="cfs" runat="server">
                                <td>
                                    <asp:Label ID="Label8" runat="server" CssClass="fontsize" Text="Shipping Name"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <%--<asp:TextBox ID="txtPurpose" runat="server" CssClass="textbox150"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlShippingName" runat="server" CssClass="ddl150">                                        
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" CssClass="fontsize" Text="Remarks"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox400" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="center" colspan="4">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Style="height: 26px;
                                        width: 60px" OnClientClick="javascript:return Validatetext();" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Style="height: 26px; width: 60px"
                                        Visible="false" OnClick="btnUpdate_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnPrint" runat="server" onclick="btnPrint_Click" 
                                        Text="Print" Visible="False" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div30" id="divHistory" runat="server" visible="false">
                        <table width="100%">
                            <tr style="height: 25px">
                                <td class="center">
                                    <asp:Label ID="Label14" runat="server" CssClass="header" Text="Pending Request"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height: 216px">
                                <td valign="top">
                                    <asp:GridView ID="gvFundRequest" runat="server" CellPadding="4" GridLines="None"
                                        OnSelectedIndexChanged="gvFundRequest_SelectedIndexChanged" CssClass="table-wrapper"
                                        AutoGenerateColumns="false" Font-Size="10pt">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:BoundField DataField="Request No" HeaderText="FR.No" SortExpression="Request No"
                                                ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="JobNo" HeaderText="Job No" ReadOnly="True" ItemStyle-CssClass="hiddencol"
                                                HeaderStyle-CssClass="hiddencol" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Customer" HeaderText="Customer" ReadOnly="True" ItemStyle-CssClass="hiddencol"
                                                HeaderStyle-CssClass="hiddencol" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Request Date" HeaderText="Req.Date" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <RowStyle CssClass="table-header light" />
                                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <AlternatingRowStyle BackColor="#E7E7FF" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div100" id="divOverAll" runat="server" visible="false">
                        <table width="100%">
                            <tr>
                                <td align="center" class="style1">
                                    <asp:Label ID="Label17" runat="server" Text="History" CssClass="header"></asp:Label>
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
                                            <asp:BoundField DataField="JobNo" HeaderText="Job No" ReadOnly="True" ItemStyle-CssClass="hiddencol"
                                                HeaderStyle-CssClass="hiddencol" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Customer" HeaderText="Customer" ReadOnly="True" ItemStyle-CssClass="hiddencol"
                                                HeaderStyle-CssClass="hiddencol" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Request Date" HeaderText="Requested Date" ReadOnly="True"
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
                                        <%--<EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />--%>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
