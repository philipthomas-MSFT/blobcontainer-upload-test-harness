//-----------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------

using Newtonsoft.Json;

/// <summary>
/// The item used to test upsert items in bulk execution.
/// </summary>
public class Item
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string? Id { set; get; }

    /// <summary>
    /// Gets or sets the FirstName.
    /// </summary>
    public string? FirstName { set; get; }


    /// <summary>
    /// Gets or sets the LastName.
    /// </summary>
    public string? LastName { get; set; }
}
