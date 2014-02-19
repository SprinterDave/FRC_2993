namespace SharpNetConsole
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelIPAddressValidInvalid = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.labelPortValidInvalid = new System.Windows.Forms.Label();
            this.bStartStop = new System.Windows.Forms.Button();
            this.bPauseResume = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbIPAddress
            // 
            this.tbIPAddress.Location = new System.Drawing.Point(76, 6);
            this.tbIPAddress.Name = "tbIPAddress";
            this.tbIPAddress.Size = new System.Drawing.Size(110, 20);
            this.tbIPAddress.TabIndex = 0;
            this.tbIPAddress.Text = "192.168.173.157";
            this.toolTip1.SetToolTip(this.tbIPAddress, "Enter the IP address, in dotted-decimal format, from which you\r\nwant to monitor U" +
        "DP broadcasts on the port specified below.\r\nUse 0.0.0.0 to monitor UDP broadcast" +
        "s from all IP addresses.");
            this.tbIPAddress.TextChanged += new System.EventHandler(this.tbIPAddress_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Address";
            // 
            // labelIPAddressValidInvalid
            // 
            this.labelIPAddressValidInvalid.AutoSize = true;
            this.labelIPAddressValidInvalid.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelIPAddressValidInvalid.Location = new System.Drawing.Point(192, 9);
            this.labelIPAddressValidInvalid.Name = "labelIPAddressValidInvalid";
            this.labelIPAddressValidInvalid.Size = new System.Drawing.Size(92, 13);
            this.labelIPAddressValidInvalid.TabIndex = 2;
            this.labelIPAddressValidInvalid.Text = "Invalid IP Address";
            this.labelIPAddressValidInvalid.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(76, 32);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(47, 20);
            this.tbPort.TabIndex = 4;
            this.tbPort.Text = "6666";
            this.toolTip1.SetToolTip(this.tbPort, "Enter the port number (0-65535) on which you want to\r\nmonitor UDP broadcasts from" +
        " the IP address specified above.");
            this.tbPort.TextChanged += new System.EventHandler(this.tbPort_TextChanged);
            // 
            // labelPortValidInvalid
            // 
            this.labelPortValidInvalid.AutoSize = true;
            this.labelPortValidInvalid.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelPortValidInvalid.Location = new System.Drawing.Point(129, 35);
            this.labelPortValidInvalid.Name = "labelPortValidInvalid";
            this.labelPortValidInvalid.Size = new System.Drawing.Size(60, 13);
            this.labelPortValidInvalid.TabIndex = 5;
            this.labelPortValidInvalid.Text = "Invalid Port";
            this.labelPortValidInvalid.Visible = false;
            // 
            // bStartStop
            // 
            this.bStartStop.Location = new System.Drawing.Point(12, 81);
            this.bStartStop.Name = "bStartStop";
            this.bStartStop.Size = new System.Drawing.Size(75, 23);
            this.bStartStop.TabIndex = 6;
            this.bStartStop.Text = "Start";
            this.bStartStop.UseVisualStyleBackColor = true;
            this.bStartStop.Click += new System.EventHandler(this.bStartStop_Click);
            // 
            // bPauseResume
            // 
            this.bPauseResume.Location = new System.Drawing.Point(93, 81);
            this.bPauseResume.Name = "bPauseResume";
            this.bPauseResume.Size = new System.Drawing.Size(75, 23);
            this.bPauseResume.TabIndex = 7;
            this.bPauseResume.Text = "Pause";
            this.bPauseResume.UseVisualStyleBackColor = true;
            this.bPauseResume.Visible = false;
            this.bPauseResume.Click += new System.EventHandler(this.bPauseResume_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(12, 58);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(79, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Enable Bell";
            this.toolTip1.SetToolTip(this.checkBox1, "Uncheck to silence with bell character (ASCII 7)");
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 113);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.bPauseResume);
            this.Controls.Add(this.bStartStop);
            this.Controls.Add(this.labelPortValidInvalid);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelIPAddressValidInvalid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbIPAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "#NetConsole";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelIPAddressValidInvalid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label labelPortValidInvalid;
        private System.Windows.Forms.Button bStartStop;
        private System.Windows.Forms.Button bPauseResume;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBox1;

    }
}

