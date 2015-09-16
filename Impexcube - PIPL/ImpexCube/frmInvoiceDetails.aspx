﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmInvoiceDetails.aspx.cs" Inherits="ImpexCube.frmInvoiceDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/Styles/Accordion.css" />
    <link rel="stylesheet" type="text/css" href="Content/Styles/jquery-ui.css" />
    <script type="text/javascript" src="Content/Scripts/Accordion.js"></script>
    <script type="text/javascript" src="Content/Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="Content/Scripts/jquery-ui.js"></script>
    <script src="Content/JQuery/JSONInvoice.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CheckGridSelected() {
            if (document.getElementById('<%= hdnEditValue.ClientID %>').value == "1") {
                return true;
            }
            else {
                return false;
            }
        }
        function ChkProduct() {
            if (document.getElementById('ContentPlaceHolder1_txtNoofProd').value == '') {
                alert('No Of product cannot be empty');
                document.getElementById('ContentPlaceHolder1_txtNoofProd').focus();
                return false;
            }
        }
        //// Call the exchange rate in Freight Type details Currency
        //        function callddlFreightCurrency() {
        //            var Sel1 = $("#ContentPlaceHolder1_ddlFreightTypeCurrency").val();
        //            var exra1 = $("#ContentPlaceHolder1_txtFreightTypeEx"); //
        //            BindExchgeRate(Sel1, exra1);
        //        }

        //        // Call the exchange rate in Insurance Type details Currency
        //        function callddlInsuranceCurrency() {
        //            var Sel1 = $("#ContentPlaceHolder1_ddlInsuranceTypeCurrency").val();
        //            var exra1 = $("#ContentPlaceHolder1_txtInsuranceTypeEx"); //
        //            BindExchgeRate(Sel1, exra1);
        //        }

        //        function callddlMiscellaneousCurrency() {
        //            var Sel1 = $("#ContentPlaceHolder1_ddlMiscelCurrency").val();
        //            var exra1 = $("#ContentPlaceHolder1_txtMiscelROE"); //
        //            BindExchgeRate(Sel1, exra1);
        //        }

        // Call the exchange rate in Invoice details Currency
        function callddlCurrency(Sel2, Exra2) {                 
            //            var Sel1 = $("#ContentPlaceHolder1_ddlInvoiceCurrency").val();
            //            var exra1 = $("#ContentPlaceHolder1_txtExchange"); //
            var Sel1 = $(Sel2).val();
            var exra1 = $(Exra2); //
            BindExchgeRate(Sel1, exra1);
        }

        // Call the exchange rate in Charge details Currency
        function callddlChargeCurrency() {
            var Sel1 = $("#ContentPlaceHolder1_ddlChargeCurrency").val();
            var exra1 = $("#ContentPlaceHolder1_txtRate"); //
            BindExchgeRate(Sel1, exra1);
        }

        // Call the exchange rate in Freight details Currency
        function callddlFreightDetails() {
            var Sel1 = $("#ContentPlaceHolder1_ddlFreightDetails").val();
            var exra1 = $("#ContentPlaceHolder1_txtFreightExchange"); //
            BindExchgeRate(Sel1, exra1);
        }

        // Call the exchange rate in Insurence details Currency
        function callddlInsurance() {
            var Sel1 = $("#ContentPlaceHolder1_ddlInsurance").val();
            var exra1 = $("#ContentPlaceHolder1_txtInsuranceExchange"); //
            BindExchgeRate(Sel1, exra1);
        }

        // Call the exchange rate in Discount details Currency
        function callddlDiscount() {
            var Sel1 = $("#ContentPlaceHolder1_ddlDiscount").val();
            var exra1 = $("#ContentPlaceHolder1_txtDiscountExchange"); //
            BindExchgeRate(Sel1, exra1);
        }

        // Call the exchange rate in Miscellameous details Currency
        function callddlMiscellameous() {
            var Sel1 = $("#ContentPlaceHolder1_ddlMiscellameous").val();
            var exra1 = $("#ContentPlaceHolder1_txtMiscellameousExchange"); //
            BindExchgeRate(Sel1, exra1);
        }

        // Call the exchange rate in Agency details Currency
        function callddlAgency() {
            var Sel1 = $("#ContentPlaceHolder1_ddlAgency").val();
            var exra1 = $("#ContentPlaceHolder1_txtAgencyExchange"); //
            BindExchgeRate(Sel1, exra1);
        }

        // Call the exchange rate in Loading details Currency
        function callddlLoading() {
            var Sel1 = $("#ContentPlaceHolder1_ddlLoading").val();
            var exra1 = $("#ContentPlaceHolder1_txtLoadingExchange"); //
            BindExchgeRate(Sel1, exra1);
        }

        function callddlHighSea() {
            var Sel1 = $("#ContentPlaceHolder1_ddlHighSea").val();
            var exra1 = $("#ContentPlaceHolder1_txtHighExRate"); //
            BindExchgeRate(Sel1, exra1);
        }

        //        function DisableFreight() {
        //            var FreightType = document.getElementById('ContentPlaceHolder1_ddlFreightTy').value;
        //            if (FreightType == 'Single freight') {
        //                document.getElementById('ContentPlaceHolder1_ddlFreightTypeCurrency').disabled = false;
        //                document.getElementById('ContentPlaceHolder1_txtFreightTypeEx').disabled = false;
        //                document.getElementById('ContentPlaceHolder1_txtFreightTypeAmount').disabled = false;
        //                document.getElementById('ContentPlaceHolder1_txtFreightTypeAmountINR').disabled = false;
        //            }
        //            else {
        //                document.getElementById('ContentPlaceHolder1_ddlFreightTypeCurrency').disabled = true;
        //                document.getElementById('ContentPlaceHolder1_txtFreightTypeEx').disabled = true;
        //                document.getElementById('ContentPlaceHolder1_txtFreightTypeAmount').disabled = true;
        //                document.getElementById('ContentPlaceHolder1_txtFreightTypeAmountINR').disabled = true;
        //            }
        //        }

        //        function DisableInsurance() {
        //            var InsType = document.getElementById('ContentPlaceHolder1_ddlInsuranceTy').value;
        //            if (InsType == 'Single Insurance') {
        //                document.getElementById('ContentPlaceHolder1_ddlInsuranceTypeCurrency').disabled = false;
        //                document.getElementById('ContentPlaceHolder1_txtInsuranceTypeEx').disabled = false;
        //                document.getElementById('ContentPlaceHolder1_txtInsuranceTypeAmount').disabled = false;
        //                document.getElementById('ContentPlaceHolder1_txtInsuranceTypeAmountINR').disabled = false;
        //            }
        //            else {
        //                document.getElementById('ContentPlaceHolder1_ddlInsuranceTypeCurrency').disabled = true;
        //                document.getElementById('ContentPlaceHolder1_txtInsuranceTypeEx').disabled = true;
        //                document.getElementById('ContentPlaceHolder1_txtInsuranceTypeAmount').disabled = true;
        //                document.getElementById('ContentPlaceHolder1_txtInsuranceTypeAmountINR').disabled = true;
        //            }
        //        }

        //        function DisableMisc() {
        //            var FreightType = document.getElementById('ContentPlaceHolder1_ddlMiscellType').value;
        //            if (FreightType == 'Single Miscellaneous') {
        //                document.getElementById('ContentPlaceHolder1_ddlMiscelCurrency').disabled = false;
        //                document.getElementById('ContentPlaceHolder1_txtMiscelROE').disabled = false;
        //                document.getElementById('ContentPlaceHolder1_txtMiscelAmount').disabled = false;
        //                document.getElementById('ContentPlaceHolder1_txtMiscelAmountINR').disabled = false;
        //            }
        //            else {
        //                document.getElementById('ContentPlaceHolder1_ddlMiscelCurrency').disabled = true;
        //                document.getElementById('ContentPlaceHolder1_txtMiscelROE').disabled = true;
        //                document.getElementById('ContentPlaceHolder1_txtMiscelAmount').disabled = true;
        //                document.getElementById('ContentPlaceHolder1_txtMiscelAmountINR').disabled = true;
        //            }
        //        }

        function FreightCalc() {
            var TotInv = document.getElementById('ContentPlaceHolder1_txtTotInv').value;
            var TotFreight = document.getElementById('ContentPlaceHolder1_txtFreightTypeAmountINR').value;
            var Inv = document.getElementById('ContentPlaceHolder1_txtProductINRValues').value;
            var FreightEx = document.getElementById('ContentPlaceHolder1_txtFreightTypeEx').value;
            var cal = parseInt(TotFreight) / parseInt(TotInv);
            var FreightAmtINR = cal * Inv;
            document.getElementById('ContentPlaceHolder1_txtFreightINRAmount').value = FreightAmtINR;
            var FreightAmt = parseInt(FreightAmtINR) / parseInt(FreightEx);
            document.getElementById('ContentPlaceHolder1_txtFreightAmount').value = FreightAmt;
            //alert(FreightAmtINR + FreightAmt);
        }
        //        function ClearInvoiceData() {
        //            try {
        //                document.getElementById('ContentPlaceHolder1_txtInvoiceNo').value = '';
        //                document.getElementById('ContentPlaceHolder1_txtDate').value = '';
        //                document.getElementById('ContentPlaceHolder1_ddlTermsofInvoice').selectedIndex = 0;
        //                document.getElementById('ContentPlaceHolder1_ddlFreightType').selectedIndex = 0;
        //                document.getElementById('ContentPlaceHolder1_ddlPayment').selectedIndex = 0;
        //                document.getElementById('ContentPlaceHolder1_ddlTrans').selectedIndex = 0;
        //                document.getElementById('ContentPlaceHolder1_ddlInvoiceCurrency').selectedIndex = 0;
        //                document.getElementById('ContentPlaceHolder1_txtExchange').value = '';
        //                document.getElementById('ContentPlaceHolder1_txtProductValues').value = '';
        //                document.getElementById('ContentPlaceHolder1_txtProductINRValues').value = '';
        //                alert('fdsf');
        //                // document.getElementById('ContentPlaceHolder1_btnAddInvoice').style.visibility = 'visible';
        //                document.getElementById('ContentPlaceHolder1_btnUpdateInvoice').style.visibility = 'hidden';
        //                return false;
        //            }
        //            catch (err) {
        //                alert(err);
        //            }
        //        }


        function Validate() {
            var InvoiceValue = document.getElementById('ContentPlaceHolder1_txtProductValues').value;

            var InvoiceCurrency = document.getElementById('ContentPlaceHolder1_ddlInvoiceCurrency').value;
            var Transaction = document.getElementById('ContentPlaceHolder1_ddlTrans').value;
            var PaymentTerms = document.getElementById('ContentPlaceHolder1_ddlPayment').value;

            if (document.getElementById('ContentPlaceHolder1_ddlJobNo').value == '~Select~') {
                alert('Please Select Job No');
                document.getElementById('ContentPlaceHolder1_ddlJobNo').focus();
                return false;
            }

            if (document.getElementById('ContentPlaceHolder1_txtInvoiceNo').value == '') {
                alert('Please Enter Invoice No');
                document.getElementById('ContentPlaceHolder1_txtInvoiceNo').focus();
                return false;
            }

            if (document.getElementById('ContentPlaceHolder1_txtDate').value == '') {
                alert('Please Enter Invoice Date');
                document.getElementById('ContentPlaceHolder1_txtDate').focus();
                return false;
            }

            if (document.getElementById('ContentPlaceHolder1_ddlTermsofInvoice').selectedIndex == 0) {
                alert('Please select Invoice Terms');
                document.getElementById('ContentPlaceHolder1_ddlTermsofInvoice').focus();
                return false;
            }

            if (PaymentTerms == "~Select~") {
                alert('Please select PaymentTerms');
                return false;
            }
            if (Transaction == "~Select~") {
                alert('Please select Transaction');
                return false;
            }
            if (InvoiceCurrency == "~Select~") {
                alert('Please select Invoice Currency');
                return false;
            }
            if (InvoiceValue == "") {
                alert('Please Enter Invoice Amount');
                return false;
            }
            FreightCalc();

        }

        function ValidateSingleFreight() {
            var FreightType = document.getElementById('ContentPlaceHolder1_ddlFreightTy').value;
            var FreightCurrency = document.getElementById('ContentPlaceHolder1_ddlFreightTypeCurrency').value;
            var FreightExRate = document.getElementById('ContentPlaceHolder1_txtFreightTypeEx').value;
            var TotalInvoiceAmt = document.getElementById('ContentPlaceHolder1_txtTotInv').value;
            if (TotalInvoiceAmt != "0") {
                if (FreightType == "Single freight") {
                    var TotalFreightAmt = document.getElementById('ContentPlaceHolder1_txtFreightTypeAmount').value;

                    var InvoiceAmt = document.getElementById('ContentPlaceHolder1_txtProductValues').value;
                    var FreightAmt = (TotalFreightAmt / TotalInvoiceAmt) * InvoiceAmt;
                    document.getElementById('ContentPlaceHolder1_txtFreightAmount').value = FreightAmt;
                    document.getElementById('ContentPlaceHolder1_ddlFreightDetails').value = FreightCurrency;
                    document.getElementById('ContentPlaceHolder1_txtFreightExchange').value = FreightExRate;
                }
            }
            else {
                alert('Please enter the Total Invoice Amount');
            }

        }

        function ValidateSingleInsurance() {
            var InsuranceType = document.getElementById('ContentPlaceHolder1_ddlInsuranceTy').value;
            var InsuranceCurrency = document.getElementById('ContentPlaceHolder1_ddlInsuranceTypeCurrency').value;
            var InsuranceExRate = document.getElementById('ContentPlaceHolder1_txtInsuranceTypeEx').value;
            var TotalInvoiceAmt = document.getElementById('ContentPlaceHolder1_txtTotInv').value;
            if (TotalInvoiceAmt != "0") {
                if (InsuranceType == "Single Insurance") {
                    var TotalInsuranceAmt = document.getElementById('ContentPlaceHolder1_txtInsuranceTypeAmount').value;

                    var InvoiceAmt = document.getElementById('ContentPlaceHolder1_txtProductValues').value;
                    var InsuranceAmt = (TotalInsuranceAmt / TotalInvoiceAmt) * InvoiceAmt;
                    document.getElementById('ContentPlaceHolder1_txtInsuranceAmount').value = InsuranceAmt;
                    document.getElementById('ContentPlaceHolder1_ddlInsurance').value = InsuranceCurrency;
                    document.getElementById('ContentPlaceHolder1_txtInsuranceExchange').value = InsuranceExRate;
                }

            }
            else {
                alert('Please enter the Total Invoice Amount');
            }
        }

        function ValidateSingleMiscellaneous() {
            var MiscellaneousType = document.getElementById('ContentPlaceHolder1_ddlMiscellType').value;
            var MiscellaneousCurrency = document.getElementById('ContentPlaceHolder1_ddlMiscelCurrency').value;
            var MiscellaneousExRate = document.getElementById('ContentPlaceHolder1_txtMiscelROE').value;
            var TotalInvoiceAmt = document.getElementById('ContentPlaceHolder1_txtTotInv').value;
            if (TotalInvoiceAmt != "0") {
                if (MiscellaneousType == "Single Miscellaneous") {
                    var TotalMiscellaneousAmt = document.getElementById('ContentPlaceHolder1_txtMiscelAmount').value;

                    var InvoiceAmt = document.getElementById('ContentPlaceHolder1_txtProductValues').value;
                    var MiscellaneousAmt = (TotalMiscellaneousAmt / TotalInvoiceAmt) * InvoiceAmt;
                    document.getElementById('ContentPlaceHolder1_txtMiscellameousAmount').value = MiscellaneousAmt;
                    document.getElementById('ContentPlaceHolder1_ddlMiscellameous').value = MiscellaneousCurrency;
                    document.getElementById('ContentPlaceHolder1_txtMiscellameousExchange').value = MiscellaneousExRate;
                }
            }
            else {
                alert('Please enter the Total Invoice Amount');
            }
        }


        //        onChange = "javascript:return FreightCalc();"
    </script>
    <style type="text/css">
        .hiddencol
        {
            display: none;
        }
    </style>
    <style type="text/css">
        .stylebtn6
        {
 padding:3px;
    margin:0px;
    cursor: pointer;
    text-align: center;
    border-radius: 2px 2px 2px 2px;/*curve of the border*/
    -webkit- border-radius: 2px 2px 2px 2px;/*support crome*/
   -moz- border-radius: 2px 2px 2px 2px;/*supportmo*/
    background: linear-gradient(to bottom, #73AAE8    0%, #73FFE8     100%) repeat scroll 0 0 transparent;
    -webkit-background: linear-gradient(to bottom, #73AAE8    0%, #73FFE8    100%) repeat scroll 0 0 transparent;
    -moz-background: linear-gradient(to bottom, #73AAE8    0%, #73FFE8    100%) repeat scroll 0 0 transparent;
    border:none;
    border:1px solid #73AAE8;
    width: 120px;
    font-size: 8pt;
    /*height:30px;*/
    color:#241e12;
          /* font-size: 8pt;
            width:120px;
            background-color:#73AAE8;*/
            
        }
        .style6
        {
            text-align: left;
        }
        .btnSize
        {
            font-size: 10pt;
        }
        .modelwindow
        {
            border: solid1px#c0c0c0;
            background: #000000;
            padding: 0px10px10px10px;
            position: absolute;
            top: -1000px;
        }
        .modelbackground
        {
            background-color: #ccccFF;
            opacity: 1.0;
            filter: alpha(opacity=80);
        }
        .HideGridColoumn
        {
            display: none;
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
            Height: 15px;
            font-family: Verdana;
            Width: 75px;
            font-size: 8pt;
            font-weight: bold;
        }
        .style11
        {
            width: 150px;
            text-align: left;
        }
        .style1002
        {
            width: 464px;
        }
        .style1003
        {}
    </style>
    <script type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Content/Images/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td valign="top">
                        <table>
                            <tr>
                                <td class="style6">
                                    <br />
                                    <asp:Button ID="btnOtherChargesVisible" runat="server" CssClass="stylebutton" OnClientClick="javascript:return CheckGridSelected();"
                                        OnClick="btnOtherChargesVisible_Click" Text="Other Charges" />
                                    <br />
                                    <asp:Button ID="btnFreightDetails" runat="server" CssClass="stylebutton" OnClientClick="javascript:return CheckGridSelected();"
                                        OnClick="btnFreightDetails_Click" Text="Freight &amp; Insurance" />
                                    <br />
                                    <asp:Button ID="btnProduct" runat="server" CssClass="stylebutton" OnClick="btnProduct_Click"
                                        Text="Product" />
                                    <br />
                                    <asp:Button ID="btnConsignor" runat="server" CssClass="stylebutton" OnClientClick="javascript:return CheckGridSelected();"
                                        OnClick="btnConsignor_Click" Text="Consignor" />
                                    <br />
                                    <asp:Button ID="btnRelationSVBDetails" runat="server" CssClass="stylebutton" OnClientClick="javascript:return CheckGridSelected();"
                                        OnClick="btnRelationSVBDetails_Click" Text="Relation &amp; SVB" />
                                    <br />
                                    <asp:Button ID="btnOtherDetails" runat="server" CssClass="stylebutton" OnClientClick="javascript:return CheckGridSelected();"
                                        OnClick="btnOtherDetails_Click" Text="Other Details" />
                                    <br />
                                    <asp:Button ID="btnCheckList" runat="server" CssClass="stylebutton" OnClick="btnCheckList_Click"
                                        Text="Check List" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Groove" Width="830px">
                            <table style="width: 800px">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFreight0" runat="server" CssClass="fontsize" Text="Freight/Insurance Type"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFreightTy" runat="server" AppendDataBoundItems="True" CssClass="ddl200"
                                            Width="400px">
                                            <asp:ListItem Selected="True">Single</asp:ListItem>
                                            <asp:ListItem>Separate</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table style="width: 800px">
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="lblInvoiceNo" runat="server" CssClass="textbox75" Text="Invoice No"></asp:Label>
                                                </td>
                                                <td class="style8">
                                                    <asp:Label ID="lblDate" runat="server" CssClass="fontsize" Text="Inv.Date"></asp:Label>
                                                </td>
                                                <td class="style9">
                                                    <asp:Label ID="lblTermsofInvoice" runat="server" CssClass="fontsize" Text="Inv.Term"></asp:Label>
                                                </td>
                                                <%-- <td class="tdcolumn100">
                                                    <asp:Label ID="lblFreight" runat="server" CssClass="fontsize" 
                                                        Text="Freight Type"></asp:Label>
                                                </td>--%>
                                                <td class="style9">
                                                    <asp:Label ID="lblPayment" runat="server" CssClass="fontsize" Text="Pay.Mode"></asp:Label>
                                                </td>
                                                <td class="style9">
                                                    <asp:Label ID="lblTrans" runat="server" CssClass="fontsize" Text="Transaction"></asp:Label>
                                                </td>
                                                <td class="style8">
                                                    <asp:Label ID="labelC" runat="server" CssClass="fontsize" Text="Currency"></asp:Label>
                                                </td>
                                                <td class="style8">
                                                    <asp:Label ID="lblExchRates" runat="server" CssClass="fontsize" Text="Ex. Rate"></asp:Label>
                                                </td>
                                                <td class="style8">
                                                    <asp:Label ID="lblProductValues" runat="server" CssClass="fontsize" Text="Amount"></asp:Label>
                                                </td>
                                                <td class="style8">
                                                    <asp:Label ID="lblProductValues0" runat="server" CssClass="fontsize" Text="INR"></asp:Label>
                                                </td>
                                                <td class="tdcolumn" colspan="2">
                                                    <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="New" CssClass="stylebutton"
                                                        Font-Bold="True" Width="50px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textbox75">
                                                    <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                                </td>
                                                <td class="tdcolumn75">
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="textbox75"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:DropDownList ID="ddlTermsofInvoice" runat="server" AppendDataBoundItems="True"
                                                        CssClass="ddl75">
                                                        <asp:ListItem>~Select~</asp:ListItem>
                                                        <asp:ListItem>FOB</asp:ListItem>
                                                        <asp:ListItem>CIF</asp:ListItem>
                                                        <asp:ListItem>CF</asp:ListItem>
                                                        <asp:ListItem>CI</asp:ListItem>
                                                        <%--<asp:ListItem>CIFC</asp:ListItem>
                                                        <asp:ListItem>Ex-Works</asp:ListItem>--%>
                                                    </asp:DropDownList>
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
                                                    <asp:DropDownList ID="ddlPayment" runat="server" AppendDataBoundItems="True" CssClass="ddl75">
                                                        <asp:ListItem>LC</asp:ListItem>
                                                        <asp:ListItem>DP</asp:ListItem>
                                                        <asp:ListItem>FC</asp:ListItem>
                                                        <%-- <asp:ListItem>DA</asp:ListItem>--%>
                                                        <asp:ListItem>SD</asp:ListItem>
                                                        <asp:ListItem>OTH</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="tdcolumn75">
                                                    <asp:DropDownList ID="ddlTrans" runat="server" AppendDataBoundItems="True" CssClass="ddl100">
                                                        <asp:ListItem Value="S">Sale</asp:ListItem>
                                                        <asp:ListItem Value="C">Consignment</asp:ListItem>
                                                        <asp:ListItem Value="H">Hire</asp:ListItem>
                                                        <asp:ListItem Value="R">Rent</asp:ListItem>
                                                        <asp:ListItem Value="G">Gift</asp:ListItem>
                                                        <asp:ListItem Value="P">Replacement</asp:ListItem>
                                                        <asp:ListItem Value="M">Sample</asp:ListItem>
                                                        <asp:ListItem Value="F">Free of Cost</asp:ListItem>
                                                        <%--  <asp:ListItem>DONATION FOR GOVT.</asp:ListItem>--%>
                                                        <asp:ListItem Value="O">Others</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="tdcolumn75">
                                                    <asp:DropDownList ID="ddlInvoiceCurrency" runat="server" AppendDataBoundItems="True"
                                                        CssClass="ddl100" onchange="javascript:return callddlCurrency('#ContentPlaceHolder1_ddlInvoiceCurrency','#ContentPlaceHolder1_txtExchange');">
                                                        <asp:ListItem Selected="True" Value="~Select~">~Select~</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchange" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtProductValues" runat="server" CssClass="textbox75" onblur="ProductValueINR('ContentPlaceHolder1_txtExchange','ContentPlaceHolder1_txtProductValues','ContentPlaceHolder1_txtProductINRValues');">0</asp:TextBox>
                                                </td>
                                                <td class="tdcolumn">
                                                    <asp:TextBox ID="txtProductINRValues" runat="server" CssClass="textbox75">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <%-- <asp:ImageButton ID="btnAddInvoice" runat="server" ImageUrl="~/Content/Images/Add.jpg"
                                                        OnClick="btnAddInvoice_Click" OnClientClick="javascript:return Validate();" ToolTip="Add" />--%>
                                                    <asp:Button ID="btnAddInvoice" runat="server" Text="Save" OnClick="btnAddInvoice_Click"
                                                        OnClientClick="javascript:return Validate();" CssClass="stylebutton" Font-Bold="True"
                                                        Width="50px" />
                                                    <asp:Button ID="btnUpdateInvoice" runat="server" Text="Update" OnClick="btnUpdateInvoice_Click"
                                                        OnClientClick="javascript:return Validate();" CssClass="stylebutton" Font-Bold="True"
                                                        Width="50px" Visible="False" />
                                                    <%--  <asp:ImageButton ID="btnUpdateInvoice" runat="server" ImageUrl="~/Content/Images/Add.jpg"
                                                        OnClick="btnUpdateInvoice_Click" OnClientClick="javascript:return Validate();"
                                                        Visible="False" ToolTip="Update" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="center" colspan="12">
                                                    <asp:GridView ID="gvInvoiceDetails" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="calibri"
                                                        Font-Size="10pt" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="gvInvoiceDetails_SelectedIndexChanged"
                                                        ShowFooter="True" ShowHeader="true" Style="text-align: center; font-size: 9pt;"
                                                        Visible="false" Width="805px">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Content/Images/delete.gif"
                                                                        OnClientClick="return confirm('Do U Want Delete?');" OnClick="btnDelete_Click"
                                                                        Width="20px" Height="20px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="InvoiceDetailsId" HeaderStyle-CssClass="hiddencol" HeaderText="Idfg"
                                                                ItemStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol">
                                                                <FooterStyle CssClass="hiddencol" />
                                                                <HeaderStyle CssClass="hiddencol" />
                                                                <ItemStyle CssClass="hiddencol" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="InvoiceNo" HeaderText="Inv. No" />
                                                            <asp:BoundField DataField="Date" HeaderText="Date" />
                                                            <asp:BoundField DataField="Terms" HeaderText="Inv.Term" />
                                                            <asp:BoundField DataField="FreightType" HeaderText="FreightType" />
                                                            <asp:BoundField DataField="PaymentTerms" HeaderText="Pay.Mode" />
                                                            <asp:BoundField DataField="TransMode" HeaderText="Transaction" />
                                                            <asp:BoundField DataField="Currency" HeaderText="Currency" />
                                                            <asp:BoundField DataField="ExchRates" HeaderText="Ex. Rate" />
                                                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                            <asp:BoundField DataField="AmountINR" HeaderText="Amount INR" />
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
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3">
                                        <asp:Panel ID="PanelOtherDetails" runat="server" Visible="False">
                                            <table border="1px" width="90%">
                                                <tr>
                                                    <td colspan="6" style="text-align: center; font-weight: 700">
                                                        Other Charges
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdcolumn100">
                                                        <asp:Label ID="lblChargeType" runat="server" CssClass="fontsize" Text="Charge Type"></asp:Label>
                                                    </td>
                                                    <td class="tdcolumn75">
                                                        <asp:Label ID="Label5" runat="server" CssClass="fontsize" Text="Currency"></asp:Label>
                                                    </td>
                                                    <td class="tdcolumn75">
                                                        <asp:Label ID="Label6" runat="server" CssClass="fontsize" Text="Ex.Rate"></asp:Label>
                                                    </td>
                                                    <td class="tdcolumn75">
                                                        <asp:Label ID="Label7" runat="server" CssClass="fontsize" Text="Amount"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="Label33" runat="server" CssClass="fontsize" Text="Amount INR"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdcolumn100">
                                                        <asp:DropDownList ID="ddlChargeType" runat="server" AppendDataBoundItems="True" CssClass="ddl200">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="tdcolumn75">
                                                        <asp:DropDownList ID="ddlChargeCurrency" runat="server" AppendDataBoundItems="True"
                                                            CssClass="ddl75" onchange="javascript:return callddlCurrency('#ContentPlaceHolder1_ddlChargeCurrency','#ContentPlaceHolder1_txtRate');">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="tdcolumn75">
                                                        <asp:TextBox ID="txtRate" runat="server" CssClass="textbox75" OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                    </td>
                                                    <td class="tdcolumn75">
                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox75" onblur="ProductValueINR('ContentPlaceHolder1_txtRate','ContentPlaceHolder1_txtAmount','ContentPlaceHolder1_txtAmountINR');">0</asp:TextBox>
                                                    </td>
                                                    <td class="tdcolumn50">
                                                        <%--<asp:ImageButton ID="btnOtherCharges" runat="server" ImageUrl="~/Content/Images/Add.jpg"
                                                            OnClick="btnOtherCharges_Click" Visible="False" />--%>
                                                        <%-- <asp:ImageButton ID="btnAddOtherCharges" runat="server" ImageUrl="~/Content/Images/Add.jpg"
                                                            OnClick="btnAddOtherCharges_Click" />--%>
                                                        <asp:TextBox ID="txtAmountINR" runat="server" CssClass="textbox75">0</asp:TextBox>
                                                    </td>
                                                    <td class="tdcolumn50">
                                                        <asp:Button ID="btnAddOtherCharges" runat="server" CssClass="stylebutton" Font-Bold="True"
                                                            OnClick="btnAddOtherCharges_Click" OnClientClick="javascript:return ValidateInvoice();"
                                                            Text="Save" Width="50px" />
                                                        <asp:Button ID="btnOtherCharges" runat="server" CssClass="stylebutton" Font-Bold="True"
                                                            OnClick="btnOtherCharges_Click" Text="Update" Visible="False" Width="50px" />
                                                        <asp:Button ID="btnCancelOtherCharges" runat="server" CssClass="stylebutton" Font-Bold="True"
                                                            OnClick="btnCancelOtherCharges_Click" OnClientClick="return confirm ('Do you want to Clear the data?')"
                                                            Text="New" Width="50px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:GridView ID="gvOtherCharges" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                                                            CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanged="gvOtherCharges_SelectedIndexChanged"
                                                            Style="font-size: 9pt; font-family: calibri" Width="589px" ShowFooter="True">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnothDelete" runat="server" ImageUrl="~/Content/Images/delete.gif"
                                                                            OnClientClick="return confirm('Do U Want Delete?');" Width="20px" Height="20px"
                                                                            OnClick="btnothDelete_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="InvoiceOtherChargesId" HeaderText="InvoiceOtherChargesId"
                                                                    HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                                    <HeaderStyle CssClass="hiddencol" />
                                                                    <ItemStyle CssClass="hiddencol" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ChargeType" HeaderText="Charge Type" />
                                                                <asp:BoundField DataField="Currency" HeaderText="Currency" />
                                                                <asp:BoundField DataField="ExchRate" HeaderText="Ex. Rate" />
                                                                <asp:BoundField DataField="ChargeAmount" HeaderText="Amount" />
                                                                <asp:BoundField DataField="AmountINR" HeaderText="AmountINR" />
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
                                                <%--<tr>
                                                    <td align="right" colspan="3">
                                                        <asp:Label ID="lblInvoiceChoice" runat="server" CssClass="fontsize" Text="Total  Amount"
                                                            Visible="False"></asp:Label>
                                                        <asp:TextBox ID="txtInvoiceChoices" runat="server" CssClass="textbox75" Visible="False">0</asp:TextBox>
                                                        <asp:Label ID="lblTotalProduct" runat="server" CssClass="fontsize" Text="Invoice Value"
                                                            Visible="False"></asp:Label>
                                                        <asp:TextBox ID="txtProductValue" runat="server" CssClass="textbox75" Visible="False">0</asp:TextBox>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:Label ID="lblOhterCharges" runat="server" CssClass="fontsize" Text="Total Other Charges"></asp:Label>
                                                        <asp:TextBox ID="txtOtherCharges" runat="server" CssClass="textbox75"></asp:TextBox>
                                                    </td>
                                                </tr>--%>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="PanelFreight" runat="server" Visible="False" Width="785px">
                                            <table width="800">
                                                <tr>
                                                    <td>
                                                        <table border="1px" width="800">
                                                            <tr>
                                                                <td colspan="6" style="text-align: center; font-weight: 700">
                                                                    Freight , Insurance &amp; Miscellaneous
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdcolumn75">
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:Label ID="lblFreightCurrency" runat="server" CssClass="fontsize" Text="Currency"></asp:Label>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:Label ID="lblFreightExchange" runat="server" CssClass="fontsize" Text="Exchg.Rate"></asp:Label>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:Label ID="lblFreightRate" runat="server" CssClass="fontsize" Text="Rate%"></asp:Label>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:Label ID="lblbFreightAmount" runat="server" CssClass="fontsize" Text="Amount"></asp:Label>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:Label ID="Label12" runat="server" CssClass="fontsize" Text="Amount INR"></asp:Label>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:Label ID="Label31" runat="server" CssClass="fontsize" Text="Total Rate%"></asp:Label>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:Label ID="Label32" runat="server" CssClass="fontsize" Text="Total Amt"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style6">
                                                                    <asp:Label ID="lblMiscellanous" runat="server" CssClass="fontsize" Text="Misc."></asp:Label>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:DropDownList ID="ddlMiscellameous" runat="server" AppendDataBoundItems="True"
                                                                        CssClass="ddl100" onchange="javascript:return callddlCurrency('#ContentPlaceHolder1_ddlMiscellameous','#ContentPlaceHolder1_txtMiscellameousExchange');">
                                                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtMiscellameousExchange" runat="server" CssClass="textbox75" onblur="javascript:return CheckExchange(this.id);"
                                                                        onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlMiscellameous','ContentPlaceHolder1_txtMiscellameousExchange','Please select miscellaneous currency');">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtMiscellameousRate" runat="server" CssClass="textbox75" onblur="CIFAmtCalculation('ContentPlaceHolder1_txtMiscellameousExchange','ContentPlaceHolder1_txtMiscellameousRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtMiscellameousAmount','ContentPlaceHolder1_txtMiscellameousINRAmount');"
                                                                        OnKeyPress="javascript:return isFloat(event);" Enabled="False">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtMiscellameousAmount" runat="server" CssClass="textbox75" onblur="CIFRateCalculation('ContentPlaceHolder1_txtMiscellameousExchange','ContentPlaceHolder1_txtMiscellameousAmount','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtMiscellameousRate','ContentPlaceHolder1_txtMiscellameousINRAmount');"
                                                                        OnKeyPress="javascript:return isFloat(event);" Enabled="False">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtMiscellameousINRAmount" runat="server" CssClass="textbox75" onkeypress="javascript:return txtboxdisable();"
                                                                        Enabled="False">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtMisTotalRate" runat="server" CssClass="textbox75" onblur="TotalFOBMisAmount('ContentPlaceHolder1_txtTotInv','ContentPlaceHolder1_txtMiscelTotalAmount','ContentPlaceHolder1_txtMisTotalRate','ContentPlaceHolder1_txtMiscellameousINRAmount','ContentPlaceHolder1_txtMiscellameousAmount','ContentPlaceHolder1_txtMiscellameousRate','ContentPlaceHolder1_txtMiscellameousExchange','ContentPlaceHolder1_txtExchange','ContentPlaceHolder1_txtProductValues');">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtMiscelTotalAmount" runat="server" CssClass="textbox75" EnableTheming="True"
                                                                        onblur="TotalFOBMisRate('ContentPlaceHolder1_txtTotInv','ContentPlaceHolder1_txtMiscelTotalAmount','ContentPlaceHolder1_txtMisTotalRate','ContentPlaceHolder1_txtMiscellameousINRAmount','ContentPlaceHolder1_txtMiscellameousAmount','ContentPlaceHolder1_txtMiscellameousRate','ContentPlaceHolder1_txtMiscellameousExchange','ContentPlaceHolder1_txtExchange','ContentPlaceHolder1_txtProductValues');">0</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style6">
                                                                    <asp:Label ID="Label9" runat="server" CssClass="fontsize" Text="Freight"></asp:Label>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:DropDownList ID="ddlFreightDetails" runat="server" AppendDataBoundItems="True"
                                                                        CssClass="ddl100" onchange="javascript:return callddlCurrency('#ContentPlaceHolder1_ddlFreightDetails','#ContentPlaceHolder1_txtFreightExchange');" >
                                                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtFreightExchange" runat="server" CssClass="textbox75" onblur="javascript:return CheckExchange(this.id);"
                                                                        onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlFreightDetails','ContentPlaceHolder1_txtFreightExchange','Please select freight currency');">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtFreightRate" runat="server" CssClass="textbox75" onblur="CIFAmtCalculation('ContentPlaceHolder1_txtFreightExchange','ContentPlaceHolder1_txtFreightRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtFreightAmount','ContentPlaceHolder1_txtFreightINRAmount');"
                                                                        OnKeyPress="javascript:return isFloat(event);" Enabled="False">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtFreightAmount" runat="server" CssClass="textbox75" onblur="CIFRateCalculation('ContentPlaceHolder1_txtFreightExchange','ContentPlaceHolder1_txtFreightAmount','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtFreightRate','ContentPlaceHolder1_txtFreightINRAmount');"
                                                                        OnKeyPress="javascript:return isFloat(event);" Enabled="False">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtFreightINRAmount" runat="server" CssClass="textbox75" onkeypress="javascript:return txtboxdisable();"
                                                                        Enabled="False">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtFrightTotalRate" runat="server" CssClass="textbox75" onblur="TotalFOBAmount('ContentPlaceHolder1_txtTotInv','ContentPlaceHolder1_txtFreightTotalAmount','ContentPlaceHolder1_txtFrightTotalRate','ContentPlaceHolder1_txtFreightINRAmount','ContentPlaceHolder1_txtFreightAmount','ContentPlaceHolder1_txtFreightRate','ContentPlaceHolder1_txtFreightExchange','ContentPlaceHolder1_txtExchange','ContentPlaceHolder1_txtProductValues','ContentPlaceHolder1_txtMiscellameousExchange', 'ContentPlaceHolder1_txtMiscelTotalAmount');">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtFreightTotalAmount" runat="server" CssClass="textbox75" onblur="TotalFOBRate('ContentPlaceHolder1_txtTotInv','ContentPlaceHolder1_txtFreightTotalAmount','ContentPlaceHolder1_txtFrightTotalRate','ContentPlaceHolder1_txtFreightINRAmount','ContentPlaceHolder1_txtFreightAmount','ContentPlaceHolder1_txtFreightRate','ContentPlaceHolder1_txtFreightExchange','ContentPlaceHolder1_txtExchange','ContentPlaceHolder1_txtProductValues');">0</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td class="style6">
                                                                    <asp:Label ID="Label10" runat="server" CssClass="fontsize" Text="Insurance"></asp:Label>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:DropDownList ID="ddlInsurance" runat="server" AppendDataBoundItems="True" CssClass="ddl100"
                                                                        Height="16px" onchange="javascript:return callddlCurrency('#ContentPlaceHolder1_ddlInsurance','#ContentPlaceHolder1_txtInsuranceExchange');">
                                                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtInsuranceExchange" runat="server" CssClass="textbox75" onblur="javascript:return CheckExchange(this.id);"
                                                                        onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlInsurance','ContentPlaceHolder1_txtInsuranceExchange','Please select insurance currency');">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtInsuranceRate" runat="server" CssClass="textbox75" onblur="InsuranceAmount('ContentPlaceHolder1_txtInsuranceExchange','ContentPlaceHolder1_txtInsuranceRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtInsuranceAmount','ContentPlaceHolder1_txtInsuranceINRAmount','ContentPlaceHolder1_txtMiscellameousINRAmount');"
                                                                        OnKeyPress="javascript:return isFloat(event);" Enabled="False">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtInsuranceAmount" runat="server" AutoPostBack="True" CssClass="textbox75"
                                                                        onblur="InsuranceRate('ContentPlaceHolder1_txtInsuranceExchange','ContentPlaceHolder1_txtInsuranceAmount','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtInsuranceRate','ContentPlaceHolder1_txtInsuranceINRAmount','ContentPlaceHolder1_txtMiscellameousINRAmount');"
                                                                        OnKeyPress="javascript:return isFloat(event);" Enabled="False">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtInsuranceINRAmount" runat="server" CssClass="textbox75" onkeypress="javascript:return txtboxdisable();"
                                                                        Enabled="False">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtInsuranceTotalRate" runat="server" CssClass="textbox75" onblur="TotalFOBAmount('ContentPlaceHolder1_txtTotInv','ContentPlaceHolder1_txtInsuranceTotalAmount','ContentPlaceHolder1_txtInsuranceTotalRate','ContentPlaceHolder1_txtInsuranceINRAmount','ContentPlaceHolder1_txtInsuranceAmount','ContentPlaceHolder1_txtInsuranceRate','ContentPlaceHolder1_txtInsuranceExchange','ContentPlaceHolder1_txtExchange','ContentPlaceHolder1_txtProductValues','ContentPlaceHolder1_txtMiscellameousExchange', 'ContentPlaceHolder1_txtMiscelTotalAmount');">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtInsuranceTotalAmount" runat="server" CssClass="textbox75" onblur="TotalFOBRate('ContentPlaceHolder1_txtTotInv','ContentPlaceHolder1_txtInsuranceTotalAmount','ContentPlaceHolder1_txtInsuranceTotalRate','ContentPlaceHolder1_txtInsuranceINRAmount','ContentPlaceHolder1_txtInsuranceAmount','ContentPlaceHolder1_txtInsuranceRate','ContentPlaceHolder1_txtInsuranceExchange','ContentPlaceHolder1_txtExchange','ContentPlaceHolder1_txtProductValues');" >0</asp:TextBox>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style6">
                                                                    <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="Discounts"></asp:Label>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:DropDownList ID="ddlDiscount" runat="server" AppendDataBoundItems="True" CssClass="ddl100"
                                                                        onchange="javascript:return callddlCurrency('#ContentPlaceHolder1_ddlDiscount','#ContentPlaceHolder1_txtDiscountExchange');">
                                                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtDiscountExchange" runat="server" CssClass="textbox75" onblur="javascript:return CheckExchange(this.id);"
                                                                        onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlDiscount','ContentPlaceHolder1_txtDiscountExchange','Please select discount currency');">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtDiscountRate" runat="server" CssClass="textbox75" onblur="CIFAmtCalculation('ContentPlaceHolder1_txtDiscountExchange','ContentPlaceHolder1_txtDiscountRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtDiscountAmount','ContentPlaceHolder1_txtDiscountINRAmount');"
                                                                        OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtDiscountAmount" runat="server" AutoPostBack="True" CssClass="textbox75"
                                                                        onblur="CIFRateCalculation('ContentPlaceHolder1_txtDiscountExchange','ContentPlaceHolder1_txtDiscountAmount','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtDiscountRate','ContentPlaceHolder1_txtDiscountINRAmount');"
                                                                        OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtDiscountINRAmount" runat="server" CssClass="textbox75" onkeypress="javascript:return txtboxdisable();">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                </td>
                                                            </tr>
                                                            <tr class="hiddencol">
                                                                <td class="hiddencol">
                                                                    <asp:Label ID="lblAgency" runat="server" CssClass="fontsize" Text="Agency"></asp:Label>
                                                                </td>
                                                                <td class="hiddencol">
                                                                    <asp:DropDownList ID="ddlAgency" runat="server" AppendDataBoundItems="True" CssClass="ddl100"
                                                                        Height="16px" onchange="javascript:return callddlCurrency('#ContentPlaceHolder1_ddlAgency','#ContentPlaceHolder1_txtAgencyExchange');">
                                                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="hiddencol">
                                                                    <asp:TextBox ID="txtAgencyExchange" runat="server" CssClass="textbox75" onblur="javascript:return CheckExchange(this.id);"
                                                                        onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlAgency','ContentPlaceHolder1_txtAgencyExchange','Please select agent currency');">0</asp:TextBox>
                                                                </td>
                                                                <td class="hiddencol">
                                                                    <asp:TextBox ID="txtAgencyRate" runat="server" CssClass="textbox75" onblur="CIFAmtCalculation('ContentPlaceHolder1_txtAgencyExchange','ContentPlaceHolder1_txtAgencyRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtAgencyAmount','ContentPlaceHolder1_txtAgencyINRAmount');"
                                                                        OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                </td>
                                                                <td class="hiddencol">
                                                                    <asp:TextBox ID="txtAgencyAmount" runat="server" CssClass="textbox75" onblur="CIFRateCalculation('ContentPlaceHolder1_txtAgencyExchange','ContentPlaceHolder1_txtAgencyAmount','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtAgencyRate','ContentPlaceHolder1_txtAgencyINRAmount');"
                                                                        OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                </td>
                                                                <td class="hiddencol">
                                                                    <asp:TextBox ID="txtAgencyINRAmount" runat="server" CssClass="textbox75" onkeypress="javascript:return txtboxdisable();">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                </td>
                                                            </tr>
                                                            <tr class="hiddencol">
                                                                <td class="hiddencol">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="fontsize" Text="Loading"></asp:Label>
                                                                </td>
                                                                <td class="hiddencol">
                                                                    <asp:DropDownList ID="ddlLoading" runat="server" AppendDataBoundItems="True" CssClass="ddl100"
                                                                        onchange="javascript:return callddlCurrency('#ContentPlaceHolder1_ddlLoading','#ContentPlaceHolder1_txtLoadingExchange');">
                                                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="hiddencol">
                                                                    <asp:TextBox ID="txtLoadingExchange" runat="server" CssClass="textbox75" onblur="javascript:return CheckExchange(this.id);"
                                                                        onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlLoading','ContentPlaceHolder1_txtLoadingExchange','Please select loading currency');">0</asp:TextBox>
                                                                </td>
                                                                <td class="hiddencol">
                                                                    <asp:TextBox ID="txtLoadingRate" runat="server" CssClass="textbox75" onblur="CIFAmtCalculation('ContentPlaceHolder1_txtLoadingExchange','ContentPlaceHolder1_txtLoadingRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtLoadingAmount','ContentPlaceHolder1_txtLoadingINRAmount');"
                                                                        OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                </td>
                                                                <td class="hiddencol">
                                                                    <asp:TextBox ID="txtLoadingAmount" runat="server" CssClass="textbox75" onblur="CIFRateCalculation('ContentPlaceHolder1_txtLoadingExchange','ContentPlaceHolder1_txtLoadingAmount','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtLoadingRate','ContentPlaceHolder1_txtLoadingINRAmount');"
                                                                        OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                </td>
                                                                <td class="hiddencol">
                                                                    <asp:TextBox ID="txtLoadingINRAmount" runat="server" CssClass="textbox75" onkeypress="javascript:return txtboxdisable();">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style6">
                                                                    <asp:Label ID="Label15" runat="server" CssClass="fontsize" Text="Addl Chrg(High Sea) "></asp:Label>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:DropDownList ID="ddlHighSea" runat="server" AppendDataBoundItems="True" CssClass="ddl100"
                                                                        onchange="javascript:return callddlCurrency('#ContentPlaceHolder1_ddlHighSea','#ContentPlaceHolder1_txtHighExRate');">
                                                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtHighExRate" runat="server" CssClass="textbox75" onblur="javascript:return CheckExchange(this.id);"
                                                                        onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlLoading','ContentPlaceHolder1_txtLoadingExchange','Please select loading currency');">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtHighRate" runat="server" CssClass="textbox75" onblur="CIFAmtCalculation('ContentPlaceHolder1_txtHighExRate','ContentPlaceHolder1_txtHighRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtHighAmt','ContentPlaceHolder1_txtHighAmtINR');"
                                                                        OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtHighAmt" runat="server" CssClass="textbox75" onblur="CIFRateCalculation('ContentPlaceHolder1_txtHighExRate','ContentPlaceHolder1_txtHighAmt','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtHighRate','ContentPlaceHolder1_txtHighAmtINR');"
                                                                        OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                    <asp:TextBox ID="txtHighAmtINR" runat="server" CssClass="textbox75" onkeypress="javascript:return txtboxdisable();">0</asp:TextBox>
                                                                </td>
                                                                <td class="tdcolumn75">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style6">
                                                                    <asp:Label ID="lblSaleCondition" runat="server" CssClass="fontsize" Text="Sale Condition"></asp:Label>
                                                                </td>
                                                                <td class="tdcolumn75" colspan="6">
                                                                    <asp:TextBox ID="txtSaleCondition" runat="server" CssClass="textbox400"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdcolumn75" colspan="7">
                                                                    <asp:Label ID="Label11" runat="server" CssClass="fontsize" Text="Any other relevant info which has a bearing on value"
                                                                        Width="400px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdcolumn75">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="tdcolumn75" colspan="6">
                                                                    <asp:TextBox ID="txtotherRelevant" runat="server" CssClass="textbox400"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Button ID="btnSaveFreight" runat="server" CssClass="stylebutton" OnClick="btnSaveFreight_Click"
                                                                        Text="Save" />
                                                                    <asp:Button ID="btnCancelFreight" runat="server" CssClass="stylebutton" OnClick="btnCancelFreight_Click"
                                                                        OnClientClick="return confirm ('Do you want to Clear the data?')" Text="Cancel" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                                            <table border="1px" width="80%">
                                                <tr>
                                                    <td colspan="3" style="text-align: center; font-weight: 700">
                                                        Consignor, Seller &amp; Agent
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdcolumn150">
                                                        <asp:Label ID="lblConsignorName" runat="server" CssClass="fontsize" Text="Consignor's Name and Address"></asp:Label>
                                                    </td>
                                                    <td class="tdcolumn150">
                                                        <table>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtConsignorName" runat="server" CssClass="textboxW400"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetSearchCustomer"
                                                                        ServicePath="~/AutoComplete.asmx" TargetControlID="txtConsignorName">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtConsignor" runat="server" CssClass="textbox400" TextMode="MultiLine"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Button ID="btnConsCheck" runat="server" CssClass="style1003" Height="45px" OnClick="btnConsCheck_Click"
                                                            Text="CHECK" Width="60px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdcolumn150">
                                                        <asp:Label ID="lblConsignorCountry" runat="server" CssClass="fontsize" Text="Country"></asp:Label>
                                                    </td>
                                                    <td class="tdcolumn150">
                                                        <asp:DropDownList ID="ddlConsignorCountry" runat="server" AppendDataBoundItems="True"
                                                            CssClass="textboxW400">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdcolumn150">
                                                        <asp:Label ID="lblSeller" runat="server" CssClass="fontsize" Text="Seller's Name and Address(If Consignor is not the seller)"></asp:Label>
                                                    </td>
                                                    <td class="tdcolumn150">
                                                        <table>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtSeller" runat="server" CssClass="textboxW400"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtSellerName" runat="server" CssClass="textbox400" TextMode="MultiLine"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdcolumn150">
                                                        <asp:Label ID="lblSellerCountry" runat="server" CssClass="fontsize" Text="Country"></asp:Label>
                                                    </td>
                                                    <td class="tdcolumn150">
                                                        <asp:DropDownList ID="ddlSellerCountry" runat="server" AppendDataBoundItems="True"
                                                            CssClass="textboxW400">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdcolumn150">
                                                        <asp:Label ID="lblBroker" runat="server" CssClass="fontsize" Text="Broker/Agent's Name and Address"></asp:Label>
                                                    </td>
                                                    <td class="tdcolumn150">
                                                        <table>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtBroker" runat="server" CssClass="textboxW400"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtBrokerName" runat="server" CssClass="textbox400" TextMode="MultiLine"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdcolumn150">
                                                        <asp:Label ID="lblBrokerCountry" runat="server" CssClass="fontsize" Text="Country"></asp:Label>
                                                    </td>
                                                    <td class="tdcolumn150">
                                                        <asp:DropDownList ID="ddlBrokerCountry" runat="server" AppendDataBoundItems="True"
                                                            CssClass="textboxW400">
                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Button ID="btnSaveConsignor" runat="server" CssClass="stylebutton" OnClick="btnSaveConsignor_Click"
                                                            Text="Save" />
                                                        <asp:Button ID="btnCancelConsignor" runat="server" CssClass="stylebutton" OnClick="btnCancelConsignor_Click"
                                                            OnClientClick="return confirm ('Do you want to Clear the data?')" Text="Cancel" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel3" runat="server" Visible="False">
                                            <table border="1px" width="80%">
                                                <tr>
                                                    <td style="text-align: center; font-weight: 700">
                                                        Relation and SVB Details
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:CheckBox ID="chkBuyer" runat="server" AutoPostBack="True" CssClass="fontsize"
                                                            OnCheckedChanged="chkBuyer_CheckedChanged" Text="Are Buyer and Seller Related ?" />
                                                        <asp:Panel ID="pnlBuyer" runat="server" BorderColor="Black" Width="300px">
                                                            <table>
                                                                <tr>
                                                                    <td class="style11">
                                                                        <asp:Label ID="lblRelation" runat="server" CssClass="fontsize" Text="Relation"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn150">
                                                                        <asp:TextBox ID="txtRelation" runat="server" CssClass="textbox150" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                    <td rowspan="3">
                                                                        <asp:Button ID="btnSVBCheck" runat="server" Height="45px" OnClick="btnSVBCheck_Click"
                                                                            Text="To Check SVB" Width="100px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style11">
                                                                        <asp:Label ID="lblBase" runat="server" CssClass="fontsize" Text="Base"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn150">
                                                                        <asp:TextBox ID="txtRelationBase" runat="server" CssClass="textbox150" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style11">
                                                                        <asp:Label ID="lblCondition" runat="server" CssClass="fontsize" Text="Condition"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn150">
                                                                        <asp:TextBox ID="txtRelationCondition" runat="server" CssClass="textbox150" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:CheckBox ID="chkSVB" runat="server" AutoPostBack="true" CssClass="fontsize"
                                                            OnCheckedChanged="chkSVB_CheckedChanged" Text="SVB Loading ?" />
                                                        <asp:Panel ID="pnlSVB" runat="server" BorderColor="Black" Width="100%">
                                                            <table>
                                                                <tr>
                                                                    <td width="75px">
                                                                        <asp:Label ID="lblSVBRelation" runat="server" CssClass="fontsize" Text="SVB Reference No"
                                                                            Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtSVBRelation" runat="server" CssClass="textbox100" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtSVBDate" runat="server" CssClass="textbox75" Enabled="False"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtSVBDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCustomHouse" runat="server" CssClass="fontsize" Text="Custom House"
                                                                            Width="100px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCustomHouse" runat="server" CssClass="textbox75" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblLoadingOn" runat="server" CssClass="fontsize" Text="Loading On"></asp:Label>
                                                                    </td>
                                                                    <td align="left" width="100px">
                                                                        <asp:DropDownList ID="ddlLoadingOn" runat="server" AppendDataBoundItems="True" CssClass="ddl100"
                                                                            Enabled="False">
                                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                            <asp:ListItem Value="A">Assessable</asp:ListItem>
                                                                            <asp:ListItem Value="D">Duty</asp:ListItem>
                                                                            <asp:ListItem Value="B">Assessable &amp; Duty</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdcolumn200">
                                                                        <asp:Label ID="lblLoadingRate" runat="server" CssClass="fontsize" Text="Loading Rate(Assbl.)"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtLoadingRateAssbl" runat="server" CssClass="textbox100" Enabled="False">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:Label ID="lblLoadingAssblStatus" runat="server" CssClass="fontsize" Text="Status"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:DropDownList ID="ddlLoadingAssblStatus" runat="server" AppendDataBoundItems="True"
                                                                            CssClass="ddl100" Enabled="False" Height="16px">
                                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                            <asp:ListItem Value="P">Provisional</asp:ListItem>
                                                                            <asp:ListItem Value="F">Final</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdcolumn200">
                                                                        <asp:Label ID="lblLoadingDuty" runat="server" CssClass="fontsize" Text="Loading Rate(Duty)"></asp:Label>
                                                                    </td>
                                                                    <td align="left" width="75px">
                                                                        <asp:TextBox ID="txtLoadingDuty" runat="server" CssClass="textbox75" Enabled="False">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:Label ID="lblLoadingDutyStatus" runat="server" CssClass="fontsize" Text="Status"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:DropDownList ID="ddlLoadingDutyStatus" runat="server" AppendDataBoundItems="True"
                                                                            CssClass="ddl100" Enabled="False">
                                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                            <asp:ListItem Value="P">Provisional</asp:ListItem>
                                                                            <asp:ListItem Value="F">Final</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnSaveRelation" runat="server" CssClass="stylebutton" OnClick="btnSaveRelation_Click"
                                                            Text="Save" />
                                                        <asp:Button ID="btnCancelRelation" runat="server" CssClass="stylebutton" OnClick="btnCancelRelation_Click"
                                                            OnClientClick="return confirm ('Do you want to Clear the data?')" Text="Cancel" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel4" runat="server" Visible="False">
                                            <table>
                                                <tr>
                                                    <td colspan="8" style="text-align: center; font-weight: 700">
                                                        Other Details
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNoofProduct" runat="server" CssClass="fontsize" Text="No of Product"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtNoofProd" runat="server" CssClass="textbox100"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:CheckBox ID="chkSinglePO" runat="server" CssClass="fontsize" Text="Single PO for all Product" />
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblContractNo" runat="server" CssClass="fontsize" Text="Contract No/Dt"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtContractNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtContractDate" runat="server" CssClass="textbox75"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtContractDate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblContractNo0" runat="server" CssClass="fontsize" Text="PO No"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPONo" runat="server" CssClass="textbox100"></asp:TextBox>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblLC" runat="server" CssClass="fontsize" Text="LC No/Dt"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtLCNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLCDate" runat="server" CssClass="textbox75"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLCDate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblLC0" runat="server" CssClass="fontsize" Text="PO Date"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPODate" runat="server" CssClass="textbox100"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtPODate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtPODate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblValuation" runat="server" CssClass="fontsize" Text="Valuation Method"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="ddlValuation" runat="server" AppendDataBoundItems="True" CssClass="ddl200">
                                                            <asp:ListItem Value="RULE 4">Transcation Value</asp:ListItem>
                                                            <asp:ListItem Value="RULE 5">Identical Goods</asp:ListItem>
                                                            <asp:ListItem Value="RULE 6">Similar Goods</asp:ListItem>
                                                            <asp:ListItem Value="RULE 7">Deductive Value</asp:ListItem>
                                                            <asp:ListItem Value="RULE 7A">Computed Value</asp:ListItem>
                                                            <asp:ListItem Value="RULE 8">Residual Method Others</asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="8">
                                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="8">
                                                        <asp:Button ID="btnSaveOtherDetails" runat="server" CssClass="stylebutton" OnClick="btnSaveOtherDetails_Click"
                                                            OnClientClick="javascript:return ChkProduct();" Text="Save" />
                                                        <asp:Button ID="btnCancelOtherDetails" runat="server" CssClass="stylebutton" OnClick="btnCancelOtherDetails_Click"
                                                            OnClientClick="return confirm ('Do you want to Clear the data?')" Text="Cancel" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td valign="top">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2" align="left" style="text-align: center">
                                    <asp:Label ID="Label30" runat="server" CssClass="fontsizehistory" Style="font-weight: 700"
                                        Text="History"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsizehistory">
                                    Job No
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlJobNo" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        CssClass="ddl150" Height="20px" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged"
                                        Width="130px">
                                        <asp:ListItem>~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsizehistory">
                                    Job Date
                                </td>
                                <td>
                                    <asp:Label ID="lblJobDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsizehistory">
                                    Importer
                                </td>
                                <td>
                                    <asp:Label ID="lblImporter" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsizehistory">
                                    Currency
                                </td>
                                <td>
                                    <asp:Label ID="lblCurrency" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsizehistory">
                                    Mode
                                </td>
                                <td>
                                    <asp:Label ID="lblMode" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsizehistory">
                                    Custom
                                </td>
                                <td>
                                    <asp:Label ID="lblCustom" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsizehistory">
                                    BE Type
                                </td>
                                <td>
                                    <asp:Label ID="lblBeType" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsizehistory">
                                    Doc Flling Status
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <%-- <tr>
                                <td class="fontsizehistory">
                                    BE No
                                </td>
                                <td>
                                    <asp:Label ID="lblBeNo" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsizehistory">
                                    BE Date
                                </td>
                                <td>
                                    <asp:Label ID="lblBeDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="fontsizehistory">
                                    Total Invoice
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotInv" runat="server" CssClass="textbox100">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsizehistory">
                                    Other Charges
                                </td>
                                <td>
                                    <asp:Label ID="lblOtherCharges" runat="server" CssClass="arealaber1a">0.00</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsizehistory" colspan="2">
                                    <asp:Label ID="lblmsg" runat="server" Style="font-weight: 700"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="Back To Shipment"
                                        CssClass="stylebutton" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <input type="hidden" runat="server" id="hdnEditValue" />
            <input type = "hidden" runat="server" id="hdnTotInvVal" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ValidateInvoice() {
            var InvoiceValue = document.getElementById('ContentPlaceHolder1_txtProductValues').value;
            var TOI = document.getElementById('ContentPlaceHolder1_ddlTermsofInvoice').value;
            var InvoiceCurrency = document.getElementById('ContentPlaceHolder1_ddlInvoiceCurrency').value;
            var Transaction = document.getElementById('ContentPlaceHolder1_ddlTrans').value;
            var PaymentTerms = document.getElementById('ContentPlaceHolder1_ddlPayment').value;
            var InvoiceNo = document.getElementById('ContentPlaceHolder1_txtInvoiceNo').value;
            if (InvoiceNo == "") {
                alert('Please Enter Invoice No');
                return false;
            }

        }

        function ValidateInvoice() {
            var ChargeType = document.getElementById('ContentPlaceHolder1_ddlChargeType').value;
            if (ChargeType == "~Select~") {
                alert('Select Charge Type');
                return false;
            }
            var Currency = document.getElementById('ContentPlaceHolder1_ddlChargeCurrency').value;
            if (Currency == "~Select~") {
                alert('Select Currency');
                return false;
            }
            var Amt = document.getElementById('ContentPlaceHolder1_txtAmount').value;
            if (Amt == "0") {
                alert('Enter Amount');
                return false;
            }
        }
    </script>
</asp:Content>
