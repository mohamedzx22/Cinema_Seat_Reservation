namespace CinemaApp
{
    partial class Form1
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
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.FlatStyle = FlatStyle.System;
            label1.Font = new Font("Diwani Letter", 20F, FontStyle.Bold, GraphicsUnit.Point, 178);
            label1.ForeColor = SystemColors.ActiveCaption;
            label1.Location = new Point(432, 150);
            label1.Name = "label1";
            label1.Size = new Size(610, 101);
            label1.TabIndex = 1;
            label1.Text = " Cinema App";
            label1.TextAlign = ContentAlignment.TopCenter;
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Black;
            label2.Location = new Point(476, 276);
            label2.Name = "label2";
            label2.Size = new Size(586, 30);
            label2.TabIndex = 2;
            label2.Text = "Welcome to Cinema App – Your Movie Adventure Starts Here!";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.GradientActiveCaption;
            button1.ForeColor = Color.Black;
            button1.Location = new Point(621, 362);
            button1.Name = "button1";
            button1.Size = new Size(232, 66);
            button1.TabIndex = 3;
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.GradientActiveCaption;
            button2.ForeColor = Color.Black;
            button2.Location = new Point(621, 467);
            button2.Name = "button2";
            button2.Size = new Size(232, 66);
            button2.TabIndex = 4;
            button2.Text = "Register";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1622, 838);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            ForeColor = Color.Coral;
            Name = "Form1";
            Text = "Cinema App";
            ResumeLayout(false);
            PerformLayout();

        }



        #endregion

        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
    }
}
