
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace OscJack
{
    // OSC over UDP server class
    public class OscServer
    {
        Thread _thread;
        UdpClient _udpClient;
        IPEndPoint _endPoint;
        OscParser _osc;

        public bool IsRunning {
            get { return _thread != null && _thread.IsAlive; }
        }

        public int MessageCount {
            get {
                lock (_osc) return _osc.MessageCount;
            }
        }

        public OscMessage PopMessage()
        {
            lock (_osc) return _osc.PopMessage();
        }

        public OscServer(int listenPort)
        {
            _endPoint = new IPEndPoint(IPAddress.Any, listenPort);
            _udpClient = new UdpClient(_endPoint);
            _udpClient.Client.ReceiveTimeout = 1000;
            _osc = new OscParser();
        }

        public void Start()
        {
            if (_thread == null) {
                _thread = new Thread(ServerLoop);
                _thread.Start();
            }
        }

        public void Close()
        {
            _udpClient.Close();
        }

        void ServerLoop()
        {
            try {
                while (true) {
                    var data = _udpClient.Receive(ref _endPoint);
                    lock (_osc) _osc.FeedData(data);
                }
            }
            catch (SocketException)
            {
                // Shutdown: nothing to do here.
            }
        }
    }
}
