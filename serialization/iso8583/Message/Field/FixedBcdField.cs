using Iso8583.Message.Encoders;

namespace Iso8583.Message.Field;

/// <summary>
/// FixedBcdField is a field that stores a fixed length BCD encoded string.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="length">The length of the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public class FixedBcdField(int index, int length, Action<Iso8583Message, string> setter, Func<Iso8583Message, string?> getter) : Iso8583Field(index, length)
{
    /// <inheritdoc/>
    public override int Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        var value = BcdEncoder.Decode(data, offset, Length);

        setter(message, value);

        offset += Length;

        return Length;
    }

    /// <inheritdoc/>
    public override bool Serialize(Iso8583Message message, Stream stream)
    {
        var value = getter(message);

        if (value is null)
            return false;

        if (value.Length % 2 != 0)
            value += '0';

        var destination = new byte[(Length + 1) / 2];

        BcdEncoder.Encode(value, destination);

        stream.Write(destination);

        return true;
    }
}