
public CheckUsrData(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckUsrData", "mount | grep \"/usr/data\" | grep -c \"rw\"", new string[] { "1" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CPUInfo(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CPUInfo", "004_cpu_id_info -t \"ARMv7 Processor rev 2 (v7l)\"", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public NandInfo(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("NandInfo", "008_nand_info -g", new string[] { "AMD/Spansion k42NJ65" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public MemSizeCheck(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("MemSizeCheck", "007_mem_size -t 510416", new string[] { "// Memory will be changed when using Meraki’s firmware.//" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckDevID(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckDevID", "017_ac3_device_id -t \"98DX3233\"", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckBoardID(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckBoardID", "003_hw_board_id -t 010", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckOSVer(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckOSVer", "001_wnc_os_ver -t 0.0.5", new string[] { "// 0.0.5 is SW version. Please check SW PN //" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckEEPROM(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckEEPROM", "012_eeprom_info -t \" MK120-5FP 600-61030\"", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckHWVer(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckHWVer", "002_hw_ver -t 00", new string[] { "// 00 is hardware version //", "// 00 is hardware version //", "// 00 is hardware version //" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckSFPInfo1(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckSFPInfo1", "013_sfp_port_info -p 1 -t \"WNC 1.5W 10G SFP+ LOOPBACK MODULE\"", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckSFPInfo2(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckSFPInfo2", "013_sfp_port_info -p 2 -t \"WNC 1.5W 10G SFP+ LOOPBACK MODULE\"", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckSFPSpeed1(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckSFPSpeed1", "014_sfp_low_speed -t 1", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckSFPSpeed2(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckSFPSpeed2", "014_sfp_low_speed -t 2", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckSFPLoading1(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckSFPLoading1", "015_sfp_port_loading -l 1 -p 1", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckSFPLoading2(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckSFPLoading2", "015_sfp_port_loading -l 1 -p 2", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public POEInit(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("POEInit", "010_poe_load -i", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public POEEnable(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("POEEnable", "010_poe_load -e 1-8", new string[] { "Port 1 is enabled", "Port 2 is enabled", "Port 3 is enabled", "Port 4 is enabled", "Port 5 is enabled", "Port 6 is enabled", "Port 7 is enabled", "Port 8 is enabled", "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public TrafficTest(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("TrafficTest", "101_traffic_test -d 10", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public POETest(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("POETest", "010_poe_load -t 13.27,19.03 -p 1-8", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public FrontPortLED_Amber(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("FrontPortLED_Amber", "104_front_port_led -s amber", new string[] { "/ #" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public SFPLEDOn(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("SFPLEDOn", "105_sfp_port_led -s 1", new string[] { "/ #" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public SysLEDOn(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("SysLEDOn", "011_sys_led -s 1", new string[] { "/ #" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public SysLEDOff(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("SysLEDOff", "011_sys_led -s 0", new string[] { "/ #" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public SetMAC(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("SetMAC", "fixed_net_info -s SN0123456789 -m 00:11:22:33:44:55", new string[] { "Fixed net info is updated" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Sync(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Sync", "sync", new string[] { "/ #" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckProductID(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckProductID", "odm product_id read", new string[] { "MS227-5FP 460-61030" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckFWVer(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckFWVer", "odm fw_version", new string[] { "T-201805241537-Gb152632-NKUST" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public WriteMAC(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("WriteMAC", "odm mac write 13:AC:0A:4D:3B:F1", new string[] {  }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public VerifyMAC(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("VerifyMAC", "odm mac_verify", new string[] { "13:AC:0A:4D:3B:F1", "PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public WriteSN(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("WriteSN", "odm serial_num write QQDN-4KJ9-YG2W", new string[] {  }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public VerifySN(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("VerifySN", "odm serial_verify", new string[] { "QQDN-4KJ9-YG2W", "PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckServices(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckServices", "odm tam services status", new string[] { "Running" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public TAMProvision(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("TAMProvision", "odm tam provision 10.128.128.254", new string[] {  }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public TAMShow(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("TAMShow", "odm tam show", new string[] { "CN=High", "CN=(53)", "" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public TAMVersion(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("TAMVersion", "odm tam version", new string[] { "1.4.591", "0x100", "6.2.2" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckProjectVer(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckProjectVer", "WNC Check Ver", new string[] { "NKUST", "10/28", "4HR" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public SystemTest(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("SystemTest", "sys -t 100 -s 0 -c 8.8.8.8", new string[] { "1000MB" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}
