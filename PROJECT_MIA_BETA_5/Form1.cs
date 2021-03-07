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
    public partial class miaMain : Form
    {
        public SpeechSynthesizer miaSynth = new SpeechSynthesizer();
        public SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        public Choices list = new Choices();
        string userPath1 = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public int startCount;
        public miaMain()
        {
            miaSynth.SelectVoiceByHints(VoiceGender.Female);
            if(startCount == 0)
            {
                startCount = 1;
                string userDoc = userPath1 + @"\Documents\MIA\SAVED TEXT DOCUMENTS";
                Directory.CreateDirectory(userDoc);
                string userMusic = userPath1 + @"\Documents\MIA\MUSIC FILES";
                Directory.CreateDirectory(userMusic);
                if (DateTime.Now.Hour < 12)
                {
                    miaSynth.Speak("Good morning sir, It is now " + DateTime.Now.ToString("h:mm tt") + " of " + DateTime.Now.ToString("M/d/yyyy."));

                }
                if (DateTime.Now.Hour > 12)
                {
                    if (DateTime.Now.Hour >= 16)
                    {
                        miaSynth.Speak("Good evening sir, It is now " + DateTime.Now.ToString("h:mm tt") + " of " + DateTime.Now.ToString("M/d/yyyy."));

                    }
                    if (DateTime.Now.Hour < 16)
                    {
                        miaSynth.Speak("Goodafternoon sir, It is now " + DateTime.Now.ToString("h:mm tt") + " of " + DateTime.Now.ToString("M/d/yyyy."));
                    }
                }
            }
            
            InitializeComponent();
        }
        public void Say(string miaSpeak)
        {
            miaSynth.Speak(miaSpeak);
            //miaText.AppendText(miaSpeak + "\n");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            miaWrt miaWrtTab = new miaWrt();
            this.Hide();
            miaWrtTab.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            miaOpen miaOpenTab = new miaOpen();
            this.Hide();
            miaOpenTab.ShowDialog();
        }

        private void miaMain_Load(object sender, EventArgs e)
        {

        }
    }
}
