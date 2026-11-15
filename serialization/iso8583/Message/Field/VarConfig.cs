namespace Iso8583.Message.Field;

/// <summary>
/// VarConfig is a configuration for a variable length field.
/// </summary>
/// <param name="LengthLength">The length of the length field.</param>
public record VarConfig(int LengthLength)
{
    /// <summary>
    /// The configuration for a variable length field with 2 bytes of length.
    /// </summary>
    public static readonly VarConfig Ll = new(2);
    /// <summary>
    /// The configuration for a variable length field with 3 bytes of length.
    /// </summary>
    public static readonly VarConfig Lll = new(3);
}