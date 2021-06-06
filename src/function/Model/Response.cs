using Azure.AI.FormRecognizer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso
{
    public class Response
    {
        public string WebHookUri { get; set; }

        public string Forms { get; set; }
    }
}
