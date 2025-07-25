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

### 4. マッチング成立時の相互通知機能（新機能）
- 相互マッチング（お互いが求めるスキルを提供できる関係）が成立した場合
- **両方のユーザー**に相手の詳細情報を含むメールを自動送信
- 連絡先情報とスキル詳細を含む包括的な通知

### 5. 手動マッチング実行機能
- 管理者が全ユーザーに対してマッチング検出を実行可能
- 特定のユーザーに対して手動でマッチング検出を実行可能

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

### 4. 相互マッチング通知（新機能）
```csharp
// コード例：手動で相互マッチング検出を実行
var skillDataService = new SkillDataService();
var matchedUsers = await skillDataService.ManualMatchingAsync(userId);

Console.WriteLine($"{matchedUsers.Count}件の相互マッチングが見つかりました。");
```

### 5. 全ユーザーマッチング実行（管理者向け）
```csharp
// コード例：全ユーザーに対してマッチング検出を実行
var skillDataService = new SkillDataService();
int totalMatches = await skillDataService.RunFullMatchingAsync();

Console.WriteLine($"合計{totalMatches}件のマッチングが成立しました。");
```

### 6. マッチング成立のメール内容
マッチング成立時に送信されるメールには以下の情報が含まれます：
- 相手の基本情報（氏名、学籍番号、クラス、メールアドレス）
- 相手が提供可能なスキルと詳細
- 相手が求めているスキルと詳細
- 対応可能期間
- コミュニケーションのポイント

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
