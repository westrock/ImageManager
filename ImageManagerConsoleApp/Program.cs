using Newtonsoft.Json.Linq;
using SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WindowsFirewallHelper;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            string response = string.Empty;
            ImageFileProcessor imageProcessor = new ImageFileProcessor();

            Console.WriteLine("Load and Analyze Files? => ");
            response = Console.ReadLine();

            if (response?.ToLower().StartsWith("y") ?? false)
            {
                Console.WriteLine("Delete all Files from Database? => ");
                response = Console.ReadLine();

                if (response?.ToLower().StartsWith("y") ?? false)
                {
                    SQL_ImageFileInfo.DeleteAll(imageProcessor.DBOConn);
                }

                //int fileCount = imageProcessor.AnalyzeFiles("^*.(BMP|NEF|JPG|JPEG|PNG|PSD|TIF|TIFF)$", @"E:\Pictures\2017\2017-08-05");
                int fileCount = imageProcessor.AnalyzeFiles("^*.(BMP|NEF|JPG|JPEG|PNG|PSD|TIF|TIFF)$", @"E:\Pictures");
            }

            if (imageProcessor.ImageFiles.Count == 0)
            {
                Console.WriteLine("Load from database? => ");
                response = Console.ReadLine();

                if (response?.ToLower().StartsWith("y") ?? false)
                {
                    FileGrouping fileGrouping;

                    Console.WriteLine("Load All(a), Singletons(s), or Multiples(m)? => ");
                    response = Console.ReadLine();

                    switch (response?.ToLower().PadRight(1).Substring(0, 1))
                    {
                        case "s":
                            fileGrouping = FileGrouping.Singleton;
                            break;
                        case "m":
                            fileGrouping = FileGrouping.Multiple;
                            break;
                        case "a":
                        default:
                            fileGrouping = FileGrouping.All;
                            break;
                    }

                    FileStatus fileStatus;

                    Console.WriteLine("Load All(a), Moved(y), or Not Moved(n)? => ");
                    response = Console.ReadLine();

                    switch (response?.ToLower().PadRight(1).Substring(0, 1))
                    {
                        case "y":
                            fileStatus = FileStatus.Moved;
                            break;
                        case "n":
                            fileStatus = FileStatus.NotMoved;
                            break;
                        case "a":
                        default:
                            fileStatus = FileStatus.All;
                            break;
                    }

                    int fileCount = imageProcessor.LoadFromDatabase(fileStatus, fileGrouping);
                }
            }


            //int fileCount = imageProcessor.AnalyzeFiles("^*.(nef|NEF)$", @"\\MEGAMINI\edward_figarsky\Documents\WindowsData\DCIM\100NCD80");

            //int fileCount = imageProcessor.AnalyzeFiles("^*.(BMP|NEF|JPG|JPEG|PNG|PSD|TIF|TIFF)$", @"E:\AdobeStockPhotos");

            if ((imageProcessor.ImageFiles.Count > 0) && (imageProcessor.ImageFiles.Where(i => i.Value.Count == 1).Count() > 0))
            {
                Console.WriteLine("Output Singleton Files? => ");
                response = Console.ReadLine();

                if (response?.ToLower().StartsWith("y") ?? false)
                {

                    imageProcessor.ProcessSingletonImages(@"E:\PhotoRepository");
                    imageProcessor.GenerateReport($@"E:PictureReport_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.csv", ",");
                }
            }
            //IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(@"\\192.168.1.148\Macintosh HD\Users\edward_figarsky 1\Pictures\Dupe\IMG_0605.JPG");
            //var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
            //var dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTime);

            //foreach (var directory in directories)
            //    foreach (var tag in directory.Tags)
            //        Console.WriteLine($"{ directory.Name} - {tag.Name} = {tag.Description}");

            //var poop = imageProcessor.ImageFiles.Where(i => i.Value.Count > 1).SelectMany(s => s.Value).Distinct(new ImageFileInfo_Comparer());

            //foreach(var imageList in imageProcessor.ImageFiles.Where(i => i.Value.Count > 1))
            //{
            //    var distinctImages = imageList.Value.Distinct(new ImageFileInfo_Comparer());

            //    foreach (var extraItem in imageList.Value.Where(v => !distinctImages.ToList().Exists(d => d.FileFullPath == v.FileFullPath)).ToList())
            //    {
            //        imageList.Value.Remove(extraItem);
            //    }
            //}

            Console.WriteLine("Press Enter to Finish => ");
            response = Console.ReadLine();

        }


        static void IPAddressExperiment()
        {
            bool ruleExists = false;
            String ipaddress = "1.22.44.66";
            List<WindowsFirewallHelper.Addresses.NetworkAddress> remoteList;

            IPAddress address = IPAddress.Parse(ipaddress);
            List<WindowsFirewallHelper.Addresses.NetworkAddress> addressList = new List<WindowsFirewallHelper.Addresses.NetworkAddress>();
            addressList.Add(new WindowsFirewallHelper.Addresses.NetworkAddress(address));
            addressList.Add(new WindowsFirewallHelper.Addresses.NetworkAddress(IPAddress.Parse("22.33.44.55")));


            string BLOCK_RULE_NAME = "BlockProbes";

            var newPortRule = FirewallManager.Instance.Rules.FirstOrDefault(r => r.Name == BLOCK_RULE_NAME);

            if (newPortRule == null)
            {
                newPortRule = FirewallManager.Instance.CreatePortRule(FirewallProfiles.Domain | FirewallProfiles.Private | FirewallProfiles.Public, BLOCK_RULE_NAME, FirewallAction.Block, 0);
                remoteList = new List<WindowsFirewallHelper.Addresses.NetworkAddress>();
            }
            else
            {
                remoteList = newPortRule.RemoteAddresses.Cast<WindowsFirewallHelper.Addresses.NetworkAddress>().ToList();
                ruleExists = remoteList.Exists(i => i.Address.Equals(address));

                if (!ruleExists)
                {
                    newPortRule.Name = $"BlockProbes_{DateTime.Now.ToString("yyyy-MM-dd_HHmmss.fff")}";

                    newPortRule = FirewallManager.Instance.CreatePortRule(FirewallProfiles.Domain | FirewallProfiles.Private | FirewallProfiles.Public, BLOCK_RULE_NAME, FirewallAction.Block, 0);
                }
            }

            if (!ruleExists)
            {
                newPortRule.Direction = FirewallDirection.Inbound;
                newPortRule.Protocol = FirewallProtocol.Any;

                remoteList.Add(new WindowsFirewallHelper.Addresses.NetworkAddress(address));
                newPortRule.RemoteAddresses = remoteList.ToArray();

                FirewallManager.Instance.Rules.Add(newPortRule);
            }


            return;

            string jsonString = @"
{
   ""ip"":""113.53.29.4"",
   ""type"":""ipv4"",
   ""continent_code"":""AS"",
   ""continent_name"":""Asia"",
   ""country_code"":""TH"",
   ""country_name"":""Thailand"",
   ""region_code"":""67"",
   ""region_name"":""Changwat Phetchabun"",
   ""city"":""Phetchabun"",
   ""zip"":""67170"",
   ""latitude"":16.4433,
   ""longitude"":101.1475,
   ""location"":{
      ""geoname_id"":1607737,
      ""capital"":""Bangkok"",
      ""languages"":[
         {
            ""code"":""th"",
            ""name"":""Thai"",
            ""native"":""\u0e44\u0e17\u0e22 \/ Phasa Thai""
         }
      ],
      ""country_flag"":""http:\/\/assets.ipstack.com\/flags\/th.svg"",
      ""country_flag_emoji"":""\ud83c\uddf9\ud83c\udded"",
      ""country_flag_emoji_unicode"":""U+1F1F9 U+1F1ED"",
      ""calling_code"":""66"",
      ""is_eu"":false
   }
}";

            JObject responseObject = JObject.Parse(jsonString);
            string continentCode = responseObject.SelectToken("continent_code")?.ToString() ?? string.Empty;
            string continentName = responseObject.SelectToken("continent_name")?.ToString() ?? string.Empty;
            string countryCode = responseObject.SelectToken("country_code")?.ToString() ?? string.Empty;
            string countryName = responseObject.SelectToken("country_name")?.ToString() ?? string.Empty;
            string regionName = responseObject.SelectToken("region_name")?.ToString() ?? string.Empty;
            string cityName = responseObject.SelectToken("city")?.ToString() ?? string.Empty;
            string zip = responseObject.SelectToken("zip")?.ToString() ?? string.Empty;
            string longitude = responseObject.SelectToken("longitude")?.ToString() ?? string.Empty;
            string latitude = responseObject.SelectToken("latitude")?.ToString() ?? string.Empty;

            JToken locationToken = responseObject.SelectToken("location");

            string capital = locationToken.SelectToken("capital")?.ToString() ?? string.Empty;


            List<TestPayload> testPayloads = new List<TestPayload>();

            TestPayload payload1 = new TestPayload() { Key = "Key 1", Value = "Value 1" };

            testPayloads.Add(payload1);
            testPayloads.Add(new TestPayload() { Key = "Key 2", Value = "Value 2" });
            testPayloads.Add(new TestPayload() { Key = "Key 3", Value = "Value 3" });

            payload1 = testPayloads.Find(p => p.Key == "Key 2");
            payload1 = new TestPayload() { Key = "Key 4", Value = "Value 4" };
            testPayloads.Add(payload1);




        }

        public static string GetPublicIP(string ipAddress, string key)  // f85013626fe28a8806edc6464aca8cce
        {
            try
            {
                string response = string.Empty;
                string url = $"http://api.ipstack.com/{ipAddress}?access_key={key}";
                System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                using (System.Net.WebResponse resp = req.GetResponse())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream()))
                    {
                        response = sr.ReadToEnd().Trim();
                    }
                }
                req = null;

                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
