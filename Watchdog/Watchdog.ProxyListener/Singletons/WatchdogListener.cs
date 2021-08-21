using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Watchdog.ProxyListener.Exceptions;
using Watchdog.ProxyListener.Models;
using Watchdog.ProxyListener.Singletons.Logging;

namespace Watchdog.ProxyListener.Singletons
{
    public class WatchdogListener
    {
        private Config _config { get; }

        private TcpListener tcpListener { get; set; }

        private ILogger _logger { get; set; }

        public bool Listening { get; set; } = false;

        private Thread _listenerThread { get; set; }

        private Dictionary<Thread, TcpClient> _clients = new Dictionary<Thread, TcpClient>();

        public Thread listenerThread
        {
            get { return _listenerThread; }
            private set { _listenerThread = value; }
        }

        public WatchdogListener(Config config, ILogger logger)
        {
            this._config = config;
            this._logger = logger;
            tcpListener = new TcpListener(IPAddress.Parse(config.WatchdogListenerIP), config.WatchdogListenerPort);
        }

        public void StartListener() {
            this.listenerThread = new Thread(ListenerThread);
            this.Listening = true;
            listenerThread.Start();
        }

        public void TerminateAllClients()
        {
            foreach (var client in _clients.Values)
            {
                client.Close();
            }
        }

        private void ListenerThread()
        {
            _logger.Log($"Started WatchdogListener Service on Port {_config.WatchdogListenerPort} With Thread ID: {listenerThread.ManagedThreadId}", LogSeverity.INFO);
            tcpListener.Start();
            _logger.Log("Waiting for connectings...", LogSeverity.INFO);
            while (this.Listening)
            {
                
                TcpClient client = tcpListener.AcceptTcpClient();
                _logger.Log("Client Connected!", LogSeverity.INFO);

                Thread thread = new Thread(new ParameterizedThreadStart(HandleClient));
                thread.Start(client);

                _clients.Add(thread, client);
            }
            _logger.Log($"Stopped WatchdogListener Service!");
        }

        private void HandleClient(object obj)
        {
            _logger.Log($"Created new thread for client with ID {Thread.CurrentThread.ManagedThreadId}", LogSeverity.INFO);
            
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            string imei = String.Empty;

            string data = null;
            Byte[] bytes = new byte[256];
            int i;

            try
            {
                while(client.Connected && (i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    _logger.Log($"Client {Thread.CurrentThread.ManagedThreadId} Recieved:", LogSeverity.DEBUG);
                    _logger.Log(data, LogSeverity.DEBUG, ConsoleColor.Magenta);

                    string str = "Hello Client";
                    Byte[] reply = Encoding.ASCII.GetBytes(str);
                    stream.Write(reply, 0, reply.Length);
                    _logger.Log($"Sent to Client {Thread.CurrentThread.ManagedThreadId}:", LogSeverity.DEBUG);
                    _logger.Log(str, LogSeverity.DEBUG, ConsoleColor.Magenta);
                    client.Close();
                }
            }
            catch (Exception e)
            {
                _logger.Log($"An exception occured while trying to read incoming data from a client", LogSeverity.ERROR);
                _logger.Log(e.ToString(), LogSeverity.STACK);
                _logger.Log($"Closing connection with client.", LogSeverity.ERROR);
                client.Close();
            }
        }
    }
}
