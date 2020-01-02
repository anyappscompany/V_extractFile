using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO;

namespace V_extractFile
{
    class sinf
    {
        public enum WinEnum { winUnknown, win95, win98, winME, winNT351, winNT4, win2000, winXP, winServ2003, winVista, win7, win8 };
        public WinEnum OsName; // содержин название ОС в WinEnum
        public string PathToTheTemporaryFolderUser; //содержит путь к временной папке пользователя
        
        public sinf()
        {
            OsName = getOSName(); 
            PathToTheTemporaryFolderUser = getPathToTheTEMPFolderUser(OsName);            
        }
        public WinEnum getOSName()
        {
            OperatingSystem os = Environment.OSVersion;
            WinEnum osName = WinEnum.winUnknown;
            switch (os.Platform)
            {
                case System.PlatformID.Win32Windows:
                    switch (os.Version.Minor)
                    {
                        case 0:
                            osName = WinEnum.win95;
                            break;
                        case 10:
                            osName = WinEnum.win98;
                            break;
                        case 90:
                            osName = WinEnum.winME;
                            break;
                    }
                    break;

                case System.PlatformID.Win32NT:
                    switch (os.Version.Major)
                    {
                        case 3:
                            osName = WinEnum.winNT351;
                            break;
                        case 4:
                            osName = WinEnum.winNT4;
                            break;
                        case 5:
                            if (os.Version.Minor == 0)
                                osName = WinEnum.win2000;
                            else if (os.Version.Minor == 1)
                                osName = WinEnum.winXP;
                            else if (os.Version.Minor == 2)
                                osName = WinEnum.winServ2003;
                            break;
                        case 6:
                            osName = WinEnum.winVista;
                            if (os.Version.Minor == 0)
                                osName = WinEnum.winVista;
                            else if (os.Version.Minor == 1)
                                osName = WinEnum.win7;
                            else if (os.Version.Minor == 2)
                                osName = WinEnum.win8;
                            break;
                    }
                    break;
            }
            return osName;
        }
        public string getPathToTheTEMPFolderUser(WinEnum osName) // возвращает путь к папке Temp текущего пользователя
        {
            string vn_PathToTheTemporaryFolderUser = String.Empty;
            switch (osName)
            {
                case WinEnum.winUnknown:
                    vn_PathToTheTemporaryFolderUser = String.Empty;
                    break;
                case WinEnum.win95:
                    break;
                case WinEnum.win98:
                    break;
                case WinEnum.winME:
                    break;
                case WinEnum.winNT351:
                    break;
                case WinEnum.winNT4:
                    break;
                case WinEnum.win2000:
                    break;
                case WinEnum.winXP:
                    {
                        vn_PathToTheTemporaryFolderUser = Environment.GetEnvironmentVariable("Temp", EnvironmentVariableTarget.User);
                    }
                    break;
                case WinEnum.winServ2003:
                    break;
                case WinEnum.winVista:
                    {
                        vn_PathToTheTemporaryFolderUser = Environment.GetEnvironmentVariable("Temp", EnvironmentVariableTarget.User);
                    }
                    break;
                case WinEnum.win7:
                    {
                        vn_PathToTheTemporaryFolderUser = Environment.GetEnvironmentVariable("Temp", EnvironmentVariableTarget.User);
                    }
                    break;
                case WinEnum.win8:
                    {
                        vn_PathToTheTemporaryFolderUser = Environment.GetEnvironmentVariable("Temp", EnvironmentVariableTarget.User);
                    }
                    break;
            }
            return vn_PathToTheTemporaryFolderUser;
        }
        public string profPath = String.Empty;
        public string profINIPath = String.Empty;
        public string localState = String.Empty;
        public bool installedGoogleChrome()
        {
            
            int rating = -1;
            WinEnum opsys = getOSName();
            switch (opsys)
            {
                case WinEnum.win7:
                    {
                        this.localState = Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Local\Google\Chrome\User Data";

                        if (File.Exists(this.localState + @"\Local State"))
                        {
                            rating += 1;
                        }
                        
                    }
                    break;
                case WinEnum.win8:
                    {
                        this.localState = Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Local\Google\Chrome\User Data";

                        if (File.Exists(this.localState + @"\Local State"))
                        {
                            rating += 1;
                        }
                        
                    }
                    break;
                case WinEnum.winVista:
                    {
                        this.localState = Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Local\Google\Chrome\User Data";

                        if (File.Exists(this.localState + @"\Local State"))
                        {
                            rating += 1;
                        }
                        
                    }
                    break;
                case WinEnum.winXP:
                    {
                        this.localState = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Local Settings\Application Data\Google\Chrome\User Data";

                        if (File.Exists(this.localState + @"\Local State"))
                        {
                            rating += 1;
                        }
                        
                    }
                    break;
                default:
                    return false;
                    break;
            }

            if (rating >= 0)
            {
                return true;
            }
            else
            {
                return false;
            } return false;
        }
        public bool installedMozilla()
        {
            
            int rating = -1;
            WinEnum opsys = getOSName(); 
            switch(opsys)
            {
                case WinEnum.win7:{
                    this.profPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\Profiles";
                    this.profINIPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\profiles.ini";
                    if(File.Exists(this.profINIPath))
                    {
                        rating += 1;
                    }
                    /*DirectoryInfo drInfo = new DirectoryInfo(this.profPath);
                    if (drInfo.Exists)
                    {
                        DirectoryInfo[] di = drInfo.GetDirectories();
                        int count = 0;
                        foreach (var dir in di)
                        {
                            //MessageBox.Show(dir.Name);
                            count++;
                        }
                        if (count > 0) rating += 1;
                    }*/
                }
                    break;
                case WinEnum.win8:
                    {
                        this.profPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\Profiles";
                        this.profINIPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\profiles.ini";
                        if (File.Exists(this.profINIPath))
                        {
                            rating += 1;
                        }
                        /*DirectoryInfo drInfo = new DirectoryInfo(this.profPath);
                        if (drInfo.Exists)
                        {
                            DirectoryInfo[] di = drInfo.GetDirectories();
                            int count = 0;
                            foreach (var dir in di)
                            {
                                //MessageBox.Show(dir.Name);
                                count++;
                            }
                            if (count > 0) rating += 1;
                        }*/
                    }
                    break;
                case WinEnum.winVista:
                    {
                        this.profPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\Profiles";
                        this.profINIPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\profiles.ini";
                        if (File.Exists(this.profINIPath))
                        {
                            rating += 1;
                        }
                        /*DirectoryInfo drInfo = new DirectoryInfo(this.profPath);
                        if (drInfo.Exists)
                        {
                            DirectoryInfo[] di = drInfo.GetDirectories();
                            int count = 0;
                            foreach (var dir in di)
                            {
                                //MessageBox.Show(dir.Name);
                                count++;
                            }
                            if (count > 0) rating += 1;
                        }*/
                    }
                    break;
                case WinEnum.winXP:
                    {
                        this.profPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\Profiles";
                        this.profINIPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\profiles.ini";
                        if (File.Exists(this.profINIPath))
                        {
                            
                            rating += 1;
                        }
                        else
                        {
                            
                        }
                        /*DirectoryInfo drInfo = new DirectoryInfo(this.profPath);
                        if (drInfo.Exists)
                        {
                            DirectoryInfo[] di = drInfo.GetDirectories();
                            int count = 0;
                            foreach (var dir in di)
                            {
                                //MessageBox.Show(profPath);
                                count++;
                            }
                            if (count > 0) rating += 1;
                        }*/
                    }
                    break;
                case WinEnum.winServ2003:
                    {
                        
                        this.profPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\Profiles";
                        this.profINIPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\profiles.ini";
                        if (File.Exists(this.profINIPath))
                        {
                            //MessageBox.Show("Путь сущ");
                            rating += 1;
                        }
                        else
                        {
                            //MessageBox.Show("Путь не сущ");
                        }
                        /*DirectoryInfo drInfo = new DirectoryInfo(this.profPath);
                        if (drInfo.Exists)
                        {
                            DirectoryInfo[] di = drInfo.GetDirectories();
                            int count = 0;
                            foreach (var dir in di)
                            {
                                //MessageBox.Show(profPath);
                                count++;
                            }
                            if (count > 0) rating += 1;
                        }*/
                    }
                    break;
                default: 
                    return false;
                    break;
            }

            if (rating >= 0)
            {
                return true;
            }
            else
            {
                return false;
            } return false;
        }

        public string fPreferences = String.Empty;
        public bool installedOpera()
        {

            int rating = -1;
            WinEnum opsys = getOSName();
            switch (opsys)
            {
                case WinEnum.win7:
                    {
                        this.fPreferences = Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Preferences";
                        if (File.Exists(this.fPreferences))
                        {
                            rating += 1;
                        }
                    }
                    break;
                case WinEnum.win8:
                    {
                        this.fPreferences = Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Preferences";

                        if (File.Exists(this.fPreferences))
                        {
                            rating += 1;
                        }                        
                    }
                    break;
                case WinEnum.winVista:
                    {
                        this.fPreferences = Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Preferences";

                        if (File.Exists(this.fPreferences))
                        {
                            rating += 1;
                        }
                        
                    }
                    break;
                case WinEnum.winXP:
                    {
                        this.fPreferences = Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Preferences";

                        if (File.Exists(this.fPreferences))
                        {

                            rating += 1;
                        }
                        else
                        {

                        }
                    }
                    break;
                case WinEnum.winServ2003:
                    {

                        this.fPreferences = Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Preferences";

                        if (File.Exists(this.fPreferences))
                        {
                            //MessageBox.Show("Путь сущ");
                            rating += 1;
                        }
                        else
                        {
                            //MessageBox.Show("Путь не сущ");
                        }                        
                    }
                    break;
                default:
                    return false;
                    break;
            }

            if (rating >= 0)
            {
                return true;
            }
            else
            {
                return false;
            } return false;
        }
        public void checkSection(string fpath, string sectionName) // поиск секции в файле, несли нет, то создается
        {
            try
            {
                bool stat = false;
                foreach (string linet in File.ReadAllLines(@fpath))
                {
                    if (linet == sectionName)
                    {
                        stat = true;
                    }
                }
                if (!stat)
                {
                    using (var writer = new StreamWriter(@fpath, true))
                    {
                        writer.WriteLine(sectionName, true);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
