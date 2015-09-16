<%@ Page Language="C#" AutoEventWireup="true" Inherits="EditInvoiceStaxUp" CodeBehind="EditInvoiceStaxUp.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FreeLibrary" Namespace="FreeLibrary" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::PIPL||Edit Invoice</title>
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
            background-image: url(   '../images/bg_homepage_right.gif' );
            border: 1px solid #ffffff;
        }
    </style>
    <style type="text/css">
        body
        {
            font-family: verdana;
            font-size: 10px;
        }
        .completionList
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
            background-color: white;
            font-family: verdana;
            font-size: 10px;
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
    </style>
    <style type="text/css">
        .accordionContent
        {
            width: 100%;
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
        .style7
        {
            width: 150px;
        }
    </style>
</head>
<body>
    <script type="text/javascript">

        function SetContextKey() {
            $find('<%=AutoCompleteExtender1.ClientID%>').set_contextKey($get("<%=txtCompName.ClientID %>").value);
        }


        function Calc() {
            var amount = 0.00;
            var table = document.getElementById('<%=GridView1.ClientID %>');
            var tamount = 0.00;
            var ntamount = 0.00;
            var staxtot = 0.00;
            var EDUCess = 0.00;
            var SHECess = 0.00;
            var Final = 0.00;
            var ladv = 0.00;
            var gtotal = 0.00;
            var Wording;
            for (var i = 1; i < table.rows.length; i++) {
                var ddl = table.rows[i].cells[4].childNodes[1].value;
                amount = table.rows[i].cells[6].childNodes[1].value;
                if (amount == "") {
                    amount = 0.00;
                }
                if (ddl == 0) {
                    ntamount = parseFloat(ntamount) + parseFloat(amount);
                    table.rows[i].cells[5].childNodes[1].value = 0.00;
                }
                else {
                    table.rows[i].cells[5].childNodes[1].value = ((parseFloat(ddl) * parseFloat(amount)) / 100).toFixed(2);
                    var stamount = table.rows[i].cells[6].childNodes[1].value;
                    tamount = parseFloat(tamount) + parseFloat(stamount);
                }
                staxtot = parseFloat(staxtot) + parseFloat(table.rows[i].cells[5].childNodes[1].value);
            }
            var staxgtot = 0.00;
            document.getElementById("SubTotal").value = parseFloat(ntamount).toFixed(2);
            document.getElementById("sTax").value = Math.round(parseFloat(staxtot)).toFixed(2);
            staxgtot = document.getElementById("sTax").value;
            document.getElementById("EdCess").value = Math.round((parseFloat(staxgtot) * (2) / 100)).toFixed(2);
            document.getElementById("SHCess").value = Math.round((parseFloat(staxgtot) * (1) / 100)).toFixed(2);
            document.getElementById('SubTotalTax').value = Math.round(parseFloat(tamount)).toFixed(2);
            var A = document.getElementById('SubTotalTax').value;
            var B = document.getElementById('SubTotal').value;
            var C = document.getElementById('sTax').value;
            var D = document.getElementById('EdCess').value;
            var E = document.getElementById('SHCess').value;
            Final = parseFloat(A) + parseFloat(B) + parseFloat(C) + parseFloat(D) + parseFloat(E);
            document.getElementById('Totals').value = Final.toFixed(2);
            ladv = document.getElementById('LessAd').value;
            if (ladv == '') {
                ladv = 0.00;
            }
            gtotal = parseFloat(document.getElementById('Totals').value) - parseFloat(ladv);
            document.getElementById('BalanceDue').value = Math.round(gtotal).toFixed(2);

            Wording = test_skill(document.getElementById('BalanceDue').value);
            document.getElementById('txtRupees').innerHTML = "Rupees" + Wording + " " + "Only";
            document.getElementById('hdnRuppees').value = "Rupees" + Wording + " " + "Only";
            return false;
            //(parseFloat(document.getElementById("ContentPlaceHolder1_SubTotalTax").value) + parseFloat(document.getElementById('ContentPlaceHolder1_SubTotal').value) + parseFloat(document.getElementById('ContentPlaceHolder1_sTax').value) + parseFloat(document.getElementById('ContentPlaceHolder1_EdCess').value) + parseFloat(document.getElementById('ContentPlaceHolder1_SHCess').value).toFixed(2));
        }

        function test_skill(s) {
            var junkVal = s;
            junkVal = Math.floor(junkVal);
            var obStr = new String(junkVal);
            numReversed = obStr.split("");
            actnumber = numReversed.reverse();

            if (Number(junkVal) >= 0) {
                //do nothing
            }
            else {
                alert('wrong Number cannot be converted');
                return false;
            }
            if (Number(junkVal) == 0) {
                obStr = "Zero";
                document.getElementById('ContentPlaceHolder1_txtRupees').value = "Rupees" + obStr + " " + "Only";
                // document.getElementById('ContentPlaceHolder1_txtRupees').value = obStr + '' + 'Rupees Zero Only';
                return obStr;

            }
            if (actnumber.length > 9) {
                alert('Oops!!!! the Number is too big to covert');
                return false;
            }
            var iWords = ["Zero", " One", " Two", " Three", " Four", " Five", " Six", " Seven", " Eight", " Nine"];
            var ePlace = ['Ten', ' Eleven', ' Twelve', ' Thirteen', ' Fourteen', ' Fifteen', ' Sixteen', ' Seventeen', ' Eighteen', ' Nineteen'];
            var tensPlace = ['and', ' Ten', ' Twenty', ' Thirty', ' Forty', ' Fifty', ' Sixty', ' Seventy', ' Eighty', ' Ninety'];
            var iWordsLength = numReversed.length;
            var totalWords = "";
            var inWords = new Array();
            var finalWord = "";
            j = 0;
            for (i = 0; i < iWordsLength; i++) {
                switch (i) {
                    case 0:
                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {
                            inWords[j] = '';
                        }
                        else {
                            inWords[j] = iWords[actnumber[i]];
                        }
                        inWords[j] = inWords[j];
                        break;
                    case 1:
                        tens_complication();
                        break;
                    case 2:
                        if (actnumber[i] == 0) {
                            inWords[j] = '';
                        }
                        else if (actnumber[i - 1] != 0 && actnumber[i - 2] != 0) {
                            inWords[j] = iWords[actnumber[i]] + ' Hundred and';
                        }
                        else {
                            inWords[j] = iWords[actnumber[i]] + ' Hundred';
                        }
                        break;
                    case 3:
                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {
                            inWords[j] = '';
                        }
                        else {
                            inWords[j] = iWords[actnumber[i]];
                        }
                        if (actnumber[i + 1] != 0 || actnumber[i] > 0) {
                            inWords[j] = inWords[j] + " Thousand";
                        }
                        break;
                    case 4:
                        tens_complication();
                        break;
                    case 5:
                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {
                            inWords[j] = '';
                        }
                        else {
                            inWords[j] = iWords[actnumber[i]];
                        }
                        inWords[j] = inWords[j] + " Lakh ";
                        break;
                    case 6:
                        tens_complication();
                        break;
                    case 7:
                        if (actnumber[i] == 0 || actnumber[i + 1] == 1) {
                            inWords[j] = '';
                        }
                        else {
                            inWords[j] = iWords[actnumber[i]];
                        }
                        inWords[j] = inWords[j] + " Crore";
                        break;
                    case 8:
                        tens_complication();
                        break;
                    default:
                        break;
                }
                j++;
            }

            function tens_complication() {
                if (actnumber[i] == 0) {
                    inWords[j] = '';
                }
                else if (actnumber[i] == 1) {
                    inWords[j] = ePlace[actnumber[i - 1]];
                }
                else {
                    inWords[j] = tensPlace[actnumber[i]];
                }
            }
            inWords.reverse();
            for (i = 0; i < inWords.length; i++) {
                finalWord += inWords[i];
            }
            return finalWord;
        }

    </script>
    <form id="form1" method="post" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/AutoComplete.asmx" />
            </Services>
        </asp:ScriptManager>
        <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" TargetControlID="txtInvoiceNo"
            ServicePath="~/AutoComplete.asmx" ServiceMethod="GetInvNoEDIT" MinimumPrefixLength="1"
            EnableCaching="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
            CompletionListItemCssClass="listItem">
        </cc1:AutoCompleteExtender>
        <table style="width: 100%; vertical-align: top;">
            <tr>
                <td align="center">
                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 810px;">
                        <tbody>
                            <tr>
                                <td align="center" colspan="2" style="width: 553px; height: 21px">
                                    <asp:Label ID="lblInvoice" runat="server" Text="INVOICE" Font-Bold="True" Font-Names="Verdana"
                                        Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 810px;">
                        <tbody>
                            <tr style="border-style: solid; border-width: 1px; border-color: #2461BF;">
                                <td align="left" id="a11" width="200px">
                                    <asp:Label ID="lblinvNumber" runat="server" Text="INVOICE NO." Font-Names="Arial"
                                        Font-Size="9pt" Font-Bold="True"></asp:Label>
                                    <asp:TextBox ID="txtInvoiceNo" runat="server" Width="100px" Font-Names="Arial" Font-Size="8pt"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td align="left" width="50px">
                                    <asp:Label ID="Label42" runat="server" Text="Date :" Font-Names="Arial" Font-Size="9pt"
                                        Font-Bold="True"></asp:Label>
                                </td>
                                <td align="left" id="a1" width="60px">
                                    <asp:TextBox ID="invDate" runat="server" Width="70px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                </td>
                                <td width="60px">
                                    <asp:Label ID="Label7" runat="server" Text="Suffix :" Font-Names="Arial" Font-Size="8pt"
                                        BorderStyle="None"></asp:Label>
                                </td>
                                <td width="60px">
                                    <asp:TextBox ID="txtSuffix" runat="server" Width="80px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                        Text="BE NO./DT." Style="font-weight: 700"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBENo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="100px"></asp:TextBox>
                                    <cc1:TextBoxWatermarkExtender ID="txtBENo_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="txtBENo" WatermarkText="BE NO">
                                    </cc1:TextBoxWatermarkExtender>
                                    <asp:TextBox ID="txtBEDate" runat="server" Font-Names="Arial" Font-Size="8pt" Width="90px"
                                        BackColor="#FFCCFF" ToolTip="BE DATE" onkeydown="return false;"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtBEDate">
                                    </cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="txtBEDate_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="txtBEDate" WatermarkText="BE DATE">
                                    </cc1:TextBoxWatermarkExtender>
                                </td>
                            </tr>
                            <tr style="border-style: solid; border-width: 1px; border-color: #2461BF;">
                                <td colspan="7">
                                    <asp:Label ID="Label9" runat="server" Text="Sub Party" Font-Names="Arial" Font-Size="8pt"
                                        Width="61px" ForeColor="Black"></asp:Label>
                                    <asp:TextBox ID="txtSubParty" onfocus="SetContextKey()" onkeyup="SetContextKey()"
                                        runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt" AutoPostBack="True"
                                        OnTextChanged="txtSubParty_TextChanged"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="0"
                                        ServiceMethod="GetSubParty" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                        CompletionListItemCssClass="listItem" ServicePath="~/AutoComplete.asmx" TargetControlID="txtSubParty"
                                        UseContextKey="true" CompletionInterval="100" EnableCaching="false">
                                    </cc1:AutoCompleteExtender>
                                    <asp:Label ID="Label54" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                        Text="SubParty Addr" Width="61px"></asp:Label>
                                    <asp:TextBox ID="txtSubPartyAddr" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Height="40px" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="border-style: solid; border-width: 1px; border-color: #2461BF;">
                                <td colspan="7">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 810px;
                                height: 260px;">
                                <tbody>
                                    <tr>
                                        <td align="left">
                                            <div style="width: 816px; left: 0px; height: 300px" class="grid_scroll">
                                                <cc2:NewRowGridView ID="GridView1" runat="server" AutoGenerateColumns="False" NewRowCount="10"
                                                    Font-Names="Arial" Font-Size="8pt" Font-Bold="True" OnRowDataBound="GridView1_RowDataBond"
                                                    Width="600px">
                                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNO" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsno" Width="15px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"sno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PARTICULARS" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDetails" Font-Names="arial" Width="360px" Text='<%# DataBinder.Eval(Container.DataItem,"charge_desc") %>'
                                                                    Font-Size="8pt" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionListCssClass="completionList"
                                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetChargeMaster"
                                                                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtDetails">
                                                                </cc1:AutoCompleteExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Narration" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtNarration" Font-Names="arial" Width="95px" Text='<%# DataBinder.Eval(Container.DataItem,"Narration") %>'
                                                                    Font-Size="8pt" runat="server" MaxLength="49"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RECEIPT DETAILS" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRecpt" Font-Names="arial" Width="95px" Text='<%# DataBinder.Eval(Container.DataItem,"receipt") %>'
                                                                    Font-Size="8pt" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Stax" AccessibleHeaderText="Stax" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlStax" runat="server" Width="50px" Font-Names="Arial" Font-Size="8pt"
                                                                    onchange="javascript:return Calc();" DataTextField='<%# DataBinder.Eval(Container.DataItem,"ServiceTaxPercent") %>'>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="StaxAmt" AccessibleHeaderText="StaxAmt" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtStaxAmt" Font-Names="arial" Width="50px" Font-Size="8pt" runat="server"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem,"ServiceTaxAmount") %>' Enabled="True"
                                                                    onkeydown="return false;"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="AMOUNT" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="amt1" Font-Names="arial" Width="75px" Font-Size="8pt" Text='<%# DataBinder.Eval(Container.DataItem,"amount") %>'
                                                                    onblur="javascript:return Calc();" AutoPostBack="false" Style="text-align: right"
                                                                    runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FTE1" TargetControlID="amt1" FilterType="Custom"
                                                                    ValidChars="0123456789." runat="server">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </cc2:NewRowGridView>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 810px;">
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
                                                            Font-Size="7pt" Text="Service Tax@" Visible="False"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="drServiceTax" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Width="50px" AutoPostBack="True" OnSelectedIndexChanged="drServiceTax_SelectedIndexChanged"
                                                            Visible="False">
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
                                                Text="Calculate" Width="120px" Visible="False" />
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label48" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                Text="Less Advance" Width="65px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="LessAd" runat="server" AutoPostBack="false" Font-Names="Arial" Font-Size="8pt"
                                                onblur="javascript:return Calc();" Style="text-align: right" Width="70px" BackColor="#FFFFCC">0</asp:TextBox>
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
                            <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 810px;">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top;" class="style28">
                                            <asp:TextBox ID="txtRupees" runat="server" Font-Names="Arial" Font-Size="7pt" Height="20px"
                                                Width="420px" BorderStyle="None"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="balance1" runat="server" BackColor="White" BorderStyle="Solid" ForeColor="White"
                                                Width="67px">0</asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="border: 1px solid #2461BF; width: 434px; vertical-align: top;">
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server" Font-Names="Arial" BackColor="#DEDFDE" Font-Size="7pt"
                                    GroupingText="Customer Info" Width="370px" ForeColor="#000040">
                                    <%--<asp:UpdatePanel ID="update1" runat="server" >
                                        <ContentTemplate>--%>
                                    <table>
                                        <tr>
                                            <td style="width: 56px; height: 26px;">
                                                <asp:Label ID="Label8" runat="server" Text="Party Name" Font-Names="Arial" Font-Size="8pt"
                                                    ForeColor="Black" Width="85px"></asp:Label>
                                            </td>
                                            <td style="height: 26px; width: 278px;">
                                                <asp:TextBox ID="txtCompName" runat="server" Font-Names="Arial" Font-Size="8pt" Height="20px"
                                                    Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 56px; height: 26px;">
                                                <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Text="Address"></asp:Label>
                                            </td>
                                            <td style="height: 26px; width: 278px;">
                                                <asp:TextBox ID="txtAdd1" runat="server" Font-Names="Arial" Font-Size="8pt" Height="40px"
                                                    Width="200px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style7">
                                                <asp:Label ID="Label51" runat="server" Text="Tally Sub Party Name" Font-Names="Arial"
                                                    Font-Size="8pt" ForeColor="Black" Width="120px" Visible="False"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlTallyAccountName" runat="server" AppendDataBoundItems="True"
                                                    Font-Names="Arial" Font-Size="8pt" Width="200px" Visible="False">
                                                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style7">
                                                <asp:Label ID="Label50" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Text="Tally Account Name" Width="100px" Visible="False"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlTallySubPartyName" runat="server" AppendDataBoundItems="True"
                                                    Font-Names="Arial" Font-Size="8pt" Visible="False" Width="200px">
                                                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 56px; height: 20px;">
                                                <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Text="Party Ref" Width="62px"></asp:Label>
                                            </td>
                                            <td style="width: 278px; height: 20px;">
                                                <asp:TextBox ID="txtParty_Reff" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style23">
                                                <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Text="City"></asp:Label>
                                            </td>
                                            <td class="style24">
                                                <asp:TextBox ID="txtCity" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 56px; height: 20px;">
                                                <asp:Label ID="Label53" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Text="Inv Seq No" Width="62px"></asp:Label>
                                            </td>
                                            <td style="width: 278px; height: 20px;">
                                                <asp:TextBox ID="txtInvSeqNo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--  </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </asp:Panel>
                                <br />
                                <br />
                                <asp:Panel ID="Panel3" runat="server" BackColor="#DEDFDE" Font-Names="Arial" Font-Size="7pt"
                                    ForeColor="#000040" GroupingText="Job Info" Width="370px">
                                    <table>
                                        <tr>
                                            <td style="width: 89px; height: 26px;">
                                                <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Text="Job No."></asp:Label>
                                            </td>
                                            <td style="height: 26px; width: 138px;">
                                                <asp:TextBox ID="txtJobNo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 89px">
                                                <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Height="16px" Text="AWB / BL No." Width="91px"></asp:Label>
                                            </td>
                                            <td style="width: 138px">
                                                <asp:TextBox ID="txtBLNo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Text="Ass. Value"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAssValue" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 89px; height: 20px;">
                                                <asp:Label ID="Label27" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Text="Custom Duty"></asp:Label>
                                            </td>
                                            <td style="height: 20px; width: 138px;">
                                                <asp:TextBox ID="txtCustomDuty" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 89px; height: 20px;">
                                                <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Text="No.of Container"></asp:Label>
                                            </td>
                                            <td style="height: 20px; width: 138px;">
                                                <asp:TextBox ID="txtNCNTR" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style25">
                                                <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Text="Quantity"></asp:Label>
                                            </td>
                                            <td class="style26">
                                                <asp:TextBox ID="txtQty" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 89px; vertical-align: top;">
                                                <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Text="Item Imported"></asp:Label>
                                            </td>
                                            <td style="width: 138px">
                                                <asp:TextBox ID="txtImpotItem" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Height="40px" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <asp:Label ID="Label46" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                    Text="Note : -"></asp:Label>
                                            </td>
                                            <td>
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
                                                <asp:CheckBox ID="chkSupplierInvNo" runat="server" AutoPostBack="True" OnCheckedChanged="chkSupplierInvNo_CheckedChanged" />
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
                        OnClick="Submit_Click" CssClass="button_image1" Text="Submit" Width="80px" />
                    <asp:Button ID="preview" runat="server" Font-Names="Arial" Font-Size="8pt" Height="25px"
                        OnClick="preview_Click" CssClass="button_image1" Text="Print Preview" Width="80px" />
                    <asp:Button ID="btnMail" runat="server" Font-Names="Arial" Font-Size="8pt" Height="25px"
                        OnClick="btnMail_Click" CssClass="button_image1" Text="Mail" Width="50px" />
                    <asp:Button ID="ExportTally" runat="server" Text="Export Tally" Font-Names="arial"
                        Font-Size="8pt" Width="70px" CssClass="button_image1" Height="25px" OnClick="ExportTally_Click" />
                    <asp:Button ID="BtnExit" runat="server" Font-Names="Arial" CssClass="button_image1"
                        Font-Size="8pt" Height="25px" Text="Exit" Width="50px" OnClick="BtnExit_Click"
                        PostBackUrl="~/Billing/index.aspx" />
                </td>
            </tr>
        </table>
    </div>
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
    </form>
</body>
</html>
