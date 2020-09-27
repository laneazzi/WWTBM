using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhoWantsToBeAMillionaire
{
    public partial class Form3 : Form

    {

        public bool gameOver;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        public void load_game_state(bool outcome,int index,int amount = 0)
        {
            gameOver = outcome;

            if (gameOver)
            {
                label1.Text = "YOU LOSE!!!!!!";
               
            }
            else if(!gameOver && index > 14)
            {
                label1.Text = "YOU WIN A MILLION DOLLARS!!!";
            }
            else if(!gameOver && index <= 14)
            {
                label1.Text = $"YOU WALK AWAY WITH {amount} DOLLARS!!!";
            }
        }


        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
