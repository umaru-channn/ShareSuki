using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ShareSuki
{
    public class DbHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["ShareSukiDBConnection"].ConnectionString;

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object result = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    result = cmd.ExecuteScalar();
                }
            }
            return result;
        }

        /// <summary>
        /// データベースを初期化し、必要なテーブルを作成します
        /// </summary>
        public static void InitializeDatabase()
        {
            string createTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='data' AND xtype='U')
                CREATE TABLE data (
                    ID INT IDENTITY(1,1) PRIMARY KEY,
                    学籍番号 INT NOT NULL,
                    クラス名 NVARCHAR(50) NULL,
                    出席番号 INT NULL,
                    性別 NVARCHAR(10) NULL,
                    氏名 NVARCHAR(100) NOT NULL,
                    求スキル NVARCHAR(100) NULL,
                    求スキル備考 NVARCHAR(MAX) NULL,
                    譲スキル NVARCHAR(100) NULL,
                    譲スキル備考 NVARCHAR(MAX) NULL,
                    待機期間 NVARCHAR(50) NULL,
                    登録日 DATETIME DEFAULT GETDATE()
                );
                
                ";

            try
            {
                ExecuteNonQuery(createTableQuery);
            }
            catch (Exception ex)
            {
                throw new Exception("データベースの初期化中にエラーが発生しました: " + ex.Message);
            }
        }
    }
}
