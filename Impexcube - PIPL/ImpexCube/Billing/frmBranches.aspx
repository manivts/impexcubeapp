<%@ Page Language="C#"  MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" CodeBehind="frmBranches.aspx.cs" Inherits="ImpexCube.Billing.frmBranches" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

                        <tr id="TrAddr" runat="server">
                            <td style="vertical-align: top;" align="left" class="style33">
                                <div id="GrdADDRSCROLL" class="grid_scroll" style="width: 700px; height: 200px;"
                                    runat="server">
                                    <asp:GridView ID="GrdPaddr" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" DataKeyNames="BranchId"
                                           Width="674px">
                                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                        <Columns>
                                            <asp:BoundField DataField="AccountCode" HeaderText="PCODE" SortExpression="party_code" />
                                            <asp:BoundField DataField="BranchId" HeaderText="Branch" SortExpression="addr_num" />
                                            <asp:BoundField DataField="Address1" HeaderText="Address" SortExpression="address" />
                                            <asp:BoundField DataField="City" HeaderText="City" SortExpression="city" />
                                            <asp:BoundField DataField="State" HeaderText="State" SortExpression="Pin" />
                                            <asp:BoundField DataField="Pincode" HeaderText="Pin" SortExpression="pin" />
                                            <asp:CommandField ButtonType="Image" HeaderText="CL" SelectImageUrl="~/image/select.jpg"
                                                ShowSelectButton="True">
                                                <HeaderStyle Height="5px" />
                                                <ItemStyle Height="5px" Width="5px" />
                                            </asp:CommandField>
                                        </Columns>
                                        <RowStyle BackColor="White" ForeColor="#003399" />
                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>