namespace Iso8583.Message.Field;

/// <summary>
/// Iso8583Field is the base class for all ISO 8583 fields.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="length">The length of the field.</param>
public abstract class Iso8583Field(int index, int length)
{
    /// The index of the field.
    public int Index { get; } = index;
    /// The length of the field.
    public int Length { get; } = length;

    /// <summary>
    /// The position of the field in the bitmap.
    /// </summary>
    public int BitMapPosition { get; set; } = (index - 1) / 8;

    /// <summary>
    /// The value of the field in the bitmap.
    /// </summary>
    public byte BitMapValue { get; set; } = (byte)(1 << (7 - ((index - 1) % 8)));

    /// <summary>
    /// Initializes a new instance of the <see cref="Iso8583Field"/> class.
    /// </summary>
    /// <param name="index">The index of the field.</param>
    protected Iso8583Field(int index) : this(index, -1)
    {
    }

    /// <summary>
    /// Deserializes the field from the message.
    /// </summary>
    /// <param name="message">The message to deserialize from.</param>
    /// <param name="data">The data to deserialize from.</param>
    /// <param name="offset">The offset to deserialize from.</param>
    /// <exception cref="InvalidOperationException">Thrown if the data is insufficient to deserialize the field.</exception>
    public abstract void Deserialize(Iso8583Message message, ReadOnlySpan<byte> data, ref int offset);

    /// <summary>
    /// Serializes the field to the message.
    /// </summary>
    /// <param name="message">The message to serialize to.</param>
    /// <param name="stream">The stream to serialize to.</param>
    /// <returns>True if the field was serialized successfully, false otherwise.</returns>
    public abstract bool Serialize(Iso8583Message message, Stream stream);

    /// <summary>
    /// Ensures that the data is available to deserialize the field.
    /// </summary>
    /// <param name="span">The span to deserialize from.</param>
    /// <param name="offset">The offset to deserialize from.</param>
    /// <exception cref="InvalidOperationException">Thrown if the data is insufficient to deserialize the field.</exception>
    protected void EnsureAvailable(ReadOnlySpan<byte> span, int offset)
    {
        if (offset + Length > span.Length)
            throw new InvalidOperationException(
                $"Insufficient data to deserialize field {Index} at offset {offset}.");
    }
}