<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmUserReport.aspx.cs" Inherits="ImpexCube.frmUserReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
     <ProgressTemplate>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <span style="color: #000066">Please wait...</span><asp:Image ID="Image1" 
    runat="server" ImageUrl="~/images/progress.gif" 
    Width="35px" />
                    </ProgressTemplate>
    </asp:UpdateProgress>
    
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <ContentTemplate>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFdate"
        Format="dd/MM/yyyy">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTdate"
        Format="dd/MM/yyyy">
    </cc1:CalendarExtender>
    <cc1:AutoCompleteExtender ID="autoComplete2" runat="server" TargetControlID="txtPName"
        ServicePath="~/AutoComplete.asmx" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListCssClass="completionList" ServiceMethod="GetPartyMaster" MinimumPrefixLength="1">
    </cc1:AutoCompleteExtender>
    <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" TargetControlID="txtJNO"
        ServicePath="~/AutoComplete.asmx" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListCssClass="completionList" ServiceMethod="GetJobNo" MinimumPrefixLength="1">
    </cc1:AutoCompleteExtender>
  
    <table style="width:100%">
       
        <tr>
            <td style="vertical-align: top; height: 419px;">
                <table style="width: 100%;">
                    <tr>
                      
                        <td align="left" style="vertical-align: top; width: 90%;">
                            <table style="width: 100%;">
                            <tr style="background-color: #f0f5f9;">
           <td style=" border-bottom: solid 1px Lavender;" align="center">
           <asp:Label ID="Label7" runat="server" Text="PIPL - User Reports" Width="100%" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" Height="18px"></asp:Label>
           </td>
           </tr>
                                <tr style="background-color: #f0f5f9;">
                                    <td style="vertical-align: top;">
                                        <table style="width: 90%;">
                                            <tr>
                                                <td align="left" id="tblMst" runat="server" style="vertical-align: top; width: 600px;">
                                                    <table style="width: 100%; height: 22px;">
                                                        <tr style="background-color: #f0f5f9;">
                                                            <td align="left" style="height: 14px;">
                                                                <table>
                                                                    <tr>
                                                                        <td >
                                                                            <asp:Label ID="lbldoc" runat="server" Font-Names="Arial" Font-Size="7pt" Text="From Date :"
                                                                                Width="54px"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:TextBox ID="txtFdate" runat="server" CausesValidation="True" Font-Names="Arial"
                                                                                Font-Size="7pt" Width="50px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                            <asp:Label ID="lblto" runat="server" Font-Names="Arial" Font-Size="7pt" 
                                                                                Text="TO:"></asp:Label></td>
<td>
                                                                            <asp:TextBox ID="txtTdate" runat="server" Font-Names="Arial" Font-Size="7pt" 
                                                                                Width="50px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="7pt" 
                                                                                Text="Report"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="drReport" Font-Names="Arial" Font-Size="7pt" runat="server"
                                                                                Width="130px" Font-Overline="False">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    
                                                                        <td >
                                                                            <asp:CheckBox ID="chkIMP" runat="server" Font-Names="Arial" Font-Size="7pt" 
                                                                                oncheckedchanged="chkIMP_CheckedChanged" Text="ImpName" />
                                                                        </td>
                                                                        <td >
                                                                            <asp:TextBox ID="txtPName" runat="server" Font-Names="Arial" Font-Size="7pt" 
                                                                                Width="110px"></asp:TextBox>
                                                                        </td>
                                                                        
                                                                        <td >
                                                                            <asp:CheckBox ID="chkJNO" runat="server" Font-Names="Arial" Font-Size="7pt" 
                                                                                oncheckedchanged="chkJNO_CheckedChanged" Text="JNO" AutoPostBack="True" />
                                                                        </td>
                                                                        <td >
                                                                            <asp:TextBox ID="txtJNO" runat="server" Font-Names="Arial" Font-Size="7pt" 
                                                                                Width="100px"></asp:TextBox>
                                                                        </td>
                                                                        <td >
                                                                            <asp:Button ID="BtnSubmit" runat="server" CssClass="button_image" 
                                                                                OnClick="BtnSubmit_Click" Text="Search" Width="55px" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="ExportPage" OnClick="ExportPage_Click" runat="server" Text="Export Excel"
                                                                                CssClass="button_image"  Width="83px"></asp:Button>
                                                                        </td>
                                                                        <td >
                                                                            <asp:Button ID="Button1" runat="server" CssClass="button_image" 
                                                                                PostBackUrl="~/PIPL/frmMarketmodulePIPL.aspx" Text="Exit" Width="55px" 
                                                                                OnClick="Button1_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <div id="GridScroll" class="grid_scroll12" style="width: 920px;">
                                            <asp:GridView ID="Grdiworkreg" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Arial" Font-Size="8pt"
                                                OnRowDataBound="Grdiworkreg_RowDataBound" ForeColor="Black" GridLines="Vertical">
                                                <RowStyle HorizontalAlign="Left" Font-Names="Arial" Font-Size="8pt" BackColor="#F7F7DE" />
                                                <HeaderStyle BackColor="#073088" CssClass="header"  Font-Names="verdana" Font-Size="7pt" ForeColor="#CCCCFF"  />
                                                <RowStyle BackColor="Snow" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" />
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.NO">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lbROnly" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Red"
                                Width="383px"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Red"
                                Width="383px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
</ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID="ExportPage" />
</Triggers>
        </asp:UpdatePanel>
</asp:Content>
