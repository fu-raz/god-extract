using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace GOD2FAT
{
    public partial class Form1 : Form
    {
        private GameListView glv;

        public Form1()
        {
            InitializeComponent();
            // Try to do stuff
            // using (var stream = new FileStream("C:\\xbox360\\49AAD81B9FCDA45E4A03D71BFCB353F8FADB236C58", FileMode.Open, FileAccess.Read))
            // using (var stream = new FileStream("C:\\xbox360\\7D712A01A804848890EE92F38CAD2229786E33B258", FileMode.Open, FileAccess.Read))
           // BaseGameInterpreter bgi = new BaseGameInterpreter("C:\\xbox360\\49AAD81B9FCDA45E4A03D71BFCB353F8FADB236C58");
            this.glv = new GameListView(listView1, imageList1);
           //  GameInterpreter gi = new GameInterpreter("C:\\xbox360\\D7931DE1B3553DDF3930"); // FIFA 15
           // glv.addGame(gi);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            textBox1.Text = fbd.SelectedPath;

            toolStripStatusLabel1.Text = "Adding files from directory " + fbd.SelectedPath;
            Regex rgx = new Regex("[ABCDEF0-9]{20,}");
            var files = Directory.GetFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories)
                .Where(path => rgx.IsMatch(path)).ToList();

            toolStripStatusLabel1.Text = "Parsing " + files.Count + " files";
            foreach (string filePath in files)
            {
                GameInterpreter gi = new GameInterpreter(filePath);
                glv.addGame(gi);
            }
            toolStripStatusLabel1.Text = "Idle";
        }




    }
}
