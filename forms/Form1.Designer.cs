
namespace Framework_Aplicacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDiretorioProjetoConvertido = new System.Windows.Forms.Button();
            this.txtDiretorioProjetoConvertido = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConverter = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDiretorioProjetoLegado = new System.Windows.Forms.Button();
            this.txtDiretorioProjetoLegado = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnConverter);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 291);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnDiretorioProjetoConvertido);
            this.groupBox3.Controls.Add(this.txtDiretorioProjetoConvertido);
            this.groupBox3.Location = new System.Drawing.Point(6, 151);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(745, 55);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Diretório do projeto convertido";
            // 
            // btnDiretorioProjetoConvertido
            // 
            this.btnDiretorioProjetoConvertido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiretorioProjetoConvertido.Image = ((System.Drawing.Image)(resources.GetObject("btnDiretorioProjetoConvertido.Image")));
            this.btnDiretorioProjetoConvertido.Location = new System.Drawing.Point(709, 18);
            this.btnDiretorioProjetoConvertido.Name = "btnDiretorioProjetoConvertido";
            this.btnDiretorioProjetoConvertido.Size = new System.Drawing.Size(30, 24);
            this.btnDiretorioProjetoConvertido.TabIndex = 2;
            this.btnDiretorioProjetoConvertido.UseVisualStyleBackColor = true;
            this.btnDiretorioProjetoConvertido.Click += new System.EventHandler(this.btnDiretorioProjetoConvertido_Click);
            // 
            // txtDiretorioProjetoConvertido
            // 
            this.txtDiretorioProjetoConvertido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiretorioProjetoConvertido.Location = new System.Drawing.Point(10, 20);
            this.txtDiretorioProjetoConvertido.Name = "txtDiretorioProjetoConvertido";
            this.txtDiretorioProjetoConvertido.ReadOnly = true;
            this.txtDiretorioProjetoConvertido.Size = new System.Drawing.Size(695, 20);
            this.txtDiretorioProjetoConvertido.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(735, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Este framework tem como objetivo a conversão de projetos WinForms C# para ASP.NET" +
    " Core 5.0 MVC";
            // 
            // btnConverter
            // 
            this.btnConverter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConverter.Location = new System.Drawing.Point(635, 242);
            this.btnConverter.Name = "btnConverter";
            this.btnConverter.Size = new System.Drawing.Size(115, 43);
            this.btnConverter.TabIndex = 3;
            this.btnConverter.Text = "Converter";
            this.btnConverter.UseVisualStyleBackColor = true;
            this.btnConverter.Click += new System.EventHandler(this.btnConverter_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnDiretorioProjetoLegado);
            this.groupBox2.Controls.Add(this.txtDiretorioProjetoLegado);
            this.groupBox2.Location = new System.Drawing.Point(6, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(745, 55);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Diretório do projeto legado";
            // 
            // btnDiretorioProjetoLegado
            // 
            this.btnDiretorioProjetoLegado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiretorioProjetoLegado.Image = ((System.Drawing.Image)(resources.GetObject("btnDiretorioProjetoLegado.Image")));
            this.btnDiretorioProjetoLegado.Location = new System.Drawing.Point(709, 18);
            this.btnDiretorioProjetoLegado.Name = "btnDiretorioProjetoLegado";
            this.btnDiretorioProjetoLegado.Size = new System.Drawing.Size(30, 24);
            this.btnDiretorioProjetoLegado.TabIndex = 2;
            this.btnDiretorioProjetoLegado.UseVisualStyleBackColor = true;
            this.btnDiretorioProjetoLegado.Click += new System.EventHandler(this.btnDiretorioProjetoLegado_Click);
            // 
            // txtDiretorioProjetoLegado
            // 
            this.txtDiretorioProjetoLegado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiretorioProjetoLegado.Location = new System.Drawing.Point(10, 20);
            this.txtDiretorioProjetoLegado.Name = "txtDiretorioProjetoLegado";
            this.txtDiretorioProjetoLegado.ReadOnly = true;
            this.txtDiretorioProjetoLegado.Size = new System.Drawing.Size(695, 20);
            this.txtDiretorioProjetoLegado.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(204, 252);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(348, 23);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Conversão em andamento, aguarde . . .";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 302);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Framework - Conversão WinForms C# para ASP.NET Core 5.0 MVC";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDiretorioProjetoLegado;
        private System.Windows.Forms.TextBox txtDiretorioProjetoLegado;
        private System.Windows.Forms.Button btnConverter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDiretorioProjetoConvertido;
        private System.Windows.Forms.TextBox txtDiretorioProjetoConvertido;
        private System.Windows.Forms.Label lblStatus;
    }
}

