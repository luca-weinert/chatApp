# Chat Application

## Overview

This project is a school project that implements a simple chat application developed using C# with a WPF client and a TCP server. It demonstrates client-server communication, enabling users to send messages and receive updates in real-time. The application is structured to allow easy extensibility and maintainability, utilizing principles such as dependency injection and event-driven architecture.

## Features

- **User and System Messages**: Handles both user-generated text messages and system messages (e.g., user status updates).
- **Event-Driven Architecture**: Utilizes events for communication between the client and server, ensuring a responsive user experience.
- **Dependency Injection**: Employs DI for better separation of concerns and improved testability.
- **Robust Connection Management**: Establishes and manages TCP connections securely and reliably.
- **Cross-Platform Compatibility**: Designed to run on any system that supports .NET.

## Architecture

The application is divided into several key components:

- **Client**: A WPF application that allows users to connect to the server, send messages, and receive updates.
- **Server**: A TCP server that manages client connections, handles incoming messages, and broadcasts events to all connected clients.
- **Communication Layer**: Handles the serialization and deserialization of events between the client and server.
- **Event Management**: Centralized event handling through an `EventFactory` for consistency across the application.

## Getting Started

### Prerequisites

- .NET 8.0 or later
- Visual Studio 2022 or later (or any compatible IDE)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/luca-weinert/chatApp
   cd chat-app
   ```

2. Open the solution file (`ChatApp.sln`) in Visual Studio.

3. Restore the NuGet packages:

   ```bash
   dotnet restore
   ```

4. Build the solution:

   ```bash
   dotnet build
   ```

### Running the Application

1. Start the server by running the server project (`ChatApp.Server`).
2. Open a new instance of the client project (`ChatApp.Client.Wpf`).
3. Enter the server IP address and port and connect.
4. Start chatting!

## Usage

- **Send Messages**: Type your message in the text box and hit "Send."

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or fix.
3. Make your changes and commit them.
4. Push to your branch.
5. Create a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Thank you to the contributors of open-source libraries used in this project.
- Special thanks to the developers who inspired this project through their work.

## Contact

For any inquiries, please reach out to:

- **Luca Weinert**
- Email: [lucaweinert8@gmail.com](mailto:lucaweinert8@gmail.com)
- LinkedIn: [Luca Weinert](https://www.linkedin.com/in/luca-weinert-b9994720b/)
