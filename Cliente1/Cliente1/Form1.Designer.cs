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
            this.components = new System.ComponentModel.Container();
            this.listViewDirect = new System.Windows.Forms.ListView();
            this.butLoginAuth = new Guna.UI2.WinForms.Guna2Button();
            this.guna2VSeparator1 = new Guna.UI2.WinForms.Guna2VSeparator();
            this.textBoxUsernameAuth = new Guna.UI2.WinForms.Guna2TextBox();
            this.textBoxPasswordAuth = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.lbServerResponse = new Guna.UI2.WinForms.Guna2TextBox();
            this.lable5 = new System.Windows.Forms.Label();
            this.butSendMSG = new Guna.UI2.WinForms.Guna2Button();
            this.txtBoxMSGToSend = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtBoxDestination = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butConnectToServer = new Guna.UI2.WinForms.Guna2Button();
            this.lbConnectionState = new System.Windows.Forms.Label();
            this.listViewOnlineUsers = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listViewDirect
            // 
            this.listViewDirect.FullRowSelect = true;
            this.listViewDirect.HideSelection = false;
            this.listViewDirect.Location = new System.Drawing.Point(548, 71);
            this.listViewDirect.Name = "listViewDirect";
            this.listViewDirect.Size = new System.Drawing.Size(475, 346);
            this.listViewDirect.TabIndex = 9;
            this.listViewDirect.UseCompatibleStateImageBehavior = false;
            this.listViewDirect.View = System.Windows.Forms.View.Details;
            // 
            // butLoginAuth
            // 
            this.butLoginAuth.BorderRadius = 7;
            this.butLoginAuth.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.butLoginAuth.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.butLoginAuth.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.butLoginAuth.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.butLoginAuth.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.butLoginAuth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.butLoginAuth.ForeColor = System.Drawing.Color.White;
            this.butLoginAuth.IndicateFocus = true;
            this.butLoginAuth.Location = new System.Drawing.Point(152, 376);
            this.butLoginAuth.Name = "butLoginAuth";
            this.butLoginAuth.Size = new System.Drawing.Size(180, 45);
            this.butLoginAuth.TabIndex = 16;
            this.butLoginAuth.Text = "Entrar";
            this.butLoginAuth.Click += new System.EventHandler(this.butLoginAuth_Click);
            // 
            // guna2VSeparator1
            // 
            this.guna2VSeparator1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.guna2VSeparator1.Location = new System.Drawing.Point(532, 13);
            this.guna2VSeparator1.Name = "guna2VSeparator1";
            this.guna2VSeparator1.Size = new System.Drawing.Size(10, 447);
            this.guna2VSeparator1.TabIndex = 17;
            // 
            // textBoxUsernameAuth
            // 
            this.textBoxUsernameAuth.Animated = true;
            this.textBoxUsernameAuth.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.textBoxUsernameAuth.BorderRadius = 7;
            this.textBoxUsernameAuth.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxUsernameAuth.DefaultText = "";
            this.textBoxUsernameAuth.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxUsernameAuth.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxUsernameAuth.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxUsernameAuth.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxUsernameAuth.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxUsernameAuth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxUsernameAuth.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxUsernameAuth.Location = new System.Drawing.Point(96, 228);
            this.textBoxUsernameAuth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxUsernameAuth.Name = "textBoxUsernameAuth";
            this.textBoxUsernameAuth.PlaceholderText = "Email";
            this.textBoxUsernameAuth.SelectedText = "";
            this.textBoxUsernameAuth.Size = new System.Drawing.Size(309, 48);
            this.textBoxUsernameAuth.TabIndex = 18;
            // 
            // textBoxPasswordAuth
            // 
            this.textBoxPasswordAuth.Animated = true;
            this.textBoxPasswordAuth.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.textBoxPasswordAuth.BorderRadius = 7;
            this.textBoxPasswordAuth.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxPasswordAuth.DefaultText = "";
            this.textBoxPasswordAuth.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxPasswordAuth.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxPasswordAuth.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxPasswordAuth.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxPasswordAuth.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxPasswordAuth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxPasswordAuth.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBoxPasswordAuth.Location = new System.Drawing.Point(96, 297);
            this.textBoxPasswordAuth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxPasswordAuth.Name = "textBoxPasswordAuth";
            this.textBoxPasswordAuth.PasswordChar = '*';
            this.textBoxPasswordAuth.PlaceholderText = "Password";
            this.textBoxPasswordAuth.SelectedText = "";
            this.textBoxPasswordAuth.Size = new System.Drawing.Size(309, 48);
            this.textBoxPasswordAuth.TabIndex = 19;
            // 
            // lbServerResponse
            // 
            this.lbServerResponse.Animated = true;
            this.lbServerResponse.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.lbServerResponse.BorderRadius = 7;
            this.lbServerResponse.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lbServerResponse.DefaultText = "";
            this.lbServerResponse.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lbServerResponse.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.lbServerResponse.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lbServerResponse.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lbServerResponse.Enabled = false;
            this.lbServerResponse.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(240)))), ((int)(((byte)(244)))));
            this.lbServerResponse.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lbServerResponse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbServerResponse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.lbServerResponse.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lbServerResponse.Location = new System.Drawing.Point(12, 13);
            this.lbServerResponse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbServerResponse.Name = "lbServerResponse";
            this.lbServerResponse.PlaceholderText = "";
            this.lbServerResponse.SelectedText = "";
            this.lbServerResponse.Size = new System.Drawing.Size(514, 48);
            this.lbServerResponse.TabIndex = 20;
            // 
            // lable5
            // 
            this.lable5.AutoSize = true;
            this.lable5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lable5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.lable5.Location = new System.Drawing.Point(133, 173);
            this.lable5.Name = "lable5";
            this.lable5.Size = new System.Drawing.Size(241, 32);
            this.lable5.TabIndex = 21;
            this.lable5.Text = "Autenticaticação";
            // 
            // butSendMSG
            // 
            this.butSendMSG.BorderRadius = 7;
            this.butSendMSG.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.butSendMSG.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.butSendMSG.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.butSendMSG.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.butSendMSG.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.butSendMSG.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.butSendMSG.ForeColor = System.Drawing.Color.White;
            this.butSendMSG.IndicateFocus = true;
            this.butSendMSG.Location = new System.Drawing.Point(889, 423);
            this.butSendMSG.Name = "butSendMSG";
            this.butSendMSG.Size = new System.Drawing.Size(134, 49);
            this.butSendMSG.TabIndex = 22;
            this.butSendMSG.Text = "Enviar";
            this.butSendMSG.Click += new System.EventHandler(this.butSendMSG_Click);
            // 
            // txtBoxMSGToSend
            // 
            this.txtBoxMSGToSend.Animated = true;
            this.txtBoxMSGToSend.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.txtBoxMSGToSend.BorderRadius = 7;
            this.txtBoxMSGToSend.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBoxMSGToSend.DefaultText = "";
            this.txtBoxMSGToSend.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBoxMSGToSend.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBoxMSGToSend.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBoxMSGToSend.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBoxMSGToSend.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBoxMSGToSend.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBoxMSGToSend.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBoxMSGToSend.Location = new System.Drawing.Point(548, 423);
            this.txtBoxMSGToSend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBoxMSGToSend.Name = "txtBoxMSGToSend";
            this.txtBoxMSGToSend.PlaceholderText = "";
            this.txtBoxMSGToSend.SelectedText = "";
            this.txtBoxMSGToSend.Size = new System.Drawing.Size(335, 48);
            this.txtBoxMSGToSend.TabIndex = 23;
            // 
            // txtBoxDestination
            // 
            this.txtBoxDestination.Animated = true;
            this.txtBoxDestination.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.txtBoxDestination.BorderRadius = 7;
            this.txtBoxDestination.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBoxDestination.DefaultText = "";
            this.txtBoxDestination.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBoxDestination.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBoxDestination.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBoxDestination.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBoxDestination.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBoxDestination.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBoxDestination.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBoxDestination.Location = new System.Drawing.Point(907, 13);
            this.txtBoxDestination.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBoxDestination.Name = "txtBoxDestination";
            this.txtBoxDestination.PlaceholderText = "";
            this.txtBoxDestination.SelectedText = "";
            this.txtBoxDestination.Size = new System.Drawing.Size(116, 48);
            this.txtBoxDestination.TabIndex = 24;
            this.txtBoxDestination.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxDestination_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.label1.Location = new System.Drawing.Point(778, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 25);
            this.label1.TabIndex = 25;
            this.label1.Text = "Receptor:";
            // 
            // butConnectToServer
            // 
            this.butConnectToServer.BorderRadius = 7;
            this.butConnectToServer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.butConnectToServer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.butConnectToServer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.butConnectToServer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.butConnectToServer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.butConnectToServer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.butConnectToServer.ForeColor = System.Drawing.Color.White;
            this.butConnectToServer.IndicateFocus = true;
            this.butConnectToServer.Location = new System.Drawing.Point(337, 71);
            this.butConnectToServer.Name = "butConnectToServer";
            this.butConnectToServer.Size = new System.Drawing.Size(180, 45);
            this.butConnectToServer.TabIndex = 26;
            this.butConnectToServer.Text = "Connectar ao Servidor";
            this.butConnectToServer.Click += new System.EventHandler(this.butConnectToServer_Click);
            // 
            // lbConnectionState
            // 
            this.lbConnectionState.AutoSize = true;
            this.lbConnectionState.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConnectionState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(94)))), ((int)(((byte)(112)))));
            this.lbConnectionState.Location = new System.Drawing.Point(21, 84);
            this.lbConnectionState.Name = "lbConnectionState";
            this.lbConnectionState.Size = new System.Drawing.Size(278, 25);
            this.lbConnectionState.TabIndex = 27;
            this.lbConnectionState.Text = "Estado da Conexão: Offline";
            // 
            // listViewOnlineUsers
            // 
            this.listViewOnlineUsers.FullRowSelect = true;
            this.listViewOnlineUsers.HideSelection = false;
            this.listViewOnlineUsers.Location = new System.Drawing.Point(1092, 11);
            this.listViewOnlineUsers.Name = "listViewOnlineUsers";
            this.listViewOnlineUsers.Size = new System.Drawing.Size(242, 460);
            this.listViewOnlineUsers.TabIndex = 28;
            this.listViewOnlineUsers.UseCompatibleStateImageBehavior = false;
            this.listViewOnlineUsers.View = System.Windows.Forms.View.Details;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 484);
            this.Controls.Add(this.listViewOnlineUsers);
            this.Controls.Add(this.lbConnectionState);
            this.Controls.Add(this.butConnectToServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxDestination);
            this.Controls.Add(this.txtBoxMSGToSend);
            this.Controls.Add(this.butSendMSG);
            this.Controls.Add(this.lable5);
            this.Controls.Add(this.lbServerResponse);
            this.Controls.Add(this.textBoxPasswordAuth);
            this.Controls.Add(this.textBoxUsernameAuth);
            this.Controls.Add(this.guna2VSeparator1);
            this.Controls.Add(this.butLoginAuth);
            this.Controls.Add(this.listViewDirect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Cliente1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listViewDirect;
        private Guna.UI2.WinForms.Guna2Button butLoginAuth;
        private Guna.UI2.WinForms.Guna2VSeparator guna2VSeparator1;
        private Guna.UI2.WinForms.Guna2TextBox textBoxUsernameAuth;
        private Guna.UI2.WinForms.Guna2TextBox textBoxPasswordAuth;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private Guna.UI2.WinForms.Guna2TextBox lbServerResponse;
        private System.Windows.Forms.Label lable5;
        private Guna.UI2.WinForms.Guna2Button butSendMSG;
        private Guna.UI2.WinForms.Guna2TextBox txtBoxMSGToSend;
        private Guna.UI2.WinForms.Guna2TextBox txtBoxDestination;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button butConnectToServer;
        private System.Windows.Forms.Label lbConnectionState;
        private System.Windows.Forms.ListView listViewOnlineUsers;
    }
}

