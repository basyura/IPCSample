using System;
using System.Collections.Generic;

namespace Data
{
    /// <summary> </summary>
    public delegate void CallEventHandler(IPCEventArg e);
    public delegate void ToolAddedEventHandler(IPCEventArg e, string tool);

    public class IPCData : MarshalByRefObject
    {
        /// <summary> </summary>
        public event CallEventHandler OnChanged;
        /// <summary> </summary>
        public event ToolAddedEventHandler ToolAdded;
        /// <summary> </summary>
        private string _Name;
        public string Name {
            get { return _Name; }
            set { _Name = value;
                  Fire();
            }
        }
        /// <summary></summary>
        private List<string> _tools = new List<string>();
        public void AddTool(string value)
        {
            if (!_tools.Contains(value))
            {
                _tools.Add(value);
            }
            if (ToolAdded != null)
            {
            }
            IPCEventArg arg = new IPCEventArg()
            {
                Data = this
            };
            ToolAdded(arg, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        public void Fire()
        {
            if (OnChanged != null)
            {
                IPCEventArg arg = new IPCEventArg()
                {
                    Data = this
                };
                OnChanged(arg);
            }
        }
        /// <summary>
        /// 有効期限を切る
        /// </summary>
        /// <returns></returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
