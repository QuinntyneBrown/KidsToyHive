targetScope = 'resourceGroup'

@description('Base name for all resources')
param baseName string = 'kidstoyhive'

@description('Location for all resources')
param location string = resourceGroup().location

@description('Environment name')
@allowed([
  'dev'
  'staging'
  'prod'
])
param environmentName string = 'dev'

@description('SQL Server administrator login')
param sqlAdministratorLogin string

@description('SQL Server administrator password')
@secure()
param sqlAdministratorPassword string

@description('App Service Plan SKU')
param appServicePlanSku string = 'B1'

@description('Database SKU')
param databaseSku string = 'Basic'

// Generate unique names for resources
var uniqueSuffix = uniqueString(resourceGroup().id)
var logAnalyticsName = '${baseName}-logs-${environmentName}-${uniqueSuffix}'
var appInsightsName = '${baseName}-ai-${environmentName}-${uniqueSuffix}'
var appServicePlanName = '${baseName}-plan-${environmentName}-${uniqueSuffix}'
var appServiceName = '${baseName}-api-${environmentName}-${uniqueSuffix}'
var sqlServerName = '${baseName}-sql-${environmentName}-${uniqueSuffix}'
var databaseName = '${baseName}Db'

// Deploy Log Analytics Workspace
module logAnalytics './modules/logAnalytics.bicep' = {
  name: 'logAnalyticsDeploy'
  params: {
    logAnalyticsName: logAnalyticsName
    location: location
  }
}

// Deploy Application Insights
module applicationInsights './modules/applicationInsights.bicep' = {
  name: 'applicationInsightsDeploy'
  params: {
    applicationInsightsName: appInsightsName
    location: location
    workspaceId: logAnalytics.outputs.logAnalyticsWorkspaceId
  }
}

// Deploy SQL Database
module sqlDatabase './modules/sqlDatabase.bicep' = {
  name: 'sqlDatabaseDeploy'
  params: {
    sqlServerName: sqlServerName
    location: location
    administratorLogin: sqlAdministratorLogin
    administratorPassword: sqlAdministratorPassword
    databaseName: databaseName
    databaseSku: databaseSku
    allowAzureServices: true
  }
}

// Deploy App Service Plan
module appServicePlan './modules/appServicePlan.bicep' = {
  name: 'appServicePlanDeploy'
  params: {
    appServicePlanName: appServicePlanName
    location: location
    skuName: appServicePlanSku
  }
}

// Deploy App Service
module appService './modules/appService.bicep' = {
  name: 'appServiceDeploy'
  params: {
    appServiceName: appServiceName
    location: location
    appServicePlanId: appServicePlan.outputs.appServicePlanId
    connectionString: sqlDatabase.outputs.connectionString
    environment: environmentName == 'prod' ? 'Production' : (environmentName == 'staging' ? 'Staging' : 'Development')
    appInsightsInstrumentationKey: applicationInsights.outputs.connectionString
  }
}

// Outputs
output apiUrl string = 'https://${appService.outputs.appServiceDefaultHostName}'
output sqlServerFqdn string = sqlDatabase.outputs.sqlServerFqdn
output databaseName string = sqlDatabase.outputs.databaseName
output appServiceName string = appService.outputs.appServiceName
output applicationInsightsName string = appInsightsName
