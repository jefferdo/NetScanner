using IPRangerClass;
using MacAddressVendorLookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var interfaces = new NetInterface();
            /*foreach (var interface_ in NetInterface.getInterfaces())
            {
                var mac = interface_.macAddress;
                Console.WriteLine("Interface: " + interface_.name + " Mac: " + mac.ToString() + " IP : " + interface_.ipv4.ToString() + " Subnet: " + interface_.mask.ToString() + " Vendor: " + interface_.vendor);
            }
            Console.ReadKey();*/

            var ipranger = new IPRanger(IPAddress.Parse("192.168.1.27"), IPAddress.Parse("255.255.255.197"));
            var iplist = ipranger.getIPRange();

            Console.WriteLine("Network ID: " + iplist[0]);
            Console.WriteLine("No. Of Host IPs: " + (iplist.Count - 2).ToString());
            Console.WriteLine("Broadcast ID: " + iplist[iplist.Count - 1]);

            Console.ReadKey();
        }
    }

    internal class Scan
    {
        public List<NetInterfaceInfo> getNetworkInterfaces()
        {
            var interfaces = new List<NetInterfaceInfo>();
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
                            interfaces.Add(new NetInterfaceInfo()
                            {
                                name = interfacename,
                                macAddress = interfaceMac,
                                ipv4 = ip.Address,
                                mask = ip.IPv4Mask,
                                vendor = getNICVendor(interfaceMac)
                            });
                        }
                    }
                }
            }

            return interfaces;
        }

        public static bool pingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        public string getMacByIp(string ip)
        {
            var macIpPairs = GetAllMacAddressesAndIppairs();
            int index = macIpPairs.FindIndex(x => x.IpAddress == ip);
            if (index >= 0)
            {
                return macIpPairs[index].MacAddress.ToUpper();
            }
            else
            {
                return null;
            }
        }

        public List<MacIpPair> GetAllMacAddressesAndIppairs()
        {
            List<MacIpPair> mip = new List<MacIpPair>();
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "arp";
            pProcess.StartInfo.Arguments = "-a ";
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();
            string cmdOutput = pProcess.StandardOutput.ReadToEnd();
            string pattern = @"(?<ip>([0-9]{1,3}\.?){4})\s*(?<mac>([a-f0-9]{2}-?){6})";

            foreach (Match m in Regex.Matches(cmdOutput, pattern, RegexOptions.IgnoreCase))
            {
                mip.Add(new MacIpPair()
                {
                    MacAddress = m.Groups["mac"].Value,
                    IpAddress = m.Groups["ip"].Value
                });
            }

            return mip;
        }

        public static string getNICVendor(String macaddress)
        {
            byte[] arr = macaddress.Split(macaddress.Contains(":") ? ':' : '-').Select(x => Convert.ToByte(x, 16)).ToArray();
            return getNICVendor(new PhysicalAddress(arr));
        }

        public static string getNICVendor(PhysicalAddress macaddress)
        {
            var vendorInfoProvider = new MacVendorBinaryReader();
            using (var resourceStream = ManufBinResource.GetStream().Result)
            {
                vendorInfoProvider.Init(resourceStream).Wait();
            }
            var addressMatcher = new MacAddressVendorLookup.AddressMatcher(vendorInfoProvider);
            var vendorInfo = addressMatcher.FindInfo(macaddress);
            if (vendorInfo != null)
            {
                var spaceIndex = vendorInfo.Organization.IndexOf("\t");
                return vendorInfo.Organization.Substring(spaceIndex).Trim();
            }
            else
                return "n/a";
        }

        internal struct MacIpPair
        {
            public string MacAddress;
            public string IpAddress;
        }

        internal struct NetInterfaceInfo
        {
            public PhysicalAddress macAddress;
            public String name;
            public IPAddress ipv4;
            public IPAddress mask;
            public String vendor;
        }
    }
}