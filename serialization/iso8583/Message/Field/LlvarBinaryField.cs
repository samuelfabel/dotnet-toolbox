namespace Iso8583.Message.Field;

/// <summary>
/// LlvarBinaryField is a field that stores a variable length binary string.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public class LlvarBinaryField(int index, Action<Iso8583Message, byte[]> setter, Func<Iso8583Message, byte[]> getter, int? maxLength = null) : VarBinaryField(index, VarConfig.Ll, setter, getter, maxLength)
{

}