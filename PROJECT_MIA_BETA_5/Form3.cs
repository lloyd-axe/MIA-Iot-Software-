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
using System.IO.Ports;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace PROJECT_MIA_BETA_5
{
    public partial class miaOpen : Form
    {
        public SpeechSynthesizer miaSynth = new SpeechSynthesizer();
        public SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        public Choices list = new Choices();
        public SerialPort portOp = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
        public bool miaAndroidless = false;
        public miaOpen()
        {
            miaSynth.SelectVoiceByHints(VoiceGender.Female);
            SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
            list.Add(new String[] {
                //ACTIVATION
                "Hello there mia",
                "Mia wake up now",
                "Mia, can you please introduce yourself",
                //LIGHTS ON
                #region
                "Mia, lights on please",
                "Mia, turn on the lights",
                "Mia, please turn on the lights",
                "Mia, turn the lights on",
                "Mia, turn the lights on please",
                "Mia, please turn the lights on",
                "Mia, lights on",
                "Mia, light up the room",
                "Mia, turn the room lights on please",
                "Mia, make it bright here",        
                //LIGHTS OFF
                "Mia, lights off please",
                "Mia, turn off the lights",
                "Mia, please turn off the lights",
                "Mia, turn the lights off",
                "Mia, turn the lights off please",
                "Mia, please turn the lights off",
                "Mia, lights off",
                "Mia, disable the lights in the room",
                "Mia, turn the room lights off please",
                "Mia, make it dark here",
                #endregion               
                //FAN ON
                #region
                "Mia, turn on the fan",
                "Mia, please turn on the fan",
                "Mia, turn on the fan please",
                "Mia, turn the fan on",
                "Mia, turn the fan on please",
                "Mia, please turn the fan on",
                "Mia, fan on please",
                //FAN OFF
                "Mia, turn off the fan",
                "Mia, please turn off the fan",
                "Mia, turn off the fan please",
                "Mia, turn the fan off",
                "Mia, turn the fan off please",
                "Mia, please turn the fan off",
                "Mia, fan off please",
                #endregion
                //OUTLET RELAY
                #region
                "Mia, turn on the outlet",
                "Mia, outlet power on",
                "Mia, please turn on the outlet",
                "Mia, apply voltage to the outlet",
                "Mia, turn off the outlet",
                "Mia, outlet power off",
                "Mia, please turn off the outlet",
                "Mia, dont apply voltage to the outlet",              
                #endregion
                //TEMPERATURE
                "Mia, what is the temperature now",
                "Mia, what is the temperature",
                "Mia, tell me how hot it is",
                "Mia, tell me how cold it is",
                "Mia, check the temperature please",
                "Mia, check the temperature",
                //TIME
                "Mia, what time is it",
                "Mia, tell me the time",
                "Mia, please tell me the time",
                "Mia, tell me the time please",
                //MUSIC
                "Mia, play some tunes",
                "Mia, give me the beat",
                "Mia, play some music",
                "Mia, play me some music",
                //FOLDERS
                "Mia, open my documents folder",
                //NEWTONS LAW
                "Mia, state newtons first law of motion",
                "Mia, state newtons second law of motion",
                "Mia, state newtons third law of motion",
                "Mia, what is the first newtons law of motion",
                "Mia, what is the second newtons law of motion",
                "Mia, what is the third newtons law of motion"

            });
            //GRAMMAR SETTINGS
            #region
            GrammarBuilder gBuilder = new GrammarBuilder(list);
            Grammar grammar = new Grammar(gBuilder);

            try
            {

                recEngine.RequestRecognizerUpdate();
                recEngine.LoadGrammar(grammar);
                recEngine.SpeechRecognized += recEngine_SpeechRecognized;
                recEngine.SetInputToDefaultAudioDevice();
                recEngine.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch { return; }
            #endregion
            InitializeComponent();
        }
        public static void KillProg(String program)
        {
            Process[] procs = null;
            try
            {
                procs = Process.GetProcessesByName(program);
                Process prog = procs[0];
                if (!prog.HasExited)
                {
                    prog.Kill();
                }
            }
            finally
            {
                if (procs != null)
                {
                    foreach (Process p in procs)
                    {
                        p.Dispose();
                    }
                }
            }
        }
        public void Say(string miaSpeak)
        {
            miaSynth.Speak(miaSpeak);
            miaText.AppendText(miaSpeak + "\n");
        }
        //RESPONSES
        #region
        //Greeting Strings
        #region
        String[] greetings = new String[5] { "Hello sir im here.", "Hello sir.", "Hi sir.", "Mia present!", "Sir, what can i do for you?" };
        public String greetings_action()
        {
            Random rand = new Random();
            return greetings[rand.Next(5)];
        }
        //Lights ON Response
        String[] lightsOnResponse = new String[3] { "Lights are now enabled sir.", "Let there be light!", "Enabling lights now." };
        public String lightsOn_action()
        {
            Random rand1 = new Random();
            return lightsOnResponse[rand1.Next(3)];
        }

        //Lights OFF Response      
        String[] lightsOffResponse = new String[3] { "Lights are now disabled sir.", "Let there be, no light!", "Disabling lights now." };
        public String lightsOff_action()
        {
            Random rand2 = new Random();
            return lightsOffResponse[rand2.Next(3)];
        }

        //Fan ON Response
        String[] fanOnResponse = new String[3] { "Turning the fan ON, please wait.", "Searching the Google how to turn ON the fan", "Please wait, I'll turn on the fan" };
        public String fanOn_action()
        {
            Random rand3 = new Random();
            return fanOnResponse[rand3.Next(3)];
        }

        //Fan OFF Response
        String[] fanOffResponse = new String[3] { "Turning the fan OFF, please wait.", "Searching the Google how to turn OFF the fan", "Please wait, I'll turn off the fan" };
        public String fanOff_action()
        {
            Random rand4 = new Random();
            return fanOffResponse[rand4.Next(3)];
        }

        //OUTLET Response
        String[] outletResponse = new String[3] { "applying voltage, please wait.", "Remembering V equals to I R, please wait", "Sir, i am now making the electrons move, please wait" };
        public String outlet_action()
        {
            Random rand5 = new Random();
            return outletResponse[rand5.Next(3)];
        }

        //TIME Response
        String[] timeResponse = new String[3] { "It is now " + DateTime.Now.ToString("h:mm tt"), "Time is now " + DateTime.Now.ToString("h:mm tt") + " .Time check brought to you by Mia", "Wish we could turn back time, to the good old days. " + DateTime.Now.ToString("h:mm tt") };
        public String time_action()
        {
            Random rand6 = new Random();
            return timeResponse[rand6.Next(3)];
        }
        #endregion
        #endregion
        public void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            String myVoice = e.Result.Text;

            //GREETINGS
            #region
            if (myVoice == "Hello there mia")
            {
                miaAndroidless = true;
                Say(greetings_action());
            }
            if (myVoice == "Mia wake up now")
            {
                miaAndroidless = true;
                Say(greetings_action());
            }
            if (myVoice == "Mia, can you please introduce yourself")
            {
                miaAndroidless = true;
                Say("Hello! I am Mia, Your personal assistant. Mia stands for, My, Personal Assistant. Im here to help!");
            }
            #endregion
            if (miaAndroidless == true)
            {
                //LIGHTS ON
                #region
                if (myVoice == "Mia, lights on please")
                {
                    Say(lightsOn_action());
                    portOp.Open();
                    portOp.WriteLine("a");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn on the lights")
                {
                    Say(lightsOn_action());
                    portOp.Open();
                    portOp.WriteLine("a");
                    portOp.Close();
                }
                if (myVoice == "Mia, please turn on the lights")
                {
                    Say(lightsOn_action());
                    portOp.Open();
                    portOp.WriteLine("a");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn the lights on")
                {
                    Say(lightsOn_action());
                    portOp.Open();
                    portOp.WriteLine("a");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn the lights on please")
                {
                    Say(lightsOn_action());
                    portOp.Open();
                    portOp.WriteLine("a");
                    portOp.Close();
                }
                if (myVoice == "Mia, please turn the lights on")
                {
                    Say(lightsOn_action());
                    portOp.Open();
                    portOp.WriteLine("a");
                    portOp.Close();
                }
                if (myVoice == "Mia, lights on")
                {
                    Say(lightsOn_action());
                    portOp.Open();
                    portOp.WriteLine("a");
                    portOp.Close();
                }
                if (myVoice == "Mia, light up the room")
                {
                    Say(lightsOn_action());
                    portOp.Open();
                    portOp.WriteLine("e");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn the room light on please")
                {
                    Say(lightsOn_action());
                    portOp.Open();
                    portOp.WriteLine("e");
                    portOp.Close();
                }
                if (myVoice == "Mia, make it bright here")
                {
                    Say(lightsOn_action());
                    portOp.Open();
                    portOp.WriteLine("e");
                    portOp.Close();
                }
                if (myVoice == "Mia, disable the lights in the room")
                {
                    Say(lightsOff_action());
                    portOp.Open();
                    portOp.WriteLine("E");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn the room lights off please")
                {
                    Say(lightsOff_action());
                    portOp.Open();
                    portOp.WriteLine("E");
                    portOp.Close();
                }
                if (myVoice == "Mia, make it dark here")
                {
                    Say(lightsOff_action());
                    portOp.Open();
                    portOp.WriteLine("E");
                    portOp.Close();
                }
                #endregion
                //LIGHTS OFF
                #region
                if (myVoice == "Mia, lights off please")
                {
                    Say(lightsOff_action());
                    portOp.Open();
                    portOp.WriteLine("A");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn off the lights")
                {
                    Say(lightsOff_action());
                    portOp.Open();
                    portOp.WriteLine("A");
                    portOp.Close();
                }
                if (myVoice == "Mia, please turn off the lights")
                {
                    Say(lightsOff_action());
                    portOp.Open();
                    portOp.WriteLine("A");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn the lights off")
                {
                    Say(lightsOff_action());
                    portOp.Open();
                    portOp.WriteLine("A");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn the lights off please")
                {
                    Say(lightsOff_action());
                    portOp.Open();
                    portOp.WriteLine("A");
                    portOp.Close();
                }
                if (myVoice == "Mia, please turn the lights off")
                {
                    Say(lightsOff_action());
                    portOp.Open();
                    portOp.WriteLine("A");
                    portOp.Close();
                }
                if (myVoice == "Mia, lights off")
                {
                    Say(lightsOff_action());
                    portOp.Open();
                    portOp.WriteLine("A");
                    portOp.Close();
                }
                #endregion
                //FAN ON
                #region
                if (myVoice == "Mia, turn on the fan")
                {
                    Say(fanOn_action());
                    portOp.Open();
                    portOp.WriteLine("b");
                    portOp.Close();
                }
                if (myVoice == "Mia, please turn on the fan")
                {
                    Say(fanOn_action());
                    portOp.Open();
                    portOp.WriteLine("b");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn on the fan please")
                {
                    Say(fanOn_action());
                    portOp.Open();
                    portOp.WriteLine("b");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn the fan on")
                {
                    Say(fanOn_action());
                    portOp.Open();
                    portOp.WriteLine("b");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn the fan on please")
                {
                    Say(fanOn_action());
                    portOp.Open();
                    portOp.WriteLine("b");
                    portOp.Close();
                }
                if (myVoice == "Mia, please turn the fan on")
                {
                    Say(fanOn_action());
                    portOp.Open();
                    portOp.WriteLine("b");
                    portOp.Close();
                }
                if (myVoice == "Mia, fan on please")
                {
                    Say(fanOn_action());
                    portOp.Open();
                    portOp.WriteLine("b");
                    portOp.Close();
                }
                #endregion
                //FAN OFF
                #region
                if (myVoice == "Mia, turn off the fan")
                {
                    Say(fanOff_action());
                    portOp.Open();
                    portOp.WriteLine("B");
                    portOp.Close();
                }
                if (myVoice == "Mia, please turn off the fan")
                {
                    Say(fanOff_action());
                    portOp.Open();
                    portOp.WriteLine("B");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn off the fan please")
                {
                    Say(fanOff_action());
                    portOp.Open();
                    portOp.WriteLine("B");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn the fan off")
                {
                    Say(fanOff_action());
                    portOp.Open();
                    portOp.WriteLine("B");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn the fan off please")
                {
                    Say(fanOff_action());
                    portOp.Open();
                    portOp.WriteLine("B");
                    portOp.Close();
                }
                if (myVoice == "Mia, please turn the fan off")
                {
                    Say(fanOff_action());
                    portOp.Open();
                    portOp.WriteLine("B");
                    portOp.Close();
                }
                if (myVoice == "Mia, fan off please")
                {
                    Say(fanOff_action());
                    portOp.Open();
                    portOp.WriteLine("B");
                    portOp.Close();
                }

                #endregion
                //DOOR LOCK
                #region
                if (myVoice == "Mia, turn on the outlet")
                {
                    Say(outlet_action());
                    portOp.Open();
                    portOp.WriteLine("c");
                    portOp.Close();
                }
                if (myVoice == "Mia, outlet power on")
                {
                    Say(outlet_action());
                    portOp.Open();
                    portOp.WriteLine("c");
                    portOp.Close();
                }
                if (myVoice == "Mia, please turn on the outlet")
                {
                    Say(outlet_action());
                    portOp.Open();
                    portOp.WriteLine("c");
                    portOp.Close();
                }
                if (myVoice == "Mia, apply voltage to the outlet")
                {
                    Say(outlet_action());
                    portOp.Open();
                    portOp.WriteLine("c");
                    portOp.Close();
                }
                if (myVoice == "Mia, turn off the outlet")
                {
                    Say(outlet_action());
                    portOp.Open();
                    portOp.WriteLine("C");
                    portOp.Close();
                }
                if (myVoice == "Mia, outlet power off")
                {
                    Say(outlet_action());
                    portOp.Open();
                    portOp.WriteLine("C");
                    portOp.Close();
                }
                if (myVoice == "Mia, please turn off the outlet")
                {
                    Say(outlet_action());
                    portOp.Open();
                    portOp.WriteLine("C");
                    portOp.Close();
                }
                if (myVoice == "Mia, dont apply voltage to the outlet")
                {
                    Say(outlet_action());
                    portOp.Open();
                    portOp.WriteLine("C");
                    portOp.Close();
                }
                #endregion
                //TIME
                #region
                if (myVoice == "Mia, what time is it")
                {
                    Say(time_action());
                }
                if (myVoice == "Mia, tell me the time")
                {
                    Say(time_action());
                }
                if (myVoice == "Mia, please tell me the time")
                {
                    Say(time_action());
                }
                if (myVoice == "Mia, tell me the time please")
                {
                    Say(time_action());
                }
                #endregion
                //MUSIC
                #region
                if (myVoice == "Mia, play some tunes")
                {
                    Say("Ok sir, i'll play my favorite song, please wait");
                    Process.Start(@"C:\Users\user\Documents\MIA\MUSIC FILES\Mkf.mp3");
                }
                if (myVoice == "Mia, give me the beat")
                {
                    Say("Ok sir, i'll play my favorite song, please wait");
                    Process.Start(@"C:\Users\user\Documents\MIA\MUSIC FILES\Mkf.mp3");
                }
                if (myVoice == "Mia, play some music")
                {
                    Say("Ok sir, i'll play my favorite song, please wait");
                    Process.Start(@"C:\Users\user\Documents\MIA\MUSIC FILES\Mkf.mp3");
                }
                if (myVoice == "Mia, play me some music")
                {
                    Say("Ok sir, i'll play my favorite song, please wait");
                    Process.Start(@"C:\Users\user\Documents\MIA\MUSIC FILES\Mkf.mp3");
                }
                #endregion
                //FOLDER
                #region
                if (myVoice == "Mia, play me some music")
                {
                    Say("Now opening the my documents folder sir, please wait");
                    Process.Start(@"C:\Users\user\Documents");
                }
                #endregion
                //NEWTONS LAW
                #region
                if (myVoice == "Mia, state newtons first law of motion")
                {
                    Say("A body continues in a state of rest or in uniform motion in a straight line, unless acted upod an external force");
                }
                if (myVoice == "Mia, what is the first newtons law of motion")
                {
                    Say("A body continues in a state of rest or in uniform motion in a straight line, unless acted upod an external force");
                }
                if (myVoice == "Mia, state newtons second law of motion")
                {
                    Say("The rate of change of momentum of a body is proportional to the externally applied force and takes place in the direction in which the force acts.");                 
                }
                if (myVoice == "Mia, what is the second newtons law of motion")
                {
                    Say("The rate of change of momentum of a body is proportional to the externally applied force and takes place in the direction in which the force acts.");
                }
                if (myVoice == "Mia, state newtons third law of motion")
                {
                    Say("For every action, there is an equal and opposite reaction.");                   
                }
                if (myVoice == "Mia, what is the third newtons law of motion")
                {
                    Say("For every action, there is an equal and opposite reaction.");
                }
                #endregion
                //TEMPERATURE
                if (myVoice == "Mia, what is the temperature now")
                {
                    portOp.Open();
                    portOp.WriteLine("d");
                    string tempVal = portOp.ReadLine();
                    Say("The temperature is about " + tempVal + " degrees celcius");
                    portOp.Close();
                }
                if (myVoice == "Mia, what is the temperature")
                {
                    portOp.Open();
                    portOp.WriteLine("d");
                    string tempVal = portOp.ReadLine();
                    Say("The temperature is about " + tempVal + " degrees celcius");
                    portOp.Close();
                }
                if (myVoice == "Mia, tell me how hot it is")
                {
                    portOp.Open();
                    portOp.WriteLine("d");
                    string tempVal = portOp.ReadLine();
                    Say("The temperature is about " + tempVal + " degrees celcius");
                    portOp.Close();
                }
                if (myVoice == "Mia, tell me how cold it is")
                {
                    portOp.Open();
                    portOp.WriteLine("d");
                    string tempVal = portOp.ReadLine();
                    Say("The temperature is about " + tempVal + " degrees celcius");
                    portOp.Close();
                }
                if (myVoice == "Mia, check the temperature please")
                {
                    portOp.Open();
                    portOp.WriteLine("d");
                    string tempVal = portOp.ReadLine();
                    Say("The temperature is about " + tempVal + " degrees celcius");
                    portOp.Close();
                }
                if (myVoice == "Mia, check the temperature")
                {
                    portOp.Open();
                    portOp.WriteLine("d");
                    string tempVal = portOp.ReadLine();
                    Say("The temperature is about " + tempVal + " degrees celcius");
                    portOp.Close();
                }
                userText.AppendText(myVoice + "\n");
            }

        }
        private void miaOpen_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            miaMain miaMainTab1 = new miaMain();
            this.Hide();
            miaMainTab1.ShowDialog();
        }
    }
}
