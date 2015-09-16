<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmJobCreation.aspx.cs" Inherits="ImpexCube.frmJobCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Content/JQuery/JSONJobDetails.js" type="text/javascript"></script>
    <script language="Javascript" type="text/javascript">
        $(document).ready(function () {
            var chkcnsr = document.getElementById('ContentPlaceHolder1_chkSingleConsignor');
            var chkhighsea = document.getElementById('ContentPlaceHolder1_chkHighSeaSale');
            hidetableCnr(chkcnsr);
            hidetable(chkhighsea);
        });
        function gridselect(s) {
            try {
                var row = s.parentNode.parentNode;
                var ind = row.rowIndex;
                var table = document.getElementById('ContentPlaceHolder1_gvJobNo');
                var jno = table.rows[ind].cells[2].childNodes[0].data;
                var jrdate = table.rows[ind].cells[3].childNodes[0].data;
                var mode = table.rows[ind].cells[4].childNodes[0].data;
                var custom = table.rows[ind].cells[5].childNodes[0].data;
                var betype = table.rows[ind].cells[6].childNodes[0].data;
                var beno = table.rows[ind].cells[7].childNodes[0].data;
                var bedate = table.rows[ind].cells[8].childNodes[0].data;
                var status = table.rows[ind].cells[9].childNodes[0].data;
                var filling = table.rows[ind].cells[10].childNodes[0].data;
                var noofinvoice = table.rows[ind].cells[11].childNodes[0].data;
                var invoicevalue = table.rows[ind].cells[12].childNodes[0].data;
                var curr = table.rows[ind].cells[13].childNodes[0].data;

                //document.getElementById("ContentPlaceHolder1_HiddenField1").value = jno;
                //                document.getElementById('ContentPlaceHolder1_btnUpdate').disable = true;
                //                document.getElementById('ContentPlaceHolder1_btnsave').disable = false;
                document.getElementById('ContentPlaceHolder1_txtjno').value = jno;
                document.getElementById('ContentPlaceHolder1_HiddenField1').value = jno;
                document.getElementById('ContentPlaceHolder1_txtJobReceivedDate').value = jrdate;
                document.getElementById('ContentPlaceHolder1_ddlMode').value = mode;
                document.getElementById('ContentPlaceHolder1_ddlBEType').value = betype;
                document.getElementById('ContentPlaceHolder1_txtBENo').value = beno;
                document.getElementById('ContentPlaceHolder1_txtBEDate').value = bedate;
                document.getElementById('ContentPlaceHolder1_ddlDocFillingStatus').value = status;
                document.getElementById('ContentPlaceHolder1_ddlCustom').value = custom;
                document.getElementById('ContentPlaceHolder1_ddlFilling').value = filling;
                document.getElementById('ContentPlaceHolder1_txtTotalNoOfInvoice').value = noofinvoice;
                document.getElementById('ContentPlaceHolder1_txtTotalInvoiceValue').value = invoicevalue;
                document.getElementById('ContentPlaceHolder1_ddlCurrency').value = curr;

            }
            catch (err) {
                alert(err.Message);

            }
        }

        function hidetable(obj) {
            if (obj.checked == true) {
                document.getElementById("HighSeaSaleTable").style.display = 'table';
                return true;
            }
            else if (obj.checked == false) {
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
            BindAccountMaster(ImporterSearch, Importer, IECodeNo, BranchSno, Address, City, State, ZipCode, ShortName);
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
        .style1
        {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="dd" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                <Services>
                    <asp:ServiceReference Path="AutoComplete.asmx" />
                </Services>
            </asp:ScriptManager>
            <table width="86%">
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="Job No" Width="43px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtjno" runat="server" CssClass="textbox150"></asp:TextBox>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" CssClass="fontsize" Text="Job Received Date"
                            Width="107px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtJobReceivedDate" runat="server" CssClass="textbox150" onkeypress="Javascript:return txtboxdisable();"
                            TabIndex="1"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtJobReceivedDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td rowspan="12" style="border-color: #C0C0C0; border-left-style: ridge;">
                    </td>
                    <td>
                        <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="Total No of Invoice"
                            Width="109px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotalNoOfInvoice" runat="server" CssClass="textbox150" OnKeyPress="javascript:return isFloat(event);"
                            TabIndex="6">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label196" runat="server" CssClass="fontsize" Text="BE Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBEType" runat="server" CssClass="ddl150" TabIndex="7">
                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                            <asp:ListItem>Home</asp:ListItem>
                            <asp:ListItem>Bond</asp:ListItem>
                            <asp:ListItem>De-Bond</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" CssClass="fontsize" Text="Doc Filling Type"
                            Width="113px"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDocFillingStatus" runat="server" CssClass="ddl150" TabIndex="2">
                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                            <asp:ListItem>Normal</asp:ListItem>
                            <asp:ListItem>Prior</asp:ListItem>
                            <asp:ListItem>Advance</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label197" runat="server" CssClass="fontsize" Text="Mode"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMode" runat="server" CssClass="ddl150" TabIndex="3">
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
                    <td>
                        <asp:Label ID="Label195" runat="server" CssClass="fontsize" Text="BE No"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBENo" runat="server" CssClass="textbox150" TabIndex="9"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" CssClass="fontsize" Text="Filling"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFilling" runat="server" CssClass="ddl150" TabIndex="4">
                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                            <asp:ListItem>Online</asp:ListItem>
                            <asp:ListItem>Manual</asp:ListItem>
                            <asp:ListItem>E-mail</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" CssClass="fontsize" Text="Custom House"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustom" runat="server" AppendDataBoundItems="True" CssClass="ddl150"
                            TabIndex="5">
                            <asp:ListItem Selected="True">~Select~</asp:ListItem>
                            <asp:ListItem>Chennai Sea</asp:ListItem>
                            <asp:ListItem>Chennai Air</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label15" runat="server" CssClass="fontsize" Text="Total Invoice Value"
                            Width="111px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotalInvoiceValue" runat="server" CssClass="textbox150" OnKeyPress="javascript:return isFloat(event);"
                            TabIndex="10">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label194" runat="server" CssClass="fontsize" Text="BE Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBEDate" runat="server" CssClass="textbox150" Height="16px" onkeypress="Javascript:return txtboxdisable();"
                            TabIndex="11"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtBEDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txtBEDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label50" runat="server" CssClass="fontsize" Text="Search Importer"
                            Width="104px"></asp:Label>
                    </td>
                    <td colspan="3" class="style1">
                        <asp:TextBox ID="txtSearchImporter" runat="server" CssClass="textboxW400" OnChange="callImporterDetails();"
                            TabIndex="13" Width="422px" ontextchanged="txtSearchImporter_TextChanged"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetSearchAccountName"
                            ServicePath="~/AutoComplete.asmx" TargetControlID="txtSearchImporter">
                        </cc1:AutoCompleteExtender>
                    </td>
                    <td class="style1">
                        <asp:CheckBox ID="ChkKachha" runat="server" AutoPostBack="True" CssClass="fontsize"
                            onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkKachha','ContentPlaceHolder1_lblKachha');"
                            TabIndex="22" Text="Kachha BE" Width="85px" />
                    </td>
                    <td class="style1">
                        <asp:TextBox ID="lblKachha" runat="server" CssClass="textbox150" Enabled="False"></asp:TextBox>
                    </td>
                    <td class="style1">
                        <asp:Label ID="Label212" runat="server" CssClass="fontsize" Text="B/E Heading" 
                            Width="70px"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:TextBox ID="txtBEHeading" runat="server" CssClass="textbox150" 
                            Height="16px" TabIndex="12"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label198" runat="server" CssClass="fontsize" Text="Importer"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtImporter" runat="server" CssClass="textboxW400" TabIndex="14"
                            ToolTip="Importer Name" Width="301px"></asp:TextBox>
                        <asp:TextBox ID="lblShortName" runat="server" CssClass="textbox75" TabIndex="15"
                            ToolTip="Short Name" Width="50px"></asp:TextBox>
                        <asp:TextBox ID="lblImpBranchNo" runat="server" CssClass="textbox50" TabIndex="16"
                            ToolTip="Branch SNO"></asp:TextBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="ChkUnderSec46" runat="server" AutoPostBack="True" CssClass="fontsize"
                            onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkUnderSec46','ContentPlaceHolder1_lblunderSec46');"
                            TabIndex="23" Text="Under Sec 46" Width="100px" />
                    </td>
                    <td>
                        <asp:TextBox ID="lblunderSec46" runat="server" CssClass="textbox150" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label205" runat="server" CssClass="fontsize" Text="IE Code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="lblIECodeNo" runat="server" CssClass="textbox150" TabIndex="17"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label206" runat="server" CssClass="fontsize" Text="Imp Ref No" Width="70px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtImporterRefNo" runat="server" CssClass="textbox150" TabIndex="18"></asp:TextBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="ChkFirstChk" runat="server" AutoPostBack="True" CssClass="fontsize"
                            onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkFirstChk','ContentPlaceHolder1_lblFirstChk');"
                            TabIndex="24" Text="First Check" Width="90px" />
                    </td>
                    <td>
                        <asp:TextBox ID="lblFirstChk" runat="server" CssClass="textbox150" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td rowspan="2">
                        <asp:Label ID="LabelAddress" runat="server" CssClass="fontsize" Text="Address"></asp:Label>
                    </td>
                    <td rowspan="2">
                        <asp:TextBox ID="lblAddress" runat="server" Font-Size="8pt" TabIndex="19" TextMode="MultiLine"
                            Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label210" runat="server" CssClass="fontsize" Text="City"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="lblCity" runat="server" CssClass="textbox150" TabIndex="20"></asp:TextBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="ChkUnderSec48" runat="server" AutoPostBack="True" CssClass="fontsize"
                            onClick="javascript:return txtdisable('ContentPlaceHolder1_ChkUnderSec48','ContentPlaceHolder1_lblUnderSec48');"
                            TabIndex="25" Text="Under Sec 48" Width="100px" />
                    </td>
                    <td>
                        <asp:TextBox ID="lblUnderSec48" runat="server" CssClass="textbox150" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label211" runat="server" CssClass="fontsize" Text="State"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="lblStateImp" runat="server" CssClass="textbox150" TabIndex="21"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblappjobno" runat="server"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:CheckBox ID="chkSingleConsignor" runat="server" OnClick="javascript:return hidetableCnr(this);"
                            Style="font-weight: 700" TabIndex="26" Text=" Single Consignor" />
                    </td>
                    <td colspan="4" align="center">
                        <asp:CheckBox ID="chkHighSeaSale" runat="server" OnClick="javascript:return hidetable(this);"
                            Style="font-weight: 700" TabIndex="27" Text="High Sea Sale   " />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table ID="SingleConsignor" >
                            <tr>
                                <td>
                                    <asp:Label ID="Label51" runat="server" CssClass="fontsize" Text="Search Consignor"
                                        Width="110px"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtSearchConsignor" runat="server" CssClass="textboxW400" OnChange="callConsignorDetails();"
                                        TabIndex="28" ToolTip="Search Consignor" Width="422px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="txtSearchConsignor_AutoCompleteExtender" runat="server"
                                        CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                        CompletionListItemCssClass="listItem" EnableCaching="true" MinimumPrefixLength="0"
                                        ServiceMethod="GetSearchAccountName" ServicePath="~/AutoComplete.asmx" TargetControlID="txtSearchConsignor">
                                    </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label26" runat="server" CssClass="fontsize" Text="Consignor"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtConsignor" runat="server" CssClass="textboxW400" 
                                        Width="362px" TabIndex="29"></asp:TextBox>
                                    <asp:TextBox ID="txtCnrShortName" runat="server" CssClass="textbox100" 
                                        ToolTip="Short Name" Width="50px" TabIndex="30"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="2" >
                                    <asp:Label ID="Label28" runat="server" CssClass="fontsize" Text="Address"></asp:Label>
                                </td>
                                <td rowspan="2" style="width: 163px;">
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="150px" 
                                        TabIndex="31"></asp:TextBox>
                                </td>
                                <td style="width: 112px;">
                                    <asp:Label ID="Label27" runat="server" CssClass="fontsize" Text="City"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textbox150" TabIndex="32"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label29" runat="server" CssClass="fontsize" Text="Country"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textbox150" TabIndex="33"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="4">
                        <table ID="HighSeaSaleTable" >
                            <tr>
                                <td style="width: 113px;">
                                    <asp:Label ID="Label52" runat="server" CssClass="fontsize" Text="Search Seller" Width="80px"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtSearchSeller" runat="server" CssClass="textboxW400" OnChange="callSellerDetails();"
                                        TabIndex="34" ToolTip="Search Seller" Width="377px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="txtSearchSeller_AutoCompleteExtender" runat="server"
                                        CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                        CompletionListItemCssClass="listItem" EnableCaching="true" MinimumPrefixLength="0"
                                        ServiceMethod="GetSearchAccountName" ServicePath="~/AutoComplete.asmx" TargetControlID="txtSearchSeller">
                                    </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label30" runat="server" CssClass="fontsize" Text="Seller Name"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtSellerName" runat="server" CssClass="textboxW400" 
                                        Width="316px" TabIndex="35"></asp:TextBox>
                                    <asp:TextBox ID="txtSelerShortName" runat="server" CssClass="textbox100" 
                                        ToolTip="Short Name" Width="50px" TabIndex="36"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label31" runat="server" CssClass="fontsize" Text="IE Code"></asp:Label>
                                </td>
                                <td style="width: 162px;">
                                    <asp:TextBox ID="lblIECodeNoHigh" runat="server" CssClass="textbox150" TabIndex="37"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label32" runat="server" CssClass="fontsize" Text="Branch SNo"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="lblSellerBranchNo" runat="server" CssClass="textbox150" TabIndex="38"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label213" runat="server" CssClass="fontsize" Text="Address"></asp:Label>
                                </td>
                                <td>
                                   <asp:TextBox ID="lblAddressHigh" runat="server" CssClass="textbox150" 
                                        TabIndex="39"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label43" runat="server" CssClass="fontsize" Text="State"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="lblStateHigh" runat="server" CssClass="textbox150" TabIndex="40"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label34" runat="server" CssClass="fontsize" Text="City"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="lblCityHigh" runat="server" CssClass="textbox150" onkeypress="javascript:return txtboxdisable();"
                                        TabIndex="41"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label35" runat="server" CssClass="fontsize" Text="Zip Code"></asp:Label>
                                </td>
                                <td colspan="1">
                                    <asp:TextBox ID="lblZipCodeHigh" runat="server" CssClass="textbox150" onkeypress="javascript:return txtboxdisable();"
                                        TabIndex="42"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="9">
                        <asp:Button ID="btnSavetomast" runat="server" CssClass="stylebutton" OnClick="btnSavetomast_Click"
                            TabIndex="43" Text="Save to Master" Visible="False" />
                        <asp:Button ID="btnJobCreation" runat="server" CssClass="stylebutton" OnClick="btnJobCreation_Click"
                            TabIndex="44" Text="Back to Job Creation" Visible="False" />
                        <asp:Button ID="btnClose" runat="server" CssClass="stylebutton" OnClick="btnClose_Click"
                            TabIndex="45" Text="New" Width="70px" />
                        <asp:Button ID="btnsave" runat="server" CssClass="stylebutton" OnClick="btnsave_Click"
                            TabIndex="46" Text="Save" Width="80px" />
                        <asp:Button ID="btnUpdate" runat="server" CssClass="stylebutton" OnClick="btnUpdate_Click"
                            TabIndex="47" Text="Update" Visible="False" Width="70px" />
                        <asp:Button ID="btnShipment" runat="server" CssClass="stylebutton" OnClick="btnShipment_Click"
                            TabIndex="48" Text="Go to Shipment" />
                    </td>
                </tr>
              <%--  <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>--%>
                <tr>
                    <td align="center" colspan="9" >
                        <div style="height: 150px; width: 1200px; overflow: auto; text-align: center" >
                            <asp:GridView ID="gvJobNo" runat="server" AutoGenerateColumns="False" CssClass="table-wrapper"
                                Width="1000px" TabIndex="49">
                                <Columns>
                                    <%-- ID, JobNo, JobReceivedDate, Mode, Custom, BEType,BENo,BEDate, DocFillingStatus, Filling,TotalNoofInvoice,TotalInvoiceValue--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="dtyuty" runat="server" OnClick="dtyuty_Click" OnClientClick="javascript:return gridselect(this);"
                                                Text="select" TabIndex="50">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID" HeaderStyle-CssClass="hiddencol" HeaderText="ID" ItemStyle-CssClass="hiddencol">
                                        <HeaderStyle CssClass="hiddencol" />
                                        <ItemStyle CssClass="hiddencol" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JobNo" HeaderText="Job No"  />
                                    <asp:BoundField DataField="JobReceivedDate" HeaderText="Job Date" />
                                    <asp:BoundField DataField="Mode" HeaderText="Mode" />
                                    <asp:BoundField DataField="Custom" HeaderText="Custom" />
                                    <asp:BoundField DataField="BEType" HeaderText="BE Type" />
                                    <asp:BoundField DataField="BENo" HeaderText="BE No" />
                                    <asp:BoundField DataField="BEDate" HeaderText="BE Date" />
                                    <asp:BoundField DataField="DocFillingStatus" HeaderText="Doc Filling" />
                                    <asp:BoundField DataField="Filling" HeaderText="Filling" />
                                    <asp:BoundField DataField="TotalNoofInvoice" HeaderText="No of Inv." />
                                    <asp:BoundField DataField="TotalInvoiceValue" HeaderText="Inv. Value" />
                                    <asp:BoundField DataField="Currency" HeaderText="Currency" />
                                </Columns>
                                <RowStyle CssClass="table-header light"  />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
