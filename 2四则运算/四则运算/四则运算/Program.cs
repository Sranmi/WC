using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 四则运算
{
    class Program
    {

        private
        static void Main(string[] args)
        {
            int r = 10;
            int n = 1;
            int[] result = new int[2];
            Console.WriteLine("请输入参数及相应的内容\n-n 输入生成题目的数量\n-r 输入题目中数值的范围");
            string[] nn = Console.ReadLine().Split(' ');
            while (nn[0] != "-n" || nn[0] != "-r")
            {
                Console.WriteLine("参数输入有误，请重新输入");
                nn = Console.ReadLine().Split(' ');
            }
            switch (nn[0])
            {
                case "-n":
                    while (int.TryParse(nn[1], out n) == false)
                    {
                        Console.WriteLine("输入有误，请重新输入数字");
                        nn[1] = Console.ReadLine();
                    }
                    break;
                case "-r":
                    while (int.TryParse(nn[1], out r) == false)
                    {
                        Console.WriteLine("输入有误，请重新输入数字");
                        nn[1] = Console.ReadLine();
                    }
                    break;
            }
            calculation c = new calculation();
            int[][] ic = new int[n][];//n个计算式中计算数的和数
            for (int i = 0; i < n; i++)
            {
                string a = c.GetProblem(r, ref result, ref ic[i]);
                for (int j = 0; j < i; j++)
                {
                    if (ic[j] == ic[i])
                        a = "重新生成";
                }
                while (a == "重新生成")
                {
                    a = c.GetProblem(r, ref result, ref ic[i]);
                    for (int j = 0; j < i; j++)
                    {
                        if (ic[j] == ic[i])
                            a = "重新生成";
                    }
                }
                //将题目存入Exercises.txt中，可追加
                using (StreamWriter sw = new StreamWriter(@"F:\作业\软件工程\2\Exercises.txt", true))
                {
                    sw.WriteLine(i + ".四则运算题目 " + a);
                }
                //将答案存入Answers.txt
                using (StreamWriter sw = new StreamWriter(@"F:\作业\软件工程\2\Answers.txt", true))
                {
                    if (result[0] == 0 || result[1] == 1)
                        sw.WriteLine(i + ".答案 " + result[0].ToString());
                    else
                        sw.WriteLine(i + ".答案 " + result[0].ToString() + "/" + result[1].ToString());
                }
            }
            Console.ReadKey();
        }
    }
}
