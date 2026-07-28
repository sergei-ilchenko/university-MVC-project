namespace Aids.GoF.Behav;
public interface IRoundingStrategy {
    double DoRound(double x);
    float DoRound(float x);
    decimal DoRound(decimal x);
}
public abstract class BaseRounding : IRoundingStrategy {
    public abstract double DoRound(double x);
    public float DoRound(float x) => (float)DoRound((double)x);
    public decimal DoRound(decimal x) => (decimal)DoRound((double)x);
}
public sealed class RoundUp(int decimals = 0) : BaseRounding {
    private readonly double factor = Math.Pow(10, decimals);
    public override double DoRound(double x)
    {
        var d = x * factor;
        return ((d > 0) ? Math.Ceiling(d) : Math.Floor(d)) / factor;
    }
}
public sealed class RoundDown(int decimals = 0) : BaseRounding {
    private readonly double factor = Math.Pow(10, decimals);
    public override double DoRound(double x)
    {
        var d = x * factor;
        return ((d > 0) ? Math.Floor(d) : Math.Ceiling(d)) / factor;
    }
}
public sealed class Round(int decimals = 0, int roundingDigit = 5) : BaseRounding {
    private readonly int roundingDigit = roundingDigit;
    private readonly double factor = Math.Pow(10, decimals);
    public override double DoRound(double x)
    {
        var d = x * factor;
        var rd = Math.Floor(Math.Abs(d % 1) * 10);
        if (rd < roundingDigit) return ((d > 0) ? Math.Floor(d) : Math.Ceiling(d)) / factor;
        return ((d > 0) ? Math.Ceiling(d) : Math.Floor(d)) / factor;
    }
}
public sealed class RoundTowardsPositive(int decimals = 0) : BaseRounding {
    private readonly double factor = Math.Pow(10, decimals);
    public override double DoRound(double x)
    {
        var d = x * factor;
        if (d > 0) return ((d > 0) ? Math.Ceiling(d) : Math.Floor(d)) / factor;
        return ((d > 0) ? Math.Floor(d) : Math.Ceiling(d)) / factor;
    }
}
public sealed class RoundTowardsNegative(int decimals = 0) : BaseRounding {
    private readonly double factor = Math.Pow(10, decimals);

    public override double DoRound(double x)
    {
        var d = x * factor;
        if (d < 0) return ((d > 0) ? Math.Ceiling(d) : Math.Floor(d)) / factor;
        return ((d > 0) ? Math.Floor(d) : Math.Ceiling(d)) / factor;
    }
}
public sealed class RoundUpByStep(double step = 0) : BaseRounding {
    public override double DoRound(double x)
    {
        if (step <= 0) return x;
        if (x > 0) return Math.Ceiling(x / step) * step;
        return Math.Floor(x / step) * step;
    }
}
public sealed class RoundDownByStep(double step = 0) : BaseRounding {
    public override double DoRound(double x)
    {
        if (step <= 0) return x;
        if (x > 0) return Math.Floor(x / step) * step;
        return Math.Ceiling(x / step) * step;
    }
}
public static class RoundingStrategy {
    public static double DoRound(this double x, IRoundingStrategy s) => s.DoRound(x);
    public static float DoRound(this float x, IRoundingStrategy s) => s.DoRound(x);
    public static decimal DoRound(this decimal x, IRoundingStrategy s) => s.DoRound(x);
}