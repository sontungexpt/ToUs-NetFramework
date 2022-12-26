# Introduce project

- The time table project use wpf and entity framework with the MVVM model

# The Features in apps

# For development

- After clone the project, open terminal in your project, type git checkout develop, then create a new branch with name convention below

## Installation

**1. Database Framework:**
- Step 1: Check if installed packages
<img src ="./README_SOURCES/Entity.png" width ="100%" height ="100%">
<img src ="./README_SOURCES/Task.png" width="100%" height="100%">
<img src = "./README_SOURCES/ZEntity.png" width="100%" height="100%">

- Step 2: Run the file ToUsQuery.sql in ssms to create db
- Step 3: When you pull or clone from develop branch please open Models folder and remove ToUs.edxm and connect again
<img src="./README_SOURCES/EntityHint_Step1.png" width="100%" heigt ="100%">
- Step 4: Connect
  - 1: Right click to folder Models -> add -> new item
  - 2: Choose Ado.Net Entity Data Model rename ToUs
<img src="./README_SOURCES/EntityHint_Step2.png" width="100%" heigt ="100%" >
  - 3: Choose Entity Form Database then click Next
<img src="./README_SOURCES/EntityHint_Step3.png" width="100%" heigt ="100%" >
  - 4: Choose New Connection
<img src="./README_SOURCES/EntityHint_Step4.png" width="100%" height="100%">
  - 5: Enter your server name and choose db TOUS, server name can be see in ssms
<img src="./README_SOURCES/EntityHint_Step5.png" width="100%" height="100%">
  - 6: Check if the name is TOUSEntites
<img src="./README_SOURCES/EntityHint_Step6.png" width="100%" height="100%">
  - 7: Untick diagram if existed then click finish
<img src="./README_SOURCES/EntityHint_Step7.png" width="100%" height="100%">
  - 8: Done

**2. Excel Framework:**
- Check if installed packages
<img src="./README_SOURCES/ExcelDataReader.png" width="100%" height="100%">
<img src="./README_SOURCES/Microsoft_crm.png" width="100%" height="100%">

**3. UI Framework:**
- Check if installed packages
<img src="./README_SOURCES/UI.png" width="100%" height="100%">

## Code Convention

[**1. Sql server name convention:**](https://github.com/ktaranov/sqlserver-kit/blob/master/SQL%20Server%20Name%20Convention%20and%20T-SQL%20Programming%20Style.md)

**2. Git branch name convention:**

- author_feature_branch-name
- Note:
  - author is coder name
  - branch-name is lowercase and separate by "-"

[**3. C# code convention:**](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

## Design

- <https://www.figma.com/file/ziPIxtmgZSOakax4XBTcOe/Demo?node-id=0%3A1&t=RAIXHg2XJCMXlSCf-1>

## Database diagram

<img src="./README_SOURCES/DatabaseDiagram.png" width="100%" height="100%">

# Author

1. Tran Vo Son Tung
2. Nguyen Van Quoc Tuan
3. Nguyen Phu Thinh
4. Le Doan Tan Tri
5. Vu Hoang Quan
