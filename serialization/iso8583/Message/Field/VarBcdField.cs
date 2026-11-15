using Iso8583.Message.Encoders;

namespace Iso8583.Message.Field;

/// <summary>
/// VarBcdField is a field that stores a variable length BCD encoded string.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="config">The configuration for the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public abstract class VarBcdField(int index, VarConfig config, Action<Iso8583Message, string> setter, Func<Iso8583Message, string?> getter, int? maxLength = null) : VarField(index, config)
{
    /// <inheritdoc/>
    public override int Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        var length = DeserializeLength(data, ref offset);

        var packedLength = length;

        if (packedLength % 2 != 0)
            packedLength++;

        var value = BcdEncoder.Decode(data, offset, packedLength);

        if (value.Length != length)
            value = value[..length];

        setter(message, value);

        offset += packedLength / 2;

        return packedLength / 2 + config.LengthLength;
    }

    protected virtual bool Validate(string value)
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

        var buffer = new byte[value.Length / 2];

        SerializeLength(stream, value.Length);

        if (value.Length % 2 != 0)
            value += '0';

        BcdEncoder.Encode(value, buffer);
        stream.Write(buffer);

        return true;
    }
}