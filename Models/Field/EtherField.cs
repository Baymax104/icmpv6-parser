namespace Models.Field;

public readonly struct EtherField {

    /// <summary>
    ///     目的地址偏移
    /// </summary>
    public const int DestinationMacPosition = 0;

    /// <summary>
    ///     MAC地址长度
    /// </summary>
    public const int MacAddressLength = 6;

    /// <summary>
    ///     源地址偏移
    /// </summary>
    public const int SourceMacPosition = 6;

    /// <summary>
    ///     类型字段长度
    /// </summary>
    public const int TypeLength = 2;

    /// <summary>
    ///     类型字段偏移
    /// </summary>
    public const int TypePosition = 12;

    /// <summary>
    ///     首部长度
    /// </summary>
    public const int HeaderLength = 14;
}
