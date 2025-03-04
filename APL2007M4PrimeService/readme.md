# Prime Number Service

A simple C# library that provides functionality to check if a number is prime.

![Prime Number Service](images/prime-service-banner.png)

## Features

- Check if a number is prime using the [`System.Numbers.PrimeService`](APL2007M4PrimeService/Numbers/PrimeService.cs) class
- Comprehensive unit tests using xUnit
- Supports .NET 9.0

## Prerequisites

- .NET 9.0 SDK or later

## Installation

To install the Prime Number Service library, you can use the .NET CLI:

```bash
dotnet add package PrimeService
```

## Build Instructions

To build the project, navigate to the project directory and run:

```bash
dotnet build
```

## Usage

Here's a basic example of how to use the PrimeService:

```csharp
using System.Numbers;

var primeService = new PrimeService();
bool isPrime = primeService.IsPrime(7); // Returns true
```

You can also check multiple numbers in a loop:

```csharp
using System.Numbers;

var primeService = new PrimeService();
int[] numbers = { 2, 3, 4, 5, 6, 7, 8, 9, 10 };

foreach (var number in numbers)
{
    Console.WriteLine($"{number} is prime: {primeService.IsPrime(number)}");
}
```

![Prime Number Check](images/prime-check-example.png)

## Methods

### `bool IsPrime(int number)`

Checks if the given number is a prime number.

**Parameters:**

- `number` (int): The number to check.

**Returns:**

- `bool`: `true` if the number is prime, otherwise `false`.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes. Make sure to include unit tests for any new functionality.

## License

This project is licensed under the MIT License.