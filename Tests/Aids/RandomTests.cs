using Random = Aids.Random;
namespace Tests.Aids;

[TestClass] public sealed class RandomTests : StaticTests {
    protected override Type setType() => typeof(Random);
    private sealed class TestClass {
        public bool? Boolean { get; set; }
        public char? Char { get; set; }
        public string? String { get; set; }
        public DateTime? DateTime { get; set; }
        public DateOnly? DateOnly { get; set; }
        public sbyte? Int8 { get; set; }
        public short? Int16 { get; set; }
        public int? Int32 { get; set; }
        public long? Int64 { get; set; }
        public byte? Uint8 { get; set; }
        public ushort? Uint16 { get; set; }
        public uint? Uint32 { get; set; }
        public ulong? Uint64 { get; set; }
        public float? Float { get; set; }
        public decimal? Decimal { get; set; }
        public double? Double { get; set; }
    }
    [TestMethod] public void ObjectTest() {
        var o = Random.Object<TestClass>();
        IsNotNull(o);
        IsNotNull(o.Boolean);
        IsNotNull(o.Char);
        IsNotNull(o.String);
        IsNotNull(o.DateTime);
        IsNotNull(o.DateOnly);
        IsNotNull(o.Int8);
        IsNotNull(o.Int16);
        IsNotNull(o.Int32);
        IsNotNull(o.Int64);
        IsNotNull(o.Uint8);
        IsNotNull(o.Uint16);
        IsNotNull(o.Uint32);
        IsNotNull(o.Uint64);
        IsNotNull(o.Float);
        IsNotNull(o.Decimal);
        IsNotNull(o.Double);
    }
    [TestMethod] public void Int32Test() {
        var a = Random.Int32();
        var i = 0;
        do {
            i++;
            var b = Random.Int32();
            AreNotEqual(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
    [TestMethod] public void BooleanTest() {
        var a = Random.Boolean();
        bool b;
        do {
            b = Random.Boolean();
        } while (b != a);
    }
    [TestMethod] public void CharTest() {
        var a = Random.Char((char)0);
        var i = 0;
        do {
            i++;
            var b = Random.Char((char)0);
            AreNotEqual(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
    [TestMethod] public void StringTest() {
        var a = Random.String();
        var i = 0;
        do {
            i++;
            var b = Random.String();
            AreNotSame(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
    [TestMethod] public void DateTimeTest() {
        var a = Random.DateTime();
        var i = 0;
        do {
            i++;
            var b = Random.DateTime();
            AreNotEqual(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
    [TestMethod]
    public void DateOnlyTest() {
        var a = Random.DateOnly();
        var i = 0;
        do {
            i++;
            var b = Random.DateOnly();
            AreNotEqual(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
    [TestMethod] public void Int8Test() {
        var a = Random.Int8();
        sbyte b;
        do {
            b = Random.Int8();
        } while (b != a);
    }
    [TestMethod] public void Int16Test() {
        var a = Random.Int16();
        var i = 0;
        do {
            i++;
            var b = Random.Int16();
            AreNotEqual(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
    [TestMethod] public void Int64Test() {
        var a = Random.Int64();
        var i = 0;
        do {
            i++;
            var b = Random.Int64();
            AreNotEqual(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
    [TestMethod] public void Uint8Test() {
        var a = Random.Uint8();
        byte b;
        do {
            b = Random.Uint8();
        } while (b != a);
    }
    [TestMethod] public void Uint16Test() {
        var a = Random.Uint16();
        var i = 0;
        do {
            i++;
            var b = Random.Uint16();
            AreNotEqual(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
    [TestMethod] public void Uint32Test() {
        var a = Random.Uint32();
        var i = 0;
        do {
            i++;
            var b = Random.Uint32();
            AreNotEqual(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
    [TestMethod] public void Uint64Test() {
        var a = Random.Uint64();
        var i = 0;
        do {
            i++;
            var b = Random.Uint64();
            AreNotEqual(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
    [TestMethod] public void FloatTest() {
        var a = Random.Float();
        var i = 0;
        do {
            i++;
            var b = Random.Float();
            AreNotEqual(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
    [TestMethod] public void DecimalTest() {
        var a = Random.Decimal();
        var i = 0;
        do {
            i++;
            var b = Random.Decimal();
            AreNotEqual(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
    [TestMethod] public void DoubleTest() {
        var a = Random.Double();
        var i = 0;
        do {
            i++;
            var b = Random.Double();
            AreNotEqual(a, b);
        } while (i < repeatCount);
        AreEqual(i, repeatCount);
    }
}
