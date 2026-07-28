namespace Aids.GoF.Crea;

public class Singleton {
        
    private static readonly Singleton instance = new ();
    private Singleton(){ }
    public static Singleton New() => instance;
}