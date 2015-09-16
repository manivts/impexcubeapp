<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmCustomerMaster.aspx.cs" Inherits="ImpexCube.frmCustomerMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="js/New Folder/jquery-1.5.2.min.js"></script>
    <script type="text/javascript" src="js/New Folder/scriptbreaker-multiple-accordion-1.js"></script>
    <script src="js/js/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="js/js/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            $("#accordion").accordion({ collapsible: true });
            var availableTags = [
			"ActionScript",
			"AppleScript",
			"Asp",
			"BASIC",
			"C",
			"C++",
			"Clojure",
			"COBOL",
			"ColdFusion",
			"Erlang",
			"Fortran",
			"Groovy",
			"Haskell",
			"Java",
			"JavaScript",
			"Lisp",
			"Perl",
			"PHP",
			"Python",
			"Ruby",
			"Scala",
			"Scheme"
		];
            $("#autocomplete").autocomplete({
                source: availableTags
            });
            $("#button").button();
            $("#radioset").buttonset();
            $("#tabs").tabs();
            $("#dialog").dialog({
                autoOpen: true,
                width: 400,
                buttons: [
				{
				    text: "Ok",
				    click: function () {
				        $(this).dialog("close");
				    }
				},
				{
				    text: "Cancel",
				    click: function () {
				        $(this).dialog("close");
				    }
				}
			]
            });

            // Link to open the dialog
            $("#dialog-link").click(function (event) {
                $("#dialog").dialog("open");
                event.preventDefault();
            });
            $("#datepicker").datepicker({
                inline: true
            });
            // Horizontal Slider
            $('#horizSlider').slider({
                range: true,
                values: [17, 67]
            }).width(500);
            // Vertical Slider				
            $("#eq > span").each(function () {
                var value = parseInt($(this).text());
                $(this).empty().slider({
                    value: value,
                    range: "min",
                    animate: true,
                    orientation: "vertical"
                });
            });



            $("#progressbar").progressbar({
                value: 70
            });

            // Icon Buttons
            $("#leftIconButton").button({
                icons: {
                    primary: 'ui-icon-info'
                }
            });

            $("#bothIconButton").button({
                icons: {
                    primary: 'ui-icon-folder-collapsed',
                    secondary: 'ui-icon-triangle-1-s'
                }
            });
            // Hover states on the static widgets
            $("#dialog-link, #icons li").hover(
			function () {
			    $(this).addClass("ui-state-hover");
			},
			function () {
			    $(this).removeClass("ui-state-hover");
			}
		);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="container">
        <div class="container-area">
            <div id="col-2ex">
                <div class="d">
                    <div class="c-ala">
                        <div class="c-aEx1">
                            <div class="content-work-extended">
                                <div class="c-s-b1">
                                    Customer Name :</div>
                                <div class="c-s-b2fixedaEx">
                                    <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="txtCode" ReadOnly="true"></asp:TextBox>
                                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="postmsgg234 required"
                                        OnTextChanged="txtCustomerName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="c-aWithouthight">
                        <div id="accordion">
                            <h3>
                                Customer Details</h3>
                            <div class="c-aEx">
                                <div class="c-accordion">
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            Branch :</div>
                                        <div class="c-s-b2we">
                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="spanddl required">
                                                <asp:ListItem>-Select-</asp:ListItem>
                                                <asp:ListItem>HO</asp:ListItem>
                                                <asp:ListItem>Branch</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            IEC Code :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtIECCode" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            Type of Company :</div>
                                        <div class="c-s-b2we">
                                            <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="spanddl required">
                                                <asp:ListItem>-Select-</asp:ListItem>
                                                <asp:ListItem>Public</asp:ListItem>
                                                <asp:ListItem>Private</asp:ListItem>
                                                <asp:ListItem>Goverment</asp:ListItem>
                                                <asp:ListItem>others</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordion">
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            Address :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            TIN No :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtTin" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            IT PAN :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtITPan" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordion">
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            City :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            BIN No :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtBin" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            AD Code No :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtAdCode" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordion">
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            Zip :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtZip" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            Importer Type :</div>
                                        <div class="c-s-b2we">
                                            <asp:DropDownList ID="ddlImporterType" runat="server" CssClass="spanddl required">
                                                <asp:ListItem>-Select-</asp:ListItem>
                                                <asp:ListItem>Goverment Departments</asp:ListItem>
                                                <asp:ListItem>Goverment Undertaking</asp:ListItem>
                                                <asp:ListItem>Private</asp:ListItem>
                                                <asp:ListItem>others</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            TAN No :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtTan" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordion">
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            State :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtState" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            Exporter Type :</div>
                                        <div class="c-s-b2we">
                                            <asp:DropDownList ID="ddlExporterType" runat="server" CssClass="spanddl required">
                                                <asp:ListItem>-Select-</asp:ListItem>
                                                <asp:ListItem>Private</asp:ListItem>
                                                <asp:ListItem>Goverment</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            Imp/Exp Type :</div>
                                        <div class="c-s-b2we">
                                            <asp:DropDownList ID="ddlImpExpType" runat="server" CssClass="spanddl required">
                                                <asp:ListItem>-Select-</asp:ListItem>
                                                <asp:ListItem>Merchant Exporter</asp:ListItem>
                                                <asp:ListItem>MFG. Exporter</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordion">
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            Country :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            Credit Limit Rs :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtCredit" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            No of Days :</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtNOD" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordion">
                                    <div class="content-work-box1accright">
                                        <div class="c-s-b1">
                                            Sales Represetative:</div>
                                        <div class="c-s-b2we">
                                            <asp:TextBox ID="txtSalesRep" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordion">
                                    <div class="cs-accbutton">
                                        <div style="float: right;">
                                            <asp:Button ID="btnSaveDetails" runat="server" Text="Save" CssClass="orange" OnClick="btnSaveDetails_Click">
                                            </asp:Button>
                                            <asp:Button ID="btnExit" runat="server" Text="Cancel" CssClass="orange"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h3>
                                Customer Branch</h3>
                            <div class="c-aEx">
                                <div class="c-accordion">
                                    <div class="menu-listacc">
                                        <div class="c-acc">
                                            <div class="m-l1accor">
                                                Branch</div>
                                            <div class="m-l3acc">
                                                Street</div>
                                            <div class="m-l3acc">
                                                Area</div>
                                            <div class="m-l1">
                                                City</div>
                                            <div class="m-l1">
                                                State</div>
                                            <div class="m-l1accor">
                                                Country</div>
                                            <div class="m-l1accor1">
                                                Add</div>
                                        </div>
                                    </div>
                                    <div class="menu-listacc">
                                        <div class="c-acctext">
                                            <div class="m-l1acc1">
                                                <asp:DropDownList ID="ddlCustomerBranch" runat="server" CssClass="span3vacc required">
                                                    <asp:ListItem>-Select-</asp:ListItem>
                                                    <asp:ListItem>HO</asp:ListItem>
                                                    <asp:ListItem>Branch</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="m-l1accs">
                                                <asp:TextBox ID="txtBranchStreet" runat="server" CssClass="postmsgg240 required"></asp:TextBox>
                                            </div>
                                            <div class="m-l1accs">
                                                <asp:TextBox ID="txtBranchArea" runat="server" CssClass="postmsgg240 required"></asp:TextBox>
                                            </div>
                                            <div class="m-l1acc">
                                                <asp:TextBox ID="txtBranchCity" runat="server" CssClass="postmsgg236 required"></asp:TextBox>
                                            </div>
                                            <div class="m-l1acc">
                                                <asp:TextBox ID="txtBranchState" runat="server" CssClass="postmsgg236 required"></asp:TextBox>
                                            </div>
                                            <div class="m-l1acc1">
                                                <asp:TextBox ID="txtBranchCountry" runat="server" CssClass="postmsgg236a required"></asp:TextBox>
                                            </div>
                                            <div class="m-l1acc1" style="text-align: center;">
                                                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/image/Add.jpg" Style="height: 24px;"
                                                    OnClick="btnAdd_Click" />
                                                <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/image/Add.jpg" Style="height: 24px;"
                                                    Visible="false" OnClick="btnUpdate_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="menu-listacc">
                                        <div class="c-acctext">
                                            <asp:GridView ID="gvBranchDetails" runat="server" Style="text-align: center" GridLines="Vertical"
                                                CssClass="table-wrapper" AutoGenerateColumns="False" Width="1000px" OnSelectedIndexChanged="gvBranchDetails_SelectedIndexChanged">
                                                <RowStyle CssClass="table-header light" />
                                                <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" ForeColor="#EE2521" />
                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                <AlternatingRowStyle BackColor="#E7E7FF" />
                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" HeaderText="Select" />
                                                    <asp:BoundField DataField="Id" HeaderText="Sl.No" ItemStyle-CssClass="hiddencol"
                                                        HeaderStyle-CssClass="hiddencol" />
                                                    <asp:BoundField DataField="Branch" HeaderText="Branch" />
                                                    <asp:BoundField DataField="Street" HeaderText="Street" />
                                                    <asp:BoundField DataField="Area" HeaderText="Area" />
                                                    <asp:BoundField DataField="City" HeaderText="City" />
                                                    <asp:BoundField DataField="State" HeaderText="State" />
                                                    <asp:BoundField DataField="Country" HeaderText="Country" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h3>
                                Customer Contacts</h3>
                            <div class="c-aEx1">
                                <div class="c-accordion1">
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            Branch :</div>
                                        <div class="c-s-b2we">
                                            <asp:DropDownList ID="ddlCompanyBranch" runat="server" CssClass="spanddl required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordion1">
                                    <div class="menu-listacc1">
                                        <div class="c-acc1">
                                            <div class="m-l1accor">
                                                Deparment</div>
                                            <div class="m-l1">
                                                Name</div>
                                            <div class="m-l1">
                                                Designation</div>
                                            <div class="m-l1">
                                                Phone</div>
                                            <div class="m-l1">
                                                Mobile</div>
                                            <div class="m-l1">
                                                Email</div>
                                            <div class="m-l1">
                                                Website</div>
                                            <div class="m-l1">
                                                Fax</div>
                                            <div class="m-l1accoradd">
                                                Add</div>
                                        </div>
                                    </div>
                                    <div class="menu-listacc1">
                                        <div class="c-acctext1">
                                            <div class="m-l1acc1">
                                                <asp:DropDownList ID="ddlCustomerDepartments" runat="server" CssClass="span3vacc required">
                                                    <asp:ListItem>-Select-</asp:ListItem>
                                                    <asp:ListItem>Commerical</asp:ListItem>
                                                    <asp:ListItem>Accounts</asp:ListItem>
                                                    <asp:ListItem>Sales</asp:ListItem>
                                                    <asp:ListItem>Purchase</asp:ListItem>
                                                    <asp:ListItem>Customer Service</asp:ListItem>
                                                    <asp:ListItem>Description</asp:ListItem>
                                                    <asp:ListItem>Transactions</asp:ListItem>
                                                    <asp:ListItem>Financial Details</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="m-l1acc">
                                                <asp:TextBox ID="txtStaffName" runat="server" CssClass="postmsgg236 required"></asp:TextBox>
                                            </div>
                                            <div class="m-l1acc">
                                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="postmsgg236 required"></asp:TextBox>
                                            </div>
                                            <div class="m-l1acc">
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="postmsgg236 required"></asp:TextBox>
                                            </div>
                                            <div class="m-l1acc">
                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="postmsgg236 required"></asp:TextBox>
                                            </div>
                                            <div class="m-l1acc">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="postmsgg236 required"></asp:TextBox>
                                            </div>
                                            <div class="m-l1acc">
                                                <asp:TextBox ID="txtWebsite" runat="server" CssClass="postmsgg236 required"></asp:TextBox>
                                            </div>
                                            <div class="m-l1acc">
                                                <asp:TextBox ID="txtFax" runat="server" CssClass="postmsgg236 required"></asp:TextBox>
                                            </div>
                                            <div class="m-l1accadd" style="text-align: center;">
                                                <asp:ImageButton ID="btnContactsSave" runat="server" ImageUrl="~/image/Add.jpg" Style="height: 24px;"
                                                    OnClick="btnContactsSave_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="menu-listacc1">
                                        <div class="c-acctext1">
                                            <asp:GridView ID="gvCustomerContacts" runat="server" Style="text-align: center" GridLines="Vertical"
                                                CssClass="table-wrapper" AutoGenerateColumns="False" Width="1160px" OnSelectedIndexChanged="gvCustomerContacts_SelectedIndexChanged">
                                                <RowStyle CssClass="table-header light" />
                                                <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" ForeColor="#EE2521" />
                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                <AlternatingRowStyle BackColor="#E7E7FF" />
                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" HeaderText="Select" />
                                                    <asp:BoundField DataField="Id" HeaderText="Sl.No" ItemStyle-CssClass="hiddencol"
                                                        HeaderStyle-CssClass="hiddencol" />
                                                    <asp:BoundField DataField="Branch" HeaderText="Branch" />
                                                    <asp:BoundField DataField="Department" HeaderText="Department" />
                                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                                    <asp:BoundField DataField="Website" HeaderText="Website" />
                                                    <asp:BoundField DataField="Fax" HeaderText="Fax" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h3>
                                User Templates</h3>
                            <div class="c-aEx1">
                                <div class="c-accordion">
                                    <div class="content-work-increase">
                                        <div class="c-s-b1">
                                            <asp:CheckBox ID="chkAutoMail" runat="server" Text="Automatic Mail" />
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordion">
                                    <div class="content-work-increase">
                                        <div class="c-s-b1">
                                            Timings :</div>
                                        <div class="c-s-b2fixeda">
                                            <asp:TextBox ID="txtTimings" runat="server" CssClass="txtTime"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordion">
                                    <div class="content-work-increase">
                                        <div class="c-s-b1">
                                            Department :</div>
                                        <div class="c-s-b2fixeda">
                                            <asp:DropDownList ID="ddlDepartments" runat="server" CssClass="span3vacc required">
                                                <asp:ListItem>-Select-</asp:ListItem>
                                                <asp:ListItem>Commerical</asp:ListItem>
                                                <asp:ListItem>Accounts</asp:ListItem>
                                                <asp:ListItem>Sales</asp:ListItem>
                                                <asp:ListItem>Purchase</asp:ListItem>
                                                <asp:ListItem>Customer Service</asp:ListItem>
                                                <asp:ListItem>Description</asp:ListItem>
                                                <asp:ListItem>Transactions</asp:ListItem>
                                                <asp:ListItem>Financial Details</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="content-work-box1we">
                                        <div class="c-s-b1">
                                            Report Type :</div>
                                        <div class="c-s-b2we">
                                            <asp:DropDownList ID="ddlReportType" runat="server" CssClass="span3vacc required">
                                                <asp:ListItem>-Select-</asp:ListItem>
                                                <asp:ListItem>PDF</asp:ListItem>
                                                <asp:ListItem>Excel</asp:ListItem>
                                                <asp:ListItem>CSV</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordion">
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            Employee Name</div>
                                    </div>
                                    <div class="content-work-box1acc1">
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b1">
                                            Email Id
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordiontemp">
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b2we">
                                            <asp:ListBox ID="lbEmployeeName" runat="server" CssClass="listbox"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc1">
                                        <div class="c-s-b2wev">
                                            <p>
                                                <asp:Button ID="btnMove" runat="server" Text=">" CssClass="orangeacc" 
                                                    onclick="btnMove_Click"></asp:Button></p>
                                            <p>
                                                <asp:Button ID="btnMoveAll" runat="server" Text=">>" CssClass="orangeacc" 
                                                    onclick="btnMoveAll_Click"></asp:Button></p>
                                            <p>
                                                <asp:Button ID="btnRemove" runat="server" Text="<" CssClass="orangeacc" 
                                                    onclick="btnRemove_Click"></asp:Button></p>
                                            <p>
                                                <asp:Button ID="btnRemoveAll" runat="server" Text="<<" CssClass="orangeacc" 
                                                    onclick="btnRemoveAll_Click"></asp:Button></p>
                                        </div>
                                    </div>
                                    <div class="content-work-box1acc">
                                        <div class="c-s-b2we">
                                            <asp:ListBox ID="lbEmailId" runat="server" CssClass="listbox"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="c-accordion">
                                    <div class="cs-accbutton">
                                        <div style="float: right;">
                                            <asp:Button ID="btnSaveTemplate" runat="server" Text="Save" CssClass="orange" 
                                                onclick="btnSaveTemplate_Click"></asp:Button>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="orange"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="d">
                    <asp:GridView ID="gvCustomer" runat="server" Style="text-align: center" GridLines="Vertical"
                        CssClass="table-wrapper" AutoGenerateColumns="False" Width="300px" AllowPaging="True"
                        OnSelectedIndexChanged="gvCustomer_SelectedIndexChanged">
                        <RowStyle CssClass="table-header light" />
                        <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" ForeColor="#EE2521" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <AlternatingRowStyle BackColor="#E7E7FF" />
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" Wrap="True" CssClass="table-footer" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" HeaderText="Select" />
                            <asp:BoundField DataField="Code" HeaderText="Code" />
                            <asp:BoundField DataField="Name" HeaderText="Customer Name" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
