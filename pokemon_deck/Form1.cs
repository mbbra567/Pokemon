using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Text.Json.Serialization;
using static System.Console;
using System.IO;
namespace pokemon_deck
{
    public partial class Form1 : Form
    {
        private ArrayList pokes = new ArrayList();
        int current;
        public Form1()
        {
            InitializeComponent();
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            var p = new Pokemon
            {
                name = NametextBox.Text,
                Height = HeighttextBox.Text,
                Type = TypetextBox.Text,
                Weight = WeighttextBox.Text,
                Moves = MovestextBox.Text,
                HP = HptextBox.Text,
                Description = textBox1.Text,
                Picture = pictureBox1.ImageLocation
            };
            if(pictureBox1 == null) { MessageBox.Show("Select a picture"); }
            string pokemonstring = JsonSerializer.Serialize(p);
            pokes.Add(p);
            StreamWriter outfile = File.CreateText("pokemon.json");
            foreach (var item in pokes)
            {
                outfile.WriteLine(JsonSerializer.Serialize(item));
            }
            outfile.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (System.IO.File.Exists(openFileDialog1.FileName))
            {
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }
        public void show()
        {
            Pokemon p = (Pokemon)pokes[current];
            NametextBox.Text = p.name;
            HeighttextBox.Text = p.Height;
            TypetextBox.Text = p.Type;
            WeighttextBox.Text = p.Weight;
            HptextBox.Text = p.HP;
            MovestextBox.Text = p.Moves;
            textBox1.Text = p.Description;
            if (File.Exists(p.Picture))
                {
                pictureBox1.Load(p.Picture);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader infile = File.OpenText("Pokemon.json");
            while(infile.Peek() != -1)
            {
                string pstring = infile.ReadLine();
                Pokemon p = JsonSerializer.Deserialize<Pokemon>(pstring);
                pokes.Add(p);
            }
            infile.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            show();
        }

        private void Nextbutton_Click(object sender, EventArgs e)
        {
            if (pokes.Count == current + 1)
            {
                current = 0;
            }
            else if (current < pokes.Count)
            {
                current++;
            }
            Show();
        }

        private void Prevbutton_Click(object sender, EventArgs e)
        {
            if (current == 0)
            {
                current = pokes.Count - 1;
            }
            else if (current > 0)
            {
                current--;
            }
            Show();
        }

        private void Lastbutton_Click(object sender, EventArgs e)
        {
           current = pokes.Count - 1;
           show();
        }
    }
}
