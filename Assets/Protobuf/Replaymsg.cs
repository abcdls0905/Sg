// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: replaymsg.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Replaypb {

  /// <summary>Holder for reflection information generated from replaymsg.proto</summary>
  public static partial class ReplaymsgReflection {

    #region Descriptor
    /// <summary>File descriptor for replaymsg.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ReplaymsgReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg9yZXBsYXltc2cucHJvdG8SCHJlcGxheXBiIh8KClJlcGxheUluZm8SEQoJ",
            "ZnJhbWVpbmZvGAEgAygMYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Replaypb.ReplayInfo), global::Replaypb.ReplayInfo.Parser, new[]{ "Frameinfo" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ReplayInfo : pb::IMessage<ReplayInfo> {
    private static readonly pb::MessageParser<ReplayInfo> _parser = new pb::MessageParser<ReplayInfo>(() => pb.ProtobufManager.New<ReplayInfo>());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ReplayInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Replaypb.ReplaymsgReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ReplayInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearData() {
      frameinfo_.Clear();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ReplayInfo(ReplayInfo other) : this() {
      frameinfo_ = other.frameinfo_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ReplayInfo Clone() {
      return new ReplayInfo(this);
    }

    /// <summary>Field number for the "frameinfo" field.</summary>
    public const int FrameinfoFieldNumber = 1;
    private static readonly pb::FieldCodec<pb::ByteString> _repeated_frameinfo_codec
        = pb::FieldCodec.ForBytes(10);
    private readonly pbc::RepeatedField<pb::ByteString> frameinfo_ = new pbc::RepeatedField<pb::ByteString>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<pb::ByteString> Frameinfo {
      get { return frameinfo_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ReplayInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ReplayInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!frameinfo_.Equals(other.frameinfo_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= frameinfo_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      frameinfo_.WriteTo(output, _repeated_frameinfo_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += frameinfo_.CalculateSize(_repeated_frameinfo_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ReplayInfo other) {
      if (other == null) {
        return;
      }
      frameinfo_.Add(other.frameinfo_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            frameinfo_.AddEntriesFrom(input, _repeated_frameinfo_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code