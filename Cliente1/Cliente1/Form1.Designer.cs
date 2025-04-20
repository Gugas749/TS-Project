namespace Cliente1
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxUsernameAuth = new System.Windows.Forms.TextBox();
            this.butLoginAuth = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPasswordAuth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbServerResponse = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listViewDirect = new System.Windows.Forms.ListView();
            this.txtBoxMSGToSend = new System.Windows.Forms.TextBox();
            this.butSendMSG = new System.Windows.Forms.Button();
            this.txtBoxDestination = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUsernameAuth
            // 
            this.textBoxUsernameAuth.Location = new System.Drawing.Point(32, 67);
            this.textBoxUsernameAuth.Name = "textBoxUsernameAuth";
            this.textBoxUsernameAuth.Size = new System.Drawing.Size(195, 22);
            this.textBoxUsernameAuth.TabIndex = 0;
            // 
            // butLoginAuth
            // 
            this.butLoginAuth.Location = new System.Drawing.Point(32, 150);
            this.butLoginAuth.Name = "butLoginAuth";
            this.butLoginAuth.Size = new System.Drawing.Size(195, 25);
            this.butLoginAuth.TabIndex = 1;
            this.butLoginAuth.Text = "Login";
            this.butLoginAuth.UseVisualStyleBackColor = true;
            this.butLoginAuth.Click += new System.EventHandler(this.butLoginAuth_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // textBoxPasswordAuth
            // 
            this.textBoxPasswordAuth.Location = new System.Drawing.Point(32, 122);
            this.textBoxPasswordAuth.Name = "textBoxPasswordAuth";
            this.textBoxPasswordAuth.Size = new System.Drawing.Size(195, 22);
            this.textBoxPasswordAuth.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxUsernameAuth);
            this.groupBox1.Controls.Add(this.butLoginAuth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxPasswordAuth);
            this.groupBox1.Location = new System.Drawing.Point(12, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 366);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auth";
            // 
            // lbServerResponse
            // 
            this.lbServerResponse.AutoSize = true;
            this.lbServerResponse.Location = new System.Drawing.Point(12, 9);
            this.lbServerResponse.Name = "lbServerResponse";
            this.lbServerResponse.Size = new System.Drawing.Size(142, 16);
            this.lbServerResponse.TabIndex = 6;
            this.lbServerResponse.Text = "Resposta do Servidor:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(588, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Conexão com o servidor: ";
            // 
            // listViewDirect
            // 
            this.listViewDirect.HideSelection = false;
            this.listViewDirect.Location = new System.Drawing.Point(322, 72);
            this.listViewDirect.Name = "listViewDirect";
            this.listViewDirect.Size = new System.Drawing.Size(314, 271);
            this.listViewDirect.TabIndex = 9;
            this.listViewDirect.UseCompatibleStateImageBehavior = false;
            // 
            // txtBoxMSGToSend
            // 
            this.txtBoxMSGToSend.Location = new System.Drawing.Point(322, 349);
            this.txtBoxMSGToSend.Name = "txtBoxMSGToSend";
            this.txtBoxMSGToSend.Size = new System.Drawing.Size(206, 22);
            this.txtBoxMSGToSend.TabIndex = 5;
            // 
            // butSendMSG
            // 
            this.butSendMSG.Location = new System.Drawing.Point(534, 349);
            this.butSendMSG.Name = "butSendMSG";
            this.butSendMSG.Size = new System.Drawing.Size(102, 25);
            this.butSendMSG.TabIndex = 5;
            this.butSendMSG.Text = "Send";
            this.butSendMSG.UseVisualStyleBackColor = true;
            this.butSendMSG.Click += new System.EventHandler(this.butSendMSG_Click);
            // 
            // txtBoxDestination
            // 
            this.txtBoxDestination.Location = new System.Drawing.Point(673, 72);
            this.txtBoxDestination.Name = "txtBoxDestination";
            this.txtBoxDestination.Size = new System.Drawing.Size(95, 22);
            this.txtBoxDestination.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtBoxDestination);
            this.Controls.Add(this.butSendMSG);
            this.Controls.Add(this.txtBoxMSGToSend);
            this.Controls.Add(this.listViewDirect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbServerResponse);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUsernameAuth;
        private System.Windows.Forms.Button butLoginAuth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPasswordAuth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listViewDirect;
        private System.Windows.Forms.TextBox txtBoxMSGToSend;
        private System.Windows.Forms.Button butSendMSG;
        private System.Windows.Forms.TextBox txtBoxDestination;
        public System.Windows.Forms.Label lbServerResponse;
    }
}

