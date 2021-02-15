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
        public AudioRecorder(Window caller)
        {
            _window = caller;
            //Keyboard.AddKeyDownHandler(_window, new KeyEventHandler(OnKeyDown));
        }

        private void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.S)
            {
                StartRecord("default-name.wav");
            }
            if (keyEventArgs.Key == Key.F)
            {
                StopRecording();
            }
        }

        public void StartRecord(string filePath)
        {
            _outputFilename = filePath;
            _waveIn = new WaveIn();
            _waveIn.DeviceNumber = 0;
            _waveIn.DataAvailable += waveIn_DataAvailable;
            _waveIn.RecordingStopped += waveIn_RecordingStopped;
            
            _waveIn.WaveFormat = new WaveFormat(8000, 1);
            
            _writer = new WaveFileWriter(_outputFilename, _waveIn.WaveFormat);
            
            _waveIn.StartRecording();
        }
        
        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            _writer.WriteData(e.Buffer, 0, e.BytesRecorded);
        }
        
        private void StopRecording()
        {
            MessageBox.Show("StopRecording");
            _waveIn.StopRecording();
        }
        
        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            _waveIn.Dispose();
            _waveIn = null;
            _writer.Close();
            _writer = null;
        }
        
        private WaveIn _waveIn;
        private WaveFileWriter _writer;
        private string _outputFilename = "default-name.wav";
        private readonly Window _window;
    }
}