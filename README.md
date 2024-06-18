# 環境構築

## インストールするもの

- VS Code

## 拡張機能

- Dev Container
- Remote Explorer
- C#
- C# dev kit

# .NET 起動方法

1. リポジトリーをクローンする
   ```bash
   git clone https://github.com/Nahaq789/cs-subscribe-user-api.git
   ```
2. コンテナ起動

   ```bash
   cd ./.devcontainer
   docker compose up --build
   ```

3. Remote Explorer で起動したコンテナに入る。

   ```bash
   dotnet --version
   ```

4. 起動確認
   ```bash
   8.0.300 と表示されれば OK
   ```

# マイグレーション

1. User.API ディレクトリで以下のコマンドを実行

   ```bash
   dotnet ef migrations add init --project ../User.Infrastructure/User.Infrastructure.csproj
   ```

   Migration フォルダが作成される。

2. 作成された内容を DB に反映する

   ```bash
   dotnet ef database update --project ../User.Infrastructure/User.Infrastructure.csproj
   ```

# マイグレーション削除方法

1. DB で以下のコマンドを実行しマイグレーションの履歴を削除する

   ```
   delete from "__EFMigrationsHistory"
   ```

   特定のマイグレーションを削除したい場合は、マイグレーション ID を指定する

2. User.API ディレクトリで以下のコマンドを実行

   ```
   dotnet ef migrations remove --project ../User.Infrastructure/User.Infrastructure.csproj
   ```
