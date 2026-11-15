using Iso8583.Message;

namespace Iso8583.Serialization;

/// <summary>
/// Serializes an ISO 8583 message to a stream.
/// </summary>
public static class Iso8583Serializer
{
    /// <summary>
    /// Serializes an ISO 8583 message to a stream.
    /// </summary>
    /// <param name="stream">The stream to serialize to.</param>
    /// <param name="message">The ISO 8583 message to serialize.</param>
    public static void Serialize(Stream stream, Iso8583Message message)
    {
        const long initialPosition = 0;

        stream.Write(new byte[8]);

        var bitmap = new byte[8];

        foreach (var field in Iso8583Message.Map.Fields)
        {
            if (field.Serialize(message, stream))
            {
                bitmap[field.BitMapPosition] |= field.BitMapValue;
            }
        }

        long currentPosition = stream.Position;

        stream.Position = initialPosition;
        stream.Write(bitmap);
        stream.Position = currentPosition;
    }

    /// <summary>
    /// Deserializes an ISO 8583 message from a span of bytes.
    /// </summary>
    /// <param name="data">The data to deserialize from.</param>
    public static Iso8583Message Deserialize(ReadOnlySpan<byte> data)
    {
        var bitmap = data[..8];

        var message = new Iso8583Message(bitmap);

        var offset = 8;

        foreach (var field in Iso8583Message.Map.Fields)
        {
            if ((bitmap[field.BitMapPosition] & field.BitMapValue) == field.BitMapValue)
            {
                field.Deserialize(message, data, ref offset);
            }
        }

        return message;
    }
}