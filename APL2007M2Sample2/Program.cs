using System.Device.Gpio;
using System.Device.I2c;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.ReadResult;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System.Text;

namespace CheeseCaveDotnet;

class Device
{
    // GPIO pin number for controlling the fan
    private static readonly int s_pin = 21;
    // GPIO controller instance
    private static GpioController s_gpio;
    // I2C device instance for communicating with the BME280 sensor
    private static I2cDevice s_i2cDevice;
    // BME280 sensor instance
    private static Bme280 s_bme280;

    // Acceptable temperature range above or below the desired temperature, in degrees Fahrenheit
    const double DesiredTempLimit = 5;
    // Acceptable humidity range above or below the desired humidity, in percentages
    const double DesiredHumidityLimit = 10;
    // Interval at which telemetry is sent to the cloud, in milliseconds
    const int IntervalInMilliseconds = 5000;

    // Azure IoT Hub device client instance
    private static DeviceClient s_deviceClient;
    // Current state of the fan
    private static stateEnum s_fanState = stateEnum.off;

    // Connection string for the Azure IoT Hub device
    private static readonly string s_deviceConnectionString = "YOUR DEVICE CONNECTION STRING HERE";

    // Enumeration for the fan state
    enum stateEnum
    {
        off,
        on,
        failed
    }

    // Main entry point of the application
    private static void Main(string[] args)
    {
        // Initialize GPIO controller and open the pin for output
        s_gpio = new GpioController();
        s_gpio.OpenPin(s_pin, PinMode.Output);

        // Initialize I2C settings and create the I2C device
        var i2cSettings = new I2cConnectionSettings(1, Bme280.DefaultI2cAddress);
        s_i2cDevice = I2cDevice.Create(i2cSettings);

        // Initialize the BME280 sensor
        s_bme280 = new Bme280(s_i2cDevice);

        // Display a startup message
        ColorMessage("Cheese Cave device app.\n", ConsoleColor.Yellow);

        // Create the device client for Azure IoT Hub
        s_deviceClient = DeviceClient.CreateFromConnectionString(s_deviceConnectionString, TransportType.Mqtt);

        // Set the method handler for the "SetFanState" direct method
        s_deviceClient.SetMethodHandlerAsync("SetFanState", SetFanState, null).Wait();

        // Start monitoring conditions and updating the twin properties
        MonitorConditionsAndUpdateTwinAsync();

        // Wait for user input to exit
        Console.ReadLine();
        // Close the GPIO pin
        s_gpio.ClosePin(s_pin);
    }

    // Monitors the conditions and updates the twin properties asynchronously
    private static async void MonitorConditionsAndUpdateTwinAsync()
    {
        while (true)
        {
            // Read sensor data from the BME280 sensor
            Bme280ReadResult sensorOutput = s_bme280.Read();

            // Update the twin properties with the current temperature and humidity
            await UpdateTwin(
                sensorOutput.Temperature.Value.DegreesFahrenheit,
                sensorOutput.Humidity.Value.Percent);

            // Wait for the specified interval before the next reading
            await Task.Delay(IntervalInMilliseconds);
        }
    }

    // Handles the "SetFanState" direct method from Azure IoT Hub
    private static Task<MethodResponse> SetFanState(MethodRequest methodRequest, object userContext)
    {
        if (s_fanState is stateEnum.failed)
        {
            // If the fan is in a failed state, return an error response
            string result = "{\"result\":\"Fan failed\"}";
            RedMessage("Direct method failed: " + result);
            return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 400));
        }
        else
        {
            try
            {
                // Parse the fan state from the method request data
                var data = Encoding.UTF8.GetString(methodRequest.Data);
                data = data.Replace("\"", "");
                s_fanState = (stateEnum)Enum.Parse(typeof(stateEnum), data);
                GreenMessage("Fan set to: " + data);

                // Set the GPIO pin value based on the fan state
                s_gpio.Write(s_pin, s_fanState == stateEnum.on ? PinValue.High : PinValue.Low);

                // Return a success response
                string result = "{\"result\":\"Executed direct method: " + methodRequest.Name + "\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
            }
            catch
            {
                // If there is an error, return an invalid parameter response
                string result = "{\"result\":\"Invalid parameter\"}";
                RedMessage("Direct method failed: " + result);
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 400));
            }
        }
    }

    // Updates the twin properties with the current temperature and humidity
    private static async Task UpdateTwin(double currentTemperature, double currentHumidity)
    {
        var reportedProperties = new TwinCollection();
        reportedProperties["fanstate"] = s_fanState.ToString();
        reportedProperties["humidity"] = Math.Round(currentHumidity, 2);
        reportedProperties["temperature"] = Math.Round(currentTemperature, 2);
        await s_deviceClient.UpdateReportedPropertiesAsync(reportedProperties);

        GreenMessage("Twin state reported: " + reportedProperties.ToJson());
    }

    // Displays a message in the specified console color
    private static void ColorMessage(string text, ConsoleColor clr)
    {
        Console.ForegroundColor = clr;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    // Displays a message in green color
    private static void GreenMessage(string text) =>
        ColorMessage(text, ConsoleColor.Green);

    // Displays a message in red color
    private static void RedMessage(string text) =>
        ColorMessage(text, ConsoleColor.Red);
}