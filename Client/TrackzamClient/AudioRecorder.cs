using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
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
            _audioVolumeWriter = new StreamWriter(_outputFilename + "\\audioVolume.txt");
            _waveIn.StartRecording();
        }
        
        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            _writer.Write(e.Buffer, 0, e.BytesRecorded);

            float volume = 0;
            // interpret as 16 bit audio
            for (int index = 0; index < e.BytesRecorded; index += 2)
            {
                short sample = (short)((e.Buffer[index + 1] << 8) |
                                       e.Buffer[index + 0]);
                // to floating point
                var sample32 = sample / 32768f;
                // absolute value 
                if (sample32 < 0) sample32 = -sample32;
                // is this the max value?
                if (sample32 > volume) volume = sample32;
            }

            volume = (float)Math.Round(volume, 3);

            _audioVolumeWriter.WriteLine("{0} {1}",  TrackzamTimer.GetTimestampString(), volume);
        }
        
        public void StopRecording()
        {
            _waveIn.StopRecording();
            _audioVolumeWriter.Close();
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
        private StreamWriter _audioVolumeWriter;

        private string _outputFilename = "default-name.wav";
        
    }
}