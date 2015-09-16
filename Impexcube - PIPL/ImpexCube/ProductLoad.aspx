<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="ProductLoad.aspx.cs" Inherits="ImpexCube.ProductLoad" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60919.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 50%;" align="left">
        <tr>
            <td class="fontsize" align="right">
            </td>
            <td width="100px">
            </td>
            <td rowspan="6" class="fontsize">
                <asp:GridView ID="gvInvoice" runat="server" ForeColor="#333333" GridLines="None"
                    Font-Size="10pt" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Invoicenumber" HeaderText="Excel Uploaded Invoice" HtmlEncode="False" />
                    </Columns>
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="fontsize" align="right">
                JobNo
            </td>
            <td width="100px">
                <asp:DropDownList ID="ddlJobNo" runat="server" Width="160px" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" CssClass="ddl150" AppendDataBoundItems="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="fontsize" align="right">
                Invoice NO
            </td>
            <td>
                <asp:DropDownList ID="ddlInvoice" runat="server" Width="160px" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlInvoice_SelectedIndexChanged" CssClass="ddl150" AppendDataBoundItems="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="fontsize" colspan="3" width="70%">
                <div style="width: 113%; overflow-y: scroll; overflow-x: scroll; height: 300px;">
                    <asp:GridView ID="gvProductDetails" runat="server" AutoGenerateColumns="False" OnRowCreated="gvProductDetails_RowCreated"
                        OnRowDataBound="gvProductDetails_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        <Columns>
                            <asp:BoundField DataField="JobNo" HeaderText="Job No." HtmlEncode="False" />
                            <asp:BoundField DataField="Invoicenumber" HeaderText="Inv.No." HtmlEncode="False" />
                            <asp:BoundField DataField="InvoiceSrNo" HeaderText="Inv.Sr.no." HtmlEncode="False"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Description" HeaderText="Product Description" ItemStyle-CssClass="textbox400"
                                ItemStyle-Wrap="true" />
                            <asp:BoundField DataField="CommodityCode" HeaderText="RITC" HtmlEncode="False" />
                            <asp:TemplateField HeaderText="RITC No">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRITCNO" runat="server"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetRITCCode" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtRITCNO">
                                    </cc1:AutoCompleteExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Calculation Based on">
                                <ItemTemplate>
                                    <asp:RadioButtonList ID="rbMeasurementType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Quantity" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Weight"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PurchaseOrderNumber" HeaderText="PO NO" HtmlEncode="False" />
                            <asp:BoundField DataField="Quantity" HeaderText="Qty" HtmlEncode="False" />
                            <asp:BoundField DataField="QuantityUnit" HeaderText="Unit" HtmlEncode="False" />
                            <asp:BoundField DataField="TotalValue" HeaderText="Tot.Val" HtmlEncode="False" />
                            <asp:BoundField DataField="ValueUnit" HeaderText="Curr" HtmlEncode="False" />
                            <asp:BoundField DataField="TotalWeight" HeaderText="Total Wt." HtmlEncode="False" />
                            <asp:BoundField DataField="WeightUnit" HeaderText="Unit" HtmlEncode="False" />
                            <asp:BoundField DataField="CountryOfOrigin" HeaderText="Country of Org." HtmlEncode="False" />
                            <asp:BoundField DataField="ProductCode" HeaderText="Prod Code" HtmlEncode="False" />
                            <asp:BoundField DataField="UnitPrice" HeaderText="Unt Price" HtmlEncode="False" />
                            <asp:BoundField DataField="Addition" HeaderText="Addition" ItemStyle-CssClass="textbox400"
                                HtmlEncode="False" />
                                <asp:BoundField DataField="POSrNo" HeaderText="POSrNo" ItemStyle-CssClass="textbox400"
                                HtmlEncode="False" />
                        </Columns>
                    </asp:GridView>
                    </div>
            </td>
        </tr>
        <tr>
            <td class="fontsize" colspan="3" width="70%" align="center">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" Enabled="false"
                    OnClick="btnSave_Click" CssClass="stylebutton" Visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>
