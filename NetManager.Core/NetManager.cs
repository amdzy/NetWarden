using SharpPcap;

namespace NetManager.Core;

public class NetManager
{

    public static CaptureDeviceList ListDevices()
    {
        return CaptureDeviceList.Instance;
    }
}
