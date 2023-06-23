namespace Delegates.Facts
{
    public class UnitTest1
    {
        [Fact]
        public void OrderedEnumerable_ReturnsOrderedCollection_ints()
        {
            var list = new List<int>() { 5, 2, 3, 6, 1 };
            Assert.Equal(new List<int>() { 1, 2, 3, 5, 6 }, list.MyOrderBy(x => x, Comparer<int>.Default));
        }

        [Fact]
        public void OrderedEnumerable_ReturnsOrderedCollection_chars()
        {
            var list = new List<int>() { 'a', 'g', 'e', 't', 'b' };
            Assert.Equal(new List<int>() { 'a', 'b', 'e', 'g', 't' }, list.MyOrderBy(x => x, Comparer<int>.Default));
        }

        [Fact]
        public void OrderedEnumerable_ReturnsOrderedCollection_strings()
        {

            string[] fruits = new string[] { "grape", "passionfruit", "banana", "mango",
                      "orange", "raspberry", "apple", "blueberry" };
            Assert.Equal(new string[] { "apple", "grape", "mango", "banana", "orange", "blueberry",
                "raspberry", "passionfruit" }, 
            fruits.MyOrderBy(y => y.Length, Comparer<int>.Default).MyThenBy(x => x, Comparer<string>.Default));
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string City { get; set; }
        }

        [Fact]
        public void MultipleOrdering()
        {
            List<Person> people = new List<Person>
            {
              new Person { Name = "John", Age = 30, City = "New York" },
              new Person { Name = "Alex", Age = 30, City = "New York" },
               new Person { Name = "Alice", Age = 25, City = "Los Angeles" },
              new Person { Name = "Bob", Age = 35, City = "Chicago" },
              new Person { Name = "Emily", Age = 25, City = "New York" },
             new Person { Name = "David", Age = 30, City = "Los Angeles" }
            };

            var sorted = people.MyOrderBy(P => P.Age, Comparer<int>.Default)
                .MyThenBy(p => p.City, Comparer<string>.Default)
                .MyThenBy(per => per.Name, Comparer<string>.Default).Select(p => p.Name)
                .ToList(); ;
            var sortedPeople = new List<string>{"Alice", "Emily", "David", "Alex", "John", "Bob" };
            Assert.Equal(sorted, sortedPeople);
        }
    }
}