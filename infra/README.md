# KidsToyHive Infrastructure

This directory contains Azure Bicep templates for deploying the KidsToyHive application infrastructure to Azure.

## Structure

- `main.bicep` - Main orchestration file that deploys all resources
- `main.parameters.json` - Parameter file for deployment configuration
- `modules/` - Reusable Bicep modules for individual resource types

## Modules

- `appService.bicep` - Azure App Service for hosting the .NET API
- `appServicePlan.bicep` - App Service Plan (compute resources)
- `sqlDatabase.bicep` - Azure SQL Database and Server
- `applicationInsights.bicep` - Application Insights for monitoring
- `logAnalytics.bicep` - Log Analytics Workspace for centralized logging

## Prerequisites

1. Azure CLI installed
2. Azure subscription with appropriate permissions
3. Resource group created in Azure

## Deployment

### Create a Resource Group

```bash
az group create --name rg-kidstoyhive-dev --location eastus
```

### Deploy Infrastructure

#### Option 1: Using Azure CLI with inline parameters

```bash
az deployment group create \
  --resource-group rg-kidstoyhive-dev \
  --template-file main.bicep \
  --parameters \
    baseName=kidstoyhive \
    environmentName=dev \
    sqlAdministratorLogin=sqladmin \
    sqlAdministratorPassword='<YourSecurePassword>' \
    appServicePlanSku=B1 \
    databaseSku=Basic
```

#### Option 2: Using parameter file

First, update `main.parameters.json` with your values, then:

```bash
az deployment group create \
  --resource-group rg-kidstoyhive-dev \
  --template-file main.bicep \
  --parameters main.parameters.json
```

### Validate Template

Before deploying, you can validate the template:

```bash
az deployment group validate \
  --resource-group rg-kidstoyhive-dev \
  --template-file main.bicep \
  --parameters main.parameters.json
```

### What-If Analysis

Preview changes before deployment:

```bash
az deployment group what-if \
  --resource-group rg-kidstoyhive-dev \
  --template-file main.bicep \
  --parameters main.parameters.json
```

## Resources Created

The deployment creates the following Azure resources:

1. **Log Analytics Workspace** - Centralized logging and monitoring
2. **Application Insights** - Application performance monitoring
3. **SQL Server** - Database server
4. **SQL Database** - Application database
5. **App Service Plan** - Compute resources for the API
6. **App Service** - Web application hosting the .NET API

## Configuration

### Environment Variables

The App Service is configured with the following environment variables:

- `ASPNETCORE_ENVIRONMENT` - Set based on environmentName parameter
- `APPLICATIONINSIGHTS_CONNECTION_STRING` - Application Insights connection
- `DefaultConnection` - SQL Database connection string

### SKU Options

#### App Service Plan SKUs
- `B1` - Basic (recommended for dev)
- `S1` - Standard (recommended for staging)
- `P1V2` - Premium (recommended for production)

#### Database SKUs
- `Basic` - Basic tier (recommended for dev)
- `S0` - Standard tier (recommended for staging/production)
- `P1` - Premium tier (high-performance workloads)

## Post-Deployment

After deployment, you'll receive outputs with:
- API URL
- SQL Server FQDN
- Database name
- App Service name
- Application Insights name

### Deploy the API Application

```bash
# Build and publish the API
cd src/KidsToyHive.Api
dotnet publish -c Release -o ./publish

# Deploy to Azure App Service
az webapp deployment source config-zip \
  --resource-group rg-kidstoyhive-dev \
  --name <app-service-name> \
  --src ./publish.zip
```

### Configure Database Connection

The connection string is automatically configured in the App Service. To run migrations:

```bash
# Update connection string in appsettings.json or user secrets
dotnet ef database update --project src/KidsToyHive.Api
```

## Security Best Practices

1. **Use Key Vault** - Store SQL admin password in Azure Key Vault
2. **Managed Identity** - Enable managed identity for App Service to access other Azure resources
3. **Network Security** - Configure VNet integration and private endpoints for production
4. **SQL Firewall** - Restrict SQL Server firewall rules to specific IPs in production

## Clean Up

To delete all resources:

```bash
az group delete --name rg-kidstoyhive-dev --yes --no-wait
```

## Troubleshooting

### View deployment logs

```bash
az deployment group show \
  --resource-group rg-kidstoyhive-dev \
  --name <deployment-name>
```

### Check App Service logs

```bash
az webapp log tail \
  --resource-group rg-kidstoyhive-dev \
  --name <app-service-name>
```

## Cost Estimation

For development environment with Basic SKUs:
- App Service Plan (B1): ~$13/month
- SQL Database (Basic): ~$5/month
- Application Insights: Pay-as-you-go
- Log Analytics: Pay-as-you-go

Total estimated cost: ~$20-30/month for dev environment
