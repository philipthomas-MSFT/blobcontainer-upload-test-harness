//-----------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------
// See https://aka.ms/new-console-template for more information

using System.Text;
using Azure.Storage.Blobs;
using Bogus;
using Newtonsoft.Json;

var items = new Faker<Item>()
    .RuleFor(item => item.Id, fake => Guid.NewGuid().ToString())
    .RuleFor(item => item.FirstName, fake => fake.Person.FirstName)
    .RuleFor(item => item.LastName, fake => fake.Person.LastName)
    .Generate(count: 10);

var connectionString = await HarnessUtility.GetEventHubConnectionStringAsync(connectionName: System.Environment.GetEnvironmentVariable("HARNESS_STORAGE_CONNECTIONSTRING_NAME"));
var serviceClient = new BlobServiceClient(connectionString: connectionString);
var container = serviceClient.GetBlobContainerClient(blobContainerName: System.Environment.GetEnvironmentVariable("HARNESS_STORAGE_CONTAINER_NAME"));

var tasks = from item in items
            let task = Task.Run(async () =>
            {
                var blobClient = container.GetBlobClient(blobName: item.Id);
                var itemAsString = JsonConvert.SerializeObject(value: item);
                Console.WriteLine(itemAsString);
                return await blobClient.UploadAsync(content: BinaryData.FromBytes(data: Encoding.UTF8.GetBytes(s: itemAsString)));
            })
            select task;

await Task.WhenAll(tasks).ConfigureAwait(continueOnCapturedContext: false);

Console.WriteLine("Done!");
