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
    public partial class CSVupload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        //const string strConnection = "Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = tcp) " +
        //   "(HOST=192.168.11.130)(PORT=1521))(CONNECT_DATA=" +
        //   "(SERVICE_NAME=xe)))"

        const string strConnection = "Data Source=XE2"
           + ";User Id=system;Password=hy76cjs9";
        OracleConnection con;

        const String strInsSQL = "Insert into CHART (NENDO,GENRE,HYOKA_NUM,KYOKU_NAME,AUTHOR_NAME,HOUR,MINUTE,SECOND,MEMBER,FILE_SIZE) values "+
            "(:NENDO , :GENRE , :HYOKA_NUM , :KYOKU_NAME , :AUTHOR_NAME , :HOUR , :MINUTE , :SECOND , :MEMBER , :FILE_SIZE)";

        protected void Button1_Click(object sender, EventArgs e)
        {
            Boolean fileOK = false;

            if (FileUpload1.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();

                String[] allowedExtensions = { ".csv", ".txt" };

                for (int i=0;i<allowedExtensions.Length;i++)
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
                        try
                        {
                            readCSV();
                        } catch (Exception ex)
                        {
                            Label1.Text = ex.Message + ex.StackTrace;
                            return;
                        } finally
                        {
                            con.Close();
                        }
                        Label1.Text = "登録成功";
                    }
                } else
                {
                    Label1.Text = "CSVファイルのみアップロード可能";
                }

            }
        }

        private void readCSV()
        {
            Byte[] data = new Byte[FileUpload1.PostedFile.ContentLength];
            FileUpload1.PostedFile.InputStream.Read(data, 0, FileUpload1.PostedFile.ContentLength);

            // System.Text.Encoding enc = GetCode(data);
            String str = null;

            Hnx8.ReadJEnc.CharCode charCode = Hnx8.ReadJEnc.ReadJEnc.JP.GetEncoding(data, data.Length, out str);
            
            String strData = "";
            // strData = System.Text.Encoding.GetEncoding("utf-8").GetString(data);
            strData = charCode.GetEncoding().GetString(data);
            ArrayList list = CsvToArrayList1(strData);
            
            for (int i=0;i<list.Count;i++)
            {
                ArrayList row = (ArrayList)list[i];
                if (row.Count == 10) {
                    var cmd = new OracleCommand(strInsSQL);
                    cmd.Connection = con;

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
                    cmd.Parameters.Add(new OracleParameter("HOUR", OracleDbType.Int32, Int32.Parse((String)row[5]), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("MINUTE", OracleDbType.Int32, Int32.Parse((String)row[6]), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("SECOND", OracleDbType.Int32, Int32.Parse((String)row[7]), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("MEMBER", OracleDbType.Int32, Int32.Parse((String)row[8]), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("FILE_SIZE", OracleDbType.Int32, Int32.Parse((String)row[9]), ParameterDirection.Input));

                    cmd.ExecuteNonQuery();
                    //cmd.Transaction.Commit();
                } else
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


        /// <summary>
        /// 文字コードを判別する
        /// </summary>
        /// <remarks>
        /// Jcode.pmのgetcodeメソッドを移植したものです。
        /// Jcode.pm(http://openlab.ring.gr.jp/Jcode/index-j.html)
        /// Jcode.pmの著作権情報
        /// Copyright 1999-2005 Dan Kogai <dankogai@dan.co.jp>
        /// This library is free software; you can redistribute it and/or modify it
        ///  under the same terms as Perl itself.
        /// </remarks>
        /// <param name="bytes">文字コードを調べるデータ</param>
        /// <returns>適当と思われるEncodingオブジェクト。
        /// 判断できなかった時はnull。</returns>
        public static System.Text.Encoding GetCode(byte[] bytes)
        {
            const byte bEscape = 0x1B;
            const byte bAt = 0x40;
            const byte bDollar = 0x24;
            const byte bAnd = 0x26;
            const byte bOpen = 0x28;    //'('
            const byte bB = 0x42;
            const byte bD = 0x44;
            const byte bJ = 0x4A;
            const byte bI = 0x49;

            int len = bytes.Length;
            byte b1, b2, b3, b4;

            //Encode::is_utf8 は無視

            bool isBinary = false;
            for (int i = 0; i < len; i++)
            {
                b1 = bytes[i];
                if (b1 <= 0x06 || b1 == 0x7F || b1 == 0xFF)
                {
                    //'binary'
                    isBinary = true;
                    if (b1 == 0x00 && i < len - 1 && bytes[i + 1] <= 0x7F)
                    {
                        //smells like raw unicode
                        return System.Text.Encoding.Unicode;
                    }
                }
            }
            if (isBinary)
            {
                return null;
            }

            //not Japanese
            bool notJapanese = true;
            for (int i = 0; i < len; i++)
            {
                b1 = bytes[i];
                if (b1 == bEscape || 0x80 <= b1)
                {
                    notJapanese = false;
                    break;
                }
            }
            if (notJapanese)
            {
                return System.Text.Encoding.ASCII;
            }

            for (int i = 0; i < len - 2; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                b3 = bytes[i + 2];

                if (b1 == bEscape)
                {
                    if (b2 == bDollar && b3 == bAt)
                    {
                        //JIS_0208 1978
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    else if (b2 == bDollar && b3 == bB)
                    {
                        //JIS_0208 1983
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    else if (b2 == bOpen && (b3 == bB || b3 == bJ))
                    {
                        //JIS_ASC
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    else if (b2 == bOpen && b3 == bI)
                    {
                        //JIS_KANA
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    if (i < len - 3)
                    {
                        b4 = bytes[i + 3];
                        if (b2 == bDollar && b3 == bOpen && b4 == bD)
                        {
                            //JIS_0212
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        }
                        if (i < len - 5 &&
                            b2 == bAnd && b3 == bAt && b4 == bEscape &&
                            bytes[i + 4] == bDollar && bytes[i + 5] == bB)
                        {
                            //JIS_0208 1990
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        }
                    }
                }
            }

            //should be euc|sjis|utf8
            //use of (?:) by Hiroki Ohzaki <ohzaki@iod.ricoh.co.jp>
            int sjis = 0;
            int euc = 0;
            int utf8 = 0;
            for (int i = 0; i < len - 1; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                if (((0x81 <= b1 && b1 <= 0x9F) || (0xE0 <= b1 && b1 <= 0xFC)) &&
                    ((0x40 <= b2 && b2 <= 0x7E) || (0x80 <= b2 && b2 <= 0xFC)))
                {
                    //SJIS_C
                    sjis += 2;
                    i++;
                }
            }
            for (int i = 0; i < len - 1; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                if (((0xA1 <= b1 && b1 <= 0xFE) && (0xA1 <= b2 && b2 <= 0xFE)) ||
                    (b1 == 0x8E && (0xA1 <= b2 && b2 <= 0xDF)))
                {
                    //EUC_C
                    //EUC_KANA
                    euc += 2;
                    i++;
                }
                else if (i < len - 2)
                {
                    b3 = bytes[i + 2];
                    if (b1 == 0x8F && (0xA1 <= b2 && b2 <= 0xFE) &&
                        (0xA1 <= b3 && b3 <= 0xFE))
                    {
                        //EUC_0212
                        euc += 3;
                        i += 2;
                    }
                }
            }
            for (int i = 0; i < len - 1; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                if ((0xC0 <= b1 && b1 <= 0xDF) && (0x80 <= b2 && b2 <= 0xBF))
                {
                    //UTF8
                    utf8 += 2;
                    i++;
                }
                else if (i < len - 2)
                {
                    b3 = bytes[i + 2];
                    if ((0xE0 <= b1 && b1 <= 0xEF) && (0x80 <= b2 && b2 <= 0xBF) &&
                        (0x80 <= b3 && b3 <= 0xBF))
                    {
                        //UTF8
                        utf8 += 3;
                        i += 2;
                    }
                }
            }
            //M. Takahashi's suggestion
            //utf8 += utf8 / 2;

            System.Diagnostics.Debug.WriteLine(
                string.Format("sjis = {0}, euc = {1}, utf8 = {2}", sjis, euc, utf8));
            if (euc > sjis && euc > utf8)
            {
                //EUC
                return System.Text.Encoding.GetEncoding(51932);
            }
            else if (sjis > euc && sjis > utf8)
            {
                //SJIS
                return System.Text.Encoding.GetEncoding(932);
            }
            else if (utf8 > euc && utf8 > sjis)
            {
                //UTF8
                return System.Text.Encoding.UTF8;
            }

            return null;
        }




    }


}