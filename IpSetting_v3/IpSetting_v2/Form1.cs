using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Management;
using System.Management.Instrumentation;
using TunaTank.MyNetworkFunctions;

namespace IpSetting_v2
{
    public partial class FrmMain : Form
    {
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

        MyNetworkClass myClass = new MyNetworkClass();

        public FrmMain()
        {
            InitializeComponent();
            
        }
        private void BtnGetIp_Click(object sender, EventArgs e)
        {
            int result = myClass.GetMyIp();
            Console.WriteLine("GetMyIp Result: {0}", result);
            LblPresentIp.Text = myClass.SelectedAdapterIpAddr;
        }

        private void BtnSetIpTSCAN2_Click(object sender, EventArgs e)
        {
            myClass.UserIpAddr = "192.168.133.10";
            myClass.UserSubMask = "255.255.255.0";
            myClass.UserGateway = "192.168.133.1";
            int result = myClass.ChangeIPAddress();
            //int result = myClass.SetIP();
            Console.WriteLine("ChangeIPAddress Result: {0}", result);

        }

        private void BtnSetDHCP_Click(object sender, EventArgs e)
        {
            myClass.SetDHCP();
        }

        private void BtnPingTest_Click(object sender, EventArgs e)
        {
            startInfo.FileName = @"cmd";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();

            process.StandardInput.Write(@"ping 192.168.133.101" + Environment.NewLine);
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();
            StringBuilder sb = new StringBuilder();
            //sb.Append("[Result Info]" + DateTime.Now + "\r\n");
            sb.Append(result);
            sb.Append("\r\n");

            TxtLog.Text = sb.ToString();
            process.WaitForExit();
            process.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var networks = NetworkInterface.GetAllNetworkInterfaces(); // 사용가능한 모든 네트워크를 배열로 모음.
            foreach (NetworkInterface net in networks)
            {
                string addr = "";
                IPInterfaceProperties netProperties = net.GetIPProperties();
                UnicastIPAddressInformationCollection uniCast = netProperties.UnicastAddresses;

                Console.WriteLine("net.Id: {0}", net.Id); // 네트워크의 고유id
                Console.WriteLine("net.Name: {0}", net.Name); // 표기되는 이름
                foreach (UnicastIPAddressInformation uni in uniCast)
                {
                    Console.WriteLine("Unicast Address : {0}", uni.Address);
                    addr = uni.Address.ToString();
                }
                Console.WriteLine("net.OperationalStatus: {0}", net.OperationalStatus); // 연결됐습니까?
                Console.WriteLine("net.NetworkInterfaceType: {0}", net.NetworkInterfaceType); // 구분용??
                Console.WriteLine("net.Description: {0}", net.Description); // 장치설명
                Console.WriteLine("net.SupportsMulticast: {0}", net.SupportsMulticast);
                Console.WriteLine("------------------");

                CboNetAdapts.Items.Add(net.Name + ", " + addr);
            }
        }



        private void FrmMain_Load(object sender, EventArgs e)
        {
            //string[] strAdaptorList = myClass.GetNetworkAdapterList();
            myClass.GetNetworkAdapterList();
            CboNetAdapts.Items.AddRange(myClass.AdapterList);
            CboNetAdapts.SelectedIndex = 0;
        }

        private void CboNetAdapts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CboNetAdapts.SelectedIndex >= 0)
            {
                this.itemSelected = CboNetAdapts.SelectedItem as string;
                myClass.SelectedAdapterIdx = CboNetAdapts.SelectedIndex;
                Console.WriteLine("Selected Item's index: {0}, Selected Item: {1}", myClass.SelectedAdapterIdx, this.itemSelected);
                LblPresentIp.Text = "";
                TxtLog.Text = "";
            }
        }

        private string itemSelected;

        private void button3_Click(object sender, EventArgs e)
        {
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            foreach (ManagementObject mo in osDetailsCollection)
            {
                foreach (PropertyData prop in mo.Properties)
                {
                    Console.WriteLine("{0}: {1}", prop.Name, prop.Value);
                }
                Console.WriteLine("==========================================================================================");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

namespace TunaTank.MyNetworkFunctions
{
    /*
    * Copyright 2019 D.W. Kim.
    */

    public class MyNetworkClass
    {
        // 필드
        private int selectedAdapterIdx;
        private string selectedAdapterName;
        private string selectedAdapterId;    // NetworkInterface의 ID
        private string selectedAdapterDesc;  // NetworkInterface의 Description
        private string selectedAdapterIpAddr;
        private string[] adapterList;
        private string userIpAddr;
        private string userSubMask;
        private string userGateway;
        private NetworkInterface[] networks = NetworkInterface.GetAllNetworkInterfaces(); // 사용가능한 모든 네트워크를 배열로 모음.

        // 이벤트
        public event EventHandler SelectedAdapterName_Changed;
        public event EventHandler UserIpAddr_Changed;
        public event EventHandler UserSubMask_Changed;
        public event EventHandler UserGateway_Changed;

        // 속성
        public int SelectedAdapterIdx
        {
            get { return this.selectedAdapterIdx; }
            set { this.selectedAdapterIdx = value; }
        }

        public string SelectedAdapterName
        {
            get { return this.selectedAdapterName; }
            set
            {
                if(this.selectedAdapterName != value)
                {
                    this.selectedAdapterName = value;
                    if (SelectedAdapterName_Changed != null)
                    {
                        SelectedAdapterName_Changed(this, EventArgs.Empty);
                    }
                }
            }
        }

        public string SelectedAdapterIpAddr
        {
            get { return this.selectedAdapterIpAddr; }
        }

        public string UserIpAddr
        {
            get { return this.userIpAddr; }
            set
            {
                if (this.userIpAddr != value)
                {
                    this.userIpAddr = value;
                    if (UserIpAddr_Changed != null)
                    {
                        UserIpAddr_Changed(this, EventArgs.Empty);
                    }
                }
            }
        }

        public string UserSubMask
        {
            get { return this.userSubMask; }
            set
            {
                if (this.userSubMask != value)
                {
                    this.userSubMask = value;
                    if (UserSubMask_Changed != null)
                    {
                        UserSubMask_Changed(this, EventArgs.Empty);
                    }
                }
            }
        }

        public string UserGateway
        {
            get { return this.userGateway; }
            set
            {
                if (this.userGateway != value)
                {
                    this.userGateway = value;
                    if (UserGateway_Changed != null)
                    {
                        UserGateway_Changed(this, EventArgs.Empty);
                    }
                }
            }
        }

        public string[] AdapterList
        {
            get { return this.adapterList; }
        }


        // 메서드
        /// <summary>
        /// PC의 네트워크 어댑터 리스트에서 Name과 IP를 모아서 string[] 배열로 return하는 함수
        /// </summary>
        /// <remarks>Requires a reference to the System.Net namespace</remarks>
        public string[] GetNetworkAdapterList()
        {
            int i = 0, cnt = 0;
            string addr = "";

            try
            {
                #region 이더넷이나 WiFi인 인터페이스만 골라서 어뎁터 리스트 string[]의 재배열

                for (i = 0; i < networks.Length; i++)
                {
                    NetworkInterface net = networks[i];
                    if (net.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || net.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        cnt++;
                    }
                }
                adapterList = new string[cnt];
                #endregion

                #region 네트워크의 Name과 IP Address만 추출하여 string[] 배열 생성 후 리턴
                for (i = 0; i < adapterList.Length; i++)
                {
                    NetworkInterface net = networks[i];
                    if (net.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || net.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        IPInterfaceProperties netProperties = net.GetIPProperties();
                        UnicastIPAddressInformationCollection uniCast = netProperties.UnicastAddresses;
                        
                        foreach (UnicastIPAddressInformation uni in uniCast)
                        {
                            addr = uni.Address.ToString();
                        }
                        adapterList[i] = net.Name + ", " + addr;
                        Console.WriteLine(adapterList[i].ToString());
                    }
                }
                return adapterList;
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}의 에러: {1}", "FnNetworkAdaptorListCheck", ex.ToString());
                foreach (NetworkInterface net in networks)
                {
                    adapterList[i] = "\0";
                    i++;
                }
                return adapterList;
            }
        }

        public int GetMyIp()
        {
            try
            {
                NetworkInterface adapter = networks[selectedAdapterIdx];
                IPInterfaceProperties netProperties = adapter.GetIPProperties();
                UnicastIPAddressInformationCollection uniCast = netProperties.UnicastAddresses;
                
                for (int i = 0; i < uniCast.Count; i++)
                {
                    // IPv6의 PrefixLength는 항상 64이므로 64가 아닌 경우로 조건문을 수행 
                    if (uniCast[i].PrefixLength != 64)
                    {
                        selectedAdapterIpAddr = uniCast[i].Address.ToString();
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}의 에러: {1}", "GetMyIp_2", ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// IPv4를 DHCP로 변경하는 함수
        /// </summary>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void SetDHCP()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                // Make sure this is a IP enabled device. Not something like memory card or VM Ware
                if ((bool)mo["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject newDNS = mo.GetMethodParameters("SetDNSServerSearchOrder");
                        newDNS["DNSServerSearchOrder"] = null;
                        ManagementBaseObject enableDHCP = mo.InvokeMethod("EnableDHCP", null, null);
                        ManagementBaseObject setDNS = mo.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0}의 에러: {1}", "SetDHCP", ex.ToString());
                        return;
                    }

                }
            }
        }

        #region IP주소설정하기 - SetIP()
        /// <summary>
        /// 로컬 머신의 IP 주소와 서브마스크를 새로 설정하는 함수
        /// </summary>
        /// <returns>처리결과</returns>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        //public int SetIP()
        //{
        //    ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //    ManagementObjectCollection objMOC = objMC.GetInstances();
        //    foreach (ManagementObject objMO in objMOC)
        //    {
        //        if ((bool)objMO["IPEnabled"])
        //        {
        //            try
        //            {

        //                ManagementBaseObject setIP;
        //                ManagementBaseObject newIP = objMO.GetMethodParameters("EnableStatic");
        //                newIP["IPAddress"] = new string[] { userIpAddr };
        //                newIP["SubnetMask"] = new string[] { userSubMask };
        //                setIP = objMO.InvokeMethod("EnableStatic", newIP, null);

        //                return 1;
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("{0}의 에러: {1}", "FnSetIP", ex.ToString());
        //                return 0;
        //            }

        //        }
        //    }
        //    return 1;
        //}
        #endregion

        #region IP 주소 변경하기 - ChangeIPAddress()
        /// <summary>
        /// IP 주소 변경하기
        /// </summary>
        /// <returns>처리 결과</returns>

        public int ChangeIPAddress()
        {
            NetworkInterface net = networks[selectedAdapterIdx];
            selectedAdapterId = net.Id;
            selectedAdapterDesc = net.Description;

            ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection managementObjectCollection = managementClass.GetInstances();

            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                string description = managementObject["Description"] as string;
                if (string.Compare(description, selectedAdapterDesc, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    try
                    {
                        ManagementBaseObject setGatewaysManagementBaseObject =
                            managementObject.GetMethodParameters("SetGateways");

                        setGatewaysManagementBaseObject["DefaultIPGateway"] = new string[] { userGateway };
                        setGatewaysManagementBaseObject["GatewayCostMetric"] = new int[] { 1 };

                        ManagementBaseObject enableStaticManagementBaseObject =
                            managementObject.GetMethodParameters("EnableStatic");

                        //enableStaticManagementBaseObject["IPAddress"] = new string[] { userIpAddr };
                        enableStaticManagementBaseObject["IPAddress"] = new string[] {"192.168.133.10"};
                        enableStaticManagementBaseObject["SubnetMask"] = new string[] { userSubMask };

                        var resNewIp = (uint)managementObject.InvokeMethod("EnableStatic", enableStaticManagementBaseObject, null)["returnValue"]; 
                        var resNewGate = (uint)managementObject.InvokeMethod("SetGateways", setGatewaysManagementBaseObject, null)["returnValue"]; 

                        Console.WriteLine("ChangeIPAddress {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                            selectedAdapterId,
                            selectedAdapterDesc,
                            description,
                            userGateway,
                            userIpAddr,
                            userSubMask,
                            resNewIp,
                            resNewGate
                            );
                        return 1;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }

            return 1;
        }
        #endregion

        public int SetIP_1()
        {
            try
            {
                IPAddress userIp = IPAddress.Parse(userIpAddr);
                
                NetworkInterface adapter = networks[selectedAdapterIdx]; // 선택한 네트워크를 어댑터로 할당 
                IPInterfaceProperties netProperties = adapter.GetIPProperties();
                UnicastIPAddressInformationCollection uniCast = netProperties.UnicastAddresses;

                for (int i = 0; i < uniCast.Count; i++)
                {
                    if (uniCast[i].PrefixLength != 64)
                    {
                        //uniCast[i].Address = userIpAddr;
                    }

                }


                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}의 에러: {1}", "SetIP", ex.ToString());
                return 0;
            }
        }
    }

}
