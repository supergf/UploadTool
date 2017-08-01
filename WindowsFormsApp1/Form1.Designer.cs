namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.inputUrl = new System.Windows.Forms.TextBox();
            this.fileButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.fileList = new System.Windows.Forms.TextBox();
            this.upload = new System.Windows.Forms.Button();
            this.folderButton = new System.Windows.Forms.Button();
            this.fileListBox = new System.Windows.Forms.ListBox();
            this.lastConfigbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(25, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "链接：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // inputUrl
            // 
            this.inputUrl.Location = new System.Drawing.Point(99, 13);
            this.inputUrl.Name = "inputUrl";
            this.inputUrl.Size = new System.Drawing.Size(302, 28);
            this.inputUrl.TabIndex = 1;
            this.inputUrl.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // fileButton
            // 
            this.fileButton.Location = new System.Drawing.Point(28, 83);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(125, 49);
            this.fileButton.TabIndex = 2;
            this.fileButton.Text = "选择上传文件";
            this.fileButton.UseVisualStyleBackColor = true;
            this.fileButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(25, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "文件列表:";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // fileList
            // 
            this.fileList.Location = new System.Drawing.Point(149, 182);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(302, 28);
            this.fileList.TabIndex = 1;
            this.fileList.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // upload
            // 
            this.upload.Location = new System.Drawing.Point(314, 83);
            this.upload.Name = "upload";
            this.upload.Size = new System.Drawing.Size(87, 49);
            this.upload.TabIndex = 3;
            this.upload.Text = "上传";
            this.upload.UseVisualStyleBackColor = true;
            this.upload.Click += new System.EventHandler(this.upload_Click);
            // 
            // folderButton
            // 
            this.folderButton.Location = new System.Drawing.Point(177, 83);
            this.folderButton.Name = "folderButton";
            this.folderButton.Size = new System.Drawing.Size(107, 49);
            this.folderButton.TabIndex = 4;
            this.folderButton.Text = "选择文件夹";
            this.folderButton.UseVisualStyleBackColor = true;
            this.folderButton.Click += new System.EventHandler(this.folderButton_Click);
            // 
            // fileListBox
            // 
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.ItemHeight = 18;
            this.fileListBox.Location = new System.Drawing.Point(149, 216);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.Size = new System.Drawing.Size(302, 94);
            this.fileListBox.TabIndex = 5;
            this.fileListBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // lastConfigbutton
            // 
            this.lastConfigbutton.Location = new System.Drawing.Point(426, 83);
            this.lastConfigbutton.Name = "lastConfigbutton";
            this.lastConfigbutton.Size = new System.Drawing.Size(129, 49);
            this.lastConfigbutton.TabIndex = 6;
            this.lastConfigbutton.Text = "加载上次上传";
            this.lastConfigbutton.UseVisualStyleBackColor = true;
            this.lastConfigbutton.Click += new System.EventHandler(this.lastConfigbutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 391);
            this.Controls.Add(this.lastConfigbutton);
            this.Controls.Add(this.fileListBox);
            this.Controls.Add(this.folderButton);
            this.Controls.Add(this.upload);
            this.Controls.Add(this.fileButton);
            this.Controls.Add(this.fileList);
            this.Controls.Add(this.inputUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputUrl;
        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fileList;
        private System.Windows.Forms.Button upload;
        private System.Windows.Forms.Button folderButton;
        private System.Windows.Forms.ListBox fileListBox;
        private System.Windows.Forms.Button lastConfigbutton;
    }
}

