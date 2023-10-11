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


    }
}