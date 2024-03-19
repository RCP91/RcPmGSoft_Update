namespace AutoUpdate.Classes
{
    partial class F_Update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Update));
            progressBar1 = new ProgressBar();
            lb_txt = new Label();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.BackColor = Color.Silver;
            progressBar1.ForeColor = Color.ForestGreen;
            progressBar1.Location = new Point(12, 60);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(377, 23);
            progressBar1.TabIndex = 4;
            // 
            // lb_txt
            // 
            lb_txt.BackColor = Color.Transparent;
            lb_txt.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lb_txt.Location = new Point(12, 9);
            lb_txt.Name = "lb_txt";
            lb_txt.Size = new Size(377, 48);
            lb_txt.TabIndex = 3;
            lb_txt.Text = "...";
            lb_txt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // F_Update
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(401, 93);
            ControlBox = false;
            Controls.Add(lb_txt);
            Controls.Add(progressBar1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "F_Update";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Checking Update!";
            ResumeLayout(false);
        }

        #endregion
        private ProgressBar progressBar1;
        private Label lb_txt;
    }
}