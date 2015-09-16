using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;


namespace ImpexCube.Billing
{
    public partial class popup : System.Web.UI.Page
    {
        string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string cUNIT = "";
        string strCharge = "";
        public string partyname = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            partyname = Request.QueryString["partyname"];
            GridView2.Visible = false;
            GridView3.Visible = false;
            GrdPaddr.DataSource = PartyAddr(partyname);
            GrdPaddr.DataBind();
        }

        
        
        public DataSet PartyAddr(string pcode)
        {
           
            SqlConnection conn = new SqlConnection(strImpex);
            conn.Open();
            string sqlQuery4 = "select *  from M_AccountDetails " +
                                   "where AccountName='" + partyname + "' ";
            SqlDataAdapter da4 = new SqlDataAdapter(sqlQuery4, conn);
            DataSet ds4 = new DataSet();
            da4.Fill(ds4, "prtMast");
            conn.Close();
            return ds4;
        }
    

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView3.Visible = true;
           // GridScroll.Visible = true;
            string CID = Convert.ToString(GridView2.SelectedDataKey.Value);
            string Bill = "YES";

            //string BType = rbInvoice.SelectedValue;
            //if (BType == "")
            //    BType = "SB";
            string Query = "";
            if ((string)Session["Contr_Type"] == "LCL")
            {
                Query = "select * from M_Quote where QuoteNo='" + CID + "' and Type='" + (string)Session["Contr_Type"] + "' order by ID ";
            }
            else if ((string)Session["Contr_Type"] == "FCL")
            {
                if ((string)Session["Contr_size"] == "20")
                {
                    Query = "select * from M_Quote where QuoteNo='" + CID + "' and  Type='20Feet' order by ID ";
                }
                else if ((string)Session["Contr_size"] == "40")
                {
                    Query = "select * from M_Quote where QuoteNo='" + CID + "' and  Type='40Feet'  order by ID ";
                }
            }
            else
            {
                Query = "select * from M_Quote where QuoteNo='" + CID + "' and  Type='AIR' order by ID  ";
            }


            //if((string)Session["shpType"]=="Air" )
            //{
            //    Query = "select * from M_Quote where QuoteNo='" + CID + "' and type='" + (string)Session["shpType"] + "'  order by ID";
            //}
            //else
            //{
            //    Query = "select * from M_Quote where QuoteNo='" + CID + "' and type<>'Air' order by ID";
            //}
            GridView3.DataSource = GetDataSQL(Query);
            GridView3.DataBind();
            Session["ContractID"] = CID;
            BtnContract_Submit.Visible = true;
        }

        public DataSet GetDataSQL(string Query)
        {
            SqlConnection conn = new SqlConnection(strImpex);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLtable");
            return ds;
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string shpType = Session["shpType"].ToString();
            string cSize = Session["Contr_size"].ToString();
            string cType = Session["Contr_Type"].ToString();
            //string Bill = rbInvoice.SelectedValue;

            if (shpType != "Air")
            {
                if (cType == "FCL")
                {
                    if (cSize == "20")
                        shpType = "ft20";
                    else if (cSize == "40")
                        shpType = "ft40";
                }
                else if (cType == "LCL")
                    shpType = "LCL";
                else
                    shpType = "break_bulk";
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                //if (Bill == "SB")
                //{
                //    e.Row.Cells[10].Visible = true;
                //    e.Row.Cells[11].Visible = false;
                //}
                //else
                //{
                //    e.Row.Cells[10].Visible = false;
                //    e.Row.Cells[11].Visible = true;
                //}
                //switch (shpType)
                //{
                //    case "Air":
                //        {
                //            e.Row.Cells[5].Visible = true;
                //            e.Row.Cells[6].Visible = false;
                //            e.Row.Cells[7].Visible = false;
                //            e.Row.Cells[8].Visible = false;
                //            e.Row.Cells[9].Visible = false;
                //            break;
                //        }
                //    case "break_bulk":
                //        {
                //            e.Row.Cells[5].Visible = false;
                //            e.Row.Cells[6].Visible = true;
                //            e.Row.Cells[7].Visible = false;
                //            e.Row.Cells[8].Visible = false;
                //            e.Row.Cells[9].Visible = false;
                //            break;
                //        }
                //    case "LCL":
                //        {
                //            e.Row.Cells[5].Visible = false;
                //            e.Row.Cells[6].Visible = false;
                //            e.Row.Cells[7].Visible = true;
                //            e.Row.Cells[8].Visible = false;
                //            e.Row.Cells[9].Visible = false;
                //            break;
                //        }

                //    case "ft20":
                //        {
                //            e.Row.Cells[5].Visible = false;
                //            e.Row.Cells[6].Visible = false;
                //            e.Row.Cells[7].Visible = false;
                //            e.Row.Cells[8].Visible = true;
                //            e.Row.Cells[9].Visible = false;
                //            break;
                //        }
                //    case "ft40":
                //        {
                //            e.Row.Cells[5].Visible = false;
                //            e.Row.Cells[6].Visible = false;
                //            e.Row.Cells[7].Visible = false;
                //            e.Row.Cells[8].Visible = false;
                //            e.Row.Cells[9].Visible = true;
                //            break;
                //        }

                //}
            }
            //if(e.Row.RowType==DataControlRowType.DataRow)
            //{
            //    CheckBox chklist = (CheckBox)e.Row.FindControl("chk");
            //    string charge = e.Row.Cells[2].Text;
            //    string product=e.Row.Cells[3].Text;
            //    e.Row.Cells[1].ForeColor = Color.White;
            //    if (charge == "Agency charges" && (product=="Variable" || product=="Maximum"))
            //    {
            //        chklist.Enabled = false;
            //    }
            //    if (Bill == "SB")
            //    {
            //        e.Row.Cells[10].Visible = true;
            //        e.Row.Cells[11].Visible = false;
            //    }
            //    else
            //    {
            //        e.Row.Cells[10].Visible = false;
            //        e.Row.Cells[11].Visible = true;
            //    }
            //    switch (shpType)
            //    {
            //        case "AIR":
            //            {
            //                e.Row.Cells[5].Visible = true;
            //                e.Row.Cells[6].Visible = false;
            //                e.Row.Cells[7].Visible = false;
            //                e.Row.Cells[8].Visible = false;
            //                e.Row.Cells[9].Visible = false;
            //                break;
            //            }

            //        case "break_bulk":
            //            {
            //                e.Row.Cells[5].Visible = false;
            //                e.Row.Cells[6].Visible = true;
            //                e.Row.Cells[7].Visible = false;
            //                e.Row.Cells[8].Visible = false;
            //                e.Row.Cells[9].Visible = false;
            //                break;
            //            }
            //        case "LCL":
            //            {
            //                e.Row.Cells[5].Visible = false;
            //                e.Row.Cells[6].Visible = false;
            //                e.Row.Cells[7].Visible = true;
            //                e.Row.Cells[8].Visible = false;
            //                e.Row.Cells[9].Visible = false;
            //                break;
            //            }
            //        case "ft20":
            //            {
            //                e.Row.Cells[5].Visible = false;
            //                e.Row.Cells[6].Visible = false;
            //                e.Row.Cells[7].Visible = false;
            //                e.Row.Cells[8].Visible = true;
            //                e.Row.Cells[9].Visible = false;
            //                break;
            //            }
            //        case "ft40":
            //            {
            //                e.Row.Cells[5].Visible = false;
            //                e.Row.Cells[6].Visible = false;
            //                e.Row.Cells[7].Visible = false;
            //                e.Row.Cells[8].Visible = false;
            //                e.Row.Cells[9].Visible = true;
            //                break;
            //            }
            //    }

            //}
        }
        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnContract_Submit_Click(object sender, EventArgs e)
        {
            Session["PageLoad"] = "2";
            foreach (GridViewRow Row in GridView3.Rows)
            {
                CheckBox chkCharge = (CheckBox)Row.FindControl("chk");
                string units = Row.Cells[4].Text;
                cUNIT = cUNIT + units + ",";
                if (chkCharge.Checked)
                {
                    string strSNO = Row.Cells[1].Text;
                    strCharge = strCharge + "" + strSNO + ",";
                }
            }
            Session["UNITS"] = cUNIT.TrimEnd(',');
            if (strCharge != "")
            {
                //tblContr.Visible = false;
                //tblINV.Visible = true;
                GetContractInfo(strCharge);
            }
            //string BiilType = rbBill.SelectedValue;
           // if (BiilType == "DP")
            //{

//                txtSubParty.Text = "";
           // }

        }

        protected void GetContractInfo(string sel)
        {
            string CID = (string)Session["ContractID"];
            //string AssValue = txtAssValue.Text;
           // pName = Session["pName"].ToString();
            string status = "ACTIVE";
            string strchargeVal = sel.TrimEnd(',');
            Session["strChargeVal"] = strchargeVal;
            string strQuery = "select * from M_Quote where QuoteNo='" + CID + "' and CustomerName='" + partyname + "'  and ID in(" + strchargeVal + ") ";
            SqlConnection cnn = new SqlConnection(strImpex);
            SqlDataAdapter daS = new SqlDataAdapter(strQuery, cnn);
            DataSet dsS = new DataSet();
            daS.Fill(dsS, "Contract");
            if (dsS.Tables["Contract"].Rows.Count != 0)
            {
                Session["QuoteParticulars"] = dsS;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "javascript", "assignvalues('" + Session["QuoteParticulars"] + "');", true);
                //Response.Redirect("~/Billing/PIPLinvoiceSTAX.aspx");
                //PIPLinvoiceSTAX = new PIPLinvoiceSTAX();
                //GridView1.DataSource = GetDataSQL(strQuery);
                
                //GridView1.DataSource = dsS;
                //GridView1.DataBind();
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            //tblINV.Visible = true;
            //tblContr.Visible = false;
        }

        protected void GrdPaddr_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < GrdPaddr.Rows.Count; i++)
            {
                if (GrdPaddr.SelectedIndex == i)
                {
                    string NO = Convert.ToString(GrdPaddr.SelectedDataKey.Value);
                    string pcode = GrdPaddr.Rows[i].Cells[0].Text;
                    SqlConnection conn = new SqlConnection(strImpex);
                    conn.Open();
                    string sqlQuery = "select *  from M_AccountDetails where Accountcode='" + pcode + "' and BranchId=" + NO + "";
                    SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "addr");
                    conn.Close();

                    DataRowView row = ds.Tables["addr"].DefaultView[0];
                    string addr1 = row["Address1"].ToString();
                    string city = row["City"].ToString();
                    string state = row["State"].ToString();
                    string pin = row["Pincode"].ToString();
                    Session["addr"] = addr1;
                    Session["city"] = city;
                    Session["state"] = state;
                    Session["Pin"] = pin;
                    Session["BCODE"] = NO;
                    //txtCity.Text = city;
                    Session["BranchID"] = row["BranchId"].ToString();
                    //txtAdd1.Text = addr1;
                    Session["Phone"] = row["PhoneNo"].ToString();


                    FillQuote(pcode, NO);

                }
            }
            //GrdADDRSCROLL.Visible = false;
            GrdPaddr.Visible = false;
            GridView2.Visible = true;
            BtnContract_Submit.Visible = false;
            
            //TrAddr.Visible = false;
            //TrAddr1.Visible = false;bt
             
        }

        public void FillQuote(string pcode, string NO)
        {

            SqlConnection conn = new SqlConnection(strImpex);
            conn.Open();
            string sqlQuery = "select *  from M_Quote where customername= '" + partyname + "' ";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "addr");
            conn.Close();

            GridView2.DataSource = ds;
            GridView2.DataBind();

        }

    }
}