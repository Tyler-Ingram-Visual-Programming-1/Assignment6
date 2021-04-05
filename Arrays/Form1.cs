using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arrays
{
//Coded by Tyler Ingram in 2021 - Assignment 6
    public partial class Form1 : Form
    {
        private StreamReader inputFile;
        private StreamWriter outputFile;
        private string inRecord, fName, lName;
        private double lowestGrade, highestGrade;
        private string[] outputData;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Nothing to put here
        }
        private void OpenFile()
        {
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
                MessageBox.Show(e.ToString());

            }
        }
        private void SaveToFile()
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    outputFile = File.CreateText(saveFileDialog1.FileName);
                    WriteOutput();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void ReadInput()
        {
            try
            {
                //Reads all data in the file and stores it in the lines array
                var lines = inputFile.ReadToEnd().Split('\n');
                outputData = new string[lines.Length];
                for (var i = 0; i < lines.Length; i++)
                {
                    inRecord = lines[i];
                    var lineDataArray = inRecord.Split(',');
                    //If current line is empty, split returns an array with a length of one.
                    //The excel test data has an empty line on line 16, so this prevents the resulting exception 
                    if (lineDataArray.Length <= 1)
                    {
                        outputData[i] = inRecord;
                        continue;
                    }

                    var totalSum = 0.0;
                    var count = 0.0;
                    lowestGrade = int.MaxValue; //To be used for finding lowest value in the array
                    highestGrade = int.MinValue;//To be used for finding highest value in the array
                    var grades = new double[5]; //Storing grades in this array
                    var highestIndex = 0;
                    var lowestIndex = 0;

                    //Set c to 2 to avoid the first name and last name in the file
                    for (var c = 2; c < lineDataArray.Length; c++)
                    {
                        var currentValue = int.Parse(lineDataArray[c]);
                        
                        if (currentValue < lowestGrade)
                        {
                            lowestGrade = currentValue;
                            lowestIndex = c;
                        }

                        if (currentValue > highestGrade)
                        {
                            highestGrade = currentValue;
                            highestIndex = c;
                        }

                        count++;
                        totalSum += currentValue;
                    }
                    
                    var index = 0;
                    for (var g = 2; g < lineDataArray.Length; g++)
                    {
                        if (g == highestIndex)
                        {
                            continue;
                        }

                        if (g == lowestIndex)
                        {
                            continue;
                        }

                        grades[index] = double.Parse(lineDataArray[g]);
                        index++;
                    }
                    //Sort array into ascending order
                    Array.Sort(grades);
                    //Finding average of the array
                    totalSum -= (highestGrade + lowestGrade);
                    count -= 2;
                    var average = totalSum / count;
                    var letterGrade = CalculateLetterGrade(average);
                    lName = lineDataArray[0]; fName = lineDataArray[1];
                    var stringArray = new string[3] {lName, fName, letterGrade.ToString()};
                    var lineData = string.Join(", ", stringArray);
                    outputData[i] = lineData;
                    ListBoxOutput(grades, average, letterGrade);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
        private void ListBoxOutput(double[] grades, double average, char letterGrade)
        {
            var sequenceNum = listBox1.Items.Count + 1;
            var gradeData = $"{grades[0]}\t{grades[1]}\t{grades[2]}\t{grades[3]}\t{grades[4].ToString()}";
            var name = $"{fName} {lName}";
            var output = $"{sequenceNum}\t{name}\t{gradeData}\t{average}\t{lowestGrade}\t{highestGrade}\t{letterGrade}";
            listBox1.Items.Add(output);
        }
        private char CalculateLetterGrade(double average)
        {
            if (average >= 90)
            {
                return 'A';
            }
            else if (average >= 80)
            {
                return 'B';
            }
            else if (average >= 70)
            {
                return 'C';
            }
            else if (average >= 60)
            {
                return 'D';
            }
            else return 'F';
        }

        private void WriteOutput()
        {
            try
            {
                foreach (var line in outputData)
                {
                    outputFile.WriteLine(line);
                }

                outputFile.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ExitApplication()
        {
            Application.Exit();
        }

        private void getGradesButton_Click(object sender, EventArgs e)
        {
            OpenFile();
            SaveToFile();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearVariables();
            ClearOutput();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void ClearVariables()
        {
            fName = ""; lName = ""; inRecord = "";
            lowestGrade = 0; highestGrade = 0;
            outputData = null;
        }
        private void ClearOutput()
        {
            listBox1.Items.Clear();
        }
    }
}