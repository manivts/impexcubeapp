<%@ Page Language="C#" MasterPageFile="~/Accounts/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="Receipt.aspx.cs" Inherits="ImpexCube.Accounts.Receipt" Title="Receipt Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style11
        {
             height: 35px;
    }
        .style8
        {
            width: 35px;
        }
        .style4
        {
            height: 26px;
        }
        .style6
        {
            width: 297px;
        }
        .completionList
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
            background-color: white;
            font-family: verdana;
            font-size: 8px;
        }
        .listItem
        {
            color: #666666;
            background-color: white;
            font-family: verdana;
            font-size: 12px;
        }
        .itemHighlighted
        {
            background-color: #ffc0c0;
        }
        .style13
        {
            font-size: 10pt;
        }
        .Hide
        {
            display: none;
        }
        .ajax__combobox_buttoncontainer button
        {
            background-image: url(mvwres://AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/AjaxControlToolkit.ComboBox.arrow-down.gif);
            background-position: center;
            background-repeat: no-repeat;
            border-color: ButtonFace;
            height: 15px;
            width: 15px;
        }
        .style14
        {
            width: 309px;
        }
        .style15
        {
            width: 207px;
        }
        .style16
        {
            width: 232px;
        }
        .style17
        {
            width: 800px;
        }
        </style>

        <style type="text/css">
          body {    text-align: center;
}
.txtbox100
{
    Width:100px;
    font-size: 13px;
    font-family: Calibri;
}
.txtbox150
{
    Width:150px;
    font-size: 13px;
    font-family: Calibri;
}
.txtbox200
{
    Width:200px;
    font-size: 13px;
    font-family: Calibri;
}
.txtbox300
{
    Width:300px;
    font-size: 13px;
    font-family: Calibri;
}
.labelsize
{
    font-size: 13px;
    font-family: Calibri;
}
.labeltitle
{
    font-size: 20px;
    font-family: Calibri;
    text-align: center; 
}
.ddl50
{
   Width:50px;
   font-size: 13px;
   font-family: Calibri;  
}
.ddl100
{
   Width:100px;
   font-size: 13px;
   font-family: Calibri;  
}
.ddl150
{
   Width:150px;
   font-size: 13px;
   font-family: Calibri;  
}
.ddl200
{
   Width:200px;
   font-size: 13px;
   font-family: Calibri;  
}
.btn70
{
   height: 26px;
   Width:70px;
   font-size: 15px;
  
   font-family: Calibri;
   background-color:Gray;
}
.btn100
{
   font-size: 15px;
  
   font-family: Calibri;
   background-color:Gray;
}
.tdalign
{
     text-align: center; 
}
.hiddencol
        {
            display: none;
        }


        .alignment
        {
            text-align:left;
        }
        .Column1
        {
            width:150px;
        }
        .Column2
        {
            width:400px;
            padding-left:120px;
            margin-top:-15px;
        }
         .Column3
        {
            width:120px;
            padding-left:550px;
             margin-top:-20px;
        }
         .Column4
        {
            width:200px;
            padding-left:720px;
            margin-top:-15px;
        }
        
        .div80per
        {
            width:83%;
            margin-left:114px;
            height:30px;
        }
        .div500px
        {
            text-align:left;
         width:500px;   
         float:left;
        }
        .div100px
        {
            text-align:left;
         width:100px;   
         float:left;
        }
        
        .div500pxBgColor
        {
         width:500px;   
         float:left;
         background-color:#3399FF;
        }
        .div100pxBgColor
        {
         width:100px;   
         float:left;
         background-color:#3399FF;
        }
        .div80perBgColor
        {
            width:80%;
            margin-left:114px;
            background-color:#3399FF;
        }
      </style>

    <%--<link href="AccStyles/AccountsStyle.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript">
        function validate() {

            if (document.getElementById('<%=ddlAccountDr.ClientID%>').value == "0") {
                alert("Please select Account before clicking the save button"); // prompt user
                return false;
                document.getElementById("ddlAccountDr").focus();
                //set focus back to control               
            }

            alert('Do you Want to Save?')
        }
        function ddl2_OnChanged() {
            //alert('check');
            var ddl1 = document.getElementById('<%=ddlAccountDr.ClientID%>');
            var ddl2 = document.getElementById('<%=ddlAccountCr.ClientID%>');

            var ddl1Value = ddl1.options[ddl1.selectedIndex].text;
            var ddl2Value = ddl2.options[ddl2.selectedIndex].text;

            if (ddl1Value == ddl2Value) {
                alert('The respective term has already been selected in Account');
                ddl2.selectedIndex = 0;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image ID="Image1" runat="server" 
                ImageUrl="~/Accounts/AccImages/progress.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

        <div>
        <asp:Label ID="Label1" runat="server" Text="Receipt" CssClass="labeltitle"></asp:Label></div>

        <div id="div1">
             <div  class="alignment labelsize Column1"> Voucher No </div>  
             <div id="div21" class="alignment labelsize Column2">
             <asp:TextBox ID="txtVchNo" runat="server" ReadOnly="True" Width="142px" 
                            CssClass="textbox140"></asp:TextBox>
              </div>
              <div class="alignment labelsize Column3" > Voucher Date </div>
              <div id="div22" class="alignment labelsize Column4">  <asp:TextBox ID="txtVchDate" runat="server" MaxLength="10" 
                            CssClass="textbox140"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                            TargetControlID="txtVchDate">
                        </cc1:CalendarExtender>
                        <cc1:FilteredTextBoxExtender ID="fteVchDate" runat="server" 
                            FilterType="Numbers,Custom" TargetControlID="txtVchDate" 
                            ValidChars="01/01/1999">
                        </cc1:FilteredTextBoxExtender></div>
    </div>
    <div class="div80per">
    <div class="div500pxBgColor">
    Account [Dr]
    </div>
    <div class="div100pxBgColor">
    Cheque No
    </div>
    <div class="div100pxBgColor">
    Cheque Date
    </div>
    <div class="div100pxBgColor">
    Amount
    </div>
    
    </div>

       <div class="div80per">
    <div class="div500px">
    <asp:DropDownList ID="ddlAccountDr" runat="server" 
                                        AppendDataBoundItems="True"  CssClass="ddl200" 
            Width="396px">
                                        <asp:ListItem Value="0">~Select~</asp:ListItem>
                                    </asp:DropDownList>
    </div>
    <div class="div100px">
        <asp:TextBox ID="txtChqNo" runat="server" CssClass="textbox100" 
            Font-Names="arial" Font-Size="8pt"></asp:TextBox>
    </div>
    <div class="div100px">
        <asp:TextBox ID="txtChqDate" runat="server" CssClass="textbox100" 
            MaxLength="10"></asp:TextBox>
        
        <cc1:CalendarExtender ID="txtChqDate_CalendarExtender" runat="server" 
            Format="dd/MM/yyyy" TargetControlID="txtChqDate">
        </cc1:CalendarExtender>
    </div>
    <div class="div100px">
        <asp:TextBox ID="txtamt1" runat="server" AutoPostBack="false" 
            CssClass="textbox100" Font-Names="arial" Font-Size="8pt" 
            Style="text-align: right" Width="100px"></asp:TextBox>
      
    </div>
    <div class="div100px">
    </div>
    </div>



    <div class="div80per">
    <div class="div500pxBgColor">
        Particulars</div>
    <div class="div100pxBgColor">
        Cost Center
    </div>
    <div class="div100pxBgColor">
        Reference
    </div>
    <div class="div100pxBgColor">
    Amount
    </div>
    
    </div>

       <div class="div80per">
    <div class="div500px">
    
        <asp:DropDownList ID="ddlAccountCr" runat="server" AppendDataBoundItems="True" 
            AutoPostBack="True" CssClass="ddl200" 
            OnSelectedIndexChanged="ddlAccountCr_SelectedIndexChanged" Width="396px">
            <asp:ListItem Value="0">~Select~</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="ddlMethod" runat="server" AppendDataBoundItems="True" 
            CssClass="ddl100">
            <asp:ListItem Value="0">~Select~</asp:ListItem>
            <asp:ListItem>On Account</asp:ListItem>
            <asp:ListItem>Advance</asp:ListItem>
            <asp:ListItem>Aget Ref</asp:ListItem>
            <asp:ListItem>New Ref.</asp:ListItem>
        </asp:DropDownList>
    
    </div>
    <div class="div100px">
        <asp:TextBox ID="txtCost" runat="server" CssClass="textbox100" 
            Font-Names="arial" Font-Size="8pt"></asp:TextBox>
        <cc1:AutoCompleteExtender ID="txtCost_AutoCompleteExtender" runat="server" 
            CompletionListCssClass="completionList" 
            CompletionListHighlightedItemCssClass="itemHighlighted" 
            CompletionListItemCssClass="listItem" EnableCaching="true" 
            MinimumPrefixLength="0" ServiceMethod="GetCostCenter" 
            ServicePath="~/AutoComplete.asmx" TargetControlID="txtCost">
        </cc1:AutoCompleteExtender>
    </div>
    <div class="div100px">
        <asp:TextBox ID="txtDetails" runat="server" AutoPostBack="True" 
            CssClass="textbox100" ontextchanged="txtDetails_TextChanged"></asp:TextBox>
        <cc1:AutoCompleteExtender ID="txtDetails_AutoCompleteExtender" runat="server" 
            CompletionListCssClass="completionList" 
            CompletionListHighlightedItemCssClass="itemHighlighted" 
            CompletionListItemCssClass="listItem" EnableCaching="true" 
            MinimumPrefixLength="0" ServiceMethod="GetInvoiceDebitNote" 
            ServicePath="~/AutoComplete.asmx" TargetControlID="txtDetails">
        </cc1:AutoCompleteExtender>
    </div>
    <div class="div100px">
        <asp:TextBox ID="txtamt2" runat="server" AutoPostBack="false" 
            CssClass="textbox100" Font-Names="arial" Font-Size="8pt" 
            Style="text-align: right" Width="100px"></asp:TextBox>
        
    </div>
    <div class="div100px">
    </div>
    </div>

       <div class="div80per">
    <div class="div500px">
    
    </div>
    <div class="div100px">
    </div>
    <div class="div100px">
    </div>
    <div class="div100px">
    </div>
    <div class="div100px">
    </div>
    </div>

            <table style="width: 76%;" align="center">
                <tr>
                    <td style="text-align: center" colspan="3">
                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style11" colspan="3">
                        <asp:Label ID="Label2" runat="server" Text="Voucher No " CssClass="fontsize"></asp:Label>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label3" runat="server" CssClass="fontsize" Text="Voucher Date "></asp:Label>
                       
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left;" class="style4">
                        <table style="width: 100%;">
                            <tr>
                                <td bgcolor="#3399FF" colspan="2" width="300px">
                                    <asp:Label ID="Label5" runat="server" CssClass="style13" ForeColor="White" Text="Account [Dr]"></asp:Label>
                                </td>
                                <td style="text-align: center" bgcolor="#3399FF" width="100px">
                                    <asp:Label ID="Label8" runat="server" CssClass="style16" Enabled="False" ForeColor="#E7E7FF"
                                        Text="Cheque No"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#3399FF" width="100px">
                                    <asp:Label ID="Label9" runat="server" CssClass="style16" Enabled="False" ForeColor="#E7E7FF"
                                        Text="Cheque Date"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#3399FF" class="style14">
                                    <asp:Label ID="lblAmount0" runat="server" CssClass="style16" ForeColor="#E7E7FF"
                                        Text="Amount"></asp:Label>
                                </td>
                                <td >
                                   
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: left;" class="style14">
                                    &nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="Chk" runat="server" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;" bgcolor="#3399FF" class="style17">
                                    <asp:Label ID="lblDr" runat="server" CssClass="style16" Text=" Particulars " Style="font-size: 10pt"
                                        ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td class="style15" style="text-align: center;" bgcolor="#3399FF" width="100px">
                                    <asp:Label ID="Label7" runat="server" CssClass="style16" Text="Method of Adj." Style="font-size: 10pt"
                                        ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td style="text-align: center" bgcolor="#3399FF" width="100px">
                                    <asp:Label ID="lblJobNo" runat="server" CssClass="style16" Text="Cost Center" Style="font-size: 10pt"
                                        ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#3399FF" width="100px">
                                    <asp:Label ID="lblRef" runat="server" CssClass="style16" Text=" Reference " Style="font-size: 10pt"
                                        ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                
                                <td style="text-align: center;" bgcolor="#3399FF" class="style14">
                                    <asp:Label ID="lblAmount" runat="server" CssClass="style16" Text="Amount" Style="font-size: 10pt"
                                        ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td >
                                </td>
                            </tr>
                            <tr style="border-style: ridge;">
                                <td class="style17">
                                    &nbsp;</td>
                                <td class="style15">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                
                                <td class="style14">
                                    &nbsp;</td>
                                <td>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn70"/>
                                    <%--<asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Accounts/AccImages/AddNew.png" 
                                        OnClick="btnAdd_Click" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <asp:GridView ID="gvReceiptDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1"
                                        Font-Names="Arial" Font-Size="8pt" GridLines="None" Width="100%" 
                                        OnSelectedIndexChanged="gvReceiptDetails_SelectedIndexChanged" 
                                        ondatabound="gvReceiptDetails_DataBound" ShowFooter="True">
                                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:BoundField DataField="TransId" HeaderText="ReceiptId" InsertVisible="False"
                                                ReadOnly="True" SortExpression="ReceiptId" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                                            <asp:BoundField DataField="AccountCrName" HeaderText="AccountCr" SortExpression="AccountCrName" />
                                            <asp:BoundField DataField="MethodOfAdj" HeaderText="MethodOfAdj" SortExpression="MethodOfAdj" />
                                            <asp:BoundField DataField="CostCenter" HeaderText="CostCenter" SortExpression="CostCenter" />
                                            <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference" />
                                            <%-- <asp:BoundField DataField="Chq_No" HeaderText="Chq/DD No" SortExpression="Chq_No" />
                                            <asp:BoundField DataField="Chq_Date" HeaderText="Chq/DD Date" SortExpression="Chq_Date" />--%>
                                            <asp:BoundField DataField="AmountCr" HeaderText="Amount" SortExpression="AmountCr" />
                                        </Columns>
                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="#E7E7FF" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <tr>
                        <td align="left" colspan="3" style="color: Red;">
                            * Indicates the mandatory field
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" width="100px">
                            <asp:Label ID="lblNarration" runat="server" Text="Narration" 
                                CssClass="fontsize"></asp:Label>
                        </td>
                        <td class="style4" style="text-align: left" width="400px">
                            <asp:TextBox ID="txtNarration" runat="server" Style="text-align: left" 
                              CssClass="textbox400" Width="400px"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:CheckBox ID="chkApproved" runat="server" AutoPostBack="True" OnCheckedChanged="chkApproved_CheckedChanged"
                                Text="Approved" Style="font-size: 10pt" CssClass="fontsize" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr id="rwRemarks" runat="server">
                        <td style="text-align: right" width="100px">
                            <asp:Label ID="Label6" runat="server" Text="Remarks" CssClass="fontsize"></asp:Label>
                        </td>
                        <td class="style4" colspan="2" style="text-align: left">
                            <asp:TextBox ID="txtRemarks" runat="server" Enabled="false" Style="text-align: left"
                               CssClass="textbox400" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" colspan="3" style="text-align: center">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick="javascript:return validate();"
                                Text="Save" Width="70px" Enabled="False" CssClass="btn70" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" OnClientClick="return confirm('Do you want to update?');"
                                Text="Update" Width="70px" CssClass="btn70" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnNew" runat="server" CausesValidation="false" OnClick="btnNew_Click"
                                Text="New" Width="70px" CssClass="btn70" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" Visible="false"
                                Width="70px" CssClass="btn70" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnExit" runat="server" CausesValidation="false" OnClick="btnExit_Click"
                                OnClientClick="return confirm('Do you want to leave this page?');" Text="Exit"
                                Width="70px" CssClass="btn70" />
                            &nbsp;&nbsp;
                            <asp:Label ID="lblResult" runat="server"></asp:Label>
                            <asp:Button ID="btnPrevious" runat="server" Text="<<" 
                                onclick="btnPrevious_Click" CssClass="btn70" />
                            <asp:Button ID="btnNext" runat="server" Text=">>" onclick="btnNext_Click" 
                                CssClass="btn70" />
                        </td>

                    </tr>
                    <tr>
                        <td class="style6" colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
