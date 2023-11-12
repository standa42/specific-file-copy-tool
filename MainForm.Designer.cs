namespace Tridic
{
    partial class MainForm
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
            ProgressBar = new ProgressBar();
            TargetFolderTextBox = new TextBox();
            StatusLabel = new Label();
            SourceFolderButton = new Button();
            TargetFolderButton = new Button();
            ReadProgressTimer = new System.Windows.Forms.Timer(components);
            SourceFolderTextBox = new TextBox();
            StartButton = new Button();
            LogListBox = new ListBox();
            DescriptionLabel = new Label();
            DescriptionLabel2 = new Label();
            DescriptionLabel3 = new Label();
            SuspendLayout();
            // 
            // ProgressBar
            // 
            ProgressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ProgressBar.Location = new Point(12, 304);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(855, 23);
            ProgressBar.TabIndex = 1;
            // 
            // TargetFolderTextBox
            // 
            TargetFolderTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TargetFolderTextBox.Location = new Point(11, 175);
            TargetFolderTextBox.Name = "TargetFolderTextBox";
            TargetFolderTextBox.ReadOnly = true;
            TargetFolderTextBox.Size = new Size(856, 23);
            TargetFolderTextBox.TabIndex = 5;
            TargetFolderTextBox.TextChanged += TargetFolderTextBox_TextChanged;
            // 
            // StatusLabel
            // 
            StatusLabel.AutoSize = true;
            StatusLabel.Location = new Point(12, 275);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(42, 15);
            StatusLabel.TabIndex = 6;
            StatusLabel.Text = "Status:";
            // 
            // SourceFolderButton
            // 
            SourceFolderButton.Location = new Point(11, 83);
            SourceFolderButton.Name = "SourceFolderButton";
            SourceFolderButton.Size = new Size(196, 23);
            SourceFolderButton.TabIndex = 7;
            SourceFolderButton.Text = "Vybrat výchozí složku (odkud)";
            SourceFolderButton.UseVisualStyleBackColor = true;
            SourceFolderButton.Click += SourceFolderButton_Click;
            // 
            // TargetFolderButton
            // 
            TargetFolderButton.Location = new Point(11, 146);
            TargetFolderButton.Name = "TargetFolderButton";
            TargetFolderButton.Size = new Size(196, 23);
            TargetFolderButton.TabIndex = 8;
            TargetFolderButton.Text = "Vybrat cílovou složku (kam)";
            TargetFolderButton.UseVisualStyleBackColor = true;
            TargetFolderButton.Click += TargetFolderButton_Click;
            // 
            // ReadProgressTimer
            // 
            ReadProgressTimer.Enabled = true;
            ReadProgressTimer.Interval = 400;
            ReadProgressTimer.Tick += ReadProgressTimer_Tick;
            // 
            // SourceFolderTextBox
            // 
            SourceFolderTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SourceFolderTextBox.Location = new Point(11, 112);
            SourceFolderTextBox.Name = "SourceFolderTextBox";
            SourceFolderTextBox.ReadOnly = true;
            SourceFolderTextBox.Size = new Size(856, 23);
            SourceFolderTextBox.TabIndex = 9;
            SourceFolderTextBox.TextChanged += SourceFolderTextBox_TextChanged;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(11, 204);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(75, 23);
            StartButton.TabIndex = 10;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // LogListBox
            // 
            LogListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LogListBox.FormattingEnabled = true;
            LogListBox.ItemHeight = 15;
            LogListBox.Location = new Point(11, 333);
            LogListBox.Name = "LogListBox";
            LogListBox.Size = new Size(856, 439);
            LogListBox.TabIndex = 11;
            // 
            // DescriptionLabel
            // 
            DescriptionLabel.AutoSize = true;
            DescriptionLabel.Location = new Point(12, 9);
            DescriptionLabel.Name = "DescriptionLabel";
            DescriptionLabel.Size = new Size(602, 15);
            DescriptionLabel.TabIndex = 12;
            DescriptionLabel.Text = "Kopíruje soubory z výchozí složky do cílové složky v předepsaném formátu. Pokud soubor již existuje, nahradí ho.";
            // 
            // DescriptionLabel2
            // 
            DescriptionLabel2.AutoSize = true;
            DescriptionLabel2.Location = new Point(12, 24);
            DescriptionLabel2.Name = "DescriptionLabel2";
            DescriptionLabel2.Size = new Size(434, 15);
            DescriptionLabel2.TabIndex = 13;
            DescriptionLabel2.Text = "Z výchozí složky bere pouze soubory, které jsou přímo v ní (nečte vnořené složky)";
            DescriptionLabel2.Click += label1_Click;
            // 
            // DescriptionLabel3
            // 
            DescriptionLabel3.AutoSize = true;
            DescriptionLabel3.Location = new Point(11, 39);
            DescriptionLabel3.Name = "DescriptionLabel3";
            DescriptionLabel3.Size = new Size(560, 15);
            DescriptionLabel3.TabIndex = 14;
            DescriptionLabel3.Text = "Transformuje následovně: 005-151-980-1.stp => \\005\\005-1\\005-15\\005-151\\005-151-9\\005-151-980-1.stp";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(879, 784);
            Controls.Add(DescriptionLabel3);
            Controls.Add(DescriptionLabel2);
            Controls.Add(DescriptionLabel);
            Controls.Add(LogListBox);
            Controls.Add(StartButton);
            Controls.Add(SourceFolderTextBox);
            Controls.Add(TargetFolderButton);
            Controls.Add(SourceFolderButton);
            Controls.Add(StatusLabel);
            Controls.Add(TargetFolderTextBox);
            Controls.Add(ProgressBar);
            Name = "MainForm";
            Text = "Tridic";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ProgressBar ProgressBar;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label DescriptionLabel2;
        private Label label2;
        private TextBox SourceFolderTextBox;
        private TextBox TargetFolderTextBox;
        private Label StatusLabel;
        private Button SourceFolderButton;
        private Button TargetFolderButton;
        private System.Windows.Forms.Timer ReadProgressTimer;
        private Button StartButton;
        private ListBox LogListBox;
        private Label DescriptionLabel;
        private Label DescriptionLabel3;
    }
}