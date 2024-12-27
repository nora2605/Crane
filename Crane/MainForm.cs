using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace Crane
{
    public partial class MainForm : Form
    {
        bool grab = false; //if crate is grabbed
        bool muted = false; //if music is muted
        bool flipped = false; //if crane is flipped in direction
        byte moving = 0; //if crane is currently moving (and in which mode)
        int[] origins = { 200, 200, 50 }; //original widths and heights of the crane parts
        int score = 0;
        int[] woScore = new int[4];
        int offset = 0; //shift of transform
        int lGravity = 5;

        int phase = 0; //phase of keys revealed
        bool restrc = true; //restricted/free gameplay

        bool drilled = false; //if the drill was used before

        SoundPlayer bgm = new SoundPlayer(); //background music player

        private Action<Keys>[] phases; //phases

        public MainForm()
        {
            InitializeComponent();
            InitPhases();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //Init Discord RPC
            InitializeClient();

            //enable timer
            timer.Enabled = true;

            this.LostFocus += (object s, EventArgs a) =>
            {
                this.Focus();
            };

            //get embedded background music as IO Stream
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resNames = asm.GetManifestResourceNames();
            Stream bgmStream = asm.GetManifestResourceStream(Array.Find<string>(resNames, (string rn) => rn.Contains("bgm.wav")));
            bgm.Stream = bgmStream;
            bgm.PlayLooping();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            updateScoreLabel();
            if (moving != 0) return;
            for (int i = 0; i <= (restrc ? phase : 5); i++)
                phases[i](e.KeyCode);
            UpdatePresence();
        }


        private void Eval(string hax) //hax
        {
            string[] sHax = hax.Split(' ');
            string cmd = sHax[0];
            List<string> args = sHax.ToList<string>();
            args.RemoveAt(0);

            switch (cmd)
            {
                case "score":
                    if (args[0] == "add")
                        try
                        {
                            int sc = Convert.ToInt32(args[1]);
                            for (int i = 0, l = Math.Min(100 - score, sc); i < l; i++)
                            {
                                Score(ScoreType.Hax);
                                sc--;
                            }
                            score += sc;
                            woScore[(int)ScoreType.Hax] += sc;
                            updateScoreLabel();
                        }
                        catch { }
                    break;
                case "gravity":
                    if (args[0] == "set")
                        try
                        {
                            lGravity = Convert.ToInt32(args[1]);
                        }
                        catch { }
                    break;
                case "modo":
                    if (args[0] == "restricted")
                    {
                        restrc = true;
                    }
                    else if (args[0] == "unrestricted" || args[0] == "free")
                    {
                        restrc = false;
                    }
                    break;
                case "vis":
                    if (args[0] == "list")
                        scoreTView.Visible = !scoreTView.Visible;
                    if (args[0] == "ywy")
                        youwonyay.Visible = !youwonyay.Visible;
                    break;
            }
        }

        private bool rectCollision(Rectangle p1, Rectangle p2) //Returns if 2 Rectangles collide
        {
            return
                p1.Height + p1.Top >= p2.Top &&
                p1.Top <= p2.Top + p2.Height &&
                p1.Left + p1.Width >= p2.Left &&
                p1.Left <= p2.Left + p2.Width;
        }

        private bool groundCollision(Rectangle p1) //returns if a rectangle touches a ground tile
        {
            bool bc = false;
            foreach (Control c in Controls)
            {
                if ((string)c.Tag == "Collider")
                    if (!bc) bc = rectCollision(p1, toRect(c));
            }
            return bc;
        }

        private bool borderCollision(Rectangle p1, Size s) //returns if boundary is touched by a rectangle
        {
            return
                p1.Left <= 0 ||
                p1.Left + p1.Width >= s.Width ||
                p1.Top <= 0 ||
                p1.Top + p1.Height >= ground.Top;
        }

        private Rectangle toRect(Control p) => new Rectangle(p.Left, p.Top, p.Width, p.Height); //converts a control to a rectangle

        private void moveH(int amount) //moves the bridge
        {
            int aAmount = flipped ? -amount : amount;
            if (moving == 0 && !canMove(toRect(cranePanel2), false, aAmount)) return;
            if (grab) crate.Left -= amount;
            if (!flipped) cranePanel2.Left -= amount;
            cranePanel2.Width += aAmount;
            hookPanel.Left -= amount;
            if (cranePanel2.Width <= 50 && moving == 0)
            {
                FlipH(amount);
            }
        }

        private void moveV(int amount) //extends/retracts the main tower
        {
            if (moving == 0)
            {
                if (!canMove(toRect(cranePanel2), true, amount)) return;
                if (!canMove(toRect(cranePanel), true, amount)) return;
                if (grab && !canMove(toRect(crate), true, -amount)) return;
            }
            if (grab) crate.Top -= amount;
            cranePanel.Height += amount;
            cranePanel.Top -= amount;
            cranePanel2.Top -= amount;
            hookPanel.Top -= amount;
        }

        private void moveHook(int amount) //extends/retracts the hook
        {
            if (moving == 0)
            {
                if (!canMove(toRect(hookPanel), true, amount)) return;
                if (grab && !canMove(toRect(crate), true, amount)) return;
            }
            if (grab) crate.Top += amount;
            hookPanel.Height += amount;
        }

        private bool canMove(Rectangle p, bool direction, int amount) =>
            !(borderCollision(p, Size) && (!direction || Math.Sign(amount) == 1) || (direction ? p.Height : p.Width) == 0); //returns if an object can move

        private void FlipH(int amount) //flips the bridge
        {
            if (borderCollision(toRect(cranePanel2), Size)) return;
            cranePanel2.Left = 375 + offset;
            flipped = !flipped;
            hookPanel.Left -= Math.Sign(amount) * (50 - hookPanel.Width);
            if (grab) crate.Left -= Math.Sign(amount) * (50 - hookPanel.Width);
        }

        private void Score(ScoreType scoreType) //scores a point
        {
            score++;
            woScore[(int)scoreType]++;
            scoreTView.Items.Clear();
            for (int i = 0; i < woScore.Length; i++) scoreTView.Items.Add(((ScoreType)i).ToString() + ": " + woScore[i]);
            updateScoreLabel();
            if (score == 1)
                scoreLabel.Visible = true;
            if (score == 5)
            {
                labelTut.Text += "\nT - Reset Game\nQ - back to origin\nE - retract into base\nM - mute\nArrow Keys - Move Window Around\nCursor - Resize Window";
                phase = 1;
            }
            else if (score == 10)
            {
                labelTut.Text += "\nU - Accelerate Gravity\nJ - Decelerate Gravity";
                phase = 2;
            }
            else if (score == 25)
            {
                labelTut.Text += "\n0 - Shift Content to Left\n9 - Shift Content to Right";
                phase = 3;
            }
            else if (score == 50)
            {
                labelTut.Text += "\nSpace - Get out the drill\nL - reset your drill";
                phase = 4;
            }
            else if (score == 100)
            {
                labelTut.Text += "\nK - see what happens\nB - Hax";
                phase = 5;
                restrc = false;
            }
            UpdatePresence();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (restrc && phase < 1)
            {
                if (WindowState == FormWindowState.Maximized) WindowState = FormWindowState.Normal;
                this.Size = new Size(800, 500);
            }
            if (restrc)
            {
                if (Width < 800) Width = 800;
                if (Height < 500) Height = 500;
            }
        }

        private void updateScoreLabel() //updates the score label
        {
            scoreLabel.Text = "Score: " + score;
            if (score >= 10) scoreLabel.Text += "\nGravity: " + lGravity;
        }

        private enum ScoreType
        {
            Void = 1,
            Gap = 2,
            Zone = 0,
            Hax = 3
        }

        private void RespawnCrate() //respawns the crate
        {
            crate.Top = 10;
            crate.Left = 175 + offset;
            grab = false;
            hookPanel.BackColor = Color.Black;
        }

        private void RelocateZone()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            scorePanel.Left = r.Next(50, Width - 50 - scorePanel.Width);
            scorePanel.Top = r.Next(50, Height - 50 - scorePanel.Height);
        }

        private bool IsInScoringZone() =>
            Math.Abs(crate.Left - scorePanel.Left) <= 6 && Math.Abs(crate.Top - scorePanel.Top) <= 6;

        private void timer_Tick(object sender, EventArgs e) //event for gameTick
        {
            if (drill.Visible)
            {
                drill.Top = hookPanel.Bottom;
                drill.Left = hookPanel.Left - 1;
            }
            if (moving == 1) //if it's currently retracting
            {
                if (hookPanel.Height > 0) moveHook(-5);
                else if (cranePanel2.Width > 0 && !flipped) moveH(-5);
                else if (cranePanel2.Width > 0 && flipped) moveH(5);
                else if (cranePanel.Height > 0) moveV(-5);
                else
                {
                    moving = 0;
                    if (flipped)
                    {
                        FlipH(-10);
                        cranePanel2.Left += 50;
                    }
                }
            }
            else if (moving == 2) //if it's currently going back to original transforms
            {
                if (hookPanel.Left != cranePanel2.Left && !flipped) hookPanel.Left = cranePanel2.Left;
                if (cranePanel.Height != origins[0])
                    moveV(5 * (origins[0] - cranePanel.Height) / Math.Abs(origins[0] - cranePanel.Height));
                else if (cranePanel2.Width != origins[1] && !flipped)
                    moveH(5 * (origins[1] - cranePanel2.Width) / Math.Abs(origins[1] - cranePanel2.Width));
                else if (cranePanel2.Width != origins[1] && flipped)
                    moveH(-5 * (origins[1] - cranePanel2.Width) / Math.Abs(origins[1] - cranePanel2.Width));
                else if (hookPanel.Height != origins[2])
                    moveHook(5 * (origins[2] - hookPanel.Height) / Math.Abs(origins[2] - hookPanel.Height));
                else
                {
                    moving = 0;
                }
            }
            if (!grab && !groundCollision(toRect(crate))) //when the crate is flying
            {
                crate.Top += lGravity;
            }
            if (groundCollision(toRect(crate)) && crate.Top + crate.Height != ground.Top) crate.Top = ground.Top - crate.Height; //so the crate doesn't glitch in the ground
            if (IsInScoringZone()) //if the crate's in the scoring zone
            {
                Score(ScoreType.Zone);
                RespawnCrate();
                RelocateZone();
            }
            if (crate.Top >= Height + 50 && (!drilled || crate.Left + crate.Width <= offset || crate.Left >= 800 + offset)) //if the crate falls outside boundaries
            {
                Score(ScoreType.Void);
                RespawnCrate();
            }
            else if (crate.Top >= Height + 50) //if the crate falls through a drill created gap
            {
                Score(ScoreType.Gap);
                RespawnCrate();
            }
            if (drill.Visible && rectCollision(toRect(drill), toRect(ground))) //Splits the ground when using the drill
            {
                ground.Left = drill.Left + drill.Width;
                ground.Width = 800 - ground.Left + offset;
                Panel g2 = new Panel();
                Controls.Add(g2);
                g2.Top = 450;
                g2.Name = "g2";
                g2.Left = offset;
                g2.Height = 25;
                g2.Width = drill.Left - offset;
                g2.Tag = "Collider";
                g2.BackColor = ground.BackColor;
                drill.Visible = false;
                crate.Visible = true;
                drilled = true;
            }
        }

        private DialogResult ShowInputDialog(ref string input) //Input Box from Stackoverflow
        {
            Size size = new Size(200, 200);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Hax";

            TextBox textBox = new TextBox();
            textBox.Size = new Size(size.Width - 10, 23);
            textBox.Location = new Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.Text = "&Hac";
            okButton.Location = new Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.Text = "&stop dun hac";
            cancelButton.Location = new Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            Label helpLabel = new Label();
            helpLabel.Name = "helpLabel";
            helpLabel.AutoSize = true;
            helpLabel.Text = "Accepted cmds: " +
                "\nscore add <value>" +
                "\nreset" +
                "\ngravity set <value>" +
                "\nvis <object> (ywy|list)" +
                "\nmodo restricted|free";
            helpLabel.Location = new Point(5, 39 + 23 + 5);
            inputBox.Controls.Add(helpLabel);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DeinitializeClient();
        }
    }
}
