# TCAdmin-Wrapper
A Wrapper for the TCAdmin SDK. Easily create a TCAdmin instance without using a configuration file.

**Notes**
 - Host the custom application as close as possible to the master MySQL server that hosts the TCAdmin database. This majorly reduces latency and much faster response times between commands.
- Connecting to a MySQL slave is supported but functionality is reduced due to read only access.
  - Specifically saving custom variables or properties on a TCAdmin object will not work as data is not synced between the two databases.
- You only need to initialize TCAdminClient once in the program, after this, you can then access all of TCAdmins SDK which will be configured already to use.

## Dependencies
Please ensure that you have these libraries installed either inside the program you are creating or installed on the OS itself.
 - [MySQL Connector (6.6.4)](https://cdn.mysql.com/archives/mysql-connector-net-6.6/mysql-connector-net-6.6.4.msi)
 - TCAdmin is installed locally and added to the System **PATH** variable. (Not required if using NuGet)

## Getting started
1. Install MySQL.Data 6.6.4 (optional)
2. Add `https://nexus-repository.openshift.alexr03.dev/repository/tcadmin/` as a source to your NuGet.
3. Install TCAdminWrapper into your project via NuGet: `nuget Install TCAdminWrapper`
4. Open `TCAdmin.Monitor.Config` on your master server (*Located in TCAdmin/Monitor*) find `TCAdmin.Database.MySql.ConnectionString` and copy its value. ***DO NOT SHARE THIS VALUE ANYWHERE IT WILL ALLOW ACCESS TO YOUR TCADMIN DATABASE***
5. Follow the examples below

##  Examples
### Basic Example:
This example will show you how to initialize the TCAdminClient class. **You only need to do this once in the program**. And then will get the Server in TCAdmin with ID 1, and print the name to the console

```csharp
        public static void Main(string[] args)
        {
            string sqlString =
                "SQL_STRING_HERE";
            TCAdminClientConfiguration configuration = new TCAdminClientConfiguration(sqlString, 
                true, 
                "TCAdminWrapperTest", new TCAdminSettings());
            TcAdminClient client = new TcAdminClient(configuration);
            
            Console.WriteLine("Name: " + new TCAdmin.SDK.Objects.Server(1));
        }
```

### Debug Configuration
If you'd like to take advantage of using TCAdmin's debug messages like seeing what SQL commands are being executed for debugging, you can do this:

When running this you will see more messages in the console until it finally prints out the server name.

```csharp
        public static void Main(string[] args)
        {
            string sqlString =
                "SQL_STRING_HERE";
            
            TCAdminSettings settings = new TCAdminSettings(debug: true, debugSql: true);
            
            TCAdminClientConfiguration configuration = new TCAdminClientConfiguration(sqlString, 
                true, 
                "TCAdminWrapperTest", settings);
            TcAdminClient client = new TcAdminClient(configuration);
            
            Console.WriteLine("Name: " + new TCAdmin.SDK.Objects.Server(1));
        }
```

### Restarting a service
You can easily restart a service by using the TCAdmin SDK. Grab the service object by creating a new instance using the serviceId constructor and proceed to call the `Restart()` method.

```csharp
        public static void Main(string[] args)
        {
            string sqlString = 
                "SQL_STRING_HERE";

            TCAdminClientConfiguration configuration = new TCAdminClientConfiguration(sqlString,
                true,
                "TCAdminWrapperTest", settings);
            TcAdminClient client = new TcAdminClient(configuration);

            Service service = new Service(1);
            Console.WriteLine("Service: " + service.NameNoHtml);
            Console.WriteLine("Restarting service.");
            service.Restart();
            Console.WriteLine("Restarted service.");
        }
```

Thats all for now! :P