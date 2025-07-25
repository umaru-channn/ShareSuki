namespace ShareSuki
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblStudentId = new System.Windows.Forms.Label();
            this.txtStudentId = new System.Windows.Forms.TextBox();
            this.lblClassName = new System.Windows.Forms.Label();
            this.cmbClassName = new System.Windows.Forms.ComboBox();
            this.lblAttendanceNo = new System.Windows.Forms.Label();
            this.txtAttendanceNo = new System.Windows.Forms.TextBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblRequestedSkill = new System.Windows.Forms.Label();
            this.cmbRequestedSkill = new System.Windows.Forms.ComboBox();
            this.lblRequestedSkillNote = new System.Windows.Forms.Label();
            this.txtRequestedSkillNote = new System.Windows.Forms.TextBox();
            this.lblOfferedSkill = new System.Windows.Forms.Label();
            this.cmbOfferedSkill = new System.Windows.Forms.ComboBox();
            this.lblOfferedSkillNote = new System.Windows.Forms.Label();
            this.txtOfferedSkillNote = new System.Windows.Forms.TextBox();
            this.lblWaitingPeriod = new System.Windows.Forms.Label();
            this.cmbWaitingPeriod = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnViewData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DeepPink;
            this.lblTitle.Location = new System.Drawing.Point(300, 30);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(184, 33);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "♡シェアスキ♡";
            // 
            // lblStudentId
            // 
            this.lblStudentId.AutoSize = true;
            this.lblStudentId.Location = new System.Drawing.Point(50, 105);
            this.lblStudentId.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStudentId.Name = "lblStudentId";
            this.lblStudentId.Size = new System.Drawing.Size(113, 18);
            this.lblStudentId.TabIndex = 1;
            this.lblStudentId.Text = "学籍番号 (*)：";
            // 
            // txtStudentId
            // 
            this.txtStudentId.Location = new System.Drawing.Point(250, 100);
            this.txtStudentId.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtStudentId.MaxLength = 6;
            this.txtStudentId.Name = "txtStudentId";
            this.txtStudentId.Size = new System.Drawing.Size(164, 25);
            this.txtStudentId.TabIndex = 2;
            this.txtStudentId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStudentId_KeyPress);
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Location = new System.Drawing.Point(50, 158);
            this.lblClassName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(73, 18);
            this.lblClassName.TabIndex = 3;
            this.lblClassName.Text = "クラス名：";
            // 
            // cmbClassName
            // 
            this.cmbClassName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClassName.Location = new System.Drawing.Point(250, 153);
            this.cmbClassName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cmbClassName.Name = "cmbClassName";
            this.cmbClassName.Size = new System.Drawing.Size(197, 26);
            this.cmbClassName.TabIndex = 4;
            // 
            // lblAttendanceNo
            // 
            this.lblAttendanceNo.AutoSize = true;
            this.lblAttendanceNo.Location = new System.Drawing.Point(50, 210);
            this.lblAttendanceNo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblAttendanceNo.Name = "lblAttendanceNo";
            this.lblAttendanceNo.Size = new System.Drawing.Size(89, 18);
            this.lblAttendanceNo.TabIndex = 5;
            this.lblAttendanceNo.Text = "出席番号：";
            // 
            // txtAttendanceNo
            // 
            this.txtAttendanceNo.Location = new System.Drawing.Point(250, 206);
            this.txtAttendanceNo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtAttendanceNo.Name = "txtAttendanceNo";
            this.txtAttendanceNo.Size = new System.Drawing.Size(97, 25);
            this.txtAttendanceNo.TabIndex = 6;
            this.txtAttendanceNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAttendanceNo_KeyPress);
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(50, 262);
            this.lblGender.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(53, 18);
            this.lblGender.TabIndex = 7;
            this.lblGender.Text = "性別：";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.Location = new System.Drawing.Point(250, 258);
            this.cmbGender.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(131, 26);
            this.cmbGender.TabIndex = 8;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(50, 315);
            this.lblName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(77, 18);
            this.lblName.TabIndex = 9;
            this.lblName.Text = "氏名 (*)：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(250, 310);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(247, 25);
            this.txtName.TabIndex = 10;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(50, 368);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(114, 18);
            this.lblEmail.TabIndex = 11;
            this.lblEmail.Text = "メールアドレス：";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(250, 363);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(331, 25);
            this.txtEmail.TabIndex = 12;
            // 
            // lblRequestedSkill
            // 
            this.lblRequestedSkill.AutoSize = true;
            this.lblRequestedSkill.Location = new System.Drawing.Point(50, 420);
            this.lblRequestedSkill.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblRequestedSkill.Name = "lblRequestedSkill";
            this.lblRequestedSkill.Size = new System.Drawing.Size(79, 18);
            this.lblRequestedSkill.TabIndex = 13;
            this.lblRequestedSkill.Text = "求スキル：";
            // 
            // cmbRequestedSkill
            // 
            this.cmbRequestedSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRequestedSkill.Location = new System.Drawing.Point(250, 416);
            this.cmbRequestedSkill.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cmbRequestedSkill.Name = "cmbRequestedSkill";
            this.cmbRequestedSkill.Size = new System.Drawing.Size(197, 26);
            this.cmbRequestedSkill.TabIndex = 14;
            // 
            // lblRequestedSkillNote
            // 
            this.lblRequestedSkillNote.AutoSize = true;
            this.lblRequestedSkillNote.Location = new System.Drawing.Point(50, 472);
            this.lblRequestedSkillNote.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblRequestedSkillNote.Name = "lblRequestedSkillNote";
            this.lblRequestedSkillNote.Size = new System.Drawing.Size(115, 18);
            this.lblRequestedSkillNote.TabIndex = 15;
            this.lblRequestedSkillNote.Text = "求スキル備考：";
            // 
            // txtRequestedSkillNote
            // 
            this.txtRequestedSkillNote.Location = new System.Drawing.Point(250, 468);
            this.txtRequestedSkillNote.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtRequestedSkillNote.Multiline = true;
            this.txtRequestedSkillNote.Name = "txtRequestedSkillNote";
            this.txtRequestedSkillNote.Size = new System.Drawing.Size(497, 58);
            this.txtRequestedSkillNote.TabIndex = 16;
            // 
            // lblOfferedSkill
            // 
            this.lblOfferedSkill.AutoSize = true;
            this.lblOfferedSkill.Location = new System.Drawing.Point(50, 555);
            this.lblOfferedSkill.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblOfferedSkill.Name = "lblOfferedSkill";
            this.lblOfferedSkill.Size = new System.Drawing.Size(79, 18);
            this.lblOfferedSkill.TabIndex = 17;
            this.lblOfferedSkill.Text = "譲スキル：";
            // 
            // cmbOfferedSkill
            // 
            this.cmbOfferedSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOfferedSkill.Location = new System.Drawing.Point(250, 550);
            this.cmbOfferedSkill.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cmbOfferedSkill.Name = "cmbOfferedSkill";
            this.cmbOfferedSkill.Size = new System.Drawing.Size(197, 26);
            this.cmbOfferedSkill.TabIndex = 18;
            // 
            // lblOfferedSkillNote
            // 
            this.lblOfferedSkillNote.AutoSize = true;
            this.lblOfferedSkillNote.Location = new System.Drawing.Point(50, 608);
            this.lblOfferedSkillNote.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblOfferedSkillNote.Name = "lblOfferedSkillNote";
            this.lblOfferedSkillNote.Size = new System.Drawing.Size(115, 18);
            this.lblOfferedSkillNote.TabIndex = 19;
            this.lblOfferedSkillNote.Text = "譲スキル備考：";
            // 
            // txtOfferedSkillNote
            // 
            this.txtOfferedSkillNote.Location = new System.Drawing.Point(250, 603);
            this.txtOfferedSkillNote.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtOfferedSkillNote.Multiline = true;
            this.txtOfferedSkillNote.Name = "txtOfferedSkillNote";
            this.txtOfferedSkillNote.Size = new System.Drawing.Size(497, 58);
            this.txtOfferedSkillNote.TabIndex = 20;
            // 
            // lblWaitingPeriod
            // 
            this.lblWaitingPeriod.AutoSize = true;
            this.lblWaitingPeriod.Location = new System.Drawing.Point(50, 690);
            this.lblWaitingPeriod.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblWaitingPeriod.Name = "lblWaitingPeriod";
            this.lblWaitingPeriod.Size = new System.Drawing.Size(89, 18);
            this.lblWaitingPeriod.TabIndex = 21;
            this.lblWaitingPeriod.Text = "待機期間：";
            // 
            // cmbWaitingPeriod
            // 
            this.cmbWaitingPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaitingPeriod.Location = new System.Drawing.Point(250, 686);
            this.cmbWaitingPeriod.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cmbWaitingPeriod.Name = "cmbWaitingPeriod";
            this.cmbWaitingPeriod.Size = new System.Drawing.Size(197, 26);
            this.cmbWaitingPeriod.TabIndex = 22;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.LightPink;
            this.btnSubmit.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.Location = new System.Drawing.Point(250, 750);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(200, 60);
            this.btnSubmit.TabIndex = 23;
            this.btnSubmit.Text = "登録";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnViewData
            // 
            this.btnViewData.BackColor = System.Drawing.Color.LightBlue;
            this.btnViewData.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.btnViewData.Location = new System.Drawing.Point(500, 750);
            this.btnViewData.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnViewData.Name = "btnViewData";
            this.btnViewData.Size = new System.Drawing.Size(167, 60);
            this.btnViewData.TabIndex = 24;
            this.btnViewData.Text = "登録データ\r\n一覧表示";
            this.btnViewData.UseVisualStyleBackColor = false;
            this.btnViewData.Click += new System.EventHandler(this.btnViewData_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.ClientSize = new System.Drawing.Size(867, 870);
            this.Controls.Add(this.btnViewData);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmbWaitingPeriod);
            this.Controls.Add(this.lblWaitingPeriod);
            this.Controls.Add(this.txtOfferedSkillNote);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblOfferedSkillNote);
            this.Controls.Add(this.cmbOfferedSkill);
            this.Controls.Add(this.lblOfferedSkill);
            this.Controls.Add(this.txtRequestedSkillNote);
            this.Controls.Add(this.lblRequestedSkillNote);
            this.Controls.Add(this.cmbRequestedSkill);
            this.Controls.Add(this.lblRequestedSkill);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.txtAttendanceNo);
            this.Controls.Add(this.lblAttendanceNo);
            this.Controls.Add(this.cmbClassName);
            this.Controls.Add(this.lblClassName);
            this.Controls.Add(this.txtStudentId);
            this.Controls.Add(this.lblStudentId);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "♡シェアスキ♡ - スキル登録";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblStudentId;
        private System.Windows.Forms.TextBox txtStudentId;
        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.ComboBox cmbClassName;
        private System.Windows.Forms.Label lblAttendanceNo;
        private System.Windows.Forms.TextBox txtAttendanceNo;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblRequestedSkill;
        private System.Windows.Forms.ComboBox cmbRequestedSkill;
        private System.Windows.Forms.Label lblRequestedSkillNote;
        private System.Windows.Forms.TextBox txtRequestedSkillNote;
        private System.Windows.Forms.Label lblOfferedSkill;
        private System.Windows.Forms.ComboBox cmbOfferedSkill;
        private System.Windows.Forms.Label lblOfferedSkillNote;
        private System.Windows.Forms.TextBox txtOfferedSkillNote;
        private System.Windows.Forms.Label lblWaitingPeriod;
        private System.Windows.Forms.ComboBox cmbWaitingPeriod;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnViewData;
    }
}

