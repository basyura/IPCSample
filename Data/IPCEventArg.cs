using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Web.Script.Serialization;

namespace Data
{
    [Serializable]
    public class IPCEventArg : MarshalByRefObject
    {
        public IPCData Data { get; set; }
    }
}
