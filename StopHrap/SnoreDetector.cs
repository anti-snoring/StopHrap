using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnoreRecorder
{
    class SnoreDetector
    {
        private const string fileName = "snoreProf.dat";
        private float[] snoreProfile;
        private float[] snoreProfileSubMean;
        private float snoreProfMean;
        private float snoreProfDisp;

        public IEnumerable<float> SnoreProfile => snoreProfile;

        public SnoreDetector()
        {
            LoadSnoreProfile();
        }

        private bool LoadSnoreProfile()
        {
            if (!File.Exists(fileName))
            {
                return false;
            }

            using (var br = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                var bytes = new byte[1000];
                var bNum = br.Read(bytes, 0, bytes.Length);
                var buff = bytes.Take(bNum).ToArray();
                snoreProfile = new float[bNum / 4];
                Buffer.BlockCopy(buff, 0, snoreProfile, 0, bNum);
            }
            snoreProfMean = snoreProfile.Sum() / snoreProfile.Length;
            snoreProfileSubMean = snoreProfile.Select(sp => sp - snoreProfMean).ToArray();
            snoreProfDisp = (float)Math.Sqrt(snoreProfileSubMean.Select(s => Math.Pow(s, 2)).Sum());
            return true;
        }

        public float CalcCorrelationCoeff(IEnumerable<float> inputSnore)
        {
            if (snoreProfile == null)
            {
                if (!LoadSnoreProfile())
                {
                    return 0;
                    // throw new InvalidOperationException("Record a sample at first!");
                }
            }

            if (inputSnore.Count() != snoreProfile.Length)
            {
                throw new ArgumentException($"The size of input data has to be {snoreProfile.Length}");
            }

            var inpSnoreMean = inputSnore.Sum() / inputSnore.Count();
            var cov = inputSnore.Select((data, indx) => (data - inpSnoreMean) * snoreProfileSubMean[indx])
                                .Sum();
            var inpSnoreDisp = (float)Math.Sqrt(inputSnore.Select(data => Math.Pow(data - inpSnoreMean, 2)).Sum());

            return cov / inpSnoreDisp / snoreProfDisp;
        }

        public bool ReloadProfile()
        {
            return LoadSnoreProfile();
        }
    }
}
