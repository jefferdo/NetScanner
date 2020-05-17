using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace NetScanner.Service
{
    public class Storage
    {
        private string filename;
        private const string ext = ".sshot";
        private const string dirPath = @"snapshots";
        public List<Snapshotfile> Snapshotfiles { get; private set; }

        public struct activeNode
        {
            public IPAddress ip { get; set; }
            public string hostname { get; set; }
            public PhysicalAddress mac { get; set; }
            public string vendor { get; set; }
        }

        public struct Snapshotfile
        {
            public int recodeCount { get; set; }
            public IPAddress netid { get; set; }
            public IPAddress mask { get; set; }
            public DateTime datetime { get; set; }
            public List<activeNode> activeNodes { get; set; }
            public string key { get; set; }
        }

        public Storage(string datetime, IList<activeNode> activeNodes, IPAddress netid, IPAddress mask)
        {
            filename = datetime + "_" + netid.ToString() + "_" + mask.ToString() + '_' + RandomString(5) + ext;
            createDir();
            var myFile = File.Create(dirPath + "/" + filename);
            myFile.Close();
            TextWriter tw = new StreamWriter(dirPath + "/" + filename);
            foreach (var line in activeNodes)
            {
                tw.WriteLine(line.ip + "," + line.hostname + "," + new PhysicalAddressProcessor(line.mac).getMacString());
            }
            tw.Close();
        }

        public Storage()
        {
            createDir();
            Snapshotfiles = new List<Snapshotfile>();

            DirectoryInfo d = new DirectoryInfo(dirPath);
            FileInfo[] Files = d.GetFiles("*" + ext); 
            foreach (FileInfo file in Files)
            {
                Debug.WriteLine(file);
                try
                {
                    var date = file.Name.Split('_')[0].Split('-');
                    var netid_ = file.Name.Split('_')[1];
                    var mask_ = file.Name.Split('_')[2];
                    var key_ = file.Name.Split('_')[3].Split('.')[0];
                    var datetime = new DateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2]),
                    Convert.ToInt32(date[3]), Convert.ToInt32(date[4]), Convert.ToInt32(date[5]));
                    var filepath = dirPath + @"\" + file.Name;
                    var lines = File.ReadLines(filepath);

                    Snapshotfile snapshotfile = new Snapshotfile();
                    snapshotfile.netid = IPAddress.Parse(netid_);
                    snapshotfile.mask = IPAddress.Parse(mask_);
                    snapshotfile.datetime = datetime;
                    snapshotfile.recodeCount = lines.Count();
                    snapshotfile.activeNodes = new List<activeNode>();
                    snapshotfile.key = key_;

                    foreach (var line in lines)
                    {
                        Debug.WriteLine(line);
                        try
                        {
                            var seg = line.Split(',');
                            var mac_ = new PhysicalAddressProcessor(seg[2]);
                            snapshotfile.activeNodes.Add(new activeNode
                            {
                                ip = IPAddress.Parse(seg[0]),
                                hostname = seg[1],
                                mac = mac_.physicalAddress
                            });
                        }
                        catch (Exception)
                        {
                        }
                    }
                    Snapshotfiles.Add(snapshotfile);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    continue;
                }
            }
        }

        private static Random random = new Random();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static void createDir()
        {
            try { Directory.CreateDirectory(dirPath); } catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        public Snapshotfile findFile(string datetime, string key)
        {
            Snapshotfile snapshotfile_ = new Snapshotfile();
            var datetime_ = DateTime.Parse(datetime);
            foreach (var snapshotfile in Snapshotfiles)
            {
                if (snapshotfile.datetime == datetime_ && snapshotfile.key == key)
                {
                    snapshotfile_ = snapshotfile;
                }
            }
            return snapshotfile_;
        }
    }
}