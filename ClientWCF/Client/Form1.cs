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

namespace WindowsFormsApp1
{
    /// <summary>The class that contains all the methods used to connect the form to the APi for the database.</summary>
    public partial class Form1 : Form
    {
        Nterface1Client client = new Nterface1Client();

        static string extensions =  "*.JPG;*.JPEG;*.PNG;*.GIF;" +
                                    "*.WEBM;*.MPG; *.MP2; *.MPEG; *.MPE; *.MPV;" +
                                    "*.OGG;" +
                                    "*.MP4; *.M4P; *.M4V;" +
                                    "*.AVI; *.WMV;" +
                                    "*.MOV; *.QT;" +
                                    "*.FLV; *.SWF;" +
                                    "*.AVCHD;";

        string[] extensionList = extensions.Split(';');

        private List<string> allFiles = new List<string>();
        private int index = 0;
        private string currentFile = null;
        private bool all = false;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Supported Formats|" + extensions;
        }

        private void message(string file)
        {
            string message = "The file: \n" + file + "\nIs not available anymore!\nPerhaps it has been moved?!\nDo you want to select it's new " +
                "path (YES) or delete the entry (NO) ?";

            try
            {
                DialogResult result = MessageBox.Show(message, "File not detected!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    openFileDialog1.FileName = file;
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string newFile = openFileDialog1.FileName;
                        if (File.Exists(newFile))
                        {
                            client.Modify("Full_Path", newFile, "Full_Path", file);

                            FileInfo file_info = new FileInfo(newFile);
                            string[] newName = file_info.Name.Split('.');
                            client.Modify("Name", newName[0], "Full_Path", newFile);
                        }
                    }
                }
                else
                {
                    client.Delete("Full_Path", file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR Message:" + ex.Message);
            }
        }

        private void checkFiles()
        {
            try
            {
                List<string> path = client.Check("Full_Path", true);
                if (!path[0].Equals("NOTHING WAS FOUND!"))
                {
                    for (int i = 0; i < path.Count(); i++)
                    {
                        if (!File.Exists(path[i]))
                        {
                            message(path[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR Check_Files:" + ex.Message);
            }
        }

        private void display(List<string> result)
        {
            string tmp = "";
            foreach (var line in result)
            {
                tmp += line.ToString();
                tmp += Environment.NewLine;
            }
            textBox2.Text = tmp;
        }

        private void form1Load(object sender, EventArgs e)
        {
            checkFiles();
            List<string> columns = client.Columns();
            columns.RemoveRange(0, 8);

            var columns2 = client.Columns();
            columns2.RemoveRange(0, 8);

            var columns3 = client.Columns();
            columns3.Insert(0, "[Show All]");
            columns3.RemoveAt(8);

            comboBox1.DataSource = columns3;
            comboBox2.DataSource = columns;
            comboBox3.DataSource = columns2;

            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void loadPicture(string path)
        {
            if (File.Exists(currentFile))
                try
                {
                    textBox5.Visible = false;
                    pictureBox1.Visible = true;
                    pictureBox1.Load(path);
                }
                catch (ArgumentException ex)
                {
                    textBox5.Visible = true;
                    pictureBox1.Visible = false;
                    FileInfo fileInfo = new FileInfo(path);
                    textBox5.Text = fileInfo.Name;
                }
        }

        private void textBox5MouseHover(object sender, EventArgs e)
        {
            checkFiles();
            toolTip1.SetToolTip(textBox5, textBox5.Text);
        }

        private void selectFile(object sender, EventArgs e)
        {
            checkFiles();
            allFiles.Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                currentFile = openFileDialog1.FileName;
                loadPicture(currentFile);
            }
        }

        private void selectFolder(object sender, EventArgs e)
        {
            checkFiles();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                if (Directory.Exists(path))
                {
                    allFiles.Clear();
                    foreach (string extension in extensionList)
                    {
                        foreach (string file in Directory.GetFiles(path, extension))
                        {
                            allFiles.Add(file);
                        }
                    }
                    

                    index = 0;
                    currentFile = allFiles[index];
                    if (File.Exists(currentFile))
                        loadPicture(currentFile);
                }
                else
                {
                    Console.WriteLine(path + " is not a valid file or directory.");
                }
            }
        }
		
        private void insertFile(object sender, EventArgs e)
        {
            checkFiles();
            if (currentFile != null)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(currentFile);
                    string[] name = fileInfo.Name.Split('.');
                    bool insert = client.Insert(name[0].ToString(), fileInfo.FullName.ToString(), fileInfo.Extension.ToString(), 
                        (fileInfo.Length / Math.Pow(10, 6)), fileInfo.CreationTime);
                    if (insert == false && all == false)
                        MessageBox.Show("The picture/video \"" + name[0] + "\" is already in the database.");

                    myUpdate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR Insert_File:" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No file selected!", "Error");
            }
        }

        private void removeFile(object sender, EventArgs e)
        {
            checkFiles();
            if (currentFile != null)
            {
                string message = "Are you sure you want to REMOVE the displayed picture/video from the database?!";
                DialogResult result = MessageBox.Show(message, "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    client.Delete("Full_Path", currentFile);
                    myUpdate();
                }
            }
        }

        private void clear(object sender, EventArgs e)
        {
            checkFiles();
            allFiles.Clear();
            index = 0;
            currentFile = null;
            pictureBox1.Image = null;
            textBox5.Visible = false;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void comboBox3MouseHover(object sender, EventArgs e)
        {
            checkFiles();
            toolTip1.SetToolTip(comboBox3, comboBox3.Text);
        }

        private void removeTag(object sender, EventArgs e)
        {
            checkFiles();
            if (comboBox3.Text != "")
            {
                string message = "Are you sure you want to REMOVE the column \"" + comboBox3.Text + "\" from the table?\n" +
                    "All the data it contains will be deleted!";
                DialogResult result = MessageBox.Show(message, "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    client.Remove(comboBox3.Text);

                    form1Load(sender, e);
                    myUpdate();
                }
            }
        }

        private void textBox4MouseHover(object sender, EventArgs e)
        {
            checkFiles();
            toolTip1.SetToolTip(textBox4, textBox4.Text);
        }

        private void addTag(object sender, EventArgs e)
        {
            checkFiles();
            if (textBox4.Text != "")
            {
                client.Add(textBox4.Text);

                form1Load(sender, e);
                myUpdate();
            }
        }

        private void comboBox1MouseHover(object sender, EventArgs e)
        {
            checkFiles();
            toolTip1.SetToolTip(comboBox1, comboBox1.Text);
        }

        private void textBox1MouseHover(object sender, EventArgs e)
        {
            checkFiles();
            toolTip1.SetToolTip(textBox1, textBox1.Text);
        }

        private void search(object sender, EventArgs e)
        {
            checkFiles();
            if (comboBox1.Text.Equals("[Show All]"))
            {
                allFiles = client.Check("Full_Path", false);
            }
            else
            {
                allFiles = client.SearchPath(comboBox1.Text, textBox1.Text);
            }

            index = 0;
            currentFile = allFiles[index];
            if (File.Exists(currentFile))
                loadPicture(currentFile);
            else
            {
                pictureBox1.Image = null;
                textBox5.Visible = false;
            }

            myUpdate();
        }

        private void comboBox2MouseHover(object sender, EventArgs e)
        {
            checkFiles();
            toolTip1.SetToolTip(comboBox2, comboBox2.Text);
        }

        private void textBox3MouseHover(object sender, EventArgs e)
        {
            checkFiles();
            toolTip1.SetToolTip(textBox3, textBox3.Text);
        }

        private void modify(object sender, EventArgs e)
        {
            checkFiles();
            if (textBox1.Text == "")
            {
                if (currentFile != null)
                {
                    client.Modify(comboBox2.Text, textBox3.Text, "Full_Path", currentFile);
                    myUpdate();
                }
            }
            else
            {
                string message = "The TEXT field from the SEARCH box is NOT empty!\nContinuing will NOT modify the displayed picture/video," +
                    " but ALL the pictures and videos that are found with SEARCH!\nDo you wish to continue?";

                DialogResult result = MessageBox.Show(message, "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    client.Modify(comboBox2.Text, textBox3.Text, comboBox1.Text, textBox1.Text);
                    textBox2.Text = "";
                    search(sender, e);
                }
            }
        }

        private void previous(object sender, EventArgs e)
        {
            checkFiles();
            if (allFiles.Count() > 1)
            {
                index--;
                if (index < 0)
                    index = allFiles.Count() - 1;
                currentFile = allFiles[index];
                if (File.Exists(currentFile))
                    loadPicture(currentFile);
                else
                {
                    allFiles.RemoveAt(index);
                    index--;
                    next(sender, e);
                }
            }
        }

        private void next(object sender, EventArgs e)
        {
            checkFiles();
            if (allFiles.Count() > 1)
            {
                index++;
                if (index >= allFiles.Count())
                    index = 0;
                currentFile = allFiles[index];
                if (File.Exists(currentFile))
                    loadPicture(currentFile);
                else
                {
                    allFiles.RemoveAt(index);
                    index--;
                    next(sender, e);
                }
            }
        }

        private void pictureBox1Paint(object sender, PaintEventArgs e)
        {
            myUpdate();
        }

        private void myUpdate()
        {
            if (currentFile != null)
            {
                List<string> result = client.Search("Full_Path", currentFile);
                display(result);
            }
        }

        private void insertAll(object sender, EventArgs e)
        {
            all = true;
            if (allFiles.Count > 0)
            {
                foreach (var pic in allFiles)
                {
                    if (File.Exists(pic))
                    {
                        loadPicture(pic);
                        currentFile = pic;
                        insertFile(sender, e);
                    }
                }
                index = allFiles.Count() - 1;
                MessageBox.Show("Done, all pictures/videos have been inserted!");
                all = false;
            }
            else
            {
                MessageBox.Show("No files selected!", "Error");
            }
        }

        private void textBox5VisibleChanged(object sender, EventArgs e)
        {
            if (textBox5.Visible == true)
            {
                myUpdate();
            }
        }
    }
}
