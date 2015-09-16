<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmAccessDBUpload.aspx.cs" Inherits="ImpexCube.frmAccessDBUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 68%;" align="center">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    File Name
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="tx4" />
                    <asp:Button ID="btnRead" runat="server" Text="Read File" Width="70px" 
                        OnClick="btnRead_Click" CssClass="stylebutton" />
                    <asp:DropDownList ID="ddlTable" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:Button ID="btnLoad" runat="server" CssClass="stylebutton" 
                        onclick="btnLoad_Click" Text="Load" />
                </td>
            </tr>
        </table>
</asp:Content>
