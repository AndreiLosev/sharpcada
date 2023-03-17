namespace srcback.Core.Helpers;

public static class BitByte
{
    public static bool GetBit(this byte b, byte pos)
    {
        if (pos > 7)
        {
            throw new OverflowException($"trying to get bit {pos} from a byte");
        }

        var x = b >> pos;

        return (x & 1) == 1;
    }
}
