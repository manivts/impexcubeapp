<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmDetention.aspx.cs" Inherits="ImpexCube.frmDetention" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<table style="border-color: #2461BF; border-style: solid; border-width: 2px;">
        <tr>
            <td style="background-color: #2461bf;">
                <center>
                    <asp:Label ID="Label35" runat="server" Font-Bold="True" Font-Size="12pt" Font-Names="Verdana"
                        Text="Ground Rent & Detention Reports" BackColor="#2461BF" BorderStyle="Solid"
                        ForeColor="White" Width="754px"></asp:Label>
                </center>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PLHead" runat="server" Height="50px" Width="125px">
                    <table>
                        <tr>
                            <td style="width: 68px; height: 26px;">
                                <asp:Label ID="Label36" runat="server" Text="Enter Job No" Width="87px" Font-Names="Arial"
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td style="height: 26px">
                                <asp:Label ID="Label37" runat="server" Text=":"></asp:Label>
                            </td>
                            <td style="height: 26px">
                                <asp:TextBox ID="txtJobNO" runat="server" Width="105px" Font-Names="Arial" Font-Size="9pt"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" EnableCaching="true"
                                    MinimumPrefixLength="1" ServiceMethod="GetJNO" CompletionListCssClass="completionList"
                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                    ServicePath="~/AutoComplete.asmx" TargetControlID="txtJobNO">
                                </cc1:AutoCompleteExtender>
                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtJobNO"
                                    WatermarkCssClass="waterText" WatermarkText="70000" runat="server">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                            <td style="height: 26px">
                                <asp:Button ID="BTNFind_Jobs" runat="server" Text="Find" OnClick="BTNFind_Jobs_Click"
                                    Width="53px" />
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="RFV2" runat="server" ControlToValidate="txtJobNO"
                        Display="None" ErrorMessage="Please Give Job Number" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="red" Width="94px"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VC2" runat="server" TargetControlID="RFV2" Width="250px">
                    </cc1:ValidatorCalloutExtender>
                    &nbsp;
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PLDET" runat="server" Width="756px">
                    <table style="background-color: White;">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Ref No." Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRNO" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                                Width="168px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Particulars" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDetails" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                                Width="168px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Supplier" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSupplier" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                                Width="168px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Receipt of Advance Set of documents"
                                                Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRASD" runat="server" Font-Names="Arial" Font-Size="8pt" Width="168px"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="Receipt Original documents" Font-Names="Arial"
                                                Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtODOC" runat="server" Font-Names="Arial" Font-Size="8pt" Width="168px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="Berthing of Vessel" Font-Names="Arial"
                                                Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBVessel" runat="server" Font-Names="Arial" Font-Size="8pt" Width="168px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text="IGM No & Date" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtIGM" runat="server" Font-Names="Arial" Font-Size="8pt" Width="168px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" Text="BOE Noting Date" Font-Names="Arial"
                                                Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBOE" runat="server" Font-Names="Arial" Font-Size="8pt" Width="168px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td style="height: 26px">
                                            <asp:Label ID="Label17" runat="server" Text="Ground Rent free upto" Font-Names="Arial"
                                                Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td style="height: 26px">
                                            <asp:Label ID="Label18" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td style="height: 26px">
                                            <asp:TextBox ID="txtGRent" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" Text="Detention free upto" Font-Names="Arial"
                                                Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDetention" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" Text="Movement from Port to CFS" Font-Names="Arial"
                                                Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMoveCFS" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label23" runat="server" Text="Movement from CFS to Plant" Font-Names="Arial"
                                                Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label24" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMovePlant" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label25" runat="server" Text="Empty Containers returned on" Font-Names="Arial"
                                                Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label26" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtContReturn" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label27" runat="server" Text="Ground Rent/CFS" Font-Names="Arial"
                                                Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label28" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBPTDamrage" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label29" runat="server" Text="Detention Charge(Shipping Line)" Font-Names="Arial"
                                                Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label30" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDEt_charge" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label31" runat="server" Text="Damage Charge" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label32" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDamage_Charge" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td style="vertical-align: top; width: 65px;">
                                            <asp:Label ID="Label33" runat="server" Text="Reasons" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:Label ID="Label34" runat="server" Text=":"></asp:Label>
                                        </td>
                                        <td style="width: 183px">
                                            <asp:GridView ID="gvDetail" runat="server" BackColor="White" BorderColor="Black"
                                                BorderStyle="Solid" BorderWidth="1px" AutoGenerateColumns="False" CellPadding="3"
                                                CellSpacing="1" Font-Names="Arial" Font-Size="8pt" GridLines="None" ShowFooter="True"
                                                Width="489px">
                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                                                <Columns>
                                                    <asp:BoundField DataField="job_stage" HeaderText="Reason" SortExpression="name">
                                                        <HeaderStyle Width="100px" />
                                                        <ItemStyle Wrap="false" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRmks" runat="server" Font-Size="8pt" Height="20px" Width="400px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <%--<asp:DataGrid ID="DGDetail1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="#333333" GridLines="None" Width="618px"
                                                BorderColor="#C0C0FF" BorderStyle="Solid" BorderWidth="1px" OnSelectedIndexChanged="DGDetail1_SelectedIndexChanged">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditItemStyle BackColor="#2461BF" Font-Names="Arial" Font-Size="7pt" />
                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Font-Names="Arial"
                                                    Font-Size="7pt" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Names="Arial"
                                                    Font-Size="8pt" />
                                                <AlternatingItemStyle BackColor="#D1DDF1" Font-Names="Arial" Font-Size="7pt" />
                                                <ItemStyle BackColor="#D1DDF1" Font-Names="Arial" Font-Size="7pt" />
                                                <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Arial"
                                                    Font-Size="8pt" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="job_stage" HeaderText="Reason" SortExpression="name">
                                                        <HeaderStyle Width="100px" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRmks" runat="server" Font-Size="8pt" Height="20px" Width="510px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>--%>
                                            <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="#333333" GridLines="None" Width="616px"
                                                BorderColor="#C0C0FF" BorderStyle="Solid" BorderWidth="1px">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditItemStyle BackColor="#2461BF" Font-Names="Arial" Font-Size="7pt" />
                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Font-Names="Arial"
                                                    Font-Size="7pt" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Names="Arial"
                                                    Font-Size="8pt" />
                                                <AlternatingItemStyle BackColor="#D1DDF1" Font-Names="Arial" Font-Size="7pt" />
                                                <ItemStyle BackColor="#D1DDF1" Font-Names="Arial" Font-Size="7pt" />
                                                <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Arial"
                                                    Font-Size="8pt" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="job_stage" HeaderText="Reason" SortExpression="name">
                                                        <HeaderStyle Width="100px" />
                                                        <ItemStyle Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRmks" runat="server" Font-Size="8pt" Height="20px" Text='<%# Bind("remark") %>'
                                                                Width="510px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 65px">
                                        </td>
                                        <td>
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="BtnSubmit" runat="server" Height="35px" Width="106px" OnClick="BtnSubmit_Click"
                                                OnClientClick="return confirm ('Are your sure want to Update ?');" />
                                            <asp:Button ID="BtnCancel" runat="server" Height="35px" Text="Exit" Width="60px"
                                                OnClick="BtnCancel_Click" />
                                            <asp:Label ID="lblResult" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <cc1:CalendarExtender ID="CE1" TargetControlID="txtBVessel" Format="dd/MM/yyyy" runat="server">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CE2" TargetControlID="txtODOC" Format="dd/MM/yyyy" runat="server">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CE4" TargetControlID="txtGRent" Format="dd/MM/yyyy" runat="server">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CE5" TargetControlID="txtDetention" Format="dd/MM/yyyy"
        runat="server">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CE6" TargetControlID="txtMoveCFS" Format="dd/MM/yyyy" runat="server">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CE7" TargetControlID="txtMovePlant" Format="dd/MM/yyyy"
        runat="server">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CE8" TargetControlID="txtContReturn" Format="dd/MM/yyyy"
        runat="server">
    </cc1:CalendarExtender>
    <cc1:TextBoxWatermarkExtender ID="TWE1" runat="server" TargetControlID="txtODOC"
        WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
    </cc1:TextBoxWatermarkExtender>
    <cc1:TextBoxWatermarkExtender ID="TWE2" runat="server" TargetControlID="txtBVessel"
        WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
    </cc1:TextBoxWatermarkExtender>
    <cc1:TextBoxWatermarkExtender ID="TWE3" runat="server" TargetControlID="txtIGM" WatermarkText="No.& dd/MM/yyyy"
        WatermarkCssClass="watermarked">
    </cc1:TextBoxWatermarkExtender>
    <cc1:TextBoxWatermarkExtender ID="TWE4" runat="server" TargetControlID="txtBOE" WatermarkText="No.& dd/MM/yyyy"
        WatermarkCssClass="watermarked">
    </cc1:TextBoxWatermarkExtender>
    <cc1:TextBoxWatermarkExtender ID="TWE5" runat="server" TargetControlID="txtGRent"
        WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
    </cc1:TextBoxWatermarkExtender>
    <cc1:TextBoxWatermarkExtender ID="TWE6" runat="server" TargetControlID="txtDetention"
        WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
    </cc1:TextBoxWatermarkExtender>
    <cc1:TextBoxWatermarkExtender ID="TWE7" runat="server" TargetControlID="txtMoveCFS"
        WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
    </cc1:TextBoxWatermarkExtender>
    <cc1:TextBoxWatermarkExtender ID="TWE8" runat="server" TargetControlID="txtMovePlant"
        WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
    </cc1:TextBoxWatermarkExtender>
    <cc1:TextBoxWatermarkExtender ID="TWE9" runat="server" TargetControlID="txtContReturn"
        WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
    </cc1:TextBoxWatermarkExtender>
    <cc1:TextBoxWatermarkExtender ID="TWE10" runat="server" TargetControlID="txtBPTDamrage"
        WatermarkText="Rupees" WatermarkCssClass="watermarked">
    </cc1:TextBoxWatermarkExtender>
    <cc1:TextBoxWatermarkExtender ID="TWE11" runat="server" TargetControlID="txtDEt_charge"
        WatermarkText="Rupees" WatermarkCssClass="watermarked">
    </cc1:TextBoxWatermarkExtender>
    <cc1:TextBoxWatermarkExtender ID="TWE12" runat="server" TargetControlID="txtDamage_Charge"
        WatermarkText="Rupees" WatermarkCssClass="watermarked">
    </cc1:TextBoxWatermarkExtender>
</asp:Content>
