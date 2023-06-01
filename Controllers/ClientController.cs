using AdminSiteAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AdminSiteAPI.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet]
        [Route("api/Admin/getClientList")]
        public List<Client> Index()
        {
            List<Client> client = new List<Client>();
            //users.Add(new User { })
            SqlConnection con = new SqlConnection(@"Data Source=JOSWINDSO-LT;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Client", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                client.Add(new Client
                {
                    code = dr["code"].ToString(),
                    name = dr["name"].ToString(),
                    permission = dr["permission"].ToString(),
                    annualSalary = dr["annualSalary"].ToString(),
                    dateOfBirth = dr["dateOfBirth"].ToString()
                });
            }
            return client;

        }
        [HttpGet]
        [Route("api/Admin/getClientListbyID/{code}")]
        public Client getclientListbyID(int code)
        {
            Client clnt = new Client();
            SqlConnection con = new SqlConnection(@"Data Source=JOSWINDSO-LT;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Client where code=@code", con);
            cmd.Parameters.AddWithValue("@code", code);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                clnt.code = dr["code"].ToString();
                clnt.name = dr["name"].ToString();
                clnt.permission = dr["permission"].ToString();
                clnt.annualSalary = dr["annualSalary"].ToString();
                clnt.dateOfBirth = dr["dateOfBirth"].ToString();
            }
            return clnt;
        }

        [HttpGet]
        [Route("api/Admin/deleteClient/{code}")]
        public int deleteClient(int code)
        {
            SqlConnection con = new SqlConnection(@"Data Source=JOSWINDSO-LT;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            string sqlStr = "delete from Client where code=@code";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            var clnt = getclientListbyID(code);
            cmd.Parameters.AddWithValue("@code", clnt.code);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}
