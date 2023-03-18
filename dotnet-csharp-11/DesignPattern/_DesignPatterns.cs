
public static class DesignPatterns
{
    public static void Run()
    {
        var singleton = Singleton.Instance;
        singleton.DoSomething();

        var factory = new Factory();
        var product1 = factory.CreateProduct("Product1");
        var product2 = factory.CreateProduct("Product2");

        var strategyContext = new StrategyContext(new StrategyA());
        strategyContext.DoSomething();
        strategyContext.SetStrategy(new StrategyB());
        strategyContext.DoSomething();

        var subject = new Subject();
        var observer1 = new Observer1();
        var observer2 = new Observer2();
        subject.Attach(observer1);
        subject.Attach(observer2);
        subject.Notify();

        var facade = new Facade();
        facade.Operation();
    }
}
