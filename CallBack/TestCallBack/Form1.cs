using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading; 

namespace TestCallBack
{
    public partial class Form1 : Form
    {
        //定义委托
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void MyCallBack(IntPtr pData, int n);   
    
        //导入库函数
        [DllImport("callbackproc.dll")]
        public static extern void Register_Callback([MarshalAs(UnmanagedType.FunctionPtr)]MyCallBack call);
        [DllImport("callbackproc.dll")]
        public static extern void StartRv();
        [DllImport("callbackproc.dll")]
        public static extern void SetPath(string str);

        MyCallBack m_call;//定义委托函数为成员变量，在后面new，防止被C#垃圾回收出现错误
        SubThread m_subthread;//定义一个线程
        /*定义一代理
         * 说明:其实例作为Invoke参数,以实现后台线程调用主线的函数
         * MessageEventArgs传递显示的信息.
         * */
        public delegate void MessageHandler(MessageEventArgs e);


        //回调函数执行操作
        void OutputText(IntPtr pData, int n)
        {
            byte[] bytes = new byte[n];
            Marshal.Copy(pData, bytes, 0, bytes.Length);

            System.Text.ASCIIEncoding converter = new System.Text.ASCIIEncoding();
            string strData = converter.GetString(bytes);

            m_subthread.OnMessageSend(this, new MessageEventArgs(strData));
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           //初始化回调函数
            m_call = new MyCallBack(OutputText);

            //注册回调函数回DLL            
            Register_Callback(m_call);

            m_subthread = new SubThread();
            //添加后台线程的消息处理实现函数,
            m_subthread.MessageSend += new SubThread.MessageEventHandler(this.Subthread_MessageSend);
        }

        /*说明:通过代理,消息事件,实际就是实现在后台线程调用本函数,以前就说了在后台线程中不能直接把消息发送到主线程,
        * 那么就要用到Invoke,关于怎么用不再多说
        * 参数要和MessageEventArgs代理的参数一至
        * **/
        private void Subthread_MessageSend(object sender, MessageEventArgs e)
        {
            //实例化代理
            MessageHandler handler = new MessageHandler(Message);
            //调用Invoke
            this.Invoke(handler, new object[] { e });
        }

        //接收消息，更新列表
        public void Message(MessageEventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + e.Message + "\r\n";
        }       
        

        //启动新线程，回调函数监听
        private void button1_Click(object sender, EventArgs e)
        {
            SetPath(@"C:\Users\Public\Pictures\XCX");
            m_subthread.m_thread = new System.Threading.Thread(new System.Threading.ThreadStart(StartRv));
            m_subthread.m_thread.Start();
        }

        //终止线程
        private void button2_Click(object sender, EventArgs e)
        {
            m_subthread.m_thread.Abort();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_subthread.m_thread != null)
            {
                m_subthread.m_thread.Abort();
            }
        }
    }

}
