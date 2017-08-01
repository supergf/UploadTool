using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //上传单个文件
        String singleFileName;
        String singleFileUrl;
        //上传文件夹
        List<DirectoryInfo> list = new List<DirectoryInfo>();//存放目录下所有子文件夹
        List<String> mutiFileName = new List<string>();
        List<String> mutiFileUrl = new List<string>();
        //存储上一次的上传信息保存
        ArrayList aList = new ArrayList(); //Path
        ArrayList bList = new ArrayList(); //Name

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofp = new OpenFileDialog();
            if (ofp.ShowDialog() == DialogResult.OK)
            {
                //文件的路径
                singleFileUrl = ofp.FileName;
                //文件的名称
                singleFileName = ofp.SafeFileName;
                //显示上传的文件名字列表
                fileList.Text = singleFileUrl;

            }
        }

        private void upload_Click(object sender, EventArgs e)
        {
            //上传单个文件
            if (fileList.Text != null && fileList.Text != "")
            {
                //上传文件所在的文件夹
                if (inputUrl.Text == null || inputUrl.Text == "") {
                    MessageBox.Show("请输入上传地址！");
                    return;
                }
                //String fileUrl = inputUrl.Text + singleFileName;
                String fileUrl = "http://10.221.24.130/test/" + singleFileName;
                //本地文件的地址
                String filePath = singleFileUrl;
                String result = string.Empty;
                //线程中执行
                Thread thread = new Thread(new ParameterizedThreadStart(delegate
                {
                    result = HttpUploadFile(fileUrl, filePath);
                    UploadCallback(result);
                }));
                thread.Start();

            }
            else
            { //上传文件夹
              //1.0需求，上传一级目录下的文件
              //拼接url地址
                if (inputUrl.Text == null || inputUrl.Text == "")
                {
                    MessageBox.Show("请输入上传地址！");
                    return;
                }
                //如果aList bList为空，则上传的不是上一次的配置
                if (aList == null && bList == null)
                {
                    //do nothing
                }
                else
                {
                    //给mutiFileName mutiFileUrl赋值
                    for (int i = 0; i < aList.Count; i++)
                    {
                        mutiFileUrl.Add((String)aList[i]);
                    }
                    aList.Clear();
                    for (int i = 0; i < bList.Count; i++)
                    {
                        mutiFileName.Add((String)bList[i]);
                    }
                    bList.Clear();
                }
                int count = mutiFileName.Count;//需要上传文件的总个数

                for (int start = 0; start < count; start++)
                {
                    //String tempUrl = inputUrl.Text + mutiFileName[start];
                    int temp = start;
                    String tempUrl = "http://10.221.24.130/test/" + mutiFileName[start];
                    //String responseCode = HttpUploadFile(tempUrl, mutiFileUrl[start]);
                    String responseCode = String.Empty;
                    //线程中执行
                    Thread thread = new Thread(new ParameterizedThreadStart(delegate
                    {
                        responseCode = HttpUploadFile(tempUrl, mutiFileUrl[temp]);
                        Invoke(new MyInvoke(updateListbox), new Object[] {temp,responseCode
                    });
                        UploadFolderCallback(responseCode);
                    }));
                    thread.Start();
                }
                //全部上传成功
                //MessageBox.Show("上传成功！");
                //保存上一次配置信息
                //aList.Clear();
                int len = mutiFileUrl.Count;
                for (int begin = 0; begin < len; begin++)
                {
                    aList.Add(mutiFileUrl[begin]);
                }
                lastConfig.Default.filePath = aList;
                //bList.Clear();
                int arrayLength = this.fileListBox.Items.Count;
                for (int i = 0; i < arrayLength; i++)
                {
                    //String str = (String)fileListBox.Items[i];
                    bList.Add(fileListBox.Items[i]);
                }
                lastConfig.Default.fileName = bList;
                lastConfig.Default.Save();
                //清空上传列表list和全局目录list
                mutiFileName.Clear();
                mutiFileUrl.Clear();
                list.Clear();
                this.fileListBox.DataSource = null;
                this.fileListBox.Items.Clear();
            }
        }

        public delegate void MyInvoke(int i, String status);
        public void updateListbox(int i, String status) {
            if (status == "OK")
            {
                fileListBox.Items[i] = fileListBox.Items[i] + "[上传完成]";
            }
            else {
                fileListBox.Items[i] = fileListBox.Items[i] + "[上传中]";
            }
        }
        private void UploadFolderCallback(string responseCode)
        {
            Invoke(new MethodInvoker(() =>
            {
                if (responseCode != "OK")
                { //一旦出现返回码异常，退出程序
                    MessageBox.Show("上传失败！");
                    //清空list信息
                    mutiFileName.Clear();
                    mutiFileUrl.Clear();
                    list.Clear();
                    this.fileListBox.Items.Clear();
                    return;
                }
            }));
        }

        /// Http上传文件
        public string HttpUploadFile(string url, string path)
        {
          
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "PUT";
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            int pos = path.LastIndexOf("\\");
            string fileName = path.Substring(pos + 1);
            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] bArr = new byte[fs.Length];
            fs.Read(bArr, 0, bArr.Length);
            fs.Close();
            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(bArr, 0, bArr.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Put请求
            //Stream instream = response.GetResponseStream();
            //StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            //获取返回状态码
            string content = response.StatusDescription;
            return content;
        }
        public void UploadCallback(string result)
        {
            Invoke(new MethodInvoker(()=>
            {
                if (result == "OK")
                {
                    MessageBox.Show("上传成功！");
                    fileList.Text = ""; //文件列表置为空
                }
                else
                {
                    MessageBox.Show("上传失败！");
                }
            }));
        }
        private void folderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            String defaultPath = "";
            fbd.Description = "请选择一个文件夹";
            //是否显示对话框左下角 新建文件夹 按钮，默认为 true  
            fbd.ShowNewFolderButton = false;
            //首次defaultPath为空，按FolderBrowserDialog默认设置（即桌面）选择  
            if (defaultPath != "")
            {
                //设置此次默认目录为上一次选中目录  
                fbd.SelectedPath = defaultPath;
            }
            //按下确定选择的按钮  
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                //记录选中的目录  
                defaultPath = fbd.SelectedPath;
            }
            else
            {
                return;
            }
            DirectoryInfo theFolder = new DirectoryInfo(defaultPath);
            //获取目录下所有的文件夹
            //list.Add(theFolder);
            //findFolders(theFolder);

            //存储一级目录下的文件
            DirectoryInfo folder = new DirectoryInfo(defaultPath);

            //遍历文件夹

            foreach (FileInfo NextFile in folder.GetFiles())
            {  //遍历文件
                this.fileListBox.DataSource = null;
                this.fileListBox.Items.Add(NextFile.Name);
                mutiFileName.Add(NextFile.Name);
                mutiFileUrl.Add(NextFile.FullName);
            }

            //遍历每一个文件夹，获得所有的文件
            //int folderLength = list.Count;
            //for (int k = 0; k < folderLength; k++)
            //{
            //    foreach (FileInfo NextFile in list[k].GetFiles())
            //    {
            //        this.fileListBox.Items.Add(NextFile.Name);
            //        mutiFileName.Add(NextFile.Name);
            //        mutiFileUrl.Add(NextFile.FullName);
            //    }
            //}
        }

        //查找目录下所有文件夹
        private void findFolders(DirectoryInfo theFolder)
        {
            if (theFolder.GetDirectories().Length != 0)
            {
                int length = theFolder.GetDirectories().Length;
                DirectoryInfo[] nextDirect = theFolder.GetDirectories();
                //将找到的目录添加到list中
                for (int i = 0; i < length; i++)
                {
                    list.Add(nextDirect[i]);
                }
                for (int j = 0; j < length; j++)//递归调用
                {
                    findFolders(nextDirect[j]);
                }
            }
            else
            {
                return;
            }

        }

        //关闭窗体保存配置信息
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)

        {
            //lastConfig.Default.fileName = fileListBox;
            //lastConfig.Default.filePath = aList; 
            lastConfig.Default.Save();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //加载上一次上传配置
        private void lastConfigbutton_Click(object sender, EventArgs e)
        {
            //在listBox中显示上传信息
            this.fileListBox.DataSource = lastConfig.Default.fileName;
            aList = lastConfig.Default.filePath;
            bList = lastConfig.Default.fileName;
        }
    }
}
