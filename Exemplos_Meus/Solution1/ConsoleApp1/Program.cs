using Topshelf;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(
                r =>
                {
                    r.SetServiceName("Teste.Teste");
                    r.SetDisplayName("Teste - Teste");
                    r.SetDescription("Serviço de Teste");

                    r.Service<TesteService>(
                       s =>
                       {
                           s.ConstructUsing(() => new TesteService());
                           s.WhenStarted(w => w.Start());
                           s.WhenStopped(w => w.Stop());
                       })
                       .RunAsNetworkService()
                       .BeforeUninstall(() =>
                       {
                       });
                });
        }
    }
}
