# ISO 8583

Practical example of serializing and deserializing ISO 8583 messages using C#.

The goal is to demonstrate the mechanics behind an ISO 8583 message rather than provide a complete implementation of the standard.

## What this example demonstrates

- MTI encoding
- Primary bitmap generation
- Bitmap parsing
- Fixed-length fields
- Variable-length fields
- Field definitions
- Message serialization
- Message deserialization
- Conversion between `string` and `byte[]`
- Round-trip serialization/deserialization

## Message structure

An ISO 8583 message is generally composed of:

```text
┌─────────────┐
│     MTI     │
├─────────────┤
│   Bitmap    │
├─────────────┤
│   Fields    │
│             │
│   Field 3   │
│   Field 4   │
│   Field 11  │
│   Field 41  │
│     ...     │
└─────────────┘
```

## Example

```text
MTI: 0100

Fields:

003 = 000000
004 = 000000001000
011 = 123456
041 = 12345678
```

The serializer converts the structured message into its wire representation.

The deserializer performs the inverse operation.

## Important

ISO 8583 is not a single universal wire format.

Different payment networks and implementations may define different:

- field definitions
- encodings
- length prefixes
- bitmap representations
- numeric formats
- character encodings
- headers
- message framing

This example intentionally uses a simplified profile so that the serialization mechanics are easy to understand.

## Goal

The important part of this example is not creating a production-ready ISO 8583 library.

It is understanding how a structured message such as:

```text
MTI + fields
```

becomes a compact wire representation:

```text
MTI + bitmap + encoded fields
```

and how the original structure can be reconstructed from the bytes received.