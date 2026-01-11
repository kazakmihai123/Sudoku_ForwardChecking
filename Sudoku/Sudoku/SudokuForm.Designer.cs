namespace Sudoku
{
    partial class SudokuForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnReset = new Button();
            btnSolve = new Button();
            labelGrid = new Label();
            comboBoxGrid = new ComboBox();
            dataGridViewSudoku = new DataGridView();
            labelTitle = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            chkSlowMode = new CheckBox();
            chkUseMRV = new CheckBox();
            btnClear = new Button();
            lblTimer = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)dataGridViewSudoku).BeginInit();
            SuspendLayout();
            // 
            // btnReset
            // 
            btnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnReset.BackColor = Color.Silver;
            btnReset.Cursor = Cursors.Hand;
            btnReset.FlatAppearance.BorderColor = Color.Black;
            btnReset.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReset.ForeColor = Color.Black;
            btnReset.Location = new Point(766, 802);
            btnReset.Margin = new Padding(3, 4, 3, 4);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(78, 38);
            btnReset.TabIndex = 9;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // btnSolve
            // 
            btnSolve.BackColor = Color.FromArgb(145, 142, 244);
            btnSolve.CausesValidation = false;
            btnSolve.Cursor = Cursors.Hand;
            btnSolve.FlatAppearance.BorderColor = Color.Black;
            btnSolve.FlatStyle = FlatStyle.Flat;
            btnSolve.Font = new Font("Trebuchet MS", 10.2F);
            btnSolve.ForeColor = Color.White;
            btnSolve.Location = new Point(55, 98);
            btnSolve.Margin = new Padding(3, 4, 3, 4);
            btnSolve.Name = "btnSolve";
            btnSolve.Size = new Size(100, 59);
            btnSolve.TabIndex = 8;
            btnSolve.Text = "Solve";
            btnSolve.UseVisualStyleBackColor = false;
            btnSolve.Click += btnSolve_Click;
            // 
            // labelGrid
            // 
            labelGrid.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelGrid.AutoSize = true;
            labelGrid.FlatStyle = FlatStyle.Flat;
            labelGrid.Font = new Font("Trebuchet MS", 10.2F);
            labelGrid.ForeColor = Color.Black;
            labelGrid.Location = new Point(693, 99);
            labelGrid.Name = "labelGrid";
            labelGrid.Size = new Size(43, 23);
            labelGrid.TabIndex = 13;
            labelGrid.Text = "Grid";
            // 
            // comboBoxGrid
            // 
            comboBoxGrid.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxGrid.BackColor = Color.WhiteSmoke;
            comboBoxGrid.Cursor = Cursors.Hand;
            comboBoxGrid.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGrid.Font = new Font("Trebuchet MS", 10.2F);
            comboBoxGrid.FormattingEnabled = true;
            comboBoxGrid.Items.AddRange(new object[] { "4 X 4", "9 X 9" });
            comboBoxGrid.Location = new Point(636, 126);
            comboBoxGrid.Margin = new Padding(3, 4, 3, 4);
            comboBoxGrid.Name = "comboBoxGrid";
            comboBoxGrid.Size = new Size(163, 31);
            comboBoxGrid.TabIndex = 11;
            comboBoxGrid.SelectedIndexChanged += comboBoxGrid_SelectedIndexChanged;
            // 
            // dataGridViewSudoku
            // 
            dataGridViewSudoku.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dataGridViewSudoku.BackgroundColor = Color.SeaShell;
            dataGridViewSudoku.BorderStyle = BorderStyle.None;
            dataGridViewSudoku.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSudoku.Location = new Point(138, 200);
            dataGridViewSudoku.Name = "dataGridViewSudoku";
            dataGridViewSudoku.RowHeadersWidth = 51;
            dataGridViewSudoku.Size = new Size(582, 582);
            dataGridViewSudoku.TabIndex = 14;
            // 
            // labelTitle
            // 
            labelTitle.Anchor = AnchorStyles.Top;
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Trebuchet MS", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitle.ForeColor = SystemColors.MenuText;
            labelTitle.Location = new Point(366, 25);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(118, 38);
            labelTitle.TabIndex = 15;
            labelTitle.Text = "Sudoku";
            // 
            // chkSlowMode
            // 
            chkSlowMode.AutoSize = true;
            chkSlowMode.Location = new Point(176, 98);
            chkSlowMode.Name = "chkSlowMode";
            chkSlowMode.Size = new Size(106, 24);
            chkSlowMode.TabIndex = 16;
            chkSlowMode.Text = "Slow mode";
            chkSlowMode.UseVisualStyleBackColor = true;
            // 
            // chkUseMRV
            // 
            chkUseMRV.AutoSize = true;
            chkUseMRV.Location = new Point(176, 133);
            chkUseMRV.Name = "chkUseMRV";
            chkUseMRV.Size = new Size(284, 24);
            chkUseMRV.TabIndex = 17;
            chkUseMRV.Text = "Use MRV(Minimum Remaining Values)";
            chkUseMRV.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClear.BackColor = Color.Silver;
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderColor = Color.Black;
            btnClear.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClear.ForeColor = Color.Black;
            btnClear.Location = new Point(682, 802);
            btnClear.Margin = new Padding(3, 4, 3, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(78, 38);
            btnClear.TabIndex = 18;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTimer.Location = new Point(379, 63);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(0, 25);
            lblTimer.TabIndex = 19;
            // 
            // timer1
            // 
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            // 
            // SudokuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(242, 237, 235);
            ClientSize = new Size(865, 853);
            Controls.Add(lblTimer);
            Controls.Add(btnClear);
            Controls.Add(chkUseMRV);
            Controls.Add(chkSlowMode);
            Controls.Add(labelTitle);
            Controls.Add(dataGridViewSudoku);
            Controls.Add(labelGrid);
            Controls.Add(comboBoxGrid);
            Controls.Add(btnReset);
            Controls.Add(btnSolve);
            Name = "SudokuForm";
            Text = "Sudoku";
            Load += SudokuForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewSudoku).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnReset;
        private Button btnSolve;
        private Label labelGrid;
        private ComboBox comboBoxGrid;
        private DataGridView dataGridViewSudoku;
        private Label labelTitle;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private CheckBox chkSlowMode;
        private CheckBox chkUseMRV;
        private Button btnClear;
        private Label lblTimer;
        private System.Windows.Forms.Timer timer1;
    }
}
