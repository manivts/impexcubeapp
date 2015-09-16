﻿<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true"
    Inherits="PIPLDebit" CodeBehind="PIPLDebit.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <title>::PIPL||Debit Note</title>
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
        .waterText
        {
            font-family: Arial;
            font-size: 8pt;
            color: Fuchsia;
            overflow: auto;
            background-color: #ffff00;
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
        body
        {
            font-family: verdana;
            font-size: 10px;
        }
        .button_image1
        {
            font-family: verdana;
            font-size: 11px;
            color: #ffffff;
            background-color: #013388;
            background-image: url('../images/bg_homepage_right.gif');
            border: 1px solid #ffffff;
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
        .grid_scroll
        {
            overflow: auto;
            height: 230px;
            width: 200px;
        }
        .style1
        {
            height: 20px;
        }
        .style2
        {
            width: 93px;
            height: 20px;
        }
        .style3
        {
            width: 243px;
        }
        .style7
        {
            width: 150px;
        }
    </style>
    <script type="text/javascript">
        function SetContextKey() {
            $find('<%=AutoCompleteExtender2.ClientID%>').set_contextKey($get("<%=txtCompName.ClientID %>").value);
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
            for (var i = 0; i < table.rows.length; i++) {
                amount = table.rows[i].cells[3].childNodes[1].value;
                if (amount == "") {
                    amount = 0.00;
                }
                ntamount = parseFloat(ntamount) + parseFloat(amount);
            }
            document.getElementById("ContentPlaceHolder1_SubTotal").value = Math.round(parseFloat(ntamount)).toFixed(2);
            ladv = document.getElementById('ContentPlaceHolder1_LessAd').value;
            if (ladv == '') {
                ladv = 0.00;
            }
            gtotal = parseFloat(document.getElementById('ContentPlaceHolder1_SubTotal').value) - parseFloat(ladv);
            document.getElementById('ContentPlaceHolder1_BalanceDue').value = Math.round(gtotal).toFixed(2);

            Wording = test_skill(document.getElementById('ContentPlaceHolder1_BalanceDue').value);
            document.getElementById('ContentPlaceHolder1_txtRupees').innerHTML = "Rupees" + Wording + " " + "Only";
            document.getElementById('ContentPlaceHolder1_hdnRuppees').value = "Rupees" + Wording + " " + "Only";
            return false;
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
    <%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">--%>
    <div style="vertical-align: top;">
        <div id="MainForm" runat="server" style="vertical-align: top;">
            <table style="z-index: 101; right: 15px; position: absolute; top: 0px">
                <tr>
                    <td>
                        <asp:Label ID="llbHead" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            Height="18px"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table id="tblDebit" runat="server" style="width: 100%; height: 700px; border: 1px;">              
                       
                            <tr id="trMain" runat="server">
                                <td align="left" style="height: 90%; vertical-align: top; background-color: white;
                                    width: 975px;">
                                    <asp:Panel ID="Panel2" runat="server" Height="640px" Width="100%" BackColor="white">
                                        <table>
                                            <tr>
                                                <td style="vertical-align: top;" align="left">
                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                        <tbody>
                                                            <tr>
                                                                <td align="center" style="width: 610px; height: 16px">
                                                                    <asp:Label ID="lblIName" runat="server" Text="IMPORTS - DEBIT NOTE" Font-Bold="True"
                                                                        Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                        <tbody>
                                                            <tr style="border-style: solid; border-width: 1px; border-color: #2461BF;">
                                                                <td style="width: 300px;" id="a1">
                                                                    <asp:Label ID="lblINumber" runat="server" Text="INV. NO." Font-Names="Arial" Font-Size="8pt"
                                                                        Font-Bold="True"></asp:Label>
                                                                    <asp:Label ID="lblInvNo" runat="server" Width="125px" Font-Bold="True" Font-Names="Arial"
                                                                        Font-Size="7pt"></asp:Label>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label7" runat="server" Text="Suffix " Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtSuffix" runat="server" Font-Names="Arial" Font-Size="8pt" Width="80px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 198px; height: 17px;">
                                                                    <asp:Label ID="Label42" runat="server" Text="Date :" Font-Names="Arial" Font-Size="8pt"
                                                                        Font-Bold="True"></asp:Label>
                                                                    <asp:TextBox ID="invDate" runat="server" Width="80px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="invDate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="border-color: #2461BF; height: 25px; border-style: solid; border-width: 1px;
                                                        width: 532px;">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px; vertical-align: top;" align="left">
                                                                    <asp:Label ID="Label6" runat="server" Text="Job No" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 80px; vertical-align: top;">
                                                                    <asp:CheckBox ID="chk" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt"
                                                                        OnCheckedChanged="chk_CheckedChanged" Visible="False" />
                                                                </td>
                                                                <td align="left" style="width: 100px; vertical-align: top;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtJNO" Font-Names="Arial" Font-Size="8pt" runat="server" 
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
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td align="left" style="vertical-align: top;">
                                                                    <asp:Button ID="BtnStandard" runat="server" Text="Go" Font-Size="8pt" OnClick="BtnStandard_Click"
                                                                        Width="41px" CssClass="button_image1" Height="25px" 
                                                                        UseSubmitBehavior="False" />
                                                                        <asp:Button ID="New" runat="server" Font-Names="Arial" Font-Size="8pt" Height="25px"
                                                        OnClick="New_Click" Text="New" Width="80px" CssClass="button_image1" Visible="false"
                                                        UseSubmitBehavior="False" />
                                                                    <asp:CheckBox ID="ChkDM" runat="server" Text="Demurrage" />
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left" style="vertical-align: top;" class="style8">
                                                                    <asp:Label ID="Label22" runat="server" Text="S.No" Font-Size="8pt" Font-Names="Arial"
                                                                        Font-Bold="False"></asp:Label>
                                                                </td>
                                                                <td style="vertical-align: top;" align="center" class="style3">
                                                                    <asp:Label ID="Label23" runat="server" Text="PARTICULARS" Font-Size="8pt" Font-Names="Arial"
                                                                        Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td align="center" style="vertical-align: top;">
                                                                    <asp:Label ID="Label24" runat="server" Text="Narration" Font-Size="8pt" Font-Names="Arial"
                                                                        Font-Bold="False" Style="font-weight: 700"></asp:Label>
                                                                </td>
                                                                <td align="center" style="vertical-align: top;">
                                                                    <asp:Label ID="Label26" runat="server" Text="AMOUNT Rs." Font-Size="8pt" Font-Names="Arial"
                                                                        Font-Bold="False" Style="font-weight: 700"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 479px;
                                                                height: 256px;">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <div style="width: 525px; height: 245px" class="grid_scroll">
                                                                                <asp:GridView ID="GridView1" runat="server" Font-Names="Arial" Font-Size="8pt" BackColor="White"
                                                                                    BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" CellPadding="3"
                                                                                    GridLines="None" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBond"
                                                                                    ShowHeader="False" Width="500px">
                                                                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                                                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                                                                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                                    <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SNO">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Bind("t1")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="PARTICULARS">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtDetails" Font-Names="arial" Width="250px" Font-Size="8pt" runat="server"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionListCssClass="completionList"
                                                                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetChargeMaster"
                                                                                                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtDetails">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="RECEIPT DETAILS">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtRecpt" Font-Names="arial" Width="100px" Font-Size="8pt" runat="server"
                                                                                                    MaxLength="49"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="AMOUNT">
                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="amt1" Font-Names="arial" Width="80px" Font-Size="8pt" AutoPostBack="false"
                                                                                                    Style="text-align: right" runat="server" onblur="javascript:return Calc();" BackColor="seashell"></asp:TextBox>
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
                                                                </tbody>
                                                            </table>
                                                            <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="balance1" runat="server" BackColor="White" BorderStyle="Solid" ForeColor="White"
                                                                                Width="67px">0</asp:TextBox>
                                                                        </td>
                                                                        <td align="right" style="width: 187px">
                                                                            <asp:Label ID="Label28" runat="server" Text="Sub Total" Font-Bold="True" Font-Names="Arial"
                                                                                Font-Size="7pt" Width="62px"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 8px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="width: 93px">
                                                                            <asp:TextBox ID="SubTotal" runat="server" Width="80px" Style="text-align: right"
                                                                                Font-Names="Arial" Font-Size="8pt" BackColor="#FFFFCC">0</asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 10px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="vertical-align: top;">
                                                                            <asp:Button ID="btncalculate" runat="server" Height="25px" OnClick="btncalculate_Click"
                                                                                Text="Calculate" Width="120px" Visible="False" />
                                                                            <br />
                                                                            <br />
                                                                        </td>
                                                                        <td align="right" colspan="2">
                                                                            &nbsp;<asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                                Font-Size="7pt" Text="Less Advance Recd." Width="150px"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 93px">
                                                                            <asp:TextBox ID="LessAd" runat="server" Width="80px" Style="text-align: right" Font-Names="Arial"
                                                                                onblur="javascript:return Calc();" Font-Size="8pt" AutoPostBack="false" BackColor="#FFFFCC">0</asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 10px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style1">
                                                                        </td>
                                                                        <td align="right" colspan="2" class="style1">
                                                                            <asp:Label ID="Label34" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                                Text="Balance Due"></asp:Label>
                                                                        </td>
                                                                        <td class="style2">
                                                                            <asp:TextBox ID="BalanceDue" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                                Width="81px" BackColor="#FFFFCC">0</asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;
                                                                border-bottom-color: #2461BF;">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="vertical-align: top;" class="style28">
                                                                            <asp:Label ID="txtRupees" runat="server" Height="23px" Width="500px" Font-Names="Arial"
                                                                                Font-Size="8pt"></asp:Label>
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
                                                <td align="left" style="border: 1px solid #2461BF; vertical-align: top;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="Panel1" runat="server" Font-Names="Arial" BackColor="#DEDFDE" Font-Size="7pt"
                                                                    GroupingText="Customer Info" Width="350px" ForeColor="#000040">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 56px; height: 26px;">
                                                                                <asp:Label ID="Label8" runat="server" Text="Party Name" Font-Names="Arial" Font-Size="8pt"
                                                                                    Width="85px" ForeColor="Black"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px; width: 278px;">
                                                                                <asp:TextBox ID="txtCompName" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"
                                                                                    Height="20px" ReadOnly="True"></asp:TextBox>&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 56px; height: 26px;">
                                                                                <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                    Text="Address"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 26px; width: 278px;">
                                                                                <asp:TextBox ID="txtAdd1" runat="server" Font-Names="Arial" Font-Size="8pt" Height="40px"
                                                                                    TextMode="MultiLine" Width="200px" ontextchanged="txtAdd1_TextChanged"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 56px">
                                                                                <asp:Label ID="Label9" runat="server" Text="Sub Party" Font-Names="Arial" Font-Size="8pt"
                                                                                    Width="61px" ForeColor="Black"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 278px">
                                                                                <asp:TextBox ID="txtSubParty" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"
                                                                                    AutoPostBack="True" onfocus="SetContextKey()" onkeyup="SetContextKey()" 
                                                                                    ontextchanged="txtSubParty_TextChanged"></asp:TextBox>.
                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" MinimumPrefixLength="0"
                                                                                    ServiceMethod="GetSubParty" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                    CompletionListItemCssClass="listItem" ServicePath="~/AutoComplete.asmx" TargetControlID="txtSubParty"
                                                                                    UseContextKey="true" CompletionInterval="100" EnableCaching="true">
                                                                                </cc1:AutoCompleteExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 56px">
                                                                                <asp:Label ID="Label54" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                    Text="SubParty Addr" Width="61px"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 278px">
                                                                                <asp:TextBox ID="txtSubPartyAddr" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Height="40px" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style7">
                                                                                <asp:Label ID="Label50" runat="server" Text="Tally Account Name" Font-Names="Arial"
                                                                                    Font-Size="8pt" ForeColor="Black" Width="100px" Visible="False"></asp:Label>
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
                                                                                <asp:Label ID="Label51" runat="server" Text="Tally Sub Party Name" Font-Names="Arial"
                                                                                    Font-Size="8pt" ForeColor="Black" Width="120px" Visible="False"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="ddlTallySubPartyName" runat="server" AppendDataBoundItems="True"
                                                                                    Enabled="False" Font-Names="Arial" Font-Size="8pt" Width="200px" Visible="False">
                                                                                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                                </asp:DropDownList>
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
                                                                                <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                    Text="Party Ref" Width="62px"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 278px; height: 20px;">
                                                                                <asp:TextBox ID="txtParty_Reff" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                    Width="200px"></asp:TextBox>
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
                                                                </asp:Panel>
                                                                <br />
                                                                <asp:Panel ID="Panel3" runat="server" BackColor="#DEDFDE" Font-Names="Arial" Font-Size="7pt"
                                                                    ForeColor="#000040" GroupingText="Job Info" Width="350px">
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
                                                                            <td style="width: 89px">
                                                                                <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                    Text="BE NO./DT."></asp:Label>
                                                                            </td>
                                                                            <td>
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
                                                                                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
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
                                                                                    Height="30px" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="vertical-align: top;">
                                                                                <asp:Label ID="Label46" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                    Text="Note : -"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" Height="30px"
                                                                                    TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="vertical-align: top; text-align: left;">
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
                                                        RequireOpenedPane="false" AutoSize="Limit" Height="50 px">
                                                        <Panes>
                                                            <cc1:AccordionPane ID="AccPan1" runat="server">
                                                                <Header>
                                                                    Importer Remarks</Header>
                                                                <Content>
                                                                    <asp:TextBox ID="txtimpRemark" Font-Names="Arial" Font-Size="8pt" Width="300px" runat="server"></asp:TextBox></Content>
                                                            </cc1:AccordionPane>
                                                            <cc1:AccordionPane ID="AccPan2" runat="server">
                                                                <Header>
                                                                    Indent Remarks</Header>
                                                                <Content>
                                                                    <asp:TextBox ID="txtIndentRemark" Font-Names="Arial" Font-Size="8pt" Width="300px"
                                                                        runat="server"></asp:TextBox></Content>
                                                            </cc1:AccordionPane>
                                                        </Panes>
                                                    </cc1:Accordion>
                                                </td>
                                                <td style="border: 1px solid #2461BF; vertical-align: top;" align="right">
                                                    <asp:Label ID="lblResult" runat="server"></asp:Label>                                                    
                                                    <asp:Button ID="Submit" runat="server" Font-Names="Arial" Font-Size="8pt" Height="25px"
                                                        OnClick="Submit_Click" Text="Submit" Width="80px" CssClass="button_image1" 
                                                        UseSubmitBehavior="False" />
                                                    <asp:Button ID="preview" runat="server" Font-Names="Arial" Font-Size="8pt" Height="25px"
                                                        OnClick="preview_Click" Text="Print Preview" Width="80px" CssClass="button_image1" />
                                                    <asp:Button ID="btnMail" runat="server" Font-Names="Arial" Font-Size="8pt" Height="25px"
                                                        OnClick="btnMail_Click" Text="Mail" Width="50px" CssClass="button_image1" />
                                                    <asp:Button ID="BtnExit" runat="server" CssClass="button_image1" Font-Names="Arial"
                                                        Font-Size="8pt" Height="25px" PostBackUrl="~/index.aspx" Text="Exit" Width="50px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <%--<tr id="TrAddr1" runat="server" style="height: 10px;">
                                <td align="left">
                                    <asp:Label ID="Label45" runat="server" Text="Select Billing Address : -" Font-Names="Arial"
                                        Font-Size="9pt" ForeColor="#2461bf"></asp:Label>
                                    <asp:RadioButtonList ID="rbBill" runat="server" Font-Size="8pt" Font-Names="Arial"
                                        Width="159px" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbBill_SelectedIndexChanged">
                                        <asp:ListItem Value="DP">Direct Party</asp:ListItem>
                                        <asp:ListItem Value="TP">Third Party</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>--%>
                            <%--<tr id="TrAddr" runat="server">
                                <td style="vertical-align: top;" align="left">
                                    <div id="GrdADDRSCROLL" class="grid_scroll" style="width: 700px; height: 200px;"
                                        runat="server">
                                    </div>
                                </td>
                            </tr>--%>                       
            </table>
            <input type="hidden" id="hdnRuppees" runat="server" />
        </div>
    </div>
    <div id="SubForm" runat="server" >
    <table id="tblGrid" runat="server">
          <tr id="TrAddr1" runat="server">
                            <td style="vertical-align: top;" align="left" class="style33">
                            <table id="tblContr" runat="server">
                                                <tr>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel4" runat="server" Height="30px" BackColor="#2461bf" Width="700px">
                                                                        <asp:Label ID="lblContr" runat="server" ForeColor="White" Font-Size="9pt" Font-Names="Arial"
                                                                            BackColor="#2461BF" Width="400px"></asp:Label>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top;">
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            
                                            </table>                                       
                                                                                     
                                                                        
                            </td>
                        </tr>
    <tr id="trBill" runat="server" style="height: 10px;">
                            <td align="left" class="style33">
                                <asp:Label ID="Label2" runat="server" Text="Select Billing Address : -" Font-Names="Arial"
                                    Font-Size="9pt" ForeColor="#2461bf"></asp:Label>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Size="8pt" Font-Names="Arial"
                                    Width="159px" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbBill_SelectedIndexChanged">
                                    <asp:ListItem Value="DP">Direct Party</asp:ListItem>
                                    <asp:ListItem Value="TP">Third Party</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>

                                                                        <tr id="trGrid" runat="server">
                                                                            <td align="center" style="vertical-align: top; text-align: left;">
                                                                           
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
                                                                                         
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                    </asp:GridView>
                                                                          
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
                                                                                     
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                    </asp:GridView>
                                                                             </div>
                                                                            </td>
                                                                        </tr>
                                                                   
                                                                           <tr id="Tr2" runat="server" style="height: 10px;">
                                                                            <td align="left">
                                                                                <asp:Label ID="Label3" runat="server" Text="Select Billing Address : -" Font-Names="Arial"
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
                                                                        <tr>
                                                    <td>
                                                        <asp:Button ID="BtnContract_Submit" runat="server" Text="Submit" OnClick="BtnContract_Submit_Click" Visible="false" />
                                                        <asp:Button ID="BtnClose" runat="server" Text="Close" OnClick="BtnClose_Click" Visible="false" />
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
    <br />
    <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" TargetControlID="txtCompName"
        ServicePath="AutoComplete.asmx" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem" ServiceMethod="GetInvoice" MinimumPrefixLength="1"
        EnableCaching="true">
    </cc1:AutoCompleteExtender>
    <br />
</asp:Content>
<%--</form>
</body>
</html>
--%>