using System;
using System.IO;
using System.Xml;
using System.Linq;

namespace Framework_Aplicacao.classes
{
    public class Referencias
    {
        public static void Localizar()
        {
            XmlNodeList elementsLegado = null;

            string csprojLegado = RetornaDiretorio_csproj(Parametros.DiretorioProjetoLegado);
            if (string.IsNullOrEmpty(csprojLegado))
                throw new Exception("Diretório do projeto legado não contem arquivo csproj!");
            else
            {
                csprojLegado = RetornaInformacaoArquivo(csprojLegado);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(csprojLegado);
                elementsLegado = xmlDoc.GetElementsByTagName("ProjectReference");
            }

            string csprojModeloDir = Path.Combine(Parametros.DiretorioProjetoConvertido, "Framework_Modelo.csproj");
            string csprojModelo = RetornaInformacaoArquivo(csprojModeloDir);
            if (!string.IsNullOrEmpty(csprojModelo))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(csprojModelo);

                XmlNode root = xmlDoc.DocumentElement;
                
                XmlElement itemGroup = xmlDoc.CreateElement("ItemGroup");

                foreach (XmlNode item in elementsLegado)
                {
                    string nomeRef = RetornaNomeReferencia(item.ChildNodes).InnerText;

                    XmlElement reference = xmlDoc.CreateElement("Reference");
                    reference.SetAttribute("Include", nomeRef);

                    XmlElement hintPath = xmlDoc.CreateElement("HintPath");
                    hintPath.InnerText = item.Attributes[0].Value;

                    //Se a referencia for um arquivo csproj, procura a dll no bin e substitui o caminho original pela da dll
                    if (hintPath.InnerText.Contains(".csproj"))
                    {
                        DirectoryInfo directoryRoot = new DirectoryInfo(Path.Combine(Parametros.DiretorioProjetoLegado, "bin"));
                        DirectoryInfo[] directories = directoryRoot.GetDirectories("*.*", SearchOption.AllDirectories);
                        foreach (DirectoryInfo dir in directories)
                        {
                            FileInfo[] fileInfo = dir.GetFiles();
                            if (fileInfo.Count(a => a.Name.Replace(a.Extension, "") == nomeRef) > 0)
                            {
                                if (!Directory.Exists(Path.Combine(Parametros.DiretorioProjetoConvertido, "dll")))
                                    Directory.CreateDirectory(Path.Combine(Parametros.DiretorioProjetoConvertido, "dll"));

                                string refDest = Path.Combine(Parametros.DiretorioProjetoConvertido, "dll", nomeRef + ".dll");
                                File.Copy(fileInfo.FirstOrDefault(a => a.Name.Replace(a.Extension, "") == nomeRef).FullName, refDest);

                                break;
                            }
                            else
                                throw new Exception("Referência aponta para arquivo csproj, mas não existe dll desta referência no bin!");
                        }
                    }

                    reference.AppendChild(hintPath);
                    itemGroup.AppendChild(reference);
                }
                root.AppendChild(itemGroup);
                xmlDoc.Save(csprojModeloDir);
            }
            else
                throw new Exception("Arquivo Framework_Modelo.csproj não encontrado!");
        }

        private static XmlNode RetornaNomeReferencia(XmlNodeList nodeList)
        {
            XmlNode name = null;
            foreach (XmlNode item in nodeList)
            {
                if (item.Name == "Name")
                {
                    name = item;
                    break;
                }
            }
            return name;
        }

        private static string RetornaInformacaoArquivo(string path)
        {
            return File.ReadAllText(path);
        }

        private static string RetornaDiretorio_csproj(string root)
        {
            string fileDir = string.Empty;
            string[] files = Directory.GetFiles(root);
            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                if (info.Extension == ".csproj")
                {
                    fileDir = file;
                    break;
                }
            }
            return fileDir;
        }
    }
}
