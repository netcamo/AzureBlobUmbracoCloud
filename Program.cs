using Azure.Storage.Blobs;

namespace AzureBlob;

class Program
{
    public static BlobContainerClient getContainer(string connectionString, string containerName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        return containerClient;

    }
    
    
        
    static async Task  Main(string[] args)
    {


        string SASUrl = "<Shared access signature URL (SAS) from Umbraco Cloud settings>";
        string containerName = "<Container Name from the Umbraco Cloud settings>";


      
        string connectionString = $"BlobEndpoint={SASUrl}";
        
        

        BlobContainerClient containerClient= getContainer(connectionString, containerName);
        
        
        string localPath = "data";
        Directory.CreateDirectory(localPath);
        string fileName = Guid.NewGuid().ToString() + ".txt";
        string localFilePath = Path.Combine(localPath, fileName);
        
        await File.WriteAllTextAsync(localFilePath, "Hello, World!"); 
        
        
        // Get a reference to a blob
        string blobName = "folder/" + Guid.NewGuid().ToString() + ".txt";
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);
        
        // Upload data from the local file, overwrite the blob if it already exists
        await blobClient.UploadAsync(localFilePath, true);
        
    }
}