namespace LauncherFs

open Newtonsoft.Json
open Newtonsoft.Json.Converters
open System.Runtime.Serialization
[<DataContract>]
type InstancesConfig =
      // Only one instance of this process will run
    | [<EnumMember>][<JsonProperty("singleInstance")>] SingleInstance = 0
      // One instance per cpu will run
    | [<EnumMember>][<JsonProperty("perCpu")>] PerCpu = 1
      // A fixed amount of instances will run. This will be specified in the "fixedInstanceAmount" field
    | [<EnumMember>][<JsonProperty("fixed")>] Fixed = 2
    