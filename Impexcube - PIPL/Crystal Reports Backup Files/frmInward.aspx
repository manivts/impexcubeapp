<%@ Page Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" Inherits="ImpexCube.frmInward" Title=":: Front Desk || Inward Info" Codebehind="frmInward.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <%-- <script language="javascript" type="text/javascript">               
           
    function FindTheCBValues(tb)
    {         
      var cbl, txtBx;                 
    
      switch(tb)
      {
              case 'tb1': 
                txtBx = document.getElementById('<%=txtJobs.ClientID%>');
//                cbl = document.getElementById('<%=cbJobs.ClientID%>');
                break;
       

                         
             default: break;           
        }
               
        if (txtBx != null && cbl != null && txtBx && cbl)
        {
            var text4TB = '';
                                                                                                                  
            var labels = cbl.getElementsByTagName('label'); 
                    
            var checkBoxes = cbl.getElementsByTagName('input');
            
            // item lengths should be equal below - 1 label per checkbox
            var cbsLength = checkBoxes.length;  
            var labelsLength = labels.length;                
            
            if (cbsLength === labelsLength)
            {                                               
                for(var i = 0; i < cbsLength; i++)
                {                                
                    if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked)
                    {                                                
                        txtBx.style.fontStyle = "normal";
                        txtBx.style.color = "Black";
                        text4TB += labels[i].innerHTML + ', ';
                    }                                           
                }           
            }                                    
            txtBx.value = text4TB.substring(0,text4TB.length - 2);                
            txtBx.title = txtBx.value;                         
            
            if (txtBx.value.length == 0)
            {  
                txtBx.style.fontStyle = "italic";
                txtBx.style.color = "#990033";
                txtBx.value = "Click and Select Below";            
                txtBx.title = "";      
            }
        }
    }
  function FindCity(tb)
    {         
      var cbl, txtBx;                 
    
      switch(tb)
      {
              case 'tb1': 
              {
                    txtBx = document.getElementById('<%=txtCity.ClientID%>');
                    cbl = document.getElementById('<%=cbCity.ClientID%>');
                
               }
                break;
       

                         
             default: break;           
        }
               
        if (txtBx != null && cbl != null && txtBx && cbl)
        {
            var text4TB = '';
                                                                                                                  
            var labels = cbl.getElementsByTagName('label'); 
                    
            var checkBoxes = cbl.getElementsByTagName('input');
            
            // item lengths should be equal below - 1 label per checkbox
            var cbsLength = checkBoxes.length;  
            var labelsLength = labels.length;                
            
            if (cbsLength === labelsLength)
            {                                               
                for(var i = 0; i < cbsLength; i++)
                {                                
                    if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked)
                    {                                                
                        txtBx.style.fontStyle = "normal";
                        txtBx.style.color = "Black";
                        text4TB += labels[i].innerHTML + ', ';
                    }                                           
                }           
            }  
            labels = '';                                  
            txtBx.value = text4TB.substring(0,text4TB.length - 2);                
            txtBx.title = txtBx.value;                         
            
            if (txtBx.value.length == 0)
            {  
                txtBx.style.fontStyle = "italic";
                txtBx.style.color = "#990033";
                txtBx.value = "Click and Select Below";            
                txtBx.title = "";      
            }
        }
    }
    function FindCityS(tb)
    {         
      var cbl, txtBx;                 
    
      switch(tb)
      {
              case 'tb1': 
              {
                    txtBx = document.getElementById('<%=txtCityS.ClientID%>');
                    cbl = document.getElementById('<%=cbCityS.ClientID%>');
                
               }
                break;
       

                         
             default: break;           
        }
               
        if (txtBx != null && cbl != null && txtBx && cbl)
        {
            var text4TB = '';
                                                                                                                  
            var labels = cbl.getElementsByTagName('label'); 
                    
            var checkBoxes = cbl.getElementsByTagName('input');
            
            // item lengths should be equal below - 1 label per checkbox
            var cbsLength = checkBoxes.length;  
            var labelsLength = labels.length;                
            
            if (cbsLength === labelsLength)
            {                                               
                for(var i = 0; i < cbsLength; i++)
                {                                
                    if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked)
                    {                                                
                        txtBx.style.fontStyle = "normal";
                        txtBx.style.color = "Black";
                        text4TB += labels[i].innerHTML + ', ';
                    }                                           
                }           
            }          
            labels='';                          
            txtBx.value = text4TB.substring(0,text4TB.length - 2);                
            txtBx.title = txtBx.value;                         
            
            if (txtBx.value.length == 0)
            {  
                txtBx.style.fontStyle = "italic";
                txtBx.style.color = "#990033";
                txtBx.value = "Click and Select Below";            
                txtBx.title = "";      
            }
        }
    }
    
    </script>--%>

    <cc1:TabContainer ID="TabContainer1" Width="950px" runat="server" ActiveTabIndex="0"
        Font-Names="Arial" Font-Size="8pt">
                <cc1:TabPanel runat="server" HeaderText="Single" ID="TabPanel1">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr style="background-color: #719ddb;">
                                            <td align="center" colspan="4">
                                                <asp:Label ID="Label2" Font-Names="calibri" Font-Size="12pt" runat="server" Text="Inward Details"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <table>
                                                <tr>
                                                        <td align="right" >
                                                            <asp:Label ID="Label19" Font-Names="arial" Font-Size="8pt" runat="server" Text="Inward Date :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDateS" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                                Width="200px" BackColor="#CCC8E2"></asp:TextBox> 
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateS"  Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                            <cc1:FilteredTextBoxExtender ID="FTEiDate" TargetControlID="txtDateS" FilterType="Custom" ValidChars="0123456789/" runat="server"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label3" Font-Names="arial" Font-Size="8pt" runat="server" Text="Select Job No :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="drJobNo" Font-Names="arial" Font-Size="8pt" runat="server"
                                                                Width="200px" AutoPostBack="True" 
                                                                onselectedindexchanged="drJobNo_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px;">
                                                            <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                            <asp:Label
                                                                ID="Label5" Font-Names="arial" Font-Size="8pt" runat="server" Text="Consignee Name :"
                                                                Height="16px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtConsignee" Font-Names="arial" Font-Size="8pt" runat="server"
                                                                Width="200px" BackColor="#CCC8E2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                                                    runat="server" ControlToValidate="txtConsignee" ErrorMessage="*" Font-Names="arial"
                                                                    Font-Size="8pt" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label
                                                                ID="Label7" Font-Names="arial" Font-Size="8pt" runat="server" Text="City :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCityS" Font-Names="arial" Font-Size="8pt" runat="server" Width="200px"
                                                                Style="margin-left: 0px" ></asp:TextBox>
                                                                    
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label
                                                                ID="Label9" Font-Names="arial" Font-Size="8pt" runat="server" Text="AWB Number:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAWBS" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                                Width="200px" BackColor="#CCC8E2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAWBS" ErrorMessage="*"
                                                                Font-Names="arial" Font-Size="8pt" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label11" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label
                                                                ID="Label12" Font-Names="arial" Font-Size="8pt" runat="server" Text="Received By :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceivedByS" Font-Names="arial" Font-Size="8pt" runat="server"
                                                                Width="200px" BackColor="#CCC8E2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                                                    runat="server" ControlToValidate="txtReceivedByS" ErrorMessage="*" Font-Names="arial"
                                                                    Font-Size="8pt" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label13" runat="server" Font-Names="arial" Font-Size="8pt" Text="Received Time :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTimeS" runat="server" Font-Names="arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top; width: 132px;" align="right">
                                                            <asp:Label ID="Label14" Font-Names="arial" Font-Size="8pt" runat="server" Text="if any Remarks :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRmksS" Font-Names="arial" Font-Size="8pt" runat="server" Width="200px"
                                                                Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="vertical-align: top; width: 132px;">
                                                            <asp:Label ID="Label15" runat="server" Text="Document Details :" 
                                                                Font-Names="arial" Font-Size="8pt" Width="100px"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top;">
                                                            <asp:TextBox ID="txtDDetailsS" Font-Names="arial" Font-Size="8pt" Height="100px"
                                                                runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            &#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                                            <asp:Button ID="BtnSubmitS" runat="server" Text="Submit" Height="20px" Width="60px"
                                                                BackColor="#FFE0C0" BorderColor="#FFC0C0" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="Arial" Font-Size="8pt" OnClick="BtnSubmitS_Click" 
                                                                ValidationGroup="a" />&#160;<asp:Button
                                                                    ID="BtnCancelS" runat="server" Text="Cancel" Height="20px" Width="60px" BackColor="#FFE0C0"
                                                                    BorderColor="#FFC0C0" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                                                                    Font-Size="8pt" OnClick="BtnCancel_Click" CausesValidation="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="* Fields are Indicate Mandatory"
                                                                Width="200px" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <table>
                                                    <tr>
                                                        <td align="left" style="vertical-align: top;">
                                                            <asp:Label ID="Label17" Font-Names="arial" Font-Size="8pt" runat="server" Text="Document List : -"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            CellPadding="4" Font-Names="Arial" Font-Size="8pt" GridLines="Horizontal" Width="154px"
                                                                            ShowHeader="False">
                                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSel1S" Font-Names="arial" Font-Size="7px" AutoPostBack="true"
                                                                                            OnCheckedChanged="chkSel1S_CheckedChanged" Width="20px" runat="server" /></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="DocumentName" SortExpression="DocumentName">
                                                                                    <ItemStyle Wrap="false" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                    <td>
                                                                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            CellPadding="4" Font-Names="Arial" Font-Size="8pt" GridLines="Horizontal" Width="154px"
                                                                            Font-Strikeout="False" Height="157px" ShowHeader="False">
                                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSel2S" Width="20px" Font-Names="arial" Font-Size="7px" AutoPostBack="true"
                                                                                            OnCheckedChanged="chkSel2S_CheckedChanged" runat="server" /></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="t1" SortExpression="t1">
                                                                                    <ItemStyle Wrap="false" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetName" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtReceivedByS">
                                    </cc1:AutoCompleteExtender>
                                
                            </ContentTemplate>
</asp:UpdatePanel>

                        </td>
                    </tr>
                </table>
            
            
        
            
            
        
        </ContentTemplate>
        

</cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Multiple">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr style="background-color: #719ddb;">
                                            <td align="center" colspan="4">
                                                <asp:Label ID="Label20" Font-Names="calibri" Font-Size="12pt" runat="server" Text="Inward Details"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <table>
                                                <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label18" Font-Names="arial" Font-Size="8pt" runat="server" Text="Inward Date :"></asp:Label>
                                                        </td>
                                                        <td >
                                                            <asp:TextBox ID="txtDate" Font-Names="arial" BackColor="#ccc8e2"  
                                                                Font-Size="8pt" runat="server" Width="200px" 
                                                                ></asp:TextBox> 
                                                            <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtDate"  Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtDate" FilterType="Custom" ValidChars="0123456789/" runat="server"> </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label21" Font-Names="arial" Font-Size="8pt" runat="server" Text="Select Consignee :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="drConsignee" runat="server" Font-Names="arial" Font-Size="8pt" 
                                                                Style="margin-left: 0px" Width="200px" 
                                                                ontextchanged="drConsignee_TextChanged"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetPartyMaster" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="drConsignee">
                                    </cc1:AutoCompleteExtender>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px;">
                                                            <asp:Label
                                                                ID="Label23" Font-Names="arial" Font-Size="8pt" runat="server" Text="City :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCity" Font-Names="arial" Font-Size="8pt" runat="server" Width="200px"
                                                                Style="margin-left: 0px" ></asp:TextBox>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label
                                                                ID="Label22" Font-Names="arial" Font-Size="8pt" runat="server" Text="Job No. :"
                                                                Height="16px"></asp:Label>
                                                                    
                                                        </td>
                                                        <td>
                                                                    
                                                            <asp:TextBox ID="txtJobs" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                                Width="200px" BackColor="#CCC8E2"></asp:TextBox>
                                                                    
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label31" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label
                                                                ID="Label24" Font-Names="arial" Font-Size="8pt" runat="server" Text="AWB Number:"></asp:Label>
                                                        </td>
                                                        <td>
                                                           
                                                            <asp:TextBox ID="txtAWB" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                                Width="200px" BackColor="#CCC8E2"></asp:TextBox>
                                                           
                                                            <asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAWB" ErrorMessage="*"
                                                                Font-Names="arial" Font-Size="8pt" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label32" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label
                                                                ID="Label25" Font-Names="arial" Font-Size="8pt" runat="server" Text="Received By :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceivedBy" Font-Names="arial" Font-Size="8pt" runat="server"
                                                                Width="200px" BackColor="#CCC8E2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                                                    runat="server" ControlToValidate="txtReceivedBy" ErrorMessage="*" Font-Names="arial"
                                                                    Font-Size="8pt" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label29" runat="server" Font-Names="arial" Font-Size="8pt" Text="Received Time :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTime" runat="server" Font-Names="arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="vertical-align: top; width: 132px;">
                                                            <asp:Label ID="Label26" Font-Names="arial" Font-Size="8pt" runat="server" Text="if any Remarks :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRmks" Font-Names="arial" Font-Size="8pt" runat="server" Width="200px"
                                                                Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="vertical-align: top; width: 132px;">
                                                            <asp:Label ID="Label27" Font-Names="arial" Font-Size="8pt" runat="server" Text="Document Details :"
                                                                Width="100px"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top;">
                                                            <asp:TextBox ID="txtDDetails" Font-Names="arial" Font-Size="8pt" Height="100px" runat="server"
                                                                Width="250px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Height="20px" Width="60px"
                                                                BackColor="#FFE0C0" BorderColor="#FFC0C0" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="Arial" Font-Size="8pt" OnClick="BtnSubmit_Click" ValidationGroup="b" />
                                                            &nbsp;<asp:Button
                                                                    ID="BtnCancel" runat="server" Text="Cancel" Height="20px" Width="60px" BackColor="#FFE0C0"
                                                                    BorderColor="#FFC0C0" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                                                                    Font-Size="8pt" OnClick="BtnCancel_Click" CausesValidation="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="* Fields are Indicate Mandatory"
                                                                Width="200px" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <table>
                                                    <tr>
                                                        <td align="left" style="vertical-align: top;">
                                                            <asp:Label ID="Label28" Font-Names="arial" Font-Size="8pt" runat="server" Text="Document List : -"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            CellPadding="4" Font-Names="Arial" Font-Size="8pt" GridLines="Horizontal" Width="154px"
                                                                            ShowHeader="False">
                                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSel1" Font-Names="arial" Font-Size="7px" AutoPostBack="true"
                                                                                            OnCheckedChanged="chkSel1_CheckedChanged" Width="20px" runat="server" /></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="DocumentName" SortExpression="DocumentName">
                                                                                    <ItemStyle Wrap="false" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                    <td>
                                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            CellPadding="4" Font-Names="Arial" Font-Size="8pt" GridLines="Horizontal" Width="154px"
                                                                            Font-Strikeout="False" Height="157px" ShowHeader="False">
                                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSel2" Width="20px" Font-Names="arial" Font-Size="7px" AutoPostBack="true"
                                                                                            OnCheckedChanged="chkSel2_CheckedChanged" runat="server" /></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="t1" SortExpression="t1">
                                                                                    <ItemStyle Wrap="false" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetName" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtReceivedBy">
                                    </cc1:AutoCompleteExtender>
                                
                            </ContentTemplate>
</asp:UpdatePanel>

                        </td>
                    </tr>
                </table>
            
            
        
            
            
        
        </ContentTemplate>
        

</cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Find">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr style="background-color: #719ddb;">
                                            <td align="center" colspan="2">
                                                <asp:Label ID="Label33" Font-Names="calibri" Font-Size="12pt" runat="server" Text="Inward Details"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <table>
                                                <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label6" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                                Text="Select Search :"></asp:Label>
                                                        </td>
                                                        <td >
                                                            <asp:RadioButtonList ID="rbSearch" runat="server" AutoPostBack="True" 
                                                                RepeatDirection="Horizontal" Font-Names="Arial" Font-Size="8pt" 
                                                                onselectedindexchanged="rbSearch_SelectedIndexChanged">
                                                                <asp:ListItem Value="AWB/BL No">BL No</asp:ListItem>
                                                                <asp:ListItem Value="Invoice  No">Invoice</asp:ListItem>
                                                                <asp:ListItem Value="Consignor Name">Consignor</asp:ListItem>
                                                            </asp:RadioButtonList>  
                                                            
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="lblH" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                                Text="AWB/BL No :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBL" runat="server" Font-Names="arial" Font-Size="8pt" 
                                                                Width="150px"></asp:TextBox>
                                                            <asp:Button ID="BtnFind" runat="server" Text="Search" CausesValidation="False" 
                                                                Font-Names="Arial" Font-Size="8pt" Width="60px" onclick="BtnFind_Click" />
                                                            <br />
                                                            <asp:Label ID="lblFResult" runat="server" Font-Names="Arial" Font-Size="7pt" 
                                                                ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label34" Font-Names="arial" Font-Size="8pt" runat="server" Text="Inward Date :"></asp:Label>
                                                        </td>
                                                        <td >
                                                            <asp:TextBox ID="txtDateF" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                                BackColor="#CCC8E2" Width="200px"></asp:TextBox> 
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateF"  Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtDateF" FilterType="Custom" ValidChars="0123456789/" runat="server"> </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label50" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                            <asp:Label ID="Label35" Font-Names="arial" Font-Size="8pt" runat="server" Text="Consignee :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtConsigneeF" runat="server" Font-Names="arial" Font-Size="8pt" 
                                                                Width="200px" BackColor="#CCC8E2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                                                ControlToValidate="txtConsigneeF" ErrorMessage="*" Font-Names="arial" 
                                                                Font-Size="8pt" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px;">
                                                            <asp:Label
                                                                ID="Label37" Font-Names="arial" Font-Size="8pt" runat="server" Text="City :"></asp:Label>
                                                                    
                                                        </td>
                                                        <td>
                                                                    
                                                            <asp:TextBox ID="txtCityF" runat="server" Font-Names="arial" Font-Size="8pt" 
                                                                Width="200px" ></asp:TextBox>
                                                                    
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label38" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                            <asp:Label
                                                                ID="Label39" Font-Names="arial" Font-Size="8pt" runat="server" Text="Job No. :"
                                                                Height="16px"></asp:Label>
                                                        </td>
                                                        <td>
                                                           
                                                            <asp:TextBox ID="txtJobNoF" runat="server" Font-Names="arial" Font-Size="8pt" 
                                                                Width="200px" BackColor="#CCC8E2"></asp:TextBox>
                                                           
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                                                ControlToValidate="txtJobNoF" ErrorMessage="*" Font-Names="arial" 
                                                                Font-Size="8pt" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label40" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label
                                                                ID="Label41" Font-Names="arial" Font-Size="8pt" runat="server" Text="AWB Number:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAWBF" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                                Width="200px" BackColor="#CCC8E2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtAWBF" ErrorMessage="*"
                                                                Font-Names="arial" Font-Size="8pt" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label42" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label
                                                                ID="Label43" Font-Names="arial" Font-Size="8pt" runat="server" Text="Received By :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceivedByF" Font-Names="arial" Font-Size="8pt" runat="server"
                                                                Width="200px" BackColor="#CCC8E2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                                                    runat="server" ControlToValidate="txtReceivedByF" ErrorMessage="*" Font-Names="arial"
                                                                    Font-Size="8pt" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 132px">
                                                            <asp:Label ID="Label44" runat="server" Font-Names="arial" Font-Size="8pt" Text="Received Time :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTimeF" runat="server" Font-Names="arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="vertical-align: top; width: 132px;">
                                                            <asp:Label ID="Label45" Font-Names="arial" Font-Size="8pt" runat="server" Text="if any Remarks :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRmksF" Font-Names="arial" Font-Size="8pt" runat="server" Width="200px"
                                                                Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="vertical-align: top; width: 132px;">
                                                            <asp:Label ID="Label46" Font-Names="arial" Font-Size="8pt" runat="server" Text="Document Details :"
                                                                Width="100px"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top;">
                                                            <asp:TextBox ID="txtDDetailsF" Font-Names="arial" Font-Size="8pt" Height="100px" runat="server"
                                                                Width="250px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="BtnSubmitF" runat="server" Text="Submit" Height="20px" Width="60px"
                                                                BackColor="#FFE0C0" BorderColor="#FFC0C0" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="Arial" Font-Size="8pt" OnClick="BtnSubmitF_Click" 
                                                                ValidationGroup="c" />
                                                            &nbsp;<asp:Button
                                                                    ID="BtnCancelF" runat="server" Text="Cancel" Height="20px" Width="60px" BackColor="#FFE0C0"
                                                                    BorderColor="#FFC0C0" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                                                                    Font-Size="8pt" OnClick="BtnCancelF_Click" CausesValidation="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="Label47" runat="server" ForeColor="Red" Text="* Fields are Indicate Mandatory"
                                                                Width="200px" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <table>
                                                    <tr>
                                                        <td align="left" style="vertical-align: top;">
                                                            <asp:Label ID="Label48" Font-Names="arial" Font-Size="8pt" runat="server" Text="Document List : -"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            CellPadding="4" Font-Names="Arial" Font-Size="8pt" GridLines="Horizontal" Width="154px"
                                                                            ShowHeader="False">
                                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSel1F" Font-Names="arial" Font-Size="7px" AutoPostBack="true"
                                                                                            OnCheckedChanged="chkSel1F_CheckedChanged" Width="20px" runat="server" /></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="DocumentName" SortExpression="DocumentName">
                                                                                    <ItemStyle Wrap="false" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                    <td>
                                                                        <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            CellPadding="4" Font-Names="Arial" Font-Size="8pt" GridLines="Horizontal" Width="154px"
                                                                            Font-Strikeout="False" Height="157px" ShowHeader="False">
                                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSel2F" Width="20px" Font-Names="arial" Font-Size="7px" AutoPostBack="true"
                                                                                            OnCheckedChanged="chkSel2F_CheckedChanged" runat="server" /></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="t1" SortExpression="t1">
                                                                                    <ItemStyle Wrap="false" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetName" ServicePath="~/AutoComplete.asmx"
                                        TargetControlID="txtReceivedByF">
                                    </cc1:AutoCompleteExtender>
                                
                            </ContentTemplate>
</asp:UpdatePanel>

                        </td>
                    </tr>
                </table>
            
            
        
            
            
        
        </ContentTemplate>
        

</cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
