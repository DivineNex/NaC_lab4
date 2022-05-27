namespace NewModules
{
    partial class SeriesCreationForm
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
            this.listBoxAllParamsNames = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxAllParamsNames
            // 
            this.listBoxAllParamsNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAllParamsNames.FormattingEnabled = true;
            this.listBoxAllParamsNames.ItemHeight = 25;
            this.listBoxAllParamsNames.Location = new System.Drawing.Point(0, 0);
            this.listBoxAllParamsNames.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.listBoxAllParamsNames.Name = "listBoxAllParamsNames";
            this.listBoxAllParamsNames.Size = new System.Drawing.Size(349, 502);
            this.listBoxAllParamsNames.TabIndex = 0;
            this.listBoxAllParamsNames.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxAllParamsNames_MouseDoubleClick);
            // 
            // SeriesCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 502);
            this.Controls.Add(this.listBoxAllParamsNames);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "SeriesCreationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор параметра";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxAllParamsNames;
    }
}