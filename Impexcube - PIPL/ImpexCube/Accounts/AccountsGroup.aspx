<%@ Page Language="C#" MasterPageFile="~/Accounts/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="~/Accounts/AccountsGroup.aspx.cs" Inherits="ImpexCube.Accounts.AccountsGroup"
    Title="Group" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style7
        {
            height: 23px;
            text-align: center;
        }
        .style9
        {
            font-size: small;
            width: 142px;
            text-align: left;
        }
        .style10
        {
            text-align: left;
        }
        .style11
        {
            font-size: small;
            width: 142px;
            height: 26px;
            text-align: left;
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
    <script type="text/javascript">
        function validate() {

            if (document.getElementById('<%=txtGroupName.ClientID%>').value == "") {
                alert("Please fill group name"); // prompt user
                return false;
                document.getElementById("txtGroupName").focus();
                //set focus back to control               
            }

            if (document.getElementById('<%=ddlUndergroup.ClientID%>').value == "0") {
                alert("Please select Undergroup before clicking the save button"); // prompt user
                return false;
                document.getElementById("ddlUndergroup").focus();
                //set focus back to control               
            }

            var mode = '<%=this.Request.QueryString["mode"]%>';

            if (mode == "New") {
                alert('Do you want to save?')
            }
            else if (mode == "Edit") {
                alert('Do you want to update?')
            }
        }
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image ID="Image1" runat="server" 
                ImageUrl="~/Accounts/AccImages/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table align="center" style="width: 75%;">
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Label ID="Label1" runat="server" 
                            Text="Group Creation" CssClass="labeltitle"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="labelsize" align="left">
                        <label class="fontsize">
                            Group Code</label>
                    </td>
                    <td align="left" >
                        <asp:TextBox ID="txtGroupCode" runat="server" CssClass="textbox150" ></asp:TextBox>
                        <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                    <td class="style12" rowspan="3">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="labelsize" align="left">
                        <label class="fontsize">
                            Group Name</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtGroupName" runat="server" CssClass="textbox150" ></asp:TextBox>                       
                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="labelsize" align="left">
                        <label class="fontsize">
                            Under Group</label>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlUndergroup" runat="server" AppendDataBoundItems="True" 
                            CssClass="ddl200" >
                            
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style14" colspan="3" align="left">
                        * Indicates the mandatory fields
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style13" colspan="3">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick="javascript:return validate();"
                            Text="Save"  CssClass="btn70" />
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" 
                            Text="Update" CssClass="btn70" />
                        <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="New" 
                             CssClass="btn70" />
                        <%--OnClientClick="javascript:return validate();"--%>
                        <asp:Button ID="btnExit" runat="server" CausesValidation="false" OnClick="btnExit_Click"
                            OnClientClick="return confirm('Do you want to leave this page?');" Text="Exit"
                             CssClass="btn70" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="grdGroup" runat="server" AllowPaging="true" AllowSorting="true"
                            AutoGenerateColumns="true" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="Arial" Font-Size="10pt" ForeColor="Black" GridLines="Vertical" OnPageIndexChanging="grdGroup_PageIndexChanging"
                            OnSelectedIndexChanged="grdGroup_SelectedIndexChanged" OnSorting="grdGroup_Sorting"
                            ShowFooter="false" ShowHeader="true" Style="text-align: left" Width="100%">
                            <FooterStyle BackColor="#C6C3C6" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                ForeColor="Black" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderText="Select" ItemStyle-Font-Bold="false"
                                    ItemStyle-Font-Names="Arial" SelectImageUrl="~/Accounts/AccImages/edit.PNG" ShowSelectButton="true">
                                    <ItemStyle Font-Bold="False" Font-Names="Arial" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
