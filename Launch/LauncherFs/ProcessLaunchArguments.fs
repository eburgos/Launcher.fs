namespace LauncherFs

open Newtonsoft.Json
open Newtonsoft.Json.Converters
open System.Runtime.Serialization

[<DataContract>]
[<AllowNullLiteral>]
type ProcessLaunchArguments() =
  /// <summary>Description</summary>
  [<DataMember>][<JsonProperty("description")>] member val Description: string = null with get, set
  /// <summary>Working directory</summary>
  [<DataMember>][<JsonProperty("workingDirectory")>] member val WorkingDirectory: string = null with get, set
  /// <summary>Path to the executable to run</summary>
  [<DataMember>][<JsonProperty("executablePath")>] member val ExecutablePath: string = null with get, set
  /// <summary>Process arguments</summary>
  [<DataMember>][<JsonProperty("arguments")>] member val Arguments: string list = [] with get, set
