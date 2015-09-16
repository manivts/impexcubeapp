<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmBankMaster.aspx.cs" Inherits="ImpexCube.frmBankMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .hiddenid
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function valsave() {
            var ddlType = document.getElementById('ContentPlaceHolder1_ddlType').selectedIndex;
            var ddlName = document.getElementById('ContentPlaceHolder1_ddlName').selectedIndex;
            if (ddlType == 0) {
                alert('Please Select Type');
                document.getElementById('ContentPlaceHolder1_ddlType').focus();
                return false;
            }
            if (ddlName ==0) {
                alert('Please Select Name');
                document.getElementById('ContentPlaceHolder1_ddlName').focus();
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_txtBankName').value == '') {
                alert('Please Enter Bank Name');
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 65%;">
        <tr>
            <td class="fontsize" align="center" colspan="4" style="color: #008080; font-size: 15px;">
                <strong>Bank Master</strong>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Type
            </td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="ddl156" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlType_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Value="~Select~">~Select~</asp:ListItem>
                </asp:DropDownList>
                <font color="red">*</font>
            </td>
            <td class="fontsize">
                A/C. No.
            </td>
            <td>
                <asp:TextBox ID="txtAccNo" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Name
            </td>
            <td>
                <asp:DropDownList ID="ddlName" runat="server" CssClass="ddl156" Width="250px">
                    <asp:ListItem Value="~Select~">~Select~</asp:ListItem>
                </asp:DropDownList>
                <font color="red">*</font>
            </td>
            <td class="fontsize">
                IFSC Code
            </td>
            <td>
                <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Bank Name
            </td>
            <td>
                <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox><font
                    color="red">*</font>
            </td>
            <td class="fontsize">
                IBan No
            </td>
            <td>
                <asp:TextBox ID="txtIBanNo" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Branch Name
            </td>
            <td>
                <asp:TextBox ID="txtBranchName" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
            <td class="fontsize">
                Swift Code
            </td>
            <td>
                <asp:TextBox ID="txtSwiftCode" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Address
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox150" Height="25px" TextMode="MultiLine"
                    Width="250px"></asp:TextBox>
            </td>
            <td class="fontsize">
                Currency
            </td>
            <td>
                <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="ddl156" Width="250px">
                    <asp:ListItem Value="~Select~">~Select~</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Country
            </td>
            <td>
                <asp:TextBox ID="txtCountry" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
            <td class="fontsize" colspan="2" rowspan="3">
                &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="masterbutton" 
                    Height="28px" onclick="btnNew_Click" />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="masterbutton" OnClientClick="javascript: return valsave();"
                    Height="28px" OnClick="btnSave_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="masterbutton" Height="28px"
                    OnClick="btnUpdate_Click" />
                <asp:Button ID="btnDiscard" runat="server" Text="Exit" CssClass="masterbutton" 
                    Height="28px" onclick="btnDiscard_Click" />
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                City
            </td>
            <td>
                <asp:TextBox ID="txtCity" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                PinCode
            </td>
            <td>
                <asp:TextBox ID="txtPinCode" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize" colspan="4">
                <div class="grid_scroll-2">
                    <asp:GridView ID="gvBankDetails" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="true"
                        Width="800px" AutoGenerateColumns="False" OnSelectedIndexChanged="gvBankDetails_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField HeaderText="Id" DataField="TransId" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-CssClass="hiddenid" ItemStyle-CssClass="hiddenid"></asp:BoundField>
                            <asp:BoundField HeaderText="Name" DataField="AccountName" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Type" DataField="AccountType" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="BankName" DataField="BankName" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Currency" DataField="Currency" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="City" DataField="City" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="A/C. No" DataField="AccNo" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle BackColor="White" />
                        <RowStyle CssClass="table-header light" />
                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <AlternatingRowStyle BackColor="#E7E7FF" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <input type="hidden" runat="server" id="hdnBankMaster" />
</asp:Content>
