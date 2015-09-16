<%@ Page Language="C#" AutoEventWireup="true" Inherits="PIPLDebitATL" Codebehind="PIPLDebitATL.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::PIPL||Apollo Debit Note</title>
     <style type="text/css">
.accordionContent {

width:100%;
}
.accordionHeaderSelected {
color: red;
cursor: pointer;
font-family: Arial,Sans-Serif;
font-size: 9px;
font-weight: bold;
margin-top: 1px;
padding: 1px;
width:100%;
}
.accordionHeader {
color: blue;
cursor: pointer;
font-family: Arial,Sans-Serif;
font-size: 9px;
font-weight: normal;
margin-top: 1px;
padding: 1px;
width:100%;
}
.href
{
color:green; 
font-weight:bold;
text-decoration:none;
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
    
	height: 22px;
	font-family: verdana;
	font-size: 11px;
	color: #ffffff;
	background-color: #013388;
	background-image: url(../images/bg_homepage_right.gif);
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
        .style4
        {
            width: 60px;
            height: 54px;
        }
        .style5
        {
            width: 80px;
            height: 54px;
        }
        .style6
        {
            width: 100px;
            height: 54px;
        }
        .style7
        {
            height: 54px;
        }
    </style>

    <script type="text/javascript">

function calculate()
{
	var v1 = parseInt(document.getElementById('ppaid1').value);
	document.getElementById('ppaid1').value=v1.toFixed(2);
	var v2 = parseInt(document.getElementById('ppaid2').value);
	document.getElementById('ppaid2').value=v2.toFixed(2);
	var v3 = parseInt(document.getElementById('ppaid3').value);
	document.getElementById('ppaid3').value=v3.toFixed(2);
	var v4 = parseInt(document.getElementById('ppaid4').value);
	document.getElementById('ppaid4').value=v4.toFixed(2);
	var v5 = parseInt(document.getElementById('ppaid5').value);
	document.getElementById('ppaid5').value=v5.toFixed(2);
	var v6 = parseInt(document.getElementById('ppaid6').value);
	document.getElementById('ppaid6').value=v6.toFixed(2);
	var v7 = parseInt(document.getElementById('ppaid7').value);
	document.getElementById('ppaid7').value=v7.toFixed(2);
	var v8 = parseInt(document.getElementById('ppaid8').value);
	document.getElementById('ppaid8').value=v8.toFixed(2);
	var v9 = parseInt(document.getElementById('ppaid9').value);
	document.getElementById('ppaid9').value=v9.toFixed(2);
	var v10 = parseInt(document.getElementById('ppaid10').value);
	document.getElementById('ppaid10').value=v10.toFixed(2);

	
	var sum = v1 + v2 + v3 + v4 + v5 + v6 + v7 + v8 + v9 + v10;
	
	document.getElementById('subpaidtotal').value = sum.toFixed(2);
	document.getElementById('LessAd').value= sum.toFixed(2);
}

function Totalcalculate()
{
	var v1 = parseInt(document.getElementById('amt1').value);
	document.getElementById('amt1').value=v1.toFixed(2);
	var v2 = parseInt(document.getElementById('amt2').value);
	document.getElementById('amt2').value=v2.toFixed(2);
	var v3 = parseInt(document.getElementById('amt3').value);
	document.getElementById('amt3').value=v3.toFixed(2);
	var v4 = parseInt(document.getElementById('amt4').value);
	document.getElementById('amt4').value=v4.toFixed(2);
	var v5 = parseInt(document.getElementById('amt5').value);
	document.getElementById('amt5').value=v5.toFixed(2);
	var v6 = parseInt(document.getElementById('amt6').value);
	document.getElementById('amt6').value=v6.toFixed(2);
	var v7 = parseInt(document.getElementById('amt7').value);
	document.getElementById('amt7').value=v7.toFixed(2);
	var v8 = parseInt(document.getElementById('amt8').value);
	document.getElementById('amt8').value=v8.toFixed(2);
	var v9 = parseInt(document.getElementById('amt9').value);
	document.getElementById('amt9').value=v9.toFixed(2);
	var v10 = parseInt(document.getElementById('amt10').value);
	document.getElementById('amt10').value=v10.toFixed(2);

	
	var sum = v1 + v2 + v3 + v4 + v5 + v6 + v7 + v8 + v9 + v10;
	
	
	
	document.getElementById('SubTotal').value = sum.toFixed(2);
	var tot= sum;

	
  var less = parseInt(document.getElementById('LessAd').value);
  document.getElementById('LessAd').value=less.toFixed(2);
  var NetTotal=sum - less;
 
 document.getElementById('balance1').value= Math.round(NetTotal);
	document.getElementById('BalanceDue').value=NetTotal.toFixed(2);
}


    </script>

    <script type="text/javascript">
    function LessADvance()
        {
   var tot=parseInt(document.getElementById('SubTotal').value);
   var less = parseInt(document.getElementById('LessAd').value);
    document.getElementById('LessAd').value=less.toFixed(2);
   
   var NetTotal=tot - less;
     var strNetTotal=Math.round(NetTotal);
     document.getElementById('balance1').value= Math.round(NetTotal);
     document.getElementById('BalanceDue').value=strNetTotal.toFixed(2);

	 var strAmount=document.getElementById('balance1').value;
        }
    </script>

</head>
<body>
    <form id="form1" method="post" runat="server">
    <div style="vertical-align: top;">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="AutoComplete.asmx" />
            </Services>
        </asp:ScriptManager>
       
        <div style="vertical-align: top;">
           
            <table style="z-index: 101; right: 15px; position: absolute; top: 0px">
                <tr>
                    <td>
                        <asp:Label ID="lblCompName" runat="server" Text="" Font-Bold="True"
                            Font-Names="Arial" Font-Size="8pt" Height="18px"></asp:Label>
                    </td>
                </tr>
            </table><br />
            <table style="width: 100%; height: 400px; border: 1px;">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            
                            <tr style="background-color: #2461bf;">
                                <td style="width: 100%;">
                                    <table style="background-color: #2461bf; width: 100%">
                                        <tr>
                                            <td align="left"  style="width: 75%">
                                                 <table >
                <tr>
                    <td>
                        <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt"
                            Text="User Name :" ForeColor="White"></asp:Label>
                        <asp:Label ID="lblUser" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt"
                            ForeColor="White"></asp:Label>
                    </td>
                    <td style="width: 7px">
                        <asp:Label ID="Label29" runat="server" ForeColor="White" Text="||" 
                            Font-Bold="True" Font-Size="8pt"></asp:Label>
                    </td>
                    <td>
                        <asp:LinkButton ID="LB_Logout" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" Font-Underline="False" ForeColor="White" OnClick="LB_Logout_Click"
                            CausesValidation="False">Sign Out</asp:LinkButton>
                    </td>
                </tr>
            </table>
                                                </td>
                                            <td align="left" style="width: 25%">
                            
                                <asp:Menu ID="Menu2" runat="server" Font-Names="Arial" Font-Size="9pt" Orientation="Horizontal"
                                    Font-Bold="True" ForeColor="White" Width="500px" DynamicVerticalOffset="12" OnMenuItemClick="Menu2_MenuItemClick"
                                    Height="30px" BackColor="#2461BF">
                                    <Items>
                                        <asp:MenuItem Text="Home  |" Value="Home" NavigateUrl="~/index.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Master" Value="Master">
                                            <asp:MenuItem NavigateUrl="~/frmChargeDtl.aspx" Text="Charge Info" Value="Charge Info">
                                            </asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/BankMaster.aspx" Text="Bank Info" Value="Bank Info">
                                            </asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Contract" Value="Contract">
                                            <asp:MenuItem NavigateUrl="~/frmContract.aspx" Text="New Contract" Value="New Contract">
                                            </asp:MenuItem>
                                            <asp:MenuItem Text="Edit Contract" Value="Edit Contract"></asp:MenuItem>
                                            <asp:MenuItem Text="Renewal Contract" Value="Renewal Contract"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Contract Billing " Value="Contract Billing Info">
                                            <asp:MenuItem NavigateUrl="~/frmContractInvoice.aspx" Text="Import - Billing" 
                                                Value="Imports">
                                            </asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/frmContractInvoiceExp.aspx" Text="Export - Billing" 
                                                Value="Export Billing"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/frmContractInvoiceATL.aspx" Text="ATL - Billing" Value="ATL - Billing">
                                            </asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/frmATLEnggBill.aspx" Text="ATL - DEBP Billing" 
                                                Value="ATL - DEBP Billing"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/frmEditInvoice.aspx" Text="Edit Billing" 
                                                Value="Edit Billing">
                                            </asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Direct Billing" Value="Bill Info">
                                            <asp:MenuItem NavigateUrl="~/PIPLinvoiceSTAX.aspx" Text="Invoice - Import" 
                                                Value="Invoice">
                                            </asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/PIPLinvoiceExp.aspx" Text="Invoice - Export" 
                                                Value="Invoice - Export"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/PIPLDebit.aspx" Text="Debit Note - Import" 
                                                Value="Debit Note">
                                            </asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/PIPLDebitExp.aspx" Text="Debit Note - Export" 
                                                Value="Debit Note - Export"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/PIPLDebitATL.aspx" Text="Debit Note - ATL Demurrage"
                                                Value="Debit Note - ATL Demurrage"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/PIPLDebitCD.aspx" Text="Debit Note - Custom Duty" Value="Custom Duty">
                                            </asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/PIPLinvoiceSTAXTrans.aspx" 
                                                Text="Transport Billing" Value="Transport Billing"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/frmEditInvoice.aspx" Text="Edit Billing" 
                                                Value="Edit Billing">
                                            </asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Enclose.aspx" Text="Encloser" Value="Enclose"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/FundREQ/frmAccount.aspx" Text="Fund Requested" Value="Fund Requested">
                                            </asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/PIPLinvoiceSTAXTrans.aspx" Text="Add Invoice" 
                                                Value="Add Invoice"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Multiple Billing" Value="Multiple Billing">
                                            <asp:MenuItem NavigateUrl="~/frmjobupdate.aspx" Text="Multiple Bill Update" 
                                                Value="Multiple Bill Update"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/frmMutipleBillingJobUpdate.aspx" 
                                                Text="Multiple Job Billing" Value="Multiple Job Billing"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Print Info" Value="Print">
                                            <asp:MenuItem NavigateUrl="~/frmPrint.aspx" Text="Invoice/Debit Note" Value="Invoice/Debit Note">
                                            </asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/frmPrintInvoice.aspx" Text="Print Billing" 
                                                Value="Print Billing"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/frmPrintInvoiceAll.aspx" 
                                                Text="Print Billing Reports" Value="Print Billing Reports"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/frmPrintDemand.aspx" Text="DD Request to Bank" Value="Demand Draft">
                                            </asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/ExcellReports/frmSearchACC.aspx" Text="Fund Reports"
                                                Value="Fund Reports"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Report" Value="Report">
                                            <asp:MenuItem NavigateUrl="~/ExcellReports/frmContractReport.aspx" Text="Contract Info"
                                                Value="Contract Info"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/ExcellReports/frmBillingReport.aspx" Text="Billing Info"
                                                Value="Billing Info"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/ExportToTally.aspx" Text="Export Tally" Value="Export Tally">
                                            </asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/ExcellReports/frmBillingSummary.aspx" Text="Billing Summary" 
                                                Value="Billing Summary"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/ExcellReports/frmBillingSummaryReport.aspx" 
                                                Text="Billing Summary Report" Value="Billing Summary Report"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/frmBillingLedger.aspx" Text="Billing Ledger Info" 
                                                Value="Billing Ledger Info"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Exit" Value="Exit"></asp:MenuItem>
                                    </Items>
                                    <StaticHoverStyle ForeColor="DodgerBlue" BackColor="#2461BF" Font-Names="Arial" Font-Size="9pt" />
                                    <DynamicMenuItemStyle BackColor="#2461BF" ForeColor="White" ItemSpacing="1px" VerticalPadding="5px"
                                        BorderColor="DarkGray" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                                        Font-Size="9pt" />
                                    <DynamicHoverStyle ForeColor="#FF8080" Font-Names="Arial" Font-Size="9pt" />
                                    <StaticMenuStyle BackColor="#2461BF" BorderStyle="None" />
                                    <StaticMenuItemStyle BackColor="#2461BF" Font-Names="Arial" Font-Size="9pt" />
                                    <DynamicMenuStyle BackColor="#2461BF" />
                                    <StaticSelectedStyle BackColor="#2461BF" Font-Names="Arial" Font-Size="9pt" />
                                    <DynamicSelectedStyle BackColor="#2461BF" Font-Names="Arial" Font-Size="9pt" />
                                </asp:Menu>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trMain" runat="server">
                                <td align="left"  style="height: 90%; vertical-align: top; background-color: white; width: 975px;">
                                    <asp:Panel ID="Panel2" runat="server" Height="553px" Width="100%" 
                                        BackColor="white">
                                       
                                            <table>
                                                <tr>
                                                    <td style="vertical-align: top;" align="left">
                                                        <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="center" style="width: 610px; height: 16px">
                                                                        <asp:Label ID="lblIName" runat="server" Text="IMPORTS - APOLLO DEBIT NOTE" Font-Bold="True"
                                                                            Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;">
                                                            <tbody>
                                                                <tr style="border-style: solid; border-width: 1px; border-color: #2461BF;">
                                                                    <td style="width:300px;" id="a1">
                                                                        <asp:Label ID="lblINumber" runat="server" Text="INV. NO." Font-Names="Arial" Font-Size="8pt"
                                                                            Font-Bold="True"></asp:Label>
                                                                        <asp:TextBox ID="lblInvNo" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="left">
                                                                        &nbsp;</td><td>
                                                                        &nbsp;</td>
                                                                    <td align="right"  style="width: 398px; height: 17px;">
                                                                        <asp:Label ID="Label42" runat="server" Text="Date :" Font-Names="Arial" Font-Size="8pt"
                                                                            Font-Bold="True"></asp:Label>
                                                                        <asp:TextBox ID="invDate" runat="server" Width="80px" Font-Names="Arial" 
                                                                            Font-Size="8pt"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="invDate"></cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table style="border: 1px solid #2461BF; height:39px; width: 532px;">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="vertical-align: top;" align="left" class="style4">
                                                                        <asp:Label ID="Label6" runat="server" Text="Job No" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                                        </td>
                                                                    <td align="left"  style="vertical-align: top;" class="style5">
                                                                        <asp:CheckBox ID="chk" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt"
                                                                            OnCheckedChanged="chk_CheckedChanged" />
                                                                    </td>
                                                                    <td align="left" style="vertical-align: top;" class="style6">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">--%>
                                                                                        <ContentTemplate>
                                                                                            <asp:DropDownList ID="drJobNo" runat="server" Width="100px" Font-Names="Arial" Font-Size="8pt"
                                                                                                OnTextChanged="drJobNo_TextChanged" 
                                                                                                >
                                                                                            </asp:DropDownList>
                                                                                            <br />
                                                                                        </ContentTemplate>
                                                                                   <%-- </asp:UpdatePanel>--%>
                                                                                </td>
                                                                                
                                                                                
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td align="left" style="vertical-align: top;" class="style7">
                                                                                    <asp:Button ID="BtnStandard" runat="server" Text="Go" Font-Size="8pt" OnClick="BtnStandard_Click"
                                                                                        Width="41px" CssClass="button_image1" Height="25px" />
                                                                                </td>
                                                                                <td class="style7" >
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
                                                                        <asp:Label ID="Label24" runat="server" Text="RECEIPT DETAILS" Font-Size="8pt" Font-Names="Arial"
                                                                            Font-Bold="False"></asp:Label>
                                                                    </td>
                                                                    
                                                                    <td align="center" style="vertical-align: top;">
                                                                        <asp:Label ID="Label26" runat="server" Text="AMOUNT Rs." Font-Size="8pt" Font-Names="Arial"
                                                                            Font-Bold="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
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
                                                                                                    <asp:TextBox ID="txtDetails"  Font-Names="arial" Width="250px" Font-Size="8pt" runat="server"></asp:TextBox>
                                                                                                    <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionListCssClass="completionList"
                                                                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetChargeMaster"
                                                                                                        ServicePath="~/AutoComplete.asmx" TargetControlID="txtDetails"></cc1:AutoCompleteExtender>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="RECEIPT DETAILS">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtRecpt" Font-Names="arial" Width="100px" Font-Size="8pt" runat="server"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="AMOUNT">
                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="amt1" Font-Names="arial" Width="80px" Font-Size="8pt" 
                                                                                                        OnTextChanged="amt1_TextChanged" Style="text-align: right" runat="server"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FTE1" TargetControlID="amt1" FilterType="Custom"
                                                                                                        ValidChars="0123456789." runat="server"></cc1:FilteredTextBoxExtender>
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
                                                                                <asp:TextBox ID="balance1" runat="server" BackColor="White" BorderStyle="Solid" 
                                                                                    ForeColor="White" Width="67px">0</asp:TextBox>
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
                                                                                <br />
                                                                                <asp:Button ID="btncalculate" runat="server" Height="25px" 
                                                                                    onclick="btncalculate_Click" Text="Calculate" Width="120px" />
                                                                                <br />
                                                                            </td>
                                                                            <td align="right" colspan="2">
                                                                                &nbsp;<asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                                    Font-Size="7pt" Text="Less Advance Recd." Width="150px"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 93px">
                                                                                <asp:TextBox ID="LessAd" runat="server" Width="80px" Style="text-align: right" Font-Names="Arial"
                                                                                    onblur="LessADvance()" OnTextChanged="LessAd_TextChanged" Font-Size="8pt" 
                                                                                    AutoPostBack="True" BackColor="#FFFFCC">0</asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 10px">
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        
                                                                        <tr>
                                                                            <td class="style1">
                                                                                
                                                                            </td>
                                                                            <td align="right" colspan="2" class="style1">
                                                                                <asp:Label ID="Label34" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                                                    Font-Size="7pt" Text="Balance Due"></asp:Label>
                                                                            </td>
                                                                            <td class="style2">
                                                                                <asp:TextBox ID="BalanceDue" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                                    Style="text-align: right" Width="81px" BackColor="#FFFFCC">0</asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 532px;
                                                                    border-bottom-color: #2461BF;">
                                                                    <tbody>
                                                                        <tr>
                                                                <td style="vertical-align: top;" class="style28">
                                                                    
                                                                    <asp:Label ID="txtRupees" runat="server" Height="23px" Width="500px" 
                                                                        Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                                    </td>
                                                                    
                                                                    <td >
                                                                        &nbsp;</td>
                                                                   
                                                                </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ContentTemplate>
                                                       <%-- </asp:UpdatePanel>--%>
                                                    </td>
                                                    <td align="left"  style="border: 1px solid #2461BF; vertical-align: top;">
                                              <table >
                                              <tr>
                                              <td>
                                               <asp:Panel ID="Panel1" runat="server" Font-Names="Arial" BackColor="#DEDFDE" Font-Size="7pt" 
                                                    GroupingText="Customer Info" Width="350px" ForeColor="#000040">
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
                                                        <td style="width: 56px; height: 26px;">
                                                            <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                ForeColor="Black" Text="Address"></asp:Label>
                                                        </td>
                                                        <td style="height: 26px; width: 278px;">
                                                            <asp:TextBox ID="txtAdd1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                Height="40px" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 56px">
                                                            <asp:Label ID="Label9" runat="server" Text="Sub Party" Font-Names="Arial" Font-Size="8pt"
                                                                Width="61px" ForeColor="Black"></asp:Label>
                                                        </td>
                                                        <td style="width: 278px">
                                                            <asp:TextBox ID="txtSubParty" runat="server" Width="200px" Font-Names="Arial"
                                                                Font-Size="8pt"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 56px">
                                                            <asp:Label ID="Label54" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                ForeColor="Black" Text="SubParty Addr" Width="61px"></asp:Label>
                                                        </td>
                                                        <td style="width: 278px">
                                                            <asp:TextBox ID="txtSubPartyAddr" runat="server" Font-Names="Arial" 
                                                                Font-Size="8pt" Height="40px" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label50" runat="server" Text="Tally Account Name" Font-Names="Arial" 
                                                                Font-Size="8pt" ForeColor="Black" Width="100px" Visible="False"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlTallyAccountName" runat="server" 
                                                                AppendDataBoundItems="True" Font-Names="Arial" Font-Size="8pt" 
                                                                Width="200px" Visible="False">
                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label51" runat="server" Text="Tally Sub Party Name" Font-Names="Arial" 
                                                                Font-Size="8pt" ForeColor="Black" Width="120px" Visible="False"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlTallySubPartyName" runat="server" 
                                                                AppendDataBoundItems="True" Enabled="False" Font-Names="Arial" Font-Size="8pt" 
                                                                Width="200px" Visible="False">
                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                            </asp:DropDownList>
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
                                                    <tr>
                                                        <td style="width: 56px; height: 20px;">
                                                            <asp:Label ID="Label53" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                ForeColor="Black" Text="Inv Seq No" Width="62px"></asp:Label>
                                                        </td>
                                                        <td style="width: 278px; height: 20px;">
                                                            <asp:TextBox ID="txtInvSeqNo" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                </asp:Panel>
                                                  <br />
                                                  <asp:Panel ID="Panel3" runat="server" BackColor="#DEDFDE" Font-Names="Arial" 
                                                      Font-Size="7pt" ForeColor="#000040" GroupingText="Job Info" Width="350px">
                                                      <table>
                                                          <tr>
                                                              <td style="width: 89px; height: 26px;">
                                                                  <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      ForeColor="Black" Text="Job No."></asp:Label>
                                                              </td>
                                                              <td style="height: 26px; width: 138px;">
                                                                  <asp:TextBox ID="txtJobNo" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      Width="200px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td style="width: 89px">
                                                                  <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      ForeColor="Black" Height="16px" Text="AWB / BL No." Width="91px"></asp:Label>
                                                              </td>
                                                              <td style="width: 138px">
                                                                  <asp:TextBox ID="txtBLNo" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      Width="200px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td style="width: 89px">
                                                                  <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      ForeColor="Black" Text="BE NO./DT."></asp:Label>
                                                              </td>
                                                              <td style="width: 138px">
                                                                  <asp:TextBox ID="txtBENo" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      Width="200px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td>
                                                                  <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      ForeColor="Black" Text="Ass. Value"></asp:Label>
                                                              </td>
                                                              <td>
                                                                  <asp:TextBox ID="txtAssValue" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      Width="200px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td style="width: 89px; height: 20px;">
                                                                  <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      ForeColor="Black" Text="Custom Duty"></asp:Label>
                                                              </td>
                                                              <td style="height: 20px; width: 138px;">
                                                                  <asp:TextBox ID="txtCustomDuty" runat="server" Font-Names="Arial" 
                                                                      Font-Size="8pt" Width="200px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td style="width: 89px; height: 20px;">
                                                                  <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      ForeColor="Black" Text="No.of Container"></asp:Label>
                                                              </td>
                                                              <td style="height: 20px; width: 138px;">
                                                                  <asp:TextBox ID="txtNCNTR" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      Width="200px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td class="style25">
                                                                  <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      ForeColor="Black" Text="Quantity"></asp:Label>
                                                              </td>
                                                              <td class="style26">
                                                                  <asp:TextBox ID="txtQty" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      Width="200px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td style="width: 89px; vertical-align: top;">
                                                                  <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      ForeColor="Black" Text="Item Imported"></asp:Label>
                                                              </td>
                                                              <td style="width: 138px">
                                                                  <asp:TextBox ID="txtImpotItem" runat="server" Font-Names="Arial" 
                                                                      Font-Size="8pt" Height="30px" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td style="vertical-align: top;">
                                                                  <asp:Label ID="Label46" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      ForeColor="Black" Text="Note : -"></asp:Label>
                                                              </td>
                                                              <td>
                                                                  <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                                      Height="30px" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td align="right" style="vertical-align: top; text-align: left;">
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
                                                <td style="border: 1px solid #2461BF;">
                                                <cc1:Accordion ID="Accordion1" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                    ContentCssClass="accordionContent" runat="server" SelectedIndex="0" FadeTransitions="true"
                                                    SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40"
                                                    RequireOpenedPane="false" AutoSize="None"><Panes><cc1:AccordionPane ID="AccPan1" runat="server"><Header>Importer Remarks</Header><Content><asp:TextBox ID="txtimpRemark" Font-Names="Arial" Font-Size="8pt" Width="500px" runat="server"></asp:TextBox></Content></cc1:AccordionPane><cc1:AccordionPane ID="AccPan2" runat="server"><Header>Indent Remarks</Header><Content><asp:TextBox ID="txtIndentRemark" Font-Names="Arial" Font-Size="8pt" Width="500px" runat="server"></asp:TextBox></Content></cc1:AccordionPane></Panes></cc1:Accordion>
                                            </td>
                                         <td style="border: 1px solid #2461BF; vertical-align: top; " align="right" >
                                                    
                                                        <asp:Label ID="lblResult" runat="server"></asp:Label>
                                                    
                                                        <asp:Button ID="Submit" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                            Height="25px" OnClick="Submit_Click" Text="Submit" Width="100px" 
                                                            CssClass="button_image1" />
                                                        <asp:Button ID="preview" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                            Height="25px" OnClick="preview_Click" Text="Print Preview" Width="80px" 
                                                            CssClass="button_image1" />
                                                        <asp:Button ID="btnMail" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                            Height="25px" OnClick="btnMail_Click" Text="Mail" Width="50px" 
                                                            CssClass="button_image1" />
                                                    
                                                        <asp:Button ID="BtnExit" runat="server" CssClass="button_image1" 
                                                            Font-Names="Arial" Font-Size="8pt" Height="25px" PostBackUrl="~/index.aspx" 
                                                            Text="Exit" Width="50px" />
                                                    
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
    </div>

    <script type="text/javascript">
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

    <br />
    <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" TargetControlID="txtCompName"
        ServicePath="AutoComplete.asmx" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem" ServiceMethod="GetInvoice" MinimumPrefixLength="1"
        EnableCaching="true">
    </cc1:AutoCompleteExtender>
    <br />
    </form>
</body>
</html>
