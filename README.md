# MicrobloggingApp Tests

This repository contains unit tests for the `MicrobloggingApp` project. The tests are written using the xUnit framework and cover various components of the application, including repositories and query handlers.

## Table of Contents

- [Introduction](#introduction)
- [Technologies](#technologies)
- [Setup](#setup)
- [Running Tests](#running-tests)
- [Test Coverage](#test-coverage)
- [Contributing](#contributing)
- [License](#license)

## Introduction

The `MicrobloggingApp` project is a microblogging platform that allows users to create and share posts. This repository focuses on testing the core functionalities of the application to ensure reliability and correctness.

## Technologies

- .NET 8
- C# 12.0
- xUnit
- Moq
- Entity Framework Core (In-Memory Database)

## Setup

To set up the project locally, follow these steps:

1. Clone the repository:

    git clone https://github.com/yourusername/microbloggingapp-tests.git
cd microbloggingapp-tests


2. Open the solution in Visual Studio 2022.

3. Restore the NuGet packages:
    
## Running Tests

To run the tests, you can use the Test Explorer in Visual Studio or run the following command in the terminal:

dotnet test


## Test Coverage

### PostRepositoryTests

- **GetAllAsync_ShouldReturnAllPosts**: Verifies that the `GetAllAsync` method returns all posts from the repository.
- **AddAsync_ShouldAddPost**: Verifies that the `AddAsync` method adds a post to the repository.

### GetTimelineQueryHandlerTests

- **Handle_ReturnsTimelineDtos**: Verifies that the `Handle` method returns the correct timeline DTOs.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes. Ensure that your code follows the existing coding style and includes appropriate tests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
