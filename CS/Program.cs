using System.Collections;

namespace CS;

public static class Program {

    public static int Main() {
        Widget<int> w = new();
        _ = w.ToList();
        return 0;
    }

}

public class Widget<T> : IWidget<T> {

    public int Count => 0;

    public IEnumerator<T> GetEnumerator() => Enumerable.Empty<T>().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}

public interface IWidget<T> : IReadOnlyCollection<T> { }
