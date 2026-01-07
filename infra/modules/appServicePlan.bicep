@description('Name of the App Service Plan')
param appServicePlanName string

@description('Location for all resources')
param location string = resourceGroup().location

@description('The SKU of App Service Plan')
param skuName string = 'B1'

@description('The SKU capacity')
param skuCapacity int = 1

resource appServicePlan 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: skuName
    capacity: skuCapacity
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

output appServicePlanId string = appServicePlan.id
output appServicePlanName string = appServicePlan.name
