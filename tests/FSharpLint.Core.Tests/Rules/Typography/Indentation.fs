module FSharpLint.Core.Tests.Rules.Typography.Indentation

open NUnit.Framework
open FSharpLint.Rules

[<TestFixture>]
type TestTypographyIndentation() =
    inherit TestIndentationRuleBase.TestIndentationRuleBase(Indentation.rule { numberOfIndentationSpaces = 4 })
    
    [<Test>]
    member this.``Error for incorrect indentation``() =
        this.Parse """
module P

let x =
  x"""

        Assert.IsTrue(this.ErrorExistsAt(5, 0))

    [<Test>]
    member this.``No error for correct indentation``() =
        this.Parse """
module P

let x =
    x"""

        Assert.IsTrue(this.NoErrorsExist)

    [<Test>]
    member this.``Error for incorrect record field indentation``() =
        this.Parse """
module P

let rainbow =
    { X = "X"
          Y = "Y"}"""

        Assert.IsTrue(this.ErrorExistsAt(6, 0))

    [<Test>]
    member this.``No error for correct record field indentation``() =
        this.Parse """
module P

let rainbow =
    { X = "X"
      Y = "Y"}"""

        Assert.IsTrue(this.NoErrorsExist)

    [<Test>]
    member this.``No error for correct record field indentation with multiple fields per line``() =
        this.Parse """
module P

let rainbow =
    { X = "X"; Z = "Z"
      Y = "Y"; W = "W"}"""

        Assert.IsTrue(this.NoErrorsExist)

    [<Test>]
    member this.``No error for correct multiline record field indentation``() =
        this.Parse """
module P

let rainbow =
    { X =
        (fun x ->
            x + 1)
      Y = "Y"}"""

        Assert.IsTrue(this.NoErrorsExist)

    [<Test>]
    member this.``No error for correct array member indentation``() =
        this.Parse """
module P

let pascalsTriangle =
    [| 1
       2
       3
    |]"""

        Assert.IsTrue(this.NoErrorsExist)

    [<Test>]
    member this.``No error for correct list member indentation``() =
        this.Parse """
module P

let pascalsTriangle =
    [ 1
      2
      3
    ]"""

        Assert.IsTrue(this.NoErrorsExist)

    [<Test>]
    member this.``No error for exceptional pipeline indentation``() =
        this.Parse """
module P

let res = 1
          |> add 2
          |> sub 3"""

        Assert.IsTrue(this.NoErrorsExist)

    [<Test>]
    member this.``No error for standard pipeline indentation``() =
        this.Parse """
module P

let res =
    1
    |> add 2
    |> sub 3"""

        Assert.IsTrue(this.NoErrorsExist)

    [<Test>]
    member this.``No error for exceptional multiline pipeline indentation``() =
        this.Parse """
module P

let res = 1
          |> (fun x ->
              x + 1)
          |> (fun x ->
              x - 3)"""

        Assert.IsTrue(this.NoErrorsExist)

    [<Test>]
    member this.``No error for exceptional object expression indentation``() =
        this.Parse """
module P

let comparer =
    { new IComparer<string> with
          member x.Compare(s1, s2) =
              let rev (s : String) =
                  new String (Array.rev (s.ToCharArray()))
              let reversed = rev s1 i
              reversed.CompareTo (rev s2) }"""

        Assert.IsTrue(this.NoErrorsExist)   