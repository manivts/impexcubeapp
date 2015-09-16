<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="efrmInvoiceExport.aspx.cs" Inherits="ImpexCube.efrmInvoiceExport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/Styles/Accordion.css" />
    <link rel="stylesheet" type="text/css" href="Content/Styles/jquery-ui.css" />
    <script type="text/javascript" src="Content/Scripts/Accordion.js"></script>
    <script type="text/javascript" src="Content/Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="Content/Scripts/jquery-ui.js"></script>
    <script src="Content/JQuery/JSONInvoice.js" type="text/javascript"></script>
    <style type="text/css">
        .style2
        {
            width: 149px;
        }
        .stylebtn6
        {
        }
        .style7
        {
            font-family: calibri;
            font-size: 8pt;
            padding-left: 0px;
            color: Purple;
            width: 115px;
        }
        .style8
        {
            width: 75px;
            text-align: center;
            font-weight: bold;
        }
        .style9
        {
            width: 100px;
            text-align: center;
            font-weight: bold;
        }
        .style10
        {
            height: 15px;
            font-family: Verdana;
            width: 75px;
            font-size: 8pt;
            font-weight: bold;
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function freightvalidate() {

            var invno = document.getElementById("<%= txtinvno.ClientID %>").value;
            if (invno == '') {
                alert('Please Select Invoice Number To Update');
                document.getElementById("<%= txtinvno.ClientID %>").focus();
                return false;
            }
        }

        function UpdateValidate() {
            if (document.getElementById("<%= txtinvno.ClientID %>").value == '') {
                alert('Please Select Invoice Number To Update');
                document.getElementById("<%= txtinvno.ClientID %>").focus();
                return false;
            }

            if (document.getElementById("<%= txtAmountinINR.ClientID %>").value == '') {
                alert('Amount cannot be empty');
                document.getElementById("<%= txtAmountinINR.ClientID %>").focus();
                return false;
            }
        }

        function callddlInvoiceCurrency() {
            var Sel1 = $("#ContentPlaceHolder1_ddlInvoiceCurrency").val();
            var exra1 = $("#ContentPlaceHolder1_txtexcrate"); //
            BindExchgeRate(Sel1, exra1);
        }
        function callddlFreightDetails() {
            var Sel1 = $("#ContentPlaceHolder1_ddlFreight").val();
            var exra1 = $("#ContentPlaceHolder1_txtfreight1"); //
            BindExchgeRate(Sel1, exra1);
        }
        function callddlInsurance() {
            var Sel1 = $("#ContentPlaceHolder1_ddlInsurace").val();
            var exra1 = $("#ContentPlaceHolder1_txtinsurance1"); //
            BindExchgeRate(Sel1, exra1);
        }
        function callddlDiscount() {
            var Sel1 = $("#ContentPlaceHolder1_ddlDiscountcurr").val();
            var exra1 = $("#ContentPlaceHolder1_txtDiscExcRate"); //
            BindExchgeRate(Sel1, exra1);
        }
        function callddlfob() {
            var Sel1 = $("#ContentPlaceHolder1_ddlPackFobCurr").val();
            var exra1 = $("#ContentPlaceHolder1_txtPackFobExcRate"); //
            BindExchgeRate(Sel1, exra1);
        }
        function callddlothdeduct() {
            var Sel1 = $("#ContentPlaceHolder1_ddlOthDedcurr").val();
            var exra1 = $("#ContentPlaceHolder1_txtOthDedExcRate"); //
            BindExchgeRate(Sel1, exra1);
        }
        function callddlcommision() {
            var Sel1 = $("#ContentPlaceHolder1_ddlCommCurr").val();
            var exra1 = $("#ContentPlaceHolder1_txtCommExcRate"); //
            BindExchgeRate(Sel1, exra1);
        }

        function NewInvoice() {
            document.getElementById("<%= txtinvno.ClientID %>").value = '';
            document.getElementById("<%= txtdate.ClientID %>").value = '';
            document.getElementById("<%= ddlTermsofInvoice.ClientID %>").selectedIndex = 0;
            document.getElementById("<%= ddlInvoiceCurrency.ClientID %>").selectedIndex = 0;
            document.getElementById("<%= txtexcrate.ClientID %>").value = 0;
            document.getElementById("<%= txtinvval.ClientID %>").value = 0;
            document.getElementById("<%= txtprodval.ClientID %>").value = 0;
            document.getElementById("<%= txtAmountinINR.ClientID %>").value = 0;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" style="height: 350px;">
                <tr>
                    <td valign="top" class="style6">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnFreightInsure" runat="server" CssClass="stylebutton" OnClick="btnFreightInsure_Click"
                                        Text="Freight Insure" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnOtherInfo" runat="server" CssClass="stylebutton" OnClick="btnOtherInfo_Click"
                                        Text="Other Info" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAnnexure" runat="server" CssClass="stylebutton" Text="Annexure C1 Details"
                                        OnClick="btnAnnexure_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnproduct" runat="server" CssClass="stylebutton" Text="Product"
                                        OnClick="btnproduct_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnCheckList" runat="server" CssClass="stylebutton" Text="CheckList" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Blue">
                            <table width="800" border="0" frame="border" style="border-style: groove;">
                                <tr>
                                    <td class="center" colspan="9">
                                        <asp:Label ID="lblHeading" runat="server" Style="font-weight: 700" Text="Invoice Details"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label2" runat="server" Text="Invoice No" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <%--                                                <td class="style10">
                                                    <asp:Label ID="lblInvoiceNo" runat="server" CssClass="textbox75" Text="Invoice No"></asp:Label>
                                                </td>--%>
                                                <td class="style8">
                                                    <asp:Label ID="Label3" runat="server" Text="Date" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td class="style8">
                                                    <asp:Label ID="Label4" runat="server" Text="Inv.Terms" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td class="style8">
                                                    <asp:Label ID="Label5" runat="server" Text="Currency" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td class="style9">
                                                    <asp:Label ID="Label6" runat="server" Text="Exchange Rate" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td class="style9">
                                                    <asp:Label ID="Label8" runat="server" Text="Invoice Value" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td colspan="1" class="style9">
                                                    <asp:Label ID="Label9" runat="server" Text="Product Value" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td class="style9">
                                                    <asp:Label ID="Label46" runat="server" CssClass="fontsize" Text="INR"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnNew" runat="server" BackColor="#73AAE8" OnClick="btnNew_Click"
                                                        Text="New" OnClientClick="javascript:return NewInvoice();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdcolumn100">
                                                    <asp:TextBox ID="txtinvno" runat="server" CssClass="textbox100"></asp:TextBox>
                                                </td>
                                                <td class="tdcolumn75">
                                                    <asp:TextBox ID="txtdate" runat="server" CssClass="textbox75" Width="75px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="tdcolumn75">
                                                    <asp:DropDownList ID="ddlTermsofInvoice" runat="server" AppendDataBoundItems="True"
                                                        CssClass="ddl75">
                                                        <asp:ListItem Text="~Select~" Value="~Select~">~Select~</asp:ListItem>
                                                        <asp:ListItem Text="FOB" Value="FOB">FOB</asp:ListItem>
                                                        <asp:ListItem Text="CIF" Value="CIF">CIF</asp:ListItem>
                                                        <asp:ListItem Text="C&amp;F" Value="C&amp;F">C&amp;F</asp:ListItem>
                                                        <asp:ListItem Text="C&amp;I" Value="C&amp;I">C&amp;I</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="tdcolumn75">
                                                    <asp:DropDownList ID="ddlInvoiceCurrency" runat="server" AppendDataBoundItems="True"
                                                        CssClass="ddl75" onchange="javascript:return callddlInvoiceCurrency();">
                                                        <asp:ListItem Selected="True" Value="~Select~">~Select~</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:TextBox ID="txtexcrate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:TextBox ID="txtinvval" runat="server" CssClass="textbox100" onchange="ExpProductValueINR();">0</asp:TextBox>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:TextBox ID="txtprodval" runat="server" CssClass="textbox100">0</asp:TextBox>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:TextBox ID="txtAmountinINR" runat="server" CssClass="textbox100">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAdd" runat="server" BackColor="#73AAE8" Height="26px" Text="Add"
                                                        Width="50px" OnClick="btnAdd_Click" />
                                                    <asp:Button ID="btnUpdate" runat="server" BackColor="#73AAE8" Height="26px" Text="Update"
                                                        Width="50px" OnClick="btnUpdate_Click" OnClientClick="javascript:return UpdateValidate();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9">
                                        <%--  <div class="grid_scroll-2">--%>
                                        <asp:GridView ID="gvInvoiceExport" runat="server" Width="845px" CssClass="table-wrapperInv"
                                            AutoGenerateColumns="False" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="calibri" Font-Size="10pt" ForeColor="Black" GridLines="Vertical"
                                            ShowFooter="false" ShowHeader="true" Style="text-align: center; font-size: 9pt;"
                                            OnSelectedIndexChanged="gvInvoiceExport_SelectedIndexChanged">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" />
                                                <%--<asp:BoundField HeaderText="Sr.No" DataField="Sr.No"></asp:BoundField>--%>
                                                <asp:BoundField HeaderText="Invoice Number" DataField="InvoiceNo" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Date" DataField="InvoiceDate" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Inv.Terms" DataField="TOI" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Currency" DataField="Currency" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Exchange Rate" DataField="CurrencyRate" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Invoice Value" DataField="InvoiceValue" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Product Value" DataField="ProductValue" HeaderStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Amount INR" DataField="InvoiceINRAmount" HeaderStyle-HorizontalAlign="Center">
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
                                        <%-- </div>--%>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="PanelFreight" runat="server" Visible="False">
                                <table width="800px">
                                    <tr>
                                        <td align="center" colspan="9">
                                            <asp:Label ID="Label10" runat="server" Style="font-weight: 700" Text="Freight Insurance &amp; Other Charges"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server" CssClass="fontsize" Text="Unit Price Includes"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlUnitprice" runat="server" CssClass="ddl150">
                                                            <asp:ListItem Text="None" Value="None" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Freight" Value="Freight"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="Label12" runat="server" CssClass="fontsize" Text="Show FOB in"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlshoefob" runat="server" CssClass="ddl150" AppendDataBoundItems="True">
                                                            <asp:ListItem Text="~Select~" Value="CIF">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label13" runat="server" CssClass="fontsize" Text="Currecy"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="Exchange Rate"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label15" runat="server" CssClass="fontsize" Text="Rate"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label16" runat="server" CssClass="fontsize" Text="Amount"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label47" runat="server" CssClass="fontsize" Text="Amount in INR"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label17" runat="server" CssClass="fontsize" Text="Freight"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlFreight" runat="server" AppendDataBoundItems="True" CssClass="ddl75"
                                                            onchange="javascript:return callddlFreightDetails();">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style2">
                                                        <asp:TextBox ID="txtfreight1" runat="server" CssClass="textbox75">0</asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtfreight2" runat="server" CssClass="textbox100" onblur="CIFAmtCalculation('ContentPlaceHolder1_txtfreight1','ContentPlaceHolder1_txtfreight2','ContentPlaceHolder1_txtAmountinINR','ContentPlaceHolder1_txtfreighamount','ContentPlaceHolder1_txtfreighamountINR');"
                                                            OnKeyPress="javascript:return isFloat(event);">0.00</asp:TextBox>
                                                        <asp:Label ID="Label18" runat="server" Text="%"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtfreighamount" runat="server" CssClass="textbox100" onblur="CIFRateCalculation('ContentPlaceHolder1_txtfreight1','ContentPlaceHolder1_txtfreighamount','ContentPlaceHolder1_txtAmountinINR','ContentPlaceHolder1_txtfreight2','ContentPlaceHolder1_txtfreighamountINR');"
                                                            OnKeyPress="javascript:return isFloat(event);">0.00</asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtfreighamountINR" runat="server" CssClass="textbox100">0.00</asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label19" runat="server" CssClass="fontsize" Text="Insurance"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlInsurace" runat="server" CssClass="ddl75" AppendDataBoundItems="True"
                                                            onchange="javascript:return callddlInsurance();">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style2">
                                                        <asp:TextBox ID="txtinsurance1" runat="server" CssClass="textbox75">0</asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtinsurerate" runat="server" CssClass="textbox100" onblur="CIFAmtCalculation('ContentPlaceHolder1_txtinsurance1','ContentPlaceHolder1_txtinsurerate','ContentPlaceHolder1_txtAmountinINR','ContentPlaceHolder1_txtinsureamount','ContentPlaceHolder1_txtinsureamountINR');"
                                                            OnKeyPress="javascript:return isFloat(event);">0.00</asp:TextBox>
                                                        <asp:Label ID="Label20" runat="server" Text="%"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtinsureamount" runat="server" CssClass="textbox100" onblur="CIFRateCalculation('ContentPlaceHolder1_txtinsurance1','ContentPlaceHolder1_txtinsureamount','ContentPlaceHolder1_txtAmountinINR','ContentPlaceHolder1_txtinsurerate','ContentPlaceHolder1_txtinsureamountINR');"
                                                            OnKeyPress="javascript:return isFloat(event);">0.00</asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtinsureamountINR" runat="server" CssClass="textbox100">0.00</asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label21" runat="server" CssClass="fontsize" Text="Discount"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDiscountcurr" runat="server" CssClass="ddl75" AppendDataBoundItems="True"
                                                            onchange="javascript:return callddlDiscount();">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style2">
                                                        <asp:TextBox ID="txtDiscExcRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtDiscRate" runat="server" CssClass="textbox100" onblur="CIFAmtCalculation('ContentPlaceHolder1_txtDiscExcRate','ContentPlaceHolder1_txtDiscRate','ContentPlaceHolder1_txtAmountinINR','ContentPlaceHolder1_txtDiscAmount','ContentPlaceHolder1_txtDiscAmountINR');"
                                                            OnKeyPress="javascript:return isFloat(event);">0.00</asp:TextBox>
                                                        <asp:Label ID="Label22" runat="server" Text="%"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDiscAmount" runat="server" CssClass="textbox100" onblur="CIFRateCalculation('ContentPlaceHolder1_txtDiscExcRate','ContentPlaceHolder1_txtDiscAmount','ContentPlaceHolder1_txtAmountinINR','ContentPlaceHolder1_txtDiscRate','ContentPlaceHolder1_txtDiscAmountINR');"
                                                            OnKeyPress="javascript:return isFloat(event);">0.00</asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDiscAmountINR" runat="server" CssClass="textbox100">0.00</asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label23" runat="server" CssClass="fontsize" Text="Commision"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCommCurr" runat="server" CssClass="ddl75" AppendDataBoundItems="True"
                                                            onchange="javascript:return callddlcommision();">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style2">
                                                        <asp:TextBox ID="txtCommExcRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtCommRate" runat="server" CssClass="textbox100" onblur="CIFAmtCalculation('ContentPlaceHolder1_txtCommExcRate','ContentPlaceHolder1_txtCommRate','ContentPlaceHolder1_txtAmountinINR','ContentPlaceHolder1_txtCommAmount','ContentPlaceHolder1_txtCommAmountINR');">0.00</asp:TextBox>
                                                        <asp:Label ID="Label24" runat="server" Text="%"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCommAmount" runat="server" CssClass="textbox100" onblur="CIFRateCalculation('ContentPlaceHolder1_txtCommExcRate','ContentPlaceHolder1_txtCommAmount','ContentPlaceHolder1_txtAmountinINR','ContentPlaceHolder1_txtCommRate','ContentPlaceHolder1_txtCommAmountINR');">0.00</asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCommAmountINR" runat="server" CssClass="textbox100">0.00</asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label25" runat="server" CssClass="fontsize" Text="Other Deduction"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlOthDedcurr" runat="server" CssClass="ddl75" AppendDataBoundItems="True"
                                                            onchange="javascript:return callddlothdeduct();">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style2">
                                                        <asp:TextBox ID="txtOthDedExcRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtOthDedRate" runat="server" CssClass="textbox100" onblur="CIFAmtCalculation('ContentPlaceHolder1_txtOthDedExcRate','ContentPlaceHolder1_txtOthDedRate','ContentPlaceHolder1_txtAmountinINR','ContentPlaceHolder1_txtOthDedAmount','ContentPlaceHolder1_txtOthDedAmountINR');">0.00</asp:TextBox>
                                                        <asp:Label ID="Label26" runat="server" Text="%"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtOthDedAmount" runat="server" CssClass="textbox100" onblur="CIFRateCalculation('ContentPlaceHolder1_txtOthDedExcRate','ContentPlaceHolder1_txtOthDedAmount','ContentPlaceHolder1_txtAmountinINR','ContentPlaceHolder1_txtOthDedRate','ContentPlaceHolder1_txtOthDedAmountINR');">0.00</asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtOthDedAmountINR" runat="server" CssClass="textbox100">0.00</asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label27" runat="server" CssClass="fontsize" Text="Packing FOB Charges"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPackFobCurr" runat="server" CssClass="ddl75" AppendDataBoundItems="True"
                                                            onchange="javascript:return callddlfob();">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style2">
                                                        <asp:TextBox ID="txtPackFobExcRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtPacFobRate" runat="server" CssClass="textbox100"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="5">
                                                        <asp:Button ID="btnSave4" runat="server" Text="Save" CssClass="stylebutton" OnClick="btnSave4_Click"
                                                            OnClientClick="javascript:return freightvalidate();" />
                                                        <asp:Button ID="btnCancel4" runat="server" CssClass="stylebutton" Text="Cancel" />
                                                    </td>
                                                    <td align="center">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
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
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="PanelOtherInfo" runat="server" Visible="False">
                                <table width="800px">
                                    <tr>
                                        <td align="center" colspan="9">
                                            <asp:Label ID="Label37" runat="server" Style="font-weight: 700" Text="Other Info"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label38" runat="server" CssClass="fontsize" Text="Export Contract No/Dt"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExpContNoDt" runat="server" CssClass="textbox150" OnTextChanged="txtExpContNoDt_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExpContNoDt1" runat="server" CssClass="textbox150"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtExpContNoDt1">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label41" runat="server" CssClass="fontsize" Text="Nature Of Payment"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNatureOfPaym" runat="server" CssClass="textbox150"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label42" runat="server" CssClass="fontsize" Text="Payment Period"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPaymPerd" runat="server" CssClass="textbox150"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label45" runat="server" CssClass="fontsize" Text="Days"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnSave5" runat="server" CssClass="stylebutton" Text="Save" OnClick="btnSave5_Click"
                                                    OnClientClick="javascript:return freightvalidate();" />
                                                <asp:Button ID="btnCanc" runat="server" CssClass="stylebutton" Text="Cancel" />
                                            </td>
                                        </tr>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="PanelAnnexure" runat="server" Visible="False">
                                <table width="800px">
                                    <tr>
                                        <td align="center" colspan="6">
                                            <asp:Label ID="Label33" runat="server" Style="font-weight: 700" Text="Annexure C1 Details"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="fontsize" Text="IE Code of EDU"></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtEDUCode" runat="server" CssClass="textbox150"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" CssClass="fontsize" Text="Branch Sl No"></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtBranchSlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label28" runat="server" CssClass="fontsize" Text="Examination Date"></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtExaminationDate" runat="server" CssClass="textbox75"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" TargetControlID="txtExaminationDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label29" runat="server" CssClass="fontsize" Text="Examining Officier"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtExaminingOfficier" runat="server" CssClass="textbox150"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label30" runat="server" CssClass="fontsize" Text="Designation"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtExamineDesignation" runat="server" CssClass="textbox150"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label31" runat="server" CssClass="fontsize" Text="Supervising Officier"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSupervisingOfficier" runat="server" CssClass="textbox150"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label32" runat="server" CssClass="fontsize" Text="Designation"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtSupervisingDesgn" runat="server" CssClass="textbox150"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label34" runat="server" CssClass="fontsize" Text="Commissionerate"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCommissionerate" runat="server" CssClass="textbox150"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label35" runat="server" CssClass="fontsize" Text="Division"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDivision" runat="server" CssClass="textbox150"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label36" runat="server" CssClass="fontsize" Text="Range"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRange" runat="server" CssClass="textbox150"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:CheckBox ID="chkExaminer" runat="server" Text="Verified Examiner Officer" CssClass="fontsize"/>
                                        </td>
                                        <td colspan="3">
                                            <asp:CheckBox ID="chkSample" runat="server" Text="Sample Forwarded" CssClass="fontsize"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label39" runat="server" CssClass="fontsize" Text="Seal Number"></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtSealNumber" runat="server" CssClass="textbox150"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="6">
                                            <asp:Button ID="btnAnnexureSave" runat="server" Text="Save" CssClass="stylebutton"
                                                OnClick="btnAnnexureSave_Click" />
                                            <asp:Button ID="btnAnnexureCancel" runat="server" CssClass="stylebutton" 
                                                Text="Cancel" onclick="btnAnnexureCancel_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="6">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                    <td class="left" rowspan="2" valign="top">
                        <table border="0.5">
                            <tr>
                                <td colspan="2" align="center">
                                    &nbsp; &nbsp; &nbsp; Job Details
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    Job No
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlJobNo" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        CssClass="ddl100" Height="20px" Width="130px" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged">
                                        <asp:ListItem>~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    Job Date
                                </td>
                                <td>
                                    <asp:Label ID="lblJobDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    Currency
                                </td>
                                <td>
                                    <asp:Label ID="lblCurrency" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    ExRate
                                </td>
                                <td>
                                    <asp:Label ID="lblExRate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    Inv No
                                </td>
                                <td>
                                    <asp:Label ID="lblInvNo" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    Inv Value
                                </td>
                                <td>
                                    <asp:Label ID="lblInvValue" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    Mode
                                </td>
                                <td>
                                    <asp:Label ID="lblMode" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    Custom
                                </td>
                                <td>
                                    <asp:Label ID="lblCustom" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    Doc Flling Status
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <%--                            <tr>
                                <td class="style7">
                                    SB No
                                </td>
                                <td>
                                    <asp:Label ID="lblSBNo" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>--%>
                            <%-- <tr>
                                <td class="style7">
                                    SB Date
                                </td>
                                <td>
                                    <asp:Label ID="lblSBDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="style7">
                                    Total Invoice
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalInvoice" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblmsg" runat="server" Style="font-weight: 700" CssClass="fontsize"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnReturn" runat="server" Text="Back To Shipment" CssClass="stylebutton"
                                        OnClick="btnReturn_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <input type="hidden" id="hdnInvoice" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
