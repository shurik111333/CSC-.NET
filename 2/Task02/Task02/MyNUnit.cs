using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MyNUnitAnnotations;

namespace Task02
{
    public class MyNUnit
    {
        private static readonly Type _methodTestAttribute = typeof(TestAttribute);
        private static readonly Type _beforeClassAttribute = typeof(BeforeClassAttribute);
        private static readonly Type _afterClassAttribute = typeof(AfterClassAttribute);
        private static readonly Type _beforeMethodAttribute = typeof(BeforeAttribute);
        private static readonly Type _afterMethodAttribute = typeof(AfterAttribute);

        private static readonly object[] _emptyParams = new object[0];

        public void RunTests(Type type, Action<string> logger)
        {
            if (type == null || logger == null)
            {
                throw new ArgumentException("type and logger must be not null");
            }
            var testMethods = GetMethodsWithAttribute(type, _methodTestAttribute).ToList();
            if (testMethods.Count == 0)
            {
                return;
            }
            logger.Invoke($"Running tests from {type.Name}");
            var testInstance = Activator.CreateInstance(type);
            var beforeMethods = GetMethodsWithAttribute(type, _beforeMethodAttribute);
            var afterMethods = GetMethodsWithAttribute(type, _afterMethodAttribute);

            InvokeMethods(GetMethodsWithAttribute(type, _beforeClassAttribute), testInstance);
            RunTests(testMethods, beforeMethods, afterMethods, testInstance, logger);
            InvokeMethods(GetMethodsWithAttribute(type, _afterClassAttribute), testInstance);
        }

        private void RunTests(
            IEnumerable<MethodInfo> tests,
            IEnumerable<MethodInfo> before,
            IEnumerable<MethodInfo> after,
            object testInstance,
            Action<string> logger)
        {
            tests.ToList().ForEach(test =>
            {
                logger.Invoke($"Test: {test.Name}");

                var td = new TestData(test, testInstance);
                if (td.IsIgnored)
                {
                    logger.Invoke($"Ignored: {td.IgnoreMessage}");
                }
                else
                {
                    InvokeMethods(before, testInstance);
                    InvokeTest(td, logger);
                    InvokeMethods(after, testInstance);
                }
                logger.Invoke("------------------------------------");
            });
        }

        private void InvokeTest(TestData td, Action<string> logger)
        {
            var sw = new Stopwatch();
            string result = "OK";
            try
            {
                sw.Start();
                td.Execute();
                sw.Stop();
                if (td.ExceptionRequired)
                {
                    result = $"Expected exception: {td.ExpectedException.Name}";
                }
            }
            catch (TargetInvocationException e)
            {
                sw.Stop();
                if (e.InnerException.GetType() != td.ExpectedException)
                {
                    result = $"Unexpected exception: {e.Message}";
                }
            }
            logger.Invoke(result);
            logger.Invoke($"Time: {sw.ElapsedMilliseconds}ms");
        }

        private void InvokeMethods(IEnumerable<MethodInfo> methods, object instance)
            => methods.ToList().ForEach(m => m.Invoke(instance, _emptyParams));

        private IEnumerable<MethodInfo> GetMethodsWithAttribute(Type t, Type attribute)
            => t.GetMethods()
                .Where(m => m.CustomAttributes.Any(attr => attr.AttributeType == attribute));

        private class TestData
        {
            public bool IsIgnored => attribute.Ignore != null;
            public bool ExceptionRequired => attribute.Expected != null;
            public string IgnoreMessage => attribute.Ignore;
            public Type ExpectedException => attribute.Expected;
            private MethodInfo _method { get; }
            private object _testInstance { get; }
            private TestAttribute attribute { get; }

            public TestData(MethodInfo m, object testInstance)
            {
                _method = m;
                _testInstance = testInstance;
                attribute = (TestAttribute) m.GetCustomAttributes(_methodTestAttribute, false)[0];
            }

            public void Execute() => _method.Invoke(_testInstance, _emptyParams);
        }
    }
}