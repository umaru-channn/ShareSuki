# ShareSuki - メール送信機能について

## 概要
ShareSukiアプリケーションにMailKitを使用したメール送信機能を追加しました。

## 追加された機能

### 1. メールアドレス管理
- SkillDataテーブルにメールアドレス列を追加
- 新規登録・編集時にメールアドレスを入力可能

### 2. 自動マッチング通知
- 新規登録時に、スキルがマッチする既存ユーザーに自動でメール通知
- 双方向マッチング（求スキル⇔譲スキル）対応

### 3. メール送信テスト機能
- メインフォームの「メールテスト」ボタンから送信テスト可能
- SMTP設定の確認・テスト送信が可能

## セットアップ手順

### 1. データベース更新
`AddEmailColumn.sql`を実行してメールアドレス列を追加：

```sql
-- ShareSukiDBのSkillDataテーブルにメールアドレス列を追加
USE ShareSukiDB;
GO

-- メールアドレス列を追加（まだ存在しない場合）
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'SkillData' AND COLUMN_NAME = 'メールアドレス')
BEGIN
    ALTER TABLE SkillData
    ADD メールアドレス NVARCHAR(255) NULL;
END
GO
```

### 2. メール設定
`App.config`でSMTP設定を更新：

```xml
<appSettings>
    <!-- メール設定 -->
    <add key="SmtpServer" value="smtp.gmail.com" />
    <add key="SmtpPort" value="587" />
    <add key="FromEmail" value="your-email@gmail.com" />
    <add key="FromName" value="ShareSuki" />
    <add key="EmailUsername" value="your-email@gmail.com" />
    <add key="EmailPassword" value="your-app-password" />
</appSettings>
```

### 3. Gmail設定（Gmail使用の場合）

1. **2段階認証を有効化**：
   - Googleアカウントの「セキュリティ」設定で2段階認証を有効にする

2. **アプリパスワードを生成**：
   - Googleアカウント → セキュリティ → 2段階認証 → アプリパスワード
   - 「アプリを選択」→「メール」、「デバイスを選択」→「その他」→「ShareSuki」
   - 生成された16桁のパスワードを`EmailPassword`に設定

3. **設定例**：
   ```xml
   <add key="SmtpServer" value="smtp.gmail.com" />
   <add key="SmtpPort" value="587" />
   <add key="FromEmail" value="yourname@gmail.com" />
   <add key="EmailUsername" value="yourname@gmail.com" />
   <add key="EmailPassword" value="abcd efgh ijkl mnop" />
   ```

### 4. 他のメールプロバイダー設定例

#### Outlook.com / Hotmail
```xml
<add key="SmtpServer" value="smtp-mail.outlook.com" />
<add key="SmtpPort" value="587" />
```

#### Yahoo Mail
```xml
<add key="SmtpServer" value="smtp.mail.yahoo.com" />
<add key="SmtpPort" value="587" />
```

## 使用方法

### 1. 基本的なメール送信
- メインフォームの「メールテスト」ボタンをクリック
- SMTP設定と送信先を入力
- 「送信」ボタンでテスト送信

### 2. 自動マッチング通知
- 新規スキル登録時に自動実行
- マッチした相手のメールアドレスが登録されていれば自動送信

### 3. 手動マッチング通知
```csharp
// コード例：手動で特定のユーザーに通知を送信
var skillDataService = new SkillDataService();
bool success = await skillDataService.SendManualMatchNotificationAsync(fromUserId, toUserId);
```

## トラブルシューティング

### よくあるエラー

1. **認証エラー**：
   - Gmail: アプリパスワードを使用しているか確認
   - 2段階認証が有効になっているか確認

2. **SMTP接続エラー**：
   - ファイアウォール設定の確認
   - SMTP サーバー・ポート番号の確認

3. **メール送信失敗**：
   - 送信制限（1日の送信数）の確認
   - メールアドレスの形式確認

### デバッグ方法
1. メールテストフォームで基本的な送信をテスト
2. App.configの設定値を確認
3. エラーメッセージを確認（コンソール出力やMessageBox）

## セキュリティ注意事項

1. **パスワード管理**：
   - App.configファイルを公開リポジトリにコミットしない
   - 本番環境では設定ファイルを適切に保護する

2. **メール内容**：
   - 個人情報の送信時は注意
   - SSL/TLS接続（StartTls）を使用

3. **送信制限**：
   - 過度な送信を避ける
   - プロバイダーの送信制限を遵守

## 参考資料

- [MailKit公式ドキュメント](http://www.mimekit.net/docs/)
- [Microsoft .NET TIPS - MailKit編](https://atmarkit.itmedia.co.jp/ait/articles/1811/21/news023.html)
- [Gmail アプリパスワード設定](https://support.google.com/accounts/answer/185833)
