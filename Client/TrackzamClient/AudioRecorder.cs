using System;
using System.Windows;
using NAudio.Wave;

namespace TrackzamClient
{
    public class AudioRecorder
    {
        public void StartRecord(string filePath)
        {
            _outputFilename = filePath;
            _waveIn = new WaveIn();
            _waveIn.DeviceNumber = 0;
            _waveIn.DataAvailable += waveIn_DataAvailable;
            _waveIn.RecordingStopped += waveIn_RecordingStopped;
            
            _waveIn.WaveFormat = new WaveFormat(8000, 1);
            
            _writer = new WaveFileWriter(_outputFilename+"\\microphone.wav", _waveIn.WaveFormat);
            
            _waveIn.StartRecording();
        }
        
        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            _writer.Write(e.Buffer, 0, e.BytesRecorded);
        }
        
        public void StopRecording()
        {
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
    }
}