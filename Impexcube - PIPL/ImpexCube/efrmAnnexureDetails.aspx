<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="efrmAnnexureDetails.aspx.cs" Inherits="ImpexCube.efrmAnnexureDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .style1
        {
            width: 166px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table><tr><td valign="top">
<table>

<tr>
<td colspan="6" align="center"  style="color: #008080; font-style: italic; font-weight: bold;
                font-size: large">
    <asp:Label ID="lblAnnexure" runat="server" Text="Annexure C1 Details"></asp:Label>
    &nbsp;</td>

</tr>

<tr>
<td colspan="6">
    &nbsp;</td>

    <td>
    </td>

</tr>

<tr>
<td class="style1">
    <asp:Label ID="lblIECode" runat="server" Text="IE Code of EOU" 
        CssClass="fontsize"></asp:Label>
</td>
<td colspan="5">
    <asp:TextBox ID="txtIECode" runat="server" CssClass="textbox150"></asp:TextBox>
    <asp:ImageButton ID="ibIECode" runat="server" Height="18px" 
        ImageUrl="~/Content/Images/Search1.png" />
</td>
</tr>
<tr>
<td class="style1">
    <asp:Label ID="lblBranchSl" runat="server" Text="Branch Sl.No" 
        CssClass="fontsize"></asp:Label>
</td>
<td colspan="5">
    <asp:TextBox ID="txtBranchSl" runat="server" CssClass="textbox150"></asp:TextBox>
</td>
</tr>
<tr>
<td class="style1">
    <asp:Label ID="lblExamDate" runat="server" Text="Examination Date" 
        CssClass="fontsize"></asp:Label>
</td>
<td colspan="5">

    <asp:TextBox ID="txtExamDate" runat="server" CssClass="textbox150"></asp:TextBox>    
    <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtExamDate">
    </cc2:CalendarExtender>
</td>
</tr>
<tr>
<td class="style1">
    <asp:Label ID="lblExaminingOfficer" runat="server" Text="Examining Officer" 
        CssClass="fontsize"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtExaminingOfficer" runat="server" CssClass="textbox150"></asp:TextBox>
</td>
<td>
    <asp:Label ID="ExDesignation" runat="server" Text="Designation" 
        CssClass="fontsize"></asp:Label>
</td>
<td colspan="3">
    <asp:TextBox ID="txtDesignation" runat="server" CssClass="textbox150"></asp:TextBox>
</td>
</tr>
<tr>
<td class="style1">
    <asp:Label ID="lblSupervising" runat="server" Text="Supervising Officer" 
        CssClass="fontsize"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtSupervising" runat="server" CssClass="textbox150"></asp:TextBox>
    </td>
    <td>
        <asp:Label ID="lblSuperDesignation" runat="server" Text="Designation" 
            CssClass="fontsize"></asp:Label>
    </td>
    <td colspan="3">
        <asp:TextBox ID="txtSuperDesignation" runat="server" CssClass="textbox150"></asp:TextBox>
    </td>
</tr>
<tr>
<td class="style1">
    <asp:Label ID="lblCommissionerate" runat="server" Text="Commissionerate" 
        CssClass="fontsize"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtCommissionerate" runat="server" CssClass="textbox150"></asp:TextBox>
</td>
<td>
    <asp:Label ID="lblDivision" runat="server" Text="Division" CssClass="fontsize"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtDivision" runat="server" CssClass="textbox150"></asp:TextBox>
</td>
<td>
    <asp:Label ID="lblRange" runat="server" Text="Range" CssClass="fontsize"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtRange" runat="server" CssClass="textbox150"></asp:TextBox>
</td>
</tr>
<tr>
<td class="style1">
    <asp:Label ID="lblVerified" runat="server" Text="Verified By Examining Officer" 
        CssClass="fontsize"></asp:Label>
</td>
<td>
    <asp:CheckBox ID="cbVerified" runat="server" Text="Verification" 
        CssClass="fontsize" AutoPostBack="True"  />
</td>
<td>
    <asp:Label ID="lblSample" runat="server" Text="Sample Forwrding" 
        CssClass="fontsize"></asp:Label>
</td>
<td colspan="3">
    <asp:CheckBox ID="cbSample" runat="server" Text="Sample" CssClass="fontsize" />
</td>
</tr>
<tr>
<td class="style1">
    <asp:Label ID="lblSealNo" runat="server" Text="Seal No"  CssClass="fontsize"></asp:Label>
    </td>
    <td colspan="5">
        <asp:TextBox ID="txtSealNo" runat="server" CssClass="textbox150"></asp:TextBox>
    </td>
</tr>
<tr>
<td colspan="6" align="center">
    <asp:Button ID="btnsaveannex" runat="server" Text="Save" BackColor="#73AAE8" 
        Height="26px" onclick="btnsaveannex_Click" Width="70px" />
        <asp:Button ID="btnupdateannex" runat="server" Text="Update" 
        BackColor="#73AAE8" Height="26px" onclick="btnupdateannex_Click" Width="70px" />
        <asp:Button ID="btncancelannex" runat="server" Text="Cancel" 
        BackColor="#73AAE8" Height="26px" Width="70px" 
        onclick="btncancelannex_Click" />    
        <asp:Button ID="btncloseannex" runat="server" Text="Close" 
        BackColor="#73AAE8" Height="26px" Width="70px" 
        onclick="btncloseannex_Click" /></td>
</tr>
<tr>
<td colspan="6">
    &nbsp;</td>
</tr>
<tr>
<td colspan="6">
    &nbsp;</td>
</tr>


</table>
</td>

<td>
<table border="0.5" style="border-width: 1px; border-style: solid">
                            <tr>
                                <td colspan="2" align="center">
                                    &nbsp; &nbsp; &nbsp; Job Details
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Job No
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlJobnoAnnexture" runat="server" 
                                        AppendDataBoundItems="True" AutoPostBack="True"
                                        CssClass="ddl100" Height="20px" Width="130px" 
                                        onselectedindexchanged="ddlJobnoshipmain_SelectedIndexChanged">
                                        <asp:ListItem>~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Job Date
                                </td>
                                <td>
                                    <asp:Label ID="lblJobDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Currency
                                </td>
                                <td>
                                    <asp:Label ID="lblCurrency" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    ExRate
                                </td>
                                <td>
                                    <asp:Label ID="lblExRate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Inv No
                                </td>
                                <td>
                                    <asp:Label ID="lblInvNo" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Inv Value
                                </td>
                                <td>
                                    <asp:Label ID="lblInvValue" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Mode
                                </td>
                                <td>
                                    <asp:Label ID="lblMode" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Custom
                                </td>
                                <td>
                                    <asp:Label ID="lblCustom" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Doc Flling Status
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    SB No
                                </td>
                                <td>
                                    <asp:Label ID="lblBeNo" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    SB Date
                                </td>
                                <td>
                                    <asp:Label ID="lblBeDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Job Approved By
                                </td>
                                <td>
                                    <asp:Label ID="lblApprovedBy" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Duty Payment Dt
                                </td>
                                <td>
                                    <asp:Label ID="lblPaymentDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize">
                                    Overseas Date
                                </td>
                                <td>
                                    <asp:Label ID="lblOverseasDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblmsg" runat="server" Style="font-weight: 700" CssClass="fontsize"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnReturn" runat="server" Text="Back To Exporter Details" 
                                        CssClass="stylebutton" Width="134px" onclick="btnReturn_Click" />
                                </td>
                            </tr>
                        </table>
</td>
</tr></table>
<table>
<tr>
<td colspan="6" valign="top" >
<fieldset style="width:180px; background-color: #CBB6CF;">
<legend style="font-weight:bold; color: #230329;" >Documents</legend>
<table style="width:100px;">
<tr>
<td>
    <asp:Label ID="lblSrNo" runat="server" Text="Sr No" CssClass="fontsize"></asp:Label>
</td>
<td>
    <asp:Label ID="lblDocumentation" runat="server" Text="Documentation" 
        CssClass="fontsize"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="lblsno" runat="server" Text="sno"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtDocumentation" runat="server" CssClass="textbox200"></asp:TextBox>
    </td>
    <td>
    <asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="masterbutton" 
            onclick="btnAdd_Click" />

</td>
<td>
    <asp:Button ID="btnupdateannexdoc" runat="server" Text="Update" 
        CssClass="masterbutton" onclick="btnupdateannexdoc_Click" />
        <asp:Button ID="btnCancelAnnexdoc" runat="server" Text="Cancel" 
        CssClass="masterbutton"  
        Visible="False"  />
</td>
</tr>
</table>
</fieldset>
</td>
</tr>
<tr>
<td colspan="6">
<div class="grid_scroll-2">
    <asp:GridView ID="gvAnnexure" runat="server" CssClass="table-wrapper" 
        AutoGenerateColumns="False" Width="592px" AutoGenerateSelectButton="True" 
        onselectedindexchanged="gvAnnexure_SelectedIndexChanged">
    <Columns>
    <asp:BoundField HeaderText="Sr No" DataField="sno" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField HeaderText="Documentation" DataField="DocumentName" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>    
    </Columns>
    <RowStyle CssClass="table-header light" />
                <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <AlternatingRowStyle BackColor="#E7E7FF" />
    </asp:GridView>

</div>
</td>
</tr>
</table>
&nbsp;
</asp:Content>
