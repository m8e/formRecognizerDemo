@description('Location for all resources except Application Insights.')
param location string = resourceGroup().location

var runtime = 'dotnet'
var appName = 'fnapp${uniqueString(resourceGroup().id)}'
var storageAccountType = 'Standard_LRS'
var functionAppName_var = appName
var hostingPlanName_var = appName
var applicationInsightsName_var = appName
var storageAccountName_var = '${uniqueString(resourceGroup().id)}azfunctions'
var functionWorkerRuntime = runtime
var appInsightsResourceId = applicationInsightsName.id

resource storageAccountName 'Microsoft.Storage/storageAccounts@2019-06-01' = {
  location: location
  name: storageAccountName_var
  sku: {
    name: storageAccountType
  }
  kind: 'Storage'
}

resource applicationInsightsName 'Microsoft.Insights/components@2018-05-01-preview' = {
  location: location
  name: applicationInsightsName_var
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}

resource hostingPlanName 'Microsoft.Web/serverfarms@2019-08-01' = {
  name: hostingPlanName_var
  location: location
  sku: {
    name: 'EP1'
    tier: 'ElasticPremium'
  }
  kind: 'elastic'
  properties: {
    maximumElasticWorkerCount: 20
  }
}

resource functionAppName 'Microsoft.Web/sites@2019-08-01' = {
  name: functionAppName_var
  location: location
  kind: 'functionapp'
  properties: {
    serverFarmId: hostingPlanName.id
    siteConfig: {
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: reference(appInsightsResourceId, '2018-05-01-preview').instrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: 'InstrumentationKey=${reference(appInsightsResourceId, '2018-05-01-preview').instrumentationKey}'
        }
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccountName_var};EndpointSuffix=${environment().suffixes.storage};AccountKey=${listkeys(storageAccountName.id, '2019-06-01').keys[0].value};'
        }
        {
          name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccountName_var};EndpointSuffix=${environment().suffixes.storage};AccountKey=${listkeys(storageAccountName.id, '2019-06-01').keys[0].value};'
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~3'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: functionWorkerRuntime
        }
        {
          name: 'WEBSITE_NODE_DEFAULT_VERSION'
          value: '~12'
        }
      ]
    }
  }
}
