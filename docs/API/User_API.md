# User_API 仕様書

## 概要

### 目的

この API は、ユーザの登録やログインの処理を行います。

### アーキテクチャ概要

DDD + CQRS

## 認証と認可

トークン方式

### 認証方式

- トークン による認証
  - 各リクエストに トークン をヘッダーに含める
  - API側でトークンを復元する。

## エンドポイント

Route: api/v1/[controller 名]

- login
- logout
- create
- update
- delete
- get

#### リクエスト

- `GET /api/v1/get/{id}`
- `POST /api/v1/login`
- `POST /api/v1/logout`
- `POST /api/v1/create`
- `PUT /api/v1/update`
- `DELETE /api/v1/delete`

#### パラメータ

#### get

| key | value |
| --- | ----- |
| id  | TD    |

#### レスポンス

```json
[
  {
    "id": 1,
    "name": "山田太郎",
    "tel": "090-1234-5678",
    "email": "taro@example.com"
  },
  {
    "id": 2,
    "name": "鈴木花子",
    "tel": "080-9876-5432",
    "email": "hanako@example.com"
  }
]
```