namespace GeezYerScreensavers
{
    using Interfaces;

    class Program
    {
        static void Main(string[] args)
        {
            CompositionRoot.Wire(new ApplicationModule());

            var runProgram = CompositionRoot.Resolve<IApp>();

            runProgram.Run();
        }
    }
}
