using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum.Telemetry
{
    public class ProtobufTelemetryWriter: ITelemetryWriter
    {
        public int MaxRecordsPerSaving { get; protected set; }
        public void Start(string directoryPath = null)
        {
            IsRecoring = true;
            StartRecordTime = DateTime.Now;

            notSavedData = new List<Frame>(MaxRecordsPerSaving);
            directoryPath = directoryPath ?? Directory.GetCurrentDirectory();
            var timeStr = DateTime.Now.ToString("HH-mm-ss__dd_mm_yyyy");
            DataFilePath = Path.Combine(directoryPath, "u"+timeStr+".telemetry");
            //Writes frame's length Header at start of the file:
            using (var file = new FileStream(DataFilePath, FileMode.Create)) {
                file.Write(BitConverter.GetBytes((long)0), 0, sizeof(long));
            }
            
        }
        public void Stop()
        {
            SaveData();
            //Writes head at the end of the file
            var head = new Header
            {
                Description = "Ursa telemetry file",
                RecordStart = this.StartRecordTime,
                RecordEnd = DateTime.Now,
                FramesCount = this.FramesCount,
            };
            using (var file = new FileStream(DataFilePath, FileMode.Append))
            {
                ProtoBuf.Serializer
                    .SerializeWithLengthPrefix(file, head, ProtoBuf.PrefixStyle.Fixed32);
            }
            FramesCount = 0;
            IsRecoring = false;
        }
        
        public bool IsRecoring { get; protected set; }
        public int FramesCount { get; protected set; }
        public DateTime LastAddition { get; protected set; }
        public DateTime StartRecordTime { get; protected set; }
        public void Add(Frame frame)
        {
            frame.MsecFromLastFrame = LastAddition == default(DateTime)?0:Convert.ToInt32((DateTime.Now - LastAddition).TotalMilliseconds);
            frame.Num = FramesCount;
            FramesCount++;
            notSavedData.Add(frame);
            LastAddition = DateTime.Now;         
            if(MaxRecordsPerSaving>0 && notSavedData.Count >= MaxRecordsPerSaving)
                SaveData();
        }

        public string DataFilePath { get; protected set; }

        List<Frame> notSavedData;

        void SaveData() {
            if (notSavedData.Count > 0)
            {
                using (var file = new FileStream(DataFilePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    file.Position = file.Length;
                    ProtoBuf.Serializer
                        .SerializeWithLengthPrefix(file, notSavedData.ToArray(), ProtoBuf.PrefixStyle.Fixed32);
                    //Updates the FrameLength parameter
                    var len = file.Length;
                    file.Position = 0;
                    file.Write(BitConverter.GetBytes(file.Length), 0, sizeof(long));
                }
                notSavedData.Clear();
            }
        }
    }
}
