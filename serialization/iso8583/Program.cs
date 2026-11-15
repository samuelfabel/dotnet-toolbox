using Iso8583.Message;
using Iso8583.Serialization;

var message = new Iso8583Message(new byte[8])
{
    ResponseCode = "00"
};

using var stream = new MemoryStream();

Iso8583Serializer.Serialize(stream, message);

var data = stream.ToArray();

Console.WriteLine($"Serialized: {Convert.ToHexString(data)}");

var decoded = Iso8583Serializer.Deserialize(data);

Console.WriteLine($"Response code: {decoded.ResponseCode}");
