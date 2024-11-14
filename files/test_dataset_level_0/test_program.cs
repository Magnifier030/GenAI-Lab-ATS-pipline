
public CheckPsuStatus(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckPsuStatus", "diag psu info test mfr_id=\"FSP GROUP\" mfr_model=\"FSP800-20FM\"", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public EnableSecureBoot(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("EnableSecureBoot", "tftp -r fuse_blower_wnc_pvt.sh -g 192.168.10.1", new string[] { "/tmp#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Read STSAFE ID(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Read STSAFE ID", "cat /tmp/DeviceId", new string[] { "Router-010000000000000000CF3519" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public packet_path_counter_clear(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("packet_path_counter_clear", "clear counters", new string[] { "localhost(config)#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public EncodePrefdl(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("EncodePrefdl", "prefdl -e prefdl_data > prefdl_data_encode", new string[] { "#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckAboorVersion(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckAboorVersion", "diag sys version aboot=\"SB_IDVH .001.001.000.001.000.00017.R-0000000000000000\"", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckSPI_Unwritable(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckSPI_Unwritable", "echo $?", new string[] { "2" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public ClearWNC(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("ClearWNC", "y", new string[] { "Press  for the Boot Menu" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Traffic_Test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Traffic_Test", "erase startup-config", new string[] { "Deleting...", "Successful operation", "awplus#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckLTEIMEI(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
                if (testResult)
            break;
    }

    return testResult;
}

public SetSysled2SolidGreen(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("SetSysled2SolidGreen", "diag mgmt led sysled_2 set=2", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public VerifyFan50(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("VerifyFan50", "diag fan speed test=4886,9072", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public run_cmdd_test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("run_cmdd_test", "post_assy > /dev/null 2>&1 &", new string[] { "~#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public WriteSnToPrefdl(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("WriteSnToPrefdl", "echo \"SerialNumber: WTW21481002\" >> prefdl_data", new string[] { "#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Update_CPU_RX_EQ(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Update_CPU_RX_EQ", "devmem 0xF4122C40 16 0xF2D3", new string[] { "~#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public SyncData(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("SyncData", "sync", new string[] { "bash-4.2#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckEMMCInfo(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckEMMCInfo", "diag storage emmc info test=\"008GB1\"", new string[] { "Result: PASS", "~#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Remove_autoBI_config(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Remove_autoBI_config", "/mnt/flash/mmc_static cache disable /dev/mmcblk0 2>/dev/null", new string[] { "Aboot#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public TestFirstUSBEthernet(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("TestFirstUSBEthernet", "diag mgmt usb ether test num_usb3_0=2 criteria_3_0=900 duration=10 side_check=1", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public packet_path_vlan_create(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("packet_path_vlan_create", "switchport access vlan 300", new string[] { "localhost(config-if-Et24)#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public OneWireTest(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("OneWireTest", "onewire id", new string[] { "0xc3a9" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Remove_autoBI_config(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Remove_autoBI_config", "boot flash:$EOS_img", new string[] { "Aboot#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Ethernet test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Ethernet test", "brctl addif br-lan eth0", new string[] { "root@OpenWrt:/#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public FUNC1_Black_LED(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("FUNC1_Black_LED", "diag mgmt led sysled_2 set=0", new string[] { "Result: PASS", "~#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Interrupt_MAC(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Interrupt_MAC", "Verify uboot. Version: 8.10.0", new string[] { "Result: PASS", "~#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public usb_test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("usb_test", "ip netns delete net1", new string[] { "bash-4.2#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public SetMgmtRightOff(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("SetMgmtRightOff", "diag mgmt oob led=1,0", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public PrintPsuBlackBox(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = testResult && CommandCheck("PrintPsuBlackBox", "PSU_black_box_tool.sh dump all", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("PrintPsuBlackBox", "PSU_black_box_tool.sh get all all", new string[] { Result: PASS }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public SetBootConfig(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("SetBootConfig", "echo \"SWI=flash:EOS.swi\" > /mnt/flash/boot-config", new string[] { "#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public SetPrintkLevel(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("SetPrintkLevel", "echo 1 4 1 3 > /proc/sys/kernel/printk", new string[] { "#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public USB_Amber_LED(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("USB_Amber_LED", "diag mgmt led sysled_5 set=1", new string[] { "Result: PASS", "~#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Traffic_Set(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Traffic_Set", "show boot", new string[] { "flash:/default.cfg (file exists)", "awplus#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public usb_test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("usb_test", "ip link set macv1 netns net1", new string[] { "bash-4.2#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public sysPwrOn_check_flashrom(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("sysPwrOn_check_flashrom", "bash sudo flashrom", new string[] { "localhost(config)#,Macronix flash chip \"MX25U12835F\"" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Write CP FW to EMMC(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Write CP FW to EMMC", "reset", new string[] { "IPQ6018#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public ShowNimSsdInformation(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = testResult && CommandCheck("ShowNimSsdInformation", "bash sudo lspci -vvv -s 15:02.0 | grep LnkCap", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("ShowNimSsdInformation", "bash sudo lspci -vvv -s 15:04.0 | grep LnkCap", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("ShowNimSsdInformation", "bash sudo lspci -vvv -s 90:02.0 | grep LnkCap", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("ShowNimSsdInformation", "bash sudo lspci -vvv -s 90:04.0 | grep LnkCap", new string[] { Width x8 }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Login_Cus(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Login_Cus", "configure terminal", new string[] { "awplus(config)#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Fan_Speed_Set_100(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Fan_Speed_Set_100", "diag fan pwm duty_cycle=100", new string[] { "Result: PASS", "~#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public WiFiSSID(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("WiFiSSID", "wifi scan", new string[] { "ESP_" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public sysPwrOn_config(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("sysPwrOn_config", "config", new string[] { "localhost(config)#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public UpLoadBILog(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("UpLoadBILog", "rm -rf burnInlog_Z2W1O2800006N01.tar", new string[] { "root@localhost:~#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public sysPwrOn_rm_boot_flash(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("sysPwrOn_rm_boot_flash", "boot flash:$diagOS_img", new string[] { "localhost login:" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckMemorySize(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckMemorySize", "diag sys memory size compare=2032608", new string[] { "Result: PASS", "~#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Set_Printk_Level(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Set_Printk_Level", "echo 1 4 1 3 > /proc/sys/kernel/printk", new string[] { "~#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public System_White_LED(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("System_White_LED", "diag mgmt led sysled set=4", new string[] { "Result: PASS", "~#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public ClearWNCData(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("ClearWNCData", "mount /dev/mmcblk0p1 /diag", new string[] { "#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public UpdateInactiveTPM(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("UpdateInactiveTPM", "tpm2util stm fu /mnt/flash/ST33TPHF2XSPI_00010200.fi", new string[] { "Upgrade counter: 0", "Starting Firmware Upgrade", "Post-upgrade counter: 1", "Upgrade successful" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Switch_Cus_FW(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Switch_Cus_FW", "Y", new string[] { "Restoring default settings... Complete" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public LAN to WAN Throughput Test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("LAN to WAN Throughput Test", "Client command: uperf -c DHCP_Server_ip -w 1024k -p 12 -t 10 -I 1", new string[] { "Throughput Limitation > 750 Mbps" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public ShowManagementSecurity(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("ShowManagementSecurity", "show management security", new string[] { "STM ST33TPHF2X TPM 2.0 (1.512)" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}
