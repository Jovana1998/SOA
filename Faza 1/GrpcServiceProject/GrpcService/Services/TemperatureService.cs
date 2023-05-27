using Grpc.Core;
using GrpcService;
using GrpcService.Model;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;

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

        public override async Task<GetAllDataResponse> GetAllData(Empty request,ServerCallContext context)
        {
            var res = await _data.Find(x => true).ToListAsync();
            var data = res.Select(x => new Data
            {
                TemperatureC = x.Temperature_c,
                TimeId = x.Time_id,
                HumidityP = x.Humidity_p,
                ID = x.ID,
                PingMs = x.Ping_ms
            }).ToList();
            var response = new GetAllDataResponse();
        
            response.Data.AddRange(data); 
            return response;
        }
        public override async Task<GetByIdResponse> GetById(GetByIdRequest request, ServerCallContext context)
        {
            var res = await _data.Find(x => true && x.ID == request.Id).FirstOrDefaultAsync();

            var response = new GetByIdResponse();
            response.Result = res != null ? "Successful" : "There is no data with this id";
            if(response.Result == "Successful")
            {
                response.Data = new Data
                {
                    TemperatureC = res.Temperature_c,
                    TimeId = res.Time_id,
                    HumidityP = res.Humidity_p,
                    ID = res.ID,
                    PingMs = res.Ping_ms
                };
            }
            return response;
        }
        public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
        {
            var data = new DataModel
            {
                Temperature_c = request.TemperatureC,
                Humidity_p = request.HumidityP,
                Ping_ms = request.PingMs,
                Time_id = request.TimeId
            };
            await _data.InsertOneAsync(data);
            var result = new CreateResponse();
            if (data.ID == null)
            {
                result.Result = "Error while inserting this data";
                return result;
            }
            result.Data= new Data
            {
                TemperatureC = data.Temperature_c,
                TimeId = data.Time_id,
                HumidityP = data.Humidity_p,
                ID = data.ID,
                PingMs = data.Ping_ms
            };
            result.Result = "Successful";
            return result;
        }
        public override async Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
        {
            var res = await _data.Find(x => true && x.ID == request.ID).FirstOrDefaultAsync();
            var result = new UpdateResponse();
            if (res == null)
            {
                result.Result = "There is no data with this Id";
                return result;
            }
            res.Temperature_c = request.TemperatureC;
            res.Humidity_p = request.HumidityP;
            res.Ping_ms = request.PingMs;
            res.Time_id = request.TimeId;
            await _data.ReplaceOneAsync(x => x.ID == request.ID, res);
            result.Data= new Data
            {
                TemperatureC = res.Temperature_c,
                TimeId = res.Time_id,
                HumidityP = res.Humidity_p,
                ID = res.ID,
                PingMs = res.Ping_ms
            };
            result.Result = "Successful";
            return result;
        }
        public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
        {
            var res = await _data.Find(x => true && x.ID == request.Id).ToListAsync();
            if(res == null)
            {
                return new DeleteResponse { Response = "There is no data with this Id" };
            }
           var r= _data.DeleteOneAsync(x => x.ID == request.Id);
            return new DeleteResponse { Response = "Successful"};
        }
    }
}