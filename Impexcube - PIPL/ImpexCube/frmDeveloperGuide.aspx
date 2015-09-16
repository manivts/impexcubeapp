<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmDeveloperGuide.aspx.cs" Inherits="ImpexCube.frmDeveloperGuide" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
        <table style="width:71%;">
            <tr>
            <td class="fontsize" align="center" colspan="2" 
                    style="color: #008080; font-size: 15px;">
                <strong>Developer Guide</strong>
            </td>
        </tr>
            
            
            <tr>
                <td width="180px">
                    <asp:Label ID="Label1" runat="server" Text="Trans Date" class="fontsize"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txttransdate" runat="server" CssClass="textbox150" 
                        Width="150px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txttransdate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                        </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Developer Name" class="fontsize"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtdevname" runat="server" CssClass="textbox150" Width="150px"></asp:TextBox></td></tr>
                    <tr>
                    <td>
                    <asp:Label ID="Label3" runat="server" Text="Alloted By" class="fontsize"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtallotedby" runat="server" CssClass="textbox150" 
                        Width="150px"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td>
                    <asp:Label ID="Label4" runat="server" Text="Module Name" class="fontsize"></asp:Label></td>
                <td>
                    
                    <asp:DropDownList ID="ddlmodulename" runat="server" CssClass="ddl156" 
                        AutoPostBack="True" onselectedindexchanged="ddlmodulename_SelectedIndexChanged">
                    </asp:DropDownList></td>
                    </tr>
                    <tr>
                    <td>
                    <asp:Label ID="Label5" runat="server" Text="Form Name" class="fontsize"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlformname" runat="server" CssClass="ddl156">
                    </asp:DropDownList></td>
                    </tr>
                    <tr>
                    <td>
                    <asp:Label ID="Label6" runat="server" Text="Description" class="fontsize"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtdesc" runat="server" CssClass="textboxHeight29" 
                                TextMode="MultiLine" Height="109px" Width="429px"></asp:TextBox></td>
                    </tr>
            <tr>
            <td align="center" colspan="2">
                &nbsp;</td>
            </tr>
            <tr>
            <td align="center" colspan="2">
            <asp:Button ID="btnNew" runat="server" Text="New" CssClass="masterbutton" Height="28px"
                    />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="masterbutton" 
                    Height="28px" onclick="btnSave_Click"  /></td>
            </tr>
        </table>
    
        <br />
    
</asp:Content>
