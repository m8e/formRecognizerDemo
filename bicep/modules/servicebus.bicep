param suffix string
param location string

var serviceBusNamespaceName = 'srv-${suffix}'
var queueProcessing = 'processForm'
var processed = 'processedForm'

resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2017-04-01' = {
  name: serviceBusNamespaceName
  location: location
  sku: {
    name: 'Standard'
  }
  properties: {}
}

resource serviceBusQueueToProcess 'Microsoft.ServiceBus/namespaces/queues@2017-04-01' = {
  name: '${serviceBusNamespace.name}/${queueProcessing}'
  properties: {
  }
}

resource serviceBusQueueProcessed 'Microsoft.ServiceBus/namespaces/queues@2017-04-01' = {
  name: '${serviceBusNamespace.name}/${processed}'
  properties: {
  }
}
