## Table of Contents
1. [Introduction](https://github.com/calebroge/2024AMS?tab=readme-ov-file#introduction)
2. [Installation](https://github.com/calebroge/2024AMS?tab=readme-ov-file#installation)
3. [Configuration](https://github.com/calebroge/2024AMS?tab=readme-ov-file#configuration)
4. [Project Documentation](https://github.com/calebroge/2024AMS?tab=readme-ov-file#project-documentation)

## Introduction

The 2024AMS records asset information and tracks every asset that is assigned to a person within the system. 
In addition, this system can generate up-to-date reports on assets and barcodes with the press of a button. 

## Installation

1. Download and install Microsoft Visual Studio Community

Ensure you have the latest version installed. If not, download and install it from Microsoft's official website. For detailed instructions, refer to the tutorials:

2. Download and install Microsoft SQL Server Management Studio

Ensure you have the latest version installed. If not, download and install it from Microsoft's official website. For detailed instructions, refer to the tutorials:

3. 


## Configuration

### 1. appsettings.json

For the project to run on Visual Studio, make sure you change the server in the appsettings.json to the name of the computer machine you are using. For example, change `Server=CALEBSPC` to the name of your machine. The code below
shows where to look and what to adjust.

```json
"ConnectionStrings": {
  "2024AMSConnectionString": "Server=CALEBSPC\\SQLEXPRESS;Database=2024AMS;Integrated Security=SSPI;Trusted_Connection=True;"
}
```

### 2. Initialize.cs

Leave the code that is commented out in the initialize page except the code below which you could change to add your email.

```C#
 // Hardcode the email address when bypassing the Okta login.
 strEmailAddress = "caleb.roge@franklincollege.edu";
```

### 3. 2024AMS.bak

In addition, you will need to download the database for this application in order for it to run and back it up on Microsoft SQL Server Management Studio. Make sure to input your name (first and last), email, and add a status (admin, facstaff, or technician) to the 
user database table.



## Project Documentation
Attached is the documentation of the project below including the software requirements, database schema, input/output design and other documents regarding the development of the software.

[Captsone Project Documentation Package.zip](https://github.com/user-attachments/files/16853307/Captsone.Project.Documentation.Package.zip)
