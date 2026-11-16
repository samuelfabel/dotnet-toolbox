using Iso8583.Message;
using Iso8583.Serialization;

var message = new Iso8583Message("0200", new byte[8])
{
    ResponseCode = "00",
    PrimaryAccountNumber = "123456789012",
    ProcessingCode = "123456",
    AmountTransaction = 100.00m,
    AmountSettlement = 100.00m,
    AmountCardholderBilling = 100.00m,
    TransmissionDateTime = DateTime.Now,
    LocalTransactionTime = TimeOnly.FromDateTime(DateTime.Now),
    LocalTransactionDate = DateOnly.FromDateTime(DateTime.Now),
};

using var stream = new MemoryStream();

Console.WriteLine(message);

Iso8583Serializer.Serialize(stream, message);

var data = stream.ToArray();

Console.WriteLine($"Serialized: {BitConverter.ToString(data)}");

var offset = 0;

var decoded = Iso8583Serializer.Deserialize(data, ref offset);

Console.WriteLine(decoded);
