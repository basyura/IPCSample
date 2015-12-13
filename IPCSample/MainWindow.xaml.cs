using System;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Data;

namespace IPCClient
{
    [Serializable]
    public class ViewModel
    {
        public string Message { get; set; }

        public void OnChanged(IPCEventArg e)
        {
        }
    }
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary></summary>
        private IPCData _data;
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            ViewModel vm = new ViewModel();
            this.DataContext = vm;
            Top  = 50;
            Left = 50;

            IpcClientChannel channel = new IpcClientChannel("IPCSampleChannel", null);
            ChannelServices.RegisterChannel(channel, true);
            StringBuilder buf = new StringBuilder();
            buf.AppendFormat("Name : {0}{1}", channel.ChannelName,     Environment.NewLine);
            Message.Text = buf.ToString();

            _data = (IPCData)Activator.GetObject(typeof(IPCData), "ipc://IPCSamplePort/IPCSampleURI");

            //_data.OnChanged += vm.OnChanged;

            new TaskFactory().StartNew(() =>{
                while(true)
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Message.Text = _data.Name;
                        TextValue.Text = _data.Name;
                    }));
                    Thread.Sleep(100);                    
                }
            });
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TextValue.Text = _data.Name;
            string text = TextValue.Text;
            Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                _data.Name = text;
            }));
        }

        private void TextValue_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _data.Name = ((TextBox)sender).Text;
        }
    }
}
