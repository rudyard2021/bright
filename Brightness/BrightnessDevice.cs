using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Brightness
{
    public static class BrightnessDevice
    {
        public static void SetBrightness(uint brightness)
        {
            if (brightness > 100)
            {
                brightness = 100;
            }
            
            // get handle to primary display
            IntPtr hmon = WindowsApi.MonitorFromWindow(IntPtr.Zero, WindowsApi.MONITOR_DEFAULTTO.PRIMARY);

            // get number of physical displays (assume only one for simplicity)
            int num;
            bool success = WindowsApi.GetNumberOfPhysicalMonitorsFromHMONITOR(hmon, out num);
            WindowsApi.PHYSICAL_MONITOR pmon = new WindowsApi.PHYSICAL_MONITOR();

            success = WindowsApi.GetPhysicalMonitorsFromHMONITOR(hmon, num, ref pmon);

            uint min, max, current;
            // commonly min and max are 0-100 which represents a percentage brightness
            success = WindowsApi.GetMonitorBrightness(pmon.hPhysicalMonitor, out min, out current, out max);

            // set to full brightness
            success = WindowsApi.SetMonitorBrightness(pmon.hPhysicalMonitor, brightness);

            success = WindowsApi.DestroyPhysicalMonitors(num, ref pmon);
        }
    }

}
