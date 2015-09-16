<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmBEFile.aspx.cs" Inherits="ImpexCube.frmBEFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">


        .style5
        {
            font-size: 8pt;
            font-family: Arial;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" style="font-size: 10pt" Text="Job No  : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlJobNo" runat="server" AppendDataBoundItems="True" 
                        AutoPostBack="True" CssClass="style5" Height="20px" Width="250px">
                        <asp:ListItem>~Select~</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnGenerate" runat="server" onclick="btnGenerate_Click" 
                        Text="Generate" />
                </td>
                <td>
                    </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:TextBox ID="txtBeFile" runat="server" BackColor="Black" BorderStyle="None" 
                        ForeColor="White" Height="600px" TextMode="MultiLine" Width="900px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<br />
</asp:Content>
