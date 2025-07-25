using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ShareSuki
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try
            {
                // アプリケーション起動時にデータベースを初期化
                DbHelper.InitializeDatabase();
                
                // アプリケーション終了時のイベントハンドラを設定
                Application.ApplicationExit += Application_ApplicationExit;
                
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"アプリケーションの起動中にエラーが発生しました:\n\n{ex.Message}", 
                    "起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// アプリケーション終了時の処理
        /// </summary>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                // データベースのクリーンアップ処理を実行
                DbHelper.CleanupDatabase();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"アプリケーション終了時のクリーンアップエラー: {ex.Message}");
            }
        }
    }
}
