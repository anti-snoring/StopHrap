using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Media;
using SnoreRecorder;

namespace StopHrap
{
    [Activity(Label = "Process")]
    public class ProcessActivity : Activity
    {
        private bool doProcess;
        private AudioRecord ar;
        private SoundProcessor sp;
        private SnoreDetector sd;
        private int sampleRate = 22050;
        private int bufferLength = 22050;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ar = new AudioRecord(AudioSource.Mic, sampleRate, ChannelIn.Mono, Android.Media.Encoding.Default, bufferLength);
            sp = new SoundProcessor(sampleRate);
            sd = new SnoreDetector();
        }

        protected override async void OnStart()
        {
            byte[] buffer = new byte[bufferLength];
            short[] values = new short[bufferLength / 2];

            try
            {
                ar.StartRecording();

                doProcess = true;

                while (doProcess)
                {
                    await ar.ReadAsync(buffer, 0, bufferLength);

                    Buffer.BlockCopy(buffer, 0, values, 0, bufferLength);

                    var snoreDFT = sp.ProcessSound(values.Select(v => (float)v));


                }
            }

            catch
            {
                throw;
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            doProcess = false;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            doProcess = false;
        }
    }
}