<%@ Page Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    Inherits="ImpexCube.frmOutward" Title=":: Front Desk || Outward Info" CodeBehind="frmOutward.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">

        function FindTheCBValues(tb) {
            var cbl, txtBx;

            switch (tb) {
                case 'tb1':
                    txtBx = document.getElementById('<%=txtJobs.ClientID%>');
                    cbl = document.getElementById('<%=cbJobs.ClientID%>');
                    break;



                default: break;
            }

            if (txtBx != null && cbl != null && txtBx && cbl) {
                var text4TB = '';

                var labels = cbl.getElementsByTagName('label');

                var checkBoxes = cbl.getElementsByTagName('input');

                // item lengths should be equal below - 1 label per checkbox
                var cbsLength = checkBoxes.length;
                var labelsLength = labels.length;

                if (cbsLength === labelsLength) {
                    for (var i = 0; i < cbsLength; i++) {
                        if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                            txtBx.style.fontStyle = "normal";
                            txtBx.style.color = "Black";
                            text4TB += labels[i].innerHTML + ', ';
                        }
                    }
                }
                txtBx.value = text4TB.substring(0, text4TB.length - 2);
                txtBx.title = txtBx.value;

                if (txtBx.value.length == 0) {
                    txtBx.style.fontStyle = "italic";
                    txtBx.style.color = "#990033";
                    txtBx.value = "Click and Select Below";
                    txtBx.title = "";
                }
            }
        }
        function FindCity(tb) {
            var cbl, txtBx;

            switch (tb) {
                case 'tb1':
                    {
                        txtBx = document.getElementById('<%=txtCity.ClientID%>');
                        cbl = document.getElementById('<%=cbCity.ClientID%>');

                    }
                    break;



                default: break;
            }

            if (txtBx != null && cbl != null && txtBx && cbl) {
                var text4TB = '';

                var labels = cbl.getElementsByTagName('label');

                var checkBoxes = cbl.getElementsByTagName('input');

                // item lengths should be equal below - 1 label per checkbox
                var cbsLength = checkBoxes.length;
                var labelsLength = labels.length;

                if (cbsLength === labelsLength) {
                    for (var i = 0; i < cbsLength; i++) {
                        if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                            txtBx.style.fontStyle = "normal";
                            txtBx.style.color = "Black";
                            text4TB += labels[i].innerHTML + ', ';
                        }
                    }
                }
                labels = '';
                txtBx.value = text4TB.substring(0, text4TB.length - 2);
                txtBx.title = txtBx.value;

                if (txtBx.value.length == 0) {
                    txtBx.style.fontStyle = "italic";
                    txtBx.style.color = "#990033";
                    txtBx.value = "Click and Select Below";
                    txtBx.title = "";
                }
            }
        }

    </script>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="950px" ActiveTabIndex="0"
        Font-Names="Arial" Font-Size="8pt">
        <cc1:TabPanel runat="server" HeaderText="Single" ID="TabPanel1">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr style="background-color: #719ddb;">
                                            <td align="center" colspan="2">
                                                <asp:Label ID="Label14" Font-Names="calibri" Font-Size="12pt" runat="server" Text="Outward Details"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label29" Font-Names="arial" Font-Size="8pt" runat="server" Text="Outward Date :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDateS" Font-Names="arial" Font-Size="8pt" runat="server" Width="200px"
                                                                BackColor="#CCC8E2"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                    TargetControlID="txtDateS" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            <cc1:FilteredTextBoxExtender ID="FTEiDate" TargetControlID="txtDateS" FilterType="Custom"
                                                                ValidChars="0123456789/" runat="server">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 114px">
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rbTypeofOutward" runat="server" RepeatDirection="Horizontal"
                                                                AutoPostBack="True" Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="rbTypeofOutward_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Value="0">Job No</asp:ListItem>
                                                                <asp:ListItem Value="1">Others</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 114px">
                                                            <asp:Label ID="Label16" Font-Names="arial" Font-Size="8pt" runat="server" Text="Select Job No :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="drJobNo" Font-Names="arial" Font-Size="8pt" runat="server"
                                                                Width="200px" AutoPostBack="True" OnSelectedIndexChanged="drJobNo_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 114px">
                                                            <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label
                                                                ID="Label18" Font-Names="arial" Font-Size="8pt" runat="server" Text="Consignee Name :"
                                                                Width="90px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtConsignee" Font-Names="arial" Font-Size="8pt" runat="server"
                                                                Width="200px" BackColor="#CCC8E2"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                                                    runat="server" Font-Names="arial" Font-Size="8pt" ErrorMessage="*" ControlToValidate="txtConsignee"
                                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 114px">
                                                            <asp:Label ID="Label20" Font-Names="arial" Font-Size="8pt" runat="server" Text="City:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCityS" Font-Names="arial" Font-Size="8pt" runat="server" Width="200px"
                                                                Style="margin-left: 0px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 114px">
                                                            <asp:Label ID="Label21" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label
                                                                ID="Label22" Font-Names="arial" Font-Size="8pt" runat="server" Text="AWB Number:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAWBS" Font-Names="arial" Font-Size="8pt" runat="server" Width="200px"
                                                                BackColor="#CCC8E2"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                                                    runat="server" ControlToValidate="txtAWBS" ErrorMessage="*" Font-Names="arial"
                                                                    Font-Size="8pt" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 114px">
                                                            <asp:Label ID="Label23" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label
                                                                ID="Label24" Font-Names="arial" Font-Size="8pt" runat="server" Text="Sent By:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSentByS" Font-Names="arial" Font-Size="8pt" runat="server" Width="200px"
                                                                BackColor="#CCC8E2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top; width: 114px;" align="right">
                                                            <asp:Label ID="Label25" Font-Names="arial" Font-Size="8pt" runat="server" Text="if any Remarks:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRmkSS" Font-Names="arial" Font-Size="8pt" runat="server" Width="200px"
                                                                Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="vertical-align: top; width: 114px;">
                                                            <asp:Label ID="Label26" Font-Names="arial" Font-Size="8pt" runat="server" Text="Document Details :"
                                                                Width="100px"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top;">
                                                            <asp:TextBox ID="txtDDetailsS" Font-Names="arial" Font-Size="8pt" Height="100px"
                                                                runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            &#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                                            <asp:Button ID="Btn_SubmitS" runat="server" Text="Submit" Height="20px" Width="60px"
                                                                BackColor="#FFE0C0" BorderColor="#FFC0C0" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="Arial" Font-Size="8pt" OnClick="Btn_SubmitS_Click" ValidationGroup="a" />&#160;<asp:Button
                                                                    ID="Btn_cancelS" runat="server" Text="Cancel" Height="20px" Width="60px" BackColor="#FFE0C0"
                                                                    BorderColor="#FFC0C0" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                                                                    Font-Size="8pt" OnClick="Btn_cancelS_Click" CausesValidation="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="Label27" runat="server" ForeColor="Red" Text="* Fields are Indicate Mandatory"
                                                                Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <table>
                                                    <tr>
                                                        <td align="left" style="vertical-align: top;">
                                                            <asp:Label ID="Label28" Font-Names="arial" Font-Size="8pt" runat="server" Text="Document List : -"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            CellPadding="4" Font-Names="Arial" Font-Size="8pt" GridLines="Horizontal" Width="154px"
                                                                            ShowHeader="False">
                                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSel1S" Font-Names="arial" Font-Size="7px" AutoPostBack="true"
                                                                                            OnCheckedChanged="chkSel1S_CheckedChanged" Width="20px" runat="server" /></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="DocumentName" SortExpression="DocumentName">
                                                                                    <ItemStyle Wrap="false" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                    <td>
                                                                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            CellPadding="4" Font-Names="Arial" Font-Size="8pt" GridLines="Horizontal" Width="154px"
                                                                            Font-Strikeout="False" Height="157px" ShowHeader="False">
                                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSel2S" Width="20px" Font-Names="arial" Font-Size="7px" AutoPostBack="true"
                                                                                            OnCheckedChanged="chkSel2S_CheckedChanged" runat="server" /></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="t1" SortExpression="t1">
                                                                                    <ItemStyle Wrap="false" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetName" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtSentByS">
                                    </cc1:AutoCompleteExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Multiple">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr style="background-color: #719ddb;">
                                            <td align="center" colspan="2">
                                                <asp:Label ID="Label2" Font-Names="calibri" Font-Size="12pt" runat="server" Text="Outward Details"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label30" Font-Names="arial" Font-Size="8pt" runat="server" Text="Outward Date :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDate" Font-Names="arial" Font-Size="8pt" runat="server" BackColor="#CCC8E2"
                                                                Width="200px"></asp:TextBox><cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtDate"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtDate"
                                                                FilterType="Custom" ValidChars="0123456789/" runat="server">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 124px">
                                                            <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="8pt" Text="Select Consignee:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="drConsignee" runat="server" AutoPostBack="True" Font-Names="arial"
                                                                Font-Size="8pt" OnSelectedIndexChanged="drConsignee_SelectedIndexChanged" Width="200px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 124px">
                                                            <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" Text="City:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCity" runat="server" Font-Names="arial" Font-Size="8pt" Width="200px"></asp:TextBox><cc1:PopupControlExtender
                                                                ID="PopupControlExtender1" runat="server" PopupControlID="pHideShowCity" Position="Bottom"
                                                                TargetControlID="txtCity" />
                                                            <asp:Panel ID="pHideShowCity" runat="server" CssClass="DropDownPanels" ScrollBars="Vertical"
                                                                Style="display: none" Width="154px">
                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:CheckBoxList ID="cbCity" runat="server" AutoPostBack="false" onClick="javascript:FindCity('tb1')"
                                                                            RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table">
                                                                        </asp:CheckBoxList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 124px">
                                                            <asp:Label ID="Label10" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label
                                                                ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Text="Job No :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtJobs" runat="server" Font-Names="arial" Font-Size="8pt" Width="200px"
                                                                OnTextChanged="txtJobs_TextChanged" BackColor="#CCC8E2"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJobs" ErrorMessage="*"
                                                                    Font-Names="arial" Font-Size="8pt" ValidationGroup="b"></asp:RequiredFieldValidator><cc1:PopupControlExtender
                                                                        ID="ajxPopUp" runat="server" PopupControlID="pHideShow" Position="Bottom" TargetControlID="txtJobs" />
                                                            <asp:Panel ID="pHideShow" runat="server" CssClass="DropDownPanels" ScrollBars="Vertical"
                                                                Style="display: none" Width="154px">
                                                                <asp:UpdatePanel ID="panel1" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:CheckBoxList ID="cbJobs" runat="server" AutoPostBack="false" onClick="javascript:FindTheCBValues('tb1')"
                                                                            RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table">
                                                                        </asp:CheckBoxList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </asp:Panel>
                                                            <asp:Button ID="btnInvoice" runat="server" Height="22px" Text="?" Width="20px" OnClick="btnInvoice_Click"
                                                                ToolTip="Get Bill No's" Font-Names="Arial" Font-Size="9pt" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 124px">
                                                            <asp:Label ID="Label12" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label
                                                                ID="Label5" runat="server" Font-Names="arial" Font-Size="8pt" Text="AWB Number:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAWB" runat="server" Font-Names="arial" Font-Size="8pt" Width="200px"
                                                                BackColor="#CCC8E2"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                                                    runat="server" ControlToValidate="txtAWB" ErrorMessage="*" Font-Names="arial"
                                                                    Font-Size="8pt" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 124px">
                                                            <asp:Label ID="Label13" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label
                                                                ID="Label6" runat="server" Font-Names="arial" Font-Size="8pt" Text="Sent By:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSentBy" runat="server" Font-Names="arial" Font-Size="8pt" Width="200px"
                                                                BackColor="#CCC8E2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="vertical-align: top; width: 124px;">
                                                            <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="8pt" Text="if any Remarks:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRmks" runat="server" Font-Names="arial" Font-Size="8pt" Height="50px"
                                                                TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="vertical-align: top; width: 124px;">
                                                            <asp:Label ID="Label9" Font-Names="arial" Font-Size="8pt" runat="server" Text="Document Details :"
                                                                Width="100px"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top;">
                                                            <asp:TextBox ID="txtDDetails" Font-Names="arial" Font-Size="8pt" Height="100px" runat="server"
                                                                Width="250px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            &#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Height="20px" Width="60px"
                                                                BackColor="#FFE0C0" BorderColor="#FFC0C0" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="Arial" Font-Size="8pt" OnClick="BtnSubmit_Click" ValidationGroup="b" />&#160;<asp:Button
                                                                    ID="BtnCancel" runat="server" Text="Cancel" Height="20px" Width="60px" BackColor="#FFE0C0"
                                                                    BorderColor="#FFC0C0" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                                                                    Font-Size="8pt" OnClick="BtnCancel_Click" CausesValidation="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="* Fields are Indicate Mandatory"
                                                                Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <table>
                                                    <tr>
                                                        <td align="left" style="vertical-align: top;">
                                                            <asp:Label ID="Label8" Font-Names="arial" Font-Size="8pt" runat="server" Text="Document List : -"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            CellPadding="4" Font-Names="Arial" Font-Size="8pt" GridLines="Horizontal" Width="154px"
                                                                            ShowHeader="False">
                                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSel1" Font-Names="arial" Font-Size="7px" AutoPostBack="true"
                                                                                            OnCheckedChanged="chkSel1_CheckedChanged" Width="20px" runat="server" /></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="DocumentName" SortExpression="DocumentName">
                                                                                    <ItemStyle Wrap="false" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                    <td>
                                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            CellPadding="4" Font-Names="Arial" Font-Size="8pt" GridLines="Horizontal" Width="154px"
                                                                            Font-Strikeout="False" Height="157px" ShowHeader="False">
                                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSel2" Width="20px" Font-Names="arial" Font-Size="7px" AutoPostBack="true"
                                                                                            OnCheckedChanged="chkSel2_CheckedChanged" runat="server" /></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="t1" SortExpression="t1">
                                                                                    <ItemStyle Wrap="false" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetName" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtSentBy">
                                    </cc1:AutoCompleteExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
