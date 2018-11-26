using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;



namespace MobileApi.Controllers
{
    public class DownloadController : Controller
    {
        //Variables
        public string _url;
        //private readonly string _url;
        public string _fullPathWhereToSave;
        //private readonly string _fullPathWhereToSave;
        private bool _result = false;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0);


        // GET: Download
        public ActionResult IndexDownload()
        {
            ViewBag.Title = "DownloadTesting";
            return View();
        }


        [HttpGet]
        public void PretendConstructor(string url, string fullPathWhereToSave)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url");
            if (string.IsNullOrEmpty(fullPathWhereToSave)) throw new ArgumentNullException("fullPathWhereToSave");

            this._url = url;
            this._fullPathWhereToSave = fullPathWhereToSave;
            int i = 1;



        }

        public bool StartDownload(int timeout)
        {
            //button1_Click();
            //button1_Click(new object(), new System.EventArgs());
            //button1_Click01(new object(), new System.EventArgs());
            //suckIT();
            //suckITSave();

            try
            {
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(_fullPathWhereToSave));

                if (System.IO.File.Exists(_fullPathWhereToSave))
                {
                    System.IO.File.Delete(_fullPathWhereToSave);
                }
                using (WebClient client = new WebClient())
                {
                    var ur = new Uri(_url);
                    // client.Credentials = new NetworkCredential("username", "password");
                    //client.DownloadProgressChanged += WebClientDownloadProgressChanged;
                    //client.DownloadFileCompleted += WebClientDownloadCompleted;
                    Console.WriteLine(@"Downloading file:");
                    client.DownloadFile(ur, _fullPathWhereToSave);
                    _semaphore.Wait(timeout);
                    return _result && System.IO.File.Exists(_fullPathWhereToSave);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Was not able to download file!");
                Console.Write(e);
                return false;
            }
            finally
            {
                this._semaphore.Dispose();
            }
        }
        private void button1_Click()
        {
            FolderBrowserDialog fytb = new FolderBrowserDialog();

            fytb.RootFolder = Environment.SpecialFolder.MyDocuments;
            fytb.ShowDialog();
            //if (fytb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            fytb.Description = "random text to user in FBD";
            MessageBox.Show(fytb.SelectedPath);



        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog fytb = new FolderBrowserDialog();

            fytb.RootFolder = Environment.SpecialFolder.MyDocuments;
            //if (fytb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            fytb.Description = "random text to user in FBD";
            MessageBox.Show(fytb.SelectedPath);



        }
        private void button1_Click01(object sender, System.EventArgs e)
        {
            SaveFileDialog fytb = new SaveFileDialog();

            //fytb.RootFolder = Environment.SpecialFolder.MyDocuments;
            fytb.DefaultExt = ".xlsx";
            fytb.ShowDialog();
            //if (fytb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            MessageBox.Show(fytb.DefaultExt);



        }

        public string suckIT()

        {
            string selectedPath = "selectedPathDeclared";
            var t = new Thread((ThreadStart)(() => {
                FolderBrowserDialog fytb = new FolderBrowserDialog();
                fytb.Description = "Select Folder to Save DataBase File";
                fytb.RootFolder = System.Environment.SpecialFolder.MyDocuments;
                fytb.ShowNewFolderButton = true;
                if (fytb.ShowDialog() == DialogResult.Cancel)
                    return;
                selectedPath = fytb.SelectedPath;
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            MessageBox.Show(selectedPath);
            Console.WriteLine(selectedPath);
            return selectedPath;
        }
        public void suckITSave()

        {
            string selectedPath = "whatmyname";
            var t = new Thread((ThreadStart)(() => {
                SaveFileDialog fytb = new SaveFileDialog();
                fytb.DefaultExt = ".xlsx";
                if (fytb.ShowDialog() == DialogResult.Cancel)
                    return;
                selectedPath = fytb.ToString();
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            MessageBox.Show(selectedPath);
            Console.WriteLine(selectedPath);
        }
    }
}