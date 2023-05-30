namespace EmailList
{
    partial class Regiser
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
            components = new System.ComponentModel.Container();
            materialFlatButton1 = new MaterialSkin.Controls.MaterialFlatButton();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            textBox3 = new TextBox();
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            textBox4 = new TextBox();
            materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            toolTip1 = new ToolTip(components);
            materialCheckBox1 = new MaterialSkin.Controls.MaterialCheckBox();
            SuspendLayout();
            // 
            // materialFlatButton1
            // 
            materialFlatButton1.AutoSize = true;
            materialFlatButton1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialFlatButton1.Depth = 0;
            materialFlatButton1.Location = new Point(247, 350);
            materialFlatButton1.Margin = new Padding(4, 6, 4, 6);
            materialFlatButton1.MouseState = MaterialSkin.MouseState.HOVER;
            materialFlatButton1.Name = "materialFlatButton1";
            materialFlatButton1.Primary = false;
            materialFlatButton1.Size = new Size(168, 36);
            materialFlatButton1.TabIndex = 0;
            materialFlatButton1.Text = "Зарегестрироваться";
            materialFlatButton1.UseVisualStyleBackColor = true;
            materialFlatButton1.Click += materialFlatButton1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(247, 132);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(214, 23);
            textBox1.TabIndex = 1;
            textBox1.KeyUp += textBox1_KeyUp;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(247, 192);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(214, 23);
            textBox2.TabIndex = 2;
            textBox2.KeyUp += textBox2_KeyUp;
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 11F, FontStyle.Regular, GraphicsUnit.Point);
            materialLabel1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialLabel1.Location = new Point(188, 132);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(53, 19);
            materialLabel1.TabIndex = 3;
            materialLabel1.Text = "Логин";
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 11F, FontStyle.Regular, GraphicsUnit.Point);
            materialLabel2.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialLabel2.Location = new Point(179, 259);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(62, 19);
            materialLabel2.TabIndex = 4;
            materialLabel2.Text = "Пароль";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(247, 318);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(214, 23);
            textBox3.TabIndex = 5;
            textBox3.KeyUp += textBox3_KeyUp;
            // 
            // materialLabel3
            // 
            materialLabel3.AutoSize = true;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto", 11F, FontStyle.Regular, GraphicsUnit.Point);
            materialLabel3.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialLabel3.Location = new Point(124, 318);
            materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(117, 19);
            materialLabel3.TabIndex = 6;
            materialLabel3.Text = "Повтор пароля";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(247, 259);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(214, 23);
            textBox4.TabIndex = 7;
            textBox4.KeyUp += textBox4_KeyUp;
            // 
            // materialLabel4
            // 
            materialLabel4.AutoSize = true;
            materialLabel4.Depth = 0;
            materialLabel4.Font = new Font("Roboto", 11F, FontStyle.Regular, GraphicsUnit.Point);
            materialLabel4.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialLabel4.Location = new Point(189, 192);
            materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel4.Name = "materialLabel4";
            materialLabel4.Size = new Size(52, 19);
            materialLabel4.TabIndex = 8;
            materialLabel4.Text = "Почта";
            // 
            // materialCheckBox1
            // 
            materialCheckBox1.AutoSize = true;
            materialCheckBox1.Depth = 0;
            materialCheckBox1.Font = new Font("Roboto", 10F, FontStyle.Regular, GraphicsUnit.Point);
            materialCheckBox1.Location = new Point(474, 350);
            materialCheckBox1.Margin = new Padding(0);
            materialCheckBox1.MouseLocation = new Point(-1, -1);
            materialCheckBox1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCheckBox1.Name = "materialCheckBox1";
            materialCheckBox1.Ripple = true;
            materialCheckBox1.Size = new Size(133, 30);
            materialCheckBox1.TabIndex = 9;
            materialCheckBox1.Text = "Администратор";
            materialCheckBox1.UseVisualStyleBackColor = true;
            // 
            // Regiser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(materialCheckBox1);
            Controls.Add(materialLabel4);
            Controls.Add(textBox4);
            Controls.Add(materialLabel3);
            Controls.Add(textBox3);
            Controls.Add(materialLabel2);
            Controls.Add(materialLabel1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(materialFlatButton1);
            Name = "Regiser";
            Text = "Register";
            Load += Regiser_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton1;
        private TextBox textBox1;
        private TextBox textBox2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private TextBox textBox3;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private TextBox textBox4;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private ToolTip toolTip1;
        private MaterialSkin.Controls.MaterialCheckBox materialCheckBox1;
    }
}