namespace JournalProgram
{
    partial class Form2
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.searchTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.searchBodyBtn = new System.Windows.Forms.Button();
            this.searchTitleBtn = new System.Windows.Forms.Button();
            this.searchTagsBtn = new System.Windows.Forms.Button();
            this.resultsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(890, 490);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.searchTitle);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(884, 43);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // searchTitle
            // 
            this.searchTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.searchTitle.AutoSize = true;
            this.searchTitle.Font = new System.Drawing.Font("Avenir LT Std 55 Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchTitle.ForeColor = System.Drawing.Color.AliceBlue;
            this.searchTitle.Location = new System.Drawing.Point(3, 0);
            this.searchTitle.Name = "searchTitle";
            this.searchTitle.Size = new System.Drawing.Size(156, 24);
            this.searchTitle.TabIndex = 0;
            this.searchTitle.Text = "Search Results | ";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.95023F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.04977F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.resultsPanel, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 52);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(884, 435);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.searchBodyBtn);
            this.flowLayoutPanel2.Controls.Add(this.searchTitleBtn);
            this.flowLayoutPanel2.Controls.Add(this.searchTagsBtn);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(135, 429);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // searchBodyBtn
            // 
            this.searchBodyBtn.AutoSize = true;
            this.searchBodyBtn.BackColor = System.Drawing.Color.SlateGray;
            this.searchBodyBtn.FlatAppearance.BorderSize = 0;
            this.searchBodyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchBodyBtn.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBodyBtn.ForeColor = System.Drawing.Color.AliceBlue;
            this.searchBodyBtn.Location = new System.Drawing.Point(3, 3);
            this.searchBodyBtn.Name = "searchBodyBtn";
            this.searchBodyBtn.Size = new System.Drawing.Size(128, 39);
            this.searchBodyBtn.TabIndex = 1;
            this.searchBodyBtn.Text = "Body";
            this.searchBodyBtn.UseVisualStyleBackColor = false;
            this.searchBodyBtn.Click += new System.EventHandler(this.searchBodyBtn_Click);
            // 
            // searchTitleBtn
            // 
            this.searchTitleBtn.AutoSize = true;
            this.searchTitleBtn.FlatAppearance.BorderSize = 0;
            this.searchTitleBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchTitleBtn.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchTitleBtn.ForeColor = System.Drawing.Color.AliceBlue;
            this.searchTitleBtn.Location = new System.Drawing.Point(3, 48);
            this.searchTitleBtn.Name = "searchTitleBtn";
            this.searchTitleBtn.Size = new System.Drawing.Size(128, 39);
            this.searchTitleBtn.TabIndex = 0;
            this.searchTitleBtn.Text = "Title";
            this.searchTitleBtn.UseVisualStyleBackColor = true;
            this.searchTitleBtn.Click += new System.EventHandler(this.searchTitleBtn_Click);
            // 
            // searchTagsBtn
            // 
            this.searchTagsBtn.AutoSize = true;
            this.searchTagsBtn.FlatAppearance.BorderSize = 0;
            this.searchTagsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchTagsBtn.Font = new System.Drawing.Font("Avenir LT Std 65 Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchTagsBtn.ForeColor = System.Drawing.Color.AliceBlue;
            this.searchTagsBtn.Location = new System.Drawing.Point(3, 93);
            this.searchTagsBtn.Name = "searchTagsBtn";
            this.searchTagsBtn.Size = new System.Drawing.Size(128, 39);
            this.searchTagsBtn.TabIndex = 2;
            this.searchTagsBtn.Text = "Tags";
            this.searchTagsBtn.UseVisualStyleBackColor = true;
            this.searchTagsBtn.Click += new System.EventHandler(this.searchTagsBtn_Click);
            // 
            // resultsPanel
            // 
            this.resultsPanel.AutoScroll = true;
            this.resultsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.resultsPanel.Location = new System.Drawing.Point(144, 3);
            this.resultsPanel.Name = "resultsPanel";
            this.resultsPanel.Size = new System.Drawing.Size(737, 429);
            this.resultsPanel.TabIndex = 1;
            this.resultsPanel.WrapContents = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(890, 490);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form2";
            this.Text = "Results";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label searchTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button searchTitleBtn;
        private System.Windows.Forms.Button searchBodyBtn;
        private System.Windows.Forms.Button searchTagsBtn;
        private System.Windows.Forms.FlowLayoutPanel resultsPanel;
    }
}