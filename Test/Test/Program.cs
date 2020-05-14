﻿using MacAddressVendorLookup;
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

            var ipranger = new IPRanger(IPAddress.Parse("192.168.1.27"), IPAddress.Parse("255.255.255.197"));
            var iplist = ipranger.getIPRange();

            Console.WriteLine("Network ID: " + iplist[0]);
            Console.WriteLine("No. Of Host IPs: " + (iplist.Count - 2).ToString());
            Console.WriteLine("Broadcast ID: " + iplist[iplist.Count - 1]);

            */

            var hostip = "192.168.1.54";
            var mask = "255.255.255.0";

            var mask_b = getByteArray(mask.ToString());
            var netID_b = getNetworkID(hostip, mask);
            var brodcastID_b = getBrodcastID(hostip, mask);

            string netid = String.Join(".", netID_b.Select(x => Convert.ToInt64(x, 2)));
            Console.WriteLine(netid);
            string brodcastid = String.Join(".", brodcastID_b.Select(x => Convert.ToInt64(x, 2)));
            Console.WriteLine(String.Join(".", brodcastid));
            Console.WriteLine(getSubnetSuffix(mask));
            var hostCount = noOfHost(mask);
            Console.WriteLine(hostCount);

            string ip = netid;
            for (var i = 0; i < hostCount; i++)
            {
                ip = getNextIpAddress(ip, 1);
                if (ip == hostip)
                {
                    Console.WriteLine(ip + " <<< ");
                }
                else
                {
                    Console.WriteLine(ip);
                }
            }

            Console.ReadKey();
        }

        internal static string[] getByteArray(String ipAddress)
        {
            return (ipAddress.Split('.').Select(x => Convert.ToString(Int32.Parse(x), 2).PadLeft(8, '0'))).ToArray();
        }

        internal static string[] getNetworkID(string ip, string mask)
        {
            var hostip_b = getByteArray(ip.ToString());
            var mask_b = getByteArray(mask.ToString());
            List<string> netID = new List<string>();

            if (hostip_b.Length == mask_b.Length)
            {
                for (var i = 0; i < hostip_b.Length; i++)
                {
                    String seg = "";
                    for (var o = 0; o < hostip_b[i].Length; o++)
                    {
                        var nbit = Byte.Parse(hostip_b[i][o].ToString()) & Byte.Parse(mask_b[i][o].ToString());
                        seg += nbit.ToString();
                    }
                    netID.Add(seg);
                }
            }
            else
            {
                throw new ArgumentException("Invalid IP address or subnet mask");
            }
            return netID.ToArray();
        }

        internal static string[] getBrodcastID(string hostip, string mask)
        {
            var mask_b = getByteArray(mask.ToString());
            var netID_b = getNetworkID(hostip, mask);
            List<string> brodcastID = new List<string>();

            for (var i = 0; i < netID_b.Length; i++)
            {
                String seg = "";
                for (var o = 0; o < netID_b[i].Length; o++)
                {
                    var nbit = Byte.Parse(netID_b[i][o].ToString()) | Byte.Parse(((mask_b[i][o] == Convert.ToChar("1")) ? "0" : "1").ToString());
                    seg += nbit.ToString();
                }
                brodcastID.Add(seg);
            }

            return brodcastID.ToArray();
        }

        internal static int getSubnetSuffix(string mask)
        {
            var mask_b = getByteArray(mask.ToString());
            var suffix = 0;
            foreach (var digit in String.Join("", mask_b))
            {
                if (digit.ToString() == "1")
                {
                    suffix++;
                }
            }

            return suffix;
        }

        internal static int noOfHost(string mask)
        {
            return Convert.ToInt32(Math.Pow(2, getSubnetSuffix("255.255.255.255") - getSubnetSuffix(mask)) - 2);
        }

        private static string getNextIpAddress(string ipAddress, uint increment)
        {
            byte[] addressBytes = IPAddress.Parse(ipAddress).GetAddressBytes().Reverse().ToArray();
            uint ipAsUint = BitConverter.ToUInt32(addressBytes, 0);
            var nextAddress = BitConverter.GetBytes(ipAsUint + increment);
            return String.Join(".", nextAddress.Reverse());
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