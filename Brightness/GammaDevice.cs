using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Brightness
{
    public class GammaDevice
    {
        public static unsafe bool SetGamma(uint brightnessPercent)
        {
            int valueMin = 128;
            int valueMax = 250;
            
            if (brightnessPercent > 100)
                brightnessPercent = 100;

            int value = (int)Math.Round(valueMin + (valueMax - valueMin) * brightnessPercent / 100.0f, 0);

            short* gArray = stackalloc short[3 * 256];
            short* idx = gArray;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 256; i++)
                {
                    int arrayVal = i * (value);
                    if (arrayVal > 65535)
                    {
                        arrayVal = 65535;
                    }
                    *idx = (short)arrayVal;
                    idx++;
                }
            }
            
            IntPtr hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc();
            bool retVal = WindowsApi.SetDeviceGammaRamp(hdc, gArray);
            return retVal;
        }
    }
}
