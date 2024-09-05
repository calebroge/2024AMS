## Table of Contents
1. [Introduction](https://github.com/calebroge/2024AMS?tab=readme-ov-file#introduction)
2. [Installation](https://github.com/calebroge/2024AMS?tab=readme-ov-file#installation)
3. [Configuration](https://github.com/calebroge/2024AMS?tab=readme-ov-file#configuration)
4. [Documentation](https://github.com/calebroge/2024AMS?tab=readme-ov-file#project-documentation)

## Introduction

The 2024AMS records asset information and tracks every asset that is assigned to a person within the system. 
In addition, this system can generate up-to-date reports on assets and barcodes with the press of a button. 

## Installation

1. Download and install Microsoft Visual Studio Community

Ensure you have the latest version installed. If not, download and install it from Microsoft's official website. 

* [Instructions for installing Visual Studio 2022](https://learn.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2022)
* [Download Visual Studio](https://visualstudio.microsoft.com/downloads/?cid=learn-onpage-download-install-visual-studio-page-cta)

2. Download and install Microsoft SQL Server Management Studio

Ensure you have the latest version installed. If not, download and install it from Microsoft's official website. 

* [Download SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)

3. Clone the repository

```
git clone https://github.com/calebroge/2024AMS.git
```

## Configuration

If you want to run this software on your machine, make sure to follow these steps:

### 1. appsettings.json

For the project to run on Visual Studio, make sure you change the server in the appsettings.json to the name of the computer machine you are using. For example, change `Server=computername` to the name of your machine. The code below
shows where to look and what to adjust.

```json
"ConnectionStrings": {
  "2024AMSConnectionString": "Server=computername\\SQLEXPRESS;Database=2024AMS;Integrated Security=SSPI;Trusted_Connection=True;"
}
```

### 2. Initialize.cs

Keep the code commented out on the cs page and change the code below to include your email.

```C#
 // Hardcode the email address when bypassing the Okta login.
 strEmailAddress = "abc@address.com";
```

### 3. 2024AMS.bak

In addition, you will need to download the database for this application in order for it to run and back it up on Microsoft SQL Server Management Studio. Make sure to input your name (first and last), email, and add a status (admin, facstaff, or technician) to the 
user database table. For restoring the database and backing it up on your machine, refer to the link below:

* [Quickstart: Backup and restore a SQL Server database with SSMS](https://learn.microsoft.com/en-us/sql/relational-databases/backup-restore/quickstart-backup-restore-database?view=sql-server-ver16&tabs=ssms)


## Documentation
Attached is the documentation of the project below including the software requirements, database schema, input/output design and other documents related the development of the software.

[Captsone Project Documentation Package.zip](https://github.com/user-attachments/files/16853307/Captsone.Project.Documentation.Package.zip)
