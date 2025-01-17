﻿// <copyright file="ChatServerPacketsRef.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

//------------------------------------------------------------------------------
// <auto-generated>
//     This source code was auto-generated by an XSL transformation.
//     Do not change this file. Instead, change the XML data which contains
//     the packet definitions and re-run the transformation (rebuild this project).
// </auto-generated>
//------------------------------------------------------------------------------

namespace MUnique.OpenMU.Network.Packets.ChatServer;

using System;
using static System.Buffers.Binary.BinaryPrimitives;

/// <summary>
/// Is sent by the client when: This packet is sent by the client after it connected to the server, to authenticate itself.
/// Causes reaction on server side: The server will check the token. If it's correct, the client gets added to the requested chat room.
/// </summary>
public readonly ref struct AuthenticateRef
{
    private readonly Span<byte> _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    public AuthenticateRef(Span<byte> data)
        : this(data, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    /// <param name="initialize">If set to <c>true</c>, the header data is automatically initialized and written to the underlying span.</param>
    private AuthenticateRef(Span<byte> data, bool initialize)
    {
        this._data = data;
        if (initialize)
        {
            var header = this.Header;
            header.Type = HeaderType;
            header.Code = Code;
            header.Length = (byte)Math.Min(data.Length, Length);
        }
    }

    /// <summary>
    /// Gets the header type of this data packet.
    /// </summary>
    public static byte HeaderType => 0xC1;

    /// <summary>
    /// Gets the operation code of this data packet.
    /// </summary>
    public static byte Code => 0x00;

    /// <summary>
    /// Gets the initial length of this data packet. When the size is dynamic, this value may be bigger than actually needed.
    /// </summary>
    public static int Length => 16;

    /// <summary>
    /// Gets the header of this packet.
    /// </summary>
    public C1HeaderWithSubCodeRef Header => new (this._data);

    /// <summary>
    /// Gets or sets the room id.
    /// </summary>
    public ushort RoomId
    {
        get => ReadUInt16LittleEndian(this._data[4..]);
        set => WriteUInt16LittleEndian(this._data[4..], value);
    }

    /// <summary>
    /// Gets or sets a token (integer number), formatted as string. This value is also "encrypted" with the 3-byte XOR key (FC CF AB).
    /// </summary>
    public string Token
    {
        get => this._data.ExtractString(6, 10, System.Text.Encoding.UTF8);
        set => this._data.Slice(6, 10).WriteString(value, System.Text.Encoding.UTF8);
    }

    /// <summary>
    /// Performs an implicit conversion from a Span of bytes to a <see cref="Authenticate"/>.
    /// </summary>
    /// <param name="packet">The packet as span.</param>
    /// <returns>The packet as struct.</returns>
    public static implicit operator AuthenticateRef(Span<byte> packet) => new (packet, false);

    /// <summary>
    /// Performs an implicit conversion from <see cref="Authenticate"/> to a Span of bytes.
    /// </summary>
    /// <param name="packet">The packet as struct.</param>
    /// <returns>The packet as byte span.</returns>
    public static implicit operator Span<byte>(AuthenticateRef packet) => packet._data; 
}


/// <summary>
/// Is sent by the server when: This packet is sent by the server after another chat client joined the chat room.
/// Causes reaction on client side: The client will add the client in its list (if over 2 clients are connected to the same room), or show its name in the title bar.
/// </summary>
public readonly ref struct ChatRoomClientJoinedRef
{
    private readonly Span<byte> _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatRoomClientJoinedRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    public ChatRoomClientJoinedRef(Span<byte> data)
        : this(data, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatRoomClientJoinedRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    /// <param name="initialize">If set to <c>true</c>, the header data is automatically initialized and written to the underlying span.</param>
    private ChatRoomClientJoinedRef(Span<byte> data, bool initialize)
    {
        this._data = data;
        if (initialize)
        {
            var header = this.Header;
            header.Type = HeaderType;
            header.Code = Code;
            header.Length = (byte)Math.Min(data.Length, Length);
            header.SubCode = SubCode;
        }
    }

    /// <summary>
    /// Gets the header type of this data packet.
    /// </summary>
    public static byte HeaderType => 0xC1;

    /// <summary>
    /// Gets the operation code of this data packet.
    /// </summary>
    public static byte Code => 0x01;

    /// <summary>
    /// Gets the operation sub-code of this data packet.
    /// The <see cref="Code" /> is used as a grouping key.
    /// </summary>
    public static byte SubCode => 0x00;

    /// <summary>
    /// Gets the initial length of this data packet. When the size is dynamic, this value may be bigger than actually needed.
    /// </summary>
    public static int Length => 15;

    /// <summary>
    /// Gets the header of this packet.
    /// </summary>
    public C1HeaderWithSubCodeRef Header => new (this._data);

    /// <summary>
    /// Gets or sets the client index.
    /// </summary>
    public byte ClientIndex
    {
        get => this._data[4];
        set => this._data[4] = value;
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name
    {
        get => this._data.ExtractString(5, 10, System.Text.Encoding.UTF8);
        set => this._data.Slice(5, 10).WriteString(value, System.Text.Encoding.UTF8);
    }

    /// <summary>
    /// Performs an implicit conversion from a Span of bytes to a <see cref="ChatRoomClientJoined"/>.
    /// </summary>
    /// <param name="packet">The packet as span.</param>
    /// <returns>The packet as struct.</returns>
    public static implicit operator ChatRoomClientJoinedRef(Span<byte> packet) => new (packet, false);

    /// <summary>
    /// Performs an implicit conversion from <see cref="ChatRoomClientJoined"/> to a Span of bytes.
    /// </summary>
    /// <param name="packet">The packet as struct.</param>
    /// <returns>The packet as byte span.</returns>
    public static implicit operator Span<byte>(ChatRoomClientJoinedRef packet) => packet._data; 
}


/// <summary>
/// Is sent by the server when: This packet is sent by the server after a chat client left the chat room.
/// Causes reaction on client side: The client will remove the client from its list, or mark its name in the title bar as offline.
/// </summary>
public readonly ref struct ChatRoomClientLeftRef
{
    private readonly Span<byte> _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatRoomClientLeftRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    public ChatRoomClientLeftRef(Span<byte> data)
        : this(data, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatRoomClientLeftRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    /// <param name="initialize">If set to <c>true</c>, the header data is automatically initialized and written to the underlying span.</param>
    private ChatRoomClientLeftRef(Span<byte> data, bool initialize)
    {
        this._data = data;
        if (initialize)
        {
            var header = this.Header;
            header.Type = HeaderType;
            header.Code = Code;
            header.Length = (byte)Math.Min(data.Length, Length);
            header.SubCode = SubCode;
        }
    }

    /// <summary>
    /// Gets the header type of this data packet.
    /// </summary>
    public static byte HeaderType => 0xC1;

    /// <summary>
    /// Gets the operation code of this data packet.
    /// </summary>
    public static byte Code => 0x01;

    /// <summary>
    /// Gets the operation sub-code of this data packet.
    /// The <see cref="Code" /> is used as a grouping key.
    /// </summary>
    public static byte SubCode => 0x01;

    /// <summary>
    /// Gets the initial length of this data packet. When the size is dynamic, this value may be bigger than actually needed.
    /// </summary>
    public static int Length => 15;

    /// <summary>
    /// Gets the header of this packet.
    /// </summary>
    public C1HeaderWithSubCodeRef Header => new (this._data);

    /// <summary>
    /// Gets or sets the client index.
    /// </summary>
    public byte ClientIndex
    {
        get => this._data[4];
        set => this._data[4] = value;
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name
    {
        get => this._data.ExtractString(5, 10, System.Text.Encoding.UTF8);
        set => this._data.Slice(5, 10).WriteString(value, System.Text.Encoding.UTF8);
    }

    /// <summary>
    /// Performs an implicit conversion from a Span of bytes to a <see cref="ChatRoomClientLeft"/>.
    /// </summary>
    /// <param name="packet">The packet as span.</param>
    /// <returns>The packet as struct.</returns>
    public static implicit operator ChatRoomClientLeftRef(Span<byte> packet) => new (packet, false);

    /// <summary>
    /// Performs an implicit conversion from <see cref="ChatRoomClientLeft"/> to a Span of bytes.
    /// </summary>
    /// <param name="packet">The packet as struct.</param>
    /// <returns>The packet as byte span.</returns>
    public static implicit operator Span<byte>(ChatRoomClientLeftRef packet) => packet._data; 
}


/// <summary>
/// Is sent by the server when: This packet is sent by the server after another chat client sent a message to the current chat room.
/// Causes reaction on client side: The client will show the message.
/// </summary>
public readonly ref struct ChatRoomClientsRef
{
    private readonly Span<byte> _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatRoomClientsRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    public ChatRoomClientsRef(Span<byte> data)
        : this(data, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatRoomClientsRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    /// <param name="initialize">If set to <c>true</c>, the header data is automatically initialized and written to the underlying span.</param>
    private ChatRoomClientsRef(Span<byte> data, bool initialize)
    {
        this._data = data;
        if (initialize)
        {
            var header = this.Header;
            header.Type = HeaderType;
            header.Code = Code;
            header.Length = (ushort)data.Length;
        }
    }

    /// <summary>
    /// Gets the header type of this data packet.
    /// </summary>
    public static byte HeaderType => 0xC2;

    /// <summary>
    /// Gets the operation code of this data packet.
    /// </summary>
    public static byte Code => 0x02;

    /// <summary>
    /// Gets the header of this packet.
    /// </summary>
    public C2HeaderRef Header => new (this._data);

    /// <summary>
    /// Gets or sets the client count.
    /// </summary>
    public byte ClientCount
    {
        get => this._data[6];
        set => this._data[6] = value;
    }

    /// <summary>
    /// Gets the <see cref="ChatClientRef"/> of the specified index.
    /// </summary>
        public ChatClientRef this[int index] => new (this._data[(8 + index * ChatClientRef.Length)..]);

    /// <summary>
    /// Performs an implicit conversion from a Span of bytes to a <see cref="ChatRoomClients"/>.
    /// </summary>
    /// <param name="packet">The packet as span.</param>
    /// <returns>The packet as struct.</returns>
    public static implicit operator ChatRoomClientsRef(Span<byte> packet) => new (packet, false);

    /// <summary>
    /// Performs an implicit conversion from <see cref="ChatRoomClients"/> to a Span of bytes.
    /// </summary>
    /// <param name="packet">The packet as struct.</param>
    /// <returns>The packet as byte span.</returns>
    public static implicit operator Span<byte>(ChatRoomClientsRef packet) => packet._data; 

    /// <summary>
    /// Calculates the size of the packet for the specified count of <see cref="ChatClientRef"/>.
    /// </summary>
    /// <param name="clientsCount">The count of <see cref="ChatClientRef"/> from which the size will be calculated.</param>
        
    public static int GetRequiredSize(int clientsCount) => clientsCount * ChatClientRef.Length + 8;


/// <summary>
/// Contains the index and the name of a connected chat client in the room..
/// </summary>
public readonly ref struct ChatClientRef
{
    private readonly Span<byte> _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatClientRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    public ChatClientRef(Span<byte> data)
    {
        this._data = data;
    }

    /// <summary>
    /// Gets the initial length of this data packet. When the size is dynamic, this value may be bigger than actually needed.
    /// </summary>
    public static int Length => 11;

    /// <summary>
    /// Gets or sets the index.
    /// </summary>
    public byte Index
    {
        get => this._data[0];
        set => this._data[0] = value;
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name
    {
        get => this._data.ExtractString(1, 10, System.Text.Encoding.UTF8);
        set => this._data.Slice(1, 10).WriteString(value, System.Text.Encoding.UTF8);
    }
}
}


/// <summary>
/// Is sent by the server when: This packet is sent by the server after another chat client sent a message to the current chat room.
/// Causes reaction on client side: The client will show the message.
/// </summary>
public readonly ref struct ChatMessageRef
{
    private readonly Span<byte> _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatMessageRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    public ChatMessageRef(Span<byte> data)
        : this(data, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatMessageRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    /// <param name="initialize">If set to <c>true</c>, the header data is automatically initialized and written to the underlying span.</param>
    private ChatMessageRef(Span<byte> data, bool initialize)
    {
        this._data = data;
        if (initialize)
        {
            var header = this.Header;
            header.Type = HeaderType;
            header.Code = Code;
            header.Length = (byte)data.Length;
        }
    }

    /// <summary>
    /// Gets the header type of this data packet.
    /// </summary>
    public static byte HeaderType => 0xC1;

    /// <summary>
    /// Gets the operation code of this data packet.
    /// </summary>
    public static byte Code => 0x04;

    /// <summary>
    /// Gets the header of this packet.
    /// </summary>
    public C1HeaderRef Header => new (this._data);

    /// <summary>
    /// Gets or sets the sender index.
    /// </summary>
    public byte SenderIndex
    {
        get => this._data[3];
        set => this._data[3] = value;
    }

    /// <summary>
    /// Gets or sets the message length.
    /// </summary>
    public byte MessageLength
    {
        get => this._data[4];
        set => this._data[4] = value;
    }

    /// <summary>
    /// Gets or sets the message. It's "encrypted" with the 3-byte XOR key (FC CF AB).
    /// </summary>
    public string Message
    {
        get => this._data.ExtractString(5, this._data.Length - 5, System.Text.Encoding.UTF8);
        set => this._data.Slice(5).WriteString(value, System.Text.Encoding.UTF8);
    }

    /// <summary>
    /// Performs an implicit conversion from a Span of bytes to a <see cref="ChatMessage"/>.
    /// </summary>
    /// <param name="packet">The packet as span.</param>
    /// <returns>The packet as struct.</returns>
    public static implicit operator ChatMessageRef(Span<byte> packet) => new (packet, false);

    /// <summary>
    /// Performs an implicit conversion from <see cref="ChatMessage"/> to a Span of bytes.
    /// </summary>
    /// <param name="packet">The packet as struct.</param>
    /// <returns>The packet as byte span.</returns>
    public static implicit operator Span<byte>(ChatMessageRef packet) => packet._data; 

    /// <summary>
    /// Calculates the size of the packet for the specified field content.
    /// </summary>
    /// <param name="content">The content of the variable 'Message' field from which the size will be calculated.</param>
    public static int GetRequiredSize(string content) => System.Text.Encoding.UTF8.GetByteCount(content) + 1 + 5;
}


/// <summary>
/// Is sent by the client when: This packet is sent by the client in a fixed interval.
/// Causes reaction on server side: The server will keep the connection and chat room intact as long as the clients sends a message in a certain period of time.
/// </summary>
public readonly ref struct KeepAliveRef
{
    private readonly Span<byte> _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeepAliveRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    public KeepAliveRef(Span<byte> data)
        : this(data, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeepAliveRef"/> struct.
    /// </summary>
    /// <param name="data">The underlying data.</param>
    /// <param name="initialize">If set to <c>true</c>, the header data is automatically initialized and written to the underlying span.</param>
    private KeepAliveRef(Span<byte> data, bool initialize)
    {
        this._data = data;
        if (initialize)
        {
            var header = this.Header;
            header.Type = HeaderType;
            header.Code = Code;
            header.Length = (byte)Math.Min(data.Length, Length);
        }
    }

    /// <summary>
    /// Gets the header type of this data packet.
    /// </summary>
    public static byte HeaderType => 0xC1;

    /// <summary>
    /// Gets the operation code of this data packet.
    /// </summary>
    public static byte Code => 0x05;

    /// <summary>
    /// Gets the initial length of this data packet. When the size is dynamic, this value may be bigger than actually needed.
    /// </summary>
    public static int Length => 3;

    /// <summary>
    /// Gets the header of this packet.
    /// </summary>
    public C1HeaderRef Header => new (this._data);

    /// <summary>
    /// Performs an implicit conversion from a Span of bytes to a <see cref="KeepAlive"/>.
    /// </summary>
    /// <param name="packet">The packet as span.</param>
    /// <returns>The packet as struct.</returns>
    public static implicit operator KeepAliveRef(Span<byte> packet) => new (packet, false);

    /// <summary>
    /// Performs an implicit conversion from <see cref="KeepAlive"/> to a Span of bytes.
    /// </summary>
    /// <param name="packet">The packet as struct.</param>
    /// <returns>The packet as byte span.</returns>
    public static implicit operator Span<byte>(KeepAliveRef packet) => packet._data; 
}
