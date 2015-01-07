namespace LauncherFs

open Newtonsoft.Json
open Newtonsoft.Json.Converters
open System.Runtime.Serialization
[<DataContract>]
type ItemStatus =
      // Item is Enabled
    | [<EnumMember>][<JsonProperty("enabled")>] Enabled = 0
      // Item is Disabled
    | [<EnumMember>][<JsonProperty("disabled")>] Disabled = 1
      // Item is bypassed. It behaves as thought it runs but it doesn't
    | [<EnumMember>][<JsonProperty("bypass")>] Bypass = 2
    