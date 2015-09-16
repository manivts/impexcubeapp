
<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" 
    Inherits="PIPLinvoiceSTAX" CodeBehind="PIPLinvoiceSTAX.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">--%>
<%--<html xmlns="http://www.w3.org/1999/xhtml">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">--%>
    <head> </head>
    <title>::PIPL||Invoice</title>
    <style type="text/css">
        body
        {
            font-family: verdana;
            font-size: 10px;
        }
        .waterText
        {
            font-family: Arial;
            font-size: 8pt;
            color: Fuchsia;
            overflow: auto;
            background-color: #ffff00;
        }
        .button_image1
        {
            font-family: verdana;
            font-size: 11px;
            color: #ffffff;
            background-color: #013388;
            background-image: url(      '../images/bg_homepage_right.gif' );
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
        }
        .style8
        {
        }
        .style14
        {
            width: 241px;
        }
        .style23
        {
            width: 56px;
            height: 26px;
        }
        .style24
        {
            width: 278px;
            height: 26px;
        }
        .style25
        {
            width: 89px;
            height: 26px;
        }
        .style26
        {
            width: 138px;
            height: 26px;
        }
        .style29
        {
            height: 222px;
        }
        .style32
        {
            width: 93px;
        }
        .style33
        {
            width: 1045px;
        }
        .style35
        {
            width: 159px;
        }
        .style36
        {
            height: 445px;
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
    <style type="text/css">

.ModalPopupBG
{
    background-color: #666699;
    filter: alpha(opacity=50);
    opacity: 0.7;
}

.HellowWorldPopup
{
    min-width:200px;
    min-height:150px;
    background:white;
}

    </style>
  
      <script type="text/javascript">

          function RemoveSpecialChar(txtName) {
              if (txtName.value != '' && txtName.value.match(/^[\w ]+$/) == null) {
                  txtName.value = txtName.value.replace(/[\W]/g, '');
              }
          }



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
              for (var i = 0; i < table.rows.length; i++) {
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
              document.getElementById("ContentPlaceHolder1_SubTotal").value = parseFloat(ntamount).toFixed(2);
              document.getElementById("ContentPlaceHolder1_sTax").value = Math.round(parseFloat(staxtot)).toFixed(2);
              staxgtot = document.getElementById("ContentPlaceHolder1_sTax").value;
              document.getElementById("ContentPlaceHolder1_EdCess").value = Math.round((parseFloat(staxgtot) * (2) / 100)).toFixed(2);
              document.getElementById("ContentPlaceHolder1_SHCess").value = Math.round((parseFloat(staxgtot) * (1) / 100)).toFixed(2);
              document.getElementById('ContentPlaceHolder1_SubTotalTax').value = Math.round(parseFloat(tamount)).toFixed(2);
              var A = document.getElementById('ContentPlaceHolder1_SubTotalTax').value;
              var B = document.getElementById('ContentPlaceHolder1_SubTotal').value;
              var C = document.getElementById('ContentPlaceHolder1_sTax').value;
              var D = document.getElementById('ContentPlaceHolder1_EdCess').value;
              var E = document.getElementById('ContentPlaceHolder1_SHCess').value;
              Final = parseFloat(A) + parseFloat(B) + parseFloat(C) + parseFloat(D) + parseFloat(E);
              document.getElementById('ContentPlaceHolder1_Totals').value = Final.toFixed(2);
              ladv = document.getElementById('ContentPlaceHolder1_LessAd').value;
              if (ladv == '') {
                  ladv = 0.00;
              }
              gtotal = parseFloat(document.getElementById('ContentPlaceHolder1_Totals').value) - parseFloat(ladv);
              document.getElementById('ContentPlaceHolder1_BalanceDue').value = Math.round(gtotal).toFixed(2);

              Wording = test_skill(document.getElementById('ContentPlaceHolder1_BalanceDue').value);
              document.getElementById('ContentPlaceHolder1_txtRupees').innerHTML = "Rupees" + Wording + " " + "Only";
              document.getElementById('ContentPlaceHolder1_hdnRuppees').value = "Rupees" + Wording + " " + "Only";
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
   
    <%--<html xmlns="http://www.w3.org/1999/xhtml">--%><%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">--%>
    <div id="MainForm" runat="server" style="vertical-align: top;">
        <table style="z-index: 101; right: 15px; position: absolute; top: 0px">
            <tr>
                <td>
                    <asp:Label ID="llbHead" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table id="tblInv" runat="server" style="width: 100%; height: 400px; border: 1px;">
            <tr>
                <td style="vertical-align: top;">
                    <table style="width: 100%;">
                        <tr id="trMain" runat="server">
                            <td align="left" style="vertical-align: top; background-color: white;">
                                <asp:Panel ID="Panel2" runat="server" Height="700px" Width="100%" BackColor="white">
                                    <table style="background-color: Window;">   

                                        <tr>
                                            <td align="left" style="vertical-align: top;" class="style36">
                                                <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 810px;">
                                                    <tbody>
                                                        <tr>
                                                            <td align="center" style="width: 610px; height: 16px">
                                                                <asp:Label ID="lblIName" runat="server" Font-Names="arial" Font-Size="10pt" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 810px;">
                                                    <tbody>
                                                        <tr style="border-style: solid; border-width: 1px; border-color: #2461BF;">
                                                            <td id="a1" class="style35">
                                                                <asp:Label ID="lblINumber" runat="server" Text="INV. NO." Font-Names="Arial" Font-Size="8pt"
                                                                    Font-Bold="True"></asp:Label>
                                                                <asp:Label ID="lblInvNo" runat="server" Width="100px" Font-Bold="True" Font-Names="Arial"
                                                                    Font-Size="7pt"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label7" runat="server" Text="Suffix " Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSuffix" runat="server" Font-Names="Arial" Font-Size="8pt" Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label42" runat="server" Text="Date :" Font-Names="Arial" Font-Size="8pt"
                                                                    Font-Bold="True"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="invDate" runat="server" Font-Names="Arial" Font-Size="8pt" Width="60px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="invDate">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 810px;">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label6" runat="server" Text="Job No" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                                <asp:CheckBox ID="chk" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="7pt"
                                                                    OnCheckedChanged="chk_CheckedChanged" />
                                                            </td>
                                                            <td align="left" style="height: 26px;">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>--%>
                                                                            <asp:TextBox ID="txtJNO" Font-Names="Arial" Font-Size="8pt" runat="server" OnTextChanged="txtJNO_TextChanged"
                                                                                Width="70px" AutoPostBack="true"></asp:TextBox>
                                                                            <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" EnableCaching="true"
                                                                                MinimumPrefixLength="1" ServiceMethod="GetJobNo" CompletionListCssClass="completionList"
                                                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                ServicePath="~/AutoComplete.asmx" TargetControlID="txtJNO">
                                                                            </cc1:AutoCompleteExtender>
                                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtJNO"
                                                                                WatermarkCssClass="waterText" WatermarkText="75079" runat="server">
                                                                            </cc1:TextBoxWatermarkExtender>
                                                                            <%--  </ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="BtnStandard" runat="server" Text="Go" Font-Size="8pt" CssClass="button_image1"
                                                                                OnClick="BtnStandard_Click" Width="41px" Height="25px" 
                                                                                UseSubmitBehavior="False" />
                                                                        </td>
                                                                        <td>
                                                                        <asp:Button ID="New" runat="server" Font-Names="Arial" Font-Size="8pt" Height="25px"
                                                                        OnClick="New_Click" CssClass="button_image1" Text="New" Width="80px" Visible="false" UseSubmitBehavior="false" />
                                                                        </td>
                                                                        <td class="style14" colspan="1">
                                                                            <asp:Button ID="Button1" runat="server" Font-Names="Arial" Font-Size="8pt" CssClass="button_image1"
                                                                                OnClick="Button1_Click" Text="Copy From Previous Bill" Visible="False" Width="150px"
                                                                                Height="25px" />
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
                                                <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 810px;">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" style="vertical-align: top;" class="style8" width="15px">
                                                                <asp:Label ID="Label53" runat="server" Text="S.No" Font-Size="8pt" Font-Names="Arial"
                                                                    Font-Bold="False"></asp:Label>
                                                            </td>
                                                            <td style="vertical-align: top;" align="left" width="262px">
                                                                <asp:Label ID="Label54" runat="server" Text="PARTICULARS" Font-Size="8pt" Font-Names="Arial"
                                                                    Font-Bold="True"></asp:Label>
                                                            </td>
                                                            <td align="left" style="vertical-align: top;" class="style8" width="166px">
                                                                <asp:Label ID="Label61" runat="server" Text="Narration" Font-Size="8pt" Font-Names="Arial"
                                                                    Font-Bold="False"></asp:Label>
                                                            </td>
                                                            <td align="left" style="vertical-align: top;" width="108px">
                                                                <asp:Label ID="Label60" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                    Text="Receipts"></asp:Label>
                                                            </td>
                                                            <td align="left" style="vertical-align: top; margin-left: 40px;" class="style8" width="58px">
                                                                <asp:Label ID="Label56" runat="server" Text="Stax" Font-Size="8pt" Font-Names="Arial"
                                                                    Font-Bold="False"></asp:Label>
                                                            </td>
                                                            <td align="left" class="style8" style="vertical-align: top;" width="60px    ">
                                                                <asp:Label ID="Label57" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                    Text="Stax Amt"></asp:Label>
                                                            </td>
                                                            <td align="left" class="style8" style="vertical-align: top;">
                                                                <asp:Label ID="Label62" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                    Text="Amount RS."></asp:Label>
                                                            </td>
                                                            <td align="left" class="style8" style="vertical-align: top;">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 810px;
                                                            height: 250px;">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="vertical-align: top;" class="style29">
                                                                        <div style="width: 802px; height: 245px" class="grid_scroll">
                                                                            <asp:GridView ID="GridView1" runat="server" Font-Names="Arial" Font-Size="8pt" BackColor="White"
                                                                                BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" CellPadding="3"
                                                                                GridLines="None" AutoGenerateColumns="False"
                                                                                ShowHeader="False" Width="600px" onrowdatabound="GridView1_RowDataBound" 
                                                                                onselectedindexchanged="GridView1_SelectedIndexChanged">
                                                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                                                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                                                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                                <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SNO">
                                                                                        <ItemTemplate>
                                                                                           <asp:Label ID="lblsno" runat="server" Text='<%# Bind("t1")%>'></asp:Label>
                                                                                          <%--  <asp:Label ID="lblsno" runat="server" ></asp:Label>--%>
                                                                                          <%----%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="PARTICULARS">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtDetails" Font-Names="arial" Width="261px" Font-Size="8pt" runat="server"></asp:TextBox>
                                                                                            <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionListCssClass="completionList"
                                                                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                                EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetChargeMaster"
                                                                                                ServicePath="~/AutoComplete.asmx" TargetControlID="txtDetails">
                                                                                            </cc1:AutoCompleteExtender>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Narration" AccessibleHeaderText="Narration">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtNarration" Font-Names="arial" Width="150px" Font-Size="8pt" runat="server"
                                                                                                MaxLength="49"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="RECEIPT DETAILS">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtRecpt" Font-Names="arial" Width="100px" Font-Size="8pt" runat="server"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Stax" AccessibleHeaderText="Stax">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStax" runat="server" Width="50px" Font-Names="Arial" Font-Size="8pt"
                                                                                                onchange="javascript:return Calc();">
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="StaxAmt" AccessibleHeaderText="StaxAmt">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtStaxAmt" Font-Names="arial" Width="50px" Font-Size="8pt" runat="server"
                                                                                                Enabled="True" Text="0.00" onkeydown="return false;"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="amt1" Font-Names="arial" Width="60px" Font-Size="8pt" AutoPostBack="False"
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
                                                        <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 810px;">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label47" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                            Text="Taxable Amount(A)"></asp:Label>
                                                                    </td>
                                                                    <td class="style32">
                                                                        <asp:TextBox ID="SubTotalTax" runat="server" AutoPostBack="false" Font-Names="Arial"
                                                                            Font-Size="8pt" Style="text-align: right" Width="60px" BackColor="#FFD2D2" onkeydown="return false;">0</asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                            Text="Non-Taxable Amount(B)" Width="112px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="SubTotal" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                            Width="60px" onkeydown="return false;">0</asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="lblSTAX0" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                            Text="Total(A+B+C+D+E)"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Totals" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                            Width="70px" BackColor="#FFFFCC" onkeydown="return false;">0</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:DropDownList ID="drServiceTax" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                            Font-Size="8pt" OnSelectedIndexChanged="drServiceTax_SelectedIndexChanged" Visible="False"
                                                                            Width="50px">
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lblSTAX" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                            Text="Service Tax@" Visible="False" Width="60px"></asp:Label>
                                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                            Text="Stax Total Amount(C)"></asp:Label>
                                                                    </td>
                                                                    <td class="style32">
                                                                        <asp:TextBox ID="sTax" runat="server" BackColor="#FFD2D2" Font-Names="Arial" Font-Size="8pt"
                                                                            Style="text-align: right" Width="60px" onkeydown="return false;">0</asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btncalculate" runat="server" Height="25px" OnClick="btncalculate_Click"
                                                                            Style="text-align: center" Text="Calculate" Visible="False" Width="120px" />
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label48" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                            Text="Less Advance" Width="65px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="LessAd" runat="server" AutoPostBack="False" BackColor="#FFFFCC"
                                                                            Font-Names="Arial" Font-Size="8pt" onblur="javascript:return Calc();" Style="text-align: right"
                                                                            Width="70px">0</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                            Text="EDU Cess@2.00% (D)"></asp:Label>
                                                                    </td>
                                                                    <td class="style32">
                                                                        <asp:TextBox ID="EdCess" runat="server" BackColor="#FFD2D2" Font-Names="Arial" Font-Size="8pt"
                                                                            Style="text-align: right" Width="60px" onkeydown="return false;">0</asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label31" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                            Text="SHE Cess @1.00% (E)"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="SHCess" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                                            Width="60px" onkeydown="return false;">0</asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label49" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                            Text="Balance Due"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="BalanceDue" runat="server" BackColor="#FFFFCC" Font-Names="Arial"
                                                                            Font-Size="8pt" Style="text-align: right; margin-left: 0px;" Width="70px" onkeydown="return false;">0</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <table>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td align="left">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td class="style32">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="balance1" runat="server" ForeColor="White" Font-Size="7px"></asp:Label>
                                                                    </td>
                                                                    <td align="right">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="right">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="width: 93px">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="right">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="vertical-align: top;">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 810px;">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="vertical-align: top;">
                                                                        <asp:Label ID="txtRupees" runat="server" Width="500px" Height="23px" Font-Names="Arial"
                                                                            Font-Size="8pt"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="border: 1px solid #2461BF; vertical-align: top;" class="style36">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel1" runat="server" Font-Names="Arial" BorderColor="#9966ff" BackColor="#DEDFDE"
                                                                Font-Size="7pt" GroupingText="Customer Info" Width="350px" ForeColor="#000040">
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 56px; height: 26px;">
                                                                            <asp:Label ID="Label8" runat="server" Text="Party Name" Font-Names="Arial" Font-Size="8pt"
                                                                                Width="85px" ForeColor="Black"></asp:Label>
                                                                        </td>
                                                                        <td style="height: 26px; width: 278px;">
                                                                            <asp:TextBox ID="txtCompName" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"
                                                                                Height="20px" Enabled="True" ReadOnly="True" MaxLength="100"></asp:TextBox>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 56px; height: 26px;">
                                                                            <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                Text="Address"></asp:Label>
                                                                        </td>
                                                                        <td style="height: 26px; width: 278px;">
                                                                            <asp:TextBox ID="txtAdd1" runat="server" Font-Names="Arial" Font-Size="8pt" Height="30px"
                                                                                TextMode="MultiLine" Width="200px" MaxLength="500"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 56px">
                                                                            <asp:Label ID="Label9" runat="server" Text="Sub Party" Font-Names="Arial" Font-Size="8pt"
                                                                                Width="61px" ForeColor="Black"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 278px">
                                                                    
                                                                            <asp:TextBox ID="txtSubParty" onfocus="SetContextKey()" 
                                                                                onkeyup = "SetContextKey()" runat="server" Width="200px"
                                                                                Font-Names="Arial" Font-Size="8pt" AutoPostBack="True" 
                                                                                ontextchanged="txtSubParty_TextChanged1" MaxLength="100"></asp:TextBox>                                                                                
                                                                           <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                                                                MinimumPrefixLength="0" ServiceMethod="GetSubParty" CompletionListCssClass="completionList"
                                                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                ServicePath="~/AutoComplete.asmx" TargetControlID="txtSubParty" UseContextKey = "true" EnableCaching="true">
                                                                            </cc1:AutoCompleteExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 56px">
                                                                            <asp:Label ID="Label64" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                Text="SubParty Addr"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 278px">
                                                                            <asp:TextBox ID="txtSubPaartAddr" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                Height="30px" TextMode="MultiLine" Width="200px" MaxLength="500"></asp:TextBox>
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
                                                                                Font-Size="8pt" ForeColor="Black" Width="102px" Visible="False"></asp:Label>
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
                                                                            <asp:TextBox ID="txtCity" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                                Width="200px" MaxLength="50"></asp:TextBox>
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
                                                                            <asp:Label ID="Label63" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                Text="Inv Seq No" Width="62px"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 278px; height: 20px;">
                                                                            <asp:TextBox ID="txtInvSeqNo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="Panel3" runat="server" BorderColor="#9966ff" BackColor="#DEDFDE" Font-Names="Arial"
                                                                Font-Size="7pt" ForeColor="#000040" GroupingText="Job Info" Width="350px">
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
                                                                            <asp:TextBox ID="txtBENo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="100px"
                                                                                ToolTip="BE NO"></asp:TextBox>
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
                                                                            <asp:TextBox ID="txtNCNTR" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px" onkeyup="javascript:RemoveSpecialChar(this)"></asp:TextBox>
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
                                                                                Height="35px" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="vertical-align: top;">
                                                                            <asp:Label ID="Label46" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                                                Text="Note : -"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" Height="35px"
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
                                                    RequireOpenedPane="false" AutoSize="Limit" Width="600 px" Height="50 px">
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
                                            </tr>
                                            <tr>
                                           <%--<td><div id="testdiv"><table><tr><td></td><td>1</td></td></tr><tr><td>2</td><td>3</td></td></tr></table></div></td>                                         --%>
                                              <td style="border: 1px solid #2461BF; vertical-align: top;" align="right">
                                                <asp:Label ID="lblResult" runat="server"></asp:Label>
                                                <asp:Button ID="Submit" runat="server" Font-Names="Arial" Font-Size="8pt" Height="25px"
                                                    OnClick="Submit_Click" CssClass="button_image1" Text="Submit" Width="80px" />
                                                <asp:Button ID="preview" runat="server" Font-Names="Arial" CssClass="button_image1"
                                                    Font-Size="8pt" Height="25px" OnClick="preview_Click" Text="Print Preview" Width="80px" />
                                                <asp:Button ID="btnMail" runat="server" Font-Names="Arial" CssClass="button_image1"
                                                    Font-Size="8pt" Height="25px" OnClick="btnMail_Click" Text="Mail" Width="50px" />
                                                <asp:Button ID="ExportTally" runat="server" Text="Export Tally" Font-Names="arial"
                                                    Font-Size="8pt" Width="70px" CssClass="button_image1" Height="25px" OnClick="ExportTally_Click" />
                                                <asp:Button ID="BtnExit" runat="server" Font-Names="Arial" CssClass="button_image1"
                                                    Font-Size="8pt" Height="25px" Text="Exit" Width="50px" PostBackUrl="~/Billing/index.aspx"
                                                    OnClick="BtnExit_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td valign="top">
                                <table width="100%">
                                    <tr>
                                        <td style="font-weight: 700; text-align: center">
                                            History
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvFundrequest" runat="server" CellPadding="4" ForeColor="#333333"
                                                GridLines="None" Font-Size="8pt">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Font-Size="8pt" />
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
        <input type="hidden" id="hdnRuppees" runat="server" />
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
                                <asp:Label ID="Label45" runat="server" Text="Select Billing Address : -" Font-Names="Arial"
                                    Font-Size="9pt" ForeColor="#2461bf"></asp:Label>
                                <asp:RadioButtonList ID="rbBill" runat="server" Font-Size="8pt" Font-Names="Arial"
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
                                                                   
                                                                           <tr id="Tr1" runat="server" style="height: 10px;">
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
                                                                        <tr>
                                                    <td>
                                                        <asp:Button ID="BtnContract_Submit" runat="server" Text="Submit" OnClick="BtnContract_Submit_Click" Visible="false" />
                                                        <asp:Button ID="BtnClose" runat="server" Text="Close" OnClick="BtnClose_Click" Visible="false" />
                                                    </td>
                                                </tr>
                                                                 </table>
    </div>

   
                                
                                                                          
</asp:Content>
