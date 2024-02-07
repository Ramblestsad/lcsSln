namespace Scratch.Utils;
public class CustomSingleton
{
    private static readonly Lazy<CustomSingleton> s_instance =
        new(() => new CustomSingleton(), true);

    protected static object _locker = new();

    public static CustomSingleton Instance => s_instance.Value;

    private CustomSingleton()
    {
    }

    public Task ThreadSafeTask()
    {
        lock (_locker)
        {
            // do something thread safe

            return Task.CompletedTask;
        }
    }
}
