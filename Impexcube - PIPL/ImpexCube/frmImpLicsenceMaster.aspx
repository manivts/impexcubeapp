<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmImpLicsenceMaster.aspx.cs" Inherits="ImpexCube.frmImpLicsenceMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="80%">
                <tr>
                    <td colspan="12" align="center">
                        <b>Licence Master</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="Label1" runat="server" Text="Licence Ref No" CssClass="fontsize"></asp:Label>
                    </td>
                    <td colspan="9">
                        <asp:TextBox ID="txtLicenceRefNo" runat="server" ReadOnly="true" 
                            CssClass="textbox200" Width="247px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="Label2" runat="server" Text="Type" CssClass="fontsize"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlSchemeType" runat="server" CssClass="textbox200" 
                            OnSelectedIndexChanged="ddlSchemeType_SelectedIndexChanged" 
                            AutoPostBack="True" Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="Label3" runat="server" Text="Scheme Notn" CssClass="fontsize"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlSchemeNotn" runat="server" CssClass="textbox100" 
                            OnSelectedIndexChanged="ddlSchemeNotn_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp;<asp:DropDownList ID="ddlSubSchemeNotn" runat="server" CssClass="textbox75">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="Label4" runat="server" Text="Licence No" CssClass="fontsize"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtLicenceNo" runat="server" CssClass="textbox100"></asp:TextBox>
                        &nbsp;<asp:Label ID="Label6" runat="server" Text="Date" CssClass="fontsize"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtLicenceDate" runat="server" CssClass="textbox100"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLicenceDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="Label5" runat="server" Text="Expiry Date" CssClass="fontsize"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="textbox100"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtExpiryDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="Label7" runat="server" Text="Organization" CssClass="fontsize"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtOrganization" runat="server" CssClass="textbox200" 
                            Width="247px"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetCustomer" ServicePath="~/AutoComplete.asmx"
                            TargetControlID="txtOrganization">
                        </cc1:AutoCompleteExtender>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="Label8" runat="server" Text="Date of Surrender" CssClass="fontsize"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtSurrenderDate" runat="server" CssClass="textbox100"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtSurrenderDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="Label9" runat="server" Text="EDI Regn No" CssClass="fontsize"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtEdiRegn" runat="server" CssClass="textbox100"></asp:TextBox>
                        &nbsp;<asp:Label ID="Label10" runat="server" Text="Date" CssClass="fontsize"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtEDIDate" runat="server" CssClass="textbox100"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEDIDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="Label11" runat="server" Text="Currency" CssClass="fontsize"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlCurency" runat="server" CssClass="textbox75">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="Label12" runat="server" Text="Port of Regn." CssClass="fontsize"></asp:Label>
                    </td>
                    <td colspan="9">
                        <asp:DropDownList ID="ddlPortRegn" runat="server" CssClass="ddl200" 
                            Width="250px">
                        </asp:DropDownList>                                                
                    </td>
                </tr>
                <tr id="rwValue" runat="server" visible="false">
                    <td class="style1" colspan="3">
                        <asp:Label ID="Label13" runat="server" Text="Total Value(INR)" CssClass="fontsize"></asp:Label>
                    </td>
                    <td colspan="9" class="style1">
                        <asp:TextBox ID="txtTotalValue" runat="server" CssClass="textbox100"></asp:TextBox>
                        &nbsp;<asp:Label ID="Label14" runat="server" Text="Op. Bal.(INR)" CssClass="fontsize"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtOpBalance" runat="server" CssClass="textbox100"></asp:TextBox>
                    </td>
                </tr>
               <tr id="rwImportItems" runat="server" visible="false" style="border-style: solid">
                     <td colspan="12">
                         <asp:Panel ID="pnlImport"  runat="server" GroupingText="Import Items">
                        <table style="border-width: thin; width: 89%">
                            <tr>
                    <td>
                        
                        <asp:Label ID="Label16" runat="server" CssClass="fontsize" Text="Description"></asp:Label>
                        
                    </td>
                    <td>
                        <asp:Label ID="Label17" runat="server" CssClass="fontsize" 
                            Text="CIF Value(INR)"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" CssClass="fontsize" Text="CIF Value(FC)"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server" CssClass="fontsize" Text="Quantity"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label20" runat="server" CssClass="fontsize" Text="Unit"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="label21" runat="server" CssClass="fontsize" Text="OP.Bal (INR)"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label22" runat="server" CssClass="fontsize" Text="OP.Bal (FC)"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label23" runat="server" CssClass="fontsize" Text="OP.Bal. Qty"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label24" runat="server" CssClass="fontsize" Text="Unit"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btnImpNew" runat="server" BackColor="#73AAE8" 
                            CssClass="ui-priority-primary" OnClick="btnImpNew_Click" Text="New" />
                    </td></tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtImpDesc" runat="server" CssClass="textbox150"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCIFValueINR" runat="server" AutoPostBack="True" 
                                        CssClass="textbox100" OnTextChanged="txtCIFValueINR_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCIFValueFC" runat="server" AutoPostBack="True" 
                                        CssClass="textbox100" OnTextChanged="txtCIFValueFC_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImpQuantity" runat="server" AutoPostBack="True" 
                                        CssClass="textbox75" OnTextChanged="txtImpQuantity_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlImpUnit" runat="server" CssClass="ddl75" 
                                        OnSelectedIndexChanged="ddlImpUnit_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImpOPBalINR" runat="server" CssClass="textbox100"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImpOpBalFC" runat="server" CssClass="textbox100"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImpOpBalQty" runat="server" CssClass="textbox100"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="txtImpUnit0" runat="server" CssClass="textbox75"></asp:Label>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnImpAdd" runat="server" 
                                        ImageUrl="~/Content/Images/Add.jpg" OnClick="btnImpAdd_Click" ToolTip="Add" />
                                    <asp:ImageButton ID="btnImpUpdate" runat="server" 
                                        ImageUrl="~/Content/Images/Add.jpg" OnClick="btnImpUpdate_Click" 
                                        ToolTip="Update" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10">
                                    <asp:GridView ID="gvImportItems" runat="server" AutoGenerateColumns="False" 
                                        AutoGenerateSelectButton="True" BorderColor="Black" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Names="calibri" Font-Size="10pt" ForeColor="Black" 
                                        GridLines="Vertical" 
                                        OnSelectedIndexChanged="gvImportItems_SelectedIndexChanged" ShowFooter="True" 
                                        ShowHeader="true" Style="text-align: center; font-size: 9pt;" Visible="True" 
                                        Width="820px">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnImpDelete" runat="server" 
                                                        ImageUrl="~/Content/Images/delete.gif" OnClick="btnImpDelete_Click" 
                                                        OnClientClick="return confirm('Do U Want Delete?');" Width="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LicenceImportID" HeaderStyle-CssClass="hiddencol" 
                                                HeaderText="Id" ItemStyle-CssClass="hiddencol">
                                            <HeaderStyle CssClass="hiddencol" />
                                            <ItemStyle CssClass="hiddencol" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Description" HeaderText="Desc" />
                                            <asp:BoundField DataField="CIFValueINR" HeaderText="FOB INR" />
                                            <asp:BoundField DataField="CIFValueFC" HeaderText="FOB FC" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                            <asp:BoundField DataField="CIFUnit" HeaderText="Unit" />
                                            <asp:BoundField DataField="OpBalINR" HeaderText="Op Bal INR" />
                                            <asp:BoundField DataField="OpBalFC" HeaderText="Op. Bal FC" />
                                            <asp:BoundField DataField="OpBalQty" HeaderText="Op. Bal Qty" />
                                            <asp:BoundField DataField="OPUnit" HeaderText="Unit" />
                                        </Columns>
                                        <FooterStyle BackColor="#C6C3C6" BorderColor="Black" BorderStyle="Solid" 
                                            BorderWidth="1px" ForeColor="Black" />
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
                    </td>
                </tr>
                <tr id="rwExportItems" runat="server"  style="border-style: solid">
                    <td colspan="12">
                        <asp:Panel ID="pnlExport"  runat="server" GroupingText="Export Items" 
                            Visible="False">
                        <table style="border-width: thin; width: 89%">
                            <tr>
                                <td class="style10">
                                    <asp:Label ID="lblDescription" runat="server" CssClass="fontsize" Text="Description"></asp:Label>
                                </td>
                                <td class="style8">
                                    <asp:Label ID="lblFobValueINR" runat="server" CssClass="fontsize" Text="FOB Value(INR)"></asp:Label>
                                </td>
                                <td class="style9">
                                    <asp:Label ID="lblFobValueFC" runat="server" CssClass="fontsize" Text="FOB Value(FC)"></asp:Label>
                                </td>
                                <%-- <td class="tdcolumn100">
                                                    <asp:Label ID="lblFreight" runat="server" CssClass="fontsize" 
                                                        Text="Freight Type"></asp:Label>
                                                </td>--%>
                                <td class="style9">
                                    <asp:Label ID="lblQuantity" runat="server" CssClass="fontsize" Text="Quantity"></asp:Label>
                                </td>
                                <td class="style9">
                                    <asp:Label ID="lblUnit" runat="server" CssClass="fontsize" Text="Unit"></asp:Label>
                                </td>
                                <td class="style8">
                                    <asp:Label ID="labelOpBalINR" runat="server" CssClass="fontsize" Text="OP.Bal (INR)"></asp:Label>
                                </td>
                                <td class="style8">
                                    <asp:Label ID="lblOPBalFC" runat="server" CssClass="fontsize" Text="OP.Bal (FC)"></asp:Label>
                                </td>
                                <td class="style8">
                                    <asp:Label ID="lblOpBalQty" runat="server" CssClass="fontsize" Text="OP.Bal. Qty"></asp:Label>
                                </td>
                                <td class="style8">
                                    <asp:Label ID="lblUnit0" runat="server" CssClass="fontsize" Text="Unit"></asp:Label>
                                </td>
                                <td class="tdcolumn">
                                    <asp:Button ID="btnNew" runat="server" BackColor="#73AAE8" Text="New" CssClass="ui-priority-primary"
                                        OnClick="btnNew_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox150"></asp:TextBox>
                                </td>
                                <td class="tdcolumn100">
                                    <asp:TextBox ID="txtFobValueINR" runat="server" CssClass="textbox100" AutoPostBack="True"
                                        OnTextChanged="txtFobValueINR_TextChanged"></asp:TextBox>
                                </td>
                                <td class="tdcolumn100">
                                    <asp:TextBox ID="txtFobValueFC" runat="server" CssClass="textbox100" AutoPostBack="True"
                                        OnTextChanged="txtFobValueFC_TextChanged"></asp:TextBox>
                                </td>
                                <%--<td class="tdcolumn100">
                                                    <asp:DropDownList ID="ddlFreightType" runat="server" 
                                                        AppendDataBoundItems="True" CssClass="ddl100">
                                                        <asp:ListItem>~Select~</asp:ListItem>
                                                        <asp:ListItem>Single freight</asp:ListItem>
                                                        <asp:ListItem>Separate freight</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>--%>
                                <td align="center" class="tdcolumn75">
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="textbox75" AutoPostBack="True"
                                        OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>
                                </td>
                                <td class="tdcolumn75">
                                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="ddl75" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="tdcolumn100">
                                    <asp:TextBox ID="txtOPBalINR" runat="server" CssClass="textbox100"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOPBalFC" runat="server" CssClass="textbox100"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOPBalQty" runat="server" CssClass="textbox100"></asp:TextBox>
                                </td>
                                <td class="tdcolumn75">
                                    <asp:Label ID="txtUnit0" runat="server" CssClass="textbox75"></asp:Label>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Content/Images/Add.jpg" ToolTip="Add"
                                        OnClick="btnAdd_Click" />
                                    <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/Content/Images/Add.jpg"
                                        Visible="False" ToolTip="Update" OnClick="btnUpdate_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="center" colspan="10">
                                    <asp:GridView ID="gvExportItems" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="calibri"
                                        Font-Size="10pt" ForeColor="Black" GridLines="Vertical" ShowFooter="True" ShowHeader="true"
                                        Style="text-align: center; font-size: 9pt;" Visible="True" Width="820px" OnSelectedIndexChanged="gvExportItems_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Content/Images/delete.gif"
                                                        OnClientClick="return confirm('Do U Want Delete?');" Width="20px" OnClick="btnDelete_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LicenceExportID" HeaderStyle-CssClass="hiddencol" HeaderText="Id"
                                                ItemStyle-CssClass="hiddencol">
                                                <HeaderStyle CssClass="hiddencol" />
                                                <ItemStyle CssClass="hiddencol" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Description" HeaderText="Desc" />
                                            <asp:BoundField DataField="FobValueINR" HeaderText="FOB INR" />
                                            <asp:BoundField DataField="FobValueFC" HeaderText="FOB FC" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                            <asp:BoundField DataField="FOBUnit" HeaderText="Unit" />
                                            <asp:BoundField DataField="OpBalINR" HeaderText="Op Bal INR" />
                                            <asp:BoundField DataField="OpBalFC" HeaderText="Op. Bal FC" />
                                            <asp:BoundField DataField="OpBalQty" HeaderText="Op. Bal Qty" />
                                            <asp:BoundField DataField="OPUnit" HeaderText="Unit" />
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
                    </td>
                </tr>
             
                <tr>
                    <td colspan="12">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="12" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Close" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
