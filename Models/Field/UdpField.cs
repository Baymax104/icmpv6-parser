namespace Models.Field;

public readonly struct UdpField {

    /// <summary>
    ///     端口长度
    /// </summary>
    public const int PortLength = 2;

    /// <summary>
    ///     源端口偏移
    /// </summary>
    public const int SourcePortPosition = 0;

    /// <summary>
    ///     目的端口偏移
    /// </summary>
    public const int DestinationPortPosition = 2;

    /// <summary>
    ///     首部大小长度
    /// </summary>
    public const int HeaderLengthLength = 2;

    /// <summary>
    ///     首部大小偏移
    /// </summary>
    public const int HeaderLengthPosition = 4;

    /// <summary>
    ///     校验和长度
    /// </summary>
    public const int ChecksumLength = 2;

    /// <summary>
    ///     校验和偏移
    /// </summary>
    public const int ChecksumPosition = 6;

    /// <summary>
    ///     首部长度
    /// </summary>
    public const int HeaderLength = 8;
}
