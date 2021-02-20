
namespace AdvRegionsDesigner
{
    partial class MainWindow
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
            this.cbAlwaysRender = new System.Windows.Forms.CheckBox();
            this.lblFPS = new System.Windows.Forms.Label();
            this.tbZoom = new System.Windows.Forms.TrackBar();
            this.flowRegions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddRegion = new System.Windows.Forms.Button();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnLoadFrom = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // cbAlwaysRender
            // 
            this.cbAlwaysRender.AutoSize = true;
            this.cbAlwaysRender.Checked = true;
            this.cbAlwaysRender.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAlwaysRender.Location = new System.Drawing.Point(466, 814);
            this.cbAlwaysRender.Name = "cbAlwaysRender";
            this.cbAlwaysRender.Size = new System.Drawing.Size(97, 17);
            this.cbAlwaysRender.TabIndex = 3;
            this.cbAlwaysRender.Text = "Always Render";
            this.cbAlwaysRender.UseVisualStyleBackColor = true;
            this.cbAlwaysRender.Visible = false;
            this.cbAlwaysRender.CheckedChanged += new System.EventHandler(this.cbAlwaysRender_CheckedChanged);
            // 
            // lblFPS
            // 
            this.lblFPS.AutoSize = true;
            this.lblFPS.Location = new System.Drawing.Point(12, 8);
            this.lblFPS.Name = "lblFPS";
            this.lblFPS.Size = new System.Drawing.Size(39, 13);
            this.lblFPS.TabIndex = 4;
            this.lblFPS.Text = "FPS: 0";
            // 
            // tbZoom
            // 
            this.tbZoom.Location = new System.Drawing.Point(140, 814);
            this.tbZoom.Maximum = 30;
            this.tbZoom.Minimum = 1;
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(320, 45);
            this.tbZoom.TabIndex = 5;
            this.tbZoom.Value = 10;
            this.tbZoom.Scroll += new System.EventHandler(this.tbZoom_Scroll);
            // 
            // flowRegions
            // 
            this.flowRegions.AutoScroll = true;
            this.flowRegions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowRegions.Location = new System.Drawing.Point(971, 8);
            this.flowRegions.Name = "flowRegions";
            this.flowRegions.Size = new System.Drawing.Size(241, 814);
            this.flowRegions.TabIndex = 6;
            // 
            // btnAddRegion
            // 
            this.btnAddRegion.Location = new System.Drawing.Point(971, 829);
            this.btnAddRegion.Name = "btnAddRegion";
            this.btnAddRegion.Size = new System.Drawing.Size(241, 23);
            this.btnAddRegion.TabIndex = 7;
            this.btnAddRegion.Text = "Add Region";
            this.btnAddRegion.UseVisualStyleBackColor = true;
            this.btnAddRegion.Click += new System.EventHandler(this.btnAddRegion_Click);
            // 
            // pbMain
            // 
            this.pbMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMain.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pbMain.Location = new System.Drawing.Point(140, 8);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(800, 800);
            this.pbMain.TabIndex = 0;
            this.pbMain.TabStop = false;
            this.pbMain.Click += new System.EventHandler(this.pbMain_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(3, 829);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnLoadFrom
            // 
            this.btnLoadFrom.Location = new System.Drawing.Point(3, 799);
            this.btnLoadFrom.Name = "btnLoadFrom";
            this.btnLoadFrom.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFrom.TabIndex = 9;
            this.btnLoadFrom.Text = "Load";
            this.btnLoadFrom.UseVisualStyleBackColor = true;
            this.btnLoadFrom.Click += new System.EventHandler(this.btnLoadFrom_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 863);
            this.Controls.Add(this.btnLoadFrom);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnAddRegion);
            this.Controls.Add(this.flowRegions);
            this.Controls.Add(this.tbZoom);
            this.Controls.Add(this.lblFPS);
            this.Controls.Add(this.cbAlwaysRender);
            this.Controls.Add(this.pbMain);
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.CheckBox cbAlwaysRender;
        private System.Windows.Forms.Label lblFPS;
        private System.Windows.Forms.TrackBar tbZoom;
        private System.Windows.Forms.FlowLayoutPanel flowRegions;
        private System.Windows.Forms.Button btnAddRegion;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnLoadFrom;
    }
}

