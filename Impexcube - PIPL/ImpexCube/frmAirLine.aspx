<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmAirLine.aspx.cs" Inherits="ImpexCube.frmAirLine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Content/Scripts/ProductDetails.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function valsave() {
            var ddlairline = document.getElementById("<%= ddlAirLine.ClientID %>");
            var selectedText = ddlairline.options[ddlairline.selectedIndex].text;
            if (selectedText == "~Select~") {
                alert('Please Select Airline');
                document.getElementById('<%=ddlAirLine.ClientID%>').focus();
                return false;
            }
            return true;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table align="left">
        <tr>
             <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                            font-size: large">
                            AirLine Master
                        </td>
        </tr>
        <tr>
             <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                            font-size: large">
                            &nbsp;</td>
        </tr>
        <tr>
            <td  style="width: 80px;">
                <asp:Label ID="lblAirline" runat="server" Text="AirLine" CssClass="fontsize"></asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="ddlAirLine" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAirLine_SelectedIndexChanged"
                    CssClass="ddl156">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPrefix" runat="server" Text="Prefix" CssClass="fontsize"></asp:Label>
              </td>
            <td>
                <asp:TextBox ID="txtPrefix" runat="server" CssClass="textbox150" OnKeyPress="javascript:return isFloat(event);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAirlineCode" runat="server" Text="AirLine Code" 
                    CssClass="fontsize"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="txtAirlineCode" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  colspan="2">
               </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CssClass="masterbutton" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="masterbutton"
                    OnClientClick="javascript: return valsave();" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                    CssClass="masterbutton" />
                <asp:Button ID="btnDiscard" runat="server" Text="Exit" OnClick="btnDiscard_Click"
                    CssClass="masterbutton" />
            
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                        &nbsp;</td>
        </tr>
        <tr>
        <td colspan="2" >
            <div  class="grid_scroll-2">
                    <asp:GridView ID="gvAirLine" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="True"
                        OnSelectedIndexChanged="gvAirLine_SelectedIndexChanged" AutoGenerateColumns="False"
                         Width="600" Height="100px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns >
                            <asp:BoundField HeaderText="AirLine" DataField="Airline" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField HeaderText="Airline Prefix" DataField="AirlinePrefix" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                            <asp:BoundField HeaderText="Airline Code" DataField="AirlineCode" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
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
    </asp:Content>
