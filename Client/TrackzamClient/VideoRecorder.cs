using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AForge.Video;
using AForge.Video.DirectShow;
using FFMediaToolkit;
using FFMediaToolkit.Encoding;
using FFMediaToolkit.Graphics;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace TrackzamClient
{
    public class VideoRecorder
    {
        public VideoRecorder(int frameRate)
        {
            _frames = new List<Bitmap>();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += OnTimerTick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1.0/frameRate*1000.0));
        }

        public void StartRecording(string path)
        {
            _filePath = path;
            _frames.Clear();
            InitializeVideoCaptureDevice();
            _canAddFrame = true;
            _videoCaptureDevice.Start();
            dispatcherTimer.Start();
        }
        
        public void StopRecording()
        {
            _videoCaptureDevice.SignalToStop();
            dispatcherTimer.Stop();
            FFmpegLoader.FFmpegPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\FFMPEG";
            var settings = new VideoEncoderSettings(width: _width, height: _height, framerate: 1, codec: VideoCodec.H264);
            settings.EncoderPreset = EncoderPreset.Fast;
            settings.CRF = 17;
            
            using(var file = MediaBuilder.CreateContainer(_filePath+"\\videoCapture.mp4").WithVideo(settings).Create())
            {
                foreach (var bitmap in _frames)
                {
                    file.Video.AddFrame(ToImageData(Convert(bitmap)));
                }
            }
        }

        private void InitializeVideoCaptureDevice()
        {
            if (_videoCaptureDevice != null)
            {
                _videoCaptureDevice.NewFrame -= OnFrameReceived;  //unsubscribe previous device
            }
            
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            _videoCaptureDevice = new VideoCaptureDevice(videoDevices[0].MonikerString);
            
            VideoCapabilities capabilities = _videoCaptureDevice.VideoCapabilities[0];
            _videoCaptureDevice.VideoResolution = capabilities;
            _width = capabilities.FrameSize.Width;
            _height = capabilities.FrameSize.Height;
            
            _videoCaptureDevice.NewFrame += OnFrameReceived;
        }

        private void OnFrameReceived(object sender, NewFrameEventArgs eventArgs)
        {
            if (_canAddFrame)
            {
                _frames.Add(ChangePixelFormat(eventArgs.Frame, PixelFormat.Format32bppArgb));
                _canAddFrame = false;
            }
        }
        
        private void OnTimerTick(object? sender, EventArgs e)
        {
            _canAddFrame = true;
        }
        
        private BitmapSource Convert(System.Drawing.Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, _width, _height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
            
            var bitmapSource = BitmapSource.Create(
                _width, _height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                PixelFormats.Bgra32, null,
                bitmapData.Scan0, bitmapData.Stride * _height, bitmapData.Stride);
            bitmap.UnlockBits(bitmapData);
            return bitmapSource;
        }

        private Bitmap ChangePixelFormat(Image bitmap, PixelFormat pixelFormat)
        {
            var clone = new Bitmap(_width, _height, pixelFormat);
            using var gr = Graphics.FromImage(clone);
            gr.DrawImage(bitmap, new Rectangle(0, 0, _width, _height));

            return clone;
        }

        private ImageData ToImageData(BitmapSource bitmap)
        {
            var wb = new WriteableBitmap(bitmap);
            return ImageData.FromPointer(wb.BackBuffer, ImagePixelFormat.Bgra32, wb.PixelWidth, wb.PixelHeight);
        }

        private int _width;
        private int _height;
        private VideoCaptureDevice _videoCaptureDevice;
        private DispatcherTimer dispatcherTimer;
        private readonly List<Bitmap> _frames;
        private string _filePath;
        private bool _canAddFrame;
        private int _frameRate;
    }
}