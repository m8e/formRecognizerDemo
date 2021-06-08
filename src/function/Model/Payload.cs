using Newtonsoft.Json;

namespace Contoso
{
    public class Payload 
    {
        [JsonProperty("fileUri")]
        public string FileUri { get; set; }
        
        [JsonProperty("modelId")]
        public string ModelId { get; set; }
    }
}