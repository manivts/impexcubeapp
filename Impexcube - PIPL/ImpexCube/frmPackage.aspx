<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmPackage.aspx.cs" Inherits="ImpexCube.frmPackage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table>
        <table class="style5" style="text-align: left">
                <tr>
                    <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                        font-size: large">
                        Package Master
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="1" class="style6">
                        Package Name
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="ddlPackage" runat="server" Width="200px"                             
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td align="right" colspan="1" class="style6">
                        Short Name
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtShortname" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="1" class="style6">
                        Plural Name
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtPluralname" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="1" class="style6">
                        Said To Contain
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtsaidtocon" runat="server" Width="150px" 
                             CssClass="postmsgg23 required"></asp:TextBox>
                    </td>
                    </tr>                    
                    <tr>
                    <td align="right" colspan="1" class="style6">
                        UNECE Code
                    </td>
                    <td class="style2">   
                        <asp:TextBox ID="txtUNECECode" runat="server"></asp:TextBox>                     
                    </td>
                    </tr>
                    <tr>
                <td style="text-align: right">
                    <asp:Button ID="btnNew" runat="server" Text="New" Width="100px" 
                        onclick="btnNew_Click" />

                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
                        onclick="btnSave_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="100px" 
                        onclick="btnUpdate_Click" />
                    <asp:Button ID="btnDiscard" runat="server" Text="Discard" Width="100px" 
                        onclick="btnDiscard_Click" />
                </td>
                </tr>
                <tr>
                    <td colspan="2">
                      <div class="grid_scroll-2">
                        <asp:GridView ID="gvPackage" runat="server" CssClass="table-wrapper" 
                              AutoGenerateSelectButton="True" 
                              onselectedindexchanged="gvPackage_SelectedIndexChanged1">
                        </asp:GridView>
                          </div>
                    </td>
                </tr>
                </table>
                </table>
                    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
