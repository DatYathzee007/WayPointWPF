using System;

namespace QKNWZ1.WpfApp
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PropToStringAttribute : Attribute
    {
        public PropToStringAttribute()
        {
        }
    }
}