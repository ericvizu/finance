# Finance

[![License](https://img.shields.io/badge/license-AGPL--3.0-blue)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-9-purple)]()
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-18-blue)]()
[![Docker](https://img.shields.io/badge/Docker-enabled-2496ED)]()

Personal project created to improve and refine skills in:

- Software development
- Software architecture
- Git
- Docker
- Backend and frontend development

## Tech Stack (so far)

- ASP.NET Core Web API
- PostgreSQL
- Docker
- Adminer

## Getting Started

Clone the repository:

```bash
git clone https://github.com/ericvizu/finance.git
```

Enter the project directory:

```bash
cd finance
```

## Running the Database

Start the PostgreSQL container:

```bash
docker compose up -d
```

PostgreSQL will be available at:

```text
localhost:5432
```

Adminer will be available at:

```text
http://localhost:8080
```

### PostgreSQL Credentials

```text
Database: finance_db
User: finance_user
Password: admin
```

## Running the Backend

Go to the backend directory:

```bash
cd backend
```

Run the API:

```bash
dotnet run
```

The API should be available at:

```text
http://localhost:5000
```

or

```text
https://localhost:5001
```

## License

This project is licensed under the GNU Affero General Public License v3.0 (AGPL-3.0).

This means you can freely use, modify, and distribute this software, but any modifications — including when used as a network service (SaaS) — must also be released under the AGPL-3.0.