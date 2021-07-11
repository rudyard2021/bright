using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
namespace Brightness
{
    public class WindowsApi
    {
        [DllImport("Dxva2")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetMonitorBrightness(IntPtr hMonitor,
            out uint pdwMinimumBrightness,
            out uint pdwCurrentBrightness,
            out uint pdwMaximumBrightness);

        [DllImport("Dxva2")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetMonitorBrightness(IntPtr hMonitor, uint newBrightness);

        [DllImport("Dxva2")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, out int numberOfPhysicalMonitors);

        [DllImport("Dxva2", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, int physicalMonitorArraySize, ref PHYSICAL_MONITOR physicalMonitorArray);

        [DllImport("Dxva2", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyPhysicalMonitors(int physicalMonitorArraySize, ref PHYSICAL_MONITOR physicalMonitorArray);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct PHYSICAL_MONITOR
        {
            internal IntPtr hPhysicalMonitor;
            //[PHYSICAL_MONITOR_DESCRIPTION_SIZE]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            internal string szPhysicalMonitorDescription;
        }

        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr hwnd, MONITOR_DEFAULTTO dwFlags);

        internal enum MONITOR_DEFAULTTO
        {
            NULL = 0x00000000,
            PRIMARY = 0x00000001,
            NEAREST = 0x00000002,
        }

        [DllImport("gdi32.dll")]
        internal unsafe static extern bool SetDeviceGammaRamp(IntPtr hdc, void* ramp);
    }
}
