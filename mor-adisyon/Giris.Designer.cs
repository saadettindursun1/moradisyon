namespace mor_adisyon
{
    partial class Giris
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
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.parolaText = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.kullaniciText = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Poppins", 18F);
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(92, 219);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(105, 42);
            this.label27.TabIndex = 14;
            this.label27.Text = "Parola :";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Poppins", 18F);
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(25, 138);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(172, 42);
            this.label26.TabIndex = 13;
            this.label26.Text = "Kullanıcı Adı :";
            // 
            // parolaText
            // 
            this.parolaText.Font = new System.Drawing.Font("Poppins", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.parolaText.Location = new System.Drawing.Point(221, 213);
            this.parolaText.Name = "parolaText";
            this.parolaText.PasswordChar = '*';
            this.parolaText.Size = new System.Drawing.Size(355, 48);
            this.parolaText.TabIndex = 2;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Poppins", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(221, 287);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(355, 45);
            this.button5.TabIndex = 3;
            this.button5.Text = "GİRİŞ";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // kullaniciText
            // 
            this.kullaniciText.Font = new System.Drawing.Font("Poppins", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.kullaniciText.Location = new System.Drawing.Point(221, 132);
            this.kullaniciText.Name = "kullaniciText";
            this.kullaniciText.Size = new System.Drawing.Size(355, 48);
            this.kullaniciText.TabIndex = 1;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Poppins", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(290, 48);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(218, 56);
            this.label25.TabIndex = 9;
            this.label25.Text = "GİRİŞ EKRANI";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.button4.Font = new System.Drawing.Font("Poppins", 12F);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(679, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 48);
            this.button4.TabIndex = 15;
            this.button4.Text = "Kapat";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Giris
            // 
            this.AcceptButton = this.button5;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.ClientSize = new System.Drawing.Size(800, 404);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.parolaText);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.kullaniciText);
            this.Controls.Add(this.label25);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Giris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giris";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox parolaText;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox kullaniciText;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button button4;
    }
}