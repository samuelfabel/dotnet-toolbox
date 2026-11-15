using System.Numerics;

namespace Iso8583.Message.Encoders;

/// <summary>
/// Binary-Coded Decimal (BCD) encoder and decoder.
/// </sumary>
public class BcdEncoder
{
    private BcdEncoder() { }

    /// <summary>
    /// Calculates the scale factor for a given number of digits.
    /// </summary>
    /// <param name="digitCount">The number of digits to scale the value by.</param>
    /// <returns>The scale factor.</returns>
    private static decimal Scale(int digitCount)
    {
        decimal scale = 1m;

        for (int i = 0; i < digitCount; i++)
            scale = checked(scale * 10m);

        return scale;
    }

    /// <summary>
    /// Encodes a string to a BCD encoded span.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="destination">The span to encode the value to.</param>
    /// <exception cref="FormatException">Thrown if the value is invalid.</exception>
    public static void Encode(string value, Span<byte> destination)
    {
        if (value.Length % 2 != 0)
            throw new FormatException("Value must have an even number of characters.");

        for (int i = 0; i < value.Length; i++)
        {
            var b = value[i] - '0';

            if (i % 2 == 0)
            {
                destination[i / 2] = (byte)(destination[i / 2] | (b << 4));
            }
            else
            {
                destination[i / 2] = (byte)(destination[i / 2] | b);
            }
        }
    }

    /// <summary>
    /// Decodes a BCD encoded span to a string.
    /// </summary>
    /// <param name="source">The span to decode.</param>
    /// <param name="start">The start index to decode from.</param>
    /// <param name="length">The length of the value to decode.</param>
    /// <returns>The decoded value.</returns>
    public static string Decode(ReadOnlySpan<byte> source, int start, int length)
    {
        var value = new char[length];

        for (int i = 0; i < length / 2; i++)
        {
            var b = source[start + i];
            var high = b >> 4;
            var low = b & 0x0F;

            value[i * 2] = (char)(high + '0');
            value[i * 2 + 1] = (char)(low + '0');
        }

        return new string(value);
    }

    /// <summary>
    /// Serializes a decimal value to a BCD encoded span.
    /// </summary>
    /// <param name="value">The value to serialize.</param>
    /// <param name="digitCount">The number of digits to serialize.</param>
    /// <param name="destination">The span to serialize the value to.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the value is negative.</exception>
    /// <exception cref="FormatException">Thrown if the value has more decimal places than allowed.</exception>
    public static void Encode(decimal value, int digitCount, Span<byte> destination)
    {
        var number = checked(value * Scale(digitCount));

        if (number < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "BCD value cannot be negative.");

        var index = destination.Length - 1;

        while (number > 0 && index >= 0)
        {
            int low = (int)(number % 10);
            number /= 10;

            int high = (int)(number % 10);
            number /= 10;

            destination[index--] = (byte)((high << 4) | low);
        }

        if (number > 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Value does not fit in the BCD field.");
    }

    /// <summary>
    /// Deserializes a decimal value from a BCD encoded span.
    /// </summary>
    /// <param name="source">The span to deserialize from.</param>
    /// <param name="start">The start index to deserialize from.</param>
    /// <param name="length">The length of the value to deserialize.</param>
    /// <param name="digitCount">The number of digits to deserialize.</param>
    /// <returns>The deserialized value.</returns>
    /// <exception cref="FormatException">Thrown if the value is invalid.</exception>
    public static decimal DecodeDecimal(ReadOnlySpan<byte> source, int start, int length, int digitCount)
    {
        decimal value = 0m;

        for (int i = start; i < start + length; i++)
        {
            var b = source[i];

            var high = b >> 4;
            var low = b & 0x0F;

            if (high > 9 || low > 9)
                throw new FormatException("Invalid BCD.");

            value = checked(value * 100m + high * 10m + low);
        }

        return value / Scale(digitCount);
    }

    /// <summary>
    /// Serializes a number value to a BCD encoded span.
    /// </summary>
    /// <typeparam name="T">The type of the value to serialize.</typeparam>
    /// <param name="value">The value to serialize.</param>
    /// <param name="destination">The span to serialize the value to.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the value is negative.</exception>
    /// <exception cref="FormatException">Thrown if the value is invalid.</exception>
    public static void Encode<T>(T value, Span<byte> destination) where T : INumber<T>
    {
        if (value < T.Zero)
            throw new ArgumentOutOfRangeException(
                nameof(value), "BCD value cannot be negative.");

        var number = value;
        var index = destination.Length - 1;

        destination.Clear();

        var scale = T.CreateChecked(10);

        while (number > T.Zero && index >= 0)
        {
            var low = int.CreateChecked(number % scale);
            number /= scale;

            var high = int.CreateChecked(number % scale);
            number /= scale;

            destination[index--] = (byte)((high << 4) | low);
        }

        if (number > T.Zero)
            throw new ArgumentOutOfRangeException(
                nameof(value), "Value does not fit in the BCD field.");
    }

    /// <summary>
    /// Deserializes a number value from a BCD encoded span.
    /// </summary>
    /// <typeparam name="T">The type of the value to deserialize.</typeparam>
    /// <param name="source">The span to deserialize from.</param>
    /// <param name="start">The start index to deserialize from.</param>
    /// <param name="length">The length of the value to deserialize.</param>
    /// <returns>The deserialized value.</returns>
    /// <exception cref="FormatException">Thrown if the value is invalid.</exception>
    public static T Decode<T>(ReadOnlySpan<byte> source, int start, int length) where T : INumber<T>
    {
        T value = T.Zero;

        var scale = T.CreateChecked(100);

        for (int i = start; i < start + length; i++)
        {
            var b = source[i];

            var high = b >> 4;
            var low = b & 0x0F;

            if (high > 9 || low > 9)
                throw new FormatException("Invalid BCD.");

            value = checked(value * scale + T.CreateChecked(high * 10 + low));
        }

        return value;
    }

    /// <summary>
    /// Serializes a double value to a BCD encoded span.
    /// </summary>
    /// <param name="value">The value to serialize.</param>
    /// <param name="precision">The precision of the value to serialize.</param>
    /// <param name="destination">The span to serialize the value to.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the value is negative.</exception>
    /// <exception cref="FormatException">Thrown if the value is invalid.</exception>
    public static void Encode(double value, int precision, Span<byte> destination)
    {
        if (!double.IsFinite(value))
            throw new ArgumentOutOfRangeException(
                nameof(value), "Value must be finite.");

        if (value < 0)
            throw new ArgumentOutOfRangeException(
                nameof(value), "BCD value cannot be negative.");

        var scaled = value * Math.Pow(10, precision);
        var rounded = Math.Round(scaled);

        if (Math.Abs(scaled - rounded) > 1e-9 * Math.Max(1d, Math.Abs(scaled)))
            throw new ArgumentException(
                "Value has more decimal places than allowed.", nameof(value));

        Encode((long)rounded, destination);
    }

    /// <summary>
    /// Deserializes a double value from a BCD encoded span.
    /// </summary>
    /// <param name="source">The span to deserialize from.</param>s
    /// <param name="start">The start index to deserialize from.</param>
    /// <param name="length">The length of the value to deserialize.</param>
    /// <param name="precision">The precision of the value to deserialize.</param>
    /// <returns>The deserialized value.</returns>
    /// <exception cref="FormatException">Thrown if the value is invalid.</exception>
    public static double DecodeDouble(ReadOnlySpan<byte> source, int start, int length, int precision)
    {
        var value = Decode<long>(source, start, length);

        return value / Math.Pow(10, precision);
    }

    /// <summary>
    /// Serializes a DateTime value to a BCD encoded span.
    /// </summary>
    /// <param name="value">The value to serialize.</param>
    /// <param name="destination">The span to serialize the value to.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the value is negative.</exception>
    /// <exception cref="FormatException">Thrown if the value is invalid.</exception>
    public static void Encode(DateTime value, Span<byte> destination)
    {
        var number = value.Year * 10000000000L + value.Month * 100000000L + value.Day * 1000000L + value.Hour * 10000L + value.Minute * 100L + value.Second;

        Encode(number, destination);
    }

    /// <summary>
    /// Deserializes a DateTime value from a BCD encoded span.
    /// </summary>
    /// <param name="source">The span to deserialize from.</param>
    /// <param name="start">The start index to deserialize from.</param>
    /// <param name="length">The length of the value to deserialize.</param>
    /// <returns>The deserialized value.</returns>
    /// <exception cref="FormatException">Thrown if the value is invalid.</exception>
    public static DateTime DecodeDateTime(ReadOnlySpan<byte> source, int start, int length)
    {
        var value = Decode<long>(source, start, length);

        var second = (int)(value % 100);
        value /= 100;

        var minute = (int)(value % 100);
        value /= 100;

        var hour = (int)(value % 100);
        value /= 100;

        var day = (int)(value % 100);
        value /= 100;

        var month = (int)(value % 100);
        var year = (int)(value / 100);

        return new DateTime(year, month, day, hour, minute, second);

    }

    /// <summary>
    /// Serializes a DateOnly value to a BCD encoded span.
    /// </summary>
    /// <param name="value">The value to serialize.</param>
    /// <param name="destination">The span to serialize the value to.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the value is negative.</exception>
    /// <exception cref="FormatException">Thrown if the value is invalid.</exception>
    public static void Encode(DateOnly value, Span<byte> destination)
    {
        var number = value.Year * 10000 + value.Month * 100 + value.Day;

        Encode(number, destination);
    }

    /// <summary>
    /// Deserializes a DateOnly value from a BCD encoded span.
    /// </summary>
    /// <param name="source">The span to deserialize from.</param>
    /// <param name="start">The start index to deserialize from.</param>
    /// <param name="length">The length of the value to deserialize.</param>
    /// <returns>The deserialized value.</returns>
    /// <exception cref="FormatException">Thrown if the value is invalid.</exception>
    public static DateOnly DecodeDateOnly(ReadOnlySpan<byte> source, int start, int length)
    {
        var value = Decode<int>(source, start, length);

        var day = value % 100;
        value /= 100;

        var month = value % 100;
        var year = value / 100;

        return new DateOnly(year, month, day);
    }

    /// <summary>
    /// Serializes a TimeOnly value to a BCD encoded span.
    /// </summary>
    /// <param name="value">The value to serialize.</param>
    /// <param name="destination">The span to serialize the value to.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the value is negative.</exception>
    /// <exception cref="FormatException">Thrown if the value is invalid.</exception>
    public static void Encode(TimeOnly value, Span<byte> destination)
    {
        var number = value.Hour * 10000 + value.Minute * 100 + value.Second;

        Encode(number, destination);
    }

    /// <summary>
    /// Deserializes a TimeOnly value from a BCD encoded span.
    /// </summary>
    /// <param name="source">The span to deserialize from.</param>
    /// <param name="start">The start index to deserialize from.</param>
    /// <param name="length">The length of the value to deserialize.</param>
    /// <returns>The deserialized value.</returns>
    /// <exception cref="FormatException">Thrown if the value is invalid.</exception>
    public static TimeOnly DecodeTimeOnly(ReadOnlySpan<byte> source, int start, int length)
    {
        var value = Decode<int>(source, start, length);

        var second = value % 100;
        value /= 100;

        var minute = value % 100;
        var hour = value / 100;

        return new TimeOnly(hour, minute, second);
    }
}