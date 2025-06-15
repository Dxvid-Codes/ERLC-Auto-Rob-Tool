using System;
using System.Threading;
using Robberies;
using Utils;

namespace ERLC_AutoRob
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("ERLC Auto-Rob Tool started. Press Ctrl+C to exit.");

            // Optionally set process DPI awareness if needed:
            WindowUtils.SetProcessDpiAwareness();

            while (true)
            {
                try
                {
                    // Only run detection when Roblox is foreground
                    if (!WindowUtils.IsRobloxActiveWindow())
                    {
                        Thread.Sleep(500);
                        continue;
                    }

                    var robbery = RobberyDetector.DetectCurrentRobbery();
                    switch (robbery)
                    {
                        case RobberyType.GlassCutting:
                            Console.WriteLine("[Detector] Glass Cutting detected. Running...");
                            GlassCutting.StartProcess();
                            break;
                        case RobberyType.ATM:
                            Console.WriteLine("[Detector] ATM detected. Running...");
                            ATM.StartProcess();
                            break;
                        case RobberyType.Lockpick:
                            Console.WriteLine("[Detector] Lockpick detected. Running...");
                            Lockpick.StartProcess();
                            break;
                        default:
                            // No known minigame visible: wait a bit
                            Thread.Sleep(500);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Error] " + ex);
                    // On error, wait a bit before retrying
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
