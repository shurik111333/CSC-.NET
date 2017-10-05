using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1;

namespace Task1Tests
{
    [TestClass()]
    public class TrieTests
    {
        private ITrie _trie;

        [TestInitialize]
        public void TestInitialize()
        {
            _trie = new Trie();
        }

        [TestMethod()]
        public void AddTest()
        {
            Assert.IsTrue(_trie.Add("abacaba"));
            Assert.IsTrue(_trie.Add("abcde"));
            Assert.IsTrue(_trie.Add("qwerty"));

            Assert.IsFalse(_trie.Add("abacaba"));
            Assert.IsFalse(_trie.Add("abcde"));
            Assert.IsFalse(_trie.Add("qwerty"));
        }

        [TestMethod()]
        public void ContainsTest()
        {
            Assert.IsTrue(_trie.Add("abacaba"));
            Assert.IsTrue(_trie.Add("abcde"));
            Assert.IsTrue(_trie.Add("qwerty"));

            Assert.IsTrue(_trie.Contains("qwerty"));
            Assert.IsTrue(_trie.Contains("abcde"));
            Assert.IsTrue(_trie.Contains("abacaba"));

            Assert.IsFalse(_trie.Contains("12345"));
            Assert.IsFalse(_trie.Contains("qwergh"));
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Assert.IsTrue(_trie.Add("abacaba"));
            Assert.IsTrue(_trie.Add("abcde"));
            Assert.IsTrue(_trie.Add("qwerty"));

            Assert.IsTrue(_trie.Contains("abcde"));
            Assert.IsTrue(_trie.Remove("abcde"));
            Assert.IsFalse(_trie.Contains("abcde"));
            Assert.IsFalse(_trie.Remove("abcde"));
            Assert.IsFalse(_trie.Remove("qqqqqqq"));
        }

        [TestMethod()]
        public void SizeTest()
        {
            Assert.AreEqual(_trie.Size(), 0);

            Assert.IsTrue(_trie.Add("abacaba"));
            Assert.AreEqual(_trie.Size(), 1);

            Assert.IsTrue(_trie.Add("abcde"));
            Assert.AreEqual(_trie.Size(), 2);

            Assert.IsTrue(_trie.Add("qwerty"));
            Assert.AreEqual(_trie.Size(), 3);

            Assert.IsTrue(_trie.Remove("abacaba"));
            Assert.AreEqual(_trie.Size(), 2);

            Assert.IsFalse(_trie.Remove("qqqqqq"));
            Assert.AreEqual(_trie.Size(), 2);

            Assert.IsTrue(_trie.Remove("abcde"));
            Assert.AreEqual(_trie.Size(), 1);

            Assert.IsTrue(_trie.Remove("qwerty"));
            Assert.AreEqual(_trie.Size(), 0);
        }

        [TestMethod()]
        public void HowManyStartsWithPrefixTest()
        {
            Assert.IsTrue(_trie.Add("abacaba"));
            Assert.IsTrue(_trie.Add("abcde"));
            Assert.IsTrue(_trie.Add("qwerty"));

            Assert.AreEqual(_trie.HowManyStartsWithPrefix("q"), 1);
            Assert.AreEqual(_trie.HowManyStartsWithPrefix("a"), 2);
            Assert.AreEqual(_trie.HowManyStartsWithPrefix("ab"), 2);
            Assert.AreEqual(_trie.HowManyStartsWithPrefix("abac"), 1);
            Assert.AreEqual(_trie.HowManyStartsWithPrefix("abde"), 0);

            Assert.IsTrue(_trie.Add("a"));
            Assert.IsTrue(_trie.Add("ab"));
            Assert.IsTrue(_trie.Add("abc"));
            Assert.IsTrue(_trie.Add("aba"));
            Assert.IsTrue(_trie.Add("abac"));

            Assert.AreEqual(_trie.HowManyStartsWithPrefix("a"), 7);
            Assert.AreEqual(_trie.HowManyStartsWithPrefix("ab"), 6);
            Assert.AreEqual(_trie.HowManyStartsWithPrefix("abc"), 2);
            Assert.AreEqual(_trie.HowManyStartsWithPrefix("aba"), 3);
        }
    }
}