// <copyright file="MiniGameReward.Generated.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

//------------------------------------------------------------------------------
// <auto-generated>
//     This source code was auto-generated by a roslyn code generator.
// </auto-generated>
//------------------------------------------------------------------------------

// ReSharper disable All

namespace MUnique.OpenMU.Persistence.BasicModel;

using MUnique.OpenMU.Persistence.Json;

/// <summary>
/// A plain implementation of <see cref="MiniGameReward"/>.
/// </summary>
public partial class MiniGameReward : MUnique.OpenMU.DataModel.Configuration.MiniGameReward, IIdentifiable, IConvertibleTo<MiniGameReward>
{
    
    /// <summary>
    /// Gets or sets the identifier of this instance.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets the raw object of <see cref="ItemReward" />.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("itemReward")]
    [System.Text.Json.Serialization.JsonPropertyName("itemReward")]
    public DropItemGroup RawItemReward
    {
        get => base.ItemReward as DropItemGroup;
        set => base.ItemReward = value;
    }

    /// <inheritdoc/>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public override MUnique.OpenMU.DataModel.Configuration.DropItemGroup ItemReward
    {
        get => base.ItemReward;
        set => base.ItemReward = value;
    }

    /// <summary>
    /// Gets the raw object of <see cref="RequiredKill" />.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("requiredKill")]
    [System.Text.Json.Serialization.JsonPropertyName("requiredKill")]
    public MonsterDefinition RawRequiredKill
    {
        get => base.RequiredKill as MonsterDefinition;
        set => base.RequiredKill = value;
    }

    /// <inheritdoc/>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public override MUnique.OpenMU.DataModel.Configuration.MonsterDefinition RequiredKill
    {
        get => base.RequiredKill;
        set => base.RequiredKill = value;
    }


    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        var baseObject = obj as IIdentifiable;
        if (baseObject != null)
        {
            return baseObject.Id == this.Id;
        }

        return base.Equals(obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    /// <inheritdoc/>
    public MiniGameReward Convert() => this;
}
