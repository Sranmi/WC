using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WC_window_framework
{
    public
    class Function
    {
        /// <summary>
        /// 统计字符数
        /// </summary>
        /// <param name="filename"></param>
        public void CharCount(string filename, ref int charcount,ref string showdata)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            int srchar;
            while ((srchar = sr.Read()) != -1)
            {
                charcount++;
            }
            sr.Close();
            showdata += "字符数：" + charcount.ToString() + "\n";
        }
        /// <summary>
        /// 统计单词数
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="wordcount"></param>
        public void WordCount(string filename, ref int wordcount,ref string showdata)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            char[] symbol = { ' ', ',', '.', '?', '!', ':', ';', '\'', '\"', '\t', '{', '}', '(', ')', '+', '-', '*', '=' };
            int srchar;
            while ((srchar = sr.Read()) != -1)
            {
                foreach (var c in symbol)
                {
                    if (srchar == (int)c)
                    {
                        wordcount++;
                    }
                }
            }
            wordcount++;
            sr.Close();
            showdata += "单词数:" + wordcount.ToString() + "\n";
        }
        /// <summary>
        /// 统计行数
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="linecount"></param>
        public void LineCount(string filename, ref int linecount,ref string showdata)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            int srchar;
            while((srchar =sr.Read ())!=-1)
            {
                if(srchar =='\n')
                {
                    linecount++;
                }
            }
            linecount++;
            sr.Close();
            showdata += "行数：" + linecount.ToString() + "\n";
        }
        /// <summary>
        /// 扩展功能：统计代码行、空行、注释行
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="notelinecount"></param>
        /// <param name="nulllinecount"></param>
        /// <param name="codelinecount"></param>
        public void SuperCount (string filename, ref int notelinecount,ref int nulllinecount,ref int codelinecount,ref string showdata)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            string line;
            while((line =sr.ReadLine ())!=null )
            {
                line = line.Trim(' ');
                line = line.Trim('\t');
                //空行
                if(line ==""||line .Length <=1)//代码中单括号为空行
                {
                    nulllinecount++;
                }
                //注释行
                else if(line.Substring (0,2)=="//"||line .Substring (1,2)=="//")
                {
                    notelinecount++;
                }
                //代码行
                else
                {
                    codelinecount++;
                }
            }
            sr.Close();
            showdata += "代码行：" + codelinecount.ToString() + "\n" + "空行：" + nulllinecount.ToString() + "\n" + "注释行：" + notelinecount.ToString() + "\n";

        }
    }
}
