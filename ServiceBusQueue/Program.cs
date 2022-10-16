using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

string conn = "";
string sbName = "test";

string[] Importance = new string[] { "High", "Medium", "Low" };

List<Order> orders = new List<Order>()
{
    new Order () { OrderID = 1, Quantity = 100, UnitPrice = 10 },
    new Order () { OrderID = 2, Quantity = 200, UnitPrice = 20 },
    new Order () { OrderID = 3, Quantity = 300, UnitPrice = 30 }
};

//await SendMessages(orders);
await ReceiveMessages();

async Task SendMessages(List<Order> orders)
{
    ServiceBusClient client = new ServiceBusClient(conn);
    ServiceBusSender serviceBusSender = client.CreateSender(sbName);
    ServiceBusMessageBatch serviceBusMessageBatch = await serviceBusSender.CreateMessageBatchAsync();
    int i = 0;
    foreach(Order order in orders)
    {
        ServiceBusMessage serviceBusMessage = new ServiceBusMessage(JsonConvert.SerializeObject(order));
        serviceBusMessage.ContentType = "application/json";
        serviceBusMessage.ApplicationProperties.Add("Importance", Importance[i]);
        
        if (!serviceBusMessageBatch.TryAddMessage(serviceBusMessage))
        {
            throw new Exception("Error occured");
        }
        i++;
        Console.WriteLine("Message sent {0}", order.OrderID);
    }

    await serviceBusSender.SendMessagesAsync(serviceBusMessageBatch);
    await client.DisposeAsync();
    await serviceBusSender.DisposeAsync();
}

async Task ReceiveMessages()
{
    try
    {
        ServiceBusClient client = new ServiceBusClient(conn);
        ServiceBusReceiver serviceBusReceiver = client.CreateReceiver(sbName, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });

        IAsyncEnumerable<ServiceBusReceivedMessage> messages = serviceBusReceiver.ReceiveMessagesAsync();

        await foreach (ServiceBusReceivedMessage message in messages)
        {

            Order? order = JsonConvert.DeserializeObject<Order>(message.Body.ToString());
            Console.WriteLine("Message ID {0}", message.MessageId);
            Console.WriteLine("Sequence Number {0}", message.SequenceNumber);
            Console.WriteLine("Order ID {0}", order?.OrderID);
            Console.WriteLine("Quantity {0}", order?.Quantity);
            Console.WriteLine("Unit Price {0}", order?.UnitPrice);
            Console.WriteLine("Message Importance {0}", message.ApplicationProperties["Importance"]);


        }

        await client.DisposeAsync();
        await serviceBusReceiver.DisposeAsync();
    }
    catch (Exception)
    {
        throw;
    }
    
}