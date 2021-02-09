using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Input;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;
using NAudio.WinForms;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;

namespace TrackzamClient
{
    public class AudioRecorder
    {
        WaveIn waveIn;
        WaveFileWriter writer;
        private string outputFilename = "default-name";
        private Window _window;
        
        public AudioRecorder(Window caller)
        {
            _window = caller;
            Keyboard.AddKeyDownHandler(_window, new KeyEventHandler(OnKeyDown));
        }

        private void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.S)
            {
                StartRecord();
            }
            if (keyEventArgs.Key == Key.F)
            {
                StopRecording();
            }
        }

        void StartRecord()
        {
            MessageBox.Show("Start Recording");
            waveIn = new WaveIn();
            waveIn.DeviceNumber = 0;
            waveIn.DataAvailable += waveIn_DataAvailable;
            waveIn.RecordingStopped += waveIn_RecordingStopped;
            
            waveIn.WaveFormat = new WaveFormat(8000, 1);
            
            writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
            
            waveIn.StartRecording();
        }
        
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer.WriteData(e.Buffer, 0, e.BytesRecorded);
        }
        
        void StopRecording()
        {
            MessageBox.Show("StopRecording");
            waveIn.StopRecording();
        }
        
        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            waveIn.Dispose();
            waveIn = null;
            writer.Close();
            writer = null;
        }

    }
}