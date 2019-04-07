using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AT3_P466426
{
    public partial class Form1 : Form
    {
        public string[] gameName;
        private string[] gamePlatform;
        private string[] gameGenre;
        int numTimes=0;
        public Form1()
        {
            
            InitializeComponent();
            gameName = new string[20];
            gameName[0] = "Kingdom Hearts";
            gameName[1] = "GTA V";
            gameName[2] = "Call of Duty";
            gameName[3] = "SIMS 4";

            gamePlatform = new string[20];
            gamePlatform[0] = "PS2";
            gamePlatform[1] = "PS4";
            gamePlatform[2] = "PC";
            gamePlatform[3] = "PS4";

            gameGenre = new string[20];
            gameGenre[0] = "Role Player";
            gameGenre[1] = "Action";
            gameGenre[2] = "Shooter";
            gameGenre[3] = "Simulation";
            numTimes = 4;
            DisplayNames(gameName);
        }

        private void BubbleSort(string[] gameName)
        {
            for (int i = 0; i < gameName.Length -1; i++)
            {
                for(int j = 1; j<gameName.Length; j++)
                {
                    if(gameName[j].CompareTo(gameName[j+1]) >= 0)
                    {
                        string temp = gameName[j];
                        gameName[j] = gameName[j + 1];
                        gameName[j + 1] = temp;
                    }
                }

            }
        }
//DisplayNames()
        private void DisplayNames(string[] gameName)
        {
            lstGame.Items.Clear();
            for(int i = 0; i < numTimes; i++)
            {
                lstGame.Items.Add(gameName[i] + " " + gamePlatform[i] + " " + gameGenre[i]);
            }
        }
//Add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(tbName.Text=="")
            {
                MessageBox.Show("Please fill in the name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
            if (tbPlatform.Text == "")
            {
                MessageBox.Show("Please fill in the platform", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             
            }
            if (tbGenre.Text == "")
            {
                MessageBox.Show("Please fill in the Genre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gameName[numTimes] = tbName.Text;
            gamePlatform[numTimes] = tbPlatform.Text;
            gameGenre[numTimes] = tbGenre.Text;
            numTimes++;

            lstGame.Items.Add(tbName.Text + " " + tbPlatform.Text + " " + tbGenre.Text);
          
        }
//Clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbName.Clear();
            tbPlatform.Clear();
            tbGenre.Clear();
        }
//Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(lstGame.SelectedIndex == -1)
            {
                MessageBox.Show("Select an item to remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lstGame.Items.Remove(lstGame.SelectedItem);
            gameName[numTimes] = tbName.Text;
            gamePlatform[numTimes] = tbPlatform.Text;
            gameGenre[numTimes] = tbGenre.Text;
            numTimes--;
        }
//Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(@"mygames.dat", FileMode.Create)))
                {
                    writer.Write(numTimes);
                    for(int i = 0; i < numTimes; i++)
                    {
                        writer.Write(gameName[i] + " " + gamePlatform[i] + " " + gameGenre[i]);
                    }
                    writer.Close();
                }
                MessageBox.Show("Data saved", "",MessageBoxButtons.OK);
            }
            catch(IOException x)
            {
                MessageBox.Show("Exception: " + x.Message);
            }
        }
//lstGame
        private void lstGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lstGame.SelectedIndex == -1)
            {
                return;
            }
            tbName.Text = gameName[lstGame.SelectedIndex];
            tbPlatform.Text = gamePlatform[lstGame.SelectedIndex];
            tbGenre.Text = gameGenre[lstGame.SelectedIndex];


        }
//Reset
        private void btnReset_Click(object sender, EventArgs e)
        {
            lstGame.Items.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            numTimes = lstGame.Items.Count;
            textBox1.Text = numTimes.ToString();
        }
//Open
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open("mygames.dat", FileMode.Open, FileAccess.Read)))
                {
                    numTimes = reader.ReadInt32();
                    for (int i = 0; i < numTimes; i++)
                    {
                        string str = reader.ReadString();
                        lstGame.Items.Add(str);
                    }
                }
            }
            catch (IOException x)
            {
                MessageBox.Show("Exception: " + x);
            }
        }
//Sort
        private void btnSort_Click(object sender, EventArgs e)
        {
            BubbleSort(gameName);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lstGame.SelectedItems.Clear();
            for(int i = lstGame.Items.Count -1; i >=0; i--)
            {
                if(lstGame.Items[i].ToString().ToLower().Contains(tbName.Text.ToLower()))
                {
                    lstGame.SetSelected(i, true);
                }
            }
            
        }
    }
}
