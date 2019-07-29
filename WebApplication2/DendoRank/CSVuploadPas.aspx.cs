using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Oracle.DataAccess.Client;
using System.Data;

namespace WebApplication2.DendoRank
{
    public partial class CSVuploadPas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        const string strConnection = "Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = tcp) " +
           "(HOST=192.168.11.120)(PORT=1521))(CONNECT_DATA=" +
           "(SERVICE_NAME=xe)))"
           + ";User Id=tomo;Password=hy76cjs9";
        OracleConnection con;

        const String strInsSQL = "Insert into CHART (NENDO,GENRE,HYOKA_NUM,KYOKU_NAME,AUTHOR_NAME,HOUR,MINUTE,SECOND,MEMBER,FILE_SIZE) values " +
            "(:NENDO , :GENRE , :HYOKA_NUM , :KYOKU_NAME , :AUTHOR_NAME , :HOUR , :MINUTE , :SECOND , :MEMBER , :FILE_SIZE)";

        const String strDelSQL = "delete from chart";

        protected void Button1_Click(object sender, EventArgs e)
        {
            Boolean fileOK = false;

            if (FileUpload1.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();

                String[] allowedExtensions = { ".csv", ".txt" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
                if (fileOK)
                {
                    using (con = new OracleConnection(strConnection))
                    {
                        con.Open();
                        OracleTransaction txn = con.BeginTransaction();
                        try
                        {
                            //全件削除
                            var delcmd = new OracleCommand(strDelSQL);
                            delcmd.Connection = con;
                            delcmd.ExecuteNonQuery();
                            delcmd.Dispose();

                            readCSV();
                            txn.Commit();
                        }
                        catch (Exception ex)
                        {
                            txn.Rollback();
                            Label1.Text = ex.StackTrace;
                            return;
                        }
                        finally
                        {
                           
                            con.Close();
                        }
                        Label1.Text = "登録成功";
                    }
                }
                else
                {
                    Label1.Text = "CSVファイルのみアップロード可能";
                }

            }
        }

        private void readCSV()
        {
            Byte[] data = new Byte[FileUpload1.PostedFile.ContentLength];
            FileUpload1.PostedFile.InputStream.Read(data, 0, FileUpload1.PostedFile.ContentLength);

            String strData = "";
            strData = System.Text.Encoding.GetEncoding("shift_jis").GetString(data);
            ArrayList list = CsvToArrayList1(strData);

            for (int i = 0; i < list.Count; i++)
            {
                ArrayList row = (ArrayList)list[i];
                if (row.Count == 8)
                {
                    var cmd = new OracleCommand(strInsSQL);
                    cmd.Connection = con;

                    Int32 hour = 0;
                    Int32 MINUTE = 0;
                    Int32 SECOND = 0;

                    hour = Int32.Parse((String)row[5]) / 3600;
                    MINUTE = Int32.Parse((String)row[5]) / 60;
                    SECOND = Int32.Parse((String)row[5]) % 60;

                    cmd.BindByName = true;
                    OracleParameter nendo = new OracleParameter("NENDO", OracleDbType.Varchar2);
                    nendo.Direction = ParameterDirection.Input;
                    nendo.Value = (String)row[0];
                    cmd.Parameters.Add(nendo);
                    OracleParameter genre = new OracleParameter("GENRE", OracleDbType.Varchar2);
                    genre.Direction = ParameterDirection.Input;
                    genre.Value = (String)row[1];
                    cmd.Parameters.Add(genre);
                    OracleParameter hyoka = new OracleParameter("HYOKA_NUM", OracleDbType.Int32);
                    hyoka.Direction = ParameterDirection.Input;
                    hyoka.Value = Int32.Parse((String)row[2]);
                    cmd.Parameters.Add(hyoka);
                    cmd.Parameters.Add(new OracleParameter("KYOKU_NAME", OracleDbType.Varchar2, (String)row[3], ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("AUTHOR_NAME", OracleDbType.Varchar2, (String)row[4], ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("HOUR", OracleDbType.Int32, hour, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("MINUTE", OracleDbType.Int32, MINUTE, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("SECOND", OracleDbType.Int32, SECOND, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("MEMBER", OracleDbType.Int32, Int32.Parse((String)row[6]), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("FILE_SIZE", OracleDbType.Int32, Int32.Parse((String)row[7]), ParameterDirection.Input));

                    cmd.ExecuteNonQuery();
                    //cmd.Transaction.Commit();
                }
                else
                {
                    
                    Label1.Text = "CSVファイルの形式が違います。";
                    return;
                }

            }

        }

        /// <summary>
        /// CSVをArrayListに変換
        /// </summary>
        /// <param name="csvText">CSVの内容が入ったString</param>
        /// <returns>変換結果のArrayList</returns>
        public ArrayList CsvToArrayList1(string csvText)
        {
            System.Collections.ArrayList csvRecords =
                new System.Collections.ArrayList();

            //前後の改行を削除しておく
            csvText = csvText.Trim(new char[] { '\r', '\n' });

            //一行取り出すための正規表現
            System.Text.RegularExpressions.Regex regLine =
                new System.Text.RegularExpressions.Regex(
                "^.*(?:\\n|$)",
                System.Text.RegularExpressions.RegexOptions.Multiline);

            //1行のCSVから各フィールドを取得するための正規表現
            System.Text.RegularExpressions.Regex regCsv =
                new System.Text.RegularExpressions.Regex(
                "\\s*(\"(?:[^\"]|\"\")*\"|[^,]*)\\s*,",
                System.Text.RegularExpressions.RegexOptions.None);

            System.Text.RegularExpressions.Match mLine = regLine.Match(csvText);
            while (mLine.Success)
            {
                //一行取り出す
                string line = mLine.Value;
                //改行記号が"で囲まれているか調べる
                while ((CountString(line, "\"") % 2) == 1)
                {
                    mLine = mLine.NextMatch();
                    if (!mLine.Success)
                    {
                        throw new ApplicationException("不正なCSV");
                    }
                    line += mLine.Value;
                }
                //行の最後の改行記号を削除
                line = line.TrimEnd(new char[] { '\r', '\n' });
                //最後に「,」をつける
                line += ",";

                //1つの行からフィールドを取り出す
                System.Collections.ArrayList csvFields =
                    new System.Collections.ArrayList();
                System.Text.RegularExpressions.Match m = regCsv.Match(line);
                while (m.Success)
                {
                    string field = m.Groups[1].Value;
                    //前後の空白を削除
                    field = field.Trim();
                    //"で囲まれている時
                    if (field.StartsWith("\"") && field.EndsWith("\""))
                    {
                        //前後の"を取る
                        field = field.Substring(1, field.Length - 2);
                        //「""」を「"」にする
                        field = field.Replace("\"\"", "\"");
                    }
                    csvFields.Add(field);
                    m = m.NextMatch();
                }

                csvFields.TrimToSize();
                csvRecords.Add(csvFields);

                mLine = mLine.NextMatch();
            }

            csvRecords.TrimToSize();
            return csvRecords;
        }

        /// <summary>
        /// 指定された文字列内にある文字列が幾つあるか数える
        /// </summary>
        /// <param name="strInput">strFindが幾つあるか数える文字列</param>
        /// <param name="strFind">数える文字列</param>
        /// <returns>strInput内にstrFindが幾つあったか</returns>
        public int CountString(string strInput, string strFind)
        {
            int foundCount = 0;
            int sPos = strInput.IndexOf(strFind);
            while (sPos > -1)
            {
                foundCount++;
                sPos = strInput.IndexOf(strFind, sPos + 1);
            }

            return foundCount;
        }






    }


}