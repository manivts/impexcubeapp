<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAuthentication.aspx.cs"
    MasterPageFile="~/ImpexCube.Master" Inherits="ImpexCube.UserAuthentication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src='<%= ResolveClientUrl("~/Scripts/Validation.js") %>'></script>
    <script type="text/javascript">
        
    </script>
    <table style="width: 154px; height: 161px; background-color: #f8f8ff">
        <tr>
            <td style="vertical-align: top">
                <br />
                <asp:LinkButton ID="BtnADD_DEPT" runat="server" Font-Names="Corbel" Font-Overline="False"
                    CausesValidation="False" Font-Size="9pt" ForeColor="DodgerBlue" Width="150px"
                    Height="25px" Font-Bold="True" BorderColor="White" BorderWidth="1px" BorderStyle="Solid">Add New</asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<cc1:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" BorderColor="AntiqueWhite"
        Corners="All" Radius="10" TargetControlID="Panel1">
    </cc1:RoundedCornersExtender>--%>
    <table>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Width="808px" BackColor="Snow">
                    <table>
                        <tr>
                            <td>
                                <table class="style9">
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Label ID="Label3" runat="server" Text="User Authorization" Font-Size="12pt"
                                                Font-Names="Verdana" Width="400px" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="background-color: Red; height: 1px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="style13" style="height: 21px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style13">
                                            <asp:Label ID="Label15" runat="server" Text="UserType" CssClass="Body"></asp:Label>
                                        </td>
                                        <td class="style12">
                                            <asp:Label ID="Label16" runat="server" CssClass="Body" Text=":"></asp:Label>
                                        </td>
                                        <td align="left" class="style11">
                                            <asp:DropDownList ID="ddlUserType" runat="server" Width="154px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelgrid" runat="server">
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="grdUserForms" runat="server" AutoGenerateColumns="false">
                                    <RowStyle BackColor="Lavender" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px"
                                        ForeColor="Black" />
                                    <Columns>
                                        <asp:BoundField DataField="FormName" HeaderText="Form NAme" />
                                        <asp:BoundField DataField="FormDesc" HeaderText="Description" />
                                        <asp:TemplateField ItemStyle-Width="55px" ItemStyle-HorizontalAlign="Center" HeaderText="Read">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRead" runat="server" Width="15px" HeaderText="Read"/>
                                            </ItemTemplate>
                                            <ItemStyle Width="55px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="55px" ItemStyle-HorizontalAlign="Center" HeaderText="Write">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkWrite" runat="server" Width="15px" />
                                            </ItemTemplate>
                                            <ItemStyle Width="55px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="55px" ItemStyle-HorizontalAlign="Center" HeaderText="Approval">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkApproval" runat="server" Width="15px" />
                                            </ItemTemplate>
                                            <ItemStyle Width="55px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#2461bf" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table align="center" width="800px">
                        <tr>
                            <td colspan="3" align="center">
                                <asp:ImageButton ID="Btnsave" runat="server" Height="25px" 
                                    ImageUrl="~/Image/Buttons/B_Save.png" onclick="Btnsave_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Image ID="Image1" runat="server" Visible="false" ImageUrl="~/Image/NoDataFound.gif"
                                    Width="210px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <input type="hidden" id="hdnDeptId" runat="server" />
</asp:Content>
