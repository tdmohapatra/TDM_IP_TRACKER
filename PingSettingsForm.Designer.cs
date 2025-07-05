namespace TDM_IP_Tracker
{
    partial class PingSettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Label lblPingCount;
        private System.Windows.Forms.NumericUpDown numPingCount;
        private System.Windows.Forms.Label lblPacketSize;
        private System.Windows.Forms.NumericUpDown numPacketSize;
        private System.Windows.Forms.Label lblTTL;
        private System.Windows.Forms.NumericUpDown numTTL;
        private System.Windows.Forms.CheckBox chkDontFragment;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.CheckBox chkResolveHostnames;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        /// <summary>
        /// Clean up resources
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTimeout = new System.Windows.Forms.Label();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblPingCount = new System.Windows.Forms.Label();
            this.numPingCount = new System.Windows.Forms.NumericUpDown();
            this.lblPacketSize = new System.Windows.Forms.Label();
            this.numPacketSize = new System.Windows.Forms.NumericUpDown();
            this.lblTTL = new System.Windows.Forms.Label();
            this.numTTL = new System.Windows.Forms.NumericUpDown();
            this.chkDontFragment = new System.Windows.Forms.CheckBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.numInterval = new System.Windows.Forms.NumericUpDown();
            this.chkResolveHostnames = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPingCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPacketSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTTL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).BeginInit();

            this.SuspendLayout();

            // lblTimeout
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Location = new System.Drawing.Point(30, 30);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(98, 15);
            this.lblTimeout.Text = "Timeout (ms):";

            // numTimeout
            this.numTimeout.Location = new System.Drawing.Point(180, 28);
            this.numTimeout.Maximum = 60000;
            this.numTimeout.Minimum = 100;
            this.numTimeout.Increment = 100;
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(120, 23);
            this.numTimeout.Value = 4000;

            // lblPingCount
            this.lblPingCount.AutoSize = true;
            this.lblPingCount.Location = new System.Drawing.Point(30, 70);
            this.lblPingCount.Name = "lblPingCount";
            this.lblPingCount.Size = new System.Drawing.Size(87, 15);
            this.lblPingCount.Text = "Ping Count:";

            // numPingCount
            this.numPingCount.Location = new System.Drawing.Point(180, 68);
            this.numPingCount.Minimum = 1;
            this.numPingCount.Maximum = 100;
            this.numPingCount.Name = "numPingCount";
            this.numPingCount.Size = new System.Drawing.Size(120, 23);
            this.numPingCount.Value = 4;

            // lblPacketSize
            this.lblPacketSize.AutoSize = true;
            this.lblPacketSize.Location = new System.Drawing.Point(30, 110);
            this.lblPacketSize.Name = "lblPacketSize";
            this.lblPacketSize.Size = new System.Drawing.Size(88, 15);
            this.lblPacketSize.Text = "Packet Size (bytes):";

            // numPacketSize
            this.numPacketSize.Location = new System.Drawing.Point(180, 108);
            this.numPacketSize.Minimum = 1;
            this.numPacketSize.Maximum = 65500;
            this.numPacketSize.Name = "numPacketSize";
            this.numPacketSize.Size = new System.Drawing.Size(120, 23);
            this.numPacketSize.Value = 32;

            // lblTTL
            this.lblTTL.AutoSize = true;
            this.lblTTL.Location = new System.Drawing.Point(30, 150);
            this.lblTTL.Name = "lblTTL";
            this.lblTTL.Size = new System.Drawing.Size(30, 15);
            this.lblTTL.Text = "TTL:";

            // numTTL
            this.numTTL.Location = new System.Drawing.Point(180, 148);
            this.numTTL.Minimum = 1;
            this.numTTL.Maximum = 255;
            this.numTTL.Name = "numTTL";
            this.numTTL.Size = new System.Drawing.Size(120, 23);
            this.numTTL.Value = 128;

            // chkDontFragment
            this.chkDontFragment.AutoSize = true;
            this.chkDontFragment.Location = new System.Drawing.Point(30, 190);
            this.chkDontFragment.Name = "chkDontFragment";
            this.chkDontFragment.Size = new System.Drawing.Size(110, 19);
            this.chkDontFragment.Text = "Don't Fragment";
            this.chkDontFragment.UseVisualStyleBackColor = true;

            // lblInterval
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(30, 230);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(100, 15);
            this.lblInterval.Text = "Interval (ms):";

            // numInterval
            this.numInterval.Location = new System.Drawing.Point(180, 228);
            this.numInterval.Minimum = 100;
            this.numInterval.Maximum = 10000;
            this.numInterval.Increment = 100;
            this.numInterval.Name = "numInterval";
            this.numInterval.Size = new System.Drawing.Size(120, 23);
            this.numInterval.Value = 1000;

            // chkResolveHostnames
            this.chkResolveHostnames.AutoSize = true;
            this.chkResolveHostnames.Location = new System.Drawing.Point(30, 270);
            this.chkResolveHostnames.Name = "chkResolveHostnames";
            this.chkResolveHostnames.Size = new System.Drawing.Size(137, 19);
            this.chkResolveHostnames.Text = "Resolve Hostnames";
            this.chkResolveHostnames.UseVisualStyleBackColor = true;
            this.chkResolveHostnames.Checked = true;

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(80, 320);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(220, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;

            // PingSettingsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 380);
            this.Controls.Add(this.lblTimeout);
            this.Controls.Add(this.numTimeout);
            this.Controls.Add(this.lblPingCount);
            this.Controls.Add(this.numPingCount);
            this.Controls.Add(this.lblPacketSize);
            this.Controls.Add(this.numPacketSize);
            this.Controls.Add(this.lblTTL);
            this.Controls.Add(this.numTTL);
            this.Controls.Add(this.chkDontFragment);
            this.Controls.Add(this.lblInterval);
            this.Controls.Add(this.numInterval);
            this.Controls.Add(this.chkResolveHostnames);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PingSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ping Settings";

            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPingCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPacketSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTTL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
