using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// TODO:
///     * Accept arguments (aka --version, --once, --loop, --help, --about)
///     * Press q to exit on --loop
///     * Print hostname
///     * ASCII bar insted of values
/// </summary>


namespace ActMonitor {
    class Program {

        #region Global vars
        static PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        static PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        static PerformanceCounter upTime = new PerformanceCounter("System", "System Up Time");
        #endregion

        #region Main (entry point)
        static void Main(string[] args) {

            // Get values one time to avoid zeroes on load
            cpuCounter.NextValue();
            ramCounter.NextValue();
            upTime.NextValue();

            while(true) {
                Console.Clear();
                Console.WriteLine("Uptime            : {0}", GetUpTime());
                Console.WriteLine("Current CPU usage : {0}", GetCurrentCpuUsage());
                Console.WriteLine("Available RAM     : {0}", GetAvailableRAM());
                System.Threading.Thread.Sleep(1000);
            }
        }
        #endregion

        #region Methods
        static string GetCurrentCpuUsage() {
            return (int)cpuCounter.NextValue() + " %";
        }

        static string GetAvailableRAM() {
            return ramCounter.NextValue() + " MB";
        }

        static string GetUpTime() {
            string fmt = "0#"; // number format
            TimeSpan ts = TimeSpan.FromSeconds(upTime.NextValue());
            string sUptime = ts.Days + "d " + ts.Hours.ToString(fmt) 
                + ":" + ts.Minutes.ToString(fmt) 
                + ":" + ts.Seconds.ToString(fmt);
            return sUptime;
        }
        #endregion
    }
}
