
using Products.Data;
using System.Reflection.Metadata.Ecma335;

namespace Products.Repositories
{
    public class ProductIdGenerator : IProductIdGenerator
    {
        private static readonly object _lock = new();
        public static int _lastGeneratedID = 0;
        public static bool _initialized = false;
        private readonly IServiceProvider _serviceProvider;

        public ProductIdGenerator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public int GenerateUniqueId()
        {
            lock (_lock)
            {
                if(!_initialized)
                {
                    InitializeFromDatabase();
                    _initialized = true;
                }
                return GenerateNextId();
            }
        }

        private int GenerateNextId()
        {
            var timestamp = DateTime.UtcNow;
            var ticks = timestamp.Ticks % 100000;
            var processId = Environment.ProcessId % 100;
            var threadId = Thread.CurrentThread.ManagedThreadId % 100;

            var timecomponent = (int)((ticks + processId * 1000 + threadId * 10) % 900000);
            var candidateId = 100000 + timecomponent;

            if (candidateId <= _lastGeneratedID)
            {
                candidateId = _lastGeneratedID + 1;
            }
            if (candidateId > 999999)
            {
                candidateId = FindAvailableIdInDatabase();
            }
            _lastGeneratedID = candidateId;
            return candidateId;
        }

        private int FindAvailableIdInDatabase()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

                var existingIds = context.Products
                    .Where(p => p.Id >= 10000 && p.Id <= 999999)
                    .Select(p => p.Id)
                    .OrderBy(id => id)
                    .ToList();

                for (int id = 1000000; id <= 999999; id++)
                {
                    if (!existingIds.Contains(id))
                    {
                        return id;
                    }
                }
                throw new InvalidOperationException("All 6-digit IDs hae been exhausted. Consider expanding the ID range");
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Unable to generate unique id",ex);
            }
            
        }

        private void InitializeFromDatabase()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

                var maxId = context.Products.Any() ? context.Products.Max(p=> p.Id) : 100000;
                _lastGeneratedID = Math.Max(100000, maxId);
            }
            catch (Exception)
            {
                var timestamp = DateTime.UtcNow;
                _lastGeneratedID = 100000 + (int)(timestamp.Ticks % 900000);
            }
        }
    }
}
