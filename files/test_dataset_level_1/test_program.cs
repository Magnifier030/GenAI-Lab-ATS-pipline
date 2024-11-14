
public check_secureboot_status(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("check_secureboot_status", "tpm2util sb -status", new string[] { "disabled: 1", "unlockspiflash: 1" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Upgrade REL FW(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Upgrade REL FW", "eth_init", new string[] { "IPQ9574#" }, 30);

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
        testResult = CommandCheck("EnableSecureBoot", "./fuse_blower_wnc_pvt.sh", new string[] { "DONE", "DONE", "DONE", "/tmp#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Check ART Partition & cal data(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Check ART Partition & cal data", "hexdump /dev/caldata", new string[] { "0000000 0000 0000 0000 0000 0000 0000 0000 0000", "*", "0200000" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Unlock Bootloader(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Unlock Bootloader", "atom ACT2 SignData --InFile \" + $\"{MAC}_zealand_inputchallengedata.txt --InFormat txt --OutFormat xml > \" + $\"{MAC}_zealand_signdata_output.xml", new string[] { "256", "aea18d135ecd1ecf0d606b44fa65e92cd1bc121118b39e72165a8ab1cb2cdd89731c50a25acb622e42ad4c7979b1245dfe30c9378237e0704290d0d5f2e829d4356588f0b3f311372a2edd077af9d2919419e35ae794914a3aa388f15d4bcb9a094c238cd7a58e5737d7b108ceba3bef6d5b9429d8c2cdc038d8fcf74d50b63ce1dd96043829b16e14f476478af5433d5d7f8dbff7430689e569c11a8feb871baee93fb579535aec3af15afaa4ade3ad41aa632eb1a8d442730c62272e847bd05de8638aec87c4031490f11cbdf793bd980a95d657461336fdfdbd46f6db752d2492c70be9fad3d6f722e7a386569a5ef36376bad750dfc4c87c765021fdec3b" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Load card(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = testResult && CommandCheck("Load card", "rmmod ecm_wifi_plugin", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Load card", "rmmod monitor", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Load card", "rmmod wifi_3_0", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Load card", "rmmod qca_ol", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Load card", "insmod qca_ol testmode=1", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Load card", "insmod wifi_3_0", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Load card", "diag_socket_app -a 192.168.1.254 &", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Load card", "/etc/init.d/ftm start", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Load card", "usr/sbin/ftm -n -c /tmp/ftm.conf &", new string[] { logging switched }, 30);

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
        testResult = CommandCheck("Traffic_Test", "test interface all seconds 10 fixed", new string[] { "Test on port1.0.1 complete", "Test on port1.0.2 complete", "Test on port1.0.3 complete", "Test on port1.0.4 complete" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Upgrade REL FW(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Upgrade REL FW", "setenv ipaddr {dutIP} ", new string[] { "IPQ9574#" }, 30);

testResult = testResult && CommandCheck("Upgrade REL FW", "setenv serverip {pcIP}", new string[] { "IPQ9574#" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public check_rtc(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("check_rtc", "hwclock --show", new string[] { "Thu Jul  7 22:08:54 2022  0.000000 seconds" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public CheckSPI_ExtraCert(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("CheckSPI_ExtraCert", "flashrom -l /etc/spi_layout -i extra_cert -r /tmp/extra_cert.save.2", new string[] { "Unsetting lock bit(s) failed.", "Reading flash... done." }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public check_eMMC_info(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("check_eMMC_info", "diag storage emmc info test", new string[] { "Result: PASS" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Remove i/o expander driver reload script(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Remove i/o expander driver reload script", "sed -i 's/\/etc\/i2c_kmodule\/insert_i2c_kmodule.sh/#\/etc\/i2c_kmodule\/insert_i2c_kmodule.sh/g' /usr/sbin/wncgps", new string[] { "#/etc/i2c_kmodule/insert_i2c_kmodule.sh > /dev/null 2>&1" }, 30);

testResult = testResult && CommandCheck("Remove i/o expander driver reload script", "sync; sync;", new string[] { "#/etc/i2c_kmodule/insert_i2c_kmodule.sh > /dev/null 2>&1" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public VerifySecurityConfig(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("VerifySecurityConfig", "securebootctl mfg –display", new string[] { "Secure Boot: enabled", "Password default storage: flash", "16:00:00:00:03:7C:02:7D:2C:8A:AB:97:4C:00:00:00:00:00:03", "28:00:00:00:11:3B:51:91:32:5F:E7:5A:5E:00:00:00:00:00:11", "Arista certificate 3: No certificate is configured", "28:00:00:00:10:3F:BA:D4:42:9C:84:ED:09:00:00:00:00:00:10" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Unlock Bootloader(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Unlock Bootloader", "3074", new string[] { "Enter Cert Chain data:" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public RemoveDUTNetwork(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("RemoveDUTNetwork", "MR_CCT.exe -r “SN”", new string[] { "[SUCCESS] DUT is not in the CCT Network" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public I2C Bus Check(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("I2C Bus Check", "i2cdetect -r -y 6", new string[] { "20: -- -- UU -- -- -- -- -- -- -- -- -- -- -- UU --" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Check board information(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Check board information", "rm -f /tmp/devinfo.tmp", new string[] { "VAT-02-000" }, 30);

testResult = testResult && CommandCheck("Check board information", "devinfo_access show", new string[] { "1222146000066" }, 30);

testResult = testResult && CommandCheck("Check board information", "ifconfig | grep eth0 | cut -d ' ' -f 11", new string[] { "3.0" }, 30);

testResult = testResult && CommandCheck("Check board information", "hexdump -C /dev/mtd4 -s 0x4 -n 6 | cut -c 11-27", new string[] { "3.21.12.DVT-RC.4" }, 30);

testResult = testResult && CommandCheck("Check board information", "hexdump -C /dev/mtd4 -s 0xa -n 6 | cut -c 11-27", new string[] { "84:EB:3E:24:19:4E" }, 30);

testResult = testResult && CommandCheck("Check board information", "hexdump -C /dev/mtd4 -s 0x5004 -n 6 | cut -c 11-27", new string[] { "84 eb 3e 24 19 4f" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Upgrade to production FW(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = testResult && CommandCheck("Upgrade to production FW", "cd /tmp", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Upgrade to production FW", "tftp -g -r openwrt-mediatek-mt7622-mediatek_mt7622-mesh-node-dvtrc1-squashfs-sysupgrade.bin 192.168.1.100", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Upgrade to production FW", "md5sum openwrt-mediatek-mt7622-mediatek_mt7622-mesh-node-dvtr", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Upgrade to production FW", "sysupgrade -F -n openwrt-mediatek-mt7622-mediatek_mt7622-mesh-node-dvtrc1-squashfs-sysupgrade.bin", new string[] { 53577a59d53742a4321d852aad2c0c83 }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Check board information after upgrading to production FW(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Check board information after upgrading to production FW", "devinfo_access show", new string[] { "VAT-02-000", "1222146000246", "3.0", "3.21.12.DVT-RC.4" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Check Ethernet and wifi MAC address(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Check Ethernet and wifi MAC address", "ifconfig eth0 | grep \"HWaddr\" | cut -c 39-55", new string[] { "84:EB:3E:24:19:4E" }, 30);

testResult = testResult && CommandCheck("Check Ethernet and wifi MAC address", "iw wlan1 info | grep addr | cut -c 7-23", new string[] { "84:eb:3e:24:19:4f" }, 30);

testResult = testResult && CommandCheck("Check Ethernet and wifi MAC address", "iw wlan0 info | grep addr | cut -c 7-23", new string[] { "84:eb:3e:24:19:50" }, 30);

testResult = testResult && CommandCheck("Check Ethernet and wifi MAC address", "iw wlan2 info | grep addr | cut -c 7-23", new string[] { "84:eb:3e:24:19:51" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Check BT MAC address(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = testResult && CommandCheck("Check BT MAC address", "uart_launcher -c 921600 -p /dev/ttyS2 -s &", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Check BT MAC address", "btservice&", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Check BT MAC address", "btmw-rpc-test", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Check BT MAC address", "Ctrl+z", new string[] { 84:EB:3E:24:19:52 }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public DDR(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("DDR", "memtester 1 1", new string[] { "ok", "ok", "ok", "ok", "ok", "ok", "ok" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public THERM_BAT Thermal(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("THERM_BAT Thermal", "cat /sys/devices/platform/soc@0/soc@0:bus@30800000/30a30000.i2c/i2c-1/1-0034/voltages/v_therm_batt", new string[] { "500 ~ 650" }, 30);

testResult = testResult && CommandCheck("THERM_BAT Thermal", "cat /sys/devices/platform/soc@0/soc@0:bus@30800000/30a30000.i2c/i2c-1/1-0034/voltages/v_therm1", new string[] { "1100 ~ 1600" }, 30);

testResult = testResult && CommandCheck("THERM_BAT Thermal", "cat /sys/devices/platform/soc@0/soc@0:bus@30800000/30a30000.i2c/i2c-1/1-0034/voltages/v_therm2", new string[] { "1100 ~ 1600" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public DECT interface check(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("DECT interface check", "cmbs_tcx -comname ttymxc0 -baud 921600", new string[] { "Version 4.13.2 - Build 8 - RC 18" }, 30);

testResult = testResult && CommandCheck("DECT interface check", "q", new string[] { "Version 4.13.2 - Build 8 - RC 18" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Speaker and Mic loopback test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Speaker and Mic loopback test", "panel-audio-test -m -v -E1", new string[] { "fundamental (bin 177 of 1024) frequency  1383:  -21.644 dB, THD(f)     0.30%", "fundamental (bin 177 of 1024) frequency  1383:  -11.684 dB, THD(f)     0.60%", "-9~ -4", "0 ~ 50", "-11~ -4", "0~ 50" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public PIEZO Output Voltage(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("PIEZO Output Voltage", "echo 14 > /sys/class/gpio/export; echo out > /sys/class/gpio/gpio14/direction; echo 1 > /sys/class/gpio/gpio14/value", new string[] { "mean_volume: -7.8 dB" }, 30);

testResult = testResult && CommandCheck("PIEZO Output Voltage", "killall dx-host; /usr/bin/dx-host socket > /dev/null &", new string[] { "max_volume: 0.0 dB" }, 30);

testResult = testResult && CommandCheck("PIEZO Output Voltage", "sleep 10", new string[] { "-10 ~ -3" }, 30);

testResult = testResult && CommandCheck("PIEZO Output Voltage", "dx-socket-cli /tmp/dx-host-sock -a \"uc0\"", new string[] { "-3 ~ 3" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public LTE status test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("LTE status test", "miniterm.py /dev/ttyUSB3 115200", new string[] { "EG91NAXGAR07A03M1G_01.002.01.002" }, 30);

testResult = testResult && CommandCheck("LTE status test", "AT+QGMR", new string[] { "15-digit" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public SIM 1 test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("SIM 1 test", "while [ ! -e /dev/ttyUSB3 ]; do echo \"waiting LTE ...\"; sleep 1; done; sleep 1", new string[] { "+QDSIM: 0" }, 30);

testResult = testResult && CommandCheck("SIM 1 test", "miniterm.py /dev/ttyUSB3 115200", new string[] { "20-digit number" }, 30);

testResult = testResult && CommandCheck("SIM 1 test", "       at+qdsim?", new string[] { "15-digit number" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public SIM 2 test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("SIM 2 test", "sleep 35", new string[] { "+QDSIM: 1" }, 30);

testResult = testResult && CommandCheck("SIM 2 test", "miniterm.py /dev/ttyUSB4 115200", new string[] { "20-digit number" }, 30);

testResult = testResult && CommandCheck("SIM 2 test", "at+qdsim?", new string[] { "15-digit number" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Write SKU number(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Write SKU number", "skunumset; umount /media/bootscript; mount /media/bootscript", new string[] { "skunumset: boardcheck succeeded" }, 30);

testResult = testResult && CommandCheck("Write SKU number", "VS-SHP000-001", new string[] { "skunumset: Done setting SKU" }, 30);

testResult = testResult && CommandCheck("Write SKU number", "cat /media/bootscript/sku.txt", new string[] { "VS-SHP000-001" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public G sensor test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("G sensor test", "grep -r \"\" /sys/bus/iio/devices/iio:device0/in*raw", new string[] { "in_accel_x_raw:108", "in_accel_y_raw:-32", "in_accel_z_raw: 1040" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public I2C device check(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("I2C device check", "i2cdetect -r -y 0 | grep -o UU | wc -l", new string[] { "1" }, 30);

testResult = testResult && CommandCheck("I2C device check", "i2cdetect -r -y 1 | grep -o UU | wc -l", new string[] { "2" }, 30);

testResult = testResult && CommandCheck("I2C device check", "i2cdetect -r -y 2 | grep -o UU | wc –l", new string[] { "2" }, 30);

testResult = testResult && CommandCheck("I2C device check", "i2cdetect -r -y 3 | grep -o UU | wc -l", new string[] { "2" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Speaker and Microphone Test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Speaker and Microphone Test", "cmbs_tcx -comname ttymxc0 -baud 921600", new string[] { "fundamental (bin 177 of 1024) frequency  1383:  -21.644 dB, THD(f)     0.30%" }, 30);

testResult = testResult && CommandCheck("Speaker and Microphone Test", "s", new string[] { "fundamental (bin 177 of 1024) frequency  1383:  -11.684 dB, THD(f)     0.60%" }, 30);

testResult = testResult && CommandCheck("Speaker and Microphone Test", "2", new string[] { "-12 ~ -4" }, 30);

testResult = testResult && CommandCheck("Speaker and Microphone Test", "S", new string[] { "0 ~ 20" }, 30);

testResult = testResult && CommandCheck("Speaker and Microphone Test", "0", new string[] { "-10 ~ -2" }, 30);

testResult = testResult && CommandCheck("Speaker and Microphone Test", "Enter", new string[] { "15 ~ 35" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public PIEZO Alarm Test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("PIEZO Alarm Test", "cd /tmp; tar xvf /usr/sbin/ffmpeg.tar > /dev/null; cp ffmpeg/ffmpeg /usr/sbin/; cp ffmpeg/lib/* /usr/lib; sync; cd", new string[] { "mean_volume: -4.0 dB" }, 30);

testResult = testResult && CommandCheck("PIEZO Alarm Test", "killall dx-host; /usr/bin/dx-host socket > /dev/null &", new string[] { "max_volume: 0.0 dB" }, 30);

testResult = testResult && CommandCheck("PIEZO Alarm Test", "sleep 10", new string[] { "-6 ~ 0" }, 30);

testResult = testResult && CommandCheck("PIEZO Alarm Test", "dx-socket-cli /tmp/dx-host-sock -a \"uc0\"", new string[] { "-6 ~ 0" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Verify BT MAC(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = testResult && CommandCheck("Verify BT MAC", "modprobe btmtk_usb", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Verify BT MAC", "boots -c raddr", new string[] { 84-EB-3E-23-99-AC }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Backup WiFi Calibration Data(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = testResult && CommandCheck("Backup WiFi Calibration Data", "cp /lib/firmware/EEPROM_MT7663.bin /media/bootscript;sync", new string[] { [請人員填寫 Criteria] }, 30);

testResult = testResult && CommandCheck("Backup WiFi Calibration Data", "md5sum /media/bootscript/EEPROM_MT7663.bin && echo PASS || echo NG", new string[] { PASS }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Verify EMMC partitions(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Verify EMMC partitions", "mount | grep mmcblk2p4 | wc -l", new string[] { "1" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public G sensor test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("G sensor test", "grep -r \"\" /sys/bus/iio/devices/iio:device0/in*raw", new string[] { "in_accel_x_raw:108", "in_accel_y_raw:-32", "in_accel_z_raw:-1040", "-100 ~ 100", "-1200 ~ -900", "-100 ~ 100" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Check FW version(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Check FW version", "cat /etc/version", new string[] { "20210118091636" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Check HW version(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Check HW version", "cat /sys/fsl_otp/HW_OCOTP_GP10", new string[] { "0x3" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public ADC test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("ADC test", "Cat /sys/bus/iio/devices/iio\:device3/in_voltage0_raw", new string[] { "3900 +/- 50" }, 30);

testResult = testResult && CommandCheck("ADC test", "Cat /sys/bus/iio/devices/iio\:device3/in_voltage1_raw", new string[] { "3900 +/- 50" }, 30);

testResult = testResult && CommandCheck("ADC test", "Cat /sys/bus/iio/devices/iio\:device3/in_voltage2_raw", new string[] { "2700 +/- 200" }, 30);

testResult = testResult && CommandCheck("ADC test", "Cat /sys/bus/iio/devices/iio\:device3/in_voltage3_raw", new string[] { "1650 +/- 200" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Speaker/Mic loopback test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Speaker/Mic loopback test", "python3 /usr/bin/cmnd_python3x.py", new string[] { "D2 fw_version=461.7" }, 30);

testResult = testResult && CommandCheck("Speaker/Mic loopback test", "ttymxc2", new string[] { "D8 fw_version=465.1" }, 30);

testResult = testResult && CommandCheck("Speaker/Mic loopback test", "b", new string[] { "panel-audio-test analysis is acceptable" }, 30);

testResult = testResult && CommandCheck("Speaker/Mic loopback test", "b", new string[] { "< 20" }, 30);

testResult = testResult && CommandCheck("Speaker/Mic loopback test", "a", new string[] { "< 20" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public BT interface check(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("BT interface check", "modprobe btmtk_usb", new string[] { "AT+QUIMSLOT=2" }, 30);

testResult = testResult && CommandCheck("BT interface check", "dmesg | grep \"usb registration success\"", new string[] { "46-digit" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public eSIM Test(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("eSIM Test", "test_comport -agc 'AT+QUIMSLOT=2'", new string[] { "AT+QUIMSLOT=2" }, 30);

testResult = testResult && CommandCheck("eSIM Test", "test_comport -agc 'AT+QUIMSLOT?'", new string[] { "46-digit" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Verify Ethernet MAC(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Verify Ethernet MAC", "cat /factory/eth_mac", new string[] { "The MAC is the same as what we set in BFT station." }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Write and check EEPROM Data(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Write and check EEPROM Data", "gpio 15 1", new string[] { "WB2043DB000092" }, 30);

testResult = testResult && CommandCheck("Write and check EEPROM Data", "rom_sn set WB2223DA001102", new string[] { "82" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Write and check EEPROM Data(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Write and check EEPROM Data", "10", new string[] { "=== PWM LED Test End ===" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Write and check EEPROM Data(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Write and check EEPROM Data", "Press “ESC” to stop to boot mode when message - “Hit any key to stop autoboot:” appears and “CM66#” prompt will be displayed. Check information by each of them.", new string[] { "R6AQ-C4 V1.0.0.10", "PCI Link Intialized", "PCI Link Intialized" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Write and check EEPROM Data(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Write and check EEPROM Data", "cat /proc/sys/kernel/version", new string[] { "#1 SMP Fri Jul 8 14:52:10 PDT 2022" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}

public Write and check EEPROM Data(int retry)
{
    bool testResult = true;

    for (int i = 0; i < retry; i++)
    {
        testResult = CommandCheck("Write and check EEPROM Data", "tecro 0 1 5 1", new string[] { "0x41 LTC9105 POWER_MONITOR Test", "53.560V", "17.353W" }, 30);

        if (testResult)
            break;
    }

    return testResult;
}
