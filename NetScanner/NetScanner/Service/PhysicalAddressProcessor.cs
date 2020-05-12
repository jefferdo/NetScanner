using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetScanner.Service
{
    public class PhysicalAddressProcessor
    {
        private String macAddress;
        private byte[] mac;
        private PhysicalAddress physicalAddress;

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
            var MacAddress = string.Join(":", this.physicalAddress.GetAddressBytes().Select(b => b.ToString("X2")));
            try
            {
                var uri = new Uri("http://api.macvendors.com/" + WebUtility.UrlEncode(MacAddress));
                using (var wc = new HttpClient())
                    return wc.GetStringAsync(uri).Result;
            }
            catch (Exception)
            {
                return "n/a";
            }
        }

        public string getMacString()
        {
            return string.Join(":", this.physicalAddress.GetAddressBytes().Select(b => b.ToString("X2")));
        }
    }
}