# Employee Leave Manager

An ASP.NET Core and Entity Framework based web application for managing employee leaves.

## Description

This web-based application is a simulation application designed to help administrators manage the leaves of their employees. It provides a user-friendly interface for administrators and employees to perform various actions related to leave management.

## Screenshots

- Login page
  ![Login-page](https://github.com/m3c-ode/LeaveManagementApp/blob/main/Screenshots/elm-1.png)
- Admin view
  ![Admin-page](https://github.com/m3c-ode/LeaveManagementApp/blob/main/Screenshots/elm-2.png)
- Employee Management view
  ![employee-Management-page](https://github.com/m3c-ode/LeaveManagementApp/blob/main/Screenshots/elm-3.png)
- Leave Types
  ![Leave-types-page](https://github.com/m3c-ode/LeaveManagementApp/blob/main/Screenshots/elm-4.png)
- Employee view dashboard
  ![Employee-view-page](https://github.com/m3c-ode/LeaveManagementApp/blob/main/Screenshots/elm-5.png)

### Administrator Features

As an administrator on this platform, you can:

- See the list of employees
- Create and manage leave types (including the number of allocated days)
- Allocate leave types and allocated days to employees
- Send notifications to employees about leave types, allocations, or status updates
- Manage leave requests submitted by employees and accept or decline them
- Have a global view of all leave requests and their statuses

### Employee Features

As an employee on this platform, you can:

- Apply for a leave by selecting available leave types and specifying the desired dates
- View the status of your leave requests, which is updated based on the administrator's response
- Check the status of your available leave allocations (number of days you can take)

## Technologies Used

- ASP.NET Core: A powerful framework used for building web applications
- Entity Framework Core: An Object-Relational Mapping (ORM) tool for working with databases
- Repository Pattern: A design pattern that separates data access logic from business logic
- Dependency Injection: A technique for providing objects that an object needs (dependencies)
- MVC Pattern: A software architectural pattern that separates an application into three main components: Models, Views, and Controllers
- C# and .NET Core Web Syntax: The programming language and syntax used for developing the application
- ASP.NET Core Identity: A membership system that provides user authentication and authorization
- Entity Framework Core Data Models: Models used for interacting with the database
- View Models: Models specifically designed for views to improve data efficiency and security
- AutoMapper: A library used for object-object mapping
- Bootstrap: A popular CSS framework for styling and layout manipulation
- External Services: Integration with email services for seamless communication and notifications
- Version Control: Utilizing GitHub for source code version control
- Deployment: Deployment to hosting platforms like Microsoft Azure, through a CI/CD pipeline using GitHub.

## Getting Started

To get started with the Employee Leave Manager application, follow these steps:

1. Clone the repository.
2. Install the required dependencies.
3. Configure the application settings, such as database connection and email service.
4. Run the application locally or deploy it to a hosting platform.

For detailed instructions on setting up and running the application, please refer to the documentation provided in the repository.

Alternatively, you can test the application on the deployment link hosted on Azure below (in progress).

## Deployment

The Employee Leave Manager application has been successfully deployed using the following methods:

- IIS (Internet Information Services): The application has been published and hosted on a local IIS server for testing purposes.
- SQL Server: The application is integrated with a SQL Server database to store and manage data related to employees, leave types, and leave requests.
- Microsoft Azure: The application has been deployed to Microsoft Azure cloud platform and can be accessed using the following address: [https://leavemanagementweb20230616123130.azurewebsites.net](https://leavemanagementweb20230616123130.azurewebsites.net). Currently offline due to subscription change.

## Contact

If you have any questions or need assistance with the Employee Leave Manager application, please don't hesitate to contact us. We are committed to delivering a reliable and user-friendly experience.

Enjoy using our application!
