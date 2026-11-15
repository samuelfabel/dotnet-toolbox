namespace Iso8583.Message.Field;

/// <summary>
/// DumbField is a field that stores a dumb value.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="length">The length of the field.</param>
public class DumbField(int index, int length) : Iso8583Field(index, length)
{
    /// <inheritdoc/>
    public override int Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        offset += Length;

        return Length;
    }

    /// <inheritdoc/>
    public override bool Serialize(Iso8583Message message, Stream stream)
    {
        stream.Write(new byte[Length]);

        return true;
    }
}