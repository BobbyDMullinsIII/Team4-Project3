namespace Team4_Project2
{
    partial class GUIForm
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
            this.MNAWLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assemblyPanel = new System.Windows.Forms.Panel();
            this.saveAssemblyButton = new System.Windows.Forms.Button();
            this.clearAssemblyButton = new System.Windows.Forms.Button();
            this.loadAssemblyButton = new System.Windows.Forms.Button();
            this.assemblyTextBox = new System.Windows.Forms.TextBox();
            this.assemblyLabel = new System.Windows.Forms.Label();
            this.simulationPanel = new System.Windows.Forms.Panel();
            this.cyclesLabel = new System.Windows.Forms.Label();
            this.counterTextBox = new System.Windows.Forms.RichTextBox();
            this.executeTextBox = new System.Windows.Forms.TextBox();
            this.storeTextBox = new System.Windows.Forms.TextBox();
            this.decodeTextBox = new System.Windows.Forms.TextBox();
            this.instructOneText = new System.Windows.Forms.TextBox();
            this.nextCycleButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.storeLabel = new System.Windows.Forms.Label();
            this.executeLabel = new System.Windows.Forms.Label();
            this.decodeLabel = new System.Windows.Forms.Label();
            this.fetchLabel = new System.Windows.Forms.Label();
            this.staticPipeLabel = new System.Windows.Forms.Label();
            this.savePipelineOutputButton = new System.Windows.Forms.Button();
            this.clearPipelineOutputButton = new System.Windows.Forms.Button();
            this.loadPipelineOutputButton = new System.Windows.Forms.Button();
            this.pipelineOutputTextBox = new System.Windows.Forms.TextBox();
            this.pipelineOutputLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.assemblyPanel.SuspendLayout();
            this.simulationPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MNAWLabel
            // 
            this.MNAWLabel.AutoSize = true;
            this.MNAWLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MNAWLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MNAWLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MNAWLabel.Location = new System.Drawing.Point(623, 35);
            this.MNAWLabel.Name = "MNAWLabel";
            this.MNAWLabel.Size = new System.Drawing.Size(339, 33);
            this.MNAWLabel.TabIndex = 0;
            this.MNAWLabel.Text = "MNAW Instruction Set GUI";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1584, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instructionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.filesToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.filesToolStripMenuItem.Text = "File";
            // 
            // instructionsToolStripMenuItem
            // 
            this.instructionsToolStripMenuItem.Name = "instructionsToolStripMenuItem";
            this.instructionsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.instructionsToolStripMenuItem.Text = "Information";
            this.instructionsToolStripMenuItem.Click += new System.EventHandler(this.informationToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // assemblyPanel
            // 
            this.assemblyPanel.BackColor = System.Drawing.Color.Black;
            this.assemblyPanel.Controls.Add(this.saveAssemblyButton);
            this.assemblyPanel.Controls.Add(this.clearAssemblyButton);
            this.assemblyPanel.Controls.Add(this.loadAssemblyButton);
            this.assemblyPanel.Controls.Add(this.assemblyTextBox);
            this.assemblyPanel.Controls.Add(this.assemblyLabel);
            this.assemblyPanel.Location = new System.Drawing.Point(0, 79);
            this.assemblyPanel.Name = "assemblyPanel";
            this.assemblyPanel.Size = new System.Drawing.Size(312, 604);
            this.assemblyPanel.TabIndex = 2;
            // 
            // saveAssemblyButton
            // 
            this.saveAssemblyButton.BackColor = System.Drawing.Color.Silver;
            this.saveAssemblyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveAssemblyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.saveAssemblyButton.ForeColor = System.Drawing.Color.Black;
            this.saveAssemblyButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.saveAssemblyButton.Location = new System.Drawing.Point(235, 461);
            this.saveAssemblyButton.Margin = new System.Windows.Forms.Padding(1);
            this.saveAssemblyButton.Name = "saveAssemblyButton";
            this.saveAssemblyButton.Size = new System.Drawing.Size(70, 40);
            this.saveAssemblyButton.TabIndex = 16;
            this.saveAssemblyButton.Text = "Save";
            this.saveAssemblyButton.UseVisualStyleBackColor = false;
            this.saveAssemblyButton.Click += new System.EventHandler(this.saveAssemblyButton_Click);
            // 
            // clearAssemblyButton
            // 
            this.clearAssemblyButton.BackColor = System.Drawing.Color.Silver;
            this.clearAssemblyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearAssemblyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.clearAssemblyButton.ForeColor = System.Drawing.Color.Black;
            this.clearAssemblyButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.clearAssemblyButton.Location = new System.Drawing.Point(12, 461);
            this.clearAssemblyButton.Margin = new System.Windows.Forms.Padding(1);
            this.clearAssemblyButton.Name = "clearAssemblyButton";
            this.clearAssemblyButton.Size = new System.Drawing.Size(140, 40);
            this.clearAssemblyButton.TabIndex = 15;
            this.clearAssemblyButton.Text = "Clear";
            this.clearAssemblyButton.UseVisualStyleBackColor = false;
            this.clearAssemblyButton.Click += new System.EventHandler(this.clearAssemblyButton_Click);
            // 
            // loadAssemblyButton
            // 
            this.loadAssemblyButton.BackColor = System.Drawing.Color.Silver;
            this.loadAssemblyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadAssemblyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.loadAssemblyButton.ForeColor = System.Drawing.Color.Black;
            this.loadAssemblyButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.loadAssemblyButton.Location = new System.Drawing.Point(163, 461);
            this.loadAssemblyButton.Margin = new System.Windows.Forms.Padding(1);
            this.loadAssemblyButton.Name = "loadAssemblyButton";
            this.loadAssemblyButton.Size = new System.Drawing.Size(70, 40);
            this.loadAssemblyButton.TabIndex = 14;
            this.loadAssemblyButton.Text = "Load";
            this.loadAssemblyButton.UseVisualStyleBackColor = false;
            this.loadAssemblyButton.Click += new System.EventHandler(this.loadAssemblyButton_Click);
            // 
            // assemblyTextBox
            // 
            this.assemblyTextBox.BackColor = System.Drawing.Color.White;
            this.assemblyTextBox.ForeColor = System.Drawing.Color.Black;
            this.assemblyTextBox.Location = new System.Drawing.Point(12, 37);
            this.assemblyTextBox.Multiline = true;
            this.assemblyTextBox.Name = "assemblyTextBox";
            this.assemblyTextBox.Size = new System.Drawing.Size(293, 420);
            this.assemblyTextBox.TabIndex = 6;
            this.assemblyTextBox.TextChanged += new System.EventHandler(this.assemblyTextBox_TextChanged);
            // 
            // assemblyLabel
            // 
            this.assemblyLabel.AutoSize = true;
            this.assemblyLabel.BackColor = System.Drawing.Color.Black;
            this.assemblyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.assemblyLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.assemblyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assemblyLabel.Location = new System.Drawing.Point(17, 1);
            this.assemblyLabel.Name = "assemblyLabel";
            this.assemblyLabel.Size = new System.Drawing.Size(282, 33);
            this.assemblyLabel.TabIndex = 5;
            this.assemblyLabel.Text = "Assembly Instructions";
            // 
            // simulationPanel
            // 
            this.simulationPanel.BackColor = System.Drawing.Color.Black;
            this.simulationPanel.Controls.Add(this.cyclesLabel);
            this.simulationPanel.Controls.Add(this.counterTextBox);
            this.simulationPanel.Controls.Add(this.executeTextBox);
            this.simulationPanel.Controls.Add(this.storeTextBox);
            this.simulationPanel.Controls.Add(this.decodeTextBox);
            this.simulationPanel.Controls.Add(this.instructOneText);
            this.simulationPanel.Controls.Add(this.nextCycleButton);
            this.simulationPanel.Controls.Add(this.startButton);
            this.simulationPanel.Controls.Add(this.storeLabel);
            this.simulationPanel.Controls.Add(this.executeLabel);
            this.simulationPanel.Controls.Add(this.decodeLabel);
            this.simulationPanel.Controls.Add(this.fetchLabel);
            this.simulationPanel.Controls.Add(this.staticPipeLabel);
            this.simulationPanel.Location = new System.Drawing.Point(318, 79);
            this.simulationPanel.Name = "simulationPanel";
            this.simulationPanel.Size = new System.Drawing.Size(946, 604);
            this.simulationPanel.TabIndex = 3;
            // 
            // cyclesLabel
            // 
            this.cyclesLabel.AutoSize = true;
            this.cyclesLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cyclesLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cyclesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cyclesLabel.Location = new System.Drawing.Point(767, 7);
            this.cyclesLabel.Name = "cyclesLabel";
            this.cyclesLabel.Size = new System.Drawing.Size(139, 27);
            this.cyclesLabel.TabIndex = 36;
            this.cyclesLabel.Text = "Cycle Counter";
            // 
            // counterTextBox
            // 
            this.counterTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.counterTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.counterTextBox.Location = new System.Drawing.Point(736, 37);
            this.counterTextBox.Name = "counterTextBox";
            this.counterTextBox.Size = new System.Drawing.Size(200, 75);
            this.counterTextBox.TabIndex = 35;
            this.counterTextBox.Text = "0";
            // 
            // executeTextBox
            // 
            this.executeTextBox.Location = new System.Drawing.Point(12, 193);
            this.executeTextBox.Name = "executeTextBox";
            this.executeTextBox.ReadOnly = true;
            this.executeTextBox.Size = new System.Drawing.Size(250, 20);
            this.executeTextBox.TabIndex = 34;
            // 
            // storeTextBox
            // 
            this.storeTextBox.Location = new System.Drawing.Point(12, 261);
            this.storeTextBox.Name = "storeTextBox";
            this.storeTextBox.ReadOnly = true;
            this.storeTextBox.Size = new System.Drawing.Size(250, 20);
            this.storeTextBox.TabIndex = 33;
            // 
            // decodeTextBox
            // 
            this.decodeTextBox.Location = new System.Drawing.Point(12, 120);
            this.decodeTextBox.Name = "decodeTextBox";
            this.decodeTextBox.ReadOnly = true;
            this.decodeTextBox.Size = new System.Drawing.Size(250, 20);
            this.decodeTextBox.TabIndex = 32;
            // 
            // instructOneText
            // 
            this.instructOneText.Location = new System.Drawing.Point(12, 49);
            this.instructOneText.Name = "instructOneText";
            this.instructOneText.ReadOnly = true;
            this.instructOneText.Size = new System.Drawing.Size(250, 20);
            this.instructOneText.TabIndex = 19;
            // 
            // nextCycleButton
            // 
            this.nextCycleButton.BackColor = System.Drawing.Color.Silver;
            this.nextCycleButton.Enabled = false;
            this.nextCycleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextCycleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.nextCycleButton.ForeColor = System.Drawing.Color.Black;
            this.nextCycleButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.nextCycleButton.Location = new System.Drawing.Point(736, 517);
            this.nextCycleButton.Margin = new System.Windows.Forms.Padding(1);
            this.nextCycleButton.Name = "nextCycleButton";
            this.nextCycleButton.Size = new System.Drawing.Size(200, 75);
            this.nextCycleButton.TabIndex = 18;
            this.nextCycleButton.Text = "Next Cycle";
            this.nextCycleButton.UseVisualStyleBackColor = false;
            this.nextCycleButton.Click += new System.EventHandler(this.nextCycleButton_Click);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.Silver;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.startButton.ForeColor = System.Drawing.Color.Black;
            this.startButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.startButton.Location = new System.Drawing.Point(12, 517);
            this.startButton.Margin = new System.Windows.Forms.Padding(1);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(200, 75);
            this.startButton.TabIndex = 17;
            this.startButton.Text = "Start Simulation";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // storeLabel
            // 
            this.storeLabel.AutoSize = true;
            this.storeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.storeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.storeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeLabel.Location = new System.Drawing.Point(12, 284);
            this.storeLabel.Name = "storeLabel";
            this.storeLabel.Size = new System.Drawing.Size(61, 27);
            this.storeLabel.TabIndex = 7;
            this.storeLabel.Text = "Store";
            // 
            // executeLabel
            // 
            this.executeLabel.AutoSize = true;
            this.executeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.executeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.executeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.executeLabel.Location = new System.Drawing.Point(12, 216);
            this.executeLabel.Name = "executeLabel";
            this.executeLabel.Size = new System.Drawing.Size(85, 27);
            this.executeLabel.TabIndex = 6;
            this.executeLabel.Text = "Execute";
            // 
            // decodeLabel
            // 
            this.decodeLabel.AutoSize = true;
            this.decodeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.decodeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.decodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decodeLabel.Location = new System.Drawing.Point(12, 143);
            this.decodeLabel.Name = "decodeLabel";
            this.decodeLabel.Size = new System.Drawing.Size(82, 27);
            this.decodeLabel.TabIndex = 5;
            this.decodeLabel.Text = "Decode";
            // 
            // fetchLabel
            // 
            this.fetchLabel.AutoSize = true;
            this.fetchLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fetchLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fetchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fetchLabel.Location = new System.Drawing.Point(12, 72);
            this.fetchLabel.Name = "fetchLabel";
            this.fetchLabel.Size = new System.Drawing.Size(63, 27);
            this.fetchLabel.TabIndex = 4;
            this.fetchLabel.Text = "Fetch";
            // 
            // staticPipeLabel
            // 
            this.staticPipeLabel.AutoSize = true;
            this.staticPipeLabel.BackColor = System.Drawing.Color.Black;
            this.staticPipeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.staticPipeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.staticPipeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staticPipeLabel.Location = new System.Drawing.Point(3, 1);
            this.staticPipeLabel.Name = "staticPipeLabel";
            this.staticPipeLabel.Size = new System.Drawing.Size(321, 33);
            this.staticPipeLabel.TabIndex = 4;
            this.staticPipeLabel.Text = "Static Pipeline Simulation";
            // 
            // savePipelineOutputButton
            // 
            this.savePipelineOutputButton.BackColor = System.Drawing.Color.Silver;
            this.savePipelineOutputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savePipelineOutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.savePipelineOutputButton.ForeColor = System.Drawing.Color.Black;
            this.savePipelineOutputButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.savePipelineOutputButton.Location = new System.Drawing.Point(235, 461);
            this.savePipelineOutputButton.Margin = new System.Windows.Forms.Padding(1);
            this.savePipelineOutputButton.Name = "savePipelineOutputButton";
            this.savePipelineOutputButton.Size = new System.Drawing.Size(70, 40);
            this.savePipelineOutputButton.TabIndex = 16;
            this.savePipelineOutputButton.Text = "Save";
            this.savePipelineOutputButton.UseVisualStyleBackColor = false;
            this.savePipelineOutputButton.Click += new System.EventHandler(this.savePipelineOutputButton_Click);
            // 
            // clearPipelineOutputButton
            // 
            this.clearPipelineOutputButton.BackColor = System.Drawing.Color.Silver;
            this.clearPipelineOutputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearPipelineOutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.clearPipelineOutputButton.ForeColor = System.Drawing.Color.Black;
            this.clearPipelineOutputButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.clearPipelineOutputButton.Location = new System.Drawing.Point(12, 461);
            this.clearPipelineOutputButton.Margin = new System.Windows.Forms.Padding(1);
            this.clearPipelineOutputButton.Name = "clearPipelineOutputButton";
            this.clearPipelineOutputButton.Size = new System.Drawing.Size(140, 40);
            this.clearPipelineOutputButton.TabIndex = 15;
            this.clearPipelineOutputButton.Text = "Clear";
            this.clearPipelineOutputButton.UseVisualStyleBackColor = false;
            this.clearPipelineOutputButton.Click += new System.EventHandler(this.clearPipelineOutputButton_Click);
            // 
            // loadPipelineOutputButton
            // 
            this.loadPipelineOutputButton.BackColor = System.Drawing.Color.Silver;
            this.loadPipelineOutputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadPipelineOutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.loadPipelineOutputButton.ForeColor = System.Drawing.Color.Black;
            this.loadPipelineOutputButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.loadPipelineOutputButton.Location = new System.Drawing.Point(163, 461);
            this.loadPipelineOutputButton.Margin = new System.Windows.Forms.Padding(1);
            this.loadPipelineOutputButton.Name = "loadPipelineOutputButton";
            this.loadPipelineOutputButton.Size = new System.Drawing.Size(70, 40);
            this.loadPipelineOutputButton.TabIndex = 14;
            this.loadPipelineOutputButton.Text = "Load";
            this.loadPipelineOutputButton.UseVisualStyleBackColor = false;
            this.loadPipelineOutputButton.Click += new System.EventHandler(this.loadPipelineOutputButton_Click);
            // 
            // pipelineOutputTextBox
            // 
            this.pipelineOutputTextBox.BackColor = System.Drawing.Color.White;
            this.pipelineOutputTextBox.ForeColor = System.Drawing.Color.Black;
            this.pipelineOutputTextBox.Location = new System.Drawing.Point(12, 37);
            this.pipelineOutputTextBox.Multiline = true;
            this.pipelineOutputTextBox.Name = "pipelineOutputTextBox";
            this.pipelineOutputTextBox.Size = new System.Drawing.Size(293, 420);
            this.pipelineOutputTextBox.TabIndex = 6;
            this.pipelineOutputTextBox.TextChanged += new System.EventHandler(this.pipelineOutputTextBox_TextChanged);
            // 
            // pipelineOutputLabel
            // 
            this.pipelineOutputLabel.AutoSize = true;
            this.pipelineOutputLabel.BackColor = System.Drawing.Color.Black;
            this.pipelineOutputLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pipelineOutputLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pipelineOutputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pipelineOutputLabel.Location = new System.Drawing.Point(58, 1);
            this.pipelineOutputLabel.Name = "pipelineOutputLabel";
            this.pipelineOutputLabel.Size = new System.Drawing.Size(201, 33);
            this.pipelineOutputLabel.TabIndex = 5;
            this.pipelineOutputLabel.Text = "Pipeline Output";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.savePipelineOutputButton);
            this.panel1.Controls.Add(this.clearPipelineOutputButton);
            this.panel1.Controls.Add(this.loadPipelineOutputButton);
            this.panel1.Controls.Add(this.pipelineOutputTextBox);
            this.panel1.Controls.Add(this.pipelineOutputLabel);
            this.panel1.Location = new System.Drawing.Point(1272, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 604);
            this.panel1.TabIndex = 17;
            // 
            // GUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1584, 681);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.simulationPanel);
            this.Controls.Add(this.assemblyPanel);
            this.Controls.Add(this.MNAWLabel);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Static Pipeline Simulation";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.assemblyPanel.ResumeLayout(false);
            this.assemblyPanel.PerformLayout();
            this.simulationPanel.ResumeLayout(false);
            this.simulationPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MNAWLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instructionsToolStripMenuItem;
        private System.Windows.Forms.Panel assemblyPanel;
        private System.Windows.Forms.Panel simulationPanel;
        private System.Windows.Forms.Label staticPipeLabel;
        private System.Windows.Forms.Label assemblyLabel;
        private System.Windows.Forms.TextBox assemblyTextBox;
        private System.Windows.Forms.Button saveAssemblyButton;
        private System.Windows.Forms.Button clearAssemblyButton;
        private System.Windows.Forms.Button loadAssemblyButton;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label storeLabel;
        private System.Windows.Forms.Label executeLabel;
        private System.Windows.Forms.Label decodeLabel;
        private System.Windows.Forms.Label fetchLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button nextCycleButton;
        private System.Windows.Forms.Button savePipelineOutputButton;
        private System.Windows.Forms.Button clearPipelineOutputButton;
        private System.Windows.Forms.Button loadPipelineOutputButton;
        private System.Windows.Forms.TextBox pipelineOutputTextBox;
        private System.Windows.Forms.Label pipelineOutputLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox executeTextBox;
        private System.Windows.Forms.TextBox storeTextBox;
        private System.Windows.Forms.TextBox decodeTextBox;
        private System.Windows.Forms.TextBox instructOneText;
        public System.Windows.Forms.RichTextBox counterTextBox;
        private System.Windows.Forms.Label cyclesLabel;
    }
}

