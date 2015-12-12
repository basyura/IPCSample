using System;

namespace Data
{
    [Serializable]
    public class IPCEventArg : MarshalByRefObject
    {
        public IPCData Data { get; set; }
    }
}
