using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PictureViewer
{
    public partial class Form1 : Form
    {   static int[] array1= new int[] { 1,2,3,4};
        static int counter=0;
     
        static Boolean checkFlow = false;
        
        List<string> imagePaths = new List<string>();
        List<string> extractedPath = new List<string>();

        public Form1()
        {   
            InitializeComponent();
        }

        public void ImagesData() {
            string givenPat = @".*?(\/[\/\w\.]+)[\s\?]?.*";
            Regex pattern = new Regex(givenPat);

            try {
                string[] storelines = File.ReadAllLines(@"F:\5th semester\visual programming\Lab_Tasks\PictureViewer\PictureViewer\ImagesDataFolder\ImagesPath.xml");
                foreach (string line in storelines)
                {
                    Match match = pattern.Match(line);
                    if (match.Success)
                    {
                        imagePaths.Add(match.Value);
                    }
                    else {
                      
                    }

                }

            }
            catch (Exception ex) { }

           
        }

        public void methodToExtractPath() {
            for (int i = 0; i < imagePaths.Count-1; i++) {
                string removeStartTag = imagePaths.ElementAt(i).Remove(0,9);
                
                string bothTagsremoval = removeStartTag.Remove(removeStartTag.Length-9,9);
                
                extractedPath.Add(bothTagsremoval);
               
               
            }
            }
        
        

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void PreviousBtn_Click(object sender, EventArgs e)
        {
            counter--;
            if (counter < 0)
            {
                MessageBox.Show("Image Not Available");

            }
            else {
                this.pictureBox1.Image = Image.FromFile(extractedPath[counter]);


            }

            checkFlow = true;

        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            counter++;
            if (counter >=4)
            {
                MessageBox.Show("Image Not Available");

            }
            else
            {
                this.pictureBox1.Image = Image.FromFile(extractedPath[counter]);

            }
            checkFlow = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ImagesData();
            
            methodToExtractPath();


            this.pictureBox1.Image = Image.FromFile(extractedPath[0]);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void Play_btn_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Pause_btn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkFlow)
            {
                
                if (counter < 0)
                {
                    counter = 4;

                }
                else
                {
                    this.pictureBox1.Image = Image.FromFile(extractedPath[counter]);

                }
                counter--;
            }
            else {
                
                if (counter >= 4)
                {
                    counter = -1;

                }
                else
                {
                    this.pictureBox1.Image = Image.FromFile(extractedPath[counter]);

                }
                counter++;
            }
        }
    }
}
