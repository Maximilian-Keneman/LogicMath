namespace LogicMath
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.OperationsList = new System.Windows.Forms.CheckedListBox();
            this.VariableNumeric = new System.Windows.Forms.NumericUpDown();
            this.SignsNumeric = new System.Windows.Forms.NumericUpDown();
            this.CreateButton = new System.Windows.Forms.Button();
            this.ExampleLabel = new System.Windows.Forms.Label();
            this.ExampleError = new System.Windows.Forms.ErrorProvider(this.components);
            this.VariableLabel = new System.Windows.Forms.Label();
            this.SignsLabel = new System.Windows.Forms.Label();
            this.OperationsLabel = new System.Windows.Forms.Label();
            this.ExampleToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.DescriptionButton = new System.Windows.Forms.Button();
            this.DNFButton = new System.Windows.Forms.Button();
            this.CNFButton = new System.Windows.Forms.Button();
            this.PDNFButton = new System.Windows.Forms.Button();
            this.PCNFButton = new System.Windows.Forms.Button();
            this.NFLabel = new System.Windows.Forms.Label();
            this.TruthTableButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.AboutLink = new System.Windows.Forms.LinkLabel();
            this.HelpLink = new System.Windows.Forms.LinkLabel();
            this.ClipBoardTimer = new System.Windows.Forms.Timer(this.components);
            this.ExamplePanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.VariableNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SignsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExampleError)).BeginInit();
            this.ExamplePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OperationsList
            // 
            this.OperationsList.CheckOnClick = true;
            this.OperationsList.FormattingEnabled = true;
            this.OperationsList.Items.AddRange(new object[] {
            "Инверсия",
            "Дизъюнкция",
            "Конъюнкция",
            "Импликация",
            "Эквивалентность",
            "Неравнозначность"});
            this.OperationsList.Location = new System.Drawing.Point(187, 30);
            this.OperationsList.Name = "OperationsList";
            this.OperationsList.Size = new System.Drawing.Size(134, 94);
            this.OperationsList.TabIndex = 0;
            this.OperationsList.SelectedIndexChanged += new System.EventHandler(this.Parameters_Changed);
            // 
            // VariableNumeric
            // 
            this.VariableNumeric.Location = new System.Drawing.Point(150, 12);
            this.VariableNumeric.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.VariableNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.VariableNumeric.Name = "VariableNumeric";
            this.VariableNumeric.Size = new System.Drawing.Size(28, 20);
            this.VariableNumeric.TabIndex = 1;
            this.VariableNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.VariableNumeric.ValueChanged += new System.EventHandler(this.Parameters_Changed);
            // 
            // SignsNumeric
            // 
            this.SignsNumeric.Location = new System.Drawing.Point(134, 38);
            this.SignsNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.SignsNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SignsNumeric.Name = "SignsNumeric";
            this.SignsNumeric.Size = new System.Drawing.Size(44, 20);
            this.SignsNumeric.TabIndex = 2;
            this.SignsNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SignsNumeric.ValueChanged += new System.EventHandler(this.Parameters_Changed);
            // 
            // CreateButton
            // 
            this.CreateButton.Enabled = false;
            this.CreateButton.Location = new System.Drawing.Point(15, 101);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 3;
            this.CreateButton.Text = "Создать";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // ExampleLabel
            // 
            this.ExampleLabel.AutoSize = true;
            this.ExampleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.ExampleLabel.Location = new System.Drawing.Point(3, 3);
            this.ExampleLabel.Margin = new System.Windows.Forms.Padding(3);
            this.ExampleLabel.Name = "ExampleLabel";
            this.ExampleLabel.Size = new System.Drawing.Size(87, 25);
            this.ExampleLabel.TabIndex = 4;
            this.ExampleLabel.Text = "Пример";
            this.ExampleLabel.DoubleClick += new System.EventHandler(this.ExampleLabel_DoubleClick);
            // 
            // ExampleError
            // 
            this.ExampleError.ContainerControl = this;
            // 
            // VariableLabel
            // 
            this.VariableLabel.AutoSize = true;
            this.VariableLabel.Location = new System.Drawing.Point(12, 14);
            this.VariableLabel.Name = "VariableLabel";
            this.VariableLabel.Size = new System.Drawing.Size(132, 13);
            this.VariableLabel.TabIndex = 5;
            this.VariableLabel.Text = "Количество переменных";
            // 
            // SignsLabel
            // 
            this.SignsLabel.AutoSize = true;
            this.SignsLabel.Location = new System.Drawing.Point(12, 40);
            this.SignsLabel.Name = "SignsLabel";
            this.SignsLabel.Size = new System.Drawing.Size(116, 13);
            this.SignsLabel.TabIndex = 6;
            this.SignsLabel.Text = "Количество действий";
            // 
            // OperationsLabel
            // 
            this.OperationsLabel.AutoSize = true;
            this.OperationsLabel.Location = new System.Drawing.Point(184, 14);
            this.OperationsLabel.Name = "OperationsLabel";
            this.OperationsLabel.Size = new System.Drawing.Size(134, 13);
            this.OperationsLabel.TabIndex = 7;
            this.OperationsLabel.Text = "Используемые действия";
            // 
            // DescriptionButton
            // 
            this.DescriptionButton.Location = new System.Drawing.Point(96, 101);
            this.DescriptionButton.Name = "DescriptionButton";
            this.DescriptionButton.Size = new System.Drawing.Size(84, 23);
            this.DescriptionButton.TabIndex = 8;
            this.DescriptionButton.Text = "Обозначения";
            this.DescriptionButton.UseVisualStyleBackColor = true;
            this.DescriptionButton.Click += new System.EventHandler(this.DescriptionButton_Click);
            // 
            // DNFButton
            // 
            this.DNFButton.Enabled = false;
            this.DNFButton.Location = new System.Drawing.Point(19, 195);
            this.DNFButton.Name = "DNFButton";
            this.DNFButton.Size = new System.Drawing.Size(50, 23);
            this.DNFButton.TabIndex = 9;
            this.DNFButton.Text = "ДНФ";
            this.DNFButton.UseVisualStyleBackColor = true;
            this.DNFButton.Click += new System.EventHandler(this.DNFButton_Click);
            // 
            // CNFButton
            // 
            this.CNFButton.Enabled = false;
            this.CNFButton.Location = new System.Drawing.Point(75, 195);
            this.CNFButton.Name = "CNFButton";
            this.CNFButton.Size = new System.Drawing.Size(50, 23);
            this.CNFButton.TabIndex = 10;
            this.CNFButton.Text = "КНФ";
            this.CNFButton.UseVisualStyleBackColor = true;
            this.CNFButton.Click += new System.EventHandler(this.CNFButton_Click);
            // 
            // PDNFButton
            // 
            this.PDNFButton.Enabled = false;
            this.PDNFButton.Location = new System.Drawing.Point(131, 195);
            this.PDNFButton.Name = "PDNFButton";
            this.PDNFButton.Size = new System.Drawing.Size(50, 23);
            this.PDNFButton.TabIndex = 11;
            this.PDNFButton.Text = "СДНФ";
            this.PDNFButton.UseVisualStyleBackColor = true;
            this.PDNFButton.Click += new System.EventHandler(this.PDNFButton_Click);
            // 
            // PCNFButton
            // 
            this.PCNFButton.Enabled = false;
            this.PCNFButton.Location = new System.Drawing.Point(187, 195);
            this.PCNFButton.Name = "PCNFButton";
            this.PCNFButton.Size = new System.Drawing.Size(50, 23);
            this.PCNFButton.TabIndex = 12;
            this.PCNFButton.Text = "СКНФ";
            this.PCNFButton.UseVisualStyleBackColor = true;
            this.PCNFButton.Click += new System.EventHandler(this.PCNFButton_Click);
            // 
            // NFLabel
            // 
            this.NFLabel.AutoEllipsis = true;
            this.NFLabel.AutoSize = true;
            this.NFLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.NFLabel.Location = new System.Drawing.Point(3, 3);
            this.NFLabel.Margin = new System.Windows.Forms.Padding(3);
            this.NFLabel.Name = "NFLabel";
            this.NFLabel.Size = new System.Drawing.Size(201, 25);
            this.NFLabel.TabIndex = 13;
            this.NFLabel.Text = "Нормальная форма";
            // 
            // TruthTableButton
            // 
            this.TruthTableButton.Enabled = false;
            this.TruthTableButton.Location = new System.Drawing.Point(243, 182);
            this.TruthTableButton.Name = "TruthTableButton";
            this.TruthTableButton.Size = new System.Drawing.Size(75, 36);
            this.TruthTableButton.TabIndex = 14;
            this.TruthTableButton.Text = "Таблица Истиности";
            this.TruthTableButton.UseVisualStyleBackColor = true;
            this.TruthTableButton.Click += new System.EventHandler(this.TruthTableButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(19, 276);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(299, 23);
            this.SaveButton.TabIndex = 15;
            this.SaveButton.Text = "Сохранить решения";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveDialog
            // 
            this.SaveDialog.Filter = "Текстовый файл|*.txt";
            this.SaveDialog.Title = "Сохранение";
            // 
            // AboutLink
            // 
            this.AboutLink.AutoSize = true;
            this.AboutLink.Location = new System.Drawing.Point(17, 302);
            this.AboutLink.Name = "AboutLink";
            this.AboutLink.Size = new System.Drawing.Size(75, 13);
            this.AboutLink.TabIndex = 16;
            this.AboutLink.TabStop = true;
            this.AboutLink.Text = "О программе";
            this.AboutLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AboutLink_LinkClicked);
            // 
            // HelpLink
            // 
            this.HelpLink.AutoSize = true;
            this.HelpLink.Location = new System.Drawing.Point(98, 302);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(50, 13);
            this.HelpLink.TabIndex = 17;
            this.HelpLink.TabStop = true;
            this.HelpLink.Text = "Справка";
            this.HelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLink_LinkClicked);
            // 
            // ClipBoardTimer
            // 
            this.ClipBoardTimer.Tick += new System.EventHandler(this.ClipBoardTimer_Tick);
            // 
            // ExamplePanel
            // 
            this.ExamplePanel.AutoScroll = true;
            this.ExamplePanel.Controls.Add(this.ExampleLabel);
            this.ExamplePanel.Location = new System.Drawing.Point(12, 130);
            this.ExamplePanel.Name = "ExamplePanel";
            this.ExamplePanel.Size = new System.Drawing.Size(309, 46);
            this.ExamplePanel.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.NFLabel);
            this.panel1.Location = new System.Drawing.Point(12, 224);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(309, 46);
            this.panel1.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(243, 302);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 18);
            this.button1.TabIndex = 20;
            this.button1.Text = "Настройки";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(333, 324);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ExamplePanel);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.AboutLink);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.TruthTableButton);
            this.Controls.Add(this.PCNFButton);
            this.Controls.Add(this.PDNFButton);
            this.Controls.Add(this.CNFButton);
            this.Controls.Add(this.DNFButton);
            this.Controls.Add(this.DescriptionButton);
            this.Controls.Add(this.OperationsLabel);
            this.Controls.Add(this.SignsLabel);
            this.Controls.Add(this.VariableLabel);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.SignsNumeric);
            this.Controls.Add(this.VariableNumeric);
            this.Controls.Add(this.OperationsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Logic";
            ((System.ComponentModel.ISupportInitialize)(this.VariableNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SignsNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExampleError)).EndInit();
            this.ExamplePanel.ResumeLayout(false);
            this.ExamplePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox OperationsList;
        private System.Windows.Forms.NumericUpDown VariableNumeric;
        private System.Windows.Forms.NumericUpDown SignsNumeric;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Label ExampleLabel;
        private System.Windows.Forms.ErrorProvider ExampleError;
        private System.Windows.Forms.Label OperationsLabel;
        private System.Windows.Forms.Label SignsLabel;
        private System.Windows.Forms.Label VariableLabel;
        private System.Windows.Forms.ToolTip ExampleToolTip;
        private System.Windows.Forms.Button DescriptionButton;
        private System.Windows.Forms.Label NFLabel;
        private System.Windows.Forms.Button PCNFButton;
        private System.Windows.Forms.Button PDNFButton;
        private System.Windows.Forms.Button CNFButton;
        private System.Windows.Forms.Button DNFButton;
        private System.Windows.Forms.Button TruthTableButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.SaveFileDialog SaveDialog;
        private System.Windows.Forms.LinkLabel AboutLink;
        private System.Windows.Forms.LinkLabel HelpLink;
        private System.Windows.Forms.Timer ClipBoardTimer;
        private System.Windows.Forms.Panel ExamplePanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}

