using Newtonsoft.Json;

namespace Contoso
{
    public class Payload 
    {

        [JsonProperty("documentId")]
        public int DocumentId { get; set; }

        [JsonProperty("fileUri")]
        public string FileUri { get; set; }
        
        [JsonProperty("modelId")]
        public string ModelId { get; set; }

        [JsonProperty("webhook")]
        public string WebHook { get; set; }
    }
}