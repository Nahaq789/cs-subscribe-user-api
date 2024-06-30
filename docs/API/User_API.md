# User_API 仕様書

## 概要

### 目的

この API は、ユーザの登録や編集を行います。

### アーキテクチャ概要

DDD + CQRS + Mediator

## 認証と認可

トークン方式

### 認証方式

- トークン による認証
- 各リクエストに JWT トークンとリフレッシュトークン をヘッダーに含める
- API 側でトークンを復元する。

## エンドポイント

Route: api/v1/User/[controller 名]

- create
- update

#### リクエスト

- `/api/v1/create`
- `/api/v1/update`

- URL: `/api/v1/create`
- メソッド: POST
- パラメータ:
  - `command`: ユーザー作成コマンド
  - `requestId`: リクエスト ID
  - `Authorization`: Bearer JWT トークン

```
{
    "name": "satoshi56",
    "email": "satoshi@example.com",
    "password": "satoshi",
    "age": 10
}
```

---

- URL: `/api/v1/update`
- メソッド: POST
- パラメータ:
  - `command`: ユーザーアップデートコマンド
  - `requestId`: リクエスト ID
  - `Authorization`: Bearer JWT トークン

```
{
    "aggregateId": "dd102ae6-0b82-4517-a590-abc9bd64c124",
    "name": "pikachu",
    "age": 11
}
```
