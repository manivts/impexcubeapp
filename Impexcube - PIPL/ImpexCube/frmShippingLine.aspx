<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmShippingLine.aspx.cs" Inherits="ImpexCube.frmShippingLine" %>
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
                        ShippingLine Master
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="1" class="style6">
                        Shipping Line
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="ddlShippingLine" runat="server" Width="200px"                             
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td align="right" colspan="1" class="style6">
                        Address
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtAddress" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="1" class="style6">
                        Contact Person
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtContactPerson" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="1" class="style6">
                        Telephone
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtTelephone" runat="server" Width="150px" 
                             CssClass="postmsgg23 required"></asp:TextBox>
                    </td>
                    </tr>                    
                    <tr>
                    <td align="right" colspan="1" class="style6">
                        Fax
                    </td>
                    <td class="style2">   
                        <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>                     
                    </td>
                    </tr>
                    <tr>
                    <td align="right" colspan="1" class="style6">
                        E-Mail
                    </td>
                    <td class="style2">   
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>                     
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
                        <asp:GridView ID="gvShippingLine" runat="server" CssClass="table-wrapper" 
                              AutoGenerateSelectButton="True" 
                              onselectedindexchanged="gvShippingLine_SelectedIndexChanged">
                        </asp:GridView>
                          </div>
                    </td>
                </tr>
                </table>
                </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
