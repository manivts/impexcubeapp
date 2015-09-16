<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmRSPMaster.aspx.cs" Inherits="ImpexCube.frmRSPMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 197px;
        }
        .style3
        {
            width: 62px;
        }
        .style4
        {
            width: 88px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="Panel1" runat="server" Width="747px">
        <table width="950px" style="height: 305px">
        <tr>
        <td align="center" colspan="2">
            <strong>
            <asp:Label ID="Label4" runat="server" CssClass="style1" 
                Text="RSP Master"></asp:Label>
            </strong>
        </td>
        </tr>
            
            <tr>
                <td align="center" colspan="2">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td >
                    <asp:Label ID="Label2" runat="server" Text="CETH" CssClass="fontsize"></asp:Label>
                </td>               
                <td >                    
                    <asp:TextBox ID="txtceth" runat="server" CssClass="textbox150" 
                        ></asp:TextBox>
                                 

                </td>
                </tr>
                <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="EFF Date" 
                        CssClass="fontsize"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txteffdate" runat="server" CssClass="textbox150"></asp:TextBox>  
                    <cc1:CalendarExtender ID="CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txteffdate">
                                                    </cc1:CalendarExtender>                   
                </td>               
            </tr>
            <tr>
                 <td  >
                     <asp:Label ID="Label3" runat="server" Text="End Date" CssClass="fontsize"></asp:Label>
                </td>          
                <td >
                    <asp:TextBox ID="txtenddate" runat="server" CssClass="textbox150"></asp:TextBox>   
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtenddate">
                                                    </cc1:CalendarExtender>                                     
                </td>
                                             
            </tr>
            <tr>
                <td  >
                     <asp:Label ID="Label1" runat="server" Text="RTA" CssClass="fontsize"></asp:Label></td>
                     <td><asp:TextBox ID="txtrta" runat="server" CssClass="textbox150"></asp:TextBox></td>
            </tr>
            <tr>
            <td align="center" colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Button ID="btnsave" runat="server" CssClass="stylebutton" Text="Save" 
                        onclick="btnsave_Click" />
                    <asp:Button ID="btnupdate" runat="server" CssClass="stylebutton" 
                        onclick="btnupdate_Click" Text="Update" />
                </td>
            </tr>
            <tr>
            <td colspan="2">
           
                <asp:GridView ID="gvrspmaster" runat="server" AutoGenerateColumns="false" 
                    CssClass="table-wrapperInv"
                                             BorderColor="Black" 
                    BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="calibri" Font-Size="10pt" 
                    ForeColor="Black" GridLines="Vertical"
                                            ShowFooter="false" ShowHeader="true" 
                    Style="text-align: center; font-size: 9pt;" Width="652px" 
                    onselectedindexchanged="gvrspmaster_SelectedIndexChanged">
                    <Columns> 
                        <asp:CommandField ShowSelectButton="True" />                         
                        <asp:BoundField DataField="CETH" HeaderText="CETH" 
                            ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="EffDt" HeaderText="EFFDate" 
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EndDT" HeaderText="End Date" 
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ABETRTA" HeaderText="RTA" 
                            ItemStyle-HorizontalAlign="Center" />
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
</asp:Content>
