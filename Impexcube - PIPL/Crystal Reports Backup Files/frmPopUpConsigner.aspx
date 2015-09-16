<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPopUpConsigner.aspx.cs" Inherits="ImpexCube.Popup.frmPopUpConsigner" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consigner Details</title>
        <script type="text/javascript">
            function GetProduct(ConsName,ConsAdd,ConCountry,mode) {
            {
          
                if (mode == "cnsr") {
                    window.opener.document.forms[0].ContentPlaceHolder1_txtConsignorName.value = ConsName;
                    window.opener.document.forms[0].ContentPlaceHolder1_txtConsignor.value = ConsAdd;
                    window.opener.document.forms[0].ContentPlaceHolder1_ddlConsignorCountry.value = ConCountry;
                    window.close();
                }
                else if (mode == "seller") {
                    window.opener.document.forms[0].ContentPlaceHolder1_txtSeller.value = ConsName;
                    window.opener.document.forms[0].ContentPlaceHolder1_txtSellerName.value = ConsAdd;
                    window.opener.document.forms[0].ContentPlaceHolder1_ddlSellerCountry.value = ConCountry;
                    window.close();
                }
                else if (mode == "agent") {
                    window.opener.document.forms[0].ContentPlaceHolder1_txtBroker.value = ConsName;
                    window.opener.document.forms[0].ContentPlaceHolder1_txtBrokerName.value = ConsAdd;
                    window.opener.document.forms[0].ContentPlaceHolder1_ddlBrokerCountry.value = ConCountry;
                    window.close();
                }
            }
            function GetImporter(PartyName, IeCodeNo, BranchSno, Address, City, State, ZipCode, CommericalTaxState, CommericalTaxType, CommericalTaxRegnNo, mode)
             {
             alert('Test');
                if (mode == "imp") {
                    window.opener.document.forms[0].ContentPlaceHolder1_txtImporter.value = PartyName;
                    window.opener.document.forms[0].ContentPlaceHolder1_lblIECodeNo.value = IeCodeNo;
                    window.opener.document.forms[0].ContentPlaceHolder1_lblImpBranchNo.value = BranchSno;
  
                    window.opener.document.forms[0].ContentPlaceHolder1_lblAddress.value = Address;

                    window.opener.document.forms[0].ContentPlaceHolder1_lblCity.value = City;
                    window.opener.document.forms[0].ContentPlaceHolder1_lblStateImp.value = State;
                    window.opener.document.forms[0].ContentPlaceHolder1_lblZipCode.value = ZipCode;

                    window.opener.document.forms[0].ContentPlaceHolder1_lblState.value = CommericalTaxState;
                    window.opener.document.forms[0].ContentPlaceHolder1_lblTaxType.value = CommericalTaxType;
                    window.opener.document.forms[0].ContentPlaceHolder1_lblRegnNo.value = CommericalTaxRegnNo;
                    window.close();
                }
                else if (mode == "high") {
                    window.opener.document.getElementById("HighSeaSaleTable").style.display = 'table';
                    window.opener.document.forms[0].ContentPlaceHolder1_txtSellerName.value = PartyName;
                    window.opener.document.forms[0].ContentPlaceHolder1_lblIECodeNoHigh.value = IeCodeNo;
                    window.opener.document.forms[0].ContentPlaceHolder1_lblSellerBranchNo.value = BranchSno;
                    window.opener.document.forms[0].ContentPlaceHolder1_lblAddressHigh.value = Address;
                    window.opener.document.forms[0].ContentPlaceHolder1_lblCityHigh.value = City;
                    window.opener.document.forms[0].ContentPlaceHolder1_lblStateHigh.value = State;
                    window.opener.document.forms[0].ContentPlaceHolder1_lblZipCodeHigh.value = ZipCode;
                    window.close();
                }

            }
            function GetExporter(PartyName, IeCodeNo, BranchSno, Address, State, mode) {
                if (mode == "Exp") {

                    window.opener.document.forms[0].ContentPlaceHolder1_txtExporter.value = PartyName;
                    window.opener.document.forms[0].ContentPlaceHolder1_txtIECode.value = IeCodeNo;
                    window.opener.document.forms[0].ContentPlaceHolder1_txtBranchSNo.value = BranchSno;
                    window.opener.document.forms[0].ContentPlaceHolder1_txtExporterAddress.value = Address;
                    window.opener.document.forms[0].ContentPlaceHolder1_txtStateProvince.value = State;
                    window.close();
                }
                else if (mode == "Notify") {

                    window.opener.document.forms[0].ContentPlaceHolder1_txtNotify.value = PartyName;
                    window.opener.document.forms[0].ContentPlaceHolder1_txtAddressExtn.value = Address;
                    window.close();
                }
                else if (mode == "Product") {

                    window.opener.document.forms[0].ContentPlaceHolder1_txtManufacture.value = PartyName;
                    window.opener.document.forms[0].ContentPlaceHolder1_txtotheriecode.value = IeCodeNo;
                    window.opener.document.forms[0].ContentPlaceHolder1_txtothBranchsno.value = BranchSno;
                    window.opener.document.forms[0].ContentPlaceHolder1_txtotheraddress.value = Address;

                    window.close();
                }


            }
    </script>
     <style type="text/css">
        .stylebutton
 {
    padding:3px;
	margin:0px;
	cursor: pointer;
	text-align: center;
	border:none;
	border:1px solid #73AAE8;
	width: 120px;
	font-size: 8pt;
	color:#241e12;
  }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table width="800">
            <tr>
                <td style="text-align: center; font-weight: 700;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=" Name"></asp:Label>
                    <asp:TextBox ID="txtSearch" runat="server" Width="600px" Height="15px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="stylebutton" 
                        onclick="btnSearch_Click" Text="Search" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Both" 
                        Width="800px">
                        <asp:GridView ID="GridView1"  runat="server" Width="800px" 
                        AutoGenerateSelectButton="true" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged" 
                        style="font-size: 8pt">
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
