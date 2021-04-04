using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arrays
{

    public partial class Form1 : Form
    {
        StreamReader inputFile;
        private String inRecord, fName, lName;
        private double grade1, grade2, grade3, grade4, meanGrade, lowestGrade, highestGrade;
        private string[] fileInputArray = new string[9];

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Hello");

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    inputFile = File.OpenText(openFileDialog1.FileName);
                    ReadInput();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void OpenFile()
        {
            InitializeComponent();
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    inputFile = File.OpenText(openFileDialog1.FileName);
                    ReadInput();
                    MessageBox.Show("Hello");

                }
            }
            catch (Exception)
            {
                Console.WriteLine();
                throw;
            }
        }

        private void ReadInput()
        {
            try
            {
                for (int i = 0; i < fileInputArray.Length; i++)
                {
                    inRecord = inputFile.ReadLine();
                    fileInputArray = inRecord.Split(',');
                    fileInputArray[i] = inRecord;
                    MessageBox.Show(fileInputArray[i]);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

    }
    
}