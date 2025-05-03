Documentation Technique Complète - API TaskFlow

Le projet TaskFlow a été conçu dans le but de fournir une API REST sécurisée et évolutive permettant la gestion de projets collaboratifs. L’objectif est d’offrir un socle technique moderne reposant sur ASP.NET Core, intégrant la gestion des utilisateurs, des projets, des tâches, ainsi qu’une authentification sécurisée basée sur JWT.

Cette API est idéale pour servir de backend à des applications web ou mobiles.
1. Prérequis
Avant de pouvoir installer et exécuter le projet, assurez-vous d’avoir les éléments suivants installés :
- .NET SDK version 8 ou 9 (https://dotnet.microsoft.com/download)
- SQL Server (LocalDB ou SQL Server Express recommandé)
- Visual Studio 2022 ou plus, ou Visual Studio Code
- Entity Framework CLI (commande : dotnet tool install --global dotnet-ef)
2. Instructions d'installation
1. Cloner le projet ou copier les fichiers sources dans un dossier local.
2. Ouvrir le projet dans Visual Studio ou VS Code.
3. Vérifier le fichier appsettings.json pour configurer la chaîne de connexion SQL Server :
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TaskFlowDb;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "clé_secrète",
    "Issuer": "TaskFlowAPI",
    "Audience": "TaskFlowClient",
    "ExpiresInMinutes": "60"
  }
}
4. Restaurer les dépendances : dotnet restore
5. Appliquer les migrations pour créer la base de données :
   dotnet ef migrations add InitialCreate
   dotnet ef database update
3. Comment exécuter l’API
Dans le dossier TaskFlow.WebApi, exécutez la commande suivante :
dotnet run
L’API démarre alors sur http://localhost:5021 (ou 5001 en HTTPS).
Vous pouvez tester les endpoints avec Swagger à l’URL suivante :
http://localhost:5021/swagger
4. Architecture et Structure
L'API suit une architecture en couches :
- Couche API (Controllers)
- Couche Métier (Repositories)
- Couche Données (EF Core, Models)
- Authentification basée sur JWT
- Sécurité avec autorisation par rôle
5. Authentification JWT
Le système d’authentification repose sur JWT (JSON Web Tokens). Les utilisateurs s’enregistrent via /api/users/register et obtiennent un token via /api/users/login. Ce token doit être transmis dans l’en-tête 'Authorization' pour accéder aux routes protégées.
Exemple d'en-tête HTTP à inclure :
Authorization: Bearer {token}
6. Contrôleurs et Endpoints
6.1 ProjectsController
- [GET] /api/projects : Liste tous les projets
- [GET] /api/projects/{id} : Récupère un projet
- [POST] /api/projects : Crée un projet
- [PUT] /api/projects/{id} : Met à jour un projet
- [DELETE] /api/projects/{id} : Supprime un projet
6.2 TasksController
- [GET] /api/tasks : Liste toutes les tâches
- [GET] /api/tasks/{id} : Récupère une tâche
- [POST] /api/tasks : Crée une tâche
- [PUT] /api/tasks/{id} : Met à jour une tâche
- [DELETE] /api/tasks/{id} : Supprime une tâche
6.3 UsersController
- [POST] /api/users/register : Enregistrement
- [POST] /api/users/login : Connexion avec retour d’un JWT
6.4 WeatherForecastController
- [GET] /weatherforecast : Données météo de démonstration
7. Middleware et Sécurité
- Middleware de gestion d’erreurs global : intercepte les exceptions non gérées (500).
- Configuration CORS : permet les appels cross-origin (Postman, navigateur, etc.).
8. Modèle de Données
- User : Id, Name, Email, PasswordHash, Role
- Project : Id, Name, Description
- TaskItem : Id, Title, Status, DueDate, Commentaires, ProjectId (clé étrangère)
9. Technologies utilisées
- .NET 8 ou 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / LocalDB
- JWT (JSON Web Tokens)
- BCrypt (Hash de mot de passe)
- Swagger pour documentation des endpoints

10. Modèles de Données
10.1 User
Représente un utilisateur de la plateforme TaskFlow.
- Id (int) : Identifiant unique de l&#39;utilisateur
- Name (string) : Nom complet
- Email (string) : Adresse email unique
- PasswordHash (string) : Hash du mot de passe via BCrypt
- Role (enum UserRole) : Rôle de l&#39;utilisateur (Admin ou User)
- Projects (ICollection&lt;Project&gt;) : Liste des projets liés à l&#39;utilisateur
10.2 Project
Représente un projet créé par un utilisateur.
- Id (int) : Identifiant unique du projet
- Name (string) : Nom du projet
- Description (string, optionnel) : Description du projet
- CreationDate (DateTime) : Date de création du projet
- UserId (int) : Identifiant de l&#39;utilisateur créateur
- User (User) : Référence vers l&#39;utilisateur ayant créé le projet
- Tasks (ICollection&lt;TaskItem&gt;) : Liste des tâches associées au projet
10.3 TaskItem
Représente une tâche liée à un projet.
- Id (int) : Identifiant unique de la tâche
- Title (string) : Titre de la tâche
- Status (enum TaskStatus) : Statut actuel de la tâche (ÀFaire, EnCours, Terminé)
- DueDate (DateTime?, optionnel) : Date limite de la tâche
- ProjectId (int) : Identifiant du projet associé
- Project (Project) : Projet auquel la tâche est liée
- Commentaires (List&lt;string&gt;) : Commentaires associés à la tâche
10.4 Énumérations
- UserRole : { Admin, User }
- TaskStatus : { ÀFaire, EnCours, Terminé }
