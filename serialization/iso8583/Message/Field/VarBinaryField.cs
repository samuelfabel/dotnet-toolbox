using Iso8583.Message.Encoders;

namespace Iso8583.Message.Field;

/// <summary>
/// VarBinaryField is a field that stores a variable length binary string.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="config">The configuration for the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public abstract class VarBinaryField(int index, VarConfig config, Action<Iso8583Message, byte[]> setter, Func<Iso8583Message, byte[]?> getter, int? maxLength = null) : VarField(index, config)
{
    /// <inheritdoc/>
    public override int Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        var length = DeserializeLength(data, ref offset);

        var value = data.Slice(offset, length);

        setter(message, value.ToArray());
        offset += length;
        return length;
    }

    protected virtual bool Validate(byte[] value)
    {
        return maxLength is null || value.Length <= maxLength;
    }

    /// <inheritdoc/>
    public override bool Serialize(Iso8583Message message, Stream stream)
    {
        var value = getter(message);

        if (value is null)
            return false;

        if (!Validate(value))
            throw new InvalidOperationException($"Value is not valid for the field.");

        SerializeLength(stream, value.Length);
        stream.Write(value);

        return true;
    }

}