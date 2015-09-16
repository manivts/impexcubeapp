<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmProductMainPage.aspx.cs" Inherits="ImpexCube.frmPRoductMainPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/Styles/Accordion.css" />
    <script type="text/javascript" src="Content/Scripts/Accordion.js"></script>
    <script type="text/javascript" src="Content/Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="Content/Scripts/jquery-ui.js"></script>
    <style type="text/css">
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
    </style>
    <script type="text/javascript">
        function calamt() {
        var uprice=document.getElementById['ContentPlaceHolder1_txtunitprice'].value;
        var qty=document.getElementById['ContentPlaceHolder1_txtqty'].value;
        var exrate = document.getElementById['ContentPlaceHolder1_lblExRate'].value;
        var amt=parseInt(uprice)*parseInt(qty);
        var amtinr=parseInt(amt)*parseInt(exrate);

        document.getElementById['ContentPlaceHolder1_txtamount'] =amt;
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
    </script>
    <%--<asp:Button ID="btnGenDesc" runat="server" CssClass="stylebtn6" OnClick="btnGenDesc_Click"
                                        Text="Generic Desc" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td rowspan="10" valign="top">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <br />
                                    <br />
                                    <asp:Button ID="btnMain" runat="server" CssClass="stylebtn6" OnClick="btnMain_Click"
                                        Text="Generic Desc" />
                                    <br />
                                    <asp:Button ID="btnDuty" runat="server" CssClass="stylebtn6" OnClick="btnDuty_Click"
                                        Text="Duty" />
                                    <%--<asp:Button ID="btnGenDesc" runat="server" CssClass="stylebtn6" OnClick="btnGenDesc_Click"
                                        Text="Generic Desc" />--%>
                                    <%--<br />--%>
                                    <asp:Button ID="btnITC" runat="server" CssClass="displaynon" OnClick="btnITC_Click"
                                        Text="ITC Lic." Visible="False" />
                                    <br />
                                    <asp:Button ID="btnOtherDuty" runat="server" CssClass="stylebtn6" OnClick="btnOtherDuty_Click"
                                        Text="Other Duty" />
                                    <br />
                                    <asp:Button ID="btnSch" runat="server" CssClass="stylebtn6" Text="Scheme" OnClick="btnSch_Click" />
                                   <%-- <br />--%>
                                    <asp:Button ID="btnPre" runat="server" CssClass="displaynon" Text="Prev BE &amp; ReImport"
                                        OnClick="btnPre_Click" Visible="False" />
                                    <br />
                                    <asp:Button ID="btnCheckList" runat="server" CssClass="stylebtn6" OnClick="btnCheckList_Click"
                                        Text="Check List" />
                                    <br />
                                    <asp:Button ID="btnBEFile" runat="server" CssClass="stylebtn6" OnClick="btnBEFile_Click"
                                        Text="BE File" />
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
                        <table width="800">
                            <%--<tr>
                                <td>
                                    <asp:Label ID="lblpro0" runat="server" CssClass="style3" Text="Search Product"></asp:Label>
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtSearchProduct" runat="server" Width="600px" Height="15px"></asp:TextBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="type0" runat="server" CssClass="style3" Text="Product Code"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtProductCode" runat="server" CssClass="textbox150" 
                                        AutoPostBack="True" ontextchanged="txtProductCode_TextChanged"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetProductCode"
                                        ServicePath="~/AutoComplete.asmx" TargetControlID="txtProductCode">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="type1" runat="server" CssClass="style3" Text="Product Family"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtProductFamily" runat="server" CssClass="textbox150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblpro" runat="server" CssClass="style3" Text="Product Name"></asp:Label>
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtpro" runat="server" Width="560px" Height="15px" 
                                        AutoPostBack="True" ontextchanged="txtpro_TextChanged" 
                                        style="font-size: 8pt"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteProduct" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetProductName"
                                        ServicePath="~/AutoComplete.asmx" TargetControlID="txtpro">
                                    </cc1:AutoCompleteExtender>
                                    <%--<asp:Button ID="btnPro" runat="server" Height="20px" OnClick="btnPro_Click" Text="Search" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label18" runat="server" CssClass="style3" Text="RITC NO"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="type" runat="server" CssClass="style3" Text="Type"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblqty" runat="server" CssClass="style3" Text="Qty"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblunit" runat="server" CssClass="style3" Text="Unit"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="nitprice" runat="server" CssClass="style3" Text="Unit Price"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblamount" runat="server" CssClass="style3" Text="Amount"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" CssClass="style3" Text="Amount INR"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnNew" runat="server" CssClass="fontsize" OnClick="btnNew_Click"
                                        Text="New" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtRITC" runat="server" AutoPostBack="True" 
                                        CssClass="textbox75"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetRITCCode" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtRITC">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddltype" runat="server" CssClass="ddl100" Height="17px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtqty" runat="server" CssClass="textbox100"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUnit" runat="server" AppendDataBoundItems="True" CssClass="ddl75">
                                        <asp:ListItem>~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtunitprice" runat="server" AutoPostBack="True" CssClass="textbox75"
                                        OnTextChanged="txtunitprice_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtamount" runat="server" AutoPostBack="True" CssClass="textbox100"
                                        OnTextChanged="txtamount_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtINRAmount" runat="server" CssClass="textbox100" AutoPostBack="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="fontsize" 
                                        onclick="btnUpdate_Click" Text="Update" Visible="False" />
                                    <asp:Button ID="btnadd" runat="server" CssClass="fontsize" OnClick="btnadd_Click"
                                        Text="Add" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="9">
                                    <div ID="divProduct" runat="server" 
                                        style="height: 120px; width: 900px; overflow: auto;">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                            AutoGenerateSelectButton="True" Font-Size="10pt" 
                                            OnRowEditing="GridView1_RowEditing" 
                                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="900px" 
                                            style="font-size: 8pt">
                                            <Columns>
                                                <%--<asp:BoundField DataField="ProductID" HeaderText="Id" ReadOnly="True" 
                                                        Visible="false" />--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" 
                                                            ImageUrl="~/Content/Images/delete.gif" OnClick="btnDelete_Click" Width="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProductID" HeaderText="Id">
                                                <%--     <FooterStyle CssClass="stylenone" />
                                                    <HeaderStyle CssClass="stylenone" />
                                                    <ItemStyle CssClass="stylenone" />--%></asp:BoundField>
                                                <asp:BoundField DataField="ProductDesc" HeaderText="Pro Des" 
                                                    HtmlEncode="False" />
                                                <asp:BoundField DataField="ProType" HeaderText="Type" HtmlEncode="False" />
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <%--                        </div>--%>
                    </td>
                    <td rowspan="10" valign="top">
                        <table style="width: 100%;">
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
                                    <asp:Label ID="lblInvAmt" runat="server" CssClass="arealaber1a"></asp:Label>
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
                                    No of Product</td>
                                <td>
                                    <asp:Label ID="lblNoofProduct" runat="server" style="font-weight: 700"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" colspan="2">
                                    <asp:Label ID="lblmsg" runat="server" Style="font-weight: 700"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnReturn" runat="server" Height="22px" OnClick="btnReturn_Click"
                                        Text="Back To Invoice" Width="225px" />
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
                               
                                <table width="800">
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
                                        <td bgcolor="#0066FF" colspan="4" style="text-align: center">
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
                                                <asp:ListItem>~Select~</asp:ListItem>
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
                                        <td width="90">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            &nbsp;<asp:Button ID="btnsaveGeneric" runat="server" OnClick="btnsaveGeneric_Click"
                                                Text="Save" Width="70px" />
                                            <asp:Button ID="btnExit2" runat="server" Text="Exit" Width="70px" />
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
                                <table width="800">
                                    <tr>
                                        <td bgcolor="#0066FF" colspan="4" style="text-align: center">
                                            <asp:Label ID="Label39" runat="server" Text="Duty Calculation"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120" colspan="1">
                                            <asp:Label ID="Label9" runat="server" CssClass="fontsize" Text="EXIM Scheme Code"></asp:Label>
                                        </td>
                                        <td width="75px" colspan="1">
                                            <asp:TextBox ID="txtEXIM" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                        <td width="60">
                                            <asp:TextBox ID="txtEximSchemeDesc" runat="server" CssClass="textbox300"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120" colspan="1">
                                            <asp:Label ID="Label10" runat="server" CssClass="fontsize" Text="Scheme Noten"></asp:Label>
                                        </td>
                                        <td width="75px" colspan="1">
                                            <asp:TextBox ID="txtSchemeNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSchemeUnit" runat="server" CssClass="textbox75"></asp:TextBox>
                                            <asp:TextBox ID="txtSchemeDesc" runat="server" CssClass="textbox200"></asp:TextBox>
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
                                            <asp:TextBox ID="txtCTH" runat="server" AutoPostBack="True" CssClass="textbox75"
                                                OnTextChanged="txtCTH_TextChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="txtCTH_AutoCompleteExtender" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetRITCCode" ServicePath="~/AutoComplete.asmx"
                                                TargetControlID="txtCTH">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                        <td width="75px" style="width: 275px">
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
                                            <asp:TextBox ID="txtRateType" runat="server" CssClass="textbox75">Standard</asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120">
                                            <asp:Label ID="Label12" runat="server" CssClass="fontsize" Text="Basic Duty/Notn-"></asp:Label>
                                        </td>
                                        <td width="75px" colspan="1">
                                            <asp:TextBox ID="txtBasicDutyNotn" runat="server" CssClass="textbox75"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetBCDNotif" ServicePath="~/AutoComplete.asmx"
                                                TargetControlID="txtBasicDutyNotn">
                                            </cc1:AutoCompleteExtender>
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
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetAddNotif" ServicePath="~/AutoComplete.asmx"
                                                TargetControlID="txtAddlExNotn">
                                            </cc1:AutoCompleteExtender>
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
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetCVDNotif" ServicePath="~/AutoComplete.asmx"
                                                TargetControlID="txtExCVDNotn">
                                            </cc1:AutoCompleteExtender>
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
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetEduNotif" ServicePath="~/AutoComplete.asmx"
                                                TargetControlID="txtEducessNotn">
                                            </cc1:AutoCompleteExtender>
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
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <asp:Button ID="btnsaveCustom" runat="server" OnClick="btnsaveCustom_Click" Text="Save"
                                                Width="70px" />
                                            <asp:Button ID="btnExit0" runat="server" Text="Exit" Width="70px" />
                                        </td>
                                    </tr>
                                </table>
                                </asp:Panel>
                            <%--</asp:View>
                            <asp:View ID="View3" runat="server">--%>
                             <asp:Panel ID="Panel3" runat="server" Visible="false">
                                <table width="800">
                                    <tr>
                                        <td colspan="3" bgcolor="#0066FF" style="text-align: center">
                                            <asp:Label ID="Label40" runat="server" Text="Other  Duty"></asp:Label>
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
                                            <asp:Label ID="Label29" runat="server" CssClass="fontsize" Text="Addl Duty of Excice(GSI)"></asp:Label>
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
                                            &nbsp;
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
                                            <asp:Button ID="btnsaveExc" runat="server" OnClick="btnsaveExc_Click" Style="text-align: center"
                                                Text="Save" Width="70px" />
                                            <asp:Button ID="btnExit1" runat="server" Text="Exit" Width="70px" />
                                        </td>
                                    </tr>
                                </table>
                                </asp:Panel>
                           <%-- </asp:View>
                            <asp:View ID="View4" runat="server">--%>
                             <asp:Panel ID="Panel4" runat="server" Visible="false">
                                <table width="800">
                                    <tr>
                                        <td colspan="8" style="text-align: center" bgcolor="#0066FF">
                                            <asp:Label ID="Label41" runat="server" Text="ITC Licence"></asp:Label>
                                        </td>
                                    </tr>
                                    <table>
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
                                                <asp:Button ID="btnITCLicAdd" runat="server" OnClick="btnITCLicAdd_Click" Text="Add" />
                                                <asp:Button ID="btnITCLicUpdate" runat="server" OnClick="btnITCLicUpdate_Click" Text="Update"
                                                    Visible="False" />
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
                             <asp:Panel ID="Panel5" runat="server" Visible="false">
                                <table width="800">
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
                                                Text="Save" />
                                            <asp:Button ID="btnPrevBEUpdate" runat="server" OnClick="btnPrevBEUpdate_Click" Text="Update"
                                                Visible="False" Width="70px" />
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
                                                Width="800px" Font-Size="10pt">
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
                             <asp:Panel ID="Panel6" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td bgcolor="#0066FF" colspan="12" style="text-align: center">
                                            &nbsp;
                                            <asp:Label ID="Label44" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana"
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
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtEDIRegNo" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="textbox75"></asp:TextBox>
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
                                            <asp:TextBox ID="txtSchemeLicDate" runat="server" CssClass="textbox75"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtSchemeLicDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSchemeType" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCifValue" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtschQty" runat="server" CssClass="textbox50"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="textbox50"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtValueDebited" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSchemeRegPort" runat="server" CssClass="textbox75"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAddScheme" runat="server" OnClick="btnAddScheme_Click" Text="Add" />
                                            <asp:Button ID="btnUpdateScheme" Visible="false" runat="server" Text="Update" OnClick="btnUpdateScheme_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <div class="d">
                                                <asp:GridView ID="gvScheme" runat="server" AutoGenerateColumns="False" CssClass="table-wrapper"
                                                    Width="800px" AutoGenerateSelectButton="True" 
                                                    onselectedindexchanged="gvScheme_SelectedIndexChanged">
                                                    <RowStyle CssClass="table-header light" />
                                                    <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" ForeColor="#EE2521" />
                                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                    <AlternatingRowStyle BackColor="#E7E7FF" />
                                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                    <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="displaynon"
                                                            ItemStyle-CssClass="displaynon" />
                                                        <asp:BoundField DataField="EDIRegNo" HeaderText="RegNo" />
                                                        <asp:BoundField DataField="Date" HeaderText="Reg Date" />
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
                                    <tr>
                                        <td colspan="12">
                                        </td>
                                    </tr>
                                </table>
                                </asp:Panel>
                            <%--</asp:View>
                        </asp:MultiView>--%>
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
