using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.CodeDomTest
{

//    异常处理语句，就是常说的try...catch语句，有时候，也会带有finally子句。要生成异常处理语句，得用到CodeTryCatchFinallyStatement类，它包含三个部分。

//1、TryStatements：尝试执行的代码块。

//2、CatchClauses：捕捉异常的代码块。CatchClauses是一个子句集合，因为一个try语句可以包含N个catch子句，而每个catch块都由CodeCatchClause类来表示，使用时应提供要捕捉的异常的类型，异常对象的临时变量名，以及catch块的语句集合。

//3、FinallyStatements：finally语句块，不管会不会发生异常，finally中的语句会执行。
    class CodeDom_try
    {

        static void Test_01()
        {
            CodeTryCatchFinallyStatement codeTryCatch = new CodeTryCatchFinallyStatement();
            codeTryCatch.TryStatements.Add(new CodeCommentStatement("try执行语句"));

            CodeCatchClause codeCatch = new CodeCatchClause();
            codeCatch.CatchExceptionType = new CodeTypeReference(typeof(Exception));
            codeCatch.LocalName = "ex";

            codeCatch.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Console)), nameof(Console.WriteLine)),new CodePropertyReferenceExpression(new CodeVariableReferenceExpression(codeCatch.LocalName),nameof(Exception.Message))));

            codeTryCatch.CatchClauses.Add(codeCatch);

            codeTryCatch.FinallyStatements.Add(new CodeCommentStatement("finally 执行..."));

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            provider.GenerateCodeFromStatement(codeTryCatch,Console.Out,new CodeGeneratorOptions() { BracingStyle="C"});
        }

        /// <summary>
        /// 多个Catch
        /// </summary>
        static void Test_02()
        {
            CodeTryCatchFinallyStatement codeTryCatch = new CodeTryCatchFinallyStatement();
            codeTryCatch.TryStatements.Add(new CodeCommentStatement("待执行的代码"));

            CodeCatchClause codeCatch_1 = new CodeCatchClause();
            codeCatch_1.CatchExceptionType = new CodeTypeReference(typeof(FieldAccessException));
            codeCatch_1.LocalName = "ex1";
            codeCatch_1.Statements.Add(new CodeCommentStatement("捕获异常1"));


            CodeCatchClause codeCatch_2 = new CodeCatchClause();
            codeCatch_2.CatchExceptionType = new CodeTypeReference(typeof(Exception));
            codeCatch_2.LocalName = "ex2";
            codeCatch_2.Statements.Add(new CodeCommentStatement("捕获异常2"));


            codeTryCatch.CatchClauses.Add(codeCatch_1);
            codeTryCatch.CatchClauses.Add(codeCatch_2);


            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            provider.GenerateCodeFromStatement(codeTryCatch, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });

        }

        /// <summary>
        /// 抛出异常
        /// </summary>
        static void Test03()
        {
            CodeTryCatchFinallyStatement codeTryCatch = new CodeTryCatchFinallyStatement();
            codeTryCatch.TryStatements.Add(new CodeThrowExceptionStatement(new CodeObjectCreateExpression(typeof(FieldAccessException))));

            //
            CodeVariableDeclarationStatement codeEx = new CodeVariableDeclarationStatement();
            codeEx.Type = new CodeTypeReference(typeof(Exception));
            codeEx.InitExpression = new CodeObjectCreateExpression(codeEx.Type, new CodePrimitiveExpression("测试异常"));
            codeEx.Name = "ex2";


            CodeThrowExceptionStatement codeThrowEx = new CodeThrowExceptionStatement(new CodeVariableReferenceExpression(codeEx.Name));

            codeTryCatch.TryStatements.Add(codeEx);
            codeTryCatch.TryStatements.Add(codeThrowEx);


            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            provider.GenerateCodeFromStatement(codeTryCatch, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });
        }
        public static void Test()
        {
            //Exception ex = new();

            //Test_01();
            //Test_02();
            Test03();
        }
    }
}
