# 🎬 CineSeats

O *CineSeats* é um sistema completo de gestão de catálogo e venda de bilhetes para cinemas. O projeto foi desenhado aplicando conceitos avançados de Engenharia de Software, como *Domain-Driven Design (DDD)* e *Clean Architecture*, garantindo uma separação clara de responsabilidades e regras de negócio isoladas.

O sistema divide-se em dois grandes ecossistemas: um *Painel de Administração* (para gerir salas, filmes e sessões) e um *Portal do Cliente* (para visualização do catálogo, escolha de lugares e compra de bilhetes).

---

## 🏗️ Arquitetura e Bounded Contexts

O back-end foi modelado seguindo os princípios de Domain-Driven Design, dividindo a aplicação em dois Bounded Contexts principais:

1. *Catalogue (Catálogo):*
   - Responsável pela gestão central do cinema.
   - *Agregados/Entidades:* Movie (Filme), Room (Sala), Session (Sessão) e Admin (Administrador).
   - *Casos de Uso:* Criação de filmes, configuração do mapa de cadeiras nas salas e geração em lote de sessões.

2. *Tickets (Bilheteira):*
   - Responsável pela transação comercial e alocação de lugares para o cliente final.
   - *Agregados/Entidades:* Order (Encomenda/Pedido), SessionSeat (Lugar da Sessão) e Ticket (Bilhete).
   - *Casos de Uso:* Bloqueio e venda de lugares, processamento da compra e geração de referência de pedido/QR Code.

---

## 🚀 Tecnologias Utilizadas

### Back-end (API)
* *Linguagem:* C# (.NET Core)
* *Arquitetura:* Clean Architecture (Domain, Application, Infrastructure, Presentation)
* *Base de Dados:* Entity Framework Core (Suporte a PostgreSQL e base de dados In-Memory para testes)
* *Documentação:* Swagger / OpenAPI

### Front-end (Interface)
* *Tecnologias:* HTML5, CSS3, Vanilla JavaScript (JS Puro)
* *Comunicação:* API Fetch
* *Gestão de Estado:* Manipulação direta do DOM (domManager.js) e armazenamento temporário via sessionStorage.

---

## ⚙️ Funcionalidades

### 👨‍💼 Área do Administrador
* Autenticação e acesso ao Dashboard.
* *Gestão de Salas:* Criação de salas com mapeamento dinâmico de filas e lugares (ex: A:10, B:10).
* *Gestão de Filmes:* Registo de filmes com título e duração em minutos.
* *Gestão de Sessões:* Associação de filmes a salas, definindo horários e preços dos bilhetes.

### 🍿 Portal do Cliente
* Listagem dos filmes atualmente em exibição.
* Consulta das sessões disponíveis filtradas por filme escolhido.
* Visualização da grelha de lugares da sessão (com indicação visual de lugares livres, selecionados e vendidos).
* Checkout e finalização da compra com geração de referência da encomenda.

---

## 🛠️ Como Executar o Projeto Localmente

### 1. Iniciar a API (Back-end)
A API foi desenvolvida para correr facilmente no *JetBrains Rider* (ou Visual Studio / CLI):

1. Abra a solução CineSeats.sln no JetBrains Rider.
2. Defina o projeto CineSeats (API) como o projeto de arranque (Startup Project).
3. Verifique se o Program.cs está configurado para a base de dados In-Memory (se não quiser configurar o PostgreSQL localmente).
4. Clique em *Run* (Play).
5. O Swagger abrirá automaticamente no navegador (ex: http://localhost:5242/swagger), indicando que a API está a escutar os pedidos.

### 2. Iniciar a Interface (Front-end)
Como o projeto utiliza Vanilla JS com módulos, os ficheiros HTML precisam de ser servidos através de um servidor web local para evitar bloqueios de CORS:

1. No explorador de ficheiros do JetBrains Rider, navegue até à pasta do front-end (CineSeatsWeb).
2. Abra o ficheiro index.html (Portal do Cliente) ou pages/catalogue/login.html (Painel de Administração).
3. Utilize o *servidor web embutido do Rider* clicando no ícone do navegador que aparece no canto superior direito do editor.

*Credenciais de Teste para o Painel de Administração:*
* *Email:* admin@cineseats.com
* *Password:* admin

---

## 👥 Autores e Responsabilidades

* *Luan:* Codificação dos Bounded Contexts, modelagem dos Agregados/Entidades, desenvolvimento dos Casos de Uso (Application) e arquitetura visual/gestão de estado do Front-end.
* *Miguel:* Infraestrutura, configuração de Injeção de Dependências, padronização dos Controladores da API, auxiliação na codificação dos contextos e integração de serviços do Front-end (api.js e métodos Fetch)
