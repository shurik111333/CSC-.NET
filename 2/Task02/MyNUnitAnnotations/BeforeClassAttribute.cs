using System;

namespace MyNUnitAnnotations
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BeforeClassAttribute : Attribute
    {
    }
}
