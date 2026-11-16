using Iso8583.Message.Encoders;

namespace Iso8583.Message.Field;

/// <summary>
/// TimeOnlyBcdField is a field that stores a TimeOnly value in BCD format.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public class TimeOnlyBcdField(int index, Action<Iso8583Message, TimeOnly> setter, Func<Iso8583Message, TimeOnly?> getter) : Iso8583Field(index, 6)
{
    /// <inheritdoc/>
    public override void Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        var value = BcdEncoder.DecodeTimeOnly(data, ref offset);

        setter(message, value);
    }

    /// <inheritdoc/>
    public override bool Serialize(Iso8583Message message, Stream stream)
    {
        var value = getter(message);

        if (value is null)
            return false;

        var buffer = BcdEncoder.Encode(value.Value);

        stream.Write(buffer);

        return true;
    }
}