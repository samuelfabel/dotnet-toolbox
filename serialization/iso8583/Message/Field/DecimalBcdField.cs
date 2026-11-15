using Iso8583.Message.Encoders;

namespace Iso8583.Message.Field;

/// <summary>
/// DoubleBcdField is a field that stores a double value in BCD format.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="precision">The precision of the value to serialize.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>    
public class DecimalBcdField(int index, int precision, Action<Iso8583Message, decimal> setter, Func<Iso8583Message, decimal?> getter) : Iso8583Field(index, 12)
{
    private readonly decimal _maxValue = (decimal)Math.Pow(10, precision) - 1;

    /// <summary>
    /// Deserializes the field from the message.
    /// </summary>
    /// <param name="message">The message to deserialize from.</param>
    /// <param name="data">The data to deserialize from.</param>
    /// <param name="offset">The offset to deserialize from.</param>
    /// <returns>The number of bytes deserialized.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the data is not available.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the value is too large for the field.</exception>
    public override int Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        var value = BcdEncoder.DecodeDecimal(data, offset, Length, precision);
        if (value > _maxValue)
            throw new InvalidOperationException($"Value is too large for the field. Maximum value is {_maxValue}.");

        setter(message, value);
        offset += Length;
        return Length;
    }

    /// <summary>
    /// Serializes the field to the message.
    /// </summary>
    /// <param name="message">The message to serialize to.</param>
    /// <param name="stream">The stream to serialize to.</param>
    /// <returns>True if the field was serialized successfully, false otherwise.</returns>
    public override bool Serialize(Iso8583Message message, Stream stream)
    {
        var value = getter(message);
        if (value is null)
            return false;

        var buffer = new byte[Length];
        BcdEncoder.Encode(value.Value, precision, buffer);
        stream.Write(buffer);

        return true;
    }

}