using System;

namespace ShareSuki
{
    public class SkillData
    {
        public int ID { get; set; }
        public int 学籍番号 { get; set; }
        public string クラス名 { get; set; }
        public int? 出席番号 { get; set; }
        public string 性別 { get; set; }
        public string 氏名 { get; set; }
        public string メールアドレス { get; set; }
        public string 求スキル { get; set; }
        public string 求スキル備考 { get; set; }
        public string 譲スキル { get; set; }
        public string 譲スキル備考 { get; set; }
        public string 待機期間 { get; set; }
        public DateTime 登録日 { get; set; }
    }
}
