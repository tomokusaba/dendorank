using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication2.DendoRank
{
    public class ValuesController : ApiController
    {
        public class Rank {
            public string Name { get; set; }
            public int Tokuten { get; set; }
            public int Yuu { get; set; }
            public int Ryou { get; set; }
            public int Ka { get; set; }
        }
        // GET api/<controller>
        public IEnumerable<Rank> Get()
        {
            const string sql = "select a_name 制作者名, hoshi 得点, (select count(hyoka_num) from chart where author_name = a_name and  hyoka_num = 2) 優, (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 1) 良 , (select count(hyoka_num) from chart where author_name = a_name and hyoka_num = 0) 可 from( select sum( case hyoka_num when 0 then 1 when 1 then 2 when 2 then 4 end  ) hoshi, author_name a_name   from chart group by author_name order by hoshi desc) order by 得点 desc,a_name";
            const string strConnection = "Data Source=XE2"
+ ";User Id=system;Password=hy76cjs9";
            List<Rank> ranks = new List<Rank>();

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
                            //List<string[]> lg = new List<string[]>();
                            //int rows = 0;
                            while (adapter.Read())
                            {
                                //if (rows == 0)
                                //{
                                    //string[] title = new string[adapter.FieldCount];
                                    //for (int i = 0; i < adapter.FieldCount; i++)
                                    //{
                                    //    title[i] = adapter.GetName(i).ToString();
                                    //}
                                    //lg.Add(title);
                                //}
                                Rank rank = new Rank();
                                rank.Name = adapter.GetString(0);
                                rank.Tokuten = adapter.GetInt32(1);
                                rank.Yuu = adapter.GetInt32(2);
                                rank.Ryou = adapter.GetInt32(3);
                                rank.Ka = adapter.GetInt32(4);
                                ranks.Add(rank);
                                //rows++;
                            }
                            //grid = lg.ToArray();
                        }
                    }
                }
                connection.Close();
            }

            return ranks;

        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}