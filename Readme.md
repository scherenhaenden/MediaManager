
# Music Storage Application

This project is a music storage application developed using .NET 7.0 and Entity Framework Core 7. It provides functionalities to manage music files and their associated information, such as title, artist, and genre. The application is designed with a three-layer architecture, consisting of Data, BusinessLogic, and GUI layers. The frontend is built using ASP.NET Blazor, providing a user-friendly interface for managing music data. The project also includes comprehensive NUnit tests for ensuring the correctness and reliability of the application.

## Features

- Add, edit, and delete music files along with their metadata (title, artist, genre).
- Utilize an in-memory database for efficient data management during testing and development.
- Three-layer architecture to maintain separation of concerns and modularity.
- User-friendly web interface powered by ASP.NET Blazor for a smooth user experience.
- Extensive NUnit test suite to validate the functionality and robustness of the application.

## Requirements

- .NET 7.0 SDK
- Entity Framework Core 7
- ASP.NET Blazor
- NUnit for testing

## Installation

1. Clone the repository: `git clone https://github.com/yourusername/your-repo.git`
2. Navigate to the project directory: `cd your-repo`
3. Build the solution: `dotnet build`
4. Run the application: `dotnet run`

## Usage

1. Launch the application by running the command `dotnet run`.
2. Access the application through your web browser using the provided URL (e.g., `http://localhost:5000`).
3. Use the web interface to manage music files and their metadata.
4. Add, edit, or delete music files by providing the required information in the respective forms.
5. The application will utilize the in-memory database for data storage during testing and development.

## Project Structure

The project is organized into the following folders:

- `Data`: Contains the data access layer, including the database context and data models.
- `BusinessLogic`: Contains the business logic layer, responsible for handling business rules and operations.
- `GUI`: Contains the ASP.NET Blazor frontend, providing the user interface for the application.
- `Tests`: Contains the NUnit test projects for unit testing the application.

## Testing

To run the NUnit tests, execute the following command:

```
dotnet test
```

The tests will verify the correctness of various functionalities and ensure the application's reliability.

## License

This project is licensed under the [MIT License](LICENSE). Feel free to use, modify, and distribute it according to the terms of the license.

## Contributions

Contributions to this project are welcome. If you encounter any issues or have suggestions for improvements, please open an issue or submit a pull request.

---

Feel free to customize this Readme template to better suit your project. Replace placeholders like `your-repo` and `yourusername` with the appropriate values. Include more detailed information about your project, its functionalities, and any other relevant details for potential users and contributors.