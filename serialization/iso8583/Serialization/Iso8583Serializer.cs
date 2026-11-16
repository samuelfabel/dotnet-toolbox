using Iso8583.Message;
using Iso8583.Message.Encoders;

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
        stream.Write(new byte[12]);

        var bitmap = new byte[8];

        foreach (var field in Iso8583Message.Map.Fields)
        {
            try
            {
                if (field.Serialize(message, stream))
                {
                    bitmap[field.BitMapPosition] |= field.BitMapValue;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error serializing field {field.Index}", ex);
            }
        }

        long currentPosition = stream.Position;

        stream.Position = 0;
        stream.Write(BcdEncoder.Encode(currentPosition - 2, 4));
        stream.Write(BcdEncoder.Encode(message.Mti));
        stream.Write(bitmap);
        stream.Position = currentPosition;
    }

    /// <summary>
    /// Deserializes an ISO 8583 message from a span of bytes.
    /// </summary>
    /// <param name="data">The data to deserialize from.</param>
    public static Iso8583Message Deserialize(ReadOnlySpan<byte> data, ref int offset)
    {
        var size = BcdEncoder.Decode<int>(data, ref offset, 4);
        data = data.Slice(offset - 2, size + 2);

        var mti = BcdEncoder.Decode(data, ref offset, 4);

        var bitmap = data.Slice(offset, 8);

        offset += 8;

        var message = new Iso8583Message(mti, bitmap);

        foreach (var field in Iso8583Message.Map.Fields)
        {
            if ((bitmap[field.BitMapPosition] & field.BitMapValue) == field.BitMapValue)
            {
                try
                {
                    field.Deserialize(message, data, ref offset);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Error deserializing field {field.Index}", ex);
                }
            }
        }

        return message;
    }
}