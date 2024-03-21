namespace Models.Field;

public readonly struct Icmp6Field {

    /// <summary>
    ///     校验和长度
    /// </summary>
    public static readonly int ChecksumLength;

    /// <summary>
    ///     校验和偏移
    /// </summary>
    public static readonly int ChecksumPosition;

    /// <summary>
    ///     Code长度
    /// </summary>
    public static readonly int CodeLength;

    /// <summary>
    ///     Code偏移
    /// </summary>
    public static readonly int CodePosition;

    /// <summary>
    ///     消息类型长度
    /// </summary>
    public static readonly int TypeLength;

    /// <summary>
    ///     消息类型偏移
    /// </summary>
    public static readonly int TypePosition;

    /// <summary>
    ///     消息偏移
    /// </summary>
    public static readonly int MessagePosition;

    /// <summary>
    ///     首部长度
    /// </summary>
    public static readonly int HeaderLength;

    static Icmp6Field() {
        ChecksumLength = 2;
        CodeLength = 1;
        TypeLength = 1;
        TypePosition = 0;
        CodePosition = TypePosition + TypeLength;
        ChecksumPosition = CodePosition + CodeLength;
        MessagePosition = ChecksumPosition + ChecksumLength;
        HeaderLength = ChecksumPosition + ChecksumLength;
    }
}
