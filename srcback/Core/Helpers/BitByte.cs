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

    public static bool GetBit(this ushort b, byte pos)
    {
        if (pos > 15)
        {
            throw new OverflowException($"trying to get bit {pos} from a ushort");
        }

        var x = b >> pos;

        return (x & 1) == 1;
    }

    public static bool GetBit(this int b, byte pos)
    {
        if (pos > 31)
        {
            throw new OverflowException($"trying to get bit {pos} from a int");
        }

        var x = b >> pos;

        return (x & 1) == 1;
    }

    public static bool[] getAllBits(this byte b) =>
        Enumerable.Range(0, 7).Select(i => b.GetBit((byte)i)).ToArray();

    public static void SetBit(this ushort w, byte pos, bool value)
    {
        if (pos > 15)
        {
            throw new OverflowException($"trying to get bit {pos} from a ushort");
        }

        var x = (ushort)(1 << pos);

        if (value)
        {
            w = (ushort)(w | x);
            return;
        }

        if (w.GetBit(pos))
        {
            w = (ushort)(w ^ x);
            return;
        }
    }

    public static void SetBit(this int dw, byte pos, bool value)
    {
        if (pos > 31)
        {
            throw new OverflowException($"trying to get bit {pos} from a int");
        }

        var x = (ushort)(1 << pos);

        if (value)
        {
            dw = (ushort)(dw | x);
            return;
        }

        if (dw.GetBit(pos))
        {
            dw = (ushort)(dw ^ x);
            return;
        }
    }

    public static void SetByte(this int dw, byte pos, byte value)
    {
        byte offset = pos switch
        {
            0 => 0,
            1 => 7,
            2 => 15,
            3 => 23,
            _ => throw new OverflowException($"trying to set byte {pos} to int"),
        };

        for (byte i = 0; i < 8; i++)
        {
            dw.SetBit((byte)(i + offset), value.GetBit(i));
        }
    }
}
