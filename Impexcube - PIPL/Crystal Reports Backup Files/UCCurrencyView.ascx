<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCurrencyView.ascx.cs" Inherits="ImpexCube.UIMaster.UCCurrencyView" %>
<style type="text/css">



.table-wrapper {
	width: 500px;
	color:Black;
	background: #E0E0E0;
	height:14px;
	font-size:8pt;
    text-align: left;
}

	a
	{	color: #324143}

	</style>
    <div class="grid_scroll-2">
                        <asp:GridView ID="gvCurrency" runat="server" CssClass="table-wrapper" 
                         AutoGenerateSelectButton="True"  onselectedindexchanged="gvCurrency_SelectedIndexChanged" 
                         AllowPaging="true">
                
                          <RowStyle CssClass="table-header light" />
                                    <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="#E7E7FF" />
                           </asp:GridView>
                                  
                        </div>