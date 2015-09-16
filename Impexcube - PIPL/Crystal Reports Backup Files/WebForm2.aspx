<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="ImpexCube.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.Chk123 {
    background-color: #85250D ;
    border-radius: 10px 10px 10px 10px;
    box-shadow: 0 1px 1px rgba(0, 0, 0, 0.3);
    content: "";
    height: 8px;
    margin-right: -4px;
    margin-top: -4px;
    position: absolute;
    right: 50%;
    top: 50%;
    width: 8px;
}

    .style1
    {
        width: 1345px;
        height: 542px;
    }

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" 
        style="z-index: 1; left: 181px; top: 279px; position: absolute; right: 872px" 
        Text="Followup"></asp:Label>
    <img class="style1" src="Content/Images/Tasks-tab.png" />
</asp:Content>
