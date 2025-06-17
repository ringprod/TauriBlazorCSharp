# TauriBlazorCSharp
Tauri with Blazor and C# backend

This project is a demonstration of how to build a desktop application using Tauri for the shell, Blazor WebAssembly for the UI, and a C# backend for the application logic.

## Project Structure

- `src`: The Blazor WebAssembly frontend project.
- `src-dotnet`: A .NET 8 class library that acts as the application's backend. It's loaded by Tauri as a plugin.
- `src-tauri`: The Rust project that configures and runs the Tauri application.

## How it works

1.  **Tauri** provides the native window and a bridge between the web frontend and the system.
2.  **Blazor WASM** runs in the Tauri webview, providing a rich UI framework with C#.
3.  The **.NET Backend** is a separate C# project that contains business logic (e.g., file system access, git operations).
4.  `TauriDotNetBridge` is used to communicate between the Rust host (Tauri) and the .NET backend.
5.  The Blazor frontend calls the .NET backend via Tauri's `invoke` mechanism, which is routed through Rust to the C# code.

## Getting Started

### Prerequisites

-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [Rust and Cargo](https://www.rust-lang.org/tools/install)
-   [Node.js and npm](https://nodejs.org/)
-   [Tauri CLI prerequisites](https://tauri.app/v2/guides/getting-started/prerequisites)

### Running the application

1.  Install Node.js dependencies:
    ```sh
    npm install
    ```
2.  Run the application in development mode:
    ```sh
    npm run tauri dev
    ```
This command will:
- Build the .NET backend plugin.
- Start the Blazor dev server with hot reload.
- Launch the Tauri application window.

### Credit
[plainionist's brain-overflow](https://github.com/plainionist/brain-overflow/)
and DownD's [updated fork](https://github.com/DownD/TauriDotNetBridge)
