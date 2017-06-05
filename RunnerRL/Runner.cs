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

        private const int GROUND_POSITION = 30;
        private const int GROUND_WIDTH = 20;
        private const int PLAYER_SIZE = 10;
        private const int PLAYER_LOCATION = 20;

        private const float SCALE = 1;


        public Runner()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
            game = new RunnerGame();

            // testing();
        }
        private void onLoad(object sender, EventArgs e)
        {
            // timerLoop.Start();
        }

        private void testing()
        {
            LinkedList<Patch> pa = game.c.Patches;
            foreach (Patch p in pa)
            {
                Console.WriteLine(p.Location + " " + p.Length + " " + p.Floor);
            }
            for (int i = 0; i < 50; i += 10)
            {
                Console.WriteLine(i + " " + game.c.floorAtPosition(i));
            }
        }

        public void loopGame(object sender, EventArgs e)
        {
            // Update
            game.update();


            // Draw
            drawPlayer();
            drawGround();

            // Reset timer
            ((Timer) sender).Start();
        }

        private void drawPlayer()
        {
            int playerBottom = this.ClientRectangle.Bottom - GROUND_POSITION - PLAYER_SIZE;
            Rectangle r = new Rectangle(PLAYER_LOCATION, (int) (playerBottom + game.Y), PLAYER_SIZE, PLAYER_SIZE);
            graphics.FillRectangle(Brushes.Blue, r);
        }

        private void drawGround()
        {

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
            timerLoop.Start();
            testRectangle();
        }

    }
}
