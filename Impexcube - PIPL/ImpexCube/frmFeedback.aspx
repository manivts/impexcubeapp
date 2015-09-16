<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmFeedback.aspx.cs" Inherits="WMS.frmFeedback" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table align="center" style="width: 800px" class="box">
        <tr>
            <td class="boxBody">
                &nbsp;<table align="center">
                    <tr>
                        <td align="left">
                            <table width="100%" style="width: 520px">
                                <tr>
                                    <td style="text-align: center;" colspan="2">
                <label style="text-align: center; color: #006600; width: 800px;">
                    Feedback Form</label></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                            <label>
                                        Page Name</label></td>
                                    <td>
                            <asp:TextBox ID="txtForm" runat="server" CssClass="boxtextbox" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <label>
                                            Description</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="boxtextbox" TextMode="MultiLine"
                                            Width="420px" Height="60"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Send" CssClass="btnLogin" 
                                OnClick="btnSubmit_Click" Width="70px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
