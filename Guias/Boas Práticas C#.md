
Data

Como tirei essas regras da msdn(Microsoft Developers Networks) elas se aplicam mais em C# e VB.NET. Mas são sim adaptáveis ao Java.
Outro importante tópico a ser debatido neste post. Segue abaixo algumas maneiras e metodologias que ao meu ponto de vista são ótimas não só quando em equipe, mas a facilidade de manutenção e qualidade do código.
Primeiro, vou falar sobre o padrão de nomenclatura que são os seguintes:
Pascal case A primeira letra de cada palavra é maiuscula e as restantes minusculas: BackColor
Camel case Primeira palavra minuscula e as próximas são em Pascal case: backColor
Uppercase Maiuscula são as letras de palavras que foram abrevidas: System.IO, System.Web.UI, IDisposable
Lembrando que o .Net é Case Sensitivity, ou seja, sabe diferenciar maiúsculo de menusculo: calcula(); Calcula();
Naming Guidelines
Abreviações Não utilizer abreviações como: OpenWin() ao invés de usar OpenWindow(), são apenas poucas letras e isso faz muita diferença quando outro desenvolvedor ou até você mesmo for ler o código para alterações ou correção de bugs.
Namespace Deve ser sempre em Pascal case, pois também dará final a uma DLL (DiarioFonte.Blog.Core.dll). NomeEmpresa.NomeTecnologia[.Feature][.Design] DiarioFonte.Blog.Core DiarioFonte.Blog.Core.Posts DiarioFonte.Blog.Data
Classe Também deve ser sempre em Pascal case, pois ao referenciar uma variável como está classe, fará toda a diferença. public Class accessControl{} accessControl acesso = new accessControl(); Agora vejamos: public Class AccessControl{} AccessControl acesso = new AccessControl();
Com certeza a segunda forma é muito mais legivel, e imagine instanciarmos: sqlConnection, textBox… não tem cara de classe, concordam?
Interface Sempre use o “I” para iniciar a Interface, assim em seu código fica muito fácil tanto implementá-la quanto utilizar em seu código. (No caso, o I Seria UpperCase, porém é apenas uma letra, e o restante utiliza o padrão de nomenclatura da Classe) public interface IControlManager{} public Class AccessControlManager : IControlManager {}
Enum Pascal case. Compare: AccessControl.tipoAcesso.administrdor, AccessControl.TipoAcesso.Administrador. Com certeza o segundo é muito mais profissional!
Parametros Use sempre Camel Case. public DataTable EnviaEmail(string de, string para, bool preAutenticar){}
Métodos Métodos devem ser escritos em Pascal Case. accessControl.EnviaEmail(”igor@xxxx.xxx”, _sendTo, false);
Propriedades Pascal Case, pois assim, usariamos a propriedade exatamente assim: accessControl.Usuario
Campos Campos para utilização apenas interna da classe devem ser escritos como Camel Case, e se for um campo que será utilizado por uma Propriedade, deve-se iniciar com um _ (Underline), como no exemplo seguinte:
private string _usuario; public string Usuario { GET{return _usuario;} SET{_usuario = value;} }
