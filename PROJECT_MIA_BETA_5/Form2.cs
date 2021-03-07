using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.IO.Ports;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace PROJECT_MIA_BETA_5
{
    public partial class miaWrt : Form
    {
        public SpeechSynthesizer miaSynth = new SpeechSynthesizer();
        public SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        public Choices list = new Choices();
        public SerialPort port = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
        string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public string portWords = "";

        public miaWrt()
        {
            miaSynth.SelectVoiceByHints(VoiceGender.Female);
            InitializeComponent();
        }
        public void Say(string miaSpeak)
        {
            miaSynth.Speak(miaSpeak);
                  
        }
        System.Threading.Thread t;
        private void miaWrt_Load(object sender, EventArgs e)
        {
            t = new System.Threading.Thread(DoThisAllTheTime);
            t.Start();
        }
        public void DoThisAllTheTime()
        {
            while (true)
            {
                port.Open();
                portWords = port.ReadLine();
                var myWords = portWords.Replace('*', ' ');
                Console.WriteLine(portWords);
                Console.WriteLine(myWords);
                Say(myWords);
                port.Close();
                if (textTextbox.InvokeRequired)
                {
                    textTextbox.Invoke(new MethodInvoker(delegate { textTextbox.Text += " " + myWords; }));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            miaMain miaMainTab = new miaMain();
            this.Hide();
            miaMainTab.ShowDialog();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textTextbox.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(titleText.Text == "")
            {
                MessageBox.Show("No Title!");
            }
            string pathUser = userPath + @"\Documents\MIA\SAVED TEXT DOCUMENTS\" + titleText.Text + ".txt";
            StreamWriter q1 = new StreamWriter(pathUser);
            q1.WriteLine(textTextbox.Text);
            MessageBox.Show("Text saved!");
            q1.Close();

        }
    }
}
