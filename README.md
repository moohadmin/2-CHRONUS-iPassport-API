# Mootech - IPassport API [ipassport-api]

[![GitHub to AWS CI Pipeline](https://github.com/moohadmin/ipassport-api/actions/workflows/githubaws-ci.yml/badge.svg)](https://github.com/moohadmin/ipassport-api/actions/workflows/githubaws-ci.yml)

# Environment Variables
| Local name                        | AWS secret name (click to see exemple)                              | Required | Default value |
| ---                               | ---                                                                 | ---      | ---           |
| ASPNETCORE_ENVIRONMENT            |                                                                     |          | Development   |
| DATABASE_CONNECTION_STRING        | [AWS_SECRET_DATABASE](#AWS-Secret-Database-Configuration)           | X        |               |
| STORAGE_S3_BUCKET_NAME            | [AWS_SECRET_S3](#AWS-Secret-S3-Object-Storage-Configuration)        | X        |               |
| AWS_ACCESS_KEY_ID                 | [AWS_SECRET_S3](#AWS-Secret-S3-Object-Storage-Configuration)        | X        |               |
| AWS_SECRET_ACCESS_KEY             | [AWS_SECRET_S3](#AWS-Secret-S3-Object-Storage-Configuration)        | X        |               |
| AWS_DEFAULT_REGION                | -                                                                   |          | sa-east-1     |
| NOTIFICATIONS_MOCK                | -                                                                   |          | false         |
| NOTIFICATIONS_BASE_URL            | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_CLIENT_ID           | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_CLIENT_SECRET       | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_GRANT_TYPE          | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_AUTHENTICATION_KEY  | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_FROM_NUMBER         | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_GET_TOKEN           | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_SEND_API_URL        | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| NOTIFICATIONS_GET_API_URL         | [AWS_SECRET_NOTIFICATIONS](#AWS-Secret-Notifications-Configuration) | X        |               |
| SECRET_JWT_TOKEN                  | [AWS_SECRET_JWT_TOKEN](#AWS-Secret-for-JWT-Configuration)           | X        |               |

## AWS Secret Database Configuration

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

## AWS Secret S3 Object Storage Configuration

> From environment variable: AWS_SECRET_S3

```json
{ 
  "bucket_name": "chronus-docs",
  "aws_access_key": "*****",
  "aws_secret": "*****"
}
```

## AWS Secret Notifications Configuration

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

## AWS Secret for JWT Configuration

> From environment variable: AWS_SECRET_JWT_TOKEN

```json
{
  "jwt_token": "*****"
}
```
