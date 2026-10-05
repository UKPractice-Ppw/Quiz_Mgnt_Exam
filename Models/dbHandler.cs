using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace QuizMgnt.Models
{
    public class dbHandler
    {
        SqlConnection con;
        private void Connection()
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            con = new SqlConnection(strcon);
        }

        public bool Add_Quiz(Quiz qz)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("INSERT INTO tblQuiz VALUES(@quiz_title,@cat_id,@total_marks)", con);
            cmd.Parameters.AddWithValue("@quiz_title", qz.quiz_title);
            cmd.Parameters.AddWithValue("@cat_id", qz.cat_id);
            cmd.Parameters.AddWithValue("@total_marks", qz.total_marks);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i >= 1;
        }

        public DataTable ddlQuery(string qry)
        {
            Connection();
            SqlCommand cmd = new SqlCommand(qry, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            con.Open();
            sda.Fill(dt);
            con.Close();
            return dt;
        }

        public List<Quiz> GetQuizList(int? cat_id, string quiz_title)
        {
            Connection();
            List<Quiz> qz = new List<Quiz>();
            string qry = "SELECT q.quiz_id, q.quiz_title, q.cat_id, qc.cat_type, q.total_marks " +
                         "FROM tblQuiz q INNER JOIN tblQuizCat qc ON q.cat_id = qc.cat_id WHERE 1=1";

            SqlCommand cmd = new SqlCommand();
            if (cat_id != null && cat_id.Value > 0)
            {
                qry += " AND q.cat_id = @cat_id";
                cmd.Parameters.AddWithValue("@cat_id", cat_id.Value);
            }
            if (!string.IsNullOrWhiteSpace(quiz_title))
            {
                qry += " AND q.quiz_title LIKE @quiz_title";
                cmd.Parameters.AddWithValue("@quiz_title", "%" + quiz_title.Trim() + "%");
            }
            cmd.CommandText = qry;
            cmd.Connection = con;
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Quiz quiz = new Quiz
                {
                    quiz_id = Convert.ToInt32(sdr["quiz_id"]),
                    quiz_title = sdr["quiz_title"].ToString(),
                    cat_id = Convert.ToInt32(sdr["cat_id"]),
                    total_marks = Convert.ToInt32(sdr["total_marks"]),
                    cat_type = sdr["cat_type"].ToString()
                };
                qz.Add(quiz);
            }
            con.Close();
            return qz;
        }
    }
}