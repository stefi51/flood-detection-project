using System;
using DeviceMicroservice.DeviceCommand;
using DeviceMicroservice.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SharedModels;



namespace DeviceMicroservice.CommandReceiver
{
    public class CommandConverter : JsonCreationConverter<ICommand>
    {
        private IDataRepository _dataRepository;
        public CommandConverter(IDataRepository repository)
        {
            _dataRepository = repository;
        }
        protected override ICommand Create(Type objectType, JObject jObject)
        {
            if (FieldExists("MinusWaterLevel", jObject))
            {
                return new DeviceDecreaseWaterLevel(this._dataRepository);
            }else if (FieldExists("PlusWaterLevel",jObject))
            {
               return new DeviceIncreaseWaterLevel(this._dataRepository);
            }
            else if (FieldExists("MinusWaterFlow", jObject))
            {
                return  new DeviceDecreaseWaterFlow(this._dataRepository);
            }
            else if(FieldExists("PlusWaterFlow", jObject))
            {
                return  new DeviceIncreaseWaterFlow(this._dataRepository);
            }
            else if (FieldExists("Reset", jObject))
            {
                return new DeviceResetCommand(this._dataRepository);
            }

            return new BaseCommand();
        }
        
        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }

    
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jObject);
        
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            T target = Create(objectType, jObject);

            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }
    }
}