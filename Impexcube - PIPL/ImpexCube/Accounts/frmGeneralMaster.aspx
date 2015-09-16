<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="frmGeneralMaster.aspx.cs" Inherits="ImpexCube.Accounts.frmGeneralMaster" %>
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
           padding-left:100px;
            width:114px;
        }
        .Column2
        {
            height:18px;
            width:400px;
            padding-left:220px;
            margin-top:-15px;
        }
        .Column2.1
        {
             padding-left:320px;
            width:114px;   
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
            padding-left:670px;
            margin-top:-15px;
            height:30px;
        }
        .Column10
        {
            
            width:400px;
            padding-left:120px;
            margin-top:-15px;
        }
        .Column40
        {
            
            width:200px;
            padding-left:670px;
            margin-top:-15px;
            height:50px;
        }
          .MulDiv
        {
            width:120px;
            float:none;
            margin-top:7px;
             margin-left:-121px;
        }
        .BrnchSrNo
        {
             float:none;
             margin-top:-19px;
            height: 11px;
        }
    </style>
    <script type="text/javascript">

        function accountname() {debugger
            var first = document.getElementById('ContentPlaceHolder1_txtName').value;
            var upper1 = first.toUpperCase();
            document.getElementById('ContentPlaceHolder1_txtName').value = upper1;
            //var second = document.getElementById('ContentPlaceHolder1_txtBranchName').value;
            //var third = document.getElementById('ContentPlaceHolder1_txtTallyAccountName').value;                      
            //document.getElementById('ContentPlaceHolder1_txtBranchName').value=first;
            var short = first.substring(0, 4);
            document.getElementById('ContentPlaceHolder1_txtShortName').value = short;
            document.getElementById('ContentPlaceHolder1_txtRefName').value = short;
            document.getElementById('ContentPlaceHolder1_txtTallyAccountName').value = upper1;
            return false;

        }

    
        function shortname() {debugger
            var name = document.getElementById('ContentPlaceHolder1_txtName').value;
            var upper = document.getElementById('ContentPlaceHolder1_txtName').value;
            var short = name.substring(0, 4);
            document.getElementById('ContentPlaceHolder1_txtShortName').value = short;
            document.getElementById('ContentPlaceHolder1_txtRefName').value = short ;
            var conupper = upper.toUpperCase();
            document.getElementById('ContentPlaceHolder1_txtName').value = conupper;
            //document.getElementById('ContentPlaceHolder1_txtBranchName').value = name;
            document.getElementById('ContentPlaceHolder1_txtTallyAccountName').value = conupper;
        }
        function valsave() {
            debugger

            if (document.getElementById('ContentPlaceHolder1_txtName').value == "") {
                alert('Please Enter The Name');
                document.getElementById('ContentPlaceHolder1_txtName').focus();
                return false;
            }
            var ddlcountry = document.getElementById('ContentPlaceHolder1_ddlCountry');
            var selectedText = ddlcountry.options[ddlcountry.selectedIndex].text;

//            if (document.getElementById('ContentPlaceHolder1_txtBranchName').value == "") {
//                alert('Please Enter The Branch Name');
//                document.getElementById('ContentPlaceHolder1_txtBranchName').focus();
//                return false;
//            }
//            if (document.getElementById('ContentPlaceHolder1_ddlCountry').selectedText == "~Select~") {
//                alert('Please Select Country');
//                document.getElementById('ContentPlaceHolder1_ddlCountry').focus();
//                return false;
//            }
            if (document.getElementById('ContentPlaceHolder1_txtBranchSrNo').value == "") {
                alert('Please enter Branch No');
                document.getElementById('ContentPlaceHolder1_txtBranchSrNo').focus();
                return false;
            }
//            if (document.getElementById('ContentPlaceHolder1_txtOpeninBalance').value == "") {
//                alert('Please enter opening balance');
//                document.getElementById('ContentPlaceHolder1_txtOpeninBalance').focus();
//                return false;
//            }
        }
        function branchval() {
            var ddlcountry = document.getElementById('ContentPlaceHolder1_ddlCountry');
            var selectedText = ddlcountry.options[ddlcountry.selectedIndex].text;

            if (document.getElementById('ContentPlaceHolder1_txtBranchName').value == "") {
                alert('Please Enter The Branch Name');
                document.getElementById('ContentPlaceHolder1_txtBranchName').focus();
                return false;
            }
            if (selectedText == "~Select~") {
                alert('Please Select Country');
                document.getElementById('ContentPlaceHolder1_ddlCountry').focus();
                return false;
            }
        }

//        function filter() {
//            var txtval = document.getElementById('ContentPlaceHolder1_txtSearch').value;
//            window.open("frmGeneralMaster.aspx?SearchText=" + txtval );
//        }

        function exit()
         {
            var status = confirm("Do You Want To Exit!");
            if (status == true) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<div>

  <asp:panel ID="panelContent" runat="server">
    <div id="div1">
             <div  class="alignment labelsize Column1"> Ledger Name</div>  
             <div id="divtxtName" class="alignment labelsize Column2">
             <asp:TextBox ID="txtName" runat="server" CssClass="txtbox150" Width="286px" onkeypress="javascript: return shortname();"                     
                    onblur="javascript: return accountname();" ></asp:TextBox><font color="red"><strong>*</strong></font>
              </div>
              <div class="alignment labelsize Column3" >Alias Name</div>
              <div id="divtxtShortName" class="alignment labelsize Column4"> <asp:TextBox ID="txtShortName" runat="server" CssClass="txtbox150" ></asp:TextBox></div>
    </div>

    <div id="div2">
             <div  class="alignment labelsize Column1"> Accounts Group</div>  
             <div id="div4" class="alignment labelsize Column2">
              <asp:DropDownList ID="ddlAccountGroup" runat="server" CssClass="ddl200" width="286px"
                    Visible="true">
                </asp:DropDownList>
              </div>
              <div class="alignment labelsize Column3" >Currency</div>
              <div id="div5" class="alignment labelsize Column4"> 
                <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="ddl150" >
                </asp:DropDownList>
                </div>
    </div>
    <div id="div3">
             <div  class="alignment labelsize Column1"> Credit Period</div>  
             <div id="div7" class="alignment labelsize Column2">
             <asp:DropDownList ID="ddlPaymentPeriod" runat="server" CssClass="ddl150" 
                    onselectedindexchanged="ddlPaymentPeriod_SelectedIndexChanged" 
                     Height="24px" Width="76px" >
                    <asp:ListItem Value="~Select~" Selected="True">~Select~</asp:ListItem>
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="15">15</asp:ListItem>
                    <asp:ListItem Value="30">30</asp:ListItem>
                    <asp:ListItem Value="45">45</asp:ListItem>
                    <asp:ListItem Value="60">60</asp:ListItem>
                    <asp:ListItem Value="75">75</asp:ListItem>
                    <asp:ListItem Value="90">90</asp:ListItem>
                </asp:DropDownList>
              </div>
              <div class="alignment labelsize Column3" >Credit Limit</div>
              <div id="div8" class="alignment labelsize Column4"> <asp:TextBox ID="txtCreditLimit" runat="server" CssClass="txtbox150" 
                   ></asp:TextBox></div>
    </div>
    <div id="div4">
             <div  class="alignment labelsize Column1"> Tally Account Name </div>  
             <div id="div10" class="alignment labelsize Column2">
                 <asp:TextBox ID="txtTallyAccountName" runat="server" CssClass="txtbox150" 
                     Width="286px"></asp:TextBox>
             
              </div>
              <div class="alignment labelsize Column3" >Mobile Number</div>
              <div id="div11" class="alignment labelsize Column4"> <asp:TextBox ID="txtMobileNo" 
                      runat="server" CssClass="txtbox150" ></asp:TextBox></div>
    </div>
    
    <div id="div39">
             <div  class="alignment labelsize Column1">Address 1 </div>  
             <div id="div40" class="alignment labelsize Column2">
             <asp:TextBox ID="txtAddress1" runat="server" CssClass="txtbox300" placeholder="Address 1"
                    TextMode="MultiLine"  Height="37px" 
                     Width="150px"  ></asp:TextBox>
              </div>

              <div class="alignment labelsize Column3" >Address 2</div>
              <div id="div41" class="alignment labelsize Column40"> 
                
                  <asp:TextBox ID="txtAddress2" 
                      runat="server" CssClass="txtbox300" placeholder="Address 2"
                    TextMode="MultiLine"  Height="37px" 
                     Width="150px" ></asp:TextBox>
                    <br />
                  
             </div>
    </div>

     <div id="div17">
            <div  class="alignment labelsize Column1"> Address 3</div>  
             <div id="div18" class="alignment labelsize Column2">
             <asp:TextBox ID="txtAddress3" runat="server" CssClass="txtbox300" placeholder="Address 3"
                    TextMode="MultiLine"   Height="37px" 
                     Width="150px" ></asp:TextBox>
              </div>
             

             <div class="alignment labelsize Column3" >Phone No </div>

             <div class="alignment labelsize Column40">
               <div id="div37" > <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtbox150" ></asp:TextBox></div>
                   <div class="alignment labelsize MulDiv" >Branch Sr.No </div>
                    <div class="BrnchSrNo"> <asp:TextBox ID="txtBranchSrNo" runat="server" 
                            CssClass="txtbox150" Height="16px" ></asp:TextBox><asp:TextBox><font color="red"><strong>*</strong></font></asp:TextBox></div>                     
              </div>
    </div>
    
     <div id="div32">
     <div  class="alignment labelsize Column1"> Email Id </div>  
             <div id="div6" class="alignment labelsize Column2">
             <asp:TextBox ID="txtEmailId" runat="server" CssClass="txtbox150" ></asp:TextBox>
              </div>
    <div class="alignment labelsize Column3" > Website </div>
              <div id="div38" class="alignment labelsize Column4"> <asp:TextBox ID="txtWebsite" runat="server" CssClass="txtbox150"></asp:TextBox></div>
    </div>
    
     <div id="div7">
             <div  class="alignment labelsize Column1"> City </div>  
             <div id="div16" class="alignment labelsize Column2">
             <asp:TextBox ID="txtCity" runat="server" CssClass="txtbox150" ></asp:TextBox>
              </div>
              <div class="alignment labelsize Column3" > AD Code </div>
              <div id="div19" class="alignment labelsize Column4"> <asp:TextBox ID="txtADCode" runat="server" CssClass="txtbox150"></asp:TextBox></div>
    </div>
     <div id="div8">
             <div  class="alignment labelsize Column1"> State </div>  
             <div id="div21" class="alignment labelsize Column2">
             <asp:TextBox ID="txtState" runat="server" CssClass="txtbox150" 
                   ></asp:TextBox>
              </div>
              <div class="alignment labelsize Column3" > TIN/VAT/LST No </div>
              <div id="div22" class="alignment labelsize Column4"> <asp:TextBox ID="txtTINno" runat="server" CssClass="txtbox150"></asp:TextBox></div>
    </div>
     <div id="div9">
             <div  class="alignment labelsize Column1"> Pincode </div>  
             <div id="div24" class="alignment labelsize Column2"><asp:TextBox ID="txtPinCode" runat="server" CssClass="txtbox150" ></asp:TextBox>
              </div>
              <div class="alignment labelsize Column3" > Cst No </div>
              <div id="div25" class="alignment labelsize Column4"> <asp:TextBox ID="txtCSTNo" runat="server" CssClass="txtbox150"></asp:TextBox></div>
    </div>
     <div id="div10">
             <div  class="alignment labelsize Column1"> Country </div>  
             <div id="div27" class="alignment labelsize Column2"><asp:DropDownList ID="ddlCountry" runat="server" CssClass="ddl150" 
                    AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                    <asp:ListItem Value="~Select~">~Select~</asp:ListItem>
                </asp:DropDownList>
              </div>
              <div class="alignment labelsize Column3" > Pan No </div>
              <div id="div28" class="alignment labelsize Column4"> <asp:TextBox ID="txtPANNo" runat="server" CssClass="txtbox150" ></asp:TextBox></div>
    </div>
     <div id="div11">
             <div  class="alignment labelsize Column1"> Country Code </div>  
             <div id="div20" class="alignment labelsize Column2"><asp:TextBox ID="txtCountryCode" runat="server" CssClass="txtbox150"
                    ></asp:TextBox>
              </div>
              <div class="alignment labelsize Column3" > Serv Tax No </div>
              <div id="div23" class="alignment labelsize Column4"> <asp:TextBox ID="txtSTaxNo" runat="server" CssClass="txtbox150" ></asp:TextBox></div>
    </div>
     <div id="div12">
             <div  class="alignment labelsize Column1"> IE Code </div>  
             <div id="div29" class="alignment labelsize Column2"><asp:TextBox ID="txtIECode" runat="server" CssClass="txtbox150"></asp:TextBox>
              </div>
              <div class="alignment labelsize Column3" > Income Tax No </div>
              <div id="div30" class="alignment labelsize Column4"> <asp:TextBox ID="txtIncomeTaxNo" runat="server" CssClass="txtbox150"></asp:TextBox></div>
    </div>
     <div id="div13">
             <div  class="alignment labelsize Column1">  OP Balance </div>  
             <div id="div26" class="alignment labelsize Column2"><asp:TextBox ID="txtOpeninBalance" runat="server" CssClass="txtbox150"></asp:TextBox>
                 <asp:DropDownList ID="ddlCRDR" runat="server" CssClass="ddl50">
                    <asp:ListItem>Cr</asp:ListItem>
                    <asp:ListItem>Dr</asp:ListItem>
                </asp:DropDownList>
              </div>
              <div class="alignment labelsize Column3" > Contact Person </div>
              <div id="div31" class="alignment labelsize Column4"><asp:TextBox ID="txtContactPerson" runat="server" CssClass="txtbox150" ></asp:TextBox></div>
    </div>
     <div id="div14">
             <div  class="alignment labelsize Column1">  Cost Center Ref Name </div>  
             <div id="div33" class="alignment labelsize Column2"><asp:TextBox ID="txtRefName" runat="server" CssClass="txtbox150"></asp:TextBox>
                 <asp:CheckBox ID="ChkCostCenter" runat="server" CssClass="labelsize" 
                    Text="Cost Centers applicable ?" TextAlign="Left" />
              </div>
              <div id="div50" class="alignment labelsize Column3"></div>
              <div id="div34" class="alignment labelsize Column4"> </div>
    </div>
    </asp:panel>
   
    
     <div>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="txtbox300" Width="400px"></asp:TextBox>
              
                <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="Search"/>
                    </div>

   
     <div>

    <table align="center">

        <tr style="color:#E7E7FF;background-color:#2461BF;">

        <td>
                    <asp:GridView ID="gvDetails" runat="server" CssClass="table-wrapper" AutoGenerateSelectButton="True"
                        Width="600px" AutoGenerateColumns="False" HorizontalAlign="Center"  
                        OnSelectedIndexChanged="gvDetails_SelectedIndexChanged1" Font-Size="12px">
                        <Columns>
                            <asp:BoundField HeaderText="Id" DataField="TransId" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
<HeaderStyle HorizontalAlign="Center" CssClass="hiddencol"></HeaderStyle>

<ItemStyle CssClass="hiddencol" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Name" DataField="AccountName" HeaderStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle BackColor="White" />
                        <RowStyle CssClass="table-header light" />
                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black"/>
                        <AlternatingRowStyle BackColor="#E7E7FF" />
                    </asp:GridView>
                    </td>
                    </tr>
                    </table>
                    </div>
     <div>        
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn70" 
                    OnClick="btnNew_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn70" OnClientClick="javascript:return valsave();"
                    OnClick="btnUpdate_Click" Visible="False" />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn70" OnClientClick="javascript:return valsave();"
                    OnClick="btnSave_Click" Visible="false"/>
                <asp:Button ID="btnDiscard" runat="server" Text="Exit" CssClass="btn70" 
                    OnClick="btnDiscard_Click" OnClientClick="javascript:return exit();" />
       </div>  
    <input type="hidden" runat="server" id="hdnCommonMaster" />
    <input type="hidden" runat="server" id="hdnBranchMaster" />
</div>
</asp:Content>
