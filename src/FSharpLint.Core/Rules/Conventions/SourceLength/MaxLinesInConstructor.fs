module FSharpLint.Rules.MaxLinesInConstructor

open FSharp.Compiler.Ast
open FSharpLint.Framework.Ast
open FSharpLint.Framework.AstInfo
open FSharpLint.Framework.Rules

let runner (config:Helper.SourceLength.Config) (args:AstNodeRuleParams) =
    match args.astNode with
    | AstNode.Binding(SynBinding.Binding(_, _, _, _, _, _, valData, _, _, _, _, _) as binding) ->
       match identifierTypeFromValData valData with
       | Constructor -> Helper.SourceLength.checkSourceLengthRule config binding.RangeOfBindingAndRhs "Constructor"
       | _ -> Array.empty
    | _ -> Array.empty

let rule config =
    { name = "MaxLinesInConstructor"
      identifier = Identifiers.MaxLinesInConstructor
      ruleConfig = { AstNodeRuleConfig.runner = runner config; cleanup = ignore } }
    |> AstNodeRule