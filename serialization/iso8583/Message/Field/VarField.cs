using Iso8583.Message.Encoders;

namespace Iso8583.Message.Field;

/// <summary>
/// VarField is a base class for variable length fields.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="config">The configuration for the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public abstract class VarField(int index, VarConfig config) : Iso8583Field(index)
{
    /// <summary>
    /// Deserializes the length of the field from the message.
    /// </summary>
    /// <param name="message">The message to deserialize from.</param>
    /// <param name="data">The data to deserialize from.</param>
    /// <param name="offset">The offset to deserialize from.</param>
    /// <returns>The length of the field.</returns>
    protected virtual int DeserializeLength(ReadOnlySpan<byte> data, ref int offset)
    {
        EnsureAvailable(data, offset);

        return VarEncoder.DecodeLength(data, config.LengthLength, offset);
    }

    /// <summary>
    /// Deserializes the length of the field from the message.
    /// </summary>
    /// <param name="stream">The stream to serialize to.</param>
    /// <param name="length">The length of the field.</param>
    protected virtual void SerializeLength(Stream stream, int length)
    {
        stream.Write(VarEncoder.EncodeLength(length, config.LengthLength));
    }
}