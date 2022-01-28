namespace client
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Username = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_Sweet = new System.Windows.Forms.Button();
            this.textBox_Sweet = new System.Windows.Forms.TextBox();
            this.button_request = new System.Windows.Forms.Button();
            this.button_request_followed = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_follow_username = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_follow = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_block = new System.Windows.Forms.Button();
            this.textBox_block_username = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_followedUsers = new System.Windows.Forms.Button();
            this.button_followingUsers = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_SweetID = new System.Windows.Forms.TextBox();
            this.button_mySweets = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(75, 19);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(88, 20);
            this.textBox_ip.TabIndex = 2;
            this.textBox_ip.Text = "192.168.195.214";
            this.textBox_ip.TextChanged += new System.EventHandler(this.textBox_ip_TextChanged);
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(75, 53);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(88, 20);
            this.textBox_port.TabIndex = 3;
            this.textBox_port.Text = "1111";
            this.textBox_port.TextChanged += new System.EventHandler(this.textBox_port_TextChanged);
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(27, 122);
            this.button_connect.Margin = new System.Windows.Forms.Padding(2);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(70, 23);
            this.button_connect.TabIndex = 4;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(220, 36);
            this.logs.Margin = new System.Windows.Forms.Padding(2);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(491, 382);
            this.logs.TabIndex = 5;
            this.logs.Text = "";
            this.logs.TextChanged += new System.EventHandler(this.logs_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 94);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Username:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox_Username
            // 
            this.textBox_Username.Location = new System.Drawing.Point(75, 91);
            this.textBox_Username.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Username.Name = "textBox_Username";
            this.textBox_Username.Size = new System.Drawing.Size(88, 20);
            this.textBox_Username.TabIndex = 12;
            this.textBox_Username.TextChanged += new System.EventHandler(this.textBox_Username_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 438);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Sweet:";
            // 
            // button_Sweet
            // 
            this.button_Sweet.Enabled = false;
            this.button_Sweet.Location = new System.Drawing.Point(630, 468);
            this.button_Sweet.Margin = new System.Windows.Forms.Padding(2);
            this.button_Sweet.Name = "button_Sweet";
            this.button_Sweet.Size = new System.Drawing.Size(80, 23);
            this.button_Sweet.TabIndex = 15;
            this.button_Sweet.Text = "Sweet";
            this.button_Sweet.UseVisualStyleBackColor = true;
            this.button_Sweet.Click += new System.EventHandler(this.button_Sweet_Click);
            // 
            // textBox_Sweet
            // 
            this.textBox_Sweet.Location = new System.Drawing.Point(81, 435);
            this.textBox_Sweet.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Sweet.Name = "textBox_Sweet";
            this.textBox_Sweet.Size = new System.Drawing.Size(629, 20);
            this.textBox_Sweet.TabIndex = 16;
            this.textBox_Sweet.TextChanged += new System.EventHandler(this.sweet_Button_TextChanged);
            // 
            // button_request
            // 
            this.button_request.Enabled = false;
            this.button_request.Location = new System.Drawing.Point(529, 468);
            this.button_request.Margin = new System.Windows.Forms.Padding(2);
            this.button_request.Name = "button_request";
            this.button_request.Size = new System.Drawing.Size(84, 50);
            this.button_request.TabIndex = 17;
            this.button_request.Text = "request all sweets";
            this.button_request.UseVisualStyleBackColor = true;
            this.button_request.Click += new System.EventHandler(this.button_request_Click);
            // 
            // button_request_followed
            // 
            this.button_request_followed.Enabled = false;
            this.button_request_followed.Location = new System.Drawing.Point(446, 468);
            this.button_request_followed.Margin = new System.Windows.Forms.Padding(2);
            this.button_request_followed.Name = "button_request_followed";
            this.button_request_followed.Size = new System.Drawing.Size(79, 50);
            this.button_request_followed.TabIndex = 18;
            this.button_request_followed.Text = "request sweets from followed users";
            this.button_request_followed.UseVisualStyleBackColor = true;
            this.button_request_followed.Click += new System.EventHandler(this.button_request_followed_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(373, 468);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 50);
            this.button1.TabIndex = 19;
            this.button1.Text = "show me the users";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Username:";
            // 
            // textBox_follow_username
            // 
            this.textBox_follow_username.Location = new System.Drawing.Point(74, 22);
            this.textBox_follow_username.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_follow_username.Name = "textBox_follow_username";
            this.textBox_follow_username.Size = new System.Drawing.Size(100, 20);
            this.textBox_follow_username.TabIndex = 12;
            this.textBox_follow_username.TextChanged += new System.EventHandler(this.textBox_Username_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_disconnect);
            this.groupBox1.Controls.Add(this.textBox_Username);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button_connect);
            this.groupBox1.Controls.Add(this.textBox_port);
            this.groupBox1.Controls.Add(this.textBox_ip);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 153);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sign In";
            // 
            // button_disconnect
            // 
            this.button_disconnect.Enabled = false;
            this.button_disconnect.Location = new System.Drawing.Point(115, 122);
            this.button_disconnect.Margin = new System.Windows.Forms.Padding(2);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(70, 23);
            this.button_disconnect.TabIndex = 13;
            this.button_disconnect.Text = "Disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBox_follow_username);
            this.groupBox2.Controls.Add(this.button_follow);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(7, 195);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 74);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Follow User";
            // 
            // button_follow
            // 
            this.button_follow.Enabled = false;
            this.button_follow.Location = new System.Drawing.Point(133, 46);
            this.button_follow.Margin = new System.Windows.Forms.Padding(2);
            this.button_follow.Name = "button_follow";
            this.button_follow.Size = new System.Drawing.Size(62, 23);
            this.button_follow.TabIndex = 4;
            this.button_follow.Text = "Follow";
            this.button_follow.UseVisualStyleBackColor = true;
            this.button_follow.Click += new System.EventHandler(this.button_follow_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_block);
            this.groupBox3.Controls.Add(this.textBox_block_username);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(7, 275);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(208, 83);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Block User";
            // 
            // button_block
            // 
            this.button_block.Enabled = false;
            this.button_block.Location = new System.Drawing.Point(133, 52);
            this.button_block.Name = "button_block";
            this.button_block.Size = new System.Drawing.Size(62, 23);
            this.button_block.TabIndex = 2;
            this.button_block.Text = "Block";
            this.button_block.UseVisualStyleBackColor = true;
            this.button_block.Click += new System.EventHandler(this.button_block_Click);
            // 
            // textBox_block_username
            // 
            this.textBox_block_username.Location = new System.Drawing.Point(74, 26);
            this.textBox_block_username.Name = "textBox_block_username";
            this.textBox_block_username.Size = new System.Drawing.Size(100, 20);
            this.textBox_block_username.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Username:";
            // 
            // button_delete
            // 
            this.button_delete.Enabled = false;
            this.button_delete.Location = new System.Drawing.Point(116, 16);
            this.button_delete.Margin = new System.Windows.Forms.Padding(2);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(80, 25);
            this.button_delete.TabIndex = 24;
            this.button_delete.Text = "Delete Sweet";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_followedUsers
            // 
            this.button_followedUsers.Enabled = false;
            this.button_followedUsers.Location = new System.Drawing.Point(81, 468);
            this.button_followedUsers.Margin = new System.Windows.Forms.Padding(2);
            this.button_followedUsers.Name = "button_followedUsers";
            this.button_followedUsers.Size = new System.Drawing.Size(91, 50);
            this.button_followedUsers.TabIndex = 25;
            this.button_followedUsers.Text = "Who am I stalking";
            this.button_followedUsers.UseVisualStyleBackColor = true;
            this.button_followedUsers.Click += new System.EventHandler(this.button_followedUsers_Click);
            // 
            // button_followingUsers
            // 
            this.button_followingUsers.Enabled = false;
            this.button_followingUsers.Location = new System.Drawing.Point(176, 468);
            this.button_followingUsers.Margin = new System.Windows.Forms.Padding(2);
            this.button_followingUsers.Name = "button_followingUsers";
            this.button_followingUsers.Size = new System.Drawing.Size(90, 50);
            this.button_followingUsers.TabIndex = 26;
            this.button_followingUsers.Text = "Who stalks me";
            this.button_followingUsers.UseVisualStyleBackColor = true;
            this.button_followingUsers.Click += new System.EventHandler(this.button_followingUsers_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox_SweetID);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.button_delete);
            this.groupBox4.Location = new System.Drawing.Point(6, 365);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(209, 53);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Delete Sweet";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Sweet ID:";
            // 
            // textBox_SweetID
            // 
            this.textBox_SweetID.Location = new System.Drawing.Point(75, 19);
            this.textBox_SweetID.Name = "textBox_SweetID";
            this.textBox_SweetID.Size = new System.Drawing.Size(35, 20);
            this.textBox_SweetID.TabIndex = 25;
            // 
            // button_mySweets
            // 
            this.button_mySweets.Enabled = false;
            this.button_mySweets.Location = new System.Drawing.Point(630, 495);
            this.button_mySweets.Name = "button_mySweets";
            this.button_mySweets.Size = new System.Drawing.Size(80, 23);
            this.button_mySweets.TabIndex = 28;
            this.button_mySweets.Text = "My Sweets";
            this.button_mySweets.UseVisualStyleBackColor = true;
            this.button_mySweets.Click += new System.EventHandler(this.button_mySweets_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 534);
            this.Controls.Add(this.button_mySweets);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button_followingUsers);
            this.Controls.Add(this.button_followedUsers);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_request_followed);
            this.Controls.Add(this.button_request);
            this.Controls.Add(this.textBox_Sweet);
            this.Controls.Add(this.button_Sweet);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logs);
            this.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Username;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Sweet;
        private System.Windows.Forms.TextBox textBox_Sweet;
        private System.Windows.Forms.Button button_request;
        private System.Windows.Forms.Button button_request_followed;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_follow_username;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_follow;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_block;
        private System.Windows.Forms.TextBox textBox_block_username;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_followedUsers;
        private System.Windows.Forms.Button button_followingUsers;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox_SweetID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_mySweets;
    }
}

