<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmGeneralMaster.aspx.cs" Inherits="ImpexCube.frmGeneralMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .opacity
        {
        	color: gray;
        	
        	
        	}
        input[type=], textarea
        {
            border: 1px solid #ccc;
        }
        input[type=text]:focus, textarea:focus
        {
            background-color: #F0F0F0;
            border: 1px solid #ccc;
        }
        .hiddenid
        {
            display: none;
        }
        .style3
        {
            font-family: Verdana;
            font-size: 8pt;
            height: 30px;
        }
        .grid_scroll-GenMaster
        {
            height: 191px;
            overflow: auto;
        }
        .style1003
        {
            text-align: left;
        }
        .style1004
        {
            text-align: left;
        }
        .style1005
        {
            text-align: left;
        }
        </style>
    <script type="text/javascript">
        window.onload = function () { checked(); };
        function shortname() {
            var name = document.getElementById('ContentPlaceHolder1_txtName').value;
            var upper = document.getElementById('ContentPlaceHolder1_txtName').value;
            var short = name.substring(0, 4);
            document.getElementById('ContentPlaceHolder1_txtShortName').value = short;
            var conupper = upper.toUpperCase();
            document.getElementById('ContentPlaceHolder1_txtName').value = conupper;
            accountname();
        }
        function accountname() {
            var first = document.getElementById('ContentPlaceHolder1_txtName').value;
            var second = document.getElementById('ContentPlaceHolder1_txtBranchName').value;
            var third = document.getElementById('ContentPlaceHolder1_txtTallyAccountName').value;
          
            document.getElementById('ContentPlaceHolder1_txtBranchName').value = first;
            document.getElementById('ContentPlaceHolder1_txtTallyAccountName').value = first; //txtTallyAccountName
            //return false;
        }
        function valsave() {
            if (document.getElementById('ContentPlaceHolder1_txtName').value == "") {
                alert('Please Enter The Name');
                document.getElementById('ContentPlaceHolder1_txtName').focus();
                return false;
            }
            var ddlcountry = document.getElementById('ContentPlaceHolder1_ddlCountry');
            var selectedText = ddlcountry.options[ddlcountry.selectedIndex].text;

            if (document.getElementById('ContentPlaceHolder1_txtBranchName').value == "") {
                alert('Please Enter The Branch Name');
                document.getElementById('ContentPlaceHolder1_txtBranchName').focus();
                return false;
            }
            if (selectedText == "~Select~") {
                alert('Please Select Country');
                document.getElementById('ContentPlaceHolder1_ddlCountry').focus();
                return false;
            }
        }
        function branchval() {
            var ddlcountry = document.getElementById('ContentPlaceHolder1_ddlCountry');
            var selectedText = ddlcountry.options[ddlcountry.selectedIndex].text;

            if (document.getElementById('ContentPlaceHolder1_txtBranchName').value == "") {
                alert('Please Enter The Branch Name');
                document.getElementById('ContentPlaceHolder1_txtBranchName').focus();
                return false;
            }
            if (selectedText == "~Select~") {
                alert('Please Select Country');
                document.getElementById('ContentPlaceHolder1_ddlCountry').focus();
                return false;
            }
        }
        function exit() {
            var status = confirm("Do You Want To Exit!");
            if (status == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function checked() {
            if (document.getElementById('ContentPlaceHolder1_chkinvseqno').checked) {
                $('#ContentPlaceHolder1_txtinvseqno').show();
                return true;
            }
            else {
                document.getElementById('ContentPlaceHolder1_txtinvseqno').style.display = 'none';
                return false;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width:94%;">
        <tr>
            <td valign="top" class="style1004">
            <table>
                <tr>
                    <td class="style3" colspan="6">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" colspan="2">
                        Code
                    </td>
                    <td>
                        <asp:TextBox ID="txtAcountCode" runat="server" CssClass="textbox150" Width="150px"
                            Enabled="False"></asp:TextBox>
                    </td>
                    <td class="fontsize" align="left" colspan="2">
                        Credit Period
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPaymentPeriod" runat="server" CssClass="ddl156" 
                            Width="83px">
                            <asp:ListItem Value="~Select~" Selected="True">~Select~</asp:ListItem>
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
                    <td class="fontsize" colspan="2">
                        Name
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtName" runat="server" CssClass="textbox150" onkeypress="javascript: return shortname();"
                            onfocus="javascript: return shortname();" onchange="javascript: return shortname();"
                            onblur="javascript: return shortname();" Width="360px"></asp:TextBox>&nbsp;</td>
                </tr>
                <tr>
                    <td class="fontsize" colspan="2">
                        Alias Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtShortName" runat="server" CssClass="textbox150" Width="150px"></asp:TextBox>
                    </td>
                    <td class="fontsize" colspan="2">
                        Prefix
                    </td>
                    <td class="fontsize">
                        <asp:TextBox ID="txtPrefix" runat="server" CssClass="textbox150" Width="82px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" colspan="2">
                        Currency
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="ddl156" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td class="fontsize" colspan="2">
                        Credit Limit
                    </td>
                    <td class="fontsize">
                        <asp:TextBox ID="txtCreditLimit" runat="server" CssClass="textbox150" Width="85px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3" colspan="2">
                        <asp:Label ID="lblAccountsGroup" runat="server" Text="Group Name" Visible="False"></asp:Label>
                    </td>
                    <td class="style3" align="left" colspan="4">
                        <asp:DropDownList ID="ddlAccountGroup" runat="server" CssClass="ddl156" Width="360px"
                            Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style3" colspan="2">
                        Contact Person
                    </td>
                    <td class="style3" colspan="4">
                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="textbox150" Width="360px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" colspan="2">
                        OP Balance
                    </td>
                    <td class="style3" colspan="2">
                        <asp:TextBox ID="txtOpeninBalance" runat="server" CssClass="textbox150" Width="100px">0</asp:TextBox>
                        <asp:DropDownList ID="ddlCRDR" runat="server" CssClass="ddl50">
                            <asp:ListItem>Cr</asp:ListItem>
                            <asp:ListItem>Dr</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style3" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="fontsize">
                        Tally Account Name
                    </td>
                    <td class="fontsize" colspan="5">
                        <asp:TextBox ID="txtTallyAccountName" runat="server" CssClass="textbox150" Width="360px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize">
                        <asp:CheckBox ID="ChkKAM1" runat="server" Text="KAM1" TextAlign="Left" Visible="False" />
                    </td>
                    <td class="fontsize" colspan="2">
                        <asp:DropDownList ID="ddlKAM1" runat="server" CssClass="ddl156" Width="150px" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td class="fontsize" colspan="3">
                        <asp:CheckBox ID="chkinvseqno" runat="server" onchange="javascript:return checked();"
                            Text="If seperate no is Yes or No" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td class="fontsize">
                        <asp:CheckBox ID="ChkKAM2" runat="server" Text="KAM2" TextAlign="Left" Visible="False" />
                    </td>
                    <td class="fontsize" colspan="2">
                        <asp:DropDownList ID="ddlKAM2" runat="server" CssClass="ddl156" Width="150px" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td class="fontsize" colspan="3">
                        <asp:TextBox ID="txtinvseqno" runat="server" CssClass="textbox150" Height="16px"
                            Visible="False" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" colspan="2">
                        CC Ref Name
                    </td>
                    <td class="fontsize">
                        <asp:TextBox ID="txtRefName" runat="server" CssClass="textbox150" Width="150px"></asp:TextBox>
                    </td>
                    <td class="fontsize" colspan="3">
                        <asp:CheckBox ID="ChkCostCenter" runat="server" CssClass="labelsize" Text="Cost Centers applicable ?"
                            TextAlign="Left" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="masterbutton" OnClick="btnUpdate_Click"
                            Height="28px" />
                    </td>
                </tr>
                </table>
            </td>
            <td valign="top" class="style1005">
            <table>
                <tr>
                    <td class="style3" align="center" colspan="6">
                        <strong>BRANCH DETAILS</strong>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize">
                        Branch ID
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtBranchId" runat="server" CssClass="textbox150" Width="200px"
                            Enabled="False">0</asp:TextBox>
                    </td>
                    <td class="fontsize">
                        Mobile No
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize">
                        Branch Name
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtBranchName" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                    <td class="fontsize">
                        Email Id
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmailId" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize">
                        Address 1
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="textbox150" Height="30px"
                            TextMode="MultiLine" Width="200px"></asp:TextBox>
                    </td>
                    <td class="fontsize">
                        Address 2
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="textbox150" Height="30px"
                            TextMode="MultiLine" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" rowspan="2">
                        Address 3
                    </td>
                    <td colspan="3" rowspan="2">
                        <asp:TextBox ID="txtAddress3" runat="server" CssClass="textbox150" Height="30px"
                            TextMode="MultiLine" Width="200px"></asp:TextBox>
                    </td>
                    <td class="fontsize">
                        Website
                    </td>
                    <td class="fontsize">
                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize">
                        Phone No
                    </td>
                    <td class="fontsize">
                        <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize">
                        City
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtCity" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                    <td class="fontsize">
                        AD Code
                    </td>
                    <td>
                        <asp:TextBox ID="txtADCode" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize">
                        State
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtState" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                    <td class="fontsize">
                        TIN/VAT/LST No
                    </td>
                    <td>
                        <asp:TextBox ID="txtTINno" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize">
                        Pincode
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtPinCode" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                    <td class="fontsize">
                        CST No
                    </td>
                    <td>
                        <asp:TextBox ID="txtCSTNo" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize">
                        Country
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="ddl156" Width="200px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                            <asp:ListItem Value="~Select~">~Select~</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="fontsize">
                        PAN No
                    </td>
                    <td>
                        <asp:TextBox ID="txtPANNo" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize">
                        Country Code
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtCountryCode" runat="server" CssClass="textbox150" Width="200px"
                            Enabled="False"></asp:TextBox>
                    </td>
                    <td class="fontsize">
                        Serv Tax No
                    </td>
                    <td>
                        <asp:TextBox ID="txtSTaxNo" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize">
                        IE Code
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtIECode" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                    <td class="fontsize">
                        Income Tax No
                    </td>
                    <td>
                        <asp:TextBox ID="txtIncomeTaxNo" runat="server" CssClass="textbox150" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnNew" runat="server" Text="New" CssClass="masterbutton" Height="28px"
                            OnClick="btnNew_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="masterbutton" OnClientClick="javascript:return valsave();"
                            OnClick="btnSave_Click" Height="28px" />
                    </td>
                    <td>
                        <asp:Button ID="btnBranchAdd" runat="server" Text="Add Branch" CssClass="masterbutton"
                            Height="28px" OnClick="btnBranchAdd_Click" Width="110px" OnClientClick="javascript: return branchval();" />
                    </td>
                    <td>
                        <asp:Button ID="btnDiscard" runat="server" Text="Exit" CssClass="masterbutton" Height="28px"
                            OnClick="btnDiscard_Click" OnClientClick="javascript:return exit();" />
                    </td>
                </tr>
            </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" Width="350px" ></asp:TextBox>
                <asp:TextBoxWatermarkExtender ID="txtSearch_TextBoxWatermarkExtender" runat="server"
                    Enabled="True" TargetControlID="txtSearch" WatermarkCssClass="opacity" WatermarkText="Name/Account Group">
                </asp:TextBoxWatermarkExtender>
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <div class="grid_scroll-GenMaster" 
                    style="width: 99%; float: left; text-align: left;">
                    <asp:GridView ID="gvDetails" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="true"
                        Width="500px" AutoGenerateColumns="False" 
                        OnSelectedIndexChanged="gvDetails_SelectedIndexChanged1" Caption=" .">
                        <Columns>
                            <asp:BoundField HeaderText="Id" DataField="TransId" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                            <asp:BoundField HeaderText="Name" DataField="AccountName" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <%--<asp:BoundField HeaderText="Type" DataField="AccountType" HeaderStyle-HorizontalAlign="Center">
                            </asp:BoundField>--%>
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
            <td valign="top">
                <div class="grid_scroll-GenMaster" style="width: 97%;">
                    <asp:GridView ID="gvBranchDetails" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="true"
                        Width="641px" AutoGenerateColumns="False" OnSelectedIndexChanged="gvBranchDetails_SelectedIndexChanged"
                        Caption="Branch Details">
                        <Columns>
                            <asp:BoundField HeaderText="Id" DataField="TransId" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
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
    </table>
    <br />
    <div>
        <div style="width: 42%; float: left; text-align: left;">
        </div>
        <div style="float: right;" align="left">
        </div>
    </div>

    <input type="hidden" runat="server" id="hdnCommonMaster" />
    <input type="hidden" runat="server" id="hdnBranchMaster" />
</asp:Content>
