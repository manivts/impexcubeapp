<%@ Page Title="" Language="C#" MasterPageFile="~/OPReport/OperationReport.Master" AutoEventWireup="true" CodeBehind="frmImportLetter.aspx.cs" Inherits="ImpexCube.OPReport.frmImportLetter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
    </style>
    <script type="text/javascript" >
        // Pannel print
        function printItn() {
            var LetterName = document.getElementById("ContentPlaceHolder1_ddlLetter").value;
            if (LetterName == "Duty Call Letter") {
                var printContent = document.getElementById("DutyCallLetter");
            }
            else if (LetterName == "Stamp Letter") {
                var printContent = document.getElementById("StampLetter");
            }
            else if (LetterName == "Import Docket") {
                var printContent = document.getElementById("ImpDocket");
            }
            else if (LetterName == "Small Declaration") {
                var printContent = document.getElementById("SmallDeclaration");
            }
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
<tr>
<td colspan="3">

</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label1" runat="server" Text="Job No"></asp:Label>
    <asp:TextBox ID="txtJobNo" runat="server" CssClass="textbox200"></asp:TextBox>
</td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Letter"></asp:Label>
    <asp:DropDownList ID="ddlLetter" runat="server" CssClass="ddl200">
    <asp:ListItem Text="~Select~"></asp:ListItem>
    <asp:ListItem Text="Duty Call Letter" Value="Duty Call Letter"></asp:ListItem>
    <asp:ListItem Text="Stamp Letter" Value="Stamp Letter"></asp:ListItem>
    <asp:ListItem Text="Import Docket" Value="Import Docket"></asp:ListItem>
    <asp:ListItem Text="Small Declaration" Value="Small Declaration"></asp:ListItem>
    </asp:DropDownList>
</td>
<td>
    <asp:Button ID="btnGenerate" runat="server" Text="Generate" 
        onclick="btnGenerate_Click" CssClass="button2" />
</td>
</tr>
<tr>
<td colspan="3" id="DutyCallLetter">
<asp:Panel ID="PnlDutyCallLetter" runat="server">
<table width="700px">
<tr>
<td align="center" colspan="2">
    <asp:Label ID="Label3" runat="server" Text="PI Logistics(India)Pvt.Ltd."></asp:Label>
</td>
</tr>
<tr>
<td align="center" colspan="2">
    <asp:Label ID="Label4" runat="server" Text="PLOT NO.E-17 SECTOR N>U.4 SAPNA,NAGAR GANDHIDHAM KUTCH,GUJARAT,370201"></asp:Label>
</td>
</tr>
<tr>
<td width="200px">
    <asp:Label ID="Label5" runat="server" Text="Your Ref No :"></asp:Label>
    <asp:Label ID="lblRefNo" runat="server"></asp:Label>
</td>
<td align="center" width="400px">
    <asp:Label ID="Label7" runat="server" Text="Date :"></asp:Label>
    <asp:Label ID="lbljobdate" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label9" runat="server" Text="Our Ref No :"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2"></td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label10" runat="server" Text="Sub :Clearance of 8.000"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2"></td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label11" runat="server" Text="With reference to the above  clearance, please send a demand draft/pay order for Rs."></asp:Label>
    <asp:Label ID="Label12" runat="server" Text="lblRs"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label13" runat="server" Text="favouring COMMISIONER OF CUSTOMS, NHAVA SHEVA A/C being import duty"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label14" runat="server" Text="payable as  per duty calculation as under."></asp:Label>
</td>
</tr>
<tr>
<td colspan="2"></td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label15" runat="server" Text="USD"></asp:Label>
    <asp:Label ID="lblUSD" runat="server"></asp:Label>
    <asp:Label ID="Label17" runat="server" Text="EX.Rate"></asp:Label>
    <asp:Label ID="lblExrate" runat="server"></asp:Label>
    <asp:Label ID="Label19" runat="server" Text="+ Freight Rs."></asp:Label>
    <asp:Label ID="lblFreiRs" runat="server"></asp:Label>
    <asp:Label ID="Label21" runat="server" Text="+ Insurance Rs."></asp:Label>
    <asp:Label ID="lblIasRs" runat="server"></asp:Label>
    <asp:Label ID="Label23" runat="server" Text="Total CIF Value RS."></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label24" runat="server" Text="Total Accessible Value Rs."></asp:Label>
    <asp:Label ID="lblTotAccsValue" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">

</td>
</tr>
<tr>
<td width="200px">
    <asp:Label ID="Label26" runat="server" Text="Duty@Basic7.500"></asp:Label>
</td>
<td width="400px">
    <asp:Label ID="lblBasic" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td width="200px">
    <asp:Label ID="Label28" runat="server" Text="+CVD 12.000 %"></asp:Label>
</td>
<td width="400px">
    <asp:Label ID="lblCVD" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td width="200px">
    <asp:Label ID="Label30" runat="server" Text="+Cess 2% on CVD"></asp:Label>
</td>
<td width="400px">
    <asp:Label ID="lblCessCVD" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td width="200px">
    <asp:Label ID="Label32" runat="server" Text="+H.EdCess0.00%"></asp:Label>
</td>
<td width="400px">
    <asp:Label ID="lblEdCess" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td width="200px" class="style1">
    <asp:Label ID="Label34" runat="server" Text="+Cess2.000%"></asp:Label>
</td>
<td class="style1" width="400px">
    <asp:Label ID="lblCess" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td width="200px">
    <asp:Label ID="Label36" runat="server" Text="H.EdCess1.000%"></asp:Label>
</td>
<td width="400px">
    <asp:Label ID="lblHEdCess1" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td width="200px">
    <asp:Label ID="Label38" runat="server" Text="+4%SAD"></asp:Label>
</td>
<td width="400px">
    <asp:Label ID="lblSAD" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2"></td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label40" runat="server" Text="Total Duty Rs."></asp:Label>
    <asp:Label ID="lblTotDuty" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label42" runat="server" 
        Text="The above Bill of Entry has been Filed in customs vide ."></asp:Label>
    <asp:Label ID="lblBENo" runat="server"></asp:Label>
    <asp:Label ID="Label176" runat="server" Text="Dt. "></asp:Label>
    <asp:Label ID="lblBEDate" runat="server"></asp:Label>
    <asp:Label ID="Label177" runat="server" Text="and  duly assessed"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">

</td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label43" runat="server" Text="Send Original set of documents if not sent earlier."></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">

</td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label44" runat="server" Text="PI Logistics (Indid) Pvt.Ltd."></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">
</td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label45" runat="server" Text="Note: This is a system generated advice and does not require signature"></asp:Label>
</td>
</tr>
</table>
</asp:Panel>
</td>
</tr>
<tr>
<td colspan="3" id="StampLetter">
<asp:Panel ID="PnlStampLetter" runat="server">
<table width="700px">
<tr>
<td colspan="2" align="center">
    <asp:Label ID="Label46" runat="server" Text="MUMBAI PORT TRUST DOCKS"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2" align="center">
    <asp:Label ID="Label47" runat="server" Text="RECEIPT FOR STAMP DUTY ON IMPORTED GOODS"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2" align="center">
    <asp:Label ID="Label48" runat="server" Text="CLEARED FROM OTHER THAN MbPT."></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">

</td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label49" runat="server" Text="Date:"></asp:Label>
    <asp:Label ID="Label50" runat="server" Text="lblDate"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">
<table style="border-color:Black;border-width:1px; width:700px;" border="5">
<tr>
<td colspan="3">
</td>
</tr>
    <tr>
        <td>
            <asp:Label ID="Label79" runat="server" Text="S.No" style="font-weight: 700"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label80" runat="server" Text="Particularss" 
                style="font-weight: 700"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label81" runat="server" Text="Description" 
                style="font-weight: 700"></asp:Label>
        </td>
    </tr>
<tr>
<td>
    <asp:Label ID="Label82" runat="server" Text="1"></asp:Label>
</td>
<td>
    <asp:Label ID="Label83" runat="server" Text="Receipt No. and Date"></asp:Label>
</td>
<td>
    <asp:Label ID="lblNoandDate" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label85" runat="server" Text="2"></asp:Label>
</td>
<td>
    <asp:Label ID="Label86" runat="server" Text="CHA Name and Address"></asp:Label>
</td>
<td>
    <asp:Label ID="lblChaNameAddr" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label88" runat="server" Text="3"></asp:Label>
</td>
<td>
    <asp:Label ID="Label89" runat="server" Text="Importer's/Exporter's Name and Address"></asp:Label>
</td>
<td>
    <asp:Label ID="lblImpExpNameAddr" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label91" runat="server" Text="4"></asp:Label>
</td>
<td>
    <asp:Label ID="Label92" runat="server" Text="Vessel Name"></asp:Label>
</td>
<td>
    <asp:Label ID="lblVesselname" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label94" runat="server" Text="5"></asp:Label>
</td>
<td>
    <asp:Label ID="Label95" runat="server" Text="IG Item No. &Date"></asp:Label>
</td>
<td>
    <asp:Label ID="lblIgmNoDate" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label97" runat="server" Text="6"></asp:Label>
</td>
<td>
    <asp:Label ID="Label98" runat="server" Text="Assessable Value"></asp:Label>
</td>
<td>
    <asp:Label ID="lblAssessableval" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label100" runat="server" Text="7"></asp:Label>
</td>
<td>
    <asp:Label ID="Label101" runat="server" Text="Custom Duty"></asp:Label>
</td>
<td>
    <asp:Label ID="lblCustomDuty" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label103" runat="server" Text="8"></asp:Label>
</td>
<td>
    <asp:Label ID="Label104" runat="server" Text="DEPB Amount Debited"></asp:Label>
</td>
<td>
    <asp:Label ID="lblDebAmnt" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label106" runat="server" Text="9"></asp:Label>
</td>
<td>
    <asp:Label ID="Label107" runat="server" Text="Interest If any delayed payment"></asp:Label>
</td>
<td>
    <asp:Label ID="lblIntdelpayment" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label109" runat="server" Text="10"></asp:Label>
</td>
<td>
    <asp:Label ID="Label110" runat="server" Text="Total Of(6to9)above"></asp:Label>
</td>
<td>
    <asp:Label ID="lblTot6to9" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label112" runat="server" Text="11"></asp:Label>
</td>
<td>
    <asp:Label ID="Label113" runat="server" Text="Manifest Weight(MW)"></asp:Label>
</td>
<td>
    <asp:Label ID="lblManiWt" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label115" runat="server" Text="12"></asp:Label>
</td>
<td>
    <asp:Label ID="Label116" runat="server" Text="Amount of stamp Duty@0.1% on 10"></asp:Label>
</td>
<td>
    <asp:Label ID="lblAmtofstmpDuty" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label118" runat="server" Text="13"></asp:Label>
</td>
<td>
    <asp:Label ID="Label119" runat="server" Text="MBPT Administrative Charges"></asp:Label>
</td>
<td>
    <asp:Label ID="lblMbPTAdminCharge" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label121" runat="server" Text="14"></asp:Label>
</td>
<td>
    <asp:Label ID="Label122" runat="server" Text="Service Taxon @ 12% on MB.P.T.Admin.Charges"></asp:Label>
</td>
<td>
    <asp:Label ID="lblSerTaxMBPT" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label124" runat="server" Text="15"></asp:Label>
</td>
<td>
    <asp:Label ID="Label125" runat="server" Text="Edu.Cess@2%+1% of Service Tax"></asp:Label>
</td>
<td>
    <asp:Label ID="lblEduCess" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label127" runat="server" Text="16"></asp:Label>
</td>
<td>
    <asp:Label ID="Label128" runat="server" Text="Total of(12 to 15)above"></asp:Label>
</td>
<td>
    <asp:Label ID="lblTot12to15" runat="server"></asp:Label>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label130" runat="server" Text="We Solenly affirm here with that the above information is correct to the best of our knowledge."></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label131" runat="server" Text="We Undertake,if any,short recovery is caused due to any reason We are willing to recoup the short"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">
    <asp:Label ID="Label132" runat="server" Text="recovery of stamp duty along with penalty,if any."></asp:Label>
</td>
</tr>
<tr>
<td colspan="2" align="right">
    <asp:Label ID="Label133" runat="server" Text="PROPER OFFICER"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2" align="right">
    <asp:Label ID="Label134" runat="server" Text="MUMBAI PORT TRUST"></asp:Label>
</td>
</tr>
<tr>
    <td colspan="1">
        <asp:Label ID="Label135" runat="server" Text="CHQ.NO"></asp:Label>
        <asp:Label ID="lblCHQNO" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label137" runat="server" Text="Dt."></asp:Label>
        <asp:Label ID="lblDt" runat="server" Text=""></asp:Label>    
</td>
<td align="right">
    <asp:Label ID="Label139" runat="server" Text="AUTHORIZED SIGNATORY"></asp:Label>
</td>
</tr>
<tr>
<td colspan="1">
    <asp:Label ID="Label140" runat="server" Text="Drawn On"></asp:Label>
    <asp:Label ID="lblDrawnOn" runat="server" Text=""></asp:Label>
</td>
<td align="right">
    <asp:Label ID="Label142" runat="server" Text="PI Logistics (India) Pvt.Ltd."></asp:Label>
</td>
</tr>
</table>
</asp:Panel>
</td>
</tr>
<tr>
<td colspan="3" id="ImpDocket">
<asp:Panel ID="PnlImportDocket" runat="server">        
    <table>
    <tr>
    <td align="center" colspan="7">
        <asp:Label ID="Label136" runat="server" Text="PI Logistics (India) Pvt.Ltd." 
            style="font-weight: 700"></asp:Label>
    </td>
    </tr>
    <tr>
    <td align="center" colspan="7">
        <asp:Label ID="Label138" runat="server" Text="Import Docket" 
            style="font-weight: 700; text-decoration: underline"></asp:Label>
    </td>
    </tr>
    <tr>
    <td align="center" class="" colspan="7">
        &nbsp;</td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label141" runat="server" Text="Job No:"></asp:Label>
    </td>
    <td class="" colspan="2">
        <asp:Label ID="lbljobno" runat="server" Text=""></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label143" runat="server" Text="Received Date:"></asp:Label>
    </td>
    <td>
        <asp:Label ID="lblreceiveddate" runat="server" Text=""></asp:Label>
    </td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label144" runat="server" Text="Date:"></asp:Label>
    </td>
    <td colspan="6">
        <asp:Label ID="lbldate" runat="server" Text=""></asp:Label>
    </td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label145" runat="server" Text="Type Of B/E:"></asp:Label>
    </td>
    <td class="" colspan="2">
        <asp:Label ID="lbltypeofBe" runat="server" Text=""></asp:Label>
    </td>
   
    <td class="">
        <asp:Label ID="Label146" runat="server" Text="Air/Sea:"></asp:Label>
    </td>
     <td>
        <asp:Label ID="lblairorsea" runat="server" Text=""></asp:Label>
    </td>
    </tr>
    <tr>
    <td colspan="">
        <asp:Label ID="Label6" runat="server" Text="Importer Name "></asp:Label>
    </td>
        <td colspan="3">
            <asp:Label ID="lblImportername" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label148" runat="server" Text="Address" ></asp:Label>
    </td>
     <td class="" colspan="3">
        <asp:Label ID="lbladdress" runat="server" Text="" ></asp:Label>
    </td>
    </tr>
        <tr>
            <td class="" colspan="7">
                &nbsp;</td>
        </tr>
    <tr>
    <td class="" colspan="7">
        <asp:Label ID="Label149" runat="server" Text="Party Reference No:"></asp:Label>
    </td>
    </tr>
       
        <tr>
            <td class="" colspan="7">
                &nbsp;</td>
        </tr>
       
    <tr>
    <td class="" colspan="7">
        <hr /></td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label150" runat="server" Text="S.S.Fit No:"></asp:Label>
    </td>
    <td class="" colspan="2">
        <asp:Label ID="lblssfitno" runat="server" Text=""></asp:Label>
    </td>
    <td class="">
        <asp:Label ID="Label151" runat="server" Text="Load Port"></asp:Label>
    </td>
    <td>
        <asp:Label ID="lblloadpoprt" runat="server" Text=""></asp:Label>
    </td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label152" runat="server" Text="B/L /AWB No:"></asp:Label>
    </td>
     <td class="" colspan="2">
        <asp:Label ID="lblBLAWBNo" runat="server" Text=""></asp:Label>
    </td>
    
    <td>
        <asp:Label ID="Label153" runat="server" Text="Dated :"></asp:Label>
    </td>
    <td>
        <asp:Label ID="lblDatedBLAWBNo" runat="server" Text=""></asp:Label>
    </td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label154" runat="server" Text="HAWB No :"></asp:Label>
    </td>
    <td class="" colspan="2">
        <asp:Label ID="lblHawbno" runat="server" Text=""></asp:Label>
    </td>    
    <td class="">
        <asp:Label ID="Label155" runat="server" Text="Dated :"></asp:Label>
    </td>
    <td>
        <asp:Label ID="lblhawbdated" runat="server" Text=""></asp:Label>
    </td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label156" runat="server" Text="No Of Packages:"></asp:Label>
    </td>
    <td class="" colspan="2">
        <asp:Label ID="lblnoofpackages" runat="server" Text=""></asp:Label>
    </td>
   
    <td>
        <asp:Label ID="Label157" runat="server" Text="Gross Weight :"></asp:Label>
    </td>
    <td>
        <asp:Label ID="lblgrossweight" runat="server" Text=""></asp:Label>
    </td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label158" runat="server" Text="Invoice:"></asp:Label>
    </td>
    <td class="" colspan="2">
        <asp:Label ID="lblInvNo" runat="server"></asp:Label>
    </td>
    <td>
    
        <asp:Label ID="Label175" runat="server" Text="Dated :"></asp:Label>
    
    </td>
    <td>
    
        <asp:Label ID="lblinvoicedated" runat="server" Text=""></asp:Label>
    
    </td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label159" runat="server" Text="Containing Invoice Value :"></asp:Label>
    </td>
    <td class="" colspan="2">
        <asp:Label ID="lblcontaininginvoicevalue" runat="server" Text=""></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label160" runat="server" Text="Exch Rate:"></asp:Label>
    </td>
    <td>
        <asp:Label ID="lblexchrate" runat="server" Text=""></asp:Label>
    </td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label161" runat="server" Text="CIF Value Rs:"></asp:Label>
    </td>
    <td class="" colspan="2">
        <asp:Label ID="lblcifvalueRs" runat="server" Text=""></asp:Label>
    </td>
    <td>
        <asp:Label ID="Label162" runat="server" Text="Ass.Value"></asp:Label>
    </td>
    <td>
        <asp:Label ID="lblassvalue" runat="server" Text=""></asp:Label>
    </td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label163" runat="server" Text="Duty Rs:"></asp:Label>
    </td>
    <td class="" colspan="2">
        <asp:Label ID="lbldutyRs" runat="server" Text=""></asp:Label>
    </td>
    </tr>
    <tr>
    <td class="">
        <asp:Label ID="Label164" runat="server" Text="Marks & Nos :"></asp:Label>
    </td>
    <td class="" colspan="2">
        <asp:Label ID="Label165" runat="server" Text="AS PER BL"></asp:Label>
    </td>
    </tr>
    <tr>
    <td  colspan="7">
        <hr /></td>
    </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="Label166" runat="server" style="text-decoration: underline; font-weight: 700;" 
                    Text="DESPATCH INSTRUCTION:"></asp:Label>
            </td>
            <td align="center" colspan="4">
                <asp:Label ID="Label167" runat="server" style="text-decoration: underline; font-weight: 700;" 
                    Text="INSURANCE DETAILS:"></asp:Label>
            </td>
        </tr>
    <tr>
    <td class="" colspan="7">
        &nbsp;</td>
    </tr>
        <tr>
            <td class="" colspan="7">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="" colspan="7">
                <hr /></td>
        </tr>
        <tr>
            <td class="" colspan="3">
                <asp:Label ID="Label168" runat="server" style="text-decoration: underline; font-weight: 700;" 
                    Text="SPECIFIC INSTRUCTIONS/APPROVAL"></asp:Label>
            </td>
            <td align="center" class="" colspan="4">
                <asp:Label ID="Label169" runat="server" style="text-decoration: underline; font-weight: 700;" 
                    Text="BILLING DETAILS "></asp:Label>
            </td>
        </tr>
    <tr>
    <td class="style1" colspan="7">
        &nbsp;</td>
    </tr>
    <tr>
    <td align="right" class="style1">
        &nbsp;</td>
     <td align="right" class="style1" colspan="2">
         &nbsp;</td>
     <td class="" align="center">
         <asp:Label ID="Label170" runat="server" Text="Bill No."></asp:Label>
   </td>
   <td class="">
        <asp:Label ID="lblbillno" runat="server" 
            Text=""></asp:Label>
   </td>
   <td class="">
        <asp:Label ID="lbldatee" runat="server" 
            Text="Date"></asp:Label>
   </td>
   <td class="">
        <asp:Label ID="LBLBILLNODATE" runat="server" 
            Text=""></asp:Label>
   </td>
    </tr>
    <tr>
     <td align="right" class="style1">
        &nbsp;</td>
     <td align="right" class="style1" colspan="2">
         &nbsp;</td>
    <td align="center" class="style1">
        <asp:Label ID="Label171" runat="server" Text="Amount Rs."></asp:Label>
    </td>
    </tr>
    <tr>
     <td align="right" class="style1">
        &nbsp;</td>
     <td align="right" class="style1" colspan="2">
         &nbsp;</td>
    <td align="right" class="style1">
        <asp:Label ID="Label172" runat="server" Text="Add.Bill/Debit No:"></asp:Label>
    </td>
    </tr>
    <tr>
     <td align="right" class="style1">
        &nbsp;</td>
     <td align="right" class="style1" colspan="2">
         &nbsp;</td>
     <td align="center" class="style1">
        <asp:Label ID="Label173" runat="server" Text="Amount Rs."></asp:Label>
    </td>
    </tr>
    </table>
    </asp:Panel>
</td>
</tr>
<tr>
<td colspan="3" id="SmallDeclaration">
<asp:Panel ID="PnlSmallDeclaration" runat="server">
<table>
    <tr>
    <td align="center">
        <asp:Label ID="Label51" runat="server" Text="Declaration" 
            style="text-decoration: underline; font-weight: 700"></asp:Label>
    </td>
    </tr>
    <tr>
    <td align="center">
        <asp:Label ID="Label52" runat="server" Text="(To Be Signed By An Importer)"></asp:Label>
    </td>
    </tr>
    <tr>
    <td align="center">
        &nbsp;</td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label53" runat="server" Text="*With C.H.A." 
            style="font-weight: 700"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label54" runat="server" Text="I/We Declare that the Contentes Of Invoice(s) No.96335546 dtd: 11-dec-2013 of M/s MDD MEDICAL SYSTEMS"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label55" runat="server" Text="(INDIA)PVT LTD and of other documents relating to the goods covered by the said invoice(s) and presented"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label56" runat="server" Text="herewith are true and correct with every respect"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        &nbsp;</td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label57" runat="server" Text="* Without C.H.A." 
            style="font-weight: 700"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label58" runat="server" Text="1. I/We declared with the contents of bill of entry for goods imported against bill of Lading No.C03641213YG dtd:"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label59" runat="server" Text="21-Dec-2013 are in  accordance with the Invoice No. 96335546 dtd: 11-Dec-2013 adn other documents presented"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label60" runat="server" Text="herewith.I/We also declared that the contents of the above mentioned invoice(s) and documents are true and correct"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label61" runat="server" Text="in every respect"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label62" runat="server" Text="2. I/We declared that I/We have not received and do not know of any other documents or information showing a"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label63" runat="server" Text="different price,value,(including local payments whether as commission or otherwise)quantity or description of the"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label64" runat="server" Text="said goods and that if at any timehereafter,I/We discover any information showing a different state of facts,I/We"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label65" runat="server" Text="will immediately make the same known to the Commissioner of Customs"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label66" runat="server" Text="3.I/We declared that the goods covered by the Bill of Entry have been imported on anout-right purchase/consignment"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label67" runat="server" Text="account"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label68" runat="server" Text="4. I/We am/are not connected with the suppliers manufactures as:"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label69" runat="server" Text="(a) Agent Distributors/Indender/Branch/Subsidiary/Concessionaire and"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label70" runat="server" Text="(b) Collaborator entiled to use of trade mark,paitant or designee"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label71" runat="server" Text="(c) Otherwise than as ordinary importers or buyers."></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label72" runat="server" Text="5. I/We declared that the method of invoiceing has changed since the date on which my/our book of account and/or"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label73" runat="server" Text="agreement with the suppliers were examined previously by custom house."></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label74" runat="server" Text="N.B." style="font-weight: 700"></asp:Label>   
        &nbsp;<asp:Label ID="Label75" runat="server" Text=":- Strike out wherever is inapplicable."></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        &nbsp;</td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="Label76" runat="server" Text="For"></asp:Label>
    
        &nbsp;<asp:Label ID="Label77" runat="server" 
            Text="MDD MEDICAL SYSTEMS (INDIA) PVT LTD" style="font-weight: 700"></asp:Label>
    </td>
    </tr>
    <tr>
    <td align="right">
        &nbsp;</td>
    </tr>
    <tr>
    <td align="right">
        <asp:Label ID="Label78" runat="server" Text="Authorised Signatory"></asp:Label>
    </td>
    </tr>
    </table>
</asp:Panel>
</td>
</tr>
<tr>
<td colspan="3" align="center">
    <%--<asp:Button ID="btnPrint" runat="server" Text="Print" onclick="printItn()" />--%>
    <input id="btnPrint" type="button" onclick="printItn()" value="Print Ticket" />
</td>
</tr>
</table>
</asp:Content>
