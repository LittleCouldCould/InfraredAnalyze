﻿namespace InfraredAnalyze
{
    partial class FrmSaveVideoConfig
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
            this.pbxTemper = new System.Windows.Forms.PictureBox();
            this.btnChangePath = new System.Windows.Forms.Button();
            this.btnSaveVideo = new System.Windows.Forms.Button();
            this.tbxSaveVideo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTemper)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxTemper
            // 
            this.pbxTemper.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbxTemper.Location = new System.Drawing.Point(178, 12);
            this.pbxTemper.Name = "pbxTemper";
            this.pbxTemper.Size = new System.Drawing.Size(320, 240);
            this.pbxTemper.TabIndex = 9;
            this.pbxTemper.TabStop = false;
            // 
            // btnChangePath
            // 
            this.btnChangePath.Location = new System.Drawing.Point(498, 264);
            this.btnChangePath.Name = "btnChangePath";
            this.btnChangePath.Size = new System.Drawing.Size(75, 23);
            this.btnChangePath.TabIndex = 8;
            this.btnChangePath.Text = "更改路径";
            this.btnChangePath.UseVisualStyleBackColor = true;
            this.btnChangePath.Click += new System.EventHandler(this.btnChangePath_Click);
            // 
            // btnSaveVideo
            // 
            this.btnSaveVideo.Location = new System.Drawing.Point(317, 304);
            this.btnSaveVideo.Name = "btnSaveVideo";
            this.btnSaveVideo.Size = new System.Drawing.Size(75, 23);
            this.btnSaveVideo.TabIndex = 7;
            this.btnSaveVideo.Text = "保存视频";
            this.btnSaveVideo.UseVisualStyleBackColor = true;
            this.btnSaveVideo.Click += new System.EventHandler(this.btnSaveVideo_Click);
            // 
            // tbxSaveVideo
            // 
            this.tbxSaveVideo.Location = new System.Drawing.Point(247, 267);
            this.tbxSaveVideo.Name = "tbxSaveVideo";
            this.tbxSaveVideo.Size = new System.Drawing.Size(208, 21);
            this.tbxSaveVideo.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "保存路径：";
            // 
            // FrmSaveVideoConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 360);
            this.Controls.Add(this.pbxTemper);
            this.Controls.Add(this.btnChangePath);
            this.Controls.Add(this.btnSaveVideo);
            this.Controls.Add(this.tbxSaveVideo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSaveVideoConfig";
            this.Text = "FrmVideoConfig";
            this.Load += new System.EventHandler(this.FrmSaveVideoConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxTemper)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxTemper;
        private System.Windows.Forms.Button btnChangePath;
        private System.Windows.Forms.Button btnSaveVideo;
        private System.Windows.Forms.TextBox tbxSaveVideo;
        private System.Windows.Forms.Label label1;
    }
}