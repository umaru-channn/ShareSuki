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
            this.btnEmailTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DeepPink;
            this.lblTitle.Location = new System.Drawing.Point(180, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(174, 22);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "♡シェアスキ♡";
            // 
            // lblStudentId
            // 
            this.lblStudentId.AutoSize = true;
            this.lblStudentId.Location = new System.Drawing.Point(30, 70);
            this.lblStudentId.Name = "lblStudentId";
            this.lblStudentId.Size = new System.Drawing.Size(77, 12);
            this.lblStudentId.TabIndex = 1;
            this.lblStudentId.Text = "学籍番号 (*)：";
            // 
            // txtStudentId
            // 
            this.txtStudentId.Location = new System.Drawing.Point(150, 67);
            this.txtStudentId.MaxLength = 6;
            this.txtStudentId.Name = "txtStudentId";
            this.txtStudentId.Size = new System.Drawing.Size(100, 19);
            this.txtStudentId.TabIndex = 2;
            this.txtStudentId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStudentId_KeyPress);
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Location = new System.Drawing.Point(30, 105);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(56, 12);
            this.lblClassName.TabIndex = 3;
            this.lblClassName.Text = "クラス名：";
            // 
            // cmbClassName
            // 
            this.cmbClassName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClassName.Location = new System.Drawing.Point(150, 102);
            this.cmbClassName.Name = "cmbClassName";
            this.cmbClassName.Size = new System.Drawing.Size(120, 20);
            this.cmbClassName.TabIndex = 4;
            // 
            // lblAttendanceNo
            // 
            this.lblAttendanceNo.AutoSize = true;
            this.lblAttendanceNo.Location = new System.Drawing.Point(30, 140);
            this.lblAttendanceNo.Name = "lblAttendanceNo";
            this.lblAttendanceNo.Size = new System.Drawing.Size(65, 12);
            this.lblAttendanceNo.TabIndex = 5;
            this.lblAttendanceNo.Text = "出席番号：";
            // 
            // txtAttendanceNo
            // 
            this.txtAttendanceNo.Location = new System.Drawing.Point(150, 137);
            this.txtAttendanceNo.Name = "txtAttendanceNo";
            this.txtAttendanceNo.Size = new System.Drawing.Size(60, 19);
            this.txtAttendanceNo.TabIndex = 6;
            this.txtAttendanceNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAttendanceNo_KeyPress);
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(30, 175);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(41, 12);
            this.lblGender.TabIndex = 7;
            this.lblGender.Text = "性別：";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.Location = new System.Drawing.Point(150, 172);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(80, 20);
            this.cmbGender.TabIndex = 8;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(30, 210);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(50, 12);
            this.lblName.TabIndex = 9;
            this.lblName.Text = "氏名 (*)：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(150, 207);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 19);
            this.txtName.TabIndex = 10;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(30, 245);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(84, 12);
            this.lblEmail.TabIndex = 11;
            this.lblEmail.Text = "メールアドレス：";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(150, 242);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 19);
            this.txtEmail.TabIndex = 12;
            // 
            // lblRequestedSkill
            // 
            this.lblRequestedSkill.AutoSize = true;
            this.lblRequestedSkill.Location = new System.Drawing.Point(30, 280);
            this.lblRequestedSkill.Name = "lblRequestedSkill";
            this.lblRequestedSkill.Size = new System.Drawing.Size(56, 12);
            this.lblRequestedSkill.TabIndex = 13;
            this.lblRequestedSkill.Text = "求スキル：";
            // 
            // cmbRequestedSkill
            // 
            this.cmbRequestedSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRequestedSkill.Location = new System.Drawing.Point(150, 277);
            this.cmbRequestedSkill.Name = "cmbRequestedSkill";
            this.cmbRequestedSkill.Size = new System.Drawing.Size(120, 20);
            this.cmbRequestedSkill.TabIndex = 14;
            // 
            // lblRequestedSkillNote
            // 
            this.lblRequestedSkillNote.AutoSize = true;
            this.lblRequestedSkillNote.Location = new System.Drawing.Point(30, 315);
            this.lblRequestedSkillNote.Name = "lblRequestedSkillNote";
            this.lblRequestedSkillNote.Size = new System.Drawing.Size(80, 12);
            this.lblRequestedSkillNote.TabIndex = 15;
            this.lblRequestedSkillNote.Text = "求スキル備考：";
            // 
            // txtRequestedSkillNote
            // 
            this.txtRequestedSkillNote.Location = new System.Drawing.Point(150, 312);
            this.txtRequestedSkillNote.Multiline = true;
            this.txtRequestedSkillNote.Name = "txtRequestedSkillNote";
            this.txtRequestedSkillNote.Size = new System.Drawing.Size(300, 40);
            this.txtRequestedSkillNote.TabIndex = 16;
            // 
            // lblOfferedSkill
            // 
            this.lblOfferedSkill.AutoSize = true;
            this.lblOfferedSkill.Location = new System.Drawing.Point(30, 370);
            this.lblOfferedSkill.Name = "lblOfferedSkill";
            this.lblOfferedSkill.Size = new System.Drawing.Size(56, 12);
            this.lblOfferedSkill.TabIndex = 17;
            this.lblOfferedSkill.Text = "譲スキル：";
            // 
            // cmbOfferedSkill
            // 
            this.cmbOfferedSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOfferedSkill.Location = new System.Drawing.Point(150, 367);
            this.cmbOfferedSkill.Name = "cmbOfferedSkill";
            this.cmbOfferedSkill.Size = new System.Drawing.Size(120, 20);
            this.cmbOfferedSkill.TabIndex = 18;
            // 
            // lblOfferedSkillNote
            // 
            this.lblOfferedSkillNote.AutoSize = true;
            this.lblOfferedSkillNote.Location = new System.Drawing.Point(30, 405);
            this.lblOfferedSkillNote.Name = "lblOfferedSkillNote";
            this.lblOfferedSkillNote.Size = new System.Drawing.Size(80, 12);
            this.lblOfferedSkillNote.TabIndex = 19;
            this.lblOfferedSkillNote.Text = "譲スキル備考：";
            // 
            // txtOfferedSkillNote
            // 
            this.txtOfferedSkillNote.Location = new System.Drawing.Point(150, 402);
            this.txtOfferedSkillNote.Multiline = true;
            this.txtOfferedSkillNote.Name = "txtOfferedSkillNote";
            this.txtOfferedSkillNote.Size = new System.Drawing.Size(300, 40);
            this.txtOfferedSkillNote.TabIndex = 20;
            // 
            // lblWaitingPeriod
            // 
            this.lblWaitingPeriod.AutoSize = true;
            this.lblWaitingPeriod.Location = new System.Drawing.Point(30, 460);
            this.lblWaitingPeriod.Name = "lblWaitingPeriod";
            this.lblWaitingPeriod.Size = new System.Drawing.Size(65, 12);
            this.lblWaitingPeriod.TabIndex = 21;
            this.lblWaitingPeriod.Text = "待機期間：";
            // 
            // cmbWaitingPeriod
            // 
            this.cmbWaitingPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaitingPeriod.Location = new System.Drawing.Point(150, 457);
            this.cmbWaitingPeriod.Name = "cmbWaitingPeriod";
            this.cmbWaitingPeriod.Size = new System.Drawing.Size(120, 20);
            this.cmbWaitingPeriod.TabIndex = 22;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.LightPink;
            this.btnSubmit.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.Location = new System.Drawing.Point(150, 500);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(120, 40);
            this.btnSubmit.TabIndex = 23;
            this.btnSubmit.Text = "登録";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnViewData
            // 
            this.btnViewData.BackColor = System.Drawing.Color.LightBlue;
            this.btnViewData.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.btnViewData.Location = new System.Drawing.Point(300, 500);
            this.btnViewData.Name = "btnViewData";
            this.btnViewData.Size = new System.Drawing.Size(100, 40);
            this.btnViewData.TabIndex = 24;
            this.btnViewData.Text = "登録データ\r\n一覧表示";
            this.btnViewData.UseVisualStyleBackColor = false;
            this.btnViewData.Click += new System.EventHandler(this.btnViewData_Click);
            // 
            // btnEmailTest
            // 
            this.btnEmailTest.BackColor = System.Drawing.Color.LightGreen;
            this.btnEmailTest.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.btnEmailTest.Location = new System.Drawing.Point(420, 500);
            this.btnEmailTest.Name = "btnEmailTest";
            this.btnEmailTest.Size = new System.Drawing.Size(80, 40);
            this.btnEmailTest.TabIndex = 25;
            this.btnEmailTest.Text = "メール\r\nテスト";
            this.btnEmailTest.UseVisualStyleBackColor = false;
            this.btnEmailTest.Click += new System.EventHandler(this.btnEmailTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.ClientSize = new System.Drawing.Size(520, 580);
            this.Controls.Add(this.btnEmailTest);
            this.Controls.Add(this.btnViewData);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmbWaitingPeriod);
            this.Controls.Add(this.lblWaitingPeriod);
            this.Controls.Add(this.txtOfferedSkillNote);
            this.Controls.Add(this.lblOfferedSkillNote);
            this.Controls.Add(this.cmbOfferedSkill);
            this.Controls.Add(this.lblOfferedSkill);
            this.Controls.Add(this.txtRequestedSkillNote);
            this.Controls.Add(this.lblRequestedSkillNote);
            this.Controls.Add(this.cmbRequestedSkill);
            this.Controls.Add(this.lblRequestedSkill);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
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
        private System.Windows.Forms.Button btnEmailTest;
    }
}

