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
        public void CharCount(StreamReader sr, ref int charcount)
        {
            int srchar;
            while ((srchar = sr.Read()) != -1)
            {
                charcount++;
            }
        }
        /// <summary>
        /// 统计单词数
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="wordcount"></param>
        public void WordCount(StreamReader sr, ref int wordcount)
        {
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
        }
        /// <summary>
        /// 统计行数
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="linecount"></param>
        public void LineCount(StreamReader sr, ref int linecount)
        {
            int srchar;
            while((srchar =sr.Read ())!=-1)
            {
                if(srchar =='\n')
                {
                    linecount++;
                }
            }
        }
    }
}
