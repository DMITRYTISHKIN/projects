namespace WindowsFormsApplication2
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сКлавиатурыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изФайлаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выводДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.наЭкранToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.решениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.северозападныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.лебедевToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оптимизацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обАвторахToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оМетодахРешенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonCreateMatrix = new System.Windows.Forms.Button();
            this.buttonConfimDate = new System.Windows.Forms.Button();
            this.dataGridViewRez = new System.Windows.Forms.DataGridView();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.labelInfo = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.labelN = new System.Windows.Forms.Label();
            this.labelM = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRez)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem,
            this.выводДанныхToolStripMenuItem,
            this.решениеToolStripMenuItem,
            this.справкаToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(557, 26);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сКлавиатурыToolStripMenuItem,
            this.изФайлаToolStripMenuItem});
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.ToolStripMenuItem.Text = "Ввод данных";
            // 
            // сКлавиатурыToolStripMenuItem
            // 
            this.сКлавиатурыToolStripMenuItem.Name = "сКлавиатурыToolStripMenuItem";
            this.сКлавиатурыToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.сКлавиатурыToolStripMenuItem.Text = "С клавиатуры";
            this.сКлавиатурыToolStripMenuItem.Click += new System.EventHandler(this.сКлавиатурыToolStripMenuItem_Click);
            // 
            // изФайлаToolStripMenuItem
            // 
            this.изФайлаToolStripMenuItem.Name = "изФайлаToolStripMenuItem";
            this.изФайлаToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.изФайлаToolStripMenuItem.Text = "Из файла";
            this.изФайлаToolStripMenuItem.Click += new System.EventHandler(this.изФайлаToolStripMenuItem_Click);
            // 
            // выводДанныхToolStripMenuItem
            // 
            this.выводДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.наЭкранToolStripMenuItem,
            this.вФайлToolStripMenuItem});
            this.выводДанныхToolStripMenuItem.Name = "выводДанныхToolStripMenuItem";
            this.выводДанныхToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.выводДанныхToolStripMenuItem.Text = "Вывод данных";
            // 
            // наЭкранToolStripMenuItem
            // 
            this.наЭкранToolStripMenuItem.Name = "наЭкранToolStripMenuItem";
            this.наЭкранToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.наЭкранToolStripMenuItem.Text = "На экран";
            this.наЭкранToolStripMenuItem.Click += new System.EventHandler(this.наЭкранToolStripMenuItem_Click);
            // 
            // вФайлToolStripMenuItem
            // 
            this.вФайлToolStripMenuItem.Enabled = false;
            this.вФайлToolStripMenuItem.Name = "вФайлToolStripMenuItem";
            this.вФайлToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.вФайлToolStripMenuItem.Text = "В файл";
            this.вФайлToolStripMenuItem.Click += new System.EventHandler(this.вФайлToolStripMenuItem_Click);
            // 
            // решениеToolStripMenuItem
            // 
            this.решениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.северозападныйToolStripMenuItem,
            this.лебедевToolStripMenuItem,
            this.оптимизацияToolStripMenuItem});
            this.решениеToolStripMenuItem.Name = "решениеToolStripMenuItem";
            this.решениеToolStripMenuItem.Size = new System.Drawing.Size(80, 22);
            this.решениеToolStripMenuItem.Text = "Решение";
            // 
            // северозападныйToolStripMenuItem
            // 
            this.северозападныйToolStripMenuItem.Enabled = false;
            this.северозападныйToolStripMenuItem.Name = "северозападныйToolStripMenuItem";
            this.северозападныйToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.северозападныйToolStripMenuItem.Text = "Северо-западный";
            this.северозападныйToolStripMenuItem.Click += new System.EventHandler(this.северозападныйToolStripMenuItem_Click);
            // 
            // лебедевToolStripMenuItem
            // 
            this.лебедевToolStripMenuItem.Enabled = false;
            this.лебедевToolStripMenuItem.Name = "лебедевToolStripMenuItem";
            this.лебедевToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.лебедевToolStripMenuItem.Text = "Лебедев";
            this.лебедевToolStripMenuItem.Click += new System.EventHandler(this.лебедевToolStripMenuItem_Click);
            // 
            // оптимизацияToolStripMenuItem
            // 
            this.оптимизацияToolStripMenuItem.Enabled = false;
            this.оптимизацияToolStripMenuItem.Name = "оптимизацияToolStripMenuItem";
            this.оптимизацияToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.оптимизацияToolStripMenuItem.Text = "Оптимизация";
            this.оптимизацияToolStripMenuItem.Click += new System.EventHandler(this.оптимизацияToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обАвторахToolStripMenuItem,
            this.оДанныхToolStripMenuItem,
            this.оМетодахРешенияToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(76, 22);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // обАвторахToolStripMenuItem
            // 
            this.обАвторахToolStripMenuItem.Name = "обАвторахToolStripMenuItem";
            this.обАвторахToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.обАвторахToolStripMenuItem.Text = "Об авторе";
            this.обАвторахToolStripMenuItem.Click += new System.EventHandler(this.обАвтореToolStripMenuItem_Click);
            // 
            // оДанныхToolStripMenuItem
            // 
            this.оДанныхToolStripMenuItem.Name = "оДанныхToolStripMenuItem";
            this.оДанныхToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.оДанныхToolStripMenuItem.Text = "О данных";
            this.оДанныхToolStripMenuItem.Click += new System.EventHandler(this.оДанныхToolStripMenuItem_Click);
            // 
            // оМетодахРешенияToolStripMenuItem
            // 
            this.оМетодахРешенияToolStripMenuItem.Name = "оМетодахРешенияToolStripMenuItem";
            this.оМетодахРешенияToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.оМетодахРешенияToolStripMenuItem.Text = "О методах решения";
            this.оМетодахРешенияToolStripMenuItem.Click += new System.EventHandler(this.оМетодахРешенияToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(63, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.Location = new System.Drawing.Point(123, 38);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(421, 243);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(49, 38);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(55, 22);
            this.textBox1.TabIndex = 2;
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(49, 66);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(55, 22);
            this.textBox2.TabIndex = 3;
            this.textBox2.Visible = false;
            // 
            // buttonCreateMatrix
            // 
            this.buttonCreateMatrix.Location = new System.Drawing.Point(12, 94);
            this.buttonCreateMatrix.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCreateMatrix.Name = "buttonCreateMatrix";
            this.buttonCreateMatrix.Size = new System.Drawing.Size(93, 30);
            this.buttonCreateMatrix.TabIndex = 10;
            this.buttonCreateMatrix.Text = "Задать";
            this.buttonCreateMatrix.UseVisualStyleBackColor = true;
            this.buttonCreateMatrix.Visible = false;
            this.buttonCreateMatrix.Click += new System.EventHandler(this.buttonCreateMatrix_Click);
            // 
            // buttonConfimDate
            // 
            this.buttonConfimDate.Location = new System.Drawing.Point(16, 292);
            this.buttonConfimDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonConfimDate.Name = "buttonConfimDate";
            this.buttonConfimDate.Size = new System.Drawing.Size(93, 33);
            this.buttonConfimDate.TabIndex = 11;
            this.buttonConfimDate.Text = "Принять";
            this.buttonConfimDate.UseVisualStyleBackColor = true;
            this.buttonConfimDate.Visible = false;
            this.buttonConfimDate.Click += new System.EventHandler(this.buttonConfimDate_Click);
            // 
            // dataGridViewRez
            // 
            this.dataGridViewRez.AllowUserToAddRows = false;
            this.dataGridViewRez.AllowUserToDeleteRows = false;
            this.dataGridViewRez.AllowUserToResizeColumns = false;
            this.dataGridViewRez.AllowUserToResizeRows = false;
            this.dataGridViewRez.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dataGridViewRez.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewRez.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewRez.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRez.ColumnHeadersVisible = false;
            this.dataGridViewRez.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewRez.Location = new System.Drawing.Point(15, 38);
            this.dataGridViewRez.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewRez.Name = "dataGridViewRez";
            this.dataGridViewRez.ReadOnly = true;
            this.dataGridViewRez.RowHeadersVisible = false;
            this.dataGridViewRez.RowTemplate.Height = 24;
            this.dataGridViewRez.Size = new System.Drawing.Size(528, 243);
            this.dataGridViewRez.TabIndex = 14;
            this.dataGridViewRez.Visible = false;
            this.dataGridViewRez.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRez_CellContentClick);
            // 
            // openDialog
            // 
            this.openDialog.FileName = "openFileDialog1";
            this.openDialog.Filter = "таблицы СSV|*.csv|Все файлы|*.*";
            // 
            // saveDialog
            // 
            this.saveDialog.FileName = "table.csv";
            this.saveDialog.Filter = "таблицы СSV|*.csv|Все файлы|*.*";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(124, 302);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(31, 17);
            this.labelInfo.TabIndex = 15;
            this.labelInfo.Text = "info";
            this.labelInfo.Visible = false;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(450, 302);
            this.clearButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(93, 23);
            this.clearButton.TabIndex = 16;
            this.clearButton.Text = "Очистить";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Visible = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // labelN
            // 
            this.labelN.AutoSize = true;
            this.labelN.Location = new System.Drawing.Point(15, 42);
            this.labelN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelN.Name = "labelN";
            this.labelN.Size = new System.Drawing.Size(26, 17);
            this.labelN.TabIndex = 17;
            this.labelN.Text = "N=";
            this.labelN.Visible = false;
            // 
            // labelM
            // 
            this.labelM.AutoSize = true;
            this.labelM.Location = new System.Drawing.Point(15, 70);
            this.labelM.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelM.Name = "labelM";
            this.labelM.Size = new System.Drawing.Size(27, 17);
            this.labelM.TabIndex = 18;
            this.labelM.Text = "M=";
            this.labelM.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(557, 346);
            this.ControlBox = false;
            this.Controls.Add(this.labelM);
            this.Controls.Add(this.labelN);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.dataGridViewRez);
            this.Controls.Add(this.buttonConfimDate);
            this.Controls.Add(this.buttonCreateMatrix);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transper";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRez)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сКлавиатурыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изФайлаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выводДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem наЭкранToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem решениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem северозападныйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem лебедевToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оптимизацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обАвторахToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оМетодахРешенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonCreateMatrix;
        private System.Windows.Forms.Button buttonConfimDate;
        private System.Windows.Forms.DataGridView dataGridViewRez;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label labelN;
        private System.Windows.Forms.Label labelM;
    }
}

