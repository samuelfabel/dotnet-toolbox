namespace Iso8583.Message.Field;

/// <summary>
/// LllvarAsciiField is a field that stores a variable length ASCII string.
/// </summary>
/// <param name="index">The index of the field.</param>
/// <param name="setter">The action to set the value of the field.</param>
/// <param name="getter">The function to get the value of the field.</param>
public class LllvarAsciiField(int index, Action<Iso8583Message, string> setter, Func<Iso8583Message, string?> getter, int? maxLength = null) : VarAsciiField(index, VarConfig.Lll, setter, getter, maxLength)
{
}