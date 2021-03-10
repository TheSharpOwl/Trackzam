using System;
using System.IO;
using System.Windows.Threading;
using NAudio.Wave;

namespace TrackzamClient
{
    public class AudioRecorder
    {
        public AudioRecorder(int sampleRate, int bits, int volumeLogUpdatePeriodMilliseconds)
        {
            _sampleRate = sampleRate;
            _bits = bits;
            _volumeLogUpdatePeriod = volumeLogUpdatePeriodMilliseconds;
        }

        public void StartRecord(string filePath)
        {
            _outputFilename = filePath;
            _waveIn = new WaveIn();
            _waveIn.DeviceNumber = 0;
            
            _waveIn.DataAvailable += waveIn_DataAvailable;
            _waveIn.RecordingStopped += waveIn_RecordingStopped;
            
            _waveIn.WaveFormat = new WaveFormat(_sampleRate, _bits, 1);
            
            _writer = new WaveFileWriter(_outputFilename+"\\microphone.wav", _waveIn.WaveFormat);
            _audioVolumeWriter = new StreamWriter(_outputFilename + "\\audioVolume.txt");
            
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += OnTimerTick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, _volumeLogUpdatePeriod);
            _dispatcherTimer.Start();
            
            _waveIn.StartRecording();
        }
        
        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            _writer.Write(e.Buffer, 0, e.BytesRecorded);

            if (_canLogVolume)
            {
                LogVolume(e);
                _canLogVolume = false;
            }
        }

        private void LogVolume(WaveInEventArgs e)
        {
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

        private void OnTimerTick(object? sender, EventArgs eventArgs)
        {
            _canLogVolume = true;
        }
        
        public void StopRecording()
        {
            _dispatcherTimer.Stop();
            _waveIn.StopRecording();
        }
        
        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            _waveIn.Dispose();
            _waveIn = null;
            _writer.Close();
            _writer = null;
            _audioVolumeWriter.Close();
        }
        
        private WaveIn _waveIn;
        private WaveFileWriter _writer;
        private StreamWriter _audioVolumeWriter;
        private DispatcherTimer _dispatcherTimer;

        private bool _canLogVolume = false;

        private int _sampleRate = 8000;
        private int _bits = 16;
        private int _volumeLogUpdatePeriod = 1000;

        private string _outputFilename = "default-name.wav";
    }
}