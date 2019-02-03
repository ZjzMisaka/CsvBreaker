using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvBreaker
{
    class CsvBreaker
    {
        private ArrayList rowAL;         //行链表,CSV文件的每一行就是一个链
        private string fileName;        //文件名
        private Encoding encoding;        //编码
        List<StringBuilder> csvDataLines = new List<StringBuilder>();
        List<string> athors = new List<string>();
        public CsvBreaker()
        {
            this.rowAL = new ArrayList();
            this.fileName = "";
            this.encoding = Encoding.Default;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="filename">文件名,包括文件路径
        public CsvBreaker(string fileName)
        {
            this.rowAL = new ArrayList();
            this.fileName = fileName;
            this.encoding = Encoding.Default;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="filename">文件名,包括文件路径
        /// <param name="encoding">文件编码
        public CsvBreaker(string fileName, Encoding encoding)
        {
            this.rowAL = new ArrayList();
            this.fileName = fileName;
            this.encoding = encoding;
        }
        /// <summary>
        /// 文件名,包括文件路径
        /// </summary>
        public string FileName
        {
            set
            {
                this.fileName = value;
            }
        }
        /// <summary>
        /// 文件编码
        /// </summary>
        public Encoding FileEncoding
        {
            set
            {
                this.encoding = value;
            }
        }
        /// <summary>
        /// 获取行数
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.rowAL.Count;
            }
        }
        /// <summary>
        /// 获取列数
        /// </summary>
        public int ColCount
        {
            get
            {
                int maxCol;
                maxCol = 0;
                for (int i = 0; i < this.rowAL.Count; i++)
                {
                    ArrayList colAL = (ArrayList)this.rowAL[i];
                    maxCol = (maxCol > colAL.Count) ? maxCol : colAL.Count;
                }
                return maxCol;
            }
        }

        public List<StringBuilder> LoadCsvFile()
        {
            //对数据的有效性进行验证
            if (fileName == null)
            {
                Console.WriteLine("请指定要载入的CSV文件名");
                return null;
            }
            else if (!File.Exists(fileName))
            {
                Console.WriteLine("指定的CSV文件不存在");
                return null;
            }
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }

            StreamReader sr = new StreamReader(fileName, encoding);

            bool quotationMarks = true;

            StringBuilder temp = new StringBuilder();
            char tempStr = '1';

            bool addFlag = false;

            int totalCount = 0;

            while (sr.Peek() != -1)
            {
                while (sr.Peek() != -1)
                {
                    if ((char)sr.Peek() == '"')
                    {
                        //是否偶数个引号
                        if (quotationMarks)
                        {
                            quotationMarks = false;
                        }
                        else
                        {
                            quotationMarks = true;
                        }
                        int tempSr1 = sr.Read();
                        tempStr = (char)tempSr1;
                        temp.Append(tempStr);
                        if ((char)sr.Peek() == ',' && quotationMarks)
                        {
                            addFlag = true;
                        }
                    }
                    //是否换行并偶数个引号
                    else if ((tempStr == '\r' && (char)sr.Peek() == '\n') && quotationMarks)
                    {
                        addFlag = true;
                        //sr.Read();
                    }
                    //读取下一个
                    if (!addFlag)
                    {
                        tempStr = (char)sr.Read();
                        temp.Append(tempStr);
                    }


                    if (addFlag)
                    {
                        csvDataLines.Add(temp);
                        sr.Read();
                        temp = new StringBuilder();
                        addFlag = false;

                        while ((char)sr.Peek() == ',')
                        {
                            temp.Append("\"\"");
                            csvDataLines.Add(temp);
                            temp = new StringBuilder();
                            sr.Read();
                        }
                    }
                }
                Console.WriteLine(++totalCount);
            }
            sr.Close();
            return csvDataLines;

        }


    }
}
