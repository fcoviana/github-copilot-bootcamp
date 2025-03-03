# CheeseCaveDotnet

## Description
CheeseCaveDotnet is a .NET application designed to monitor and control the environment within a cheese cave. It uses a BME280 sensor to measure temperature and humidity, and a GPIO-controlled fan to maintain the desired conditions. The application communicates with Azure IoT Hub to send telemetry data and receive direct method calls to control the fan.

## Setup Instructions
1. **Clone the Repository**:
    ```sh
    git clone https://github.com/yourusername/CheeseCaveDotnet.git
    cd CheeseCaveDotnet
    ```

2. **Install .NET 6.0 SDK**:
    Download and install the .NET 6.0 SDK from the [official website](https://dotnet.microsoft.com/download/dotnet/6.0).

3. **Restore Dependencies**:
    ```sh
    dotnet restore
    ```

4. **Build the Project**:
    ```sh
    dotnet build
    ```

5. **Set Up Azure IoT Hub**:
    - Create an Azure IoT Hub and register a new device.
    - Copy the device connection string and replace the placeholder in [`Program.cs`](Program.cs):
      ```csharp
      private static readonly string s_deviceConnectionString = "YOUR DEVICE CONNECTION STRING HERE";
      ```

6. **Run the Application**:
    ```sh
    dotnet run
    ```

## Usage
- **Monitoring Conditions**:
  The application continuously reads temperature and humidity data from the BME280 sensor and sends telemetry data to Azure IoT Hub at regular intervals.

- **Controlling the Fan**:
  You can control the fan state (on/off) by sending direct method calls from Azure IoT Hub. The fan state is also updated in the device twin properties.

## Contributor Guidelines
We welcome contributions to CheeseCaveDotnet! To contribute, please follow these steps:
1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Make your changes and commit them with clear and concise messages.
4. Push your changes to your forked repository.
5. Create a pull request to the main repository.

Please ensure your code adheres to the project's coding standards and includes appropriate tests.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.