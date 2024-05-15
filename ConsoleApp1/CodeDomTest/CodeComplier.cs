using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp1.CodeDomTest
{
    //CodeDomProvider 类提供了三个可以执行编译的方法：

    //1、CompileAssemblyFromSource——这个好懂，也好办，就是用字符串直接构建代码，然后传给这个方法，就可以把源代码编译了。

    //2、CompileAssemblyFromFile——这个是把一个代码文件传给方法进行编译，文件中包含源代码。

    //3、CompileAssemblyFromDom——这个重载版本跟我们之前所学的内容关联性最大，因为它是把 CodeCompileUnit 实例传进去来编译的。
    



// 1、如果你要生成可直接运行的程序集，即.exe，那就得把GenerateExecutable属性设置为true，默认它是为false的，即生成dll文件。
// 所有可执行文件，不管你用啥语言写，都必须有入口点的，所以，如果要生成exe，就必须设置MainClass属性，它指的是包含Main方法的类，类名必须完整，要写上命名空间的名字，
// 如my.Program。

//2、设置OutputAssembly属性，指定输出文件名，可以是绝对路径，也可以是相对路径。如dddd.exe、kkkk.dll等。
//当然你可以用其他名字，如comm.ft，但是，如果要生成exe，后缀必须是.exe，这样才能双击运行。如果这个属性没有指定，
//它会生成一个随机的文件名，并且输出临时文件目录下。注意：输出文件是设置OutputAssembly属性，不是CoreAssemblyFileName属性，
//千万不要弄错，CoreAssemblyFileName是设置核心类库的位置，即常见的 mscorlib.dll，主要是包含.net基本类型的程序集，一般我们不用设置它，由编译器自行选择合适的版本。

//3、如果GenerateInMemory设置为true，则可以不设置OutputAssembly，因为GenerateInMemory属性表示把程序集生成到内存中，而不是文件中。

//4、TempFiles设置编译时所产生的临时文件的路径，默认是临时文件夹，这个一般不用改。

//5、编译过程实际上是调用.net的命令行工具的，对于VB.NET语言，调用vbc命令，对于C#语言，调用csc命令。
//如果要指定一些编译器选项，可以设置CompilerOptions属性，它是一个字符串。关于编译选项，可以在开发工具的命令行工具中输入csc /?或vbc /?查看。
    class CodeComplier
    {

        public static void Test_01()
        {
            //文件路径
            string srccodePath = @"G:\Test\CodeDomStudy\ConsoleApp1\CodeDomTest\Demo.cs";

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");

            //编译参数
            CompilerParameters p = new CompilerParameters();
            //输出文件
            p.OutputAssembly = "DemoLib.dll";

            //添加引用程序集，只是演示
            //mscorLib.dll是不用添加的，它是默认库
            p.ReferencedAssemblies.Add("System.dll");
            p.ReferencedAssemblies.Add("System.Linq.dll");


            //编译
            CompilerResults results = provider.CompileAssemblyFromFile(p, srccodePath);


            if(results.Errors.Count==0)
            {
                Console.WriteLine("编译成功...");

                //获取刚刚编译的程序集
                Assembly assembly = results.CompiledAssembly;

                Console.WriteLine($"程序集全名：{assembly.FullName}");
                Console.WriteLine($"程序集路径：{assembly.Location}");

                Type[] types = assembly.GetTypes();
                Console.WriteLine("-------------------------------------\r\n类型列表：");
                foreach (var item in types)
                {
                    Console.WriteLine("\t"+item.FullName);
                }


            }
            else
            {

                //编译错误
                Console.WriteLine("编译错误!!!");

                foreach (CompilerError item in results.Errors)
                {
                    Console.WriteLine($"文件{item.FileName}:行 {item.Line},列{item.Column}, 错误号{item.ErrorNumber},错误信息：{item.ErrorText}");
                }

            }

        }


        public static void Test_02()
        {
            CodeCompileUnit unit = new CodeCompileUnit();
            CodeNamespace ns = new CodeNamespace();
            ns.Name = "MyApp";
            ns.Imports.Add(new CodeNamespaceImport(nameof(System)));
            ns.Imports.Add(new CodeNamespaceImport("System.Windows.Forms"));

            unit.Namespaces.Add(ns);

            //创建一个Program类，添加一个程序入口函数
            CodeTypeDeclaration codeType = new CodeTypeDeclaration();
            codeType.Name = "Program";
            ns.Types.Add(codeType);
            codeType.TypeAttributes = TypeAttributes.NotPublic ;

            CodeEntryPointMethod entryPointMethod = new CodeEntryPointMethod();
            entryPointMethod.CustomAttributes.Add(new CodeAttributeDeclaration(nameof(STAThreadAttribute)));
            entryPointMethod.Name = "main";
            entryPointMethod.ReturnType = new CodeTypeReference(typeof(void));
            codeType.Members.Add(entryPointMethod);

            //创建窗口实例
            CodeVariableDeclarationStatement form = new CodeVariableDeclarationStatement();
            entryPointMethod.Statements.Add(form);
            form.Name = "mainWindow";
            form.Type = new CodeTypeReference("System.Windows.Forms.Form");
            form.InitExpression = new CodeObjectCreateExpression("System.Windows.Forms.Form");

            
            
            //设置标题栏
            CodeAssignStatement statementForm = new CodeAssignStatement();
            statementForm.Left = new CodePropertyReferenceExpression(new CodeVariableReferenceExpression(form.Name), nameof(System.Windows.Forms.Form.Text));
            statementForm.Right = new CodePrimitiveExpression("这是hpy测试窗体");

            entryPointMethod.Statements.Add(statementForm);


            //调用ShowDialog
            CodeMethodReferenceExpression codeMethodForm = new CodeMethodReferenceExpression();
            codeMethodForm.TargetObject = new CodeVariableReferenceExpression(form.Name);
            codeMethodForm.MethodName = nameof(Form.ShowDialog);

            CodeMethodInvokeExpression invokeExpressionForm = new CodeMethodInvokeExpression();
            invokeExpressionForm.Method = codeMethodForm;
            entryPointMethod.Statements.Add(invokeExpressionForm);

            ////通过Application.Run 启动窗口
            //CodeMethodReferenceExpression codeMethodApp = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(nameof(Application)), nameof(Application.Run));

            //CodeMethodInvokeExpression codeMethodInvokeApp = new CodeMethodInvokeExpression(codeMethodApp);
            //codeMethodInvokeApp.Method = codeMethodApp;
            //codeMethodInvokeApp.Parameters.Add(new CodeVariableReferenceExpression(form.Name));
            //entryPointMethod.Statements.Add(codeMethodInvokeApp);

            //生成代码
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CS");

            provider.GenerateCodeFromCompileUnit(unit, Console.Out, new CodeGeneratorOptions() { BracingStyle = "C" });


            CompilerParameters p = new CompilerParameters();
            p.GenerateExecutable = true;
            p.CompilerOptions = "/t:winexe";//非控制台程序
            p.OutputAssembly = "mytestWindow.exe";

            //包含入口点函数
            p.MainClass = $"{ns.Name}.{codeType.Name}";

            //引用程序集
            p.ReferencedAssemblies.Add("System.dll");
            p.ReferencedAssemblies.Add("System.Windows.Forms.dll");

            //编译
            CompilerResults results = provider.CompileAssemblyFromDom(p, unit);

            if(results.Errors.Count==0)
            {
                Console.WriteLine("编译成功");
                Process.Start(results.CompiledAssembly.Location);
            }
            else
            {
                Console.WriteLine("编译失败！！！！");

                foreach (CompilerError item in results.Errors)
                {
                    Console.WriteLine($"{item.ErrorNumber}:{item.ErrorText}");
                }
            }

        }

        public static void Test()
        {
            //Test_01();
            Test_02();
        }
    }
}
