# Amazon Cognito Identity Provider examples using AWS SDK for .NET


This project provides a service layer, `CognitoService`, for interacting with AWS Cognito Identity Provider using the AWS SDK for .NET. The `CognitoService` class implements common authentication functionalities, including user sign-up, sign-in, password reset, and changing password.

## Features

- **User Registration (Sign-Up)**
- **User Sign-In (Authentication)**
- **Email Confirmation for Account Activation**
- **Resending Confirmation Codes**
- **Password Reset and Change**
- **Global Sign-Out for revoking refresh token**

## Requirements

- **AWS Account**: An AWS account with Cognito User Pool configured.
- **AWS SDK for .NET**: The project uses the AWS SDK for .NET, specifically `Amazon.CognitoIdentityProvider`.

## Configuration

- **AWS Account**: An AWS account with Cognito User Pool configured.
- **AWS SDK for .NET**: The project uses the AWS SDK for .NET, specifically `Amazon.CognitoIdentityProvider`.

Set up the following environment variables to store your AWS Cognito secrets:
  
- **AWS:ClientId**: Your Cognito App Client ID
- **UserPoolId**: Your Cognito User Pool ID
- **CognitoJwksUrl**: Your Cognito token signing key URL
- **Issuer**: As following, https://cognito-idp.your-region.amazonaws.com/your-user-pool-id **(Replace your-region & your-user-pool-id)**

Or, you can configure these values in an appsettings.json file.

# API Endpoints

| Endpoint                   | Method | Description                          | Request Body                                                                                                                             | Headers                            | Response Example                                                                                 |
|----------------------------|--------|--------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------|--------------------------------------------------------------------------------------------------|
| `/api/auth/signup`         | POST   | Registers a new user                 | `{ "email": "user@example.com", "password": "Password123!", "name": "User Name" }`                                                       | -                                  | `{ "email": "user@example.com", "userConfirmed": true }`                                         |
| `/api/auth/signin`         | POST   | Authenticates a user                 | `{ "email": "user@example.com", "password": "Password123!" }`                                                                            | -                                  | `{ "token": "your-access-token" }`                                                               |
| `/api/auth/verify-email`   | POST   | Confirms user email with code        | `{ "email": "user@example.com", "confirmationCode": "123456" }`                                                                          | -                                  | `{ "message": "Email verified successfully" }`                                                   |
| `/api/auth/resend-verification` | POST   | Resends email confirmation code      | `{ "email": "user@example.com" }`                                                                                                        | -                                  | `{ "message": "Confirmation code resent" }`                                                      |
| `/api/auth/forgot-password` | POST   | Initiates password reset             | `{ "email": "user@example.com" }`                                                                                                        | -                                  | `{ "message": "Password reset code sent" }`                                                      |
| `/api/auth/reset-password` | POST   | Confirms password reset with code    | `{ "email": "user@example.com", "verificationCode": "123456", "newPassword": "NewPassword123!" }`                                        | -                                  | `{ "message": "Password reset successful" }`                                                     |
| `/api/auth/change-password`| POST   | Changes user password                | `{ "previousPassword": "CurrentPassword123!", "newPassword": "NewPassword123!" }`                                                        | `Authorization: Bearer <token>`    | `{ "message": "Password changed successfully" }`                                                |
| `/api/auth/logout`         | GET    | Logs out the user globally           | -                                                                                                                                        | `Authorization: Bearer <token>`    | `{ "message": "Successfully signed out" }`                                                       |
