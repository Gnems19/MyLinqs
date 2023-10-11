using System.Collections;
namespace MyLinqs
{
    public class MyLinq<T> : IEnumerable<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public MyLinq(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            _enumerator = collection.GetEnumerator();
        }


        public MyLinq(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
        }

        public MyLinq<T> Where(Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            return new MyLinq<T>(WhereEnumerable(predicate));
        }

        // creates a specific enumerable without creating an actual enumerable class and then ovveriding its enumerator by hand 
        // just giving the iteration logic with a method
        private IEnumerable<T> WhereEnumerable(Func<T, bool> predicate)
        {
            while (_enumerator.MoveNext())
            {
                if (predicate(_enumerator.Current))
                {
                    yield return _enumerator.Current;
                }
            }
        }

        public MyLinq<R> Select<R>(Func<T, R> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            return new MyLinq<R>(SelectEnumerable(selector));
        }

        private IEnumerable<R> SelectEnumerable<R>(Func<T, R> selector)
        {
            while (_enumerator.MoveNext())
            {
                yield return selector(_enumerator.Current);
            }
        }

        public MyLinq<T> Take(int count)
        {
            return new MyLinq<T>(TakeEnumerable(count));
        }

        private IEnumerable<T> TakeEnumerable(int count)
        {
            while(count > 0 && _enumerator.MoveNext())
            {
                count--;

                yield return _enumerator.Current;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}