using Iso8583.Message.Encoders;

namespace Iso8583.Message.Field;

/// <summary>
/// DateTimeBcdField is a field that stores a DateTime value in BCD format.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public class DateTimeBcdField(int index, Action<Iso8583Message, DateTime> setter, Func<Iso8583Message, DateTime?> getter) : Iso8583Field(index, 12)
{
    /// <inheritdoc/>
    public override int Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        var value = BcdEncoder.DecodeDateTime(data, offset, Length);

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

        var buffer = new byte[Length / 2];

        BcdEncoder.Encode(value.Value, buffer);

        stream.Write(buffer);

        return true;
    }

}