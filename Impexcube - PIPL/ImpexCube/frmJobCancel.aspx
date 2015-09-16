<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmJobCancel.aspx.cs" Inherits="ImpexCube.frmJobCancel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width: 39%; height: 94px; text-align: left;">
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Job No"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtJobNo" runat="server" AutoPostBack="True" OnTextChanged="txtJobNo_TextChanged"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" CompletionListCssClass="completionList"
                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetJobNoIworkreg"
                        ServicePath="~/AutoComplete.asmx" TargetControlID="txtJobNo">
                    </cc1:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Party Name"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPartyName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Button ID="btnJobCancel" runat="server" Text="Job Cancel" OnClick="btnJobCancel_Click"
                        OnClientClick="return confirm('Do you want to Cancel the Job?');" />
                </td>
                <td>
                    <asp:Button ID="btnExit" runat="server" Text="Exit" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
