using System.Data;
using System.Data.Common;
using System;

namespace WebApplication2.App_Code
{
    public class DataAccess {
        //DB接続関連はクラスで保有
        DbProviderFactory factory;
        DbConnection conn;
        DbConnectionStringBuilder ocsb;
        DbCommand cmd;
        DbDataAdapter da;
        DataSet ds;
        public DbConnection DbConnect()
        {
            factory =
              DbProviderFactories.GetFactory("Oracle.DataAccess.Client");
            ocsb = factory.CreateConnectionStringBuilder();

            //接続文字列の設定
            ocsb["Data Source"] = "xe";
            ocsb["User ID"] = "tomo";
            ocsb["Password"] = "hy76cjs9";
            conn = factory.CreateConnection();
            conn.ConnectionString = ocsb.ConnectionString;


            //データベース接続
            conn.Open();
            return conn;
        }

        public DataSet GetData()
        {
            conn = this.DbConnect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT rowid,chart.* FROM chart";
            da = factory.CreateDataAdapter();
            da.SelectCommand = cmd;
            //da.DeleteCommand
            //SELECTの実行
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public void EditDept(string NENDO, string GENRE, Int32 HYOKA_NUM,string KYOKU_NAME,string AUTHOR_NAME,Int32 HOUR,Int32 MINUTE,Int32 SECOND,Int32 MEMBER,Int32 FILE_SIZE,string ROWID)
        {
            string sql;
            //パラメータは使いまわさないためメソッド内で宣言
            DbParameter prmNENDO;
            DbParameter prmGENRE;
            DbParameter prmHYOKA_NUM;
            DbParameter prmKYOKU_NAME;
            DbParameter prmAUTHOR_NAME;
            DbParameter prmHOUR;
            DbParameter prmMINUTE;
            DbParameter prmSECOND;
            DbParameter prmMEMBER;
            DbParameter prmFILE_SIZE;
            DbParameter prmROWID;

            //パラメータはバインド変数で渡す
            sql = "Update chart set "
                   + " NENDO = :NENDO,"
                   + " GENRE = :GENRE,"
                   + " HYOKA_NUM = :HYOKA_NUM,"
                   + " KYOKU_NAME = :KYOKU_NAME,"
                   + " AUTHOR_NAME = :AUTHOR_NAME,"
                   + " HOUR = :HOUR_A,"
                   + " MINUTE = :MINUTE_A,"
                   + " SECOND = :SECOND_A,"
                   + " MEMBER = :MEMBER_A,"
                   + " FILE_SIZE = :FILE_SIZE"
                   + " Where ROWID = :ROWID_A";

            conn = this.DbConnect();
            cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            //パラメータ定義
            prmNENDO = cmd.CreateParameter();
            prmNENDO.Value = NENDO;
            cmd.Parameters.Add(prmNENDO);

            prmGENRE = cmd.CreateParameter();
            prmGENRE.Value = GENRE;
            cmd.Parameters.Add(prmGENRE);

            prmHYOKA_NUM = cmd.CreateParameter();
            prmHYOKA_NUM.Value = HYOKA_NUM;
            cmd.Parameters.Add(prmHYOKA_NUM);

            prmKYOKU_NAME = cmd.CreateParameter();
            prmKYOKU_NAME.Value = KYOKU_NAME;
            cmd.Parameters.Add(prmKYOKU_NAME);

            prmAUTHOR_NAME = cmd.CreateParameter();
            prmAUTHOR_NAME.Value = AUTHOR_NAME;
            cmd.Parameters.Add(prmAUTHOR_NAME);


            prmHOUR = cmd.CreateParameter();
            prmHOUR.Value = HOUR;
            cmd.Parameters.Add(prmHOUR);

            prmMINUTE = cmd.CreateParameter();
            prmMINUTE.Value = MINUTE;
            cmd.Parameters.Add(prmMINUTE);

            prmSECOND = cmd.CreateParameter();
            prmSECOND.Value = SECOND;
            cmd.Parameters.Add(prmSECOND);

            prmMEMBER = cmd.CreateParameter();
            prmMEMBER.Value = MEMBER;
            cmd.Parameters.Add(prmMEMBER);

            prmFILE_SIZE = cmd.CreateParameter();
            prmFILE_SIZE.Value = FILE_SIZE;
            cmd.Parameters.Add(prmFILE_SIZE);

            prmROWID = cmd.CreateParameter();
            prmROWID.Value = ROWID;
            cmd.Parameters.Add(prmROWID);

            //UPDATE実行
            cmd.ExecuteNonQuery();
            //cmd.Dispose();
        }

        public void delete(string ROWID, string NENDO)
        {
            string sql;
            //パラメータは使いまわさないためメソッド内で宣言
             DbParameter prmROWID;

            //パラメータはバインド変数で渡す
            sql = "delete from chart where  ROWID = :ROWID_A";


            conn = this.DbConnect();
            cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            //パラメータ定義

            prmROWID = cmd.CreateParameter();
            prmROWID.Value = ROWID;
            cmd.Parameters.Add(prmROWID);

            //UPDATE実行
            cmd.ExecuteNonQuery();
            //cmd.Dispose();
        }

    }
}