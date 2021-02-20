
namespace AdvRegionsDesigner.Models.UI
{
    partial class RegionControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtRegionName = new System.Windows.Forms.TextBox();
            this.lblPoints = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.contextMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbVisibility = new System.Windows.Forms.PictureBox();
            this.pbColor = new System.Windows.Forms.PictureBox();
            this.contextMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVisibility)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRegionName
            // 
            this.txtRegionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegionName.Location = new System.Drawing.Point(4, 4);
            this.txtRegionName.Name = "txtRegionName";
            this.txtRegionName.Size = new System.Drawing.Size(210, 26);
            this.txtRegionName.TabIndex = 0;
            this.txtRegionName.Text = "New Region";
            this.txtRegionName.TextChanged += new System.EventHandler(this.txtRegionName_TextChanged);
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoints.Location = new System.Drawing.Point(4, 33);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(66, 18);
            this.lblPoints.TabIndex = 1;
            this.lblPoints.Text = "Points: 0";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(4, 59);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(66, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(76, 59);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(66, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(146, 59);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(66, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // contextMain
            // 
            this.contextMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetColorToolStripMenuItem});
            this.contextMain.Name = "contextMain";
            this.contextMain.Size = new System.Drawing.Size(135, 26);
            // 
            // resetColorToolStripMenuItem
            // 
            this.resetColorToolStripMenuItem.Name = "resetColorToolStripMenuItem";
            this.resetColorToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.resetColorToolStripMenuItem.Text = "Reset Color";
            this.resetColorToolStripMenuItem.Click += new System.EventHandler(this.resetColorToolStripMenuItem_Click);
            // 
            // pbVisibility
            // 
            this.pbVisibility.Image = global::AdvancedRegionsDedicator.RenderAssets.Alpha;
            this.pbVisibility.Location = new System.Drawing.Point(162, 36);
            this.pbVisibility.Name = "pbVisibility";
            this.pbVisibility.Size = new System.Drawing.Size(23, 18);
            this.pbVisibility.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbVisibility.TabIndex = 6;
            this.pbVisibility.TabStop = false;
            this.pbVisibility.Click += new System.EventHandler(this.pbVisibility_Click);
            // 
            // pbColor
            // 
            this.pbColor.Location = new System.Drawing.Point(188, 34);
            this.pbColor.Name = "pbColor";
            this.pbColor.Size = new System.Drawing.Size(21, 20);
            this.pbColor.TabIndex = 5;
            this.pbColor.TabStop = false;
            this.pbColor.Click += new System.EventHandler(this.pbColor_Click);
            // 
            // RegionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMain;
            this.Controls.Add(this.pbVisibility);
            this.Controls.Add(this.pbColor);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.txtRegionName);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "RegionControl";
            this.Size = new System.Drawing.Size(215, 85);
            this.contextMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbVisibility)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRegionName;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.PictureBox pbColor;
        private System.Windows.Forms.ContextMenuStrip contextMain;
        private System.Windows.Forms.ToolStripMenuItem resetColorToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbVisibility;
    }
}
