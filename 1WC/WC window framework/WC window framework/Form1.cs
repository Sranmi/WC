using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WC_window_framework
{
    public partial class Form1 : Form
    {
        private int charcount;
        private int wordcount;
        private int linecount;
        private string filename;
        private string[] parameter;
        private string showdata = null;

        private int codelinecount;
        private int notelinecount;
        private int nulllinecount;

        //private bool iss;
        private string format;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.charcount = 0;
            this.wordcount = 0;
            this.linecount = 0;
            this.codelinecount = 0;
            this.nulllinecount = 0;
            this.notelinecount = 0;
            // this.iss = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Operator();
            if (showdata != null)
                MessageBox.Show(showdata);
            ClearData();

        }
        /// <summary>
        /// 查询总操作流程
        /// </summary>
        private void Operator()
        {
            //读取用户输入的文件名和参数（其中多个参数以空格分开）
            filename = textBox2.Text;
            parameter = textBox1.Text.Split(' ');

            foreach (var s in parameter)
            {
                if (s != "-c" && s != "-w" && s != "-l" && s != "-s" && s != "-a" && s != "-x")
                {
                    MessageBox.Show("参数输入有误，请重新输入，注意多参数之间必须用空格隔开");
                    break;
                }
                //参数为-s时遍历其文件夹下所有符合条件的文件
                //if (s == "-s")
                //{
                //    //读取条件文件格式
                //    foreach (var c in parameter)
                //    {
                //        if (c.Contains("*."))
                //        {
                //            format = c.Replace("*", "");
                //        }
                //    }
                //    //整理输入路径
                //    string[] paths = filename.Split('\\');
                //    int pathsLength = paths.Length;
                //    string path = "";
                //    for (int i = 0; i < pathsLength - 1; i++)
                //    {
                //        paths[i] += '\\';
                //        path += paths[i];
                //    }
                //    string lastname = paths[pathsLength - 1];

                //    int j = 0;
                //    DirectoryInfo dir = new DirectoryInfo(path);
                //    FileSystemInfo[] inf = dir.GetFileSystemInfos();
                    
                //    foreach (FileSystemInfo finf in inf)
                //    {
                //        if(finf is DirectoryInfo)//判断是否为文件夹
                //        {
                //            DirectoryInfo dinf = new DirectoryInfo(finf.FullName);
                //            FileInfo[] f = dinf.GetFiles();
                //            foreach (FileInfo file in f )
                //            {
                //                for (i= 0;i<exception)
                //            }
                //            //扩展名与条件相符
                //            if (finf.Extension.Equals(format))
                    //        //读取文件的完整目录和文件名
                    //        filename = finf.FullName;
                    //    MessageBox.Show(filename );
                    //    if (Directory.Exists(filename))
                    //    {
                    //        showdata += "文件" + j + ":\n";
                    //        Function fc = new Function();
                    //        fc.CharCount(filename, ref charcount, ref showdata);
                    //        fc.WordCount(filename, ref wordcount, ref showdata);
                    //        fc.LineCount(filename, ref linecount, ref showdata);
                    //        fc.SuperCount(filename, ref notelinecount, ref nulllinecount, ref codelinecount, ref showdata);
                    //    }
                    //    j++;
                    //}
                //    break;
               // }
                //参数-x输出图形界面选择文件并读取文件名
                if (s == "-x")
                {
                    OpenFile();
                }
                //判断文件是否存在
                if (File.Exists(filename))
                {
                    Function fc = new Function();
                    if (s == "-c" || s == "-x")
                    {
                        fc.CharCount(filename, ref charcount, ref showdata);
                    }
                    if (s == "-w" || s == "-x")
                    {
                        fc.WordCount(filename, ref wordcount, ref showdata);

                    }
                    if (s == "-l" || s == "-x")
                    {
                        fc.LineCount(filename, ref linecount, ref showdata);

                    }
                    if (s == "-a" || s == "-x")
                    {
                        fc.SuperCount(filename, ref notelinecount, ref nulllinecount, ref codelinecount, ref showdata);
                    }
                }
                else
                {
                    MessageBox.Show("文件名不存在，请重新输入");
                    break;
                }
                //参数为"-x"时只能单独使用，跳出循环
                if (s == "-x")
                    break;
            }
        }
        /// <summary>
        /// 当参数为"-x"或点击三点按钮时打开图形界面
        /// </summary>
        private void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "D:\\";
            ofd.Filter = "所有文件|*.*|文本文件|*.txt";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;//一次只能选择单个文件
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "-x";
            Operator();
            if (showdata != null)
                MessageBox.Show(showdata);
            ClearData();
        }
        /// <summary>
        /// 输出一次数据后 将数据清零
        /// </summary>
        private void ClearData()
        {
            this.charcount = 0;
            this.wordcount = 0;
            this.linecount = 0;
            showdata = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
