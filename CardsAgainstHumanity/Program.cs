namespace CardsAgainstHumanity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUI UI = new UI();
            Manager manager = new Manager(UI);

            manager.Run();
        }
    }
}
