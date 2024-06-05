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

namespace Skills_International
{
    public partial class RegistrationForm : Form
    {
        private SQL sql = new SQL();
        public RegistrationForm()
        {
            InitializeComponent();
            LoadRegistrationNumbers();
        }
        
        private void contactnotxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void registerbtn_Click(object sender, EventArgs e)
        {
            string gender = maleroundbtn.Checked ? "Male" : "Female";
            try
            {
                sql.RegisterStudent(fnametxt.Text, lnametxt.Text, dobpicker.Value, gender, addresstxt.Text, emailtxt.Text,
                                    string.IsNullOrWhiteSpace(mobilepnotxt.Text) ? (int?)null : Convert.ToInt32(mobilepnotxt.Text),
                                    string.IsNullOrWhiteSpace(homepnotxt.Text) ? (int?)null : Convert.ToInt32(homepnotxt.Text),
                                    parentnametxt.Text, nictxt.Text,
                                    string.IsNullOrWhiteSpace(contactnotxt.Text) ? (int?)null : Convert.ToInt32(contactnotxt.Text));
                MessageBox.Show("Student Registered Successfully", "Register Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadRegistrationNumbers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ClearForm()
        {
            
            fnametxt.Clear();
            lnametxt.Clear();
            dobpicker.Value = DateTime.Now;
            maleroundbtn.Checked = false;
            femaleroundbtn.Checked = false;
            addresstxt.Clear();
            emailtxt.Clear();
            mobilepnotxt.Clear();
            homepnotxt.Clear();
            parentnametxt.Clear();
            nictxt.Clear();
            contactnotxt.Clear();

        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void LoadRegistrationNumbers()
        {
            try
            {
                regnotxt.Items.Clear();
                int[] registrationNumbers = sql.GetRegistrationNumbers();
                foreach (int regNo in registrationNumbers)
                {
                    regnotxt.Items.Add(regNo.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void regnotxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(regnotxt.SelectedItem.ToString(), out int regNo))
            {
                try
                {
                    DataRow studentDetails = sql.GetRegistrationDetails(regNo);
                    if (studentDetails != null)
                    {
                        fnametxt.Text = studentDetails["firstName"].ToString();
                        lnametxt.Text = studentDetails["lastName"].ToString();
                        dobpicker.Value = Convert.ToDateTime(studentDetails["dateOfBirth"]);
                        string gender = studentDetails["gender"].ToString();
                        if (gender == "Male")
                        {
                            maleroundbtn.Checked = true;
                        }
                        else if (gender == "Female")
                        {
                            femaleroundbtn.Checked = true;
                        }
                        addresstxt.Text = studentDetails["address"].ToString();
                        emailtxt.Text = studentDetails["email"].ToString();
                        mobilepnotxt.Text = studentDetails["mobilePhone"].ToString();
                        homepnotxt.Text = studentDetails["homePhone"].ToString();
                        parentnametxt.Text = studentDetails["parentName"].ToString();
                        nictxt.Text = studentDetails["nic"].ToString();
                        contactnotxt.Text = studentDetails["contactNo"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(regnotxt.SelectedItem.ToString(), out int regNo))
            {
                string gender = maleroundbtn.Checked ? "Male" : "Female";
                try
                {
                    sql.UpdateStudent(regNo, fnametxt.Text, lnametxt.Text, dobpicker.Value, gender, addresstxt.Text, emailtxt.Text,
                                      string.IsNullOrWhiteSpace(mobilepnotxt.Text) ? (int?)null : Convert.ToInt32(mobilepnotxt.Text),
                                      string.IsNullOrWhiteSpace(homepnotxt.Text) ? (int?)null : Convert.ToInt32(homepnotxt.Text),
                                      parentnametxt.Text, nictxt.Text,
                                      string.IsNullOrWhiteSpace(contactnotxt.Text) ? (int?)null : Convert.ToInt32(contactnotxt.Text));
                    MessageBox.Show("Record Updated Successfully", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(regnotxt.SelectedItem.ToString(), out int regNo))
            {
                if (MessageBox.Show("Are you sure, Do you really want to Delete this Record...?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        sql.DeleteStudent(regNo);
                        MessageBox.Show("Record Deleted Successfully", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadRegistrationNumbers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void logoutlbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.ShowDialog();

        }

        private void closebtn_MouseEnter(object sender, EventArgs e)
        {
            closebtn.BackColor = Color.Red;
            closebtn.IconColor = Color.White;
        }

        private void closebtn_MouseLeave(object sender, EventArgs e)
        {
            closebtn.BackColor = Color.Transparent;
            closebtn.IconColor = Color.Black;
        }

        private void exitlinklbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Are you sure, Do you really want to Exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
