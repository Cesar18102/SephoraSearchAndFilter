
namespace SephoraSearchEngine
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BadWordsTextBox = new System.Windows.Forms.TextBox();
            this.LoadCategoriesButton = new System.Windows.Forms.Button();
            this.CategoryTree = new System.Windows.Forms.TreeView();
            this.ApiKeysTextBox = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LogWindow = new System.Windows.Forms.RichTextBox();
            this.ProductsGrid = new System.Windows.Forms.DataGridView();
            this.ExportButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.ExportButton);
            this.panel1.Controls.Add(this.BadWordsTextBox);
            this.panel1.Controls.Add(this.LoadCategoriesButton);
            this.panel1.Controls.Add(this.CategoryTree);
            this.panel1.Controls.Add(this.ApiKeysTextBox);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 598);
            this.panel1.TabIndex = 5;
            // 
            // BadWordsTextBox
            // 
            this.BadWordsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BadWordsTextBox.Location = new System.Drawing.Point(12, 502);
            this.BadWordsTextBox.Name = "BadWordsTextBox";
            this.BadWordsTextBox.Size = new System.Drawing.Size(430, 20);
            this.BadWordsTextBox.TabIndex = 6;
            this.BadWordsTextBox.TextChanged += new System.EventHandler(this.BadWordsTextBox_TextChanged);
            // 
            // LoadCategoriesButton
            // 
            this.LoadCategoriesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LoadCategoriesButton.Location = new System.Drawing.Point(12, 473);
            this.LoadCategoriesButton.Name = "LoadCategoriesButton";
            this.LoadCategoriesButton.Size = new System.Drawing.Size(125, 23);
            this.LoadCategoriesButton.TabIndex = 5;
            this.LoadCategoriesButton.Text = "Load Categories";
            this.LoadCategoriesButton.UseVisualStyleBackColor = true;
            this.LoadCategoriesButton.Click += new System.EventHandler(this.LoadCategoriesButton_Click);
            // 
            // CategoryTree
            // 
            this.CategoryTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CategoryTree.Location = new System.Drawing.Point(0, 0);
            this.CategoryTree.Name = "CategoryTree";
            this.CategoryTree.Size = new System.Drawing.Size(445, 467);
            this.CategoryTree.TabIndex = 4;
            this.CategoryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.CategoryTree_BeforeExpand);
            this.CategoryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CategoryTree_AfterSelect);
            // 
            // ApiKeysTextBox
            // 
            this.ApiKeysTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ApiKeysTextBox.Location = new System.Drawing.Point(0, 528);
            this.ApiKeysTextBox.Name = "ApiKeysTextBox";
            this.ApiKeysTextBox.Size = new System.Drawing.Size(445, 70);
            this.ApiKeysTextBox.TabIndex = 3;
            this.ApiKeysTextBox.Text = "";
            this.ApiKeysTextBox.TextChanged += new System.EventHandler(this.ApiKeysTextBox_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.LogWindow);
            this.panel2.Controls.Add(this.ProductsGrid);
            this.panel2.Location = new System.Drawing.Point(451, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(673, 598);
            this.panel2.TabIndex = 6;
            // 
            // LogWindow
            // 
            this.LogWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogWindow.Location = new System.Drawing.Point(0, 473);
            this.LogWindow.Name = "LogWindow";
            this.LogWindow.ReadOnly = true;
            this.LogWindow.Size = new System.Drawing.Size(673, 125);
            this.LogWindow.TabIndex = 6;
            this.LogWindow.Text = "";
            // 
            // ProductsGrid
            // 
            this.ProductsGrid.AllowUserToAddRows = false;
            this.ProductsGrid.AllowUserToDeleteRows = false;
            this.ProductsGrid.AllowUserToResizeColumns = false;
            this.ProductsGrid.AllowUserToResizeRows = false;
            this.ProductsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProductsGrid.Location = new System.Drawing.Point(0, 0);
            this.ProductsGrid.Name = "ProductsGrid";
            this.ProductsGrid.ReadOnly = true;
            this.ProductsGrid.Size = new System.Drawing.Size(673, 467);
            this.ProductsGrid.TabIndex = 5;
            // 
            // ExportButton
            // 
            this.ExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExportButton.Location = new System.Drawing.Point(317, 471);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(125, 23);
            this.ExportButton.TabIndex = 7;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 598);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProductsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView CategoryTree;
        private System.Windows.Forms.RichTextBox ApiKeysTextBox;
        private System.Windows.Forms.Button LoadCategoriesButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox LogWindow;
        private System.Windows.Forms.DataGridView ProductsGrid;
        private System.Windows.Forms.TextBox BadWordsTextBox;
        private System.Windows.Forms.Button ExportButton;
    }
}

