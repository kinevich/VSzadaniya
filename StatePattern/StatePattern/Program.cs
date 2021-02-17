namespace StatePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var phone = new Phone(new NormalPhoneState());

            phone.Call();
        }
    }
}

