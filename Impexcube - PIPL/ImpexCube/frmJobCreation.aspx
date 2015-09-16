<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmJobCreation.aspx.cs" Inherits="ImpexCube.frmJobCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Content/JQuery/JSONJobDetails.js" type="text/javascript"></script>
    <script language="Javascript" type="text/javascript">
        function Validate() {
            debugger          
//            document.getElementById('ErrorMessage').innerHTML = "";
            if (document.getElementById('ContentPlaceHolder1_txtjno').value == "") {
                document.getElementById('ContentPlaceHolder1_txtjno').focus();
                alert("Please Enter The JobNo");
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_txtJobReceivedDate').value == "") {
                alert('Please Enter The JobDate');
                document.getElementById('ContentPlaceHolder1_txtJobReceivedDate').focus();
                return false;
            }
            var ddlfilling = document.getElementById('ContentPlaceHolder1_ddlFilling');
            var fillingtext = ddlfilling.options[ddlfilling.selectedIndex].text;
            if (fillingtext == "~Select~") {
                alert("Please Select Doc Filling");
                return false;
            }
            var ddlmode = document.getElementById('ContentPlaceHolder1_ddlMode');
            var modetext = ddlmode.options[ddlmode.selectedIndex].text;
            if (modetext == "~Select~") {
                alert("Please Select Mode");
                return false;
            }
            var ddlcustom = document.getElementById('ContentPlaceHolder1_ddlCustom');
            var customtext = ddlcustom.options[ddlcustom.selectedIndex].text;
            if (customtext == "~Select~") {
                alert("Please Select Custom House");
                return false;
            }
            var ddldoctype = document.getElementById('ContentPlaceHolder1_ddlDocFillingStatus');
            var doctypetext = ddldoctype.options[ddldoctype.selectedIndex].text;
            if (doctypetext == "~Select~") {
                alert("Please Select Doc Type");
                return false;
            }
            var ddlbetype = document.getElementById('ContentPlaceHolder1_ddlBEType');
            var betypetext = ddlbetype.options[ddlbetype.selectedIndex].text;
            if (betypetext == "~Select~") {
                alert("Please Select BE Type");
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_txtImporter').value == "") {
                document.getElementById('ContentPlaceHolder1_txtImporter').focus();
               alert("Please Enter Importer Name");
                return false;
            }
//            if (document.getElementById('ContentPlaceHolder1_lblIECodeNo').value == "") {
//                document.getElementById('ContentPlaceHolder1_lblIECodeNo').focus();
//                alert("Please Enter IE Code ");
//                return false;
//            }
            if (document.getElementById('ContentPlaceHolder1_lblAddress').value == "") {
                document.getElementById('ContentPlaceHolder1_lblAddress').focus();
                alert("Please Enter Importer Address ");
                return false;
            }
            var totinv = 0;
            var totinvalue = 0.00;
            totinv = document.getElementById('ContentPlaceHolder1_txtTotalNoOfInvoice').value;
            totinvalue = document.getElementById('ContentPlaceHolder1_txtTotalInvoiceValue').value;
            if ((totinv !== "0") || (totinv != 0)) {
                if ((totinvalue == "0") || (totinvalue == 0)) {
                    alert("Please Enter Total Invoice Value ");
                    document.getElementById('ContentPlaceHolder1_txtTotalInvoiceValue').focus();
                    return false;
                }
            }
            if ((totinvalue !== "0") || (totinvalue != 0)) {
                if ((totinv == "0") || (totinv == 0)) {
                    alert( "Please Enter Total No Of Invoice ");
                    document.getElementById('ContentPlaceHolder1_txtTotalNoOfInvoice').focus();
                    return false;
                }
            }
            var retVal = confirmation();
            if (retVal == true) {
                return true;
            }
            else {
                return false;
            }

        }

        function TabAllow(e) {
            if (e.shiftKey || e.keyCode == 9) {
                return true;
            }
            else {
                return false;
            }
        }
                   
        function confirmation() {
            debugger

                var res = confirm("Are you sure to save");
                if (res == false) {
                    return false;
                }
                else {
                    return true;
                }                             

            }
        

        function callImporterDetails() {debugger
            //var ImporterSearch = $("#ContentPlaceHolder1_txtSearchImporter").val();
            var ImporterSearch = $("#ContentPlaceHolder1_txtImporter").val();
            var Importer = $("#ContentPlaceHolder1_txtImporter");
            var ShortName = $("#ContentPlaceHolder1_lblShortName");
            var IECodeNo = $("#ContentPlaceHolder1_lblIECodeNo");
            var BranchSno = $("#ContentPlaceHolder1_lblImpBranchNo");
            var Address = $("#ContentPlaceHolder1_lblAddress");
            var City = $("#ContentPlaceHolder1_lblCity");
            var State = $("#ContentPlaceHolder1_lblStateImp");
            var ZipCode = $("#ContentPlaceHolder1_lblZipCode");
            BindAccountMaster(ImporterSearch, Importer, IECodeNo, BranchSno, Address, City, State, ZipCode, ShortName);
        }
        function callConsignorDetails() {//OnChange="callConsignorDetails();"
            // var ImporterSearch = $("#ContentPlaceHolder1_txtSearchConsignor").val();
            var ImporterSearch = $("#ContentPlaceHolder1_txtConsignor").val();
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
        function callSellerDetails() {//OnChange="callSellerDetails();"
            //var ImporterSearch = $("#ContentPlaceHolder1_txtSearchSeller").val();
            var ImporterSearch = $("#ContentPlaceHolder1_txtSellerName").val();
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
        function exbonding() {

        }
    </script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="Content/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Content/JQuery/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script src="Content/Scripts/ProductDetails.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=gvJobNo.ClientID %>').Scrollable();
        }
)
    </script>
    <style type="text/css">
        body
        {
            font-family: "Trebuchet MS" , "Helvetica" , "Arial" , "Verdana" , "sans-serif";
            font-size: 62.5%;
        }
        input[type=], textarea
        {
            border: 1px solid #ccc;
        }
        input[type=text]:focus, textarea:focus
        {
            background-color: #F0F0F0;
            border: 1px solid #ccc;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#ContentPlaceHolder1_txtJobReceivedDate").datepicker({
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
    <asp:UpdatePanel ID="dd" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                <Services>
                    <asp:ServiceReference Path="AutoComplete.asmx" />
                </Services>
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server">
                        <table id="Page2" width="100%">
                            <tr>
                                <td colspan="5" style="text-align: center">
                                    <strong>General Details</strong>
                                </td>
                                <td rowspan="18" style="border-color: #C0C0C0; border-left-style: ridge;">
                                </td>
                                <td rowspan="18">
                                </td>
                                <td align="center" colspan="4">
                                    <strong>Importer Details</strong>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="2">
                                    <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="Job No/Date"></asp:Label><font
                                        color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtjno" runat="server" CssClass="textbox150" TabIndex="1"></asp:TextBox>
                                    <br />
                                </td>
                                <td valign="top">
                                    <asp:Button ID="btnSearchJob" runat="server" Height="18px" OnClick="btnSearchJob_Click"
                                        Text="::" />
                                </td>
                                <td rowspan="2">
                                    <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="Total No of Invoice"
                                        Width="109px"></asp:Label>
                                </td>
                                <td rowspan="2">
                                    <asp:TextBox ID="txtTotalNoOfInvoice" runat="server" CssClass="textbox150" MaxLength="3"
                                        OnKeyPress="javascript:return isFloat(event);" TabIndex="7">0</asp:TextBox>
                                </td>
                                <td rowspan="2">
                                    <asp:Label ID="Label198" runat="server" CssClass="fontsize" Text="Importer"></asp:Label><font
                                        color="red">*</font>
                                </td>
                                <td colspan="3" rowspan="2">
                                    <asp:TextBox ID="txtImporter" runat="server" CssClass="textboxW400" OnChange="callImporterDetails();"
                                        TabIndex="23" ToolTip="Importer Name" ></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetSearchCustomer"
                                        ServicePath="~/AutoComplete.asmx" TargetControlID="txtImporter">
                                    </cc1:AutoCompleteExtender>
                                    <asp:TextBox ID="lblShortName" runat="server" CssClass="textbox75" TabIndex="24"
                                        ToolTip="Short Name"></asp:TextBox>
                                    <asp:TextBox ID="lblImpBranchNo" runat="server" CssClass="textbox75" TabIndex="25"
                                        ToolTip="Branch SNO"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtJobReceivedDate" runat="server" CssClass="textbox150" TabIndex="2"></asp:TextBox>
                                    <%--  <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtJobReceivedDate">
                                    </cc1:CalendarExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" CssClass="fontsize" Text="Filling"></asp:Label><font
                                        color="red">*</font>
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlFilling" runat="server" CssClass="ddl150" TabIndex="5">
                                        <asp:ListItem>Manual</asp:ListItem>
                                        <asp:ListItem>Online</asp:ListItem>
                                        <asp:ListItem>E-mail</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label15" runat="server" CssClass="fontsize" Text="Total Invoice Value"
                                        Width="111px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalInvoiceValue" runat="server" CssClass="textbox150" OnKeyPress="javascript:return isFloat(event);"
                                        TabIndex="9">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label205" runat="server" CssClass="fontsize" Text="IE Code"></asp:Label><font
                                        color="red">*</font>
                                </td>
                                <td>
                                    <asp:TextBox ID="lblIECodeNo" runat="server" CssClass="textbox150" TabIndex="26"></asp:TextBox>
                                    <asp:TextBox ID="txtImporterRefNo" runat="server" CssClass="textbox150" TabIndex="27"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label210" runat="server" CssClass="fontsize" Text="City"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="lblCity" runat="server" CssClass="textbox150" TabIndex="29"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label197" runat="server" CssClass="fontsize" Text="Mode"></asp:Label><font
                                        color="red">*</font>
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlMode" runat="server" CssClass="ddl150" TabIndex="4" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlMode_SelectedIndexChanged">
                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                        <asp:ListItem>Sea</asp:ListItem>
                                        <asp:ListItem>Air</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblCurrency0" runat="server" CssClass="fontsize" Text="Currency"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCurrency" runat="server" AppendDataBoundItems="True" CssClass="ddl150"
                                        TabIndex="8">
                                        <asp:ListItem Selected="True" Value="~Select~">~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td rowspan="2">
                                    <asp:Label ID="LabelAddress" runat="server" CssClass="fontsize" Text="Address"></asp:Label><font
                                        color="red">*</font>
                                </td>
                                <td rowspan="2">
                                    <asp:TextBox ID="lblAddress" runat="server" Font-Size="8pt" Height="55px" TabIndex="28"
                                        TextMode="MultiLine" Width="256px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label211" runat="server" CssClass="fontsize" Text="State"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="lblStateImp" runat="server" CssClass="textbox150" TabIndex="30"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" CssClass="fontsize" Text="Custom House"></asp:Label><font
                                        color="red">*</font>
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlCustom" runat="server" AppendDataBoundItems="True" CssClass="ddl150"
                                        TabIndex="6">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label11" runat="server" CssClass="fontsize" Text="Doc Filling Type"></asp:Label><font
                                        color="red">*</font>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDocFillingStatus" runat="server" CssClass="ddl150" TabIndex="3">
                                        <asp:ListItem Value="N">Normal</asp:ListItem>
                                        <asp:ListItem Value="Y">Prior</asp:ListItem>
                                        <asp:ListItem Value="A">Advance</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label215" runat="server" CssClass="fontsize" Text="Zip Code"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="lblZipCode" runat="server" CssClass="textbox150" onkeypress="javascript:return TabAllow(event);"
                                        TabIndex="45"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label195" runat="server" CssClass="fontsize" Text="BE No"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtBENo" runat="server" CssClass="textbox150" TabIndex="10"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label196" runat="server" CssClass="fontsize" Text="BE Type"></asp:Label><font
                                        color="red">*</font>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBEType" runat="server" CssClass="ddl150" TabIndex="11" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlBEType_SelectedIndexChanged">
                                        <asp:ListItem Value="H">Home</asp:ListItem>
                                        <asp:ListItem Value="W">Bond</asp:ListItem>
                                        <asp:ListItem Value="X">Ex-Bond</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="4" style="text-align: center">
                                    <strong>
                                        <asp:CheckBox ID="chkCon" runat="server" AutoPostBack="True" OnCheckedChanged="chkCon_CheckedChanged" />
                                        Consignor Details</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label194" runat="server" CssClass="fontsize" Text="BE Date"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtBEDate" runat="server" CssClass="textbox150" Height="16px" onkeypress="Javascript:return TabAllow(event);"
                                        TabIndex="12"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtBEDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtBEDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Label ID="Label212" runat="server" CssClass="fontsize" Text="B/E Heading" Width="70px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBEHeading" runat="server" CssClass="textbox150" Height="16px"
                                        TabIndex="13"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label26" runat="server" CssClass="fontsize" Text="Consignor"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtConsignor" runat="server" CssClass="textboxW400" OnChange="callConsignorDetails();"
                                        TabIndex="32" Enabled="False"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="txtSearchConsignor_AutoCompleteExtender" runat="server"
                                        CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                        CompletionListItemCssClass="listItem" EnableCaching="true" MinimumPrefixLength="0"
                                        ServiceMethod="GetSearchConsignor" ServicePath="~/AutoComplete.asmx" TargetControlID="txtConsignor">
                                    </cc1:AutoCompleteExtender>
                                    <asp:TextBox ID="txtCnrShortName" runat="server" CssClass="textbox100" TabIndex="33"
                                        ToolTip="Short Name" Width="50px" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="fontsize">
                                    <font color="red">*</font> <b>Indicates Mandatory Fields</b>
                                </td>
                                <td>
                                    <asp:Label ID="Label214" runat="server" CssClass="fontsize" Text=" Imp. Type" Width="70px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlImpType" runat="server" CssClass="ddl150" TabIndex="11">
                                        <asp:ListItem Value="P">Private</asp:ListItem>
                                        <asp:ListItem Value="O">Others</asp:ListItem>
                                        <asp:ListItem Value="G">Goverment Departments(both Center And State)</asp:ListItem>
                                        <asp:ListItem Value="U">Goverment Undertakings(both Center And State)</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td rowspan="2">
                                    <asp:Label ID="Label28" runat="server" CssClass="fontsize" Text="Address"></asp:Label>
                                </td>
                                <td rowspan="2">
                                    <asp:TextBox ID="txtAddress" runat="server" Height="55px" TabIndex="34" TextMode="MultiLine"
                                        Width="256px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label27" runat="server" CssClass="fontsize" Text="City"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textbox150" TabIndex="35"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    <asp:CheckBox ID="ChkKachha" runat="server" AutoPostBack="True" CssClass="fontsize"
                                        onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkKachha','ContentPlaceHolder1_lblKachha');"
                                        TabIndex="14" Text="Kachha BE" Width="85px" />
                                </td>
                                <td class="style1" colspan="2">
                                    <asp:TextBox ID="lblKachha" runat="server" CssClass="textbox150" Enabled="False"
                                        TabIndex="15"></asp:TextBox>
                                </td>
                                <td class="style1">
                                    <asp:CheckBox ID="ChkFirstChk" runat="server" AutoPostBack="True" CssClass="fontsize"
                                        onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkFirstChk','ContentPlaceHolder1_lblFirstChk');"
                                        TabIndex="18" Text="First Check" Width="90px" />
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="lblFirstChk" runat="server" CssClass="textbox150" Enabled="False"
                                        TabIndex="19"></asp:TextBox>
                                </td>
                                <td class="style1">
                                    <asp:Label ID="Label29" runat="server" CssClass="fontsize" Text="Country"></asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textbox150" TabIndex="36" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="ChkUnderSec46" runat="server" AutoPostBack="True" CssClass="fontsize"
                                        onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkUnderSec46','ContentPlaceHolder1_lblunderSec46');"
                                        TabIndex="16" Text="Under Sec 46" Width="100px" />
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="lblunderSec46" runat="server" CssClass="textbox150" Enabled="False"
                                        TabIndex="17"></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox ID="ChkUnderSec48" runat="server" AutoPostBack="True" CssClass="fontsize"
                                        onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkUnderSec48','ContentPlaceHolder1_lblUnderSec48');"
                                        TabIndex="20" Text="Under Sec 48" Width="100px" />
                                </td>
                                <td>
                                    <asp:TextBox ID="lblUnderSec48" runat="server" CssClass="textbox150" Enabled="False"
                                        TabIndex="21"></asp:TextBox>
                                </td>
                                <td align="center" colspan="4">
                                    <asp:CheckBox ID="chkSVB" runat="server" CssClass="fontsize" Text="(SVB) Are Buyer and Seller Related ?"
                                        TextAlign="Left" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="ChkGreenChannel" runat="server" AutoPostBack="True" CssClass="fontsize"
                                        Height="22px" onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkGreenChannel','ContentPlaceHolder1_lblGreenChannel');"
                                        TabIndex="18" Text="GreenChannel" Width="102px" />
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="lblGreenChannel" runat="server" CssClass="textbox150" Enabled="False"
                                        TabIndex="21"></asp:TextBox>
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox ID="ChkClearanceBond" runat="server" AutoPostBack="True" CssClass="fontsize"
                                        Height="16px" OnCheckedChanged="ChkClearanceBond_CheckedChanged" Text="Clearance Against Bond"
                                        Width="238px" />
                                </td>
                                <td colspan="4" style="text-align: center">
                                    <asp:CheckBox ID="ChkHSS" runat="server" AutoPostBack="True" OnCheckedChanged="ChkHSS_CheckedChanged" />
                                    <strong>High Sea Sale Details</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" rowspan="6" style="text-align: left" valign="top">
                                    <cc1:TabContainer ID="tcBondCertification" runat="server" ActiveTabIndex="0" Visible="false"
                                        Width="100%">
                                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                                            <HeaderTemplate>
                                                Bond
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <div>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblBondType" runat="server" CssClass="fontsize" Text="Bond Type"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBondNo" runat="server" CssClass="fontsize" Text="Bond Number"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblRegister" runat="server" CssClass="fontsize" Text="Registered At"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblSave" runat="server" CssClass="fontsize"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="ddlBondType" runat="server" CssClass="ddl150">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBondNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlRegisteredAt" runat="server" AppendDataBoundItems="True"
                                                                    CssClass="ddl150" TabIndex="6">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnBondSave" runat="server" CssClass="stylebutton" OnClick="btnBondSave_Click"
                                                                    Text="Save" Width="50px" />
                                                                <asp:Button ID="btnBondUpdate" runat="server" CssClass="stylebutton" OnClick="btnBondUpdate_Click"
                                                                    Text="Update" Width="60px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:GridView ID="gvBondType" runat="server" AutoGenerateColumns="False" CssClass="table-wrapper"
                                                                    OnSelectedIndexChanged="gvBondType_SelectedIndexChanged" Width="500px">
                                                                    <AlternatingRowStyle BackColor="#E7E7FF" />
                                                                    <Columns>
                                                                        <asp:CommandField ShowSelectButton="True" />
                                                                        <asp:BoundField DataField="TransId" HeaderText="ID">
                                                                            <HeaderStyle CssClass="hiddencol" />
                                                                            <ItemStyle CssClass="hiddencol" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="JobNo" HeaderText="Job No" HtmlEncode="False" />
                                                                        <asp:BoundField DataField="BondType" HeaderText="Bond Type" HtmlEncode="False" />
                                                                        <asp:BoundField DataField="BondNumber" HeaderText="Bond Number" HtmlEncode="False" />
                                                                        <asp:BoundField DataField="RegisteredDate" HeaderText="Registered Date" HtmlEncode="False" />
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" />
                                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                    <RowStyle CssClass="table-header light" />
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                                            <HeaderTemplate>
                                                Certificate<br />
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <div>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblCertificationNo" runat="server" CssClass="fontsize" Text="Cer. No"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblCertiType" runat="server" CssClass="fontsize" Text="Cer. Type"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblCertiDate" runat="server" CssClass="fontsize" Text="Date"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblCommisionRate" runat="server" CssClass="fontsize" Text="Com. Rate"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblDivision" runat="server" CssClass="fontsize" Text="Division"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblRange" runat="server" CssClass="fontsize" Text="Range"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtCertiNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlCerType" runat="server" CssClass="ddl150">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCertiDate" runat="server" CssClass="textbox75"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtCertiDate">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCommisionRate" runat="server" CssClass="textbox50"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDivision" runat="server" CssClass="textbox50"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRange" runat="server" CssClass="textbox50"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnCertificationSave" runat="server" CssClass="stylebutton" OnClick="btnCertificationSave_Click"
                                                                    Text="Save" Width="50px" />
                                                                <asp:Button ID="btnCertificationUpdate" runat="server" CssClass="stylebutton" OnClick="btnCertificationUpdate_Click"
                                                                    Text="Update" Width="60px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="7">
                                                                <asp:GridView ID="gvCertification" runat="server" AutoGenerateColumns="False" CssClass="table-wrapper"
                                                                    OnSelectedIndexChanged="gvCertification_SelectedIndexChanged" Width="450px">
                                                                    <AlternatingRowStyle BackColor="#E7E7FF" />
                                                                    <Columns>
                                                                        <asp:CommandField ShowSelectButton="True" />
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Content/Images/delete.gif"
                                                                                    OnClientClick="return confirm('Do U Want Delete?');" OnClick="btnDelete_Click"
                                                                                    Width="20px" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="TransId" HeaderText="ID">
                                                                            <HeaderStyle CssClass="hiddencol" />
                                                                            <ItemStyle CssClass="hiddencol" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="JobNo" HeaderText="Job No" HtmlEncode="False" />
                                                                        <asp:BoundField DataField="CertificateNo" HeaderText="Cer. No" HtmlEncode="False" />
                                                                        <asp:BoundField DataField="CertificateType" HeaderText="Type" />
                                                                        <asp:BoundField DataField="CertificateDate" HeaderText="Date" HtmlEncode="False" />
                                                                        <asp:BoundField DataField="Commissionerrate" HeaderText="Com. Rate" HtmlEncode="False" />
                                                                        <asp:BoundField DataField="Division" HeaderText="Division" HtmlEncode="False" />
                                                                        <asp:BoundField DataField="Range" HeaderText="Range" />
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" />
                                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                    <RowStyle CssClass="table-header light" />
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                                            <HeaderTemplate>
                                                Bonding Details<br />
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style2">
                                                            <asp:Label ID="Label216" runat="server" Text="Bonding Period" CssClass="fontsize"></asp:Label>
                                                        </td>
                                                        <td class="style2" colspan="2">
                                                            <asp:TextBox ID="txtbondingfrom" CssClass="textbox100" runat="server"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="From"
                                                                TargetControlID="txtbondingfrom" Enabled="True">
                                                            </cc1:TextBoxWatermarkExtender>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txtbondingfrom">
                                                            </cc1:CalendarExtender>
                                                            &nbsp;
                                                            <asp:TextBox ID="txtbondingto" runat="server" CssClass="textbox100"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="To"
                                                                TargetControlID="txtbondingto" Enabled="True">
                                                            </cc1:TextBoxWatermarkExtender>
                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txtbondingto">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            <asp:Label ID="Label218" runat="server" Text="Bonding Job No" CssClass="fontsize"></asp:Label>
                                                        </td>
                                                        <td class="style2">
                                                            <asp:TextBox ID="txtbondingjobno" runat="server" CssClass="textbox100" OnTextChanged="txtbondingjobno_TextChanged"
                                                                Width="150px" AutoPostBack="true"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" MinimumPrefixLength="1"
                                                                ServiceMethod="GetInBondJobNo" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                CompletionListItemCssClass="listItem" ServicePath="~/AutoComplete.asmx" TargetControlID="txtbondingjobno"
                                                                DelimiterCharacters="" Enabled="True">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" TargetControlID="txtbondingjobno"
                                                                WatermarkText="JobNo " runat="server" Enabled="True">
                                                            </cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            <asp:Label ID="Label219" runat="server" Text="In-Bond BE No:" CssClass="fontsize"></asp:Label>
                                                        </td>
                                                        <td class="style2">
                                                            <asp:TextBox ID="txtbondblno" runat="server" CssClass="textbox100"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label222" runat="server" Text="Date" CssClass="fontsize"></asp:Label>
                                                            <asp:TextBox ID="txtdatebillno" runat="server" CssClass="textbox100"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txtdatebillno">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            <asp:Label ID="Label220" runat="server" Text="Bond No" CssClass="fontsize"></asp:Label>
                                                        </td>
                                                        <td class="style2">
                                                            <asp:TextBox ID="txtExBondNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label223" runat="server" Text="Date" CssClass="fontsize"></asp:Label>
                                                            <asp:TextBox ID="txtbondnodate" runat="server" CssClass="textbox100"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txtbondnodate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            <asp:Label ID="Label228" runat="server" CssClass="fontsize" Text="Bond Expiry Date"></asp:Label>
                                                        </td>
                                                        <td class="style2">
                                                            <asp:TextBox ID="txtexpirydate" runat="server"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txtexpirydate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            <asp:Label ID="Label229" runat="server" CssClass="fontsize" Text="Warehouse"></asp:Label>
                                                        </td>
                                                        <td class="style2">
                                                            <asp:TextBox ID="txtwarehouse" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            <asp:Label ID="Label230" runat="server" CssClass="fontsize" Text="Code"></asp:Label>
                                                        </td>
                                                        <td class="style2">
                                                            <asp:TextBox ID="txtcode" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnEXBondSave" runat="server" CssClass="stylebutton" Text="Save"
                                                                Width="80px" OnClick="btnEXBondSave_Click" />
                                                            <asp:Button ID="btnExBondUpdate" runat="server" CssClass="stylebutton" Text="Update"
                                                                Width="80px" OnClick="btnExBondUpdate_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2" colspan="3">
                                                            <asp:GridView ID="gvEXBond" runat="server" AutoGenerateColumns="False" CssClass="table-wrapper"
                                                                OnSelectedIndexChanged="gvEXBond_SelectedIndexChanged" Width="500px">
                                                                <AlternatingRowStyle BackColor="#E7E7FF" />
                                                                <Columns>
                                                                    <asp:CommandField ShowSelectButton="True" />
                                                                    <asp:BoundField DataField="TransId" HeaderText="ID">
                                                                        <HeaderStyle CssClass="hiddencol" />
                                                                        <ItemStyle CssClass="hiddencol" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="JobNo" HeaderText="Job No" HtmlEncode="False" />
                                                                    <asp:BoundField DataField="ExBondFDate" HeaderText="From" HtmlEncode="False" />
                                                                    <asp:BoundField DataField="ExBondTDate" HeaderText="To" HtmlEncode="False" />
                                                                    <asp:BoundField DataField="EXBondBLNO" HeaderText="In-Bond BE No" HtmlEncode="False" />
                                                                </Columns>
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                                                <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" />
                                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                <RowStyle CssClass="table-header light" />
                                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                                </td>
                                <td>
                                    <asp:Label ID="Label30" runat="server" CssClass="fontsize" Text="Seller Name"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtSellerName" runat="server" CssClass="textboxW400" OnChange="callSellerDetails();"
                                        TabIndex="38" Enabled="False"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="txtSearchSeller_AutoCompleteExtender" runat="server"
                                        CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                        CompletionListItemCssClass="listItem" EnableCaching="true" MinimumPrefixLength="0"
                                        ServiceMethod="GetSearchCustomer" ServicePath="~/AutoComplete.asmx" TargetControlID="txtSellerName">
                                    </cc1:AutoCompleteExtender>
                                    <asp:TextBox ID="txtSelerShortName" runat="server" CssClass="textbox75" TabIndex="39"
                                        ToolTip="Short Name" Enabled="False"></asp:TextBox>
                                    <asp:TextBox ID="lblSellerBranchNo" runat="server" CssClass="textbox75" TabIndex="41"
                                        ToolTip="Branch SL No" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label31" runat="server" CssClass="fontsize" Text="IE Code"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="lblIECodeNoHigh" runat="server" CssClass="textbox150" TabIndex="40"
                                        Enabled="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label34" runat="server" CssClass="fontsize" Text="City"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="lblCityHigh" runat="server" CssClass="textbox150" TabIndex="44"
                                        Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="2">
                                    <asp:Label ID="Label213" runat="server" CssClass="fontsize" Text="Address"></asp:Label>
                                </td>
                                <td rowspan="2">
                                    <asp:TextBox ID="lblAddressHigh" runat="server" CssClass="textbox150" Height="55px"
                                        TabIndex="42" TextMode="MultiLine" Width="256px" Enabled="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label43" runat="server" CssClass="fontsize" Text="State"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="lblStateHigh" runat="server" CssClass="textbox150" TabIndex="43"
                                        Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label35" runat="server" CssClass="fontsize" Text="Zip Code"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="lblZipCodeHigh" runat="server" CssClass="textbox150" onkeypress="javascript:return TabAllow(event);"
                                        TabIndex="45" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: right">
                                    <asp:Button ID="btnSavetomast" runat="server" CssClass="stylebutton" TabIndex="46"
                                        Text="Save to Master" Visible="False" Width="102px" />
                                    <asp:Button ID="btnsave" runat="server" CssClass="stylebutton" OnClick="btnsave_Click"
                                        TabIndex="49" Text="Save" Width="80px" OnClientClick="return Validate();"/>
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="stylebutton" OnClick="btnUpdate_Click"
                                        TabIndex="50" Text="Update" Visible="False" Width="70px" />
                                    <asp:Button ID="btnNewjob" runat="server" CssClass="stylebutton" OnClick="btnNewjob_Click"
                                        Text="New Job" Width="75px" />
                                    <asp:Button ID="btnExit" runat="server" CssClass="stylebutton" OnClick="btnExit_Click"
                                        Text="Exit" />
                                    <asp:Button ID="btnShipment" runat="server" CssClass="stylebutton" OnClick="btnShipment_Click"
                                        TabIndex="51" Text="Go to Shipment" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server">
                        <table id="Page1" style="width: 100%; margin-top: 0px;">
                            <tr>
                                <td colspan="3" align="center">
                                    Search
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="textbox200" TabIndex="45"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="stylebutton" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnClose" runat="server" CssClass="stylebutton" OnClick="btnClose_Click"
                                        TabIndex="48" Text="New Job" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    <div style="height: 350px; width: 1200px; overflow: auto; text-align: center" tabindex="-1">
                                        <asp:GridView ID="gvJobNo" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                                            CssClass="table-wrapper" TabIndex="52" Width="1000px" OnSelectedIndexChanged="gvJobNo_SelectedIndexChanged">
                                            <Columns>
                                                <%-- ID, JobNo, JobReceivedDate, Mode, Custom, BEType,BENo,BEDate, DocFillingStatus, Filling,TotalNoofInvoice,TotalInvoiceValue--%>
                                                <%-- <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="dtyuty" runat="server" OnClick="dtyuty_Click" 
                                                OnClientClick="javascript:return gridselect(this);" TabIndex="53" Text="select">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                                <asp:BoundField DataField="ID" HeaderStyle-CssClass="hiddencol" HeaderText="ID" ItemStyle-CssClass="hiddencol">
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemStyle CssClass="hiddencol" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="JobNo" HeaderText="Job No" HtmlEncode="False" />
                                                <asp:BoundField DataField="JobReceivedDate" HeaderText="Job Date" HtmlEncode="False" />
                                                <asp:BoundField DataField="Mode" HeaderText="Mode" HtmlEncode="False" />
                                                <asp:BoundField DataField="Custom" HeaderText="Custom" HtmlEncode="False" />
                                                <asp:BoundField DataField="BEType" HeaderText="BE Type" HtmlEncode="False" />
                                                <asp:BoundField DataField="BENo" HeaderText="BE No" HtmlEncode="False" />
                                                <asp:BoundField DataField="BEDate" HeaderText="BE Date" HtmlEncode="False" />                                                
                                                <asp:BoundField DataField="ImporterName" HeaderText="ImporterName" HtmlEncode="False" />
                                            </Columns>
                                            <RowStyle CssClass="table-header light" />
                                            <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" />
                                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                            <AlternatingRowStyle BackColor="#E7E7FF" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
