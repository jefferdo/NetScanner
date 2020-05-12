using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Test
{
    public class NetInterface
    {
        public String macAddress { get; set; }
        public String name { get; set; }
        public String ipv4 { get; set; }
        public String mask { get; set; }
        public String vendor { get; set; }

        public static IList<NetInterface> getInterfaces()
        {
            var nt = new List<NetInterface>();
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    var interfacename = ni.Name;
                    var interfaceMac = ni.GetPhysicalAddress();
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            var mac = new PhysicalAddressProcessor(interfaceMac);
                            nt.Add(new NetInterface()
                            {
                                name = interfacename,
                                macAddress = interfaceMac.ToString(),
                                ipv4 = ip.Address.ToString(),
                                mask = ip.IPv4Mask.ToString(),
                                vendor = mac.getNICVendor().Result
                            });
                        }
                    }
                }
            }

            return nt;
        }
    }
}