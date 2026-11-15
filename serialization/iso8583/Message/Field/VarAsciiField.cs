using Iso8583.Message.Encoders;

namespace Iso8583.Message.Field;

/// <summary>
/// VarAsciiField is a field that stores a variable length ASCII string.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="config">The configuration for the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public abstract class VarAsciiField(int index, VarConfig config, Action<Iso8583Message, string> setter, Func<Iso8583Message, string?> getter, int? maxLength = null) : VarField(index, config)
{
    /// <inheritdoc/>
    public override int Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        var value = VarEncoder.Decode(data, config.LengthLength, ref offset);

        setter(message, value);
        offset += value.Length + config.LengthLength;

        return value.Length + config.LengthLength;
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

        VarEncoder.Encode(value, config.LengthLength, stream);

        return true;
    }
}
