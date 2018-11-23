using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace StopHrap
{
    [Activity(Label = "Успокаивающий звук")]
    public class CalmSoundSettingsActivity : Activity
    {
        PlayAudio playAudio = new PlayAudio();
        RecordAudio recordAudio = new RecordAudio();
        NotificationManager nMan = new NotificationManager();


        EditText txtEditCorrelationCoefficient;
        Spinner spinnerCurrentSoundName;


        private const int REQUEST_PERMISSION_CODE = 1000;
        


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.CalmSoundSettingsLayout);
            Initialise();

        }

        

        private void Initialise()
        {
            spinnerCurrentSoundName = FindViewById<Spinner>(Resource.Id.spinnerCurrentSoundName);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, SoundNames());
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerCurrentSoundName.Adapter = adapter;


            txtEditCorrelationCoefficient = FindViewById<EditText>(Resource.Id.txtEditCorrelationCoefficient);
            var btnRecordSound = FindViewById<Button>(Resource.Id.btnRecordSound);
            btnRecordSound.Click += BtnRecordSound_Click;
        }

        private string[] SoundNames()
        {
            string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath.ToString();
            string[] temparray = { "Apple", "Banana", "Cantaloupe" };
            return temparray;
        }

        private async void BtnRecordSound_Click(object sender, EventArgs e)
        {
            if (txtEditCorrelationCoefficient.Text.Length <= 0)
            {
                txtEditCorrelationCoefficient.RequestFocus();
                return;
            }
            if (((Button)sender).Text == "Записать")
            {
                
                recordAudio.FilePath = $"{Android.OS.Environment.ExternalStorageDirectory.AbsolutePath.ToString()}/{txtEditCorrelationCoefficient.Text}_{DateTime.Now}.mp4";
                await recordAudio.StartAsync();
                ((Button)sender).Text = "Остановить";
            }
            else
            {
                recordAudio.Stop();
                ((Button)sender).Text = "Записать";
            }
        }

    }
}