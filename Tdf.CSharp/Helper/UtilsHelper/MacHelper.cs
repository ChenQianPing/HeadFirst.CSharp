using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.UtilsHelper
{
    public static class MacHelper
    {
        #region 根据截取ipconfig /all命令的输出流获取网卡Mac
        /// <summary>
        /// 根据截取ipconfig /all命令的输出流获取网卡Mac
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMacByIpConfig()
        {
            var macs = new List<string>();

            var startInfo = new ProcessStartInfo("ipconfig", "/all")
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            var p = Process.Start(startInfo);
            // 截取输出流
            var reader = p.StandardOutput;
            var line = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    line = line.Trim();

                    if (line.StartsWith("Physical Address"))
                    {
                        macs.Add(line);
                    }
                }


                line = reader.ReadLine();
            }

            // 等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
            reader.Close();

            return macs;
        }
        #endregion

        #region 通过WMI读取MAC地址，引用System.Management，推荐使用
        /// <summary>
        /// 通过WMI读取MAC地址
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMacByWmi()
        {
            var macs = new List<string>();
            try
            {
                var mac = "";
                var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                var moc = mc.GetInstances();
                foreach (var mo in moc)
                {
                    if ((bool)mo["IPEnabled"])
                    {
                        mac = mo["MacAddress"].ToString();
                        macs.Add(mac);
                    }
                }
                moc = null;
                mc = null;
            }
            catch
            {
            }

            return macs;
        }
        #endregion

        #region 通过NetworkInterface读取网卡Mac
        /// <summary>
        /// 返回描述本地计算机上的网络接口的对象（网络接口也称为网络适配器）.
        /// </summary>
        /// <returns></returns>
        public static NetworkInterface[] NetCardInfo()
        {
            return NetworkInterface.GetAllNetworkInterfaces();
        }

        
        public static List<string> GetMacByNetworkInterface()
        {
            var macs = new List<string>();
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var ni in interfaces)
            {
                macs.Add(ni.GetPhysicalAddress().ToString());
            }
            return macs;
        }
        #endregion

        #region 通过SendARP获取网卡Mac，网络被禁用或未接入网络（如没插网线）时此方法失灵

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        public static string GetMacBySendArp(string remoteIp)
        {
            var macAddress = new StringBuilder();

            try
            {
                var remote = inet_addr(remoteIp);

                var macInfo = new Int64();
                var length = 6;
                SendARP(remote, 0, ref macInfo, ref length);

                var temp = Convert.ToString(macInfo, 16).PadLeft(12, '0').ToUpper();

                var x = 12;
                for (var i = 0; i < 6; i++)
                {
                    if (i == 5)
                    {
                        macAddress.Append(temp.Substring(x - 2, 2));
                    }
                    else
                    {
                        macAddress.Append(temp.Substring(x - 2, 2) + "-");
                    }
                    x -= 2;
                }

                return macAddress.ToString();
            }
            catch
            {
                return macAddress.ToString();
            }
        }
        #endregion

        #region 通过注册表读取MAC地址
        // HKEY_LOCAL_MACHINE\Software\Microsoft\Windows Genuine Advantage
        #endregion

        #region TestMethod
        public static void TestMacHelper()
        {
            var lstMac1 = MacHelper.GetMacByIpConfig();

            foreach (var mac1 in lstMac1)
            {
                Console.WriteLine("mac1：" + mac1);
            }

            var lstMac2 = MacHelper.GetMacByWmi();

            foreach (var mac2 in lstMac2)
            {
                Console.WriteLine("mac2：" + mac2);
            }

            var lstMac3 = MacHelper.GetMacByNetworkInterface();

            foreach (var mac3 in lstMac3)
            {
                Console.WriteLine("mac3：" + mac3);
            }
        }
        #endregion

    }
}
