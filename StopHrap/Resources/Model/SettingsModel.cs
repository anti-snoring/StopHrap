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
using SQLite;

namespace StopHrap.Resources.Model
{
    public class SettingsModel
    {
        [PrimaryKey]
        public int Id { get; set; }
        public decimal CompareCoef { get; set; }
        public decimal OpenThreshold { get; set; }
        public decimal SoundThreshold { get; set; }
        public int SnoreCount { get; set; }
        public int CounterCooldownTime { get; set; }
        public decimal CorrelationCoefficient { get; set; }
    }
}