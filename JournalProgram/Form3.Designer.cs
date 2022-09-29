namespace JournalProgram
{
    partial class Form3
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
            this.entryPanel = new System.Windows.Forms.TableLayoutPanel();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.closeBtn = new System.Windows.Forms.Button();
            this.encySubmitBtn = new System.Windows.Forms.Button();
            this.outputLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.entryNbSelect = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.bodyTextBox = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.entryTagTitle = new System.Windows.Forms.Label();
            this.entryTagSelect = new System.Windows.Forms.ComboBox();
            this.addTagBtn = new System.Windows.Forms.Button();
            this.selectedTagsLbl = new System.Windows.Forms.Label();
            this.deleteTagsBtn = new System.Windows.Forms.Button();
            this.entryPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // entryPanel
            // 
            this.entryPanel.ColumnCount = 1;
            this.entryPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.entryPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.entryPanel.Controls.Add(this.titleTextBox, 0, 0);
            this.entryPanel.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.entryPanel.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.entryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entryPanel.Location = new System.Drawing.Point(0, 0);
            this.entryPanel.Name = "entryPanel";
            this.entryPanel.RowCount = 3;
            this.entryPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.entryPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.entryPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.entryPanel.Size = new System.Drawing.Size(960, 536);
            this.entryPanel.TabIndex = 1;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleTextBox.Location = new System.Drawing.Point(3, 3);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(954, 26);
            this.titleTextBox.TabIndex = 0;
            this.titleTextBox.Text = "Title";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.closeBtn);
            this.flowLayoutPanel1.Controls.Add(this.encySubmitBtn);
            this.flowLayoutPanel1.Controls.Add(this.outputLbl);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.entryNbSelect);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 484);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(954, 49);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // closeBtn
            // 
            this.closeBtn.AutoSize = true;
            this.closeBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.closeBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.closeBtn.Location = new System.Drawing.Point(883, 3);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(68, 30);
            this.closeBtn.TabIndex = 0;
            this.closeBtn.Text = "Cancel";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // encySubmitBtn
            // 
            this.encySubmitBtn.AutoSize = true;
            this.encySubmitBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.encySubmitBtn.Location = new System.Drawing.Point(754, 3);
            this.encySubmitBtn.Name = "encySubmitBtn";
            this.encySubmitBtn.Size = new System.Drawing.Size(123, 30);
            this.encySubmitBtn.TabIndex = 1;
            this.encySubmitBtn.Text = "Save Changes";
            this.encySubmitBtn.UseVisualStyleBackColor = true;
            this.encySubmitBtn.Click += new System.EventHandler(this.encySubmitBtn_Click);
            // 
            // outputLbl
            // 
            this.outputLbl.AutoSize = true;
            this.outputLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputLbl.Location = new System.Drawing.Point(748, 0);
            this.outputLbl.Name = "outputLbl";
            this.outputLbl.Size = new System.Drawing.Size(0, 36);
            this.outputLbl.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(742, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 3;
            // 
            // entryNbSelect
            // 
            this.entryNbSelect.FormattingEnabled = true;
            this.entryNbSelect.Location = new System.Drawing.Point(563, 3);
            this.entryNbSelect.Name = "entryNbSelect";
            this.entryNbSelect.Size = new System.Drawing.Size(173, 28);
            this.entryNbSelect.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(479, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Notebook";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.bodyTextBox, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel5, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 29);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.29391F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.706093F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(954, 449);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // bodyTextBox
            // 
            this.bodyTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bodyTextBox.Location = new System.Drawing.Point(3, 3);
            this.bodyTextBox.Name = "bodyTextBox";
            this.bodyTextBox.Size = new System.Drawing.Size(948, 408);
            this.bodyTextBox.TabIndex = 1;
            this.bodyTextBox.Text = "";
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.entryTagTitle);
            this.flowLayoutPanel5.Controls.Add(this.entryTagSelect);
            this.flowLayoutPanel5.Controls.Add(this.addTagBtn);
            this.flowLayoutPanel5.Controls.Add(this.selectedTagsLbl);
            this.flowLayoutPanel5.Controls.Add(this.deleteTagsBtn);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 417);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(948, 29);
            this.flowLayoutPanel5.TabIndex = 2;
            // 
            // entryTagTitle
            // 
            this.entryTagTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.entryTagTitle.AutoSize = true;
            this.entryTagTitle.Location = new System.Drawing.Point(3, 8);
            this.entryTagTitle.Name = "entryTagTitle";
            this.entryTagTitle.Size = new System.Drawing.Size(44, 20);
            this.entryTagTitle.TabIndex = 0;
            this.entryTagTitle.Text = "Tags";
            // 
            // entryTagSelect
            // 
            this.entryTagSelect.FormattingEnabled = true;
            this.entryTagSelect.Location = new System.Drawing.Point(53, 3);
            this.entryTagSelect.Name = "entryTagSelect";
            this.entryTagSelect.Size = new System.Drawing.Size(138, 28);
            this.entryTagSelect.TabIndex = 1;
            // 
            // addTagBtn
            // 
            this.addTagBtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.addTagBtn.AutoSize = true;
            this.addTagBtn.Location = new System.Drawing.Point(197, 3);
            this.addTagBtn.Name = "addTagBtn";
            this.addTagBtn.Size = new System.Drawing.Size(79, 30);
            this.addTagBtn.TabIndex = 2;
            this.addTagBtn.Text = "Add Tag";
            this.addTagBtn.UseVisualStyleBackColor = true;
            this.addTagBtn.Click += new System.EventHandler(this.addTagBtn_Click);
            // 
            // selectedTagsLbl
            // 
            this.selectedTagsLbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.selectedTagsLbl.AutoSize = true;
            this.selectedTagsLbl.Location = new System.Drawing.Point(282, 8);
            this.selectedTagsLbl.Name = "selectedTagsLbl";
            this.selectedTagsLbl.Size = new System.Drawing.Size(0, 20);
            this.selectedTagsLbl.TabIndex = 3;
            // 
            // deleteTagsBtn
            // 
            this.deleteTagsBtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.deleteTagsBtn.AutoSize = true;
            this.deleteTagsBtn.Location = new System.Drawing.Point(288, 3);
            this.deleteTagsBtn.Name = "deleteTagsBtn";
            this.deleteTagsBtn.Size = new System.Drawing.Size(117, 30);
            this.deleteTagsBtn.TabIndex = 4;
            this.deleteTagsBtn.Text = "Remove Tags";
            this.deleteTagsBtn.UseVisualStyleBackColor = true;
            this.deleteTagsBtn.Click += new System.EventHandler(this.deleteTagsBtn_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 536);
            this.Controls.Add(this.entryPanel);
            this.Name = "Form3";
            this.Text = "Form3";
            this.entryPanel.ResumeLayout(false);
            this.entryPanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel entryPanel;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button encySubmitBtn;
        private System.Windows.Forms.Label outputLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox entryNbSelect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RichTextBox bodyTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label entryTagTitle;
        private System.Windows.Forms.ComboBox entryTagSelect;
        private System.Windows.Forms.Button addTagBtn;
        private System.Windows.Forms.Label selectedTagsLbl;
        private System.Windows.Forms.Button deleteTagsBtn;
    }
}