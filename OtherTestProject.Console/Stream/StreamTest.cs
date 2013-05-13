using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using Wd.Common;
using Wd.Extensions;

namespace OtherTestProject.Console
{
    class StreamTest
    {
        public static void Test()
        {
            byte[] buffer = null;

            string testString = "Stream!Hello world";
            char[] readCharArray = null;
            byte[] readBuffer = null;
            string readString = string.Empty;
            //关于MemoryStream 我会在后续章节详细阐述
            using (MemoryStream stream = new MemoryStream())
            {
                System.Console.WriteLine("初始字符串为：{0}", testString);
                //如果该流可写
                if (stream.CanWrite)
                {
                    //首先我们尝试将testString写入流中
                    //关于Encoding我会在另一篇文章中详细说明，暂且通过它实现string->byte[]的转换
                    buffer = Encoding.Default.GetBytes(testString);
                    //我们从该数组的第一个位置开始写，长度为3，写完之后 stream中便有了数据
                    //对于新手来说很难理解的就是数据是什么时候写入到流中，在冗长的项目代码面前，我碰见过很
                    //多新手都会有这种经历，我希望能够用如此简单的代码让新手或者老手们在温故下基础
                    stream.Write(buffer, 0, 3);

                    System.Console.WriteLine("现在Stream.Postion在第{0}位置", stream.Position + 1);

                    //从刚才结束的位置（当前位置）往后移3位，到第7位
                    long newPositionInStream = stream.CanSeek ? stream.Seek(3, SeekOrigin.Current) : 0;

                    System.Console.WriteLine("重新定位后Stream.Postion在第{0}位置", newPositionInStream + 1);
                    if (newPositionInStream < buffer.Length)
                    {
                        //将从新位置（第7位）一直写到buffer的末尾，注意下stream已经写入了3个数据“Str”
                        stream.Write(buffer, (int)newPositionInStream, buffer.Length - (int)newPositionInStream);
                    }


                    //写完后将stream的Position属性设置成0，开始读流中的数据
                    stream.Position = 0;

                    // 设置一个空的盒子来接收流中的数据，长度根据stream的长度来决定
                    readBuffer = new byte[stream.Length];


                    //设置stream总的读取数量 ，
                    //注意！这时候流已经把数据读到了readBuffer中
                    int count = stream.CanRead ? stream.Read(readBuffer, 0, readBuffer.Length) : 0;


                    //由于刚开始时我们使用加密Encoding的方式,所以我们必须解密将readBuffer转化成Char数组，这样才能重新拼接成string

                    //首先通过流读出的readBuffer的数据求出从相应Char的数量
                    int charCount = Encoding.Default.GetCharCount(readBuffer, 0, count);
                    //通过该Char的数量 设定一个新的readCharArray数组
                    readCharArray = new char[charCount];
                    //Encoding 类的强悍之处就是不仅包含加密的方法，甚至将解密者都能创建出来（GetDecoder()），
                    //解密者便会将readCharArray填充（通过GetChars方法，把readBuffer 逐个转化将byte转化成char，并且按一致顺序填充到readCharArray中）
                    Encoding.Default.GetDecoder().GetChars(readBuffer, 0, count, readCharArray, 0);
                    for (int i = 0; i < readCharArray.Length; i++)
                    {
                        readString += readCharArray[i];
                    }
                    System.Console.WriteLine("读取的字符串为：{0}", readString);
                }

                stream.Close();
            }

            System.Console.ReadLine();
        }

        public static void Test2()
        {
            string text = "abc" + Environment.NewLine + "def" + Environment.NewLine + "ghi"
                + Environment.NewLine + "jkl" + Environment.NewLine + "mno" + Environment.NewLine + "pqr"
                + Environment.NewLine + "stu" + Environment.NewLine + "vwx" + Environment.NewLine + "yz";

            //using (TextReader reader = new StringReader(text))
            //{
            //    while (reader.Peek() != -1)
            //    {
            //        System.Console.WriteLine("Peek = {0}", (char)reader.Peek());
            //        System.Console.WriteLine("Read = {0}", (char)reader.Read());

            //    }
            //    reader.Close();
            //}

            //using (TextReader reader = new StringReader(text))
            //{
            //    char[] charBuffer = new char[3];
            //    int data = reader.ReadBlock(charBuffer, 0, 3);
            //    for (int i = 0; i < charBuffer.Length; i++)
            //    {
            //        System.Console.WriteLine("通过readBlock读出的数据：{0}", charBuffer[i]);
            //    }
            //    reader.Close();
            //}

            using (TextReader reader = new StringReader(text))
            {
                char[] charBuffer = new char[10];
                while (reader.Peek() != -1)
                {
                    int data = reader.ReadBlock(charBuffer, 0, 10);
                    System.Console.WriteLine("Block:" + charBuffer.Join(""));
                }
                reader.Close();
            }

            using (TextReader reader = new StringReader(text))
            {
                string lineData = reader.ReadLine();
                System.Console.WriteLine("第一行的数据为:{0}", lineData);
                reader.Close();
            }

            using (TextReader reader = new StringReader(text))
            {
                string allData = reader.ReadToEnd();
                System.Console.WriteLine("全部的数据为:{0}", allData);
                reader.Close();
            }

            System.Console.ReadLine();
        }

        public static void Test3()
        {
            FileStream fs = new FileStream(@"D:\cw\测试文件\UML参考手册.doc", FileMode.Open, FileAccess.Read);

            System.Console.ReadLine();

            File.Delete(@"D:\cw\测试文件\UML参考手册.doc");
        }
        public static void Test4()
        {
            #region
            //UpFileSingleTest test = new UpFileSingleTest();
            //FileInfo info = new FileInfo(@"D:\cw\测试文件\UML参考手册.doc");
            ////取得文件总长度
            //var fileLegth = info.Length;
            ////假设将文件切成5段
            //var divide = 5;
            ////取到每个文件段的长度
            //var perFileLengh = (int)fileLegth / divide;
            ////表示最后剩下的文件段长度比perFileLengh小
            //var restCount = (int)fileLegth % divide;
            ////循环上传数据
            //for (int i = 0; i < divide + 1; i++)
            //{
            //    //每次定义不同的数据段,假设数据长度是500，那么每段的开始位置都是i*perFileLength
            //    var startPosition = i * perFileLengh;
            //    //取得每次数据段的数据量
            //    var totalCount = fileLegth - perFileLengh * i > perFileLengh ? perFileLengh : (int)(fileLegth - perFileLengh * i);
            //    //上传该段数据
            //    test.UpLoadFileFromLocal(@"D:\cw\测试文件\UML参考手册.doc", @"D:\cw\测试文件\UML参考手册1.doc", startPosition, i == divide ? divide : totalCount);
            //}

            //string fileMd5 = Md5Helper.GetMd5(@"D:\cw\测试文件\UML参考手册.doc");

            //string descFileMd5 = Md5Helper.GetMd5(@"D:\cw\测试文件\UML参考手册1.doc");
            //if (fileMd5 == descFileMd5)
            //{
            //    System.Console.WriteLine("文件成功拷备");
            //}

            //FileStream fs = new FileStream(@"D:\cw\测试文件\UML参考手册.doc", FileMode.Open, FileAccess.Read);
            //FileStream fs1 = new FileStream(@"D:\cw\测试文件\UML参考手册1.doc", FileMode.Open, FileAccess.Read);
            //if (fs == fs1)
            //{
            //    System.Console.WriteLine("文件成功拷备");
            //}
            #endregion

            UpFileSingleTest test = new UpFileSingleTest();
            FileInfo info = new FileInfo(@"D:\cw\测试文件\硬盘更新数据文件目录结构20100831(源文 封面)doc (4).doc");

            int perSize = 1024 * 1;
            long leftSize = info.Length;
            int interations = (int)(info.Length / perSize) + (info.Length % perSize > 0 ? 1 : 0);

            //循环上传数据
            for (int i = 0; i < interations; i++)
            {
                int startPosition = perSize * i;

                //上传该段数据
                test.UpLoadFileBySlice(@"D:\cw\测试文件\硬盘更新数据文件目录结构20100831(源文 封面)doc (4).doc", @"D:\cw\测试文件\硬盘更新数据文件目录结构20100831(源文 封面)doc (4)1.doc",
                    startPosition,
                   (int)(leftSize > perSize ? perSize : leftSize));

                System.Console.WriteLine(test.bytesCount);

                leftSize -= perSize;
            }


            if (Md5Helper.GetMd5(@"D:\cw\测试文件\硬盘更新数据文件目录结构20100831(源文 封面)doc (4).doc") == Md5Helper.GetMd5(@"D:\cw\测试文件\硬盘更新数据文件目录结构20100831(源文 封面)doc (4)1.doc"))
            {
                System.Console.WriteLine("文件成功拷备");
            }

            FileStream fs = new FileStream(@"D:\cw\测试文件\硬盘更新数据文件目录结构20100831(源文 封面)doc (4).doc", FileMode.Open, FileAccess.Read);
            FileStream fs1 = new FileStream(@"D:\cw\测试文件\硬盘更新数据文件目录结构20100831(源文 封面)doc (4)1.doc", FileMode.Open, FileAccess.Read);

        }
        public static void Test5()
        {
            UpFileSingleTest test = new UpFileSingleTest();
            FileInfo info = new FileInfo(@"D:\cw\测试文件\UML参考手册.doc");

            int perSize = 1024 * 1024 / 2;
            long leftSize = 0;
            //循环上传数据
            for (int i = 0, count = (int)(info.Length / perSize); i < count; i++)
            {
                int startPosition = perSize * i;

                //上传该段数据
                test.UpLoadFileBySlice(@"D:\cw\测试文件\UML参考手册.doc", @"D:\cw\测试文件\UML参考手册1.doc",
                    startPosition,
                   (int)(leftSize > perSize ? perSize : leftSize));

                leftSize = info.Length - perSize;
            }
            if (Md5Helper.GetMd5(@"D:\cw\测试文件\UML参考手册.doc") == Md5Helper.GetMd5(@"D:\cw\测试文件\UML参考手册1.doc"))
            {
                System.Console.WriteLine("文件成功拷备");
            }
        }
    }

    /// <summary>
    /// 分段上传例子
    /// </summary>
    public class UpFileSingleTest
    {
        //我们定义Buffer为1000
        public const int BUFFER_COUNT = 1024 /2;

        public long bytesCount;
        /// <summary>
        /// 将文件上传至服务器（本地），由于采取分段传输所以，
        /// 每段必须有一个起始位置和相对应该数据段的数据
        /// </summary>
        /// <param name="filePath">服务器上文件地址</param>
        /// <param name="startPositon">分段起始位置</param>
        /// <param name="btArray">每段的数据</param>
        private void WriteToServer(string filePath, int startPositon, byte[] btArray)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using (fileStream)
            {
                fileStream.Position = startPositon;
                //将该段数据通过FileStream写入文件中，每次写一段的数据，就好比是个水池，分段蓄水一样，直到蓄满为止
                fileStream.Write(btArray, 0, btArray.Length);
                //将流的位置设置在该段起始位置
            }
            bytesCount += btArray.Length;
        }


        /// <summary>
        /// 处理单独一段本地数据上传至服务器的逻辑，根据客户端传入的startPostion
        /// 和totalCount来处理相应段的数据上传至服务器（本地）
        /// </summary>
        /// <param name="localFilePath">本地需要上传的文件地址</param>
        /// <param name="uploadFilePath">服务器（本地）目标地址</param>
        /// <param name="startPostion">该段起始位置</param>
        /// <param name="totalCount">该段最大数据量</param>
        public void UpLoadFileFromLocal(string localFilePath, string uploadFilePath, int startPostion, int totalCount)
        {
            //if(!File.Exists(localFilePath)){return;}
            //每次临时读取数据数
            int tempReadCount = 0;
            int tempBuffer = 0;
            //定义一个缓冲区数组
            byte[] bufferByteArray = new byte[1024 * 1024 / 2];
            //定义一个FileStream对象
            FileStream fileStream = new FileStream(localFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //将流的位置设置在每段数据的初始位置
            fileStream.Position = startPostion;
            using (fileStream)
            {
                //循环将该段数据读出在写入服务器中
                while (tempReadCount < totalCount)
                {
                    tempBuffer = BUFFER_COUNT;
                    //每段起始位置+每次循环读取数据的长度
                    var writeStartPosition = startPostion + tempReadCount;
                    //当缓冲区的数据加上临时读取数大于该段数据量时，
                    //则设置缓冲区的数据为totalCount-tempReadCount 这一段的数据
                    if (tempBuffer + tempReadCount > totalCount)
                    {
                        //缓冲区的数据为totalCount-tempReadCount 
                        tempBuffer = totalCount - tempReadCount;
                        //读取该段数据放入bufferByteArray数组中
                        fileStream.Read(bufferByteArray, 0, tempBuffer);
                        if (tempBuffer > 0)
                        {
                            byte[] newTempBtArray = new byte[tempBuffer];
                            Array.Copy(bufferByteArray, 0, newTempBtArray, 0, tempBuffer);
                            //将缓冲区的数据上传至服务器
                            this.WriteToServer(uploadFilePath, writeStartPosition, newTempBtArray);
                        }

                    }
                    //如果缓冲区的数据量小于该段数据量，并且tempBuffer=设定BUFFER_COUNT时，通过
                    //while 循环每次读取一样的buffer值的数据写入服务器中，直到将该段数据全部处理完毕
                    else if (tempBuffer == BUFFER_COUNT)
                    {
                        fileStream.Read(bufferByteArray, 0, tempBuffer);
                        this.WriteToServer(uploadFilePath, writeStartPosition, bufferByteArray);
                    }

                    //通过每次的缓冲区数据，累计增加临时读取数
                    tempReadCount += tempBuffer;
                }
            }
        }

        // 文件分段上传
        public void UpLoadFileBySlice(string filePath, string descFilePath, int startPosition, int bytesCount)
        {
            byte[] fileTempBuffer = new byte[1024 * 1];
            int bytesRead = 0;
            int length = 0;
            byte[] buffer = null;
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                // 设置文件流读取位置
                fs.Position = startPosition;
                do
                {
                    // 读取文件流
                    bytesRead = fs.Read(fileTempBuffer, 0, fileTempBuffer.Length);
                    buffer = new byte[bytesRead];
                    Array.Copy(fileTempBuffer, buffer, bytesRead);
                    this.WriteToServer(descFilePath, startPosition + length, buffer);

                    length += bytesRead;
                }
                while (bytesRead > 0 && bytesCount > length);
            }
        }
    }
}
