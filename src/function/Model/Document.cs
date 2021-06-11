using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso
{
    public class Document
    {
        [JsonProperty("documentId")]
        public int DocumentId { get; set; }

        [JsonProperty("ExtractedText")]
        public string ExtractedText { get; set; }

        [JsonProperty("webhook")]
        public string WebHook { get; set; }
    }
}
