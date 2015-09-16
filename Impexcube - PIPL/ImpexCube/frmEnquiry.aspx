<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmEnquiry.aspx.cs" Inherits="ImpexCube.CRM.frmEnquiry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 798px;
        }
        .style3
        {
            width: 112px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td style="text-align: center" align="center" class="style1">
                <asp:Label ID="lblCustomername0" runat="server" Text="Enquiry" 
                    CssClass="header"></asp:Label>
            </td>
        </tr>
      </table>
     
        <asp:Panel ID="Panel1" runat="server" Width="806px" GroupingText="Customer">
        <table>
        <tr>
            <td >
                <asp:Label ID="lblCustomername1" runat="server" CssClass="fontsize" 
                    Text="Enquiry No"></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblEnquiryNo" runat="server" CssClass="fontsize"></asp:Label>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td >
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCustomername" runat="server" CssClass="fontsize" 
                        Text="Customer Name *"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCustomername" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblPhoneNo" runat="server" CssClass="fontsize" Text="Phone No"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
            </tr>
        <tr>
            <td rowspan="2" >
                <asp:Label ID="lblAddress" runat="server" Text="Address" CssClass="fontsize"></asp:Label>
            </td>
            <td rowspan="2" colspan="2" >
                <asp:TextBox ID="txtAddress" runat="server"   CssClass="textbox150" 
                    Width="200px" Height="35px" TextMode="MultiLine"
                   ></asp:TextBox>
            </td>
            <td >
                <asp:Label ID="lblEmail" runat="server" Text="Email ID" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtContactPerson" runat="server" 
                    CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        </table>
        </asp:Panel>
         <asp:Panel ID="Panel2" runat="server" Width="806px"><%-- GroupingText="Shipment Details"--%>
      <table>
      <tr>
      <td>
       <asp:Panel ID="Panel3" runat="server" Width="806px" 
              GroupingText="Details">
       <table>
       <tr>
       <td>
       
           <asp:Label ID="lblSalesPerson" runat="server" Text="Sales Person" CssClass="fontsize"></asp:Label>
       
       </td>
       <td>
       
           <asp:TextBox ID="txtRitcCode" runat="server" CssClass="textbox150"></asp:TextBox>
       
       </td>
       <td colspan="2">
       
           <asp:Label ID="lblReferredBy" runat="server" Text="Referred By" CssClass="fontsize"></asp:Label>
       
       </td>
       <td colspan="2">
       
           <asp:TextBox ID="txtIfsCode" runat="server" CssClass="textbox150"></asp:TextBox>
       
       </td>
       </tr>
        <tr>
            <td>
                <asp:Label ID="lblModeOfEnquiry" runat="server" Text="Mode Of Enquiry" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="drModeOfEnquiry" runat="server" 
                    CssClass="ddl150">
                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                    <asp:ListItem>Mail</asp:ListItem>
                    <asp:ListItem>Reference</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="2">
                <asp:Label ID="lblCustyp" runat="server" Text="Customer Type" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlCusTyp" runat="server">
                    <asp:ListItem>~Select~</asp:ListItem>
                    <asp:ListItem>Direct</asp:ListItem>
                    <asp:ListItem>B to B</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCommodity" runat="server" Text="Commodity" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCommodity" runat="server" 
                    CssClass="textbox150"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblmoship" runat="server" Text="Mode of Shipment" 
                    CssClass="fontsize" ></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlShipMode" runat="server" AppendDataBoundItems="True" 
                    AutoPostBack="True">
                    <asp:ListItem>~Select~</asp:ListItem>
                    <asp:ListItem>Imp</asp:ListItem>
                    <asp:ListItem>Exp</asp:ListItem>
                    <asp:ListItem>Both</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="2">
                <asp:Label ID="lblModeOfShipment" runat="server" Text="Type Of Shipment" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:CheckBox ID="chkAir" runat="server" AutoPostBack="True" 
                    CssClass="fontsize" OnCheckedChanged="chkAir_CheckedChanged" Text="AIR" />                
                <asp:CheckBox ID="chkLcl" runat="server" AutoPostBack="True" 
                    CssClass="fontsize" OnCheckedChanged="chkLcl_CheckedChanged" Text="LCL" />
                    <asp:CheckBox ID="chk20Feet" runat="server" AutoPostBack="True" 
                    CssClass="fontsize" Text="20Feet" 
                    oncheckedchanged="chk20Feet_CheckedChanged" />
                    <asp:CheckBox ID="chk40Feet" runat="server" AutoPostBack="True" 
                    CssClass="fontsize" Text="40Feet" 
                    oncheckedchanged="chk40Feet_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPol" runat="server" Text="POL" CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPol" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td colspan="2">
                <asp:Label ID="lblPod" runat="server" Text="POD" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPod" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFinDest" runat="server" CssClass="fontsize" 
                     Text="Delivery At"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFinDest" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td style="font-family: Verdana; font-size: small" colspan="2">
                <asp:Label ID="earance" runat="server" Text="Clearance At" CssClass="fontsize"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtClearance" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCutdate" runat="server" Text="Cut off date" 
                    CssClass="fontsize"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCutofdate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCutofdate" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td>                
               <asp:CheckBox ID="chkLoctrans" runat="server" 
                    CssClass="fontsize" Text="Include Local Transport" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
        </asp:Panel>
        </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="3">
                
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
                                <asp:Label ID="lblQntTyp" runat="server" CssClass="fontsize" Text="Type"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlQntTypAir" runat="server" CssClass="ddl150">
                                    <asp:ListItem>~Select~</asp:ListItem>
                                    <asp:ListItem>Pieces</asp:ListItem>
                                    <asp:ListItem>Boxes</asp:ListItem>
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
                            
                                <asp:Label ID="lblUom" runat="server" CssClass="fontsize" Text="UOM"></asp:Label>
                            
                            </td>
                            <td>
                            
                                <asp:DropDownList ID="drAirUom" runat="server" CssClass="ddl150">
                                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                    <asp:ListItem>KILO GRAMS</asp:ListItem>
                                    <asp:ListItem>GRAMS</asp:ListItem>
                                    <asp:ListItem>Tons</asp:ListItem>
                                </asp:DropDownList>
                            
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
                <asp:Panel ID="pnl20Feet" runat="server" GroupingText="20FEET" Width="800px">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblContainerType" runat="server" Text="Container Type" 
                                    CssClass="fontsize"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:DropDownList ID="drContainerType" runat="server" CssClass="textbox150">
                                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                    <asp:ListItem>20Dry</asp:ListItem>
                                    <asp:ListItem>20 Feet Open Top</asp:ListItem>
                                    <asp:ListItem>20 Reefer</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td colspan="2">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblContainerNos" runat="server" Text="No of Containers" 
                                    CssClass="fontsize"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtContainerNos" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnl40Feet" runat="server" GroupingText="40FEET" Width="800px">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblConType" runat="server" Text="Container Type" 
                                    CssClass="fontsize"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:DropDownList ID="ddl40feetcont" runat="server" CssClass="textbox150">
                                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                    <asp:ListItem>40Dry</asp:ListItem>
                                    <asp:ListItem>40 Feet Open Top</asp:ListItem>
                                    <asp:ListItem>40 Reefer</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="No of Containers" 
                                    CssClass="fontsize"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtContNo" runat="server" CssClass="textbox150"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
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
                                <asp:Label ID="Qnttyp" runat="server" CssClass="fontsize" Text="Type"></asp:Label>
                            </td>
                            <td colspan="2">
                                
                                <asp:DropDownList ID="ddlQntTyp" runat="server" CssClass="textbox150">
                                    <asp:ListItem>~Select~</asp:ListItem>
                                    <asp:ListItem>Pieces</asp:ListItem>
                                    <asp:ListItem>Boxes</asp:ListItem>
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
                            
                                <asp:Label ID="lblLclUom0" runat="server" CssClass="fontsize" Text="UOM"></asp:Label>
                            
                            </td>
                            <td>
                            
                                <asp:DropDownList ID="drLclUom" runat="server" CssClass="textbox150">
                                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                    <asp:ListItem>KILO GRAMS</asp:ListItem>
                                    <asp:ListItem>GRAMS</asp:ListItem>
                                    <asp:ListItem>Tons</asp:ListItem>
                                </asp:DropDownList>
                            
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
    </asp:Panel> 
    <table>
        <tr>
            <td>
                
            </td>
            <td align="center">
            <asp:Button ID="btnSave" runat="server" CssClass="Button123" Text="Save" 
                    OnClick="btnSave_Click" BackColor="#76A7F8" ForeColor="White" />
                <asp:Button ID="btnUpdate" runat="server" CssClass="Button123" Text="Update" 
                    onclick="btnUpdate_Click" BackColor="#76A7F8" ForeColor="White" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="Button123" 
                    Text="Cancel" onclick="btnCancel_Click" BackColor="#76A7F8" 
                    ForeColor="White" />
            </td>
            
            <td>
                
            </td>
            
        </tr>
    </table>
    <div class="grid_scroll_enquiry" style="overflow:scroll; width:840px;" >
        <asp:GridView ID="grdEnquiry" runat="server" AllowPaging="True" 
                            CellPadding="4" Font-Names="Arial" Font-Size="10pt" ForeColor="#333333"                             
                             onselectedindexchanged="grdEnquiry_SelectedIndexChanged"
                            Style="text-align: center; font-size: small;" CssClass="grid-style" 
                            Width="817px" AutoGenerateColumns="True" 
            onpageindexchanging="grdEnquiry_PageIndexChanging" 
            AutoGenerateSelectButton="True" PageSize="8">
                            <FooterStyle BackColor="#507CD1" BorderColor="#fff" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Bold="True" ForeColor="White" Width="350px" />
                            <RowStyle BackColor="#EFF3FB" />
                            <RowStyle BackColor="#c9cbcc" ForeColor="Black" Font-Size="Small" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right"/>
                    <HeaderStyle BackColor="#3d99ea" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#a8d6ff" />
                            <SelectedRowStyle BackColor="#0099FF" />
                    </asp:GridView>
    </div>


</asp:Content>
