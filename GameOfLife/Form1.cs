using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        private int counter = 0;
        private bool newRandom = true;

        public Form1()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {           
            if (this.counter == 0)
            {
                this.AddButton1();
                this.counter++;
            }

            if (this.counter % 2 == 0)
            {
                this.Next(false, newRandom);
                newRandom = false;
            }
            else
            {
                this.Next(true, newRandom);
                newRandom = false;
            }
            this.counter++;

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            newRandom = true;
            this.counter = 1;
        }
    }
}
