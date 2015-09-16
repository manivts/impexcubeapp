<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="efrmProductExport.aspx.cs" Inherits="ImpexCube.efrmProductExport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style10
        {
            width: 118px;
        }
        .style11
        {
        }
        .style13
        {
        }
        .style17
        {
            width: 625px;
        }
        .style19
        {
            width: 225px;
        }
        .style20
        {
        }
        .hiddencol
        {
            display: none;
        }
        .style22
        {
            font-family: Verdana;
            font-size: 8pt;
            font-weight: bold;
        }
        .style23
        {
            height: 280px;
        }
    </style>
    <script type="text/javascript">
        function productvalidate() {
            var invno = document.getElementById("<%= txtDesc.ClientID %>").value;
            if (invno == "") {
                alert('Please Select Product To Update');
                document.getElementById("<%= txtDesc.ClientID %>").focus();
                return false;
            }
            return true;
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0">
                <tr>
                    <td valign="top">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnproduct" runat="server" CssClass="stylebtn6" Text="General" OnClick="btnproduct_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnCessexpduty" runat="server" CssClass="stylebtn6" Text="Cess/Exp.Duty"
                                        OnClick="btnCessexpduty_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnquota" runat="server" CssClass="stylebtn6" Text="Quota" OnClick="btnquota_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAr4det" runat="server" Text="AR4D Details" OnClick="btnAr4det_Click"
                                        CssClass="stylebtn6" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnreexp" runat="server" Text="Re-Export" OnClick="btnreexp_Click"
                                        CssClass="stylebtn6" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnotherdet" runat="server" Text="Other Details" OnClick="btnotherdet_Click"
                                        CssClass="stylebtn6" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnCheckList" runat="server" Text="CheckList" CssClass="stylebtn6"
                                        OnClick="btnCheckList_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <table width="875px" frame="border" style="border-style: groove">                            
                            <tr>
                                <td class="style20" colspan="5">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="type0" runat="server" CssClass="style22" Text="Product Code"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtProductCode" runat="server" CssClass="textbox150" AutoPostBack="True"
                                                    OnTextChanged="txtProductCode_TextChanged"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetProductCode" ServicePath="~/AutoComplete.asmx"
                                                    TargetControlID="txtProductCode">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="type1" runat="server" CssClass="style22" Text="Product Family"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtProductFamily" runat="server" CssClass="textbox150"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Product Name" CssClass="style22"></asp:Label>
                                            </td>
                                            <td colspan="3" width="500px">
                                                <asp:TextBox ID="txtDesc" runat="server" Width="560px" Height="15px" AutoPostBack="True"
                                                    OnTextChanged="txtDesc_TextChanged"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteProduct" runat="server" CompletionListCssClass="completionList"
                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetProductName" ServicePath="~/AutoComplete.asmx"
                                                    TargetControlID="txtDesc">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="850px">
                                        <tr>
                                            <td class="tdcolumn100">
                                                <asp:Label ID="Label3" runat="server" CssClass="style22" Text="RITC Code"></asp:Label>
                                            </td>
                                            <td class="tdcolumn150">
                                                <asp:Label ID="Label4" runat="server" CssClass="style22" Text="Quantity"></asp:Label>
                                            </td>
                                            <td class="tdcolumn150">
                                                <asp:Label ID="Label5" runat="server" CssClass="style22" Text="Unit price"></asp:Label>
                                            </td>
                                            <td class="tdcolumn150">
                                                <asp:Label ID="Label6" runat="server" CssClass="style22" Text="Per"></asp:Label>
                                            </td>
                                            <td class="tdcolumn150">
                                                <asp:Label ID="Label7" runat="server" CssClass="style22" Text="Amount"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdcolumn100">
                                                <asp:TextBox ID="txtRitccode" runat="server" CssClass="textbox75"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                    EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetRITCCode" ServicePath="~/AutoComplete.asmx"
                                                    TargetControlID="txtRitccode">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td class="tdcolumn150">
                                                <asp:TextBox ID="txtQuan" runat="server" CssClass="textbox75" Height="15px"></asp:TextBox>
                                                <asp:DropDownList ID="ddlquan1" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                    <asp:ListItem Selected="True" Value="0">~Select~</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="tdcolumn150">
                                                <asp:TextBox ID="txtunitpric" runat="server" CssClass="textbox75"></asp:TextBox>
                                                <asp:DropDownList ID="ddluirpriz1" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                    <asp:ListItem Selected="True" Value="0">~Select~</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="tdcolumn150">
                                                <asp:TextBox ID="txtper" runat="server" CssClass="textbox75" Height="15px"></asp:TextBox>
                                                <asp:DropDownList ID="ddlper1" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                    <asp:ListItem Selected="True" Value="0">~Select~</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="tdcolumn150">
                                                <asp:TextBox ID="txtamount" runat="server" CssClass="textbox75"></asp:TextBox>
                                                <asp:DropDownList ID="ddlAmount1" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                    <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAddd" runat="server" BackColor="#73AAE8" Height="26px" OnClick="btnAddd_Click"
                                                    Text="Add" Width="50px" />
                                                <asp:Button ID="btnUpdate" runat="server" BackColor="#73AAE8" Height="26px" OnClick="btnUpdate_Click"
                                                    Text="Update" Visible="False" Width="50px" />
                                                <asp:Button ID="btnMainCancel" runat="server" BackColor="#73AAE8" Height="26px" OnClick="btnMainCancel_Click"
                                                    Text="New" Width="50px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <div class="grid_scroll-3">
                                        <asp:GridView ID="gvProductExp" runat="server" CssClass="table-wrapper" AutoGenerateColumns="False"
                                            OnSelectedIndexChanged="gvProductExp_SelectedIndexChanged">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" />
                                                <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Job No" DataField="JobNo"></asp:BoundField>
                                                <asp:BoundField HeaderText="Invoice No" DataField="InvoiceNo"></asp:BoundField>
                                                <asp:BoundField HeaderText="Description" DataField="Description"></asp:BoundField>
                                                <asp:BoundField HeaderText="RITC" DataField="RITCCode"></asp:BoundField>
                                                <asp:BoundField HeaderText="Quatity" DataField="Quantity"></asp:BoundField>
                                                <asp:BoundField HeaderText="Unit Price" DataField="UnitPrice"></asp:BoundField>
                                                <asp:BoundField HeaderText="Amount" DataField="Amount"></asp:BoundField>
                                                <%--<asp:BoundField HeaderText="Exim Cd" DataField="EximCode"></asp:BoundField>--%>
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
                        <asp:Panel ID="View1" runat="server" Visible="false">
                            <table style="border-style: groove" frame="border" width="875px">
                                <tr>
                                    <td width="765px">
                                        <table width="865px">
                                            <tr>
                                                <td style="text-align: center" rowspan="1">
                                                    <asp:Label ID="Label10" runat="server" Text="General"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="865px">
                                                    <table>
                                                        <tr>
                                                            <td class="style17">
                                                                <table style="width: 489px" width="300">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label11" runat="server" CssClass="fontsize" Text="Exim Code"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlExim" runat="server" CssClass="ddl150">
                                                                                <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label12" runat="server" CssClass="fontsize" Text="NFEI Category"></asp:Label>
                                                                            <asp:DropDownList ID="ddlnfeicode" runat="server" CssClass="ddl150">
                                                                                <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="Free Trade Sample" Value="Free Trade Sample"></asp:ListItem>
                                                                                <asp:ListItem Text="" Value="Diplomatic"> Diplomatic </asp:ListItem>
                                                                                <asp:ListItem Text="Warranty Replacement" Value="Warranty Replacement"></asp:ListItem>
                                                                                <asp:ListItem Text="Currency Chest" Value="Currency Chest"> </asp:ListItem>
                                                                                <asp:ListItem Text="Tourist Purchase" Value="Tourist Purchase"></asp:ListItem>
                                                                                <asp:ListItem Text="Defence Goods" Value="Defence Goods"> </asp:ListItem>
                                                                                <asp:ListItem Text="Gift Parcel" Value="Gift Parcel">Gift Parcel</asp:ListItem>
                                                                                <asp:ListItem Text="Others" Value="Others">Others</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style17">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label9" runat="server" CssClass="fontsize" Text="Alternate Quantity"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAlternateQty" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlAlternateQtyUnit" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" Value="0">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style17">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <table>
                                                                                <tr>
                                                                                    <td colspan="6" style="text-align: center">
                                                                                        <asp:Label ID="Label20" runat="server" Text="Present Market Value(PMV)"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="Label13" runat="server" CssClass="fontsize" Text="Currency"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="ddl150">
                                                                                            <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="INR" Value="INR"></asp:ListItem>
                                                                                            <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
                                                                                            <asp:ListItem Text="EURO" Value="EURO"></asp:ListItem>
                                                                                            <asp:ListItem Text="Dinar" Value="Dinar"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td colspan="4">
                                                                                        <asp:Label ID="Label14" runat="server" CssClass="fontsize" Text="Calc Method"></asp:Label>
                                                                                        <asp:DropDownList ID="ddlcalcmethd" runat="server" CssClass="ddl100">
                                                                                            <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="%age" Value="%age"></asp:ListItem>
                                                                                            <asp:ListItem Text="Manual" Value="Manual"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <asp:TextBox ID="txtcalcmethd" runat="server" CssClass="textbox100" Height="16px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="Label15" runat="server" CssClass="fontsize" Text="PMV/Unit"></asp:Label>
                                                                                    </td>
                                                                                    <td colspan="2">
                                                                                        <asp:TextBox ID="txtpmvunit" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                                        <asp:DropDownList ID="ddlpmvunit1" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                                                            <asp:ListItem Selected="True" Value="0">~Select~</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label16" runat="server" CssClass="fontsize" Text="Total PMV"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txttotalpmv" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddltotalpmv1" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                                                            <asp:ListItem Selected="True" Value="0">~Select~</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <asp:Label ID="Label17" runat="server" CssClass="fontsize" Text="Reward Item"></asp:Label>
                                                                            <asp:CheckBox ID="chkReward" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label18" runat="server" CssClass="fontsize" Text="STR Code"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtstrcode" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="ImageButton6" runat="server" Height="19px" ImageUrl="~/Content/Images/Search1.png" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="btngeneralsave" runat="server" BackColor="#73AAE8" Height="26px"
                                                                                Text="Save" Width="70px" OnClick="btngeneralsave_Click" OnClientClick="javascript:return productvalidate();" />
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    &nbsp;
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label24" runat="server" CssClass="fontsize" Text="Doc Type"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label25" runat="server" CssClass="fontsize" Text="Description"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label26" runat="server" CssClass="fontsize" Text="Agency Code"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label27" runat="server" CssClass="fontsize" Text="Agency Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label28" runat="server" CssClass="fontsize" Text="Document Name"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddldoctype" runat="server" CssClass="ddl150">
                                                        <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Quota" Value="Quota"></asp:ListItem>
                                                        <asp:ListItem Text="Inspection" Value="Inspection"></asp:ListItem>
                                                        <asp:ListItem Text="Licence" Value="Licence"></asp:ListItem>
                                                        <asp:ListItem Text="Canalized" Value="Canalized"></asp:ListItem>
                                                        <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdescr" runat="server" CssClass="textbox200"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtagencyco" runat="server" CssClass="textbox100"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtagencyname" runat="server" CssClass="textbox100"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDocname" runat="server" CssClass="textbox100"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnaddGenral" runat="server" BackColor="#73AAE8" Height="26px" Text="Add"
                                                        Width="70px" OnClick="btnaddGenral_Click" />
                                                    <asp:Button ID="btnUpdategeneral" runat="server" BackColor="#73AAE8" Height="26px"
                                                        Text="Update" Width="70px" OnClick="btnUpdategeneral_Click" Visible="False" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel4" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="gvprodgen" runat="server" AutoGenerateColumns="False" CssClass="table-wrapperInv"
                                                                        OnSelectedIndexChanged="gvprodgen_SelectedIndexChanged">
                                                                        <Columns>
                                                                            <asp:CommandField ShowSelectButton="True" />
                                                                            <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                                            <asp:BoundField DataField="DocType" HeaderText="DocType" />
                                                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                                                            <asp:BoundField DataField="AgencyCode" HeaderText="AgencyCode" />
                                                                            <asp:BoundField DataField="AgencyName" HeaderText="AgencyName" />
                                                                            <asp:BoundField DataField="DocumentName" HeaderText="DocumentName" />
                                                                        </Columns>
                                                                        <RowStyle CssClass="table-header light" />
                                                                        <HeaderStyle BackColor="#E7E7FF" CssClass="table-row" />
                                                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                        <AlternatingRowStyle BackColor="#E7E7FF" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="View2" runat="server" Visible="false">
                            <table frame="border" style="border-style: groove" width="875px">
                                <tr>
                                    <td class="style23">
                                        <table width="865px" border="0">
                                            <tr>
                                                <td colspan="1" style="text-align: center">
                                                    <asp:Label ID="Label29" runat="server" Text="Cess/Exp.Duty"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="870px">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <table border="0">
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="Label57" runat="server" Text="Cess/Duty" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td class="style10" colspan="6" align="right">
                                                                            <asp:Label ID="Label58" runat="server" Text="Cess/Duty Rate" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td colspan="4" align="center">
                                                                            <asp:Label ID="Label59" runat="server" Text="Tariff Value(T.V)" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td colspan="3" align="center">
                                                                            <asp:Label ID="Label126" runat="server" Text="Qty for Cess/Duty" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="Label125" runat="server" Text="Cess Desc" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="margin-left: 40px">
                                                                            <asp:Label ID="Label30" runat="server" Text="Export Duty" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlExpcessduty" runat="server" CssClass="ddl150">
                                                                                <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (5.00%)" Value="1 (5.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (10.00%)" Value="1 (10.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (15.00%)" Value="1 (15.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (RS.8000.00/MTS)" Value="1 (RS.8000.00/MTS)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (0.00%)" Value="1 (0.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (8.00%)" Value="1 (8.00%)"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="style10">
                                                                            <asp:TextBox ID="txtexpcessdutyrate" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label31" runat="server" Text="% or Rs" Width="55px" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td colspan="9">
                                                                            <asp:TextBox ID="txtexpdutyper0" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                            <asp:Label ID="Label124" runat="server" Text="/"></asp:Label>
                                                                            <asp:DropDownList ID="ddlexpdutyrate0" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtexpqtydutycessuty" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label33" runat="server" Text="Cess" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlcesscessduty" runat="server" CssClass="ddl150">
                                                                                <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (5.00%)" Value="1 (5.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (10.00%)" Value="1 (10.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (15.00%)" Value="1 (15.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (RS.8000.00/MTS)" Value="1 (RS.8000.00/MTS)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (0.00%)" Value="1 (0.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (8.00%)" Value="1 (8.00%)"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="style10">
                                                                            <asp:TextBox ID="txtcessdutyrte" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="Label34" runat="server" Text="% or Rs" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtcessper" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label35" runat="server" Text="/"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlcessrs" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" Value="0">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtcesstariff" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label36" runat="server" Text="/"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlcessvalue" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" Value="0">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtcessqtycessduty" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label37" runat="server" Text="Unit" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtcesscessdesc" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <%-- </td>--%>
                                                                    <%-- </tr>--%>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label38" runat="server" Text="Oth.Duty/Cess" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlothcessduty" runat="server" CssClass="ddl150">
                                                                                <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (5.00%)" Value="1 (5.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (10.00%)" Value="1 (10.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (15.00%)" Value="1 (15.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (RS.8000.00/MTS)" Value="1 (RS.8000.00/MTS)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (0.00%)" Value="1 (0.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (8.00%)" Value="1 (8.00%)"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="style10">
                                                                            <asp:TextBox ID="txtothcessdutyrate" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="Label39" runat="server" Text="% or Rs" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtothdutyper" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label40" runat="server" Text="/"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlothdutyrs" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" Value="0">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td colspan="5">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtothqtyforcess1" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtothcessdesc1" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label41" runat="server" Text="Third Cess" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlthirdcess" runat="server" CssClass="ddl150">
                                                                                <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (5.00%)" Value="1 (5.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (10.00%)" Value="1 (10.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (15.00%)" Value="1 (15.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (RS.8000.00/MTS)" Value="1 (RS.8000.00/MTS)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (0.00%)" Value="1 (0.00%)"></asp:ListItem>
                                                                                <asp:ListItem Text="1 (8.00%)" Value="1 (8.00%)"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="style10">
                                                                            <asp:TextBox ID="oththirdcessdutyrate" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="Label42" runat="server" Text="% or Rs" CssClass="fontsize"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtthirdcessper" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label45" runat="server" Text="/"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlthirdcessrs" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" Value="0">~Select~</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td colspan="5">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtthirdqtyforcess1" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtthirdcessdesc0" runat="server" CssClass="textbox50">0</asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="865px" border="0">
                                            <tr>
                                                <td style="text-align: center" colspan="6">
                                                    <asp:Label ID="Label2" runat="server" Text="CENVAT Details"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Certificate Number" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcertnum" runat="server" CssClass="textbox150"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label47" runat="server" Text="Date" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcenvatdate" runat="server" CssClass="textbox100"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtcenvatdate"
                                                        Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label48" runat="server" Text="Valid UpTo" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtvalidupto" runat="server" CssClass="textbox150"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtvalidupto"
                                                        Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label49" runat="server" Text="CEX Office Code" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcexofccode" runat="server" CssClass="textbox150"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label50" runat="server" Text="Assessee Code" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAssessee" runat="server" CssClass="textbox150"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="6">
                                                    <asp:Button ID="btncesssav" runat="server" Text="Save" BackColor="#73AAE8" Height="26px"
                                                        Width="70px" OnClick="btncesssav_Click" OnClientClick="javascript:return productvalidate();" />
                                                    <asp:Button ID="btncessCancel" runat="server" Text="Cancel" BackColor="#73AAE8" Height="26px"
                                                        Width="70px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="View3" runat="server" Visible="false">
                            <table width="875px" frame="border" style="border-style: groove">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="Label62" runat="server" Text="Quota"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    &nbsp;
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label64" runat="server" Text="Quota Certificate No" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label65" runat="server" Text="Agency" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label66" runat="server" Text="Expiry Date" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label67" runat="server" Text="Quantity" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label68" runat="server" Text="Unit" CssClass="fontsize"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtquotacert" runat="server" CssClass="textbox100"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlagencyq" runat="server" CssClass="ddl150">
                                                        <asp:ListItem Text="~Select~" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="AEPC" Value="AEPC"></asp:ListItem>
                                                        <asp:ListItem Text="AIHB" Value="AIHB"></asp:ListItem>
                                                        <asp:ListItem Text="APEDA" Value="APEDA"></asp:ListItem>
                                                        <asp:ListItem Text="ASI" Value="ASI"></asp:ListItem>
                                                        <asp:ListItem Text="CEPC" Value="CEPC"></asp:ListItem>
                                                        <asp:ListItem Text="CLRI" Value="CLRI"></asp:ListItem>
                                                        <asp:ListItem Text="COFFEE" Value="COFFEE"></asp:ListItem>
                                                        <asp:ListItem Text="CTEPC" Value="CTEPC"></asp:ListItem>
                                                        <asp:ListItem Text="HEPC" Value="HEPC"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem>
                                                        <asp:ListItem Text="SRTEPC" Value="SRTEPC"></asp:ListItem>
                                                        <asp:ListItem Text="WIB" Value="WIB"></asp:ListItem>
                                                        <asp:ListItem Text="WWEPC" Value="WWEPC"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtexpdateq" runat="server" CssClass="textbox100"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="cclExdate" TargetControlID="txtexpdateq" runat="server"
                                                        Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtquantityq" runat="server" CssClass="textbox100"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlunitq" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                        <asp:ListItem Selected="True" Value="0">~Select~</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnaddq" runat="server" Text="Add" BackColor="#73AAE8" Height="26px"
                                                        Width="70px" OnClick="btnaddq_Click" />
                                                    <asp:Button ID="btnupdateq" runat="server" Text="Update" BackColor="#73AAE8" Height="26px"
                                                        Width="70px" OnClick="btnupdateq_Click" Visible="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="9">
                                                    <div class="grid_scroll-2">
                                                        <asp:GridView ID="gvproductquota" runat="server" CssClass="table-wrapperInv" AutoGenerateColumns="False"
                                                            OnSelectedIndexChanged="gvproductquota_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:CommandField ShowSelectButton="True" />
                                                                <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                                <asp:BoundField HeaderText="Quota Certificate No" DataField="QuotaCertificateNo">
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Agency" DataField="Agency"></asp:BoundField>
                                                                <asp:BoundField HeaderText="ExpiryDate" DataField="ExpiryDate"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Quantity" DataField="Quantity"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Unit" DataField="Unit"></asp:BoundField>
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
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="View4" runat="server" Visible="false">
                            <table width="875px" frame="border" style="border-style: groove">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="Label74" runat="server" Text="AR4 Details"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="Label75" runat="server" Text="AR4 No" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label76" runat="server" Text="AR4 Date" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label77" runat="server" Text="CommissioneRate" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label78" runat="server" Text="Division" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label79" runat="server" Text="Range" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label80" runat="server" Text="Remarks" CssClass="fontsize"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtar4no" runat="server" CssClass="textbox100"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtar4date" runat="server" CssClass="textbox75"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="txtar4date" runat="server"
                                                        Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcommisionrate" runat="server" Height="15px" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdivision" runat="server" Height="15px" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtrange" runat="server" Height="15px" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtremarkss" runat="server" CssClass="textd" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAddar4" runat="server" Text="Add" BackColor="#73AAE8" Height="26px"
                                                        Width="70px" OnClick="btnAddar4_Click" />
                                                    <asp:Button ID="btnUpdAr4" runat="server" Text="Update" BackColor="#73AAE8" Height="26px"
                                                        Width="70px" OnClick="btnUpdAr4_Click" Visible="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="10">
                                                    <div class="grid_scroll-2">
                                                        <asp:GridView ID="gvAr4Details" runat="server" CssClass="table-wrapperInv" AutoGenerateColumns="False"
                                                            OnSelectedIndexChanged="gvAr4Details_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:CommandField ShowSelectButton="True" />
                                                                <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                                <asp:BoundField HeaderText="Job No" DataField="JobNo"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Invoice No" DataField="InvoiceNo"></asp:BoundField>
                                                                <asp:BoundField HeaderText="AR4.No" DataField="AR4No"></asp:BoundField>
                                                                <asp:BoundField HeaderText="CommissioneRate" DataField="Commissionerate"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Division" DataField="Division"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Range" DataField="Range"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Remarks" DataField="Remark"></asp:BoundField>
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
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="View5" runat="server" Visible="false">
                            <table width="875px" frame="border" style="border-style: groove">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="Label86" runat="server" Text="Re-Export"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel13" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label87" runat="server" Text="B/E Number" CssClass="fontsize"></asp:Label>
                                                                </td>
                                                                <td colspan="3" class="style19">
                                                                    <asp:TextBox ID="txtbenumber1" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                    <asp:TextBox ID="txtbenumdate" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender4" TargetControlID="txtbenumdate" runat="server"
                                                                        Format="dd/MM/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                    &nbsp; &nbsp;<asp:Button ID="btninit0" runat="server" BackColor="#73AAE8" Height="26px" Text="Init"
                                                                        Width="60px" Visible="False" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label131" runat="server" CssClass="fontsize" Text="Quantity Exported"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtquanexp3" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddlquanexp2" runat="server" AppendDataBoundItems="True" CssClass="ddl50">
                                                                        <asp:ListItem Selected="True" Value="0">~Select~</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label89" runat="server" CssClass="fontsize" Text="Invoice SNo"></asp:Label>
                                                                </td>
                                                                <td class="style19" colspan="3">
                                                                    <asp:TextBox ID="txtinvoicesno" runat="server" CssClass="textbox50"></asp:TextBox>
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    <asp:Label ID="Label90" runat="server" CssClass="fontsize" Text="Item SNo"></asp:Label>
                                                                    &nbsp;
                                                                    <asp:TextBox ID="txttemsno" runat="server" CssClass="textbox50"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label129" runat="server" CssClass="fontsize" Text="Technical Details"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txttechnicaldet" runat="server" CssClass="textd" TextMode="MultiLine"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label92" runat="server" Text="Import Port Code" CssClass="fontsize"></asp:Label>
                                                                </td>
                                                                <td align="left" colspan="2">
                                                                    <asp:TextBox ID="txtimportportcode" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="Label128" runat="server" Text="Other Identifying Paramters" CssClass="fontsize"></asp:Label>
                                                                </td>
                                                                <td class="style13">
                                                                    <asp:TextBox ID="txtothIdenParam1" runat="server" CssClass="textd" TextMode="MultiLine"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label93" runat="server" Text="B/E Item Desc" CssClass="fontsize"></asp:Label>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:TextBox ID="txtbeitemdesc" runat="server" CssClass="textdd" TextMode="MultiLine"></asp:TextBox>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:CheckBox ID="chkagaistoblig" runat="server" CssClass="fontsize" Text="Against Export Obligation"
                                                                        Width="180px" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label132" runat="server" Text="Obligation No." CssClass="fontsize"></asp:Label>
                                                                    <asp:TextBox ID="txtOblinum0" runat="server" CssClass="textbox100" Width="98px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td colspan="3" class="style19">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="style13">
                                                                    <asp:Label ID="Label94" runat="server" Text="Drawback Amount Claimed" CssClass="fontsize"></asp:Label>
                                                                </td>
                                                                <td class="style11">
                                                                    <asp:TextBox ID="txtdraAmtClaim" runat="server" Height="15px" Width="158px" OnKeypress="return isNumber(event)"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label95" runat="server" CssClass="fontsize" Text="Quantity Imported"
                                                                        Width="110px"></asp:Label>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:TextBox ID="txtquantityimport3" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddlquantityimport4" runat="server" CssClass="ddl50" AppendDataBoundItems="True">
                                                                        <asp:ListItem Selected="True">~Select~</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:CheckBox ID="chkitemunused" runat="server" CssClass="fontsize" Text="Item Un-Used" />
                                                                </td>
                                                                <td class="style11">
                                                                    <asp:CheckBox ID="chkcommpermisn" runat="server" CssClass="fontsize" Text="Commissioner Permission" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:Label ID="Label134" runat="server" CssClass="fontsize" Text="Assessable Value"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtassessibleval1" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                </td>
                                                                <td align="left" colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Label ID="Label133" runat="server" CssClass="fontsize" Text="Board Number"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtboardnum1" runat="server" CssClass="textbox100" Width="98px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtboarddate1" runat="server" CssClass="textbox75"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender5" TargetControlID="txtboarddate1" runat="server"
                                                                        Format="dd/MM/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label104" runat="server" Text="Total Duty Paid" CssClass="fontsize"></asp:Label>
                                                                </td>
                                                                <td colspan="3" class="style19">
                                                                    <asp:TextBox ID="txttotaldutypaid" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                    <asp:TextBox ID="txttotdutaiddate" runat="server" CssClass="textbox100"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender6" TargetControlID="txttotdutaiddate" runat="server"
                                                                        Format="dd/MM/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkmodvatavail" runat="server" CssClass="fontsize" Text="MODVAT Availed" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkmodvatreserved" runat="server" CssClass="fontsize" Text="MODVAT Reserved" />
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="right">
                                                                    <asp:Button ID="btnrexpsave" runat="server" Text="Save" BackColor="#73AAE8" Height="26px"
                                                                        Width="70px" OnClick="btnrexpsave_Click" OnClientClick="javascript:return productvalidate();" />
                                                                    <asp:Button ID="btnrexpCancel" runat="server" Text="Cancel" BackColor="#73AAE8" Height="26px"
                                                                        Width="70px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="6">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="View6" runat="server" Visible="false">
                            <table width="875px" frame="border" style="border-style: groove">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="Label113" runat="server" Text="Other Details"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="Label19" runat="server" Text="Accessories If Any" CssClass="fontsize"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtotherRemarks" runat="server" CssClass="textd" TextMode="MultiLine"
                                                        Width="459px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:CheckBox ID="chkthirdparty" runat="server" CssClass="fontsize" Text="Third Party Export" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label115" runat="server" Text="Manufacturer" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtManufacture" runat="server" CssClass="textbox400"></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton17" runat="server" Height="19px" ImageUrl="~/Content/Images/Search1.png"
                                                        OnClientClick=" popupwindow('frmPopUpConsigner.aspx?mode=Product');" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label116" runat="server" Text="IE Code" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtotheriecode" runat="server" CssClass="textbox200"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label117" runat="server" Text="Branch SNo" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtothBranchsno" runat="server" CssClass="textbox100"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label118" runat="server" Text="Address" CssClass="fontsize"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtotheraddress" runat="server" CssClass="textdd" TextMode="MultiLine"
                                                        Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td colspan="2" style="text-align: right">
                                                    <asp:Button ID="btnothsave" runat="server" BackColor="#73AAE8" Height="26px" OnClick="btnothsave_Click"
                                                        OnClientClick="javascript:return productvalidate();" Text="Save" Width="70px" />
                                                    <asp:Button ID="btnothCancel" runat="server" BackColor="#73AAE8" Height="26px" Text="Cancel"
                                                        Width="70px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="right">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td valign="top">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2" align="center">
                                    &nbsp; &nbsp;<strong> &nbsp; Job Details</strong>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Job No
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlJobNo" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        CssClass="ddl100" Height="20px" Width="150px" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged">
                                        <asp:ListItem Value="0">~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Invoice No
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlInvNo" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        CssClass="ddl100" Height="20px" Width="150px" OnSelectedIndexChanged="ddlInvNo_SelectedIndexChanged">
                                        <%--  <asp:ListItem>~Select~</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Job Date
                                </td>
                                <td>
                                    <asp:Label ID="lblJobDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Invoice Date
                                </td>
                                <td>
                                    <asp:Label ID="lblInvDate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Invoice Amount
                                </td>
                                <td>
                                    <asp:Label ID="lblInvAmt" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Currency
                                </td>
                                <td>
                                    <asp:Label ID="lblCurrency" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Exch Rate
                                </td>
                                <td>
                                    <asp:Label ID="lblExRate" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Freight
                                </td>
                                <td>
                                    <asp:Label ID="lblFrie" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Insurance
                                </td>
                                <td>
                                    <asp:Label ID="lblIns" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Agency Charge
                                </td>
                                <td>
                                    <asp:Label ID="lblAgen" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Miscellaneous
                                </td>
                                <td>
                                    <asp:Label ID="lblMisc" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Terms
                                </td>
                                <td>
                                    <asp:Label ID="lblTerms" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Product
                                </td>
                                <td>
                                    <asp:Label ID="lblProduct" runat="server" CssClass="arealaber1a" Width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Mode
                                </td>
                                <td>
                                    <asp:Label ID="lblMode" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Custom
                                </td>
                                <td>
                                    <asp:Label ID="lblCustom" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    Doc Flling Status
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" CssClass="arealaber1a"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" width="300">
                                    No of Products
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoProducts" runat="server" CssClass="textbox75"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="fontsize" colspan="2">
                                    <asp:Label ID="lblmsg" runat="server" Style="font-weight: 700"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnReturn" runat="server" CssClass="stylebutton" Text="Back To Invoice"
                                        OnClick="btnReturn_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <input type="hidden" id="hdnDoc" runat="server" />
            <input type="hidden" id="hdnQuota" runat="server" />
            <input type="hidden" id="hdnAr4" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
