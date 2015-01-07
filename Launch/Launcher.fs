namespace LauncherFs

type ProcessItem (item: LaunchItem) =
    static member runAll haltAll items =
        items
        |> List.map (fun i -> new ProcessItem (i))
        |> List.map (fun i -> i.Start haltAll)
        |> List.forall (fun b -> b)

    member private this.LaunchInstance (haltAll: unit -> int) (instanceNum: int) =
        let workingDirectory =
            match item.StartProcess.WorkingDirectory |> System.String.IsNullOrEmpty with
            | true ->
                System.AppDomain.CurrentDomain.BaseDirectory
            | false ->
                item.StartProcess.WorkingDirectory
        let psi = new System.Diagnostics.ProcessStartInfo(
                    CreateNoWindow=true,
                    WindowStyle=System.Diagnostics.ProcessWindowStyle.Hidden,
                    UseShellExecute=false,
                    RedirectStandardOutput=true,
                    WorkingDirectory=workingDirectory,
                    FileName=item.StartProcess.ExecutablePath,
                    Arguments=(item.StartProcess.Arguments |> String.concat " ")
                  )
        let startTime = System.DateTime.Now
        let proc = System.Diagnostics.Process.Start (psi)
        proc.Exited.Add (fun _ ->
                                match item.StopConfig.OnStopAction with
                                | StopAction.Halt ->
                                    //Halt everything
                                    haltAll () |> ignore
                                    ()
                                | StopAction.Relaunch ->
                                    let threshold = item.StopConfig.RelaunchTimeThreshold
                                    let relaunch =
                                        (((System.DateTime.Now - startTime).TotalMilliseconds |> System.Math.Round) |> int) >= threshold
                                    match relaunch with
                                    | false ->
                                        //Halt everything
                                        haltAll () |> ignore
                                        ()
                                    | true ->
                                        this.LaunchInstance haltAll (instanceNum) |> ignore
                                | _ ->
                                    ()
                        )
        true
    
    member private this.Launch (haltAll: unit -> int) =
        let instanceAmount =
            match item.InstanceConfig with
            | InstancesConfig.PerCpu ->
                System.Environment.ProcessorCount
            | InstancesConfig.Fixed ->
                item.FixedInstanceAmount
            | InstancesConfig.SingleInstance | _ ->
                1
        let launch = this.LaunchInstance haltAll
        seq { 1 .. instanceAmount}
        |> Seq.map launch
        |> Seq.forall (fun b -> b)

    member internal this.Start haltAll =
        let runDependents =
            match item.Status with
            | ItemStatus.Bypass ->
                true
            | ItemStatus.Enabled ->
                this.Launch haltAll
            | _ ->
                false
        match runDependents with
        | false ->
            true
        | true ->
            item.Dependents
            |> ProcessItem.runAll haltAll


type Launcher (config: string) =
    let configJson = System.IO.File.ReadAllText config
    let app = Newtonsoft.Json.JsonConvert.DeserializeObject<LauncherFs.Application>(configJson)
    member public this.Start () =
        match app.Items |> ProcessItem.runAll this.Stop with
        | false ->
            1 //Error code
        | true ->
            0 //All ok
    member private this.Stop () =
        0
    member public this.Install () =
        0
    member public this.UnInstall () =
        0
    member private this.Status () =
        0