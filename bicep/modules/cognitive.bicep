param location string
param suffix string

var name = 'frm-${suffix}'

resource formRecognizer 'Microsoft.CognitiveServices/accounts@2017-04-18' = {
  name: name
  location: location
  sku: {
    name: 'S0'
  }
  kind: 'FormRecognizer'
}
