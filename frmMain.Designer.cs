namespace CheckPowerShell
{
    partial class frmMain
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
            this.lblNet = new System.Windows.Forms.Label();
            this.lblPS = new System.Windows.Forms.Label();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.cmdUpgradeNET = new System.Windows.Forms.LinkLabel();
            this.cmdUpgradePS = new System.Windows.Forms.LinkLabel();
            this.lblVerNET = new System.Windows.Forms.Label();
            this.lblVerPS = new System.Windows.Forms.Label();
            this.cmdWMF = new System.Windows.Forms.LinkLabel();
            this.lblExPol = new System.Windows.Forms.Label();
            this.lblExPolValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNet
            // 
            this.lblNet.AutoSize = true;
            this.lblNet.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblNet.Location = new System.Drawing.Point(14, 25);
            this.lblNet.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNet.Name = "lblNet";
            this.lblNet.Size = new System.Drawing.Size(212, 18);
            this.lblNet.TabIndex = 0;
            this.lblNet.Text = ".NET Framework version:";
            // 
            // lblPS
            // 
            this.lblPS.AutoSize = true;
            this.lblPS.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPS.Location = new System.Drawing.Point(14, 65);
            this.lblPS.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblPS.Name = "lblPS";
            this.lblPS.Size = new System.Drawing.Size(169, 18);
            this.lblPS.TabIndex = 1;
            this.lblPS.Text = "PowerShell version:";
            // 
            // lblExplanation
            // 
            this.lblExplanation.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblExplanation.Location = new System.Drawing.Point(14, 131);
            this.lblExplanation.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new System.Drawing.Size(437, 82);
            this.lblExplanation.TabIndex = 2;
            this.lblExplanation.Text = "Minimum compatible version of PowerShell is 5.1. Your version is 4.0. Upgrading w" +
    "ill install PowerShell 7.2";
            // 
            // cmdUpgradeNET
            // 
            this.cmdUpgradeNET.AutoSize = true;
            this.cmdUpgradeNET.Location = new System.Drawing.Point(375, 25);
            this.cmdUpgradeNET.Name = "cmdUpgradeNET";
            this.cmdUpgradeNET.Size = new System.Drawing.Size(67, 18);
            this.cmdUpgradeNET.TabIndex = 4;
            this.cmdUpgradeNET.TabStop = true;
            this.cmdUpgradeNET.Text = "Update";
            this.cmdUpgradeNET.Visible = false;
            this.cmdUpgradeNET.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdUpgradeNET_LinkClicked);
            // 
            // cmdUpgradePS
            // 
            this.cmdUpgradePS.AutoSize = true;
            this.cmdUpgradePS.Location = new System.Drawing.Point(375, 65);
            this.cmdUpgradePS.Name = "cmdUpgradePS";
            this.cmdUpgradePS.Size = new System.Drawing.Size(67, 18);
            this.cmdUpgradePS.TabIndex = 5;
            this.cmdUpgradePS.TabStop = true;
            this.cmdUpgradePS.Text = "Update";
            this.cmdUpgradePS.Visible = false;
            this.cmdUpgradePS.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdUpgradePS_LinkClicked);
            // 
            // lblVerNET
            // 
            this.lblVerNET.AutoSize = true;
            this.lblVerNET.Location = new System.Drawing.Point(250, 25);
            this.lblVerNET.Name = "lblVerNET";
            this.lblVerNET.Size = new System.Drawing.Size(58, 18);
            this.lblVerNET.TabIndex = 6;
            this.lblVerNET.Text = "label1";
            // 
            // lblVerPS
            // 
            this.lblVerPS.AutoSize = true;
            this.lblVerPS.Location = new System.Drawing.Point(250, 65);
            this.lblVerPS.Name = "lblVerPS";
            this.lblVerPS.Size = new System.Drawing.Size(58, 18);
            this.lblVerPS.TabIndex = 7;
            this.lblVerPS.Text = "label2";
            // 
            // cmdWMF
            // 
            this.cmdWMF.AutoSize = true;
            this.cmdWMF.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdWMF.Location = new System.Drawing.Point(14, 197);
            this.cmdWMF.Name = "cmdWMF";
            this.cmdWMF.Size = new System.Drawing.Size(290, 16);
            this.cmdWMF.TabIndex = 8;
            this.cmdWMF.TabStop = true;
            this.cmdWMF.Text = "Install Windows Management Foundation 4";
            this.cmdWMF.Visible = false;
            this.cmdWMF.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdWMF_LinkClicked);
            // 
            // lblExPol
            // 
            this.lblExPol.AutoSize = true;
            this.lblExPol.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblExPol.Location = new System.Drawing.Point(14, 102);
            this.lblExPol.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblExPol.Name = "lblExPol";
            this.lblExPol.Size = new System.Drawing.Size(147, 18);
            this.lblExPol.TabIndex = 9;
            this.lblExPol.Text = "Execution policy:";
            // 
            // lblExPolValue
            // 
            this.lblExPolValue.AutoSize = true;
            this.lblExPolValue.Location = new System.Drawing.Point(250, 102);
            this.lblExPolValue.Name = "lblExPolValue";
            this.lblExPolValue.Size = new System.Drawing.Size(58, 18);
            this.lblExPolValue.TabIndex = 10;
            this.lblExPolValue.Text = "label2";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 224);
            this.Controls.Add(this.lblExPolValue);
            this.Controls.Add(this.lblExPol);
            this.Controls.Add(this.cmdWMF);
            this.Controls.Add(this.lblVerPS);
            this.Controls.Add(this.lblVerNET);
            this.Controls.Add(this.cmdUpgradePS);
            this.Controls.Add(this.cmdUpgradeNET);
            this.Controls.Add(this.lblExplanation);
            this.Controls.Add(this.lblPS);
            this.Controls.Add(this.lblNet);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check PowerShell version";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNet;
        private System.Windows.Forms.Label lblPS;
        private System.Windows.Forms.Label lblExplanation;
        private System.Windows.Forms.LinkLabel cmdUpgradeNET;
        private System.Windows.Forms.LinkLabel cmdUpgradePS;
        private System.Windows.Forms.Label lblVerNET;
        private System.Windows.Forms.Label lblVerPS;
        private System.Windows.Forms.LinkLabel cmdWMF;
        private System.Windows.Forms.Label lblExPol;
        private System.Windows.Forms.Label lblExPolValue;
    }
}

