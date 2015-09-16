<%@ Page Title="Receipt Register" Language="C#" MasterPageFile="~/Accounts/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="ReceiptReport.aspx.cs" Inherits="AccountsManagement.ReceiptReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .waterText
        {
            font-family: Arial;
            font-size: 8pt;
            color: Fuchsia;
            background-color: #FFFFFF;
        }
    </style>
    <style type="text/css">
        input[type=], textarea
        {
            border: 1px solid #ccc;
        }
        input[type=text]:focus, textarea:focus
        {
            background-color: #F0F0F0;
            border: 1px solid #ccc;
        }
        .hiddenid
        {
            display: none;
        }
        .grid_scroll-GenMaster
        {
            height: 191px;
            overflow: auto;
        }
        .alignment
        {
            text-align:left;
        }
        .Column1
        {
           padding-left:402px;
            width:150px;
        }
        .Column2
        {
            height:30px;
            width:400px;
            padding-left:511px;
            margin-top:-15px;
        }
        
       
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />

  <div>
       <asp:Label ID="Label1" runat="server" Text="Receipt Register" CssClass="labeltitle"></asp:Label>
    </div>
    <br />

    <div>
      <div class="alignment labelsize Column1">
         <asp:Label ID="lblFromDate" runat="server" Text="From Date" 
                    CssClass="labelsize" ></asp:Label>
      </div>

      <div class="alignment labelsize Column2">
          <asp:TextBox ID="txtFrom" runat="server" MaxLength="10" 
                    CssClass="txtbox100"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:FilteredTextBoxExtender ID="FTEDate" TargetControlID="txtFrom" FilterType="Numbers,Custom"
                    ValidChars="01/01/1999" runat="server">
                </cc1:FilteredTextBoxExtender>
      </div>
    </div>

    <div>
      <div class="alignment labelsize Column1">
        <asp:Label ID="lblToDate" runat="server" Text="To Date" CssClass="labelsize"></asp:Label>
      </div>
      <div class="alignment labelsize Column2">
         <asp:TextBox ID="txtTo" runat="server"  MaxLength="10" CssClass="txtbox100"></asp:TextBox>
                <cc1:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                    TargetControlID="txtTo">
                </cc1:CalendarExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtTo"
                    FilterType="Numbers,Custom" ValidChars="01/01/1999" runat="server">
                </cc1:FilteredTextBoxExtender>
      </div>
    </div>

    <div>
      <div class="alignment labelsize Column1">
        <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="labelsize"></asp:Label>
      </div>
      <div class="alignment labelsize Column2">
        <asp:DropDownList ID="ddlStatus" runat="server" Enabled="true" 
                CssClass="ddl100" Height="20px" Width="165px" >            
            <asp:ListItem Value="All" Text="All"></asp:ListItem>
            </asp:DropDownList>
      </div>
    </div>

    <div>
      <asp:Button ID="btnGetReport" runat="server" Text="GetReport" 
                    OnClick="btnGetReport_Click" CssClass="btn100" />
                &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="ExportToExcel" 
                    OnClick="btnExcel_Click" CssClass="btn100" />
                &nbsp;&nbsp;
                <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" 
                    CssClass="btn70" />
    </div>
    <br />

    <div>
         <asp:GridView ID="gvReceiptReport" runat="server" Style="text-align: center" AutoGenerateColumns="true"
                    BorderColor="Black" BorderStyle="Solid" Font-Names="Arial" BorderWidth="1px"
                    Font-Size="10pt" ForeColor="Black" GridLines="Vertical" ShowFooter="false" ShowHeader="true"
                    Width="100%" OnSelectedIndexChanged="gvReceiptReport_SelectedIndexChanged"
                    AllowSorting="True" 
                    OnPageIndexChanging="gvReceiptReport_PageIndexChanging" 
                    OnSorting="gvReceiptReport_Sorting">
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" Width="350px" />
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                    <AlternatingRowStyle BackColor="White" />
                   <%-- <Columns>
                        <asp:CommandField ButtonType="Image" ItemStyle-Font-Bold="false" HeaderText="Print"
                            ItemStyle-Font-Names="Arial" ShowSelectButton="true" SelectImageUrl="~/Image/print-icon.png">
                            <ItemStyle Font-Names="Arial" Font-Bold="False"></ItemStyle>
                        </asp:CommandField>
                    </Columns>--%>
                </asp:GridView>
    </div>



    <%--<table style="width: 76%;" align="center">
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="Label1" runat="server"  
                    Text="Receipt Register" CssClass="labeltitle"></asp:Label>
            </td>
        </tr>
             <tr>
            <td style="text-align: left; width:75px;">
                <asp:Label ID="lblFromDate" runat="server" Text="From Date" 
                    CssClass="labelsize" ></asp:Label>
                </td>
                <td style="text-align: left;">
                <asp:TextBox ID="txtFrom" runat="server" MaxLength="10" 
                    CssClass="txtbox100"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:FilteredTextBoxExtender ID="FTEDate" TargetControlID="txtFrom" FilterType="Numbers,Custom"
                    ValidChars="01/01/1999" runat="server">
                </cc1:FilteredTextBoxExtender>
            </td>
            </tr>
             <tr>     
               <td style="text-align: left; width:75px;" >
                   <asp:Label ID="lblToDate" runat="server" Text="To Date" CssClass="labelsize"></asp:Label>
                   </td>
                   <td style="text-align: left;">
                <asp:TextBox ID="txtTo" runat="server"  MaxLength="10" CssClass="txtbox100"></asp:TextBox>
                <cc1:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                    TargetControlID="txtTo">
                </cc1:CalendarExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtTo"
                    FilterType="Numbers,Custom" ValidChars="01/01/1999" runat="server">
                </cc1:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
        <td style="text-align: left; width:75px;" >
            <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="labelsize"></asp:Label>
        </td>
        <td style="text-align: left;">
            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="true" 
                CssClass="ddl100" >            
            <asp:ListItem Value="All" Text="All"></asp:ListItem>
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="btnGetReport" runat="server" Text="GetReport" 
                    OnClick="btnGetReport_Click" CssClass="btn100" />
                &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="ExportToExcel" 
                    OnClick="btnExcel_Click" CssClass="btn100" />
                &nbsp;&nbsp;
                <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" 
                    CssClass="btn70" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left">
                <asp:GridView ID="gvReceiptReport" runat="server" Style="text-align: center" AutoGenerateColumns="true"
                    BorderColor="Black" BorderStyle="Solid" Font-Names="Arial" BorderWidth="1px"
                    Font-Size="10pt" ForeColor="Black" GridLines="Vertical" ShowFooter="false" ShowHeader="true"
                    Width="100%" OnSelectedIndexChanged="gvReceiptReport_SelectedIndexChanged"
                    AllowSorting="True" 
                    OnPageIndexChanging="gvReceiptReport_PageIndexChanging" 
                    OnSorting="gvReceiptReport_Sorting">
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" Width="350px" />
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" ItemStyle-Font-Bold="false" HeaderText="Print"
                            ItemStyle-Font-Names="Arial" ShowSelectButton="true" SelectImageUrl="~/Image/print-icon.png">
                            <ItemStyle Font-Names="Arial" Font-Bold="False"></ItemStyle>
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        </table>--%>
       
</asp:Content>
