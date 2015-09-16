<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="efrmShipmentMain.aspx.cs" Inherits="ImpexCube.efrmShipmentMain" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
	     input[type=], textarea
        {
            border: 1px solid #ccc;
        }
        input[type=text]:focus, textarea:focus
        {
            background-color: #F0F0F0;
            border: 1px solid #ccc;
        }
		.style1
		{
			width: 204px;
		}
		.style2
		{
			width: 201px;
		}
		.style4
		{
			text-align: right;
		}
		.style5
		{
			width: 150px;
		}
		.style7
		{
			width: 250px;
		}
		.style8
		{
			width: 790px;
		}
		.style9
		{
			text-align: left;
		}
		.style22
        {
            font-family: Verdana;
            font-size: 8pt;
            font-weight: bold;
        }
	   <%-- .stylebtn6
		{
 padding:3px;
	margin:0px;
	cursor: pointer;
	text-align: center;
	border-radius: 2px 2px 2px 2px;/*curve of the border*/
	-webkit- border-radius: 2px 2px 2px 2px;/*support crome*/
   -moz- border-radius: 2px 2px 2px 2px;/*supportmo*/
	background: linear-gradient(to bottom, #73AAE8    0%, #73FFE8     100%) repeat scroll 0 0 transparent;
	-webkit-background: linear-gradient(to bottom, #73AAE8    0%, #73FFE8    100%) repeat scroll 0 0 transparent;
	-moz-background: linear-gradient(to bottom, #73AAE8    0%, #73FFE8    100%) repeat scroll 0 0 transparent;
	border:none;
	border:1px solid #73AAE8;
	width: 120px;
	font-size: 8pt;
	/*height:30px;*/
	color:#241e12;
		  /* font-size: 8pt;
			width:120px;
			background-color:#73AAE8;*/
			
		}--%>
	</style>
    <script type="text/javascript">
        function TabAllow(e) {
            if (e.shiftKey || e.keyCode == 9) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td class="center" colspan="2">
                        <asp:Label ID="Label44" runat="server" Text="Export Shipment" Style="font-weight: 700"></asp:Label>
                    </td>
                    <tr>
                        <td valign="top">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnMain" runat="server" CssClass="stylebtn6" OnClick="btnMain_Click"
                                            Text="Main" />
                                        <br />
                                        <asp:Button ID="btnStuff" runat="server" CssClass="stylebtn6" OnClick="btnDuty_Click"
                                            Text="Stuffing Details" />
                                        <br />
                                        <asp:Button ID="btnInvoicePrint" runat="server" CssClass="stylebtn6" OnClick="btnGenDesc_Click"
                                            Text="Invoice Printing" />
                                        <br />
                                        <asp:Button ID="btnShippingBill" runat="server" CssClass="stylebtn6" OnClick="btnITC_Click"
                                            Text="Shipping Bill Printing" />
                                        <br />
                                        <asp:Button ID="btnContainerDetails" runat="server" CssClass="stylebtn6" Text="Container"
                                            OnClick="btnContainerDetails_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="View1" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel3" runat="server" GroupingText="Main">
                                                    <table width="800">
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label1" runat="server" CssClass="fontsize" Text="Discharge Country"
                                                                                Width="120px"></asp:Label>
                                                                        </td>
                                                                        <td class="style7">
                                                                            <asp:DropDownList ID="ddlDisCountry" runat="server" AutoPostBack="True" CssClass="ddl175"
                                                                                OnSelectedIndexChanged="ddlDisCountry_SelectedIndexChanged" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" Value="~Select~">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label2" runat="server" CssClass="fontsize" Text="Nature of Cargo"
                                                                                Width="100px"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlNatureofCorgo" runat="server" CssClass="ddl175">
                                                                                <asp:ListItem Value="0">~Select~</asp:ListItem>
                                                                                <asp:ListItem>ship</asp:ListItem>
                                                                                <asp:ListItem>bike</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label3" runat="server" CssClass="fontsize" Text="Discharge Port"></asp:Label>
                                                                        </td>
                                                                        <td class="style7">
                                                                            <asp:DropDownList ID="ddlDisPort" runat="server" AutoPostBack="True" CssClass="ddl175"
                                                                                AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" Value="~Select~">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="Total No.Of Pkgs"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtpkgs1" runat="server" CssClass="textbox100" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                                                                            <asp:DropDownList ID="ddlTotalUnit" runat="server" CssClass="ddl75">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnPackingdetails" runat="server" BackColor="#73AAE8" Height="26px"
                                                                                Text="Packing Details" Width="100px" Visible="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label5" runat="server" CssClass="fontsize" Text="Destination Country"></asp:Label>
                                                                        </td>
                                                                        <td class="style7">
                                                                            <asp:DropDownList ID="ddlDesCountry" runat="server" AutoPostBack="True" CssClass="ddl175"
                                                                                OnSelectedIndexChanged="ddlDesCountry_SelectedIndexChanged" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" Value="~Select~">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label6" runat="server" CssClass="fontsize" Text="Loose Pkgs"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtLoosePkgs" runat="server" CssClass="textbox100" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label7" runat="server" CssClass="fontsize" Text="Destination Port"></asp:Label>
                                                                        </td>
                                                                        <td class="style7">
                                                                            <asp:DropDownList ID="ddlDesPort" runat="server" AutoPostBack="True" CssClass="ddl175"
                                                                                AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" Value="~Select~">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="style5">
                                                                            <asp:Label ID="lblNoofContainer" runat="server" CssClass="fontsize" Text="No.Of Containers"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNoofContainer" runat="server" CssClass="textbox100" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="lblAirlineVoyage" runat="server" CssClass="fontsize" Text="Voyage No."></asp:Label>
                                                                        </td>
                                                                        <td class="style7" colspan="4">
                                                                            <asp:TextBox ID="txtVoyageNo" runat="server" CssClass="textbox185"></asp:TextBox>
                                                                            <asp:TextBox ID="txtAirlineCode" runat="server" CssClass="textbox40" Visible="False"></asp:TextBox>
                                                                            <asp:TextBox ID="txtAirline" runat="server" CssClass="textbox150" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="lblShippingFlightNo" runat="server" CssClass="fontsize" Text="Shipping Line"></asp:Label>
                                                                        </td>
                                                                        <td class="style7">
                                                                            <asp:TextBox ID="txtShippingLine" runat="server" CssClass="textbox185"></asp:TextBox>
                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" CompletionListCssClass="completionList"
                                                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetShipment" ServicePath="~/AutoComplete.asmx"
                                                                                TargetControlID="txtShippingLine">
                                                                            </cc1:AutoCompleteExtender>
                                                                            <asp:TextBox ID="txtFlightNo" runat="server" CssClass="textbox100" Visible="False"></asp:TextBox>
                                                                            <asp:TextBox ID="txtFlightDate" runat="server" CssClass="textbox75" Visible="False"
                                                                                onkeypress="Javascript:return TabAllow(event);"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFlightDate">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label11" runat="server" CssClass="fontsize" Text="Gross Weight"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtGrossWeight1" runat="server" CssClass="textbox100" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                                                                            <asp:DropDownList ID="ddlGrossUnit" runat="server" CssClass="ddl75">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="lblVessel" runat="server" CssClass="fontsize" Text="Vessel/Sailing Date"></asp:Label>
                                                                        </td>
                                                                        <td class="style7">
                                                                            <asp:TextBox ID="txtVesselDate" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                            <asp:TextBox ID="txtSailingDate" runat="server" CssClass="textbox75" onkeypress="Javascript:return TabAllow(event);"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="txtSailingDate">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label13" runat="server" CssClass="fontsize" Text="NetWeight"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNetWeight1" runat="server" CssClass="textbox100" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
                                                                            <asp:DropDownList ID="ddlNetUnit" runat="server" CssClass="ddl75">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="EGM No/Date"></asp:Label>
                                                                        </td>
                                                                        <td class="style7">
                                                                            <asp:TextBox ID="txtEGMNO" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                            <asp:TextBox ID="txtEGMDate" runat="server" CssClass="textbox75" onkeypress="Javascript:return TabAllow(event);"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEGMDate">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label42" runat="server" CssClass="fontsize" Text="Marks&amp;Nos."></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtMarksnos" runat="server" CssClass="textboxHeight29" TextMode="MultiLine"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="lblMBLNo" runat="server" CssClass="fontsize" Text="MBL No/Date"></asp:Label>
                                                                        </td>
                                                                        <td class="style7" colspan="4">
                                                                            <asp:TextBox ID="txtMBLNO" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                            <asp:TextBox ID="txtMBLDate" runat="server" CssClass="textbox75" onkeypress="Javascript:return TabAllow(event);"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtMBLDate">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="lblHBLNo" runat="server" CssClass="fontsize" Text="HBL No/Date"></asp:Label>
                                                                        </td>
                                                                        <td class="style7" colspan="4">
                                                                            <asp:TextBox ID="txtHBLNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                            <asp:TextBox ID="txtHBLDate" runat="server" CssClass="textbox75" onkeypress="Javascript:return TabAllow(event);"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtHBLDate">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label17" runat="server" CssClass="fontsize" Text="Pre-Carriage by"></asp:Label>
                                                                        </td>
                                                                        <td class="style7">
                                                                            <asp:TextBox ID="txtPreCarriage" runat="server" CssClass="textbox185"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label18" runat="server" CssClass="fontsize" Text="Place Of Receipt"></asp:Label>
                                                                        </td>
                                                                        <td class="style7">
                                                                            <asp:TextBox ID="txtPlcereceipt" runat="server" CssClass="textbox185"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style5">
                                                                            <asp:Label ID="Label19" runat="server" CssClass="fontsize" Text="State Of Origin"></asp:Label>
                                                                        </td>
                                                                        <td class="style7">
                                                                            <asp:DropDownList ID="ddlStateofOrigin" runat="server" AppendDataBoundItems="True"
                                                                                CssClass="ddl175">
                                                                                <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style9" colspan="4">
                                                                            <asp:Label ID="Label20" runat="server" CssClass="fontsize" Text="Annexture C-Details Being Filled With Annexture-A...."></asp:Label>
                                                                            <asp:CheckBox ID="chkAnnexture" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style4" colspan="5">
                                                                            <asp:Button ID="btnSave" runat="server" CssClass="stylebutton" OnClick="btnSave_Click"
                                                                                Text="Save" Width="70px" />
                                                                            &nbsp;
                                                                            <asp:Button ID="btnUpdate" runat="server" CssClass="stylebutton" OnClick="btnUpdate_Click"
                                                                                Text="Update" Width="70px" />
                                                                            &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="stylebutton" OnClick="btnCancel_Click"
                                                                                Text="Cancel" Width="70px" />
                                                                            &nbsp;<asp:Button ID="btnClose" runat="server" CssClass="stylebutton" OnClick="btnClose_Click"
                                                                                Text="Close" Width="70px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <asp:Panel ID="Panel1" runat="server" GroupingText="Stuffing Details">
                                        <table width="800">
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="Label21" runat="server" CssClass="fontsize" Text="Goods Stuffed At"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlGoodsStuffedat" runat="server" CssClass="ddl150">
                                                        <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                        <asp:ListItem Selected="True">Dock</asp:ListItem>
                                                        <asp:ListItem>Factory</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CheckBox ID="chkSampleAccom" runat="server" />
                                                    <asp:Label ID="Label22" runat="server" CssClass="fontsize" Text="Sample Accompanied"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="Label23" runat="server" CssClass="fontsize" Text="Factory Address"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFactoryAddress" runat="server" CssClass="textboxHeight29" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="Label24" runat="server" CssClass="fontsize" Text="Seal Type"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSealType" runat="server" CssClass="ddl150">
                                                        <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                        <asp:ListItem>Agent Seal</asp:ListItem>
                                                        <asp:ListItem>Self Seal</asp:ListItem>
                                                        <asp:ListItem>Warehouse</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="Label25" runat="server" CssClass="fontsize" Text="Seal No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSealNo" runat="server" CssClass="textbox150"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <asp:Label ID="Label26" runat="server" CssClass="fontsize" Text="Agecy Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox150"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:Button ID="btnSave1" runat="server" CssClass="stylebutton" Text="Save" Visible="False"
                                                        Width="70px" />
                                                    <asp:Button ID="btnUpdatestuff" runat="server" CssClass="stylebutton" OnClick="btnUpdatestuff_Click"
                                                        Text="Save" Width="70px" />
                                                    <asp:Button ID="btnCancel1" runat="server" CssClass="stylebutton" OnClick="btnCancel1_Click"
                                                        Text="Cancel" Width="70px" />
                                                    <asp:Button ID="btnClose1" runat="server" CssClass="stylebutton" OnClick="btnClose1_Click"
                                                        Text="Close" Width="70px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    <asp:Panel ID="Panel2" runat="server" GroupingText="Invoice Printing">
                                        <table width="800">
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="Label28" runat="server" CssClass="fontsize" Text="Buyer's Order No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtbuyerorder1" runat="server" CssClass="textbox100"></asp:TextBox>
                                                    <asp:TextBox ID="txtbuyerorder2" runat="server" CssClass="textbox75" onkeypress="Javascript:return TabAllow(event);"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" TargetControlID="txtbuyerorder2">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="Label29" runat="server" CssClass="fontsize" Text="Other Referenes"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOtherreferences" runat="server" CssClass="textbox175"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="Label30" runat="server" CssClass="fontsize" Text="Terms Of Delivery And Payment"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDeliveryPayment" runat="server" CssClass="textbox100" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="Label31" runat="server" CssClass="fontsize" Text="Origin Country"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOriginCountry" runat="server" CssClass="textbox175"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionListCssClass="completionList"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetCountry" ServicePath="~/AutoComplete.asmx"
                                                        TargetControlID="txtOriginCountry">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    <asp:Label ID="Label32" runat="server" CssClass="fontsize" Text="Invoice Header"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInvoiceHeader" runat="server" CssClass="textbox175"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:Button ID="btnSave2" runat="server" CssClass="stylebutton" Text="Save" Visible="False"
                                                        Width="70px" />
                                                    <asp:Button ID="btnUpdateInvPrint" runat="server" CssClass="stylebutton" OnClick="btnUpdateInvPrint_Click"
                                                        Text="Save" Width="70px" />
                                                    <asp:Button ID="btnCancel2" runat="server" CssClass="stylebutton" OnClick="btnCancel2_Click"
                                                        Text="Cancel" Width="70px" />
                                                    <asp:Button ID="btnClose2" runat="server" CssClass="stylebutton" OnClick="btnClose2_Click"
                                                        Text="Close" Width="70px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </asp:View>
                                <asp:View ID="View4" runat="server">
                                    <asp:Panel ID="Panel4" runat="server" GroupingText="Shipping Bill Printing">
                                        <table width="800">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label34" runat="server" CssClass="fontsize" Text="Q/Cert.No/Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCertnoDate" runat="server" CssClass="radio-area3" TextMode="MultiLine"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCertnoDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label35" runat="server" CssClass="larea" Text="Export Trade Control"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExportControl" runat="server" CssClass="radio-area3" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label36" runat="server" CssClass="fontsize" Text="Type Of Shipment"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTypefShipment" runat="server" CssClass="ddl150">
                                                        <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                        <asp:ListItem Selected="True">Outright Sale</asp:ListItem>
                                                        <asp:ListItem>Consignment Export</asp:ListItem>
                                                        <asp:ListItem>Others</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label37" runat="server" CssClass="fontsize" Text="Export Under"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlExportUnder" runat="server" CssClass="ddl150">
                                                        <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                        <asp:ListItem Selected="True">Deferred Credit</asp:ListItem>
                                                        <asp:ListItem>Joint Ventures</asp:ListItem>
                                                        <asp:ListItem>Rupee Credit</asp:ListItem>
                                                        <asp:ListItem>Other</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label39" runat="server" CssClass="fontsize" Text="S/B Type"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSBtype" runat="server" CssClass="ddl150">
                                                        <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                        <asp:ListItem Selected="True">White - Free/DEEC</asp:ListItem>
                                                        <asp:ListItem>Green - Drawback</asp:ListItem>
                                                        <asp:ListItem>Blue - DEPB</asp:ListItem>
                                                        <asp:ListItem>Yellow - Dutiable</asp:ListItem>
                                                        <asp:ListItem>Pink - ExBond</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label40" runat="server" CssClass="fontsize" Text="S/B Heading"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSBHeading" runat="server" CssClass="textbox150"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label41" runat="server" CssClass="fontsize" Text="Type To Be Printed On S/B Bottom Area"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtSBBottomArea" runat="server" CssClass="postmsgg23ex1" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td align="right" class="style8">
                                                    <asp:Button ID="btnSave3" runat="server" CssClass="stylebutton" OnClick="btnSave3_Click"
                                                        Text="Save" Visible="False" Width="70px" />
                                                    <asp:Button ID="btnUpdateShipbillprint" runat="server" CssClass="stylebutton" OnClick="btnUpdateShipbillprint_Click"
                                                        Text="Save" Width="70px" />
                                                    <asp:Button ID="btnCancel3" runat="server" CssClass="stylebutton" OnClick="btnCancel3_Click"
                                                        Text="Cancel" Width="70px" />
                                                    <asp:Button ID="btnClose3" runat="server" CssClass="stylebutton" OnClick="btnClose3_Click"
                                                        Text="Close" Width="70px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </asp:View>
                                <asp:View ID="View5" runat="server">
                                    <asp:Panel ID="Panel5" runat="server" GroupingText="Container Details">
                                        <table width="850px">
                                            <tr>
                                                <td class="tdcolumn100">
                                                    <asp:Label ID="Label8" runat="server" CssClass="fontsize" Text="Container No"></asp:Label>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:Label ID="Label9" runat="server" CssClass="fontsize" Text="Seal No"></asp:Label>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:Label ID="Label10" runat="server" CssClass="fontsize" Text="Seal Date"></asp:Label>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:Label ID="Label12" runat="server" CssClass="fontsize" Text="Size"></asp:Label>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:Label ID="Label15" runat="server" CssClass="fontsize" Text="Type"></asp:Label>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:Label ID="Label16" runat="server" CssClass="fontsize" Text="No Of Packets Stuffed"></asp:Label>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:Label ID="Label27" runat="server" CssClass="fontsize" Text="Transporter"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdcolumn100">
                                                    <asp:TextBox ID="txtContainerNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:TextBox ID="txtContainerSeal" runat="server" CssClass="textbox75" Height="15px"></asp:TextBox>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:TextBox ID="txtSealDate" runat="server" CssClass="textbox75"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" TargetControlID="txtSealDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:TextBox ID="txtSize" runat="server" CssClass="textbox75" Height="15px"></asp:TextBox>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:TextBox ID="txtType" runat="server" CssClass="textbox75"></asp:TextBox>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:TextBox ID="txtPacketStuffed" runat="server" CssClass="textbox75" Height="15px"></asp:TextBox>
                                                </td>
                                                <td class="tdcolumn100">
                                                    <asp:TextBox ID="txtTransporter" runat="server" CssClass="textbox75"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAddd" runat="server" Height="26px" Text="Add" Width="50px" 
                                                        CssClass="stylebutton" onclick="btnAddd_Click" />
                                                    <asp:Button ID="btnContainerUpdate" runat="server" Height="26px" Text="Update" Visible="False"
                                                        Width="50px" CssClass="stylebutton" onclick="btnContainerUpdate_Click"/>
                                                    <asp:Button ID="btnMainCancel" runat="server" Height="26px" Text="New" Width="50px"
                                                        CssClass="stylebutton" onclick="btnMainCancel_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <asp:GridView ID="gvContainerDetails" runat="server" CssClass="table-wrapper" 
                                                        AutoGenerateColumns="False" 
                                                        onselectedindexchanged="gvContainerDetails_SelectedIndexChanged">
                                                        <Columns>
                                                            <asp:CommandField ShowSelectButton="True" />
                                                            <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Job No" DataField="JobNo"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Container No" DataField="ContainerNo"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Seal No" DataField="SealNo"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Seal Date" DataField="SealDate"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Size" DataField="Size"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Type" DataField="Type"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Packet Stuffed" DataField="NoofPktsStuffed"></asp:BoundField>
                                                            <asp:BoundField HeaderText="Transporter" DataField="Transporter"></asp:BoundField>
                                                            <%--<asp:BoundField HeaderText="Exim Cd" DataField="EximCode"></asp:BoundField>--%>
                                                        </Columns>
                                                        <RowStyle CssClass="table-header light" />
                                                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                        <AlternatingRowStyle BackColor="#E7E7FF" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </asp:View>
                            </asp:MultiView>
                        </td>
                        <td valign="top">
                            <table border="0.5">
                                <tr>
                                    <td align="center" colspan="2">
                                        &nbsp; &nbsp; &nbsp; Job Details
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fontsize">
                                        <asp:Label ID="Label55" runat="server" CssClass="fontsizehistory" Text="Job Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlJobnoshipmain" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="True" CssClass="ddl100" Height="20px" OnSelectedIndexChanged="ddlJobnoshipmain_SelectedIndexChanged"
                                            Width="130px">
                                            <asp:ListItem>~Select~</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        <asp:Label ID="Label45" runat="server" CssClass="fontsizehistory" Text="Job Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblJobReceivedDate" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label46" runat="server" CssClass="fontsizehistory" Text="Mode"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:Label ID="lblMode" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label47" runat="server" CssClass="fontsizehistory" Text="Custom"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:Label ID="lblCustom" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label48" runat="server" CssClass="fontsizehistory" Text="SB Type"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:Label ID="lblBEType" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label49" runat="server" CssClass="fontsizehistory" Text="Doc Filling"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:Label ID="lblDocFillingStatus" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label50" runat="server" CssClass="fontsizehistory" Text="SB No"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:Label ID="lblBENo" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label51" runat="server" CssClass="fontsizehistory" Text="SB Date"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:Label ID="lblBEDate" runat="server" CssClass="fontsize"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="fontsize" Style="font-weight: 700"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3" valign="middle">
                            <asp:Button ID="btnReturn" runat="server" CssClass="stylebutton" OnClick="btnReturn_Click"
                                Text="Back To JobCreation" Width="134px" />
                            &nbsp;
                            <asp:Button ID="btnForward" runat="server" CssClass="stylebutton" OnClick="btnForward_Click"
                                Text="Go To Invoice" Width="134px" />
                        </td>
                    </tr>
                </tr>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
