namespace LauncherFs

open Newtonsoft.Json
open Newtonsoft.Json.Converters
open System.Runtime.Serialization

[<DataContract>]
[<AllowNullLiteral>]
type Application() =
  /// <summary>Application name</summary>
  [<DataMember>][<JsonProperty("name")>] member val Name: string = null with get, set
  /// <summary>Application name</summary>
  [<DataMember>][<JsonProperty("serviceName")>] member val ServiceName: string = null with get, set
  /// <summary>Processes that should execute</summary>
  [<DataMember>][<JsonProperty("items")>] member val Items: LauncherFs.LaunchItem list = [] with get, set
