# prmToolkit.EF.RepositoryBase
Fornece os principais métodos para seu repositorio que utiliza Entity Framework.
- Crud
- Consultas com ou sem include
- Consultas com ou sem Tracking
- Etc


Métodos disponíveis nesta primeira versão
- IQueryable<TEntidade> ListarPor
- IQueryable<TEntidade> ListarOrdenadosPor
- TEntidade ObterPor
- bool Existe
- IQueryable<TEntidade> Listar
- TEntidade Adicionar
- TEntidade Editar
-  void Remover
-  void AdicionarLista

## Instalação
Para instalar utilize o seguinte comando abaixo ou procure no Manage Nuget Packages
```sh
Install-Package prmToolkit.EF.RepositoryBase -Version 1.0.0
```
## Instalando no projeto
Instale o pacote no projeto onde esteja sua interface e implementação.
![image](https://user-images.githubusercontent.com/6010161/115731769-c823af80-a35d-11eb-9c9b-c8c50e4b7e74.png)

Veja como ficou minha interface dos repositorios no projeto de dominio
![image](https://user-images.githubusercontent.com/6010161/115731929-e8536e80-a35d-11eb-9dcd-19d208c4ff28.png)

Veja como ficou minha implementação no projeto de infraestrutura
![image](https://user-images.githubusercontent.com/6010161/115732144-18027680-a35e-11eb-816e-0d70a7b4b200.png)

## Usando os métodos
### Existe 
Passe a expressão lambda com a condição que deseja verificar se a entidade existe
```csharp
 //Verificar se o usuário já existe
  if (_repositoryJogador.Existe(x => x.Email == request.Email))
  {
      AddNotification("Email", MSG.ESTE_X0_JA_EXISTE.ToFormat("E-mail"));
      return new Response(this);
  }
```
### Adicionar nova entidade
Passe a entidade que deseja salvar para o método Adicionar
```csharp
  Entities.Jogador jogador = new Entities.Jogador(request.Nome, request.Email, request.Senha);
  _repositoryJogador.Adicionar(jogador);
```
### Editar uma entidade
Passe a entidade que deseja alterar para o método Editar
```csharp
  _repositoryJogador.Editar(jogador);
```
### Remover uma entidade
Passe a entidade que deseja alterar para o método Editar
```csharp
  _repositoryJogador.Remover(jogador);
```
### Listar entidades
Utilize seu repositorio para listar as entidades retornando um IQueryable<TEntidade>
```csharp
  //Lista com Tracking como default
  var jogadorCollection = _repositoryJogador.Listar();
  //ou
  var jogadorCollection = _repositoryJogador.Listar(QueryTrackingBehavior.TrackAll);
  
  //Listar sem o Tracking
  var jogadorCollection = _repositoryJogador.Listar(QueryTrackingBehavior.NoTracking);
  //ou
  var jogadorCollection = _repositoryJogador.Listar(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
  
  //Listar com Include
  var jogadorCollection = _repositoryJogador.Listar(x => x.Sala);
  //ou
  var jogadorCollection = _repositoryJogador.Listar("Sala");
```

### Listar por
Utilize seu repositorio para listar as entidades passando alguma condição retornando um IQueryable<TEntidade>
```csharp
  //Lista com Tracking como default
  var jogadorCollection = _repositoryJogador.ListarPor(x => x.Email == "paulo.analista@outlook.com");
  //ou
  var jogadorCollection = _repositoryJogador.ListarPor(x => x.Email == "paulo.analista@outlook.com", QueryTrackingBehavior.TrackAll);
  
  //Listar sem o Tracking
  var jogadorCollection = _repositoryJogador.ListarPor(x => x.Email == "paulo.analista@outlook.com", QueryTrackingBehavior.NoTracking);
  //ou
  var jogadorCollection = _repositoryJogador.ListarPor(x => x.Email == "paulo.analista@outlook.com", QueryTrackingBehavior.NoTrackingWithIdentityResolution);
  
  //Listar com Include
  var jogadorCollection = _repositoryJogador.ListarPor(x => x.Email == "paulo.analista@outlook.com", x=>x.Sala);
  //ou
  var jogadorCollection = _repositoryJogador.ListarPor(x => x.Email == "paulo.analista@outlook.com", "Sala");
  
  //Usando mais de uma condição
  var jogadorCollection = _repositoryJogador.ListarPor(x => x.Email == "paulo.analista@outlook.com" && x.Sala.Nome=="Sala1", x=>x.Sala);
```

### Listar por de forma ordenada
Utilize seu repositorio para listar as entidades passando alguma condição retornando um IQueryable<TEntidade> de forma ordenada
```csharp
  //Lista com Tracking como default
  var jogadorCollection = _repositoryJogador.ListarEOrdenadosPor(x => x.Email == "paulo.analista@outlook.com", x => x.DataCadastro, true);
  //ou
  var jogadorCollection = _repositoryJogador.ListarEOrdenadosPor(x => x.Email == "paulo.analista@outlook.com", x => x.DataCadastro, true, QueryTrackingBehavior.TrackAll);
  
  //Listar sem o Tracking
  var jogadorCollection = _repositoryJogador.ListarEOrdenadosPor(x => x.Email == "paulo.analista@outlook.com", x => x.DataCadastro, true, QueryTrackingBehavior.NoTracking);
  //ou
  var jogadorCollection = _repositoryJogador.ListarEOrdenadosPor(x => x.Email == "paulo.analista@outlook.com", x => x.DataCadastro, true, QueryTrackingBehavior.NoTrackingWithIdentityResolution);
  
  //Listar com Include
  var jogadorCollection = _repositoryJogador.ListarEOrdenadosPor(x => x.Email == "paulo.analista@outlook.com", x => x.DataCadastro, true, QueryTrackingBehavior.NoTrackingWithIdentityResolution, x=>x.Sala);
  //ou
  var jogadorCollection = _repositoryJogador.ListarEOrdenadosPor(x => x.Email == "paulo.analista@outlook.com", x => x.DataCadastro, true, QueryTrackingBehavior.NoTrackingWithIdentityResolution, "Sala");
  
  //Usando mais de uma condição
  var jogadorCollection = _repositoryJogador.ListarEOrdenadosPor(x => x.Email == "paulo.analista@outlook.com" && x.Sala.Nome=="Sala'", x => x.DataCadastro, true, x=>x.Sala);
```

### Sobre os Includes
O include pode ser passado via expressão lambda ou via string
```csharp
var jogadorCollection = _repositoryJogador.ListarPor(x => x.Email == "paulo.analista@outlook.com", x => x.Sala, x=>x.Turma, x=>x.Empresa);
// ou
var jogadorCollection = _repositoryJogador.ListarPor(x => x.Email == "paulo.analista@outlook.com", "Sala", "Turma", "Departamento.Empresa");
```

### Obtendo entidades
Para obter uma entidade basta passar a condição
```csharp
Jogador joagador = _repositoryJogador.ObterPor(x => x.Email == "paulo.analista@outlook.com");
//ou
Jogador joagador = _repositoryJogador.ObterPor(x => x.Email == "paulo.analista@outlook.com", QueryTrackingBehavior.TrackAll, x=>x.Sala);
```

### Sugestões
Fique a vontade para enviar sugetões, a ideia é facilitar nosso trabalho.

# VEJA TAMBÉM
## Grupo de Estudo no Telegram
- [Participe gratuitamente do grupo de estudo](https://t.me/blogilovecode)

## Cursos baratos!
- [Meus cursos](https://olha.la/udemy)

## Fique ligado, acesse!
- [Blog ILoveCode](https://ilovecode.com.br)

## Novidades, cupons de descontos e cursos gratuitos
https://olha.la/ilovecode-receber-cupons-novidades
