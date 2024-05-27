DESAFIO.BLOG API
Bem-vindo ao repositório da API DESAFIO.BLOG. Esta API fornece funcionalidades de autenticação, gerenciamento de usuários e gerenciamento de postagens de blog. A API é construída usando ASP.NET Core e segue os princípios RESTful.

Índice
Instalação
Uso
Endpoints
AuthController
PostsController
Segurança
Contribuição
Licença
Instalação
Clone o repositório:

bash
Copiar código
git clone https://github.com/seu-usuario/DESAFIO.BLOG.git
cd DESAFIO.BLOG
Restaure as dependências:

bash
Copiar código
dotnet restore
Atualize a string de conexão do banco de dados:

Modifique o arquivo appsettings.json para atualizar sua string de conexão com o banco de dados.

Aplique as migrações:

bash
Copiar código
dotnet ef database update
Execute a aplicação:

bash
Copiar código
dotnet run
Uso
Depois que a aplicação estiver em execução, você pode usar ferramentas como Postman ou curl para interagir com a API. A API está hospedada em https://localhost:5001.

Endpoints
AuthController
Endpoints relacionados à autenticação e gerenciamento de usuários.

POST api/auth/login

Faz login de um usuário e recebe um token JWT.

json
Copiar código
{
  "email": "usuario@exemplo.com",
  "password": "suasenha"
}
POST api/auth/register

Registra um novo usuário.

json
Copiar código
{
  "email": "usuario@exemplo.com",
  "password": "suasenha",
  "isAdmin": false
}
POST api/auth/logout

Faz logout do usuário atual.

GET api/auth/verifytoken

Verifica o token JWT e obtém informações do usuário.

PostsController
Endpoints relacionados ao gerenciamento de postagens de blog.

GET api/posts

Obtém todas as postagens do blog. Este endpoint está aberto a todos os usuários.

GET api/posts/{id}

Obtém uma postagem específica por ID. Este endpoint está aberto a todos os usuários.

POST api/posts

Cria uma nova postagem no blog. Este endpoint requer autenticação.

json
Copiar código
{
  "title": "Título da Postagem",
  "content": "Conteúdo da postagem...",
  "userId": "id-do-usuario-guid"
}
PUT api/posts/{id}

Atualiza uma postagem existente. Este endpoint requer autenticação.

json
Copiar código
{
  "id": "id-da-postagem-guid",
  "title": "Título Atualizado da Postagem",
  "content": "Conteúdo atualizado da postagem...",
  "userId": "id-do-usuario-guid"
}
DELETE api/posts/{id}

Exclui uma postagem. Este endpoint requer autenticação.

Segurança
A API usa JWT (JSON Web Token) para proteger os endpoints.
Ações sensíveis como criar, atualizar e excluir postagens requerem que o usuário esteja autenticado.
Os usuários podem ser atribuídos a funções (por exemplo, Admin) para conceder permissões adicionais.
Contribuição
Faça um fork do repositório.
Crie um novo branch (git checkout -b feature-branch).
Faça suas alterações.
Commit suas alterações (git commit -m 'Adicione nova funcionalidade').
Push para o branch (git push origin feature-branch).
Crie um novo Pull Request.
