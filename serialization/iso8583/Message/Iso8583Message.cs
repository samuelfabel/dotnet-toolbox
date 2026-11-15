using Iso8583.Message.Field;

namespace Iso8583.Message;

/// <summary>
/// Represents an ISO 8583 message with various fields.
/// </summary>
public class Iso8583Message(ReadOnlySpan<byte> primaryBitmap)
{
    /// <summary>
    /// Gets or sets the primary bitmap of the ISO 8583 message.
    /// </summary>
    public byte[] PrimaryBitmap { get; set; } = primaryBitmap.ToArray();

    /// <summary>
    /// Gets or sets the secondary bitmap of the ISO 8583 message. #1
    /// </summary>
    public byte[]? SecondaryBitmap { get; set; }

    /// <summary>
    /// Gets or sets the primary account number (PAN) of the ISO 8583 message. #2
    /// </summary>
    public string? PrimaryAccountNumber { get; set; }

    /// <summary>
    ///     Gets or sets the processing code of the ISO 8583 message. #3
    /// </summary>
    public string? ProcessingCode { get; set; }

    /// <summary>
    /// Gets or sets the amount, transaction of the ISO 8583 message. #4
    /// </summary>
    public decimal? AmountTransaction { get; set; }

    /// <summary>
    /// Gets or sets the amount, settlement of the ISO 8583 message. #5
    /// </summary>
    public decimal? AmountSettlement { get; set; }

    /// <summary>
    /// Gets or sets the amount, cardholder billing of the ISO 8583 message. #6
    /// </summary>
    public decimal? AmountCardholderBilling { get; set; }

    /// <summary>
    /// Gets or sets the transmission date and time of the ISO 8583 message. #7
    /// </summary>
    public DateTime? TransmissionDateTime { get; set; }

    /// <summary>
    /// Gets or sets the amount, cardholder billing fee of the ISO 8583 message. #8
    /// </summary>
    public decimal? AmountCardholderBillingFee { get; set; }

    /// <summary>
    /// Gets or sets the conversion rate, settlement of the ISO 8583 message. #9
    /// </summary>
    public decimal? ConversionRateSettlement { get; set; }

    /// <summary>
    /// Gets or sets the conversion rate, cardholder billing of the ISO 8583 message. #10
    /// </summary>
    public decimal? ConversionRateCardholderBilling { get; set; }

    /// <summary>
    /// Gets or sets the system trace audit number (STAN) of the ISO 8583 message. #11
    /// </summary>
    public string? SystemTraceAuditNumber { get; set; }

    /// <summary>
    /// Gets or sets the local transaction time of the ISO 8583 message. #12
    /// </summary>
    public TimeOnly? LocalTransactionTime { get; set; }

    /// <summary>
    /// Gets or sets the local transaction date of the ISO 8583 message. #13
    /// </summary>
    public DateOnly? LocalTransactionDate { get; set; }

    /// <summary>
    /// Gets or sets the expiration date of the ISO 8583 message. #14
    /// </summary>
    public DateOnly? ExpirationDate { get; set; }

    /// <summary>
    /// Gets or sets the settlement date of the ISO 8583 message. #15
    /// </summary>
    public DateOnly? SettlementDate { get; set; }

    /// <summary>
    /// Gets or sets the conversion date of the ISO 8583 message. #16
    /// </summary>
    public DateOnly? ConversionDate { get; set; }

    /// <summary>
    /// Gets or sets the capture date of the ISO 8583 message. #17
    /// </summary>
    public DateOnly CaptureDate { get; set; }

    /// <summary>
    /// Gets or sets the merchant type / MCC of the ISO 8583 message. #18
    /// </summary>
    public string? MerchantType { get; set; }

    /// <summary>
    /// Gets or sets the acquiring institution country code. #19
    /// </summary>
    public string? AcquiringInstitutionCountryCode { get; set; }

    /// <summary>
    /// Gets or sets the PAN extended country code. #20
    /// </summary>
    public string? PanExtendedCountryCode { get; set; }

    /// <summary>
    /// Gets or sets the forwarding institution country code. #21
    /// </summary>
    public string? ForwardingInstitutionCountryCode { get; set; }

    /// <summary>
    /// Gets or sets the point of service entry mode. #22
    /// </summary>
    public string? PointOfServiceEntryMode { get; set; }

    /// <summary>
    /// Gets or sets the card sequence number. #23
    /// </summary>
    public string? CardSequenceNumber { get; set; }

    /// <summary>
    /// Gets or sets the function code / NII. #24
    /// </summary>
    public string? FunctionCode { get; set; }

    /// <summary>
    /// Gets or sets the point of service condition code. #25
    /// </summary>
    public string? PointOfServiceConditionCode { get; set; }

    /// <summary>
    /// Gets or sets the point of service capture code. #26
    /// </summary>
    public string? PointOfServiceCaptureCode { get; set; }

    /// <summary>
    /// Gets or sets the authorizing identification response length. #27
    /// </summary>
    public byte? AuthorizingIdentificationResponseLength { get; set; }

    /// <summary>
    /// Gets or sets the transaction fee. #28
    /// </summary>
    public decimal? TransactionFee { get; set; }

    /// <summary>
    /// Gets or sets the settlement fee. #29
    /// </summary>
    public decimal? SettlementFee { get; set; }

    /// <summary>
    /// Gets or sets the transaction processing fee. #30
    /// </summary>
    public decimal? TransactionProcessingFee { get; set; }

    /// <summary>
    /// Gets or sets the settlement processing fee. #31
    /// </summary>
    public decimal? SettlementProcessingFee { get; set; }

    /// <summary>
    /// Gets or sets the acquiring institution identification code. #32
    /// </summary>
    public string? AcquiringInstitutionIdentificationCode { get; set; }

    /// <summary>
    /// Gets or sets the forwarding institution identification code. #33
    /// </summary>
    public string? ForwardingInstitutionIdentificationCode { get; set; }

    /// <summary>
    /// Gets or sets the PAN extended. #34
    /// </summary>
    public string? PanExtended { get; set; }

    /// <summary>
    /// Gets or sets the track 2 data. #35
    /// </summary>
    public string? Track2Data { get; set; }

    /// <summary>
    /// Gets or sets the track 3 data. #36
    /// </summary>
    public string? Track3Data { get; set; }

    /// <summary>
    /// Gets or sets the retrieval reference number. #37
    /// </summary>
    public string? RetrievalReferenceNumber { get; set; }

    /// <summary>
    /// Gets or sets the authorization identification response. #38
    /// </summary>
    public string? AuthorizationIdentificationResponse { get; set; }

    /// <summary>
    /// Gets or sets the response code. #39
    /// </summary>
    public string? ResponseCode { get; set; }

    /// <summary>
    /// Gets or sets the service restriction code. #40
    /// </summary>
    public string? ServiceRestrictionCode { get; set; }

    /// <summary>
    /// Gets or sets the card acceptor terminal identification. #41
    /// </summary>
    public string? CardAcceptorTerminalIdentification { get; set; }

    /// <summary>
    /// Gets or sets the card acceptor identification code. #42
    /// </summary>
    public string? CardAcceptorIdentificationCode { get; set; }

    /// <summary>
    /// Gets or sets the card acceptor name / location. #43
    /// </summary>
    public string? CardAcceptorNameLocation { get; set; }

    /// <summary>
    /// Gets or sets the additional response data. #44
    /// </summary>
    public string? AdditionalResponseData { get; set; }

    /// <summary>
    /// Gets or sets the track 1 data. #45
    /// </summary>
    public string? Track1Data { get; set; }

    /// <summary>
    /// Gets or sets the additional ISO data. #46
    /// </summary>
    public string? AdditionalDataIso { get; set; }

    /// <summary>
    /// Gets or sets the additional national data. #47
    /// </summary>
    public string? AdditionalDataNational { get; set; }

    /// <summary>
    /// Gets or sets the additional private data. #48
    /// </summary>
    public string? AdditionalDataPrivate { get; set; }

    /// <summary>
    /// Gets or sets the transaction currency code. #49
    /// </summary>
    public string? CurrencyCodeTransaction { get; set; }

    /// <summary>
    /// Gets or sets the settlement currency code. #50
    /// </summary>
    public string? CurrencyCodeSettlement { get; set; }

    /// <summary>
    /// Gets or sets the cardholder billing currency code. #51
    /// </summary>
    public string? CurrencyCodeCardholderBilling { get; set; }

    /// <summary>
    /// Gets or sets the PIN data. #52
    /// </summary>
    public byte[]? PinData { get; set; }

    /// <summary>
    /// Gets or sets the security related control information. #53
    /// </summary>
    public string? SecurityRelatedControlInformation { get; set; }

    /// <summary>
    /// Gets or sets the additional amounts. #54
    /// </summary>
    public string? AdditionalAmounts { get; set; }

    /// <summary>
    /// Gets or sets the ICC / EMV data. #55
    /// </summary>
    public byte[]? IccData { get; set; }

    /// <summary>
    /// Gets or sets the field reserved for ISO use. #56
    /// </summary>
    public string? ReservedIso { get; set; }

    /// <summary>
    /// Gets or sets the field reserved for national use. #57
    /// </summary>
    public string? ReservedNational57 { get; set; }

    /// <summary>
    /// Gets or sets the field reserved for national use. #58
    /// </summary>
    public string? ReservedNational58 { get; set; }

    /// <summary>
    /// Gets or sets the field reserved for national use. #59
    /// </summary>
    public string? ReservedNational59 { get; set; }

    /// <summary>
    /// Gets or sets the field reserved for national use. #60
    /// </summary>
    public string? ReservedNational60 { get; set; }

    /// <summary>
    /// Gets or sets the field reserved for national use. #61
    /// </summary>
    public string? ReservedNational61 { get; set; }

    /// <summary>
    /// Gets or sets the field reserved for private use. #62
    /// </summary>
    public string? ReservedPrivate62 { get; set; }

    /// <summary>
    /// Gets or sets the field reserved for private use. #63
    /// </summary>
    public string? ReservedPrivate63 { get; set; }

    /// <summary>
    /// Gets or sets the message authentication code (MAC). #64
    /// </summary>
    public byte[]? MessageAuthenticationCode { get; set; }

    /// <summary>
    /// Represents a mapping of ISO 8583 fields to their corresponding field definitions.
    /// </summary>
    public static class Map
    {
        /// <summary>
        /// Gets a dictionary that maps field indices to their corresponding ISO 8583 field definitions.
        /// </summary>
        public static readonly Iso8583Field[] Fields = [
            new FixedBinaryField(1, 8, (m, v) => m.SecondaryBitmap = v, m => m.SecondaryBitmap),
            new FixedBcdField(2, 19, (m, v) => m.PrimaryAccountNumber = v, m => m.PrimaryAccountNumber),
            new FixedBcdField(3, 6, (m, v) => m.ProcessingCode = v, m => m.ProcessingCode),
            new DecimalBcdField(4, 12, (m, v) => m.AmountTransaction = v, m => m.AmountTransaction),
            new DecimalBcdField(5, 12, (m, v) => m.AmountSettlement = v, m => m.AmountSettlement),
            new DecimalBcdField(6, 12, (m, v) => m.AmountCardholderBilling = v, m => m.AmountCardholderBilling),
            new DateTimeBcdField(7,  (m, v) => m.TransmissionDateTime = v, m => m.TransmissionDateTime),
            new DecimalBcdField(8, 8, (m, v) => m.AmountCardholderBillingFee = v, m => m.AmountCardholderBillingFee),
            new DecimalBcdField(9, 8, (m, v) => m.ConversionRateSettlement = v, m => m.ConversionRateSettlement),
            new DecimalBcdField(10, 8, (m, v) => m.ConversionRateCardholderBilling = v, m => m.ConversionRateCardholderBilling),
            new FixedBcdField(11, 6, (m, v) => m.SystemTraceAuditNumber = v, m => m.SystemTraceAuditNumber),
            new TimeOnlyBcdField(12, (m, v) => m.LocalTransactionTime = v, m => m.LocalTransactionTime),
            new DateOnlyBcdField(13, (m, v) => m.LocalTransactionDate = v, m => m.LocalTransactionDate),
            new DateOnlyBcdField(14, (m, v) => m.ExpirationDate = v, m => m.ExpirationDate),
            new DateOnlyBcdField(15, (m, v) => m.SettlementDate = v, m => m.SettlementDate),
            new DateOnlyBcdField(16, (m, v) => m.ConversionDate = v, m => m.ConversionDate),
            new DateOnlyBcdField(17, (m, v) => m.CaptureDate = v, m => m.CaptureDate),
            new FixedBcdField(18, 4, (m, v) => m.MerchantType = v, m => m.MerchantType),
            new FixedBcdField(19, 3, (m, v) => m.AcquiringInstitutionCountryCode = v, m => m.AcquiringInstitutionCountryCode),
            new FixedBcdField(20, 3, (m, v) => m.PanExtendedCountryCode = v, m => m.PanExtendedCountryCode),
            new FixedBcdField(21, 3, (m, v) => m.ForwardingInstitutionCountryCode = v, m => m.ForwardingInstitutionCountryCode),
            new FixedBcdField(22, 3, (m, v) => m.PointOfServiceEntryMode = v, m => m.PointOfServiceEntryMode),
            new FixedBcdField(23, 3, (m, v) => m.CardSequenceNumber = v, m => m.CardSequenceNumber),
            new FixedBcdField(24, 3, (m, v) => m.FunctionCode = v, m => m.FunctionCode),
            new FixedBcdField(25, 2, (m, v) => m.PointOfServiceConditionCode = v, m => m.PointOfServiceConditionCode),
            new FixedBcdField(26, 2, (m, v) => m.PointOfServiceCaptureCode = v, m => m.PointOfServiceCaptureCode),
            new ByteField(27, (m, v) => m.AuthorizingIdentificationResponseLength = v, m => m.AuthorizingIdentificationResponseLength),
            new DecimalBcdField(28, 8, (m, v) => m.TransactionFee = v, m => m.TransactionFee),
            new DecimalBcdField(29, 8, (m, v) => m.SettlementFee = v, m => m.SettlementFee),
            new DecimalBcdField(30, 8, (m, v) => m.TransactionProcessingFee = v, m => m.TransactionProcessingFee),
            new DecimalBcdField(31, 8, (m, v) => m.SettlementProcessingFee = v, m => m.SettlementProcessingFee),
            new LlvarAsciiField(32, (m, v) => m.AcquiringInstitutionIdentificationCode = v, m => m.AcquiringInstitutionIdentificationCode),
            new LlvarAsciiField(33, (m, v) => m.ForwardingInstitutionIdentificationCode = v, m => m.ForwardingInstitutionIdentificationCode),
            new LlvarAsciiField(34, (m, v) => m.PanExtended = v, m => m.PanExtended),
            new LlvarAsciiField(35, (m, v) => m.Track2Data = v, m => m.Track2Data),
            new LllvarAsciiField(36, (m, v) => m.Track3Data = v, m => m.Track3Data),
            new FixedAsciiField(37, 12, (m, v) => m.RetrievalReferenceNumber = v, m => m.RetrievalReferenceNumber),
            new FixedAsciiField(38, 6, (m, v) => m.AuthorizationIdentificationResponse = v, m => m.AuthorizationIdentificationResponse),
            new FixedAsciiField(39, 2, (m, v) => m.ResponseCode = v, m => m.ResponseCode),
            new FixedAsciiField(40, 3, (m, v) => m.ServiceRestrictionCode = v, m => m.ServiceRestrictionCode),
            new FixedAsciiField(41, 8, (m, v) => m.CardAcceptorTerminalIdentification = v, m => m.CardAcceptorTerminalIdentification),
            new FixedAsciiField(42, 15, (m, v) => m.CardAcceptorIdentificationCode = v, m => m.CardAcceptorIdentificationCode),
            new FixedAsciiField(43, 40, (m, v) => m.CardAcceptorNameLocation = v, m => m.CardAcceptorNameLocation),
            new LlvarAsciiField(44, (m, v) => m.AdditionalResponseData = v, m => m.AdditionalResponseData),
            new LlvarAsciiField(45, (m, v) => m.Track1Data = v, m => m.Track1Data),
            new LllvarAsciiField(46, (m, v) => m.AdditionalDataIso = v, m => m.AdditionalDataIso, 999),
            new LllvarAsciiField(47, (m, v) => m.AdditionalDataNational = v, m => m.AdditionalDataNational, 999),
            new LllvarAsciiField(48, (m, v) => m.AdditionalDataPrivate = v, m => m.AdditionalDataPrivate, 999),
            new FixedBcdField(49, 3, (m, v) => m.CurrencyCodeTransaction = v, m => m.CurrencyCodeTransaction),
            new FixedBcdField(50, 3, (m, v) => m.CurrencyCodeSettlement = v, m => m.CurrencyCodeSettlement),
            new FixedBcdField(51, 3, (m, v) => m.CurrencyCodeCardholderBilling = v, m => m.CurrencyCodeCardholderBilling),
            new FixedBinaryField(52, 8, (m, v) => m.PinData = v, m => m.PinData),
            new FixedBcdField(53, 16, (m, v) => m.SecurityRelatedControlInformation = v, m => m.SecurityRelatedControlInformation),
            new LllvarAsciiField(54, (m, v) => m.AdditionalAmounts = v, m => m.AdditionalAmounts),
            new LllvarBinaryField(55, (m, v) => m.IccData = v, m => m.IccData, 999),
            new LllvarAsciiField(56, (m, v) => m.ReservedIso = v, m => m.ReservedIso, 999),
            new LllvarAsciiField(57, (m, v) => m.ReservedNational57 = v, m => m.ReservedNational57, 999),
            new LllvarAsciiField(58, (m, v) => m.ReservedNational58 = v, m => m.ReservedNational58, 999),
            new LllvarAsciiField(59, (m, v) => m.ReservedNational59 = v, m => m.ReservedNational59, 999),
            new LllvarAsciiField(60, (m, v) => m.ReservedNational60 = v, m => m.ReservedNational60, 999),
            new LllvarAsciiField(61, (m, v) => m.ReservedNational61 = v, m => m.ReservedNational61, 999),
            new LllvarAsciiField(62, (m, v) => m.ReservedPrivate62 = v, m => m.ReservedPrivate62, 999),
            new LllvarAsciiField(63, (m, v) => m.ReservedPrivate63 = v, m => m.ReservedPrivate63, 999),
            new FixedBinaryField(64, 8, (m, v) => m.MessageAuthenticationCode = v, m => m.MessageAuthenticationCode),
        ];
    }
}

