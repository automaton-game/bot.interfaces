using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using AutomataNETjuegos.Compilador.Excepciones;
using AutomataNETjuegos.Contratos.Robots;
using AutomataNETjuegos.Logica;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AutomataNETjuegos.Compilador
{
    public class FabricaRobot : IFabricaRobot
    {
        private readonly ITempFileManager tempFileManager;

        public FabricaRobot(
            ITempFileManager tempFileManager)
        {
            this.tempFileManager = tempFileManager;
        }

        public IRobot ObtenerRobot(Type tipo)
        {
            return (IRobot)Activator.CreateInstance(tipo); ;
        }

        public IRobot ObtenerRobot(string t)
        {

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(t);

            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IRobot).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).GetTypeInfo().Assembly.Location)
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var tempFile = tempFileManager.Create();
            var result = compilation.Emit(tempFile);

            if (!result.Success)
            {
                IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                var errores = failures.Select(f => f.ToString()).ToArray();
                throw new ExcepcionCompilacion { ErroresCompilacion = errores };
            }
            else
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(tempFile);
                var type = assembly.ExportedTypes.FirstOrDefault(tipo =>
                    tipo.IsClass && tipo.IsPublic && tipo.IsVisible && typeof(IRobot).IsAssignableFrom(tipo));
                return ObtenerRobot(type);
            }
        }
    }
}
