using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Framework_Aplicacao.classes
{
    public class Converter
    {
        private static string NomeNovoComponenteBindingSource { get; set; }
        private static string NomeProjeto { get; set; }
        private static List<string> Componente = new List<string>();

        public static void Forms()
        {
            List<FileInfo> files = new List<FileInfo>();

            DirectoryInfo directoryRoot = new DirectoryInfo(Parametros.DiretorioProjetoLegado);
            DirectoryInfo[] directories = directoryRoot.GetDirectories("*.*", SearchOption.AllDirectories);

            List<string> forms = new List<string>();
            forms.AddRange(directoryRoot.GetFiles().Where(a => a.Extension == ".resx").Select(a => a.Name.Replace(a.Extension, "")));
            files.AddRange(directoryRoot.GetFiles().Where(a => a.Name.Split('.')[0] == forms.Find(b => b == a.Name.Split('.')[0])));
            NomeProjeto = directoryRoot.GetFiles().FirstOrDefault(a => a.Extension == ".csproj").Name.Replace(".csproj", "");

            foreach (DirectoryInfo dir in directories)
            {
                if (!dir.FullName.Contains("bin") && !dir.FullName.Contains("obj") && !dir.FullName.Contains("Properties"))
                {
                    forms = new List<string>();
                    forms.AddRange(dir.GetFiles().Where(a => a.Extension == ".resx").Select(a => a.Name.Replace(a.Extension, "")));
                    files.AddRange(dir.GetFiles().Where(a => a.Name.Split('.')[0] == forms.Find(b => b == a.Name.Split('.')[0])));
                }
            }

            directoryRoot = new DirectoryInfo(Parametros.DiretorioProjetoConvertido);
            var grupo = files.GroupBy(a => a.Name.Split('.')[0]);

            foreach (var item in grupo)
            {
                #region Variáveis
                string paginaModelo = new FileInfo("../../modelo/Framework_Modelo/Views/Modelos/Pagina.cshtml").OpenText().ReadToEnd();
                string utilizaBindingSource = string.Empty;
                string paginaTabela = string.Empty;
                string paginaTabelaHeader = string.Empty;
                string paginaTabelaBody = string.Empty;
                string paginaComponentes = string.Empty;
                string paginaJS = string.Empty;
                string paginaJSFunctions = string.Empty;
                string paginaModel = string.Empty;
                string paginaController = string.Empty;
                string paginaInterface = string.Empty;
                string paginaService = string.Empty;
                string location = string.Empty;
                string size = string.Empty;
                string[] arrayLocation = null;
                string[] arraySize = null;

                string designer = item.FirstOrDefault(a => a.Name.Contains("Designer")).OpenText().ReadToEnd();
                string cs = item.FirstOrDefault(a => a.Name.Split('.')[1] == "cs").OpenText().ReadToEnd();
                string initializeComponent = GetMetodo(designer, "InitializeComponent()");
                List<string> componentes = GetComponentes(designer);
                List<string> componentesDet = GetComponentesDetalhe(initializeComponent);
                #endregion

                if (componentes.Exists(a => a.Contains(".BindingSource")))
                    utilizaBindingSource = componentes.Find(a => a.Contains(".BindingSource")).Split(' ')[2].Replace(";", "");

                for (int i = 0; i < componentesDet.Count; i++)
                {
                    string nome = componentes[i].Split(' ')[2].Replace(";", "");
                    string tipo = componentes[i].Split(' ')[1];

                    Componente.Add(nome);

                    string[] array = componentesDet[i].Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    try
                    {
                        arrayLocation = array.First(a => a.Contains(".Location")).Trim().Replace(");", "").Split('(')[1].Split(',');
                        location = string.Format("left: {0}px; top: {1}px;", arrayLocation[0].Trim(), arrayLocation[1].Trim());
                        arraySize = array.First(a => a.Contains(".Size")).Trim().Replace(");", "").Split('(')[1].Split(',');
                        size = string.Format("width: {0}px; height: {1}px;", arraySize[0].Trim(), arraySize[1].Trim());
                    }
                    catch
                    {
                        arrayLocation = null;
                        location = string.Empty;
                        arraySize = null;
                        size = string.Empty;
                    }

                    if (tipo.Contains(".TextBox"))
                    {
                        paginaComponentes += string.Format("<input type=\"text\" id=\"{0}\" name=\"{0}\" style=\"position: absolute; {1} {2}\" value=\"@ViewData[\"{0}\"]\"/>\r\n", nome, location, size);

                        var eventos = array.Where(a => a.Contains("+=")).ToList();
                        if (eventos.Exists(a => a.Contains(".Leave")))
                        {
                            string leave = GetMetodo(cs, nome + "_Leave");
                            leave = VerificaBindingSource(leave, utilizaBindingSource, nome);
                            leave = leave.Replace(nome + ".Text", "p_" + nome);
                            leave = leave.Replace("return;\r\n", "return new JsonResult(\"\");");

                            paginaController += string.Format(@"public IActionResult {0}_Leave(string p_{0})
                                                                {{
                                                                    {1}
                                                                    return new JsonResult("""");
                                                                }}" + "\r\n", nome, leave);

                            paginaJS += string.Format(@"$(""#{0}"").on(""blur"", function() {{
                                                            if ($(""#{0}"").val() !== """") {{
                                                                var params = {{
                                                                    p_{0}: $(this).val(),
                                                                }};
                                                                $.ajax({{
                                                                    type: 'POST',
                                                                    url: '@Url.Action(""{0}_Leave"", ""{1}"")',
                                                                    contentType: 'application/x-www-form-urlencoded',
                                                                    data: params,
                                                                    success: function(result) {{
                                                                        if (result !== ""null"")
                                                                        {{
                                                                            alert(result.responseText);
                                                                        }}
                                                                    }},
                                                                    error: function(result) {{
                                                                        alert(""Ocorreu um erro!\n\n"" + result.responseText);
                                                                    }}
                                                                }});
                                                            }}
                                                        }});" + "\r\n", nome, item.Key);
                        }
                        //Demais eventos

                    }
                    else if (tipo.Contains(".Label"))
                    {
                        if (array.First(a => a.Contains(".AutoSize")) != null)
                        {
                            if (array.First(a => a.Contains(".AutoSize")).Trim().Split('=')[1].Trim().Contains("true"))
                                size = "";
                        }

                        string text = array.First(a => a.Contains(".Text")).Trim().Replace("\"", "").Replace(";", "").Split('=')[1].Trim();

                        paginaComponentes += string.Format("<label id=\"{0}\" style=\"position: absolute; {1} {2}\">{3}</label>\r\n", nome, location, size, text);

                        var eventos = array.Where(a => a.Contains("+=")).ToList();
                        if (eventos.Contains(".Leave"))
                        {
                            paginaJS += string.Format(@"$(""#{0}"").on(""blur"", function() {{
                                                            if ($(""#{0}"").val() !== """") {{
                                                                var params = {{
                                                                    p_{0}: $(this).val(),
                                                                }};
                                                                $.ajax({{
                                                                    type: 'POST',
                                                                    url: '@Url.Action(""{0}_Leave"", ""{1}"")',
                                                                    contentType: 'application/x-www-form-urlencoded',
                                                                    data: params,
                                                                    success: function(result) {{
                                                                        if (result === ""null"")
                                                                        {{
                                                                        }}
                                                                    }},
                                                                    error: function(result) {{
                                                                        alert(""Ocorreu um erro!\n\n"" + result.responseText);
                                                                    }}
                                                                }});
                                                            }}
                                                        }});" + "\r\n", nome, item.Key);
                        }
                        //Demais eventos


                    }
                    else if (tipo.Contains(".Button"))
                    {
                        string text = array.First(a => a.Contains(".Text")).Trim().Replace("\"", "").Replace(";", "").Split('=')[1].Trim();

                        paginaComponentes += string.Format("<button id=\"{0}\" style=\"position: absolute; {1} {2}\">{3}</button>\r\n", nome, location, size, text);

                        var eventos = array.Where(a => a.Contains("+=")).ToList();
                        if (eventos.Exists(a => a.Contains(".Click")))
                        {
                            string click = GetMetodo(cs, nome + "_Click");
                            click = VerificaBindingSource(click, utilizaBindingSource, nome);
                            click = click.Replace("return;\r\n", "return new JsonResult(\"\");");

                            paginaController += string.Format(@"public IActionResult {0}_Click()
                                                                {{
                                                                    {1}
                                                                    return new JsonResult("""");
                                                                }}" + "\r\n", nome, click);


                            paginaJS += string.Format(@"$(""#{0}"").on(""click"", function() {{
                                                            $.ajax({{
                                                                type: 'POST',
                                                                url: '@Url.Action(""{0}_Click"", ""{1}"")',
                                                                contentType: 'application/x-www-form-urlencoded',
                                                                data: null,
                                                                success: function(result) {{
                                                                    if (result === ""null"")
                                                                    {{
                                                                    }}
                                                                }},
                                                                error: function(result) {{
                                                                    alert(""Ocorreu um erro!\n\n"" + result.responseText);
                                                                }}
                                                            }});
                                                        }});" + "\r\n", nome, item.Key);
                        }
                        //Demais eventos


                    }
                    else if (tipo.Contains(".DataGridView"))
                    {
                        if (tipo.Contains("Column"))
                        {
                            //Monta a Model de acordo com as colunas da tabela
                            if (tipo.Contains("TextBox"))
                                paginaModel += string.Format("public string {0} {{ get; set; }}" + "\r\n", nome);
                            else if (tipo.Contains("CheckBox"))
                                paginaModel += string.Format("public bool {0} {{ get; set; }}" + "\r\n", nome);


                            paginaTabelaHeader += string.Format(@"<th>{0}</th>" + "\r\n", nome);
                            paginaTabelaBody += string.Format(@"<td>@item.{0}</td>" + "\r\n", nome);
                        }
                        else
                        {
                            //array.First(a => a.Contains(".DataSource")).Trim().Replace("\"", "").Replace(";", "").Split('=')[1].Trim();

                            paginaTabela += @"<div class=""bloco"" style=""margin-top: 30px;"">
                                                     <div class=""row"">
                                                      <table id=""" + nome + @""" class=""table table-bordered table-striped"" style=""" + location + " " + size + @""">
                                                          <thead>
                                                              <tr>
                                                                  {0}
                                                              </tr>
                                                          </thead>
                                                          <tbody>
                                                              @foreach (var item in ViewBag." + NomeNovoComponenteBindingSource + @")
                                                              {{
                                                                  <tr>
                                                                      {1}
                                                                  </tr>
                                                              }}
                                                          </tbody>
                                                      </table>
                                                  </div>
                                              </div>";

                            paginaJS += string.Format(@"$(""#{0}"").DataTable();" + "\r\n", nome);

                            paginaJSFunctions += @"$.fn.dataTable.ext.order['dom-text'] = function (settings, col) {
                                                       return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
                                                           return $('input', td).val();
                                                       });
                                                   };" + "\r\n";
                        }
                    }
                    else if (tipo.Contains(".SaveFileDialog"))
                    {

                    }
                }

                //Cria a Model
                paginaModel = string.Format(NovaModel(item.Key), paginaModel);

                //Ajusta as referencias da controller
                string referencia = string.Empty;
                if (paginaController.Contains("CultureInfo"))
                    referencia += "using System.Globalization;\r\n";
                if (paginaController.Contains("Thread"))
                    referencia += "using System.Threading;\r\n";
                if (paginaController.Contains("Stream"))
                    referencia += "using System.IO;\r\n";

                //Verifica componentes 
                foreach (string comp in Componente)
                {
                    if (paginaController.Contains(comp + ".Text."))
                        paginaController = paginaController.Replace(comp + ".Text.", "ViewData[\"" + comp + "\"].ToString().");
                    if (paginaController.Contains(comp + ".Text)"))
                        paginaController = paginaController.Replace(comp + ".Text)", "ViewData[\"" + comp + "\"].ToString())");
                    if (paginaController.Contains(comp + ".Text"))
                        paginaController = paginaController.Replace(comp + ".Text", "ViewData[\"" + comp + "\"]");
                }

                //Ajustes temporários
                paginaController = paginaController.Replace("m_str = str;", "StringBuilder m_str = str;\n\t\t\tint i = 0;\n\t\t\tint j = 0;");
                paginaController = paginaController.Replace("MessageBox.Show", "//MessageBox.Show");
                paginaController = paginaController.Replace("saveFileDialog.FileName =", "string nomeArquivo =");
                paginaController = paginaController.Replace("if (saveFileDialog.ShowDialog() != DialogResult.OK)\r\n", "//if (saveFileDialog.ShowDialog() != DialogResult.OK)\r\n//");
                paginaController = paginaController.Replace("= saveFileDialog.FileName", "= \"C:/Temp/\"" + " + nomeArquivo");
                paginaController = paginaController.Replace("sw.Write(m_str.ToString());", "sw.Write(m_str.ToString());\r\n\t\t\t\tbyte[] fileBytes = System.IO.File.ReadAllBytes(arquivo);\r\n\t\t\t\treturn File(fileBytes, \"application/force-download\", nomeArquivo);");

                //Cria a Controller
                paginaController = string.Format(NovaController(item.Key), referencia, paginaController);

                //Cria a Interface
                paginaInterface = string.Format(NovaInterface(item.Key), paginaInterface);

                //Cria a Service
                paginaService = string.Format(NovaService(item.Key), paginaService);

                //Se existe uma tabela, adiciona junto aos componentes
                if (!string.IsNullOrEmpty(paginaTabela))
                {
                    paginaTabela = string.Format(paginaTabela, paginaTabelaHeader, paginaTabelaBody);
                    paginaComponentes += paginaTabela;
                }

                //Adiciona os componentes dentro da pagina
                paginaModelo = paginaModelo.Replace("@*Modelo*@", item.Key);
                paginaModelo = paginaModelo.Replace("@*@model*@", "@model " + item.Key);
                paginaModelo = paginaModelo.Replace("<!--*##HTML##*-->", paginaComponentes);
                paginaModelo = paginaModelo.Replace("//*##JS##*", paginaJS);
                paginaModelo = paginaModelo.Replace("//*##JS_FUNCTIONS##*", paginaJSFunctions);

                //Gravar Model em arquivo para o novo projeto
                GravarArquivo(Path.Combine(Parametros.DiretorioProjetoConvertido, "Models"), item.Key + ".cs", paginaModel);

                //Gravar Controller em arquivo para o novo projeto
                GravarArquivo(Path.Combine(Parametros.DiretorioProjetoConvertido, "Controllers"), item.Key + "Controller.cs", paginaController);

                //Gravar Interface em arquivo para o novo projeto
                GravarArquivo(Path.Combine(Parametros.DiretorioProjetoConvertido, "Interfaces"), "I" + item.Key + "Service.cs", paginaInterface);

                //Gravar Service em arquivo para o novo projeto
                GravarArquivo(Path.Combine(Parametros.DiretorioProjetoConvertido, "Services"), item.Key + "Service.cs", paginaService);

                //Gravar pagina em arquivo para o novo projeto
                GravarArquivo(Path.Combine(Parametros.DiretorioProjetoConvertido, "Views", item.Key), item.Key + ".cshtml", paginaModelo);
            }
        }

        private static string VerificaBindingSource(string metodo, string utilizaBindingSource, string nomeNovoComponente)
        {
            string ret = metodo;

            if (!string.IsNullOrEmpty(utilizaBindingSource))
            {
                if (metodo.Contains(utilizaBindingSource))
                {
                    if (string.IsNullOrEmpty(NomeNovoComponenteBindingSource))
                        NomeNovoComponenteBindingSource = nomeNovoComponente;

                    ret = metodo.Replace(utilizaBindingSource + ".DataSource", "ViewBag." + NomeNovoComponenteBindingSource);
                }
            }

            return ret;
        }

        private static void GravarArquivo(string dirNovoArquivo, string nomeArquivo, string stringArquivo)
        {
            if (!Directory.Exists(dirNovoArquivo))
                Directory.CreateDirectory(dirNovoArquivo);

            StreamWriter writer = new StreamWriter(Path.Combine(dirNovoArquivo, nomeArquivo));
            writer.Write(stringArquivo);
            writer.Close();
        }

        private static string GetMetodo(string arquivo, string nomeMetodo)
        {
            int index = arquivo.IndexOf(nomeMetodo);
            string aux = arquivo.Substring(index);
            index = aux.IndexOf("{\r\n");
            aux = aux.Substring(index);
            List<char> pilhaChaves = new List<char>();
            int indexMetodo = 0;
            foreach (char c in aux)
            {
                indexMetodo++;

                if (c == '{')
                    pilhaChaves.Add(c);
                else if (c == '}')
                    pilhaChaves.RemoveAt(pilhaChaves.Count - 1);

                if (pilhaChaves.Count == 0)
                    break;
            }
            aux = aux.Substring(1, indexMetodo - 2).Trim();
            return aux;
        }

        private static List<string> GetComponentes(string arquivo)
        {
            List<string> lista = new List<string>();
            int index = arquivo.IndexOf("#endregion");
            string comp = arquivo.Substring(index);
            comp = comp.Substring(0, comp.IndexOf("}\r\n")).Trim();
            string[] array = comp.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            for (int i = 2; i < array.Length; i++)
                lista.Add(array[i].Trim());                

            return lista;
        }

        private static List<string> GetComponentesDetalhe(string arquivo)
        {
            List<string> lista = new List<string>();
            string arquivoAux = arquivo;
            int indexTotal = 0;
            
            while (indexTotal < arquivo.Length)
            {
                int index = arquivoAux.IndexOf("// \r\n");
                string comp = arquivoAux.Substring(index);
                int index2 = comp.IndexOf(";\r\n            //") + 1;
                if (index2 == 0 && comp.Length > 0)
                {
                    lista.Add(comp);
                    break;
                }

                comp = comp.Substring(0, index2).Trim();
                indexTotal += index + index2;
                arquivoAux = arquivoAux.Substring(index + index2).Trim();
                lista.Add(comp);
            }
            
            return lista;
        }

        private static string NovaController(string nome)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("using System;");
            str.AppendLine("using System.Collections.Generic;");
            str.AppendLine("using System.Text;");
            str.AppendLine("using Framework_Modelo.Interfaces; ");
            str.AppendLine("using Microsoft.AspNetCore.Mvc; ");
            str.AppendLine("using " + NomeProjeto + ";");
            str.AppendLine("{0}");
            str.AppendLine();
            str.AppendLine("namespace Framework_Modelo.Controllers ");
            str.AppendLine("{{ ");
                str.AppendLine("\tpublic class " + nome + "Controller : Controller ");
                str.AppendLine("\t{{ ");
                    str.AppendLine("\t\tprivate readonly I" + nome + "Service _service; ");
                    str.AppendLine("\t\tpublic " + nome + "Controller(I" + nome + "Service service) ");
                    str.AppendLine("\t\t{{ ");
                        str.AppendLine("\t\t\t_service = service; ");
                    str.AppendLine("\t\t}} ");
                    str.AppendLine("\t\tpublic IActionResult " + nome + "() ");
                    str.AppendLine("\t\t{{ ");
                        str.AppendLine("\t\tViewBag.Tabela = new List<Tabela>(); "); //Ajuste temporário
                    str.AppendLine("\t\t\treturn View(); ");
                    str.AppendLine("\t\t}} ");
                    str.AppendLine("\t\t{1} ");
                str.AppendLine("\t}} ");
            str.AppendLine("}} ");

            return str.ToString();
        }

        private static string NovaInterface(string nome)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("using Framework_Modelo.Models; ");
            str.AppendLine("using System.Collections.Generic; ");
            str.AppendLine();
            str.AppendLine("namespace Framework_Modelo.Interfaces ");
            str.AppendLine("{{ ");
                str.AppendLine("\tpublic interface I" + nome + "Service ");
                str.AppendLine("\t{{ ");
                    str.AppendLine("\t\t{0}");
                str.AppendLine("\t}} ");
            str.AppendLine("}} ");

            return str.ToString();
        }

        private static string NovaModel(string nome)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("namespace Framework_Modelo.Models ");
            str.AppendLine("{{ ");
                str.AppendLine("\tpublic class " + nome + " ");
                str.AppendLine("\t{{ ");
                    str.AppendLine("\t\t{0}");
                str.AppendLine("\t}} ");
            str.AppendLine("}} ");

            return str.ToString();
        }

        private static string NovaService(string nome)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("using Framework_Modelo.Interfaces; ");
            str.AppendLine("using Framework_Modelo.Models; ");
            str.AppendLine("using System.Collections.Generic; ");
            str.AppendLine();
            str.AppendLine("namespace Framework_Modelo.Services ");
            str.AppendLine("{{ ");
                str.AppendLine("\tpublic class " + nome + "Service : I" + nome + "Service ");
                str.AppendLine("\t{{ ");
                    str.AppendLine("\t\tprivate readonly string _connectionString; ");
                    str.AppendLine("\t\tpublic " + nome + "Service(string connectionString) ");
                    str.AppendLine("\t\t{{ ");
                        str.AppendLine("\t\t\t_connectionString = connectionString; ");
                    str.AppendLine("\t\t}} ");
                    str.AppendLine("\t\t{0}");
                str.AppendLine("\t}}");
            str.AppendLine("}}");

            return str.ToString();
        }
    }
}