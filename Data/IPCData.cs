using System;
using System.Collections.Generic;

namespace Data
{
    public class IPCData : MarshalByRefObject
    {
        /// <summary> </summary>
        public delegate void CallEventHandler(IPCEventArg e);
        /// <summary> </summary>
        public event CallEventHandler OnChanged;
        /// <summary> </summary>
        private string _Name;
        public string Name {
            get { return _Name; }
            set { _Name = value;
                  Fire();
            }
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
