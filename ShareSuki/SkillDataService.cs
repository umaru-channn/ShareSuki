using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ShareSuki
{
    public class SkillDataService
    {
        private readonly EmailService _emailService;

        public SkillDataService()
        {
            _emailService = EmailConfig.CreateEmailService();
        }
        public List<SkillData> GetAllSkillData()
        {
            List<SkillData> skillDataList = new List<SkillData>();
            string query = "SELECT * FROM data ORDER BY 登録日 DESC";
            
            DataTable dt = DbHelper.ExecuteQuery(query);
            
            foreach (DataRow row in dt.Rows)
            {
                skillDataList.Add(ConvertDataRowToSkillData(row));
            }
            
            return skillDataList;
        }

        public SkillData GetSkillDataById(int id)
        {
            string query = "SELECT * FROM data WHERE ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", id) };
            
            DataTable dt = DbHelper.ExecuteQuery(query, parameters);
            
            if (dt.Rows.Count > 0)
            {
                return ConvertDataRowToSkillData(dt.Rows[0]);
            }
            
            return null;
        }

        public int AddSkillData(SkillData skillData)
        {
            string query = @"
                INSERT INTO data (学籍番号, クラス名, 出席番号, 性別, 氏名, メールアドレス, 求スキル, 求スキル備考, 譲スキル, 譲スキル備考, 待機期間)
                VALUES (@学籍番号, @クラス名, @出席番号, @性別, @氏名, @メールアドレス, @求スキル, @求スキル備考, @譲スキル, @譲スキル備考, @待機期間);
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@学籍番号", skillData.学籍番号),
                new SqlParameter("@クラス名", (object)skillData.クラス名 ?? DBNull.Value),
                new SqlParameter("@出席番号", (object)skillData.出席番号 ?? DBNull.Value),
                new SqlParameter("@性別", (object)skillData.性別 ?? DBNull.Value),
                new SqlParameter("@氏名", skillData.氏名),
                new SqlParameter("@メールアドレス", (object)skillData.メールアドレス ?? DBNull.Value),
                new SqlParameter("@求スキル", (object)skillData.求スキル ?? DBNull.Value),
                new SqlParameter("@求スキル備考", (object)skillData.求スキル備考 ?? DBNull.Value),
                new SqlParameter("@譲スキル", (object)skillData.譲スキル ?? DBNull.Value),
                new SqlParameter("@譲スキル備考", (object)skillData.譲スキル備考 ?? DBNull.Value),
                new SqlParameter("@待機期間", (object)skillData.待機期間 ?? DBNull.Value)
            };

            // 安全なトランザクション版を使用してデータを挿入
            object result = null;
            using (var connection = new System.Data.SqlClient.SqlConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["ShareSukiDBConnection"].ConnectionString))
            {
                System.Data.SqlClient.SqlTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    
                    using (var cmd = new System.Data.SqlClient.SqlCommand(query, connection, transaction))
                    {
                        cmd.Parameters.AddRange(parameters);
                        result = cmd.ExecuteScalar();
                        transaction.Commit();
                        Console.WriteLine($"新しいデータが正常に挿入されました。ID: {result}");
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    Console.WriteLine($"データ挿入エラー: {ex.Message}");
                    throw;
                }
            }
            
            int newId = Convert.ToInt32(result);

            // 新規登録時に自動でマッチング検出と通知を送信
            _ = Task.Run(() => CheckAndNotifyMatchingAsync(newId));

            return newId;
        }

        public void UpdateSkillData(SkillData skillData)
        {
            string query = @"
                UPDATE data SET 
                    学籍番号 = @学籍番号,
                    クラス名 = @クラス名,
                    出席番号 = @出席番号,
                    性別 = @性別,
                    氏名 = @氏名,
                    メールアドレス = @メールアドレス,
                    求スキル = @求スキル,
                    求スキル備考 = @求スキル備考,
                    譲スキル = @譲スキル,
                    譲スキル備考 = @譲スキル備考,
                    待機期間 = @待機期間
                WHERE ID = @ID";

            SqlParameter[] parameters = {
                new SqlParameter("@ID", skillData.ID),
                new SqlParameter("@学籍番号", skillData.学籍番号),
                new SqlParameter("@クラス名", (object)skillData.クラス名 ?? DBNull.Value),
                new SqlParameter("@出席番号", (object)skillData.出席番号 ?? DBNull.Value),
                new SqlParameter("@性別", (object)skillData.性別 ?? DBNull.Value),
                new SqlParameter("@氏名", skillData.氏名),
                new SqlParameter("@メールアドレス", (object)skillData.メールアドレス ?? DBNull.Value),
                new SqlParameter("@求スキル", (object)skillData.求スキル ?? DBNull.Value),
                new SqlParameter("@求スキル備考", (object)skillData.求スキル備考 ?? DBNull.Value),
                new SqlParameter("@譲スキル", (object)skillData.譲スキル ?? DBNull.Value),
                new SqlParameter("@譲スキル備考", (object)skillData.譲スキル備考 ?? DBNull.Value),
                new SqlParameter("@待機期間", (object)skillData.待機期間 ?? DBNull.Value)
            };

            // 安全なトランザクション版を使用してデータを更新
            bool success = DbHelper.UpdateDataSafely(query, parameters);
            if (!success)
            {
                throw new Exception("データの更新に失敗しました。");
            }
            
            // 更新時にもマッチング検出と通知を実行
            _ = Task.Run(() => CheckAndNotifyMatchingAsync(skillData.ID));
        }

        public void DeleteSkillData(int id)
        {
            string query = "DELETE FROM data WHERE ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", id) };
            
            DbHelper.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// 自動マッチングのための検索
        /// </summary>
        /// <param name="requestedSkill">求めているスキル</param>
        /// <param name="currentUserId">現在のユーザーID（自分を除外するため）</param>
        /// <returns>マッチするスキルデータのリスト</returns>
        public List<SkillData> FindMatchingSkills(string requestedSkill, int currentUserId)
        {
            List<SkillData> matches = new List<SkillData>();
            string query = "SELECT * FROM data WHERE 譲スキル = @求スキル AND ID != @現在ユーザーID";
            
            SqlParameter[] parameters = {
                new SqlParameter("@求スキル", requestedSkill),
                new SqlParameter("@現在ユーザーID", currentUserId)
            };
            
            DataTable dt = DbHelper.ExecuteQuery(query, parameters);
            
            foreach (DataRow row in dt.Rows)
            {
                matches.Add(ConvertDataRowToSkillData(row));
            }
            
            return matches;
        }

        /// <summary>
        /// 逆マッチング - 自分の譲スキルを求めている人を検索
        /// </summary>
        /// <param name="offeredSkill">提供するスキル</param>
        /// <param name="currentUserId">現在のユーザーID</param>
        /// <returns>マッチするスキルデータのリスト</returns>
        public List<SkillData> FindReverseMatchingSkills(string offeredSkill, int currentUserId)
        {
            List<SkillData> matches = new List<SkillData>();
            string query = "SELECT * FROM data WHERE 求スキル = @譲スキル AND ID != @現在ユーザーID";
            
            SqlParameter[] parameters = {
                new SqlParameter("@譲スキル", offeredSkill),
                new SqlParameter("@現在ユーザーID", currentUserId)
            };
            
            DataTable dt = DbHelper.ExecuteQuery(query, parameters);
            
            foreach (DataRow row in dt.Rows)
            {
                matches.Add(ConvertDataRowToSkillData(row));
            }
            
            return matches;
        }

        private SkillData ConvertDataRowToSkillData(DataRow row)
        {
            return new SkillData
            {
                ID = Convert.ToInt32(row["ID"]),
                学籍番号 = Convert.ToInt32(row["学籍番号"]),
                クラス名 = row["クラス名"] == DBNull.Value ? null : row["クラス名"].ToString(),
                出席番号 = row["出席番号"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["出席番号"]),
                性別 = row["性別"] == DBNull.Value ? null : row["性別"].ToString(),
                氏名 = row["氏名"].ToString(),
                メールアドレス = row.Table.Columns.Contains("メールアドレス") && row["メールアドレス"] != DBNull.Value ? row["メールアドレス"].ToString() : null,
                求スキル = row["求スキル"] == DBNull.Value ? null : row["求スキル"].ToString(),
                求スキル備考 = row["求スキル備考"] == DBNull.Value ? null : row["求スキル備考"].ToString(),
                譲スキル = row["譲スキル"] == DBNull.Value ? null : row["譲スキル"].ToString(),
                譲スキル備考 = row["譲スキル備考"] == DBNull.Value ? null : row["譲スキル備考"].ToString(),
                待機期間 = row["待機期間"] == DBNull.Value ? null : row["待機期間"].ToString(),
                登録日 = Convert.ToDateTime(row["登録日"])
            };
        }

        // 以下の機能は無効化（相互マッチング機能のみ使用）
        /*
        /// <summary>
        /// 手動でマッチング通知を送信
        /// </summary>
        /// <param name="fromUserId">送信者のユーザーID</param>
        /// <param name="toUserId">受信者のユーザーID</param>
        /// <returns></returns>
        public async Task<bool> SendManualMatchNotificationAsync(int fromUserId, int toUserId)
        {
            try
            {
                var fromUser = GetSkillDataById(fromUserId);
                var toUser = GetSkillDataById(toUserId);

                if (fromUser == null || toUser == null || string.IsNullOrEmpty(toUser.メールアドレス))
                {
                    return false;
                }

                return await _emailService.SendSkillMatchNotificationAsync(toUser, fromUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"手動通知送信エラー: {ex.Message}");
                return false;
            }
        }
        */

        /// <summary>
        /// マッチング成立の検出とメール通知を行います
        /// </summary>
        /// <param name="newUserId">新規登録または更新されたユーザーのID</param>
        /// <returns></returns>
        public async Task<List<SkillData>> CheckAndNotifyMatchingAsync(int newUserId)
        {
            var matchedUsers = new List<SkillData>();
            
            try
            {
                var newUser = GetSkillDataById(newUserId);
                if (newUser == null) return matchedUsers;

                // 相互マッチング（お互いが求めるスキルを提供できる関係）を検索
                var mutualMatches = FindMutualMatches(newUserId);
                
                foreach (var matchedUser in mutualMatches)
                {
                    // 相互マッチング成立通知メールを送信
                    bool emailSent = await _emailService.SendMutualMatchNotificationAsync(newUser, matchedUser);
                    
                    if (emailSent)
                    {
                        matchedUsers.Add(matchedUser);
                        
                        // ログ出力（実際の運用では適切なログ出力を行う）
                        Console.WriteLine($"相互マッチング成立通知送信完了: {newUser.氏名} ↔ {matchedUser.氏名}");
                    }
                }

                // 一方向のマッチング通知は無効化（相互マッチングのみ使用）
                // await SendOneWayMatchingNotificationsAsync(newUserId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"マッチング検出・通知エラー: {ex.Message}");
            }

            return matchedUsers;
        }

        /// <summary>
        /// 相互マッチング（お互いが求めるスキルを提供できる関係）を検索します
        /// </summary>
        /// <param name="userId">検索対象のユーザーID</param>
        /// <returns>相互マッチするユーザーのリスト</returns>
        public List<SkillData> FindMutualMatches(int userId)
        {
            var mutualMatches = new List<SkillData>();
            
            try
            {
                var user = GetSkillDataById(userId);
                if (user == null || string.IsNullOrEmpty(user.求スキル) || string.IsNullOrEmpty(user.譲スキル))
                {
                    return mutualMatches;
                }

                string query = @"
                    SELECT * FROM data 
                    WHERE ID != @現在ユーザーID 
                    AND 譲スキル = @求スキル 
                    AND 求スキル = @譲スキル
                    AND メールアドレス IS NOT NULL 
                    AND メールアドレス != ''";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@現在ユーザーID", userId),
                    new SqlParameter("@求スキル", user.求スキル),
                    new SqlParameter("@譲スキル", user.譲スキル)
                };
                
                DataTable dt = DbHelper.ExecuteQuery(query, parameters);
                
                foreach (DataRow row in dt.Rows)
                {
                    mutualMatches.Add(ConvertDataRowToSkillData(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"相互マッチング検索エラー: {ex.Message}");
            }
            
            return mutualMatches;
        }

        // 以下の機能は無効化（相互マッチング機能のみ使用）
        /*
        /// <summary>
        /// 一方向のマッチング通知を送信（既存機能を改名）
        /// </summary>
        /// <param name="newUserId">新規登録されたユーザーのID</param>
        private async Task SendOneWayMatchingNotificationsAsync(int newUserId)
        {
            try
            {
                var newUser = GetSkillDataById(newUserId);
                if (newUser == null) return;

                // 新規ユーザーが求めているスキルを提供できる人を検索
                if (!string.IsNullOrEmpty(newUser.求スキル))
                {
                    var matches = FindMatchingSkills(newUser.求スキル, newUserId);
                    foreach (var match in matches)
                    {
                        if (!string.IsNullOrEmpty(match.メールアドレス))
                        {
                            await _emailService.SendSkillMatchNotificationAsync(match, newUser);
                        }
                    }
                }

                // 新規ユーザーが提供できるスキルを求めている人を検索
                if (!string.IsNullOrEmpty(newUser.譲スキル))
                {
                    var reverseMatches = FindReverseMatchingSkills(newUser.譲スキル, newUserId);
                    foreach (var match in reverseMatches)
                    {
                        if (!string.IsNullOrEmpty(match.メールアドレス))
                        {
                            await _emailService.SendSkillMatchNotificationAsync(match, newUser);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"一方向マッチング通知送信エラー: {ex.Message}");
            }
        }
        */

        /// <summary>
        /// 手動でマッチング検出とメール通知を実行
        /// </summary>
        /// <param name="userId">対象ユーザーのID</param>
        /// <returns>マッチしたユーザーのリスト</returns>
        public async Task<List<SkillData>> ManualMatchingAsync(int userId)
        {
            return await CheckAndNotifyMatchingAsync(userId);
        }

        /// <summary>
        /// 全ユーザーに対してマッチング検出を実行（管理者向け機能）
        /// </summary>
        /// <returns>マッチング成立数</returns>
        public async Task<int> RunFullMatchingAsync()
        {
            int matchCount = 0;
            
            try
            {
                var allUsers = GetAllSkillData();
                
                foreach (var user in allUsers)
                {
                    if (!string.IsNullOrEmpty(user.メールアドレス))
                    {
                        var matches = await CheckAndNotifyMatchingAsync(user.ID);
                        matchCount += matches.Count;
                    }
                    
                    // 過負荷を防ぐため少し待機
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"全体マッチング実行エラー: {ex.Message}");
            }
            
            return matchCount;
        }
    }
}
