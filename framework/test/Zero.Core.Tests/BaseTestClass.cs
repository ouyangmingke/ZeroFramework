namespace Zero.Core.Tests
{
    internal class BaseTestClass
    {
        public virtual string CurrentType()
        {
            return "Current Type :  " + GetType();
        }
    }
    internal class ClassA : BaseTestClass
    {
    }
    internal class ClassB : BaseTestClass
    {
    }
    internal class ClassC : BaseTestClass
    {
    }
}
