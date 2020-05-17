using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;

namespace NetScanner.Service
{
    public class PhysicalAddressProcessor
    {
        private String macAddress;
        private byte[] mac;
        public PhysicalAddress physicalAddress { get; private set; }

        public PhysicalAddressProcessor(String macaddress)
        {
            this.macAddress = macaddress;
            this.mac = macaddress.Split(macaddress.Contains(":") ? ':' : '-').Select(x => Convert.ToByte(x, 16)).ToArray();
            physicalAddress = new PhysicalAddress(this.mac);
        }

        public PhysicalAddressProcessor(PhysicalAddress physicalAddress)
        {
            this.physicalAddress = physicalAddress;
        }

        public string getNICVendor()
        {
            var MacAddress = string.Join("-", this.physicalAddress.GetAddressBytes().Select(b => b.ToString("X2")));
            try
            {
                string mac = MacAddress;
                string vendor = "n/a";
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "NetScanner.Resources.maclist.txt";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    foreach (string result in reader.ReadToEnd().Split('\n'))
                    {
                        var macinfo = result.Split('|');
                        if (macinfo[0] == mac.Substring(0, 8))
                        {
                            vendor = macinfo[1];
                            break;
                        }
                    }
                }
                return vendor;
            }
            catch (Exception)
            {
                return "n/a";
            }
        }

        public string getMacString()
        {
            try
            {
                return string.Join(":", this.physicalAddress.GetAddressBytes().Select(b => b.ToString("X2")));
            }
            catch (NullReferenceException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return "n/a";
            }
        }

        public static string getMacString(PhysicalAddress physicalAddress)
        {
            try
            {
                return string.Join(":", physicalAddress.GetAddressBytes().Select(b => b.ToString("X2")));
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.Message);
                return "n/a";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "n/a";
            }
        }
    }
}