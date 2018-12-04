using System.Collections.Generic;
using System.IO;

namespace Game
{
    public class CsvTable
    {
        public List<string> Columns = new List<string>();
        public List<string[]> Rows = new List<string[]>();
    }

    public class CSVHelper
    {
        public static CsvTable ReadFromCSV(string data, bool hasTitle = false)
        {
            CsvTable dt = new CsvTable();
            StringReader sr = new StringReader(data);
            bool bFirst = true;

            //逐行读取
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] elements = line.Split(',');

                //第一次读取数据时，要创建数据列
                if (bFirst)
                {
                    for (int i = 0; i < elements.Length; i++)
                    {
                        dt.Columns.Add("");
                    }
                    bFirst = false;
                }

                //有标题行时，第一行当做标题行处理
                if (hasTitle)
                {
                    for (int i = 0; i < dt.Columns.Count && i < elements.Length; i++)
                    {
                        dt.Columns[i] = elements[i];
                    }
                    hasTitle = false;
                }
                else //读取一行数据
                {
                    if (elements.Length == dt.Columns.Count)
                    {
                        dt.Rows.Add(elements);
                    }
                    else
                    {
                        //throw new Exception("CSV格式错误：表格各行列数不一致");
                    }
                }
            }
            sr.Close();

            return dt;
        }

        /// <summary>
        /// 将DataTable内容保存到CSV文件中
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="path">CSV文件地址</param>
        /// <param name="hasTitle">是否要输出数据表各列列名作为CSV文件第一行</param>
        public static void SaveToCSV(CsvTable dt, string path, bool hasTitle = false)
        {
            StreamWriter sw = new StreamWriter(path);

            //输出标题行（如果有）
            if (hasTitle)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i != dt.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.WriteLine();
            }

            //输出文件内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sw.Write(dt.Rows[i][j].ToString());
                    if (j != dt.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.WriteLine();
            }

            sw.Close();
        }
    }

    public class CsvDataType : BaseDataType<CsvTable>
    {
        public CsvDataType(string name) : base(name + ".csv")
        {
        }
        public override CsvTable Serialize(byte[] binary)
        {
            return CSVHelper.ReadFromCSV(System.Text.Encoding.UTF8.GetString(binary));
        }
    }
}