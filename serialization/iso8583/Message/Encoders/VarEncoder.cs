namespace Iso8583.Message.Encoders;

/// <summary>
/// VarEncoder is a class that provides methods for encoding and decoding variable length strings.
/// </summary>
/// <remarks>
/// This class is used to encode and decode variable length strings to and from byte arrays.
/// </remarks>
public class VarEncoder : TextEncoder
{
    public static LengthEncoding LengthEncoding { get; set; } = LengthEncoding.Ascii;

    private VarEncoder() { }

    /// <summary>
    /// Encodes a string to a variable length byte array.
    /// </summary>
    /// <param name="value">The string to encode.</param>
    /// <param name="lengthLength">The length of the length field.</param>
    /// <param name="destination">The destination stream.</param>
    /// <returns>The number of bytes written to the destination stream.</returns>
    public static int Encode(string value, int lengthLength, Stream destination)
    {
        var bytes = Encoding.GetBytes(value);

        destination.Write(EncodeLength(bytes.Length, lengthLength));

        destination.Write(bytes);

        return bytes.Length + lengthLength;
    }

    /// <summary>
    /// Decodes a variable length string from a span of bytes.
    /// </summary>
    /// <param name="source">The span of bytes to decode from.</param>
    /// <param name="offset">The offset to decode from.</param>
    /// <returns>The decoded string.</returns>
    public static string Decode(ReadOnlySpan<byte> source, int lengthLength, ref int offset)
    {
        var length = DecodeLength(source, lengthLength, ref offset);

        var value = Encoding.GetString(source.Slice(offset, length));

        offset += length;

        return value;
    }

    /// <summary>
    /// Encodes the length of a string to a span of bytes.
    /// </summary>
    /// <param name="length">The length of the string.</param>
    /// <param name="lengthLength">The length of the length field.</param>
    /// <returns>The span of bytes containing the encoded length.</returns>
    public static Span<byte> EncodeLength(int length, int lengthLength)
    {
        switch (LengthEncoding)
        {
            case LengthEncoding.Ascii:
                {
                    var bytes = new byte[lengthLength];

                    for (int i = lengthLength - 1; i >= 0; i--)
                    {
                        bytes[i] = (byte)((length % 10) + '0');
                        length /= 10;
                    }
                    return bytes;
                }
            case LengthEncoding.Bcd:
                {
                    var bytes = new byte[lengthLength];
                    BcdEncoder.Encode(length, bytes);
                    return bytes;
                }
            default:
                return BitConverter.GetBytes(length).AsSpan(0, lengthLength);
        }
    }

    /// <summary>
    /// Decodes the length of a variable length string from a span of bytes.
    /// </summary>
    /// <param name="source">The span of bytes to decode from.</param>
    /// <param name="lengthLength">The length of the length field.</param>
    /// <param name="offset">The offset to decode from.</param>
    /// <returns>The decoded length.</returns>
    public static int DecodeLength(ReadOnlySpan<byte> source, int lengthLength, ref int offset)
    {
        switch (LengthEncoding)
        {
            case LengthEncoding.Ascii:
                int length = 0;

                for (int i = 0; i < lengthLength; i++)
                {
                    length = length * 10 + (source[offset + i] - '0');
                }

                offset += lengthLength;

                return length;
            case LengthEncoding.Bcd:
                return BcdEncoder.Decode<int>(source, ref offset, lengthLength);
            default:
                var bytes = source.Slice(offset, lengthLength);

                if (lengthLength < 4)
                {
                    var paddedBytes = new byte[4];
                    bytes.CopyTo(paddedBytes);
                    bytes = paddedBytes;
                }

                offset += lengthLength;

                return BitConverter.ToInt32(bytes);
        }
    }
}

/// <summary>
/// The encoding used for the length field.
/// </summary>
public enum LengthEncoding
{
    /// <summary>
    /// The length field is encoded in ASCII.
    /// </summary>
    /// <remarks>
    /// The length field is encoded as a series of ASCII characters, starting from the most significant digit.
    /// </remarks>
    Ascii,
    /// <summary>
    /// The length field is encoded in BCD.
    /// </summary>
    /// <remarks>
    /// The length field is encoded as a series of BCD digits, starting from the most significant digit.
    Bcd,
    /// <summary>
    /// The length field is encoded in binary.
    /// </summary>
    /// <remarks>
    /// The length field is encoded as a series of binary digits, starting from the most significant digit.
    /// </remarks>
    Binary
}