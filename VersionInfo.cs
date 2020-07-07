using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckPowerShell
{
    class VersionInfo
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Revision { get; set; }
        public int Build { get; set; }

        public VersionInfo(int major, int minor, int revision = 0, int build = 0)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
            Build = build;
        }

        public VersionInfo(string version)
        {
            var parts = version.Split('.');
            Major = ParseInt32(parts[0]);
            Minor = parts.Length > 1 ? ParseInt32(parts[1]) : 0;
            Revision = parts.Length > 2 ? ParseInt32(parts[2]) : 0;
            Build = parts.Length > 3 ? ParseInt32(parts[3]) : 0;
        }

        public VersionInfo(Version version)
        {
            this.Major = version.Major;
            Minor = version.Minor;
            Revision = version.Revision;
            Build = version.Build;
        }

        private static Int32 ParseInt32(string p)
        {
            return !Int32.TryParse(p, out var result) ? 0 : result;
        }

        public bool LesserThan(VersionInfo targetVersion)
        {
            return IsOlderThan(this, targetVersion);
        }

        /// <summary>
        /// Check if ourVersion is older than targetVersion.
        /// </summary>
        /// <param name="ourVersion"></param>
        /// <param name="targetVersion"></param>
        /// <returns></returns>
        public static bool IsOlderThan(VersionInfo ourVersion, VersionInfo targetVersion)
        {
            if (ourVersion.Major < targetVersion.Major) return true;
            if (ourVersion.Major > targetVersion.Major) return false;
            //So, major is the same.
            if (ourVersion.Minor < targetVersion.Minor) return true;
            if (ourVersion.Minor > targetVersion.Minor) return false;
            //Minor is the same too
            if (ourVersion.Revision < targetVersion.Revision) return true;
            if (ourVersion.Revision > targetVersion.Revision) return false;
            //Revision is the same, last step - check build
            if (ourVersion.Build < targetVersion.Build) return true;
            if (ourVersion.Build > targetVersion.Build) return false;
            //They are equal, return false
            return false;
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Revision}.{Build}";
        }

        public static bool Equals(ref VersionInfo first, ref VersionInfo second)
        {
            return (first.Major == second.Major && first.Minor == second.Minor && first.Revision == second.Revision && first.Build == second.Build);
        }

        public bool BetterOrEqual(string versionCode)
        {//VersionCode should be lower or equal to ours
            var target = new VersionInfo(versionCode);
            return BetterOrEqual(target);
        }

        public bool BetterOrEqual(VersionInfo target)
        {
            if (Major == target.Major && Minor == target.Minor && Revision == target.Revision && Build == target.Build) return true;
            return (IsOlderThan(target, this));//It's a question of whether the version we are comparing with, is older than ours. Then ours is better.
        }

    }
}
