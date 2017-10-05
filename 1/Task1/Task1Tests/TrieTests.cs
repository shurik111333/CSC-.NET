using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1;

namespace Task1Tests
{
    [TestClass]
    public class TrieTests
    {
        private ITrie _trie;

        private readonly List<string> _dataFromTrie = new List<string>() { "abacaba", "abcde", "qwerty", "a", "ab", "abc", "aba", "abac" };
        private readonly Dictionary<String, int> _testingPrefixes = new Dictionary<string, int>()
        {
            { "a", 7 },
            { "ab", 6 },
            { "abc", 2 },
            { "aba", 3 }
        };
        private readonly List<string> _dataNotFromTrie = new List<string>() { "12345", "qwergh", "qqqqqqq" };

        private void FillTrie(ITrie trie)
        {
            foreach (string s in _dataFromTrie)
            {
                trie.Add(s);
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _trie = new Trie();
        }

        [TestMethod]
        public void AddTest()
        {
            foreach (string s in _dataFromTrie)
            {
                Assert.IsTrue(_trie.Add(s));
            }

            foreach (string s in _dataFromTrie)
            {
                Assert.IsFalse(_trie.Add(s));
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddNullTest()
        {
            _trie.Add(null);
        }

        [TestMethod]
        public void ContainsTest()
        {
            FillTrie(_trie);

            foreach (string s in _dataFromTrie)
            {
                Assert.IsTrue(_trie.Contains(s));
            }

            foreach (string s in _dataNotFromTrie)
            {
                Assert.IsFalse(_trie.Contains(s));
            }

            foreach (string s in _dataFromTrie)
            {
                _trie.Remove(s);
                Assert.IsFalse(_trie.Contains(s));
            }
        }

        [TestMethod]
        public void ContainsNullTest()
        {
            Assert.IsFalse(_trie.Contains(null));

            _trie.Add(string.Empty);

            Assert.IsFalse(_trie.Contains(null));
        }

        [TestMethod]
        public void RemoveTest()
        {
            FillTrie(_trie);

            foreach (string s in _dataNotFromTrie)
            {
                Assert.IsFalse(_trie.Remove(s));
            }

            foreach (string s in _dataFromTrie)
            {
                Assert.IsTrue(_trie.Remove(s));
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void RemoveNullTest()
        {
            _trie.Remove(null);
        }

        [TestMethod]
        public void SizeTest()
        {
            int expectedSize = 0;
            Assert.AreEqual(expectedSize, _trie.Size());

            foreach (string s in _dataFromTrie)
            {
                _trie.Add(s);
                expectedSize++;
                Assert.AreEqual(expectedSize, _trie.Size());
            }

            foreach (string s in _dataNotFromTrie)
            {
                _trie.Remove(s);
                Assert.AreEqual(expectedSize, _trie.Size());
            }

            foreach (string s in _dataFromTrie)
            {
                _trie.Remove(s);
                expectedSize--;
                Assert.AreEqual(expectedSize, _trie.Size());
            }
        }

        [TestMethod]
        public void HowManyStartsWithPrefixTest()
        {
            FillTrie(_trie);

            foreach (var test in _testingPrefixes)
            {
                string prefix = test.Key;
                int expectedValue = test.Value;
                Assert.AreEqual(_trie.HowManyStartsWithPrefix(prefix), expectedValue);
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void HowManyStartWithNullPrefixTest()
        {
            _trie.HowManyStartsWithPrefix(null);
        }
    }
}