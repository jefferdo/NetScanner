using IPRangerClass;
using System;
using System.Collections.Generic;
using System.Net;

namespace NetScanner.Model
{
    public class Network
    {
        private IPAddress networkID;
        private IPAddress mask;
        private String name;
        private int hostCount;

        private IList<IPAddress> ip_range;

        public Network(IPAddress networkID, IPAddress mask)
        {
            this.networkID = networkID;
            this.mask = mask;
            var ip_ranger = new IPRanger(networkID, mask);
            this.ip_range = ip_ranger.getIPRange();
            this.hostCount = this.ip_range.Count;
        }

        public IList<IPAddress> getIPRange()
        {
            return ip_range;
        }
    }
}