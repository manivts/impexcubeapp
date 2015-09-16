<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmUserRoles.aspx.cs" Inherits="ImpexCube.frmUserRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function Validation() {

            if (document.getElementById('<%=txtUserRoleName.ClientID%>').value == '') {
                alert('Enter User Role Name');
                document.getElementById('<%=txtUserRoleName.ClientID%>').focus();
                return false;
            }
        }
    </script>
    <center>
        <asp:Label ID="lblRoles" runat="server" Text="USER ROLES" BackColor="#CCFFFF" Font-Bold="True"></asp:Label></center>
    <table align="center">
        <tr>
            <td>
                &nbsp
            </td>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbluserRoleName" runat="server" Text="UserRole Name : "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUserRoleName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="63px" OnClick="btnSave_Click" OnClientClick="javascript:return Validation();" />
            </td>
            <td align="justify">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    onclick="btnCancel_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="grdUserRole" runat="server">
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
