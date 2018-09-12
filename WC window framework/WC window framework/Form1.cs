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
        private int workcount;
        private int linecount;
        private string filename;
        private string[] parameter;
        private string showdata=null ;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.charcount = 0;
            this.workcount = 0;
            this.linecount = 0;
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
                //参数-x输出图形界面选择文件并读取文件名
                if (s == "-x")
                {
                    OpenFile();
                }
                //判断文件是否存在
                if (File.Exists(filename))
                {
                    FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                    StreamReader sr = new StreamReader(fs);
                    Function fc = new Function();
                    if (s == "-c"||s=="-x")
                    {
                        fc.CharCount(sr, ref charcount);
                        showdata += "字符数：" + charcount.ToString()+"\n";
                    }
                    if (s == "-w" || s == "-x")
                    {
                        fc.WordCount(sr, ref workcount);
                        showdata += "单词数:" + workcount.ToString() + "\n";
                    }
                    if (s == "-l" || s == "-x")
                    {
                        fc.LineCount(sr, ref linecount);
                        showdata += "行数：" + linecount.ToString() + "\n";
                    }
                    //else if (s == "-")
                    // {

                    // }
                    if (s !="-c"&&s!="-w"&&s!="-l"&&s!="-s"&&s!="-a"&&s!="-x")
                    {
                        MessageBox.Show("参数输入有误，请重新输入，注意多参数之间必须用空格隔开");
                        break;
                    }
                    sr.Close();
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
            ofd.RestoreDirectory = true ;
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
            this.workcount = 0;
            this.linecount = 0;
            showdata = null;
        }
    }
}
