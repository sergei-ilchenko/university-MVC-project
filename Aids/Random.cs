using Aids.GoF.Behav;
namespace Aids;

public static class Random {

    private static readonly System.Random r = new();
    public static long Int64(long min = long.MinValue, long max = long.MaxValue)
        => (min <= max) ? r.NextInt64(min, max) : r.NextInt64(max, min);
    public static sbyte Int8(sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue)
        => (sbyte)Int32(min, max);
    public static short Int16(short min = short.MinValue, short max = short.MaxValue)
        => (short)Int32(min, max);
    public static int Int32(int min = int.MinValue, int max = int.MaxValue)
        => (min <= max) ? r.Next(min, max) : r.Next(max, min);
    public static byte Uint8(byte min = byte.MinValue, byte max = byte.MaxValue)
        => (byte)Int32(min, max);
    public static ushort Uint16(ushort min = ushort.MinValue, ushort max = ushort.MaxValue)
        => (ushort)Int32(min, max);
    public static uint Uint32(uint min = uint.MinValue, uint max = uint.MaxValue)
        => (uint)Int64(min, max);
    public static ulong Uint64(ulong min = ulong.MinValue, ulong max = ulong.MaxValue)
    {
        var min64 = long.MinValue + (long)min;
        var max64 = long.MaxValue + (long)max;
        var int64 = Int64(min64, max64);
        var r = (ulong)(int64 - long.MinValue);
        return r;
    }
    public static double Double(double min = double.MinValue, double max = double.MaxValue)
    {
        bool isInfinity(double d) => double.IsInfinity(d) || double.IsNaN(d);
        if (min > max) (min, max) = (max, min);
        var d = r.NextDouble();
        var dmin = d * min;
        var dmax = d * max;
        d = min + dmax - dmin;
        if (isInfinity(d)) d = min - dmin + dmax;
        return d;
    }
    public static decimal Decimal(decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
    {
        if (min > max) (min, max) = (max, min);
        var i = Uint64();
        var d = (decimal)i / ulong.MaxValue;
        var dmin = d * min;
        var dmax = d * max;
        try {
            return min + dmax - dmin;
        }
        catch {
            return min - dmin + dmax;
        }
    }
    public static float Float(float min = float.MinValue, float max = float.MaxValue)
        => (float)Double(min, max);
    public static bool Boolean() => Int32() % 2 == 0;
    public static char Char(char min = (char)ushort.MinValue, char max = (char)ushort.MaxValue) => (char)Int32(min, max);
    public static char Char(string? allowedChars = null) =>
        (allowedChars == null)
            ? Char('!', '}')
            : allowedChars[Int32(0, allowedChars.Length)];
    public static string String(ushort minLength = 5, ushort maxLength = 10,
        string? allowedChars = null)
    {
        var lenght = Uint16(minLength, maxLength);
        var s = new char[lenght];
        for (var i = 0; i < lenght; i++) s[i] = Char(allowedChars);
        return new(s);
    }
    public static DateTime DateTime(DateTime min = default, DateTime max = default)
        => new(Int64(ticks(min), ticks(max, false)));
    public static DateOnly DateOnly(DateOnly min = default, DateOnly max = default)
    {
        var randomDateTime = DateTime();
        return System.DateOnly.FromDateTime(randomDateTime);
    }
    public static object? EnumOf(Type t)
    {
        if (!t.IsEnum) return null;
        var values = Enum.GetValues(t);
        var index = Int32(0, values.Length);
        return values.GetValue(index);
    }
    public static T Object<T>() where T : class, new()
    {
        var o = new T();
        return (Object(o) as T) ?? o;
    }
    public static object? Object(Type t)
    {
        var o = Activator.CreateInstance(t);
        return Object(o);
    }
    public static object? Object(object? o)
    {
        var properties = o?.GetType().GetProperties() ?? [];
        foreach (var p in properties)
        {
            if (p.CanWrite)
            {
                var v = random(p.PropertyType);
                if (v is not null) p.SetValue(o, v);
            }
        }
        return o;
    }
    private static long ticks(DateTime dt, bool isMin = true)
    {
        var minDt = System.DateTime.MinValue;
        var maxDt = System.DateTime.MaxValue;
        dt = dt == default ? isMin ? minDt : maxDt : dt;
        return dt.Ticks;
    }
    public static object? random(Type t)
    {
        if (t == null) return null;
        var s = new Round(2);
        var x = Nullable.GetUnderlyingType(t);
        if (x is not null) t = x;
        if (t.IsEnum) return EnumOf(t);
        if (t == typeof(byte)) return Uint8(0, 10);
        if (t == typeof(ushort)) return Uint16(0, 100);
        if (t == typeof(uint)) return Uint32(0, 1000);
        if (t == typeof(ulong)) return Uint64(0, 10000);
        if (t == typeof(sbyte)) return Int8(-10, 10);
        if (t == typeof(short)) return Int16(-100, 100);
        if (t == typeof(int)) return Int32(-1000, 1000);
        if (t == typeof(long)) return Int64(-10000, 10000);
        if (t == typeof(bool)) return Boolean();
        if (t == typeof(char)) return Char('A', 'Z');
        if (t == typeof(string)) return String(5, 10, "abcdefghijklmnopqrstuvwxyz");
        if (t == typeof(double)) return Double(-100.0, 100.0).DoRound(s);
        if (t == typeof(float)) return Float(-10.0f, 10.0f).DoRound(s);
        if (t == typeof(decimal)) return Decimal(0, 100.00m).DoRound(s);
        if (t == typeof(DateTime)) return DateTime(System.DateTime.Now.AddYears(-100), System.DateTime.Now);
        if (t == typeof(DateOnly)) return DateOnly(System.DateOnly.FromDateTime(System.DateTime.Now.AddYears(-100)),
            System.DateOnly.FromDateTime(System.DateTime.Now.AddYears(10)));
        return null;
    }
    public static T? Type<T>() => (T?)random(typeof(T));
}