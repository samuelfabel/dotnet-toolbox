# dotnet-toolbox

A collection of small .NET examples and utilities for solving practical development problems.

Each directory contains an independent example or component. The code is intended to be read, run, and adapted to a specific project.

## Projects

| Area | Description |
|---|---|
| [serialization/iso8583](serialization/iso8583) | ISO 8583 message field encoding, bitmap handling, serialization, and deserialization |

## Requirements

- .NET SDK 10.0 for the ISO 8583 example.

Individual projects may have their own requirements. Check the project file and README before running them.

## Run an example

From the repository root:

```bash
dotnet run --project serialization/iso8583/Iso8583.csproj
```

The ISO 8583 console example creates a message with response code DE39, serializes it to bytes, prints the hexadecimal representation, and deserializes it.

## Scope

The examples focus on implementation details that are useful in day-to-day development. They are not intended to define a universal protocol profile or replace the documentation of the systems they integrate with.

For protocol-specific behavior, use the format and field definitions required by the target system.
