using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace WebApplication2.DendoRank
{
    /// <summary>
    /// DendoService1 の概要の説明です
    /// </summary>
    [WebService(Namespace = "https://musewiki.dip.jp/dendoweb/DendoRank/DendoService1.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // この Web サービスを、スクリプトから ASP.NET AJAX を使用して呼び出せるようにするには、次の行のコメントを解除します。
    // [System.Web.Script.Services.ScriptService]
    public class DendoService1 : System.Web.Services.WebService
    {
        const string sql1 = "select author_name 制作者名,count(hyoka_num) 曲数 from chart group by author_name order by 曲数 desc,author_name";
        const string sql2 = "select author_name 制作者名,count(author_name) 曲数 from(select  author_name from  chart where  hyoka_num = 2)group by author_name order by 曲数 desc,author_name";
        const string sql3 = "select author_name 制作者名,count(author_name) 曲数 from(select  author_name from  chart where  hyoka_num = 1)group by author_name order by 曲数 desc,author_name";
        const string sql4 = "select author_name 制作者名,count(author_name) 曲数 from(select  author_name from  chart where  hyoka_num = 0)group by author_name order by 曲数 desc,author_name";
        const string sql5 = "select   kyoku_name 曲名,   author_name 制作者名,   file_size サイズ from   chart order by サイズ desc,kyoku_name ";
        const string sql6 = "select   kyoku_name 曲名,   author_name 制作者名,   file_size サイズ from   chart order by サイズ asc,kyoku_name ";
        const string sql7 = "select   kyoku_name 曲名,   author_name 制作者名,   hour || ':' || minute ||':' || second 時間 from   chart order by (hour * 3600) + (minute * 60) + second asc,kyoku_name ";
        const string sql8 = "select   kyoku_name 曲名,   author_name 制作者名,   hour || ':' || minute ||':' || second 時間 from   chart order by (hour * 3600) + (minute * 60) + second desc,kyoku_name ";
        const string sql9 = "select   author_name 制作者名,   sum(file_size) ファイルサイズ from   chart group by author_name order by ファイルサイズ desc,author_name ";
        const string sql12 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and  hyoka_num = 2) 優, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 1) 良 , (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 0) 可 from( select sum( case hyoka_num when 0 then 1 when 1 then 2 when 2 then 4 end  ) hoshi, author_name a_name   from chart group by author_name order by hoshi desc) order by 得点 desc,a_name";
        const string sql10 = "select   author_name 制作者名, hour || ':' || minute || ':' || second 演奏時間 from ( select   author_name,   trunc(time/3600) hour, trunc(mod(time,3600) / 60) minute, mod(time,60) second, time from ( select   author_name,   sum((hour * 3600) + (minute * 60) + second) time from   chart group by author_name order by time desc,author_name))";
        const string sql11 = "select author_name 制作者名, round(f_size/time,2) 愛 from( select author_name, sum(file_size) f_size, sum((hour * 3600) + (minute * 60) + second) time from  chart group by author_name) where time >=1200 order by 愛 desc,author_name";
        const string sql13 = "select kyoku_name 曲名, count(kyoku_name) 曲数 from chart group by kyoku_name order by 曲数 desc, kyoku_name";
        const string sql1999 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '1999') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '1999') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '1999') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '1999'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2000 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2000') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2000') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2000') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2000'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2001 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2001') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2001') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2001') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2001'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2002 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2002') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2002') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2002') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2002'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2003 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2003') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2003') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2003') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2003'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2004 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2004') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2004') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2004') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2004'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2005 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2005') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2005') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2005') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2005'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2006 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2006') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2006') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2006') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2006'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2007 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2007') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2007') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2007') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2007'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2008 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2008') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2008') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2008') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2008'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2009 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2009') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2009') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2009') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2009'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2010 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2010') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2010') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2010') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2010'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2011 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2011') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2011') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2011') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2011'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2012 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2012') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2012') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2012') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2012'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2013 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2013') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2013') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2013') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2013'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2014 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2014') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2014') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2014') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2014'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2015 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2015') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2015') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2015') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2015'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2016 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2016') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2016') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2016') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2016'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2017 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2017') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2017') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2017') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2017'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2018 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2018') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2018') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2018') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2018'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2019 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2019') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2019') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2019') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2019'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2020 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2020') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2020') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2020') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2020'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2021 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2021') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2021') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2021') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2021'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";
        const string sql2022 = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 2 and nendo = '2022') 優, (select count(hyoka_num)  from     chart   where     author_name = a_name and  hyoka_num = 1 and  nendo = '2022') 良 , (select count(hyoka_num)   from chart   where author_name = a_name and hyoka_num = 0 and     nendo = '2022') 可 from(   select      sum(     case hyoka_num     when 0 then 1     when 1 then 2     when 2 then 4 end     ) hoshi,     author_name a_name   from      chart    where     nendo = '2022'   group by      author_name    order by hoshi desc) order by 得点 desc,a_name";

        [WebMethod]
        public string[] HelloWorld()
        {
            string[] str = { "a", "b" };
            return str;
        }

        [WebMethod]
        public string[][] DendoRank(int id)
        {
            //        const string strConnection = "Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = tcp) " +
            //"(HOST=192.168.11.130)(PORT=1521))(CONNECT_DATA=" +
            //"(SERVICE_NAME=xe2)))"
            //+ ";User Id=system;Password=hy76cjs9";
            const string strConnection = "Data Source=XE2"
           + ";User Id=system;Password=hy76cjs9";

            string sql = "";

            switch (id)
            {
                case 1:
                    sql = sql1;
                    break;
                case 2:
                    sql = sql2;
                    break;
                case 3:
                    sql = sql3;
                    break;
                case 4:
                    sql = sql4;
                    break;
                case 5:
                    sql = sql5;
                    break;
                case 6:
                    sql = sql6;
                    break;
                case 7:
                    sql = sql7;
                    break;
                case 8:
                    sql = sql8;
                    break;
                case 9:
                    sql = sql9;
                    break;
                case 10:
                    sql = sql10;
                    break;
                case 11:
                    sql = sql11;
                    break;
                case 12:
                    sql = sql12;
                    break;
                case 13:
                    sql = sql13;
                    break;
                case 1999:
                    sql = sql1999;
                    break;
                case 2000:
                    sql = sql2000;
                    break;
                case 2001:
                    sql = sql2001;
                    break;
                case 2002:
                    sql = sql2002;
                    break;
                case 2003:
                    sql = sql2003;
                    break;
                case 2004:
                    sql = sql2004;
                    break;
                case 2005:
                    sql = sql2005;
                    break;
                case 2006:
                    sql = sql2006;
                    break;
                case 2007:
                    sql = sql2007;
                    break;
                case 2008:
                    sql = sql2008;
                    break;
                case 2009:
                    sql = sql2009;
                    break;
                case 2010:
                    sql = sql2010;
                    break;
                case 2011:
                    sql = sql2011;
                    break;
                case 2012:
                    sql = sql2012;
                    break;
                case 2013:
                    sql = sql2013;
                    break;
                case 2014:
                    sql = sql2014;
                    break;
                case 2015:
                    sql = sql2015;
                    break;
                case 2016:
                    sql = sql2016;
                    break;
                case 2017:
                    sql = sql2017;
                    break;
                case 2018:
                    sql = sql2018;
                    break;
                case 2019:
                    sql = sql2019;
                    break;
                case 2020:
                    sql = sql2020;
                    break;
                case 2021:
                    sql = sql2021;
                    break;
                case 2022:
                    sql = sql2022;
                    break;
                default:
                    sql = sql12;
                    break;
            }
            DataList dl = new DataList();
            string[][] grid = new string[0][];
            using (var connection = new OracleConnection(strConnection))
            {
                connection.Open();
                using (var command = new OracleCommand(sql, connection))
                {
                    using (var adapter = command.ExecuteReader())
                    {
                        if (adapter.HasRows)
                        {
                            //grid = new string[adapter.RowSize][];
                            List<string[]> lg = new List<string[]>();
                            int rows = 0;
                            while (adapter.Read())
                            {
                                if (rows == 0)
                                {
                                    string[] title = new string[adapter.FieldCount];
                                    for (int i = 0; i < adapter.FieldCount; i++)
                                    {
                                        title[i] = adapter.GetName(i).ToString();
                                    }
                                    lg.Add(title);
                                }
                                string[] row = new string[adapter.FieldCount];
                                for (int i=0; i < adapter.FieldCount;i++)
                                {
                                    row[i] = adapter.GetValue(i).ToString();
                                }
                                lg.Add(row);
                                rows++;
                            }
                            grid = lg.ToArray();
                        }
                    }
                }
                connection.Close();
            }

            

            return grid;
        }
    }
}

