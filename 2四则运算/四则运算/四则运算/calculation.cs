using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 四则运算
{
    class calculation
    {
        //设aXbYcZd
        private int[][] a;//计算数设为a/b 分子分母分开存储 默认a=0.b=1
        private char[] X;
        private char[] symbol = { '+', '-', '*', '/' };
        public calculation()
        {

        }
        /// <summary>
        /// 生成一个题目并保存至exercises.txt
        /// </summary>
        /// <param name="r">运算数范围</param>
        /// <returns></returns>
        public string GetProblem(int r, ref int[] result,ref int [] ic )
        {
            a = new int[4][] { new int[] { 0, 1 }, new int[] { 0, 1 }, new int[] { 0, 1 }, new int[] { 0, 1 } };
            X = new char[3] { ' ', ' ', ' ' };//默认为空格，方便判断
            //至多有两对括号，且成对出现
            //bool b1 = false, b2 = false;
            //计算式至少有两个运算数，一个运算符，则a，d不为0
            string problem = "";
            //Random rd = new Random(Guid.NewGuid().GetHashCode());
            //int i1 = rd.Next(0, 2);
            //if(i1==1)
            //{
            //    b1 = true;
            //    problem += "(";
            //}
            while (a[0][0] == 0)
            {
                GetNumber(a[0], r, ref problem);
            }
            GetSymbol(a[0][0], ref problem, ref X[0]);
            GetNumber(a[1], r, ref problem);
            GetSymbol(a[1][0], ref problem, ref X[1]);
            GetNumber(a[2], r, ref problem);
            GetSymbol(a[2][0], ref problem, ref X[2]);
            while (a[3][0] == 0)
            {
                GetNumber(a[3], r, ref problem);
            }
            problem += "=";
            ic= IsCommon();
            //Console.WriteLine(problem);
            TreeNode<Number> sym = new TreeNode<Number>();
            GetTree(0, 3, sym);
            LinkBinaryTree<Number> link = new LinkBinaryTree<Number>(sym.Data);
            result = link.GetResult(sym, ref problem);
            return problem;
        }
        /// <summary>
        /// 四个运算数等于随机自然数或真分数
        /// </summary>
        /// <param name="r">数字范围</param>
        /// <param name="pb">运算数输出形式</param>
        /// <returns>运算数计算形式</returns>
        private void GetNumber(int[] a, int r, ref string pb)
        {
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            a[0] = rd.Next(0, r);
            //是否生成真分数
            int c = rd.Next(0, 2);
            //a=0时，不需要写入problem
            if (c == 1 && a[0] != 0)
            {
                //生成分母应大于1
                a[1] = rd.Next(2, r);
                // n += "/" + a[1].ToString();
                if (a[0] >= a[1])
                {
                    if (a[0] % a[1] == 0)
                    {
                        pb += (a[0] / a[1]).ToString() + "  ";
                    }
                    else
                    {
                        int gg = GetCommon(a[0] % a[1], a[1]);
                        pb += (a[0] / a[1]).ToString() + "'" + (a[0] % a[1] / gg).ToString() + "/" + (a[1] / gg).ToString() + "  ";
                    }
                }
                else
                {
                    int g = GetCommon(a[0], a[1]);
                    a[0] /= g;
                    a[1] /= g;
                    pb += a[0].ToString() + "/" + a[1].ToString() + "  ";
                }
            }
            else if (c == 0 && a[0] != 0)
            {
                pb += a[0].ToString() + "  ";
            }
        }
        /// <summary>
        /// 随机取运算符，若a=0，则运算符取+，方便计算结果
        /// </summary>
        /// <param name="a"></param>
        /// <param name="problem"></param>
        /// <param name="X"></param>
        private void GetSymbol(int a, ref string problem, ref char X)
        {
            if (a != 0)
            {
                Random rd = new Random(Guid.NewGuid().GetHashCode());
                X = symbol[rd.Next(0, 4)];
                problem += X.ToString() + "  ";
            }
        }
        /// <summary>
        /// 计算两个数的最大公因子，为分数约分
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int GetCommon(int a, int b)
        {
            int d = 0;
            if (a == 0)
            {
                b = 1;
            }
            else
            {
                while (b % a != 0)
                {

                    d = b % a;
                    b = a;
                    a = d;
                }
                b = a;
            }
            return b;
        }
        /// <summary>
        /// 获取运算符等级
        /// </summary>
        /// <param name="c">当前字符</param>
        /// <returns></returns>
        private static int GetOperationLevel(char c)
        {
            switch (c)
            {
                case '+': return 1;
                case '-': return 1;
                case '*': return 2;
                case '/': return 2;
                default: return 3;
            }
        }
        /// <summary>
        /// 获取优先级最小的运算符
        /// </summary>
        /// <returns></returns>
        private int GetMin(int s, int e, ref int j)
        {
            int min = s;

            for (int i = s; i < e; i++)
            {
                if (X[i] != ' ')
                {
                    j++;
                    if (GetOperationLevel(X[i]) <= GetOperationLevel(X[min]))
                        min = i;
                }
            }
            //运算符空，结束遍历
            return min;
        }
        private void GetTree(int s, int e, TreeNode<Number> sym)
        {
            int j = 0;
            int min = GetMin(s, e, ref j);
            if (j == 0)
            {
                sym.Data = new Number(a[e][0], a[e][1], ' ');
                // Console.WriteLine(sym.Data.Son.ToString() + "/" + sym.Data.Mother.ToString()+sym.Data.Symbol.ToString());
            }
            else
            {
                if (min != -1)
                {
                    sym.Data = new Number(0, 0, X[min]);
                    //   Console.WriteLine(sym.Data.Son.ToString() + "/" + sym.Data.Mother.ToString() + sym.Data.Symbol.ToString());
                    X[min] = ' ';
                    TreeNode<Number> left = new TreeNode<Number>();
                    GetTree(s, min, left);
                    sym.LChild = left;
                    TreeNode<Number> right = new TreeNode<Number>();
                    GetTree(min + 1, e, right);
                    sym.RChild = right;
                }
            }
        }

        /// <summary>
        /// 将计算数相加，与其他计算式的相加值一样时舍去
        /// </summary>
        /// <returns></returns>
        private int[] IsCommon()
        {
            string s = "";
            LinkBinaryTree<Number> ic = new LinkBinaryTree<Number>();
            int[] c = ic.Arithmetic(a[0], a[1], '+', ref s);
            c = ic.Arithmetic(c, a[2], '+', ref s);
            c= ic.Arithmetic(c, a[3], '+', ref s);
            return c;
        }
    }
}



