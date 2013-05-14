using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;

using System.Text.RegularExpressions;
using System.Diagnostics;
using Cnki.NetworkDisk.Client;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Xml.Linq;

using MSXML2;
using System.Reflection;
using System.Collections;
using Wd.Common;
using Wd.Extensions;

namespace OtherTestProject.Console
{
    class Test
    {
        public static void Reverse(int[] array, int begin, int end)
        {
            //if (array == null || array.Length == 0) throw new ArgumentNullException("array", "数组对象不能为空");
            //if (begin < 0 || begin > array.Length) throw new ArgumentOutOfRangeException("begin", "参数begin值应该大于0并且小于end");
            //if (end < begin || end > array.Length) throw new ArgumentOutOfRangeException("end", "参数end值应该大于begin值并且小于数组最大长度");

            //for (int i = begin, j = end; i < j; i++, j--)
            //{
            //    int temp = array[i];
            //    array[i] = array[j];
            //    array[j] = temp;
            //}
        }
    }
    class Test1
    {
        private int value = 10;
        private void Method()
        {
            object o = (object)value;
            System.Console.WriteLine(o);
        }
    }

    partial class Program
    {
        private static DOMDocument40 domDoc;

        static void Main(string[] args)
        {
            new B();

            //new WebResponseEncoding().GetEncoding("http://2010.163.com/special/00863I1M/rss_2010.xml");

            //Program.XmlResolveTest();
            //Program.SqlBatchImportTest();
            //Program.WebRequestEncoding();
            Program.HttpWebRequestTest0();

            //int[] array = { 1, 2, 3, 4, 5, 6 };

            //Test.Reverse(array, 1, 0);

            //for (int i = 0; i < array.Length; i++)
            //{
            //    System.Console.WriteLine(array[i]);
            //}


            //AgrithomTest.Test();


            #region 单例测试
            //for (int i = 0; i < 100; i++)
            //{
            //    Action a = () => System.Console.WriteLine(Singleton.Instance.GetHashCode());
            //    a.BeginInvoke(null, null);
            //} 
            #endregion

            #region 之前
            // 1:
            //ReplaceEfficiency re = new ReplaceEfficiency();
            //// 2.初始化
            //re.InitResource();

            //int interations = 10000;

            //CodeTimer.Initialize();

            //CodeTimer.Time("使用基础替代方式(index,substring)效率", interations, () =>
            //                                                        {
            //                                                            re.ReplaceByBasicMethod();
            //                                                        });

            //CodeTimer.Time("使用正则表达式替代方式效率", interations, () =>
            //                                                        {
            //                                                            re.ReplaceByRegex();
            //                                                        });

            //CodeTimer.Time("使用节点引用替代方式效率", interations, () =>
            //                                                        {
            //                                                            re.ReplaceByRef();
            //                                                        });

            //SqlRemoveRepeat sql = new SqlRemoveRepeat();
            //sql.Test1(str);

            //CodeTimer.Initialize();


            //CodeTimer.Time("测试stream1", 1, () => StreamEffective.TestGetStream1());


            //CodeTimer.Time("测试stream2", 1, () => StreamEffective.TestGetStream());


            //CodeTimer.Time("测试stream3", 1, () => StreamEffective.TestGetStream2());

            //StreamTest.Test4();


            //new ThreadPoolInfo().Display();

            //CodeTimer.Time("aa", 1, () =>
            //{
            //    StreamEffective.GetStream(@"D:\cw\测试文件\孙鑫C++教程06.rmvb");
            //});

            //new ActionExceptionTest().TestActionException1();



            //IPAddress address = IPAddress.Parse("192.168.21.107");
            //// Start the asynchronous request for DNS information.
            //IAsyncResult result = Dns.BeginGetHostEntry(address, null, null);
            //System.Console.WriteLine("Processing request for information...");
            //// Wait until the operation completes.
            //result.AsyncWaitHandle.WaitOne(5);
            //// The operation completed. Process the results.
            //try
            //{
            //    // Get the results.
            //    IPHostEntry host = Dns.EndGetHostEntry(result);
            //    string[] aliases = host.Aliases;
            //    IPAddress[] addresses = host.AddressList;
            //    if (aliases.Length > 0)
            //    {
            //        System.Console.WriteLine("Aliases");
            //        for (int i = 0; i < aliases.Length; i++)
            //        {
            //            System.Console.WriteLine("{0}", aliases[i]);
            //        }
            //    }
            //    if (addresses.Length > 0)
            //    {
            //        System.Console.WriteLine("Addresses");
            //        for (int i = 0; i < addresses.Length; i++)
            //        {
            //            System.Console.WriteLine("{0}", addresses[i].ToString());
            //        }
            //    }
            //}
            //catch (SocketException e)
            //{
            //    System.Console.WriteLine("Exception occurred while processing the request: {0}",
            //        e.Message);
            //}
            //BlockUntilOperationCompletes.Test2();

            //StreamEffective.TestGetStream();

            //int threadId;
            //DelegateAsyncTest delegateAsync = new DelegateAsyncTest();
            //string message = delegateAsync.TestMethod(3000, out threadId);
            //System.Console.WriteLine(message);
            //System.Console.WriteLine("Current Thread Id is {0}", threadId.ToString());

            //AsyncMethodCaller caller = new AsyncMethodCaller(delegateAsync.TestMethod);
            //caller.BeginInvoke(3000, out threadId, (ar) =>
            //{
            //    AsyncMethodCaller callerEnd = ar.AsyncState as AsyncMethodCaller;
            //    message = callerEnd.EndInvoke(out threadId, ar);
            //    System.Console.WriteLine(message);
            //    System.Console.WriteLine("Current Thread Id is {0}", threadId.ToString());

            //}, caller);


            //// The asynchronous method puts the thread id here.
            //int threadId;

            //// Create an instance of the test class.
            //DelegateAsyncTest ad = new DelegateAsyncTest();

            //// Create the delegate.
            //AsyncMethodCaller caller = new AsyncMethodCaller(ad.TestMethod);

            //// Initiate the asychronous call.
            //IAsyncResult result = caller.BeginInvoke(3000,
            //    out threadId, null, null);

            //Thread.Sleep(0);
            //System.Console.WriteLine("Main thread {0} does some work.",
            //    Thread.CurrentThread.ManagedThreadId);

            //// Wait for the WaitHandle to become signaled.
            //result.AsyncWaitHandle.WaitOne();


            //// Call EndInvoke to wait for the asynchronous call to complete,
            //// and to retrieve the results.
            //string returnValue = caller.EndInvoke(out threadId, result);

            //// Close the wait handle.
            //result.AsyncWaitHandle.Close();

            //System.Console.WriteLine("The call executed on thread {0}, with return value \"{1}\".",
            //    threadId, returnValue);



            //DemonstrateAsyncPattern demonstrator = new DemonstrateAsyncPattern();
            //demonstrator.FactorizeNumberUsingCallback();
            //demonstrator.FactorizeNumberAndWait();

            //App.Test();

            //new Search().FindFile("SomeFile.dat");

            //UrlTest.GetParams("http://192.168.100.152/Grid2008/_OrganizationLibrary/TemplateIndex/M031001/Index.aspx?t=&targetname=xbdx&a=1");

            //UrlTest.GetParams("http://192.168.100.152/Grid2008/_OrganizationLibrary/TemplateIndex/M031001/Index.aspx?t=&targetname=xbdx&a=1");




            // 邮箱是否匹配
            //string pattern = @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
            //string email = "cwzyj157@sina.com";

            //bool result = email.IsMatch(pattern, StringRegexOptions.IgnoreCase);

            //Match m = email.Match(pattern);

            //MatchCollection mc = "http://192.168.100.152/Grid2008/_OrganizationLibrary/TemplateIndex/M031001/Index.aspx?t=&targetname=xbdx&a=1".Matches(@"(?<=\?|&)(.*?=.*?)(?=&|#|$)");

            //System.Console.WriteLine(result);

            //byte[] ary = new byte[50];
            //for (int i = 0; i < ary.Length; i++)
            //{
            //    ary[i] = (byte)i;
            //}

            //System.Console.WriteLine(ary.ToHex());

            //System.Console.WriteLine(ary.ToHex(" "));

            //CodeTimer.Initialize();
            //string[] ary = new string[50];
            //for (int i = 0; i < ary.Length; i++)
            //{
            //    ary[i] = "" + i + i;
            //}
            //int interations = 10000000;

            //CodeTimer.Time("使用param数组", interations, () =>
            //{
            //    TestAAA(ary);
            //});
            //CodeTimer.Time("直接使用数组", interations, () =>
            //{
            //    TestBBB(ary);
            //});

            //CodeTimer.Time("使用param数组", interations, () =>
            //{
            //    string.Format("执行{0}", new object[] { interations });
            //});
            //CodeTimer.Time("直接使用数组", interations, () =>
            //{
            //    string.Format("执行{0}", interations);
            //});

            //ExtensionTest();

            //string value = Regedit.GetValue("FileServiceInstall");

            //System.Console.WriteLine(value);

            //TestWcfService();

            //Stream stream = File.OpenRead(@"C:\Users\w.chen\Desktop\临时文件\80M - 副本.rar");

            //CodeTimer.Initialize();
            //CodeTimer.Time("分段读", 1, () =>
            //{
            //    MemoryStream ms = BackUpSteam(stream);
            //    ms.Close();
            //    ms = null;
            //});
            //stream.Position = 0;
            //CodeTimer.Time("分段读", 1, () =>
            //{
            //    MemoryStream ms = BackUpSteam4(stream);
            //    ms.Close();
            //    ms = null;
            //});
            //stream.Position = 0;
            //CodeTimer.Time("分段读", 1, () =>
            //{
            //    MemoryStream ms = BackUpSteam1(stream);
            //    ms.Close();
            //    ms = null;
            //});
            //stream.Position = 0;
            //CodeTimer.Time("分段读", 1, () =>
            //{
            //    MemoryStream ms = BackUpSteam2(stream);
            //    ms.Close();
            //    ms = null;
            //}); 
            //stream.Position = 0;
            //CodeTimer.Time("分段读", 1, () =>
            //{
            //    MemoryStream ms = BackUpSteam3(stream);
            //    ms.Close();
            //    ms = null;
            //});

            //stream.Close();
            //stream = null;

            #endregion

            #region 测试文件是否相同
            //string scdbFolderPath = @"D:\cw\ProjectCode\Just_Code\OtherTestProject\OtherTestProject.Console\OtherTestProject.Console\TestReplaceEfficiency\cachefile\gb\search";
            //new ScdbSame(scdbFolderPath).CompareAllFile();
            #endregion

            //new ThreadPoolApp().CancellationTokenTest();

            //new ThreadPoolApp().LinkedTokenTest();

            //new TaskApp().TaskParentTest();

            //FileStream fs = new FileStream(@"D:\cw\测试文件\孙鑫C++教程06.rmvb", FileMode.Open, FileAccess.Read);

            //int i = 0;
            //while (true)
            //{
            //    ulong allSize = GetFreeSpace(@"D:\cw\测试文件");
            //    if (allSize < int.MaxValue) break;
            //    FileStream fs1 = new FileStream(@"D:\cw\孙鑫C++教程06_" + i + ".rmvb", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //    fs1.SetLength(int.MaxValue);
            //    i++;
            //}
            //byte[] fileTempBuffer = new byte[1024 * 64];
            //int bytesRead = 0;
            //do
            //{
            //    bytesRead = fs.Read(fileTempBuffer, 0, fileTempBuffer.Length);
            //    fs1.Write(fileTempBuffer, 0, bytesRead);
            //}
            //while (bytesRead > 0);
            //fs.Position = 0;
            //fs1.Position = 0;
            //System.Console.WriteLine(Md5Helper.GetMd5(fs, false));
            //System.Console.WriteLine(Md5Helper.GetMd5(fs1, false));


            //ulong allSize1 = GetFreeSpace(@"D:\cw\测试文件");

            //System.Console.WriteLine(allSize1);

            //string pattern = @"(?<=\[Flag=")(\w{1})(?=\"])";
            //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
            //Match m = regex.Match("已排重[Flag="T"]");

            //XmlTest.Test();

            //StringBuilder sb = new StringBuilder();
            //sb.Append("中日新感觉派比较研究，实际上是由三方参加在场的对话，除了中日新感觉派两方外，还有论文作者一方。不言而喻，在这场对话中，成为“共同语言”的只能是各方所拥有的艺术感觉。一般说来，中日新感觉派艺术感觉的共同特征，表现在这几方面：一、物人合一，二、感觉变形；三、\0");
            //sb.Append(@"#IF(#LENGTH("正文快照")>238)... #ENDIF</div>");

            //System.Console.WriteLine(sb.ToString());

            //TimerTest.Go();

            //System.Console.WriteLine("当前CPU亲和力: {0}", Process.GetCurrentProcess().ProcessorAffinity);
            //Process.GetCurrentProcess().ProcessorAffinity = (System.IntPtr)2;
            //System.Console.WriteLine("当前CPU亲和力: {0}", Process.GetCurrentProcess().ProcessorAffinity);


            //object target = "w.chen";
            //object arg = "ff";
            //Type[] argTypes = new Type[] { arg.GetType() };
            //MethodInfo method = target.GetType().GetMethod("Contains", argTypes);
            //object[] param = new object[] { arg };
            //bool result = Convert.ToBoolean(method.Invoke(target, param));
            //System.Console.WriteLine(result);


            //dynamic target1 = "w.chen";
            //dynamic arg1 = "ff";

            //result = target1.Contains(arg1);
            //System.Console.WriteLine(result);

            //// 创建邮件管理器
            //MailManager mailManager = new MailManager();
            //// 给传真添加邮件事件关注
            //Fax fax = new Fax(mailManager);
            //Pager pager = new Pager(mailManager);
            //// 接受邮件动作
            //mailManager.SimulateNewMail("cwzyj157@sina.com", "w.chen@sunlike.com.cn", "测试");
            //// 注销事件关注
            //fax.UnRegister(mailManager);
            //pager.UnRegister(mailManager);


            //TaskSchedulerTest.Use();

            //UnsafeTest.Test();

            //ScriptType st = ScriptType.Sql;
            //ScriptBase sb;
            //ScriptEventArgs eventArgs;

            //if (st == ScriptType.Web)
            //{
            //    sb = new WebScriptDetection();
            //    eventArgs = new ScriptEventArgs("W_mobile", "W_email", "W_rem");
            //}
            //else if (st == ScriptType.Sql)
            //{
            //    sb = new SqlScriptDetection();
            //    eventArgs = new ScriptEventArgs("S_mobile", "S_email", "S_rem");
            //}
            //else
            //{
            //    sb = new OtherScriptDetection();
            //    eventArgs = new ScriptEventArgs("O_mobile", "O_email", "O_rem");
            //}

            //sb.OnDetection(eventArgs);

            //IEnumerable<string> str = new List<string>();
            //str.Concat(new string[] { "1", "2", "3" });
            //System.Console.WriteLine(str);

            //NullVaribleTest();


            //ObserveGc();


            //ReflectionTest();


            //OrderBizModuleType.KC.GetDescribe();


            var a = "{" + string.Format("\"fail\":\"true\",\"errorMsg\":\"{0}\"", "123") + "}";

            //ComputeBoundAsyncInvokeTest();

            //RssChannelHelperTest();

            System.Console.ReadLine();
        }

        [DllImport("kernel32.dll")]
        private static extern bool GetDiskFreeSpaceEx(
            string lpDirectoryName, out ulong lpFreeBytesAvailable, out ulong lpTotalNumberOfBytes, out ulong lpTotalNumberOfFreeBytes);

        /// <summary>
        /// 取得磁盘剩余空间
        /// </summary>
        /// <param name="driveDirectoryName">驱动器名</param>
        /// <returns>剩余空间</returns>
        private static ulong GetFreeSpace(string path)
        {
            ulong freeBytesAvailable, totalNumberOfBytes, totalNumberOfFreeBytes;

            string driveName = GetDrive(path);

            GetDiskFreeSpaceEx(driveName, out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes);
            return freeBytesAvailable;
        }
        private static string GetDrive(string path)
        {
            return path.Substring(0, path.IndexOf(":\\") + 2);
        }

        private const int BUFFER_SIZE = 64 * 1024;
        /// <summary>
        /// 从本地读取文件流
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件流</returns>
        public static MemoryStream BackUpSteam(Stream stream)
        {
            if (stream == null || !stream.CanRead)
                return null;

            MemoryStream ms = new MemoryStream((int)stream.Length);
            byte[] fileBuffer = new byte[BUFFER_SIZE];
            int bytesRead = 0;
            do
            {
                bytesRead = stream.Read(fileBuffer, 0, fileBuffer.Length);
                ms.Write(fileBuffer, 0, bytesRead);
            }
            while (bytesRead > 0);
            //stream.Close();
            ms.Position = 0;
            System.Console.WriteLine(Md5Helper.GetMd5(ms, false));

            return ms;
        }

        /// <summary>
        /// 从本地读取文件流
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件流</returns>
        public static MemoryStream BackUpSteam4(Stream stream)
        {
            if (stream == null || !stream.CanRead)
                return null;

            MemoryStream ms = new MemoryStream();
            byte[] fileBuffer = new byte[BUFFER_SIZE];
            int bytesRead = 0;
            do
            {
                bytesRead = stream.Read(fileBuffer, 0, fileBuffer.Length);
                ms.Write(fileBuffer, 0, bytesRead);
            }
            while (bytesRead > 0);
            //stream.Close();
            ms.Position = 0;
            System.Console.WriteLine(Md5Helper.GetMd5(ms, false));

            return ms;
        }

        /// <summary>
        /// 从本地读取文件流
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件流</returns>
        public static MemoryStream BackUpSteam1(Stream stream)
        {
            if (stream == null || !stream.CanRead)
                return null;


            byte[] fileBuffer = new byte[stream.Length];

            int length = stream.Read(fileBuffer, 0, fileBuffer.Length);

            MemoryStream ms = new MemoryStream(fileBuffer);
            ms.Position = 0;
            System.Console.WriteLine(Md5Helper.GetMd5(ms, false));
            return ms;
        }

        /// <summary>
        /// 从本地读取文件流
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件流</returns>
        public static MemoryStream BackUpSteam2(Stream stream)
        {
            if (stream == null || !stream.CanRead)
                return null;

            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            ms.Position = 0;
            System.Console.WriteLine(Md5Helper.GetMd5(ms, false));
            return ms;
        }

        /// <summary>
        /// 从本地读取文件流
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件流</returns>
        public static MemoryStream BackUpSteam3(Stream stream)
        {
            if (stream == null || !stream.CanRead)
                return null;

            MemoryStream ms = new MemoryStream((int)stream.Length);
            stream.CopyTo(ms);
            ms.Position = 0;
            System.Console.WriteLine(Md5Helper.GetMd5(ms, false));
            return ms;
        }


        static void ExtensionTest()
        {
            #region url字符串参数处理
            UrlParams urlParam = new UrlParams("http://192.168.100.152/Grid2008/_OrganizationLibrary/TemplateIndex/M031001/Index.aspx?t=&targetname=xbdx&a=1");
            System.Console.WriteLine("targetname=" + urlParam.Params["p"]);
            #endregion

            #region 全角半角
            string str = "工和　Ｒ　ＤＤＳＩＪ＃￥……";
            str = str.ToDBC();

            str = "林圾 #￥%&……%^%&（）";
            str = str.ToSBC();
            #endregion

            #region 键值对
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.TryAdd("aaaa", 0)
                .TryAdd("bbbb", 1)
                .TryAdd("cccc", 2)
                .AddOrReplace("bbbb", 4);

            Dictionary<string, int> dict2 = new Dictionary<string, int>();
            dict2.TryAdd("dddd", 3)
                 .AddRange(dict, false);



            foreach (KeyValuePair<string, int> kvp in dict2)
            {
                System.Console.WriteLine("Key----Value:{0}----{1}", kvp.Key.ToString(), kvp.Value.ToString());
            }

            #endregion

        }

        static void TestAAA(params string[] ary)
        {
            if (ary != null && ary.Length > 0)
            {
                ary.Join("#");
            }
        }
        static void TestBBB(string[] ary)
        {
            if (ary != null && ary.Length > 0)
            {
                ary.Join("#");
            }
        }

        static void TestWcfService()
        {
            string path = @"C:\Users\w.chen\Desktop\临时文件";


            //string path = @"D:\cw";


            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] fiAry = di.GetFiles();

            //Dictionary<string, Stream> dict = new Dictionary<string, Stream>();

            //foreach (FileInfo fi in fiAry)
            //{
            //    string fileName = fi.Name;
            //    Stream inputStream = fi.OpenRead();
            //    dict.Add(fileName, inputStream);
            //}

            foreach (FileInfo fi in fiAry)
            {
                Stream stream = fi.OpenRead();
                Action<string, Stream> action = (m, n) =>
                {
                    UploadData(m, n);
                    n.Close();
                    n = null;
                };
                action.BeginInvoke(fi.FullName, stream, null, null);
            }
            int i = 0;
            while (i < 60)
            {
                Thread.Sleep(1000);
                GC.Collect();
                i++;
            }

        }

        public static void UploadData(string fileName, Stream inputStream)
        {
            try
            {
                IFileUpload client = ServiceProxyFactory<IFileUpload>.CreateProxy("http://192.168.21.107:8057/NetDrive/FileUpload/FileUpload");
                UploadInMsg info = new UploadInMsg();
                info.BID = "ZK-100";
                info.FileName = fileName;
                info.ticket = "5e8b1cm8#%";
                info.FileData = inputStream;
                UploadRetMsg result;
                result = client.UploadFile(info);

                info.FileData = null;
                info = null;

                //if (result.HasError)
                //{
                //    System.Console.WriteLine("上传文件上传失败：" + fileName);
                //}
                //else
                //{
                //    System.Console.WriteLine("上传文件上传成功：" + result.FileCode);
                //}
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("上传文件上传失败：" + fileName + "ex:" + ex.Message);
            }
        }



        /// 从缓存中取出值
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>结果</returns>
        private static T GetObjectFromApplication<T>(string key)
        {
            // 取缓存
            object objCache = null;
            return (T)objCache;
        }
    }

    public class XmlTest
    {
        private static DOMDocument40 domDoc;

        public static void Test()
        {

            string zoneIdList = @"scdb_txt,scdb_hidden,scdb_sub,scdb_0_hidden
,scdb_0_txt
,scdb_0_sub
,scdb_1_hidden
,ref
,scdb_1_txt
,scdb_1_sub
,scdb_2_hidden
,scdb_ref
,scdb_2_txt
,scdb_2_sub
,scdb_3_hidden
,scdb_0_ref
,scdb_3_txt
,scdb_3_sub
,scdb_1_ref
,scdb_4_txt
,scdb_4_hidden
,scdb_5_txt
,scdb_4_sub
,callpaper
,scdb_2_ref
,scdb_6_txt
,scdb_5_sub
,scdb_3_ref
,scdb_7_txt
,scdb_6_sub
,scdb_lbl1
,scdb_5_hidden
,scdb_4_ref
,scdb_8_txt
,scdb_7_sub
,scdb_5_ref
,scdb_9_txt
,scdb_8_sub
,scdb_6_hidden
,scdb_10_txt
,scdb_9_sub
,scdb_7_hidden
,scdb_6_ref
,scdb_11_txt
,scdb_10_sub
,scdb_8_hidden
,scdb_7_ref
,scdb_12_txt
,scdb_9_hidden
,scdb_8_ref
,scdb_13_txt
,scdb_11_sub";

            List<string> list = zoneIdList.Replace("\r\n", "").Split(',').ToList();


            Dictionary<string, string> dictZoneList1 = new Dictionary<string, string>();
            Dictionary<string, string> dictZoneList2 = new Dictionary<string, string>();
            Dictionary<string, string> dictZoneList3 = new Dictionary<string, string>();

            string zoneId = string.Empty;
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            XmlDocument doc = new XmlDocHelper(@"D:\search\SCDB.xml").GetXmlDocument();
            sw2.Stop();
            System.Console.WriteLine("\tTime Elapsed:\t" + sw2.ElapsedMilliseconds.ToString("N0") + "ms");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            // 分单个读
            foreach (string str in list)
            {
                XmlNode xn = doc.SelectSingleNode("/DataConfiger/zone[@id=" + str + "]");
                if (xn != null)
                {
                    zoneId = xn.Attributes["id"].Value;
                    if (!dictZoneList1.ContainsKey(zoneId))
                        dictZoneList1.Add(zoneId, xn.InnerText);
                }
            }
            sw.Stop();
            System.Console.WriteLine("\tTime Elapsed:\t" + sw.ElapsedMilliseconds.ToString("N0") + "ms");

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            // 一次性读
            XmlNodeList nodeList = doc.SelectNodes("/DataConfiger/zone");

            foreach (XmlNode xn in nodeList)
            {
                zoneId = xn.Attributes["id"].Value;
                if (list.Contains(zoneId) && !dictZoneList2.ContainsKey(zoneId))
                    dictZoneList2.Add(zoneId, xn.InnerText);
            }
            sw1.Stop();
            System.Console.WriteLine("\tTime Elapsed:\t" + sw1.ElapsedMilliseconds.ToString("N0") + "ms");



            Stopwatch sw3 = new Stopwatch();
            sw3.Start();

            XmlTextReader xmlReader = new XmlTextReader(@"D:\search\SCDB.xml");
            xmlReader.WhitespaceHandling = WhitespaceHandling.None;
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "zone")
                {
                    if (xmlReader.MoveToAttribute("id"))
                    {
                        zoneId = xmlReader.Value;
                    }
                }
                if (xmlReader.NodeType == XmlNodeType.CDATA)
                {
                    if (list.Contains(zoneId) && !dictZoneList3.ContainsKey(zoneId))
                        dictZoneList3.Add(zoneId, xmlReader.Value);
                }
            }
            sw3.Stop();
            System.Console.WriteLine("\tTime Elapsed:\t" + sw3.ElapsedMilliseconds.ToString("N0") + "ms");


            #region MSXML

            //Stopwatch sw4 = new Stopwatch();
            //sw4.Start();
            //domDoc = new DOMDocument40();
            //domDoc.load(@"D:\search\SCDB.xml");
            //sw4.Stop();
            //System.Console.WriteLine("\tTime Elapsed:\t" + sw4.ElapsedMilliseconds.ToString("N0") + "ms");

            //Stopwatch sw5 = new Stopwatch();
            //sw5.Start();
            //IXMLDOMNodeList domList = domDoc.selectNodes("/DataConfiger/zone");

            //Dictionary<string, string> dictZoneList4 = new Dictionary<string, string>();

            //IXMLDOMNode node = domList.nextNode();
            //while (node != null)
            //{
            //    if (node.nodeType == DOMNodeType.NODE_ELEMENT && node.nodeName == "zone")
            //    {
            //        zoneId = node.attributes.getNamedItem("id").text;
            //        if (list.Contains(zoneId) && !dictZoneList4.ContainsKey(zoneId))
            //        {
            //            dictZoneList4.Add(zoneId, node.text);
            //        }
            //    }
            //    node = domList.nextNode();
            //}
            //sw5.Stop();
            //System.Console.WriteLine("\tTime Elapsed:\t" + sw5.ElapsedMilliseconds.ToString("N0") + "ms");

            #endregion
        }

        public static void EnlargeXml()
        {
            XmlDocument doc = new XmlDocHelper(@"D:\search\SCDB.xml").GetXmlDocument();

            for (int i = 0, length = doc.DocumentElement.ChildNodes.Count; i < length; i++)
            {
                XmlNode xn = doc.DocumentElement.ChildNodes[i];

                if (xn.NodeType == XmlNodeType.Element)
                {
                    xn = xn.CloneNode(true); ;
                    xn.Attributes["id"].Value = xn.Attributes["id"].Value + "_0";
                }
                doc.DocumentElement.AppendChild(doc.ImportNode(xn, true));
            }
            doc.Save(@"D:\search\SCDB.xml");

        }
    }

    /// <summary>
    /// 读取XML文档辅助类
    /// </summary>
    public class XmlDocHelper
    {
        private string xmlPath;
        public XmlDocHelper(string xmlPath)
        {
            this.xmlPath = xmlPath;
        }

        public XmlDocument GetXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(xmlPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            return doc;
        }
    }


    public class TimerTest
    {
        private static Timer altTimer;
        public static void Go()
        {
            System.Console.WriteLine("Main thread:starting a timer");

            altTimer = new Timer(new TimerCallback(ComputeBoundOp), null, 0, Timeout.Infinite);
            System.Console.WriteLine("Main thread:Doing other work here...");

        }

        private static void ComputeBoundOp(object state)
        {
            //altTimer.Change(Timeout.Infinite, Timeout.Infinite);
            System.Console.WriteLine("In ComputeBoundOp:state={0}", System.DateTime.Now);
            Thread.Sleep(10000);
            altTimer.Change(2000, 2000);
        }
    }



    public class AgrithomTest
    {
        public static void Test()
        {

        }
    }


    public class A
    {
        public A()
        {
            Print();
        }
        public virtual void Print() { }
    }
    public class B : A
    {
        int i = 1;
        int j;
        public B()
        {
            j = -1;
        }
        public override void Print()
        {
            System.Console.WriteLine("i={0},j={1}", i, j);
        }
    }
}
