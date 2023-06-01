using AdminSiteAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AdminSiteAPI.Controllers
{
    [ApiController]
    public class AdminController : Controller
    {
        [HttpGet]
        [Route("api/Admin/getUserList")]
        public List<User> Index()
        {
            List<User> users = new List<User>();
            //users.Add(new User { })
            SqlConnection con = new SqlConnection(@"Data Source=JOSWINDSO-LT;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Users", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                users.Add(new User
                {
                    id = dr["id"].ToString(),
                    username = dr["username"].ToString(),
                    companyID = dr["CompanyID"].ToString(),
                    companyName = dr["CompanyName"].ToString(),
                    usertype = dr["usertype"].ToString()
                });
            }
            return users;

        }
        [HttpGet]
        [Route("api/Admin/getuserListbyID/{id}")]
        public User getuserListbyID(int id)
        {
            User usr = new User();
            SqlConnection con = new SqlConnection(@"Data Source=JOSWINDSO-LT;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Users where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                usr.id = dr["id"].ToString();
                usr.username = dr["username"].ToString();
                usr.companyID = dr["companyID"].ToString();
                usr.companyName = dr["companyName"].ToString();
                usr.usertype = dr["usertype"].ToString();
            }
            return usr;
        }

        [HttpPost]
        [Route("api/Admin/addUser")]
        public int addUser([FromBody] User usr)
        {
            SqlConnection con = new SqlConnection(@"Data Source=JOSWINDSO-LT;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            string sqlStr = "insert into Users values(@id,@username,@companyID,@companyName,@userType)";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            cmd.Parameters.AddWithValue("@id", usr.id);
            cmd.Parameters.AddWithValue("@username", usr.username);
            cmd.Parameters.AddWithValue("@companyID", usr.companyID);
            cmd.Parameters.AddWithValue("@companyName", usr.companyName);
            cmd.Parameters.AddWithValue("@userType", usr.usertype);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        [HttpGet]
        [Route("api/Admin/deleteUser/{id}")]
        public int deleteUser(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=JOSWINDSO-LT;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            string sqlStr = "delete from Users where id=@id";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            var usr = getuserListbyID(id);
            cmd.Parameters.AddWithValue("@id", usr.id);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

    }
}
