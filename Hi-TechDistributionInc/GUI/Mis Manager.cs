using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hi_TechDistributionInc.DAL;
using Hi_TechDistributionInc.BLL;
using System.IO;
using System.Data.SqlClient;
using Hi_TechDistributionInc.Models;
using Hi_TechDistributionInc.VALIDATION;

namespace Hi_TechDistributionInc.GUI
{


    public partial class Mis_Manager : Form
    {
        SqlDataAdapter da;
        DataSet dsCustomerDB;
        DataTable dtCustomers;
        SqlCommandBuilder sqlBuilder;
        HiTechDistributionDBEntities1 dBEntities = new HiTechDistributionDBEntities1();

        BLL.Employee Emp = new BLL.Employee();
        string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        public Mis_Manager(Users use)
        {
            InitializeComponent();

            //Tab Control
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);

            //Removing tabs that are not relevant to the person based on their job/ Access Level
            labelUserName.Text = use.UserName;
            labelUserNameWelc.Text = use.UserName + ",";
            if (use.AccessLevel == 1)
            {
                tabControl1.TabPages.Remove(Customer);
                tabControl1.TabPages.Remove(Books);
                tabControl1.TabPages.Remove(Category);
                tabControl1.TabPages.Remove(Publishers);
                tabControl1.TabPages.Remove(AuthorBooks);
                tabControl1.TabPages.Remove(Author);
                tabControl1.TabPages.Remove(Orders);
                tabControl1.SelectedTab = Employee;
            }
            if (use.AccessLevel == 2)
            {
                tabControl1.TabPages.Remove(User);
                tabControl1.TabPages.Remove(Employee);
                tabControl1.TabPages.Remove(Books);
                tabControl1.TabPages.Remove(Category);
                tabControl1.TabPages.Remove(Publishers);
                tabControl1.TabPages.Remove(AuthorBooks);
                tabControl1.TabPages.Remove(Author);
                tabControl1.TabPages.Remove(Orders);
                tabControl1.SelectedTab = Customer;
            }
            if (use.AccessLevel == 3)
            {
                tabControl1.TabPages.Remove(User);
                tabControl1.TabPages.Remove(Employee);
                tabControl1.TabPages.Remove(Customer);
                tabControl1.TabPages.Remove(Orders);
                tabControl1.SelectedTab = Books;
            }
            if (use.AccessLevel == 4)
            {
                tabControl1.TabPages.Remove(Customer);
                tabControl1.TabPages.Remove(Employee);
                tabControl1.TabPages.Remove(Books);
                tabControl1.TabPages.Remove(Category);
                tabControl1.TabPages.Remove(Publishers);
                tabControl1.TabPages.Remove(AuthorBooks);
                tabControl1.TabPages.Remove(Author);
                tabControl1.TabPages.Remove(User);
                tabControl1.SelectedTab = Orders;
            }


            //Change name of tab based on which tab is active
            if (tabControl1.SelectedTab == User)
            {
                this.Text = "User Management";
            }
            if (tabControl1.SelectedTab == Employee)
            {
                this.Text = "Employee Management";
            }
            if (tabControl1.SelectedTab == Customer)
            {
                this.Text = "Customer Management";
            }
            if (tabControl1.SelectedTab == Books)
            {
                this.Text = "Book Management";
            }
            if (tabControl1.SelectedTab == Category)
            {
                this.Text = "Category Management";
            }
            if (tabControl1.SelectedTab == Publishers)
            {
                this.Text = "Publisher Management";
            }
            if (tabControl1.SelectedTab == Publishers)
            {
                this.Text = "Author book Management";
            }
            if (tabControl1.SelectedTab == Author)
            {
                this.Text = "Author Management";
                MessageBox.Show("hi");
            }
            if (tabControl1.SelectedTab == Orders)
            {
                this.Text = "Order Management";
            }


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult exit;
            exit = MessageBox.Show("Are you sure you wish to exit?", "Exit confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (exit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            dataGridViewCustomer.DataSource = dsCustomerDB.Tables["Customers"];

        }

        private void Mis_Manager_Load(object sender, EventArgs e)
        {

            //Customer Management dataset

            dsCustomerDB = new DataSet("CustomerDS");
            dtCustomers = new DataTable("Customers");

            dsCustomerDB.Tables.Add(dtCustomers);

            dtCustomers.Columns.Add("CustomerId", typeof(Int32));
            dtCustomers.Columns.Add("FirstName", typeof(System.String));
            dtCustomers.Columns.Add("LastName", typeof(System.String));
            dtCustomers.Columns.Add("City", typeof(System.String));
            dtCustomers.Columns.Add("Address", typeof(System.String));
            dtCustomers.Columns.Add("PostalCode", typeof(System.String));
            dtCustomers.Columns.Add("PhoneNumber", typeof(System.String));
            dtCustomers.Columns.Add("FaxNumber", typeof(System.String));
            dtCustomers.Columns.Add("CreditLimit", typeof(float));

            dtCustomers.PrimaryKey = new DataColumn[] { dtCustomers.Columns["CustomerId"] };

            da = new SqlDataAdapter("SELECT * FROM Customers", UtilityDB.ConnectDB());
            sqlBuilder = new SqlCommandBuilder(da);

            da.Fill(dsCustomerDB.Tables["Customers"]);


            //Selecting default sort by options
            comboBoxUsers.SelectedIndex = 0;
            comboBoxSearchEmployee.SelectedIndex = 0;
            comboBoxSearchCus.SelectedIndex = 0;
            comboBoxBook.SelectedIndex = 0;
            comboBoxSearchCat.SelectedIndex = 0;
            comboBoxPubSearch.SelectedIndex = 0;
            comboBoxSearchAuthb.SelectedIndex = 0;
            comboBoxSearchAuth.SelectedIndex = 0;
            comboBoxOrders.SelectedIndex = 0;

            //Checking if skip login is active
            if (Properties.Settings.Default.Skiplogin == true)
            {
                checkBoxSkip.Checked = true;
            }


            //Checking if dark mode is active
            if (Properties.Settings.Default.DarkMode == true)
            {
                checkBoxDarkMode.Checked = true;
            }
            else
            {
                checkBoxDarkMode.Checked = false;
            }


            //Reading from text file changelog.txt and printing in welcome tab 
            string line;
            try
            {
                StreamReader sr = new StreamReader(path + @"\ChangeLog.txt");
                line = sr.ReadLine();
                if (File.Exists(path + @"\ChangeLog.txt"))
                {
                    while (line != null)
                    {
                        richTextBoxChangeLog.Text += line;
                        richTextBoxChangeLog.Text += "\n";
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
                else
                {
                    richTextBoxChangeLog.Text += "changelog.txt file is missing or has been deleted";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Change Name of form when clicking on certain tab
            if (tabControl1.SelectedTab == User)
            {
                this.Text = "User Management";
                if (Properties.Settings.Default.DarkMode == true)
                {
                    tabControl1.SelectedTab.BackColor = Color.Black;
                    tabControl1.SelectedTab.ForeColor = Color.White;
                }
                else
                {
                    tabControl1.SelectedTab.BackColor = DefaultBackColor;
                    tabControl1.SelectedTab.ForeColor = DefaultForeColor;
                }
            }
            else if (tabControl1.SelectedTab == Employee)
            {
                this.Text = "Employee Management";
                if (Properties.Settings.Default.DarkMode == true)
                {
                    tabControl1.SelectedTab.BackColor = Color.Black;
                    tabControl1.SelectedTab.ForeColor = Color.White;
                }
                else
                {
                    tabControl1.SelectedTab.BackColor = DefaultBackColor;
                    tabControl1.SelectedTab.ForeColor = DefaultForeColor;
                }
            }
            else if (tabControl1.SelectedTab == Customer)
            {
                this.Text = "Customer Management";
                if (Properties.Settings.Default.DarkMode == true)
                {
                    tabControl1.SelectedTab.BackColor = Color.Black;
                    tabControl1.SelectedTab.ForeColor = Color.White;
                }
                else
                {
                    tabControl1.SelectedTab.BackColor = DefaultBackColor;
                    tabControl1.SelectedTab.ForeColor = DefaultForeColor;
                }
            }
            else if (tabControl1.SelectedTab == Books)
            {
                this.Text = "Book Management";
                if (Properties.Settings.Default.DarkMode == true)
                {
                    tabControl1.SelectedTab.BackColor = Color.Black;
                    tabControl1.SelectedTab.ForeColor = Color.White;
                }
                else
                {
                    tabControl1.SelectedTab.BackColor = DefaultBackColor;
                    tabControl1.SelectedTab.ForeColor = DefaultForeColor;
                }
            }
            else if (tabControl1.SelectedTab == Category)
            {
                this.Text = "Category Management";
                if (Properties.Settings.Default.DarkMode == true)
                {
                    tabControl1.SelectedTab.BackColor = Color.Black;
                    tabControl1.SelectedTab.ForeColor = Color.White;
                }
                else
                {
                    tabControl1.SelectedTab.BackColor = DefaultBackColor;
                    tabControl1.SelectedTab.ForeColor = DefaultForeColor;
                }
            }
            else if (tabControl1.SelectedTab == Publishers)
            {
                this.Text = "Publisher Management";
                if (Properties.Settings.Default.DarkMode == true)
                {
                    tabControl1.SelectedTab.BackColor = Color.Black;
                    tabControl1.SelectedTab.ForeColor = Color.White;
                }
                else
                {
                    tabControl1.SelectedTab.BackColor = DefaultBackColor;
                    tabControl1.SelectedTab.ForeColor = DefaultForeColor;
                }
            }
            else if (tabControl1.SelectedTab == AuthorBooks)
            {
                this.Text = "Author Book Management";
                if (Properties.Settings.Default.DarkMode == true)
                {
                    tabControl1.SelectedTab.BackColor = Color.Black;
                    tabControl1.SelectedTab.ForeColor = Color.White;
                }
                else
                {
                    tabControl1.SelectedTab.BackColor = DefaultBackColor;
                    tabControl1.SelectedTab.ForeColor = DefaultForeColor;
                }
            }
            else if (tabControl1.SelectedTab == Author)
            {
                this.Text = "Author Management";
                if (Properties.Settings.Default.DarkMode == true)
                {
                    tabControl1.SelectedTab.BackColor = Color.Black;
                    tabControl1.SelectedTab.ForeColor = Color.White;
                }
                else
                {
                    tabControl1.SelectedTab.BackColor = DefaultBackColor;
                    tabControl1.SelectedTab.ForeColor = DefaultForeColor;
                }
            }
            else if (tabControl1.SelectedTab == Orders)
            {
                this.Text = "Order Management";
                if (Properties.Settings.Default.DarkMode == true)
                {
                    tabControl1.SelectedTab.BackColor = Color.Black;
                    tabControl1.SelectedTab.ForeColor = Color.White;
                }
                else
                {
                    tabControl1.SelectedTab.BackColor = DefaultBackColor;
                    tabControl1.SelectedTab.ForeColor = DefaultForeColor;
                }
            }
            else
            {
                this.Text = "Hi-Tech Distrubution Inc";
                if (Properties.Settings.Default.DarkMode == true)
                {
                    tabControl1.SelectedTab.BackColor = Color.Black;
                    tabControl1.SelectedTab.ForeColor = Color.White;
                }
                else
                {
                    tabControl1.SelectedTab.BackColor = DefaultBackColor;
                    tabControl1.SelectedTab.ForeColor = DefaultForeColor;
                }
            }
        }


        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = textBoxUserNameUsers.Text.Trim();
                string password = textBoxPasswordUsers.Text.Trim();
                Users use = new Users();
                Users Use = new Users();
                Use = Use.SearchRecord(userName);

                if (Use != null)
                {
                    MessageBox.Show("User already exists in the database.", "Save unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Validation
                    if (textBoxUserNameUsers.TextLength > 4 && textBoxPasswordUsers.TextLength > 4)
                    {
                        if (textBoxEmployeeIdUsers.TextLength >= 1 && textBoxEmployeeIdUsers.TextLength != 3)
                        {
                            MessageBox.Show("Employee id must be 3 digits.", "Save unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (textBoxEmployeeIdUsers.TextLength == 3)
                            {
                                int? employeeId = Convert.ToInt32(textBoxEmployeeIdUsers.Text.Trim());
                                use.EmployeeId = employeeId;
                            }
                            use.UserName = userName;
                            use.Password = password;
                            use.SaveUser(use);
                            MessageBox.Show("User: " + use.UserName + " has been added to the database.", "Sucessfully added user", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Username and password must be greater than 4 characters", "Save unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonListUser_Click(object sender, EventArgs e)
        {
            Users use = new Users();
            List<Users> listUser = use.ListAllRecords();
            listViewUsers.Items.Clear();
            if (listUser.Count != 0)
            {
                foreach (Users aUser in listUser)
                {
                    ListViewItem item = new ListViewItem(aUser.UserName);
                    item.SubItems.Add(aUser.Password);
                    item.SubItems.Add(aUser.EmployeeId.ToString());
                    listViewUsers.Items.Add(item);

                }
            }
            else
            {
                MessageBox.Show("Nothing in the Users database", "Database Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdateUser_Click(object sender, EventArgs e)
        {
            DialogResult confirm;
            confirm = MessageBox.Show("Are you sure you wish to update this user?", "Update confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (confirm == DialogResult.Yes)
            {
                try
                {

                    Users use = new Users();
                    string userName = textBoxUserNameUsers.Text.Trim();
                    string password = textBoxPasswordUsers.Text.Trim();
                    Users Use = new Users();
                    Use = Use.SearchRecord(userName);

                    if (Use == null)
                    {
                        MessageBox.Show("User doesn't exists in the database.", "Update unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (textBoxEmployeeIdUsers.TextLength > 0)
                        {
                            int? employeeId = Convert.ToInt32(textBoxEmployeeIdUsers.Text.Trim());
                            use.EmployeeId = employeeId;
                        }

                        use.UserName = userName;
                        use.Password = password;
                        use.UpdateRecord(use);
                        MessageBox.Show("User: " + use.UserName + " has been updated.", "Sucessfully updated user", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Update input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            DialogResult confirm;
            confirm = MessageBox.Show("Are you sure you wish to delete this user?", "Deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    Users use = new Users();
                    string userName = textBoxUserNameUsers.Text.Trim();
                    Users Use = new Users();
                    Use = Use.SearchRecord(userName);
                    if (Use == null)
                    {
                        MessageBox.Show("User doesn't exists in the database.", "Delete unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        use.UserName = userName;
                        use.DeleteRecord(use);
                        MessageBox.Show("User: " + use.UserName + " has been deleted.", "Sucessfully deleted user", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSearchUser_Click(object sender, EventArgs e)
        {
            try
            {


                if (comboBoxUsers.SelectedIndex == 0)
                {
                    string input = textBoxSearchUsers.Text.Trim();
                    Users use = new Users();
                    //Search By Username
                    use = use.SearchRecord(input);
                    if (use == null)
                    {
                        MessageBox.Show("User doesn't exists in the database.", "Search unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        listViewUsers.Items.Clear();
                        ListViewItem item = new ListViewItem(use.UserName);
                        item.SubItems.Add(use.Password);
                        item.SubItems.Add(use.EmployeeId.ToString());
                        listViewUsers.Items.Add(item);
                    }

                }
                else
                {
                    //Search By EmployeeId
                    int input2 = Convert.ToInt32(textBoxSearchUsers.Text.Trim());
                    Users use = new Users();
                    List<Users> listUse = use.SearchRecord(input2);
                    if (listUse.Count == 0)
                    {
                        MessageBox.Show("User doesn't exists in the database.", "Search unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        listViewUsers.Items.Clear();
                        foreach (Users aUser in listUse)
                        {
                            ListViewItem item = new ListViewItem(aUser.UserName);
                            item.SubItems.Add(aUser.Password);
                            item.SubItems.Add(aUser.EmployeeId.ToString());
                            listViewUsers.Items.Add(item);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Properties.Settings.Default.Skiplogin = false;
            Properties.Settings.Default.Save();
            this.Hide();
            Form login = new Login();
            login.ShowDialog();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            BLL.Employee emp = new BLL.Employee();
            List<BLL.Employee> listEmp = emp.ListAllRecords();
            listViewEmployee.Items.Clear();
            if (listEmp.Count != 0)
            {
                foreach (BLL.Employee anEmp in listEmp)
                {
                    ListViewItem item = new ListViewItem(anEmp.EmployeeId.ToString());
                    item.SubItems.Add(anEmp.FirstName);
                    item.SubItems.Add(anEmp.LastName);
                    item.SubItems.Add(anEmp.PhoneNumber);
                    item.SubItems.Add(anEmp.Email);
                    item.SubItems.Add(anEmp.JobId.ToString());
                    listViewEmployee.Items.Add(item);

                }
            }
            else
            {
                MessageBox.Show("Nothing in the database", "Database Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveEmployee_Click(object sender, EventArgs e)
        {
            try
            {

                int empId = Convert.ToInt32(textBoxEmployeeId.Text.Trim());
                string Fname = textBoxEmployeeFName.Text.Trim();
                string Lname = textBoxEmployeeLname.Text.Trim();
                string PhoneNumber = maskedTextBoxPhoneEmp.Text.Trim();
                string Email = textBoxEmployeeEmail.Text.Trim();
                int Jobid = Convert.ToInt32(textBoxEmployeeJobId.Text.Trim());

                BLL.Employee emp = new BLL.Employee();

                emp = emp.SearchRecord(empId);

                if (emp != null)//Found
                {
                    MessageBox.Show("Employee already exists", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Data Validation
                    if (!(Validator.IsvalidNum(empId.ToString(), 3)))
                    {
                        MessageBox.Show("Invalid Employee ID. Employee ID must be 3 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidString(Fname, 3, 50)))
                    {
                        MessageBox.Show("Invalid first name, must be between 3-50 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidString(Lname, 3, 50)))
                    {
                        MessageBox.Show("Invalid Last name, must be between 3-50 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (PhoneNumber.Length != 14)
                    {
                        MessageBox.Show("Invalid Phone number. Phone number must be 10 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxEmployeeJobId.TextLength > 3 || textBoxEmployeeJobId.TextLength == 0)
                    {
                        MessageBox.Show("Invalid Job ID. Job ID must be between 1-3 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidEmail(Email)))
                    {
                        MessageBox.Show("Invalid Email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Emp.EmployeeId = empId;
                        Emp.FirstName = Fname;
                        Emp.LastName = Lname;
                        Emp.PhoneNumber = PhoneNumber;
                        Emp.Email = Email;
                        Emp.JobId = Jobid;
                        Emp.SaveEmployee(Emp);
                        MessageBox.Show("Employee: " + Emp.FirstName + " " + Emp.LastName + " has been added to the database.", "Sucessfully added employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSearchEmployee_Click(object sender, EventArgs e)
        {
            if (comboBoxSearchEmployee.SelectedIndex == 0)
            {
                try
                {
                    BLL.Employee emp = new BLL.Employee();
                    int empId = Convert.ToInt32(textBoxSearchEmployee.Text.Trim());
                    emp = emp.SearchRecord(empId);


                    if (emp != null)
                    {
                        listViewEmployee.Items.Clear();
                        ListViewItem item = new ListViewItem(emp.EmployeeId.ToString());
                        item.SubItems.Add(emp.FirstName);
                        item.SubItems.Add(emp.LastName);
                        item.SubItems.Add(emp.PhoneNumber);
                        item.SubItems.Add(emp.Email);
                        item.SubItems.Add(emp.JobId.ToString());
                        listViewEmployee.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Employee Not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (comboBoxSearchEmployee.SelectedIndex == 1)
            {
                try
                {
                    BLL.Employee emp = new BLL.Employee();
                    List<BLL.Employee> listEmp = emp.SearchRecord(textBoxSearchEmployee.Text.Trim());

                    if (listEmp.Count != 0)
                    {
                        listViewEmployee.Items.Clear();
                        foreach (BLL.Employee anEmp in listEmp)
                        {
                            ListViewItem item = new ListViewItem(anEmp.EmployeeId.ToString());
                            item.SubItems.Add(anEmp.FirstName);
                            item.SubItems.Add(anEmp.LastName);
                            item.SubItems.Add(anEmp.PhoneNumber);
                            item.SubItems.Add(anEmp.Email);
                            item.SubItems.Add(anEmp.JobId.ToString());
                            listViewEmployee.Items.Add(item);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee Not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (comboBoxSearchEmployee.SelectedIndex == 2)
            {
                try
                {
                    BLL.Employee emp = new BLL.Employee();
                    List<BLL.Employee> listEmp = emp.SearchJobId(Convert.ToInt32(textBoxSearchEmployee.Text.Trim()));
                    if (listEmp.Count != 0)
                    {
                        listViewEmployee.Items.Clear();
                        foreach (BLL.Employee anEmp in listEmp)
                        {
                            ListViewItem item = new ListViewItem(anEmp.EmployeeId.ToString());
                            item.SubItems.Add(anEmp.FirstName);
                            item.SubItems.Add(anEmp.LastName);
                            item.SubItems.Add(anEmp.PhoneNumber);
                            item.SubItems.Add(anEmp.Email);
                            item.SubItems.Add(anEmp.JobId.ToString());
                            listViewEmployee.Items.Add(item);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Job title not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonUpdateEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                int empId = Convert.ToInt32(textBoxEmployeeId.Text.Trim());
                string Fname = textBoxEmployeeFName.Text.Trim();
                string Lname = textBoxEmployeeLname.Text.Trim();
                string PhoneNumber = maskedTextBoxPhoneEmp.Text.Trim();
                string Email = textBoxEmployeeEmail.Text.Trim();
                int Jobid = Convert.ToInt32(textBoxEmployeeJobId.Text.Trim());


                BLL.Employee emp = new BLL.Employee();
                emp = emp.SearchRecord(empId);
                if (emp != null)
                {
                    //Data Validation
                    if (!(Validator.IsvalidNum(empId.ToString(), 3)))
                    {
                        MessageBox.Show("Invalid Employee ID. Employee ID must be 3 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidString(Fname, 3, 50)))
                    {
                        MessageBox.Show("Invalid first name, must be between 3-50 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidString(Lname, 3, 50)))
                    {
                        MessageBox.Show("Invalid Last name, must be between 3-50 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (PhoneNumber.Length != 14)
                    {
                        MessageBox.Show("Invalid Phone number. Phone number must be 10 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxEmployeeJobId.TextLength > 3 || textBoxEmployeeJobId.TextLength == 0)
                    {
                        MessageBox.Show("Invalid Job ID. Job ID must be between 1-3 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidEmail(Email)))
                    {
                        MessageBox.Show("Invalid Email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Emp.EmployeeId = empId;
                        Emp.FirstName = Fname;
                        Emp.LastName = Lname;
                        Emp.PhoneNumber = PhoneNumber;
                        Emp.Email = Email;
                        Emp.JobId = Jobid;
                        Emp.UpdateRecord(Emp);
                        MessageBox.Show("Employee: " + Emp.EmployeeId + ": records have been updated.", "Sucessfully updated employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Employee Not found", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleteEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                int empId = Convert.ToInt32(textBoxEmployeeId.Text.Trim());
                BLL.Employee emp = new BLL.Employee();
                emp = emp.SearchRecord(empId);
                if (emp != null)
                {
                    DialogResult cancel;
                    cancel = MessageBox.Show("Are you sure you wish to delete this employee?", "Exit confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (cancel == DialogResult.Yes)
                    {
                        Emp.EmployeeId = empId;
                        Emp.DeleteRecord(Emp);
                        MessageBox.Show("Employee: " + Emp.EmployeeId + ": has been deleted.", "Sucessfully deleted employee", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    MessageBox.Show("Employee not found", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    int empId = Convert.ToInt32(textBoxEmployeeId.Text.Trim());
                    var searchEmp = dBEntities.Users.SingleOrDefault(n => n.EmployeeId == empId);
                    if (searchEmp != null)
                    {
                        //If Employee cannot be deleted set access level to 0 so they can't access the application
                        DialogResult confirm;
                        confirm = MessageBox.Show("Employee cannot be deleted, set their account to inactive?", "Deletion error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (confirm == DialogResult.Yes)
                        {
                            searchEmp.AccessLevel = 0;
                            dBEntities.SaveChanges();

                        }
                    }

                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.Message, "Delete input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                

            }
        }

        private void textBoxPasswordUsers_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPasswordUsers.TextLength < 4 && textBoxPasswordUsers.TextLength > 0)
            {
                labelPassword.Visible = true;
                labelPassword.ForeColor = System.Drawing.Color.Red;
                labelPassword.Text = "Weak";
            }
            else if (textBoxPasswordUsers.TextLength < 8 && textBoxPasswordUsers.TextLength > 0)
            {
                labelPassword.Visible = true;
                labelPassword.ForeColor = System.Drawing.Color.Orange;
                labelPassword.Text = "Okay";
            }
            else if (textBoxPasswordUsers.TextLength >= 8 && textBoxPasswordUsers.TextLength > 0)
            {
                labelPassword.Visible = true;
                labelPassword.ForeColor = System.Drawing.Color.Green;
                labelPassword.Text = "Good";
            }
            else
            {
                labelPassword.Visible = false;
            }
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDarkMode.Checked == true)
            {
                //gloabl
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
                tabControl1.SelectedTab.BackColor = Color.Black;
                tabControl1.SelectedTab.ForeColor = Color.White;
                Properties.Settings.Default.DarkMode = true;
                Properties.Settings.Default.Save();
                linkLabel1.LinkColor = Color.Red;
                //Welcome page
                buttonChangePass.BackColor = Color.Green;
                buttonExit.BackColor = Color.Green;
                groupBoxChangePass.ForeColor = Color.White;
                groupBoxAppOpp.ForeColor = Color.White;
                //Customer
                buttonSave.BackColor = Color.Green;
                buttonUpdate.BackColor = Color.Green;
                buttonDelete.BackColor = Color.Green;
                buttonList.BackColor = Color.Green;
                buttonSearch.BackColor = Color.Green;
                buttonUpdateDb.BackColor = Color.Green;
                groupBoxCustomer.ForeColor = Color.White;
                groupBoxSearchCust.ForeColor = Color.White;
                dataGridViewCustomer.ForeColor = Color.Black;
                //Employee
                buttonSaveEmployee.BackColor = Color.Green;
                buttonUpdateEmployee.BackColor = Color.Green;
                buttonDeleteEmployee.BackColor = Color.Green;
                buttonListEmployee.BackColor = Color.Green;
                buttonSearchEmployee.BackColor = Color.Green;
                groupBoxEmployee.ForeColor = Color.White;
                groupBoxSearchEmp.ForeColor = Color.White;
                //User
                buttonSaveUser.BackColor = Color.Green;
                buttonUpdateUser.BackColor = Color.Green;
                buttonDeleteUser.BackColor = Color.Green;
                buttonListUser.BackColor = Color.Green;
                buttonSearchUser.BackColor = Color.Green;
                groupBoxUserInfo.ForeColor = Color.White;
                groupBoxSearchUser.ForeColor = Color.White;
                //Books
                buttonSaveBook.BackColor = Color.Green;
                buttonUpdateBook.BackColor = Color.Green;
                buttonDeleteBook.BackColor = Color.Green;
                buttonListBook.BackColor = Color.Green;
                buttonSearchBook.BackColor = Color.Green;
                groupBoxBookInfo.ForeColor = Color.White;
                groupBoxSearchBook.ForeColor = Color.White;
                linkLabelCatId.LinkColor = Color.LightBlue;
                linkLabelPubId.LinkColor = Color.LightBlue;
                //Category
                buttonSaveCat.BackColor = Color.Green;
                buttonUpdateCat.BackColor = Color.Green;
                buttonDeleteCat.BackColor = Color.Green;
                buttonListCat.BackColor = Color.Green;
                buttonSearchCat.BackColor = Color.Green;
                groupBoxCatInfo.ForeColor = Color.White;
                groupBoxCatSearch.ForeColor = Color.White;
                //Publisher
                buttonSavePub.BackColor = Color.Green;
                buttonUpdatePub.BackColor = Color.Green;
                buttonDeletePub.BackColor = Color.Green;
                buttonListPub.BackColor = Color.Green;
                buttonPubSearch.BackColor = Color.Green;
                groupBoxPubInfo.ForeColor = Color.White;
                groupBoxPubSearch.ForeColor = Color.White;
                //Author Books
                buttonSaveAuthB.BackColor = Color.Green;
                buttonUpdateAuthB.BackColor = Color.Green;
                buttonDeleteAuthB.BackColor = Color.Green;
                buttonListAuthBook.BackColor = Color.Green;
                buttonSearchAuthb.BackColor = Color.Green;
                groupBoxAuthorBInfo.ForeColor = Color.White;
                groupBoxSearchAuthB.ForeColor = Color.White;
                //Author 
                buttonSaveAuth.BackColor = Color.Green;
                buttonUpdateAuth.BackColor = Color.Green;
                buttonDeleteAuth.BackColor = Color.Green;
                buttonListAuth.BackColor = Color.Green;
                buttonSearchAuth.BackColor = Color.Green;
                groupBoxAuthInfo.ForeColor = Color.White;
                groupBoxSearchAuth.ForeColor = Color.White;
                //Orders
                buttonSaveOrder.BackColor = Color.Green;
                buttonUpdateOrder.BackColor = Color.Green;
                buttonDeleteOrder.BackColor = Color.Green;
                buttonListOrder.BackColor = Color.Green;
                buttonSearchOrder.BackColor = Color.Green;
                groupBoxOrders.ForeColor = Color.White;
                groupBoxSearchOrder.ForeColor = Color.White;
                linkLabel2.LinkColor = Color.LightBlue;
                linkLabel3.LinkColor = Color.LightBlue;
            }
            else
            {
                //Global
                this.BackColor = DefaultBackColor;
                this.ForeColor = DefaultForeColor;
                tabControl1.SelectedTab.BackColor = DefaultBackColor;
                tabControl1.SelectedTab.ForeColor = DefaultForeColor;
                Properties.Settings.Default.DarkMode = false;
                Properties.Settings.Default.Save();
                linkLabel1.LinkColor = default;
                //Welcome
                buttonChangePass.BackColor = DefaultBackColor;
                buttonExit.BackColor = DefaultBackColor;
                groupBoxChangePass.ForeColor = DefaultForeColor;
                groupBoxAppOpp.ForeColor = DefaultForeColor;
                //Customer
                buttonSave.BackColor = DefaultBackColor;
                buttonUpdate.BackColor = DefaultBackColor;
                buttonDelete.BackColor = DefaultBackColor;
                buttonList.BackColor = DefaultBackColor;
                buttonSearch.BackColor = DefaultBackColor;
                buttonUpdateDb.BackColor = DefaultBackColor;
                groupBoxCustomer.ForeColor = DefaultForeColor;
                groupBoxSearchCust.ForeColor = DefaultForeColor;
                //Employee
                buttonSaveEmployee.BackColor = DefaultBackColor;
                buttonUpdateEmployee.BackColor = DefaultBackColor;
                buttonDeleteEmployee.BackColor = DefaultBackColor;
                buttonListEmployee.BackColor = DefaultBackColor;
                buttonSearchEmployee.BackColor = DefaultBackColor;
                groupBoxEmployee.ForeColor = DefaultForeColor;
                groupBoxSearchEmp.ForeColor = DefaultForeColor;
                //User
                buttonSaveUser.BackColor = DefaultBackColor;
                buttonUpdateUser.BackColor = DefaultBackColor;
                buttonDeleteUser.BackColor = DefaultBackColor;
                buttonListUser.BackColor = DefaultBackColor;
                buttonSearchUser.BackColor = DefaultBackColor;
                groupBoxUserInfo.ForeColor = DefaultForeColor;
                groupBoxSearchUser.ForeColor = DefaultForeColor;
                //Books
                buttonSaveBook.BackColor = DefaultBackColor;
                buttonUpdateBook.BackColor = DefaultBackColor;
                buttonDeleteBook.BackColor = DefaultBackColor;
                buttonListBook.BackColor = DefaultBackColor;
                buttonSearchBook.BackColor = DefaultBackColor;
                groupBoxBookInfo.ForeColor = DefaultForeColor;
                groupBoxSearchBook.ForeColor = DefaultForeColor;
                linkLabelCatId.LinkColor = default;
                linkLabelPubId.LinkColor = default;
                //Category
                buttonSaveCat.BackColor = DefaultBackColor;
                buttonUpdateCat.BackColor = DefaultBackColor;
                buttonDeleteCat.BackColor = DefaultBackColor;
                buttonListCat.BackColor = DefaultBackColor;
                buttonSearchCat.BackColor = DefaultBackColor;
                groupBoxCatInfo.ForeColor = DefaultForeColor;
                groupBoxCatSearch.ForeColor = DefaultForeColor;
                //Publisher
                buttonSavePub.BackColor = DefaultBackColor;
                buttonUpdatePub.BackColor = DefaultBackColor;
                buttonDeletePub.BackColor = DefaultBackColor;
                buttonListPub.BackColor = DefaultBackColor;
                buttonPubSearch.BackColor = DefaultBackColor;
                groupBoxPubInfo.ForeColor = DefaultForeColor;
                groupBoxPubSearch.ForeColor = DefaultForeColor;
                //Author Books
                buttonSaveAuthB.BackColor = DefaultBackColor;
                buttonUpdateAuthB.BackColor = DefaultBackColor;
                buttonDeleteAuthB.BackColor = DefaultBackColor;
                buttonListAuthBook.BackColor = DefaultBackColor;
                buttonSearchAuthb.BackColor = DefaultBackColor;
                groupBoxAuthorBInfo.ForeColor = DefaultForeColor;
                groupBoxSearchAuthB.ForeColor = DefaultForeColor;
                //Author 
                buttonSaveAuth.BackColor = DefaultBackColor;
                buttonUpdateAuth.BackColor = DefaultBackColor;
                buttonDeleteAuth.BackColor = DefaultBackColor;
                buttonListAuth.BackColor = DefaultBackColor;
                buttonSearchAuth.BackColor = DefaultBackColor;
                groupBoxAuthInfo.ForeColor = DefaultForeColor;
                groupBoxSearchAuth.ForeColor = DefaultForeColor;
                //Orders
                buttonSaveOrder.BackColor = DefaultBackColor;
                buttonUpdateOrder.BackColor = DefaultBackColor;
                buttonDeleteOrder.BackColor = DefaultBackColor;
                buttonListOrder.BackColor = DefaultBackColor;
                buttonSearchOrder.BackColor = DefaultBackColor;
                groupBoxOrders.ForeColor = DefaultForeColor;
                groupBoxSearchOrder.ForeColor = DefaultForeColor;
                linkLabel2.LinkColor = default;
                linkLabel3.LinkColor = default;
            }

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBoxSkip.Checked == true)
            {
                Properties.Settings.Default.Skiplogin = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Skiplogin = false;
                Properties.Settings.Default.Save();
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult exit;
            exit = MessageBox.Show("Are you sure you wish to exit?", "Exit confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (exit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxNewPass.TextLength >= 4)
                {

                    if (textBoxNewPass.Text == textBoxNewPassCon.Text)
                    {
                        Users use = new Users();
                        string userName = labelUserName.Text.Trim();
                        string password = textBoxNewPass.Text.Trim();
                        Users Use = new Users();
                        Use = Use.SearchRecord(userName);

                        if (Use == null)
                        {
                            MessageBox.Show("User doesn't exists in the database.", "Update unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            use.UserName = userName;
                            use.Password = password;
                            use.ChangePassword(use);
                            MessageBox.Show("Your password has been updated.", "Sucessfully updated password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Passwords don't match.", "Update unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Passwords must be greater than 4 characters", "Update unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            da.Fill(dsCustomerDB.Tables["Customers"]);
            dataGridViewCustomer.DataSource = dsCustomerDB.Tables["Customers"];
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Data Validation
                string pNum = maskedTextBoxCusPNum.Text.ToString();
                if (textBoxCustId.TextLength > 3 || textBoxCustId.TextLength == 0)
                {
                    MessageBox.Show("Invalid Customer ID. Customer ID must be between 1-3 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(Validator.IsvalidString(textBoxFirstName.Text, 3, 50)))
                {
                    MessageBox.Show("Invalid first name, must be between 3-50 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(Validator.IsvalidString(textBoxLastName.Text, 3, 50)))
                {
                    MessageBox.Show("Invalid Last name, must be between 3-50 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(Validator.IsvalidString(textBoxCity.Text, 3, 75)))
                {
                    MessageBox.Show("Invalid city name, must be between 3-75 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(Validator.IsvalidAddress(textBoxAddress.Text)))
                {
                    MessageBox.Show("Invalid address, must be atleast 1 number followed by at least 2 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(Validator.IsvalidPostalCode(textBoxPostalCode.Text)))
                {
                    MessageBox.Show("Invalid Postal Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (pNum.Length != 14)
                {
                    MessageBox.Show("Invalid Phone number. Phone number must be 10 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBoxFaxNumber.TextLength != 10)
                {
                    MessageBox.Show("Invalid fax number. Fax number must be 10 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataRow dr = dtCustomers.NewRow();
                    dr["CustomerId"] = textBoxCustId.Text.Trim();
                    dr["FirstName"] = textBoxFirstName.Text.Trim();
                    dr["LastName"] = textBoxLastName.Text.Trim();
                    dr["City"] = textBoxCity.Text.Trim();
                    dr["Address"] = textBoxAddress.Text.Trim();
                    dr["PostalCode"] = textBoxPostalCode.Text.Trim();
                    dr["PhoneNumber"] = maskedTextBoxCusPNum.Text.Trim();
                    dr["FaxNumber"] = textBoxFaxNumber.Text.Trim();
                    dr["CreditLimit"] = textBoxCreditLimit.Text.Trim();
                    dtCustomers.Rows.Add(dr);
                    MessageBox.Show("Customer added, make sure to click update database to save changes.", "Added Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = dtCustomers.Rows.Find(textBoxCustId.Text.Trim());
                if (dr != null)
                {
                    //Data Validation
                    string pNum = maskedTextBoxCusPNum.Text.ToString();
                    if (textBoxCustId.TextLength > 3 || textBoxCustId.TextLength == 0)
                    {
                        MessageBox.Show("Invalid Customer ID. Customer ID must be between 1-3 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidString(textBoxFirstName.Text, 3, 50)))
                    {
                        MessageBox.Show("Invalid first name, must be between 3-50 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidString(textBoxLastName.Text, 3, 50)))
                    {
                        MessageBox.Show("Invalid Last name, must be between 3-50 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidString(textBoxCity.Text, 3, 75)))
                    {
                        MessageBox.Show("Invalid city name, must be between 3-75 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidAddress(textBoxAddress.Text)))
                    {
                        MessageBox.Show("Invalid address, must be atleast 1 number followed by at least 2 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidPostalCode(textBoxPostalCode.Text)))
                    {
                        MessageBox.Show("Invalid Postal Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (pNum.Length != 14)
                    {
                        MessageBox.Show("Invalid Phone number. Phone number must be 10 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxFaxNumber.TextLength != 10)
                    {
                        MessageBox.Show("Invalid fax number. Fax number must be 10 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        dr["CustomerId"] = textBoxCustId.Text.Trim();
                        dr["FirstName"] = textBoxFirstName.Text.Trim();
                        dr["LastName"] = textBoxLastName.Text.Trim();
                        dr["City"] = textBoxCity.Text.Trim();
                        dr["Address"] = textBoxAddress.Text.Trim();
                        dr["PostalCode"] = textBoxPostalCode.Text.Trim();
                        dr["PhoneNumber"] = maskedTextBoxCusPNum.Text.Trim();
                        dr["FaxNumber"] = textBoxFaxNumber.Text.Trim();
                        dr["CreditLimit"] = textBoxCreditLimit.Text.Trim();
                        MessageBox.Show("Customer modified, make sure to click update database to save changes.", "Updated Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Customer not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = dtCustomers.Rows.Find(textBoxCustId.Text.Trim());
                if (dr != null)
                {
                    DialogResult cancel;
                    cancel = MessageBox.Show("Are you sure you wish to delete this Customer?", "Exit confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (cancel == DialogResult.Yes)
                    {
                        dr.Delete();
                        MessageBox.Show("Customer Deleted, make sure to click update database to save changes.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Customer not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonUpdateDb_Click(object sender, EventArgs e)
        {
            try
            {
                da.Update(dsCustomerDB.Tables["Customers"]);
                MessageBox.Show("Database has been updated sucessfully.", "Confirmation");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = dtCustomers.Rows.Find(textBoxSearch.Text.Trim());
                if (dr != null)
                {
                    textBoxCustId.Text = dr["CustomerId"].ToString();
                    textBoxFirstName.Text = dr["FirstName"].ToString();
                    textBoxLastName.Text = dr["LastName"].ToString();
                    textBoxCity.Text = dr["City"].ToString();
                    textBoxAddress.Text = dr["Address"].ToString();
                    textBoxPostalCode.Text = dr["PostalCode"].ToString();
                    maskedTextBoxCusPNum.Text = dr["PhoneNumber"].ToString();
                    textBoxFaxNumber.Text = dr["FaxNumber"].ToString();
                    textBoxCreditLimit.Text = dr["CreditLimit"].ToString();
                }
                else
                {
                    MessageBox.Show("Customer not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonListBook_Click(object sender, EventArgs e)
        {
            listViewBooks.Items.Clear();
            var listBooks = (from book in dBEntities.Books
                             select book).ToList<Book>();
            if (listBooks.Count != 0)
            {
                foreach (Book aBook in listBooks)
                {
                    ListViewItem item = new ListViewItem(aBook.ISBN.ToString());
                    item.SubItems.Add(aBook.BookTitle);
                    item.SubItems.Add(aBook.QOH.ToString());
                    item.SubItems.Add(aBook.CatergoryId.ToString());
                    item.SubItems.Add(aBook.PublisherId.ToString());
                    item.SubItems.Add(aBook.Price.ToString());
                    listViewBooks.Items.Add(item);

                }
            }
            else
            {
                MessageBox.Show("Book Database is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string listOfCat = "";
            var listCat = (from cat in dBEntities.Catergories
                           select cat).ToList<Catergory>();
            if (listCat.Count != 0)
            {
                foreach (Catergory aCat in listCat)
                {
                    listOfCat += "Id: " + aCat.CatergoryId + "  " + aCat.CatergoryName + "\n";

                }
                MessageBox.Show(listOfCat, "List Of Catergories", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Catergory Database is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string listOfPub = "";
            var listPub = (from pub in dBEntities.Publishers
                           select pub).ToList<Publisher>();
            if (listPub.Count != 0)
            {
                foreach (Publisher apub in listPub)
                {
                    listOfPub += "Id: " + apub.PublisherId + "  " + apub.PublisherName + "\n";

                }
                MessageBox.Show(listOfPub, "List Of Publishers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Publisher Database is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSearchBook_Click(object sender, EventArgs e)
        {

            listViewBooks.Items.Clear();
            if (comboBoxBook.SelectedIndex == 0)
            {
                try
                {
                    long isbn = Convert.ToInt64(textBoxSearchBook.Text.Trim());
                    var searchIsbn = dBEntities.Books.Where(n => n.ISBN == isbn).ToList<Book>();
                    if (searchIsbn.Count != 0)
                    {
                        foreach (Book aBook in searchIsbn)
                        {
                            ListViewItem item = new ListViewItem(aBook.ISBN.ToString());
                            item.SubItems.Add(aBook.BookTitle);
                            item.SubItems.Add(aBook.QOH.ToString());
                            item.SubItems.Add(aBook.CatergoryId.ToString());
                            item.SubItems.Add(aBook.PublisherId.ToString());
                            item.SubItems.Add(aBook.Price.ToString());
                            listViewBooks.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Book Not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBoxBook.SelectedIndex == 1)
            {
                try
                {
                    string title = textBoxSearchBook.Text.Trim();
                    var searchTitle = dBEntities.Books.Where(n => n.BookTitle == title).ToList<Book>();
                    if (searchTitle.Count != 0)
                    {
                        foreach (Book aBook in searchTitle)
                        {
                            ListViewItem item = new ListViewItem(aBook.ISBN.ToString());
                            item.SubItems.Add(aBook.BookTitle);
                            item.SubItems.Add(aBook.QOH.ToString());
                            item.SubItems.Add(aBook.CatergoryId.ToString());
                            item.SubItems.Add(aBook.PublisherId.ToString());
                            item.SubItems.Add(aBook.Price.ToString());
                            listViewBooks.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Book Not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBoxBook.SelectedIndex == 2)
            {
                try
                {
                    int catId = Convert.ToInt32(textBoxSearchBook.Text.Trim());
                    var searchCatId = dBEntities.Books.Where(n => n.CatergoryId == catId).ToList<Book>();
                    if (searchCatId.Count != 0)
                    {
                        foreach (Book aBook in searchCatId)
                        {
                            ListViewItem item = new ListViewItem(aBook.ISBN.ToString());
                            item.SubItems.Add(aBook.BookTitle);
                            item.SubItems.Add(aBook.QOH.ToString());
                            item.SubItems.Add(aBook.CatergoryId.ToString());
                            item.SubItems.Add(aBook.PublisherId.ToString());
                            item.SubItems.Add(aBook.Price.ToString());
                            listViewBooks.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Book Not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBoxBook.SelectedIndex == 3)
            {
                try
                {
                    int pubId = Convert.ToInt32(textBoxSearchBook.Text.Trim());
                    var searchPubId = dBEntities.Books.Where(n => n.PublisherId == pubId).ToList<Book>();
                    if (searchPubId.Count != 0)
                    {
                        foreach (Book aBook in searchPubId)
                        {
                            ListViewItem item = new ListViewItem(aBook.ISBN.ToString());
                            item.SubItems.Add(aBook.BookTitle);
                            item.SubItems.Add(aBook.QOH.ToString());
                            item.SubItems.Add(aBook.CatergoryId.ToString());
                            item.SubItems.Add(aBook.PublisherId.ToString());
                            item.SubItems.Add(aBook.Price.ToString());
                            listViewBooks.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Book Not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSaveBook_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            try
            {
                //Data Validation
                if (!(Validator.IsvalidNum(textBoxISBN.Text, 13)))
                {
                    MessageBox.Show("Invalid ISBN, must be 13 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBoxBookTitle.TextLength < 3)
                {
                    MessageBox.Show("Invalid book title, must be between 3-50 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBoxQOH.TextLength == 0)
                {
                    MessageBox.Show("Invalid QOH, can't be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBoxCatId.TextLength > 3 || textBoxCatId.TextLength == 0)
                {
                    MessageBox.Show("Invalid category Id, must be between 1-3 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(Validator.IsvalidNum(textBoxPubId.Text, 4)))
                {
                    MessageBox.Show("Invalid publisher Id, must be 4 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBoxPrice.TextLength == 0)
                {
                    MessageBox.Show("Invalid Price, can't be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    long isbn = Convert.ToInt64(textBoxISBN.Text.Trim());
                    var searchIsbn = dBEntities.Books.Where(n => n.ISBN == isbn).ToList<Book>();
                    if (searchIsbn.Count == 0)
                    {
                        book.ISBN = Convert.ToInt64(textBoxISBN.Text.Trim());
                        book.BookTitle = textBoxBookTitle.Text.Trim();
                        book.QOH = Convert.ToInt32(textBoxQOH.Text.Trim());
                        book.CatergoryId = Convert.ToInt32(textBoxCatId.Text.Trim());
                        book.PublisherId = Convert.ToInt32(textBoxPubId.Text.Trim());
                        book.Price = float.Parse(textBoxPrice.Text.Trim());
                        dBEntities.Books.Add(book);
                        dBEntities.SaveChanges();
                        MessageBox.Show("Book saved!", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("A book with the same ISBN already exists.", "ISBN input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    dBEntities.Books.Remove(book);
                }
                catch
                {

                }
            }
        }

        private void textBoxISBN_TextChanged(object sender, EventArgs e)
        {
            if (textBoxISBN.TextLength == 13)
            {
                labelIsbn.Visible = true;
            }
            else
            {
                labelIsbn.Visible = false;
            }
        }

        private void buttonUpdateBook_Click(object sender, EventArgs e)
        {
            try
            {
                long isbn = Convert.ToInt64(textBoxISBN.Text.Trim());
                var searchIsbn = dBEntities.Books.SingleOrDefault(n => n.ISBN == isbn);
                if (searchIsbn != null)
                {
                    //Data Validation
                    if (!(Validator.IsvalidNum(textBoxISBN.Text, 13)))
                    {
                        MessageBox.Show("Invalid ISBN, must be 13 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidString(textBoxBookTitle.Text, 3, 50)))
                    {
                        MessageBox.Show("Invalid book title, must be between 3-50 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxQOH.TextLength == 0)
                    {
                        MessageBox.Show("Invalid QOH, can't be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxCatId.TextLength > 3 || textBoxCatId.TextLength == 0)
                    {
                        MessageBox.Show("Invalid category Id, must be between 1-3 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidNum(textBoxPubId.Text, 4)))
                    {
                        MessageBox.Show("Invalid publisher Id, must be 4 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxPrice.TextLength == 0)
                    {
                        MessageBox.Show("Invalid Price, can't be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        searchIsbn.ISBN = long.Parse(textBoxISBN.Text.Trim());
                        searchIsbn.BookTitle = textBoxBookTitle.Text.Trim();
                        searchIsbn.QOH = Convert.ToInt32(textBoxQOH.Text.Trim());
                        searchIsbn.CatergoryId = Convert.ToInt32(textBoxCatId.Text.Trim());
                        searchIsbn.PublisherId = Convert.ToInt32(textBoxPubId.Text.Trim());
                        searchIsbn.Price = float.Parse(textBoxPrice.Text.Trim());
                        dBEntities.SaveChanges();
                        MessageBox.Show("Book Updated!", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                else
                {
                    MessageBox.Show("Book doesn't exist", "Book not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonDeleteBook_Click(object sender, EventArgs e)
        {
            try
            {
                long isbn = Convert.ToInt64(textBoxISBN.Text.Trim());
                var searchIsbn = dBEntities.Books.SingleOrDefault(n => n.ISBN == isbn);
                if (searchIsbn != null)
                {
                    DialogResult confirm;
                    confirm = MessageBox.Show("Are you sure you wish to delete this book?", "Deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (confirm == DialogResult.Yes)
                    {
                        dBEntities.Books.Remove(searchIsbn);
                        dBEntities.SaveChanges();
                        MessageBox.Show("Book deleted.", "Deletion successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Book doesn't exist", "Book not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Catergory_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listViewCat.Items.Clear();
            var listCat = (from cat in dBEntities.Catergories
                           select cat).ToList<Catergory>();
            if (listCat.Count != 0)
            {
                foreach (Catergory aCat in listCat)
                {
                    ListViewItem item = new ListViewItem(aCat.CatergoryId.ToString());
                    item.SubItems.Add(aCat.CatergoryName);
                    listViewCat.Items.Add(item);

                }
            }
            else
            {
                MessageBox.Show("Category Database is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Catergory cat = new Catergory();
            try
            {
                int catId = Convert.ToInt32(textBoxCategoryId.Text.Trim());
                var searchCatId = dBEntities.Catergories.Where(n => n.CatergoryId == catId).ToList<Catergory>();
                if (searchCatId.Count == 0)
                {
                    if (textBoxCategoryId.TextLength > 3 || textBoxCategoryId.TextLength == 0)
                    {
                        MessageBox.Show("Invalid Category Id, must be between 1-3 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxCategoryName.TextLength < 3)
                    {
                        MessageBox.Show("Invalid Category name, must be between at least 3 characters long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        cat.CatergoryId = Convert.ToInt32(textBoxCategoryId.Text.Trim());
                        cat.CatergoryName = textBoxCategoryName.Text.Trim();
                        dBEntities.Catergories.Add(cat);
                        dBEntities.SaveChanges();
                        MessageBox.Show("Category saved!", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("A category with the same ID already exists.", "ISBN input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    dBEntities.Catergories.Remove(cat);
                }
                catch
                {

                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                int catId = Convert.ToInt32(textBoxCategoryId.Text.Trim());
                var searchCatId = dBEntities.Catergories.Single(n => n.CatergoryId == catId);
                if (searchCatId != null)
                {
                    DialogResult confirm;
                    confirm = MessageBox.Show("Are you sure you wish to delete this Category?", "Deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (confirm == DialogResult.Yes)
                    {
                        dBEntities.Catergories.Remove(searchCatId);
                        dBEntities.SaveChanges();
                        MessageBox.Show("Category deleted.", "Deletion successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Category doesn't exist", "Book not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int catId = Convert.ToInt32(textBoxCategoryId.Text.Trim());
                var searchCatId = dBEntities.Catergories.SingleOrDefault(n => n.CatergoryId == catId);
                if (textBoxCategoryId.TextLength > 3 || textBoxCategoryId.TextLength == 0)
                {
                    MessageBox.Show("Invalid Category Id, must be between 1-3 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBoxCategoryName.TextLength < 3)
                {
                    MessageBox.Show("Invalid Category name, must be between at least 3 characters long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    if (searchCatId != null)
                    {
                        searchCatId.CatergoryId = Convert.ToInt32(textBoxCategoryId.Text.Trim());
                        searchCatId.CatergoryName = textBoxCategoryName.Text.Trim();
                        dBEntities.SaveChanges();
                        MessageBox.Show("Category Updated!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Category doesn't exist", "Category not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            listViewCat.Items.Clear();
            if (comboBoxSearchCat.SelectedIndex == 0)
            {
                try
                {
                    int catId = Convert.ToInt32(textBoxSearchCat.Text.Trim());
                    var searchCatId = dBEntities.Catergories.Where(n => n.CatergoryId == catId).ToList<Catergory>();
                    if (searchCatId.Count != 0)
                    {
                        foreach (Catergory aCat in searchCatId)
                        {
                            ListViewItem item = new ListViewItem(aCat.CatergoryId.ToString());
                            item.SubItems.Add(aCat.CatergoryName);
                            listViewCat.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Category Not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBoxSearchCat.SelectedIndex == 1)
            {
                try
                {
                    string catName = textBoxSearchCat.Text.Trim();
                    var searchCatName = dBEntities.Catergories.Where(n => n.CatergoryName == catName).ToList<Catergory>();
                    if (searchCatName.Count != 0)
                    {
                        foreach (Catergory aCat in searchCatName)
                        {
                            ListViewItem item = new ListViewItem(aCat.CatergoryId.ToString());
                            item.SubItems.Add(aCat.CatergoryName);
                            listViewCat.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Category Not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            listViewPub.Items.Clear();
            var listpub = (from pub in dBEntities.Publishers
                           select pub).ToList<Publisher>();
            if (listpub.Count != 0)
            {
                foreach (Publisher aPub in listpub)
                {
                    ListViewItem item = new ListViewItem(aPub.PublisherId.ToString());
                    item.SubItems.Add(aPub.PublisherName);
                    item.SubItems.Add(aPub.WebAddress);
                    listViewPub.Items.Add(item);

                }
            }
            else
            {
                MessageBox.Show("Publisher Database is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            listViewPub.Items.Clear();
            if (comboBoxPubSearch.SelectedIndex == 0)
            {
                try
                {
                    int pubId = Convert.ToInt32(textBoxSearchPub.Text.Trim());
                    var searchpubId = dBEntities.Publishers.Where(n => n.PublisherId == pubId).ToList<Publisher>();
                    if (searchpubId.Count != 0)
                    {
                        foreach (Publisher aPub in searchpubId)
                        {
                            ListViewItem item = new ListViewItem(aPub.PublisherId.ToString());
                            item.SubItems.Add(aPub.PublisherName);
                            item.SubItems.Add(aPub.WebAddress);
                            listViewPub.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Publisher Not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBoxPubSearch.SelectedIndex == 1)
            {
                try
                {
                    string pubName = textBoxSearchPub.Text.Trim();
                    var searchpubName = dBEntities.Publishers.Where(n => n.PublisherName == pubName).ToList<Publisher>();
                    if (searchpubName.Count != 0)
                    {
                        foreach (Publisher aPub in searchpubName)
                        {
                            ListViewItem item = new ListViewItem(aPub.PublisherId.ToString());
                            item.SubItems.Add(aPub.PublisherName);
                            item.SubItems.Add(aPub.WebAddress);
                            listViewPub.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Publisher Not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSavePub_Click(object sender, EventArgs e)
        {
            Publisher pub = new Publisher();
            try
            {
                int pubId = Convert.ToInt32(textBoxPublisherId.Text.Trim());
                var searchpubId = dBEntities.Publishers.Where(n => n.PublisherId == pubId).ToList<Publisher>();
                if (searchpubId.Count == 0)
                {
                    if ((!(Validator.IsvalidNum(textBoxPublisherId.Text, 4))))
                    {
                        MessageBox.Show("Invalid publisher id, must be 4 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxPublisherName.TextLength <= 3)
                    {
                        MessageBox.Show("Invalid publisher name, must be at least 4 characters long.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if ((!(Validator.IsvalidWebSite(textBoxWebAddress.Text))))
                    {
                        MessageBox.Show("Invalid Website, please do not include www.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        pub.PublisherId = Convert.ToInt32(textBoxPublisherId.Text.Trim());
                        pub.PublisherName = textBoxPublisherName.Text.Trim();
                        pub.WebAddress = textBoxWebAddress.Text.Trim();
                        dBEntities.Publishers.Add(pub);
                        dBEntities.SaveChanges();
                        MessageBox.Show("Publisher saved!", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("A publisher with the same ID already exists.", "ISBN input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    dBEntities.Publishers.Remove(pub);
                }
                catch
                {

                }
            }
        }

        private void buttonDeletePub_Click(object sender, EventArgs e)
        {
            try
            {
                int pubId = Convert.ToInt32(textBoxPublisherId.Text.Trim());
                var searchpubId = dBEntities.Publishers.Single(n => n.PublisherId == pubId);
                if (searchpubId != null)
                {
                    DialogResult confirm;
                    confirm = MessageBox.Show("Are you sure you wish to delete this Publisher?", "Deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (confirm == DialogResult.Yes)
                    {
                        dBEntities.Publishers.Remove(searchpubId);
                        dBEntities.SaveChanges();
                        MessageBox.Show("Publisher deleted.", "Deletion successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Publisher doesn't exist", "Book not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdatePub_Click(object sender, EventArgs e)
        {
            try
            {
                int pubId = Convert.ToInt32(textBoxPublisherId.Text.Trim());
                var searchpubId = dBEntities.Publishers.SingleOrDefault(n => n.PublisherId == pubId);
                if (searchpubId != null)
                {
                    if ((!(Validator.IsvalidNum(pubId.ToString(), 4))))
                    {
                        MessageBox.Show("Invalid publisher id, must be 4 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxPublisherName.TextLength <= 3)
                    {
                        MessageBox.Show("Invalid publisher name, must be at least 4 characters long.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if ((!(Validator.IsvalidWebSite(textBoxWebAddress.Text))))
                    {
                        MessageBox.Show("Invalid Website, please do not include www.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        searchpubId.PublisherId = Convert.ToInt32(textBoxPublisherId.Text.Trim());
                        searchpubId.PublisherName = textBoxPublisherName.Text.Trim();
                        searchpubId.WebAddress = textBoxWebAddress.Text.Trim();
                        dBEntities.SaveChanges();
                        MessageBox.Show("Publisher Updated!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Publisher doesn't exist", "Category not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label68_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            listViewAuthorb.Items.Clear();
            var listAuth = (from auth in dBEntities.AuthorBooks
                            select auth).ToList<AuthorBook>();
            if (listAuth.Count != 0)
            {
                foreach (AuthorBook aAuth in listAuth)
                {
                    ListViewItem item = new ListViewItem(aAuth.AuthorId.ToString());
                    item.SubItems.Add(aAuth.ISBN.ToString());
                    item.SubItems.Add(aAuth.YearPublished.ToString());
                    item.SubItems.Add(aAuth.Edition);
                    listViewAuthorb.Items.Add(item);

                }
            }
            else
            {
                MessageBox.Show("Author Books Database is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                int authId = Convert.ToInt32(textBoxAuthorId.Text.Trim());
                long isbn = Convert.ToInt64(textBoxISBNAbook.Text.Trim());
                var searchAuthBook = dBEntities.AuthorBooks.SingleOrDefault(n => n.AuthorId == authId && n.ISBN == isbn);
                if (searchAuthBook != null)
                {
                    if ((!(Validator.IsvalidNum(authId.ToString(), 4))))
                    {
                        MessageBox.Show("Invalid author id, must be 4 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidNum(isbn.ToString(), 13)))
                    {
                        MessageBox.Show("Invalid ISBN, must be 13 digits.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidNum(textBoxYear.Text, 4)))
                    {
                        MessageBox.Show("Invalid year, must be 4 digits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxEdition.TextLength <= 3)
                    {
                        MessageBox.Show("Invalid edition, must be greater than 4 characters or digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        searchAuthBook.AuthorId = Convert.ToInt32(textBoxAuthorId.Text.Trim());
                        searchAuthBook.ISBN = Convert.ToInt64(textBoxISBNAbook.Text.Trim());
                        searchAuthBook.YearPublished = Convert.ToInt32(textBoxYear.Text.Trim());
                        searchAuthBook.Edition = textBoxEdition.Text.Trim();
                        dBEntities.SaveChanges();
                        MessageBox.Show("Author Book Updated!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Author book doesn't exist", "Category not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            AuthorBook authBook = new AuthorBook();
            try
            {
                int authId = Convert.ToInt32(textBoxAuthorId.Text.Trim());
                long isbn = Convert.ToInt64(textBoxISBNAbook.Text.Trim());
                var searchAuthBook = dBEntities.AuthorBooks.Where(n => n.AuthorId == authId && n.ISBN == isbn).ToList<AuthorBook>();
                if (searchAuthBook.Count == 0)
                {
                    if ((!(Validator.IsvalidNum(authId.ToString(), 4))))
                    {
                        MessageBox.Show("Invalid author id, must be 4 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidNum(isbn.ToString(), 13)))
                    {
                        MessageBox.Show("Invalid ISBN, must be 13 digits.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidNum(textBoxYear.Text, 4)))
                    {
                        MessageBox.Show("Invalid year, must be 4 digits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxEdition.TextLength <= 3)
                    {
                        MessageBox.Show("Invalid edition, must be greater than 4 characters or digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        authBook.AuthorId = Convert.ToInt32(textBoxAuthorId.Text.Trim());
                        authBook.ISBN = Convert.ToInt64(textBoxISBNAbook.Text.Trim());
                        authBook.YearPublished = Convert.ToInt32(textBoxYear.Text.Trim());
                        authBook.Edition = textBoxEdition.Text.Trim();
                        dBEntities.AuthorBooks.Add(authBook);
                        dBEntities.SaveChanges();
                        MessageBox.Show("Author Book saved!", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("A author book with the same ID  and ISBN already exists.", "ISBN input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    dBEntities.AuthorBooks.Remove(authBook);
                }
                catch
                {

                }
            }
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            try
            {
                int authId = Convert.ToInt32(textBoxAuthorId.Text.Trim());
                long isbn = Convert.ToInt64(textBoxISBNAbook.Text.Trim());
                var searchAuthBook = dBEntities.AuthorBooks.Single(n => n.AuthorId == authId && n.ISBN == isbn);
                if (searchAuthBook != null)
                {
                    DialogResult confirm;
                    confirm = MessageBox.Show("Are you sure you wish to delete this Author book?", "Deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (confirm == DialogResult.Yes)
                    {
                        dBEntities.AuthorBooks.Remove(searchAuthBook);
                        dBEntities.SaveChanges();
                        MessageBox.Show("Author book deleted.", "Deletion successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Author book doesn't exist", "Book not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSearchAuthb_Click(object sender, EventArgs e)
        {
            listViewAuthorb.Items.Clear();
            if (comboBoxSearchAuthb.SelectedIndex == 0)
            {
                try
                {
                    int authId = Convert.ToInt32(textBoxSearchAuthb.Text.Trim());
                    var searchAuthBook = dBEntities.AuthorBooks.Where(n => n.AuthorId == authId).ToList<AuthorBook>();
                    if (searchAuthBook.Count != 0)
                    {
                        foreach (AuthorBook aAuth in searchAuthBook)
                        {
                            ListViewItem item = new ListViewItem(aAuth.AuthorId.ToString());
                            item.SubItems.Add(aAuth.ISBN.ToString());
                            item.SubItems.Add(aAuth.YearPublished.ToString());
                            item.SubItems.Add(aAuth.Edition);
                            listViewAuthorb.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Author book not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBoxSearchAuthb.SelectedIndex == 1)
            {
                try
                {
                    long isbn = Convert.ToInt64(textBoxSearchAuthb.Text.Trim());
                    var searchAuthBook = dBEntities.AuthorBooks.Where(n => n.ISBN == isbn).ToList<AuthorBook>();
                    if (searchAuthBook.Count != 0)
                    {
                        foreach (AuthorBook aAuth in searchAuthBook)
                        {
                            ListViewItem item = new ListViewItem(aAuth.AuthorId.ToString());
                            item.SubItems.Add(aAuth.ISBN.ToString());
                            item.SubItems.Add(aAuth.YearPublished.ToString());
                            item.SubItems.Add(aAuth.Edition);
                            listViewAuthorb.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Author book not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonListAuth_Click(object sender, EventArgs e)
        {
            listViewAuthor.Items.Clear();
            var listAuth = (from auth in dBEntities.Authors
                            select auth).ToList<Author>();
            if (listAuth.Count != 0)
            {
                foreach (Author aAuth in listAuth)
                {
                    ListViewItem item = new ListViewItem(aAuth.AuthorId.ToString());
                    item.SubItems.Add(aAuth.FirstName);
                    item.SubItems.Add(aAuth.LastName);
                    item.SubItems.Add(aAuth.Email);
                    listViewAuthor.Items.Add(item);

                }
            }
            else
            {
                MessageBox.Show("Author Database is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveAuth_Click(object sender, EventArgs e)
        {
            Author auth = new Author();
            try
            {
                int authId = Convert.ToInt32(textBoxAuthIdAuth.Text.Trim());
                var searchAuth = dBEntities.Authors.Where(n => n.AuthorId == authId).ToList<Author>();
                if (searchAuth.Count == 0)
                {
                    if ((!(Validator.IsvalidNum(authId.ToString(), 4))))
                    {
                        MessageBox.Show("Invalid author id, must be 4 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidString(textBoxAuthFName.Text, 3, 50)))
                    {
                        MessageBox.Show("Invalid author first name, must be between 3-50 characters", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidString(textBoxAuthLName.Text, 3, 50)))
                    {
                        MessageBox.Show("Invalid author last name, must be between 3-50 characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidEmail(textBoxAuthEmail.Text)))
                    {
                        MessageBox.Show("Invalid email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        auth.AuthorId = Convert.ToInt32(textBoxAuthIdAuth.Text.Trim());
                        auth.FirstName = textBoxAuthFName.Text.Trim();
                        auth.LastName = textBoxAuthLName.Text.Trim();
                        auth.Email = textBoxAuthEmail.Text.Trim();
                        dBEntities.Authors.Add(auth);
                        dBEntities.SaveChanges();
                        MessageBox.Show("Author saved!", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("A author with the same ID already exists.", "Author id input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    dBEntities.Authors.Remove(auth);
                }
                catch
                {

                }
            }
        }

        private void buttonUpdateAuth_Click(object sender, EventArgs e)
        {
            try
            {
                int authId = Convert.ToInt32(textBoxAuthIdAuth.Text.Trim());
                var searchAuth = dBEntities.Authors.SingleOrDefault(n => n.AuthorId == authId);
                if (searchAuth != null)
                {
                    if ((!(Validator.IsvalidNum(authId.ToString(), 4))))
                    {
                        MessageBox.Show("Invalid author id, must be 4 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidString(textBoxAuthFName.Text, 3, 50)))
                    {
                        MessageBox.Show("Invalid author first name, must be between 3-50 characters", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidString(textBoxAuthLName.Text, 3, 50)))
                    {
                        MessageBox.Show("Invalid author last name, must be between 3-50 characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidEmail(textBoxAuthEmail.Text)))
                    {
                        MessageBox.Show("Invalid email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        searchAuth.AuthorId = Convert.ToInt32(textBoxAuthIdAuth.Text.Trim());
                        searchAuth.FirstName = textBoxAuthFName.Text.Trim();
                        searchAuth.LastName = textBoxAuthLName.Text.Trim();
                        searchAuth.Email = textBoxAuthEmail.Text.Trim();
                        dBEntities.SaveChanges();
                        MessageBox.Show("Author Updated!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Author doesn't exist", "Category not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleteAuth_Click(object sender, EventArgs e)
        {
            try
            {
                int authId = Convert.ToInt32(textBoxAuthIdAuth.Text.Trim());
                var searchAuth = dBEntities.Authors.Single(n => n.AuthorId == authId);
                if (searchAuth != null)
                {
                    DialogResult confirm;
                    confirm = MessageBox.Show("Are you sure you wish to delete this Author?", "Deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (confirm == DialogResult.Yes)
                    {
                        dBEntities.Authors.Remove(searchAuth);
                        dBEntities.SaveChanges();
                        MessageBox.Show("Author deleted.", "Deletion successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Author doesn't exist", "Author not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSearchAuth_Click(object sender, EventArgs e)
        {
            listViewAuthor.Items.Clear();
            if (comboBoxSearchAuth.SelectedIndex == 0)
            {
                try
                {
                    int authId = Convert.ToInt32(textBoxSearchAuth.Text.Trim());
                    var searchAuth = dBEntities.Authors.Where(n => n.AuthorId == authId).ToList<Author>();
                    if (searchAuth.Count != 0)
                    {
                        foreach (Author aAuth in searchAuth)
                        {
                            ListViewItem item = new ListViewItem(aAuth.AuthorId.ToString());
                            item.SubItems.Add(aAuth.FirstName);
                            item.SubItems.Add(aAuth.LastName);
                            item.SubItems.Add(aAuth.Email);
                            listViewAuthor.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Author not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (comboBoxSearchAuth.SelectedIndex == 1)
            {
                try
                {
                    string authLName = textBoxSearchAuth.Text.Trim();
                    var searchAuth = dBEntities.Authors.Where(n => n.LastName == authLName).ToList<Author>();
                    if (searchAuth.Count != 0)
                    {
                        foreach (Author aAuth in searchAuth)
                        {
                            ListViewItem item = new ListViewItem(aAuth.AuthorId.ToString());
                            item.SubItems.Add(aAuth.FirstName);
                            item.SubItems.Add(aAuth.LastName);
                            item.SubItems.Add(aAuth.Email);
                            listViewAuthor.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Author not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string userName = labelUserName.Text;
                var searchUName = dBEntities.Users.Single(n => n.UserName == userName);
                if (searchUName != null)
                {
                    textBoxEmpIdOrder.Text = searchUName.EmployeeId.ToString();

                }
                else
                {
                    MessageBox.Show("User not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DateTime today = DateTime.Today;
            maskedTextBoxOrderDate.Text = today.ToString("d");

        }

        private void buttonListOrder_Click(object sender, EventArgs e)
        {
            listViewOrders.Items.Clear();
            var listOrders = (from orders in dBEntities.Orders
                              select orders).ToList<Order>();
            if (listOrders.Count != 0)
            {
                foreach (Order aOrder in listOrders)
                {
                    ListViewItem item = new ListViewItem(aOrder.OrderId.ToString());
                    item.SubItems.Add(aOrder.OrderDate.ToString());
                    item.SubItems.Add(aOrder.OrderType);
                    item.SubItems.Add(aOrder.RequiredDate.ToString());
                    item.SubItems.Add(aOrder.ShippingDate.ToString());
                    item.SubItems.Add(aOrder.OrderStatus);
                    item.SubItems.Add(aOrder.CustomerId.ToString());
                    item.SubItems.Add(aOrder.EmployeeId.ToString());
                    listViewOrders.Items.Add(item);

                }
            }
            else
            {
                MessageBox.Show("Orders Database is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleteOrder_Click(object sender, EventArgs e)
        {
            try
            {
                int orderId = Convert.ToInt32(textBoxOrderId.Text.Trim());
                var searchOrder = dBEntities.Orders.Single(n => n.OrderId == orderId);
                if (searchOrder != null)
                {
                    DialogResult confirm;
                    confirm = MessageBox.Show("Are you sure you wish to delete this order?", "Deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (confirm == DialogResult.Yes)
                    {
                        dBEntities.Orders.Remove(searchOrder);
                        dBEntities.SaveChanges();
                        MessageBox.Show("Order deleted.", "Deletion successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Order doesn't exist", "Order not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveOrder_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            try
            {
                if (textBoxOrderType.TextLength <= 3)
                {
                    MessageBox.Show("Invalid order type, must be greater than 3 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBoxOrderStatus.TextLength <= 3)
                {
                    MessageBox.Show("Invalid order status, must be greater than 3 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBoxOderCustId.TextLength > 3 || textBoxOderCustId.TextLength == 0)
                {
                    MessageBox.Show("Invalid Customer ID. Customer ID must be between 1-3 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(Validator.IsvalidNum(textBoxEmpIdOrder.Text, 3)))
                {
                    MessageBox.Show("Invalid Employee ID. Employee ID must be 3 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    order.OrderDate = DateTime.Parse(maskedTextBoxOrderDate.Text.Trim());
                    order.OrderType = textBoxOrderType.Text.Trim();
                    order.RequiredDate = DateTime.Parse(maskedTextBoxRequiredDate.Text.Trim());
                    order.ShippingDate = DateTime.Parse(maskedTextBoxShippingDate.Text.Trim());
                    order.OrderStatus = textBoxOrderStatus.Text.Trim();
                    order.CustomerId = Convert.ToInt32(textBoxOderCustId.Text.Trim());
                    order.EmployeeId = Convert.ToInt32(textBoxEmpIdOrder.Text.Trim());
                    dBEntities.Orders.Add(order);
                    dBEntities.SaveChanges();
                    MessageBox.Show("Order saved!", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.InnerException);
                try
                {
                    dBEntities.Orders.Remove(order);
                }
                catch
                {

                }
            }
        }

        private void buttonUpdateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                int orderId = Convert.ToInt32(textBoxOrderId.Text.Trim());
                var searchOrder = dBEntities.Orders.SingleOrDefault(n => n.OrderId == orderId);
                if (searchOrder != null)
                {
                    if (textBoxOrderId.TextLength == 0)
                    {
                        MessageBox.Show("Invalid order id, can't be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxOrderType.TextLength <= 3)
                    {
                        MessageBox.Show("Invalid order type, must be greater than 3 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxOrderStatus.TextLength <= 3)
                    {
                        MessageBox.Show("Invalid order status, must be greater than 3 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (textBoxOderCustId.TextLength > 3 || textBoxOderCustId.TextLength == 0)
                    {
                        MessageBox.Show("Invalid Customer ID. Customer ID must be between 1-3 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!(Validator.IsvalidNum(textBoxEmpIdOrder.Text, 3)))
                    {
                        MessageBox.Show("Invalid Employee ID. Employee ID must be 3 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        searchOrder.OrderId = Convert.ToInt32(textBoxOrderId.Text.Trim());
                        searchOrder.OrderDate = DateTime.Parse(maskedTextBoxOrderDate.Text.Trim());
                        searchOrder.OrderType = textBoxOrderType.Text.Trim();
                        searchOrder.RequiredDate = DateTime.Parse(maskedTextBoxRequiredDate.Text.Trim());
                        searchOrder.ShippingDate = DateTime.Parse(maskedTextBoxShippingDate.Text.Trim());
                        searchOrder.OrderStatus = textBoxOrderStatus.Text.Trim();
                        searchOrder.CustomerId = Convert.ToInt32(textBoxOderCustId.Text.Trim());
                        searchOrder.EmployeeId = Convert.ToInt32(textBoxEmpIdOrder.Text.Trim());
                        dBEntities.SaveChanges();
                        MessageBox.Show("Order Updated!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Order doesn't exist", "Order not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_4(object sender, EventArgs e)
        {
            listViewOrders.Items.Clear();
            if (comboBoxOrders.SelectedIndex == 0)
            {
                try
                {
                    int orderId = Convert.ToInt32(textBoxSearchOrder.Text.Trim());
                    var searchOrder = dBEntities.Orders.Where(n => n.OrderId == orderId).ToList<Order>();
                    if (searchOrder.Count != 0)
                    {
                        foreach (Order aOrder in searchOrder)
                        {
                            ListViewItem item = new ListViewItem(aOrder.OrderId.ToString());
                            item.SubItems.Add(aOrder.OrderDate.ToString());
                            item.SubItems.Add(aOrder.OrderType);
                            item.SubItems.Add(aOrder.RequiredDate.ToString());
                            item.SubItems.Add(aOrder.ShippingDate.ToString());
                            item.SubItems.Add(aOrder.OrderStatus);
                            item.SubItems.Add(aOrder.CustomerId.ToString());
                            item.SubItems.Add(aOrder.EmployeeId.ToString());
                            listViewOrders.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Order not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBoxOrders.SelectedIndex == 1)
            {
                try
                {
                    int custId = Convert.ToInt32(textBoxSearchOrder.Text.Trim());
                    var searchOrder = dBEntities.Orders.Where(n => n.CustomerId == custId).ToList<Order>();
                    if (searchOrder.Count != 0)
                    {
                        foreach (Order aOrder in searchOrder)
                        {
                            ListViewItem item = new ListViewItem(aOrder.OrderId.ToString());
                            item.SubItems.Add(aOrder.OrderDate.ToString());
                            item.SubItems.Add(aOrder.OrderType);
                            item.SubItems.Add(aOrder.RequiredDate.ToString());
                            item.SubItems.Add(aOrder.ShippingDate.ToString());
                            item.SubItems.Add(aOrder.OrderStatus);
                            item.SubItems.Add(aOrder.CustomerId.ToString());
                            item.SubItems.Add(aOrder.EmployeeId.ToString());
                            listViewOrders.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Order not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBoxOrders.SelectedIndex == 2)
            {
                try
                {
                    int empId = Convert.ToInt32(textBoxSearchOrder.Text.Trim());
                    var searchOrder = dBEntities.Orders.Where(n => n.EmployeeId == empId).ToList<Order>();
                    if (searchOrder.Count != 0)
                    {
                        foreach (Order aOrder in searchOrder)
                        {
                            ListViewItem item = new ListViewItem(aOrder.OrderId.ToString());
                            item.SubItems.Add(aOrder.OrderDate.ToString());
                            item.SubItems.Add(aOrder.OrderType);
                            item.SubItems.Add(aOrder.RequiredDate.ToString());
                            item.SubItems.Add(aOrder.ShippingDate.ToString());
                            item.SubItems.Add(aOrder.OrderStatus);
                            item.SubItems.Add(aOrder.CustomerId.ToString());
                            item.SubItems.Add(aOrder.EmployeeId.ToString());
                            listViewOrders.Items.Add(item);

                        }

                    }
                    else
                    {
                        MessageBox.Show("Order not found", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click_5(object sender, EventArgs e)
        {


        }

        private void textBoxISBNAbook_TextChanged(object sender, EventArgs e)
        {
            if (textBoxISBNAbook.TextLength == 13)
            {
                labelISBNAuth.Visible = true;
            }
            else
            {
                labelISBNAuth.Visible = false;
            }
        }

        private void listViewAuthorb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button2_Click_7(object sender, EventArgs e)
        {
        }

    }
}




