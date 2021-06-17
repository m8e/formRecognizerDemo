param location string = 'eastus'

var suffix = uniqueString(resourceGroup().id)

module bus './modules/servicebus.bicep' = {
  name: 'bus'
  params: {
    location: location
    suffix: suffix
  }
}

module cognitive './modules/cognitive.bicep' = {
  name: 'cognitive'
  params: {
    location: location
    suffix: suffix
  }
}

module function 'modules/function.bicep' = {
  name: 'function'
  params: {
    location: location
    suffix: suffix
  }
}
