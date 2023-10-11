using MyLinqs;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestCheck()
        {
            Assert.True(true);
        }
        [Fact]
        public void TestSelect()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var expected = list.Select(x => x * 2);
            var actual = new MyLinq<int>(list).Select(x => x * 2).GetEnumerator();
            foreach (var item in expected)
            {
                actual.MoveNext();
                Assert.Equal(item, actual.Current);
            }
        }
        [Fact]
        public void TestWhere()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var expected = list.Where(x => x % 2 == 0);
            var actual = new MyLinq<int>(list).Where(x => x % 2 == 0).GetEnumerator();
            foreach (var item in expected)
            {
                actual.MoveNext();
                Assert.Equal(item, actual.Current);
            }
        }

        [Fact]
        public void TestSelectWhere()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var expected = list.Select(x => x * 2).Where(x => x % 2 == 0);
            var actual = new MyLinq<int>(list).Select(x => x * 2).Where(x => x % 2 == 0).GetEnumerator();
            foreach (var item in expected)
            {
                actual.MoveNext();
                Assert.Equal(item, actual.Current);
            }
        }
        [Fact]
        public void TestWhereSelect()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var expected = list.Where(x => x % 2 == 0).Select(x => x * 2);
            var actual = new MyLinq<int>(list).Where(x => x % 2 == 0).Select(x => x * 2).GetEnumerator();
            foreach (var item in expected)
            {
                actual.MoveNext();
                Assert.Equal(item, actual.Current);
            }
        }
        [Fact]
        public void TestTake()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var expected = list.Take(3);
            var actual = new MyLinq<int>(list).Take(3).GetEnumerator();
            foreach (var item in expected)
            {
                actual.MoveNext();
                Assert.Equal(item, actual.Current);
            }
        }
        [Fact]
        public void TestAgregate()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var expected = list.Aggregate((x, y) => x + y);
            var actual = new MyLinq<int>(list).Aggregate((x, y) => x + y);
            Assert.Equal(expected, actual);

            var list1 = new List<int> { 1, 2, 3, 4, 5 };
            var expected1 = list1.Aggregate(0, (x, y) => x + y);
            var actual1 = new MyLinq<int>(list1).Aggregate(0, (x, y) => x + y);
            Assert.Equal(expected1, actual1);

            var evenEvensList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var expected2 = evenEvensList.Aggregate(true, (x, y) => x ^ y % 2 == 0);
            var actual2 = new MyLinq<int>(evenEvensList).Aggregate(true, (x, y) => x ^ y % 2 == 0);
            Assert.Equal(expected2, actual2);
        }
        [Fact]
        public void TestCount()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var expected = list.Count();
            var actual = new MyLinq<int>(list).Count();
            Assert.Equal(expected, actual);

            var list1 = new List<int> { 1, 2, 3, 4, 5 };
            var expected1 = list1.Count(x => x % 2 == 0);
            var actual1 = new MyLinq<int>(list1).Count(x => x % 2 == 0);
            Assert.Equal(expected1, actual1);
        }
        [Fact] public void TestFirstOrDefault()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var expected = list.FirstOrDefault();
            var actual = new MyLinq<int>(list).FirstOrDefault();
            Assert.Equal(expected, actual);
            var list1 = new List<int> { };
            var expected1 = list1.FirstOrDefault();
            var actual1 = new MyLinq<int>(list1).FirstOrDefault();
            Assert.Equal(expected1, actual1);
        }

        [Fact]
        public void TestDistinct()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 99, 999};
            var expected = list.Distinct();
            var actual = new MyLinq<int>(list).Distinct().GetEnumerator();
            foreach (var item in expected)
            {
                actual.MoveNext();
                Assert.Equal(item, actual.Current);
            }
        }

        [Fact]
        public void TestMinMax()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, -11};
            var expected = list.Min();
            var actual = new MyLinq<int>(list).Min();
            Assert.Equal(expected, actual);
            expected = list.Max();
            actual = new MyLinq<int>(list).Max();
            Assert.Equal(expected, actual);
        }
    }
}