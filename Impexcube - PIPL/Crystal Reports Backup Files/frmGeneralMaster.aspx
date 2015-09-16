<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmGeneralMaster.aspx.cs" Inherits="ImpexCube.frmGeneralMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .hiddenid
        {
            display: none;
        }
        .style1
        {
            font-family: Verdana;
            font-size: 8pt;
            height: 23px;
        }
        .style2
        {
            height: 23px;
        }
        .style3
        {
            font-family: Verdana;
            font-size: 8pt;
            height: 30px;
        }
        .style4
        {
            height: 30px;
        }
        .grid_scroll-GenMaster
        {
            height: 191px;
            overflow: auto;
        }
    </style>
    <script type="text/javascript">
        function shortname() {
            var name = document.getElementById('ContentPlaceHolder1_txtName').value;
            var upper = document.getElementById('ContentPlaceHolder1_txtName').value;
            var short = name.substring(0, 4);
            document.getElementById('ContentPlaceHolder1_txtShortName').value = short;
            var conupper = upper.toUpperCase();
            document.getElementById('ContentPlaceHolder1_txtName').value = conupper;
        }
        function valsave() {
            if (document.getElementById('ContentPlaceHolder1_txtName').value == "") {
                alert('Please Enter The Mandatory Fields');
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="fontsize" align="center" colspan="5">
                <asp:Label ID="lblMaster" runat="server" Text="Master" Font-Bold="True" Font-Size="Medium"></asp:Label>
            </td>
            <td rowspan="19" valign="top">
                <div class="grid_scroll-GenMaster">
                    <asp:GridView ID="gvDetails" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="true"
                        Width="500px" AutoGenerateColumns="False" OnSelectedIndexChanged="gvDetails_SelectedIndexChanged1">
                        <Columns>
                            <asp:BoundField HeaderText="Id" DataField="TransId" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Name" DataField="AccountName" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Type" DataField="AccountType" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Branch" DataField="BranchName" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Country" DataField="Country" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="City" DataField="City" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="State" DataField="State" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle BackColor="White" />
                        <RowStyle CssClass="table-header light" />
                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <AlternatingRowStyle BackColor="#E7E7FF" />
                    </asp:GridView>
                </div>
                <p>
                </p>
                <center>
                    <asp:Label ID="lblBranchDetails" runat="server" Text="Branch Details" Font-Bold="True"></asp:Label></center>
                <p>
                </p>
                <div class="grid_scroll-GenMaster">
                    <asp:GridView ID="gvBranchDetails" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="true"
                        Width="500px" AutoGenerateColumns="False" 
                        onselectedindexchanged="gvBranchDetails_SelectedIndexChanged">
                         <Columns>
                            <asp:BoundField HeaderText="Id" DataField="TransId" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Name" DataField="AccountName" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Branch" DataField="BranchName" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Country" DataField="Country" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="City" DataField="City" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="State" DataField="State" HeaderStyle-HorizontalAlign="Center">
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
        <tr>
            <td class="fontsize" align="left" colspan="1">
                Code
            </td>
            <td class="fontsize" align="left" colspan="2">
                <asp:TextBox ID="txtAcountCode" runat="server" CssClass="textbox150" Width="250px"
                    Enabled="False"></asp:TextBox><font color=red><strong>*</strong></font>
            </td>
            <td class="fontsize" align="left">
                Credit Period
            </td>
            <td class="fontsize" align="left">
                <asp:DropDownList ID="ddlPaymentPeriod" runat="server" CssClass="ddl156" Width="256px">
                    <asp:ListItem Value="~Select~" Selected=True>~Select~</asp:ListItem>
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="15">15</asp:ListItem>
                    <asp:ListItem Value="30">30</asp:ListItem>
                    <asp:ListItem Value="45">45</asp:ListItem>
                    <asp:ListItem Value="60">60</asp:ListItem>
                    <asp:ListItem Value="75">75</asp:ListItem>
                    <asp:ListItem Value="90">90</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Name
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox150" onkeypress="javascript: return shortname();"
                    Width="250px"></asp:TextBox><font color=red>*</font>
            </td>
            <td class="fontsize">
                Credit Limit
            </td>
            <td>
                <asp:TextBox ID="txtCreditLimit" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Short Name/Alias
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtShortName" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
            <td class="fontsize">
                Currency
            </td>
            <td>
                <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="ddl156" Width="256px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style3">
                Prefix
            </td>
            <td class="style4" colspan="2">
                <asp:TextBox ID="txtPrefix" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
            <td class="style3">
                Contact Person
            </td>
            <td class="style4">
                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3" align="center" colspan="5">
                <strong>Branch Details</strong>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Branch ID
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtBranchId" runat="server" CssClass="textbox150" 
                    Width="250px" Enabled="False">1</asp:TextBox>
            </td>
            <td class="fontsize">
                Mobile No
            </td>
            <td>
                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Branch Name
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtBranchName" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
            <td class="fontsize">
                Email Id
            </td>
            <td>
                <asp:TextBox ID="txtEmailId" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Address 1
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtAddress1" runat="server" CssClass="textbox150" Height="30px"
                    TextMode="MultiLine" Width="250px"></asp:TextBox>
            </td>
            <td class="fontsize">
                Address 2
            </td>
            <td>
                <asp:TextBox ID="txtAddress2" runat="server" CssClass="textbox150" Height="30px"
                    TextMode="MultiLine" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Address 3
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtAddress3" runat="server" CssClass="textbox150" Height="30px"
                    TextMode="MultiLine" Width="250px"></asp:TextBox>
            </td>
            <td class="fontsize">
                Website
            </td>
            <td class="fontsize">
                <asp:TextBox ID="txtWebsite" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                City
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtCity" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
            <td class="fontsize">
                Phone No
            </td>
            <td>
                <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                State
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtState" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
            <td class="fontsize">
                TIN/VAT/LST No
            </td>
            <td>
                <asp:TextBox ID="txtTINno" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Pincode
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPinCode" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
            <td class="fontsize">
                CST No
            </td>
            <td>
                <asp:TextBox ID="txtCSTNo" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Country
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="ddl156" 
                    Width="256px" AutoPostBack="True" 
                    onselectedindexchanged="ddlCountry_SelectedIndexChanged">
                    <asp:ListItem Value="~Select~">~Select~</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="fontsize">
                PAN No
            </td>
            <td>
                <asp:TextBox ID="txtPANNo" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Country Code
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtCountryCode" runat="server" CssClass="textbox150" 
                    Width="250px" Enabled="False"></asp:TextBox>
            </td>
            <td class="fontsize">
                Serv Tax No
            </td>
            <td>
                <asp:TextBox ID="txtSTaxNo" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                IE Code
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtIECode" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
            <td class="fontsize">
                Income Tax No
            </td>
            <td>
                <asp:TextBox ID="txtIncomeTaxNo" runat="server" CssClass="textbox150" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1" colspan="2">
                &nbsp;
                &nbsp;
            </td>
            <td class="style2" colspan="3" valign="top">
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="masterbutton" Height="28px"
                    OnClick="btnNew_Click" />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="masterbutton" OnClientClick="javascript: return valsave();"
                    OnClick="btnSave_Click" Height="28px" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="masterbutton" OnClick="btnUpdate_Click"
                    Height="28px" />
                <asp:Button ID="btnBranchAdd" runat="server" Text="Add Branch" CssClass="masterbutton"
                    Height="28px" onclick="btnBranchAdd_Click" Width="110px" />
                <asp:Button ID="btnDiscard" runat="server" Text="Exit" CssClass="masterbutton" Height="28px"
                    OnClick="btnDiscard_Click" />
            </td>
        </tr>
        <tr>
            <td class="style1" colspan="2">
                &nbsp;</td>
            <td class="style2" colspan="3" valign="top">
                &nbsp;</td>
        </tr>
    </table>
    <input type="hidden" runat="server" id="hdnCommonMaster" />
    <input type="hidden" runat="server" id="hdnBranchMaster" />
    </asp:Content>
