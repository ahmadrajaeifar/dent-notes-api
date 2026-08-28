# Dental Notes API

A RESTful backend application built with **ASP.NET Core** for managing dental clinics, patients, dentists, and day-to-day clinic operations.

The project is being developed as a practical dental-clinic management system, with a focus on clean backend architecture, separation of concerns, and maintainable application design.

> **Project status:** 🚧 Work in Progress

## Overview

Dental Notes API is designed to provide the backend infrastructure for a dental clinic management system.

The solution is currently organized into separate projects to keep the API, shared contracts, and web application concerns independent from each other.

### Main areas

- 👨‍⚕️ Dentist management
- 🧑‍⚕️ Patient management
- 🏥 Dental clinic operations
- 👤 User and role management
- 🔐 Authentication and authorization
- 📝 Registration and profile management
- 📍 Location management
- 🔄 RESTful API endpoints

## Solution Structure

```text
DentalClinic.sln
│
├── DentalClinic.Api
│   └── API and application logic
│
├── DentalClinic.Contracts
│   └── Shared DTOs and contracts
│
└── DentalClinic.Web
    └── Web application
```

The solution is intentionally separated into multiple projects so that shared contracts and application responsibilities can evolve independently.

## Technology Stack

### Backend

- **C#**
- **ASP.NET Core**
- **Entity Framework Core**
- **SQL Server**

### Architecture & Design

The project follows a layered approach with an emphasis on:

- Separation of concerns
- DTO-based data transfer
- Service-oriented application logic
- Dependency Injection
- Entity Framework Core for data access
- Role-based authorization
- RESTful API design

## Authentication & Authorization

The application includes user authentication and role-based authorization.

Different user roles can have different permissions and application flows, allowing the system to distinguish between users such as:

- Dentist
- Patient
- Seller
- Administrator

Authorization policies are used to restrict access to protected resources.

## Registration Flow

User registration is handled as a dedicated application flow rather than simply creating a user record.

The registration process supports scenarios such as:

- Creating a new user
- Continuing an incomplete registration
- Detecting existing users
- Validating credentials
- Assigning user roles
- Completing user registration

This allows users who have not completed their profile to continue the registration process later.

## Project Goals

The main goal of this project is to build a maintainable and extensible backend for a real-world dental clinic management system.

Some of the current development goals include:

- Improving separation of application responsibilities
- Moving business logic out of controllers
- Expanding service-based architecture
- Improving DTO and result models
- Strengthening authentication and authorization
- Adding additional clinic-management features
- Improving API documentation
- Adding automated tests

## Getting Started

### Prerequisites

Before running the project, make sure you have:

- .NET SDK compatible with the solution
- SQL Server
- A development environment such as Visual Studio or another compatible IDE

### Clone the repository

```bash
git clone https://github.com/ahmadrajaeifar/dent-notes-api.git
```

```bash
cd dent-notes-api
```

### Database Configuration

Configure the SQL Server connection string according to your local environment.

> **Important:** Do not commit production credentials, passwords, connection strings, or other sensitive configuration values to the repository.

### Run the project

Open the solution:

```text
DentalClinic.sln
```

Then build and run the desired startup project from your development environment.

## Contributing

Contributions are welcome.

If you would like to improve the project:

1. Fork the repository.
2. Create a feature branch.

```bash
git checkout -b feature/your-feature
```

3. Make your changes.
4. Commit your changes with a clear commit message.

```bash
git add .
git commit -m "Add your feature description"
```

5. Push the branch.

```bash
git push origin feature/your-feature
```

6. Open a Pull Request.

Suggestions, bug reports, architectural improvements, and feature contributions are all welcome.

## Roadmap

The project is actively evolving. Planned improvements include:

- [ ] Expand dental clinic management features
- [ ] Improve API documentation
- [ ] Add automated tests
- [ ] Improve validation and error handling
- [ ] Expand authentication and authorization
- [ ] Improve application architecture
- [ ] Add more comprehensive patient and dentist workflows
- [ ] Improve deployment and configuration
- [ ] Add additional reporting capabilities

## License

This project is licensed under the **MIT License**.

See the [LICENSE](LICENSE) file for details.

## Author

**Ahmad Rajaeifar**

GitHub: [@ahmadrajaeifar](https://github.com/ahmadrajaeifar)

---

⭐ If you find the project useful, feel free to star the repository or contribute to its development.
