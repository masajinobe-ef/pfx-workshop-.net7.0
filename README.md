<p align="center">
    <img src="pic.jpg" alt="pic"/>
</p>

<center>

# 🧢Priscilla Custom Effects management program with .NET-7.0 and PostgreSQL

</center>

<p align="center">
    <b><a href="https://github.com/masajinobe-ef/pfx-workshop-.net7.0#overview">English</a></b>
    |
    <b><a href="https://github.com/masajinobe-ef/pfx-workshop-.net7.0/tree/main?tab=readme-ov-file#%D0%BE%D0%B1%D0%B7%D0%BE%D1%80">Russian</a></b>
</p>

<center>

Written by [@masajinobe](https://github.com/masajinobe-ef)

</center>

<center>

[![Release version](https://img.shields.io/github/v/release/masajinobe-ef/pfx-workshop-.net7.0?color=brightgreen&label=Download&style=for-the-badge)](#установка "Installation")
[![License: GPLv3](https://img.shields.io/badge/license-GPLv3-blue.svg?style=for-the-badge)](LICENSE "License")
[![Donate](https://img.shields.io/badge/_-Donate-red.svg?logo=githubsponsors&labelColor=555555&style=for-the-badge)](https://boosty.to/priscilla-custom-effects "Donate")
[![Stars](https://img.shields.io/github/stars/masajinobe-ef/pfx-workshop-.net7.0?color=fff&labelColor=0C0E0F&style=for-the-badge)](https://boosty.to/priscilla-custom-effects "Stars")

</center>

<p align="center" >
    <a href="https://github.com/masajinobe-ef/pfx-workshop-.net7.0">
        <img src="https://img.shields.io/github/languages/code-size/masajinobe-ef/pfx-workshop-.net7.0.svg?style=for-the-badge" alt="Code Size" />
    </a>
    <a href="https://github.com/masajinobe-ef/pfx-workshop-.net7.0">
        <img src="https://img.shields.io/github/repo-size/masajinobe-ef/pfx-workshop-.net7.0.svg?style=for-the-badge" alt="Repository size" />
    </a>
    <a href="https://github.com/masajinobe-ef/pfx-workshop-.net7.0">
        <img src="https://img.shields.io/github/languages/top/masajinobe-ef/pfx-workshop-.net7.0.svg?style=for-the-badge" alt="Top language" />
    </a>
</p>
<p align="center" >
    <a href="https://priscilla-custom-effects.github.io">
        <img src="logo.png" alt="logo" width="200"/>
    </a>
</p>

## 📄 Обзор

Мастерская Priscilla Custom Effects специализируется на создании индивидуальных эффектов для музыкальных инструментов. Наше программное обеспечение представляет собой инструмент для управления процессами и операциями, связанными с производством, заказами и инвентаризацией.

## 🛠️ Установка

Убедитесь, что у вас установлен [.NET 7.0 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) и [PostgreSQL](https://www.postgresql.org).
Клонируйте репозиторий на локальную машину:

```sh
git clone https://github.com/masajinobe-ef/pfx-workshop-.net7.0.git
```

### [Доступные релизы](https://github.com/masajinobe-ef/pfx-workshop-.net7.0/releases)

Настройте подключение к базе данных PostgreSQL в файле конфигурации **appsettings.json**:

```console
{
    "ConnectionString": {
        "Host": "127.0.0.1",    // Адрес хоста базы данных PostgreSQL
        "Port": "5432",         // Порт, на котором работает PostgreSQL
        "Database": "postgres", // Имя базы данных
        "Username": "postgres", // Имя пользователя для подключения к базе данных
        "Password": "0123",     // Пароль пользователя для подключения к базе данных
        "IncludeErrorDetail": "true" 
        // Флаг, указывающий, нужно ли включать подробные сведения об ошибках приложения
    }
}
```

## Лицензия

**This project is licensed under the GPL-3.0 license.**
