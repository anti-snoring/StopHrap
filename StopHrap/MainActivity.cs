using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace StopHrap
{
    [Activity(Label = "Стоп храп", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Initialise();
        }

        private void Initialise()
        {
            var btnSettings = FindViewById<Button>(Resource.Id.btnSettings);
            var btnStatistic = FindViewById<Button>(Resource.Id.btnStatistic);
            var tgglBntStartOff = FindViewById<ToggleButton>(Resource.Id.tgglBntStartOff);

            btnSettings.Click += BtnSettings_Click;
            btnStatistic.Click += BtnStatistic_Click;
            tgglBntStartOff.CheckedChange += TgglBntStartOff_CheckedChange;
        }

        private void TgglBntStartOff_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (e.IsChecked)
            {
                //TODO Start scanning 
            }
            else
            {
                //TODO Stop scanning
            }
        }
        
        private void BtnStatistic_Click(object sender, System.EventArgs e)
        {
            Intent settingsLayout = new Intent(this, typeof(StatisticActivity));
            StartActivity(settingsLayout);
        }

        private void BtnSettings_Click(object sender, System.EventArgs e)
        {
            Intent settingsLayout = new Intent(this, typeof(SettingsActivity));
            StartActivity(settingsLayout);
        }
    }
}

