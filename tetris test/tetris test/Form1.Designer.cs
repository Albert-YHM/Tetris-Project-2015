namespace WindowsFormsApplication1
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
            this.components = new System.ComponentModel.Container();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.pBox1 = new System.Windows.Forms.PictureBox();
            this.pBox2 = new System.Windows.Forms.PictureBox();
            this.lblControls = new System.Windows.Forms.Label();
            this.btnVolUp = new System.Windows.Forms.Button();
            this.btnVolDown = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblHighScore = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Interval = 500;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // pBox1
            // 
            this.pBox1.Location = new System.Drawing.Point(12, 12);
            this.pBox1.Name = "pBox1";
            this.pBox1.Size = new System.Drawing.Size(251, 501);
            this.pBox1.TabIndex = 4;
            this.pBox1.TabStop = false;
            this.pBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pBox1_Paint);
            // 
            // pBox2
            // 
            this.pBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pBox2.Location = new System.Drawing.Point(275, 12);
            this.pBox2.Name = "pBox2";
            this.pBox2.Size = new System.Drawing.Size(126, 55);
            this.pBox2.TabIndex = 6;
            this.pBox2.TabStop = false;
            this.pBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pBox2_Paint);
            // 
            // lblControls
            // 
            this.lblControls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblControls.Location = new System.Drawing.Point(275, 189);
            this.lblControls.Name = "lblControls";
            this.lblControls.Size = new System.Drawing.Size(126, 124);
            this.lblControls.TabIndex = 0;
            this.lblControls.Text = "Controls\r\nStart New Game =          \r\nLeft Click Board\r\nRotate = W\r\nLeft = A\r\nRig" +
                "ht = D\r\nDown = S\r\nChange Songs =\r\n1,2,3,4,5,6,7,8,9";
            // 
            // btnVolUp
            // 
            this.btnVolUp.Location = new System.Drawing.Point(275, 322);
            this.btnVolUp.Name = "btnVolUp";
            this.btnVolUp.Size = new System.Drawing.Size(126, 23);
            this.btnVolUp.TabIndex = 1;
            this.btnVolUp.Text = "Volume Up";
            this.btnVolUp.UseVisualStyleBackColor = true;
            this.btnVolUp.Click += new System.EventHandler(this.btnVolUp_Click);
            // 
            // btnVolDown
            // 
            this.btnVolDown.Location = new System.Drawing.Point(275, 351);
            this.btnVolDown.Name = "btnVolDown";
            this.btnVolDown.Size = new System.Drawing.Size(126, 23);
            this.btnVolDown.TabIndex = 2;
            this.btnVolDown.Text = "Volume Down";
            this.btnVolDown.UseVisualStyleBackColor = true;
            this.btnVolDown.Click += new System.EventHandler(this.btnVolDown_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTitle.Location = new System.Drawing.Point(275, 88);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(126, 15);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "High Score";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHighScore
            // 
            this.lblHighScore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHighScore.Location = new System.Drawing.Point(275, 112);
            this.lblHighScore.Name = "lblHighScore";
            this.lblHighScore.Size = new System.Drawing.Size(126, 15);
            this.lblHighScore.TabIndex = 0;
            this.lblHighScore.Text = "0";
            // 
            // lblScore
            // 
            this.lblScore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblScore.Location = new System.Drawing.Point(275, 163);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(126, 15);
            this.lblScore.TabIndex = 0;
            this.lblScore.Text = "0";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(275, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Score";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnStart
            // 
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStart.Location = new System.Drawing.Point(275, 385);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(126, 128);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start New Game";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 537);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblHighScore);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnVolDown);
            this.Controls.Add(this.btnVolUp);
            this.Controls.Add(this.lblControls);
            this.Controls.Add(this.pBox2);
            this.Controls.Add(this.pBox1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Tetris";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.pBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.PictureBox pBox1;
        private System.Windows.Forms.PictureBox pBox2;
        private System.Windows.Forms.Label lblControls;
        private System.Windows.Forms.Button btnVolUp;
        private System.Windows.Forms.Button btnVolDown;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblHighScore;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStart;


    }
}

