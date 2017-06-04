using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunnerRL
{
    public partial class Runner : Form
    {
        private Graphics graphics;
        private RunnerGame game;
        public Runner()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
            game = new RunnerGame();
        }

        public void loopGame(object sender, EventArgs e)
        {
            // Update
            game.update();
        }

        public void testRectangle()
        {
            Rectangle display = this.DisplayRectangle;
            int top = display.Bottom - 30;
            int left = display.Left;
            Rectangle r = new Rectangle(left, top, display.Width, 20);
            graphics.FillRectangle(Brushes.Black, r);
        }

        public void testRectangle(object sender, EventArgs e)
        {
            testRectangle();
        }

    }
}
