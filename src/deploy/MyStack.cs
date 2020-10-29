using Pulumi;
using Pulumi.Azure.Core;
using Pulumi.Azure.Sql;
using Pulumi.Azure.ContainerService;
using Pulumi.Docker;
using Pulumi.Azure.AppService;
using Pulumi.Azure.AppService.Inputs;

class MyStack : Stack
{
    public MyStack()
    {
        var config = new Pulumi.Config();
        var sqlPassword = config.Require("sqlPassword");


        // Create an Azure Resource Group
        var resourceGroup = new ResourceGroup("resourceGroup");

        // SqlServer & database instance
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
        var todoDatabase = new Database("tododb", new DatabaseArgs
        {
            ResourceGroupName = resourceGroup.Name,
            Location = resourceGroup.Location,
            ServerName = sqlServer.Name,
            RequestedServiceObjectiveName = "S0",
        });

        SqlConnectionString = Output.Format($"Server=tcp:{sqlServer.Name}.database.windows.net;initial catalog={todoDatabase.Name};user ID={sqlServer.AdministratorLogin};password={sqlPassword};Min Pool Size=0;Max Pool Size=30;Persist Security Info=true");


        // docker registry
        var registry = new Registry($"todo-acr", new RegistryArgs()
        {
            ResourceGroupName = resourceGroup.Name,
            Sku = "Basic",
            AdminEnabled = true
        });

        // build docker image
        var dockerImage = new Pulumi.Docker.Image("blazortodoapp", new ImageArgs()
        {
            ImageName = Output.Format($"{registry.LoginServer}/blazortodoapp:latest"),
            Build = new DockerBuild()
            {
                Context = "../Superpowers.Net5.WebApi",
            },
            Registry = new ImageRegistry()
            {
                Server = registry.LoginServer,
                Username = registry.AdminUsername,
                Password = registry.AdminPassword
            }
        });


        // App Service plan
        var appServicePlan = new Plan("admin-portal-plan", new PlanArgs()
        {
            ResourceGroupName = resourceGroup.Name,
            Location = resourceGroup.Location,
            Kind = "linux",
            Reserved = true,
            Sku = new PlanSkuArgs()
            {
                Tier = "Basic",
                Size = "B1",
                Capacity = 1
            }
        });

        // blazor web app from docker image
        var app = new AppService("blazor-todo-app", new AppServiceArgs()
        {
            ResourceGroupName = resourceGroup.Name,
            AppServicePlanId = appServicePlan.Id,
            Location = resourceGroup.Location,
            AppSettings = new InputMap<string>()
            {
                { "WEBSITES_ENABLE_APP_SERVICE_STORAGE", "false" },
                { "DOCKER_REGISTRY_SERVER_URL", Output.Format($"https://{registry.LoginServer}") },
                { "DOCKER_REGISTRY_SERVER_USERNAME", registry.AdminUsername },
                { "DOCKER_REGISTRY_SERVER_PASSWORD", registry.AdminPassword },
                { "WEBSITES_PORT", "80,443" },
                { "ConnecitonStrings__TodoDb", SqlConnectionString },

            },
            SiteConfig = new AppServiceSiteConfigArgs()
            {
                AlwaysOn = true,
                LinuxFxVersion = Output.Format($"DOCKER|{dockerImage.ImageName}")
            },
            HttpsOnly = true
        });

    }


    [Output]
    public Output<string> SqlConnectionString { get; set; }
}
