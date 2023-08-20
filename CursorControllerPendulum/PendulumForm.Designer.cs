namespace CursorControllerPendulum
{
    partial class PendulumForm
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
            components = new System.ComponentModel.Container();
            PendulumPictureBox = new PictureBox();
            PendulumTimer = new System.Windows.Forms.Timer(components);
            MouseTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)PendulumPictureBox).BeginInit();
            SuspendLayout();
            // 
            // PendulumPictureBox
            // 
            PendulumPictureBox.Location = new Point(0, 0);
            PendulumPictureBox.Name = "PendulumPictureBox";
            PendulumPictureBox.Size = new Size(1280, 720);
            PendulumPictureBox.TabIndex = 0;
            PendulumPictureBox.TabStop = false;
            // 
            // PendulumTimer
            // 
            PendulumTimer.Enabled = true;
            PendulumTimer.Interval = 16;
            PendulumTimer.Tick += PendulumTimerTick;
            // 
            // MouseTimer
            // 
            MouseTimer.Interval = 32;
            MouseTimer.Tick += MouseTimerTick;
            // 
            // PendulumForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 722);
            Controls.Add(PendulumPictureBox);
            Name = "PendulumForm";
            Text = "PendulumForm";
            Load += PendulumFormLoad;
            ((System.ComponentModel.ISupportInitialize)PendulumPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox PendulumPictureBox;
        private System.Windows.Forms.Timer PendulumTimer;
        private System.Windows.Forms.Timer MouseTimer;
    }
}