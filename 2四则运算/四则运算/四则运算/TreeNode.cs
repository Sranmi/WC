using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 四则运算
{
    /// <summary>
    /// 二叉链表结点类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNode<T>
    {
        private T data;               //数据域
        private TreeNode<T> lChild;   //左孩子   树中一个结点的子树的根结点称为这个结点的孩子
        private TreeNode<T> rChild;   //右孩子

        public TreeNode(T val, TreeNode<T> lp, TreeNode<T> rp)
        {
            data = val;
            lChild = lp;
            rChild = rp;
        }

        public TreeNode(TreeNode<T> lp, TreeNode<T> rp)
        {
            data = default(T);
            lChild = lp;
            rChild = rp;
        }

        public TreeNode(T val)
        {
            data = val;
            lChild = null;
            rChild = null;
        }

        public TreeNode()
        {
            data = default(T);
            lChild = null;
            rChild = null;
        }

        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        public TreeNode<T> LChild
        {
            get { return lChild; }
            set { lChild = value; }
        }

        public TreeNode<T> RChild
        {
            get { return rChild; }
            set { rChild = value; }
        }
        /// <summary>
        /// 定义索引文件结点的数据类型
        /// </summary>
        public struct indexnode
        {
            int key;         //键
            int offset;      //位置
            public indexnode(int key, int offset)
            {
                this.key = key;
                this.offset = offset;
            }

            //键属性
            public int Key
            {
                get { return key; }
                set { key = value; }
            }
            //位置属性
            public int Offset
            {
                get { return offset; }
                set { offset = value; }
            }


        }

    }
    public class LinkBinaryTree<T>
    {
        private TreeNode<T> head;       //头引用
        public TreeNode<T> Head
        {
            get { return head; }
            set { head = value; }
        }
        public LinkBinaryTree()
        {
            head = null;
        }
        public LinkBinaryTree(T val)
        {
            TreeNode<T> p = new TreeNode<T>(val);
            head = p;
        }
        public LinkBinaryTree(T val, TreeNode<T> lp, TreeNode<T> rp)
        {
            TreeNode<T> p = new TreeNode<T>(val, lp, rp);
            head = p;
        }
        //判断是否是空二叉树
        public bool IsEmpty()
        {
            if (head == null)
                return true;
            else
                return false;
        }
        //获取根结点
        public TreeNode<T> Root()
        {
            return head;
        }
        //获取结点的左孩子结点
        public TreeNode<T> GetLChild(TreeNode<T> p)
        {
            return p.LChild;
        }
        public TreeNode<T> GetRChild(TreeNode<T> p)
        {
            return p.RChild;
        }
        //将结点p的左子树插入值为val的新结点，原来的左子树称为新结点的左子树
        public void InsertL(T val, TreeNode<T> p)
        {
            TreeNode<T> tmp = new TreeNode<T>(val);
            tmp.LChild = p.LChild;
            p.LChild = tmp;
        }
        //将结点p的右子树插入值为val的新结点，原来的右子树称为新节点的右子树
        public void InsertR(T val, TreeNode<T> p)
        {
            TreeNode<T> tmp = new TreeNode<T>(val);
            tmp.RChild = p.RChild;
            p.RChild = tmp;
        }
        //若p非空 删除p的左子树
        public TreeNode<T> DeleteL(TreeNode<T> p)
        {
            if ((p == null) || (p.LChild == null))
                return null;
            TreeNode<T> tmp = p.LChild;
            p.LChild = null;
            return tmp;
        }
        //若p非空 删除p的右子树
        public TreeNode<T> DeleteR(TreeNode<T> p)
        {
            if ((p == null) || (p.RChild == null))
                return null;
            TreeNode<T> tmp = p.RChild;
            p.RChild = null;
            return tmp;
        }
        //判断是否是叶子结点
        public bool IsLeaf(TreeNode<T> p)
        {
            if ((p != null) && (p.RChild == null) && (p.LChild == null))
                return true;
            else
                return false;
        }



        private List<int[]> num = new List<int[]>();
        private List<char> sym = new List<char>();
        char s;
        //后序遍历
        //遍历根结点的左子树->遍历根结点的右子树->根结点
        private void postorder(TreeNode<Number> ptr,ref string problem)
        {
            if (IsEmpty())
            {
                Console.WriteLine("Tree is Empty !");
                return;
            }
            if (ptr != null)
            {
                postorder(ptr.LChild,ref problem);
                postorder(ptr.RChild,ref problem );
                // Console.WriteLine(ptr.Data.Son.ToString ()  + "/"+ptr .Data.Mother .ToString ()+ptr .Data .Symbol .ToString () );
                char c = ptr.Data.Symbol;
                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    sym.Add(c);
                }
                else
                {
                    int[] nn = new int[2] { ptr.Data.Son, ptr.Data.Mother };
                    num.Add(nn);
                }
                if(sym.Count ==1&&num.Count>=2 )
                {
                    num [num.Count - 2] =  Arithmetic(num[num.Count - 2], num[num.Count -1], sym[0],ref problem);
                    num.RemoveAt(num.Count - 1);
                    sym.RemoveAt(0);
                }
            }

        }

        public int [] GetResult(TreeNode<Number> ptr,ref string problem)
        {
            postorder(ptr,ref problem );
            return num[0];
        }

        int s1, s2, s3;
        public int[] Arithmetic(int[] b1, int[] b2, char s, ref string problem)
        {
            int[] result = new int[2];
            switch (s)
            {
                case '+':
                    s1 = b1[0] * b2[1] + b2[0] * b1[1];
                    s2 = b1[1] * b2[1];
                    calculation c1 = new calculation();
                    s3 = c1.GetCommon(s1, s2);
                    result[0] = s1 / s3;
                    result[1] = s2 / s3;
                    break;
                case '-':
                    s1 = b1[0] * b2[1] - b2[0] * b1[1];
                    s2 = b1[1] * b2[1];

                    calculation c2 = new calculation();
                    s3 = c2.GetCommon(s1, s2);
                    result[0] = s1 / s3;
                    result[1] = s2 / s3;
                    //运算过程中出现负数，则主函数进行判断，舍去后重新生成计算式
                    if (s1 < 0)
                    {
                        problem = "重新生成";
                    }
                    break;
                case '*':
                    s1 = b1[0] * b2[0];
                    s2 = b1[1] * b2[1];
                    calculation c3 = new calculation();
                    s3 = c3.GetCommon(s1, s2);
                    result[0] = s1 / s3;
                    result[1] = s2 / s3;
                    break;
                case '/':
                    s1 = b1[0] * b2[1];
                    s2 = b1[1] * b2[0];
                    calculation c4 = new calculation();
                    s3 = c4.GetCommon(s1, s2);
                    result[0] = s1 / s3;
                    result[1] = s2 / s3;
                    break;
            }
            return result;
        }
    }
}
