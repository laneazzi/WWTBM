using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Security.Policy;

namespace WhoWantsToBeAMillionaire
{
    public partial class Game : Form
    {
        public static ArrayList questions = new ArrayList(15);
  
        List<List<string>> answers = new List<List<string>>(15);

        public static Hashtable answer_key = new Hashtable();

        public List<int> positions = new List<int>();

        public int Score = 0;

        public int score_index = 0;

        public int[] ScoreList = {100,200,300,500,1000,2000,4000,8000,16000,32000,64000,125000,250000,500000}; 

        public int current_level = 0;

        public int[] question_arangements = { 0, 1, 2, 3 };

        public Label previous_answer_label;

        public bool used_fifty_fifty = false;

        public bool submitted = false; 


        

        public Game()
        {
            InitializeComponent();
            

        }




        


        public void load_questions_and_answers()
        {
            var counter = 0;
            var temp_array = new List<string>();

            foreach (string current_string in Infile.input)
            {
                if (counter == 0)
                {
                    questions.Add(current_string.ToLower());
                    counter += 1;
                }
                else
                {
                    temp_array.Add(current_string.ToLower());
                    counter += 1;
                    if (counter == 5)
                    {
                        counter = 0;
                        answers.Add(temp_array);
                        temp_array = new List<string>();

                    }
                }

            }

            for (var index = 0; index < 15; index++)
            {
                answer_key.Add(questions[index], answers[index][0]);
               
            }

        }

      

        public void shuffle()
        {
            for(int index = 1; index < question_arangements.Length; index++)
            {
                int temp = question_arangements[index - 1];
                question_arangements[index - 1] = question_arangements[index];
                question_arangements[index] = temp;
            }
        }



        public void load_buttons_and_labels(int index)
        {

            Label[] labels = { score_label1, score_label2, score_label3, score_label4, score_label5, score_label6, score_label7,
                score_label8, score_label9, score_label10, score_label11, score_label12, score_label13, score_label14, score_label15 };


            if (index == 0)
            {
                labels[index].BackColor = Color.Yellow;
            }
            else
            {
                labels[index - 1].BackColor = Color.LightBlue;
                labels[index].BackColor = Color.Yellow;

            }



            header_label.Text = questions[index].ToString();

            if (current_level + 1 == 1 || current_level + 1 == 5 || current_level + 1 == 10)
            {
                header_label.Text += "\n\n\n\n\n\n\n This is a safe haven  round!";
            }

            label_option_a.Text = answers[index][question_arangements[0]];
            label_option_b.Text = answers[index][question_arangements[1]];
            label_option_c.Text = answers[index][question_arangements[2]];
            label_option_d.Text = answers[index][question_arangements[3]];
            shuffle();

        }



        public void open_messagebox(string message,string title)
        {
           // MessageBoxButtons buttons = MessageBoxButtons.AbortRetryIgnore;
           if(MessageBox.Show(message, title, MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No){
                Application.Exit();
            }
           

            
        }


        public void player_has_lost(bool gameOver,int index,int score)
        {
            //Form3 frm3 = new Form3();
            //this.Hide();
            //frm3.load_game_state(outcome,index,score);
            ////frm3.ShowDialog();

            string title = "Results";
           


            if (gameOver)
            {
                string message = "YOU LOST!!!!!! Would you like to try again?";

                open_messagebox(message, title);
            }
            else if(!gameOver && index > 14)
            {
                string message = "YOU W0N A MILLION DOLLARS!!!, would you like to close the window?";
                open_messagebox(message, title);
            }
            else if(!gameOver && index <= 14)
            {
              
                string message = $"YOU WALK AWAY WITH {score} DOLLARS!!! , would you like to close the window?";
                open_messagebox(message, title);
            }

            










            

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1_TextChanged_1(sender,e);
            
            load_questions_and_answers();

            header_label.Text = "Welcome to who wants to be a millionaire!!!";
            
            load_buttons_and_labels(current_level);

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        public void nextLevel() {
            current_level += 1;

            if (current_level > 14)
            {
                player_has_lost(false,current_level,Score);
                return;
            }
            
            Score = ScoreList[score_index];
            score_index += 1;
            load_buttons_and_labels(current_level);
            input_box.Text = "";
            previous_answer_label.BackColor = Color.CornflowerBlue;

        }





      
       

        

        private void Form2_Shown(object sender, EventArgs e)
        {
           
           

        }

        

        private void walkAwayOn_Click(object sender, EventArgs e)
        {

            player_has_lost(false, current_level,Score);

        }



        public Label highlight_label()
        {
            Label[] labels = { label_option_a, label_option_b, label_option_c, label_option_d };
            Label correct_label = labels[0]; 
            foreach(Label curr_label in labels)
            {
                
                if (curr_label.Text == answer_key[questions[current_level]].ToString())
                {
                    correct_label = curr_label;
                    correct_label.BackColor = Color.IndianRed;
                   
                    return correct_label;
                }
            }

            return correct_label;
        }

        public void revert_label_backcolor()
        {
            
        }



        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            submit_answer_button.Enabled = input_box.Text == "" ? false : true; // Uses ternary operator 

           
        }

        //If there is no text in the textbox, button1 is not clickable.
        //If there is something in the textbox, button1 is clickable.
     

        private void submit_answer_button_Click(object sender, EventArgs e)
        {  
            if (input_box.Text.ToLower() == answer_key[questions[current_level]].ToString())//continue
            {

                previous_answer_label = highlight_label();

                header_label.Text = "You got the correct answer!";

                submitted = true;
                
            }
           else if (current_level + 1 == 1 || current_level + 1 == 5 || current_level + 1 == 10)
            {
                previous_answer_label = highlight_label();
                header_label.Text = "You survived due to your safe heaven!!!!";
                submitted = true;
            }

            else//you lose
            {

                player_has_lost(true, current_level, Score);
            }
        }




        private void next_question_Click(object sender, EventArgs e)
        {

            if (submitted)
            {
                nextLevel();
                submitted = false;
            }
            
    }

        private void fifty_fiftybutton_Click(object sender, EventArgs e)
        {
            // we need to reduce the number of answers by half
            if (!used_fifty_fifty)
            {
                //reduce the amount of options 

                Label[] labels = { label_option_a, label_option_b, label_option_c, label_option_d };
                int counter = 0;


                foreach(Label current_label in labels)
                {
                    if(current_label.Text != answer_key[questions[current_level]].ToString())
                    {
                        

                        counter += 1;
                        current_label.Text = "######";
                        used_fifty_fifty = true;
                        fifty_fifty_button.Text = "";
                        if (counter == 2) return;
                        
                    }
                }
                
            }
           

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void score_label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void header_label_Click(object sender, EventArgs e)
        {

        }

        private void label_option_b_Click(object sender, EventArgs e)
        {

        }

        private void label_option_c_Click(object sender, EventArgs e)
        {

        }

        private void label_option_a_Click(object sender, EventArgs e)
        {

        }

        private void label_option_d_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void score_label15_Click(object sender, EventArgs e)
        {

        }

        private void score_label14_Click(object sender, EventArgs e)
        {

        }

        private void score_label13_Click(object sender, EventArgs e)
        {

        }

        private void score_label11_Click(object sender, EventArgs e)
        {

        }

        private void score_label10_Click(object sender, EventArgs e)
        {

        }

        private void score_label8_Click(object sender, EventArgs e)
        {

        }

        private void score_label7_Click(object sender, EventArgs e)
        {

        }

        private void score_label6_Click(object sender, EventArgs e)
        {

        }

        private void score_label3_Click(object sender, EventArgs e)
        {

        }

        private void score_label2_Click(object sender, EventArgs e)
        {

        }
    }
}
