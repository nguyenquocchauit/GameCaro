
namespace GameCaro_Lan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pn_GameBoard = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pb_Logo = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pb_Avatar = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.btn_LAN = new System.Windows.Forms.Button();
            this.btn_Undo = new System.Windows.Forms.Button();
            this.prcbCoolDown = new System.Windows.Forms.ProgressBar();
            this.txt_PlayerName = new System.Windows.Forms.TextBox();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.tmCoolDown = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Logo)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Avatar)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pn_GameBoard
            // 
            this.pn_GameBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pn_GameBoard.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pn_GameBoard.Location = new System.Drawing.Point(12, 30);
            this.pn_GameBoard.Name = "pn_GameBoard";
            this.pn_GameBoard.Size = new System.Drawing.Size(656, 811);
            this.pn_GameBoard.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.pb_Logo);
            this.panel2.Location = new System.Drawing.Point(693, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(251, 208);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(0, 255);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(319, 249);
            this.panel3.TabIndex = 2;
            // 
            // pb_Logo
            // 
            this.pb_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_Logo.Image = ((System.Drawing.Image)(resources.GetObject("pb_Logo.Image")));
            this.pb_Logo.Location = new System.Drawing.Point(0, -1);
            this.pb_Logo.Name = "pb_Logo";
            this.pb_Logo.Size = new System.Drawing.Size(247, 206);
            this.pb_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_Logo.TabIndex = 3;
            this.pb_Logo.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.pb_Avatar);
            this.panel4.Location = new System.Drawing.Point(797, 493);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(147, 145);
            this.panel4.TabIndex = 2;
            // 
            // pb_Avatar
            // 
            this.pb_Avatar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_Avatar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_Avatar.Location = new System.Drawing.Point(2, 0);
            this.pb_Avatar.Name = "pb_Avatar";
            this.pb_Avatar.Size = new System.Drawing.Size(143, 142);
            this.pb_Avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_Avatar.TabIndex = 0;
            this.pb_Avatar.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.pictureBox2);
            this.panel5.Location = new System.Drawing.Point(693, 255);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(251, 208);
            this.panel5.TabIndex = 3;
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(0, 255);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(319, 249);
            this.panel6.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, -1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(247, 206);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // txt_IP
            // 
            this.txt_IP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_IP.Location = new System.Drawing.Point(693, 810);
            this.txt_IP.Multiline = true;
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(251, 31);
            this.txt_IP.TabIndex = 4;
            this.txt_IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_LAN
            // 
            this.btn_LAN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LAN.BackColor = System.Drawing.Color.SpringGreen;
            this.btn_LAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LAN.Location = new System.Drawing.Point(694, 755);
            this.btn_LAN.Name = "btn_LAN";
            this.btn_LAN.Size = new System.Drawing.Size(248, 49);
            this.btn_LAN.TabIndex = 5;
            this.btn_LAN.Text = "Connect";
            this.btn_LAN.UseVisualStyleBackColor = false;
            this.btn_LAN.Click += new System.EventHandler(this.btn_LAN_Click);
            // 
            // btn_Undo
            // 
            this.btn_Undo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Undo.BackColor = System.Drawing.Color.SpringGreen;
            this.btn_Undo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Undo.Location = new System.Drawing.Point(696, 540);
            this.btn_Undo.Name = "btn_Undo";
            this.btn_Undo.Size = new System.Drawing.Size(102, 49);
            this.btn_Undo.TabIndex = 6;
            this.btn_Undo.Text = "Undo";
            this.btn_Undo.UseVisualStyleBackColor = false;
            this.btn_Undo.Click += new System.EventHandler(this.btn_Undo_Click);
            // 
            // prcbCoolDown
            // 
            this.prcbCoolDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prcbCoolDown.Location = new System.Drawing.Point(693, 704);
            this.prcbCoolDown.Name = "prcbCoolDown";
            this.prcbCoolDown.Size = new System.Drawing.Size(249, 41);
            this.prcbCoolDown.TabIndex = 7;
            // 
            // txt_PlayerName
            // 
            this.txt_PlayerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_PlayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PlayerName.Location = new System.Drawing.Point(693, 652);
            this.txt_PlayerName.Multiline = true;
            this.txt_PlayerName.Name = "txt_PlayerName";
            this.txt_PlayerName.Size = new System.Drawing.Size(251, 38);
            this.txt_PlayerName.TabIndex = 8;
            this.txt_PlayerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnNewGame
            // 
            this.btnNewGame.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewGame.Location = new System.Drawing.Point(11, 1);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(115, 27);
            this.btnNewGame.TabIndex = 9;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = false;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuit.Location = new System.Drawing.Point(125, 1);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(102, 27);
            this.btnQuit.TabIndex = 10;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // tmCoolDown
            // 
            this.tmCoolDown.Tick += new System.EventHandler(this.tmCoolDown_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 853);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.txt_PlayerName);
            this.Controls.Add(this.prcbCoolDown);
            this.Controls.Add(this.btn_Undo);
            this.Controls.Add(this.btn_LAN);
            this.Controls.Add(this.txt_IP);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pn_GameBoard);
            this.MaximumSize = new System.Drawing.Size(1900, 900);
            this.MinimumSize = new System.Drawing.Size(972, 900);
            this.Name = "Form1";
            this.Text = "Game Caro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Logo)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Avatar)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pn_GameBoard;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.Button btn_LAN;
        private System.Windows.Forms.Button btn_Undo;
        private System.Windows.Forms.ProgressBar prcbCoolDown;
        private System.Windows.Forms.TextBox txt_PlayerName;
        private System.Windows.Forms.PictureBox pb_Logo;
        private System.Windows.Forms.PictureBox pb_Avatar;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Timer tmCoolDown;
    }
}

