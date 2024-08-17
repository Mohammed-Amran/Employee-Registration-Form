using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employee
{
    public partial class Form2 : Form
    {


        //connecting to database.

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H4B53OM\SAJADY;Initial Catalog=diwan;Integrated Security=True;");

        SqlCommand cmd;


        #region Contructor
        public Form2()
        {
            InitializeComponent();

            this.Height = 510;

            this.FormBorderStyle = FormBorderStyle.Sizable;

            this.StartPosition = FormStartPosition.CenterScreen;

            returnBackPic.MouseEnter += new EventHandler(button_MouseEnter);

            returnBackPic.MouseLeave += new EventHandler(button_MouseLeave);

        }

        #endregion


        private void Form2_Load(object sender, EventArgs e)
        {

        }


        #region Show All Emplyee Button
        private void showEmployee_Click(object sender, EventArgs e)
        {
            showEmployee.Visible = false;

            SearchButton.Visible = false;
            DateSearch.Visible = false;
            SearchBydateLabel.Visible = false;

            employeeGridView.Visible = true;
            
            //Defining hte command.

            cmd = new SqlCommand("SELECT NAME, AGE, SPECIALITY, ADDRESS, PHONE_NUMBER, BRANCH, DATE_OF_REGISTER FROM EMPLOYEE_REGISTRATION", con);

            try { 

            con.Open();

           

          //1st create a dataAdapter.

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);


          //2nd create a dataTable to hold the data.

            DataTable dataTable = new DataTable();

          
          //3rd filling the dataTable with the data.

            dataAdapter.Fill(dataTable);

          //finally binding the data from the dataTable to the EmployeeGridView.

            employeeGridView.DataSource = dataTable;
            }

            catch(Exception ex)
            {

                MessageBox.Show("An Error Occured: " + ex.Message);
            }

            finally
            { 
                con.Close();
            }
            

        }

        #endregion

        #region  Return Back Button
        private void returnBackPic_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.ShowDialog();
            this.Close();
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            returnBackPic.Size = new Size(returnBackPic.Width + 10, returnBackPic.Height + 10);

            returnBackPic.Location = new Point(returnBackPic.Location.X - 5, returnBackPic.Location.Y - 5);
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            returnBackPic.Size = new Size(returnBackPic.Width - 10, returnBackPic.Height - 10);

            returnBackPic.Location = new Point(returnBackPic.Location.X + 5, returnBackPic.Location.Y + 5);
        }

        #endregion


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void employeeGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DateSearch_ValueChanged(object sender, EventArgs e)
        {

        }



        //Searching by date.
        #region Search Button
        private void SearchButton_Click(object sender, EventArgs e)
        {
            showEmployee.Visible = false;

            SearchButton.Visible = false;
            DateSearch.Visible = false;
            SearchBydateLabel.Visible = false;

            employeeGridView.Visible = true;

            DateTime dateToSearch = DateSearch.Value;

           


            cmd = new SqlCommand("SELECT * FROM EMPLOYEE_REGISTRATION WHERE CONVERT(date, DATE_OF_REGISTER) = @dateToSearch", con);

           

           
            try
            {
                
                con.Open();

                MessageBox.Show(dateToSearch.ToString("yyyy-MM-dd"));

                cmd.Parameters.Add("@dateToSearch", SqlDbType.Date).Value = dateToSearch;



                //1st create a dataAdapter.

                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(cmd);


                //2nd create a dataTable to hold the data.

                DataTable dataTable1 = new DataTable();


                //3rd filling the dataTable with the data.

                dataAdapter1.Fill(dataTable1);

                //finally binding the data from the dataTable to the EmployeeGridView.

                employeeGridView.DataSource = dataTable1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldnt search: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        #endregion
       




    } //Closing Bracket of the Class

} //Closing Bracket of the nameSpace
