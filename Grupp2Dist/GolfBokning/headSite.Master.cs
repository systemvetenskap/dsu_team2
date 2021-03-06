﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.IO;

namespace GolfBokning
{
    public partial class headSite : System.Web.UI.MasterPage
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        protected void Page_Load(object sender, EventArgs e)
        {
                myCookie = Request.Cookies["LoginCookie"];

                string pageName = Request.Url.Segments.Last();
                HtmlGenericControl divJS = new HtmlGenericControl();
                divJS.Attributes["class"] = "hiddenPage";
                divJS.TagName = "div";
                divJS.InnerHtml = pageName;

                if (Request.Cookies["LoginCookie"] != null)
                {
                    if (myCookie["_logged"] == "1")
                    {
                        // Check restrictions
                        if (myCookie["_staff"] != "1")
                        {
                            if (pageName == "addResults.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                            else if (pageName == "closeLane.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                            else if (pageName == "staffmember.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                            else if (pageName == "cms.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                            else if (pageName == "member.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                            else if (pageName == "news.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                            else if (pageName == "personalverktyg.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                            else if (pageName == "sendMail.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                            else if (pageName == "tournament.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                            else if (pageName == "tournamentdraw.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                            else if (pageName == "tournamententry.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                            else if (pageName == "tournamentpers.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                            else if (pageName == "tournament.aspx")
                            {
                                Response.Redirect("403_staff.aspx");
                            }
                        }
                    }
                }
                else
                {
                    if (pageName == "addResults.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "closeLane.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "cms.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "member.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "news.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "personalverktyg.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "sendMail.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "tournament.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "tournamentdraw.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "tournamententry.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "tournamentpers.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "tournament.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "appresults.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "booking.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "chatt.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "member.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "myAccount.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "minasidor.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "staffmember.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "tournamentresult.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                    else if (pageName == "unbook.aspx")
                    {
                        Response.Redirect("403_login.aspx");
                    }
                }

                if (pageName == "booking.aspx")
                {
                    NavBooking.Attributes["class"] = "active";
                    NavBookingPersonal.Attributes["class"] = "active";
                }

                else if (pageName == "tournamentresult.aspx")
                {
                    NavTournamentresult.Attributes["class"] = "active";
                }

                else if (pageName == "tournamentmembadd.aspx")
                {
                    NavTournamentmembadd.Attributes["class"] = "active";
                }


                else if (pageName == "minasidor.aspx")
                {
                    NavMinaSidor.Attributes["class"] = "active";
                }

                else if (pageName == "tournamententry.aspx")
                {
                    //NavTournamententry.Attributes["class"] = "active";
                }

                else if (pageName == "myAccount.aspx")
                {
                    NavMyAccount.Attributes["class"] = "active";
                }

                else if (pageName == "personalverktyg.aspx")
                {
                    NavPersonalVerktyg.Attributes["class"] = "active";
                }



                else if (pageName == "member.aspx")
                {
                    NavMember.Attributes["class"] = "active";
                }

                else if (pageName == "tournament.aspx")
                {
                    NavTournament.Attributes["class"] = "active";
                }

                else if (pageName == "addResults.aspx")
                {
                    NavAddResults.Attributes["class"] = "active";
                }

                else if (pageName == "news.aspx")
                {
                    NavNews.Attributes["class"] = "active";
                }

                else if (pageName == "closeLane.aspx")
                {
                    NavCloseLane.Attributes["class"] = "active";
                }

                else if (pageName == "sendMail.aspx")
                {
                    NavSendMail.Attributes["class"] = "active";
                }
                else if (pageName == "staffmember.aspx")
                {
                    NavStaffMember.Attributes["class"] = "active";
                }

                else if (pageName == "tournamentpers.aspx")
                {
                    NavTournamentPers.Attributes["class"] = "active";
                }

                else if (pageName == "chatt.aspx")
                {
                    NavChat.Attributes["class"] = "active";
                }

                //else
                //{
                //    PanelHidden.Controls.Add(divJS);
                //}
                PanelHidden.Controls.Add(divJS);

            //MemberPage.Visible = false;
            //MemberPage.Attributes["href"] = "member.aspx";
            //StaffsPage.Visible = false;
            //StaffsPage.Attributes["href"] = "staffmember.aspx";
            //NewsPage.Visible = false;
            //NewsPage.Attributes["href"] = "news.aspx";
            LoginCreate.Visible = true;
            LoginCreate.Text = "<span class='glyphicon glyphicon-user'></span> Bli medlem";
            LoginCreate.Attributes["href"] = "register.aspx";
            
            //EditTournamentPage.Visible = false;
            //EditTournamentPage.Attributes["href"] = "tournament.aspx";
            //addResultsL.Visible = false;
            //hanteraNyheter.Visible = false;
            //closeBanaL.Visible = false;




            if (Request.Cookies["LoginCookie"] != null)
            {
                // Check if session variable _logged is 1
                if (myCookie["_logged"].ToString() == "1")
                {
                    LoginLogout.Text = "<span class='glyphicon glyphicon-remove'></span> Logga ut";
                    LoginLogout.Attributes["class"] = "logoutClick";
                    LoginCreate.Text = "<span class='glyphicon glyphicon-user'></span> Min sida";
                    LoginCreate.Attributes["href"] = "minasidor.aspx";
                    LabelName.Text = "Välkommen " + myCookie["_name"].ToString().Replace("Ã¶", "ö").Replace("Ã¥", "ä").Replace("Ã¤", "") +"!";

                    if (!string.IsNullOrEmpty(myCookie["_staff"] as string))
                    {
                        if (myCookie["_staff"].ToString() == "1")
                        {
                            //MemberPage.Visible = true;
                            //StaffsPage.Visible = true;
                            //NewsPage.Visible = true;
                            //EditTournamentPage.Visible = true;
                            //addResultsL.Visible = true;
                            //addResultsL.Attributes["href"] = "addResults.aspx";
                            //hanteraNyheter.Visible = true;
                            //hanteraNyheter.Attributes["href"] = "news.aspx";
                            //closeBanaL.Visible = true;
                            //closeBanaL.Attributes["href"] = "closeLane.aspx";
                            LoginCreate2.Text = "<span class='glyphicon glyphicon-user'></span> Personalverktyg";
                            LoginCreate2.Attributes["href"] = "personalverktyg.aspx";
                        }
                    }
                }
            }
            else
            {
                LoginLogout.Text = "<span class='glyphicon glyphicon-ok'></span> Logga in";
                LoginLogout.Attributes["href"] = "loginform.aspx";
            }
        }
    }
}