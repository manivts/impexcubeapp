<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmImporter.aspx.cs" Inherits="ImpexCube.frmImporter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="width100">
        <table>
            <tr>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    </asp:UpdateProgress>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;
                            <div class="div70">
                                <table class="tableImp">
                                    <tr>
                                        <td class="center" colspan="4">
                                            <asp:Label ID="Label1" runat="server" Text="Importer Details"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="Importer"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlImporter" runat="server" AppendDataBoundItems="True" 
                                                AutoPostBack="True" CssClass="ddl150" 
                                                OnSelectedIndexChanged="ddlImporter_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Button ID="btnNew" runat="server" onclick="btnNew_Click" Text="New" />
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
                                        <td>
                                            <asp:Label ID="Label5" runat="server" CssClass="fontsize" Text="IE Code No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblIECodeNo" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" CssClass="fontsize" 
                                                Text="Port of Shipment"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPortofShipment" runat="server" CssClass="ddl200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" CssClass="fontsize" Text="Branch SNo"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBranchSno" runat="server" AppendDataBoundItems="True" 
                                                AutoPostBack="True" CssClass="ddl200" 
                                                OnSelectedIndexChanged="ddlBranchSno_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" CssClass="fontsize" 
                                                Text="Country of Origin"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCountryOfOrigin" runat="server" CssClass="ddl200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" CssClass="fontsize" Text="Address"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAddress" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" CssClass="fontsize" 
                                                Text="Country of Shipment"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCountryOfShipment" runat="server" CssClass="ddl200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" CssClass="fontsize" Text="City"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCity" runat="server" CssClass="fontsize"></asp:Label>
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
                                            <asp:Label ID="Label9" runat="server" CssClass="fontsize" Text="State/Zip Code"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStateImp" runat="server" CssClass="fontsize"></asp:Label>
                                            /<asp:Label ID="lblZipCode" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="Label16" runat="server" Text="Commerical Tax Details "></asp:Label>
                                            &nbsp; &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" CssClass="fontsize" Text="State"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblState" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" CssClass="fontsize" Text="Tax Type"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTaxType" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:CheckBox ID="chkHighSeaSale" runat="server" AutoPostBack="True" 
                                                OnCheckedChanged="chkHighSeaSale_CheckedChanged" Text=" High Sea Sale" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" CssClass="fontsize" Text="Regn. No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRegnNo" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label24" runat="server" CssClass="fontsize" Text="Seller Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSellerName" runat="server" AppendDataBoundItems="True" 
                                                AutoPostBack="True" CssClass="ddl200" 
                                                OnSelectedIndexChanged="ddlSellerName_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox ID="chkSingleConsignor" runat="server" Text=" Single Consignor" />
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label25" runat="server" CssClass="fontsize" Text="IE Code No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblIECodeNoHigh" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" CssClass="fontsize" Text="Consignor"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtConsignor" runat="server" CssClass="ddl200"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label26" runat="server" CssClass="fontsize" Text="Branch SNo"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBranchSnoHigh" runat="server" 
                                                AppendDataBoundItems="True" AutoPostBack="True" CssClass="ddl200" 
                                                OnSelectedIndexChanged="ddlBranchSnoHigh_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" CssClass="fontsize" Text="Address"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="ddl200"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label27" runat="server" CssClass="fontsize" Text="Address"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAddressHigh" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" CssClass="fontsize" Text="City"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="ddl200" 
                                                AppendDataBoundItems="True">
                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label28" runat="server" CssClass="fontsize" Text="City"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCityHigh" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label23" runat="server" CssClass="fontsize" Text="Country"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="ddl200"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label29" runat="server" CssClass="fontsize" 
                                                Text="State/Zip Code"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStateHigh" runat="server" CssClass="fontsize"></asp:Label>
                                            /<asp:Label ID="lblZipCodeHigh" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="center" colspan="4">
                                            <asp:Button ID="btnJobCreation" runat="server" onclick="btnJobCreation_Click" 
                                                Text="Back to Job Creation" />
                                            &nbsp;
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" 
                                                Width="70px" />
                                            &nbsp;
                                            <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                                                Text="Update" Visible="False" />
                                            &nbsp; &nbsp;
                                            <asp:Button ID="btnShipment" runat="server" Text="Go to Shipment" 
                                                onclick="btnShipment_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td valign="top">
                            &nbsp;
                            <div class="div30">
                                <table class="tableImpHis">
                                    <tr>
                                        <td class="center" colspan="2">
                                            <asp:Label ID="Label30" runat="server" Text="History"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label31" runat="server" CssClass="fontsize" Text="Job No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlJobNo" runat="server" AppendDataBoundItems="True" 
                                                AutoPostBack="True" CssClass="span3v1 required" 
                                                OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label32" runat="server" CssClass="fontsize" 
                                                Text="Job Received Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblJobReceivedDate" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label33" runat="server" CssClass="fontsize" Text="Mode"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMode" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label34" runat="server" CssClass="fontsize" Text="Custom"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCustom" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label35" runat="server" CssClass="fontsize" Text="BE Type"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblBEType" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label36" runat="server" CssClass="fontsize" 
                                                Text="Doc Filling Status"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDocFillingStatus" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label37" runat="server" CssClass="fontsize" Text="BE No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblBENo" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label38" runat="server" CssClass="fontsize" Text="BE Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblBEDate" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label39" runat="server" CssClass="fontsize" 
                                                Text="Job Approved By"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblJobApprovedBy" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label40" runat="server" CssClass="fontsize" 
                                                Text="Duty Payment Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDutyPaymentDate" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label41" runat="server" CssClass="fontsize" Text="Oversea Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOverseaDate" runat="server" CssClass="fontsize"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
