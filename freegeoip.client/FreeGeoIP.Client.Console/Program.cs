﻿namespace FreeGeoIP.Client.Console
{
    using System;

    /// <summary>
    /// An application that looks up geolocation data by host name or IP address. This class cannot be inherited.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry-point to the application.
        /// </summary>
        internal static void Main()
        {
            var client = new FreeGeoIPClient();

            string hostNameOrIPAddress = null;

            while (true)
            {
                Console.Write("Host name or IP address: ");
                hostNameOrIPAddress = Console.ReadLine();

                if (string.IsNullOrEmpty(hostNameOrIPAddress))
                {
                    break;
                }

                IPLookup result;

                try
                {
                    result = client.LookupAsync(hostNameOrIPAddress).Result;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine();
                    Console.Error.WriteLine("Failed to look up IP address: {0}", ex.ToString());
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("Result for {0}:", hostNameOrIPAddress);
                Console.WriteLine();

                if (result == null)
                {
                    Console.WriteLine("  Not found.");
                }
                else
                {
                    Console.WriteLine("   IP address: {0}", result.IPAddress);
                    Console.WriteLine("     Lat/Long: {0:N2},{1:N2}", result.Latitude, result.Longitude);
                    Console.WriteLine("         City: {0}", result.City);
                    Console.WriteLine("  Postal Code: {0}", result.PostalCode);
                    Console.WriteLine("       Region: {0}", result.RegionCode);
                    Console.WriteLine("      Country: {0}", result.CountryCode);
                    Console.WriteLine("    Time Zone: {0}", result.TimeZone);
                }

                Console.WriteLine();
            }
        }
    }
}
