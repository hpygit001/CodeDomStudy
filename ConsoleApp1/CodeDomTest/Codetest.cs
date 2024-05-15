using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.CodeDomTest
{

    /// <summary>
    /// CodeDom 作用：
    /// 1. 生成代码文档。这个听起来很玄？不玄，就是咱们在VS里常常耍的代码生成，比如你添加了一个服务引用，VS会帮你生成一个客户端代理类。
    /// 2.动态编译程序集。这个也好懂，就是动态编译。
    /// </summary>
    class Codetest
    {

      
        public static void Test()
        {

            //Test_02();
            //Test_03();
            //Test_04();
            //Test_05();
            //Test_06();
            //Test_07();
            //Test_08();

            //Test_09();
            //Test_10();


            //Test_11();

            //Test_12();
            //Test_13();
            CodeTest_Type.Test();
        }
        /// <summary>
        /// 生成一个dog类
        /// </summary>
        public static void Test_01()
        {
            CodeTypeDeclaration dcl = new CodeTypeDeclaration("Dog");
            dcl.IsStruct = true;

            dcl.Attributes = MemberAttributes.Public;

            //生成C#代码
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成C#代码：");

            provider.GenerateCodeFromType(dcl, Console.Out, null);

            //生成VB代码
            provider = CodeDomProvider.CreateProvider("VB");
            Console.WriteLine("\n生成VB代码：");
            provider.GenerateCodeFromType(dcl, Console.Out, null);

            ////生成C++代码
            //provider = CodeDomProvider.CreateProvider("cpp");
            //Console.WriteLine("\n生成C++代码：");

            //provider.GenerateCodeFromType(dcl, Console.Out, null);
        }

        public static void Test_02()
        {
            CodeVariableReferenceExpression left = new CodeVariableReferenceExpression("a");
            CodeVariableReferenceExpression right = new CodeVariableReferenceExpression("b");
            CodeBinaryOperatorExpression opt = new CodeBinaryOperatorExpression();
            opt.Operator = CodeBinaryOperatorType.Add;
            opt.Left = left;
            opt.Right = right;

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成C#代码：");
            provider.GenerateCodeFromExpression(opt, Console.Out, null);


        }


        public static void Test_03()
        {
            CodeThisReferenceExpression thisexpr = new CodeThisReferenceExpression();
            CodeFieldReferenceExpression fexp = new CodeFieldReferenceExpression();
            fexp.FieldName = "m_val";
            fexp.TargetObject = thisexpr;
            CodeBinaryOperatorExpression opt = new CodeBinaryOperatorExpression();
            opt.Operator = CodeBinaryOperatorType.Assign;
            opt.Left = fexp;
            opt.Right = new CodePrimitiveExpression((int)500);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成C#代码");
            provider.GenerateCodeFromExpression(opt, Console.Out, null);
        }


        public static void Test_04()
        {
            CodeTypeOfExpression texp = new CodeTypeOfExpression(typeof(string));
           
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成C#代码");

            provider.GenerateCodeFromExpression(texp, Console.Out, null);
        }

        public static void Test_05()
        {
            CodeVariableReferenceExpression exp = new CodeVariableReferenceExpression();
            exp.VariableName = "x";
            CodeTypeReference codeType = new CodeTypeReference(typeof(decimal));
            CodeCastExpression codeCast = new CodeCastExpression();
            codeCast.TargetType = codeType;
            codeCast.Expression = exp;

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成C#代码");
            provider.GenerateCodeFromExpression(codeCast,Console.Out,null);
        }

        public static void Test_06()
        {
            CodeVariableReferenceExpression left = new CodeVariableReferenceExpression("f");
            CodeBinaryOperatorExpression opt = new CodeBinaryOperatorExpression();
            opt.Left = left;
            opt.Operator = CodeBinaryOperatorType.Assign;
            opt.Right = new CodePrimitiveExpression("so hot");

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成C#代码");
            provider.GenerateCodeFromExpression(opt, Console.Out, null);
        }


        /// <summary>
        /// 变量声明，然后赋值；int n; n=999;
        /// </summary>
        public static void Test_07()
        {
            CodeVariableDeclarationStatement decl = new CodeVariableDeclarationStatement(typeof(int), "n");
            CodeAssignStatement ass = new CodeAssignStatement();
            ass.Left = new CodeVariableReferenceExpression("n");
            ass.Right = new CodePrimitiveExpression((int)98900);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成C#代码：");

            provider.GenerateCodeFromStatement(decl, Console.Out, null);
            provider.GenerateCodeFromStatement(ass, Console.Out, null);


        }

        /// <summary>
        /// 创建变量时进行初始化赋值
        /// </summary>
        public static void Test_08()
        {
            CodeVariableDeclarationStatement decl = new CodeVariableDeclarationStatement(typeof(int), "n", new CodePrimitiveExpression((int)9800));
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成C#代码：");

            provider.GenerateCodeFromStatement(decl, Console.Out, null);
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        public static void Test_09()
        {
            CodeNamespace codeNamespace = new CodeNamespace("Motion");
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            compileUnit.Namespaces.Add(codeNamespace);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成C#代码：");
            provider.GenerateCodeFromCompileUnit(compileUnit,Console.Out,new CodeGeneratorOptions() { BracingStyle="C"});
        }


        /// <summary>
        /// 命名空间、类
        /// </summary>
        public static void Test_10()
        {
            CodeNamespace codeNamespace = new CodeNamespace("Motion");

           
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            compileUnit.Namespaces.Add(codeNamespace);

            CodeTypeDeclaration codeType = new CodeTypeDeclaration("Axis");
            codeType.IsClass = true;
            codeType.Attributes = MemberAttributes.Public;
            codeNamespace.Types.Add(codeType);

            
            


            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成C#代码：");
            provider.GenerateCodeFromCompileUnit(compileUnit, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });
        }


        /// <summary>
        /// 多个命名空间
        /// </summary>
        public static void Test_11()
        {
            CodeNamespace codeNamespace = new CodeNamespace("Motion");
            CodeNamespace codeNamespace2 = new CodeNamespace("Motion.AA");


            CodeCompileUnit compileUnit = new CodeCompileUnit();
            compileUnit.Namespaces.Add(codeNamespace);
            compileUnit.Namespaces.Add(codeNamespace2);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成的C#代码：");
            provider.GenerateCodeFromCompileUnit(compileUnit, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });

        }


        /// <summary>
        /// 引用命名空间
        /// </summary>
        public static void Test_12()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            CodeNamespace codeNamespace = new CodeNamespace("Motion");
            compileUnit.Namespaces.Add(codeNamespace);
            
            codeNamespace.Imports.Add(new CodeNamespaceImport("AA"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("Dispenser"));

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成的C#代码：");
            provider.GenerateCodeFromCompileUnit(compileUnit, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });

        }

        /// <summary>
        /// 引用程序集，
        /// </summary>
        public static void Test_13()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            compileUnit.ReferencedAssemblies.Add("System");
            compileUnit.ReferencedAssemblies.Add("AA");
            CodeNamespace codeNamespace = new CodeNamespace("Motion");
            compileUnit.Namespaces.Add(codeNamespace);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成的C#代码：");
            provider.GenerateCodeFromCompileUnit(compileUnit, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });


        }
    }

    /// <summary>
    /// 定义类型
    /// </summary>
    class CodeTest_Type
    {
        internal static void Test()
        {
            //Test_01();
            //Test_02();
            //Test_03();

            //Test_04();
            //Test_05();
            //Test_06();

            //Test_07();

            //Test_08();
            //Test_08();
            //Test_09();
            //Test_10();
            Test_11();
            //Test_12();
        }
        public static void Test_01()
        {
            //编译单元
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            //命名空间
            CodeNamespace codeNamespace = new CodeNamespace("Motion");
            compileUnit.Namespaces.Add(codeNamespace);

            //定义一个类型
            CodeTypeDeclaration codeType = new CodeTypeDeclaration();
            codeType.Name = "AxisBase";
            codeType.IsClass = true;//默认True,可以不设置
            codeType.TypeAttributes = System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Sealed;

            codeNamespace.Types.Add(codeType);

            CodeTypeDeclaration codeType_point = new CodeTypeDeclaration();
            codeType_point.Name = "Point";
            codeType_point.IsStruct = true;

            codeNamespace.Types.Add(codeType_point);

            CodeTypeDeclaration codeType_homeMode = new CodeTypeDeclaration();
            codeType_homeMode.Name = "E_HomeMode";
            codeType_homeMode.IsEnum = true;

            codeNamespace.Types.Add(codeType_homeMode);

            //
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");

            Console.WriteLine("生成的C#代码：");
            //provider.GenerateCodeFromCompileUnit(compileUnit, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });
            provider.GenerateCodeFromNamespace(codeNamespace, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });
        }

       

        /// <summary>
        /// 定义委托类型
        /// </summary>
        public static void Test_02()
        {
           
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            CodeNamespace codeNamespace = new CodeNamespace("Motion");
            compileUnit.Namespaces.Add(codeNamespace);

            CodeTypeDelegate typeDelegateAction = new CodeTypeDelegate();
            typeDelegateAction.Name = "onAdded";
            typeDelegateAction.Parameters.Add(new CodeParameterDeclarationExpression() { Name="X",Type=new CodeTypeReference(typeof(int))});
            typeDelegateAction.Parameters.Add(new CodeParameterDeclarationExpression() { Name = "Y", Type = new CodeTypeReference(typeof(int)) });

            typeDelegateAction.ReturnType = new CodeTypeReference(typeof(int));

            codeNamespace.Types.Add(typeDelegateAction);


            CodeTypeDelegate typeDelegate_DoWork = new CodeTypeDelegate();
            typeDelegate_DoWork.Name = "DoWork";
            typeDelegate_DoWork.ReturnType = new CodeTypeReference(typeof(void));

            codeNamespace.Types.Add(typeDelegate_DoWork);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成的C#：");

            provider.GenerateCodeFromNamespace(codeNamespace, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });

        }


        /// <summary>
        /// 类-继承
        /// </summary>
        public static void Test_03()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            ////using System;
            ////using System.CodeDom;
            ////using System.CodeDom.Compiler;
            ////using System.Collections.Generic;
            ////using System.Linq;
            ////using System.Text;
            ////using System.Threading.Tasks;
            //compileUnit.ReferencedAssemblies.Add("System");
            //compileUnit.ReferencedAssemblies.Add("System.Linq");
            //compileUnit.ReferencedAssemblies.Add("System.Text");


            CodeNamespace codeNamespace = new CodeNamespace();
            codeNamespace.Name ="Motion";

            compileUnit.Namespaces.Add(codeNamespace);


            CodeTypeDeclaration codeType_A = new CodeTypeDeclaration();
            codeType_A.Name = "A";
            CodeTypeDeclaration codeType_B = new CodeTypeDeclaration();
            codeType_B.Name = "B";

            codeType_B.BaseTypes.Add(new CodeTypeReference(codeType_A.Name));
            codeType_B.BaseTypes.Add(new CodeTypeReference(typeof(string)));
            codeNamespace.Types.AddRange(new CodeTypeDeclaration[] { codeType_A, codeType_B });


            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成的C#代码：");
            provider.GenerateCodeFromCompileUnit(compileUnit, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });

            //编译测试
            CompilerParameters compiler = new CompilerParameters();
            compiler.OutputAssembly = "TestAB.dll";
            CompilerResults results = provider.CompileAssemblyFromDom(compiler, compileUnit);

            if(results.Errors.Count==0)
            {
                Console.WriteLine("编译完成。");
                Console.WriteLine($"生成的程序集：{results.CompiledAssembly.Location}");
            }
            else
            {
                foreach (CompilerError item in results.Errors)
                {
                    Console.WriteLine($"{item.ErrorNumber} -- {item.ErrorText}");
                }
            }

            

        }


        /// <summary>
        /// 接口-实现
        /// </summary>
        public static void Test_04()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            CodeNamespace codeNamespace = new CodeNamespace("Motion");
            compileUnit.Namespaces.Add(codeNamespace);


            CodeTypeDeclaration codeType_IMov = new CodeTypeDeclaration();
            codeType_IMov.Name = "IMov";
            codeType_IMov.IsInterface = true;

            CodeTypeDeclaration codeType_ICheckPos = new CodeTypeDeclaration("ICheckPos");
            codeType_ICheckPos.IsInterface = true;


            CodeTypeDeclaration codeType_Axis = new CodeTypeDeclaration();
            codeType_Axis.Name = "AxisBase";

            codeType_Axis.BaseTypes.Add(new CodeTypeReference(codeType_IMov.Name));
            codeType_Axis.BaseTypes.Add(new CodeTypeReference(codeType_ICheckPos.Name));



            codeNamespace.Types.Add(codeType_IMov);
            codeNamespace.Types.Add(codeType_ICheckPos);

            codeNamespace.Types.Add(codeType_Axis);


            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成C#代码：");
            provider.GenerateCodeFromCompileUnit(compileUnit, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });


            CompilerParameters compilerParameters = new CompilerParameters();
            compilerParameters.OutputAssembly = "TestMotion.dll";

            //compilerParameters.ReferencedAssemblies.Add("System.dll");

            CompilerResults results = provider.CompileAssemblyFromDom(compilerParameters, compileUnit);

           
            
            if (results.Errors.Count==0)
            {
                Console.WriteLine("编译完成");
                Console.WriteLine($"生成的程序集：{results.CompiledAssembly.Location}");
            }
            else
            {
                foreach (CompilerError item in results.Errors)
                {
                    Console.WriteLine($"{item.ErrorNumber} : {item.ErrorText}");
                }
            }

        }

        public int Age { get; set; }
        /// <summary>
        /// 类成员定义
        /// </summary>
        public static void Test_05()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
           
            CodeNamespace ns = new CodeNamespace("Sample");
            ns.Imports.Add(new CodeNamespaceImport(nameof(System)));

            compileUnit.Namespaces.Add(ns);

            

             CodeTypeDeclaration codeType = new CodeTypeDeclaration();
            codeType.Name = "Person";
            ns.Types.Add(codeType);
            //创建一个构造函数，含义一个参数Name
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            CodeParameterDeclarationExpression p1 = new CodeParameterDeclarationExpression();
            p1.Name = "Name";
            p1.Type = new CodeTypeReference(typeof(string));
            constructor.Parameters.Add(p1);

            codeType.Members.Add(constructor);

            //
            CodeMemberProperty codeMember_Age = new CodeMemberProperty();
            codeMember_Age.Name = "Age";
            codeMember_Age.Type = new CodeTypeReference(typeof(int));
            codeMember_Age.Attributes = MemberAttributes.Final | MemberAttributes.Public;
           
            codeMember_Age.HasGet = true;
            codeMember_Age.HasSet = true;
           
            codeType.Members.Add(codeMember_Age);



            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            Console.WriteLine("生成的C#代码：");
            provider.GenerateCodeFromCompileUnit(compileUnit, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });



        }


        /// <summary>
        /// 创建一个类，在构造函数里赋值
        /// </summary>
        public static void Test_06()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
           
            CodeNamespace codeNamespace = new CodeNamespace("Data");
            codeNamespace.Imports.Add(new CodeNamespaceImport(nameof(System)));
            compileUnit.Namespaces.Add(codeNamespace);

            CodeTypeDeclaration codeTypeDemo = new CodeTypeDeclaration();
            codeNamespace.Types.Add(codeTypeDemo);
            codeTypeDemo.Name = "Demo";
            codeTypeDemo.Attributes = MemberAttributes.Public;

            CodeMemberField memberField = new CodeMemberField();
            memberField.Attributes = MemberAttributes.Private;
            memberField.Name = "ma";
            memberField.Type = new CodeTypeReference(typeof(string));
            codeTypeDemo.Members.Add(memberField);

            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            codeTypeDemo.Members.Add(constructor);

         


            //创建参数
            CodeParameterDeclarationExpression p1 = new CodeParameterDeclarationExpression();
            p1.Name = "ma";
            p1.Type = new CodeTypeReference(typeof(string));

            constructor.Parameters.Add(p1);



            CodeAssignStatement assignStatement = new CodeAssignStatement();
            assignStatement.Left = new CodeFieldReferenceExpression() {  FieldName= memberField.Name,TargetObject=new CodeThisReferenceExpression()};
            assignStatement.Right = new CodeVariableReferenceExpression() { VariableName = p1.Name };

            
            constructor.Statements.Add(assignStatement);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");

            Console.WriteLine("生成的C#代码：");

            provider.GenerateCodeFromCompileUnit(compileUnit, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });


        }

        /// <summary>
        /// 生成方法
        /// </summary>
        public static void Test_07()
        {

            CodeMemberMethod memberMethod = new CodeMemberMethod();

            memberMethod.Name = "MovAbs";

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");

            provider.GenerateCodeFromMember(memberMethod, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });

        }

        public   delegate void OnAdded(int a, int b);

        public static event System.EventHandler Click;

        /// <summary>
        /// 生成一个类，添加一个方法Add（int a,int b）
        /// </summary>
        public static void Test_08()
        {
            CodeTypeDeclaration codeType = new CodeTypeDeclaration("Calc");


            CodeMemberMethod method_Add = new CodeMemberMethod();
            method_Add.Name = "Add";
            method_Add.Attributes = MemberAttributes.Public|MemberAttributes.Static;

            method_Add.ReturnType = new CodeTypeReference(typeof(int));

            CodeParameterDeclarationExpression p1 = new CodeParameterDeclarationExpression();
            p1.Name = "a";
            p1.Type = new CodeTypeReference(typeof(int));
            CodeParameterDeclarationExpression p2 = new CodeParameterDeclarationExpression();
            p2.Name = "b";
            p2.Type = new CodeTypeReference(typeof(int));

            method_Add.Parameters.Add(p1);
            method_Add.Parameters.Add(p2);

            CodeBinaryOperatorExpression opt = new CodeBinaryOperatorExpression();
            opt.Operator = CodeBinaryOperatorType.Add;
            opt.Left = new CodeVariableReferenceExpression(p1.Name);
            opt.Right = new CodeVariableReferenceExpression(p2.Name);


            CodeMethodReturnStatement returnStatement = new CodeMethodReturnStatement();

            returnStatement.Expression = opt;

            method_Add.Statements.Add(returnStatement);

            codeType.Members.Add(method_Add);


            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");

            provider.GenerateCodeFromType(codeType, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });

        }

        /// <summary>
        /// 生成一个Program 类，main方法
        /// </summary>
        public static void Test_09()
        {
            CodeTypeDeclaration codeType = new CodeTypeDeclaration();
            codeType.Name = "Program";

            //入口方法
            CodeEntryPointMethod main = new CodeEntryPointMethod();

            codeType.Members.Add(main);

            //
            CodeMethodReferenceExpression expression = new CodeMethodReferenceExpression();
            expression.TargetObject = new CodeTypeReferenceExpression(typeof(Console));
            expression.MethodName = nameof(Console.WriteLine);

            CodeMethodInvokeExpression invokeExpression = new CodeMethodInvokeExpression();
            invokeExpression.Method = expression;
            invokeExpression.Parameters.Add(new CodePrimitiveExpression("Motion ...."));

            main.Statements.Add(new CodeExpressionStatement(invokeExpression));

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            provider.GenerateCodeFromType(codeType, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });


        }


        /// <summary>
        /// 生成事件
        /// </summary>
        public static void Test_10()
        {
            CodeMemberEvent memberEvent = new CodeMemberEvent();
            memberEvent.Name = "Click";
            memberEvent.Attributes = MemberAttributes.Public;
            memberEvent.Type = new CodeTypeReference(typeof(System.EventHandler));

            //System.EventHandler<string> eventHandler;

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            provider.GenerateCodeFromMember(memberEvent, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });


        }

        /// <summary>
        ///生成  泛型事件System.EventHandler<T>
        /// </summary>
        public static void Test_11()
        {
            CodeCompileUnit unit = new CodeCompileUnit();


            CodeNamespace ns = new CodeNamespace("Sample");
            ns.Imports.Add(new CodeNamespaceImport(nameof(System)));
            unit.Namespaces.Add(ns);

            //创建一个自定义的事件参数类MyEventArgs,继承系统的EventArgs
            //新增一个私有的字段m_data;在构造参里进行赋值
            //新增一个属性访问器，返回m_data
            CodeTypeDeclaration codeType_MyEventArgs = new CodeTypeDeclaration();
            ns.Types.Add(codeType_MyEventArgs);
            codeType_MyEventArgs.Name = "MyEventArgs";
            codeType_MyEventArgs.BaseTypes.Add(new CodeTypeReference(typeof(System.EventArgs)));

            CodeMemberField field_data = new CodeMemberField();
            field_data.Name = "m_data";
            field_data.Type = new CodeTypeReference(typeof(byte));
            codeType_MyEventArgs.Members.Add(field_data);

            CodeMemberProperty property_data = new CodeMemberProperty();
            property_data.Name = "Data";
            property_data.Attributes = MemberAttributes.Public;

            property_data.HasGet = true;
            property_data.HasSet = false;
            property_data.GetStatements.Add(new CodeMethodReturnStatement() { Expression=new CodeFieldReferenceExpression(new CodeThisReferenceExpression(),field_data.Name)  });

            codeType_MyEventArgs.Members.Add(property_data);

            //构造函数，对m_data进行赋值
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            CodeParameterDeclarationExpression p1 = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(byte)), "data");
            constructor.Parameters.Add(p1);

            constructor.Statements.Add(new CodeAssignStatement() {
                Left=new CodeFieldReferenceExpression() { TargetObject=new CodeThisReferenceExpression(),FieldName= field_data.Name} ,
                Right=new CodeVariableReferenceExpression(p1.Name)
            });
            codeType_MyEventArgs.Members.Add(constructor);

            //定义一个类型，在类型里定义一个事件
            CodeTypeDeclaration codeTypeDemo = new CodeTypeDeclaration();
            codeTypeDemo.Name = "Demo";
            ns.Types.Add(codeTypeDemo);

            CodeMemberEvent memberEvent = new CodeMemberEvent();


            memberEvent.Name = "OnAdded";
            //泛型类型声明
            //<类型名字>`<泛型参数个数>[<类型列表>,……n]
            //如果类型要写上程序集、版本号之类的信息，就再套一层中括号，把类型括起来 Order`2[[System.Byte,mscorelib,Version=4.0.0.0,PublicKeyToken=xxxxxx], [System.Int32,mscorelib,Version=4.0.0.0]]
            //很简单，不用记，获取类型的Type，再看看它的FullName属性就知道了。
            memberEvent.Type = new CodeTypeReference($"{nameof(System.EventHandler)}`1[{codeType_MyEventArgs.Name}]");
            codeTypeDemo.Members.Add(memberEvent);


            CodeTypeDelegate typeDelegate = new CodeTypeDelegate();
            typeDelegate.Name = "SetPositionEventHandler";
            typeDelegate.ReturnType = new CodeTypeReference(typeof(int));
            typeDelegate.Parameters.Add(new CodeParameterDeclarationExpression() { Name = "x", Type = new CodeTypeReference(typeof(int)) });

            typeDelegate.Parameters.Add(new CodeParameterDeclarationExpression() { Name = "y", Type = new CodeTypeReference(typeof(int)) });

            ns.Types.Add(typeDelegate);

            CodeMemberEvent event_1 = new CodeMemberEvent();
            event_1.Name = "OnSetPosition";
            event_1.Type = new CodeTypeReference(typeDelegate.Name);
            codeTypeDemo.Members.Add(event_1);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");

            provider.GenerateCodeFromCompileUnit(unit, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });


            


        }



        /// <summary>
        /// 抽象类、抽象属性
        /// </summary>
        public static void Test_12()
        {
            CodeTypeDeclaration codeType = new CodeTypeDeclaration("Person");
            codeType.TypeAttributes = System.Reflection.TypeAttributes.Abstract;

            CodeMemberProperty propertyName = new CodeMemberProperty();
            propertyName.Name = "Name";
            propertyName.Type = new CodeTypeReference(typeof(string));
            
            propertyName.HasGet = true;
            propertyName.HasSet = false;
            propertyName.Attributes = MemberAttributes.Abstract|MemberAttributes.Public;

            codeType.Members.Add(propertyName);


            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            provider.GenerateCodeFromType(codeType,Console.Out,new CodeGeneratorOptions() { BracingStyle="C"});

        }

        /// <summary>
        /// 定义方法的输入、输出参数；默认的都是输入的；输出有out、ref关键字
        /// </summary>
        public static void Test_13()
        {
            CodeTypeDeclaration codeType = new CodeTypeDeclaration();
            codeType.Name = "Calc";
            //创建静态类，通过添加#Region 间接实现
            codeType.StartDirectives.Add(new CodeRegionDirective(
           CodeRegionMode.Start, Environment.NewLine+"static"));
            codeType.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, string.Empty));
            codeType.TypeAttributes = System.Reflection.TypeAttributes.NotPublic;

            CodeMemberMethod method_add = new CodeMemberMethod();
            method_add.ReturnType = new CodeTypeReference(typeof(void));
            method_add.Attributes = MemberAttributes.Static| MemberAttributes.Public;
            method_add.Parameters.Add(new CodeParameterDeclarationExpression() { Name = "a", Type = new CodeTypeReference(typeof(int)) });
            method_add.Parameters.Add(new CodeParameterDeclarationExpression() { Name = "b", Type = new CodeTypeReference(typeof(int)) });
            method_add.Parameters.Add(new CodeParameterDeclarationExpression() { Name = "c", Type = new CodeTypeReference(typeof(int)),Direction= FieldDirection.Out });
            method_add.Statements.Add(new CodeCommentStatement("加法程序"));

            codeType.Members.Add(method_add);

            CodeMemberMethod method_Swap = new CodeMemberMethod();
            method_Swap.ReturnType = new CodeTypeReference(typeof(void));
            method_Swap.Parameters.Add(new CodeParameterDeclarationExpression() { Name = "a", Type = new CodeTypeReference(typeof(int)), Direction = FieldDirection.Ref });
            method_Swap.Parameters.Add(new CodeParameterDeclarationExpression() { Name = "b", Type = new CodeTypeReference(typeof(int)), Direction = FieldDirection.Ref });
            method_Swap.Statements.Add(new CodeCommentStatement("交换程序"));

            method_Swap.Attributes = MemberAttributes.Static|MemberAttributes.Public;
            codeType.Members.Add(method_Swap);


            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");

            provider.GenerateCodeFromType(codeType, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });
        }

        /// <summary>
        /// CodeVariableReferenceExpression 适用与引用变量、也适应引用方法参数。CodeArgumentReferenceExpression 专门为方法参数设计的，使用和变量引用一样
        /// </summary>
        public static void Test_14()
        {
            CodeMemberMethod method = new CodeMemberMethod();

            CodeParameterDeclarationExpression p1 = new CodeParameterDeclarationExpression();
            p1.Name = "a";
            p1.Type = new CodeTypeReference(typeof(int));

            method.Parameters.Add(p1);

            CodeAssignStatement assignStatement = new CodeAssignStatement();
            assignStatement.Left = new CodeArgumentReferenceExpression() { ParameterName = p1.Name };
            assignStatement.Right = new CodePrimitiveExpression((int)300);
            method.Statements.Add(assignStatement);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");

            provider.GenerateCodeFromMember(method, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });

        }

        /// <summary>
        /// 索引器-数组定义
        /// CodeDom不支持多维数组
        /// new string[10]
        /// </summary>
        public static void Test_15()
        {
            CodeArrayCreateExpression codeArray = new CodeArrayCreateExpression();
            codeArray.CreateType = new CodeTypeReference(typeof(string));
            codeArray.Size = 10;

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            provider.GenerateCodeFromExpression(codeArray, Console.Out, null);


        }

        /// <summary>
        /// 数组定义-初始化 int[] a=new int[]{1,2,3}
        /// </summary>
        public static void Test_16()
        {
            CodeArrayCreateExpression codeArray = new CodeArrayCreateExpression();
            codeArray.CreateType = new CodeTypeReference(typeof(int));
            codeArray.Initializers.Add(new CodePrimitiveExpression(1));
            codeArray.Initializers.Add(new CodePrimitiveExpression(2));
            codeArray.Initializers.Add(new CodePrimitiveExpression(3));

            CodeVariableDeclarationStatement codeVariable = new CodeVariableDeclarationStatement();

            codeVariable.Type = new CodeTypeReference(typeof(int[]));
            codeVariable.Name = "a";
            codeVariable.InitExpression = codeArray;

            //访问数组元素
            CodeArrayIndexerExpression codeArrayIndexer = new CodeArrayIndexerExpression();
            codeArrayIndexer.TargetObject = new CodeVariableReferenceExpression(codeVariable.Name);
            codeArrayIndexer.Indices.Add(new CodePrimitiveExpression(0));

            CodeAssignStatement codeAssign = new CodeAssignStatement();
            codeAssign.Left = codeArrayIndexer;
            codeAssign.Right = new CodePrimitiveExpression(10);

            //表达式与赋值语句是有区别的，表达式会用括号括起来，且结束没有分号。而CodeAssignStatement 是没有括号，结束有分号
            CodeBinaryOperatorExpression codeBinaryOperator = new CodeBinaryOperatorExpression();
            codeBinaryOperator.Operator = CodeBinaryOperatorType.Assign;
            codeBinaryOperator.Left = codeArrayIndexer;
            codeBinaryOperator.Right = new CodePrimitiveExpression(9);


            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            provider.GenerateCodeFromStatement(codeVariable, Console.Out, null);
            provider.GenerateCodeFromStatement(codeAssign, Console.Out, null);

            provider.GenerateCodeFromExpression(codeBinaryOperator, Console.Out, null);

        }

        /// <summary>
        /// 字典创建
        /// </summary>
        public static void Test_17()
        {
            CodeVariableDeclarationStatement codeVariable = new CodeVariableDeclarationStatement();
            codeVariable.Type = new CodeTypeReference(typeof(Dictionary<string,string>));
            codeVariable.Name = "a";
            codeVariable.InitExpression = new CodeObjectCreateExpression(typeof(Dictionary<string, string>));
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");

           


            //给字典元素赋值
            //左边添加引用
            //右边添加值
            CodeAssignStatement assignStatement_1 = new CodeAssignStatement();
            assignStatement_1.Left =new CodeIndexerExpression(new CodeVariableReferenceExpression() { VariableName= codeVariable.Name},new CodePrimitiveExpression("Age"));
            assignStatement_1.Right = new CodePrimitiveExpression("18");

            CodeAssignStatement assignStatement_2 = new CodeAssignStatement();
            assignStatement_2.Left = new CodeIndexerExpression(new CodeVariableReferenceExpression() { VariableName = codeVariable.Name }, new CodePrimitiveExpression("Name"));
            assignStatement_2.Right = new CodePrimitiveExpression("hpy");


            provider.GenerateCodeFromStatement(codeVariable, Console.Out, new CodeGeneratorOptions() { BracingStyle="C"});
            provider.GenerateCodeFromStatement(assignStatement_1, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });
            provider.GenerateCodeFromStatement(assignStatement_2, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });

        }

        /// <summary>
        /// 声明类索引器，使用CodeMemberProperty 
        /// 这里有个地方要注意，就是索引器成员的名字，为了兼容各种语言，较合适的做法是把它命名为“item”或“Item”（不分大小写），
        /// 这样一来，生成C#代码时，就能够生成 this[int k] 这样的语法，只有这样的语法才能被认为是索引器。
        /// </summary>
        public static void Test_18()
        {
            CodeTypeDeclaration codeType = new CodeTypeDeclaration();
            codeType.Name = "Person";

            CodeMemberProperty memberProperty = new CodeMemberProperty();
            memberProperty.Attributes = MemberAttributes.Final | MemberAttributes.Public;
            memberProperty.Name = "item";
            memberProperty.Type = new CodeTypeReference(typeof(string));
            memberProperty.Parameters.Add(new CodeParameterDeclarationExpression() { Type = new CodeTypeReference(typeof(int)), Name = "k" });

            //一般属性
            CodeMemberProperty memberProperty2 = new CodeMemberProperty();

            memberProperty2.Name = "item";
            memberProperty2.Type = new CodeTypeReference(typeof(int));


            codeType.Members.Add(memberProperty);
            codeType.Members.Add(memberProperty2);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");



            provider.GenerateCodeFromType(codeType, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });
        }
    }

    /// <summary>
    /// 分支与循环
    /// </summary>
    class code_swicthloop
    {

        /// <summary>
        /// CodeConditionStatement  类似于if
        /// 1、条件，用于判断给定的表达式是否为true。
        /// 2、当条件成立时所执行的代码。
        /// 3、当条件不成立时所执行的代码。
        /// </summary>
        public static void Test_01()
        {
            //取模运算
            CodeBinaryOperatorExpression modexp = new CodeBinaryOperatorExpression();
            modexp.Operator = CodeBinaryOperatorType.Modulus;
            modexp.Left = new CodePrimitiveExpression((int)6);
            modexp.Right = new CodePrimitiveExpression((int)2);

            //相等运算
            CodeBinaryOperatorExpression eqexp = new CodeBinaryOperatorExpression();
            eqexp.Operator = CodeBinaryOperatorType.IdentityEquality;

            eqexp.Left = modexp;
            eqexp.Right = new CodePrimitiveExpression(0);


            //分支语句
            CodeConditionStatement conditionStatement = new CodeConditionStatement();
            conditionStatement.Condition = eqexp;

            CodeMethodReferenceExpression codeMethod = new CodeMethodReferenceExpression();
            codeMethod.TargetObject = new CodeTypeReferenceExpression(typeof(Console));
            codeMethod.MethodName = nameof(Console.WriteLine);

            //如果为True
            conditionStatement.TrueStatements.Add(new CodeMethodInvokeExpression(codeMethod,new CodePrimitiveExpression("这是偶数")));

            //如果为False;
            conditionStatement.FalseStatements.Add(new CodeMethodInvokeExpression(codeMethod, new CodePrimitiveExpression("这是奇数")));


            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            provider.GenerateCodeFromStatement(conditionStatement, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });
        }

        /// <summary>
        /// 定义一个字符串，访问字符串的属性
        /// string str="ABCS1212121"; if(str.Length){}
        /// </summary>
        public static void Test_02()
        {
            CodeVariableDeclarationStatement codeVariable = new CodeVariableDeclarationStatement();
            codeVariable.Name = "str";
            codeVariable.Type = new CodeTypeReference(typeof(string));
            codeVariable.InitExpression = new CodePrimitiveExpression("Abcdajqoeuqoe");

            CodePropertyReferenceExpression codeProperty = new CodePropertyReferenceExpression();
            codeProperty.TargetObject = new CodeVariableReferenceExpression(codeVariable.Name);
            codeProperty.PropertyName = nameof(string.Length);

            CodeBinaryOperatorExpression codeBinary = new CodeBinaryOperatorExpression();
            codeBinary.Operator = CodeBinaryOperatorType.GreaterThan;
            codeBinary.Left = codeProperty;
            codeBinary.Right = new CodePrimitiveExpression(5);

            CodeConditionStatement codeCondition = new CodeConditionStatement();
            codeCondition.Condition = codeBinary;

            codeCondition.TrueStatements.Add(new CodeMethodInvokeExpression(
                new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Console)),nameof(Console.WriteLine)),new CodePrimitiveExpression("字符串长度大于5")));


            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            provider.GenerateCodeFromStatement(codeVariable, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });
            provider.GenerateCodeFromStatement(codeCondition, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });

        }

        /// <summary>
        /// 循环语句 CodeIterationStatement 
        /// 1、循环条件的初始值。
        /// 2、判断是否执行循环的条件。
        /// 3、对循环条件的更改。
        /// </summary>
        public static void Test_03()
        {
            CodeIterationStatement codeIteration = new CodeIterationStatement();
            //初始化
            codeIteration.InitStatement = new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(int)), "i", new CodePrimitiveExpression(0));
            //循环条件
            codeIteration.TestExpression = new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("i"), CodeBinaryOperatorType.LessThan, new CodePrimitiveExpression(10));

            //条件更改
            codeIteration.IncrementStatement = new CodeAssignStatement(new CodeVariableReferenceExpression("i"), 
                new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("i"), CodeBinaryOperatorType.Add, new CodePrimitiveExpression(1)));

            codeIteration.Statements.Add(new CodeCommentStatement("循环体"));

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            provider.GenerateCodeFromStatement(codeIteration, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });

        }

        /// <summary>
        /// 死循环
        /// </summary>
        public static void Test_04()
        {
            CodeIterationStatement codeIteration = new CodeIterationStatement();
            codeIteration.InitStatement = new CodeSnippetStatement("");
            codeIteration.TestExpression = new CodePrimitiveExpression(true);
            codeIteration.IncrementStatement = new CodeSnippetStatement("");

            codeIteration.Statements.Add(new CodeCommentStatement("循环体"));

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");
            provider.GenerateCodeFromStatement(codeIteration, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });
        }
        public static void Test()
        {
            //Test_01();
            //Test_02();
            //Test_03();
            Test_04();
        }
    }

    class StaticTest
    {
        public static void T()
        {
            //CodeTest_Type.Test_13();
            //CodeTest_Type.Test_14();

            //CodeTest_Type.Test_15();
            //CodeTest_Type.Test_16();
            //CodeTest_Type.Test_17();
            //CodeTest_Type.Test_18();

            code_swicthloop.Test();
        }
    }
}
