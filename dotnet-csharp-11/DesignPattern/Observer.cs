// See https://aka.ms/new-console-template for more information
public abstract class Observer
{
    public abstract void Update();
}

public class Observer1 : Observer
{
    public override void Update()
    {
        Console.WriteLine("Observer1: Update()");
    }
}

public class Observer2 : Observer
{
    public override void Update()
    {
        Console.WriteLine("Observer2: Update()");
    }
}

public class Subject
{
    private readonly List<Observer> observers = new List<Observer>();

    public void Attach(Observer observer)
    {
        observers.Add(observer);
    }

    public void Detach(Observer observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in observers)
        {
            observer.Update();
        }
    }
}