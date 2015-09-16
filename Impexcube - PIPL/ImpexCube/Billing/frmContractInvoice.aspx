<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="frmContractInvoice" CodeBehind="frmContractInvoice.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="frmContractInvoice" Codebehind="frmContractInvoice.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<%--<html xmlns="http://www.w3.org/1999/xhtml">--%>
<%--<head runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <title>::PIPL || INVOICE</title>
    <style type="text/css">
        .accordionContent
        {
            width: 100%;
        }
        .waterText
        {
            font-family: Arial;
            font-size: 8pt;
            color: Fuchsia;
            overflow: auto;
            background-color: #ffff00;
        }
        .accordionHeaderSelected
        {
            color: red;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 9px;
            font-weight: bold;
            margin-top: 1px;
            padding: 1px;
            width: 100%;
        }
        .accordionHeader
        {
            color: blue;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 9px;
            font-weight: normal;
            margin-top: 1px;
            padding: 1px;
            width: 100%;
        }
        .href
        {
            color: green;
            font-weight: bold;
            text-decoration: none;
        }
    </style>
    <style type="text/css">
        .grid_scroll
        {
            overflow: auto;
            height: 230px;
            width: 200px;
        }
        .button_image1
        {
            font-family: verdana;
            font-size: 11px;
            color: #ffffff;
            background-color: #013388;
            background-image: url(  '../images/bg_homepage_right.gif' );
            border: 1px solid #ffffff;
        }
    </style>
    <style type="text/css">
        body
        {
            font-family: verdana;
            font-size: 10px;
        }
        .waterFore
        {
            color: red;
            font-family: verdana;
            font-size: 8px;
        }
        .completionList
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
            font-family: verdana;
            font-size: 10px;
            background-color: white;
        }
        .listItem
        {
            color: #666666;
            background-color: white;
            font-family: verdana;
            font-size: 10px;
        }
        .itemHighlighted
        {
            background-color: #ffc0c0;
        }
        .style4
        {
            width: 50px;
        }
        .style5
        {
            width: 117px;
        }
        .style6
        {
            width: 196px;
        }
        .style7
        {
            width: 150px;
        }
        .style8
        {
            width: 160px;
        }
    </style>
    <script type="text/javascript">
        function InvoiceValue() {
            var i;
            var sum = "";
            var staxs = "";
            var Ecess = "";
            var Scess = "";
            var AllTax = "";
            var tot = "";
            var less = "";
            var NetTotal = "";

            var v1 = parseInt(document.getElementById('GridView1_amt1').value);
            document.getElementById('GridView1_amt1').value = v1.toFixed(2);

            sum = v1.value;

            document.getElementById('SubTotal').value = sum.toFixed(2);

            var StrSTax = parseFloat(document.getElementById('ServiceTax').value);

            sum = parseInt(document.getElementById('SubTotal').value);
            var strSum = Math.round(sum);
            document.getElementById('SubTotal').value = strSum.toFixed(2);

            staxs = strSum * (StrSTax / 100);
            Ecess = staxs * 0.02;
            Scess = staxs * 0.01;


            var ssTax = parseFloat(staxs);

            var strssTax = Math.round(ssTax);
            document.getElementById('sTax').value = strssTax.toFixed(2);

            var strEcess = Math.round(Ecess);
            var strScess = Math.round(Scess);
            document.getElementById('edcess').value = strEcess.toFixed(2);
            document.getElementById('shcess').value = strScess.toFixed(2);

            AllTax = strssTax + strEcess + strScess;

            tot = strSum + AllTax;

            var strTot = Math.round(tot);
            document.getElementById('Totals').value = tot.toFixed(2);
            less = parseInt(document.getElementById('LessAd').value);
            document.getElementById('LessAd').value = less.toFixed(2);

            NetTotal = tot - less;
            var strNetTotal = Math.round(NetTotal);
            document.getElementById('balance1').value = Math.round(NetTotal);
            document.getElementById('BalanceDue').value = strNetTotal.toFixed(2);

            var strAmount = document.getElementById('balance1').value;
        }
        function Totalcalculate() {
            var v1 = parseInt(document.getElementById('GridView1_amt1').value);
            document.getElementById('GridView1_amt1').value = v1.toFixed(2);

            var sum = v1.value;

            document.getElementById('SubTotal').value = sum.toFixed(2);

        }

        function CallServiceTax() {

            var i;
            var sum = "";
            var staxs = "";
            var Ecess = "";
            var Scess = "";
            var AllTax = "";
            var tot = "";
            var less = "";
            var NetTotal = "";

            var StrSTax = parseFloat(document.getElementById('ServiceTax').value);

            sum = parseInt(document.getElementById('SubTotal').value);
            var strSum = Math.round(sum);
            document.getElementById('SubTotal').value = strSum.toFixed(2);

            staxs = strSum * (StrSTax / 100);
            Ecess = staxs * 0.02;
            Scess = staxs * 0.01;


            var ssTax = parseFloat(staxs);

            var strssTax = Math.round(ssTax);
            document.getElementById('sTax').value = strssTax.toFixed(2);

            var strEcess = Math.round(Ecess);
            var strScess = Math.round(Scess);
            document.getElementById('edcess').value = strEcess.toFixed(2);
            document.getElementById('shcess').value = strScess.toFixed(2);

            AllTax = strssTax + strEcess + strScess;

            tot = strSum + AllTax;

            var strTot = Math.round(tot);
            document.getElementById('Totals').value = tot.toFixed(2);
            less = parseInt(document.getElementById('LessAd').value);
            document.getElementById('LessAd').value = less.toFixed(2);

            NetTotal = tot - less;
            var strNetTotal = Math.round(NetTotal);
            document.getElementById('balance1').value = Math.round(NetTotal);
            document.getElementById('BalanceDue').value = strNetTotal.toFixed(2);

            var strAmount = document.getElementById('balance1').value;
        }

    </script>
    <script type="text/javascript">
        function LessADvance() {
            var tot = parseInt(document.getElementById('Totals').value);
            var less = parseInt(document.getElementById('LessAd').value);
            document.getElementById('LessAd').value = less.toFixed(2);

            var NetTotal = tot - less;
            var strNetTotal = Math.round(NetTotal);
            document.getElementById('balance1').value = Math.round(NetTotal);
            document.getElementById('BalanceDue').value = strNetTotal.toFixed(2);

            var strAmount = document.getElementById('balance1').value;
        }
    </script>
    <script type="text/javascript">
        function RestrictInt(val) {
            if (isNaN(val)) {
                val = val.substring(0, val.length - 1);
                document.form1.txtPhone.value = val;
                return false;
            }
            return true;
        }
    </script>
    <%--<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="frmContractInvoice" Codebehind="frmContractInvoice.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>    <%--<html xmlns="http://www.w3.org/1999/xhtml">--%>
    <div style="vertical-align: top;">
        <br />
        <table style="width: 100%; height: 400px; border: 1px;">
            <tr>
                <td style="height: 20%">
                    <table style="width: 100%;">
                        <tr>
                            <td style="height: 90%; vertical-align: top; background-color: white; width: 975px;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="left">
                                            <table id="tblINV" runat="server" style="border-color: #2461BF; border-style: solid;
                                                border-width: 1px;">
                                                <tr>
                                                    <td style="vertical-align: top;">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td align="left">
                                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                                        <tr>
                                                                            <td align="left" class="style4">
                                                                                <asp:Label ID="Label3" runat="server" Text="Bill Type :" Font-Names="Arial" Font-Size="8pt"
                                                                                    Width="50px" Visible="False"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:RadioButtonList ID="rbInvoice" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbInvoice_SelectedIndexChanged">
                                                                                    <asp:ListItem Value="SB">Invoice</asp:ListItem>
                                                                                    <asp:ListItem Value="DB">Debit Note</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td align="left">
                                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="center" style="width: 610px; height: 16px">
                                                                                    <asp:Label ID="lblIName" runat="server" Font-Names="arial" Font-Size="10pt" Font-Bold="True">CONTRACT 
                                                                                        INVOICE - IMPORTS</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                                        <tbody>
                                                                            <tr style="border-style: solid; border-width: 1px; border-color: #2461BF;">
                                                                                <td id="a1">
                                                                                    <asp:Label ID="lblINumber" runat="server" Text="INV. NO." Font-Names="Arial" Font-Size="8pt"
                                                                                        Font-Bold="True"></asp:Label>
                                                                                    <asp:Label ID="lblBill" runat="server" Width="120px" Font-Bold="True" Font-Names="Arial"
                                                                                        Font-Size="7pt"></asp:Label>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label6" runat="server" Text="Suffix " Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtSuffix" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 198px; height: 17px;">
                                                                                    <asp:Label ID="Label42" runat="server" Text="Date :" Font-Names="Arial" Font-Size="8pt"
                                                                                        Font-Bold="True"></asp:Label>
                                                                                    <asp:TextBox ID="invDate" runat="server" Width="80px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:Label ID="Label23" runat="server" Text="Job No" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                                                    <asp:CheckBox ID="chk" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt"
                                                                                        OnCheckedChanged="chk_CheckedChanged" />
                                                                                </td>
                                                                                <td align="left" style="height: 26px;">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtJNO" Font-Names="Arial" Font-Size="8pt" runat="server" OnTextChanged="txtJNO_TextChanged"
                                                                                                    Width="70px"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" EnableCaching="true"
                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetJobNo" CompletionListCssClass="completionList"
                                                                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtJNO">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtJNO"
                                                                                                    WatermarkCssClass="waterText" WatermarkText="75079" runat="server">
                                                                                                </cc1:TextBoxWatermarkExtender>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Button ID="BtnStandard" runat="server" Text="Go" Font-Size="8pt" OnClick="BtnStandard_Click"
                                                                                                    Width="41px" CssClass="button_image1" Height="25px" />
                                                                                            </td>
                                                                                            <td class="style8">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td align="left">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left" style="vertical-align: top;" class="style5">
                                                                                    <asp:Label ID="Label22" runat="server" Text="S.No" Font-Size="8pt" Font-Names="Arial"
                                                                                        Font-Bold="False"></asp:Label>
                                                                                </td>
                                                                                <td style="vertical-align: top;" align="center" class="style6">
                                                                                    <asp:Label ID="Label1" runat="server" Text="PARTICULARS" Font-Size="8pt" Font-Names="Arial"
                                                                                        Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td align="center" style="vertical-align: top;">
                                                                                    <asp:Label ID="Label24" runat="server" Text="RECEIPT DETAILS" Font-Size="8pt" Font-Names="Arial"
                                                                                        Font-Bold="False"></asp:Label>
                                                                                </td>
                                                                                <td align="center" style="vertical-align: top;">
                                                                                    <asp:Label ID="Label5" runat="server" Text="TAX" Font-Size="8pt" Font-Names="Arial"
                                                                                        Font-Bold="False"></asp:Label>
                                                                                </td>
                                                                                <td align="center" style="vertical-align: top;">
                                                                                    <asp:Label ID="Label7" runat="server" Text="AMOUNT Rs." Font-Size="8pt" Font-Names="Arial"
                                                                                        Font-Bold="False"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <ContentTemplate>
                                                                            <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                                                <tr>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div style="width: 525px; height: 245px" class="grid_scroll">
                                                                                            <asp:GridView ID="GridView1" runat="server" CellPadding="3" Font-Names="Arial" Font-Size="8pt"
                                                                                                BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1"
                                                                                                GridLines="None" AutoGenerateColumns="False" Width="520px" ShowHeader="false"
                                                                                                OnRowDataBound="GridView1_RowDataBound">
                                                                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                                                                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                                                                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                                                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                                                <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="ID" SortExpression="sno">
                                                                                                        <ItemStyle ForeColor="#DEDFDE" Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:TemplateField HeaderText="S.No">
                                                                                                        <ItemTemplate>
                                                                                                            <%# Container.DataItemIndex + 1 %>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                                                                                        <ItemStyle Wrap="False" Width="250px" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:TemplateField HeaderText="RECEIPT">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:TextBox ID="txtRECPT" runat="server" Width="100px" Style="text-align: left"></asp:TextBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="S.TAX">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkSTAX" runat="server" Font-Names="arial" Font-Size="8pt" AutoPostBack="false" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Amount(Rs.)">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:TextBox ID="amt1" runat="server" Width="80px" AutoPostBack="false" Style="text-align: right">0</asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FTE1" TargetControlID="amt1" FilterType="Custom"
                                                                                                                ValidChars="0123456789." runat="server">
                                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label47" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                                                Text="Taxable Amount"></asp:Label>
                                                                                        </td>
                                                                                        <td class="style32">
                                                                                            <asp:TextBox ID="SubTotalTax" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                                                Font-Size="8pt" Style="text-align: right" Width="60px" BackColor="#FFD2D2">0</asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                                                Text="Non-Taxable Amount" Width="95px"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="SubTotal" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                                                Width="60px">0</asp:TextBox>
                                                                                        </td>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="lblSTAX0" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                                                Text="Total"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Totals" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                                                Width="70px" BackColor="#FFFFCC">0</asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td align="right">
                                                                                                        <asp:Label ID="lblSTAX" runat="server" Width="60px" Font-Bold="True" Font-Names="Arial"
                                                                                                            Font-Size="7pt" Text="Service Tax@"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="left">
                                                                                                        <asp:DropDownList ID="drServiceTax" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                                            Width="50px" AutoPostBack="false">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td class="style32">
                                                                                            <asp:TextBox ID="sTax" runat="server" Style="text-align: right" Width="60px" Font-Names="Arial"
                                                                                                Font-Size="8pt" BackColor="#FFD2D2">0</asp:TextBox>&nbsp;
                                                                                        </td>
                                                                                        <td colspan="2">
                                                                                            <asp:Button ID="btncalculate" runat="server" Height="25px" OnClick="btncalculate_Click"
                                                                                                Text="Calculate" Width="120px" />
                                                                                            <asp:TextBox ID="balance1" runat="server" BackColor="White" BorderStyle="Solid" ForeColor="White"
                                                                                                Width="67px">0</asp:TextBox>
                                                                                        </td>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label48" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                                                Text="Less Advance" Width="65px"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="LessAd" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt"
                                                                                                onblur="LessADvance()" OnTextChanged="LessAd_TextChanged" Style="text-align: right"
                                                                                                Width="70px" BackColor="#FFFFCC">0</asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label30" runat="server" Text="EDU Cess@2.00%" Font-Names="Arial" Font-Size="7pt"
                                                                                                Font-Bold="True"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="EdCess" runat="server" Width="60px" Font-Names="Arial" Font-Size="8pt"
                                                                                                Style="text-align: right" BackColor="#FFD2D2">0</asp:TextBox>
                                                                                        </td>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label31" runat="server" Text="SHE Cess @1.00%" Font-Names="Arial"
                                                                                                Font-Size="7pt" Font-Bold="True"></asp:Label>
                                                                                        </td>
                                                                                        <td style="width: 93px">
                                                                                            <asp:TextBox ID="SHCess" runat="server" Width="60px" Font-Names="Arial" Font-Size="8pt"
                                                                                                Style="text-align: right">0</asp:TextBox>
                                                                                        </td>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label49" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                                                Text="Balance Due"></asp:Label>
                                                                                        </td>
                                                                                        <td style="vertical-align: top;">
                                                                                            <asp:TextBox ID="BalanceDue" runat="server" Width="70px" Font-Names="Arial" Font-Size="8pt"
                                                                                                Style="text-align: right; margin-left: 0px;" BackColor="#FFFFCC">0</asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                            <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="vertical-align: top;" class="style28">
                                                                                            <asp:Label ID="txtRupees" runat="server" Font-Names="Arial" Font-Size="8pt" Height="23px"
                                                                                                Width="500px"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="border: 1px solid #2461BF; width: 434px; vertical-align: top;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel1" runat="server" Font-Names="Arial" BackColor="#DEDFDE" Font-Size="7pt"
                                                                        GroupingText="Customer Info" Width="400px" ForeColor="#000040">
                                                                        <table>
                                                                            <tr>
                                                                                <td align="right" class="style7">
                                                                                    <asp:Label ID="Label8" runat="server" Text="Party Name" Font-Names="Arial" Font-Size="8pt"
                                                                                        Width="85px" ForeColor="Black"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtCompName" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"
                                                                                        Height="20px"></asp:TextBox>&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" class="style7">
                                                                                    <asp:Label ID="Label9" runat="server" Text="Sub Party" Font-Names="Arial" Font-Size="8pt"
                                                                                        Width="61px" ForeColor="Black"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtSubParty" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"
                                                                                        AutoPostBack="True" OnTextChanged="txtSubParty_TextChanged"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" class="style7">
                                                                                    <asp:Label ID="Label50" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Text="Tally Account Name" Width="100px"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="ddlTallyAccountName" runat="server" AppendDataBoundItems="True"
                                                                                        Font-Names="Arial" Font-Size="8pt" Width="200px">
                                                                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" class="style7">
                                                                                    <asp:Label ID="Label51" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Text="Tally Sub Party Name" Width="120px"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="ddlTallySubPartyName" runat="server" AppendDataBoundItems="True"
                                                                                        Enabled="False" Font-Names="Arial" Font-Size="8pt" Width="200px">
                                                                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="vertical-align: top;">
                                                                                    <asp:Label ID="Label10" runat="server" Text="Address" Font-Names="Arial" Font-Size="8pt"
                                                                                        ForeColor="Black"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtAdd1" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"
                                                                                        Height="40px" TextMode="MultiLine"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" class="style7">
                                                                                    <asp:Label ID="Label11" runat="server" Text="City" Font-Names="Arial" Font-Size="8pt"
                                                                                        ForeColor="Black"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtCity" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" class="style7">
                                                                                    <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Text="Party Ref" Width="62px"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtParty_Reff" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                        Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <br />
                                                                    <br />
                                                                    <asp:Panel ID="Panel3" runat="server" BackColor="#DEDFDE" Font-Names="Arial" Font-Size="7pt"
                                                                        ForeColor="#000040" GroupingText="Job Info" Width="400px">
                                                                        <table style="width: 362px">
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Text="Job No."></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtJobNo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Height="16px" Text="AWB / BL No." Width="91px"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtBLNo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Text="BE NO./DT."></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtBENo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Text="Ass. Value"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtAssValue" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label27" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Text="Custom Duty"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtCustomDuty" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                        Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Text="No.of Container"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="height: 20px; width: 138px;">
                                                                                    <asp:TextBox ID="txtNCNTR" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Text="Quantity"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtQty" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="vertical-align: top;">
                                                                                    <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Text="Item Imported"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtImpotItem" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                        Height="40px" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="vertical-align: top;">
                                                                                    <asp:Label ID="Label46" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Text="Note : -"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" Height="40px"
                                                                                        TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="vertical-align: top;">
                                                                                    <asp:Label ID="Label52" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                        Text="Supplier Inv No"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:CheckBox ID="chkSupplierInvNo" runat="server" OnCheckedChanged="chkSupplierInvNo_CheckedChanged"
                                                                                        AutoPostBack="True" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border: 1px solid #2461BF;">
                                                        <cc1:Accordion ID="Accordion1" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                            ContentCssClass="accordionContent" runat="server" SelectedIndex="0" FadeTransitions="true"
                                                            SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40"
                                                            RequireOpenedPane="false" AutoSize="None">
                                                            <Panes>
                                                                <cc1:AccordionPane ID="AccPan1" runat="server">
                                                                    <Header>
                                                                        Importer Remarks</Header>
                                                                    <Content>
                                                                        <asp:TextBox ID="txtimpRemark" Font-Names="Arial" Font-Size="8pt" Width="500px" runat="server"></asp:TextBox>
                                                                    </Content>
                                                                </cc1:AccordionPane>
                                                                <cc1:AccordionPane ID="AccPan2" runat="server">
                                                                    <Header>
                                                                        Indent Remarks</Header>
                                                                    <Content>
                                                                        <asp:TextBox ID="txtIndentRemark" Font-Names="Arial" Font-Size="8pt" Width="500px"
                                                                            runat="server"></asp:TextBox>
                                                                    </Content>
                                                                </cc1:AccordionPane>
                                                            </Panes>
                                                        </cc1:Accordion>
                                                    </td>
                                                    <td style="border: 1px solid #2461BF; vertical-align: top;" align="right">
                                                        <asp:Label ID="lblResult" runat="server"></asp:Label>
                                                        <asp:Button ID="Submit" runat="server" Font-Names="Arial" Font-Size="8pt" Height="25px"
                                                            OnClick="Submit_Click" Text="Submit" Width="80px" CssClass="button_image1" />
                                                        <asp:Button ID="preview" runat="server" Font-Names="Arial" Font-Size="8pt" Height="25px"
                                                            OnClick="preview_Click" Text="Print Preview" Width="80px" CssClass="button_image1" />
                                                        <asp:Button ID="btnMail" runat="server" Font-Names="Arial" Font-Size="8pt" Height="25px"
                                                            OnClick="btnMail_Click" Text="Mail" Width="50px" CssClass="button_image1" 
                                                            Visible="False" />
                                                        <asp:Button ID="ExportTally" runat="server" Text="Export Tally" Font-Names="arial"
                                                            Font-Size="8pt" Width="70px" CssClass="button_image1" Height="25px" OnClick="ExportTally_Click" />
                                                        <asp:Button ID="BtnExit" runat="server" Font-Names="Arial" CssClass="button_image1"
                                                            Font-Size="8pt" Height="25px" Text="Exit" Width="50px" PostBackUrl="~/index.aspx"
                                                            OnClick="BtnExit_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblContr" runat="server">
                                                <tr>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel2" runat="server" Height="30px" BackColor="#2461bf" Width="700px">
                                                                        <asp:Label ID="lblContr" runat="server" ForeColor="White" Font-Size="9pt" Font-Names="Arial"
                                                                            BackColor="#2461BF" Width="400px"></asp:Label>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top;">
                                                                    <table style="border: solid 1px #2461bf; width: 700px;">
                                                                        <tr id="trBill" runat="server">
                                                                            <td align="center" style="vertical-align: top;">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label25" runat="server" Text="Billing Address :" Font-Names="Arial"
                                                                                                Font-Size="8pt"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:RadioButtonList ID="rbBill" runat="server" Font-Size="8pt" Font-Names="Arial"
                                                                                                        Width="159px" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbBill_SelectedIndexChanged">
                                                                                                        <asp:ListItem Value="DP">Direct Party</asp:ListItem>
                                                                                                        <asp:ListItem Value="TP">Third Party</asp:ListItem>
                                                                                                    </asp:RadioButtonList>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" style="vertical-align: top;">
                                                                                <center style="margin-left: 40px">
                                                                                    <asp:GridView ID="GridView2" runat="server" Width="336px" Font-Size="8pt" Font-Names="Arial"
                                                                                        CellPadding="4" AutoGenerateColumns="False" DataKeyNames="QuoteNo" OnSelectedIndexChanged="GridView2_SelectedIndexChanged"
                                                                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                                                                        ForeColor="Black" GridLines="Vertical">
                                                                                        <FooterStyle BackColor="#CCCC99" />
                                                                                        <RowStyle Font-Names="Arial" Font-Size="8pt" BackColor="#F7F7DE" />
                                                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" Font-Names="Arial"
                                                                                            Font-Size="8pt" />
                                                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" Font-Names="Arial"
                                                                                            Font-Size="8pt" />
                                                                                        <Columns>
                                                                                            <asp:CommandField ShowSelectButton="True" ButtonType="Button">
                                                                                                <ControlStyle Font-Names="Arial" Font-Size="8pt" />
                                                                                            </asp:CommandField>
                                                                                            <asp:BoundField DataField="QuoteNo" HeaderText="Code" SortExpression="QuoteNo">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="CustomerName" HeaderText="Contract Name" SortExpression="CustomerName">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="CustomerName" HeaderText="Party Name" SortExpression="CustomerName">
                                                                                                <HeaderStyle Wrap="False" />
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <%--   <asp:BoundField DataField="" HeaderText="Approved By" SortExpression="">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="" HeaderText="Effect From" SortExpression="">
                                                                                                <ItemStyle Wrap="False" />
                                                                                                <HeaderStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="" HeaderText="Effect To" SortExpression="">
                                                                                                <ItemStyle Wrap="False" />
                                                                                                <HeaderStyle Wrap="False" />
                                                                                            </asp:BoundField>--%>
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                    </asp:GridView>
                                                                                </center>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="vertical-align: top;" align="left">
                                                                                <div id="GridScroll" class="grid_scroll" style="width: 691px" runat="server">
                                                                                    <br />
                                                                                    <asp:GridView ID="GridView3" runat="server" Width="336px" Font-Size="8pt" Font-Names="Arial"
                                                                                        CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="GridView3_RowDataBound"
                                                                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                                                                        ForeColor="Black" GridLines="Vertical" 
                                                                                        onselectedindexchanged="GridView3_SelectedIndexChanged">
                                                                                        <FooterStyle BackColor="#CCCC99" />
                                                                                        <RowStyle Font-Names="Arial" Font-Size="8pt" BackColor="#F7F7DE" />
                                                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" Font-Names="Arial"
                                                                                            Font-Size="8pt" />
                                                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chk" runat="server" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="ID" SortExpression="sno">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="ActualRate" HeaderText="Actual Rate" SortExpression="ActualRate">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="MinRate" HeaderText="Min Rate" SortExpression="MinRate">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="VarRate" HeaderText="Var Rate" SortExpression="VarRate">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="MaxRate" HeaderText="Max Rate" SortExpression="MaxRate">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="FixRate" HeaderText="Fixed Rate" SortExpression="FixRate">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Unit" HeaderText="unit" SortExpression="unit">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <%-- <asp:BoundField DataField="break_bulk" HeaderText="Break Bulk" SortExpression="break_bulk">
                                                                                                <ItemStyle Wrap="False" />
                                                                                                <HeaderStyle Wrap="false" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="lcl" HeaderText="LCL" SortExpression="lcl">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="ft20" HeaderText="20 FT" SortExpression="ft20">
                                                                                                <ItemStyle Wrap="False" />
                                                                                                <HeaderStyle Wrap="false" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="ft40" HeaderText="40 FT" SortExpression="ft40">
                                                                                                <ItemStyle Wrap="False" />
                                                                                                <HeaderStyle Wrap="false" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="SB" HeaderText="Sales Bill" SortExpression="sb">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="DB" HeaderText="Debit Note" SortExpression="db">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>--%>
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="TrAddr1" runat="server" style="height: 10px;">
                                                                            <td align="left">
                                                                                <asp:Label ID="Label26" runat="server" Text="Select Billing Address : -" Font-Names="Arial"
                                                                                    Font-Size="9pt" ForeColor="#2461bf"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="TrAddr" runat="server">
                                                                            <td style="vertical-align: top;" align="left">
                                                                                <div id="GrdADDRSCROLL" class="grid_scroll" style="width: 700px; height: 200px;"
                                                                                    runat="server">
                                                                                    <asp:GridView ID="GrdPaddr" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                                        BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" DataKeyNames="BranchId"
                                                                                        Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="GrdPaddr_SelectedIndexChanged"
                                                                                        Width="674px">
                                                                                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="AccountCode" HeaderText="PCODE" SortExpression="AccountCode" />
                                                                                            <asp:BoundField DataField="BranchId" HeaderText="Branch" SortExpression="BranchId" />
                                                                                            <asp:BoundField DataField="Address1" HeaderText="Address" SortExpression="Address1" />
                                                                                            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                                                                                            <asp:BoundField DataField="State" HeaderText="State" SortExpression="Pin" />
                                                                                            <asp:BoundField DataField="Pincode" HeaderText="Pin" SortExpression="Pincode" />
                                                                                            <asp:CommandField ButtonType="Image" HeaderText="CL" SelectImageUrl="image/select.JPG"
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
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BtnContract_Submit" runat="server" Text="Submit" OnClick="BtnContract_Submit_Click" />
                                                        <asp:Button ID="BtnClose" runat="server" Text="Close" OnClick="BtnClose_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                            <table width="100%">
                            <tr>
                            <td style="font-weight: 700; text-align: center">
                            
                                History</td>
                            </tr>
                            <tr>
                            <td>
                            
                                <asp:GridView ID="gvFundrequest" runat="server" CellPadding="4" Font-Size="8pt" 
                                    ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" Font-Size="8pt" 
                                        ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            
                            </td>
                            </tr>
                            </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="invDate">
    </cc1:CalendarExtender>
    <cc1:RoundedCornersExtender ID="REC2" TargetControlID="Panel2" Radius="10" BorderColor="#2461bf"
        Corners="Top" runat="server">
    </cc1:RoundedCornersExtender>
    <%--<head runat="server">--%>
</asp:Content>
