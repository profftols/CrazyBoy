namespace _ProjectBoy.Scripts.Infostructure.Services
{
    public class AllServices
    {
        private static AllServices instance;
        public static AllServices Container => instance ??= new AllServices();

        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}