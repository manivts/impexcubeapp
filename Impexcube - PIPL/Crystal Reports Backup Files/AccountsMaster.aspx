<%@ Page Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="AccountsMaster.aspx.cs" Inherits="ImpexCube.Accounts.AccountsMaster"
    Title="Ledger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script language="javascript" type="text/javascript">
        function ValidateEmail(objEmail) {
            var reg = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
            if (!reg.test(objEmail.value)) {
                alert('Please fill valid email');
                onfocus(objEmail);
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image ID="Image1" runat="server" 
                ImageUrl="~/Content/Images/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%; font-size: medium;">
                <tr>
                    <td colspan="7" style="text-align: center">
                        <asp:Label ID="Label1" runat="server" CssClass="labeltitle" Text="Ledger"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" style="text-align: left">
                        <label>
                        Ledger Code</label></td>
                    <td colspan="2">
                        <asp:TextBox ID="txtAcntCode" runat="server" 
                            CssClass="textbox140"></asp:TextBox>
                        <asp:TextBox ID="txtShortName" runat="server" ToolTip="Short Name" 
                           CssClass="textbox140"></asp:TextBox>
                    </td>
                    <td class="fontsize" rowspan="2" align="left">
                        Billing Address1
                    </td>
                    <td rowspan="2">
                        <asp:TextBox ID="txtAddress1" runat="server" TextMode="MultiLine" 
                            CssClass="textboxHeight29"></asp:TextBox>
                    </td>
                    <td class="fontsize" rowspan="2" align="left">
                        Billing Address2
                    </td>
                    <td rowspan="2">
                        <asp:TextBox ID="txtAddress2" runat="server" TextMode="MultiLine"
                            CssClass="textboxHeight29"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" align="left">
                        <label>
                        Ledger Name</label>&nbsp;
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtAccountName" runat="server"
                             CssClass="textbox140"></asp:TextBox>
                <%--  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtAccountName"
                    FilterType="UppercaseLetters,LowercaseLetters" runat="server">
                </cc1:FilteredTextBoxExtender>--%>
                        <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" align="left">
                        Group
                    </td>
                    <td colspan="2" >
                        <asp:DropDownList ID="ddlAccountsGroup" runat="server" 
                            OnSelectedIndexChanged="ddlAccountsGroup_SelectedIndexChanged" CssClass="ddl150" 
                            >
                        </asp:DropDownList>
                    </td>
                    <td class="fontsize" align="left">
                        City
                    </td>
                    <td>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="textbox140" ></asp:TextBox>
                <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtCity"
                    FilterType="UppercaseLetters,LowercaseLetters" runat="server">
                </cc1:FilteredTextBoxExtender>--%>
                    </td>
                    <td class="fontsize" align="left">
                        State
                    </td>
                    <td>
                        <asp:TextBox ID="txtState" runat="server" CssClass="textbox140"></asp:TextBox>
                <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtState"
                    FilterType="UppercaseLetters,LowercaseLetters" runat="server">
                </cc1:FilteredTextBoxExtender>--%>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" align="left" >
                        Phone No
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtPhNo" runat="server" CssClass="textbox140"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="ftePhNo" runat="server" 
                            FilterType="Numbers,Custom" TargetControlID="txtPhNo" ValidChars="+-0123456789">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td class="fontsize" align="left">
                        Country
                    </td>
                    <td>
                        <asp:TextBox ID="txtCountry" runat="server"  CssClass="textbox140"></asp:TextBox>
                <%--  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtCountry"
                    FilterType="UppercaseLetters,LowercaseLetters" runat="server">
                </cc1:FilteredTextBoxExtender>--%>
                    </td>
                    <td class="fontsize" align="left">
                        Pin Code
                    </td>
                    <td>
                        <asp:TextBox ID="txtPinCode" runat="server" MaxLength="6"  
                            CssClass="textbox140"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" 
                            FilterType="Numbers,Custom" TargetControlID="txtPinCode" 
                            ValidChars="0123456789">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" align="left">
                        Bank AccNo
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtBankAccNo" runat="server" 
                            CssClass="textbox140"></asp:TextBox>
                <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtContact"
                    FilterType="UppercaseLetters,LowercaseLetters" runat="server">
                </cc1:FilteredTextBoxExtender>--%>
                    </td>
                    <td class="fontsize" align="left">
                        Bank Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox140"></asp:TextBox>
                    </td>
                    <td class="fontsize" align="left">
                        Bank Branch Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtBankBranchName" runat="server" 
                            CssClass="textbox140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" align="left">
                        IE Code
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtIECode" runat="server" CssClass="textbox140"></asp:TextBox>
                <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtContact"
                    FilterType="UppercaseLetters,LowercaseLetters" runat="server">
                </cc1:FilteredTextBoxExtender>--%>
                    </td>
                    <td class="fontsize" align="left">
                        IFSC Code
                    </td>
                    <td>
                        <asp:TextBox ID="txtIFSC" runat="server" CssClass="textbox140"></asp:TextBox>
                    </td>
                    <td class="fontsize" align="left">
                        RTGS
                    </td>
                    <td>
                        <asp:TextBox ID="txtRTGS" runat="server" CssClass="textbox140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" align="left">
                        Mobile No
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtMobile" runat="server"  CssClass="textbox140"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" 
                            FilterType="Numbers,Custom" TargetControlID="txtMobile" 
                            ValidChars="+0123456789">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td class="fontsize" align="left">
                        Email Id
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox140"></asp:TextBox>
                    </td>
                    <td class="fontsize" align="left">
                        Web Site
                    </td>
                    <td>
                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="textbox140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" align="left">
                        TIN No
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtTin" runat="server" CssClass="textbox140"></asp:TextBox>
                    </td>
                    <td class="fontsize" align="left">
                        LST No
                    </td>
                    <td>
                        <asp:TextBox ID="txtLST" runat="server"  CssClass="textbox140"></asp:TextBox>
                    </td>
                    <td class="fontsize" align="left">
                        VAT No
                    </td>
                    <td>
                        <asp:TextBox ID="txtVAT" runat="server" CssClass="textbox140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" align="left">
                        CST No
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtCST" runat="server"  CssClass="textbox140"></asp:TextBox>
                    </td>
                    <td class="fontsize" align="left">
                        TAN No
                    </td>
                    <td>
                        <asp:TextBox ID="txtTanNo" runat="server" CssClass="textbox140"></asp:TextBox>
                    </td>
                    <td class="fontsize" align="left">
                        PAN No
                    </td>
                    <td>
                        <asp:TextBox ID="txtPAN" runat="server"  CssClass="textbox140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" align="left">
                        Contact Person
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtContact" runat="server"  CssClass="textbox140" 
                            ontextchanged="txtContact_TextChanged"></asp:TextBox>
                    </td>
                    <td class="fontsize" align="left">
                        Service Tax No
                    </td>
                    <td>
                        <asp:TextBox ID="txtSTaxNo" runat="server"  CssClass="textbox140"></asp:TextBox>
                    </td>
                    <td class="fontsize" align="left">
                        MSME No
                    </td>
                    <td>
                        <asp:TextBox ID="txtMSMENo" runat="server"  CssClass="textbox140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" align="left">
                        Opening Balance
                    </td>
                    <td colspan="2" >
                        <asp:TextBox ID="txtOpeninBalance" runat="server" 
                            CssClass="textbox140"></asp:TextBox>
                        <asp:DropDownList ID="ddlCRDR" runat="server" CssClass="ddl50" 
                            onselectedindexchanged="ddlCRDR_SelectedIndexChanged">
                            <asp:ListItem>Cr</asp:ListItem>
                            <asp:ListItem>Dr</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="fontsize" align="left">
                        Credit Limit
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditLimit" runat="server" 
                            CssClass="textbox140"></asp:TextBox>
                    </td>
                    <td class="fontsize" align="left">
                        Reference
                    </td>
                    <td>
                        <asp:TextBox ID="txtReference" runat="server"  
                            CssClass="textbox140"></asp:TextBox>
                    </td>
                </tr>
        <%--<tr>
            <td class="style14" colspan="6">
                
            </td>
        </tr>--%>
                <tr>
                    <td class="style13" colspan="2" >
                        <asp:CheckBox ID="ChkCostCenter" runat="server" 
                            Text="Cost Centers applicable ?" TextAlign="Left" CssClass="fontsize" />
                    </td>
                    <td class="style13" colspan="2">
                        <asp:CheckBox ID="ChkApproved" runat="server" Text="Ledger Approved" 
                            TextAlign="Left" CssClass="fontsize" />
                    </td>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  colspan="7">
                        <asp:Button ID="btnSave" runat="server" CssClass="masterbutton" 
                            OnClick="btnSave_Click" OnClientClick="return confirm('Do you want to save?');" 
                             Text="Save"  />
                        <asp:Button ID="btnUpdate" runat="server" CssClass="masterbutton" 
                            OnClick="btnUpdate_Click" 
                            OnClientClick="return confirm('Do you want to update?');" Text="Update" 
                            />
                        <asp:Button ID="btnNew" runat="server" CausesValidation="false" 
                            CssClass="masterbutton" OnClick="btnNew_Click" Text="New"  />
                        <asp:Button ID="btnExit" runat="server" CausesValidation="false" 
                            CssClass="masterbutton" OnClick="btnExit_Click" 
                            OnClientClick="return confirm('Do you want to leave this page?');" Text="Exit" 
                             />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
