using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blits_Audio
{
    public partial class BlitsAudio : Form
    {


        // Create class-level accessible variables to store the audio recorder and capturer instance
        private WaveFileWriter RecordedAudioWriter = null;
        private WasapiLoopbackCapture CaptureInstance = null;

        public BlitsAudio()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string outputFilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\system_recorded_audio.wav";
           // string outputFilePath = @"C:\Users\domsd\Desktop\system_recorded_audio.wav";

            // Redefine the capturer instance with a new instance of the LoopbackCapture class
            CaptureInstance = new WasapiLoopbackCapture();

            // Redefine the audio writer instance with the given configuration
            RecordedAudioWriter = new WaveFileWriter(outputFilePath, CaptureInstance.WaveFormat);

            // When the capturer receives audio, start writing the buffer into the mentioned file
            CaptureInstance.DataAvailable += (s, a) =>
            {
                RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
            };

            // When the Capturer Stops
            CaptureInstance.RecordingStopped += (s, a) =>
            {
                RecordedAudioWriter.Dispose();
                RecordedAudioWriter = null;
                CaptureInstance.Dispose();
            };

            // Enable "Stop button" and disable "Start Button"
            button1.Enabled = false;
            button2.Enabled = true;

            // Start recording !
            CaptureInstance.StartRecording();

        }
       
        private void button2_Click(object sender, EventArgs e)
        {

             // Stop recording !
            CaptureInstance.StopRecording();
            // Enable "Start button" and disable "Stop Button"
            button1.Enabled = true;
            button2.Enabled = false;
        

            
          
        }


    }
}
