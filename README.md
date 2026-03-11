# CarSaleMan - Car Sale Management System (汽车销售管理系统)

A comprehensive Windows desktop application for managing car sales, inventory, and dealership operations.

## Overview

CarSaleMan is a complete car dealership management system built with C# Windows Forms. It provides complete lifecycle management for vehicle sales including inventory tracking, sales processing, financial management, and comprehensive reporting.

## Technology Stack

| Component | Technology |
|-----------|------------|
| **Framework** | .NET Framework 4.0 |
| **Language** | C# |
| **UI** | Windows Forms (WinForms) |
| **Database** | SQL Server 2005+ |
| **UI Controls** | ComponentOne (C1.Win.C1Chart, C1.Win.C1FlexGrid, C1.C1Report) |
| **Architecture** | MDI (Multiple Document Interface) |

## Features

### 1. Vehicle Management
- **On-Road Car Management** (在途车辆) - Track vehicles in transit
- **Store In** (入库管理) - Vehicle入库 management
- **Store Out** (出库管理) - Vehicle出库 management
- **Store Change** (库存调拨) - Internal stock transfers
- **Special Car Management** (特殊车辆) - Handle special case vehicles

### 2. Repair & Service
- **Append Repair** (追加维修) - Additional repair orders
- **Service Journal** (维修日志) - Track maintenance history

### 3. Search & Queries
- **Search On-Road Cars** - Query vehicles in transit
- **Search Store In Records** - Query入库 records
- **Search Store Out Records** - Query出库 records

### 4. Statistics & Analytics
- **Quarterly Sales Statistics** - Sales performance by quarter
- **Sales by Region** - Geographic sales analysis
- **Customer Job Analysis** - Customer occupation statistics
- **Car Type Color Analysis** - Color preference analysis
- **Handler Statistics** - Sales personnel performance
- **Remaining Amount Statistics** - Outstanding payments
- **Sales by Car Type** - Product line performance

### 5. Reports
- **Store Detail Report** - Detailed inventory report
- **Store Car Type Detail** - Inventory by vehicle type
- **On-Road Detail Report** - In-transit vehicle details
- **Profit Detail Report** - Profitability analysis
- **Store Total Report** - Total inventory summary
- **Store Car Type Total** - Summary by vehicle type
- **Reserve Sales Report** - Pre-orders and reservations
- **Sales Total Report** - Total sales summary
- **Sales Count Total** - Units sold summary
- **Whole Sales Report** - Wholesale transactions
- **Car Series Report** - Sales by vehicle series

### 6. Charts & Visualization
- **Purchase Total Chart** - Total purchases visualization
- **Sales Kind Count Chart** - Sales by category
- **Sales Count Chart** - Sales volume trends
- **Sales Region Chart** - Regional sales distribution
- **Customer Job Chart** - Customer demographics

### 7. Finance Management
- **Finance Store** - Financial tracking
- **Finance Parameters** - Financial configuration

### 8. Settings & Administration
- **Car Type Management** - Vehicle type configuration
- **Car Company Management** - Manufacturer/brand setup
- **Base Data Management** - System reference data
- **Base Data Import** - Bulk data import (Excel)
- **User Permission Management** - Role-based access control
- **Password Management** - Password change functionality
- **Environment Settings** - System configuration

## Project Structure

```
dmy-car-sale-app/
├── CarSaleMan/                         # Main application directory
│   ├── CarSaleMan.sln                  # Visual Studio solution file
│   └── CarSaleMan/
│       ├── CarSaleMan.csproj           # Project file
│       ├── CarSaleMain.cs              # Application entry point
│       ├── CommonMisc.cs               # Common utilities and helpers
│       ├── Frm*.cs                     # Form classes (UI)
│       ├── CmsDB.xsd                   # Database schema
│       └── Properties/                 # Project properties
├── Database/                           # Database files
│   ├── csm.mdf                         # SQL Server database
│   └── csm_log.ldf                     # Transaction log
├── Document/                           # Documentation
│   ├── CSM_DatabaseDesign.xlsx         # Database design document
│   ├── Original/                       # Original source files
│   ├── Resource/                       # Documentation resources
│   └── Test/                           # Test data
├── Installer/                          # Installer files
└── README.md                           # This file
```

## Database

- **Database Name**: csm
- **SQL Server Version**: 2005 or higher
- **Authentication**: SQL Server Authentication (sa login)

### Database Files
- `Database/csm.mdf` - Main database file
- `Database/csm_log.ldf` - Transaction log

## System Requirements

- **Operating System**: Windows XP/Vista/7/8/10
- **.NET Framework**: .NET Framework 4.0 or higher
- **Database**: SQL Server 2005 or higher
- **RAM**: Minimum 2GB
- **Disk Space**: 500MB for installation

## Setup Instructions

### 1. Database Setup
1. Install SQL Server 2005 or higher
2. Restore or attach the database from `Database/csm.mdf`
3. Configure database connection in application settings

### 2. Application Configuration
1. Open the solution in Visual Studio 2010 or later
2. Build the project
3. Run the application
4. On first launch, configure:
   - Server address
   - Database credentials
   - Auto-logon preference

### 3. Login
- Default login credentials should be provided by system administrator

## Security Features

- **Password Encryption**: DES encryption for stored passwords
- **User Authentication**: SQL Server authentication
- **Role-Based Permissions**: Menu and function-level access control
- **Audit Trail**: Action history logging

## Key Classes and Modules

| Class | Description |
|-------|-------------|
| `CarSaleMain` | Application entry point and initialization |
| `CommonMisc` | Common utilities, environment settings, logging |
| `DBProvider` | Database connection and operations |
| `Global` | Global constants and user session data |
| `CsmEncrypt` | Encryption/decryption utilities |
| `Permission` | User permission management |
| `FrmMDIMain` | Main MDI parent form |
| `FrmLogon` | Login form |
| `FrmEnvSet` | Environment settings form |

## Menu Structure

```
├── Car Management (车辆管理)
│   ├── On-Road Car (在途车辆)
│   ├── Store In (入库管理)
│   ├── Store Change (库存调拨)
│   ├── Store Out (出库管理)
│   ├── Special Car (特殊车辆)
│   ├── Append Repair (追加维修)
│   └── Service Journal (维修日志)
├── Statistics (统计查询)
│   ├── Search On-Road Car
│   ├── Search Store In
│   ├── Search Store Out
│   ├── Sales by Quarter
│   ├── Sales by Region
│   ├── Customer Job Analysis
│   ├── Car Type Color Analysis
│   ├── Handler Statistics
│   ├── Remaining Amount
│   └── Sales by Car Type
├── Reports (报表)
│   ├── Store Detail
│   ├── Store Car Type Detail
│   ├── On-Road Detail
│   ├── Profit Detail
│   ├── Store Total
│   ├── Store Car Type Total
│   ├── Reserve Sales
│   ├── Sales Total
│   ├── Sales Count Total
│   ├── Whole Sales Total
│   └── Car Series Total
├── Charts (图表)
│   ├── Purchase Total
│   ├── Sales Kind Count
│   ├── Sales Count
│   ├── Sales Region
│   └── Customer Job
├── Finance (财务管理)
│   ├── Finance Summary
│   └── Finance Parameters
├── Settings (设置)
│   ├── Car Type
│   ├── Base Data
│   ├── Car Company
│   └── User Permission
└── System
    ├── Change Password
    └── Exit
```

## Version Information

- **Application Version**: 2.0
- **Application Title**: 汽车销售管理系统 (Car Sale Management System)

## License

This project is for educational and internal use purposes.

## Notes

- The application uses Chinese language interface
- All monetary values are in Chinese Yuan (RMB)
- The system supports multi-user concurrent access
- Data can be exported to Excel and PDF formats

## Troubleshooting

1. **Database Connection Failed**: Check SQL Server is running and credentials are correct
2. **Login Failed**: Verify username and password with administrator
3. **Permission Issues**: Contact administrator for menu access rights
4. **Export Errors**: Ensure Excel/PDF components are properly installed

