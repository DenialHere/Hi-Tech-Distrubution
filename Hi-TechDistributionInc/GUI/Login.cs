using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hi_TechDistributionInc.BLL;
using Hi_TechDistributionInc.DAL;

namespace Hi_TechDistributionInc.GUI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBoxUserName.Text;
            string password = textBoxPassword.Text;

            Users use = new Users();
            use = use.Login(userName, password);
            if (use != null)
            {
                if (use.AccessLevel == 0)
                {
                    MessageBox.Show("Your account has been denied access to this application. Your access level doesn't allow you to access any part of the system. Please contact the administrator if you think this is a mistake.", "Access level error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Form misManager = new Mis_Manager(use);
                    misManager.ShowDialog();
                    this.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Invalid Username or password", "Invalid login credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Remember == true && Properties.Settings.Default.Username != string.Empty)
            {
                textBoxUserName.Text = Properties.Settings.Default.Username;
                textBoxPassword.Text = Properties.Settings.Default.Password;
                checkBoxRememberMe.Checked = true;
            }
            if (Properties.Settings.Default.Skiplogin == true && Properties.Settings.Default.Remember == true)
            {
                string userName = textBoxUserName.Text;
                string password = textBoxPassword.Text;

                Users use = new Users();
                use = use.Login(userName, password);
                if (use != null)
                {
                    Form misManager = new Mis_Manager(use);
                    misManager.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Username or password", "Invalid login credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void checkBoxDeveloper_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDeveloper.Checked == true)
            {
                buttonManager.Visible = true;
                buttonSalesManager.Visible = true;
                buttonController.Visible = true;
                buttonOrderJenifer.Visible = true;
                buttonOrderMary.Visible = true;
            }
            else
            {
                buttonManager.Visible = false;
                buttonSalesManager.Visible = false;
                buttonController.Visible = false;
                buttonOrderJenifer.Visible = false;
                buttonOrderMary.Visible = false;
            }
        }

        private void buttonManager_Click(object sender, EventArgs e)
        {
            textBoxUserName.Text = "Hbrown2020";
            textBoxPassword.Text = "2020";
            Properties.Settings.Default.Remember = false;
            Properties.Settings.Default.Save();
            checkBoxRememberMe.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxUserName.Text = "Tmoore2020";
            textBoxPassword.Text = "2020";
            Properties.Settings.Default.Save();
            checkBoxRememberMe.Checked = false;
        }

        private void checkBoxPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPassword.Checked == true)
            {
                textBoxPassword.UseSystemPasswordChar = true;
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = false;
            }

        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRememberMe.Checked == true)
            {
                Properties.Settings.Default.Username = textBoxUserName.Text;
                Properties.Settings.Default.Password = textBoxPassword.Text;
                Properties.Settings.Default.Remember = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = null;
                Properties.Settings.Default.Password = null;
                Properties.Settings.Default.Remember = false;
                Properties.Settings.Default.Save();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please contact the administrator to reset your password." ,"Password reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBoxUserName.Text = "Pwang2020";
            textBoxPassword.Text = "2020";
            Properties.Settings.Default.Save();
            checkBoxRememberMe.Checked = false;
        }

        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
            checkBoxRememberMe.Checked = false;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            textBoxUserName.Text = "Mbrown2020";
            textBoxPassword.Text = "2020";
            Properties.Settings.Default.Save();
            checkBoxRememberMe.Checked = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBoxUserName.Text = "Jbouchard2020";
            textBoxPassword.Text = "2020";
            Properties.Settings.Default.Save();
            checkBoxRememberMe.Checked = false;
        }
    }
}
