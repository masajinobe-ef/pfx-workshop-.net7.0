<p align="center">
    <img src="pic.jpg" alt="pic"/>
</p>

<center>

# 🧢Priscilla Custom Effects management program with .NET-7.0 and PostgreSQL

</center>

<p align="center">
    <b><a href="https://github.com/masajinobe-ef/pfx-workshop-.net7.0/blob/main/README-EN.md/#%EF%B8%8F-installation">English</a></b>
    |
    <b><a href="https://github.com/masajinobe-ef/pfx-workshop-.net7.0#%EF%B8%8F-установка">Russian</a></b>
</p>

[![Release version](https://img.shields.io/github/v/release/masajinobe-ef/pfx-workshop-.net7.0?color=brightgreen&label=Download&style=for-the-badge)](#%EF%B8%8F-installation "Installation")
[![License: GPLv3](https://img.shields.io/badge/license-GPLv3-blue.svg?style=for-the-badge)](LICENSE "License")
[![Donate](https://img.shields.io/badge/_-Donate-red.svg?logo=githubsponsors&labelColor=555555&style=for-the-badge)](https://boosty.to/priscilla-custom-effects "Donate")
[![Stars](https://img.shields.io/github/stars/masajinobe-ef/pfx-workshop-.net7.0?color=fff&labelColor=0C0E0F&style=for-the-badge)](https://boosty.to/priscilla-custom-effects "Stars")
[![Code Size](https://img.shields.io/github/languages/code-size/masajinobe-ef/pfx-workshop-.net7.0.svg?style=for-the-badge)](https://github.com/masajinobe-ef/pfx-workshop-.net7.0 "Code Size")
[![Repository size](https://img.shields.io/github/repo-size/masajinobe-ef/pfx-workshop-.net7.0.svg?style=for-the-badge)](https://github.com/masajinobe-ef/pfx-workshop-.net7.0 "Repository size")
[![Stars](https://img.shields.io/github/languages/top/masajinobe-ef/pfx-workshop-.net7.0.svg?style=for-the-badge)](https://github.com/masajinobe-ef/pfx-workshop-.net7.0 "Stars")

<p align="center" >
    <a href="https://priscilla-custom-effects.github.io">
        <img src="logo.png" alt="logo" width="256"/>
    </a>
</p>

## 📄 Overview

Priscilla Custom Effects workshop specializes in creating custom effects for musical instruments. Our software serves as a tool for managing processes and operations related to production, orders, and inventory.

## 🛠️ Installation

Make sure you have [.NET 7.0 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) and [PostgreSQL](https://www.postgresql.org) installed.
Clone the repository to your local machine:

```sh
git clone https://github.com/masajinobe-ef/pfx-workshop-.net7.0.git
```

**[Available Releases](https://github.com/masajinobe-ef/pfx-workshop-.net7.0/releases)**

Configure the connection to the PostgreSQL database in the **appsettings.json** configuration file:

```console
{
    "ConnectionString": {
        "Host": "127.0.0.1",    // PostgreSQL database host address
        "Port": "5432",         // Port PostgreSQL is running on
        "Database": "postgres", // Database name
        "Username": "postgres", // Username for connecting to the database
        "Password": "0123",     // Password for the user to connect to the database
        "IncludeErrorDetail": "true"
        // Flag indicating whether to include detailed application error information
    }
}
```

## License

**This project is licensed under the GPL-3.0 license.**

Written by [@masajinobe](https://github.com/masajinobe-ef)