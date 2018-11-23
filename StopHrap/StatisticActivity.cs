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

namespace StopHrap
{
    [Activity(Label = "Статистика")]
    public class StatisticActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.StatisticLayout);
            Initialise();

        }

        private void Initialise()
        {
            /*Data section*/
            //Коэффициент сравнения
            var txtEditCompareCoef = FindViewById<EditText>(Resource.Id.txtEditCompareCoef);
            //Порог открытия
            var txtEditOpenThreshold = FindViewById<EditText>(Resource.Id.txtEditOpenThreshold);
            //Порог шума
            var txtEditSoundThreshold = FindViewById<EditText>(Resource.Id.txtEditSoundThreshold);
            //Кол-во всхрапов
            var txtEditSnoreCount = FindViewById<EditText>(Resource.Id.txtEditSnoreCount);
            //Cooldown счётчика(сек)
            var txtEditCounterCooldownTime = FindViewById<EditText>(Resource.Id.txtEditCounterCooldownTime);
            //Коэффициент кореляции
            var txtEditCorrelationCoefficient = FindViewById<EditText>(Resource.Id.txtEditCorrelationCoefficient);

            /*Buttons section*/
            var btnDefaulSettings = FindViewById<Button>(Resource.Id.btnDefaulSettings);
            var btnLoadProfile = FindViewById<Button>(Resource.Id.btnLoadProfile);
            var btnRecordProfile = FindViewById<Button>(Resource.Id.btnRecordProfile);
            var btnRecordCalmSound = FindViewById<Button>(Resource.Id.btnRecordCalmSound);
            var btnSaveSettings = FindViewById<Button>(Resource.Id.btnSaveSettings);

            btnDefaulSettings.Click += BtnDefaulSettings_Click;
            btnLoadProfile.Click += BtnLoadProfile_Click;
            btnRecordProfile.Click += BtnRecordProfile_Click;
            btnRecordCalmSound.Click += BtnRecordCalmSound_Click;
            btnSaveSettings.Click += BtnSaveSettings_Click;
        }

        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}