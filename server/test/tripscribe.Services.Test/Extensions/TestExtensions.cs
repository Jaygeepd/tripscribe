using System.Xml.Serialization;
using AutoFixture;

namespace tripscribe.Services.Test.Extensions;

public static class TestExtensions
{
    public static ValueExtractor<T> MockWithOne<T>(this IFixture @this, T value)
    {
        var result = new Queue<T>();
        
        result.Enqueue(value);

        return new ValueExtractor<T>(result, @this);
        
    }
}

public class ValueExtractor<T>
{
    private readonly Queue<T> _queue;
    private readonly IFixture _fixture;

    public ValueExtractor(Queue<T> queue, IFixture fixture)
    {
        _queue = queue;
        _fixture = fixture;
    }

    public T GetValue()
    {
        return _queue.Any() ? _queue.Dequeue() : _fixture.Create<T>();
    }
    
}