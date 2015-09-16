<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmImpLicenceSearch.aspx.cs" Inherits="ImpexCube.frmImpLicenceSearch" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60919.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td class="fontsize" width="80px">
                Organization
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtOrganization" runat="server" CssClass="textbox150" Width="355px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                          CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                          EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetConsignee"
                                          ServicePath="~/AutoComplete.asmx" TargetControlID="txtOrganization"/>
            </td>
            <td>
                &nbsp;</td>
            <td width="200px">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="fontsize" width="150px">
                Lic Ref. No
            </td>
            <td width="100px">
                <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox150" ></asp:TextBox>
            </td>
            <td class="fontsize">
                Lic No</td>
            <td>
                
                <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox150" ></asp:TextBox>
                
            </td>
            <td>
                
                <asp:Button ID="Button1" runat="server" CssClass="stylebutton" Text="Search" />
                
            </td>
            <td>
                
                <asp:Button ID="Button2" runat="server" CssClass="stylebutton" 
                    Text="New Licence" onclick="Button2_Click" />
                
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
            <td colspan="3">
                
            </td>
            <td>
                
                &nbsp;</td>
            <td>
                
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="6">
                
            </td>
        </tr>
    </table>
</asp:Content>
