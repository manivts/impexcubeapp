<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="CostCenterMaster.aspx.cs" Inherits="ImpexCube.Accounts.CostCenterMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style7
        {
            height: 23px;
            text-align: center;
        }
        .style10
        {
            text-align: left;
            font-weight: 700;
        }
        .style12
        {
            text-align: left;
        }
        .style13
        {
            height: 23px;
            text-align: center;
            width: 938px;
            color: #FF0000;
        }
        .style14
        {
            height: 23px;
            width: 938px;
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <table style="width: 81%;" align="center">
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Label ID="Label1" runat="server"
                     Text="Cost Center" CssClass="labeltitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <label class="fontsize">
                    Cost Center Code</label>
            </td>
            <td class="style12">
                <asp:TextBox ID="txtCostCenterCode" runat="server" Width="150px" 
                    ReadOnly="true" CssClass="textbox140"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td align="left" class="fontsize">
                <label class="labelsize">
                    Under </label>&nbsp;</td>
            <td class="style12">
                <asp:TextBox ID="txtCostUnder" runat="server" Width="150px" 
                    CssClass="textbox140"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <label class="fontsize">
                    Category</label>
            </td>
            <td class="style12">
                <asp:TextBox ID="txtCategory" runat="server" Width="150px" 
                    CssClass="textbox140"></asp:TextBox>
            </td>
            <td class="fontsize" align="left">
                Department</td>
            <td class="style12">
                <asp:TextBox ID="txtDepartment" runat="server" Width="150px" 
                    CssClass="textbox140"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="labelsize" align="left">
                <label class="fontsize">
                    Cost Center Name</label>
            </td>
            <td class="style12">
                <asp:TextBox ID="txtCostCenterName" runat="server" Width="150px" 
                    CssClass="textbox140"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td class="fontsize" align="left">
                User Name</td>
            <td class="style12">
                <asp:TextBox ID="txtUserName" runat="server" Width="150px" 
                    CssClass="textbox140"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style14" colspan="4" align="left">
                * Indicates the mandatory fields
            </td>
        </tr>
        <tr>
            <td class="style13" colspan="4" align="left">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" 
                OnClientClick="return confirm('Do you want to Save?');"
                    CssClass="masterbutton"  />
                <asp:Button ID="btnUpdate" runat="server" CssClass="masterbutton" Text="Update" OnClick="btnUpdate_Click"
                    OnClientClick="return confirm('Do you want to Update?');"  />
                <asp:Button ID="btnNew" runat="server" CssClass="masterbutton" Text="New" 
                    OnClick="btnNew_Click"  />
                <asp:Button ID="btnExit" runat="server" CssClass="masterbutton" Text="Exit" 
                    OnClick="btnExit_Click" OnClientClick="return confirm('Do you want to leave this page?');"
                    CausesValidation="false"  />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvCostCenter" runat="server" 
                    Style="text-align: left; font-size: 9pt;" AutoGenerateColumns="true"
                    AllowPaging="true" AllowSorting="true" BorderColor="Black" BorderStyle="Solid"
                    Font-Names="Arial" BorderWidth="1px" Font-Size="10pt" ForeColor="Black" GridLines="Vertical"
                    ShowFooter="false" ShowHeader="true" Width="75%" 
                    onselectedindexchanged="gvCostCenter_SelectedIndexChanged" 
                    onpageindexchanging="gvCostCenter_PageIndexChanging" 
                    onsorting="gvCostCenter_Sorting">
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" />
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" ItemStyle-Font-Bold="false" HeaderText="Select"
                            ItemStyle-Font-Names="Arial" ShowSelectButton="true" SelectImageUrl="~/Content/Images/edit.PNG">
                            <ItemStyle Font-Names="Arial" Font-Bold="False"></ItemStyle>
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
