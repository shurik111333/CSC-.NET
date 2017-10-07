using System;

namespace MyNUnitAnnotations
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AfterAttribute : Attribute
    {
    }
}
