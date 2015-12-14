using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Text;
using System.Windows;
using Data;

namespace IPCServer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPCData _data;

        public MainWindow()
        {
            InitializeComponent();
            Top  = 50;
            Left = 600;

            Dictionary<string, string> prop = new Dictionary<string,string>
            {
                { "PortName", "IPCSample" }
            };

            //IpcChannel chanel = new IpcChannel(prop, null, new BinaryServerFormatterSinkProvider {
            //                        TypeFilterLevel = TypeFilterLevel.Full
            //                    });
            IpcServerChannel channel = new IpcServerChannel("IPCSampleChannel", "IPCSamplePort");
            ChannelServices.RegisterChannel(channel, true);

            _data = new IPCData()
            {
                Name = "hoge"
            };
            RemotingServices.Marshal(_data, "IPCSampleURI", typeof(IPCData));
            //RemotingConfiguration.RegisterWellKnownServiceType(
            //    typeof(IPCData), "IPCSampleURI", 
            //    System.Runtime.Remoting.WellKnownObjectMode.Singleton);
            _data.ToolAdded += (d, tool) => {
                IpcClientChannel cl = new IpcClientChannel("IPCSampleChannel_" + tool, null);
                ChannelServices.RegisterChannel(cl, true);
                IPCData cldata = (IPCData)Activator.GetObject(typeof(IPCData), "ipc://IPCSamplePort2/IPCSampleURI_" + tool);
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    TextValue.Text = "!!!!!!!!!!!!!!!";
                }));
            };

            _data.OnChanged += (d) => {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    TextValue.Text = d.Data.Name;
                }));
            };

            StringBuilder buf = new StringBuilder();
            buf.AppendFormat("Name : {0}{1}", channel.ChannelName,     Environment.NewLine);
            buf.AppendFormat("URI  : {0}{1}", channel.GetChannelUri(), Environment.NewLine);
            foreach(string uri in channel.GetUrlsForUri("IPCSampleURI"))
            {
                buf.Append(uri).Append(Environment.NewLine);
            }

            Message.Text = buf.ToString();

        }

        private void TextValue_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _data.Name = ((TextBox)sender).Text;
        }
    }
}
