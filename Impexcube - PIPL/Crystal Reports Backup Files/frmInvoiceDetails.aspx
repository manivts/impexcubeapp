<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" 
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
        // Call the exchange rate in Freight Type details Currency
        function callddlFreightCurrency() {
            var Sel1 = $("#ContentPlaceHolder1_ddlFreightTypeCurrency").val();
            var exra1 = $("#ContentPlaceHolder1_txtFreightTypeEx"); //
            BindExchgeRate(Sel1, exra1);
        }

        // Call the exchange rate in Insurance Type details Currency
        function callddlInsuranceCurrency() {
            var Sel1 = $("#ContentPlaceHolder1_ddlInsuranceTypeCurrency").val();
            var exra1 = $("#ContentPlaceHolder1_txtInsuranceTypeEx"); //
            BindExchgeRate(Sel1, exra1);
        }

        // Call the exchange rate in Invoice details Currency
      function callddlInvoiceCurrency() {
      var Sel1 = $("#ContentPlaceHolder1_ddlInvoiceCurrency").val();
      var exra1 = $("#ContentPlaceHolder1_txtExchange"); //
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

      function DisableFreight() {
          var FreightType = document.getElementById('ContentPlaceHolder1_ddlFreightTy').value;
          if (FreightType == 'Single freight') {
              document.getElementById('ContentPlaceHolder1_ddlFreightTypeCurrency').disabled = false;
              document.getElementById('ContentPlaceHolder1_txtFreightTypeEx').disabled = false;
              document.getElementById('ContentPlaceHolder1_txtFreightTypeAmount').disabled = false;
              document.getElementById('ContentPlaceHolder1_txtFreightTypeAmountINR').disabled = false;
          }
          else {
              document.getElementById('ContentPlaceHolder1_ddlFreightTypeCurrency').disabled = true;
              document.getElementById('ContentPlaceHolder1_txtFreightTypeEx').disabled = true;
              document.getElementById('ContentPlaceHolder1_txtFreightTypeAmount').disabled = true;
              document.getElementById('ContentPlaceHolder1_txtFreightTypeAmountINR').disabled = true;
          }
      }

      function DisableInsurance() {
          var InsType = document.getElementById('ContentPlaceHolder1_ddlInsuranceTy').value;
          if (InsType == 'Single Insurance') {
              document.getElementById('ContentPlaceHolder1_ddlInsuranceTypeCurrency').disabled = false;
              document.getElementById('ContentPlaceHolder1_txtInsuranceTypeEx').disabled = false;
              document.getElementById('ContentPlaceHolder1_txtInsuranceTypeAmount').disabled = false;
              document.getElementById('ContentPlaceHolder1_txtInsuranceTypeAmountINR').disabled = false;
          }
          else {
              document.getElementById('ContentPlaceHolder1_ddlInsuranceTypeCurrency').disabled = true;
              document.getElementById('ContentPlaceHolder1_txtInsuranceTypeEx').disabled = true;
              document.getElementById('ContentPlaceHolder1_txtInsuranceTypeAmount').disabled = true;
              document.getElementById('ContentPlaceHolder1_txtInsuranceTypeAmountINR').disabled = true;
          }
      }

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
      function ClearInvoiceData() {
          try {
              document.getElementById('ContentPlaceHolder1_txtInvoiceNo').value = '';
              document.getElementById('ContentPlaceHolder1_txtDate').value = '';
              document.getElementById('ContentPlaceHolder1_ddlTermsofInvoice').selectedIndex = 0;
              document.getElementById('ContentPlaceHolder1_ddlFreightType').selectedIndex = 0;
              document.getElementById('ContentPlaceHolder1_ddlPayment').selectedIndex = 0;
              document.getElementById('ContentPlaceHolder1_ddlTrans').selectedIndex = 0;
              document.getElementById('ContentPlaceHolder1_ddlInvoiceCurrency').selectedIndex = 0;
              document.getElementById('ContentPlaceHolder1_txtExchange').value = '';
              document.getElementById('ContentPlaceHolder1_txtProductValues').value = '';
              document.getElementById('ContentPlaceHolder1_txtProductINRValues').value = '';
              alert('fdsf');
              // document.getElementById('ContentPlaceHolder1_btnAddInvoice').style.visibility = 'visible';
              document.getElementById('ContentPlaceHolder1_btnUpdateInvoice').style.visibility = 'hidden';
              return false;
          }
          catch (err) {
              alert(err);
          }
      }


      function Validate() {
          if (document.getElementById('ContentPlaceHolder1_txtInvoiceNo').value == '') {
              alert('Invoice cannot be empty');
              document.getElementById('ContentPlaceHolder1_txtInvoiceNo').focus();
              return false;
          }

          if (document.getElementById('ContentPlaceHolder1_txtDate').value == '') {
              alert('Date cannot be empty');
              document.getElementById('ContentPlaceHolder1_txtDate').focus();
              return false;
          }

          if (document.getElementById('ContentPlaceHolder1_ddlTermsofInvoice').selectedIndex == 0) {
              alert('Please select invoice terms');
              document.getElementById('ContentPlaceHolder1_ddlTermsofInvoice').focus();
              return false;
          }
          FreightCalc();
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
        .style4
        {
            font-family: calibri;
            font-size: 8pt;
            padding-left: 0px;
            color :Purple;
            width:100px;
        }
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
        .style7
        {
            font-family: calibri;
            font-size: 8pt;
            padding-left: 0px;
            color : Purple;
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
            Height: 15px;
            font-family: Verdana;
            Width: 75px;
            font-size: 8pt;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td valign="top">
                        <table>
                            <tr>
                                <td class="style6">
                                    <br />
                                    <asp:Button ID="btnOtherChargesVisible" runat="server" CssClass="stylebtn6" OnClientClick="javascript:return CheckGridSelected();" OnClick="btnOtherChargesVisible_Click"
                                        Text="Other Charges" />
                                    <br />
                                    <asp:Button ID="btnFreightDetails" runat="server" CssClass="stylebtn6" OnClientClick="javascript:return CheckGridSelected();" OnClick="btnFreightDetails_Click"
                                        Text="Freight &amp; Insurance" />
                                    <br />
                                    <asp:Button ID="btnProduct" runat="server" CssClass="stylebtn6" OnClick="btnProduct_Click"
                                        Text="Product" />
                                    <br />
                                    <asp:Button ID="btnConsignor" runat="server" CssClass="stylebtn6" OnClientClick="javascript:return CheckGridSelected();" OnClick="btnConsignor_Click"
                                        Text="Consignor" />
                                    <br />
                                    <asp:Button ID="btnRelationSVBDetails" runat="server" CssClass="stylebtn6" OnClientClick="javascript:return CheckGridSelected();" OnClick="btnRelationSVBDetails_Click"
                                        Text="Relation &amp; SVB" />
                                    <br />
                                    <asp:Button ID="btnOtherDetails" runat="server" CssClass="stylebtn6" OnClientClick="javascript:return CheckGridSelected();" OnClick="btnOtherDetails_Click"
                                        Text="Other Details" />
                                    <br />
                                    <asp:Button ID="btnCheckList" runat="server" CssClass="stylebtn6" OnClick="btnCheckList_Click"
                                        Text="Check List" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Groove" 
                            Width="830px">
                            <table style="width: 800px">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFreight0" runat="server" CssClass="fontsize" 
                                            Text="Freight Type"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFreightTy" runat="server" AppendDataBoundItems="True" onChange="javascript:return DisableFreight();"
                                            CssClass="ddl150">
                                            <asp:ListItem>~Select~</asp:ListItem>
                                            <asp:ListItem>Single freight</asp:ListItem>
                                            <asp:ListItem>Separate freight</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFreight1" runat="server" CssClass="fontsize" Text="Currency"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFreightTypeCurrency" runat="server" 
                                            AppendDataBoundItems="True" CssClass="ddl75" 
                                            onchange="javascript:return callddlFreightCurrency();" Enabled="False">
                                            <asp:ListItem Selected="True" Value="~Select~">~Select~</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFreight2" runat="server" CssClass="fontsize" Text="ROE"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFreightTypeEx" runat="server" CssClass="textbox50" 
                                            Enabled="False">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFreight3" runat="server" CssClass="fontsize" Text="Amount"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFreightTypeAmount" runat="server" CssClass="textbox75" 
                                            onchange="FreightValueINR();" Enabled="False">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProductValues1" runat="server" CssClass="fontsize" 
                                            Text="Amount INR"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFreightTypeAmountINR" runat="server" CssClass="textbox75" 
                                            Enabled="False">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFreight4" runat="server" CssClass="fontsize" 
                                            Text="Insurance Type"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlInsuranceTy" runat="server" 
                                            AppendDataBoundItems="True" CssClass="ddl150" onChange="javascript:return DisableInsurance();">
                                            <asp:ListItem>~Select~</asp:ListItem>
                                            <asp:ListItem>Single Insurance</asp:ListItem>
                                            <asp:ListItem>Separate Insurance</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFreight5" runat="server" CssClass="fontsize" Text="Currency"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlInsuranceTypeCurrency" runat="server" 
                                            AppendDataBoundItems="True" CssClass="ddl75" 
                                            onchange="javascript:return callddlInsuranceCurrency();" Enabled="False">
                                            <asp:ListItem Selected="True" Value="~Select~">~Select~</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFreight8" runat="server" CssClass="fontsize" Text="ROE"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInsuranceTypeEx" runat="server" CssClass="textbox50" 
                                            Enabled="False" EnableTheming="True">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFreight9" runat="server" CssClass="fontsize" Text="Amount"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInsuranceTypeAmount" runat="server" CssClass="textbox75" 
                                            onchange="InsuranceValueINR();" Enabled="False" EnableTheming="True">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProductValues2" runat="server" CssClass="fontsize" 
                                            Text="Amount INR"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInsuranceTypeAmountINR" runat="server" CssClass="textbox75" 
                                            Enabled="False" EnableTheming="True">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table style="width: 800px">
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="lblInvoiceNo" runat="server" CssClass="textbox75" 
                                                        Text="Invoice No"></asp:Label>
                                                </td>
                                                <td class="style8">
                                                    <asp:Label ID="lblDate" runat="server" CssClass="fontsize" Text="Inv.Date"></asp:Label>
                                                </td>
                                                <td class="style9">
                                                    <asp:Label ID="lblTermsofInvoice" runat="server" CssClass="fontsize" 
                                                        Text="Inv.Term"></asp:Label>
                                                </td>
                                               <%-- <td class="tdcolumn100">
                                                    <asp:Label ID="lblFreight" runat="server" CssClass="fontsize" 
                                                        Text="Freight Type"></asp:Label>
                                                </td>--%>
                                                <td class="style9">
                                                    <asp:Label ID="lblPayment" runat="server" CssClass="fontsize" Text="Pay.Mode"></asp:Label>
                                                </td>
                                                <td class="style9">
                                                    <asp:Label ID="lblTrans" runat="server" CssClass="fontsize" 
                                                        Text="Transaction"></asp:Label>
                                                </td>
                                                <td class="style8">
                                                    <asp:Label ID="labelC" runat="server" CssClass="fontsize" Text="Currency"></asp:Label>
                                                </td>
                                                <td class="style8">
                                                    <asp:Label ID="lblExchRates" runat="server" CssClass="fontsize" Text="Ex. Rate"></asp:Label>
                                                </td>
                                                <td class="style8">
                                                    <asp:Label ID="lblProductValues" runat="server" CssClass="fontsize" 
                                                        Text="Amount"></asp:Label>
                                                </td>
                                                <td class="style8">
                                                    <asp:Label ID="lblProductValues0" runat="server" CssClass="fontsize" 
                                                        Text="INR"></asp:Label>
                                                </td>
                                                <td class="tdcolumn" colspan="2">
                                                    <asp:Button ID="btnNew" runat="server" BackColor="#73AAE8" 
                                                        onclick="btnNew_Click" Text="New" CssClass="ui-priority-primary" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textbox75">
                                                    <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                                </td>
                                                <td class="tdcolumn75">
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="textbox75"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                                        TargetControlID="txtDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:DropDownList ID="ddlTermsofInvoice" runat="server" 
                                                        AppendDataBoundItems="True" CssClass="ddl75">
                                                        <asp:ListItem>~Select~</asp:ListItem>
                                                        <asp:ListItem>FOB</asp:ListItem>
                                                        <asp:ListItem>CIF</asp:ListItem>
                                                        <asp:ListItem>C&amp;F</asp:ListItem>
                                                        <asp:ListItem>C&amp;I</asp:ListItem>
                                                        <asp:ListItem>CIFC</asp:ListItem>
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
                                                    <asp:DropDownList ID="ddlPayment" runat="server" AppendDataBoundItems="True" 
                                                        CssClass="ddl75">
                                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                        <asp:ListItem>LC</asp:ListItem>
                                                        <asp:ListItem>DP</asp:ListItem>
                                                        <asp:ListItem>FOC</asp:ListItem>
                                                        <asp:ListItem>DA</asp:ListItem>
                                                        <asp:ListItem>SD</asp:ListItem>
                                                        <asp:ListItem>Others</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="tdcolumn75">
                                                    <asp:DropDownList ID="ddlTrans" runat="server" AppendDataBoundItems="True" 
                                                        CssClass="ddl100">
                                                        <asp:ListItem>~Select~</asp:ListItem>
                                                        <asp:ListItem>Sale</asp:ListItem>
                                                        <asp:ListItem>Consignment</asp:ListItem>
                                                        <asp:ListItem>Hire</asp:ListItem>
                                                        <asp:ListItem>Rent</asp:ListItem>
                                                        <asp:ListItem>Gift</asp:ListItem>
                                                        <asp:ListItem>Sample</asp:ListItem>
                                                        <asp:ListItem>Free of Cost</asp:ListItem>
                                                        <asp:ListItem>DONATION FOR GOVT.</asp:ListItem>
                                                        <asp:ListItem>Others</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="tdcolumn75">
                                                    <asp:DropDownList ID="ddlInvoiceCurrency" runat="server" 
                                                        AppendDataBoundItems="True" CssClass="ddl100" 
                                                        onchange="javascript:return callddlInvoiceCurrency();">
                                                        <asp:ListItem Selected="True" Value="~Select~">~Select~</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchange" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtProductValues" runat="server" CssClass="textbox75" 
                                                        onchange="ProductValueINR();">0</asp:TextBox>
                                                </td>
                                                <td class="tdcolumn">
                                                    <asp:TextBox ID="txtProductINRValues" runat="server" CssClass="textbox75">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnAddInvoice" runat="server" 
                                                        ImageUrl="~/Content/Images/Add.jpg" OnClick="btnAddInvoice_Click" 
                                                        OnClientClick="javascript:return Validate();" ToolTip="Add" />
                                                    <asp:ImageButton ID="btnUpdateInvoice" runat="server" 
                                                        ImageUrl="~/Content/Images/Add.jpg" OnClick="btnUpdateInvoice_Click" 
                                                        OnClientClick="javascript:return Validate();" Visible="False" 
                                                        ToolTip="Update" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="center" colspan="12">
                                                    <asp:GridView ID="gvInvoiceDetails" runat="server" AutoGenerateColumns="False" 
                                                        AutoGenerateSelectButton="True" BorderColor="Black" BorderStyle="Solid" 
                                                        BorderWidth="1px" Font-Names="calibri" Font-Size="10pt" ForeColor="Black" 
                                                        GridLines="Vertical" 
                                                        OnSelectedIndexChanged="gvInvoiceDetails_SelectedIndexChanged" 
                                                        ShowFooter="True" ShowHeader="true" 
                                                        Style="text-align: center; font-size: 9pt;" Visible="false" Width="820px">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnDelete" runat="server" 
                                                                        ImageUrl="~/Content/Images/delete.gif" OnClientClick="return confirm('Do U Want Delete?');" onclick="btnDelete_Click" Width="20px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="InvoiceDetailsId" HeaderStyle-CssClass="hiddencol" 
                                                                HeaderText="Idfg" ItemStyle-CssClass="hiddencol" />
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
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="10">

                                                <asp:Panel ID="PanelOtherDetails" runat="server" Visible="False">
                                                <table border="1px" width="80%">
                                                    <tr>
                                                        <td colspan="5" style="text-align: center; font-weight: 700">
                                                            Other Charges</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdcolumn100">
                                                            <asp:Label ID="lblChargeType" runat="server" CssClass="fontsize" 
                                                                Text="Charge Type"></asp:Label>
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
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdcolumn100">
                                                            <asp:DropDownList ID="ddlChargeType" runat="server" AppendDataBoundItems="True" 
                                                                CssClass="ddl200">
                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="tdcolumn75">
                                                            <asp:DropDownList ID="ddlChargeCurrency" runat="server" 
                                                                AppendDataBoundItems="True" CssClass="ddl75" 
                                                                onchange="javascript:return callddlChargeCurrency();">
                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="tdcolumn75">
                                                            <asp:TextBox ID="txtRate" runat="server" CssClass="textbox75" 
                                                                OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                        </td>
                                                        <td class="tdcolumn75">
                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                                        </td>
                                                        <td class="tdcolumn50">
                                                            <asp:ImageButton ID="btnOtherCharges" runat="server" 
                                                                ImageUrl="~/Content/Images/Add.jpg" OnClick="btnOtherCharges_Click" 
                                                                Visible="False" />
                                                            <asp:ImageButton ID="btnAddOtherCharges" runat="server" 
                                                                ImageUrl="~/Content/Images/Add.jpg" OnClick="btnAddOtherCharges_Click" />
                                                            <asp:Button ID="btnCancelOtherCharges" runat="server" BackColor="#73AAE8" OnClick="btnCancelOtherCharges_Click" 
                                                                OnClientClick="return confirm ('Do you want to Clear the data?')" 
                                                                Text="New" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5">
                                                            <asp:GridView ID="gvOtherCharges" runat="server" AutoGenerateColumns="False" 
                                                                AutoGenerateSelectButton="True" CellPadding="4" ForeColor="#333333" 
                                                                GridLines="Vertical" 
                                                                OnSelectedIndexChanged="gvOtherCharges_SelectedIndexChanged" 
                                                                Style="font-size: 9pt; font-family: calibri" Width="589px">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ChargeType" HeaderText="Charge Type" />
                                                                    <asp:BoundField DataField="Currency" HeaderText="Currency" />
                                                                    <asp:BoundField DataField="ExchRate" HeaderText="Ex. Rate" />
                                                                    <asp:BoundField DataField="ChargeAmount" HeaderText="Amount" />
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
                                                        <td align="right" colspan="3">
                                                            <asp:Label ID="lblInvoiceChoice" runat="server" CssClass="fontsize" 
                                                                Text="Total  Amount" Visible="False"></asp:Label>
                                                            <asp:TextBox ID="txtInvoiceChoices" runat="server" CssClass="textbox75" 
                                                                Visible="False">0</asp:TextBox>
                                                            <asp:Label ID="lblTotalProduct" runat="server" CssClass="fontsize" 
                                                                Text="Invoice Value" Visible="False"></asp:Label>
                                                            <asp:TextBox ID="txtProductValue" runat="server" CssClass="textbox75" 
                                                                Visible="False">0</asp:TextBox>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:Label ID="lblOhterCharges" runat="server" CssClass="fontsize" 
                                                                Text="Total Other Charges"></asp:Label>
                                                            <asp:TextBox ID="txtOtherCharges" runat="server" CssClass="textbox75"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                </asp:Panel>
                                                
                                             <asp:Panel ID="PanelFreight" runat="server" Visible="False">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table border="1px" width="500">
                                                                <tr>
                                                                    <td colspan="7" style="text-align: center; font-weight: 700">
                                                                        Freight , Insurance &amp; Miscellaneous
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdcolumn75">
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:Label ID="lblFreightCurrency" runat="server" CssClass="fontsize" 
                                                                            Text="Currency"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75" colspan="2">
                                                                        <asp:Label ID="lblFreightExchange" runat="server" CssClass="fontsize" 
                                                                            Text="Exchg.Rate"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:Label ID="lblFreightRate" runat="server" CssClass="fontsize" Text="Rate%"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:Label ID="lblbFreightAmount" runat="server" CssClass="fontsize" 
                                                                            Text="Amount"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:Label ID="Label12" runat="server" CssClass="fontsize" Text="Amount INR"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdcolumn75">
                                                                        <asp:Label ID="Label9" runat="server" CssClass="fontsize" Text="Freight"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:DropDownList ID="ddlFreightDetails" runat="server" 
                                                                            AppendDataBoundItems="True" CssClass="ddl100" 
                                                                            onchange="javascript:return callddlFreightDetails();">
                                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="tdcolumn75" colspan="2">
                                                                        <asp:TextBox ID="txtFreightExchange" runat="server" CssClass="textbox75" 
                                                                            onblur="javascript:return CheckExchange(this.id);" 
                                                                            onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlFreightDetails','ContentPlaceHolder1_txtFreightExchange','Please select freight currency');">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtFreightRate" runat="server" CssClass="textbox75" 
                                                                            onblur="CIFAmtCalculation('ContentPlaceHolder1_txtFreightExchange','ContentPlaceHolder1_txtFreightRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtFreightAmount','ContentPlaceHolder1_txtFreightINRAmount');" 
                                                                            OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtFreightAmount" runat="server" CssClass="textbox75" 
                                                                            onblur="CIFRateCalculation('ContentPlaceHolder1_txtFreightExchange','ContentPlaceHolder1_txtFreightAmount','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtFreightRate','ContentPlaceHolder1_txtFreightINRAmount');" 
                                                                            OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtFreightINRAmount" runat="server" CssClass="textbox75" 
                                                                            onkeypress="javascript:return txtboxdisable();">0</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdcolumn75">
                                                                        <asp:Label ID="Label10" runat="server" CssClass="fontsize" Text="Insurance"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:DropDownList ID="ddlInsurance" runat="server" AppendDataBoundItems="True" 
                                                                            CssClass="ddl100" Height="16px" 
                                                                            onchange="javascript:return callddlInsurance();">
                                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="tdcolumn75" colspan="2">
                                                                        <asp:TextBox ID="txtInsuranceExchange" runat="server" CssClass="textbox75" 
                                                                            onblur="javascript:return CheckExchange(this.id);" 
                                                                            onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlInsurance','ContentPlaceHolder1_txtInsuranceExchange','Please select insurance currency');">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtInsuranceRate" runat="server" CssClass="textbox75" 
                                                                            onblur="CIFAmtCalculation('ContentPlaceHolder1_txtInsuranceExchange','ContentPlaceHolder1_txtInsuranceRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtInsuranceAmount','ContentPlaceHolder1_txtInsuranceINRAmount');" 
                                                                            OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtInsuranceAmount" runat="server" AutoPostBack="True" 
                                                                            CssClass="textbox75" 
                                                                            onblur="CIFRateCalculation('ContentPlaceHolder1_txtInsuranceExchange','ContentPlaceHolder1_txtInsuranceAmount','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtInsuranceRate','ContentPlaceHolder1_txtInsuranceINRAmount');" 
                                                                            OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtInsuranceINRAmount" runat="server" CssClass="textbox75" 
                                                                            onkeypress="javascript:return txtboxdisable();">0</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdcolumn75">
                                                                        <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="Discounts"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:DropDownList ID="ddlDiscount" runat="server" AppendDataBoundItems="True" 
                                                                            CssClass="ddl100" onchange="javascript:return callddlDiscount();">
                                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="tdcolumn75" colspan="2">
                                                                        <asp:TextBox ID="txtDiscountExchange" runat="server" CssClass="textbox75" 
                                                                            onblur="javascript:return CheckExchange(this.id);" 
                                                                            onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlDiscount','ContentPlaceHolder1_txtDiscountExchange','Please select discount currency');">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtDiscountRate" runat="server" CssClass="textbox75" 
                                                                            onblur="CIFAmtCalculation('ContentPlaceHolder1_txtDiscountExchange','ContentPlaceHolder1_txtDiscountRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtDiscountAmount','ContentPlaceHolder1_txtDiscountINRAmount');" 
                                                                            OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtDiscountAmount" runat="server" AutoPostBack="True" 
                                                                            CssClass="textbox75" 
                                                                            onblur="CIFRateCalculation('ContentPlaceHolder1_txtDiscountExchange','ContentPlaceHolder1_txtDiscountAmount','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtDiscountRate','ContentPlaceHolder1_txtDiscountINRAmount');" 
                                                                            OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtDiscountINRAmount" runat="server" CssClass="textbox75" 
                                                                            onkeypress="javascript:return txtboxdisable();">0</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdcolumn75">
                                                                        <asp:Label ID="lblMiscellanous" runat="server" CssClass="fontsize" 
                                                                            Text="Miscellaneous"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:DropDownList ID="ddlMiscellameous" runat="server" 
                                                                            AppendDataBoundItems="True" CssClass="ddl100" 
                                                                            onchange="javascript:return callddlMiscellameous();">
                                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="tdcolumn75" colspan="2">
                                                                        <asp:TextBox ID="txtMiscellameousExchange" runat="server" CssClass="textbox75" 
                                                                            onblur="javascript:return CheckExchange(this.id);" 
                                                                            onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlMiscellameous','ContentPlaceHolder1_txtMiscellameousExchange','Please select miscellaneous currency');">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtMiscellameousRate" runat="server" CssClass="textbox75" 
                                                                            onblur="CIFAmtCalculation('ContentPlaceHolder1_txtMiscellameousExchange','ContentPlaceHolder1_txtMiscellameousRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtMiscellameousAmount','ContentPlaceHolder1_txtMiscellameousINRAmount');" 
                                                                            OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtMiscellameousAmount" runat="server" CssClass="textbox75" 
                                                                            onblur="CIFRateCalculation('ContentPlaceHolder1_txtMiscellameousExchange','ContentPlaceHolder1_txtMiscellameousAmount','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtMiscellameousRate','ContentPlaceHolder1_txtMiscellameousINRAmount');" 
                                                                            OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtMiscellameousINRAmount" runat="server" CssClass="textbox75" 
                                                                            onkeypress="javascript:return txtboxdisable();">0</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdcolumn75">
                                                                        <asp:Label ID="lblAgency" runat="server" CssClass="fontsize" Text="Agency"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:DropDownList ID="ddlAgency" runat="server" AppendDataBoundItems="True" 
                                                                            CssClass="ddl100" Height="16px" onchange="javascript:return callddlAgency();">
                                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="tdcolumn75" colspan="2">
                                                                        <asp:TextBox ID="txtAgencyExchange" runat="server" CssClass="textbox75" 
                                                                            onblur="javascript:return CheckExchange(this.id);" 
                                                                            onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlAgency','ContentPlaceHolder1_txtAgencyExchange','Please select agent currency');">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtAgencyRate" runat="server" CssClass="textbox75" 
                                                                            onblur="CIFAmtCalculation('ContentPlaceHolder1_txtAgencyExchange','ContentPlaceHolder1_txtAgencyRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtAgencyAmount','ContentPlaceHolder1_txtAgencyINRAmount');" 
                                                                            OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtAgencyAmount" runat="server" CssClass="textbox75" 
                                                                            onblur="CIFRateCalculation('ContentPlaceHolder1_txtAgencyExchange','ContentPlaceHolder1_txtAgencyAmount','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtAgencyRate','ContentPlaceHolder1_txtAgencyINRAmount');" 
                                                                            OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtAgencyINRAmount" runat="server" CssClass="textbox75" 
                                                                            onkeypress="javascript:return txtboxdisable();">0</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdcolumn75">
                                                                        <asp:Label ID="Label8" runat="server" CssClass="fontsize" Text="Loading"></asp:Label>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:DropDownList ID="ddlLoading" runat="server" AppendDataBoundItems="True" 
                                                                            CssClass="ddl100" onchange="javascript:return callddlLoading();">
                                                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="tdcolumn75" colspan="2">
                                                                        <asp:TextBox ID="txtLoadingExchange" runat="server" CssClass="textbox75" 
                                                                            onblur="javascript:return CheckExchange(this.id);" 
                                                                            onkeypress="javascript:return CheckCurrency(event,'ContentPlaceHolder1_ddlLoading','ContentPlaceHolder1_txtLoadingExchange','Please select loading currency');">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtLoadingRate" runat="server" CssClass="textbox75" 
                                                                            onblur="CIFAmtCalculation('ContentPlaceHolder1_txtLoadingExchange','ContentPlaceHolder1_txtLoadingRate','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtLoadingAmount','ContentPlaceHolder1_txtLoadingINRAmount');" 
                                                                            OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtLoadingAmount" runat="server" CssClass="textbox75" 
                                                                            onblur="CIFRateCalculation('ContentPlaceHolder1_txtLoadingExchange','ContentPlaceHolder1_txtLoadingAmount','ContentPlaceHolder1_txtProductINRValues','ContentPlaceHolder1_txtLoadingRate','ContentPlaceHolder1_txtLoadingINRAmount');" 
                                                                            OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                                                    </td>
                                                                    <td class="tdcolumn75">
                                                                        <asp:TextBox ID="txtLoadingINRAmount" runat="server" CssClass="textbox75" 
                                                                            onkeypress="javascript:return txtboxdisable();"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <asp:Button ID="btnSaveFreight" runat="server" CssClass="stylebutton" 
                                                                            OnClick="btnSaveFreight_Click" Text="Save" />
                                                                        <asp:Button ID="btnCancelFreight" runat="server" CssClass="stylebutton" 
                                                                            OnClick="btnCancelFreight_Click" 
                                                                            OnClientClick="return confirm ('Do you want to Clear the data?')" 
                                                                            Text="Cancel" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <table style="width: 241px">
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:Label ID="lblSaleCondition" runat="server" CssClass="fontsize" 
                                                                            Text="Sale Condition"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtSaleCondition" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:Label ID="Label11" runat="server" CssClass="fontsize" 
                                                                            Text="Any other relevant info which has a bearing on value"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtotherRelevant" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                            <asp:Label ID="lblConsignorName" runat="server" CssClass="fontsize" 
                                                                Text="Consignor's Name and Address"></asp:Label>
                                                        </td>
                                                        <td class="tdcolumn150">
                                                            <table>
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <asp:TextBox ID="txtConsignorName" runat="server" CssClass="textboxW400"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtConsignor" runat="server" CssClass="textbox400" 
                                                                            TextMode="MultiLine"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnNewConsignor" runat="server" Height="20px" 
                                                                OnClientClick=" popupwindow('frmPopUpConsigner.aspx?mode=cnsr');" Text="?" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdcolumn150">
                                                            <asp:Label ID="lblConsignorCountry" runat="server" CssClass="fontsize" 
                                                                Text="Country"></asp:Label>
                                                        </td>
                                                        <td class="tdcolumn150">
                                                            <asp:DropDownList ID="ddlConsignorCountry" runat="server" 
                                                                AppendDataBoundItems="True" CssClass="textboxW400">
                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdcolumn150">
                                                            <asp:Label ID="lblSeller" runat="server" CssClass="fontsize" 
                                                                Text="Seller's Name and Address(If Consignor is not the seller)"></asp:Label>
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
                                                                        <asp:TextBox ID="txtSellerName" runat="server" CssClass="textbox400" 
                                                                            TextMode="MultiLine"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnSellerNew" runat="server" Height="20px" 
                                                                OnClientClick=" popupwindow('frmPopUpConsigner.aspx?mode=seller');" Text="?" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdcolumn150">
                                                            <asp:Label ID="lblSellerCountry" runat="server" CssClass="fontsize" 
                                                                Text="Country"></asp:Label>
                                                        </td>
                                                        <td class="tdcolumn150">
                                                            <asp:DropDownList ID="ddlSellerCountry" runat="server" 
                                                                AppendDataBoundItems="True" CssClass="textboxW400">
                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdcolumn150">
                                                            <asp:Label ID="lblBroker" runat="server" CssClass="fontsize" 
                                                                Text="Broker/Agent's Name and Address"></asp:Label>
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
                                                                        <asp:TextBox ID="txtBrokerName" runat="server" CssClass="textbox400" 
                                                                            TextMode="MultiLine"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnBrokerNew" runat="server" Height="20px" 
                                                                OnClientClick=" popupwindow('frmPopUpConsigner.aspx?mode=agent');" Text="?" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdcolumn150">
                                                            <asp:Label ID="lblBrokerCountry" runat="server" CssClass="fontsize" 
                                                                Text="Country"></asp:Label>
                                                        </td>
                                                        <td class="tdcolumn150">
                                                            <asp:DropDownList ID="ddlBrokerCountry" runat="server" 
                                                                AppendDataBoundItems="True" CssClass="textboxW400">
                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Button ID="btnSaveConsignor" runat="server" CssClass="stylebutton" 
                                                                OnClick="btnSaveConsignor_Click" Text="Save" />
                                                            <asp:Button ID="btnCancelConsignor" runat="server" CssClass="stylebutton" 
                                                                OnClick="btnCancelConsignor_Click" 
                                                                OnClientClick="return confirm ('Do you want to Clear the data?')" 
                                                                Text="Cancel" />
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
                                                            <asp:CheckBox ID="chkBuyer" runat="server" AutoPostBack="True" 
                                                                CssClass="fontsize" OnCheckedChanged="chkBuyer_CheckedChanged" 
                                                                Text="Are Buyer and Seller Related ?" />
                                                            <asp:Panel ID="pnlBuyer" runat="server" BorderColor="Black" Width="300px">
                                                                <table>
                                                                    <tr>
                                                                        <td class="tdcolumn150">
                                                                            <asp:Label ID="lblRelation" runat="server" CssClass="fontsize" Text="Relation"></asp:Label>
                                                                        </td>
                                                                        <td class="tdcolumn150">
                                                                            <asp:TextBox ID="txtRelation" runat="server" CssClass="textbox150" 
                                                                                Enabled="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="tdcolumn150">
                                                                            <asp:Label ID="lblBase" runat="server" CssClass="fontsize" Text="Base"></asp:Label>
                                                                        </td>
                                                                        <td class="tdcolumn150">
                                                                            <asp:TextBox ID="txtRelationBase" runat="server" CssClass="textbox150" 
                                                                                Enabled="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="tdcolumn150">
                                                                            <asp:Label ID="lblCondition" runat="server" CssClass="fontsize" 
                                                                                Text="Condition"></asp:Label>
                                                                        </td>
                                                                        <td class="tdcolumn150">
                                                                            <asp:TextBox ID="txtRelationCondition" runat="server" CssClass="textbox150" 
                                                                                Enabled="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:CheckBox ID="chkSVB" runat="server" AutoPostBack="true" 
                                                                CssClass="fontsize" OnCheckedChanged="chkSVB_CheckedChanged" 
                                                                Text="SVB Loading ?" />
                                                            <asp:Panel ID="pnlSVB" runat="server" BorderColor="Black" Width="100%">
                                                                <table>
                                                                    <tr>
                                                                        <td class="tdcolumn75">
                                                                            <asp:Label ID="lblSVBRelation" runat="server" CssClass="fontsize" 
                                                                                Text="SVB Reference No" Width="150px"></asp:Label>
                                                                        </td>
                                                                        <td class="tdcolumn75">
                                                                            <asp:TextBox ID="txtSVBRelation" runat="server" CssClass="textbox100" 
                                                                                Enabled="False"></asp:TextBox>
                                                                        </td>
                                                                        <td class="tdcolumn75">
                                                                            <asp:TextBox ID="txtSVBDate" runat="server" CssClass="textbox75" 
                                                                                Enabled="False"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                                                                                TargetControlID="txtSVBDate"></cc1:CalendarExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCustomHouse" runat="server" CssClass="fontsize" 
                                                                                Text="Custom House" Width="100px"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCustomHouse" runat="server" CssClass="textbox75" 
                                                                                Enabled="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="tdcolumn100">
                                                                            <asp:Label ID="lblLoadingOn" runat="server" CssClass="fontsize" 
                                                                                Text="Loading On"></asp:Label>
                                                                        </td>
                                                                        <td class="tdcolumn100">
                                                                            <asp:DropDownList ID="ddlLoadingOn" runat="server" AppendDataBoundItems="True" 
                                                                                CssClass="ddl100" Enabled="False">
                                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="tdcolumn200">
                                                                            <asp:Label ID="lblLoadingRate" runat="server" CssClass="fontsize" 
                                                                                Text="Loading Rate(Assbl.)"></asp:Label>
                                                                        </td>
                                                                        <td class="tdcolumn75">
                                                                            <asp:TextBox ID="txtLoadingRateAssbl" runat="server" CssClass="textbox100" 
                                                                                Enabled="False">0</asp:TextBox>
                                                                        </td>
                                                                        <td class="tdcolumn75">
                                                                            <asp:Label ID="lblLoadingAssblStatus" runat="server" CssClass="fontsize" 
                                                                                Text="Status"></asp:Label>
                                                                        </td>
                                                                        <td class="tdcolumn75">
                                                                            <asp:DropDownList ID="ddlLoadingAssblStatus" runat="server" 
                                                                                AppendDataBoundItems="True" CssClass="ddl75" Enabled="False">
                                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="tdcolumn200">
                                                                            <asp:Label ID="lblLoadingDuty" runat="server" CssClass="fontsize" 
                                                                                Text="Loading Rate(Duty)"></asp:Label>
                                                                        </td>
                                                                        <td class="tdcolumn75">
                                                                            <asp:TextBox ID="txtLoadingDuty" runat="server" CssClass="textbox75" 
                                                                                Enabled="False">0</asp:TextBox>
                                                                        </td>
                                                                        <td class="tdcolumn75">
                                                                            <asp:Label ID="lblLoadingDutyStatus" runat="server" CssClass="fontsize" 
                                                                                Text="Status"></asp:Label>
                                                                        </td>
                                                                        <td class="tdcolumn75">
                                                                            <asp:DropDownList ID="ddlLoadingDutyStatus" runat="server" 
                                                                                AppendDataBoundItems="True" CssClass="ddl100" Enabled="False">
                                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnSaveRelation" runat="server" CssClass="stylebutton" 
                                                                OnClick="btnSaveRelation_Click" Text="Save" />
                                                            <asp:Button ID="btnCancelRelation" runat="server" CssClass="stylebutton" 
                                                                OnClick="btnCancelRelation_Click" 
                                                                OnClientClick="return confirm ('Do you want to Clear the data?')" 
                                                                Text="Cancel" />
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
                                                            <asp:Label ID="lblNoofProduct" runat="server" CssClass="fontsize" 
                                                                Text="No of Product"></asp:Label>
                                                        </td>
                                                        <td colspan="3" style="text-align: right">
                                                            <asp:TextBox ID="txtNoofProd" runat="server" CssClass="textbox100"></asp:TextBox>
                                                        </td>
                                                        <td class="tdcolumn75">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdcolumn100">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdcolumn100">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdcolumn75">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="fontsize" 
                                                                Text="PO. All Product" />
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td colspan="2" style="text-align: right">
                                                            &nbsp;
                                                        </td>
                                                        <td colspan="2" style="text-align: right">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdcolumn100">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdcolumn100">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdcolumn75">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: left">
                                                            <asp:Label ID="lblContractNo0" runat="server" CssClass="fontsize" Text="PO No"></asp:Label>
                                                        </td>
                                                        <td colspan="2" style="text-align: left">
                                                            <asp:TextBox ID="txtPONo" runat="server" CssClass="textbox100"></asp:TextBox>
                                                        </td>
                                                        <td class="tdcolumn75">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdcolumn100">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdcolumn100">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdcolumn75">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblLC0" runat="server" CssClass="fontsize" Text="PO Date"></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txtPODate" runat="server" CssClass="textbox100"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtPODate_CalendarExtender" runat="server" 
                                                                Format="dd/MM/yyyy" TargetControlID="txtPODate"></cc1:CalendarExtender>
                                                        </td>
                                                        <td class="tdcolumn75">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdcolumn100">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdcolumn100">
                                                            &nbsp;
                                                        </td>
                                                        <td class="tdcolumn75">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="displaynon">
                                                            <asp:Label ID="lblContractNo" runat="server" CssClass="fontsize" 
                                                                Text="Contract No/Dt"></asp:Label>
                                                        </td>
                                                        <td class="displaynon" colspan="3">
                                                            <asp:TextBox ID="txtContractNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                                        </td>
                                                        <td class="displaynon">
                                                            <asp:TextBox ID="txtContractDate" runat="server" CssClass="textbox75"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" 
                                                                TargetControlID="txtContractDate"></cc1:CalendarExtender>
                                                        </td>
                                                        <td class="displaynon">
                                                            &nbsp;
                                                        </td>
                                                        <td class="displaynon">
                                                            &nbsp;
                                                        </td>
                                                        <td class="displaynon">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="displaynon">
                                                            <asp:Label ID="lblLC" runat="server" CssClass="fontsize" Text="LC No/Dt"></asp:Label>
                                                        </td>
                                                        <td class="displaynon" colspan="3">
                                                            <asp:TextBox ID="txtLCNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                                        </td>
                                                        <td class="displaynon">
                                                            <asp:TextBox ID="txtLCDate" runat="server" CssClass="textbox75"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                                                                TargetControlID="txtLCDate"></cc1:CalendarExtender>
                                                        </td>
                                                        <td class="displaynon">
                                                            &nbsp;
                                                        </td>
                                                        <td class="displaynon">
                                                            &nbsp;
                                                        </td>
                                                        <td class="displaynon">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="displaynon">
                                                            <asp:Label ID="lblValuation" runat="server" CssClass="fontsize" 
                                                                Text="Valuation Method"></asp:Label>
                                                        </td>
                                                        <td class="displaynon" colspan="3">
                                                            <asp:DropDownList ID="ddlValuation" runat="server" AppendDataBoundItems="True" 
                                                                CssClass="ddl100">
                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="displaynon">
                                                            &nbsp;
                                                        </td>
                                                        <td class="displaynon">
                                                            &nbsp;
                                                        </td>
                                                        <td class="displaynon">
                                                            &nbsp;
                                                        </td>
                                                        <td class="displaynon">
                                                            &nbsp;
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8">
                                                            <asp:Button ID="btnSaveOtherDetails" runat="server" CssClass="stylebutton" 
                                                                OnClick="btnSaveOtherDetails_Click" 
                                                                OnClientClick="javascript:return ChkProduct();" Text="Save" />
                                                            <asp:Button ID="btnCancelOtherDetails" runat="server" CssClass="stylebutton" 
                                                                OnClick="btnCancelOtherDetails_Click" 
                                                                OnClientClick="return confirm ('Do you want to Clear the data?')" 
                                                                Text="Cancel" />
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
                                <td colspan="2">
                                    &nbsp; &nbsp; &nbsp; Job Details
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
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
                                <td class="style7">
                                    Job Date
                                </td>
                                <td>
                                    <asp:Label ID="lblJobDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    Currency</td>
                                <td>
                                    <asp:Label ID="lblCurrency" runat="server" CssClass="arealaber1a"></asp:Label>
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
                                    BE Type
                                </td>
                                <td>
                                    <asp:Label ID="lblBeType" runat="server" CssClass="arealaber1a"></asp:Label>
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
                            <tr>
                                <td class="style7">
                                    BE No
                                </td>
                                <td>
                                    <asp:Label ID="lblBeNo" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    BE Date
                                </td>
                                <td>
                                    <asp:Label ID="lblBeDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    Total Invoice</td>
                                <td>
                                    <asp:TextBox ID="txtTotInv" runat="server" CssClass="textbox100">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" colspan="2">
                                    <asp:Label ID="lblmsg" runat="server" Style="font-weight: 700"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click"
                                        Text="Back To Shipment" CssClass="stylebutton" />
                                </td>
                            </tr>
                        </table>
                    </td>
                   
                </tr>
            </table>
            <br />
            <br />
            <input type="hidden" runat="server" id="hdnEditValue" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
