using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using StopHrap.Resources.DataHelper;
using StopHrap.Resources.Model;

namespace StopHrap
{
    [Activity(Label = "Настройки")]
    public class SettingsActivity : Activity
    {
        DataBase db;
        SettingsModel defaultSettingsModel;//значения по умолчанию
        EditText txtEditCompareCoef;//Коэффициент сравнения
        EditText txtEditOpenThreshold;//Порог открытия
        EditText txtEditSoundThreshold;//Порог шума
        EditText txtEditSnoreCount;//Кол-во всхрапов
        EditText txtEditCounterCooldownTime;//Cooldown счётчика(сек)
        EditText txtEditCorrelationCoefficient;//Коэффициент кореляции

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SettingsLayout);
            Initialise();
            BindSettingsData(db.SelectTableSettigns()?.FirstOrDefault());
        }


        private void BindSettingsData(SettingsModel data)
        {
            if (data != null)
            {
                txtEditCompareCoef.Text = $"{data.CompareCoef}";
                txtEditOpenThreshold.Text = $"{data.OpenThreshold}";
                txtEditSoundThreshold.Text = $"{data.SoundThreshold}";
                txtEditSnoreCount.Text = $"{data.SnoreCount}";
                txtEditCounterCooldownTime.Text = $"{data.CounterCooldownTime}";
                txtEditCorrelationCoefficient.Text = $"{data.CorrelationCoefficient}";
            }
        }

        private void Initialise()
        {
            /*Initialise data*/
            db = new DataBase();
            
            defaultSettingsModel = new SettingsModel()
            {
                CompareCoef = decimal.Parse(Resources.GetString(Resource.String.default_CompareCoef)),
                OpenThreshold = decimal.Parse(Resources.GetString(Resource.String.default_OpenThreshold)),
                SoundThreshold = decimal.Parse(Resources.GetString(Resource.String.default_SoundThreshold)),
                SnoreCount = int.Parse(Resources.GetString(Resource.String.default_SnoreCount)),
                CounterCooldownTime = int.Parse(Resources.GetString(Resource.String.default_CounterCooldownTime)),
                CorrelationCoefficient = decimal.Parse(Resources.GetString(Resource.String.default_CorrelationCoefficient))
            };
            db.CreateDataBase(defaultSettingsModel);

            /*Data section*/
            
            txtEditCompareCoef = FindViewById<EditText>(Resource.Id.txtEditCompareCoef);
            txtEditOpenThreshold = FindViewById<EditText>(Resource.Id.txtEditOpenThreshold);
            txtEditSoundThreshold = FindViewById<EditText>(Resource.Id.txtEditSoundThreshold);
            txtEditSnoreCount = FindViewById<EditText>(Resource.Id.txtEditSnoreCount);
            txtEditCounterCooldownTime = FindViewById<EditText>(Resource.Id.txtEditCounterCooldownTime);
            txtEditCorrelationCoefficient = FindViewById<EditText>(Resource.Id.txtEditCorrelationCoefficient);

            /*Buttons section*/
            var btnDefaulSettings = FindViewById<Button>(Resource.Id.btnDefaulSettings);
            var btnLoadProfile = FindViewById<Button>(Resource.Id.btnLoadProfile);
            var btnRecordProfile = FindViewById<Button>(Resource.Id.btnRecordProfile);
            var btnRecordCalmSound = FindViewById<Button>(Resource.Id.btnRecordCalmSound);
            var btnSaveSettings = FindViewById<Button>(Resource.Id.btnSaveSettings);

            /*Events section*/
            btnDefaulSettings.Click += BtnDefaulSettings_Click;
            btnLoadProfile.Click += BtnLoadProfile_Click;
            btnRecordProfile.Click += BtnRecordProfile_Click;
            btnRecordCalmSound.Click += BtnRecordCalmSound_Click;
            btnSaveSettings.Click += BtnSaveSettings_Click;
        }

        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            var data = db.SelectTableSettigns()?.FirstOrDefault();
            if (data != null)
            {
                decimal dtmp;
                int itmp;

                decimal.TryParse(txtEditCompareCoef.Text, out dtmp);
                data.CompareCoef = dtmp;

                decimal.TryParse(txtEditCorrelationCoefficient.Text, out dtmp);
                data.CorrelationCoefficient = dtmp;

                decimal.TryParse(txtEditSoundThreshold.Text, out dtmp);
                data.SoundThreshold = dtmp;

                int.TryParse(txtEditCounterCooldownTime.Text, out itmp);
                data.CounterCooldownTime = itmp;

                decimal.TryParse(txtEditOpenThreshold.Text, out dtmp);
                data.OpenThreshold = dtmp;

                int.TryParse(txtEditSnoreCount.Text, out itmp);
                data.SnoreCount = itmp;

                db.UpdateTableSettigns(data);
            }
        }

        private void BtnRecordCalmSound_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnRecordProfile_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnLoadProfile_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnDefaulSettings_Click(object sender, EventArgs e)
        {
            BindSettingsData(defaultSettingsModel);
        }
    }
}
