# Bank Management System

This project is a **Bank Management System** implemented using **Onion Architecture** with ASP.NET Core. The system is designed to demonstrate a clean, maintainable, and scalable software architecture by separating concerns into multiple layers.

## Project Structure

The solution follows the Onion Architecture pattern, consisting of multiple projects:

- **Core**: Contains the domain entities, interfaces, and business logic.
- **Application**: Contains the application services and business rules.
- **Infrastructure**: Handles data access, repositories, and external services.
- **WebUI**: The presentation layer, implemented as an ASP.NET Core MVC or Web API project.

## 🚀 Features

- Manage bank accounts and customers.
- Implement business rules via domain services.
- Data persistence through repositories.
- Separation of concerns ensures easier testing and maintenance.

## 🧠 Technologies Used

- ASP.NET Core (version used in the project)
- Entity Framework Core
- C#
- Onion Architecture design pattern

## ⚙️ How to Run

1. **Clone the repository:**

   ```bash
   git clone https://github.com/NoorTantawi/Bank.git
2. **Open the solution in Visual Studio or your preferred IDE.**

3. **Update the connection string in the appropriate project (Infrastructure or WebUI) appsettings.json file to point to your SQL Server database.**

4. **Apply migrations and update the database:**

    ```bash
    dotnet ef database update
    
5. **Build and run the project.**

**Contribution**

Feel free to fork this project and submit pull requests. For major changes, please open an issue first to discuss what you would like to change.

License
This project is open source and available under the MIT License.

## 🙋‍♂️ Author

**Noor-Aldeen Tayseer Tantawi**  
[LinkedIn](https://www.linkedin.com/in/nooraldeen-tantawi-3bb899237/)  
[GitHub](https://github.com/NoorTantawi)

---

Feel free to fork, contribute, or submit issues!

