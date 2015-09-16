<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MainMaster.Master" AutoEventWireup="true"
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
    <style type="text/css">
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
        .grid_scroll-GenMaster
        {
            height: 191px;
            overflow: auto;
        }
        .alignment
        {
            text-align:left;
        }
        .Column1
        {
           padding-left:200px;
            width:150px;
        }
        .Column2
        {
            height:18px;
            width:400px;
            padding-left:350px;
            margin-top:-15px;
        }
         .Column3
        {
            width:120px;
            padding-left:550px;
             margin-top:-20px;
        }
         .Column4
        {
            width:200px;
            padding-left:635px;
            margin-top:-15px;
            height: 30px;
        }
        .Column10
        {
            padding-left:200px;
           
            width:400px;
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <div>Cost Center</div><br/>

    <div>
       <div class="alignment labelsize Column1">
            <label class="labelsize">Cost Center Code</label>
       </div>
       <div class="alignment labelsize Column2">
            <asp:TextBox ID="txtCostCenterCode" runat="server" Width="150px" ReadOnly="true" CssClass="textbox140"></asp:TextBox>
            <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
       </div>
       <div class="alignment labelsize Column3">
         <label class="labelsize"> Under </label>
       </div>
       <div class="alignment labelsize Column4">
          <asp:TextBox ID="txtCostUnder" runat="server" Width="150px" 
                    CssClass="textbox140"></asp:TextBox>
       </div>
    </div>

    <div>
      <div class="alignment labelsize Column1">
            <label class="fontsize">Category</label>
       </div>
       <div class="alignment labelsize Column2">
            <asp:TextBox ID="txtCategory" runat="server" Width="150px" CssClass="textbox140"></asp:TextBox>
       </div>
       <div class="alignment labelsize Column3">
         <label class="labelsize">
                    Department </label>
       </div>
       <div class="alignment labelsize Column4">
          <asp:TextBox ID="txtDepartment" runat="server" Width="150px" 
                    CssClass="textbox140"></asp:TextBox>
       </div>
    </div>

    <div>
      <div class="alignment labelsize Column1">
            <label class="fontsize">Cost Center Name</label>
       </div>
       <div class="alignment labelsize Column2">
             <asp:TextBox ID="txtCostCenterName" runat="server" Width="150px" CssClass="textbox140"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
       </div>
       <div class="alignment labelsize Column3">
         <label class="labelsize">User Name </label>
       </div>
       <div class="alignment labelsize Column4">
          <asp:TextBox ID="txtUserName" runat="server" Width="150px" 
                    CssClass="textbox140"></asp:TextBox>
       </div>
    </div>

    <div>
      <div class="alignment labelsize Column10">
       <asp:Label ID="Label6" runat="server" ForeColor="Red">* Indicates the mandatory fields</asp:Label>
     </div>
    </div>

    <div>
               <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" 
                OnClientClick="return confirm('Do you want to Save?');"
                    CssClass="btn70"  />
                <asp:Button ID="btnUpdate" runat="server" CssClass="btn70" Text="Update" OnClick="btnUpdate_Click"
                    OnClientClick="return confirm('Do you want to Update?');"  />
                <asp:Button ID="btnNew" runat="server" CssClass="btn70" Text="New" 
                    OnClick="btnNew_Click"  />
                <asp:Button ID="btnExit" runat="server" CssClass="btn70" Text="Exit" 
                    OnClick="btnExit_Click" OnClientClick="return confirm('Do you want to leave this page?');"
                    CausesValidation="false"  />
    </div><br/>

    <div>
               <asp:GridView ID="gvCostCenter" runat="server" 
                    Style="text-align: left; font-size: 9pt;" AutoGenerateColumns="true"
                    AllowPaging="true" AllowSorting="true" BorderColor="Black" BorderStyle="Solid"
                    Font-Names="Arial" BorderWidth="1px" Font-Size="10pt" ForeColor="Black" GridLines="Vertical"
                    ShowFooter="false" ShowHeader="true" Width="100%" 
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
                            ItemStyle-Font-Names="Arial" ShowSelectButton="true" SelectImageUrl="~/Accounts/AccImages/edit.PNG">
                            <ItemStyle Font-Names="Arial" Font-Bold="False"></ItemStyle>
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
    </div>
    <%--<table style="width: 75%;" align="center">
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Label ID="Label1" runat="server"
                     Text="Cost Center" CssClass="labeltitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <label class="labelsize">
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
            <td align="left" class="labelsize">
                <label class="fontsize">
                    Category</label>
            </td>
            <td class="style12">
                <asp:TextBox ID="txtCategory" runat="server" Width="150px" 
                    CssClass="textbox140"></asp:TextBox>
            </td>
            <td class="labelsize" align="left">
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
            <td class="labelsize" align="left">
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
                    CssClass="btn70"  />
                <asp:Button ID="btnUpdate" runat="server" CssClass="btn70" Text="Update" OnClick="btnUpdate_Click"
                    OnClientClick="return confirm('Do you want to Update?');"  />
                <asp:Button ID="btnNew" runat="server" CssClass="btn70" Text="New" 
                    OnClick="btnNew_Click"  />
                <asp:Button ID="btnExit" runat="server" CssClass="btn70" Text="Exit" 
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
                    ShowFooter="false" ShowHeader="true" Width="100%" 
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
                            ItemStyle-Font-Names="Arial" ShowSelectButton="true" SelectImageUrl="~/Accounts/AccImages/edit.PNG">
                            <ItemStyle Font-Names="Arial" Font-Bold="False"></ItemStyle>
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>--%>
</asp:Content>
