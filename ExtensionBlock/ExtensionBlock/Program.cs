
using System.Numerics;

List<string> names = new() { "Alice", "Bob", "Charlie" };


//string first = names.First<string>();
string first = names.First();  // For Extension Method
Console.WriteLine(first); // Output: Alice
string firstGet = names.First;
Console.WriteLine(firstGet); // Output: Alice

//------------------------------------------------------------

Func<string, string> Selector = (input) => $"Hello {input}";

var output =  names.Select(Selector);
Console.WriteLine(string.Join(",", output));
//------------------------------------------------------------

var r = IEnumerable<int>.Range(1, 10);

Console.ReadKey();


public static class MyEnumerable
{
    #region Prior to C#14
    //public static TSource First<TSource>(this IEnumerable<TSource> source)
    //{
    //    foreach (TSource item in source) return item;
    //    throw new InvalidOperationException("Sequence contains no elements");
    //}

    //public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    //{
    //    foreach (TSource item in source)
    //    {
    //        yield return selector(item);
    //    }
    //}
    #endregion
    #region C#14 - Extension Block
    extension<TSource>(IEnumerable<TSource> source)
    {
        //public TSource First()
        //{
        //    foreach (TSource item in source) return item;
        //    throw new InvalidOperationException("Sequence contains no elements");
        //}

        // We can simply change it to extension Property
        public TSource First
        {
            get { 
                foreach (TSource item in source) return item;
                throw new InvalidOperationException("Sequence contains no elements");
            }
        }

        public IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
        {
            foreach (TSource item in source)
            {
                yield return selector(item);
            }
        }
    }

    
    //extension(IEnumerable<int>)
    //{
    //    public static IEnumerable<int> Range(int start, int count)
    //    {
    //        for (int i = 0; i < count; i++) yield return start++;
    //    }
    //}

    //If I want to dynamically specify the IEnumerable type
    extension<T>(IEnumerable<T>) where T: INumber<T>
    {
        //Here we can use Int, long, float etc.
        public static IEnumerable<T> Range(T start, int count)
        {
            for (int i = 0; i < count; i++) yield return start++;
        }
    }
    #endregion
}


