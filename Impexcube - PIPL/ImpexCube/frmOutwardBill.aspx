<%@ Page Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    Inherits="ImpexCube.Dashboard_frmOutwardBill" Title=":: PIPL - Front Desk || OUTWARDING BILLS"
    CodeBehind="frmOutwardBill.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td style="border-bottom-color: Red; border-bottom-style: solid; border-bottom-width: thick;"
                align="center">
                <asp:Label ID="Label5" runat="server" Text="Billing Outward Details - Panel" Font-Bold="True"
                    FoFont-Names="Arial" Font-Size="10pt" ForeColor="#2461bf"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border-bottom-color: Red; border-bottom-style: solid; border-bottom-width: thick;
                vertical-align: top;">
                <table>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td align="left" style="width: 52px">
                                                <asp:Label ID="Label7" runat="server" Text="Billing From :" FoFont-Names="Arial"
                                                    Font-Size="8pt" Width="60px"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtFrom" runat="server" Width="70px" FoFont-Names="Arial" Font-Size="8pt"
                                                    AutoPostBack="True" OnTextChanged="txtFrom_TextChanged"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="fromCE" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label9" runat="server" FoFont-Names="Arial" Font-Size="8pt" Text="To : "
                                                    Width="20px"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtTo" runat="server" FoFont-Names="Arial" Font-Size="8pt" Width="70px"
                                                    AutoPostBack="True" OnTextChanged="txtTo_TextChanged"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" FoFont-Names="Arial" Font-Size="8pt" Text="Consignee Name "
                                                    Width="85px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drConsignee" FoFont-Names="arial" Font-Size="8pt" runat="server"
                                                    Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                  
                                   
                                    <cc1:FilteredTextBoxExtender ID="FTEiDate" TargetControlID="txtFrom" FilterType="Custom"
                                        ValidChars="0123456789/" runat="server">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="FTEoDate" TargetControlID="txtTo" FilterType="Custom"
                                        ValidChars="0123456789/" runat="server">
                                    </cc1:FilteredTextBoxExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" Height="24px" OnClick="BtnSearch_Click"
                                Width="80px" FoFont-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            <asp:Button ID="BtnSave" runat="server" Font-Names="Arial" Font-Size="8pt" Height="24px"
                                Text="Save" Width="80px" OnClick="BtnSave_Click" />
                        </td>
                        <td>
                            <asp:Button ID="BtnCancel" runat="server" Height="25px" OnClick="BtnCancel_Click"
                                Text="Cancel" Width="80px" FoFont-Names="Arial" Font-Size="8pt" PostBackUrl="~/frmDashboardMain.aspx" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="border: solid 1px red;">
            <td style="height: 307px; vertical-align: top;">
                <table id="tlHead" runat="server" style="background-color: #719BDF;">
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" FoFont-Names="arial" Font-Size="8pt" Font-Bold="true"
                                ForeColor="White" Text="JNO"></asp:Label>
                        </td>
                        <td style="width: 40px;">
                            <asp:Label ID="Label3" runat="server" FoFont-Names="arial" Font-Size="8pt" Font-Bold="true"
                                ForeColor="White" Text="IMPORTER"></asp:Label>
                        </td>
                        <td align="center" style="width: 50px;">
                            <asp:Label ID="Label4" runat="server" FoFont-Names="arial" Font-Size="8pt" Font-Bold="true"
                                ForeColor="White" Text="DATE"></asp:Label>
                        </td>
                        <td align="center" style="width: 80px;">
                            <asp:Label ID="Label6" runat="server" FoFont-Names="arial" Font-Size="8pt" Font-Bold="true"
                                ForeColor="White" Text="CITY"></asp:Label>
                        </td>
                        <td align="center" style="width: 80px;">
                            <asp:Label ID="Label8" runat="server" FoFont-Names="arial" Font-Size="8pt" Font-Bold="true"
                                ForeColor="White" Text="COURIER"></asp:Label>
                        </td>
                        <td align="center" style="width: 80px;">
                            <asp:Label ID="Label10" runat="server" FoFont-Names="arial" Font-Size="8pt" Font-Bold="true"
                                ForeColor="White" Text="AWB NO."></asp:Label>
                        </td>
                        <td align="center" style="width: 80px;">
                            <asp:Label ID="Label11" runat="server" FoFont-Names="arial" Font-Size="8pt" Font-Bold="true"
                                ForeColor="White" Text="SENT BY"></asp:Label>
                        </td>
                        <td align="center" style="width: 160px;">
                            <asp:Label ID="Label12" runat="server" FoFont-Names="arial" Font-Size="8pt" Font-Bold="true"
                                ForeColor="White" Text="DOC DETAILS"></asp:Label>
                        </td>
                        <td align="center" style="width: 150px;">
                            <asp:Label ID="Label13" runat="server" FoFont-Names="arial" Font-Size="8pt" Font-Bold="true"
                                ForeColor="White" Text="REMARKS"></asp:Label>
                        </td>
                        <td style="width: 50px;">
                            <asp:Label ID="Label14" runat="server" FoFont-Names="arial" Font-Size="8pt" Font-Bold="true"
                                ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="GridScroll" class="grid_scrollbig">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        FoFont-Names="Arial" Font-Size="8pt" ShowHeader="False" Width="154px" DataKeyNames="jobno"
                        OnRowDataBound="GridView1_RowDataBound" ForeColor="#333333" GridLines="None">
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#E3EAEB" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="jobno" HeaderText="JNO" SortExpression="jobno">
                                <ItemStyle Wrap="false" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:TemplateField Visible="False" HeaderText="IMPORTER">
                                <ItemTemplate>
                                    <asp:Label ID="lblImporter" Visible="false" runat="server" Text='<%#Bind("compName") %>'
                                        Font-Names="Arial" Font-Size="8pt" Width="40px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IMPORTER">
                                <ItemTemplate>
                                    <asp:Label ID="lblImp" runat="server" ToolTip='<%#Bind("compName")%>' Text='<%#Bind("compName") %>'
                                        Font-Names="Arial" Font-Size="8pt" Width="40px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DATE">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" Font-Names="Arial" Font-Size="8pt" Width="50px" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CE3" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                    <cc1:FilteredTextBoxExtender ID="FTEddate" TargetControlID="txtDate" FilterType="Custom"
                                        ValidChars="0123456789/" runat="server">
                                    </cc1:FilteredTextBoxExtender>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CITY">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCity" Font-Names="Arial" Font-Size="8pt" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="COURIER">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAddRmks" Font-Names="Arial" Font-Size="8pt" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AWB No.">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAWBNo" Font-Names="Arial" Font-Size="8pt" Width="70px" runat="server"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sent By">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSentBy" Font-Names="Arial" Font-Size="8pt" Width="70px" runat="server"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetName" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtSentBy">
                                    </cc1:AutoCompleteExtender>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Document Details">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDDetails" TextMode="MultiLine" Font-Names="Arial" Font-Size="8pt"
                                        Width="170px" runat="server"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRmks" Font-Names="Arial" TextMode="MultiLine" Font-Size="8pt"
                                        Width="140px" runat="server"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSel1S" FoFont-Names="arial" Font-Size="7px" Width="20px" runat="server" /></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblresult" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
