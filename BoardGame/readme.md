# BoardGame API Doc

## Content
- [Member](#member) <br />
- [Register](#register) <br />
- [Login](#login) <br />
- [ActivateRegistration](#activateRegistration) <br />

## Member

### Register

This API endpoint allows users to register for an account on the application. Upon successful registration, a confirmation email will be sent to the provided email address. The user will need to activate their account by clicking the link in the confirmation email before being able to access all features of the application.

**Request URL:** 

`<{$siteurl}>/Member/Register`

**Request Method:** 

`POST` 

**Content-Type:**

`application/json`

**Request Parameters:**

| Parameter | Required | Type | Example | Description |
| --- | --- | --- | --- | --- |
| Name | Yes | string | Maru | User's full name |
| Account | Yes | string | Maru1301 | Unique username for the user's account |
| Password | Yes | string | Password | User's password for account access |
| ConfirmPassword | Yes | string | Password | Confirmation of the user's password |
| Email | Yes | string | mailto:example@mail.com | User's email for account notifications and recovery |

**Example Request:**

```json
{
  "Name": "Name",
  "Account": "Account",
  "Password": "Password",
  "ConfirmPassword": "ConfirmPassword",
  "Email": "example@mail.com",
}
```

**Response:**

The response will be a JSON object with a status code and a message depending on the outcome of the registration process.

- **Success:**
    - Status Code: `StatusCodes.OK (200)`
    - Message: `"Registration successful! Confirmation email sent!"`
- **Failure:**
    - Status Code: `StatusCodes.BadRequest (400)`
    - Message: A specific error message related to the registration failure (e.g., "Account already exists", "Password does not meet complexity requirements").
- **Internal Server Error:**
    - Status Code: `StatusCodes.InternalServerError (500)`
    - Message: A generic error message indicating an unexpected issue (e.g., "An error occurred during activation. Please try again later.").

**Example Success Response:**

```json
{
  "Message": "Registration successful! Confirmation email sent!"
}
```

### Login

This API endpoint allows users to log in to their existing account on the application. Upon successful login, a token will be returned that can be used for subsequent API calls requiring authentication.

**Authentication:**

This endpoint is marked with `[AllowAnonymous]`, allowing unauthenticated users to access it.

**Request URL:**

`<{$siteurl}>/Member/Login`

**Request Method:**

`POST`

**Content-Type:**

`application/json`

**Request Parameters:**

| Parameter | Required | Type | Example | Description |
| --- | --- | --- | --- | --- |
| Account | Yes | string | Maru1301 | User's account username |
| Password | Yes | string | Password | User's password for account access |

**Example Request:**

```json
{
  "account": "account",
  "password": "password"
}
```

**Response:**

The response will be a JSON object with a status code and a message depending on the outcome of the login process.

**Success:**

- Status Code: `StatusCodes.OK` (200)
- Message: A JSON object containing the following properties:
    - `token`: The generated JWT token used for subsequent authenticated API calls.

**Failure:**

- Status Code: `StatusCodes.BadRequest` (400)
- Message: A specific error message related to the login failure (e.g., "Invalid account or password").

**Internal Server Error:**

- Status Code: `StatusCodes.InternalServerError` (500)
- Message: A generic error message indicating an unexpected issue (e.g., "An error occurred during login. Please try again later.").

**Example Success Response:**

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5C...",
}
```

### ActivateRegistration

This API endpoint allows users to activate their registration by verifying their member ID and confirmation code. Upon successful activation, the user's account becomes active.

**Request URL**: 

`<{$siteurl}>/Member/ActivateRegistration`

**Request Method**: 

`GET` 

**Content-Type**:

`application/json`

**Request Parameters**:

| Parameter | Required | Type | Example | Description |
| --- | --- | --- | --- | --- |
| memberId | Yes | int | 1301 | The ID of the member to activate. |
| confirmationCode | Yes | string | 12345678 | Code for registration confirmation sent by email |

**Response:**

The response will be a JSON object with a status code and a message depending on the outcome of the activation process.

- **Success:**
    - Status Code: `StatusCodes.OK (200)`
    - Message: `"Activation successful"`
- **Failure:**
    - Status Code: `StatusCodes.BadRequest (400)`
    - Message: A specific error message related to the activation failure (e.g., "Member not found", "Invalid confirmation code").
- **Internal Server Error:**
    - Status Code: `StatusCodes.InternalServerError (500)`
    - Message: A generic error message indicating an unexpected issue (e.g., "An error occurred during activation. Please try again later.").
