<%@ Page Title="" Language="C#" MasterPageFile="~/OPReport/OperationReport.Master" AutoEventWireup="true" CodeBehind="frmImportItemReport.aspx.cs" Inherits="ImpexCube.OPReport.frmImportItemReport" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        <link rel="stylesheet" type="text/css" href="Content/Styles/Accordion.css" />
    <link rel="stylesheet" type="text/css" href="Content/Styles/jquery-ui.css" />
        .style1
        {
            width: 74px;
        }
        .style2
        {
            width: 112px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="Panel1" runat="server">
        <table>
            <tr>
                <td class="style2" >
                    <asp:Label ID="Label2" runat="server" Text="Report For" CssClass="fontsize"></asp:Label>
                </td>               
                <td >
                    <asp:DropDownList ID="ddlreportfor" runat="server" CssClass="ddl156" 
                        onselectedindexchanged="ddlreportfor_SelectedIndexChanged" 
                        AutoPostBack="True">
                        <asp:ListItem>JobNo</asp:ListItem>
                        <asp:ListItem>Importer</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtjobno" runat="server" CssClass="textbox150"></asp:TextBox>
                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetImPorterJobNo"
                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtjobno">
                </cc1:AutoCompleteExtender>                

                </td>
                </tr>
                <tr>
                <td class="style2" >
                    <asp:Label ID="Label5" runat="server" Text="From Date -To Date" 
                        CssClass="fontsize"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txtjobdate" runat="server" CssClass="textbox150"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtjobdate">
                                                    </cc1:CalendarExtender>
                </td>               
            </tr>
            <tr>
                 <td class="style2" >
                     <asp:Label ID="Label3" runat="server" Text="Product Description" CssClass="fontsize"></asp:Label>
                </td>          
                <td >
                    <asp:TextBox ID="txtproductdessc" runat="server" CssClass="textboxMulti200" 
                        Width="320px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnGenerate" runat="server" CssClass="stylebutton" 
                        Text="Generate" onclick="btnGenerate_Click" />
                </td>
                
                <td colspan="1" class="style9">
                    &nbsp;</td>                
            </tr>
            <tr>
                <td class="style8" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
            <td colspan="3">
            <asp:GridView ID="gvimportitemreport" runat="server" Width="1100px" CssClass="table-wrapperInv"
                                            AutoGenerateColumns="False" BorderColor="Black" 
                    BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="calibri" Font-Size="10pt" 
                    ForeColor="Black" GridLines="Vertical"
                                            ShowFooter="false" ShowHeader="true" Style="text-align: center; font-size: 9pt;">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" />                                                
                                                <asp:BoundField HeaderText="Job No" DataField="JobNo" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Job Date" DataField="JobReceivedDate" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="BE No" DataField="BENo" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="BE Date" DataField="BEDate" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="HAWB/HBL No" DataField="HouseBLNo" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="MAWB/MBL No" DataField="MasterBLNo" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Product Desc" DataField="ProductDesc" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Quantity" DataField="Qty" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Unit" DataField="Unit" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Unit Price" DataField="UnitPrice" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Amount" DataField="Amount" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Assesable Value(INR)" DataField="AssableValue" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="CIF Value(INR)" DataField="TotalInvoiceValue" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Total Basic Duty(INR)" DataField="TotBasicDutyAmt" HeaderStyle-HorizontalAlign="Center">
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
