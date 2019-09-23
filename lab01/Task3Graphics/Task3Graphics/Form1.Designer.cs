namespace Task3Graphics
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
            this.valuebar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.huebar = new System.Windows.Forms.TrackBar();
            this.saturationbar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openfilebutton = new System.Windows.Forms.Button();
            this.savefilebutton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.redrawbutton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.valuebar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.huebar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // valuebar
            // 
            this.valuebar.LargeChange = 1;
            this.valuebar.Location = new System.Drawing.Point(861, 56);
            this.valuebar.Maximum = 100;
            this.valuebar.Minimum = -100;
            this.valuebar.Name = "valuebar";
            this.valuebar.Size = new System.Drawing.Size(217, 56);
            this.valuebar.TabIndex = 200;
            this.valuebar.Scroll += new System.EventHandler(this.valuebar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(946, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Value";
           
            // 
            // huebar
            // 
            this.huebar.LargeChange = 1;
            this.huebar.Location = new System.Drawing.Point(421, 59);
            this.huebar.Maximum = 360;
            this.huebar.Minimum = -360;
            this.huebar.Name = "huebar";
            this.huebar.Size = new System.Drawing.Size(217, 56);
            this.huebar.TabIndex = 2;
            this.huebar.Scroll += new System.EventHandler(this.huebar_Scroll);
            // 
            // saturationbar
            // 
            this.saturationbar.LargeChange = 1;
            this.saturationbar.Location = new System.Drawing.Point(644, 59);
            this.saturationbar.Maximum = 100;
            this.saturationbar.Minimum = -100;
            this.saturationbar.Name = "saturationbar";
            this.saturationbar.Size = new System.Drawing.Size(217, 56);
            this.saturationbar.TabIndex = 3;
            this.saturationbar.Scroll += new System.EventHandler(this.saturationbar_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(706, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Saturation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(500, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hue";
            // 
            // openfilebutton
            // 
            this.openfilebutton.Location = new System.Drawing.Point(7, 31);
            this.openfilebutton.Name = "openfilebutton";
            this.openfilebutton.Size = new System.Drawing.Size(70, 66);
            this.openfilebutton.TabIndex = 6;
            this.openfilebutton.Text = "Open File";
            this.openfilebutton.UseVisualStyleBackColor = true;
            this.openfilebutton.Click += new System.EventHandler(this.openfilebutton_Click);
            // 
            // savefilebutton
            // 
            this.savefilebutton.Location = new System.Drawing.Point(83, 30);
            this.savefilebutton.Name = "savefilebutton";
            this.savefilebutton.Size = new System.Drawing.Size(65, 68);
            this.savefilebutton.TabIndex = 7;
            this.savefilebutton.Text = "Save File";
            this.savefilebutton.UseVisualStyleBackColor = true;
            this.savefilebutton.Click += new System.EventHandler(this.savefilebutton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(272, 30);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(119, 67);
            this.ResetButton.TabIndex = 201;
            this.ResetButton.Text = "Reset picture!";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1572, 815);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 202;
            this.pictureBox1.TabStop = false;
            // 
            // redrawbutton
            // 
            this.redrawbutton.Location = new System.Drawing.Point(167, 31);
            this.redrawbutton.Name = "redrawbutton";
            this.redrawbutton.Size = new System.Drawing.Size(81, 67);
            this.redrawbutton.TabIndex = 203;
            this.redrawbutton.Text = "Redraw";
            this.redrawbutton.UseVisualStyleBackColor = true;
            this.redrawbutton.Click += new System.EventHandler(this.redrawbutton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.valuebar);
            this.groupBox1.Controls.Add(this.redrawbutton);
            this.groupBox1.Controls.Add(this.saturationbar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ResetButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.openfilebutton);
            this.groupBox1.Controls.Add(this.huebar);
            this.groupBox1.Controls.Add(this.savefilebutton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1572, 115);
            this.groupBox1.TabIndex = 204;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1572, 815);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.valuebar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.huebar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar valuebar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar huebar;
        private System.Windows.Forms.TrackBar saturationbar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button openfilebutton;
        private System.Windows.Forms.Button savefilebutton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button redrawbutton;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

