using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tridic.Utils;

namespace Tridic.Algorithm
{
    internal class AlgorithmParameters
    {
        public string SourceFolder { get; set; }

        public string TargetFolder { get; set; }

        public ConcurrentQueue<(UIMessageType, string)> Channel { get; set; }

        public AlgorithmParameters(string sourceFolder, string targetFolder, ConcurrentQueue<(UIMessageType, string)> channel)
        {
            SourceFolder = sourceFolder;
            TargetFolder = targetFolder;
            Channel = channel;
        }
    }
}
