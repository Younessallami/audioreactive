
using System;
using System.Collections;
using System.Collections.Generic;

namespace OscJack
{
    // OSC data directory class
    // Provides last received data for each address.
    public class OscDirectory : IEnumerable<KeyValuePair<string, Object[]>>
    {
        #region Public Methods

        public OscDirectory(int port) : this(new int[]{port})
        {
        }

        public OscDirectory(int[] portList)
        {
            _dataStore = new Dictionary<string, Object[]>();
            _servers = new OscServer[portList.Length];
            for (var i = 0; i < portList.Length; i++) {
                _servers[i] = new OscServer(portList[i]);
                _servers[i].Start();
            }
        }

        public int TotalMessageCount {
            get {
                UpdateState();
                return _totalMessageCount;
            }
        }

        public bool HasData(string address)
        {
            return _dataStore.ContainsKey(address);
        }

        public Object[] GetData(string address)
        {
            UpdateState();
            Object[] data;
            _dataStore.TryGetValue(address, out data);
            return data;
        }

        #endregion

        #region Enumerable Interface

        public IEnumerator<KeyValuePair<string, Object[]>> GetEnumerator()
        {
            return _dataStore.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dataStore.GetEnumerator();
        }

        #endregion

        #region Private Objects And Functions

        Dictionary<string, Object[]> _dataStore;
        OscServer[] _servers;
        int _totalMessageCount;

        void UpdateState()
        {
            foreach (var server in _servers) {
                while (server.MessageCount > 0) {
                    var message = server.PopMessage();
                    _dataStore[message.address] = message.data;
                    _totalMessageCount++;
                }
            }
        }

        #endregion
    }
}
