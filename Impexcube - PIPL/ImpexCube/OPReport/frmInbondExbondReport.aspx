<%@ Page Title="" Language="C#" MasterPageFile="~/OPReport/OperationReport.Master" AutoEventWireup="true"
    CodeBehind="frmInbondExbondReport.aspx.cs" Inherits="ImpexCube.OPReport.frmInbondExbondReport" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="Panel1" runat="server">
        <table>
            <tr>
                <td class="style1" >
                    <asp:RadioButton ID="rbimporter" runat="server" Text="Importer" CssClass="fontsize"/>
                </td>               
                <td class="style8">
                    <asp:TextBox ID="txtimporter" runat="server" CssClass="textbox200"></asp:TextBox>
                </td>
                <td class="style8">
                    <asp:Label ID="Label5" runat="server" Text="Date" CssClass="fontsize"></asp:Label>
                </td>
                <td class="style9">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="textbox200"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                                                    </cc1:CalendarExtender>
                </td>
                <td class="style9" colspan="2">
                    &nbsp;
                    </td>
            </tr>
            <tr>
                 <td class="style1">
                     <asp:RadioButton ID="rbjobno" runat="server" Text="Job No." CssClass="fontsize"/>
                </td>          
                <td class="style8">
                    <asp:TextBox ID="txtjobno" runat="server" CssClass="textbox200"></asp:TextBox>
                </td>
                <td class="style8">
                    <asp:Label ID="Label1" runat="server" Text="Branch" CssClass="fontsize"></asp:Label>
                </td>
                <td class="style9">
                    <asp:DropDownList ID="ddlbranch" runat="server" CssClass="ddl200">
                    </asp:DropDownList>
                </td>
                <td class="style9">
                    <asp:Button ID="btngenerate" runat="server" Text="Generate" 
                        CssClass="stylebutton" onclick="btngenerate_Click"/>
                </td>
                <td colspan="1" class="style9">
                    <asp:Button ID="btnsendmail" runat="server" Text="Send Mail" CssClass="stylebutton"/>
                </td>                
            </tr>
            <tr>
                <td class="style8" colspan="6">
                    &nbsp;</td>
            </tr>
            <tr>
            <td colspan="6">
            <asp:GridView ID="gvInbondExbond" runat="server" Width="845px" CssClass="table-wrapperInv"
                                            AutoGenerateColumns="False" BorderColor="Black" 
                    BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="calibri" Font-Size="10pt" 
                    ForeColor="Black" GridLines="Vertical"
                                            ShowFooter="false" ShowHeader="true" Style="text-align: center; font-size: 9pt;"
                                            
                    OnSelectedIndexChanged="gvInbondExbond_SelectedIndexChanged" 
                    onrowdatabound="gvInbondExbond_RowDataBound">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" />
                                                <%--<asp:BoundField HeaderText="Sr.No" DataField="Sr.No"></asp:BoundField>--%>
                                                <asp:BoundField HeaderText="Sr.No" DataField="InvoiceNo" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Product Desription" DataField="InvoiceDate" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Quantity" DataField="TOI" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Value In" DataField="Currency" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Duty" DataField="CurrencyRate" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Job No" DataField="InvoiceValue" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Date" DataField="ProductValue" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="BE No" DataField="InvoiceINRAmount" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="BE Date" DataField="InvoiceINRAmount" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Quantity" DataField="InvoiceINRAmount" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Value In" DataField="InvoiceINRAmount" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Duty" DataField="InvoiceINRAmount" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Quantity" DataField="InvoiceINRAmount" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="#C6C3C6" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                ForeColor="Black" />
                                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
            </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
