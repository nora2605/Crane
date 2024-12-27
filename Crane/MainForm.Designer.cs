
namespace Crane
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.basePanel = new System.Windows.Forms.Panel();
            this.cranePanel = new System.Windows.Forms.Panel();
            this.cranePanel2 = new System.Windows.Forms.Panel();
            this.hookPanel = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.crate = new System.Windows.Forms.PictureBox();
            this.labelTut = new System.Windows.Forms.Label();
            this.ground = new System.Windows.Forms.Panel();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.scorePanel = new System.Windows.Forms.Panel();
            this.drill = new System.Windows.Forms.PictureBox();
            this.youwonyay = new System.Windows.Forms.Label();
            this.scoreTView = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.crate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drill)).BeginInit();
            this.SuspendLayout();
            // 
            // basePanel
            // 
            this.basePanel.BackColor = System.Drawing.Color.DimGray;
            this.basePanel.Location = new System.Drawing.Point(300, 400);
            this.basePanel.Name = "basePanel";
            this.basePanel.Size = new System.Drawing.Size(200, 50);
            this.basePanel.TabIndex = 0;
            // 
            // cranePanel
            // 
            this.cranePanel.BackColor = System.Drawing.Color.Yellow;
            this.cranePanel.Location = new System.Drawing.Point(375, 200);
            this.cranePanel.Name = "cranePanel";
            this.cranePanel.Size = new System.Drawing.Size(50, 200);
            this.cranePanel.TabIndex = 1;
            // 
            // cranePanel2
            // 
            this.cranePanel2.BackColor = System.Drawing.Color.DarkOrange;
            this.cranePanel2.Location = new System.Drawing.Point(225, 150);
            this.cranePanel2.Name = "cranePanel2";
            this.cranePanel2.Size = new System.Drawing.Size(200, 50);
            this.cranePanel2.TabIndex = 2;
            // 
            // hookPanel
            // 
            this.hookPanel.BackColor = System.Drawing.Color.Black;
            this.hookPanel.Location = new System.Drawing.Point(225, 200);
            this.hookPanel.Name = "hookPanel";
            this.hookPanel.Size = new System.Drawing.Size(25, 50);
            this.hookPanel.TabIndex = 3;
            // 
            // timer
            // 
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // crate
            // 
            this.crate.Image = ((System.Drawing.Image)(resources.GetObject("crate.Image")));
            this.crate.Location = new System.Drawing.Point(175, 350);
            this.crate.Name = "crate";
            this.crate.Size = new System.Drawing.Size(100, 100);
            this.crate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.crate.TabIndex = 4;
            this.crate.TabStop = false;
            // 
            // labelTut
            // 
            this.labelTut.AutoSize = true;
            this.labelTut.ForeColor = System.Drawing.Color.DarkRed;
            this.labelTut.Location = new System.Drawing.Point(603, 9);
            this.labelTut.Name = "labelTut";
            this.labelTut.Size = new System.Drawing.Size(135, 65);
            this.labelTut.TabIndex = 5;
            this.labelTut.Text = "Controls:\r\nW/S - up/down\r\nA/D - extend/retract bridge\r\nR/F - extend/retract hook\r" +
    "\nG - attach/drop crate";
            // 
            // ground
            // 
            this.ground.BackColor = System.Drawing.Color.SpringGreen;
            this.ground.Location = new System.Drawing.Point(0, 450);
            this.ground.Name = "ground";
            this.ground.Size = new System.Drawing.Size(800, 25);
            this.ground.TabIndex = 6;
            this.ground.Tag = "Collider";
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Location = new System.Drawing.Point(13, 9);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(47, 13);
            this.scoreLabel.TabIndex = 7;
            this.scoreLabel.Text = "Score: 0";
            this.scoreLabel.Visible = false;
            // 
            // scorePanel
            // 
            this.scorePanel.BackColor = System.Drawing.Color.Red;
            this.scorePanel.Location = new System.Drawing.Point(530, 350);
            this.scorePanel.Name = "scorePanel";
            this.scorePanel.Size = new System.Drawing.Size(100, 100);
            this.scorePanel.TabIndex = 8;
            // 
            // drill
            // 
            this.drill.BackColor = System.Drawing.Color.Transparent;
            this.drill.Image = ((System.Drawing.Image)(resources.GetObject("drill.Image")));
            this.drill.Location = new System.Drawing.Point(175, 350);
            this.drill.Name = "drill";
            this.drill.Size = new System.Drawing.Size(102, 100);
            this.drill.TabIndex = 10;
            this.drill.TabStop = false;
            this.drill.Visible = false;
            // 
            // youwonyay
            // 
            this.youwonyay.AutoSize = true;
            this.youwonyay.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.youwonyay.Location = new System.Drawing.Point(215, 9);
            this.youwonyay.Name = "youwonyay";
            this.youwonyay.Size = new System.Drawing.Size(311, 55);
            this.youwonyay.TabIndex = 11;
            this.youwonyay.Text = "You won yay";
            this.youwonyay.Visible = false;
            // 
            // scoreTView
            // 
            this.scoreTView.HideSelection = false;
            this.scoreTView.Location = new System.Drawing.Point(651, 350);
            this.scoreTView.Name = "scoreTView";
            this.scoreTView.Size = new System.Drawing.Size(121, 100);
            this.scoreTView.TabIndex = 12;
            this.scoreTView.UseCompatibleStateImageBehavior = false;
            this.scoreTView.View = System.Windows.Forms.View.List;
            this.scoreTView.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.scoreTView);
            this.Controls.Add(this.youwonyay);
            this.Controls.Add(this.drill);
            this.Controls.Add(this.crate);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.ground);
            this.Controls.Add(this.labelTut);
            this.Controls.Add(this.hookPanel);
            this.Controls.Add(this.cranePanel2);
            this.Controls.Add(this.cranePanel);
            this.Controls.Add(this.basePanel);
            this.Controls.Add(this.scorePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Craen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.crate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drill)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel basePanel;
        private System.Windows.Forms.Panel cranePanel;
        private System.Windows.Forms.Panel cranePanel2;
        private System.Windows.Forms.Panel hookPanel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox crate;
        private System.Windows.Forms.Label labelTut;
        private System.Windows.Forms.Panel ground;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Panel scorePanel;
        private System.Windows.Forms.PictureBox drill;
        private System.Windows.Forms.Label youwonyay;
        private System.Windows.Forms.ListView scoreTView;
    }
}

