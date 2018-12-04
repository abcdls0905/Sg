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

            //���ж�ȡ
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] elements = line.Split(',');

                //��һ�ζ�ȡ����ʱ��Ҫ����������
                if (bFirst)
                {
                    for (int i = 0; i < elements.Length; i++)
                    {
                        dt.Columns.Add("");
                    }
                    bFirst = false;
                }

                //�б�����ʱ����һ�е��������д���
                if (hasTitle)
                {
                    for (int i = 0; i < dt.Columns.Count && i < elements.Length; i++)
                    {
                        dt.Columns[i] = elements[i];
                    }
                    hasTitle = false;
                }
                else //��ȡһ������
                {
                    if (elements.Length == dt.Columns.Count)
                    {
                        dt.Rows.Add(elements);
                    }
                    else
                    {
                        //throw new Exception("CSV��ʽ���󣺱�����������һ��");
                    }
                }
            }
            sr.Close();

            return dt;
        }

        /// <summary>
        /// ��DataTable���ݱ��浽CSV�ļ���
        /// </summary>
        /// <param name="dt">���ݱ�</param>
        /// <param name="path">CSV�ļ���ַ</param>
        /// <param name="hasTitle">�Ƿ�Ҫ������ݱ����������ΪCSV�ļ���һ��</param>
        public static void SaveToCSV(CsvTable dt, string path, bool hasTitle = false)
        {
            StreamWriter sw = new StreamWriter(path);

            //��������У�����У�
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

            //����ļ�����
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