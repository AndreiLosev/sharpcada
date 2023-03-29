using sharpcada.Core.Enams;
using sharpcada.Exception;
using sharpcada.Core.DTO;

namespace srcback.Core.Helpers.ByteOrderConverter;


public class ByteOrderConverter
{
    public byte[] Applay(ByteOrder byteOrder, byte[] value) =>
        byteOrder switch
        {
            ByteOrder.BigEndian => value,
            ByteOrder.LittleEndian => value.Reverse().ToArray(),
            _ => throw new UnimplementedExceprion(),
        };

    public ForDeviceParametr[] Applay(ByteOrder byteOrder, ForDeviceParametr[] value) =>
        byteOrder switch
        {
            ByteOrder.BigEndian => value,
            ByteOrder.LittleEndian => value.Select(i => i.Value.Length switch
                {
                    1 => i,
                    _ => new ForDeviceParametr(i.Value.Reverse().ToArray(), i.ParamAddres),
                }).ToArray(), 
            _ => throw new UnimplementedExceprion(),
        };
}
