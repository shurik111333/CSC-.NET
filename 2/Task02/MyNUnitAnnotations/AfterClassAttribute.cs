using System;

namespace MyNUnitAnnotations
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AfterClassAttribute : Attribute
    {
    }
}
