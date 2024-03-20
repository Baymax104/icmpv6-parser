namespace Models.Field;

public struct NdpOptionField {

    /// <summary>
    /// Type偏移
    /// </summary>
    public const int TypePosition = 0;

    /// <summary>
    /// 长度偏移
    /// </summary>
    public const int LengthPosition = 1;

    /// <summary>
    /// 负载偏移
    /// </summary>
    public const int PayloadPosition = 2;

    /// <summary>
    /// 首部长度
    /// </summary>
    public const int HeaderLength = 2;
}

public struct PrefixInformationOptionField {

    /// <summary>
    /// Preferred Lifetime偏移
    /// </summary>
    public const int PreferredLifetimePosition = 6;

    /// <summary>
    /// Prefix偏移
    /// </summary>
    public const int PrefixPosition = 14;

    /// <summary>
    /// Valid Lifetime偏移
    /// </summary>
    public const int ValidLifetimePosition = 2;

    /// <summary>
    /// Valid Lifetime长度
    /// </summary>
    public const int ValidLifetimeLength = 4;

    /// <summary>
    /// Preferred Lifetime长度
    /// </summary>
    public const int PreferredLifetimeLength = 4;

    /// <summary>
    /// Prefix Length偏移
    /// </summary>
    public const int PrefixLengthPosition = 0;

    /// <summary>
    /// OnLink和Autonomous Address Configuration偏移
    /// </summary>
    public const int LAPosition = 1;

}

public struct RedirectedHeaderOptionField {
    /// <summary>
    /// IpHeaderData偏移
    /// </summary>
    public const int IpHeaderDataPosition = 6;
}

public struct MtuOptionField {
    /// <summary>
    /// MTU偏移
    /// </summary>
    public const int MtuPosition = 2;

    /// <summary>
    /// MTU长度
    /// </summary>
    public const int MtuLength = 4;
}
