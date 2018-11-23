using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SnoreRecorder
{
    class SoundProcessor
    {
        private float[] meanBuff;
        private double sampleRate;
        private List<float> samplesList;
        private const double fMin = 10;
        private const double fMax = 8000;

        public float ThresholdLevel { get; set; }
        public float MeanValueThreshold { get; set; }

        public SoundProcessor(int sampRate = 44100)
        {
            sampleRate = sampRate;
        }

        public IEnumerable<float> ProcessSound(IEnumerable<float> samples)
        {

            samplesList = samples.ToList();
            if (IsOverMean())
            {
                return ThresholdData(makeFFT(samplesList));
            }
            else
            {
                return Enumerable.Empty<float>();
            }
        }

        private IEnumerable<float> makeFFT(IEnumerable<float> samps)
        {
            var sampsWithIndx = samps.Select((s, i) => new { sample = s, indx = i });
            var N = samps.Count();
            var fLowest = sampleRate / N;
            var fftData = new List<Complex>();
            for (double k = fMin / fLowest; k < fMax / fLowest; k *= Math.Pow(2, 1.0 / 8.0))
            {
                var compSum = new Complex();
                foreach (var sampWithIndx in sampsWithIndx)
                {
                    var phase = 2 * Math.PI * k * sampWithIndx.indx / N;
                    compSum += Complex.FromPolarCoordinates(sampWithIndx.sample, phase);
                }
                fftData.Add(compSum);
            }
            var maxMag = fftData.Max(fd => fd.Magnitude);
            return fftData.Select(d => (float)(d.Magnitude / maxMag));
        }

        private IEnumerable<float> ThresholdData(IEnumerable<float> data)
        {
            return data.Select(d => d > ThresholdLevel ? d : 0);
        }

        private bool IsOverMean()
        {
            var positiveValues = samplesList.Where(s => s > 0);
            return positiveValues.Sum() / positiveValues.Count() > MeanValueThreshold;
        }

        #region Obsolete
        private void AddToMeanBuff(IEnumerable<float> data)
        {
            var dataArr = data.ToArray();
            if (meanBuff == null)
            {
                meanBuff = new float[dataArr.Length];
            }
            for (int i = 0; i < dataArr.Length; i++)
            {
                meanBuff[i] += dataArr[i];
            }
        }
        #endregion
    }
}

//QweQwe5