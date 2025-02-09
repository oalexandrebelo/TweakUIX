﻿using System.Management.Automation;

namespace TweakUIX.Tweaks.System
{
    internal class EnableWSL : TweaksBase
    {
        private static readonly ErrorHelper logger = ErrorHelper.Instance;

        public override string ID()
        {
            return "Enable Microsoft Windows Subsystem for Linux";
        }

        public override string Info()
        {
            return "";
        }

        public override bool CheckTweak()
        {
            string script = "Get-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux";
            PowerShell powerShell = PowerShell.Create();
            powerShell.AddScript(script);
            var results = powerShell.Invoke();

            foreach (var item in results)
            {
                var Status = item.Members["State"].Value;

                if (Status.ToString() == "Enabled")
                {
                    logger.Log("Microsoft Windows Subsystem for Linux is already enabled.");
                    return false;
                }
            }
            logger.Log("Microsoft Windows Subsystem for Linux is disabled.");

            return true;
        }

        public override bool DoTweak()
        {
            string script = "Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux";

            PowerShell powerShell = PowerShell.Create();

            powerShell.AddScript(script);
            powerShell.Invoke();

            logger.Log("- Microsoft Windows Subsystem for Linux has been successfully enabled.");
            return true;
        }

        public override bool UndoTweak()
        {
            string script = "Disable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux";

            PowerShell powerShell = PowerShell.Create();

            powerShell.AddScript(script);
            powerShell.Invoke();

            logger.Log("- Microsoft Windows Subsystem for Linux has been successfully disabled.");
            return true;
        }
    }
}