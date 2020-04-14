# thecatapi
Aplicação para coletar informações da API de Gatos: thecatapi.com. Estas informações serão armazenadas em uma base de dados e disponibilizadas através de uma API-REST. Para consultas da API, serão criados logs e métricas de execução utilizando ferramentas de logging e métricas.

# Construção do Projeto
Utilizando a metodologia Ágil (Scrum), primeiro, foi entendido o problema, afim de criar um BackLog no Azure (TFS) para definir os *Épicos* e *Backlog Item*, para determinar um MVP e assim, iniciar a criação da arquitetura e a solução do projeto. 

O problema consiste em:

- Para cada uma das raças de gatos disponíveis, armazenar as informações de origem, temperamento e descrição em uma base de dados. (se disponível)
- Para cada uma das raças acima, salvar o endereço de 3 imagens em uma base de dados. (se disponível)
- Salvar o endereço de 3 imagens de gatos com chapéu.
- Salvar o endereço de 3 imagens de gatos com óculos.

Estas informações deverão ser disponibilizadas em uma API Rest da seguinte forma:

- API capaz de listar todas as raças
- API capaz de listar as informações de uma raça
- API capaz de a partir de um temperamento listar as raças
- API capaz de a partir de uma origem listar as raças 

Segue o detalhamento do projeto:


### Estruturar solução
#### Criar uma solution para o projeto
Criar uma Solution, separando em projetos de acordo com o contexto de cada um: 
- Domínio, 
- Interfaces de integração, 
- Camada para persistência e leitura da base de dados, 
- Aplicação Projeto para orquestrar funcionalidades entre projetos
- Projeto de Testes (TDD)

Com a criação desta solution, já é possível executar a leitura da TheCatAPI, armazenar na base de dados e as consultas solicitadas no projeto, com isso a construção de uma **Aplicação** e uma **API Rest** servem para poder interfacear com o usuário as informações.
#### Criar projeto de Testes TDD
Criar um projeto, utilizando XUnit para montar planos de testes e executar as rotinas que serão implementadas nos demais projetos.
#### Criar projeto de domínio
Estruturar todo domínio da solução necessário para coletar informações do The-Cat, armazenar em base de dados, consulta de informações, logs e métricas.

Neste projeto, serão definidas:

- Entities: Entidades de banco de dados, que serão utilizadas no projeto repositório, com o auxilio de uma ferramenta ORM (no caso Dapper), e será responsável por determinar o modelo de banco de dados.
- Models: Classes de objetos contendo regras de negócio e modelos para integração com a API TheCats.
- Interfaces: Assinaturas abstratas definindo o comportamento que as classes criadas em outros projetos deverão seguir.  
#### Criar projeto de interfaces de integração
Criar um projeto que permita a interface de Integração com o The-Cat.

- Este projeto será responsável em fazer as chamadas da API TheCat e retornar as informações serializadas para um objeto facilitando assim a manipulação dos dados.
#### Criar projeto de persistência da aplicação
Criar projeto responsável em armazenar e consultar informações que foram coletadas do The-Cat
#### Criar projeto Application
Esta camada deve servir para orquestrar os demais projetos criados, para conseguir executar as funcionalidades requeridas.

### Aplicação para coleta de dados / API para expor informações coletadas
#### Criar projeto Blazor Server / API
Projeto será criado em .NET Core 3.1, no padrão Blazor Server, contendo interface WebService para que o usuário possa coletar informações e também uma API Rest com os métodos utilizados para expor informações coletadas. A principal vantagem nesta abordagem é que no mesmo projeto podemos ter a Interface e a API em uma única publicação.
O projeto deverá utilizar o Swagger para expor a documentação da API permitindo com isso a iteração de forma documentada.

Imagem 1: Interface do Swagger com a definição da API e métodos disponíveis. O Swagger também permite a execução dos mesmos.

![Alt text](https://user-images.githubusercontent.com/13984252/79273106-d8ff3280-7e78-11ea-8b38-0c578d7fd1d8.png)




