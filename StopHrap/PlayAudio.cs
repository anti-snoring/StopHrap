using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using System.Threading.Tasks;

namespace StopHrap
{
    //
    // Shows how to use the MediaPlayer class to play audio.
    class PlayAudio : INotificationReceiver
    {
        MediaPlayer player = null;
        public string FilePath { get; set; }

        public async Task StartPlayerAsync()
        {
            try
            {
                if (player == null)
                {
                    player = new MediaPlayer();
                }
                else
                {
                    player.Reset();
                }

                // This method works better than setting the file path in SetDataSource. Don't know why.
                Java.IO.File file = new Java.IO.File(FilePath);
                Java.IO.FileInputStream fis = new Java.IO.FileInputStream(file);
                await player.SetDataSourceAsync(fis.FD);

                //player.SetDataSource(filePath);
                player.Prepare();
                player.Start();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }

        public void StopPlayer()
        {
            if ((player != null))
            {
                if (player.IsPlaying)
                {
                    player.Stop();
                }
                player.Release();
                player = null;
            }
        }

        public async Task StartAsync()
        {
            await StartPlayerAsync();
        }

        public void Stop()
        {
            this.StopPlayer();
        }
    }
}