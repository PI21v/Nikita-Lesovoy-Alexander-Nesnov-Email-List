namespace EmailList
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
            components = new System.ComponentModel.Container();
            materialFlatButton1 = new MaterialSkin.Controls.MaterialFlatButton();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            materialFlatButton2 = new MaterialSkin.Controls.MaterialFlatButton();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            toolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // materialFlatButton1
            // 
            materialFlatButton1.AutoSize = true;
            materialFlatButton1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialFlatButton1.Depth = 0;
            materialFlatButton1.Location = new Point(286, 266);
            materialFlatButton1.Margin = new Padding(4, 6, 4, 6);
            materialFlatButton1.MouseState = MaterialSkin.MouseState.HOVER;
            materialFlatButton1.Name = "materialFlatButton1";
            materialFlatButton1.Primary = false;
            materialFlatButton1.Size = new Size(112, 36);
            materialFlatButton1.TabIndex = 0;
            materialFlatButton1.Text = "Авторизация";
            materialFlatButton1.UseVisualStyleBackColor = true;
            materialFlatButton1.Click += materialFlatButton1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(286, 176);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(232, 23);
            textBox1.TabIndex = 1;
            textBox1.KeyDown += textBox1_KeyDown;
            textBox1.KeyUp += textBox1_KeyUp;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(286, 234);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(232, 23);
            textBox2.TabIndex = 2;
            textBox2.KeyDown += textBox2_KeyDown;
            textBox2.KeyUp += textBox2_KeyUp;
            // 
            // materialFlatButton2
            // 
            materialFlatButton2.AutoSize = true;
            materialFlatButton2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialFlatButton2.Depth = 0;
            materialFlatButton2.Location = new Point(547, 266);
            materialFlatButton2.Margin = new Padding(4, 6, 4, 6);
            materialFlatButton2.MouseState = MaterialSkin.MouseState.HOVER;
            materialFlatButton2.Name = "materialFlatButton2";
            materialFlatButton2.Primary = false;
            materialFlatButton2.Size = new Size(110, 36);
            materialFlatButton2.TabIndex = 3;
            materialFlatButton2.Text = "Регистрация";
            materialFlatButton2.TextAlign = ContentAlignment.MiddleRight;
            materialFlatButton2.UseVisualStyleBackColor = true;
            materialFlatButton2.Click += materialFlatButton2_Click;
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 11F, FontStyle.Regular, GraphicsUnit.Point);
            materialLabel1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialLabel1.Location = new Point(218, 176);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(53, 19);
            materialLabel1.TabIndex = 4;
            materialLabel1.Text = "Логин";
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 11F, FontStyle.Regular, GraphicsUnit.Point);
            materialLabel2.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialLabel2.Location = new Point(218, 234);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(62, 19);
            materialLabel2.TabIndex = 5;
            materialLabel2.Text = "Пароль";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(materialLabel2);
            Controls.Add(materialLabel1);
            Controls.Add(materialFlatButton2);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(materialFlatButton1);
            Name = "Form1";
            Text = "Login";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton1;
        private TextBox textBox1;
        private TextBox textBox2;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private ToolTip toolTip1;
    }
}