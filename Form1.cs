using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Employee
{
    public partial class Form1 : Form
    {

        //connecting to database.

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H4B53OM\SAJADY;Initial Catalog=diwan;Integrated Security=True;");

        SqlCommand cmd;


        //--------This is the CONSTRUCTOR----------

        #region Constructor
        public Form1()
        {
            InitializeComponent();

            this.Height = 510;

            this.FormBorderStyle = FormBorderStyle.Sizable;

            this.StartPosition = FormStartPosition.CenterScreen;

            NextButton.MouseEnter += new EventHandler(button_MouseEnter);

            NextButton.MouseLeave += new EventHandler(button_MouseLeave);


            button1.MouseEnter += new EventHandler(Savebutton_MouseEnter);

            button1.MouseLeave += new EventHandler(Savebutton_MouseLeave);


            button2.MouseEnter += new EventHandler(Cancelbutton_MouseEnter);

            button2.MouseLeave += new EventHandler(Cancelbutton_MouseLeave);
        }

        #endregion


        /*========- Initialization of Components -==========*/

        #region component Initialization
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //This is the DATE_OF_REGISTERATION INPUT.
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }


        //This is the NAME textbox
        private void textBox6_TextChanged_1(object sender, EventArgs e)
        { }


        //This is the AGE textbox
        private void textBox5_TextChanged(object sender, EventArgs e)
        { }


        //This is the SPECIALITY textbox
        private void textBox4_TextChanged(object sender, EventArgs e)
        { }


        //This is the ADDRESS textbox
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


        //This is the PHONENUMBER textbox
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //This is the BRANCH textbox
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //This is the Speciality.
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion


        /*===================- Methods for Validation of the Inputs -==============*/
        #region Method's of Validation


        //-1.
        //Method for handling Validation of the Name Input.
        private bool nameInputValidation()
        {
            String name = nameInput.Text;


            if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || name.Length > 40 || name.Any(char.IsDigit))
            {
                NameError.Visible = true;
                nameInput.Focus();
                return false;
            }
            else{
                NameError.Visible = false;
                return true;
            }

        }


        //-2.
        //Method for handling Validation of the Age Input
        private bool AgeInputValidation()
        {

            if (string.IsNullOrWhiteSpace(ageInput.Text) || !int.TryParse(ageInput.Text, out int age) || ageInput.Text.Length != 2)
            {
                AgeError.Visible = true;
                ageInput.Focus();
                return false;
            }
            else
            {
                AgeError.Visible = false;
                return true;
            }



        }



        //-3.
        //Method for handling Validation of the Address Input
        private bool AddressInputValidation()
        {
            if (string.IsNullOrWhiteSpace(addressInput.Text) || addressInput.Text.Length > 150 || addressInput.Text.Any(char.IsDigit))
            {
                AddressError.Visible = true;
                addressInput.Focus();
                return false;
            }
            else
            {
                AddressError.Visible = false;
                return true;
            }
          
        }


        //-4.
        //Method for handling Validation of the PhoneNumber Input
        private bool PhoneNumbersInputValidation()
        {
            string phoneNumberPattern = @"^07\d{9}$"; // Matches phone numbers in the format 07xxxxxxxxx

            if (!Regex.IsMatch(phonenumberInput.Text, phoneNumberPattern))
            {
                PhoneError.Visible = true;
                phonenumberInput.Focus();
                return false;
            }
            else
            {
                PhoneError.Visible = false;
                return true;

            }
        }

        


        //-5.
        //Method for handling Validation of the Speciality Input
        private bool SpecialityInputValidation()
        {
            if (string.IsNullOrEmpty(specialitycomboInput.Text))
            {
                SpecialityError.Visible = true;
                specialitycomboInput.Focus();
                return false;
            }
            else {
                SpecialityError.Visible = false;
                return true;
            }

        }


        //-6.
        //Method for handling Validation of the Branch Input
        private bool BranchInputValidation()
        {
            if (string.IsNullOrEmpty(branchcomboInput.Text))
            {
                BranchError.Visible = true;
                branchcomboInput.Focus();
                return false;
            }
            else
            {
                BranchError.Visible = false;
                return true;
            }
           
        }

        #endregion


        private bool ValidateAllInputs()
        {
            bool isNameValid = nameInputValidation();
            bool isAgeValid = AgeInputValidation();
            bool isAddressValid = AddressInputValidation();
            bool isPhoneNumberValid = PhoneNumbersInputValidation();
            bool isSpecialityValid = SpecialityInputValidation();
            bool isBranchValid = BranchInputValidation();

            // Return true only if all validations are successful
            return isNameValid && isAgeValid && isAddressValid && isPhoneNumberValid && isSpecialityValid && isBranchValid;
        }

        //========-- This is the SAVE button --=================

        #region Save Button
        private void button1_Click(object sender, EventArgs e)
        {

            bool isValid = ValidateAllInputs();

            if (isValid)
            {
                try
                {
                    DateTime DateOfRegister = dateofregisterInput.Value;

                    con.Open();

                    cmd = new SqlCommand("INSERT INTO EMPLOYEE_REGISTRATION(NAME, AGE, SPECIALITY, ADDRESS, PHONE_NUMBER, BRANCH, DATE_OF_REGISTER) VALUES (@Name,@Age, @Speciality, @Address, @PhoneNumber, @Branch, @RegisterDate)", con);

                    cmd.Parameters.AddWithValue("@Name", nameInput.Text);
                    cmd.Parameters.AddWithValue("@Age", ageInput.Text);
                    cmd.Parameters.AddWithValue("@Speciality", specialitycomboInput.Text);
                    cmd.Parameters.AddWithValue("@Address", addressInput.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phonenumberInput.Text);
                    cmd.Parameters.AddWithValue("@Branch", branchcomboInput.Text);
                    cmd.Parameters.AddWithValue("@RegisterDate", dateofregisterInput.Value);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data has been inserted into Diwan Database");

                    // Clear input fields after successful insertion
                    nameInput.Text = "";
                    ageInput.Text = "";
                    specialitycomboInput.Text = "";
                    addressInput.Text = "";
                    phonenumberInput.Text = "";
                    branchcomboInput.Text = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Employee couldn't be added: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }


        } //closing tag of the save button method.

        private void ClearInputs()
        {
            nameInput.Text = "";
            ageInput.Text = "";
            specialitycomboInput.Text = "";
            addressInput.Text = "";
            phonenumberInput.Text = "";
            branchcomboInput.Text = "";
        }

        private void Savebutton_MouseEnter(object sender, EventArgs e)
        {
            button1.Size = new Size(button1.Width + 10, button1.Height + 10);

            button1.Location = new Point(button1.Location.X - 5, button1.Location.Y - 5);
        }

        private void Savebutton_MouseLeave(object sender, EventArgs e)
        {
            button1.Size = new Size(button1.Width - 10, button1.Height - 10);

            button1.Location = new Point(button1.Location.X + 5, button1.Location.Y + 5);
        }


        #endregion



        #region Cancel Button
        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();
        }


        private void Cancelbutton_MouseEnter(object sender, EventArgs e)
        {
            button2.Size = new Size(button2.Width + 10, button2.Height + 10);

            button2.Location = new Point(button2.Location.X - 5, button2.Location.Y - 5);
        }

        private void Cancelbutton_MouseLeave(object sender, EventArgs e)
        {
            button2.Size = new Size(button2.Width - 10, button2.Height - 10);

            button2.Location = new Point(button2.Location.X + 5, button2.Location.Y + 5);
        }

        #endregion


        #region Next-Button-Related
        private void NextButton_Click(object sender, EventArgs e)
        {
            Form2 obj = new Form2();

            obj.ShowDialog();

            this.Close();
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            NextButton.Size = new Size(NextButton.Width + 10, NextButton.Height + 10);

            NextButton.Location = new Point(NextButton.Location.X - 5, NextButton.Location.Y - 5);
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            NextButton.Size = new Size(NextButton.Width - 10, NextButton.Height - 10);

            NextButton.Location = new Point(NextButton.Location.X + 5, NextButton.Location.Y + 5);
        }

        #endregion



        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.Transparent;


        }



        private void label9_Click(object sender, EventArgs e)
        {

        }


    } //closing tag of the Class -2-

  


    } //closing tag of the namespace




