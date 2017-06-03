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
        public Runner()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
            testRectangle();
        }

        public void testRectangle()
        {
            Rectangle r = new Rectangle(0, this.Bottom - 10, this.Width, 10); 
        }

    }
}
