
using System;

namespace OscJack
{
    // OSC master directory class
    // Provides the interface for the OSC master directory.
    public static class OscMaster
    {
        // UDP listening port number list
        static int[] listenPortList = { 9000 };

        // Determines whether any data has arrived to a given address.
        public static bool HasData(string address)
        {
            return _directory.HasData(address);
        }

        // Returns a data set which was sent to a given address.
        public static Object[] GetData(string address)
        {
            return _directory.GetData(address);
        }

        // Returns a reference to the master directory instance.
        public static OscDirectory MasterDirectory {
            get { return _directory; }
        }

        static OscDirectory _directory = new OscDirectory(listenPortList);
    }
}
