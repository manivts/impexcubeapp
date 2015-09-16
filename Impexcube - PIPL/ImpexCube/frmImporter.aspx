<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmImporter.aspx.cs" Inherits="ImpexCube.frmImporter" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Content/JQuery/JSONJobDetails.js" type="text/javascript"></script>
   
        
    <style type="text/css">
        .style1
        {
            text-align: left;
        }
    </style>
   
        
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <div class="width100">--%>
 <script type="text/javascript">
     function hidetable(obj) {
         if (obj.checked == true) {
             document.getElementById("HighSeaSaleTable").style.display = 'table';
             return true;
         }
         else if(obj.checked == false){
             document.getElementById("HighSeaSaleTable").style.display = 'none';
             return true;
         }
         return true;
     }
     function hidetableCnr(obj) {
         if (obj.checked == true) {
             document.getElementById("SingleConsignor").style.display = 'table';
             return true;
         }
         else if (obj.checked == false) {
             document.getElementById("SingleConsignor").style.display = 'none';
             return true;
         }
         return true;
     }
     //  To Set the Job details 
     function callddlJobNo() {
         var JobNo = $("#ContentPlaceHolder1_ddlJobNo").val();
         var JobReceivedDate = $("#ContentPlaceHolder1_lblJobReceivedDate");
         var Mode = $("#ContentPlaceHolder1_lblMode");
         var Custom = $("#ContentPlaceHolder1_lblCustom");
         var BEType = $("#ContentPlaceHolder1_lblBEType");
         var DocFillingStatus = $("#ContentPlaceHolder1_lblDocFillingStatus");
         var BENo = $("#ContentPlaceHolder1_lblBENo");
         var BEDate = $("#ContentPlaceHolder1_lblBEDate");
         BindJobDetails(JobNo, JobReceivedDate, Mode, Custom, BEType, DocFillingStatus, BENo, BEDate);
     }
     function callImporterDetails() {
         var ImporterSearch = $("#ContentPlaceHolder1_txtSearchImporter").val();
         var Importer = $("#ContentPlaceHolder1_txtImporter");
         var ShortName = $("#ContentPlaceHolder1_lblShortName");
         var IECodeNo = $("#ContentPlaceHolder1_lblIECodeNo");
         var BranchSno = $("#ContentPlaceHolder1_lblImpBranchNo");
         var Address = $("#ContentPlaceHolder1_lblAddress");
         var City = $("#ContentPlaceHolder1_lblCity");
         var State = $("#ContentPlaceHolder1_lblStateImp");
         var ZipCode = $("#ContentPlaceHolder1_lblZipCode");
         BindAccountMaster(ImporterSearch, Importer, IECodeNo, BranchSno, Address, City, State, ZipCode, ShortName)
     }
     function callConsignorDetails() {
         var ImporterSearch = $("#ContentPlaceHolder1_txtSearchConsignor").val();
         var Importer = $("#ContentPlaceHolder1_txtConsignor");
         var ShortName = $("#ContentPlaceHolder1_txtCnrShortName");
         var IECodeNo = $("#ContentPlaceHolder1_lblIECodeNo1");
         var BranchSno = $("#ContentPlaceHolder1_lblImpBranchNo1");
         var Address = $("#ContentPlaceHolder1_txtAddress");
         var City = $("#ContentPlaceHolder1_txtCity");
         var State = $("#ContentPlaceHolder1_txtCountry");
         var ZipCode = $("#ContentPlaceHolder1_lblZipCode1");
         BindAccountMaster(ImporterSearch, Importer, IECodeNo, BranchSno, Address, City, State, ZipCode, ShortName)
     }
     function callSellerDetails() {
         var ImporterSearch = $("#ContentPlaceHolder1_txtSearchSeller").val();
         var Importer = $("#ContentPlaceHolder1_txtSellerName");
         var ShortName = $("#ContentPlaceHolder1_txtSelerShortName");
         var IECodeNo = $("#ContentPlaceHolder1_lblIECodeNoHigh");
         var BranchSno = $("#ContentPlaceHolder1_lblSellerBranchNo");
         var Address = $("#ContentPlaceHolder1_lblAddressHigh");
         var City = $("#ContentPlaceHolder1_lblCityHigh");
         var State = $("#ContentPlaceHolder1_lblStateHigh");
         var ZipCode = $("#ContentPlaceHolder1_lblZipCodeHigh");
         BindAccountMaster(ImporterSearch, Importer, IECodeNo, BranchSno, Address, City, State, ZipCode, ShortName)
     }

</script>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    <Services>
                     <asp:ServiceReference Path="AutoComplete.asmx" /></Services>
                    </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="1000">
                    <tr>
                        <td width="750">
                         <%--   <div class="div70">--%>
                                <table width="750">
                                    <tr>
                                        <td class="center" colspan="4">
                                            <asp:Label ID="Label53" runat="server" style="font-weight: 700" 
                                                Text="Importer Details"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label50" runat="server" 
                                                Text="Search Importer" style="font-weight: 700"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtSearchImporter" runat="server" CssClass="textboxW400" OnChange="callImporterDetails();"></asp:TextBox>
                                             <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                                    CompletionListCssClass="completionList" 
                                                    CompletionListHighlightedItemCssClass="itemHighlighted" 
                                                    CompletionListItemCssClass="listItem" EnableCaching="true" 
                                                    MinimumPrefixLength="0" ServiceMethod="GetSearchAccountName" 
                                                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtSearchImporter"></asp:AutoCompleteExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="Importer"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtImporter" runat="server" CssClass="textboxW400" 
                                                ToolTip="Importer Name" TabIndex="1"></asp:TextBox>
                                            <asp:TextBox ID="lblShortName" runat="server" CssClass="textbox100" 
                                                ToolTip="Short Name"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" CssClass="fontsize" Text="IE Code No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblIECodeNo" runat="server" CssClass="textbox200" ></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" CssClass="fontsize" 
                                                Text="Importer Ref No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtImporterRefNo" runat="server" CssClass="ddl200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2">
                                            <asp:Label ID="Label7" runat="server" CssClass="fontsize" Text="Address"></asp:Label>
                                        </td>
                                        <td rowspan="2">
                                            <asp:TextBox ID="lblAddress" runat="server" Font-Size="8pt" 
                                                TextMode="MultiLine" Width="200px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" CssClass="fontsize" Text="Branch SNo"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblImpBranchNo" runat="server" CssClass="textbox200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" CssClass="fontsize" 
                                                Text="Commerical Regn. No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblRegnNo" runat="server" CssClass="textbox200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" CssClass="fontsize" Text="City"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblCity" runat="server" CssClass="textbox200"></asp:TextBox>
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="Label17" runat="server" CssClass="fontsize" 
                                                Text="Commerical Tax State"></asp:Label>
                                        </td>
                                        <td class="style1">
                                            <asp:TextBox ID="lblState" runat="server" CssClass="textbox200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" CssClass="fontsize" Text="State"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblStateImp" runat="server" CssClass="textbox200"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" CssClass="fontsize" 
                                                Text="Commerical Tax Type"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblTaxType" runat="server" CssClass="textbox200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label42" runat="server" CssClass="fontsize" Text="Zip Code"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblZipCode" runat="server" CssClass="textbox200"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="B/E Heading"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBEHeading" runat="server" CssClass="ddl200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" CssClass="fontsize" 
                                                Text="Country of Origin"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txtCountryOfOrigin" runat="server" CssClass="ddl200">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" CssClass="fontsize" 
                                                Text="Port of Shipment"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txtPortofShipment" runat="server" CssClass="ddl200">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" CssClass="fontsize" 
                                                Text="Country of Shipment"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txtCountryOfShipment" runat="server" AutoPostBack="True" 
                                                CssClass="ddl200" 
                                                onselectedindexchanged="txtCountryOfShipment_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChkUnderSec46" runat="server" CssClass="fontsize" 
                                                Text="Under Sec 46" AutoPostBack="True" 
                                                  onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkUnderSec46','ContentPlaceHolder1_lblunderSec46');"/><%--oncheckedchanged="ChkUnderSec46_CheckedChanged"--%>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblunderSec46" runat="server" CssClass="textbox200" 
                                                 Enabled="False"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ChkKachha" runat="server" CssClass="fontsize" 
                                                Text="Kachha BE" AutoPostBack="True" 
                                                onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkKachha','ContentPlaceHolder1_lblKachha');" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblKachha" runat="server" CssClass="textbox200" 
                                                 Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChkFirstChk" runat="server" CssClass="fontsize" 
                                                Text="First Check" AutoPostBack="True" 
                                                onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkFirstChk','ContentPlaceHolder1_lblFirstChk');"/>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblFirstChk" runat="server" CssClass="textbox200" 
                                                 Enabled="False"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ChkUnderSec48" runat="server" CssClass="fontsize" 
                                                Text="Under Sec 48" AutoPostBack="True" 
                                                onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkUnderSec48','ContentPlaceHolder1_lblUnderSec48');" />
                                        </td>
                                        <td style="margin-left: 40px">
                                            <asp:TextBox ID="lblUnderSec48" runat="server" CssClass="textbox200" 
                                                 Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="center">
                                            <asp:CheckBox ID="chkSingleConsignor" runat="server" Text=" Single Consignor" 
                                                  OnClick="javascript:return hidetableCnr(this);" style="font-weight: 700" />
                                        </td>
                                    </tr>
                                    <tr>
                                    <td colspan="4">
                                    <table ID="SingleConsignor" style="width:100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label51" runat="server" style="font-weight: 700" 
                                                Text="Search Consignor"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtSearchConsignor" runat="server" CssClass="textboxW400" 
                                                OnChange="callConsignorDetails();" ToolTip="Search Consignor"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtSearchConsignor_AutoCompleteExtender" 
                                                runat="server" CompletionListCssClass="completionList" 
                                                CompletionListHighlightedItemCssClass="itemHighlighted" 
                                                CompletionListItemCssClass="listItem" EnableCaching="true" 
                                                MinimumPrefixLength="0" ServiceMethod="GetSearchAccountName" 
                                                ServicePath="~/AutoComplete.asmx" TargetControlID="txtSearchConsignor">
                                            </asp:AutoCompleteExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" CssClass="fontsize" Text="Consignor"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtConsignor" runat="server" CssClass="textboxW400"></asp:TextBox>
                                            <asp:TextBox ID="txtCnrShortName" runat="server" CssClass="textbox100" 
                                                ToolTip="Short Name"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" CssClass="fontsize" Text="City"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="ddl200"></asp:TextBox>
                                        </td>
                                        <td rowspan="2">
                                            <asp:Label ID="Label21" runat="server" CssClass="fontsize" Text="Address"></asp:Label>
                                        </td>
                                        <td rowspan="2">
                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label23" runat="server" CssClass="fontsize" Text="Country"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="ddl200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td class="center" colspan="4">
                                            <asp:CheckBox ID="chkHighSeaSale" runat="server" 
                                                OnClick="javascript:return hidetable(this);" Text="High Sea Sale   " 
                                                style="font-weight: 700" />
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                                    <table ID="HighSeaSaleTable" style="width:100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label52" runat="server" Text="Search Seller" 
                                                                    style="font-weight: 700"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtSearchSeller" runat="server" CssClass="textboxW400" 
                                                                    OnChange="callSellerDetails();" ToolTip="Search Seller"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="txtSearchSeller_AutoCompleteExtender" 
                                                                    runat="server" CompletionListCssClass="completionList" 
                                                                    CompletionListHighlightedItemCssClass="itemHighlighted" 
                                                                    CompletionListItemCssClass="listItem" EnableCaching="true" 
                                                                    MinimumPrefixLength="0" ServiceMethod="GetSearchAccountName" 
                                                                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtSearchSeller">
                                                                </asp:AutoCompleteExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label24" runat="server" CssClass="fontsize" Text="Seller Name"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtSellerName" runat="server" CssClass="textboxW400"></asp:TextBox>
                                                                <asp:TextBox ID="txtSelerShortName" runat="server" CssClass="textbox100" 
                                                                    ToolTip="Short Name"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label25" runat="server" CssClass="fontsize" Text="IE Code No"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="lblIECodeNoHigh" runat="server" CssClass="textbox200" ></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label26" runat="server" CssClass="fontsize" Text="Branch SNo"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="lblSellerBranchNo" runat="server" CssClass="textbox200"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label27" runat="server" CssClass="fontsize" Text="Address"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="lblAddressHigh" runat="server" CssClass="textbox200" ></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label43" runat="server" CssClass="fontsize" Text="State"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="lblStateHigh" runat="server" CssClass="textbox200"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label28" runat="server" CssClass="fontsize" Text="City"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="lblCityHigh" runat="server" CssClass="textbox200" onkeypress="javascript:return txtboxdisable();"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label29" runat="server" CssClass="fontsize" Text="Zip Code"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="lblZipCodeHigh" runat="server" CssClass="textbox200" onkeypress="javascript:return txtboxdisable();"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <asp:Button ID="btnSavetomast" runat="server" CssClass="stylebutton" 
                                                onclick="btnSavetomast_Click" Text="Save to Master" />
                                            <asp:Button ID="btnJobCreation" runat="server" onclick="btnJobCreation_Click" 
                                                Text="Back to Job Creation" CssClass="stylebutton" />
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" 
                                                Width="70px" CssClass="stylebutton" />
                                            <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                                                Text="Update" Visible="False" CssClass="stylebutton" />
                                            <asp:Button ID="btnShipment" runat="server" onclick="btnShipment_Click" 
                                                Text="Go to Shipment" CssClass="stylebutton" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center" colspan="4">
                                            &nbsp; &nbsp; &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                </table>
                        <%--    </div>--%>
                        </td>
                        <td valign="top" style="text-align: left" width="250">
                            <table width="250" style="height: 300px">
                                <tr>
                                    <td class="center" colspan="2">
                                        <asp:Label ID="Label30" runat="server" Text="History"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label31" runat="server" CssClass="fontsizehistory" Text="Job No"></asp:Label>
                                    </td>
                                    <td>
                                     <%--onchange="javascript:return callddlJobNo();" --%>
                                        <asp:DropDownList ID="ddlJobNo" runat="server" AppendDataBoundItems="True" 
                                            AutoPostBack="True" CssClass="ddl150" 
                                           
                                            onselectedindexchanged="ddlJobNo_SelectedIndexChanged">
                                          
                                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label32" runat="server" CssClass="fontsizehistory" 
                                            Text="Job Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblJobReceivedDate" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label33" runat="server" CssClass="fontsizehistory" Text="Mode"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMode" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label34" runat="server" CssClass="fontsizehistory" Text="Custom"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCustom" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label35" runat="server" CssClass="fontsizehistory" 
                                            Text="BE Type"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBEType" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label36" runat="server" CssClass="fontsizehistory" 
                                            Text="Doc Filling"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDocFillingStatus" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label37" runat="server" CssClass="fontsizehistory" Text="BE No"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBENo" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label38" runat="server" CssClass="fontsizehistory" 
                                            Text="BE Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBEDate" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label39" runat="server" CssClass="fontsizehistory" 
                                            Text="Job Appr."></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblJobApprovedBy" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label40" runat="server" CssClass="fontsizehistory" 
                                            Text="Duty Pay. Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDutyPaymentDate" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label41" runat="server" CssClass="fontsizehistory" 
                                            Text="OOC Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOverseaDate" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
  <%--  </div>--%>
</asp:Content>
