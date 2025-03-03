# Prime Number Service

A simple C# library that provides functionality to check if a number is prime.

## Features

- Check if a number is prime using the [`System.Numbers.PrimeService`](APL2007M4PrimeService/Numbers/PrimeService.cs) class
- Comprehensive unit tests using xUnit
- Supports .NET 9.0

## Installation

To install the Prime Number Service library, you can use the .NET CLI:

```bash
dotnet add package PrimeService
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

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes. Make sure to include unit tests for any new functionality.

## License

This project is licensed under the MIT License.