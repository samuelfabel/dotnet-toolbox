

namespace Iso8583.Message.Field;

/// <summary>
/// ByteField is a field that stores a single byte.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public class ByteField(int index, Action<Iso8583Message, byte> setter, Func<Iso8583Message, byte?> getter) : Iso8583Field(index)
{
    /// <inheritdoc/>
    public override int Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        var value = data[offset];
        setter(message, value);
        offset++;
        return 1;
    }

    /// <inheritdoc/>
    public override bool Serialize(Iso8583Message message, Stream stream)
    {
        var value = getter(message);

        if (value is null)
            return false;

        stream.WriteByte(value.Value);

        return true;
    }
}