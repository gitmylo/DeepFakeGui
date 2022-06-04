namespace DeepFakeGui
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
            this.imagePreviewBox = new System.Windows.Forms.PictureBox();
            this.imagePickDialog = new System.Windows.Forms.OpenFileDialog();
            this.videoPickDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageLoadButton = new System.Windows.Forms.Button();
            this.videoLoadButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.processButton = new System.Windows.Forms.Button();
            this.modelBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize) (this.imagePreviewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // imagePreviewBox
            // 
            this.imagePreviewBox.BackColor = System.Drawing.SystemColors.Control;
            this.imagePreviewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagePreviewBox.Location = new System.Drawing.Point(12, 12);
            this.imagePreviewBox.Name = "imagePreviewBox";
            this.imagePreviewBox.Size = new System.Drawing.Size(128, 125);
            this.imagePreviewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imagePreviewBox.TabIndex = 0;
            this.imagePreviewBox.TabStop = false;
            // 
            // imagePickDialog
            // 
            this.imagePickDialog.InitialDirectory = "templateimages";
            this.imagePickDialog.Tag = "";
            this.imagePickDialog.Title = "Pick an image";
            this.imagePickDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.imagePickDialog_FileOk);
            // 
            // videoPickDialog
            // 
            this.videoPickDialog.InitialDirectory = "motionvideos";
            this.videoPickDialog.Tag = "";
            this.videoPickDialog.Title = "Pick a video";
            this.videoPickDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.videoPickDialog_FileOk);
            // 
            // imageLoadButton
            // 
            this.imageLoadButton.Location = new System.Drawing.Point(146, 12);
            this.imageLoadButton.Name = "imageLoadButton";
            this.imageLoadButton.Size = new System.Drawing.Size(128, 23);
            this.imageLoadButton.TabIndex = 1;
            this.imageLoadButton.Text = "Load image";
            this.imageLoadButton.UseVisualStyleBackColor = true;
            this.imageLoadButton.Click += new System.EventHandler(this.imageLoadButton_Click);
            // 
            // videoLoadButton
            // 
            this.videoLoadButton.Location = new System.Drawing.Point(146, 41);
            this.videoLoadButton.Name = "videoLoadButton";
            this.videoLoadButton.Size = new System.Drawing.Size(128, 23);
            this.videoLoadButton.TabIndex = 2;
            this.videoLoadButton.Text = "Load video";
            this.videoLoadButton.UseVisualStyleBackColor = true;
            this.videoLoadButton.Click += new System.EventHandler(this.videoLoadButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(146, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Resolution";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(146, 117);
            this.numericUpDown1.Maximum = new decimal(new int[] {999999, 0, 0, 0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(128, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {512, 0, 0, 0});
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(146, 70);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(128, 26);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Absolute Position";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // processButton
            // 
            this.processButton.Location = new System.Drawing.Point(12, 143);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(128, 41);
            this.processButton.TabIndex = 6;
            this.processButton.Text = "Process";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.processButton_Click);
            // 
            // modelBox
            // 
            this.modelBox.Location = new System.Drawing.Point(146, 164);
            this.modelBox.Name = "modelBox";
            this.modelBox.Size = new System.Drawing.Size(133, 20);
            this.modelBox.TabIndex = 7;
            this.modelBox.Text = "vox-adv";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(146, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Config/Dataset";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 196);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.modelBox);
            this.Controls.Add(this.processButton);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.videoLoadButton);
            this.Controls.Add(this.imageLoadButton);
            this.Controls.Add(this.imagePreviewBox);
            this.HelpButton = true;
            this.MaximumSize = new System.Drawing.Size(307, 235);
            this.MinimumSize = new System.Drawing.Size(307, 235);
            this.Name = "Form1";
            this.Text = "First order motion gui";
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.form1_HelpRequested);
            ((System.ComponentModel.ISupportInitialize) (this.imagePreviewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox modelBox;

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Button processButton;

        private System.Windows.Forms.CheckBox checkBox1;

        private System.Windows.Forms.NumericUpDown numericUpDown1;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Button imageLoadButton;
        private System.Windows.Forms.Button videoLoadButton;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.OpenFileDialog videoPickDialog;

        private System.Windows.Forms.OpenFileDialog imagePickDialog;

        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        private System.Windows.Forms.PictureBox imagePreviewBox;

        #endregion
    }
}