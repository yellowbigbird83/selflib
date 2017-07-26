using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebGet
{
    public class ImageBuffer
    {
        #region param
        protected List<ImageData> ListImage { get; set; }
        protected List<ImageData> ListImageChosen { get; set; }

        protected ConcurrentQueue<ImageData> TaskQueuePreview {get;set;} 
        protected ConcurrentQueue<ImageData> TaskQueueHd  {get;set;}

        public int ThreadCntPreview { get; set; }
        public int ThreadCntHd { get; set; }
        public bool StopFlag { get; set; }

        private static ImageBuffer _instance = null;
        #endregion

        private ImageBuffer()
        {
            TaskQueuePreview = new ConcurrentQueue<ImageData>();
            TaskQueueHd = new ConcurrentQueue<ImageData>();

            ThreadCntPreview = 1;
            ThreadCntHd = 1;

            ListImage = new List<ImageData>();
            ListImageChosen = new List<ImageData>();
        }


        public static ImageBuffer Get()
        {
            if(_instance == null)
            {
                _instance = new ImageBuffer();
            }
            return _instance;
        }

        protected object ObjLockListImage = new object();
        protected object ObjLockListImageChosen = new object();
        public void Add(ImageData img)
        {
            if (img == null)
                return;

            lock(ObjLockListImage)
            {
                ListImage.Add(img);
                TaskQueuePreview.Enqueue(img);
            }
        }

        public void AddHd(ImageData img)
        {
            if (img == null)
                return;

            lock (ObjLockListImageChosen)
            {
                ListImageChosen.Add(img);
                TaskQueueHd.Enqueue(img);
            }
        }

        public ImageData GetImage(int imgIdx)
        {
            if (imgIdx < 0 
                || imgIdx >= ListImage.Count)
                return null;

            return ListImage[imgIdx];
        }

        public void SetImageDownloadHd(int imgIdx)
        {
            ImageData img = GetImage(imgIdx);
        }

        public void StartDownload()
        {
            StopFlag = false;

            ManualResetEvent handlePrview = new ManualResetEvent(false);
            for (int i = 0; i < ThreadCntPreview; i++)
            {
                Thread thread = new Thread(ThreadDownloadPreview);
                thread.Start(handlePrview);
            }

            ManualResetEvent handleHd = new ManualResetEvent(false);
            for (int i = 0; i < ThreadCntHd; i++)
            {
                Thread thread = new Thread(ThreadDownloadHd);
                thread.Start(handleHd);
            }
        }

        #region thread
        public void ThreadDownloadPreview(object obj)
        {
            ManualResetEvent handle = obj as ManualResetEvent;

            ImageData imgData = null;
            string strPath = ImageData.GetSavePath();
            HttpDownloader http = new HttpDownloader(strPath);

            while(!StopFlag)
            {
                if (TaskQueuePreview.TryDequeue(out imgData))
                {
                    if(imgData == null
                        //|| 
                        )
                    {
                        continue;
                    }
                    imgData.Download(true, http);
                }
                else
                {
                    Thread.Sleep(30);
                }
            }
        }

        public void ThreadDownloadHd(object obj)
        {
            ManualResetEvent handle = obj as ManualResetEvent;
            ImageData imgData = null;
            string strPath = ImageData.GetSavePath();
            HttpDownloader http = new HttpDownloader(strPath);

            while (!StopFlag)
            {
                if (TaskQueueHd.TryDequeue(out imgData))
                {
                    if (imgData == null
                        //|| 
                        )
                    {
                        continue;
                    }
                    imgData.Download(false, http);
                }
                else
                {
                    Thread.Sleep(30);
                }
            }
        }
        #endregion


    }
}
