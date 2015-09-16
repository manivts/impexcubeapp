<%@ Page Title="" Language="C#" MasterPageFile="~/OPReport/OperationReport.Master" AutoEventWireup="true"
    CodeBehind="frmImpJobStatus.aspx.cs" Inherits="ImpexCube.OPReport.frmImpJobStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" >
     // Pannel print
     function printItn() {
         var printContent = document.getElementById("tblReport");
         //var windowUrl = 'about:blank';
         var windowName = 'Print';
         //var WinPrint = window.open(windowUrl, windowName, 'left=300,top=300,right=500,bottom=500,width=1000,height=500');
         var WinPrint = window.open('', '', 'left=300,top=300,right=500,bottom=500,width=1000,height=500');
         WinPrint.document.write('<' + 'html' + '><' + 'body style="background:none !important"' + '>');
         //WinPrint.document.write(printContent.innerHTML);
         WinPrint.document.write(printContent.innerHTML);
         WinPrint.document.write('</body></html>');
         WinPrint.document.close();
         WinPrint.focus();
         WinPrint.print();
         WinPrint.close();
     }
    </script>
    <style type="text/css">
        .style1
        {
        }
        .style5
        {
        }
        .style6
        {
            width: 131px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="lbl" runat="server" CssClass="fontsize" Text="Job No"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlJobNo" runat="server" CssClass="ddl150" AutoPostBack="true"
                    AppendDataBoundItems="true">
                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnGenerate" runat="server" Text="Generate" 
                    CssClass="stylebutton" onclick="btnGenerate_Click" />
            </td>
            <td><input id="btnPrint" type="button" onclick="printItn()" value="Print" 
                class="stylebutton" /></td>
        </tr>
        </table>
        <table ><tr><td id="tblReport">
        <table >
        <tr><td align="center" colspan="4">
                <asp:Label ID="lblCompName" runat="server" CssClass="fontsize" 
                Font-Bold="True" Font-Size="Smaller" ></asp:Label>
            </td></tr>
        <tr><td align="center" colspan="4">Import Job Status As On
            <asp:Label ID="lblDate" runat="server" CssClass="fontsize" ></asp:Label>
            </td></tr>
        <tr><td class="style1" colspan="4" 
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
            <asp:Label ID="lblJobNo0" runat="server" CssClass="fontsize" Text="Printed On" 
                Font-Size="Smaller"></asp:Label>:
            <asp:Label ID="lblDate1" runat="server" CssClass="fontsize" 
                Font-Size="Smaller" ></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label23" runat="server" CssClass="fontsize" Text="Job No" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5" width="300px">
                <asp:Label ID="lblJobNo" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style6">
                <asp:Label ID="Label2" runat="server" CssClass="fontsize" 
                    Text="Doc. Recd. On :" Font-Size="Smaller"></asp:Label>
            </td><td class="style1">
                <asp:Label ID="lblDocRecd" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td></tr>
        <tr><td class="style6" valign="top">
            <asp:Label ID="Label1" runat="server" CssClass="fontsize" Text="Importer" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5" align="left" valign="top">
                <asp:Label ID="lblImporter" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
                <br />
                <asp:Label ID="lblImporterAddr" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style5" align="left" colspan="2" valign="top">
                &nbsp;</td></tr>
        <tr><td class="style6" valign="top">
            <asp:Label ID="Label3" runat="server" CssClass="fontsize" Text="Consignor" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5" valign="top">
                <asp:Label ID="lblConsignor" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
                <br />
                <asp:Label ID="lblConsignorAddr" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style5" colspan="2" valign="top">
                &nbsp;</td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label4" runat="server" CssClass="fontsize" 
                Text="Port Of Loading" Font-Size="Smaller"></asp:Label>
            </td><td class="style5">
                <asp:Label ID="lblPortOfLoading" runat="server" CssClass="fontsize" Font-Size="Smaller" 
                    ></asp:Label>
            </td><td class="style6">
                <asp:Label ID="Label7" runat="server" CssClass="fontsize" 
                    Text="Port Of Destination :" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblPortOfDestination" runat="server" CssClass="fontsize" Font-Size="Smaller" 
                    ></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label5" runat="server" CssClass="fontsize" Text="B/E Type" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5">
                <asp:Label ID="lblBEType" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style6">
                <asp:Label ID="Label8" runat="server" CssClass="fontsize" 
                    Text="B/E No &amp; Date" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblBENo" runat="server" CssClass="fontsize" Font-Size="Smaller" ></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label13" runat="server" CssClass="fontsize" Text="Duty Amount" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5">
                <asp:Label ID="lblDutyAmount" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style6">
                <asp:Label ID="Label10" runat="server" CssClass="fontsize" 
                    Text="Receipt No/Date" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblRecieptNo" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="Inter. Amount" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5">
                <asp:Label ID="lblInterAmount" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style6">
                <asp:Label ID="Label11" runat="server" CssClass="fontsize" 
                    Text="Receipt No/Date :" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblInterReceiptNo" runat="server" CssClass="fontsize" Font-Size="Smaller" 
                    ></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label15" runat="server" CssClass="fontsize" Text="Bill No/Date" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5">
                <asp:Label ID="lblBillNo" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style6">
                <asp:Label ID="lblJobNo15" runat="server" CssClass="fontsize" 
                    Text="Db Note No/Date" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblDbNoNo" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label16" runat="server" CssClass="fontsize" Text="B/L No/Date" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5">
                <asp:Label ID="lblBLNo" runat="server" CssClass="fontsize" Font-Size="Smaller"></asp:Label>
            </td><td class="style6">
                <asp:Label ID="lblJobNo16" runat="server" CssClass="fontsize" 
                    Text="Add. B/L No/Date" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblAddBL" runat="server" CssClass="fontsize" Font-Size="Smaller"></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label17" runat="server" CssClass="fontsize" Text="Vessel" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5">
                <asp:Label ID="lblVessel" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style6">
                <asp:Label ID="lblJobNo17" runat="server" CssClass="fontsize" 
                    Text="IGM No &amp; Date :" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblIGMNo" runat="server" CssClass="fontsize" Font-Size="Smaller"></asp:Label>
            </td></tr>
        <tr><td class="style6"></td><td class="style5"></td><td class="style6">
            <asp:Label ID="lblJobNo18" runat="server" CssClass="fontsize" 
                Text="Tr. Vessel :" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblTrVessel" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label18" runat="server" CssClass="fontsize" 
                Text="No Of Packages" Font-Size="Smaller"></asp:Label>
            </td><td class="style5">
                <asp:Label ID="lblNoOfPackages" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style6">
                <asp:Label ID="lblJobNo19" runat="server" CssClass="fontsize" 
                    Text="Gross Weight :" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblGrossWt" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label19" runat="server" CssClass="fontsize" 
                Text="Invoice No/Date" Font-Size="Smaller"></asp:Label>
            </td><td class="style5">
                <asp:Label ID="lblInvoiceNo" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style6">
                <asp:Label ID="lblJobNo20" runat="server" CssClass="fontsize" 
                    Text="Invoice Value :" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblInValue" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label20" runat="server" CssClass="fontsize" Text="Freight" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5">
                <asp:Label ID="lblFreight" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style6">
                <asp:Label ID="lblJobNo21" runat="server" CssClass="fontsize" 
                    Text="Insurance :" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblInsurance" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label21" runat="server" CssClass="fontsize" Text="Exchange Rate" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5">
                <asp:Label ID="lblExRate" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style6">
                <asp:Label ID="lblJobNo22" runat="server" CssClass="fontsize" 
                    Text="Assbl. Value :" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblAssblValue" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label22" runat="server" CssClass="fontsize" 
                Text="Invoice Description" Font-Size="Smaller"></asp:Label>
            </td><td class="style5">
                <asp:Label ID="lblInvDesc" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td><td class="style6">
                <asp:Label ID="lblJobNo23" runat="server" CssClass="fontsize" 
                    Text="Marks &amp; Nos. :" Font-Size="Smaller"></asp:Label>
            </td><td>
                <asp:Label ID="lblMarks" runat="server" CssClass="fontsize" 
                    Font-Size="Smaller" ></asp:Label>
            </td></tr>
        <tr><td class="style6"></td><td class="style5"></td><td class="style6"></td><td></td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label24" runat="server" CssClass="fontsize" 
                Text="Job Movement"></asp:Label>
            </td><td class="style5">&nbsp;</td><td class="style6">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td class="style6" 
                style="border-style: solid none solid none; border-top-width: 1px; border-bottom-width: 1px; border-color: #000000">
            <asp:Label ID="Label25" runat="server" CssClass="fontsize" Text="Stage"></asp:Label>
            </td><td class="style5" 
                style="border-style: solid none solid none; border-top-width: 1px; border-bottom-width: 1px; border-color: #000000">
            <asp:Label ID="Label26" runat="server" CssClass="fontsize" Text="Date"></asp:Label>
            </td><td class="style6" 
                style="border-style: solid none solid none; border-top-width: 1px; border-bottom-width: 1px; border-color: #000000">
            <asp:Label ID="Label27" runat="server" CssClass="fontsize" Text="Remark"></asp:Label>
            </td>
            <td style="border-style: solid none solid none; border-top-width: 1px; border-bottom-width: 1px; border-color: #000000">
            <asp:Label ID="Label28" runat="server" CssClass="fontsize" Text="Handled By"></asp:Label>
            </td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label29" runat="server" CssClass="fontsize" 
                Text="Submitted To Customs" Font-Size="Smaller"></asp:Label>
            </td><td class="style5">&nbsp;</td><td class="style6">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label30" runat="server" CssClass="fontsize" 
                Text="BE Filed" Font-Size="Smaller"></asp:Label>
            </td><td class="style5">&nbsp;</td><td class="style6">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label31" runat="server" CssClass="fontsize" 
                Text="Assessed" Font-Size="Smaller"></asp:Label>
            </td><td class="style5">&nbsp;</td><td class="style6">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label32" runat="server" CssClass="fontsize" 
                Text="Duty Paid" Font-Size="Smaller"></asp:Label>
            </td><td class="style5">&nbsp;</td><td class="style6">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label33" runat="server" CssClass="fontsize" Text="DO Collect" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5">&nbsp;</td><td class="style6">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label34" runat="server" CssClass="fontsize" Text="Examined" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5">&nbsp;</td><td class="style6">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label35" runat="server" CssClass="fontsize" Text="Out of charge" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5">&nbsp;</td><td class="style6">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td class="style6">
            <asp:Label ID="Label36" runat="server" CssClass="fontsize" Text="Delivery" 
                Font-Size="Smaller"></asp:Label>
            </td><td class="style5">&nbsp;</td><td class="style6">&nbsp;</td><td>&nbsp;</td></tr>
        </table>
        </td>
        </tr>
        </table>
       
</asp:Content>
