namespace SyncContext
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.mListBox = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mToolStripButtonThreads = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mListBox
            // 
            this.mListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mListBox.FormattingEnabled = true;
            this.mListBox.Location = new System.Drawing.Point(0, 0);
            this.mListBox.Name = "mListBox";
            this.mListBox.Size = new System.Drawing.Size(284, 264);
            this.mListBox.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mToolStripButtonThreads});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mToolStripButtonThreads
            // 
            this.mToolStripButtonThreads.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mToolStripButtonThreads.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mToolStripButtonThreads.Name = "mToolStripButtonThreads";
            this.mToolStripButtonThreads.Size = new System.Drawing.Size(148, 22);
            this.mToolStripButtonThreads.Text = "Press Here to start threads";
            this.mToolStripButtonThreads.Click += new System.EventHandler(this.mToolStripButtonThreads_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mListBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox mListBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton mToolStripButtonThreads;
    }
}
