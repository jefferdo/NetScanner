using System;
using System.Net;
using System.Net.NetworkInformation;

namespace NetScanner.Model
{
    public class Network
    {
        private IPAddress myipv4;
        private IPAddress mask;
        private PhysicalAddress mac;
        private String name;

        internal struct node
        {
            public PhysicalAddress macAddress;
            public IPAddress ipv4;
            public IPAddress mask;
        }
    }
}