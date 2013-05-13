using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace OtherTestProject.Console
{
    public class StreamEffective
    {

        /*
         * 1.测试使用不同的缓存空间大小读取文件效能
         * 2.测试同样文件大小采取循环读与一次性读出来的效能比对
         * 
         */

        const int BUFFER_SIZE = 1024 * 8;



        public static MemoryStream GetStream(string filePath)
        {
            //GetMemoryStatus();

            //System.Console.WriteLine(filePath);


            // 读取网络流
            MemoryStream ms = new MemoryStream();
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            int bytesRead;
            int totalBytes = 0;
            byte[] fileTempBuffer = new byte[BUFFER_SIZE];
            try
            {
                do
                {
                    bytesRead = fs.Read(fileTempBuffer, 0, fileTempBuffer.Length);
                    ms.Write(fileTempBuffer, 0, bytesRead);
                    totalBytes += bytesRead;
                }
                while (bytesRead > 0);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("该内存流已经使用了{0}M容量的内存,该内存流最大容量为{1}M,溢出时容量为{2}M",
                    GC.GetTotalMemory(false) / (1024 * 1024),//MemoryStream已经消耗内存量
                    ms.Capacity / (1024 * 1024), //MemoryStream最大的可用容量
                    ms.Length / (1024 * 1024));//MemoryStream当前流的长度（容量）

                System.Console.WriteLine(filePath);

                System.Console.WriteLine("GetNetworkStream:" + ex.Message);
            }
            finally
            {
                ms.Close();
                ms = null;
            }
            System.Console.WriteLine("Total {0} bytes received,Done!", totalBytes);

            return ms;
        }

        #region  队列
        static long sum = 0;
        private static System.Collections.Generic.Queue<FileInfo> q = new Queue<FileInfo>();
        public static void TestGetStream()
        {
            DirectoryInfo diRoot = new DirectoryInfo(@"D:\cw\测试文件");

            FileInfo[] fiArray = diRoot.GetFiles(/*"*.rmvb", SearchOption.TopDirectoryOnly*/);

            foreach (FileInfo fi in fiArray)
            {
                q.Enqueue(fi);
            }
            while (true)
            {
                if (q.Count == 0) break;

                FileInfo fi = q.Peek();
                if (fi != null && sum + fi.Length < 500 * 1024 * 1024)
                {
                    q.Dequeue();

                    System.Threading.Interlocked.Add(ref sum, fi.Length);
                    //sum += fi.Length;

                    System.Console.WriteLine("当前文件大小{0},总大小{1}", fi.Length / 1024 / 1024, sum / 1024 / 1024);
                    System.Console.WriteLine("文件----{0}------进入发送", fi.Name);

                    Action a = () => GetStream(fi.FullName);
                    a.BeginInvoke(new AsyncCallback(AA), fi);
                }
            }
        }
        public static void AA(IAsyncResult ar)
        {
            FileInfo fi = ar.AsyncState as FileInfo;
            if (fi != null)
            {
                System.Threading.Interlocked.Add(ref sum, -fi.Length);

                //sum -= fi.Length;
                System.Console.WriteLine("\r\n");
                System.Console.WriteLine("剩余大小{0}", sum / 1024 / 1024);
                System.Console.WriteLine("文件----{0}------发送完成，大小{1}", fi.Name, fi.Length / 1024 / 1024);
                System.Console.WriteLine("\r\n");
            }
        }
        #endregion

        #region  链表
        private static LinkedList<FileInfo> ls = new LinkedList<FileInfo>();
        private static long sum1 = 0;
        public static void TestGetStream2()
        {
            DirectoryInfo diRoot = new DirectoryInfo(@"D:\cw\测试文件");

            FileInfo[] fiArray = diRoot.GetFiles(/*"*.rmvb", SearchOption.TopDirectoryOnly*/);

            foreach (FileInfo fi in fiArray)
            {
                ls.AddLast(fi);
            }

            while (true)
            {
                if (ls.Count == 0) break;
                FileInfo fi = ls.First.Value;

                if (fi != null && sum1 + fi.Length > 500 * 1024 * 1024)
                {
                    IEnumerable<FileInfo> e = ls.Where<FileInfo>(m => { return m.Length < 500 * 1024 * 1024 - sum1; });
                    FileInfo[] fiAry = e.ToArray();
                    if (fiAry.Length > 0)
                        fi = fiAry[0];
                }

                if (fi != null && sum1 + fi.Length < 500 * 1024 * 1024)
                {
                    ls.Remove(fi);
                    sum1 += fi.Length;

                    //System.Console.WriteLine("当前文件大小{0},总大小{1}", fi.Length / 1024 / 1024, sum1 / 1024 / 1024);
                    //System.Console.WriteLine("文件----{0}------进入发送", fi.Name);

                    Action a = () => GetStream(fi.FullName);

                    a.BeginInvoke(new AsyncCallback(BB), fi);
                }
            }
        }
        public static void BB(IAsyncResult ar)
        {
            FileInfo fi = ar.AsyncState as FileInfo;
            if (fi != null)
            {
                sum1 -= fi.Length;
                //System.Console.WriteLine("\r\n");
                //System.Console.WriteLine("剩余大小{0}", sum1 / 1024 / 1024);
                //System.Console.WriteLine("文件----{0}------发送完成，大小{1}", fi.Name, fi.Length / 1024 / 1024);
                //System.Console.WriteLine("\r\n");
            }
        }
        #endregion
        private static object objLock = new object();
        public static void TestGetStream1()
        {
            DirectoryInfo diRoot = new DirectoryInfo(@"D:\cw\测试文件");

            FileInfo[] fiArray = diRoot.GetFiles("*.rmvb", SearchOption.TopDirectoryOnly);

            foreach (FileInfo fi in fiArray)
            {
                Action ac = () => GetStream(fi.FullName);
                ac.BeginInvoke(null, null);
            }
        }

        public struct MEMORY_INFO
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }
        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);

        private static void GetMemoryStatus()
        {
            MEMORY_INFO MemInfo;
            MemInfo = new MEMORY_INFO();
            GlobalMemoryStatus(ref MemInfo);

            long totalMb = Convert.ToInt64(MemInfo.dwTotalPhys.ToString()) / 1024 / 1024;
            long avaliableMb = Convert.ToInt64(MemInfo.dwAvailPhys.ToString()) / 1024 / 1024;

            System.Console.WriteLine("物理内存共有" + totalMb + " MB");
            System.Console.WriteLine("可使用的物理内存有" + avaliableMb + " MB");
        }
    }
}
