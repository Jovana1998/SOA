using Grpc.Core;
using GrpcService;
using GrpcService.Model;
using MongoDB.Driver;

namespace GrpcService.Services
{
    public class TemperatureService : Temperature.TemperatureBase
    {
        private readonly ILogger<TemperatureService> _logger;
        private readonly IMongoCollection<DataModel> _data;
        public TemperatureService(ILogger<TemperatureService> logger)
        {
            _logger = logger;
            var client = new MongoClient("mongodb://soa:soa12345@cluster0-shard-00-00.xnw0z.mongodb.net:27017,cluster0-shard-00-01.xnw0z.mongodb.net:27017,cluster0-shard-00-02.xnw0z.mongodb.net:27017/myFirstDatabase?ssl=true&replicaSet=atlas-qmzfdd-shard-0&authSource=admin&retryWrites=true&w=majority");
            var database = client.GetDatabase("HomeSensorData");
            _data = database.GetCollection<DataModel>("DATA");
           
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var res =   await _data.Find(x => x.ID == "6445743c1ea9933fcc9fb205").FirstOrDefaultAsync();
            return new HelloReply
            {
                Message = "Hello " + request.Name
            };
        }

        public override async Task<GetAllDataResponse> GetAllData(GetAllDataRequest request, ServerCallContext context)
        {
            var res = await _data?.Find(x => true).ToListAsync();
            return new GetAllDataResponse
            {
              
            };
        }
    }
}