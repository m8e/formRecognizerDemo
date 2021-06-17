param location string
param suffix string

var strAccountname = 'strf${suffix}'
var workspaceName ='wrk-${suffix}'
var appInsightName = 'appinsight-${suffix}'
var hostingPlanName = 'hosting-${suffix}'
var functionName = 'func-${suffix}'

resource str 'Microsoft.Storage/storageAccounts@2019-06-01' = {
  location: location
  name: strAccountname
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
}

resource workspace 'Microsoft.OperationalInsights/workspaces@2020-08-01' = {
  name: workspaceName
  location: location
}

resource appinsight 'Microsoft.Insights/components@2020-02-02-preview' = {
  location: location
  name: appInsightName
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: workspace.id
  }
}

resource hosting 'Microsoft.Web/serverfarms@2019-08-01' = {
  name: hostingPlanName
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
  name: functionName
  location: location
  kind: 'functionapp'
  properties: {
    serverFarmId: hosting.id
    siteConfig: {
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: reference(appinsight.id, '2018-05-01-preview').instrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: 'InstrumentationKey=${reference(appinsight.id, '2018-05-01-preview').instrumentationKey}'
        }
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${str.name};AccountKey=${listkeys(str.id, '2019-06-01').keys[0].value};EndpointSuffix=${environment().suffixes.storage}'
        }
        {
          name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
          value: 'DefaultEndpointsProtocol=https;AccountName=${str.name};AccountKey=${listkeys(str.id, '2019-06-01').keys[0].value};EndpointSuffix=${environment().suffixes.storage}'
        }
        {
          name: 'WEBSITE_CONTENTSHARE'
          value: '${toLower(functionName)}95c3'
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~3'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet'
        }
        {
          name: 'WEBSITE_NODE_DEFAULT_VERSION'
          value: '~12'
        }
      ]
    }
  }
}
