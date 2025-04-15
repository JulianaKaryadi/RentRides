# RentRides: Modern Car Rental System

## Project Description
RentRides is a web-based car rental application that digitizes the entire rental process, enabling customer self-service, online bookings, and comprehensive administrative tools.

## Project Overview

RentRides is a comprehensive digital transformation of a traditional car rental system that revolutionizes the rental experience for both customers and administrators. This web-based application leverages modern technologies and user-centric design to streamline the entire rental process – from browsing available vehicles to making reservations, reporting damages, and providing feedback.

The system transforms manual, paper-based processes into an efficient digital ecosystem where customers can self-register, browse a detailed catalog with advanced filtering options, make online bookings with deposits, report damages during returns, and provide ratings and feedback. Administrators benefit from enhanced tools for user management, car inventory control, financial analytics, and customer communication.

Built with C#, ASP.NET, and MySQL, RentRides implements 18 sophisticated design patterns to ensure maintainability, scalability, and flexibility – making it not just a practical solution for today's car rental needs, but a platform that can easily evolve with future requirements. range of digital features.

## Key Features

### For Customers
- **Self-Registration**: User-friendly registration process with secure account management
- **Service Catalog**: Browse available cars with advanced filtering by brand, seats, and price range
- **Online Booking System**: Streamlined booking process with integrated deposit functionality
- **Damage Reporting**: Report car damage during returns with clear process for additional charges
- **Rating & History**: View rental history and provide feedback on rental experiences
- **User Dashboard**: Manage profile, view past bookings, and track current rentals

### For Administrators
- **User Management**: Add, edit, and delete user information
- **Car Inventory Management**: Add new cars and manage the existing fleet
- **Income Analytics**: View total income with graphical representations
- **Feedback Analysis**: Analyze customer feedback with pie charts
- **Customer Communication**: Contact users via email directly from the system
- **Booking Details**: View comprehensive information about all bookings

## Technologies Used

- **Database Management System**: MySQL
- **Programming Languages**: C#, ASP.NET, CSS
- **Integrated Development Environment**: Visual Studio
- **Design Patterns**: Implemented design patterns for maintainability & scalability

## System Architecture

RentRides follows the MVC (Model-View-Controller) architecture pattern:

- **Model**: Represents the data and business logic
- **View**: Handles the user interface
- **Controller**: Manages user input and updates the model and view accordingly

### Key Design Patterns
1. **Repository Pattern**: Manages data access and persistence for cars, users, and bookings, providing a clean separation between business logic and data access layers
2. **Command Pattern**: Encapsulates actions related to booking, returning, and managing customer data, allowing for better control over complex operations
3. **Observer Pattern**: Notifies users and admins of critical events like booking confirmations, return confirmations, and feedback submissions
4. **Mediator Pattern**: Reduces direct dependencies between components, facilitating communication through a central mediator for cleaner code organization
5. **Factory Pattern**: Creates instances of car objects and user objects dynamically, making it easier to extend and maintain the system as it grows

## System Workflow

1. **User Registration**: Customers create accounts with personal details
2. **Car Selection**: Browse available cars with filtering options
3. **Booking Process**: Select dates, calculate costs, and make deposit
4. **Car Usage**: Rental period during which customer uses the vehicle
5. **Return Process**: Report any damage and complete return procedures
6. **Feedback**: Rate experience and provide comments
7. **History Access**: View past rental details and ratings
