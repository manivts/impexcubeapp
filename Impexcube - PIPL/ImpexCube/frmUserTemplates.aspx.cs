using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

namespace ImpexCube
{
    public partial class frmUserTemplates : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.UserTemplateBL objUserReports = new VTS.ImpexCube.Business.UserTemplateBL();
        ArrayList arraylist1 = new ArrayList();
        ArrayList arraylist2 = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                //string formID = "Report Template";
                //Authenticate.Forms(formID);
                //string Validate = (string)Session["DISABLE"];
                //if (Validate == "True")
                //{
                DropImporterName();
                GridReportTemplates();
                BindListValues();
                btnSaveTemplate.Text = "Save";
                Session["ReportTemplates"] = string.Empty;
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);
                //}

            }            
        }

        private void BindListValues()
        {
            DataSet ds = objUserReports.SelectJobFields();
            if (ds.Tables["fields"].Rows.Count != 0)
            {
                lbJobField.DataSource = ds;
                lbJobField.DataTextField = "JobFields";
                lbJobField.DataValueField = "Test";
                lbJobField.DataBind();
            }
            else
            {
                lbJobField.DataSource = null;
                lbJobField.DataBind();
            }
        }

        private void GridReportTemplates()
        {
            DataSet ds = objUserReports.SelectUserReportTemplate();
            if (ds.Tables["templates"].Rows.Count != 0)
            {
                gvTemplateDetails.DataSource = ds;                
                gvTemplateDetails.DataBind();                
            }
            else
            {
                gvTemplateDetails.DataSource = null;
                gvTemplateDetails.DataBind();
            }
        }

        private void DropImporterName()
        {
            DataSet ds = objUserReports.SelectImportName();
            if (ds.Tables["importer"].Rows.Count != 0)
            {
                ddlPartyName.DataSource = ds;
                ddlPartyName.DataTextField = "Importer";
                ddlPartyName.DataValueField = "Importer";
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                ddlPartyName.DataSource = null;
                ddlPartyName.DataBind();
            }
        }

        protected void btnSaveTemplate_Click(object sender, EventArgs e)
        {   
            int result = new int();
            string fields = string.Empty;
            try
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                for (int i = 0; i < lbCustomField.Items.Count ; i++)
                {
                    fields += lbCustomField.Items[i].Value + ",";
                }
                if (fields.Length > 0)
                {
                    fields = fields.Remove((fields.Length - 1), 1);
                }

                if ((string)Session["ReportTemplates"] == "")
                {
                    result = objUserReports.InsertReportTemplates(ddlPartyName.SelectedItem.Text, txtTemplate.Text, fields, (string)Session["USER-NAME"], date, (string)Session["USER-NAME"], date);

                    if (result == 1)
                    {
                        GridReportTemplates();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmUserTemplates.aspx';", true);
                    }
                }
                else if ((string)Session["ReportTemplates"] == "Edit")
                {
                    int id = Convert.ToInt32(Session["Id"]);
                    result = objUserReports.UpdateReportTemplates( id, ddlPartyName.SelectedItem.Text, txtTemplate.Text, fields, (string)Session["USER-NAME"], date);

                    if (result == 1)
                    {
                        GridReportTemplates();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmUserTemplates.aspx';", true);
                    }
                }
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
        }

        protected void gvTemplateDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ReportTemplates"] = "Edit";
            btnSaveTemplate.Text = "Update";
            Session["Id"] = Convert.ToInt32(gvTemplateDetails.SelectedRow.Cells[1].Text);
            ddlPartyName.SelectedIndex = ddlPartyName.Items.IndexOf(ddlPartyName.Items.FindByText(gvTemplateDetails.SelectedRow.Cells[2].Text));
            txtTemplate.Text = gvTemplateDetails.SelectedRow.Cells[3].Text;
            string customfields = gvTemplateDetails.SelectedRow.Cells[4].Text;
            if (customfields != "" || customfields != "&nbsp;")
            {
                string[] arr1 = customfields.Split(',');
                foreach (string itemfields in arr1)
                {
                    lbCustomField.Items.Add(itemfields);
                }
            }
            else if (customfields == "" || customfields == "&nbsp;")
            {
                lbCustomField.Items.Clear();
            }            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmUserTemplates.aspx");
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {

          
            string lstr1 = lbJobField.Text;
            if (lstr1 == "")
            {
                Response.Write("<script>alert('Please select atleast one  to move')</script>");

            }
            else
            {
                for (int iLoop = 0; iLoop <= lbJobField.Items.Count - 1; iLoop++)
                {
                    if (lbJobField.Items[iLoop].Selected)
                    {

                        lbCustomField.Items.Add(lbJobField.Items[iLoop].Text);
                    }
                }
            }
            lbCustomField.DataBind();


            //if (lbJobField.SelectedIndex >= 0)
            //{
            //    for (int i = 0; i < lbJobField.Items.Count; i++)
            //    {
            //        if (lbJobField.Items[i].Selected)
            //        {
            //            if (!arraylist1.Contains(lbJobField.Items[i]))
            //            {
            //                arraylist1.Add(lbJobField.Items[i]);

            //            }
            //        }
            //    }
            //    for (int i = 0; i < arraylist1.Count; i++)
            //    {
            //        if (!lbCustomField.Items.Contains(((ListItem)arraylist1[i])))
            //        {
            //            lbCustomField.Items.Add(((ListItem)arraylist1[i]));
            //        }
            //        lbCustomField.Items.Remove(((ListItem)arraylist1[i]));
            //    }
            //    lbCustomField.SelectedIndex = -1;
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select atleast one in employee to move');", true);
            //}
        }

        protected void btnMoveAll_Click(object sender, EventArgs e)
        {
            while (lbJobField.Items.Count != 0)
            {
                for (int i = 0; i < lbJobField.Items.Count; i++)
                {
                    lbCustomField.Items.Add(lbJobField.Items[i]);
                    lbJobField.Items.Remove(lbJobField.Items[i]);
                }
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbCustomField.SelectedIndex >= 0)
            {
                for (int i = 0; i < lbCustomField.Items.Count; i++)
                {
                    if (lbCustomField.Items[i].Selected)
                    {
                        if (!arraylist2.Contains(lbCustomField.Items[i]))
                        {
                            arraylist2.Add(lbCustomField.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arraylist2.Count; i++)
                {
                    if (!lbJobField.Items.Contains(((ListItem)arraylist2[i])))
                    {
                        lbJobField.Items.Add(((ListItem)arraylist2[i]));
                    }
                    lbCustomField.Items.Remove(((ListItem)arraylist2[i]));
                }
                lbJobField.SelectedIndex = -1;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select atleast one in employee to move');", true);
            }
        }

        protected void btnRemoveAll_Click(object sender, EventArgs e)
        {
            

            lbCustomField.Items.Clear();
            BindListValues();
            //while (lbCustomField.Items.Count != 0)
            //{
            //    for (int i = 0; i < lbCustomField.Items.Count; i++)
            //    {
            //        lbJobField.Items.Add(lbJobField.Items[i]);
            //        lbCustomField.Items.Remove(lbJobField.Items[i]);
            //    }
            //}
        }

        protected void btnMoveUp_Click(object sender, EventArgs e)
        {
            // only if the first item isn't the current one
            if (lbCustomField.SelectedIndex > 0)
            {
                // add a duplicate item up in the listbox
                lbCustomField.Items.Insert(lbCustomField.SelectedIndex - 1, lbCustomField.Text);
                // make it the current item
                lbCustomField.SelectedIndex = (lbCustomField.SelectedIndex - 2);
                // delete the old occurrence of this item
                lbCustomField.Items.RemoveAt(lbCustomField.SelectedIndex + 2);

            }
        }

        protected void btnMoveDown_Click(object sender, EventArgs e)
        {
            // only if the first item isn't the current one
            if (lbCustomField.SelectedIndex < lbCustomField.Items.Count - 1)
            {
                // add a duplicate item up in the listbox
                // add a duplicate item down in the listbox
                lbCustomField.Items.Insert(lbCustomField.SelectedIndex + 2, lbCustomField.Text);
                // make it the current item
                lbCustomField.SelectedIndex = lbCustomField.SelectedIndex + 2;
                // delete the old occurrence of this item
                lbCustomField.Items.RemoveAt(lbCustomField.SelectedIndex - 2);

            }
        }
    }
}