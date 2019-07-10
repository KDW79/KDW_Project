namespace IpSetting_v2
{
    partial class FrmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblPresentIp = new System.Windows.Forms.Label();
            this.BtnGetIp = new System.Windows.Forms.Button();
            this.BtnSetIpTSCAN2 = new System.Windows.Forms.Button();
            this.BtnSetDHCP = new System.Windows.Forms.Button();
            this.BtnPingTest = new System.Windows.Forms.Button();
            this.TxtLog = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CboNetAdapts = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblPresentIp
            // 
            this.LblPresentIp.AutoSize = true;
            this.LblPresentIp.Location = new System.Drawing.Point(187, 19);
            this.LblPresentIp.Name = "LblPresentIp";
            this.LblPresentIp.Size = new System.Drawing.Size(40, 12);
            this.LblPresentIp.TabIndex = 0;
            this.LblPresentIp.Text = "현재IP";
            // 
            // BtnGetIp
            // 
            this.BtnGetIp.Location = new System.Drawing.Point(88, 12);
            this.BtnGetIp.Name = "BtnGetIp";
            this.BtnGetIp.Size = new System.Drawing.Size(93, 27);
            this.BtnGetIp.TabIndex = 1;
            this.BtnGetIp.Text = "현재IP확인";
            this.BtnGetIp.UseVisualStyleBackColor = true;
            this.BtnGetIp.Click += new System.EventHandler(this.BtnGetIp_Click);
            // 
            // BtnSetIpTSCAN2
            // 
            this.BtnSetIpTSCAN2.Location = new System.Drawing.Point(86, 75);
            this.BtnSetIpTSCAN2.Name = "BtnSetIpTSCAN2";
            this.BtnSetIpTSCAN2.Size = new System.Drawing.Size(93, 27);
            this.BtnSetIpTSCAN2.TabIndex = 2;
            this.BtnSetIpTSCAN2.Text = "192.168.133.10";
            this.BtnSetIpTSCAN2.UseVisualStyleBackColor = true;
            this.BtnSetIpTSCAN2.Click += new System.EventHandler(this.BtnSetIpTSCAN2_Click);
            // 
            // BtnSetDHCP
            // 
            this.BtnSetDHCP.Location = new System.Drawing.Point(185, 75);
            this.BtnSetDHCP.Name = "BtnSetDHCP";
            this.BtnSetDHCP.Size = new System.Drawing.Size(85, 27);
            this.BtnSetDHCP.TabIndex = 3;
            this.BtnSetDHCP.Text = "To DHCP";
            this.BtnSetDHCP.UseVisualStyleBackColor = true;
            this.BtnSetDHCP.Click += new System.EventHandler(this.BtnSetDHCP_Click);
            // 
            // BtnPingTest
            // 
            this.BtnPingTest.Location = new System.Drawing.Point(5, 75);
            this.BtnPingTest.Name = "BtnPingTest";
            this.BtnPingTest.Size = new System.Drawing.Size(75, 27);
            this.BtnPingTest.TabIndex = 4;
            this.BtnPingTest.Text = "Ping Test";
            this.BtnPingTest.UseVisualStyleBackColor = true;
            this.BtnPingTest.Click += new System.EventHandler(this.BtnPingTest_Click);
            // 
            // TxtLog
            // 
            this.TxtLog.Location = new System.Drawing.Point(5, 108);
            this.TxtLog.Multiline = true;
            this.TxtLog.Name = "TxtLog";
            this.TxtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtLog.Size = new System.Drawing.Size(265, 124);
            this.TxtLog.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "제작: 전장선행생기팀 김동원 (190708)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "네트웍어댑터";
            // 
            // CboNetAdapts
            // 
            this.CboNetAdapts.FormattingEnabled = true;
            this.CboNetAdapts.Location = new System.Drawing.Point(5, 45);
            this.CboNetAdapts.Name = "CboNetAdapts";
            this.CboNetAdapts.Size = new System.Drawing.Size(265, 20);
            this.CboNetAdapts.TabIndex = 8;
            this.CboNetAdapts.SelectedIndexChanged += new System.EventHandler(this.CboNetAdapts_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 265);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 25);
            this.button1.TabIndex = 9;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(90, 265);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 25);
            this.button2.TabIndex = 10;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(183, 265);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(53, 25);
            this.button3.TabIndex = 11;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 335);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CboNetAdapts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtLog);
            this.Controls.Add(this.BtnPingTest);
            this.Controls.Add(this.BtnSetDHCP);
            this.Controls.Add(this.BtnSetIpTSCAN2);
            this.Controls.Add(this.BtnGetIp);
            this.Controls.Add(this.LblPresentIp);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "IP설정(T스캔2)";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblPresentIp;
        private System.Windows.Forms.Button BtnGetIp;
        private System.Windows.Forms.Button BtnSetIpTSCAN2;
        private System.Windows.Forms.Button BtnSetDHCP;
        private System.Windows.Forms.Button BtnPingTest;
        private System.Windows.Forms.TextBox TxtLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CboNetAdapts;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

