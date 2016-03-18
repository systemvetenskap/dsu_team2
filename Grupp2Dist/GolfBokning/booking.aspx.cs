using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Web.UI.HtmlControls;

namespace GolfBokning
{
    public partial class booking : System.Web.UI.Page
    {
        public void fillbookingtable(DateTime date)
        {
            clsBookings.clsBooking list = new clsBookings.clsBooking();
            List<Bookingcell> lista = new List<Bookingcell>();

            lista = list.getDataTable(date);

            foreach (Bookingcell bcell in lista)
            {
                string cell = "cell" + bcell.cellID;
                var temp = bookingtable.FindControl(cell);

                if (cell == temp.ID.ToString())
                {
                    Label lbl = (Label)temp.FindControl("lbl" + bcell.cellID) as Label;
                    lbl.Text = "Hpc: " + bcell.hpc.ToString();

                    HtmlGenericControl p0 = (HtmlGenericControl)temp.FindControl("p1" + bcell.cellID);
                    HtmlGenericControl p1 = (HtmlGenericControl)temp.FindControl("p2" + bcell.cellID);
                    HtmlGenericControl p2 = (HtmlGenericControl)temp.FindControl("p3" + bcell.cellID);
                    HtmlGenericControl p3 = (HtmlGenericControl)temp.FindControl("p4" + bcell.cellID);
                    HtmlGenericControl[] arr = { p0, p1, p2, p3 };

                    for (int i = 0; i < bcell.players.Count; i++)
                    {
                        if (arr[i].InnerText != "M" || arr[i].InnerText != "F")
                        {
                            string gender = "";

                            if (bcell.players[i].gender == "Male")
                            {
                                gender = "M";

                            }
                            else
                            {
                                gender = "F";
                            }
                            if (i == 0)
                            {
                                p0.InnerText = gender;
                                if (gender == "M")
                                {
                                    p0.Attributes["class"] = "bookedm";
                                }
                                else
                                {
                                    p0.Attributes["class"] = "bookedf";
                                }
                            }
                            else if (i == 1)
                            {
                                p1.InnerText = gender;
                                if (gender == "M")
                                {
                                    p1.Attributes["class"] = "bookedm";
                                }
                                else
                                {
                                    p1.Attributes["class"] = "bookedf";
                                }
                            }
                            else if (i == 2)
                            {
                                p2.InnerText = gender;
                                if (gender == "M")
                                {
                                    p2.Attributes["class"] = "bookedm";
                                }
                                else
                                {
                                    p2.Attributes["class"] = "bookedf";
                                }
                            }
                            else if (i == 3)
                            {
                                p3.InnerText = gender;
                                if (gender == "M")
                                {
                                    p3.Attributes["class"] = "bookedm";
                                }
                                else
                                {
                                    p3.Attributes["class"] = "bookedf";
                                }
                            }
                        }
                    }                   
                }
            }
        }
        //public void emptybookingtable()
        //{
        //    foreach (TableCellCollection tc in bookingtable)
        //    {

        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            fillbookingtable(DateTime.Today);
            if (!IsPostBack)
            {
                table_calender.Visible = false;
            }                       
            
        }

        protected void btn_calender_Click(object sender, ImageClickEventArgs e)
        {
            if (table_calender.Visible == false)
            {
                table_calender.Visible = true;
            }
            else
            {
                table_calender.Visible = false;
            }
        }

        protected void table_calender_SelectionChanged(object sender, EventArgs e)
        {
            
            fillbookingtable(table_calender.SelectedDate);

        }
       
    }
}