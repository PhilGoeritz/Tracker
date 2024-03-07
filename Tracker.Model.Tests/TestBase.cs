namespace Tracker.Model.Tests;

public abstract class TestBase
{
    [OneTimeSetUp]
    public void Initialize()
    {
        EstablishContext();
    }
    
    protected virtual void EstablishContext() { }
}