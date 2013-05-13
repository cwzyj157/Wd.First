using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OtherTestProject.Console
{
    public class SqlRemoveRepeat
    {
        public void Test1(string input)
        {
            //(?<=^\[length=)(\d+)(?=\])
            //string pattern = @"(?:\(.*?\))";
            //Regex regex = new Regex(pattern);

            //input = "(((aaaa)))";

            //MatchCollection mc = regex.Matches(input);


            //foreach (Match m in mc)
            //{
            //    System.Console.WriteLine(m.Value);
            //}

            List<StateMetaInfo> listMetaData = new List<StateMetaInfo>();
            Stack<string> stack = new Stack<string>();

            DealString(input, listMetaData, stack);

            int level = listMetaData.Max(s => s.level);

            listMetaData = listMetaData.Where(s => s.level == level).OrderBy(s => s.lastIndex).ToList();


            System.Console.WriteLine(level);

            System.Console.ReadLine();

        }

        public void DealList(StateMetaInfo leftMetaData, StateMetaInfo rightMetaData)
        {
            if (leftMetaData.content == rightMetaData.content)
            {
                rightMetaData = null;
            }
        }


        //public void DealString(string input, List<StateMetaInfo> listMetaData, Stack<string> stack)
        //{
        //    //去头尾空格
        //    input = input.Trim();

        //    int i = 0;
        //    int tempIndex = 0;
        //    string tempStr = "";
        //    string tempStackStr = "";
        //    bool isInit = false;
        //    while (i < input.Length)
        //    {
        //        if (input[i] == ' ')
        //        {
        //            i++;
        //        }
        //        else if (input[i] == '(')
        //        {
        //            stack.Push(input[i].ToString());
        //            i++;
        //        }
        //        else if (input[i] == ')')
        //        {
        //            StateMetaInfo includeMetaInfo = listMetaData.Count > 0 && !isInit ? listMetaData[listMetaData.Count - 1] : null;

        //            tempStackStr = input[i].ToString();
        //            tempStackStr = stack.Pop() + tempStackStr;
        //            tempStackStr = (listMetaData.Count > 0 && !isInit ?
        //                            listMetaData[listMetaData.Count - 1].content : "") + tempStackStr;
        //            tempStackStr = stack.Pop() + tempStackStr;

        //            listMetaData.Add(new StateMetaInfo()
        //            {
        //                content = tempStackStr,
        //                includeMetaInfo = includeMetaInfo,
        //                level = includeMetaInfo == null ? 1 : includeMetaInfo.level + 1,
        //                lastIndex = i
        //            });

        //            i++;
        //            isInit = false;
        //            if (stack.Count == 0 || stack.Peek() != "(")
        //            {
        //                tempIndex = input.IndexOf("(", i);
        //                if (tempIndex > 0)
        //                {
        //                    tempStr = input.Substring(i, tempIndex - i).Trim();
        //                    stack.Push(tempStr);
        //                    i = tempIndex;
        //                    isInit = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            tempIndex = input.IndexOf(")", i);
        //            tempStr = input.Substring(i, tempIndex - i).Trim();


        //            int existXls = tempStr.IndexOf("XLS(");
        //            while (existXls > 0)
        //            {
        //                tempIndex = input.IndexOf(")", tempIndex + 1);
        //                tempStr = input.Substring(i, tempIndex - i).Trim();
        //                existXls = tempStr.IndexOf("XLS(", existXls + 1);
        //            }


        //            stack.Push(tempStr);
        //            i = tempIndex;
        //        }
        //    }
        //}

        public void DealString(string input, List<StateMetaInfo> listMetaData, Stack<string> stack)
        {
            // 入栈直到括号互相匹配，此时写入一个最大等级，原后出栈，递归实现对子串的匹配

            input = input.Trim();
            if (input[0] == '(')//以左括号开头
            {
                int i = 0;

                int leftBracketCount = 0;
                int rightBracketCount = 0;

                int leftBracketIndex = -1;


                while (i < input.Length)
                {
                    if (input[i] == '(')
                    {
                        if (leftBracketIndex == -1)
                            leftBracketIndex = i;

                        leftBracketCount++;
                    }
                    else if (input[i] == ')')
                    {
                        rightBracketCount++;
                    }
                    if (leftBracketCount != 0 && leftBracketCount == rightBracketCount)
                    {
                        string content = input.Substring(leftBracketIndex, i - leftBracketIndex + 1).Trim();

                        StateMetaInfo metaInfo = null;
                        //listMetaData.Count > 0
                        //    ? listMetaData.Single(s => s.content.IndexOf(input) > 0 && s.lastIndex > i)
                        //    : null;

                        listMetaData.Add(new StateMetaInfo()
                            {
                                content = content,
                                level = metaInfo == null ? 0 : metaInfo.level + 1,
                                includeMetaInfo = metaInfo,
                                lastIndex = i
                            });
                        //DealString(content.Substring(1, content.Length - 2), listMetaData, stack);


                        // 处理右侧
                        int k = input.IndexOf("(", i);
                        if (k > 0)
                        {
                            stack.Push(input.Substring(i + 1, k - i - 1).Trim());



                            DealString(input.Substring(k), listMetaData, stack);
                            i = k;
                        }
                        else
                        {
                            break;
                        }
                        leftBracketCount = rightBracketCount = 0;
                    }
                    i++;
                }
            }
        }

    }
    public class StateMetaInfo
    {
        public string content;
        public int level;
        public int lastIndex;
        public StateMetaInfo includeMetaInfo;

    }
}
