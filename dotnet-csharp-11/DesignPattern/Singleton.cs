// See https://aka.ms/new-console-template for more information
public sealed class Singleton
{
    private static Singleton instance = null;
    private static readonly object padlock = new object();

    private Singleton()
    {
    }

    public static Singleton Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }

    public void DoSomething()
    {
        Console.WriteLine("Doing something SINGLETON...\n");
    }
}
