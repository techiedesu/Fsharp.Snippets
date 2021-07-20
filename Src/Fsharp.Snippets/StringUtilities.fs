module Fsharp.Snippets.StringUtilities

    open System
    open System.Text
    open NUnit.Framework

    let camelCaseToUnderscore (str: String) =
            match str with
            | null -> null
            | _ ->
                let mutable firstCall = true

                let processChar char =
                    if firstCall || Char.IsLower char then
                        firstCall <- false
                        Char.ToString(Char.ToLower char)
                    else
                        $"_{Char.ToLower char}"

                let sb = StringBuilder()
                str |> String.iter (fun c -> processChar c |> sb.Append |> ignore)

                sb.ToString()

    let tryCamelCaseToUnderscore (str: String) =
        match camelCaseToUnderscore str with
        | null -> None
        | x -> Some x
        
    [<TestCase("CamelCase", "camel_case")>]
    [<TestCase("camelCase", "camel_case")>]
    [<TestCase("camelCAse", "camel_c_ase")>]
    [<TestCase("", "")>]
    [<TestCase(null, null)>] // Throwing exception isn't the best way
    let ``convert CamelCase to under_score`` (source: String) (expected: String) =
        let result = camelCaseToUnderscore source
        Assert.AreEqual (expected, result)