using Pulumi;
using Pulumi.Azure.Core;
using Pulumi.Azure.KeyVault.Inputs;
using Pulumi.Azure.Storage;
using Pulumi.Azure;
using Pulumi.Azure.Sql;
using Pulumi.Azure.Sql.Inputs;

class MyStack : Stack
{
    public MyStack()
    {
        var config = new Pulumi.Config();
        var sqlPassword = config.Require("sqlPassword");


        // Create an Azure Resource Group
        var resourceGroup = new ResourceGroup("resourceGroup");

        var sqlServer = new SqlServer("todosqlserver", new SqlServerArgs
        {
            ResourceGroupName = resourceGroup.Name,
            Location = resourceGroup.Location,
            Version = "12.0",
            AdministratorLogin = "TodoSqlAdmin",
            AdministratorLoginPassword = sqlPassword,           
        });
        var sqlFiewwallRule = new FirewallRule("sqlFirewallRule", new FirewallRuleArgs
        {
            ResourceGroupName = resourceGroup.Name,
            ServerName = sqlServer.Name,
            StartIpAddress = "0.0.0.0",
            EndIpAddress = "255.255.255.255",
        });
        //var storageAccount = new Account("exampleAccount", new AccountArgs
        //{
        //    ResourceGroupName = resourceGroup.Name,
        //    Location = resourceGroup.Location,
        //    AccountTier = "Standard",
        //    AccountReplicationType = "LRS",
        //});
        var todoDatabase = new Database("tododb", new DatabaseArgs
        {
            ResourceGroupName = resourceGroup.Name,
            Location = resourceGroup.Location,
            ServerName = sqlServer.Name,
            RequestedServiceObjectiveName = "S0",
            //ExtendedAuditingPolicy = new DatabaseExtendedAuditingPolicyArgs
            //{
            //    StorageEndpoint = storageAccount.PrimaryBlobEndpoint,
            //    StorageAccountAccessKey = storageAccount.PrimaryAccessKey,
            //    StorageAccountAccessKeyIsSecondary = true,
            //    RetentionInDays = 6,
            //},
            //Tags =
            //{
            //    { "environment", "production" },
            //},
        });

        SqlConnectionString = Output.Format($"Server=tcp:{sqlServer.Name}.database.windows.net;initial catalog={todoDatabase.Name};user ID={sqlServer.AdministratorLogin};password={sqlPassword};Min Pool Size=0;Max Pool Size=30;Persist Security Info=true");
    }


    [Output]
    public Output<string> SqlConnectionString { get; set; }
}
