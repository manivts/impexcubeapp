<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmEnquiry.aspx.cs" Inherits="ImpexCube.CRM.frmEnquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Label ID="lblCustomername0" runat="server" Text="Enquiry" 
                    CssClass="header"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCustomername" runat="server" Text="Customer Name" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCustomername" runat="server" Width="180px" 
                    CssClass="textbox150"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPhoneNo" runat="server" Width="180px" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td rowspan="2">
                <asp:Label ID="lblAddress" runat="server" Text="Address" CssClass="fontsize"></asp:Label>
            </td>
            <td rowspan="2">
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Height="67px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblEmail" runat="server" Text="Email ID" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Width="180px" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtContactPerson" runat="server" Width="180px" 
                    CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModeOfEnquiry" runat="server" Text="Mode Of Enquiry" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="drModeOfEnquiry" runat="server" Width="182px" 
                    CssClass="ddl150">
                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                    <asp:ListItem>Mail</asp:ListItem>
                    <asp:ListItem>Reference</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblRifs" runat="server" Text="RITC Code" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRitcCode" runat="server" Width="180px" 
                    CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCommodity" runat="server" Text="Commodity" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCommodity" runat="server" Width="180px" 
                    CssClass="textbox150"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="IFS Code" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtIfsCode" runat="server" Width="180px" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPol" runat="server" Text="POL" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPol" runat="server" Width="180px" CssClass="textbox150"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblPod" runat="server" Text="POD" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPod" runat="server" Width="180px" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModeOfShipment" runat="server" Text="Type Of Shipment" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="3">
                <asp:CheckBox ID="chkAir" runat="server" Text="AIR" AutoPostBack="True" 
                    OnCheckedChanged="chkAir_CheckedChanged" CssClass="fontsize" />
                <asp:CheckBox ID="chkFcl" runat="server" Text="FCL" AutoPostBack="True" 
                    OnCheckedChanged="chkFcl_CheckedChanged" CssClass="fontsize" />
                <asp:CheckBox ID="chkLcl" runat="server" Text="LCL" AutoPostBack="True" 
                    OnCheckedChanged="chkLcl_CheckedChanged" CssClass="fontsize" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnlAir" runat="server" GroupingText="AIR" Width="800px">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblAirQuantity" runat="server" Text="Quantity" 
                                    CssClass="fontsize"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAirQuantity" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblUom" runat="server" Text="UOM" CssClass="fontsize"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="drAirUom" runat="server" CssClass="ddl150">
                                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                    <asp:ListItem>KILO GRAMS</asp:ListItem>
                                    <asp:ListItem>GRAMS</asp:ListItem>
                                    <asp:ListItem>Tons</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAirGrossWeight" runat="server" Text="Gross Weight" 
                                    CssClass="fontsize"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAirGrossWeight" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblAirChargeableWeight" runat="server" Text="Chargeable Weight" 
                                    CssClass="fontsize"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAirChargeableWeight" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblAirVolume" runat="server" Text="Volume (CBM)" 
                                    CssClass="fontsize"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAirVolume" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnlFcl" runat="server" GroupingText="FCL" Width="800px">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblContainerType" runat="server" Text="Container Type" 
                                    CssClass="fontsize"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drContainerType" runat="server" CssClass="textbox150">
                                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                    <asp:ListItem>20Dry</asp:ListItem>
                                    <asp:ListItem>20 Feet Open Top</asp:ListItem>
                                    <asp:ListItem>20 Reefer</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblContainerNos" runat="server" Text="Container Nos" 
                                    CssClass="fontsize"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtContainerNos" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnlLcl" runat="server" GroupingText="LCL" Width="800px">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblLclQuantity" runat="server" Text="Quantity" 
                                    CssClass="fontsize"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLclQuantity" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblLclUom" runat="server" Text="UOM" CssClass="fontsize"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="drLclUom" runat="server" CssClass="textbox150">
                                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                    <asp:ListItem>KILO GRAMS</asp:ListItem>
                                    <asp:ListItem>GRAMS</asp:ListItem>
                                    <asp:ListItem>Tons</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblLclGrossWeight" runat="server" Text="GROSS WEIGHT" 
                                    CssClass="fontsize"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLclGrossWeight" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblLclChargeableWeight" runat="server" Text="Chargeable Weight" 
                                    CssClass="fontsize"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLclChargeableWeight" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblLclVolume" runat="server" Text="Volume (CBM)" 
                                    CssClass="fontsize"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLclVolume" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            </td>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                    onclick="btnUpdate_Click" />
            </td>
            
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
            </td>
            
        </tr>
    </table>
    <div class="grid_scroll_enquiry" >
        <asp:GridView ID="grdEnquiry" runat="server" AutoGenerateSelectButton="True" 
            onselectedindexchanged="grdEnquiry_SelectedIndexChanged"  CssClass="table-wrapper" >
             <RowStyle CssClass="table-header light" />
                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <AlternatingRowStyle BackColor="#E7E7FF" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>


</asp:Content>
