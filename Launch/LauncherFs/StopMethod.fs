namespace LauncherFs

open Newtonsoft.Json
open Newtonsoft.Json.Converters
open System.Runtime.Serialization
[<DataContract>]
type StopMethod =
      // Just kill the process
    | [<EnumMember>][<JsonProperty("kill")>] Kill = 0
      // Run a command to stop it
    | [<EnumMember>][<JsonProperty("command")>] Command = 1
    