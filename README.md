# 🧢Priscilla Custom Effects management program with .NET-7.0 / PostgreSQL

<p align="center">
    <img src="pic.jpg" alt="pic"/>
</p>
<p align="center">
    <b><a href="https://github.com/masajinobe-ef/pfx-workshop-.net7.0#overview">English</a></b>
    |
    <b><a href="https://github.com/masajinobe-ef/pfx-workshop-.net7.0#obzor">Russian</a></b>
</p>

## Обзор

Мастерская Priscilla Custom Effects специализируется на создании индивидуальных эффектов для музыкальных инструментов. Наше программное обеспечение представляет собой инструмент для управления процессами и операциями, связанными с производством, заказами и инвентаризацией.

## Установка

Убедитесь, что у вас установлен [.NET 7.0 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) и [PostgreSQL](https://www.postgresql.org).
Клонируйте репозиторий на локальную машину:

```bash
git clone https://github.com/masajinobe-ef/pfx-workshop-.net7.0.git
```

### [Доступные релизы](https://github.com/masajinobe-ef/pfx-workshop-.net7.0/releases)

Настройте подключение к базе данных PostgreSQL в файле конфигурации **appsettings.json**:

```json
{
    "ConnectionString": {
        "Host": "127.0.0.1", // Адрес хоста базы данных PostgreSQL
        "Port": "5432", // Порт, на котором работает PostgreSQL
        "Database": "postgres", // Имя базы данных
        "Username": "postgres", // Имя пользователя для подключения к базе данных
        "Password": "0123", // Пароль пользователя для подключения к базе данных
        "IncludeErrorDetail": "true" // Флаг, указывающий, нужно ли включать подробные сведения об ошибках приложения
    }
}
```

## Overview

Priscilla Custom Effects workshop specializes in creating custom effects for musical instruments. Our software serves as a tool for managing processes and operations related to production, orders, and inventory.

## Installation

Make sure you have [.NET 7.0 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) and [PostgreSQL](https://www.postgresql.org) installed.
Clone the repository to your local machine:

```bash
git clone https://github.com/masajinobe-ef/pfx-workshop-.net7.0.git
```

### [Available Releases](https://github.com/masajinobe-ef/pfx-workshop-.net7.0/releases)

Configure the connection to the PostgreSQL database in the **appsettings.json** configuration file::

```json
{
    "ConnectionString": {
        "Host": "127.0.0.1", // PostgreSQL database host address
        "Port": "5432", // Port PostgreSQL is running on
        "Database": "postgres", // Database name
        "Username": "postgres", // Username for connecting to the database
        "Password": "0123", // Password for the user to connect to the database
        "IncludeErrorDetail": "true" // Flag indicating whether to include detailed application error information
    }
}
```

This project is licensed under the GPL-3.0 license.
