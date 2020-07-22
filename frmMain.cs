using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckPowerShell
{
    public partial class frmMain : Form
    {
        private VersionInfo winVer;//Current windows version
        private Dictionary<string, VersionInfo> WindowsVersions;
        private bool net45Installed;
        private VersionInfo netVersion;
        private VersionInfo maxVerNet; //Maximum version of NETFX for this Windows

        //double verWindowsMin = 
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            winVer = new VersionInfo(Environment.OSVersion.Version);
            //Minimal version for 4.5 is Windows Server 2008 SP2 and Windows Vista SP2
            WindowsVersions = new Dictionary<string, VersionInfo>();
            WindowsVersions.Add("win2008r2", new VersionInfo(6,1,0,0));//Windows 7 and Server 2008 R2
            WindowsVersions.Add("win2008sp2", new VersionInfo(6,0,6002, 0));//Windows Vista SP2 and 2008 SP2
            WindowsVersions.Add("win8", new VersionInfo(6,2));//Windows 8 and Windows Server 2012
            WindowsVersions.Add("win81", new VersionInfo(6,3));//Windows 8.1, Windows Server 2012 R2, 6.3.9200. Update 1 is 6.3.9600
            WindowsVersions.Add("win10", new VersionInfo("10.0.10240")); //Windows 10, first release
            WindowsVersions.Add("win2016", new VersionInfo("10.0.1607.14393"));//Windows Server 2016 RTM
            WindowsVersions.Add("win2019", new VersionInfo("10.0.1809.17763"));//Windows Server 2019 LTSC
            string explanation = "";

            if (winVer.LesserThan(WindowsVersions["win2008sp2"]))
            {
                MessageBox.Show("This application works for Windows Server 2008 R2 / Windows 7 and up. It appears that your operating system is older than that. Note, that minimum version required for PowerShell 7 / .NET Framework 4.5 is Windows Server 2008 SP2 / Windows Vista SP2.", "Windows version outdated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblExplanation.Text = "Your Windows version is too old for this application.";
                return;
            }
            //Check version of PowerShell
            VersionInfo psVersion = null;
            //1. Do we have PS7 and up?
            bool newPSInstalled = RegHelper.BranchExists(@"SOFTWARE\Microsoft\PowerShellCore\InstalledVersions");
            bool oldPSInstalled = RegHelper.BranchExists(@"SOFTWARE\Microsoft\PowerShell");
            if (!newPSInstalled)
            {
                cmdUpgradePS.Enabled = true;
                explanation += "New version of PowerShell was not found.";
                //Check for version of old powershell
                if (!oldPSInstalled)
                {
                    explanation += " Old version of PowerShell wasn't found either.";
                } else
                {
                    if (RegHelper.BranchExists(@"SOFTWARE\Microsoft\PowerShell\3\PowerShellEngine"))
                    {
                        psVersion = new VersionInfo(RegHelper.GetSettingStringEx(@"SOFTWARE\Microsoft\PowerShell\3\PowerShellEngine", "PowerShellVersion"));
                    }
                    else if (RegHelper.BranchExists(@"SOFTWARE\Microsoft\PowerShell\1\PowerShellEngine"))
                    {
                        psVersion = new VersionInfo(RegHelper.GetSettingStringEx(@"SOFTWARE\Microsoft\PowerShell\1\PowerShellEngine", "PowerShellVersion"));
                    }
                }
            } else
            {//We have new PS installed. Hurray!
                psVersion = new VersionInfo(RegHelper.GetSettingStringEx(@"SOFTWARE\Microsoft\PowerShellCore\InstalledVersions\31ab5147-9a97-4452-8443-d9709f0516e1","SemanticVersion"));
                explanation = "New version of PowerShell detected, update is not required.";
            }

            if (psVersion != null) lblVerPS.Text = psVersion.ToString();
            //Check version of .NET Framework
            net45Installed = RegHelper.BranchExists(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full");
            if (!net45Installed)
            {
                explanation += " No .NET Framework 4.5 or higher is installed. This is prerequisite for PowerShell 7.";
                //Since we don't have 4.5, perhaps we can install better one? Minimum for 4.5 is 7SP2, 4.6 is maximum for Vista/Win2008, 4.6.1 maximum for Win8, but not for Win7. Others can take everything
                maxVerNet = new VersionInfo("4.7.2");
                //if (VersionInfo.Equals(winVer, WindowsVersions["win2008sp2"])) maxVerNet.

            } else
            {
                //Ok, what version do we have?
                string netVersionID = RegHelper.GetSettingStringEx(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full", "Version");
                netVersion = new VersionInfo(netVersionID);
                lblVerNET.Text = netVersionID;
                explanation += " .NET Framework version is compatible with latest version of PowerShell.";
            }
            //Color code versions
            ColourLabelVersion("4.5", "4.7.2", netVersion, ref lblVerNET);
            //PS
            ColourLabelVersion("5.1", "7", psVersion, ref lblVerPS);
            //Display update buttons?
            if (lblVerPS.ForeColor != Color.DarkGreen && net45Installed) cmdUpgradePS.Visible = true;
            if (lblVerNET.ForeColor != Color.DarkGreen) cmdUpgradeNET.Visible = true;
            //WMF?
            if ((psVersion == null || psVersion.Major < 4) && winVer.LesserThan(WindowsVersions["win2016"]))
            {
                cmdWMF.Visible = true;
            }
            lblExplanation.Text = explanation;
        }

        private void ColourLabelVersion(string minVersion, string goodVersion, VersionInfo actualVersion, ref Label targetControl)
        {
            if (actualVersion.BetterOrEqual(minVersion)) targetControl.ForeColor = Color.DarkOrange;
            else targetControl.ForeColor = Color.DarkRed;
            if (actualVersion.BetterOrEqual(goodVersion)) targetControl.ForeColor = Color.DarkGreen;
        }

        private void cmdUpgradePS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string psSetupFile = @"PowerShell-7.0.2-win-x64.msi";
            string psLink = @"https://github.com/PowerShell/PowerShell/releases/download/v7.0.2/PowerShell-7.0.2-win-x64.msi";
            //Detect local file
            try
            {
                string setupFile = GetLocalFile(psSetupFile, psLink);
                if (String.IsNullOrEmpty(setupFile)) return;
                //Execute local file
                //msiexec.exe /i file.msi /passive
                RunMSI(setupFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private bool RunMSI(string setupFile)
        {
            string arg = setupFile;
            if (setupFile.Contains(" ")) arg = "\"" + setupFile + "\"";
            var cmd = new Command
            {
                Line = "msiexec.exe",
                Arguments = $"/i {arg} /passive",
                Shell = false,
                Wait = false,
                UI = ProcessWindowStyle.Normal
            };
            var result = (cmd.Execute() > 0);//this is process ID
            if (result)
            {
                MessageBox.Show("Setup has started. Please wait for it to end, then restart this application, if further installations needed.\nIt may take a few minutes to complete installation.", "Setup started", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return result;
        }

        private string GetLocalFile(string fileName, string url)
        {
            //Do we have this file next to executable?
            var appDir = GetAppDir();
            bool isNetwork = PathIsNetworkPath(appDir);
            var fullFileName = Path.Combine(appDir, fileName);
            if (File.Exists(fullFileName) &! isNetwork) return fullFileName;
            var tmpFileName = Path.Combine(Path.GetTempPath(), fileName);
            if (File.Exists(tmpFileName)) return tmpFileName;//Already downloaded
            //Are we running from network location? Then copy file to temporary directory locally... If that file doesn't exist here yet
            if (File.Exists(fullFileName) && isNetwork)
            {
                File.Copy(fullFileName, tmpFileName);
                return tmpFileName;
            }

            if (String.IsNullOrEmpty(url))
            {
                MessageBox.Show($"Couldn't find file {fileName} in {appDir} and don't know the url for it. Can't proceed.", "File not found", MessageBoxButtons.OK);
                return "";
            }
            //If file doesn't exist - let's download it. Check that we have at least 200Mb free space on temp drive.
            var sure = AskUser($"We couldn't find file {fileName} in {appDir}, but we can download it from {url}. Would you like to proceed with download and execution of that file? This might take some time.\n\nOtherwise you can copy required file to {fullFileName}");
            if (sure != DialogResult.Yes) return "";
            //Download the file
            ServicePointManager.Expect100Continue = true;                
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            using (var client = new WebClient())
            {
                
                client.DownloadFile(url, tmpFileName);
            }

            if (!File.Exists(tmpFileName))
            {
                MessageBox.Show($"Error downloading file from {url}", "File not downloaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            return tmpFileName;
        }

        internal DialogResult AskUser(string question, string title = "Confirmation")
        {
            return (MessageBox.Show(question, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1));
        }

        internal static string GetAppDir()
        {
            var ass = Assembly.GetEntryAssembly();
            return ass == null ? "" : Path.GetDirectoryName (ass.Location);
        }

        [DllImport("shlwapi.dll")]
        private static extern bool PathIsNetworkPath(string pszPath);

        private void cmdUpgradeNET_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string psSetupFile = @"NDP472-KB4054531-Web.exe";
            string psLink = @"http://go.microsoft.com/fwlink/?LinkId=863262";
            //Detect local file
            try
            {
                string setupFile = GetLocalFile(psSetupFile, psLink);
                if (String.IsNullOrEmpty(setupFile)) return;
                //Execute local file
                //msiexec.exe /i file.msi /passive
                RunEXE(setupFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void RunEXE(string setupFile)
        {
            //string arg = setupFile;
            //if (setupFile.Contains(" ")) arg = "\"" + setupFile + "\"";
            var cmd = new Command
            {
                Line = setupFile,
                Arguments = $"/passive /showrmui /promptrestart",
                Shell = false,
                Wait = false,
                UI = ProcessWindowStyle.Normal
            };
            var result = (cmd.Execute() > 0);//this is process ID
            if (result)
            {
                MessageBox.Show("Setup has started. Please wait for it to end, then restart this application, if further installations needed.\nIt may take a few minutes to complete installation.", "Setup started", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return;
        }

        private void cmdWMF_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var cmd = new Command();
            cmd.Line = "https://www.microsoft.com/en-us/download/details.aspx?id=54616";
            cmd.Shell = true;
            cmd.Execute();
        }
    }
}
