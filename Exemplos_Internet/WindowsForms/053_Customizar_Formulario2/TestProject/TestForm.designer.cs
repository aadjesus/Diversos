namespace TestProject
{
    partial class TestForm
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
            this.btnAttach = new System.Windows.Forms.Button();
            this.btnFullScreen = new System.Windows.Forms.Button();
            this.btnMovable = new System.Windows.Forms.Button();
            this.btnSizable = new System.Windows.Forms.Button();
            this.btnCloseButton = new System.Windows.Forms.Button();
            this.btnGetKeyState = new System.Windows.Forms.Button();
            this.cmbKeys = new System.Windows.Forms.ComboBox();
            this.txtKeyState = new System.Windows.Forms.TextBox();
            this.txtKeyValue = new System.Windows.Forms.TextBox();
            this.lblKeyState = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAttach
            // 
            this.btnAttach.BackColor = System.Drawing.Color.Red;
            this.btnAttach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttach.Location = new System.Drawing.Point(72, 34);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(119, 23);
            this.btnAttach.TabIndex = 0;
            this.btnAttach.TabStop = false;
            this.btnAttach.Text = "Attach to Desktop";
            this.btnAttach.UseVisualStyleBackColor = false;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.BackColor = System.Drawing.Color.Red;
            this.btnFullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFullScreen.Location = new System.Drawing.Point(258, 34);
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(119, 23);
            this.btnFullScreen.TabIndex = 1;
            this.btnFullScreen.TabStop = false;
            this.btnFullScreen.Text = "Full Screen";
            this.btnFullScreen.UseVisualStyleBackColor = false;
            this.btnFullScreen.Click += new System.EventHandler(this.btnFullScreen_Click);
            // 
            // btnMovable
            // 
            this.btnMovable.BackColor = System.Drawing.Color.Green;
            this.btnMovable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMovable.Location = new System.Drawing.Point(72, 90);
            this.btnMovable.Name = "btnMovable";
            this.btnMovable.Size = new System.Drawing.Size(119, 23);
            this.btnMovable.TabIndex = 2;
            this.btnMovable.TabStop = false;
            this.btnMovable.Text = "Movable";
            this.btnMovable.UseVisualStyleBackColor = false;
            this.btnMovable.Click += new System.EventHandler(this.btnMovable_Click);
            // 
            // btnSizable
            // 
            this.btnSizable.BackColor = System.Drawing.Color.Green;
            this.btnSizable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSizable.Location = new System.Drawing.Point(260, 90);
            this.btnSizable.Name = "btnSizable";
            this.btnSizable.Size = new System.Drawing.Size(119, 23);
            this.btnSizable.TabIndex = 3;
            this.btnSizable.TabStop = false;
            this.btnSizable.Text = "Sizable";
            this.btnSizable.UseVisualStyleBackColor = false;
            this.btnSizable.Click += new System.EventHandler(this.btnSizable_Click);
            // 
            // btnCloseButton
            // 
            this.btnCloseButton.BackColor = System.Drawing.Color.Red;
            this.btnCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseButton.Location = new System.Drawing.Point(166, 257);
            this.btnCloseButton.Name = "btnCloseButton";
            this.btnCloseButton.Size = new System.Drawing.Size(119, 23);
            this.btnCloseButton.TabIndex = 4;
            this.btnCloseButton.TabStop = false;
            this.btnCloseButton.Text = "Disable Close Button";
            this.btnCloseButton.UseVisualStyleBackColor = false;
            this.btnCloseButton.Click += new System.EventHandler(this.btnCloseButton_Click);
            // 
            // btnGetKeyState
            // 
            this.btnGetKeyState.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnGetKeyState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetKeyState.ForeColor = System.Drawing.Color.White;
            this.btnGetKeyState.Location = new System.Drawing.Point(72, 146);
            this.btnGetKeyState.Name = "btnGetKeyState";
            this.btnGetKeyState.Size = new System.Drawing.Size(119, 23);
            this.btnGetKeyState.TabIndex = 5;
            this.btnGetKeyState.TabStop = false;
            this.btnGetKeyState.Text = "Get Key State";
            this.btnGetKeyState.UseVisualStyleBackColor = false;
            this.btnGetKeyState.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGetKeyState_KeyDown);
            this.btnGetKeyState.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnGetKeyState_KeyUp);
            this.btnGetKeyState.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnGetKeyState_MouseDown);
            this.btnGetKeyState.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnGetKeyState_MouseUp);
            // 
            // cmbKeys
            // 
            this.cmbKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKeys.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbKeys.FormattingEnabled = true;
            this.cmbKeys.Location = new System.Drawing.Point(258, 146);
            this.cmbKeys.Name = "cmbKeys";
            this.cmbKeys.Size = new System.Drawing.Size(121, 23);
            this.cmbKeys.TabIndex = 6;
            this.cmbKeys.TabStop = false;
            // 
            // txtKeyState
            // 
            this.txtKeyState.Location = new System.Drawing.Point(118, 207);
            this.txtKeyState.Name = "txtKeyState";
            this.txtKeyState.ReadOnly = true;
            this.txtKeyState.Size = new System.Drawing.Size(100, 20);
            this.txtKeyState.TabIndex = 7;
            this.txtKeyState.TabStop = false;
            // 
            // txtKeyValue
            // 
            this.txtKeyValue.Location = new System.Drawing.Point(236, 207);
            this.txtKeyValue.Name = "txtKeyValue";
            this.txtKeyValue.ReadOnly = true;
            this.txtKeyValue.Size = new System.Drawing.Size(100, 20);
            this.txtKeyValue.TabIndex = 8;
            this.txtKeyValue.TabStop = false;
            // 
            // lblKeyState
            // 
            this.lblKeyState.AutoSize = true;
            this.lblKeyState.Location = new System.Drawing.Point(115, 191);
            this.lblKeyState.Name = "lblKeyState";
            this.lblKeyState.Size = new System.Drawing.Size(56, 13);
            this.lblKeyState.TabIndex = 9;
            this.lblKeyState.Text = "Key State:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(233, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Key Value:";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 311);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblKeyState);
            this.Controls.Add(this.txtKeyValue);
            this.Controls.Add(this.txtKeyState);
            this.Controls.Add(this.cmbKeys);
            this.Controls.Add(this.btnGetKeyState);
            this.Controls.Add(this.btnCloseButton);
            this.Controls.Add(this.btnSizable);
            this.Controls.Add(this.btnMovable);
            this.Controls.Add(this.btnFullScreen);
            this.Controls.Add(this.btnAttach);
            this.Name = "TestForm";
            this.Text = "";
            this.PaintFrameArea += new System.Windows.Forms.PaintEventHandler(this.Form2_PaintTitleBar);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.Button btnFullScreen;
        private System.Windows.Forms.Button btnMovable;
        private System.Windows.Forms.Button btnSizable;
        private System.Windows.Forms.Button btnCloseButton;
        private System.Windows.Forms.Button btnGetKeyState;
        private System.Windows.Forms.ComboBox cmbKeys;
        private System.Windows.Forms.TextBox txtKeyState;
        private System.Windows.Forms.TextBox txtKeyValue;
        private System.Windows.Forms.Label lblKeyState;
        private System.Windows.Forms.Label label1;


    }
}