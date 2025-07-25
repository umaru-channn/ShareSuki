using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareSuki
{
    public partial class DataViewForm : Form
    {
        private SkillDataService _skillDataService;

        public DataViewForm()
        {
            InitializeComponent();
            _skillDataService = new SkillDataService();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<SkillData> skillDataList = _skillDataService.GetAllSkillData();
                
                // DataGridViewに表示するためのDataTableを作成
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("学籍番号", typeof(int));
                dt.Columns.Add("クラス名", typeof(string));
                dt.Columns.Add("出席番号", typeof(string));
                dt.Columns.Add("性別", typeof(string));
                dt.Columns.Add("氏名", typeof(string));
                dt.Columns.Add("求スキル", typeof(string));
                dt.Columns.Add("求スキル備考", typeof(string));
                dt.Columns.Add("譲スキル", typeof(string));
                dt.Columns.Add("譲スキル備考", typeof(string));
                dt.Columns.Add("待機期間", typeof(string));
                dt.Columns.Add("登録日", typeof(DateTime));

                foreach (var skill in skillDataList)
                {
                    dt.Rows.Add(
                        skill.ID,
                        skill.学籍番号,
                        skill.クラス名,
                        skill.出席番号?.ToString(),
                        skill.性別,
                        skill.氏名,
                        skill.求スキル,
                        skill.求スキル備考,
                        skill.譲スキル,
                        skill.譲スキル備考,
                        skill.待機期間,
                        skill.登録日
                    );
                }

                dataGridView1.DataSource = dt;
                
                // カラム幅の調整
                dataGridView1.Columns["ID"].Width = 50;
                dataGridView1.Columns["学籍番号"].Width = 80;
                dataGridView1.Columns["クラス名"].Width = 80;
                dataGridView1.Columns["出席番号"].Width = 70;
                dataGridView1.Columns["性別"].Width = 50;
                dataGridView1.Columns["氏名"].Width = 100;
                dataGridView1.Columns["求スキル"].Width = 100;
                dataGridView1.Columns["求スキル備考"].Width = 120;
                dataGridView1.Columns["譲スキル"].Width = 100;
                dataGridView1.Columns["譲スキル備考"].Width = 120;
                dataGridView1.Columns["待機期間"].Width = 100;
                dataGridView1.Columns["登録日"].Width = 120;

                lblRecordCount.Text = $"登録件数: {skillDataList.Count}件";
            }
            catch (Exception ex)
            {
                MessageBox.Show("データの読み込み中にエラーが発生しました: " + ex.Message, 
                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("削除するデータを選択してください。", 
                    "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("選択されたデータを削除しますか？", 
                "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);
                    _skillDataService.DeleteSkillData(selectedId);
                    MessageBox.Show("データが削除されました。", 
                        "削除完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // データを再読み込み
                }
                catch (Exception ex)
                {
                    MessageBox.Show("データの削除中にエラーが発生しました: " + ex.Message, 
                        "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                LoadData(); // 検索文字列が空の場合は全データを表示
                return;
            }

            try
            {
                List<SkillData> skillDataList = _skillDataService.GetAllSkillData();
                
                // 検索フィルタリング
                var filteredData = skillDataList.Where(skill => 
                    skill.氏名.Contains(searchText) ||
                    (skill.求スキル != null && skill.求スキル.Contains(searchText)) ||
                    (skill.譲スキル != null && skill.譲スキル.Contains(searchText)) ||
                    (skill.クラス名 != null && skill.クラス名.Contains(searchText))
                ).ToList();

                // 検索結果をDataGridViewに表示
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("学籍番号", typeof(int));
                dt.Columns.Add("クラス名", typeof(string));
                dt.Columns.Add("出席番号", typeof(string));
                dt.Columns.Add("性別", typeof(string));
                dt.Columns.Add("氏名", typeof(string));
                dt.Columns.Add("求スキル", typeof(string));
                dt.Columns.Add("求スキル備考", typeof(string));
                dt.Columns.Add("譲スキル", typeof(string));
                dt.Columns.Add("譲スキル備考", typeof(string));
                dt.Columns.Add("待機期間", typeof(string));
                dt.Columns.Add("登録日", typeof(DateTime));

                foreach (var skill in filteredData)
                {
                    dt.Rows.Add(
                        skill.ID,
                        skill.学籍番号,
                        skill.クラス名,
                        skill.出席番号?.ToString(),
                        skill.性別,
                        skill.氏名,
                        skill.求スキル,
                        skill.求スキル備考,
                        skill.譲スキル,
                        skill.譲スキル備考,
                        skill.待機期間,
                        skill.登録日
                    );
                }

                dataGridView1.DataSource = dt;
                lblRecordCount.Text = $"検索結果: {filteredData.Count}件 (全{skillDataList.Count}件中)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("検索中にエラーが発生しました: " + ex.Message, 
                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
