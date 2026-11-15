using System.Text;

namespace Iso8583.Message.Encoders;

/// <summary>
/// TextEncoder is a class that provides methods for encoding and decoding text strings.
/// </summary>
/// <remarks>
/// This class is used to encode and decode text strings to and from byte arrays.
/// </remarks>
public class TextEncoder
{
    protected TextEncoder() { }

    /// <summary>
    /// The encoding to use for the strings.
    /// </summary>
    public static Encoding Encoding { get; set; } = Encoding.ASCII;
}