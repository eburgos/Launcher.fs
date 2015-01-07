open LauncherFs

[<EntryPoint>]
let main argv =
    let defaultConfig = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Launch.json")
    match argv |> List.ofArray with
    | ["start"] ->
        (new Launcher(defaultConfig)).Start ()
    | ["start" ; p] ->
        (new Launcher (p)).Start ()
    | ["install"] ->
        (new Launcher (defaultConfig)).Install ()
    | ["uninstall"] ->
        (new Launcher (defaultConfig)).UnInstall ()
    | illegalArgs ->
        (illegalArgs |> String.concat " ")
        |> printfn "Illegal arguments: %s"
        1

