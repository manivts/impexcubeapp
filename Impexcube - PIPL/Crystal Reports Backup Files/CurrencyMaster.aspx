<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="CurrencyMaster.aspx.cs" Inherits="ImpexCube.CurrencyMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function validate() {
        var Curency = document.getElementById("<%= txtCurrencymaster.ClientID %>").value;
        if (Curency == "") {
            alert('Please Enter Currency');
            document.getElementById("<%= txtCurrencymaster.ClientID %>").focus();
            return false;
        }
        return true;
    }
</script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           

<style type="text/css">
.table-wrapper {
	width: 300px;
	color:Black;
	background: #E0E0E0;
	height:14px;
	font-size:8pt;
    text-align: left;
}

	a
	{	color: #324143}

.postmsgg23 {
	color:#000;

	width: 200px;
	
border: 1px solid #c8c6c6;
	
	height:20px;
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
                    <td>
                    <table>
                <tr>
                    <td colspan="1" class="fontsize" >
                        Currency</td>
                    <td class="style2">
                        <asp:TextBox ID="txtCurrencymaster" runat="server" CssClass="textbox150"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td colspan="1" class="fontsize" >
                        Short Name(For Printing)
                    </td>
                    <td >
                        <asp:TextBox ID="txtShortname" runat="server" CssClass="textbox150" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="1" class="fontsize" >
                        Currency Code
                    </td>
                    <td >
                        <asp:TextBox ID="txtCurrency" runat="server" CssClass="textbox150" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="1" class="fontsize" >
                        Currency Code(For EDI)
                    </td>
                    <td >
                        <asp:TextBox ID="txtCurencycode" runat="server" CssClass="textbox150" 
                             ></asp:TextBox>
                    </td>
                    </tr>
                    
                    <tr>
                    <td colspan="1" class="fontsize" >
                        Currency Code(For B\E)
                    </td>
                    <td >
                        <asp:TextBox ID="txtCurrencyBe" runat="server" CssClass="textbox150" 
                             ></asp:TextBox>
                    </td>
                    </tr>
                
                <tr>
                    <td colspan="1" class="fontsize" >
                        Standard Currency
                    </td>
                    <td >
                        <asp:DropDownList ID="ddlstandardcurrency" runat="server" CssClass="ddl156">
                            <asp:ListItem>~Select~</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="fontsize" >
                        Unit
                    </td>
                    <td >
                        <asp:TextBox ID="txtCurrencyUnit" runat="server" CssClass="textbox150"   
                            ></asp:TextBox>
                    </td>
               </tr>
                <tr>
                    <td class="fontsize" >
                        Conversion Factor
                    </td>
                    <td >
                        <asp:TextBox ID="txtConversion" runat="server" CssClass="textbox150"   
                            ></asp:TextBox>
                    </td>
                     </tr>
                     <tr>
                    <td class="fontsize" >
                        Exchange Rate(Import)
                    </td>
                    <td >
                        <asp:TextBox ID="txtexchangeimp" runat="server" CssClass="textbox150" 
                            ></asp:TextBox>
                    </td>
               </tr>
               <tr>
                    <td class="fontsize" >
                        Export Rate(Export)
                    </td>
                    <td >
                        <asp:TextBox ID="txtExchangeExp" runat="server" CssClass="textbox150"   
                            ></asp:TextBox>
                    </td>
               </tr>
               <tr>
                    <td class="fontsize" >
                        Effective From
                    </td>
                    <td >
                        <asp:TextBox ID="txtEffectiveFrom" runat="server" CssClass="textbox150"></asp:TextBox>
                              
                              <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffectiveFrom"
                                        Format="MM/dd/yyyy">
                                    </cc1:CalendarExtender>
                    </td>
               </tr>
               
               <tr>
                <td style="text-align: right">
                    <asp:Button ID="btnNew" runat="server" Text="New" Width="100px" 
                        onclick="btnNew_Click" CssClass="masterbutton" />

                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
                        onclick="btnSave_Click" CssClass="masterbutton" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="100px" 
                        onclick="btnUpdate_Click" CssClass="masterbutton" />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" Width="100px" 
                        onclick="btnExit_Click" CssClass="masterbutton" />
                </td>
                </tr>
                        <tr>
                            <td style="text-align: right">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                </table>
                </td>
                <td valign="top">
                
                <div class="grid_scroll-2">
                        <asp:GridView ID="gvCurrency" runat="server" CssClass="table-wrapper" 
                         AutoGenerateSelectButton="True"  
                            onselectedindexchanged="gvCurrency_SelectedIndexChanged">
                
                          <RowStyle CssClass="table-header light" />
                                    <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="#E7E7FF" />
                           </asp:GridView>
                                  
                        </div>
                       
                </td>
                </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
