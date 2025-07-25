using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace ShareSuki
{
    public class DbHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["ShareSukiDBConnection"].ConnectionString;

        /// <summary>
        /// データベース接続をテストします
        /// </summary>
        /// <returns>接続成功時はtrue、失敗時はfalse</returns>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine($"データベース接続成功: {conn.State}");
                    return conn.State == ConnectionState.Open;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"データベース接続テストエラー: {ex.Message}");
                Console.WriteLine($"接続文字列: {connectionString}");
                return false;
            }
        }

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
            try
            {
                // データベースファイルの存在確認とログ出力
                LogDatabaseFileLocation();
                
                // 接続テスト
                if (!TestConnection())
                {
                    throw new Exception("データベースに接続できません。");
                }

                // まず、既存のテーブル構造をチェックして、問題があれば修復
                FixTableStructureIfNeeded();

                string createTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='data' AND xtype='U')
                    CREATE TABLE data (
                        ID INT IDENTITY(1,1) PRIMARY KEY,
                        学籍番号 INT NOT NULL,
                        クラス名 NVARCHAR(50) NULL,
                        出席番号 INT NULL,
                        性別 NVARCHAR(10) NULL,
                        氏名 NVARCHAR(100) NOT NULL,
                        メールアドレス NVARCHAR(100) NULL,
                        求スキル NVARCHAR(100) NULL,
                        求スキル備考 NVARCHAR(MAX) NULL,
                        譲スキル NVARCHAR(100) NULL,
                        譲スキル備考 NVARCHAR(MAX) NULL,
                        待機期間 NVARCHAR(50) NULL,
                        登録日 DATETIME DEFAULT GETDATE()
                    );";

                ExecuteNonQuery(createTableQuery);
                
                // データ件数を確認
                int dataCount = GetDataCount();
                Console.WriteLine($"データベースの初期化が完了しました。現在のデータ件数: {dataCount}");
            }
            catch (Exception ex)
            {
                throw new Exception($"データベースの初期化中にエラーが発生しました: {ex.Message}");
            }
        }

        /// <summary>
        /// テーブル構造に問題がある場合の修復処理
        /// </summary>
        private static void FixTableStructureIfNeeded()
        {
            try
            {
                // テーブルが存在するかチェック
                string checkTableQuery = "SELECT COUNT(*) FROM sysobjects WHERE name='data' AND xtype='U'";
                object result = ExecuteScalar(checkTableQuery);
                
                if (Convert.ToInt32(result) > 0)
                {
                    // メールアドレスカラムが存在しない場合のみ追加
                    string checkColumnQuery = "SELECT COUNT(*) FROM sys.columns WHERE object_id = OBJECT_ID('data') AND name = 'メールアドレス'";
                    object columnResult = ExecuteScalar(checkColumnQuery);
                    
                    if (Convert.ToInt32(columnResult) == 0)
                    {
                        string addColumnQuery = "ALTER TABLE data ADD メールアドレス NVARCHAR(100) NULL";
                        ExecuteNonQuery(addColumnQuery);
                        Console.WriteLine("メールアドレスカラムを追加しました。");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"テーブル構造修復中にエラー: {ex.Message}");
                // エラーが発生した場合は、既存のテーブルをバックアップして再作成することを検討
                // 但し、ここでは既存データを保持するため、警告のみ出力
            }
        }

        /// <summary>
        /// データベースファイルの場所をログ出力します
        /// </summary>
        private static void LogDatabaseFileLocation()
        {
            try
            {
                string databaseFilePath = @"C:\Users\it222184.TSITCL\source\repos\ShareSuki\ShareSuki\ShareSukiDB.mdf";
                string logFilePath = @"C:\Users\it222184.TSITCL\source\repos\ShareSuki\ShareSuki\ShareSukiDB_log.ldf";

                Console.WriteLine($"データベースファイルパス:");
                Console.WriteLine($"  MDF: {databaseFilePath}");
                Console.WriteLine($"  LDF: {logFilePath}");
                Console.WriteLine($"  MDF存在: {File.Exists(databaseFilePath)}");
                Console.WriteLine($"  LDF存在: {File.Exists(logFilePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"データベースファイル場所確認エラー: {ex.Message}");
            }
        }

        /// <summary>
        /// データベースのバックアップを作成します
        /// </summary>
        /// <param name="backupPath">バックアップファイルの保存先パス</param>
        /// <returns>バックアップ成功時はtrue</returns>
        public static bool CreateBackup(string backupPath = null)
        {
            try
            {
                if (string.IsNullOrEmpty(backupPath))
                {
                    string appDataPath = AppDomain.CurrentDomain.BaseDirectory;
                    backupPath = Path.Combine(appDataPath, $"ShareSukiDB_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak");
                }

                string backupQuery = $"BACKUP DATABASE ShareSukiDB TO DISK = '{backupPath}'";
                ExecuteNonQuery(backupQuery);
                
                Console.WriteLine($"データベースのバックアップが完了しました: {backupPath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"データベースバックアップエラー: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 登録済みデータの数を取得します
        /// </summary>
        /// <returns>データ件数</returns>
        public static int GetDataCount()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM data";
                object result = ExecuteScalar(query);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"データ件数取得エラー: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// データを安全に挿入します（トランザクション使用）
        /// </summary>
        public static bool InsertDataSafely(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        transaction.Commit();
                        
                        Console.WriteLine($"データが正常に挿入されました。影響を受けた行数: {rowsAffected}");
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    Console.WriteLine($"データ挿入エラー: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// データを安全に更新します（トランザクション使用）
        /// </summary>
        public static bool UpdateDataSafely(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        transaction.Commit();
                        
                        Console.WriteLine($"データが正常に更新されました。影響を受けた行数: {rowsAffected}");
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    Console.WriteLine($"データ更新エラー: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// アプリケーション終了時のクリーンアップ処理
        /// </summary>
        public static void CleanupDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // データベース接続を明示的にクローズ
                    conn.Close();
                }
                Console.WriteLine("データベースのクリーンアップが完了しました。");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"データベースクリーンアップエラー: {ex.Message}");
            }
        }
    }
}
