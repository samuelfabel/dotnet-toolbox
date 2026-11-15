using System.Text;

namespace Iso8583.Message.Encoders;

/// <summary>
/// FixedEncoder is a class that provides methods for encoding and decoding fixed length strings.
/// </summary>
/// <remarks>
/// This class is used to encode and decode fixed length strings to and from byte arrays.
/// </remarks>
public class FixedEncoder : TextEncoder
{
    private FixedEncoder() { }

    /// <summary>
    /// Encodes a string to a fixed length byte array.
    /// </summary>
    /// <param name="value">The string to encode.</param>
    /// <param name="destination">The destination stream.</param>
    /// <param name="length">The length of the encoded string.</param>
    /// <param name="paddingChar">The padding character to use if the string is shorter than the length.</param>
    /// <exception cref="ArgumentException">Thrown if the string is too long.</exception>
    public static void Encode(string value, Stream destination, int length, char paddingChar = ' ')
    {
        value = value.PadRight(length, paddingChar);

        var bytes = Encoding.GetBytes(value);

        if (bytes.Length > length)
            throw new ArgumentException("Value is too long");

        destination.Write(bytes);
    }

    /// <summary>
    /// Decodes a fixed length byte array to a string.
    /// </summary>
    /// <param name="source">The source byte array.</param>
    /// <param name="start">The start index in the source byte array.</param>
    /// <param name="length">The length of the decoded string.</param>
    /// <returns>The decoded string.</returns>
    public static string Decode(ReadOnlySpan<byte> source, int start, int length)
    {
        return Encoding.GetString(source.Slice(start, length));
    }
}