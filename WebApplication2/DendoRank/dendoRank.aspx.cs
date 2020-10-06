using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2.DendoRank
{
    public partial class dendoRank : System.Web.UI.Page


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

        protected void Page_Load(object sender, EventArgs e)
        {
            const string strConnection = "Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = tcp) " +
                "(HOST=192.168.11.120)(PORT=1521))(CONNECT_DATA=" +
                "(SERVICE_NAME=xe)))"
                + ";User Id=tomo;Password=hy76cjs9";

            string id = Page.Request.QueryString.Get("id");
            string sql = "";

            switch(id)
            {
                case "1":
                    sql = sql1;
                    Label1.Text = "殿堂登録曲数";
                    ptitle.Text = "殿堂ランキング・殿堂登録曲数";
                    break;
                case "2":
                    sql = sql2;
                    Label1.Text = "★登録曲数";
                    ptitle.Text = "殿堂ランキング・★登録曲数";
                    break;
                case "3":
                    sql = sql3;
                    Label1.Text = "☆登録曲数";
                    ptitle.Text = "殿堂ランキング・☆登録曲数";
                    break;
                case "4":
                    sql = sql4;
                    Label1.Text = "無印登録曲数";
                    ptitle.Text = "殿堂ランキング・無印登録曲数";
                    break;
                case "5":
                    sql = sql5;
                    Label1.Text = "楽曲別ファイルサイズ最大";
                    ptitle.Text = "殿堂ランキング・楽曲別ファイルサイズ最大";
                    break;
                case "6":
                    sql = sql6;
                    Label1.Text = "楽曲別ファイルサイズ最小";
                    ptitle.Text = "殿堂ランキング・楽曲別ファイルサイズ最小";
                    break;
                case "7":
                    sql = sql7;
                    Label1.Text = "楽曲別短時間演奏";
                    ptitle.Text = "殿堂ランキング・楽曲別短時間演奏";
                    break;
                case "8":
                    sql = sql8;
                    Label1.Text = "楽曲別長時間演奏";
                    ptitle.Text = "殿堂ランキング・楽曲別長時間演奏";
                    break;
                case "9":
                    sql = sql9;
                    Label1.Text = "作者別ファイルサイズ最大";
                    ptitle.Text = "殿堂ランキング・作者別ファイルサイズ最大";
                    break;
                case "10":
                    sql = sql10;
                    Label1.Text = "作者別演奏時間最大";
                    ptitle.Text = "殿堂ランキング・作者別演奏時間最大";
                    break;
                case "11":
                    sql = sql11;
                    Label1.Text = "作者別時間あたりのファイルサイズ";
                    ptitle.Text = "殿堂ランキング・作者別時間あたりのファイルサイズ";
                    break;
                case "12":
                    sql = sql12;
                    Label1.Text = "殿堂ポイント";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント";
                    break;
                case "13":
                    sql = sql13;
                    Label1.Text = "曲名ランキング";
                    ptitle.Text = "殿堂ランキング・曲名ランキング";
                    break;
                case "1999":
                    sql = sql1999;
                    Label1.Text = "殿堂ポイント1999年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント1999年度版";
                    break;
                case "2000":
                    sql = sql2000;
                    Label1.Text = "殿堂ポイント2000年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2000年度版";
                    break;
                case "2001":
                    sql = sql2001;
                    Label1.Text = "殿堂ポイント2001年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2001年度版";
                    break;
                case "2002":
                    sql = sql2002;
                    Label1.Text = "殿堂ポイント2002年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2002年度版";
                    break;
                case "2003":
                    sql = sql2003;
                    Label1.Text = "殿堂ポイント2003年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2003年度版";
                    break;
                case "2004":
                    sql = sql2004;
                    Label1.Text = "殿堂ポイント2004年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2004年度版";
                    break;
                case "2005":
                    sql = sql2005;
                    Label1.Text = "殿堂ポイント2005年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2005年度版";
                    break;
                case "2006":
                    sql = sql2006;
                    Label1.Text = "殿堂ポイント2006年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2006年度版";
                    break;
                case "2007":
                    sql = sql2007;
                    Label1.Text = "殿堂ポイント2007年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2007年度版";
                    break;
                case "2008":
                    sql = sql2008;
                    Label1.Text = "殿堂ポイント2008年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2008年度版";
                    break;
                case "2009":
                    sql = sql2009;
                    Label1.Text = "殿堂ポイント2009年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2009年度版";
                    break;
                case "2010":
                    sql = sql2010;
                    Label1.Text = "殿堂ポイント2010年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2010年度版";
                    break;
                case "2011":
                    sql = sql2011;
                    Label1.Text = "殿堂ポイント2011年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2011年度版";
                    break;
                case "2012":
                    sql = sql2012;
                    Label1.Text = "殿堂ポイント2012年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2012年度版";
                    break;
                case "2013":
                    sql = sql2013;
                    Label1.Text = "殿堂ポイント2013年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2013年度版";
                    break;
                case "2014":
                    sql = sql2014;
                    Label1.Text = "殿堂ポイント2014年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2014年度版";
                    break;
                case "2015":
                    sql = sql2015;
                    Label1.Text = "殿堂ポイント2015年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2015年度版";
                    break;
                case "2016":
                    sql = sql2016;
                    Label1.Text = "殿堂ポイント2016年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2016年度版";
                    break;
                case "2017":
                    sql = sql2017;
                    Label1.Text = "殿堂ポイント2017年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2017年度版";
                    break;
                case "2018":
                    sql = sql2018;
                    Label1.Text = "殿堂ポイント2018年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2018年度版";
                    break;
                case "2019":
                    sql = sql2019;
                    Label1.Text = "殿堂ポイント2019年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2019年度版";
                    break;
                case "2020":
                    sql = sql2020;
                    Label1.Text = "殿堂ポイント2020年度版";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント2020年度版";
                    break;
                default:
                    sql = sql12;
                    Label1.Text = "殿堂ポイント";
                    ptitle.Text = "殿堂ランキング・殿堂ポイント";
                    break;

            }

            using(var connection = new OracleConnection(strConnection))
            {
                connection.Open();
                using (var command = new OracleCommand(sql, connection))
                {
                    using (var adapter = command.ExecuteReader())
                    {
                        GridView1.DataSource = adapter;
                        GridView1.DataBind();
                    }
                }
                connection.Close();
            }

        }
    }
}