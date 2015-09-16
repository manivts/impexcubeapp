<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmProductMainPage.aspx.cs" Inherits="ImpexCube.frmPRoductMainPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/Styles/Accordion.css" />
    <script type="text/javascript" src="Content/Scripts/Accordion.js"></script>
    <script type="text/javascript" src="Content/Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="Content/Scripts/jquery-ui.js"></script>
    <style type="text/css">
		.waterText
		{
			font-family: Arial;
			font-size: 8pt;
			color: Fuchsia;
			overflow: auto;
			background-color: #FFFFFF;
		}
	   .completionListProduct
		{
			border: solid 1px #444444;
			margin: 0px;
			padding: 2px;
			height: 100px;
			width: 600px ! important;
			overflow: auto;
			font-family: verdana;
			font-size: 10px;
			background-color: white;
		}
		.completionListCode
		{
			border: solid 1px #444444;
			margin: 0px;
			padding: 2px;
			height: 100px;
			width: 200px ! important;
			overflow: auto;
			font-family: verdana;
			font-size: 10px;
			background-color: white;
		}
		 .completionListExim
		{
			border: solid 1px #444444;
			margin: 0px;
			padding: 2px;
			height: 100px;
			width: 300px ! important;
			overflow: auto;
			font-family: verdana;
			font-size: 10px;
			background-color: white;
		}
		.style1
		{
			height: 23px;
		}
		.stylenone
		{
			display:none;
		}
		.style3
		{
			font-family: Verdana;
			font-size: 8pt;
			font-weight: bold;
		text-align: center;
	}
		.style4
		{
			font-family: Arial;
			font-size: 8pt;
			font-weight: bold;
			padding: 1px;
			}
		.style5
		{
			font-size: 8pt;
			font-family: Arial;
		}
		.stylebtn6
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
		}
		.style6
		{
			font-size: 8pt;
		}
		.style7
		{
			text-align: center;
		}
		.style8
		{}
	    .style9
        {
            color: #FF0000;
        }
	</style>
    <script type="text/javascript">
        function calamt() {
            var uprice = document.getElementById['ContentPlaceHolder1_txtunitprice'].value;
            var qty = document.getElementById['ContentPlaceHolder1_txtqty'].value;
            var exrate = document.getElementById['ContentPlaceHolder1_lblExRate'].value;
            var amt = parseInt(uprice) * parseInt(qty);
            var amtinr = parseInt(amt) * parseInt(exrate);

            document.getElementById['ContentPlaceHolder1_txtamount'] = amt;
            document.getElementById['ContentPlaceHolder1_txtINRAmount'] = amtinr;
        }
        function caluprice() {

            var qty = document.getElementById['ContentPlaceHolder1_txtqty'].value;
            var exrate = document.getElementById['ContentPlaceHolder1_lblExRate'].value;
            var amt = document.getElementById['ContentPlaceHolder1_txtamount'].value;
            //  parseInt(uprice) * parseInt(qty);
            var amtinr = parseInt(amt) * parseInt(exrate);
            var uprice = parseInt(amt) * parseInt(qty);
            //document.getElementById['ContentPlaceHolder1_txtamount'] = amt;
            document.getElementById['ContentPlaceHolder1_txtunitprice'] = uprice;
            document.getElementById['ContentPlaceHolder1_txtINRAmount'] = amtinr;
        }
        function SchemeName() {
            try {
                var scheme = document.getElementById('ContentPlaceHolder1_txtEXIM').value;

                var schemecode = scheme.substring(0, 2);
                if (scheme != schemecode) {
                    var schemename = scheme.substring(2, 65);

                    document.getElementById('ContentPlaceHolder1_txtEximSchemeDesc').value = schemename;
                    document.getElementById('ContentPlaceHolder1_txtEXIM').value = schemecode;
                }
            }
            catch (err) {
                alert(err.Message);
            }
        }
        function SchemeNotn() {
            try {
                var scheme = document.getElementById('ContentPlaceHolder1_txtSchemeNotn').value;

                var schemenotn = scheme.split(" ");

                if (scheme != schemenotn[0]) {

                    document.getElementById('ContentPlaceHolder1_txtSchemeNotn').value = schemenotn[0];
                    document.getElementById('ContentPlaceHolder1_txtSchemeUnit').value = schemenotn[1];
                    document.getElementById('ContentPlaceHolder1_txtSchemeDesc').value = schemenotn[2];
                }
            }
            catch (err) {
                alert(err.Message);
            }
        }
        function DutyNotn(Notification, SLNo, DutyRate) {
            try {
                var scheme = document.getElementById(Notification).value;

                var schemenotn = scheme.split("     |  ");
                //'ContentPlaceHolder1_txtBasicDutyNotn','ContentPlaceHolder1_txtBasicDutySno','ContentPlaceHolder1_txtBasicDutyRate'
                if (scheme != schemenotn[0]) {
                    document.getElementById(Notification).value = schemenotn[0];
                    document.getElementById(SLNo).value = schemenotn[1];
                    document.getElementById(DutyRate).value = schemenotn[2];
                }
            }
            catch (err) {
                alert(err.Message);
            }
        }
        function exit() {
            var status = confirm("Do You Want To Delete?");
            if (status == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <%--<asp:Button ID="btnGenDesc" runat="server" CssClass="stylebtn6" OnClick="btnGenDesc_Click"
										Text="Generic Desc" />--%><%--<asp:Button ID="btnGenDesc" runat="server" CssClass="stylebtn6" OnClick="btnGenDesc_Click"
										Text="Generic Desc" />--%><%--<asp:Button ID="btnGenDesc" runat="server" CssClass="stylebtn6" OnClick="btnGenDesc_Click"
										Text="Generic Desc" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="1000">
                <tr>
                    <td rowspan="10" valign="top">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <br />
                                    <br />
                                    <br />
                                    <%--<asp:Button ID="btnGenDesc" runat="server" CssClass="stylebtn6" OnClick="btnGenDesc_Click"
										Text="Generic Desc" />--%>
                                    <%--<br />--%>
                                    <asp:Button ID="btnITC" runat="server" CssClass="displaynon" OnClick="btnITC_Click"
                                        Text="ITC Lic." Visible="False" />
                                    <br />
                                    <br />
                                    <%-- <br />--%>
                                    <asp:Button ID="btnPre" runat="server" CssClass="displaynon" Text="Prev BE &amp; ReImport"
                                        OnClick="btnPre_Click" Visible="False" />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" style="height: 128px;">
                        <%--                        <div ID="Panel1" runat="server">--%>
                        <table width="850">
                            <%--<tr>
								<td>
									<asp:Label ID="lblpro0" runat="server" CssClass="style3" Text="Search Product"></asp:Label>
								</td>
								<td colspan="7">
									<asp:TextBox ID="txtSearchProduct" runat="server" Width="600px" Height="15px"></asp:TextBox>
								</td>
							</tr>--%>
                            <tr>
                                <td colspan="8">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblpro" runat="server" CssClass="style3" Text="Product Name"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                        ID="type0" runat="server" CssClass="style3" Text="Product Code"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="type1" runat="server" CssClass="style3" Text="Product Family"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <asp:TextBox ID="txtpro" runat="server" AutoPostBack="True" Height="15px" OnTextChanged="txtpro_TextChanged"
                                        Style="font-size: 8pt" Width="500px" CssClass="fontsize"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteProduct" runat="server" CompletionListCssClass="completionListProduct"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetProductName" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtpro">
                                    </cc1:AutoCompleteExtender>
                                    <%--<asp:Button ID="btnPro" runat="server" Height="20px" OnClick="btnPro_Click" Text="Search" />--%>
                                    <asp:TextBox ID="txtProductCode" runat="server" AutoPostBack="True" Height="15px"
                                        OnTextChanged="txtProductCode_TextChanged" Width="120px" CssClass="fontsize"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionListCode"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetProductCode" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtProductCode">
                                    </cc1:AutoCompleteExtender>
                                    <asp:TextBox ID="txtProductFamily" runat="server" Height="15px" Width="120px" CssClass="fontsize"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    <asp:Label ID="Label18" runat="server" CssClass="style3" Text="RITC NO"></asp:Label>
                                </td>
                                <td class="style7">
                                    <asp:Label ID="type" runat="server" CssClass="style3" Text="Type"></asp:Label>
                                </td>
                                <td class="style7">
                                    <asp:Label ID="lblqty" runat="server" CssClass="style3" Text="Qty"></asp:Label>
                                </td>
                                <td class="style7">
                                    <asp:Label ID="lblunit" runat="server" CssClass="style3" Text="Unit"></asp:Label>
                                </td>
                                <td class="style7">
                                    <asp:Label ID="nitprice" runat="server" CssClass="style3" Text="Unit Price"></asp:Label>
                                </td>
                                <td class="style7">
                                    <asp:Label ID="lblamount" runat="server" CssClass="style3" Text="Amount"></asp:Label>
                                </td>
                                <td class="style7">
                                    <asp:Label ID="Label1" runat="server" CssClass="style3" Text="Amount INR"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnNew" runat="server" CssClass="stylebutton" OnClick="btnNew_Click"
                                        Text="New" Width="60px" Height="20px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    <asp:TextBox ID="txtRITC" runat="server" AutoPostBack="True" CssClass="textbox75"
                                        OnTextChanged="txtRITC_TextChanged"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetRITCCode" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtRITC">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td class="style7">
                                    <asp:DropDownList ID="ddltype" runat="server" CssClass="ddl100" Height="17px">
                                    </asp:DropDownList>
                                </td>
                                <td class="style7">
                                    <asp:TextBox ID="txtqty" runat="server" CssClass="textbox100"></asp:TextBox>
                                </td>
                                <td class="style7">
                                    <asp:DropDownList ID="ddlUnit" runat="server" AppendDataBoundItems="True" CssClass="ddl75">
                                        <asp:ListItem>~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="style7">
                                    <asp:TextBox ID="txtunitprice" runat="server" AutoPostBack="True" CssClass="textbox75"
                                        OnTextChanged="txtunitprice_TextChanged"></asp:TextBox>
                                </td>
                                <td class="style7">
                                    <asp:TextBox ID="txtamount" runat="server" AutoPostBack="True" CssClass="textbox100"
                                        OnTextChanged="txtamount_TextChanged"></asp:TextBox>
                                </td>
                                <td class="style7">
                                    <asp:TextBox ID="txtINRAmount" runat="server" CssClass="textbox100" AutoPostBack="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="stylebutton" OnClick="btnUpdate_Click"
                                        Text="Update" Visible="False" Height="20px" Width="60px" />
                                    <asp:Button ID="btnadd" runat="server" CssClass="stylebutton" OnClick="btnadd_Click"
                                        Text="Add" Visible="False" Width="60px" Height="20px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" style="text-align: center">
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnbckProduct" runat="server" CssClass="stylebutton"
                                        OnClick="btnbckProduct_Click" Text="Product" Width="100px" />
                                    &nbsp;<asp:Button ID="btnMain" runat="server" CssClass="stylebutton" OnClick="btnMain_Click"
                                        Text="Generic Desc" Width="100px" />
                                    <asp:Button ID="btnDuty" runat="server" CssClass="stylebutton" OnClick="btnDuty_Click"
                                        Text="Duty" Width="100px" />
                                    <asp:Button ID="btnOtherDuty" runat="server" CssClass="stylebutton" OnClick="btnOtherDuty_Click"
                                        Text="Other Duty" Width="100px" Visible="False" />
                                    <asp:Button ID="btnSch" runat="server" CssClass="stylebutton" OnClick="btnSch_Click"
                                        Text="Scheme" Visible="False" Width="100px" />
                                    <asp:Button ID="btnCheckList" runat="server" CssClass="stylebutton" OnClick="btnCheckList_Click"
                                        Text="Check List" Width="100px" />
                                    <asp:Button ID="btnBEFile" runat="server" CssClass="stylebutton" OnClick="btnBEFile_Click"
                                        Text="BE File" Width="100px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <div id="divProduct" runat="server" style="height: 300px; width: 850px; overflow: auto;">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                                            Font-Size="10pt" OnRowEditing="GridView1_RowEditing" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                            Width="850px" Style="font-size: 8pt" ShowFooter="True">
                                            <Columns>
                                                <%--<asp:BoundField DataField="ProductID" HeaderText="Id" ReadOnly="True" 
														Visible="false" />--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Content/Images/delete.gif"
                                                            OnClick="btnDelete_Click" Width="20px" OnClientClick="javascript:return exit();" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="SNo" ItemStyle-Width="10px">
													<ItemTemplate>
														<asp:Label ID="lblSerial" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
													</ItemTemplate>
													<ItemStyle Width="10px" />
												</asp:TemplateField>--%>
                                                <asp:BoundField DataField="ProductSNo" HeaderText="SNo" HtmlEncode="False" />
                                                <asp:BoundField DataField="ProductID" HeaderText="Id" HeaderStyle-CssClass="hiddencol"
                                                    ItemStyle-CssClass="hiddencol">
                                                    <%--     <FooterStyle CssClass="stylenone" />
													<HeaderStyle CssClass="stylenone" />
													<ItemStyle CssClass="stylenone" />--%>
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemStyle CssClass="hiddencol" />
                                                    <FooterStyle CssClass="hiddencol" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProductDesc" HeaderText="Pro Des" HtmlEncode="False" />
                                                <asp:BoundField DataField="ProType" HeaderText="Type" HtmlEncode="False" />
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnCopy" runat="server" ImageUrl="~/Content/Images/Copy.jpg"
                                                            OnClick="btnCopy_Click" Width="40px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<input id="btnUp" type="button" value="&uArr;" style="color: Green;" />
						<input id="btnDown" type="button" value="&dArr;" style="color: Green;" />
					</ItemTemplate>
													 <ItemStyle HorizontalAlign="Center" />
				</asp:TemplateField>--%>
                                            </Columns>
                                            <FooterStyle BackColor="#C6C3C6" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                ForeColor="Black" />
                                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        <%--      </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <%--                        </div>--%>
                    </td>
                    <td rowspan="10" valign="top">
                        <table width="150">
                            <tr>
                                <td colspan="2">
                                    &nbsp; &nbsp; &nbsp; Job Details
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Job No
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlJobNo" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged" CssClass="style5" Height="20px"
                                        Width="150px">
                                        <asp:ListItem>~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Invoice No
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlInvNo" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlInvNo_SelectedIndexChanged" CssClass="style5" Height="20px"
                                        Width="150px">
                                        <asp:ListItem>~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Job Date
                                </td>
                                <td>
                                    <asp:Label ID="lblJobDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Inv. Date
                                </td>
                                <td>
                                    <asp:Label ID="lblInvDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Inv. Amount
                                </td>
                                <td>
                                    <asp:Label ID="lblInvAmt" runat="server" Style="font-weight: 700"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Currency
                                </td>
                                <td>
                                    <asp:Label ID="lblCurrency" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Exch Rate
                                </td>
                                <td>
                                    <asp:Label ID="lblExRate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Freight
                                </td>
                                <td>
                                    <asp:Label ID="lblFrie" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Insurence
                                </td>
                                <td>
                                    <asp:Label ID="lblIns" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Charges
                                </td>
                                <td>
                                    <asp:Label ID="lblAgen" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Miscellaneous
                                </td>
                                <td>
                                    <asp:Label ID="lblMisc" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Addl Chrg
                                </td>
                                <td>
                                    <asp:Label ID="lblAdlChrg" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Terms
                                </td>
                                <td>
                                    <asp:Label ID="lblTerms" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Product
                                </td>
                                <td>
                                    <asp:Label ID="lblProduct" runat="server" CssClass="arealaber1a" Width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Mode
                                </td>
                                <td>
                                    <asp:Label ID="lblMode" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Custom
                                </td>
                                <td>
                                    <asp:Label ID="lblCustom" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    No of Product
                                </td>
                                <td>
                                    <asp:Label ID="lblNoofProduct" runat="server" Style="font-weight: 700"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Assable Value
                                </td>
                                <td>
                                    <asp:Label ID="lblAssableValue" runat="server" Style="font-weight: 700"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="300">
                                    Duty Amount
                                </td>
                                <td>
                                    <asp:Label ID="lblDutyAmount" runat="server" Style="font-weight: 700"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" colspan="2">
                                    <asp:Label ID="lblmsg" runat="server" Style="font-weight: 700"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="Back To Invoice"
                                        Width="225px" CssClass="stylebutton" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <%--<asp:MultiView ID="MultiView1" runat="server">
							<asp:View ID="View1" runat="server">--%>
                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <table width="850">
                                <%--<tr>
											<td bgcolor="#0066FF" colspan="4" style="text-align: center">
												<asp:Label ID="Label38" runat="server" Text="Main"></asp:Label>
											</td>
										</tr>--%>
                                <%-- <tr>
											<td>
												<asp:Label ID="blitcloc" runat="server" CssClass="fontsize" Text="ITC Location"></asp:Label>
											</td>
											<td>
												<asp:TextBox ID="txtitcloc" runat="server" CssClass="textbox300"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>
												<asp:Label ID="lblitchscode" runat="server" CssClass="fontsize" 
													Text="ITCHSCode"></asp:Label>
											</td>
											<td>
												<asp:TextBox ID="txtitchscode" runat="server" CssClass="textbox150"></asp:TextBox>
											</td>
										</tr>--%>
                                <tr>
                                    <td bgcolor="#0066FF" colspan="5" style="text-align: center">
                                        <asp:Label ID="Label42" runat="server" Text="Generic Description"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="120">
                                        <asp:Label ID="lblsadnotn0" runat="server" CssClass="fontsize" Text="Generic Desc"></asp:Label>
                                    </td>
                                    <td width="180">
                                        <asp:TextBox ID="txtgenericdesc" runat="server" CssClass="textboxHeight29"></asp:TextBox>
                                    </td>
                                    <td width="90">
                                        <asp:Label ID="lblsadnotn6" runat="server" CssClass="fontsize" Text="Accessories"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtaccessories" runat="server" CssClass="textboxHeight29"></asp:TextBox>
                                    </td>
                                    <td rowspan="3">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style3" colspan="2">
                                                    Alternative UQC Details
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblqty0" runat="server" CssClass="style3" Text="Qty"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAltqty" runat="server" CssClass="textbox100">0.00</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblunit0" runat="server" CssClass="style3" Text="Unit"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAltUnit" runat="server" AppendDataBoundItems="True" CssClass="ddl100">
                                                        <asp:ListItem Value="0">~Select~</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="nitprice0" runat="server" CssClass="style3" Text="Unit Price" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAltunitprice" runat="server" AutoPostBack="True" CssClass="textbox100"
                                                        OnTextChanged="txtunitprice_TextChanged" Visible="False">0.00</asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1" width="120">
                                        <asp:Label ID="lblsadnotn1" runat="server" CssClass="fontsize" Text="Manufacturer"></asp:Label>
                                    </td>
                                    <td class="style1" width="180">
                                        <asp:TextBox ID="txtmanufacturer" runat="server" CssClass="textbox150"></asp:TextBox>
                                    </td>
                                    <td class="style1" width="90">
                                        <asp:Label ID="lblsadnotn4" runat="server" CssClass="fontsize" Text="End Use"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        &nbsp;<asp:TextBox ID="endcase" runat="server" CssClass="textbox150"></asp:TextBox>
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
                                        <asp:Label ID="lblsadnotn5" runat="server" CssClass="fontsize" Text="Country of Origin"
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlcountryorigin" runat="server" CssClass="ddl150" AppendDataBoundItems="True">
                                            <asp:ListItem Value="~Select~">~Select~</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="120">
                                        <asp:Label ID="lblsadnotn3" runat="server" CssClass="fontsize" Text="Model"></asp:Label>
                                    </td>
                                    <td width="180">
                                        <asp:TextBox ID="txtmodel" runat="server" CssClass="textbox150"></asp:TextBox>
                                    </td>
                                    <td colspan="3" style="text-align: right">
                                        <asp:Button ID="btnsaveGeneric" runat="server" CssClass="stylebutton" Height="21px"
                                            OnClick="btnsaveGeneric_Click" Text="Save" Width="70px" />
                                    </td>
                                </tr>
                                <%--<tr>
											<td>
												<asp:Label ID="lblpolicy0" runat="server" CssClass="fontsize" Text="Loading"></asp:Label>
											</td>
											<td>
												<asp:TextBox ID="txtLoadOn" runat="server" CssClass="textbox50"></asp:TextBox>
												<asp:Label ID="lblpolicy1" runat="server" CssClass="fontsize" Text="On"></asp:Label>
												<asp:TextBox ID="txtLoadTerm" runat="server" CssClass="textbox50"></asp:TextBox>
												<asp:TextBox ID="txtLoadRate" runat="server" CssClass="textbox50"></asp:TextBox>
												<asp:Label ID="lblLoadingUnit" runat="server" CssClass="fontsize"></asp:Label>
												<asp:TextBox ID="txtLoadAmount" runat="server" CssClass="textbox50"></asp:TextBox>
												<asp:Label ID="lblLoadingCur" runat="server" CssClass="fontsize"></asp:Label>
											</td>
										</tr>
								 <tr>
										<td colspan="2" style="text-align: center">
											&nbsp;
											<asp:Button ID="btnsave" runat="server" BackColor="#73AAE8" Height="26px" OnClick="btnsave_Click"
												Text="Save" Width="70px" />
											<asp:Button ID="btnExit" runat="server" BackColor="#73AAE8" OnClick="btnExit_Click"
												Text="Exit" Width="70px" />
										</td>
									</tr>--%>
                            </table>
                        </asp:Panel>
                        <%-- </asp:View>
							<asp:View ID="View2" runat="server">--%>
                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                            <table width="850">
                                <tr>
                                    <td bgcolor="#0066FF" colspan="5" style="text-align: center">
                                        <asp:Label ID="Label39" runat="server" Text="Duty Calculation"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160" colspan="1">
                                        <asp:Label ID="Label9" runat="server" CssClass="fontsize" Text="EXIM Scheme Code"></asp:Label>
                                    </td>
                                    <td width="75px" colspan="1">
                                        <asp:TextBox ID="txtEXIM" runat="server" CssClass="textbox100" OnChange="SchemeName();"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" CompletionListCssClass="completionListExim"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetEXIMCode" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtEXIM">
                                        </cc1:AutoCompleteExtender>
                                        <%-- <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtEXIM"
								WatermarkCssClass="waterText" WatermarkText="SchemeCode" runat="server">
							</cc1:TextBoxWatermarkExtender>--%>
                                    </td>
                                    <td width="60" colspan="3">
                                        <asp:TextBox ID="txtEximSchemeDesc" runat="server" CssClass="textbox400"></asp:TextBox>
                                        <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" CompletionListCssClass="completionList"
											CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
											EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetEXIMCode" ServicePath="~/AutoComplete.asmx"
											TargetControlID="txtEximSchemeDesc">
										</cc1:AutoCompleteExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160" colspan="1">
                                        <asp:Label ID="Label10" runat="server" CssClass="fontsize" Text="Scheme Noten"></asp:Label>
                                    </td>
                                    <td width="75px" colspan="1">
                                        <asp:TextBox ID="txtSchemeNotn" runat="server" CssClass="textbox100" OnChange="SchemeNotn();"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" CompletionListCssClass="completionListExim"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetSchemeCode" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtSchemeNotn">
                                        </cc1:AutoCompleteExtender>
                                        <%--  <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtSchemeNotn"
											 WatermarkCssClass="waterText" WatermarkText="SchemeName" runat="server">
										 </cc1:TextBoxWatermarkExtender>--%>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtSchemeUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtSchemeDesc" runat="server" CssClass="textbox300"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="Label11" runat="server" CssClass="fontsize" Text="CTH NO"></asp:Label>
                                    </td>
                                    <td width="75px" colspan="1">
                                        <%-- <cc1:AutoCompleteExtender ID="txtCTH_AutoCompleteExtender" runat="server" 
											TargetControlID="txtCTH">
										</cc1:AutoCompleteExtender>--%>
                                        <asp:TextBox ID="txtCTH" runat="server" AutoPostBack="True" CssClass="textbox100"
                                            OnTextChanged="txtCTH_TextChanged"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtCTH_AutoCompleteExtender" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetRITCCode" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtCTH">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td class="style8" colspan="3">
                                        <asp:Label ID="lblcetno" runat="server" CssClass="fontsize" Text="CET No"></asp:Label>
                                        <asp:TextBox ID="txtCETNo" runat="server" AutoPostBack="True" CssClass="textbox75"
                                            OnTextChanged="txtCETNo_TextChanged"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetRITCCode" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtCETNo">
                                        </cc1:AutoCompleteExtender>
                                        <%--<cc1:AutoCompleteExtender ID="txtRITC_AutoCompleteExtender" runat="server" 
											TargetControlID="txtRITC">
										</cc1:AutoCompleteExtender>--%>
                                        <asp:Label ID="Label20" runat="server" CssClass="fontsize" Text="Rate Type"></asp:Label>
                                        :<asp:DropDownList ID="ddlRateType" runat="server" CssClass="fontsize" Width="150px">
                                            <asp:ListItem Value="S">Standard</asp:ListItem>
                                            <asp:ListItem Value="P">Preferential</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="Label12" runat="server" CssClass="fontsize" Text="BCD/Notn"></asp:Label>
                                    </td>
                                    <td width="75px" colspan="1">
                                        <asp:TextBox ID="txtBasicDutyNotn" runat="server" CssClass="textbox100" OnChange="DutyNotn('ContentPlaceHolder1_txtBasicDutyNotn','ContentPlaceHolder1_txtBasicDutySno','ContentPlaceHolder1_txtBasicDutyRate');"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionListCode"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetNotification"
                                            ServicePath="~/AutoComplete.asmx" TargetControlID="txtBasicDutyNotn">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td class="fontsize" colspan="3">
                                        <asp:TextBox ID="txtBasicDutySno" runat="server" CssClass="textbox75" ToolTip="BCD S.No"></asp:TextBox>
                                        <asp:TextBox ID="txtBasicDutyRate" runat="server" CssClass="textbox75" OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                        <span class="fontsize">%<asp:DropDownList ID="ddlBasicDutyFlag" runat="server" CssClass="fontsize">
                                            <asp:ListItem>Plus</asp:ListItem>
                                            <asp:ListItem>Minus</asp:ListItem>
                                            <asp:ListItem>Higher</asp:ListItem>
                                            <asp:ListItem>Lower</asp:ListItem>
                                        </asp:DropDownList>
                                        </span>Rs<asp:TextBox ID="txtBasicDutyAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        /
                                        <asp:TextBox ID="txtBasicDutyUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="UOMList" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtBasicDutyUnit">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                       <tr>
                                    <td width="160">
                                        <asp:Label ID="Label13" runat="server" CssClass="fontsize" Text="Education Cess-"></asp:Label>
                                    </td>
                                    <td width="75px" colspan="1">
                                        <asp:TextBox ID="txtEducessNotn" runat="server" CssClass="textbox100" OnChange="DutyNotn('ContentPlaceHolder1_txtEducessNotn','ContentPlaceHolder1_txtEduCessSNo','ContentPlaceHolder1_txtEducessRate');"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionListCssClass="completionListCode"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetNotification"
                                            ServicePath="~/AutoComplete.asmx" TargetControlID="txtEducessNotn">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtEduCessSNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtEducessRate" runat="server" CssClass="textbox50" OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                        <span class="style6">%
                                            <asp:Label ID="Label22" runat="server" CssClass="fontsize" Text="SHE.Cess"></asp:Label>
                                        </span>&nbsp;<asp:TextBox ID="txtSHECessNotn" runat="server" CssClass="textbox100"></asp:TextBox>
                                        <asp:TextBox ID="txtSHECessSNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtSHECessRate" runat="server" CssClass="textbox50">0</asp:TextBox>
                                        <span class="style6">%</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="lbladdlduty" runat="server" CssClass="fontsize" Text="CVD/Notn"></asp:Label>
                                    </td>
                                    <td colspan="1">
                                        <asp:TextBox ID="txtAddlExNotn" runat="server" CssClass="textbox100" OnChange="DutyNotn('ContentPlaceHolder1_txtAddlExNotn','ContentPlaceHolder1_txtAddlExSlNo','ContentPlaceHolder1_txtAddlExRate');"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" CompletionListCssClass="completionListCode"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetNotification"
                                            ServicePath="~/AutoComplete.asmx" TargetControlID="txtAddlExNotn">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td class="fontsize" colspan="3">
                                        <asp:TextBox ID="txtAddlExSlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtAddlExRate" runat="server" CssClass="textbox75" OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                        <span class="style6">%<span class="fontsize"><asp:DropDownList ID="ddlAddlExFlag"
                                            runat="server" CssClass="fontsize">
                                            <asp:ListItem>Plus</asp:ListItem>
                                            <asp:ListItem>Minus</asp:ListItem>
                                            <asp:ListItem>Higher</asp:ListItem>
                                            <asp:ListItem>Lower</asp:ListItem>
                                        </asp:DropDownList>
                                        </span></span>Rs<asp:TextBox ID="txtAddlExAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        /<asp:TextBox ID="txtAddlExUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender10" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="UOMList" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtAddlExUnit">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                     <tr>
                                    <td width="160">
                                        <asp:Label ID="Label25" runat="server" CssClass="fontsize" Text="Education Cess-"></asp:Label>
                                    </td>
                                    <td width="75px" colspan="1">
                                        <asp:TextBox ID="txtExEduCessNotn" runat="server" CssClass="textbox100" 
                                            OnChange="DutyNotn('ContentPlaceHolder1_txtExEduCessNotn','ContentPlaceHolder1_txtExEduCessslNo','ContentPlaceHolder1_txtExEduCessRate');"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender21" runat="server" CompletionListCssClass="completionListCode"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetNotification"
                                            ServicePath="~/AutoComplete.asmx" TargetControlID="txtExEduCessNotn">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtExEduCessslNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <span class="style6">
                                        <asp:TextBox ID="txtExEduCessRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        %
                                            <asp:Label ID="Label26" runat="server" CssClass="fontsize" Text="SHE.Cess"></asp:Label>
                                        </span>&nbsp;<asp:TextBox ID="txtExSHECessRate" runat="server" 
                                            CssClass="textbox75">0</asp:TextBox>
                                        <span class="style6">%</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="lblcetno0" runat="server" CssClass="fontsize" Text="MRP Duty"></asp:Label>
                                    </td>
                                    <td colspan="4" class="fontsize">
                                        <asp:CheckBox ID="chkMRPDuty" runat="server" CssClass="fontsize" Text="Sr No in List" />
                                        <asp:TextBox ID="txtMRPSNo" runat="server" CssClass="textbox50"></asp:TextBox>
                                        <asp:Label ID="lblcetno2" runat="server" CssClass="fontsize" Text="MRP Duty"></asp:Label>
                                        <asp:TextBox ID="txtMRP" runat="server" CssClass="textbox50"></asp:TextBox>
                                        /
                                        <asp:TextBox ID="txtMRPUnit" runat="server" CssClass="textbox50"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender11" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="UOMList" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtMRPUnit">
                                        </cc1:AutoCompleteExtender>
                                        <asp:Label ID="lblcetno3" runat="server" CssClass="fontsize" Text="@Abtt"></asp:Label>
                                        <asp:TextBox ID="txtMRPAbatement" runat="server" CssClass="textbox50"></asp:TextBox>
                                        %
                                    </td>
                                </tr>
                         
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="lblcvd" runat="server" CssClass="fontsize" Text="SAD/Notn"></asp:Label>
                                    </td>
                                    <td colspan="1" width="75px">
                                        <asp:TextBox ID="txtExCVDNotn" runat="server" CssClass="textbox100" OnChange="DutyNotn('ContentPlaceHolder1_txtExCVDNotn','ContentPlaceHolder1_txtExCVDSlNo','ContentPlaceHolder1_txtEXCVDRate');"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" CompletionListCssClass="completionListCode"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetNotification"
                                            ServicePath="~/AutoComplete.asmx" TargetControlID="txtExCVDNotn">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtExCVDSlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtEXCVDRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="lblcessnotn" runat="server" CssClass="fontsize" Text="Cess &amp; Notn"></asp:Label>
                                    </td>
                                    <td colspan="1" width="75px">
                                        <asp:TextBox ID="txtExCessNotn" runat="server" CssClass="textbox100" OnChange="DutyNotn('ContentPlaceHolder1_txtExCessNotn','ContentPlaceHolder1_txtExCessSlNo','ContentPlaceHolder1_txtExCessRate');"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender19" runat="server" CompletionListCssClass="completionListCode"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetNotification"
                                            ServicePath="~/AutoComplete.asmx" TargetControlID="txtExCessNotn">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td class="fontsize" colspan="3">
                                        <asp:TextBox ID="txtExCessSlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtExCessRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        %<span class="style6"><span class="fontsize"><asp:DropDownList ID="ddlExCessFlag"
                                            runat="server" CssClass="fontsize">
                                            <asp:ListItem>Plus</asp:ListItem>
                                            <asp:ListItem>Minus</asp:ListItem>
                                            <asp:ListItem>Higher</asp:ListItem>
                                            <asp:ListItem>Lower</asp:ListItem>
                                        </asp:DropDownList>
                                            Rs<asp:TextBox ID="txtExCessAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                            /<asp:TextBox ID="txtExCessUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender16" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="UOMList" ServicePath="~/AutoComplete.asmx"
                                                TargetControlID="txtExCessUnit">
                                            </cc1:AutoCompleteExtender>
                                        </span></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="Label16" runat="server" CssClass="fontsize" Text="SAPTA Notn"></asp:Label>
                                    </td>
                                    <td colspan="1" width="75px">
                                        <asp:TextBox ID="txtSAPTNotn" runat="server" CssClass="textbox100" OnChange="DutyNotn('ContentPlaceHolder1_txtSAPTNotn','ContentPlaceHolder1_txtSAPTSno','ContentPlaceHolder1_txtSAPTDesc');"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender20" runat="server" CompletionListCssClass="completionListCode"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetSAPTANotification"
                                            ServicePath="~/AutoComplete.asmx" TargetControlID="txtSAPTNotn">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtSAPTSno" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtSAPTDesc" runat="server" CssClass="textbox100" OnKeyPress="javascript:return isFloat(event);">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="lblpolicy" runat="server" CssClass="fontsize" Text="Policy Para"></asp:Label>
                                    </td>
                                    <td colspan="1" width="75px">
                                        <asp:TextBox ID="txtpolicy" runat="server" CssClass="textbox100"></asp:TextBox>
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lblpolicyyear" runat="server" CssClass="fontsize" Text="Policy Year"></asp:Label>
                                        <asp:TextBox ID="txtpyear" runat="server" CssClass="textbox100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="lblpolicy2" runat="server" CssClass="fontsize" Text="PO No"></asp:Label>
                                    </td>
                                    <td colspan="1" width="75px">
                                        <asp:TextBox ID="txtPONo" runat="server" CssClass="textbox100" ToolTip="PO No"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblpolicyyear0" runat="server" CssClass="fontsize" Text="PO Date"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txtPODate" runat="server" CssClass="textbox100"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtPODate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtPODate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblpolicyyear1" runat="server" CssClass="fontsize" 
                                            Text="PO SL. No"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPOSLNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtPOSLNo_CalendarExtender" runat="server" 
                                            Format="dd/MM/yyyy" TargetControlID="txtPOSLNo">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>

                                <tr>
                                    <td width="160">
                                        <asp:Label ID="Label29" runat="server" CssClass="fontsize" Text="Addl Duty of Excice(GSI)"></asp:Label>
                                    </td>
                                    <td width="75px">
                                        <asp:TextBox ID="txtExGSIAddlDutyNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                                    </td>
                                    <td class="fontsize" colspan="3">
                                        <asp:TextBox ID="txtExGSIAddlDutySlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtExGSIAddlDutyRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        <span class="style6"><span class="fontsize">
                                            <asp:DropDownList ID="ddlExGSIAddlDutyFlag" runat="server" CssClass="fontsize">
                                                <asp:ListItem>Plus</asp:ListItem>
                                                <asp:ListItem>Minus</asp:ListItem>
                                                <asp:ListItem>Higher</asp:ListItem>
                                                <asp:ListItem>Lower</asp:ListItem>
                                            </asp:DropDownList>
                                        </span></span>Rs<asp:TextBox ID="txtExGSIAddlDutyAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        /<asp:TextBox ID="txtExGSIAddlDutyUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender12" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="UOMList" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtExGSIAddlDutyUnit">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="lblsplexcise" runat="server" CssClass="fontsize" Text="Spl.Excise Duty(sched-II)"></asp:Label>
                                    </td>
                                    <td width="75px">
                                        <asp:TextBox ID="txtExSPLExDutyNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                                    </td>
                                    <td class="fontsize" colspan="3">
                                        <asp:TextBox ID="txtExSPLExDutySlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtExSPLExDutyRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        <span class="style6"><span class="fontsize">
                                            <asp:DropDownList ID="ddlExSPLExDutyFlag" runat="server" CssClass="fontsize">
                                                <asp:ListItem>Plus</asp:ListItem>
                                                <asp:ListItem>Minus</asp:ListItem>
                                                <asp:ListItem>Higher</asp:ListItem>
                                                <asp:ListItem>Lower</asp:ListItem>
                                            </asp:DropDownList>
                                        </span></span>Rs<asp:TextBox ID="txtExSPLExDutyAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        /<asp:TextBox ID="txtExSPLExDutyUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender13" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="UOMList" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtExSPLExDutyUnit">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="lbladdlexcise" runat="server" CssClass="fontsize" Text="Addl Excise Duty(TTA)"></asp:Label>
                                    </td>
                                    <td width="75px">
                                        <asp:TextBox ID="txtExTTAAddlDutyNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                                    </td>
                                    <td class="fontsize" colspan="3">
                                        <asp:TextBox ID="txtExTTAAddlDutySlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtExTTAAddlDutyRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        <span class="style6"><span class="fontsize">
                                            <asp:DropDownList ID="ddlExTTAAddlDutyFlag" runat="server" CssClass="fontsize">
                                                <asp:ListItem>Plus</asp:ListItem>
                                                <asp:ListItem>Minus</asp:ListItem>
                                                <asp:ListItem>Higher</asp:ListItem>
                                                <asp:ListItem>Lower</asp:ListItem>
                                            </asp:DropDownList>
                                        </span></span>Rs<asp:TextBox ID="txtExTTAAddlDutyAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        /<asp:TextBox ID="txtExTTAAddlDutyUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender14" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="UOMList" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtExTTAAddlDutyUnit">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="lblhealthcess" runat="server" CssClass="fontsize" Text="Health Cess"></asp:Label>
                                    </td>
                                    <td width="75px">
                                        <asp:TextBox ID="txtExHealthCessNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                                    </td>
                                    <td class="fontsize" colspan="3">
                                        <asp:TextBox ID="txtExHealthCessSlNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtExHealthCessRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        <span class="style6"><span class="fontsize">
                                            <asp:DropDownList ID="ddlExHealthCessFlag" runat="server" CssClass="fontsize">
                                                <asp:ListItem>Plus</asp:ListItem>
                                                <asp:ListItem>Minus</asp:ListItem>
                                                <asp:ListItem>Higher</asp:ListItem>
                                                <asp:ListItem>Lower</asp:ListItem>
                                            </asp:DropDownList>
                                        </span></span>Rs<asp:TextBox ID="txtExHealthCessAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        /<asp:TextBox ID="txtExHealthCessUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender15" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="UOMList" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtExHealthCessUnit">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160">
                                        <asp:Label ID="lblsadnotn" runat="server" CssClass="fontsize" 
                                            Text="SAD Notn. &amp; Duty(Test)" Visible="False"></asp:Label>
                                    </td>
                                    <td width="75px">
                                        <asp:TextBox ID="txtExSADNotn" runat="server" CssClass="textbox75" 
                                            Visible="False"></asp:TextBox>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtExSADSlno" runat="server" CssClass="textbox75" 
                                            Height="16px" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtExSADRate" runat="server" CssClass="textbox75" 
                                            Visible="False">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160" colspan="1">
                                        <asp:Label ID="Label21" runat="server" CssClass="fontsize" Text="Addl Notn" 
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td width="75px" colspan="1">
                                        <asp:TextBox ID="txtAddlNotn" runat="server" CssClass="textbox75" 
                                            Visible="False"></asp:TextBox>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtAddlNotnSno" runat="server" CssClass="textbox75" 
                                            Visible="False"></asp:TextBox>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160" colspan="1">
                                        <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="NCD"></asp:Label>
                                    </td>
                                    <td width="75px" colspan="1">
                                        <asp:TextBox ID="txtNCDNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                                    </td>
                                    <td class="fontsize" colspan="3">
                                        <asp:TextBox ID="txtNCDSNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtNCDRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        %<span class="style6"><span class="fontsize"><asp:DropDownList ID="ddlNCDFlag" runat="server"
                                            CssClass="fontsize">
                                            <asp:ListItem>Plus</asp:ListItem>
                                            <asp:ListItem>Minus</asp:ListItem>
                                            <asp:ListItem>Higher</asp:ListItem>
                                            <asp:ListItem>Lower</asp:ListItem>
                                        </asp:DropDownList>
                                        </span></span>Rs<asp:TextBox ID="txtNCDAmount" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        /<asp:TextBox ID="txtNCDUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender17" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="UOMList" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtNCDUnit">
                                        </cc1:AutoCompleteExtender>
                                        <asp:TextBox ID="txtNCDRule" runat="server" CssClass="textbox75">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160" colspan="1">
                                        <asp:Label ID="Label15" runat="server" CssClass="fontsize" Text="Surcharge &amp; Notn"></asp:Label>
                                    </td>
                                    <td width="75px" colspan="1">
                                        <asp:TextBox ID="txtSurNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                                    </td>
                                    <td class="fontsize" colspan="3">
                                        <asp:TextBox ID="txtSurSno" runat="server" CssClass="textbox75"></asp:TextBox>
                                        <asp:TextBox ID="txtSurRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                        %
                                    </td>
                                </tr>
                                <tr>
                                    <td width="160" colspan="1">
                                        <asp:Label ID="Label17" runat="server" CssClass="fontsize" Text="Tarrif Value Notn"></asp:Label>
                                    </td>
                                    <td width="75px" colspan="1">
                                        <asp:TextBox ID="txtTarrifNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                                    </td>
                                    <td colspan="3">
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
                                    <td colspan="5" style="text-align: right">
                                        <span class="style9">Please Verify the Duty Rate and Save it.</span><asp:Button ID="btnsaveCustom" runat="server" CssClass="stylebutton" Height="21px"
                                            OnClick="btnsaveCustom_Click" Text="Save" Width="70px" />
                                        <asp:Button ID="btnExit0" runat="server" CssClass="stylebutton" Height="21px" Text="Exit"
                                            Width="70px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <%--</asp:View>
							<asp:View ID="View3" runat="server">--%>
                        <asp:Panel ID="Panel3" runat="server" Visible="false">
                            <table width="850">

                                <tr>
                                    <td colspan="3" style="text-align: right">
                                        <asp:Button ID="btnsaveExc" runat="server" OnClick="btnsaveExc_Click" Style="text-align: center"
                                            Text="Save" Width="70px" CssClass="stylebutton" Height="21px" 
                                            Visible="False" />
                                        <asp:Button ID="btnExit1" runat="server" Text="Exit" Width="70px" CssClass="stylebutton"
                                            Height="21px" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <%-- </asp:View>
							<asp:View ID="View4" runat="server">--%>
                        <asp:Panel ID="Panel4" runat="server" Visible="false">
                            <table width="850">
                                <tr>
                                    <td colspan="8" style="text-align: center" bgcolor="#0066FF">
                                        <asp:Label ID="Label41" runat="server" Text="ITC Licence"></asp:Label>
                                    </td>
                                </tr>
                                <table width="850">
                                    <tr>
                                        <td style="text-align: left" width="75px">
                                            <asp:Label ID="Label30" runat="server" CssClass="fontsize" Text="Licence No"></asp:Label>
                                        </td>
                                        <td style="text-align: left" width="75px">
                                            <asp:Label ID="Label31" runat="server" CssClass="fontsize" Text="Date"></asp:Label>
                                        </td>
                                        <td style="text-align: left" width="75px">
                                            <asp:Label ID="Label32" runat="server" CssClass="fontsize" Text="Quantity"></asp:Label>
                                        </td>
                                        <td style="text-align: left" width="75px">
                                            <asp:Label ID="Label33" runat="server" CssClass="fontsize" Text="Debit Value"></asp:Label>
                                        </td>
                                        <td style="text-align: left" width="75px">
                                            <asp:Label ID="Label34" runat="server" CssClass="fontsize" Text="RA Number"></asp:Label>
                                        </td>
                                        <td style="text-align: left" width="75px">
                                            <asp:Label ID="Label35" runat="server" CssClass="fontsize" Text="RA Date"></asp:Label>
                                        </td>
                                        <td style="text-align: left" width="75px">
                                            <asp:Label ID="Label36" runat="server" CssClass="fontsize" Text="Reg. Port"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" width="75px">
                                            <asp:TextBox ID="txtLicenceNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left" width="75px">
                                            <asp:TextBox ID="txtLicenceDate" runat="server" CssClass="textbox75"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLicenceDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td style="text-align: left" width="75px">
                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left" width="75px">
                                            <asp:TextBox ID="txtDebitValue" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left" width="75px">
                                            <asp:TextBox ID="txtRANumber" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left" width="75px">
                                            <asp:TextBox ID="txtRADate" runat="server" CssClass="textbox75"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtRADate">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td style="text-align: left" width="75px">
                                            <asp:TextBox ID="txtRegPort" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Button ID="btnITCLicAdd" runat="server" OnClick="btnITCLicAdd_Click" Text="Add"
                                                CssClass="fontsize" Height="21px" Width="60px" />
                                            <asp:Button ID="btnITCLicUpdate" runat="server" OnClick="btnITCLicUpdate_Click" Text="Update"
                                                Visible="False" CssClass="fontsize" Height="21px" Width="60px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" style="text-align: left">
                                            <asp:GridView ID="gvITCLicDetails" runat="server" AutoGenerateSelectButton="True"
                                                CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvITCLicDetails_SelectedIndexChanged"
                                                Width="800px" Style="font-size: 8pt; font-family: Verdana">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </table>
                        </asp:Panel>
                        <%-- </asp:View>
							<asp:View ID="View5" runat="server">--%>
                        <asp:Panel ID="Panel5" runat="server" Visible="false" Width="800px">
                            <table width="850">
                                <tr>
                                    <td bgcolor="#0066FF" colspan="3" style="text-align: center">
                                        <asp:Label ID="Label43" runat="server" Text="Prev BE &amp; ReImport"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="120">
                                        <asp:Label ID="lblsadnotn8" runat="server" CssClass="fontsize" Text="Previous B/E No"></asp:Label>
                                    </td>
                                    <td width="150">
                                        <asp:TextBox ID="txtPrevBENo" runat="server" CssClass="textbox150"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPrevBEDate" runat="server" CssClass="textbox150"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="120">
                                        <asp:Label ID="lblsadnotn9" runat="server" CssClass="fontsize" Text="Unit Value"></asp:Label>
                                    </td>
                                    <td width="150">
                                        <asp:DropDownList ID="ddlUnitValue" runat="server" CssClass="ddl75">
                                            <asp:ListItem>~Select~</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtUnitRate" runat="server" CssClass="textbox75">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblsadnotn11" runat="server" CssClass="fontsize" Text="Custom House"></asp:Label>
                                        <asp:TextBox ID="txtCustomHouse" runat="server" CssClass="textbox75"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="120">
                                        &nbsp;
                                    </td>
                                    <td colspan="2" width="150">
                                        <asp:Button ID="btnPrevBEDetails" runat="server" OnClick="btnPrevBEDetails_Click"
                                            Text="Save" CssClass="stylebtn6" Height="21px" Width="60px" />
                                        <asp:Button ID="btnPrevBEUpdate" runat="server" OnClick="btnPrevBEUpdate_Click" Text="Update"
                                            Visible="False" Width="60px" CssClass="stylebtn6" Height="21px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="120">
                                        <asp:Label ID="lblsadnotn10" runat="server" CssClass="fontsize" Text="ReImport Details"></asp:Label>
                                    </td>
                                    <td colspan="2" width="150">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 270px" width="120">
                                        <asp:GridView ID="gvPrevBEDetails" runat="server" AutoGenerateSelectButton="True"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvPrevBEDetails_SelectedIndexChanged"
                                            Width="850px" Font-Size="10pt">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Font-Size="10pt" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <%-- </asp:View>
							<asp:View ID="View6" runat="server">--%>
                        <asp:Panel ID="Panel6" runat="server" Visible="false" Width="800px">
                            <table width="850">
                                <tr>
                                    <td bgcolor="#0066FF" colspan="12" style="text-align: center">
                                        &nbsp;
                                        <asp:Label ID="LblScheme" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana"
                                            Text="SCHEME DETAILS"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="fontsize" Text="EDIRegNo"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" CssClass="fontsize" Text="Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" CssClass="fontsize" Text="S.No"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label45" runat="server" CssClass="fontsize" Text="Lic.No"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" CssClass="fontsize" Text="Lic. Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" CssClass="fontsize" Text="Sch. Type"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" CssClass="fontsize" Text="CIFValue"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" CssClass="fontsize" Text="Qty"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label46" runat="server" CssClass="fontsize" Text="Unit"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label47" runat="server" CssClass="fontsize" Text="Value"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label48" runat="server" CssClass="fontsize" Text="RegPort"></asp:Label>
                                    </td>
                                    <td rowspan="2">
                                        <asp:Button ID="btnAddScheme" runat="server" OnClick="btnAddScheme_Click" Text="Add"
                                            CssClass="stylebutton" Height="21px" Width="60px" />
                                        <br />
                                        <asp:Button ID="btnUpdateScheme" runat="server" OnClick="btnUpdateScheme_Click" Text="Update"
                                            Visible="false" CssClass="stylebutton" Height="21px" Width="60px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtEDIRegNo" runat="server" CssClass="textbox100"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender18" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetEDIRegnNo" ServicePath="~/AutoComplete.asmx"
                                            TargetControlID="txtEDIRegNo">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="textbox50"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtItemSnoinLic" runat="server" CssClass="textbox50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSchemeLicNo" runat="server" CssClass="textbox50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSchemeLicDate" runat="server" CssClass="textbox50"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtSchemeLicDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSchemeType" runat="server" CssClass="textbox50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCifValue" runat="server" CssClass="textbox50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtschQty" runat="server" CssClass="textbox50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUnit" runat="server" CssClass="textbox50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtValueDebited" runat="server" CssClass="textbox50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSchemeRegPort" runat="server" CssClass="textbox50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="12">
                                        <div class="d">
                                            <asp:GridView ID="gvScheme" runat="server" AutoGenerateColumns="False" Width="800px"
                                                AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvScheme_SelectedIndexChanged"
                                                CssClass="table-wrapper">
                                                <RowStyle CssClass="table-header light" />
                                                <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" ForeColor="#EE2521" />
                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                <AlternatingRowStyle BackColor="#E7E7FF" />
                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="displaynon"
                                                        ItemStyle-CssClass="displaynon" />
                                                    <asp:BoundField DataField="EDIRegNo" HeaderText="RegNo" />
                                                    <asp:BoundField DataField="EDIDate" HeaderText="Reg Date" />
                                                    <asp:BoundField DataField="ItemSnoinLic" HeaderText="S.No" />
                                                    <asp:BoundField DataField="SchemeLicNo" HeaderText="Lic.No" />
                                                    <asp:BoundField DataField="SchemeLicDate" HeaderText="Lic.Date" />
                                                    <asp:BoundField DataField="SchemeType" HeaderText="Lic.Type" />
                                                    <asp:BoundField DataField="CIFValue" HeaderText="CIFValue" />
                                                    <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                    <asp:BoundField DataField="ValueDebited" HeaderText="Value" />
                                                    <asp:BoundField DataField="RegPort" HeaderText="RegPort" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <%--</asp:View>
						</asp:MultiView>--%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btnUpdateProduct" runat="server" Text="Update Product Master" CssClass="fontsize"
                            Height="21px" OnClick="btnUpdateProduct_Click" Visible="False" />
                        <asp:Button ID="btnSaveAll" runat="server" CssClass="stylebtn6" Height="21px" Text="Save"
                            Visible="False" Width="70px" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSch" />
            <asp:PostBackTrigger ControlID="btnPre" />
        </Triggers>
    </asp:UpdatePanel>
    <br />
</asp:Content>
