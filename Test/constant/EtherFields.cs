namespace Test.constant;

public readonly struct EtherFields {
    public static readonly int DestinationMacPosition;

    public static readonly int HeaderLength;

    public static readonly int MacAddressLength;

    public static readonly int SourceMacPosition;

    public static readonly int TypeLength;

    public static readonly int TypePosition;

    static EtherFields() {
        DestinationMacPosition = 0;
        MacAddressLength = 6;
        TypeLength = 2;
        SourceMacPosition = MacAddressLength;
        TypePosition = MacAddressLength * 2;
        HeaderLength = TypePosition + TypeLength;
    }
}

