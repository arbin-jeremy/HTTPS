namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            serverbtn = new Button();
            client1btn = new Button();
            client2btn = new Button();
            label1 = new Label();
            textBoxNumFiles = new TextBox();
            textBoxFileSize = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBoxServerUrl = new TextBox();
            label5 = new Label();
            textBoxTimeStamp1 = new TextBox();
            textBoxTimeStamp2 = new TextBox();
            label6 = new Label();
            label7 = new Label();
            textBoxTotalTime = new TextBox();
            label8 = new Label();
            SuspendLayout();
            // 
            // serverbtn
            // 
            serverbtn.Location = new Point(305, 355);
            serverbtn.Name = "serverbtn";
            serverbtn.Size = new Size(117, 23);
            serverbtn.TabIndex = 0;
            serverbtn.Text = "Start Server";
            serverbtn.UseVisualStyleBackColor = true;
            serverbtn.Click += serverbtn_Click;
            // 
            // client1btn
            // 
            client1btn.Location = new Point(134, 400);
            client1btn.Name = "client1btn";
            client1btn.Size = new Size(117, 23);
            client1btn.TabIndex = 1;
            client1btn.Text = "upload";
            client1btn.UseVisualStyleBackColor = true;
            client1btn.Click += client1btn_Click;
            // 
            // client2btn
            // 
            client2btn.Location = new Point(488, 400);
            client2btn.Name = "client2btn";
            client2btn.Size = new Size(117, 23);
            client2btn.TabIndex = 2;
            client2btn.Text = "download";
            client2btn.UseVisualStyleBackColor = true;
            client2btn.Click += client2btn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(116, 41);
            label1.Name = "label1";
            label1.Size = new Size(163, 15);
            label1.TabIndex = 3;
            label1.Text = "HTTP Files Transfer Speed Test";
            // 
            // textBoxNumFiles
            // 
            textBoxNumFiles.Location = new Point(451, 100);
            textBoxNumFiles.Name = "textBoxNumFiles";
            textBoxNumFiles.Size = new Size(100, 23);
            textBoxNumFiles.TabIndex = 4;
            textBoxNumFiles.TextChanged += textBoxNumFiles_TextChanged;
            // 
            // textBoxFileSize
            // 
            textBoxFileSize.Location = new Point(451, 143);
            textBoxFileSize.Name = "textBoxFileSize";
            textBoxFileSize.Size = new Size(100, 23);
            textBoxFileSize.TabIndex = 5;
            textBoxFileSize.TextChanged += textBoxFileSize_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(382, 100);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 6;
            label2.Text = "Num_files:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(395, 146);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 7;
            label3.Text = "File size:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(557, 146);
            label4.Name = "label4";
            label4.Size = new Size(20, 15);
            label4.TabIndex = 8;
            label4.Text = "kB";
            // 
            // textBoxServerUrl
            // 
            textBoxServerUrl.Location = new Point(451, 198);
            textBoxServerUrl.Name = "textBoxServerUrl";
            textBoxServerUrl.Size = new Size(348, 23);
            textBoxServerUrl.TabIndex = 9;
            textBoxServerUrl.TextChanged += textBoxServerUrl_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(384, 201);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 10;
            label5.Text = "serverURL";
            // 
            // textBoxTimeStamp1
            // 
            textBoxTimeStamp1.Location = new Point(134, 143);
            textBoxTimeStamp1.Name = "textBoxTimeStamp1";
            textBoxTimeStamp1.Size = new Size(145, 23);
            textBoxTimeStamp1.TabIndex = 11;
            // 
            // textBoxTimeStamp2
            // 
            textBoxTimeStamp2.Location = new Point(134, 198);
            textBoxTimeStamp2.Name = "textBoxTimeStamp2";
            textBoxTimeStamp2.Size = new Size(145, 23);
            textBoxTimeStamp2.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(21, 146);
            label6.Name = "label6";
            label6.Size = new Size(107, 15);
            label6.TabIndex = 13;
            label6.Text = "Upload Timestamp";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(5, 201);
            label7.Name = "label7";
            label7.Size = new Size(123, 15);
            label7.TabIndex = 14;
            label7.Text = "Download Timestamp";
            // 
            // textBoxTotalTime
            // 
            textBoxTotalTime.Location = new Point(258, 271);
            textBoxTotalTime.Name = "textBoxTotalTime";
            textBoxTotalTime.Size = new Size(145, 23);
            textBoxTotalTime.TabIndex = 15;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(80, 279);
            label8.Name = "label8";
            label8.Size = new Size(141, 15);
            label8.TabIndex = 16;
            label8.Text = "Total File transfer Time (s)";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(825, 472);
            Controls.Add(label8);
            Controls.Add(textBoxTotalTime);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(textBoxTimeStamp2);
            Controls.Add(textBoxTimeStamp1);
            Controls.Add(label5);
            Controls.Add(textBoxServerUrl);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBoxFileSize);
            Controls.Add(textBoxNumFiles);
            Controls.Add(label1);
            Controls.Add(client2btn);
            Controls.Add(client1btn);
            Controls.Add(serverbtn);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button serverbtn;
        private Button client1btn;
        private Button client2btn;
        private Label label1;
        private TextBox textBoxNumFiles;
        private TextBox textBoxFileSize;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBoxServerUrl;
        private Label label5;
        private TextBox textBoxTimeStamp1;
        private TextBox textBoxTimeStamp2;
        private Label label6;
        private Label label7;
        private TextBox textBoxTotalTime;
        private Label label8;
    }
}
