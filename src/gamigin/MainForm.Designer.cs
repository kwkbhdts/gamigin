namespace Gamigin
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openButton = new System.Windows.Forms.Button();
            this.startAndEndButton = new System.Windows.Forms.Button();
            this.targetFilePathTextBox = new System.Windows.Forms.TextBox();
            this.dragAndDropPictureBox = new System.Windows.Forms.PictureBox();
            this.dragAndDropLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dragAndDropPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.Image = global::Gamigin.Properties.Resources.file_open;
            this.openButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.openButton.Location = new System.Drawing.Point(12, 12);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(60, 60);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "open";
            this.openButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.OnOpenButtonClicked);
            // 
            // startAndEndButton
            // 
            this.startAndEndButton.Image = global::Gamigin.Properties.Resources.hourglass_blue;
            this.startAndEndButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.startAndEndButton.Location = new System.Drawing.Point(78, 12);
            this.startAndEndButton.Name = "startAndEndButton";
            this.startAndEndButton.Size = new System.Drawing.Size(60, 60);
            this.startAndEndButton.TabIndex = 1;
            this.startAndEndButton.Text = "start";
            this.startAndEndButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.startAndEndButton.UseVisualStyleBackColor = true;
            this.startAndEndButton.Click += new System.EventHandler(this.OnStartAndEndButtonClicked);
            // 
            // targetFilePathTextBox
            // 
            this.targetFilePathTextBox.Location = new System.Drawing.Point(12, 78);
            this.targetFilePathTextBox.Name = "targetFilePathTextBox";
            this.targetFilePathTextBox.ReadOnly = true;
            this.targetFilePathTextBox.Size = new System.Drawing.Size(225, 23);
            this.targetFilePathTextBox.TabIndex = 2;
            // 
            // dragAndDropPictureBox
            // 
            this.dragAndDropPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dragAndDropPictureBox.Image = global::Gamigin.Properties.Resources.drag_and_drop;
            this.dragAndDropPictureBox.Location = new System.Drawing.Point(174, 18);
            this.dragAndDropPictureBox.Name = "dragAndDropPictureBox";
            this.dragAndDropPictureBox.Size = new System.Drawing.Size(33, 34);
            this.dragAndDropPictureBox.TabIndex = 3;
            this.dragAndDropPictureBox.TabStop = false;
            // 
            // dragAndDropLabel
            // 
            this.dragAndDropLabel.AutoSize = true;
            this.dragAndDropLabel.Location = new System.Drawing.Point(174, 55);
            this.dragAndDropLabel.Name = "dragAndDropLabel";
            this.dragAndDropLabel.Size = new System.Drawing.Size(33, 15);
            this.dragAndDropLabel.TabIndex = 4;
            this.dragAndDropLabel.Text = "D&&D";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 113);
            this.Controls.Add(this.dragAndDropLabel);
            this.Controls.Add(this.dragAndDropPictureBox);
            this.Controls.Add(this.targetFilePathTextBox);
            this.Controls.Add(this.startAndEndButton);
            this.Controls.Add(this.openButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Gamigin";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.dragAndDropPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button openButton;
        private Button startAndEndButton;
        private TextBox targetFilePathTextBox;
        private PictureBox dragAndDropPictureBox;
        private Label dragAndDropLabel;
    }
}