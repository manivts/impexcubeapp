<%@ Page Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="ContraEntry.aspx.cs" Inherits="ImpexCube.Accounts.ContraEntry" Title="Contra Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style11
        {
            width: 120px;
        }
        .style8
        {
            width: 35px;
        }
        .style4
        {
            height: 26px;
        }
        .style10
        {
            height: 26px;
            width: 332px;
        }
        .style16
        {
            font-size: 10pt;
        }
        .Hide
        {
        	display:none;
        }
        .style17
        {
            width: 909px;
        }
    </style>
    <script type="text/javascript">
        function validate() {

            if (document.getElementById('<%=ddlAccountCr.ClientID%>').value == "0") {
                alert("Please select Cr before clicking the save button"); // prompt user
                return false;
                document.getElementById("ddlAccountCr").focus();
                //set focus back to control               
            }
            alert('Do you Want to Save?')
        }
        function ddl2_OnChanged() {
            var ddl1 = document.getElementById('<%=ddlAccountCr.ClientID%>');
            var ddl2 = document.getElementById('<%=ddlAccountDr.ClientID%>');

            var ddl1Value = ddl1.options[ddl1.selectedIndex].text;
            var ddl2Value = ddl2.options[ddl2.selectedIndex].text;

            if (ddl1Value == ddl2Value) {
                alert('The respective term has already been selected in Dr');
                ddl2.selectedIndex = 0;
            }
        }
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image ID="Image1" runat="server" 
                ImageUrl="~/Content/Images/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table align="center" style="width: 76%;" border="1px">
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:Label ID="Label1" runat="server"
                            Text="Contra Entry" CssClass="labeltitle"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style11" style="text-align: right">
                        <asp:Label ID="Label2" runat="server" Style="text-align: left" Text="Voucher No "
                            CssClass="fontsize"></asp:Label>
                    </td>
                    <td class="style17" style="text-align: left">
                        <asp:TextBox ID="txtVchNo" runat="server" ReadOnly="True" 
                            CssClass="textbox140"></asp:TextBox>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="Label3" runat="server" Text="VoucherDate " CssClass="fontsize"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtVchDate" runat="server" CssClass="textbox140"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtVchDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style4" style="text-align: right">
                        <asp:Label ID="Label4" runat="server" Text="Account" CssClass="fontsize"></asp:Label>
                    </td>
                    <td class="style4" colspan="2" style="text-align: left">
                        <asp:DropDownList ID="ddlAccountCr" runat="server" AppendDataBoundItems="True" 
                            onselectedindexchanged="ddlAccountCr_SelectedIndexChanged" 
                            CssClass="ddl150">
                            <asp:ListItem>~Select~</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvAccountCr" runat="server" ControlToValidate="ddlAccountCr"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                    <td class="style10" style="text-align: left">
                        <asp:Label ID="LabelOP" runat="server" CssClass="style16"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style4" colspan="4" style="text-align: left">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: center;" bgcolor="#2461BF">
                                    <asp:Label ID="lblDr" runat="server" Text=" Particulars " CssClass="style16" ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#2461BF">
                                    <asp:Label ID="lblRef" runat="server" Text=" Reference " CssClass="style16" ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#2461BF">
                                    <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No" CssClass="style16" Enabled="False"
                                        ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#2461BF">
                                    <asp:Label ID="lblChequeDate" runat="server" Text="Cheque Date" CssClass="style16"
                                        Enabled="False" ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#2461BF">
                                    <asp:Label ID="lblAmount" runat="server" Text="Amount" CssClass="style16" ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td bgcolor="#2461BF">
                                </td>
                            </tr>
                            <tr style="border-style: ridge;">
                                <td>
                                    <asp:DropDownList ID="ddlAccountDr" runat="server" Font-Size="8pt" Width="250px"
                                        AppendDataBoundItems="True" OnChange="javascript:return ddl2_OnChanged();">
                                        <asp:ListItem>~Select~</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDetails" runat="server" Font-Names="arial"  
                                       CssClass="textbox140"></asp:TextBox>
                                </td>
                                <td ID="textbox140">
                                    <asp:TextBox ID="txtChqNo" runat="server" Font-Names="arial"  
                                        CssClass="textbox140"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtChqDate" runat="server" Font-Names="arial" 
                                        MaxLength="10" CssClass="textbox140"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTEDate" TargetControlID="txtChqDate" FilterType="Numbers,Custom"
                                        ValidChars="01/01/1999" runat="server">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtChqDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtamt1" runat="server" AutoPostBack="false" Font-Names="arial"
                                        Style="text-align: right"  
                                        CssClass="textbox140"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTE1" TargetControlID="txtamt1" FilterType="Custom"
                                        ValidChars="0123456789." runat="server">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/content/Images/Add.jpg" OnClick="btnAdd_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <asp:GridView ID="gvContra" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        CellPadding="3" CellSpacing="1" Font-Names="Arial" Font-Size="8pt" GridLines="None"
                                        Width="100%" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" DataKeyNames="ContraId"
                                        OnSelectedIndexChanged="gvContra_SelectedIndexChanged">
                                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:BoundField DataField="ContraId" HeaderText="ContraId" InsertVisible="False"
                                                ReadOnly="True" SortExpression="ContraId" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                                            <asp:BoundField DataField="AccCode" HeaderText="AccCode" SortExpression="AccCode" />
                                            <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference" />
                                            <asp:BoundField DataField="Chq_No" HeaderText="Chq No" SortExpression="Chq_No" />
                                            <asp:BoundField DataField="Chq_Date" HeaderText="Chq Date" SortExpression="Chq_Date"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                                        </Columns>
                                        <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:Label ID="lblNarration" runat="server" Text="Narration" CssClass="style16"></asp:Label>
                    </td>
                    <td class="style4" colspan="2" style="text-align: left">
                        <asp:TextBox ID="txtNarration" runat="server" Style="text-align: left" 
                            CssClass="textbox400"></asp:TextBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkApproved" runat="server" Text="Approved" 
                            CssClass="fontsize" />
                    </td>
                </tr>
                <tr>
                    <td class="style4" colspan="4" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick="return confirm('Do you want to Save');"
                            Text="Save" Width="70px" CssClass="masterbutton" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" OnClientClick="return confirm('Do you want to Update');"
                            Text="Update" Width="70px" CssClass="masterbutton" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="New" 
                            Width="70px" CssClass="masterbutton" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Width="70px" 
                            CssClass="masterbutton" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnExit" runat="server" OnClick="btnExit_Click" OnClientClick="return confirm('Do you want to Leave this Page');"
                            Text="Exit" Width="70px" CssClass="masterbutton" />
                        &nbsp;
                        <asp:Label ID="lblResult" runat="server"></asp:Label>
                        <asp:Button ID="btnPrevius" runat="server" Text="<<" 
                            onclick="btnPrevius_Click" CssClass="masterbutton" />
                        <asp:Button ID="btnNext" runat="server" Text=">>" onclick="btnNext_Click" 
                            CssClass="masterbutton" />
                    </td>
                </tr>
                <tr>
                    <td class="style6" colspan="4">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
