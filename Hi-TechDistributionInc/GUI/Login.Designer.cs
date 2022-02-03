namespace Hi_TechDistributionInc.GUI
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.checkBoxDeveloper = new System.Windows.Forms.CheckBox();
            this.buttonManager = new System.Windows.Forms.Button();
            this.buttonSalesManager = new System.Windows.Forms.Button();
            this.checkBoxPassword = new System.Windows.Forms.CheckBox();
            this.checkBoxRememberMe = new System.Windows.Forms.CheckBox();
            this.buttonController = new System.Windows.Forms.Button();
            this.buttonOrderMary = new System.Windows.Forms.Button();
            this.buttonOrderJenifer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(193, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(196, 63);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(224, 23);
            this.textBoxUserName.TabIndex = 1;
            this.textBoxUserName.TextChanged += new System.EventHandler(this.textBoxUserName_TextChanged);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(196, 113);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(224, 23);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.UseSystemPasswordChar = true;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(193, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(329, 167);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(91, 27);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "&Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(191, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hi-TechDistribution Inc.";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(194, 164);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(99, 15);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Forgot Password";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // checkBoxDeveloper
            // 
            this.checkBoxDeveloper.AutoSize = true;
            this.checkBoxDeveloper.Location = new System.Drawing.Point(13, 167);
            this.checkBoxDeveloper.Name = "checkBoxDeveloper";
            this.checkBoxDeveloper.Size = new System.Drawing.Size(127, 19);
            this.checkBoxDeveloper.TabIndex = 7;
            this.checkBoxDeveloper.Text = "Developer Options";
            this.checkBoxDeveloper.UseVisualStyleBackColor = true;
            this.checkBoxDeveloper.CheckedChanged += new System.EventHandler(this.checkBoxDeveloper_CheckedChanged);
            // 
            // buttonManager
            // 
            this.buttonManager.Location = new System.Drawing.Point(13, 11);
            this.buttonManager.Name = "buttonManager";
            this.buttonManager.Size = new System.Drawing.Size(127, 23);
            this.buttonManager.TabIndex = 8;
            this.buttonManager.Text = "Mis Manager";
            this.buttonManager.UseVisualStyleBackColor = true;
            this.buttonManager.Visible = false;
            this.buttonManager.Click += new System.EventHandler(this.buttonManager_Click);
            // 
            // buttonSalesManager
            // 
            this.buttonSalesManager.Location = new System.Drawing.Point(13, 40);
            this.buttonSalesManager.Name = "buttonSalesManager";
            this.buttonSalesManager.Size = new System.Drawing.Size(127, 23);
            this.buttonSalesManager.TabIndex = 9;
            this.buttonSalesManager.Text = "Sales Manager";
            this.buttonSalesManager.UseVisualStyleBackColor = true;
            this.buttonSalesManager.Visible = false;
            this.buttonSalesManager.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBoxPassword
            // 
            this.checkBoxPassword.AutoSize = true;
            this.checkBoxPassword.Checked = true;
            this.checkBoxPassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPassword.Location = new System.Drawing.Point(312, 142);
            this.checkBoxPassword.Name = "checkBoxPassword";
            this.checkBoxPassword.Size = new System.Drawing.Size(108, 19);
            this.checkBoxPassword.TabIndex = 10;
            this.checkBoxPassword.Text = "Hide Password";
            this.checkBoxPassword.UseVisualStyleBackColor = true;
            this.checkBoxPassword.CheckedChanged += new System.EventHandler(this.checkBoxPassword_CheckedChanged);
            // 
            // checkBoxRememberMe
            // 
            this.checkBoxRememberMe.AutoSize = true;
            this.checkBoxRememberMe.Location = new System.Drawing.Point(196, 142);
            this.checkBoxRememberMe.Name = "checkBoxRememberMe";
            this.checkBoxRememberMe.Size = new System.Drawing.Size(102, 19);
            this.checkBoxRememberMe.TabIndex = 11;
            this.checkBoxRememberMe.Text = "Remember me";
            this.checkBoxRememberMe.UseVisualStyleBackColor = true;
            this.checkBoxRememberMe.CheckedChanged += new System.EventHandler(this.checkBoxRememberMe_CheckedChanged);
            // 
            // buttonController
            // 
            this.buttonController.Location = new System.Drawing.Point(13, 69);
            this.buttonController.Name = "buttonController";
            this.buttonController.Size = new System.Drawing.Size(127, 23);
            this.buttonController.TabIndex = 12;
            this.buttonController.Text = "Inventory Controller";
            this.buttonController.UseVisualStyleBackColor = true;
            this.buttonController.Visible = false;
            this.buttonController.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // buttonOrderMary
            // 
            this.buttonOrderMary.Location = new System.Drawing.Point(13, 98);
            this.buttonOrderMary.Name = "buttonOrderMary";
            this.buttonOrderMary.Size = new System.Drawing.Size(127, 23);
            this.buttonOrderMary.TabIndex = 13;
            this.buttonOrderMary.Text = "Order Clerk Mary";
            this.buttonOrderMary.UseVisualStyleBackColor = true;
            this.buttonOrderMary.Visible = false;
            this.buttonOrderMary.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // buttonOrderJenifer
            // 
            this.buttonOrderJenifer.Location = new System.Drawing.Point(12, 127);
            this.buttonOrderJenifer.Name = "buttonOrderJenifer";
            this.buttonOrderJenifer.Size = new System.Drawing.Size(127, 23);
            this.buttonOrderJenifer.TabIndex = 14;
            this.buttonOrderJenifer.Text = "Order Clerk Jenifer";
            this.buttonOrderJenifer.UseVisualStyleBackColor = true;
            this.buttonOrderJenifer.Visible = false;
            this.buttonOrderJenifer.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Login
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 200);
            this.Controls.Add(this.buttonOrderJenifer);
            this.Controls.Add(this.buttonOrderMary);
            this.Controls.Add(this.buttonController);
            this.Controls.Add(this.checkBoxRememberMe);
            this.Controls.Add(this.checkBoxPassword);
            this.Controls.Add(this.buttonSalesManager);
            this.Controls.Add(this.buttonManager);
            this.Controls.Add(this.checkBoxDeveloper);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Login";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox checkBoxDeveloper;
        private System.Windows.Forms.Button buttonManager;
        private System.Windows.Forms.Button buttonSalesManager;
        private System.Windows.Forms.CheckBox checkBoxPassword;
        private System.Windows.Forms.CheckBox checkBoxRememberMe;
        private System.Windows.Forms.Button buttonController;
        private System.Windows.Forms.Button buttonOrderMary;
        private System.Windows.Forms.Button buttonOrderJenifer;
    }
}