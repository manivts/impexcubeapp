﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmPortMaster.aspx.cs" Inherits="ImpexCube.frmPortMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="left">
        <tr>
             <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                            font-size: large">
                            Port Master
                        </td>
        </tr>
        <tr>
             <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                            font-size: large" class="style1">
                            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCountry" runat="server" Text="Country" 
                    CssClass="fontsize"></asp:Label>
              </td>
            <td>
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="ddl156" 
                    AutoPostBack="True" onselectedindexchanged="ddlCountry_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPortCode" runat="server" Text="Port Code" CssClass="fontsize"></asp:Label>
              </td>
            <td>
                <asp:TextBox ID="txtPortCode" runat="server" CssClass="textbox150" 
                    Enabled="False" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="lblPortName" runat="server" Text="Port Name" 
                    CssClass="fontsize"></asp:Label>
                </td>
            <td class="style2">
                <asp:TextBox ID="txtPortName" runat="server" CssClass="textbox150" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lblUNECE" runat="server" Text="UNECE Code" 
                    CssClass="fontsize"></asp:Label>
                </td>
            <td class="style1">
                <asp:TextBox ID="txtUNECE" runat="server" CssClass="textbox150" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  colspan="2">
               </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                        <asp:Button ID="btnNew" runat="server" Text="New" CssClass="masterbutton" 
                            onclick="btnNew_Click" />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="masterbutton" 
                            onclick="btnSave_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update"
                    CssClass="masterbutton" onclick="btnUpdate_Click" />
                <asp:Button ID="btnDiscard" runat="server" Text="Exit"
                    CssClass="masterbutton" onclick="btnDiscard_Click" />
            
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                        &nbsp;</td>
        </tr>
        <tr>
        <td colspan="2" >
            <div  class="grid_scroll-2">
                    <asp:GridView ID="gvPort" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="True"
                         AutoGenerateColumns="False"
                         Width="600px" onselectedindexchanged="gvPort_SelectedIndexChanged" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns >
                            <asp:BoundField HeaderText="PortId" DataField="PortId" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField HeaderText="PortCode" DataField="PortCode" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField HeaderText="PortName" DataField="PortName" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField HeaderText="CountryCode" DataField="CountryCode" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField HeaderText="UneceCode" DataField="UneceCode" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
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
          <input type="hidden" id="hdnPortID" runat="server" />
</asp:Content>
