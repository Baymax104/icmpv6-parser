namespace Models.Field;

public readonly struct Icmp6Field {

    /// <summary>
    ///     消息类型偏移
    /// </summary>
    public const int TypePosition = 0;
    
    /// <summary>
    ///     消息类型长度
    /// </summary>
    public const int TypeLength = 1;
    
    /// <summary>
    ///     Code偏移
    /// </summary>
    public const int CodePosition = 1;
    
    /// <summary>
    ///     Code长度
    /// </summary>
    public const int CodeLength = 1;
    
    /// <summary>
    ///     校验和偏移
    /// </summary>
    public const int ChecksumPosition = 2;

    /// <summary>
    ///     校验和长度
    /// </summary>
    public const int ChecksumLength = 2;
    
    /// <summary>
    ///     消息偏移
    /// </summary>
    public const int MessagePosition = 4;

    /// <summary>
    ///     首部长度
    /// </summary>
    public const int HeaderLength = 4;

}

public readonly struct EchoField {

    /// <summary>
    ///     Identifier偏移
    /// </summary>
    public const int IdentifierPosition = 0;

    /// <summary>
    ///     Identifier长度
    /// </summary>
    public const int IdentifierLength = 2;

    /// <summary>
    ///     SequenceNumber偏移
    /// </summary>
    public const int SequenceNumberPosition = 2;

    /// <summary>
    ///     SequenceNumber长度
    /// </summary>
    public const int SequenceNumberLength = 2;

    /// <summary>
    ///     Data偏移
    /// </summary>
    public const int DataPosition = 4;

    /// <summary>
    ///     首部长度
    /// </summary>
    public const int HeaderLength = 4;
}
