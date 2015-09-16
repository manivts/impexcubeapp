<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmSingleJobSync.aspx.cs" Inherits="ImpexCube.frmSingleJobSync" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<div>
        <table style="z-index: auto; top: 5px; right: 5px; height:5px; position: absolute;">
        <tr>
        <td>
        
        </td>
        </tr>
        </table>
<asp:UpdatePanel ID="a" runat="server">
<ContentTemplate>
<table style="background-color: #f0f5f9; width: 100%; height: 130px;">
<tr style="background-color: #f0f5f9;">
<td align="left"  style=" vertical-align: top; width: 961px; height: 126px;">
 <table>
 <tr>
 <td align="center" colspan="6" >
     <asp:Label ID="Label1" runat="server" Text="PIPL - Job Sync" 
         Font-Names="Calibri" Font-Size="13pt" ForeColor="#2461BF"></asp:Label>
 </td>
 </tr>
 <tr>
 <td align="left" >
 <asp:Label ID="Label2" runat="server" Text="Enter Job Number" Font-Names="Arial" Font-Size="8pt" Width="97px"></asp:Label>
 </td>
 <td>
 <asp:Label ID="Label3" runat="server" Text=":" Font-Names="Arial" Font-Size="8pt"></asp:Label>
 </td>
 <td align="left" style="width: 326px" >
     <asp:DropDownList ID="drJOBNO" Font-Names="Verdana" Font-Size="8pt" 
         runat="server" Width="200px">
     </asp:DropDownList>
         <asp:Button ID="BtnSync" runat="server" Font-Names="Verdana" Font-Size="8pt" 
         Text="Sync  " onclick="BtnSync_Click" />
     </td>
 <td align="left" >
     <asp:Label ID="ld1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Date"></asp:Label></td>
 <td>
 <asp:Label ID="ld2" runat="server" Text=":" Font-Names="Arial" Font-Size="8pt"></asp:Label>
 </td>
 <td>
       <asp:TextBox ID="txtjDate" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"></asp:TextBox>

 </td>
 </tr>
 <tr id="tr1" runat="server" >
 <td align="left" >
     <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Party Name"></asp:Label></td>
 <td>
  <asp:Label ID="Label5" runat="server" Text=":" Font-Names="Arial" Font-Size="8pt"></asp:Label>

 </td>
 <td align="left" style="width: 326px" >
      <asp:TextBox ID="txtPName" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Width="230px"></asp:TextBox>
 </td>
 
 <td align="left" >
     <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Shipment Type"></asp:Label></td>
 <td>
 <asp:Label ID="Label7" runat="server" Text=":" Font-Names="Arial" Font-Size="8pt"></asp:Label>
 </td>
 <td>
       <asp:TextBox ID="txtStype" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"></asp:TextBox>

 </td>
 
 
 </tr>
 <tr id="tr2" runat="server" >
 <td align="left" >
     <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Invoice Details"></asp:Label></td>
 <td>
  <asp:Label ID="Label9" runat="server" Text=":" Font-Names="Arial" Font-Size="8pt"></asp:Label>

 </td>
 <td align="left" style="width: 326px" >
      <asp:TextBox ID="txtINVDesc" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Width="230px"></asp:TextBox>
 </td>
 
 <td align="left" >
     <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="BE Type"></asp:Label></td>
 <td>
 <asp:Label ID="Label11" runat="server" Text=":" Font-Names="Arial" Font-Size="8pt"></asp:Label>
 </td>
 <td>
       <asp:TextBox ID="txtBEType" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Font-Bold="False"></asp:TextBox>

 </td>
 
 
 </tr>
 </table>   
    
 </td>
 <table>
 <tr>
    <td  style="width:961px" align="center">
                                                                            <asp:UpdateProgress ID="UpdateProgress6" runat="server">
                                                                                <ProgressTemplate>
                                                                                    <span style="font-family: Calibri; font-size: 12px; color: #2461BF;">Please Wait ...
                                                                                        <img src="../../images/progress.gif"></span></ProgressTemplate>
                                                                            </asp:UpdateProgress>
                                                                        </td>
 </tr>
                    </table>
</tr>

<tr id="trGrid" runat="server" >
<td style="width: 961px">

</td>
</tr>
<tr id="trBtn" style="background-color: #f0f5f9;" runat="server" >
<td align="left" style="width: 961px">
    </td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>


 </div>
</asp:Content>
