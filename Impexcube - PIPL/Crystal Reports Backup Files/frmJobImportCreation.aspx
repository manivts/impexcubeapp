<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmJobImportCreation.aspx.cs" Inherits="ImpexCube.frmJobImportCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://localhost:1979/js/js/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="Content/Scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=gvJobNo.ClientID %>').Scrollable();
        }
)
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="width1001">
        <table class="width1001">
            <tr>
                <td colspan="7">
                    <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" />
                    &nbsp;
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="7" style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="Job Creation"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    &nbsp;
                </td>
                <td colspan="2" style="text-align: left">
                    &nbsp;
                </td>
                <td colspan="3" style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label26" runat="server" CssClass="fontsize" Text="File To Import"></asp:Label>
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    &nbsp;&nbsp;
                </td>
                <td style="text-align: left">
                    <asp:Button ID="btnRead" runat="server" OnClick="btnRead_Click" Text="Read" />
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label2" runat="server" CssClass="fontsize" Text="Job No"></asp:Label>
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:TextBox ID="txtJobNo" runat="server" CssClass="textbox200"></asp:TextBox>
                </td>
                <td class="tclass">
                    <asp:Label ID="Label3" runat="server" CssClass="fontsize" Text="Job Date"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtJobDate" runat="server" CssClass="textbox150" OnKeyPress="javascript:return false;"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtJobDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="Shipment Type"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlShipmentType" runat="server" CssClass="ddl150" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlShipmentType_SelectedIndexChanged">
                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                        <asp:ListItem>Air</asp:ListItem>
                        <asp:ListItem>Sea</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label6" runat="server" CssClass="fontsize" Text="Imp/Exp Name"></asp:Label>
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:TextBox ID="txtImpExpName" runat="server" CssClass="textbox200"></asp:TextBox>
                </td>
                <td class="tclass">
                    <asp:Label ID="Label7" runat="server" CssClass="fontsize" Text="Imp/Exp Code"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtImpExpCode" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label5" runat="server" CssClass="fontsize" Text="Mode of Shipment"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlModeOfShipment" runat="server" CssClass="ddl150" Enabled="False">
                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                        <asp:ListItem>LCL</asp:ListItem>
                        <asp:ListItem>FCL</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label11" runat="server" CssClass="fontsize" Text="Imp/Exp Branch Code"></asp:Label>
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:TextBox ID="txtImpExpBranchCode" runat="server" CssClass="textbox200"></asp:TextBox>
                </td>
                <td class="tclass">
                    <asp:Label ID="Label12" runat="server" CssClass="fontsize" Text="Imp/Exp Address"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtImpExpAddress" runat="server" TextMode="MultiLine" Width="150px"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label16" runat="server" CssClass="fontsize" Text="Imp/Exp Class Type"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlImpExpClassType" runat="server" CssClass="ddl150">
                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                        <asp:ListItem>Private</asp:ListItem>
                        <asp:ListItem>Government</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label13" runat="server" CssClass="fontsize" Text="Port of Origin"></asp:Label>
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:TextBox ID="txtPortOfOrigin" runat="server" CssClass="textbox200"></asp:TextBox>
                </td>
                <td class="tclass">
                    <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="Origin Port Code"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOriginPortCode" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label15" runat="server" CssClass="fontsize" Text="Origin State Code"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOriginStateCode" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label22" runat="server" CssClass="fontsize" Text="Origin Country Code"></asp:Label>
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:TextBox ID="txtOriginCountryCode" runat="server" CssClass="textbox200"></asp:TextBox>
                </td>
                <td class="tclass">
                    <asp:Label ID="Label17" runat="server" CssClass="fontsize" Text="Port of Destination"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtPortOfDestination" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label18" runat="server" CssClass="fontsize" Text="Destination Port Code"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDestinationPortCode" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label23" runat="server" CssClass="fontsize" Text="Destination State Code"></asp:Label>
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:TextBox ID="txtDestinationStateCode" runat="server" CssClass="textbox200"></asp:TextBox>
                </td>
                <td class="tclass">
                    <asp:Label ID="Label19" runat="server" CssClass="fontsize" Text="Destination Country Code"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDestinationCountryCode" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label20" runat="server" CssClass="fontsize" Text="Foreign Exchange Bank Code"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtForeignExchangeBankCode" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label21" runat="server" CssClass="fontsize" Text="Consignee Address"></asp:Label>
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:TextBox ID="txtConsigneeAddress" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                </td>
                <td class="tclass">
                    <asp:Label ID="Label24" runat="server" CssClass="fontsize" Text="Invoice no"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label25" runat="server" CssClass="fontsize" Text="Invoice Date"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="textbox150" OnKeyPress="javascript:return false;"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtInvoiceDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td colspan="7" style="text-align: center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    &nbsp;
                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                        Visible="False" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                        OnClientClick=" return confirm('Do you want to Cancel?')" />
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Panel ID="GridPanel" runat="server">
                        <div class="grid_scroll-2">
                            <asp:GridView ID="gvJobNo" runat="server" CellPadding="4" CssClass="table-wrapper"
                                GridLines="None" Font-Size="9pt" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                                OnSelectedIndexChanged="gvJobNo_SelectedIndexChanged">
                                <RowStyle CssClass="table-header light" Font-Size="9pt" />
                                <Columns>
                                    <asp:BoundField HeaderText="Job No" DataField="JobNo" />
                                    <asp:BoundField HeaderText="Imp/Exp Name" DataField="ImpExpName" />
                                </Columns>
                                <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" Font-Size="9pt" />
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" Font-Size="9pt" />
                                <AlternatingRowStyle BackColor="#E7E7FF" Font-Size="9pt" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
