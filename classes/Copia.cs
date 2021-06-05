using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Framework_Aplicacao.classes
{
    public class Copia
    {
        public static void ProjetoModelo()
        {
            DirectoryCopy("../../modelo/Framework_Modelo", Parametros.DiretorioProjetoConvertido, true, true);
        }

        public static void Estrutura()
        {
            string[] files = Directory.GetFiles(Parametros.DiretorioProjetoLegado);
            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);

                if (info.Name.ToLower() != "program.cs" && 
                    info.Extension != ".csproj" && 
                    info.Extension != ".user" && 
                    info.Extension != ".vspscc" && 
                    info.Extension != ".sln" && 
                    info.Extension != ".config")
                    FileCopy(file, Path.Combine(Parametros.DiretorioProjetoConvertido, info.Name));
            }

            string[] directories = Directory.GetDirectories(Parametros.DiretorioProjetoLegado);
            foreach (string dir in directories)
            {
                DirectoryInfo infoDir = new DirectoryInfo(dir);
                
                if (infoDir.Name != "bin" && infoDir.Name != "obj" && infoDir.Name != "Properties")
                    DirectoryCopy(dir, Path.Combine(Parametros.DiretorioProjetoConvertido, infoDir.Name), true, false);   
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, bool copyForms)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
                throw new DirectoryNotFoundException("Diretório não existe ou não pode ser encontrado: " + dir.FullName);

            DirectoryInfo[] dirs = dir.GetDirectories();
            Directory.CreateDirectory(destDirName);
            FileInfo[] files = dir.GetFiles();

            List<string> forms = new List<string>();
            if (!copyForms)
                forms.AddRange(files.Where(a => a.Extension == ".resx").Select(a => a.Name.Replace(a.Extension, "")));

            foreach (FileInfo file in files)
            {
                if (!copyForms)
                    if (forms.Exists(a => a == file.Name.Split('.')[0]))
                        continue;
                
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs, copyForms);
                }
            }
        }

        private static void FileCopy(string sourceFileName, string destFileName, bool copyForms = false)
        {
            if (copyForms)
            {
                File.Copy(sourceFileName, destFileName, false);
            }
            else
            {
                FileInfo info = new FileInfo(sourceFileName);
                DirectoryInfo dir = new DirectoryInfo(info.DirectoryName);
                FileInfo[] files = dir.GetFiles();

                List<string> forms = new List<string>();
                forms.AddRange(files.Where(a => a.Extension == ".resx").Select(a => a.Name.Replace(a.Extension, "")));

                if (!forms.Exists(a => a == info.Name.Split('.')[0]))
                    File.Copy(sourceFileName, destFileName, false);
            }
        }
    }
}