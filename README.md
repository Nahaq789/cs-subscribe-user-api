# 環境構築

## インストールするもの

- VS Code

## 拡張機能

- Dev Container
- Remote Explorer

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
