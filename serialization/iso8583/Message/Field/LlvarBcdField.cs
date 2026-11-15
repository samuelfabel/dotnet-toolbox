namespace Iso8583.Message.Field;

/// <summary>
/// LlvarBcdField is a field that stores a variable length BCD encoded string.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public class LlvarBcdField(int index, Action<Iso8583Message, string> setter, Func<Iso8583Message, string> getter, int? maxLength = null) : VarBcdField(index, VarConfig.Ll, setter, getter, maxLength)
{
}