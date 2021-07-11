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
    public class Program
    {
        public static void Main(string[] args)
        {
            uint gammaDefault = Brightness.Properties.Settings.Default.Gamma;
            uint brightDefault = Brightness.Properties.Settings.Default.Bright;

            string gammaValue = null;
            string brightValue = null;

            if (args.Length > 0)
            {
                gammaValue = args[0];
                brightValue = args[0];
            }
            else
            {
                Dictionary<string, string> data = GetJson();
                if (data.ContainsKey("name"))
                {
                    if (data["name"] == "gamma.up")
                        gammaValue = (gammaDefault + 3).ToString();
                    else if (data["name"] == "gamma.down")
                        gammaValue = (gammaDefault - 3).ToString();
                    else if (data["name"] == "gamma")
                        gammaValue = data["value1"];

                    else if (data["name"] == "bright.up")
                        brightValue = (brightDefault + 3).ToString();
                    else if (data["name"] == "bright.down")
                        brightValue = (brightDefault - 3).ToString();
                    else if (data["name"] == "bright")
                        brightValue = data["value1"];
                }
            }

            uint gamma, bright;
            
            uint.TryParse(Default(gammaValue, gammaDefault.ToString()), out gamma);
            uint.TryParse(Default(brightValue, brightDefault.ToString()), out bright);
            Brightness.Properties.Settings.Default.Bright = bright;
            Brightness.Properties.Settings.Default.Gamma = gamma;
            Brightness.Properties.Settings.Default.Save();

            GammaDevice.SetGamma(gamma);
            BrightnessDevice.SetBrightness(bright);
        }

        public static string Default(string value, string defaultValue)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            return value;
        }

        public static Dictionary<string, string> GetJson()
        {
            string fileRequest = "request.data";
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (File.Exists(fileRequest) == false)
                return data;

            using (StreamReader stream = new StreamReader(fileRequest))
            {
                string json = stream.ReadToEnd();
                data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            return data;
        }
    }
}
