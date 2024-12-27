using System;
using System.Drawing;
using System.Windows.Forms;

namespace Crane
{
    public partial class MainForm : Form
    {
        public void InitPhases()
        {
            phases = new Action<Keys>[]
            {
                (Keys e) =>
                {
                    switch (e)
                    {
                        case Keys.A: //extend horizontally
                            moveH(10);
                        break;
                        case Keys.D: //retract horizontally
                            moveH(-10);
                        break;
                        case Keys.W: //extend vertically
                            moveV(10);
                        break;
                        case Keys.S: //retract vertically
                            moveV(-10);
                        break;
                        case Keys.F: //extend hook
                            moveHook(10);
                        break;
                        case Keys.R: //retract hook
                            moveHook(-10);
                        break;
                        case Keys.G: //grap / ungrap
                            if (drill.Visible) break;
                            if (rectCollision(toRect(hookPanel), toRect(crate)))
                            {
                                grab = !grab;
                                hookPanel.BackColor = hookPanel.BackColor == Color.Black ? Color.Red : Color.Black;
                            }
                            break;
                        case Keys.B: //hax
                            string hax = "";
                            DialogResult DR = ShowInputDialog(ref hax);
                            if (DR == DialogResult.OK)
                                Eval(hax);
                            break;
                    }
                },
                (Keys e) =>
                {
                    switch (e)
                    {
                        case Keys.Q: //back to origin
                            grab = false;
                            hookPanel.BackColor = Color.Black;
                            moving = 2;
                            break;
                        case Keys.E: //retract
                            grab = false;
                            hookPanel.BackColor = Color.Black;
                            moving = 1;
                            break;
                        case Keys.M: //Mute
                            if (!muted)
                                bgm.Stop();
                            else
                                bgm.PlayLooping();
                            muted = !muted;
                            break;
                        case Keys.T: //reset
                            score = 0;
                            foreach (Control c in Controls)
                                c.Left -= offset;
                            offset = 0;
                            if (drilled)
                            {
                                foreach (Control c in Controls)
                                {
                                    if (c.Name == "g2")
                                    {
                                        c.Dispose();
                                        Controls.Remove(c);
                                    }
                                }
                                ground.Left = offset;
                                ground.Width = 800;
                                drilled = false;
                            }
                            crate.Visible = true;
                            drill.Visible = false;
                            Size = new Size(800, 500);
                            restrc = true;
                            phase = 0;
                            grab = false;
                            hookPanel.BackColor = Color.Black;
                            flipped = false;
                            labelTut.Text = "Controls:\nW/S - up/down\n" +
                                "A/D - extend/retract bridge\nR/F - extend/retract hook\n" +
                                "G - attach/drop crate";
                            youwonyay.Visible = false;
                            scoreTView.Visible = false;
                            updateScoreLabel();

                            cranePanel.Height = origins[0];
                            cranePanel.Top = 200;
                            cranePanel2.Width = origins[1];
                            cranePanel2.Left = 225;
                            hookPanel.Height = origins[2];
                            hookPanel.Left = cranePanel2.Left;

                            ResetPresence();
                            break;
                        case Keys.Right: //Move Window on screen
                            this.Left += 10;
                            break;
                        case Keys.Left:
                            this.Left -= 10;
                            break;
                        case Keys.Up:
                            this.Top -= 10;
                            break;
                        case Keys.Down:
                            this.Top += 10;
                            break;
                    }
                },
                (Keys e) =>
                {
                    switch (e)
                    {
                        case Keys.U:
                            lGravity = Math.Min(lGravity + 5, 25);
                            break;
                        case Keys.J:
                            lGravity = Math.Max(lGravity - 5, 0);
                            break;
                    }
                },
                (Keys e) =>
                {
                    switch (e)
                    {
                        case Keys.D9: //Shift View
                            foreach (Control c in Controls)
                                c.Left += 10;
                            offset += 10;
                            break;
                        case Keys.D0:
                            foreach (Control c in Controls)
                                c.Left -= 10;
                            offset -= 10;
                            break;
                    }
                },
                (Keys e) =>
                {
                    switch (e)
                    {
                        case Keys.Space: //get out the drill
                            if (drilled) break;
                            drill.Visible = crate.Visible;
                            crate.Visible = !crate.Visible;
                            break;
                        case Keys.L: //reset drilling
                            if (drilled)
                            {
                                foreach (Control c in Controls)
                                {
                                    if (c.Name == "g2")
                                    {
                                        c.Dispose();
                                        Controls.Remove(c);
                                    }
                                }
                                ground.Left = offset;
                                ground.Width = 800;
                                drilled = false;
                            }
                            break;
                    }
                },
                (Keys e) =>
                {
                    switch (e)
                    {
                        case Keys.K:
                        youwonyay.Visible = true;
                        scoreTView.Visible = true;
                        break;
                    }
                }
            };
        }
    }
}
