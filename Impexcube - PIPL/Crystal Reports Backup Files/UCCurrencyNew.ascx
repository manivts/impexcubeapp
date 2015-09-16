<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCurrencyNew.ascx.cs" Inherits="ImpexCube.UIMaster.UCCurrencyNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<style type="text/css">

.postmsgg23 {
	color:#000;

	width: 200px;
	
border: 1px solid #c8c6c6;
	
	height:20px;
}
        .style1
        {
            height: 23px;
        }
    .style2
    {
        font-size: 8pt;
    }
    .style3
    {
        height: 23px;
        font-size: 8pt;
    }
    .style4
    {
        font-size: 8pt;
        font-weight: bold;
    }
    .style5
    {
        height: 23px;
        font-size: 8pt;
        font-weight: bold;
    }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table class="style5" style="text-align: left">
                <tr>
                    <td colspan="2" align="center" style="color: #008080; font-style: italic; font-weight: bold;
                        font-size: large">
                        Currency Master
                    </td>
                </tr>
                <tr>
                    <td colspan="2" ></td>
                </tr>
                <tr>
                    <td colspan="1" class="style4">
                        Currency
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="ddlCurrency" runat="server" Width="200px"                             
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td colspan="1" class="style4">
                        Short Name(For Printing)
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtShortname" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="1" class="style4">
                        Currency Code
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtCurrency" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="1" class="style4">
                        Currency Code(For EDI)
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtCurencycode" runat="server" Width="200px" 
                             CssClass="postmsgg23 required"></asp:TextBox>
                    </td>
                    </tr>
                    
                    <tr>
                    <td colspan="1" class="style4">
                        Currency Code(For B\E)
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtCurrencyBe" runat="server" Width="200px" 
                             CssClass="postmsgg23 required"></asp:TextBox>
                    </td>
                    </tr>
                
                <tr>
                    <td colspan="1" class="style4">
                        Standard Currency
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="ddlstandardcurrency" runat="server" Width="200px">
                            <asp:ListItem>~Select~</asp:ListItem>
                            <asp:ListItem Value="0">Yes</asp:ListItem>
                            <asp:ListItem Value="1">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Unit
                    </td>
                    <td class="style7">
                        <asp:TextBox ID="txtCurrencyUnit" runat="server" Width="200px"  
                            CssClass="postmsgg23 required"></asp:TextBox>
                    </td>
               </tr>
                <tr>
                    <td class="style4">
                        Conversion Factor
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="txtConversion" runat="server" Width="200px"  
                            CssClass="postmsgg23 required"></asp:TextBox>
                    </td>
                     </tr>
                     <tr>
                    <td class="style5">
                        Exchange Rate(Import)
                    </td>
                    <td class="style1">
                        <asp:TextBox ID="txtexchangeimp" runat="server" Width="200px"  
                            CssClass="postmsgg23 required"></asp:TextBox>
                    </td>
               </tr>
               <tr>
                    <td class="style4">
                        Export Rate(Export)
                    </td>
                    <td class="style7">
                        <asp:TextBox ID="txtExchangeExp" runat="server" Width="200px"  
                            CssClass="postmsgg23 required"></asp:TextBox>
                    </td>
               </tr>
               <tr>
                    <td class="style4">
                        Effective From
                    </td>
                    <td class="style7">
                        <asp:TextBox ID="txtEffectiveFrom" runat="server" Width="200px"  
                            CssClass="postmsgg23 required" AutoPostBack="True"></asp:TextBox>
                              
                              <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffectiveFrom"
                                        Format="MM/dd/yyyy">
                                    </cc1:CalendarExtender>
                    </td>
               </tr>
               
               <tr>
                <td style="text-align: right">
                    <asp:Button ID="btnNew" runat="server" Text="New" Width="100px" 
                        onclick="btnNew_Click" />

                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
                        onclick="btnSave_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="100px" 
                        onclick="btnUpdate_Click" />
                    <asp:Button ID="btnDiscard" runat="server" Text="Discard" Width="100px" 
                        onclick="btnDiscard_Click" />
                </td>
                </tr>
                </table>
                
