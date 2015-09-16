<%@ Page Language="C#" MasterPageFile="~/Billing/MasterPage.master" AutoEventWireup="true" Inherits="frmBillingSummary" Title="::PIPLBilling || Billing Summary Report Status " Codebehind="frmBillingSummary.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <style type="text/css">
        .hiddencol
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;
        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.gvReport.ClientID %>');
            var TargetChildControl = "chkBxSelect";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                        Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }

        function ChildClick(CheckBox, HCheckBox) {
            //get target control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter; 
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;
            //Change state of the header CheckBox.
            if (Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if (Counter == TotalChkBx)
                HeaderCheckBox.checked = true;
        }
    </script>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
            <span style="font-size: small; color: #000066">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Please Wait...</span><asp:Image ID="Image123" runat="server" 
                ImageUrl="~/Billing/image/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblShortName" runat="server" Text="" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="10pt" ForeColor="#719BDF"></asp:Label>
                        <asp:Label ID="Label3" runat="server" Text="- Billing Summary Update" Font-Bold="True"
                            Font-Names="Verdana" Font-Size="10pt" ForeColor="#719BDF"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 725px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr valign="middle">
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="From" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFrom" runat="server" Font-Names="Arial" Font-Size="8pt" Width="60px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="To" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="60px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkIMP" runat="server" AutoPostBack="true" Checked="false" Font-Names="Arial"
                                                Font-Size="8pt" OnCheckedChanged="chkIMP_CheckedChanged" Text="Importer" Width="60px" />
                                        </td>
                                        <td style="font-family: Arial; font-size: 8pt;">
                                            <asp:TextBox ID="txtPName" runat="server" AutoPostBack="true" Font-Names="Arial"
                                                Font-Size="8pt" Width="160px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkJobNo" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkJobNo_CheckedChanged"
                                                Font-Names="Arial" Font-Size="8pt" Text="Job No" Width="60px" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtJobNo" runat="server" Font-Names="Arial" Font-Size="8pt" Enabled="false"
                                                Width="143px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Summary ID"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSummaryID" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                AutoPostBack="True" OnTextChanged="txtSummaryID_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                                </cc1:CalendarExtender>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
                                </cc1:CalendarExtender>
                                <cc1:AutoCompleteExtender ID="ACE1" runat="server" EnableCaching="true" MinimumPrefixLength="1"
                                    ServiceMethod="GetCompany" ServicePath="~/AutoComplete.asmx" TargetControlID="txtPName">
                                </cc1:AutoCompleteExtender>
                                <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" EnableCaching="true"
                                    MinimumPrefixLength="1" ServiceMethod="GetInvJobNo" CompletionListCssClass="completionList"
                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtJobNo">
                                </cc1:AutoCompleteExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="vertical-align: top;">
                        <asp:Button ID="Btn_search" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Search"
                            Width="70px" OnClick="Btn_search_Click" Height="25px" CssClass="button_image1" />
                    </td>
                    <td align="left">
                        <asp:Button ID="btnExport" runat="server" Text="Summary Update" Font-Names="Arial"
                            Font-Size="8pt" OnClick="btnExport_Click" Width="100px" Height="25px" CssClass="button_image1" />
                    </td>
                    <td style="width: 200px;" align="left">
                        &nbsp;
                    </td>
                </tr>
                
                <tr>
                    <td colspan="4" align="left">
                        <div id="DivTag" runat="server" class="grid_scroll">
                            <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" BorderColor="#DEDFDE"
                                BorderStyle="None" BorderWidth="1px" Font-Size="8pt" CellPadding="4" BackColor="White"
                                OnRowDataBound="gvReport_RowDataBound" ForeColor="Black" GridLines="Vertical"
                                ShowFooter="True">
                                <FooterStyle BackColor="Silver" BorderStyle="None" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" HorizontalAlign="Right" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.NO">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="invoiceDate" HeaderText="BILL DATE" SortExpression="Desc" />
                                    <asp:BoundField DataField="jobno" HeaderText="JOB NO" SortExpression="Type">
                                        <ItemStyle Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="INV" HeaderText="INV.NO" SortExpression="Desc" />
                                    <asp:BoundField DataField="impRemark" HeaderText="Supplier INV.NO" SortExpression="Desc" />
                                    <asp:TemplateField HeaderText="AGENCY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAgency" Font-Names="arial" Font-Size="8pt" runat="server" CssClass="mAlign"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <%--<asp:BoundField DataField="DN" HeaderText="DN.NO" SortExpression="Desc" />--%>
                                    <asp:TemplateField HeaderText="CFS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCFS" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="STax On CFS Charges">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTaxCFS" Font-Names="arial" Font-Size="8pt" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDO" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="STax On DO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTaxDO" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AAI">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAAI" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="STax on AAI">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTaxAAI" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Survey Fees">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSurveyFees" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="STax on Survey Fees">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTaxSurveyFees" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Custom Duty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomDuty" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Embassy Charges">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmbassyCharges" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="less_advance" HeaderText="ADVANCE RECEVIED">
                                        <ItemStyle CssClass="mAlign" />
                                    </asp:BoundField>
                                      <asp:TemplateField HeaderText="S.TAX">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStax" Font-Names="arial" Font-Size="8pt" runat="server" CssClass="mAlign"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TOTAL">
                                        <ItemTemplate>
                                            <asp:Label ID="lbliTotal" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField HeaderText="TOTAL">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldTtotal" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                   <%-- <asp:TemplateField HeaderText="TOTAL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgTotal" Font-Names="arial" Font-Size="8pt" CssClass="mAlign" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="All">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkBxSelect" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkBxHeader" onclick="javascript:HeaderClick(this);" runat="server" />
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                 <%--<asp:BoundField DataField="Trans" HeaderText="TransId">
                                        <ItemStyle CssClass="hiddencol" />
                                        <HeaderStyle CssClass="hiddencol" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="TransId" HeaderText="Id">
                                        <ItemStyle CssClass="hiddencol" />
                                        <HeaderStyle CssClass="hiddencol" />
                                    </asp:BoundField>
                                </Columns>
                                <RowStyle HorizontalAlign="Left" Font-Names="Arial" Font-Size="8pt" BackColor="#F7F7DE"
                                    Font-Overline="False" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#3399FF" Font-Names="Arial"
                                    Font-Size="7pt" />
                                <RowStyle BackColor="Snow" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
