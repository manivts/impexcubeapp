<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmDocument.aspx.cs" Inherits="ImpexCube.frmDocument" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
      function preventBackspace(e) {
          var evt = e || window.event;
          if (evt) {
              var keyCode = evt.charCode || evt.keyCode;
              if (keyCode === 8) {
                  if (evt.preventDefault) {
                      evt.preventDefault();
                  } else {
                      evt.returnValue = false;
                  }
              }
          }
      }
</script>
    <script language="javascript" type="text/javascript">
        function NewJobCreationDetails() {
            window.open('ffDocumentnew.aspx', '_blank', 'width=800,height=280,titlebar=no, menubar=no, scrollbars=yes,header=no toolbar=no, location=no, resizable=no, left=180, top=200');
            false;
        }

        function ViewJobCreationDetails() {
            window.open('ffDocumentview.aspx', '_blank', 'width=800,height=280,titlebar=no, menubar=no, scrollbars=yes,header=no toolbar=no, location=no, resizable=no, left=180, top=200');
            false;
        }
    </script>
    <style type="text/css">
        .completionList
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
            font-family: verdana;
            font-size: 10px;
            background-color: white;
        }
        .listItem
        {
            color: #666666;
            background-color: white;
            font-family: verdana;
            font-size: 10px;
        }
        .itemHighlighted
        {
            background-color: #ffc0c0;
        }
        .styleLabel
        {
            font-size: 8pt;
        }
        .cpHeader
        {
            cursor: pointer;
        }
        .cpBody
        {
            background-color: #DCE4F9;
            font: normal 11px auto Verdana, Arial;
            border: 1px gray;
            width: 450px;
            padding: 4px;
            padding-top: 7px;
        }
        .FontSize
        {
            font-family: Arial;
            font-size: 8pt;
        }
    </style>
    <style type="text/css">
        .BackColorTab
        {
            font-family: Verdana, Arial, Courier New;
            font-size: 10px;
            color: Gray;
            background-color: Silver;
        }
        .button
        {
            background: #073088;
            font-family: Trebuchet MS;
            font-size: 12px;
            font-weight: bold;
            color: #FFFFFF;
            }
        .button:hover
        {
            background: #deefef;
            border: solid 1px grey;
            font-family: Arial, sans-serif;
            font-size: 12px;
            font-weight: bold;
            color: Red;
            height: 25px;
        }
        .button1
        {
            background: #073088;
            border: solid 1px grey;
            font-family: Trebuchet MS;
            font-size: 12px;
            font-weight: bold;
            color: Red;
            height: 25px;
        }
        .button1:hover
        {
            background: #deefef;
            border: solid 1px grey;
            font-family: Arial, sans-serif;
            font-size: 12px;
            font-weight: bold;
            color: Red;
            height: 25px;
        }
        .buttonsmall
        {
            background: #073088;
            border: solid 1px grey;
            font-family: Trebuchet MS;
            font-size: 12px;
            font-weight: bold;
            color: #FFFFFF;
            height: 25px;
        }
        .buttonsmall:hover
        {
            background: white;
            border: solid 1px grey;
            font-family: Arial, sans-serif;
            font-size: 12px;
            font-weight: bold;
            color: Red;
            height: 25px;
        }
        .grid_scrollg
        {
            overflow: auto;
            border: solid 1px white;
            height: 300px;
            width: 450px;
        }
        .buttonclick
        {
            background: blue;
            border: solid 1px grey;
            font-family: Arial, sans-serif;
            font-size: 12px;
            font-weight: bold;
            color: Red;
            height: 25px;
        }
        #doid
        {
            height: 446px;
        }
        #frame1
        {
            margin-left: 47px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
                                                                                <table>
                                                                                <tr>
                                                                                <td valign="top" align="left">
                                                                               <%-- <div id="tabine" runat="server">
                                                        <cc1:TabContainer ID="JobContainer" runat="server" ActiveTabIndex="0" Font-Names="Arial" 
                                                            Font-Size="8pt"  >--%>
                                                            <%--<cc1:TabPanel runat="server" HeaderText="Document Upload" ID="PnlDocumentUpload" CssClass="lbl">
                                                                
                                                                
                                                                <ContentTemplate>--%>
                                                                                <div style="height: 390px; width:900px;">
                                                                                <asp:Panel ID="Panel2" runat="server" GroupingText="Document Upload" CssClass="lbl">
                                                                                
                                                                                <table>
                                                                                <tr>
                                                                                <td colspan="2" align="center" 
                                                                                        style="color: #000080; font-weight: bold; font-size: large">
                                                                                Document Upload
                                                                                </td>
                                                                                </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label2" runat="server" Text="JOB No" 
                                                                                                CssClass="FontSize"></asp:Label>
                                                                                            </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlJobNo" runat="server" CssClass="ddl150" 
                                                                                                AppendDataBoundItems="true" AutoPostBack="True" 
                                                                                                onselectedindexchanged="ddlJobNo_SelectedIndexChanged">
                                                                                                <asp:ListItem>~Select~</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="Label1" runat="server" CssClass="FontSize" Text="Select File"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="textbox300" 
                                                                                                Height="27px" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr >
                                                                                        <td align="right">
                                                                                            <asp:Label ID="lblFileType1" runat="server" Text="Document Given By" 
                                                                                                CssClass="FontSize"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtDocumentGiven" runat="server" CssClass="textbox300" Width="250px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <asp:Label ID="lblFileType0" runat="server" CssClass="FontSize" 
                                                                                                Text="Document Type"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddFileCategory" runat="server" CssClass="ddl175" 
                                                                                                Width="250px">
                                                                                                <asp:ListItem>~Select~</asp:ListItem>
                                                                                                <asp:ListItem>Shipping Document</asp:ListItem>
                                                                                                <asp:ListItem>Customer Document</asp:ListItem>
                                                                                                <asp:ListItem>Clearance Document</asp:ListItem>
                                                                                                <asp:ListItem>Payment Document</asp:ListItem>
                                                                                                <asp:ListItem>Delivery Document</asp:ListItem>
                                                                                                <asp:ListItem>KYC Document</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trFileName" runat="server" >
                                                                                        <td id="Td1" runat="server" align="right">
                                                                                            <asp:Label ID="lblFileType" runat="server" Text="Document Name" 
                                                                                                CssClass="FontSize"></asp:Label>
                                                                                        </td>
                                                                                        <td id="Td2" runat="server">
                                                                                            <asp:DropDownList ID="ddfiletype" runat="server" Width="250px" 
                                                                                                CssClass="ddl175">
                                                                                                <asp:ListItem>~Select~</asp:ListItem>
                                                                                                <asp:ListItem>Invoice</asp:ListItem>
                                                                                                <asp:ListItem>Packing List</asp:ListItem>
                                                                                                <asp:ListItem>Bill Of Ladding</asp:ListItem>
                                                                                                <asp:ListItem>Certificate Of Origin</asp:ListItem>
                                                                                                <asp:ListItem>Certificate Of Analysis</asp:ListItem>
                                                                                                <asp:ListItem>Technical Write-Up</asp:ListItem>
                                                                                                <asp:ListItem>Catalogue</asp:ListItem>
                                                                                                <asp:ListItem>Purchase Order</asp:ListItem>
                                                                                                <asp:ListItem>LC</asp:ListItem>
                                                                                                <asp:ListItem>Debit Note</asp:ListItem>
                                                                                                <asp:ListItem>Delivery Order</asp:ListItem>
                                                                                                <asp:ListItem>Payment Receipt</asp:ListItem>
                                                                                                <asp:ListItem>Container Bond</asp:ListItem>
                                                                                                <asp:ListItem>Delivery Challan</asp:ListItem>
                                                                                                <asp:ListItem>Others</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    <td colspan="2"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    <td colspan="2">
                                                                                        <asp:GridView ID="gvdocument" runat="server" AutoGenerateSelectButton="True" 
                                                                                            CellPadding="2" ForeColor="#333333" GridLines="None" 
                                                                                            onselectedindexchanged="gvdocument_SelectedIndexChanged" Width="700px" 
                                                                                            AutoGenerateColumns="False">
                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                            <Columns>
                                                                                                <asp:BoundField HeaderText="Job No" DataField="jobno" >
                                                                                                <ControlStyle Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="File Name" DataField="filename" >
                                                                                                <ControlStyle Width="100px" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="File Type" DataField="filetype" >
                                                                                                <ControlStyle Width="100px" />
                                                                                                </asp:BoundField>
                                                                                            </Columns>
                                                                                            <EditRowStyle BackColor="#2461BF" />
                                                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                                            <RowStyle BackColor="#EFF3FB" />
                                                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    <td colspan="2"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    <td colspan="2" align="center">
                                                                                    <asp:Button ID="btnupdate" CssClass="btne" OnClientClick="return confirm('Do you want to Update?');"
                                        runat="server" Text="Update" Width="60px" Font-Names="Arial" 
                                        Font-Size="8pt" Height="30px"
                                        OnClick="btnupdate_Click" Visible="False" />
                                    <asp:Button ID="btnSave" runat="server" CssClass="btne" Font-Names="Arial" 
                                        Font-Size="8pt" Height="30px" OnClick="btnSave_Click" 
                                        OnClientClick="return confirm('Do you want to Save?');" Text="Save" 
                                        Width="60px" />
                                                                                    </td>
                                                                                    </tr>
                                                                                    </table>
                                                                                   </asp:Panel>
                                                                                    </div>
                                                                                   <%-- </ContentTemplate>
                                                                                   
                                                            </cc1:TabPanel>--%>
                                                           <%-- </cc1:TabContainer>
                                                                                    </div>--%>
                                                                                    </td>
                                                                                    <td valign="top">
                                                                                        &nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            
                                                                        
                                                                    </ContentTemplate>
                                                                     <Triggers>
                                                                        <asp:PostBackTrigger ControlID="gvdocument" />
                                                                    <asp:PostBackTrigger ControlID="btnSave" />
                                                                     <asp:PostBackTrigger ControlID="btnupdate" />
                                                                    </Triggers>
                                                           
                                </asp:UpdatePanel>
                                
                            
            <input type="hidden" id="hdnTransportType" runat="server" />
    <input type="hidden" id="hdnLclFcl" runat="server" />
    <input type="hidden" id="hdndg" runat="server" />    
</asp:Content>
