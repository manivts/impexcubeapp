<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MainMaster.Master" AutoEventWireup="true" CodeBehind="Sync.aspx.cs" Inherits="AccountsManagement.Sync" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .style8
    {
        color: #000066;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
    AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <span class="style8">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            Please Wait.....</span><asp:Image ID="Image1" runat="server" 
                ImageUrl="~/Accounts/AccImages/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
<div>
<table width="100%">
<tr>
<td style="text-align: center" width="100%">

    <asp:Label ID="Label1" runat="server" 
        style="font-weight: 700; font-size: large" Text="Sync"></asp:Label>

</td>
</tr>
<tr>
<td style="text-align: center" width="100%">

    <asp:Button ID="btnCharge" runat="server" onclick="btnCharge_Click" 
        Text="Charge Master Sync" Width="200px" />

</td>
</tr>
<tr>
<td style="text-align: center" width="100%">

    <asp:Button ID="ButtonJobNo" runat="server" onclick="ButtonJobNo_Click" 
        Text="Job No" Width="200px" />
    </td>
</tr>
<tr>
<td style="text-align: center" width="100%">

    <asp:Button ID="btnParty" runat="server" Text="Party Master Sync" 
        Width="200px" onclick="btnParty_Click" />

</td>
</tr>
<tr>
<td style="text-align: center" width="100%">

    &nbsp;</td>
</tr>
</table>
</div>
</ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
