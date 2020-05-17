using IPRangerClass;
using NetScanner.Service;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace NetScanner.Model
{
    public class NetInterface
    {
        public string macAddress { get; set; }
        public string name { get; set; }
        public string ipv4 { get; set; }
        public string netv4 { get; set; }
        public string mask { get; set; }
        public string vendor { get; set; }

        public IList<NetInterface> getInterfaces()
        {
            var nts = new List<NetInterface>();
            IPRanger ipr;
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    var interfacename = ni.Name;
                    var interfaceMac = ni.GetPhysicalAddress();
                    NetInterface nt = null;
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            var mac = new PhysicalAddressProcessor(interfaceMac);
                            ipr = new IPRanger(ip.Address, ip.IPv4Mask);
                            nt = new NetInterface()
                            {
                                name = interfacename,
                                macAddress = mac.getMacString(),
                                ipv4 = ip.Address.ToString(),
                                netv4 = ipr.getIPRange()[0].ToString(),
                                mask = ip.IPv4Mask.ToString(),
                                vendor = mac.getNICVendor()
                            };
                            nts.Add(nt);
                        }
                    }
                }
            }
            return nts;
        }
    }
}