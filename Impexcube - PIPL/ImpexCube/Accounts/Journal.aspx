<%@ Page Language="C#" MasterPageFile="~/Accounts/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="Journal.aspx.cs" Inherits="ImpexCube.Accounts.Journal" Title="Journal Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            text-align: center;
        }
        .style8
        {
            width: 35px;
        }
        .Hide
        {
        	display:none;
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
   font-size: 13px;
   font-family: Calibri;  
}
.ddl200
{
   font-size: 13px;
   font-family: Calibri;  
}
.btn70
{
   height: 26px;
   Width:70px;
   font-size: 15px;
  margin-top: -40px;
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
            width:180px;
            padding-left:222px;
            margin-top:-15px;
        }
        .Column2
        {
            width:294px;
            padding-left:319px;
            margin-top:-15px;
        }
         .Column3
        {
            width:120px;
            padding-left:536px;
             margin-top:-15px;
        }
         .Column4
        {
            width:200px;
            padding-left:642px;
            margin-top:-15px;
        }
        
        .div80per
        {
            width:83%;
            margin-left:190px;
            height:30px;
        }
        .div500px
        {
            text-align:left;
         width:216px;   
         float:left;
            height: 17px;
        }
        .div100px
        {
            text-align:left;
         width:110px;   
         float:left;
        }
         .div101px
        {
            text-align:left;
         width:104px;   
         float:left;
        }
        .div102px
        {
            text-align:left;
         width:37px;   
         float:left;
        }
        .div103px
        {
            text-align:left;
         width:88px;   
         float:left;
        }
        
        .div500pxBgColor
        {
         width:219px;   
         float:left;
         background-color:#3399FF;
        }
        .div100pxBgColor
        {
         width:110px;   
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
            <asp:Label ID="Label1" runat="server" Text="Journal Entry" CssClass="labeltitle"></asp:Label>
        </div>
        <br />
        <div>
          <div class="alignment labelsize Column1"><asp:Label ID="Label3" runat="server" Text="Voucher No " CssClass="fontsize"></asp:Label></div>
          <div class="alignment labelsize Column2"><asp:TextBox ID="txtVchNo" runat="server" ReadOnly="True" 
                            style="font-family: Arial; font-size: 8pt" CssClass="textbox140" 
                            EnableTheming="False"></asp:TextBox></div>
          <div class="alignment labelsize Column3"><asp:Label ID="Label4" runat="server" Text="Voucher Date " CssClass="fontsize"></asp:Label></div>
          <div class="alignment labelsize Column4"> <asp:TextBox ID="txtVchDate" runat="server" ReadOnly="False" 
                            style="font-family: Arial; font-size: 8pt" CssClass="textbox140"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                            TargetControlID="txtVchDate">
                        </cc1:CalendarExtender></div>
        </div>
        <br/>

        
        <div class="div80per">
          <div class="div500pxBgColor">Account Name</div>
          <div class="div100pxBgColor">Method of obj</div>
          <div class="div100pxBgColor">Cost Center</div>
          <div class="div100pxBgColor">Reference</div>
          <div class="div100pxBgColor"></div>
          <div class="div100pxBgColor">Amount</div>
    
        </div>

        <div class="div80per">
            <div class="div500px"><asp:DropDownList ID="ddlAccountName" runat="server" 
                    AppendDataBoundItems="True" Font-Size="8pt" CssClass="ddl200" Height="16px" 
                    Width="216px">
                                        <asp:ListItem>-Select-</asp:ListItem>
                                    </asp:DropDownList>  </div> 
           <div class="div100px"> <asp:DropDownList ID="ddlMethod" runat="server" AppendDataBoundItems="True" 
                                        Font-Size="8pt" CssClass="ddl150" Height="16px" Width="111px">
                                        <asp:ListItem Value="~Select~">~Select~</asp:ListItem>
                                        <asp:ListItem>On Account</asp:ListItem>
                                        <asp:ListItem>Advance</asp:ListItem>
                                        <asp:ListItem>New Ref.</asp:ListItem>
                                    </asp:DropDownList> </div> 
           <div class="div100px"> <asp:TextBox ID="txtCost" runat="server" CssClass="textbox100" 
                                        Font-Names="arial" Font-Size="8pt" Width="106px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem" DelimiterCharacters=""
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetCostCenter"
                                        ServicePath="~/AutoComplete.asmx" TargetControlID="txtCost">
                                    </cc1:AutoCompleteExtender></div> 
             <div class="div101px"><asp:TextBox ID="txtDetails" runat="server" AutoPostBack="true" 
                                        CssClass="textbox100" Font-Names="arial" Font-Size="8pt" Width="100px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="acetxtdetails" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetInvoiceNo" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtDetails">
                                    </cc1:AutoCompleteExtender> </div>
           <div class="div102px"> <asp:DropDownList ID="ddlDrCr" runat="server" 
                    CssClass="ddl50" Width="38px" >
                                        <asp:ListItem>Dr</asp:ListItem>
                                        <asp:ListItem>Cr</asp:ListItem>
                                    </asp:DropDownList></div>
             <div class="div103px"><asp:TextBox ID="txtamt1" runat="server" AutoPostBack="false" 
                                        Font-Names="arial" Font-Size="8pt" Style="text-align: right" 
                                        CssClass="textbox100" Width="80px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTE1" runat="server" FilterType="Custom" 
                                        TargetControlID="txtamt1" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender> </div>              
            <div class="div101px"><asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn70"/></div>
        </div> 
        

        <div>
              <asp:GridView ID="gvJournalDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1"
                                        Font-Names="Arial" Font-Size="8pt" GridLines="None" Width="100%" 
                                        onselectedindexchanged="gvJournalDetails_SelectedIndexChanged">
                                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:BoundField DataField="TransId" HeaderText="Slno" InsertVisible="False"
                                                ReadOnly="True" SortExpression="TransId" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                                            <asp:BoundField DataField="AccountCode" HeaderText="Account Name" SortExpression="AccountCode" />
                                            <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference" />
                                            <asp:BoundField DataField="CostCenter" HeaderText="CostCenter" SortExpression="CostCenter" />                                            
                                            <asp:BoundField DataField="DrCr" HeaderText="DR/CR" SortExpression="DrCr" />                                            
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                                        </Columns>
                                        <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                                    </asp:GridView>
        </div>
        <br />
        <div>
            <div class="alignment labelsize Column1"> <asp:Label ID="lblNarration" runat="server" Text="Narration" 
                            CssClass="fontsize"></asp:Label></div>
            <div class="alignment labelsize Column2"> <asp:TextBox ID="txtNarration" runat="server" 
                            Style="text-align: left" CssClass="textbox300" width="212px" ></asp:TextBox>
                        <asp:CheckBox ID="Chk" runat="server" Visible="False" />
                        
            
              <asp:CheckBox ID="chkApproved" runat="server" Text="Approved" 
                            CssClass="fontsize" />
            </div>


        </div>
        <br />

        <div>
              <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" 
                            OnClientClick="return confirm('Do you want to Save');" Width="70px" 
                            CssClass="btn70" />
                        &nbsp;
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                            OnClientClick="return confirm('Do you want to Update');" Width="70px" 
                            CssClass="btn70" />
                        &nbsp;
                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" Style="height: 26px"
                            CausesValidation="false" CssClass="btn70" />
                        &nbsp;
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Width="70px" 
                            Visible="False" CssClass="btn70" />
                        &nbsp;
                        <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" CausesValidation="false"
                            OnClientClick="return confirm('Do you want to Leave this Page');" 
                            Width="70px" CssClass="btn70" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<<" 
                            onclick="btnPrevious_Click" CssClass="btn70" />
                        <asp:Button ID="btnNext" runat="server" Text=">>" onclick="btnNext_Click" 
                            CssClass="btn70" />
        </div>
       


           <%-- <table style="width: 83%;" align="center">
                <tr>
                    <td colspan="3" style="text-align: center">
                        &nbsp;<asp:Label ID="Label1" runat="server" Text="Journal Entry" CssClass="labeltitle"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" class="style11" colspan="3">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label3" runat="server" Text="Voucher No " CssClass="fontsize"></asp:Label>
                        <asp:TextBox ID="txtVchNo" runat="server" ReadOnly="True" 
                            style="font-family: Arial; font-size: 8pt" CssClass="textbox140" 
                            EnableTheming="False"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label4" runat="server" Text="Voucher Date " CssClass="fontsize"></asp:Label>
                        <asp:TextBox ID="txtVchDate" runat="server" ReadOnly="False" 
                            style="font-family: Arial; font-size: 8pt" CssClass="textbox140"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                            TargetControlID="txtVchDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
               <tr>
                    <td colspan="3" style="text-align: left;" class="style4">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: center;" bgcolor="#2461BF" width="200px">
                                    <asp:Label ID="lblDr" runat="server" CssClass="style16" Text=" Account Name " Style="font-size: 10pt"
                                        ForeColor="#E7E7FF"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#2461BF" width="150px">
                                    <asp:Label ID="Label7" runat="server" CssClass="style16" ForeColor="#E7E7FF" 
                                        Style="font-size: 10pt" Text="Method of Adj."></asp:Label>
                                </td>
                                <td class="style15" style="text-align: center;" bgcolor="#2461BF" width="100px">
                                    <asp:Label ID="lblJobNo" runat="server" CssClass="style16" ForeColor="#E7E7FF" 
                                        Style="font-size: 10pt" Text="Cost Center"></asp:Label>
                                </td>
                                <td style="text-align: center;" bgcolor="#2461BF" width="100px">
                                    <asp:Label ID="lblRef" runat="server" CssClass="style16" ForeColor="#E7E7FF" 
                                        Style="font-size: 10pt" Text=" Reference "></asp:Label>
                                </td>
                                <td style="text-align: center" bgcolor="#2461BF" width="50px">
                                    `</td>
                                <td style="text-align: center;" bgcolor="#2461BF" width="100px">
                                    <asp:Label ID="lblAmount" runat="server" CssClass="style16" ForeColor="#E7E7FF" 
                                        Style="font-size: 10pt" Text="Amount"></asp:Label>
                                </td>                                
                                <td>
                                </td>
                            </tr>
                            <tr style="border-style: ridge;">
                                <td class="style4">
                                    <asp:DropDownList ID="ddlAccountName" runat="server" 
                                        AppendDataBoundItems="True" Font-Size="8pt" CssClass="ddl200"
                                      >
                                        <asp:ListItem>-Select-</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMethod" runat="server" AppendDataBoundItems="True" 
                                        Font-Size="8pt" CssClass="ddl150">
                                        <asp:ListItem Value="~Select~">~Select~</asp:ListItem>
                                        <asp:ListItem>On Account</asp:ListItem>
                                        <asp:ListItem>Advance</asp:ListItem>
                                        <asp:ListItem>New Ref.</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCost" runat="server" CssClass="textbox100" 
                                        Font-Names="arial" Font-Size="8pt"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem" DelimiterCharacters=""
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetCostCenter"
                                        ServicePath="~/AutoComplete.asmx" TargetControlID="txtCost">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td class="style4">
                                    
                                    <asp:TextBox ID="txtDetails" runat="server" AutoPostBack="true" 
                                        CssClass="textbox100" Font-Names="arial" Font-Size="8pt"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="acetxtdetails" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetInvoiceNo" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtDetails">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td class="style4">
                                    <asp:DropDownList ID="ddlDrCr" runat="server" CssClass="ddl50" >
                                        <asp:ListItem>Dr</asp:ListItem>
                                        <asp:ListItem>Cr</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="style4">
                                    <asp:TextBox ID="txtamt1" runat="server" AutoPostBack="false" 
                                        Font-Names="arial" Font-Size="8pt" Style="text-align: right" 
                                        CssClass="textbox100"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTE1" runat="server" FilterType="Custom" 
                                        TargetControlID="txtamt1" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </td>                                
                                <td>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn70"/>
                                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Accounts/AccImages/AddNew.png" 
                                        OnClick="btnAdd_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <asp:GridView ID="gvJournalDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1"
                                        Font-Names="Arial" Font-Size="8pt" GridLines="None" Width="100%" 
                                        onselectedindexchanged="gvJournalDetails_SelectedIndexChanged">
                                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:BoundField DataField="TransId" HeaderText="Slno" InsertVisible="False"
                                                ReadOnly="True" SortExpression="TransId" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                                            <asp:BoundField DataField="AccountCode" HeaderText="Account Name" SortExpression="AccountCode" />
                                            <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference" />
                                            <asp:BoundField DataField="CostCenter" HeaderText="CostCenter" SortExpression="CostCenter" />                                            
                                            <asp:BoundField DataField="DrCr" HeaderText="DR/CR" SortExpression="DrCr" />                                            
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                                        </Columns>
                                        <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>                
                <tr>
                    <td style="text-align: left" width="80px">
                        <asp:Label ID="lblNarration" runat="server" Text="Narration" 
                            CssClass="fontsize"></asp:Label>
                    </td>
                    <td style="text-align: left" class="style4" width="500px">
                        <asp:TextBox ID="txtNarration" runat="server" 
                            Style="text-align: left" CssClass="textbox300" Width="400px" ></asp:TextBox>
                        <asp:CheckBox ID="Chk" runat="server" Visible="False" />
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkApproved" runat="server" Text="Approved" 
                            CssClass="fontsize" />
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" 
                            OnClientClick="return confirm('Do you want to Save');" Width="70px" 
                            CssClass="btn70" />
                        &nbsp;
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                            OnClientClick="return confirm('Do you want to Update');" Width="70px" 
                            CssClass="btn70" />
                        &nbsp;
                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" Style="height: 26px"
                            CausesValidation="false" CssClass="btn70" />
                        &nbsp;
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Width="70px" 
                            Visible="False" CssClass="btn70" />
                        &nbsp;
                        <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" CausesValidation="false"
                            OnClientClick="return confirm('Do you want to Leave this Page');" 
                            Width="70px" CssClass="btn70" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<<" 
                            onclick="btnPrevious_Click" CssClass="btn70" />
                        <asp:Button ID="btnNext" runat="server" Text=">>" onclick="btnNext_Click" 
                            CssClass="btn70" />
                    </td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
