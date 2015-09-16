<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmProductMaster.aspx.cs" Inherits="ImpexCube.frmProductMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table>
        <tr>
        <td>
            <asp:RadioButtonList ID="rbPro" runat="server" AutoPostBack="True" 
                onselectedindexchanged="rbPro_SelectedIndexChanged" 
                RepeatDirection="Horizontal">
                <asp:ListItem>New Product</asp:ListItem>
                <asp:ListItem>Search Product</asp:ListItem>
            </asp:RadioButtonList>
        </td>
        </tr>
        </table>
            <div id="divProHide" runat="server">
                <asp:Panel ID="pnlProductDeatils" runat="server" Width="794px">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" CssClass="fontsize" Text="Product Code"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProCode" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" CssClass="fontsize" Text="RITC NO"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRITC" runat="server" CssClass="textbox150"></asp:TextBox>
                                <asp:Label ID="Label2" runat="server" CssClass="fontsize" Text="Product Family"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProFamily" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" CssClass="fontsize" Text="Product Name"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtProName" runat="server" CssClass="textbox400"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table width="800">
                       <tr>
                            <td bgcolor="#0066FF" colspan="6" style="text-align: center">
                                <asp:Label ID="Label6" runat="server" Text="Generic Description"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="120">
                                <asp:Label ID="lblsadnotn0" runat="server" CssClass="fontsize" 
                                    Text="Generic Desc"></asp:Label>
                            </td>
                            <td width="180">
                                <asp:TextBox ID="txtgenericdesc" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td width="90">
                                <asp:Label ID="lblsadnotn6" runat="server" CssClass="fontsize" 
                                    Text="Accessories"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtaccessories" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblsadnotn1" runat="server" CssClass="fontsize" 
                                    Text="Manufacturer"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtmanufacturer" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="120">
                                <asp:Label ID="lblsadnotn2" runat="server" CssClass="fontsize" Text="Brand"></asp:Label>
                            </td>
                            <td width="180">
                                <asp:TextBox ID="brand" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td width="90">
                                <asp:Label ID="lblsadnotn3" runat="server" CssClass="fontsize" Text="Model"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtmodel" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblsadnotn4" runat="server" CssClass="fontsize" Text="End Use"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="endcase" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="120">
                                <asp:Label ID="lblsadnotn5" runat="server" CssClass="fontsize" Text="Country of Origin"
                                    Width="100px"></asp:Label>
                            </td>
                            <td width="180">
                                <asp:DropDownList ID="ddlcountryorigin" runat="server" AppendDataBoundItems="True"
                                    CssClass="ddl150">
                                    <asp:ListItem>~Select~</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="90">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table width="800">
                        <tr>
                            <td bgcolor="#0066FF" colspan="4" style="text-align: center">
                                <asp:Label ID="Label39" runat="server" Text="Duty Calculation"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="120">
                                <asp:Label ID="Label11" runat="server" CssClass="fontsize" Text="CTH NO"></asp:Label>
                            </td>
                            <td width="75px" colspan="1">
                                <%-- <cc1:AutoCompleteExtender ID="txtCTH_AutoCompleteExtender" runat="server" 
                                            TargetControlID="txtCTH">
                                        </cc1:AutoCompleteExtender>--%>
                                <asp:TextBox ID="txtCTH" runat="server" AutoPostBack="True" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td width="75px" style="width: 275px">
                                <asp:Label ID="lblcetno" runat="server" CssClass="fontsize" Text="CET No"></asp:Label>
                                <asp:TextBox ID="txtCETNo" runat="server" AutoPostBack="True" CssClass="textbox75"></asp:TextBox>
                                <asp:Label ID="Label20" runat="server" CssClass="fontsize" Text="Rate Type"></asp:Label>
                                <asp:TextBox ID="txtRateType" runat="server" CssClass="textbox75">Standard</asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td width="120">
                                <asp:Label ID="Label12" runat="server" CssClass="fontsize" Text="Basic Duty/Notn-"></asp:Label>
                            </td>
                            <td width="75px" colspan="1">
                                <asp:TextBox ID="txtBasicDutyNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtBasicDutySno" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtBasicDutyRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                %<asp:TextBox ID="txtBasicDutyFlag" runat="server" CssClass="textbox75">Plus</asp:TextBox>
                                Rs<asp:TextBox ID="txtBasicDutyAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                /<asp:TextBox ID="txtBasicDutyUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbladdlduty" runat="server" CssClass="fontsize" Text="Addl Duty(Exsise Duty)-"></asp:Label>
                            </td>
                            <td colspan="1">
                                <asp:TextBox ID="txtAddlExNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtAddlExSlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtAddlExRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                %<asp:TextBox ID="txtAddlExFlag" runat="server" CssClass="textbox75">Plus</asp:TextBox>
                                Rs<asp:TextBox ID="txtAddlExAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                /<asp:TextBox ID="txtAddlExUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblcetno0" runat="server" CssClass="fontsize" Text="MRP Duty"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:CheckBox ID="chkMRPDuty" runat="server" CssClass="fontsize" Text="Sr No in List" />
                                <asp:TextBox ID="txtMRPSNo" runat="server" CssClass="textbox50"></asp:TextBox>
                                <asp:Label ID="lblcetno2" runat="server" CssClass="fontsize" Text="MRP Duty"></asp:Label>
                                <asp:TextBox ID="txtMRP" runat="server" CssClass="textbox50"></asp:TextBox>
                                /
                                <asp:TextBox ID="txtMRPUnit" runat="server" CssClass="textbox50"></asp:TextBox>
                                <asp:Label ID="lblcetno3" runat="server" CssClass="fontsize" Text="MRP Rate"></asp:Label>
                                <asp:TextBox ID="txtMRPAbatement" runat="server" CssClass="textbox50"></asp:TextBox>
                                %
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblcvd" runat="server" CssClass="fontsize" Text="CVD(Sub section-5)-"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExCVDNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtExCVDSlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtEXCVDRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                <asp:Label ID="lblpolicy" runat="server" CssClass="fontsize" Text="Policy Para"></asp:Label>
                                <asp:TextBox ID="txtpolicy" runat="server" CssClass="textbox150"></asp:TextBox>
                                <asp:Label ID="lblpolicyyear" runat="server" CssClass="fontsize" Text="Policy Year"></asp:Label>
                                <asp:TextBox ID="txtpyear" runat="server" CssClass="textbox50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="120">
                                <asp:Label ID="Label13" runat="server" CssClass="fontsize" Text="Education Cess-"></asp:Label>
                            </td>
                            <td width="75px" colspan="1">
                                <asp:TextBox ID="txtEducessNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtEduCessSNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtEducessRate" runat="server" CssClass="textbox50">0</asp:TextBox>
                                %
                                <asp:Label ID="Label22" runat="server" CssClass="fontsize" Text="Sec. &amp; Higher Edu.Cess"></asp:Label>
                                <asp:TextBox ID="txtSHECessNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtSHECessSNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtSHECessRate" runat="server" CssClass="textbox50">0</asp:TextBox>
                                %
                            </td>
                        </tr>
                    </table>
                    <table width="800">
                        <tr>
                            <td bgcolor="#0066FF" colspan="4" style="text-align: center">
                                <asp:Label ID="Label4" runat="server" Text="Other Duty"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle" width="150">
                                <asp:Label ID="educational" runat="server" CssClass="fontsize" Text="Educational Cess-"></asp:Label>
                            </td>
                            <td width="75px">
                                <asp:TextBox ID="txtExEduCessRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                            </td>
                            <td>
                                %<asp:Label ID="lblcetno1" runat="server" CssClass="fontsize" Text="Sec &amp; Higher E CESS"></asp:Label>
                                <asp:TextBox ID="txtExSHECessRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                %
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle" width="150">
                                <asp:Label ID="Label29" runat="server" CssClass="fontsize" Text="Addl Duty of Excice(GSI)"
                                    Width="150px"></asp:Label>
                            </td>
                            <td width="75px">
                                <asp:TextBox ID="txtExGSIAddlDutyNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExGSIAddlDutySlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtExGSIAddlDutyRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                <asp:TextBox ID="txtExGSIAddlDutyFlag" runat="server" CssClass="textbox75">Plus</asp:TextBox>
                                Rs<asp:TextBox ID="txtExGSIAddlDutyAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                /<asp:TextBox ID="txtExGSIAddlDutyUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle" width="150">
                                <asp:Label ID="lblsplexcise" runat="server" CssClass="fontsize" Text="Spl.Excise Duty(sched-II)"></asp:Label>
                            </td>
                            <td width="75px">
                                <asp:TextBox ID="txtExSPLExDutyNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExSPLExDutySlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtExSPLExDutyRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                <asp:TextBox ID="txtExSPLExDutyFlag" runat="server" CssClass="textbox75">Plus</asp:TextBox>
                                Rs<asp:TextBox ID="txtExSPLExDutyAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                /<asp:TextBox ID="txtExSPLExDutyUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle" width="150">
                                <asp:Label ID="lbladdlexcise" runat="server" CssClass="fontsize" Text="Addl Excise Duty(TTA)"></asp:Label>
                            </td>
                            <td width="75px">
                                <asp:TextBox ID="txtExTTAAddlDutyNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExTTAAddlDutySlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtExTTAAddlDutyRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                <asp:TextBox ID="txtExTTAAddlDutyFlag" runat="server" CssClass="textbox75">Plus</asp:TextBox>
                                Rs<asp:TextBox ID="txtExTTAAddlDutyAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                /<asp:TextBox ID="txtExTTAAddlDutyUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle" width="150">
                                <asp:Label ID="lblhealthcess" runat="server" CssClass="fontsize" Text="Health Cess"></asp:Label>
                            </td>
                            <td width="75px">
                                <asp:TextBox ID="txtExHealthCessNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExHealthCessSlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtExHealthCessRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                <asp:TextBox ID="txtExHealthCessFlag" runat="server" CssClass="textbox75">Plus</asp:TextBox>
                                Rs<asp:TextBox ID="txtExHealthCessAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                /<asp:TextBox ID="txtExHealthCessUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle" width="150">
                                <asp:Label ID="lblcessnotn" runat="server" CssClass="fontsize" Text="Cess &amp; Notn"></asp:Label>
                            </td>
                            <td width="75px">
                                <asp:TextBox ID="txtExCessNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExCessSlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtExCessRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                %
                                <asp:TextBox ID="txtExCessFlag" runat="server" CssClass="textbox75">Plus</asp:TextBox>
                                Rs<asp:TextBox ID="txtExCessAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                /<asp:TextBox ID="txtExCessUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle" width="150">
                                <asp:Label ID="lblsadnotn" runat="server" CssClass="fontsize" Text="SAD Notn. &amp; Duty"></asp:Label>
                            </td>
                            <td width="75px">
                                <asp:TextBox ID="txtExSADNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExSADSlno" runat="server" CssClass="textbox75" Height="16px"></asp:TextBox>
                                <asp:TextBox ID="txtExSADRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="120" colspan="1">
                                <asp:Label ID="Label21" runat="server" CssClass="fontsize" Text="Addl Notn"></asp:Label>
                            </td>
                            <td width="75px" colspan="1">
                                <asp:TextBox ID="txtAddlNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddlNotnSno" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="120" colspan="1">
                                <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="NCD"></asp:Label>
                            </td>
                            <td width="75px" colspan="1">
                                <asp:TextBox ID="txtNCDNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNCDSNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtNCDRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                %<asp:TextBox ID="txtNCDFlag" runat="server" CssClass="textbox75">Plus</asp:TextBox>
                                Rs<asp:TextBox ID="txtNCDAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                /<asp:TextBox ID="txtNCDUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtNCDRule" runat="server" CssClass="textbox75">0</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="120" colspan="1">
                                <asp:Label ID="Label15" runat="server" CssClass="fontsize" Text="Surcharge &amp; Notn"></asp:Label>
                            </td>
                            <td width="75px" colspan="1">
                                <asp:TextBox ID="txtSurNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSurSno" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtSurRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                %
                            </td>
                        </tr>
                        <tr>
                            <td width="120" colspan="1">
                                <asp:Label ID="Label16" runat="server" CssClass="fontsize" Text="SAPTA Notn"></asp:Label>
                            </td>
                            <td width="75px" colspan="1">
                                <asp:TextBox ID="txtSAPTNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSAPTSno" runat="server" CssClass="textbox75"></asp:TextBox>
                                <asp:TextBox ID="txtSAPTDesc" runat="server" CssClass="textbox200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="120" colspan="1">
                                <asp:Label ID="Label17" runat="server" CssClass="fontsize" Text="Tarrif Value Notn"></asp:Label>
                            </td>
                            <td width="75px" colspan="1">
                                <asp:TextBox ID="txtTarrifNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label19" runat="server" CssClass="fontsize" Text="SNo"></asp:Label>
                                <asp:TextBox ID="txtTraiffSno" runat="server" CssClass="textbox50"></asp:TextBox>
                                <asp:Label ID="Label23" runat="server" CssClass="fontsize" Text="Tarrif Unit Qty"></asp:Label>
                                <asp:TextBox ID="txtTarriffUnitQty" runat="server" CssClass="textbox75">0</asp:TextBox>
                                <asp:Label ID="Label24" runat="server" CssClass="fontsize" Text="Amount/Unit"></asp:Label>
                                <asp:TextBox ID="txtTraiffUnit" runat="server" CssClass="textbox75">0</asp:TextBox>
                                <asp:TextBox ID="txttraiffRate" runat="server" CssClass="textbox50">0</asp:TextBox>
                                <asp:TextBox ID="txttraiffAmount" runat="server" CssClass="textbox50">0</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center">
                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="masterbutton" OnClick="btnSave_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="masterbutton" />
                            </td>
                            <td>
                                <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="masterbutton" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div id="divProSearch" runat="server">
                <asp:Panel ID="pnlSearch" runat="server">
                    <table style="width: 707px">
                        <tr>
                            <td>
                                <asp:Label ID="Label40" runat="server" CssClass="fontsize" Text="Search Product/Code"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="textbox150" Width="441px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" CssClass="masterbutton" Height="26px" OnClick="btnSearch_Click"
                                    Text="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div class="grid_scroll-GenMaster">
                                    <asp:GridView ID="gvProduct" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="true"
                                        Width="500px" AutoGenerateColumns="False" OnSelectedIndexChanged="gvProduct_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField HeaderText="Id" DataField="TransId" HeaderStyle-HorizontalAlign="left"
                                                ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                                            <asp:BoundField HeaderText="ProductCode" DataField="ProductCode" HeaderStyle-HorizontalAlign="left"
                                                ItemStyle-CssClass="left"></asp:BoundField>
                                            <asp:BoundField HeaderText="ProductDesc" DataField="ProductDesc" HeaderStyle-HorizontalAlign="left"
                                                ItemStyle-CssClass="left"></asp:BoundField>
                                            <asp:BoundField HeaderText="RITCNo" DataField="RITCNo" HeaderStyle-HorizontalAlign="left"
                                                ItemStyle-CssClass="left"></asp:BoundField>
                                            <asp:BoundField HeaderText="CETNo" DataField="CETNo" HeaderStyle-HorizontalAlign="left"
                                                ItemStyle-CssClass="left"></asp:BoundField>
                                            <asp:BoundField HeaderText="CTHNo" DataField="CTHNo" HeaderStyle-HorizontalAlign="left"
                                                ItemStyle-CssClass="left"></asp:BoundField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="White" />
                                        <RowStyle CssClass="table-header light" />
                                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <AlternatingRowStyle BackColor="#E7E7FF" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
