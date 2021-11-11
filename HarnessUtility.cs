//-----------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------

using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

/// <summary>
/// Suite of utility methods needed for the test harness.
/// </summary>
public class HarnessUtility
{
    /// <summary>
    /// Method used for obtaining the EventHub connection string from key vault.
    /// </summary>
    /// <param name="connectionName">The name of the connection.</param>
    public static async Task<string> GetEventHubConnectionStringAsync(string connectionName)
    {
        var tokenProvider = new AzureServiceTokenProvider();
        var client = new KeyVaultClient(authenticationCallback: new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));
        var secret = await client.GetSecretAsync(
            vaultBaseUrl: System.Environment.GetEnvironmentVariable("HARNESS_KEY_VAULT_URL"),
            secretName: connectionName).ConfigureAwait(continueOnCapturedContext: false);

        return secret.Value;
    }
}