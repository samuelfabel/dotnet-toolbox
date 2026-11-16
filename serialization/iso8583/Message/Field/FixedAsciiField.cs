using Iso8583.Message.Encoders;

namespace Iso8583.Message.Field;

/// <summary>
/// FixedAsciiField is a field that stores a fixed length ASCII string.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="length">The length of the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
/// <param name="paddingChar">The padding character to use if the value is less than the field length.</param>
public class FixedAsciiField(int index, int length, Action<Iso8583Message, string> setter, Func<Iso8583Message, string?> getter, char paddingChar = ' ') : Iso8583Field(index, length)
{
    /// <inheritdoc/>
    public override void Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        var value = FixedEncoder.Decode(data, ref offset, Length);

        setter(message, value);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown if the value is too long for the field.</exception>
    public override bool Serialize(Iso8583Message message, Stream stream)
    {
        var value = getter(message);

        if (value is null)
            return false;

        FixedEncoder.Encode(value, stream, Length, paddingChar);

        return true;
    }
}