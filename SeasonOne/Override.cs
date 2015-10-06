public class Override
{
    public class BaseClass
    {
        public void DisplayName()
        {
            System.Console.WriteLine("BaseClass");
        }
    }

    public class DerivedClass : BaseClass
    {
        public virtual void DisplayName()
        {
            System.Console.WriteLine("DerivedClass");
        }
    }

    public class SubDerivedClass : DerivedClass
    {
        public override void DisplayName()
        {
            System.Console.WriteLine("SubDerivedClass");
        }
    }

    public class SuperSubDerivedClass : SubDerivedClass
    {
        public new void DisplayName()
        {
            System.Console.WriteLine("SuperSubDerivedClass");
        }
    }

    public static void Test()
    {
        SuperSubDerivedClass super = new SuperSubDerivedClass();
        SubDerivedClass sub = super;
        DerivedClass derived = super;
        BaseClass basec = super;

        super.DisplayName();
        sub.DisplayName();
        derived.DisplayName();
        basec.DisplayName();
    }
}