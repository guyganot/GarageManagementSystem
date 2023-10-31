# Garage Management System

# Introduction
The Garage Management System is a C# application designed to help manage vehicles in a garage. It allows you to store information about different types of vehicles, their owners, and their repair status. This README provides an overview of the system and how to get started.

# Features
- Vehicle Management: Store information about different types of vehicles, such as cars, motorbikes, and trucks.
- Owner Information: Record details about the owners of the vehicles.
- Repair Status: Track the status of vehicles in the garage, including whether they are in repair, repaired, or paid for.
- Wheel and Energy Type Handling: Implement classes for wheels and energy types (fuel or electric) for vehicles.
- Exception Handling: Utilize custom exceptions for handling errors.

# Prerequisites:
- .NET Framework or .NET Core
- Integrated Development Environment (IDE) such as Visual Studio
  
# Installation
- Clone the repository:
git clone https://github.com/yourusername/garage-management-system.git
- Open the project in your preferred IDE.
- Build and run the application.

# Usage
1. Launch the Garage Management System.
2. Use the application to add, update, or view information about vehicles and their owners.
3. Track the repair status of vehicles and manage wheel and energy type information.

# Code Structure
The project's code is organized into several classes:

- Motorbike, Truck, Vehicle: Classes representing different types of vehicles.
- VehicleInfo: Stores information about vehicle owners and their vehicle status.
- Wheel: Represents vehicle wheels with air pressure and other properties.
- ValueOutOfRangeException: Custom exception class for handling value out-of-range errors.
