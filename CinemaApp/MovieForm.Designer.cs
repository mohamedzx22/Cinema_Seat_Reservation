namespace CinemaApp

{

    partial class MovieForm

    {

        /// <summary>

        /// Required designer variable.

        /// </summary>

        private System.ComponentModel.IContainer components = null;



        // تعريف الـ FlowLayoutPanel

        private System.Windows.Forms.FlowLayoutPanel flowPanel;



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

        /// Required method for Designer support – do not modify

        /// the contents of this method with the code editor.

        /// </summary>

        private void InitializeComponent()

        {

            this.components = new System.ComponentModel.Container();

            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();

            this.SuspendLayout();

            // 

            // flowPanel

            // 

            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            this.flowPanel.AutoScroll = true;

            this.flowPanel.Location = new System.Drawing.Point(0, 0);

            this.flowPanel.Name = "flowPanel";

            this.flowPanel.Size = new System.Drawing.Size(1646, 902);

            this.flowPanel.TabIndex = 0;

            // 

            // MovieForm

            // 

            this.ClientSize = new System.Drawing.Size(1646, 902);

            this.Controls.Add(this.flowPanel);

            this.Name = "MovieForm";

            this.Text = "Available Movies";

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.ResumeLayout(false);

        }



        #endregion

    }

}

