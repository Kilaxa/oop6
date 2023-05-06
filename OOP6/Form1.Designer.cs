namespace OOP6
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
            checkBox1 = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button_circle = new Button();
            button_square = new Button();
            button_triangle = new Button();
            button_section = new Button();
            button5 = new Button();
            button6 = new Button();
            trackBar1 = new TrackBar();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBox1.AutoSize = true;
            checkBox1.FlatStyle = FlatStyle.Flat;
            checkBox1.Location = new Point(729, 12);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(58, 24);
            checkBox1.TabIndex = 2;
            checkBox1.TabStop = false;
            checkBox1.Text = "Cntrl";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(683, 6);
            button1.Name = "button1";
            button1.Size = new Size(35, 35);
            button1.TabIndex = 3;
            button1.TabStop = false;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(640, 6);
            button2.Name = "button2";
            button2.Size = new Size(35, 35);
            button2.TabIndex = 4;
            button2.TabStop = false;
            button2.Text = "-";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(533, 6);
            button3.Name = "button3";
            button3.Size = new Size(100, 35);
            button3.TabIndex = 5;
            button3.TabStop = false;
            button3.Text = "Delete";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Location = new Point(640, 47);
            button4.Name = "button4";
            button4.Size = new Size(100, 35);
            button4.TabIndex = 7;
            button4.TabStop = false;
            button4.Text = "Unselect All";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button_circle
            // 
            button_circle.BackgroundImage = Properties.Resources.free_icon_circle_5886755;
            button_circle.BackgroundImageLayout = ImageLayout.Stretch;
            button_circle.FlatStyle = FlatStyle.Flat;
            button_circle.Location = new Point(12, 12);
            button_circle.Name = "button_circle";
            button_circle.Size = new Size(50, 50);
            button_circle.TabIndex = 8;
            button_circle.UseVisualStyleBackColor = true;
            button_circle.Click += button_circle_Click;
            // 
            // button_square
            // 
            button_square.BackgroundImage = Properties.Resources.free_icon_square_5888641;
            button_square.BackgroundImageLayout = ImageLayout.Stretch;
            button_square.FlatStyle = FlatStyle.Flat;
            button_square.Location = new Point(68, 12);
            button_square.Name = "button_square";
            button_square.Size = new Size(50, 50);
            button_square.TabIndex = 9;
            button_square.UseVisualStyleBackColor = true;
            button_square.Click += button_square_Click;
            // 
            // button_triangle
            // 
            button_triangle.BackgroundImage = Properties.Resources.free_icon_triangle_5888654;
            button_triangle.BackgroundImageLayout = ImageLayout.Stretch;
            button_triangle.FlatStyle = FlatStyle.Flat;
            button_triangle.Location = new Point(124, 12);
            button_triangle.Name = "button_triangle";
            button_triangle.Size = new Size(50, 50);
            button_triangle.TabIndex = 10;
            button_triangle.UseVisualStyleBackColor = true;
            button_triangle.Click += button_triangle_Click;
            // 
            // button_section
            // 
            button_section.BackgroundImage = Properties.Resources.free_icon_minus_339879;
            button_section.BackgroundImageLayout = ImageLayout.Stretch;
            button_section.FlatStyle = FlatStyle.Flat;
            button_section.Location = new Point(180, 12);
            button_section.Name = "button_section";
            button_section.Size = new Size(50, 50);
            button_section.TabIndex = 11;
            button_section.UseVisualStyleBackColor = true;
            button_section.Click += button_section_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.Red;
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Location = new Point(279, 12);
            button5.Name = "button5";
            button5.Size = new Size(50, 50);
            button5.TabIndex = 12;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Location = new Point(533, 47);
            button6.Name = "button6";
            button6.Size = new Size(100, 35);
            button6.TabIndex = 13;
            button6.TabStop = false;
            button6.Text = "Select All";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // trackBar1
            // 
            trackBar1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            trackBar1.Location = new Point(640, 88);
            trackBar1.Maximum = 100;
            trackBar1.Minimum = 10;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(150, 56);
            trackBar1.TabIndex = 1;
            trackBar1.TabStop = false;
            trackBar1.TickFrequency = 5;
            trackBar1.Value = 20;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // button7
            // 
            button7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Location = new Point(534, 88);
            button7.Name = "button7";
            button7.Size = new Size(100, 35);
            button7.TabIndex = 14;
            button7.TabStop = false;
            button7.Text = "Group";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button8.FlatStyle = FlatStyle.Flat;
            button8.Location = new Point(427, 6);
            button8.Name = "button8";
            button8.Size = new Size(100, 35);
            button8.TabIndex = 15;
            button8.TabStop = false;
            button8.Text = "Save";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button9
            // 
            button9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button9.FlatStyle = FlatStyle.Flat;
            button9.Location = new Point(427, 47);
            button9.Name = "button9";
            button9.Size = new Size(100, 35);
            button9.TabIndex = 16;
            button9.TabStop = false;
            button9.Text = "Load";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button_section);
            Controls.Add(button_triangle);
            Controls.Add(button_square);
            Controls.Add(button_circle);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(checkBox1);
            Controls.Add(trackBar1);
            KeyPreview = true;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            Paint += Form1_Paint;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            MouseClick += Form1_MouseClick;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private CheckBox checkBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button_circle;
        private Button button_square;
        private Button button_triangle;
        private Button button_section;
        private Button button5;
        private Button button6;
        private TrackBar trackBar1;
        private Button button7;
        private Button button8;
        private Button button9;
    }
}