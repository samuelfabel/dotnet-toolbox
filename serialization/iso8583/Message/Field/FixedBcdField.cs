using Iso8583.Message.Encoders;

namespace Iso8583.Message.Field;

/// <summary>
/// FixedBcdField is a field that stores a fixed length BCD encoded string.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="length">The length of the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public class FixedBcdField(int index, int length, Action<Iso8583Message, string> setter, Func<Iso8583Message, string?> getter) : Iso8583Field(index, (length + 1) / 2)
{
    /// <inheritdoc/>
    public override void Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        var value = BcdEncoder.Decode(data, ref offset, length);

        if (value.Length > length)
            value = value.Substring(value.Length - length, length);

        setter(message, value);
    }

    /// <inheritdoc/>
    public override bool Serialize(Iso8583Message message, Stream stream)
    {
        var value = getter(message);

        if (value is null)
            return false;

        value = value.PadLeft(length, '0');

        if (value.Length % 2 != 0)
            value = $"0{value}";

        var buffer = BcdEncoder.Encode(value);

        stream.Write(buffer);

        return true;
    }
}