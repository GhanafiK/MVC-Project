# 🏢 Employee Management System - ASP.NET MVC

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-5C2D91?logo=.net&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoft-sql-server&logoColor=white)
![Google OAuth](https://img.shields.io/badge/Google_OAuth-4285F4?logo=google&logoColor=white)

A secure and scalable employee management system built with ASP.NET MVC that handles authentication, authorization, and CRUD operations.
## ✨ Key Features

### 🔐 Authentication System
- **Dual Login Options**:
  - Traditional email/password registration
  - Google OAuth integration
- **Password Recovery**:
  - Secure email reset links
  - Token-based validation
### 🏗️ Core Modules
| Module | Features |
|--------|----------|
| **👥 User Management** | View all registered users |
| **👔 Employee Management** | CRUD operations with department assignment |
| **🏛️ Department Management** | Create/edit/delete departments |
| **🎚️ Role Management** | Assign roles via checkbox interface |

## 🛠️ Technical Stack
| Layer | Technologies |
|-------|--------------|
| **Frontend** | Razor Views, Bootstrap 5, jQuery |
| **Backend** | ASP.NET Core MVC, C# |
| **Database** | Entity Framework Core, SQL Server |A
| **Security** | ASP.NET Identity, Role-based Authorization |
| **Services** | SMTP Email, Google Authentication |

## ✨ Enhanced Features

### 🔄 Smart Deletion Handling
| Module | Behavior |
|--------|----------|
| **Role Deletion** | Automatically removes role from all assigned users |
| **Department Deletion** | Cleans up references from all affected employees |

### 🔍 Powerful Search
| Entity | Searchable By |
|--------|--------------|
| Employees |Employee Name |
| Departments |Department Name|
| Roles | Role Name |
| Users |User Name|

### Prerequisites
- .NET 6 SDK
- SQL Server 2019+
- Google Developer Account (for OAuth)

## 🖼️ Visual Walkthrough
<div align="left">
    <img src="screenshots/Register.png" width="300px" height="200px" alt="Step 1: Register">
    <img src="screenshots/Login.png" width="300px" height="200px" alt="Step 2:  Login">
    <img src="screenshots/Reset Password 1.png" width="300px" height="200px" alt="Reset password">
    <img src="screenshots/Reset Password 2.png" width="300px" height="200px" alt="Reset password">
    <img src="screenshots/Reset Password 3.png" width="300px" height="200px" alt="Reset password">
    <img src="screenshots/All Departments.png" width="300px" height="200px" alt="All Departments">
    <img src="screenshots/Department Details.png" width="300px" height="200px" alt="Department Details">
    <img src="screenshots/Edit Department.png" width="300px" height="200px" alt="Edit Department">
    <img src="screenshots/Delete Department.png" width="300px" height="200px" alt="Delete Department">
    <img src="screenshots/All Employees.png" width="300px" height="200px" alt="All Employees">
    <img src="screenshots/Create Employee.png" width="300px" height="200px" alt="Create Employee">
    <img src="screenshots/Employee Details 1.png" width="300px" height="200px" alt="Employee Details 1">
    <img src="screenshots/Employee Details 2.png" width="300px" height="200px" alt="Employee Details 2">
    <img src="screenshots/Edit Employee.png" width="300px" height="200px" alt="Edit Employee.png">
    <img src="screenshots/Delete Employee.png" width="300px" height="200px" alt="Delete Employee">
    <img src="screenshots/All Users.png" width="300px" height="200px" alt="All Users">
    <img src="screenshots/Delete User.png" width="300px" height="200px" alt="Delete User">
    <img src="screenshots/All Roles.png" width="300px" height="200px" alt="All Roles">
    <img src="screenshots/Create Role.png" width="300px" height="200px" alt="Create Role">
    <img src="screenshots/Edit Role.png" width="300px" height="200px" alt="Edit Role">
    <img src="screenshots/Delete Role.png" width="300px" height="200px" alt="Delete Role">
</div>

## Contact
Gamal Hanafi - [LinkedIn](https://www.linkedin.com/in/gamal-khalil-56993a268/) - (gamalhanafi26@gmail.com) <br />
