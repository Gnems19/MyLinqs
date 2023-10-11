using System.Collections;
using System.Collections.Generic;

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

        public MyLinq<T> Where(Predicate<T> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            return new MyLinq<T>(WhereEnumerable(predicate));
        }

        // creates a specific enumerable without creating an actual enumerable class and then ovveriding its enumerator by hand 
        // just giving the iteration logic
        private IEnumerable<T> WhereEnumerable(Predicate<T> predicate)
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

        public MyLinq<T> Skip(int count)
        {
            return new MyLinq<T>(SkipEnumerable(count));
        }

        private IEnumerable<T> SkipEnumerable(int count)
        {
            while (count > 0 && _enumerator.MoveNext())
            {
                count--;
            }
            while (_enumerator.MoveNext())
            {
                yield return _enumerator.Current;
            }
        }

        public T? Agregate(Func<T, T, T> acumulator)
        {
            if (acumulator == null)
            {
                throw new ArgumentNullException(nameof(acumulator));
            }
            if (!_enumerator.MoveNext())
            {
                return default;
            }
            var result = _enumerator.Current;
            while (_enumerator.MoveNext())
            {
                result = acumulator(result, _enumerator.Current);
            }
            return result;
        }

        public T Agregate(T seed, Func<T, T, T> acumulator)
        {
            if (acumulator == null)
            {
                throw new ArgumentNullException(nameof(acumulator));
            }
            var result = seed;
            while (_enumerator.MoveNext())
            {
                result = acumulator(result, _enumerator.Current);
            }
            return result;
        }

        public TAccumulate Agregate<TAccumulate>(TAccumulate seed, Func<TAccumulate, T, TAccumulate> acumulator)
        {
            if(acumulator == null)
            {
                throw new ArgumentNullException(nameof(acumulator));
            }
            var result = seed;
            while (_enumerator.MoveNext())
            {
                result = acumulator(result, _enumerator.Current);
            }
            return result;
        }

        public long Count()
        {
            long count = 0;
            while (_enumerator.MoveNext())
            {
                count++;
            }
            return count;
        }

        public long Count(Predicate<T> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            long count = 0;
            while (_enumerator.MoveNext())
            {
                if(predicate(_enumerator.Current))
                {
                    count++;
                }
            }
            return count;
        }

        public T? FirstOrDefault(Predicate<T> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            while (_enumerator.MoveNext())
            {
                if(predicate(_enumerator.Current))
                {
                    return _enumerator.Current;
                }
            }
            return default;
        }

        public MyLinq<T> Distinct()
        {
            return new MyLinq<T>(DistinctEnumerable());
        }

        private IEnumerable<T> DistinctEnumerable()
        {
            HashSet<T> distinctValues = new();
            while (_enumerator.MoveNext())
            {
                if(distinctValues.Add(_enumerator.Current))
                {
                    yield return _enumerator.Current;
                }
            }
        }
        
        public T? Max(Comparer<T> comparer)
        {
            if(comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }
            return Agregate((x, y) => comparer.Compare(x, y) > 0 ? x : y);
        }
        public T? Min(Comparer<T> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }
            return Agregate((x, y) => comparer.Compare(x, y) < 0 ? x : y);
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