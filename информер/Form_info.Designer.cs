namespace l2mega_informer
{
    partial class Form_info
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_info));
            this.webBrowser_info = new System.Windows.Forms.WebBrowser();
            this.linkLabel_info = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel_reva = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // webBrowser_info
            // 
            this.webBrowser_info.Location = new System.Drawing.Point(0, -1);
            this.webBrowser_info.MaximumSize = new System.Drawing.Size(635, 300);
            this.webBrowser_info.MinimumSize = new System.Drawing.Size(635, 300);
            this.webBrowser_info.Name = "webBrowser_info";
            this.webBrowser_info.ScrollBarsEnabled = false;
            this.webBrowser_info.Size = new System.Drawing.Size(635, 300);
            this.webBrowser_info.TabIndex = 0;
            // 
            // linkLabel_info
            // 
            this.linkLabel_info.AutoSize = true;
            this.linkLabel_info.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabel_info.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel_info.Location = new System.Drawing.Point(146, 307);
            this.linkLabel_info.Name = "linkLabel_info";
            this.linkLabel_info.Size = new System.Drawing.Size(158, 31);
            this.linkLabel_info.TabIndex = 1;
            this.linkLabel_info.TabStop = true;
            this.linkLabel_info.Text = "l2mega.net";
            this.linkLabel_info.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_info_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 320);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ссылка";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(325, 320);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ваша версия информера";
            // 
            // linkLabel_reva
            // 
            this.linkLabel_reva.AutoSize = true;
            this.linkLabel_reva.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabel_reva.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel_reva.LinkColor = System.Drawing.Color.Navy;
            this.linkLabel_reva.Location = new System.Drawing.Point(465, 313);
            this.linkLabel_reva.Name = "linkLabel_reva";
            this.linkLabel_reva.Size = new System.Drawing.Size(44, 25);
            this.linkLabel_reva.TabIndex = 4;
            this.linkLabel_reva.TabStop = true;
            this.linkLabel_reva.Text = "v.1";
            // 
            // Form_info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(635, 347);
            this.Controls.Add(this.linkLabel_reva);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel_info);
            this.Controls.Add(this.webBrowser_info);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(643, 381);
            this.MinimumSize = new System.Drawing.Size(643, 381);
            this.Name = "Form_info";
            this.Text = "Form_info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser_info;
        private System.Windows.Forms.LinkLabel linkLabel_info;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel_reva;
    }
}