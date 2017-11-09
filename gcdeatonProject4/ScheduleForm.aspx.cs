using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace gcdeatonProject4
{
    public partial class ScheduleForm : System.Web.UI.Page
    {
        List<string> listOfCourses = new List<string>();
        List<Section> schedule = new List<Section>();
        List<Section> selectedCourseOne = new List<Section>();
        List<Section> selectedCourseTwo = new List<Section>();
        List<Section> selectedCourseThree = new List<Section>();
        List<Section> selectedCourseFour = new List<Section>();
        List<string> sectionsUsed = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Queries.SqlQueries query = new Queries.SqlQueries();
                query.SetCommand("Select DISTINCT [CourseNumber] FROM Schedule");
                query.Connect();
                DataTable dt = query.RunSelectQuery();
                query.Disconnect();

                foreach (DataRow row in dt.Rows)
                {
                    ListItem courseItem = new ListItem();
                    courseItem.Text = row["CourseNumber"].ToString();
                    ddlCourseNumbers.Items.Add(courseItem);

                }
            }
            if (Session["listOfCourses"] == null)
            {
                Session["listOfCourses"] = new List<string>();
            }
            listOfCourses = (List<string>)Session["listOfCourses"];

        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            

            
            if (listOfCourses.Count == 4)
            {
                Response.Write("Four courses already added");
            }
            else
            {
                if (listOfCourses.Contains(ddlCourseNumbers.SelectedValue.ToString()))
                {
                    Response.Write("Class already in schedule");
                }
                else
                {
                    listOfCourses.Add(ddlCourseNumbers.SelectedValue.ToString());
                    Session["listOfCourses"] = listOfCourses;
                }
            }



            
        }

       
        protected void btnSchedule_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (listOfCourses.Count == 4)
            {
                Section sec = new Section();
                Queries.SqlQueries query = new Queries.SqlQueries();
                query.SetCommand("Select * FROM Schedule WHERE CourseNumber = @CourseNumber1 OR CourseNumber = @CourseNumber2 OR CourseNumber = @CourseNumber3 Or CourseNumber = @CourseNumber4 ");
                query.AddParameters("@CourseNumber1", listOfCourses[0]);
                query.AddParameters("@CourseNumber2", listOfCourses[1]);
                query.AddParameters("@CourseNumber3", listOfCourses[2]);
                query.AddParameters("@CourseNumber4", listOfCourses[3]);
                query.Connect();
                dt = query.RunSelectQuery();
                query.Disconnect();
            }
            else
            {
                Response.Write("Must have four courses selected");
            }

            foreach(DataRow row in dt.Rows)
            {
                Section sec = new Section();

                sec.sectionNumber = row["SectionNumber"].ToString();
                sec.days = row["Days"].ToString();
                sec.startTime = int.Parse(row["StartTime"].ToString());
                sec.endTime = int.Parse(row["EndTime"].ToString());
                sec.courseNumber = row["CourseNumber"].ToString();
                sec.CRN = row["CRN"].ToString();

                if(sec.courseNumber == listOfCourses[0])
                {
                    selectedCourseOne.Add(sec);
                }
                else if (sec.courseNumber == listOfCourses[1])
                {
                    selectedCourseTwo.Add(sec);
                }
                else if (sec.courseNumber == listOfCourses[2])
                {
                    selectedCourseThree.Add(sec);
                }
                else if (sec.courseNumber == listOfCourses[3])
                {
                    selectedCourseFour.Add(sec);
                }
                else
                {
                    Response.Write("nope");
                }
            }

            search();
        }

        private void search()
        {
            

            while (schedule.Count < 4)
            {
                if (schedule.Count == 0)
                {
                    getSectionOne();
                }
                else if(schedule.Count == 1)
                {
                    foreach(Section sec in selectedCourseTwo)
                    {
                        if (check(sec))
                        {
                            schedule.Add(sec);
                            
                            break;
                        }
                    }
                }
                else if (schedule.Count == 2)
                {
                    foreach (Section sec in selectedCourseThree)
                    {
                        if (check(sec))
                        {
                            schedule.Add(sec);
                            
                            break;
                        }
                        else
                        {

                        }
                    }
                }
                else if (schedule.Count == 3)
                {
                    foreach (Section sec in selectedCourseFour)
                    {
                        if (check(sec))
                        {
                            schedule.Add(sec);
                            
                            break;
                        }
                    }
                }
                else
                {
                    schedule.Clear();
                }
                int count = schedule.Count - 1;
                Response.Write(schedule[count].CRN);
                Response.Write(" ");
                Response.Write(schedule[count].days);
                Response.Write(" ");
                Response.Write(schedule[count].startTime);
                Response.Write(" ");
                Response.Write(schedule[count].endTime);
                Response.Write(" ");
                Response.Write("<br/>");
            }

        }


        private void getSectionOne()
        {
                foreach (Section sec in selectedCourseOne)
                {
                  if (!sectionsUsed.Contains(sec.CRN))
                  {
                    schedule.Add(sec);
                    sectionsUsed.Add(sec.CRN);
                    break;
                  }
                }
        }

        private bool check(Section sectionChecked)
        {
            foreach (Section sec in schedule)
            {
                if (!sectionsUsed.Contains(sectionChecked.CRN) && sec.days != sectionChecked.days && sectionChecked.startTime > sec.endTime || sec.startTime > sectionChecked.endTime)
                {
                    return true;
                }
            }
            return false;

        }
    }
}