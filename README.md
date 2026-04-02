Bamboo Information Management System (BIMS)
Overview

BIMS is a console-based enterprise system written in C# that manages bamboo species, inventory, growth tracking, and financial data.

Features
Species database (biological profiles)
Inventory management
Growth logging
Expense tracking
Financial reporting (Excel + PDF)
JSON-based persistence
Automatic backups
Architecture
Program.cs – Main controller and UI
Models.cs – Data models
Persistence.cs – File handling and reporting
SeedDataGenerator.cs – Sample data
Data Storage

All data is stored in JSON files:

inventory.json
species.json
logs.json
expenses.json
Relationships
One species → many inventory batches
One batch → many logs and expenses
How to Run
Clone repository
Open in Visual Studio
Run project
Choose to load or seed data
Key Concepts Demonstrated
Object-Oriented Programming
Separation of Concerns
File-based persistence
LINQ queries
Basic financial calculations
Future Improvements
GUI (WPF / Web)
Database (SQL)
Authentication
API integration
