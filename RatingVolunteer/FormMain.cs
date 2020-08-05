using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;


namespace RatingVolunteer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        void LoadXmlWay(string path)
        {
            StreamWriter sw = new StreamWriter("mapo.txt");   
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);
            XmlElement xRoot = xdoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                if (xnode.Name == "node")
                {
                    User user = new User();
                    XmlNode nodeLat = xnode.Attributes.GetNamedItem("lat");
                    XmlNode nodeLon = xnode.Attributes.GetNamedItem("lon");
                    foreach (XmlElement childnode in xnode.ChildNodes)
                    {
                        var tagK = childnode.Attributes.GetNamedItem("k");
                        var tagV = childnode.Attributes.GetNamedItem("v");
                        if (tagK != null && tagV != null)
                        {
                            if (tagK.Value == "name" && !tagV.Value.Contains("улица") && !tagV.Value.Contains("ул.") && !tagV.Value.Contains("проспект"))
                            {
                                user.Lat = nodeLat.Value; user.Lon = nodeLon.Value; user.Name = tagV.Value;
                                sw.WriteLine(user.Name + ";" + user.Lat + ";" + user.Lon);
                                richTextBoxRating.Text += user.Name + ";" + user.Lat + ";" + user.Lon + "\n";
                            }
                        }
                    }

                }
            }
            sw.Close();
        }


        private void OpenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            LoadXmlWay(filename);
            MessageBox.Show("Файл открыт");
        }
        private void HelpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("1. Откройте файл .osm\n2. Нажмите получить данные");
        }

    }
}

