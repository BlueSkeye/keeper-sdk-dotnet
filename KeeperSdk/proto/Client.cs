// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: client.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Tokens {

  /// <summary>Holder for reflection information generated from client.proto</summary>
  public static partial class ClientReflection {

    #region Descriptor
    /// <summary>File descriptor for client.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ClientReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgxjbGllbnQucHJvdG8SBlRva2VucyJ1ChhCcmVhY2hXYXRjaFVwZGF0ZVJl",
            "cXVlc3QSQgoYYnJlYWNoV2F0Y2hSZWNvcmRSZXF1ZXN0GAEgAygLMiAuVG9r",
            "ZW5zLkJyZWFjaFdhdGNoUmVjb3JkUmVxdWVzdBIVCg1lbmNyeXB0ZWREYXRh",
            "GAIgASgMIpwBChhCcmVhY2hXYXRjaFJlY29yZFJlcXVlc3QSEQoJcmVjb3Jk",
            "VWlkGAEgASgMEhUKDWVuY3J5cHRlZERhdGEYAiABKAwSOAoTYnJlYWNoV2F0",
            "Y2hJbmZvVHlwZRgDIAEoDjIbLlRva2Vucy5CcmVhY2hXYXRjaEluZm9UeXBl",
            "EhwKFHVwZGF0ZVVzZXJXaG9TY2FubmVkGAQgASgIIoEBCg9CcmVhY2hXYXRj",
            "aERhdGESJQoJcGFzc3dvcmRzGAEgAygLMhIuVG9rZW5zLkJXUGFzc3dvcmQS",
            "IgoGZW1haWxzGAIgAygLMhIuVG9rZW5zLkJXUGFzc3dvcmQSIwoHZG9tYWlu",
            "cxgDIAMoCzISLlRva2Vucy5CV1Bhc3N3b3JkIl0KCkJXUGFzc3dvcmQSDQoF",
            "dmFsdWUYASABKAkSEAoIcmVzb2x2ZWQYAiABKAQSIAoGc3RhdHVzGAMgASgO",
            "MhAuVG9rZW5zLkJXU3RhdHVzEgwKBGV1aWQYBCABKAwqOQoTQnJlYWNoV2F0",
            "Y2hJbmZvVHlwZRIKCgZSRUNPUkQQABIWChJBTFRFUk5BVEVfUEFTU1dPUkQQ",
            "ASpFCghCV1N0YXR1cxIICgRHT09EEAASCwoHQ0hBTkdFRBABEggKBFdFQUsQ",
            "AhIMCghCUkVBQ0hFRBADEgoKBklHTk9SRRAEQiIKGGNvbS5rZWVwZXJzZWN1",
            "cml0eS5wcm90b0IGQ2xpZW50YgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Tokens.BreachWatchInfoType), typeof(global::Tokens.BWStatus), }, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Tokens.BreachWatchUpdateRequest), global::Tokens.BreachWatchUpdateRequest.Parser, new[]{ "BreachWatchRecordRequest", "EncryptedData" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Tokens.BreachWatchRecordRequest), global::Tokens.BreachWatchRecordRequest.Parser, new[]{ "RecordUid", "EncryptedData", "BreachWatchInfoType", "UpdateUserWhoScanned" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Tokens.BreachWatchData), global::Tokens.BreachWatchData.Parser, new[]{ "Passwords", "Emails", "Domains" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Tokens.BWPassword), global::Tokens.BWPassword.Parser, new[]{ "Value", "Resolved", "Status", "Euid" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum BreachWatchInfoType {
    /// <summary>
    /// note: this also is used for master password where the recordUid is blank
    /// </summary>
    [pbr::OriginalName("RECORD")] Record = 0,
    /// <summary>
    /// for any user_auth passwords, including the passwords used through Pythia
    /// </summary>
    [pbr::OriginalName("ALTERNATE_PASSWORD")] AlternatePassword = 1,
  }

  public enum BWStatus {
    [pbr::OriginalName("GOOD")] Good = 0,
    [pbr::OriginalName("CHANGED")] Changed = 1,
    [pbr::OriginalName("WEAK")] Weak = 2,
    [pbr::OriginalName("BREACHED")] Breached = 3,
    [pbr::OriginalName("IGNORE")] Ignore = 4,
  }

  #endregion

  #region Messages
  public sealed partial class BreachWatchUpdateRequest : pb::IMessage<BreachWatchUpdateRequest> {
    private static readonly pb::MessageParser<BreachWatchUpdateRequest> _parser = new pb::MessageParser<BreachWatchUpdateRequest>(() => new BreachWatchUpdateRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<BreachWatchUpdateRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tokens.ClientReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BreachWatchUpdateRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BreachWatchUpdateRequest(BreachWatchUpdateRequest other) : this() {
      breachWatchRecordRequest_ = other.breachWatchRecordRequest_.Clone();
      encryptedData_ = other.encryptedData_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BreachWatchUpdateRequest Clone() {
      return new BreachWatchUpdateRequest(this);
    }

    /// <summary>Field number for the "breachWatchRecordRequest" field.</summary>
    public const int BreachWatchRecordRequestFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Tokens.BreachWatchRecordRequest> _repeated_breachWatchRecordRequest_codec
        = pb::FieldCodec.ForMessage(10, global::Tokens.BreachWatchRecordRequest.Parser);
    private readonly pbc::RepeatedField<global::Tokens.BreachWatchRecordRequest> breachWatchRecordRequest_ = new pbc::RepeatedField<global::Tokens.BreachWatchRecordRequest>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Tokens.BreachWatchRecordRequest> BreachWatchRecordRequest {
      get { return breachWatchRecordRequest_; }
    }

    /// <summary>Field number for the "encryptedData" field.</summary>
    public const int EncryptedDataFieldNumber = 2;
    private pb::ByteString encryptedData_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString EncryptedData {
      get { return encryptedData_; }
      set {
        encryptedData_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as BreachWatchUpdateRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(BreachWatchUpdateRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!breachWatchRecordRequest_.Equals(other.breachWatchRecordRequest_)) return false;
      if (EncryptedData != other.EncryptedData) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= breachWatchRecordRequest_.GetHashCode();
      if (EncryptedData.Length != 0) hash ^= EncryptedData.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      breachWatchRecordRequest_.WriteTo(output, _repeated_breachWatchRecordRequest_codec);
      if (EncryptedData.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(EncryptedData);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += breachWatchRecordRequest_.CalculateSize(_repeated_breachWatchRecordRequest_codec);
      if (EncryptedData.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(EncryptedData);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(BreachWatchUpdateRequest other) {
      if (other == null) {
        return;
      }
      breachWatchRecordRequest_.Add(other.breachWatchRecordRequest_);
      if (other.EncryptedData.Length != 0) {
        EncryptedData = other.EncryptedData;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            breachWatchRecordRequest_.AddEntriesFrom(input, _repeated_breachWatchRecordRequest_codec);
            break;
          }
          case 18: {
            EncryptedData = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  public sealed partial class BreachWatchRecordRequest : pb::IMessage<BreachWatchRecordRequest> {
    private static readonly pb::MessageParser<BreachWatchRecordRequest> _parser = new pb::MessageParser<BreachWatchRecordRequest>(() => new BreachWatchRecordRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<BreachWatchRecordRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tokens.ClientReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BreachWatchRecordRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BreachWatchRecordRequest(BreachWatchRecordRequest other) : this() {
      recordUid_ = other.recordUid_;
      encryptedData_ = other.encryptedData_;
      breachWatchInfoType_ = other.breachWatchInfoType_;
      updateUserWhoScanned_ = other.updateUserWhoScanned_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BreachWatchRecordRequest Clone() {
      return new BreachWatchRecordRequest(this);
    }

    /// <summary>Field number for the "recordUid" field.</summary>
    public const int RecordUidFieldNumber = 1;
    private pb::ByteString recordUid_ = pb::ByteString.Empty;
    /// <summary>
    /// if you store the recordUid as a string this is URLSafeBase64.decode(recordUid)
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString RecordUid {
      get { return recordUid_; }
      set {
        recordUid_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "encryptedData" field.</summary>
    public const int EncryptedDataFieldNumber = 2;
    private pb::ByteString encryptedData_ = pb::ByteString.Empty;
    /// <summary>
    /// This is a BreachWatchRecordData message encrypted with the record key
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString EncryptedData {
      get { return encryptedData_; }
      set {
        encryptedData_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "breachWatchInfoType" field.</summary>
    public const int BreachWatchInfoTypeFieldNumber = 3;
    private global::Tokens.BreachWatchInfoType breachWatchInfoType_ = global::Tokens.BreachWatchInfoType.Record;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Tokens.BreachWatchInfoType BreachWatchInfoType {
      get { return breachWatchInfoType_; }
      set {
        breachWatchInfoType_ = value;
      }
    }

    /// <summary>Field number for the "updateUserWhoScanned" field.</summary>
    public const int UpdateUserWhoScannedFieldNumber = 4;
    private bool updateUserWhoScanned_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool UpdateUserWhoScanned {
      get { return updateUserWhoScanned_; }
      set {
        updateUserWhoScanned_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as BreachWatchRecordRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(BreachWatchRecordRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (RecordUid != other.RecordUid) return false;
      if (EncryptedData != other.EncryptedData) return false;
      if (BreachWatchInfoType != other.BreachWatchInfoType) return false;
      if (UpdateUserWhoScanned != other.UpdateUserWhoScanned) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (RecordUid.Length != 0) hash ^= RecordUid.GetHashCode();
      if (EncryptedData.Length != 0) hash ^= EncryptedData.GetHashCode();
      if (BreachWatchInfoType != global::Tokens.BreachWatchInfoType.Record) hash ^= BreachWatchInfoType.GetHashCode();
      if (UpdateUserWhoScanned != false) hash ^= UpdateUserWhoScanned.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (RecordUid.Length != 0) {
        output.WriteRawTag(10);
        output.WriteBytes(RecordUid);
      }
      if (EncryptedData.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(EncryptedData);
      }
      if (BreachWatchInfoType != global::Tokens.BreachWatchInfoType.Record) {
        output.WriteRawTag(24);
        output.WriteEnum((int) BreachWatchInfoType);
      }
      if (UpdateUserWhoScanned != false) {
        output.WriteRawTag(32);
        output.WriteBool(UpdateUserWhoScanned);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (RecordUid.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(RecordUid);
      }
      if (EncryptedData.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(EncryptedData);
      }
      if (BreachWatchInfoType != global::Tokens.BreachWatchInfoType.Record) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) BreachWatchInfoType);
      }
      if (UpdateUserWhoScanned != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(BreachWatchRecordRequest other) {
      if (other == null) {
        return;
      }
      if (other.RecordUid.Length != 0) {
        RecordUid = other.RecordUid;
      }
      if (other.EncryptedData.Length != 0) {
        EncryptedData = other.EncryptedData;
      }
      if (other.BreachWatchInfoType != global::Tokens.BreachWatchInfoType.Record) {
        BreachWatchInfoType = other.BreachWatchInfoType;
      }
      if (other.UpdateUserWhoScanned != false) {
        UpdateUserWhoScanned = other.UpdateUserWhoScanned;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            RecordUid = input.ReadBytes();
            break;
          }
          case 18: {
            EncryptedData = input.ReadBytes();
            break;
          }
          case 24: {
            BreachWatchInfoType = (global::Tokens.BreachWatchInfoType) input.ReadEnum();
            break;
          }
          case 32: {
            UpdateUserWhoScanned = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  public sealed partial class BreachWatchData : pb::IMessage<BreachWatchData> {
    private static readonly pb::MessageParser<BreachWatchData> _parser = new pb::MessageParser<BreachWatchData>(() => new BreachWatchData());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<BreachWatchData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tokens.ClientReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BreachWatchData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BreachWatchData(BreachWatchData other) : this() {
      passwords_ = other.passwords_.Clone();
      emails_ = other.emails_.Clone();
      domains_ = other.domains_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BreachWatchData Clone() {
      return new BreachWatchData(this);
    }

    /// <summary>Field number for the "passwords" field.</summary>
    public const int PasswordsFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Tokens.BWPassword> _repeated_passwords_codec
        = pb::FieldCodec.ForMessage(10, global::Tokens.BWPassword.Parser);
    private readonly pbc::RepeatedField<global::Tokens.BWPassword> passwords_ = new pbc::RepeatedField<global::Tokens.BWPassword>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Tokens.BWPassword> Passwords {
      get { return passwords_; }
    }

    /// <summary>Field number for the "emails" field.</summary>
    public const int EmailsFieldNumber = 2;
    private static readonly pb::FieldCodec<global::Tokens.BWPassword> _repeated_emails_codec
        = pb::FieldCodec.ForMessage(18, global::Tokens.BWPassword.Parser);
    private readonly pbc::RepeatedField<global::Tokens.BWPassword> emails_ = new pbc::RepeatedField<global::Tokens.BWPassword>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Tokens.BWPassword> Emails {
      get { return emails_; }
    }

    /// <summary>Field number for the "domains" field.</summary>
    public const int DomainsFieldNumber = 3;
    private static readonly pb::FieldCodec<global::Tokens.BWPassword> _repeated_domains_codec
        = pb::FieldCodec.ForMessage(26, global::Tokens.BWPassword.Parser);
    private readonly pbc::RepeatedField<global::Tokens.BWPassword> domains_ = new pbc::RepeatedField<global::Tokens.BWPassword>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Tokens.BWPassword> Domains {
      get { return domains_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as BreachWatchData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(BreachWatchData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!passwords_.Equals(other.passwords_)) return false;
      if(!emails_.Equals(other.emails_)) return false;
      if(!domains_.Equals(other.domains_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= passwords_.GetHashCode();
      hash ^= emails_.GetHashCode();
      hash ^= domains_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      passwords_.WriteTo(output, _repeated_passwords_codec);
      emails_.WriteTo(output, _repeated_emails_codec);
      domains_.WriteTo(output, _repeated_domains_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += passwords_.CalculateSize(_repeated_passwords_codec);
      size += emails_.CalculateSize(_repeated_emails_codec);
      size += domains_.CalculateSize(_repeated_domains_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(BreachWatchData other) {
      if (other == null) {
        return;
      }
      passwords_.Add(other.passwords_);
      emails_.Add(other.emails_);
      domains_.Add(other.domains_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            passwords_.AddEntriesFrom(input, _repeated_passwords_codec);
            break;
          }
          case 18: {
            emails_.AddEntriesFrom(input, _repeated_emails_codec);
            break;
          }
          case 26: {
            domains_.AddEntriesFrom(input, _repeated_domains_codec);
            break;
          }
        }
      }
    }

  }

  public sealed partial class BWPassword : pb::IMessage<BWPassword> {
    private static readonly pb::MessageParser<BWPassword> _parser = new pb::MessageParser<BWPassword>(() => new BWPassword());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<BWPassword> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tokens.ClientReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BWPassword() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BWPassword(BWPassword other) : this() {
      value_ = other.value_;
      resolved_ = other.resolved_;
      status_ = other.status_;
      euid_ = other.euid_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BWPassword Clone() {
      return new BWPassword(this);
    }

    /// <summary>Field number for the "value" field.</summary>
    public const int ValueFieldNumber = 1;
    private string value_ = "";
    /// <summary>
    /// the original password
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Value {
      get { return value_; }
      set {
        value_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "resolved" field.</summary>
    public const int ResolvedFieldNumber = 2;
    private ulong resolved_;
    /// <summary>
    /// time stamp for when it was resolved
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong Resolved {
      get { return resolved_; }
      set {
        resolved_ = value;
      }
    }

    /// <summary>Field number for the "status" field.</summary>
    public const int StatusFieldNumber = 3;
    private global::Tokens.BWStatus status_ = global::Tokens.BWStatus.Good;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Tokens.BWStatus Status {
      get { return status_; }
      set {
        status_ = value;
      }
    }

    /// <summary>Field number for the "euid" field.</summary>
    public const int EuidFieldNumber = 4;
    private pb::ByteString euid_ = pb::ByteString.Empty;
    /// <summary>
    /// if breached this is empty, else this is the value returned by keeperapp after submission
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Euid {
      get { return euid_; }
      set {
        euid_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as BWPassword);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(BWPassword other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Value != other.Value) return false;
      if (Resolved != other.Resolved) return false;
      if (Status != other.Status) return false;
      if (Euid != other.Euid) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Value.Length != 0) hash ^= Value.GetHashCode();
      if (Resolved != 0UL) hash ^= Resolved.GetHashCode();
      if (Status != global::Tokens.BWStatus.Good) hash ^= Status.GetHashCode();
      if (Euid.Length != 0) hash ^= Euid.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Value.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Value);
      }
      if (Resolved != 0UL) {
        output.WriteRawTag(16);
        output.WriteUInt64(Resolved);
      }
      if (Status != global::Tokens.BWStatus.Good) {
        output.WriteRawTag(24);
        output.WriteEnum((int) Status);
      }
      if (Euid.Length != 0) {
        output.WriteRawTag(34);
        output.WriteBytes(Euid);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Value.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Value);
      }
      if (Resolved != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Resolved);
      }
      if (Status != global::Tokens.BWStatus.Good) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Status);
      }
      if (Euid.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Euid);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(BWPassword other) {
      if (other == null) {
        return;
      }
      if (other.Value.Length != 0) {
        Value = other.Value;
      }
      if (other.Resolved != 0UL) {
        Resolved = other.Resolved;
      }
      if (other.Status != global::Tokens.BWStatus.Good) {
        Status = other.Status;
      }
      if (other.Euid.Length != 0) {
        Euid = other.Euid;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Value = input.ReadString();
            break;
          }
          case 16: {
            Resolved = input.ReadUInt64();
            break;
          }
          case 24: {
            Status = (global::Tokens.BWStatus) input.ReadEnum();
            break;
          }
          case 34: {
            Euid = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
