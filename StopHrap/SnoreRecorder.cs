using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnoreRecorder
{
    class SnoreRecorder
    {
        private const string fileName = "snoreProf.dat";

        private bool isRecording;
        private float[] meanBuff;
        private int dataGotCounter;
        private float[] recordedBuff;

        public IEnumerable<float> RecordedBuff => recordedBuff;
        public bool IsRecording => isRecording;

        public void StartRecording()
        {
            if (!isRecording)
            {
                meanBuff = null;
                dataGotCounter = 0;
                isRecording = true;
            }
        }

        public void StopRecording()
        {
            //
            if (isRecording)
            {
                isRecording = false;
                var meanValues = meanBuff.Select(mb => mb / dataGotCounter).ToArray();
                if (recordedBuff == null)
                {
                    recordedBuff = new float[meanBuff.Length];
                    recordedBuff = meanValues;
                }
                else
                {
                    for (int i = 0; i < meanValues.Length; i++)
                    {
                        recordedBuff[i] = (recordedBuff[i] + meanValues[i]) / 2;
                    }
                }
                SaveRecord();
            }
        }

        public void GetData(IEnumerable<float> data)
        {
            if (isRecording)
            {
                var dataArray = data.ToArray();
                meanBuff = new float[dataArray.Length];
                for (int i = 0; i < dataArray.Length; i++)
                {
                    meanBuff[i] += dataArray[i];
                }
                dataGotCounter++;
            }
        }

        public void SaveRecord()
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                var byteArr = new byte[recordedBuff.Length * 4];
                Buffer.BlockCopy(recordedBuff, 0, byteArr, 0, byteArr.Length);
                bw.Write(byteArr);
            }
        }
    }
}
