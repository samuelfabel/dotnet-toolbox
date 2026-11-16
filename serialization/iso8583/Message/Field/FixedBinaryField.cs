namespace Iso8583.Message.Field;

/// <summary>
/// FixedBinaryField is a field that stores a fixed length binary value.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="length">The length of the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public class FixedBinaryField(int index, int length, Action<Iso8583Message, byte[]> setter, Func<Iso8583Message, byte[]?> getter) : Iso8583Field(index, length)
{
    /// <inheritdoc/>
    public override void Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        var value = data.Slice(offset, Length);

        setter(message, value.ToArray());

        offset += Length;
    }

    /// <inheritdoc/>
    public override bool Serialize(Iso8583Message message, Stream stream)
    {
        var value = getter(message);

        if (value is null)
            return false;

        stream.Write(value);

        return true;
    }
}