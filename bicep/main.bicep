param location string = 'eastus'

var suffix = uniqueString(resourceGroup().id)

module bus './modules/servicebus.bicep' = {
  name: 'bus'
  params: {
    location: location
    suffix: suffix
  }
}
