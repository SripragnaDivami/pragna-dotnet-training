# Order Management System

A .NET console application that demonstrates order processing and management with LINQ operations, built as part of .NET training exercises.

## Project Overview

This application simulates an order management system that processes customer orders, calculates revenues, applies discounts, and provides various order analytics using LINQ queries.

## Project Structure

```
task1/
├── myfirst/
│   ├── Program.cs              # Main entry point
│   ├── Models/
│   │   ├── Order.cs           # Order data model
│   │   └── OrderStatus.cs     # Order status enum (Pending, Completed, Failed)
│   ├── Services/
│   │   └── OrderService.cs    # Business logic for order operations
│   └── Data/
│       └── OrderData.cs       # Sample order data
└── task1.sln                  # Solution file
```


## 🔧 Technologies Used

- .NET 10.0
- C#
- LINQ for data querying and manipulation

## 📦 Models

### OrderModel

- `OrderId`: Unique identifier for the order
- `CustomerName`: Name of the customer
- `IsVipCustomer`: VIP status flag
- `OrderAmount`: Total order value
- `Status`: Current order status (Pending/Completed/Failed)
- `Discount`: Applied discount amount

### OrderStatus Enum

- `Pending`: Order is pending
- `Completed`: Order has been completed
- `Failed`: Order has failed

## Getting Started

### Prerequisites

- .NET SDK 10.0 or higher

### Installation

1. Clone the repository:

```bash
git clone <repository-url>
cd task1
```


### Running the Application

```bash
cd myfirst
dotnet run
```


## Key Learning Points

This project demonstrates:

- Object-oriented programming in C#
- LINQ queries (`Where`, `Sum`, `FirstOrDefault`, `Select`)
- Enum usage for status management
- Service layer pattern for business logic
- Data layer separation
- Nullable types and null-safety




