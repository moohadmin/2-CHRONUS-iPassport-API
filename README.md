# Mootech - IPassport API [ipassport-api]

[![GitHub to AWS CI Pipeline](https://github.com/moohadmin/ipassport-api/actions/workflows/githubaws-ci.yml/badge.svg)](https://github.com/moohadmin/ipassport-api/actions/workflows/githubaws-ci.yml)

## Environment Variables

| Local name                       | AWS secret counterpart (click for format exemple)                   | Required | Default value |
| -------------------------------- | ------------------------------------------------------------------- | -------- | ------------- |
| ASPNETCORE_ENVIRONMENT           |                                                                     |          | Development   |
| DATABASE_CONNECTION_STRING       | [AWS_SECRET_DATABASE](#AWS-Secret-Database-Configuration)           | X        |               |
| STORAGE_S3_BUCKET_NAME           | [AWS_SECRET_S3](#AWS-Secret-S3-Object-Storage-Configuration)        | X        |               |
| AWS_ACCESS_KEY_ID                | [AWS_SECRET_S3](#AWS-Secret-S3-Object-Storage-Configuration)        | X        |               |
| AWS_SECRET_ACCESS_KEY            | [AWS_SECRET_S3](#AWS-Secret-S3-Object-Storage-Configuration)        | X        |               |
| AWS_DEFAULT_REGION               | -                                                                   |          | sa-east-1     |
| NOTIFICATIONS_MOCK               | -                                                                   |          | false         |
| NOTIFICATIONS_BASE_URL           | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_CLIENT_ID          | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_CLIENT_SECRET      | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_GRANT_TYPE         | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_AUTHENTICATION_KEY | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_FROM_NUMBER        | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_GET_TOKEN          | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_SEND_API_URL       | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_GET_API_URL        | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| SECRET_JWT_TOKEN                 | [AWS_SECRET_JWT_TOKEN](#AWS-Secret-for-JWT-Configuration)           | X        |               |

> If a variable is marked as required, and has a **local** name and a **AWS Secret counterpart**, one or another must be filled. In this case of boith are filled the read sequence will ignore **AWS Secret counterpart**.

> For configuration of the environment variables on VS Code head to section [VS Code environment variables configuration](#VS-Code-environment-variables-configuration)

### AWS Secret Database Configuration

> From environment variable: AWS_SECRET_DATABASE

```json
{
    "username": "passport",
    "password": "1234",
    "engine": "postgres",
    "host": "localhost",
    "port": 5432,
    "db_name": "passport"
}
```

### AWS Secret S3 Object Storage Configuration

> From environment variable: AWS_SECRET_S3

```json
{
    "bucket_name": "chronus-docs",
    "aws_access_key": "*****",
    "aws_secret": "*****"
}
```

### AWS Secret Notifications Configuration

> From environment variable: AWS_SECRET_NOTIFICATIONS

```json
{
    "base_url": "https://xxxxx.api.infobip.com",
    "client_id": "*****",
    "client_secret": "*****",
    "grant_type": "client_credentials",
    "authentication_key": "*****",
    "from_number": "IPassport",
    "get_token": "/auth/1/oauth2/token",
    "send_api_url": "/sms/2/text/advanced",
    "get_api_url": "/sms/1/reports"
}
```

### AWS Secret for JWT Configuration

> From environment variable: AWS_SECRET_JWT_TOKEN

```json
{
    "jwt_token": "*****"
}
```

### VS Code environment variables configuration

Just put the following snippet on yout `<project-root>/.vscode/launch.json` file. Replace values with `*****` for the right ones.

```json
{
    "env": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "STORAGE_S3_BUCKET_NAME": "chronus-docs",
        "AWS_ACCESS_KEY_ID": "*****",
        "AWS_SECRET_ACCESS_KEY": "*****",
        "AWS_DEFAULT_REGION": "sa-east-1",
        "NOTIFICATIONS_MOCK": "true",
        "NOTIFICATIONS_BASE_URL": "https://*****.api.infobip.com",
        "NOTIFICATIONS_CLIENT_ID": "*****",
        "NOTIFICATIONS_CLIENT_SECRET": "*****",
        "NOTIFICATIONS_GRANT_TYPE": "client_credentials",
        "NOTIFICATIONS_AUTHENTICATION_KEY": "*****",
        "NOTIFICATIONS_FROM_NUMBER": "IPassport",
        "NOTIFICATIONS_GET_TOKEN": "/auth/1/oauth2/token",
        "NOTIFICATIONS_SEND_API_URL": "/sms/2/text/advanced",
        "NOTIFICATIONS_GET_API_URL": "/sms/1/reports",
        "SECRET_JWT_TOKEN": "*****"
    }
}
```

> for convenience, the rest of the code are omitted.
