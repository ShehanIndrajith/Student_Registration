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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        
        private void loginbtn_Click(object sender, EventArgs e)
        {
            SQL sql = new SQL();
            bool isValidLogin = false;
            try
            {
                isValidLogin = sql.CheckLogin(usernametxt.Text, passwordtxt.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return;
            }

            if (isValidLogin)
            {
                
                RegistrationForm regForm = new RegistrationForm();
                regForm.Show();
                this.Hide();
            }
            else
            {
                
                MessageBox.Show("Invalid Login credentials, Please check Username and Password and try again",
                    "Invalid Login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            usernametxt.Clear();
            passwordtxt.Clear();

        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, Do you really want to Exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
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

        private void eyeopenbtn_Click(object sender, EventArgs e)
        {
            if (passwordtxt.PasswordChar == '●')
            {
                eyeclosebtn.Visible=true;
                eyeopenbtn.Visible=false;
                passwordtxt.PasswordChar = '\0';
            }
        }

        private void eyeclosebtn_Click(object sender, EventArgs e)
        {
            if (passwordtxt.PasswordChar == '\0')
            {
                eyeopenbtn.Visible=true;
                eyeclosebtn.Visible = false;
                passwordtxt.PasswordChar = '●';
            }
        }
    }
}
