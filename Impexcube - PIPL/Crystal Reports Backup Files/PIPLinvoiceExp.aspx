
<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="PIPLinvoiceExp" Codebehind="PIPLinvoiceExp.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <title>::PIPL||Invoice</title>
    <style type="text/css">
    body {
    font-family: verdana;font-size:10px;
    }
    .completionList {
    border:solid 1px #444444;
    margin:0px;
    padding:2px;
    height: 100px;
    overflow:auto;
     background-color:white;
      font-family: verdana;font-size:10px;
    }
    .button_image1
        {
            height: 22px;
            font-family: verdana;
            font-size: 11px;
            color: #ffffff;
            background-color: #013388;
            background-image: url(../images/bg_homepage_right.gif);
            border: 1px solid #ffffff;
        }
    .listItem {
    
    color: #666666;
    background-color:white;
     font-family: verdana;font-size:10px;
    }
    .itemHighlighted{
     background-color: #ffc0c0;
     }
</style>
 <style type="text/css" >
    .grid_scroll
{
    overflow: auto;
    height: 230px;
    
    width: 200px;
}


     .style8
     {
         width: 71px;
     }
     

     .style14
     {
         width: 241px;
     }


     .style21
     {
         width: 220px;
     }


        .style7
        {
            width: 150px;
        }
        

    </style>
    <script type="text/javascript" >
    
    function InvoiceValue()
    {
    var i; 
var sum="";
var staxs="";
var Ecess="";
var Scess="";
var AllTax="";
var tot="";
var less="";
var NetTotal="";

var v1 = parseInt(document.getElementById('GridView1_amt1').value);
	document.getElementById('GridView1_amt1').value=v1.toFixed(2);
	
	 sum = v1.value ;
	
	document.getElementById('SubTotal').value = sum.toFixed(2);
	
var StrSTax = parseFloat(document.getElementById('ServiceTax').value);
   
     sum = parseInt(document.getElementById('SubTotal').value);
	 var strSum=Math.round(sum);
	 document.getElementById('SubTotal').value = strSum.toFixed(2);
	
	 staxs=strSum * (StrSTax/100);
	 Ecess=staxs * 0.02;
	 Scess=staxs * 0.01;
	 
	 
	 var ssTax=parseFloat(staxs);
	 
	 var strssTax=Math.round(ssTax);
	 document.getElementById('sTax').value=strssTax.toFixed(2);
	 
	 var strEcess=Math.round(Ecess);
	 var strScess=Math.round(Scess);
	 document.getElementById('EdCess').value=strEcess.toFixed(2);
	 document.getElementById('SHCess').value=strScess.toFixed(2);
	
	 AllTax=strssTax + strEcess + strScess;
	 
	 tot= strSum + AllTax;
    
     var strTot=Math.round(tot);
	 document.getElementById('Totals').value=tot.toFixed(2);
     less = parseInt(document.getElementById('LessAd').value);
    document.getElementById('LessAd').value=less.toFixed(2);
   
     NetTotal=tot - less;
     var strNetTotal=Math.round(NetTotal);
     document.getElementById('balance1').value= Math.round(NetTotal);
     document.getElementById('BalanceDue').value=strNetTotal.toFixed(2);

	 var strAmount=document.getElementById('balance1').value;
    }
    function Totalcalculate()
{
	var v1 = parseInt(document.getElementById('GridView1_amt1').value);
	document.getElementById('GridView1_amt1').value=v1.toFixed(2);
	
	var sum = v1.value ;
	
	document.getElementById('SubTotal').value = sum.toFixed(2);
	
}

function CallServiceTax() 
{

var i; 
var sum="";
var staxs="";
var Ecess="";
var Scess="";
var AllTax="";
var tot="";
var less="";
var NetTotal="";

var StrSTax = parseFloat(document.getElementById('ServiceTax').value);
   
     sum = parseInt(document.getElementById('SubTotal').value);
	 var strSum=Math.round(sum);
	 document.getElementById('SubTotal').value = strSum.toFixed(2);
	
	 staxs=strSum * (StrSTax/100);
	 Ecess=staxs * 0.02;
	 Scess=staxs * 0.01;
	 
	 
	 var ssTax=parseFloat(staxs);
	 
	 var strssTax=Math.round(ssTax);
	 document.getElementById('sTax').value=strssTax.toFixed(2);
	 
	 var strEcess=Math.round(Ecess);
	 var strScess=Math.round(Scess);
	 document.getElementById('edcess').value=strEcess.toFixed(2);
	 document.getElementById('shcess').value=strScess.toFixed(2);
	
	 AllTax=strssTax + strEcess + strScess;
	 
	 tot= strSum + AllTax;
    
     var strTot=Math.round(tot);
	 document.getElementById('Totals').value=tot.toFixed(2);
     less = parseInt(document.getElementById('LessAd').value);
    document.getElementById('LessAd').value=less.toFixed(2);
   
     NetTotal=tot - less;
     var strNetTotal=Math.round(NetTotal);
     document.getElementById('balance1').value= Math.round(NetTotal);
     document.getElementById('BalanceDue').value=strNetTotal.toFixed(2);

	 var strAmount=document.getElementById('balance1').value;
}

    </script>
    <script type="text/javascript" >
    function LessADvance()
        {
   var tot=parseInt(document.getElementById('Totals').value);
   var less = parseInt(document.getElementById('LessAd').value);
    document.getElementById('LessAd').value=less.toFixed(2);
   
   var NetTotal=tot - less;
     var strNetTotal=Math.round(NetTotal);
     document.getElementById('balance1').value= Math.round(NetTotal);
     document.getElementById('BalanceDue').value=strNetTotal.toFixed(2);

	 var strAmount=document.getElementById('balance1').value;
        }
    </script>


<%--</head>--%>
<%--<body>
    <form id="form1"  method="post"  runat="server">--%>
        <div>

    <br />
   <table style="width: 100%; height: 400px; border: 1px;">
            <tr>
                <td style="vertical-align: top;">
                    <table style="width: 100%;">
                        
                        <tr id="trMain" runat="server">
                            <td align="left" style="height: 90%; vertical-align: top; background-color: white;
                                width: 975px; border: solid 1px #2461bf;">
                                <asp:Panel ID="Panel2" runat="server" Height="620px" Width="100%" 
                                    BackColor="white">
                                    <table style="background-color: Window; ">
                                        <tr>
                                            <td align="left">
                                                <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                    <tbody>
                                                        <tr>
                                                            <td align="center" style="width: 610px; height: 16px">
                                                                <asp:Label ID="lblIName" runat="server" Font-Names="arial" Font-Size="10pt" Font-Bold="True"></asp:Label>
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
                                                                <asp:Label ID="lblInvNo" runat="server" Width="145px" Font-Bold="True" Font-Names="Arial"
                                                                    Font-Size="7pt"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label7" runat="server" Text="Suffix " Font-Names="Arial" 
                                                                    Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSuffix" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 198px; height: 17px;">
                                                                <asp:Label ID="Label42" runat="server" Text="Date :" Font-Names="Arial" Font-Size="8pt"
                                                                    Font-Bold="True"></asp:Label>
                                                                <asp:TextBox ID="invDate" runat="server" Width="100px" Font-Names="Arial" 
                                                                    Font-Size="8pt"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="invDate"></cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label6" runat="server" Text="Job No" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                                <asp:CheckBox ID="chk" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt"
                                                                    OnCheckedChanged="chk_CheckedChanged" />
                                                            </td>
                                                            <td align="left" style="height: 26px;">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:DropDownList ID="drJobNo" runat="server" Width="100px" Font-Names="Arial" Font-Size="8pt"
                                                                                        OnTextChanged="drJobNo_TextChanged" AutoPostBack="True">
                                                                                    </asp:DropDownList>
                                                                                    <br />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="BtnStandard" runat="server" Text="Go" Font-Size="8pt" OnClick="BtnStandard_Click"
                                                                                Width="41px" CssClass="button_image1" />
                                                                        </td>
                                                                        <td class="style14">
                                                                            <asp:Button ID="Button1" runat="server" Font-Names="Arial" Font-Size="8pt" OnClick="Button1_Click"
                                                                                Text="Copy From Previous Bill" Visible="False" Width="150px" CssClass="button_image1" />
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
                                                            <td align="left" style="vertical-align: top;" class="style8">
                                                                <asp:Label ID="Label22" runat="server" Text="S.No" Font-Size="8pt" 
                                                                    Font-Names="Arial" Font-Bold="False"></asp:Label>
                                                            </td>
                                                            <td style="vertical-align: top;" align="center" class="style21" >
                                                                <asp:Label ID="Label23" runat="server" Text="PARTICULARS" Font-Size="8pt" 
                                                                    Font-Names="Arial" Font-Bold="True"></asp:Label>
                                                            </td>
                                                            <td align="center" style="vertical-align: top;" >
                                                                <asp:Label ID="Label24" runat="server" Text="RECEIPT DETAILS" Font-Size="8pt" 
                                                                    Font-Names="Arial" Font-Bold="False"></asp:Label>
                                                            </td>
                                                            <td align="center" style="vertical-align: top;" >
                                                                <asp:Label ID="Label25" runat="server" Text="TAX" Font-Size="8pt" 
                                                                    Font-Names="Arial" Font-Bold="False"></asp:Label>
                                                            </td>
                                                            <td align="center" style="vertical-align: top;" >
                                                                <asp:Label ID="Label26" runat="server" Text="AMOUNT Rs." Font-Size="8pt" 
                                                                    Font-Names="Arial" Font-Bold="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 479px;
                                                            height: 246px;">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="vertical-align: top;" class="style29">
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
                                                                                            <asp:TextBox ID="txtRecpt" Font-Names="arial" Width="100px" Font-Size="8pt" runat="server"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="S.TAX">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkSTAX" runat="server" Font-Names="arial" Font-Size="8pt" AutoPostBack="True"
                                                                                                OnCheckedChanged="chkSTAX_CheckedChanged" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="amt1" Font-Names="arial" Width="60px" Font-Size="8pt" OnTextChanged="amt1_TextChanged"
                                                                                                AutoPostBack="true" Style="text-align: right" runat="server"></asp:TextBox>
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
                                                            <td align="right" >
                                                            
                                                                <asp:Label ID="Label47" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                                    Font-Size="7pt" Text="Taxable Amount"></asp:Label>
                                                            
                                                            </td>
                                                            <td class="style32">
                                                            
                                                                <asp:TextBox ID="SubTotalTax" runat="server" AutoPostBack="True" 
                                                                    Font-Names="Arial" Font-Size="8pt" Style="text-align: right" Width="60px" 
                                                                    BackColor="#FFD2D2">0</asp:TextBox>
                                                            
                                                                
                                                            
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                                    Font-Size="7pt" Text="Non-Taxable Amount" Width="95px"></asp:Label>
                                                                </td>
                                                            <td>
                                                            
                                                                <asp:TextBox ID="SubTotal" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                    Style="text-align: right" Width="60px">0</asp:TextBox>
                                                            
                                                            </td>
                                                            <td align="right" >
                                                            
                                                                <asp:Label ID="lblSTAX0" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                                    Font-Size="7pt" Text="Total"></asp:Label>
                                                            
                                                            </td>
                                                            <td>
                                                            
                                                                <asp:TextBox ID="Totals" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                    Style="text-align: right" Width="70px" BackColor="#FFFFCC">0</asp:TextBox>
                                                            
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                            
                                                            <td align="left">
                                                                        <table>
                                                                            <tr>
                                                                                <td  align="right">
                                                                                    <asp:Label ID="lblSTAX" runat="server" Width="60px" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                                                                                        Text="Service Tax@"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="drServiceTax" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                        Width="50px" AutoPostBack="True" OnSelectedIndexChanged="drServiceTax_SelectedIndexChanged">
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
                                                                       <asp:Button ID="btncalculate" runat="server" Height="23px" 
                                                                           onclick="btncalculate_Click" Text="Calculate" Width="150px" />
                                                                </td>
                                                                     <td align="right">
                                                                         <asp:Label ID="Label48" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                                             Font-Size="7pt" Text="Less Advance" Width="65px"></asp:Label>
                                                                    </td>
                                                                    <td >
                                                                        <asp:TextBox ID="LessAd" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt"
                                                                            onblur="LessADvance()" OnTextChanged="LessAd_TextChanged" Style="text-align: right"
                                                                            Width="70px" BackColor="#FFFFCC">0</asp:TextBox>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                             <td align="right">
                                                                        <asp:Label ID="Label30" runat="server" Text="EDU Cess@2.00%" Font-Names="Arial"
                                                                            Font-Size="7pt" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                    <td >
                                                                        <asp:TextBox ID="EdCess" runat="server" Width="60px" Font-Names="Arial" Font-Size="8pt"
                                                                            Style="text-align: right" BackColor="#FFD2D2">0</asp:TextBox>
                                                                    </td>
                                                             <td align="right">
                                                                        <asp:Label ID="Label31" runat="server" Text="SHE Cess @1.00%"
                                                                            Font-Names="Arial" Font-Size="7pt" Font-Bold="True" ></asp:Label>
                                                                    </td>
                                                                    <td style="width: 93px">
                                                                        <asp:TextBox ID="SHCess" runat="server" Width="60px" Font-Names="Arial" Font-Size="8pt"
                                                                            Style="text-align: right">0</asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label49" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                                            Font-Size="7pt" Text="Balance Due"></asp:Label>
                                                                    </td>
                                                                    <td style="vertical-align: top;" >
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
                                                                    
                                                                    <asp:TextBox ID="txtRupees" runat="server" Font-Names="Arial" Font-Size="7pt" 
                                                                        Height="20px" Width="420px" BorderStyle="None"></asp:TextBox>
                                                                    </td>
                                                                    
                                                                    <td >
                                                                        <asp:TextBox ID="balance1" runat="server" BackColor="White" BorderStyle="Solid" 
                                                                            ForeColor="White" Width="67px">0</asp:TextBox>
                                                                    </td>
                                                                   
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="border: 1px solid #2461BF; vertical-align: top;">
                                              <table >
                                              <tr>
                                              <td>
                                               <asp:Panel ID="Panel1" runat="server" Font-Names="Arial" BackColor="#DEDFDE" Font-Size="7pt" 
                                                    GroupingText="Customer Info" Width="400px" ForeColor="#000040">
                                                <table>
                                                <tr>
                                                        <td style="width: 56px; height: 26px;">
                                                            <asp:Label ID="Label8" runat="server" Text="Party Name" Font-Names="Arial" 
                                                                Font-Size="8pt" Width="85px" ForeColor="Black"></asp:Label>
                                                        </td>
                                                        <td style="height: 26px; width: 278px;">
                                                            <asp:TextBox ID="txtCompName" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"
                                                                Height="20px"></asp:TextBox>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 56px">
                                                            <asp:Label ID="Label9" runat="server" Text="Sub Party" Font-Names="Arial" Font-Size="8pt"
                                                                Width="61px" ForeColor="Black"></asp:Label>
                                                        </td>
                                                        <td style="width: 278px">
                                                            <asp:TextBox ID="txtSubParty" runat="server" Width="200px" Font-Names="Arial"
                                                                Font-Size="8pt" AutoPostBack="True" 
                                                                ontextchanged="txtSubParty_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style7">
                                                            <asp:Label ID="Label50" runat="server" Text="Tally Account Name" Font-Names="Arial" 
                                                                Font-Size="8pt" ForeColor="Black" Width="100px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlTallyAccountName" runat="server" 
                                                                AppendDataBoundItems="True" Font-Names="Arial" Font-Size="8pt" Width="200px">
                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style7">
                                                            <asp:Label ID="Label51" runat="server" Text="Tally Sub Party Name" Font-Names="Arial" 
                                                                Font-Size="8pt" ForeColor="Black" Width="120px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlTallySubPartyName" runat="server" 
                                                                AppendDataBoundItems="True" Enabled="False" Font-Names="Arial" Font-Size="8pt" 
                                                                Width="200px">
                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 56px; vertical-align: top;">
                                                            <asp:Label ID="Label10" runat="server" Text="Address" Font-Names="Arial" 
                                                                Font-Size="8pt" ForeColor="Black"></asp:Label>
                                                        </td>
                                                        <td style="width: 278px; ">
                                                            <asp:TextBox ID="txtAdd1" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Width="200px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style23">
                                                            <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                ForeColor="Black" Text="City"></asp:Label>
                                                        </td>
                                                        <td class="style24">
                                                            <asp:TextBox ID="txtCity" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 56px; height: 20px;">
                                                            <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                ForeColor="Black" Text="Party Ref" Width="62px"></asp:Label>
                                                        </td>
                                                        <td style="width: 278px; height: 20px;">
                                                            <asp:TextBox ID="txtParty_Reff" runat="server" Font-Names="Arial" 
                                                                Font-Size="8pt" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                </asp:Panel>
                                                  <br />
                                                <asp:Panel ID="Panel3" runat="server" Font-Names="Arial" BackColor="#DEDFDE" Font-Size="7pt" 
                                                    GroupingText="Job Info" Width="400px" ForeColor="#000040">
                                                <table >
                                                    
                                                    <tr>
                                                        <td style="width: 89px; height: 26px;">
                                                            <asp:Label ID="Label14" runat="server" Text="Job No." Font-Names="Arial" 
                                                                Font-Size="8pt" ForeColor="Black"></asp:Label>
                                                        </td>
                                                        <td style="height: 26px; width: 138px;">
                                                            <asp:TextBox ID="txtJobNo" runat="server" Width="200px" Font-Names="Arial" 
                                                                Font-Size="8pt"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 89px">
                                                            <asp:Label ID="Label15" runat="server" Text="AWB / BL No." Font-Names="Arial" Font-Size="8pt"
                                                                Width="91px" Height="16px" ForeColor="Black"></asp:Label>
                                                        </td>
                                                        <td style="width: 138px">
                                                            <asp:TextBox ID="txtBLNo" runat="server" Width="200px" Font-Names="Arial" 
                                                                Font-Size="8pt"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 89px">
                                                            <asp:Label ID="Label16" runat="server" Text="BE NO./DT." Font-Names="Arial" 
                                                                Font-Size="8pt" ForeColor="Black"></asp:Label>
                                                        </td>
                                                        <td style="width: 138px">
                                                            <asp:TextBox ID="txtBENo" runat="server" Width="200px" Font-Names="Arial" 
                                                                Font-Size="8pt"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                Text="Ass. Value" ForeColor="Black"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAssValue" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 89px; height: 20px;">
                                                            <asp:Label ID="Label27" runat="server" Text="Custom Duty" Font-Names="Arial" 
                                                                Font-Size="8pt" ForeColor="Black"></asp:Label>
                                                        </td>
                                                        <td style="height: 20px; width: 138px;">
                                                            <asp:TextBox ID="txtCustomDuty" runat="server" Width="200px" Font-Names="Arial" 
                                                                Font-Size="8pt"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 89px; height: 20px;">
                                                            <asp:Label ID="Label21" runat="server" Text="No.of Container" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black"></asp:Label>
                                                        </td>
                                                        <td style="height: 20px; width: 138px;">
                                                            <asp:TextBox ID="txtNCNTR" runat="server" Width="200px" Font-Names="Arial" 
                                                                Font-Size="8pt"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style25">
                                                            <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                Text="Quantity" ForeColor="Black"></asp:Label>
                                                        </td>
                                                        <td class="style26">
                                                            <asp:TextBox ID="txtQty" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 89px; vertical-align: top;">
                                                            <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                Text="Item Imported" ForeColor="Black"></asp:Label>
                                                        </td>
                                                        <td style="width: 138px">
                                                            <asp:TextBox ID="txtImpotItem" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Width="200px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td style="vertical-align: top;">
                                                        <asp:Label ID="Label46" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                            ForeColor="Black" Text="Note : -"></asp:Label>
                                                        </td>
                                                    <td>
                                                    <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" Height="40px"
                                                                            TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                                            </td> 
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td align="right" style="vertical-align: top;">
                                                            <asp:Label ID="Label52" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                ForeColor="Black" Text="Supplier Inv No"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:CheckBox ID="chkSupplierInvNo" runat="server" AutoPostBack="True" 
                                                                oncheckedchanged="chkSupplierInvNo_CheckedChanged" />
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
                                         <td align="right" colspan="2" style="border: 1px solid #2461BF; " >
                                                    
                                                        <asp:Label ID="lblResult" runat="server"></asp:Label>
                                                    
                                                        <asp:Button ID="Submit" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                            Height="25px" OnClick="Submit_Click" Text="Submit" Width="100px" CssClass="button_image1" />
                                                        <asp:Button ID="preview" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                            Height="25px" OnClick="preview_Click" Text="Print Preview" Width="80px" CssClass="button_image1" />
                                                        <asp:Button ID="btnMail" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                            Height="25px" OnClick="btnMail_Click" Text="Mail" Width="50px" CssClass="button_image1" />
                                                    
                                                        <asp:Button ID="Button2" runat="server" CssClass="button_image1" 
                                                            Font-Names="Arial" Font-Size="8pt" Height="25px" onclick="Button2_Click" 
                                                            Text="Exit" Width="50px" PostBackUrl="~/index.aspx" />
                                                    
                                                    </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr id="TrAddr1" runat="server" style="height: 10px;">
                            <td align="left">
                                <asp:Label ID="Label45" runat="server" Text="Select Billing Address : -" Font-Names="Arial"
                                    Font-Size="9pt" ForeColor="#2461bf"></asp:Label>
                                <asp:RadioButtonList ID="rbBill" runat="server" Font-Size="8pt" Font-Names="Arial"
                                    Width="159px" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbBill_SelectedIndexChanged">
                                    <asp:ListItem Value="DP">Direct Party</asp:ListItem>
                                    <asp:ListItem Value="TP">Third Party</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="TrAddr" runat="server">
                            <td style="vertical-align: top;" align="left">
                                <div id="GrdADDRSCROLL" class="grid_scroll" style="width: 700px; height: 200px;"
                                    runat="server">
                                    <asp:GridView ID="GrdPaddr" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" DataKeyNames="addr_num"
                                        Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="GrdPaddr_SelectedIndexChanged"
                                        Width="674px">
                                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                        <Columns>
                                            <asp:BoundField DataField="party_code" HeaderText="PCODE" SortExpression="party_code" />
                                            <asp:BoundField DataField="addr_num" HeaderText="Branch" SortExpression="addr_num" />
                                            <asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />
                                            <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />
                                            <asp:BoundField DataField="state" HeaderText="State" SortExpression="Pin" />
                                            <asp:BoundField DataField="pin" HeaderText="Pin" SortExpression="pin" />
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
                    </table>
                </td>
            </tr>
        </table>
            
            
          
           
       
        </div>
       
    <script type="text/javascript" >
function RestrictInt(val)
 {
    if(isNaN(val))
    {
      val = val.substring(0, val.length-1);
      document.form1.txtPhone.value=val;
      return false;
    }
    return true;
 }
</script>

      </asp:Content>
  
   <%-- </form>
</body>
</html>
--%>