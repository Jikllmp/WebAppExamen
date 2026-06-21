# ImmoSearch

Projet réalisé dans le cadre du cours Angular / .NET. C'est un clone simplifié d'Immoweb : on peut consulter des annonces immobilières, les filtrer, et prendre rendez-vous directement depuis une annonce.

## Stack

**Backend**
- ASP.NET Core (.NET 10)
- Clean Architecture : Api / Core / Infrastructure
- Dapper (pas d'Entity Framework)
- SQL Server

**Frontend**
- Angular 22
- Nouvelle syntaxe (@if, @for)
- Services Angular pour la gestion d'état (pas de NgRx)

## Ce que ça fait

- Inscription/connexion, deux rôles : Particulier et Agence
- Liste des annonces avec filtres (prix, région, type de bien)
- Détail d'une annonce
- Publication/modification/suppression d'annonce, uniquement si on est l'agence qui l'a publiée
- Prise de RDV sur une annonce
- Liste des RDV (côté agence on voit aussi les infos du particulier)
- Favoris

## Prérequis — tout ce qu'il faut installer avant de commencer

### 1. .NET SDK 10

Télécharger et installer : https://dotnet.microsoft.com/download/dotnet/10.0

Vérifier l'installation :
```
dotnet --version
```
Doit afficher une version 10.x.x

### 2. Node.js (version 20 ou plus)

Télécharger et installer : https://nodejs.org/

Vérifier l'installation :
```
node --version
npm --version
```

### 3. Angular CLI

Une fois Node.js installé :
```
npm install -g @angular/cli
```

Vérifier l'installation :
```
ng version
```
Doit afficher une version Angular CLI 22.x.x ou compatible.

### 4. SQL Server

Le projet utilise SQL Server. Si ce n'est pas déjà installé sur la machine :

Télécharger SQL Server Express (gratuit) : https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads
- Choisir le type d'installation "Basic"
- Laisser les options par défaut
- Noter le nom de l'instance affiché à la fin de l'installation (ex : `NOM-PC\SQLEXPRESS`)

Pour exécuter le script SQL, un client graphique est conseillé mais pas obligatoire :
- SQL Server Management Studio (SSMS) — proposé directement après l'installation de SQL Server, ou ici : https://learn.microsoft.com/fr-fr/sql/ssms/download-sql-server-management-studio-ssms
- Ou Azure Data Studio, ou sqlcmd en ligne de commande

### 5. Git

Pour cloner le dépôt. Si pas déjà installé : https://git-scm.com/downloads

### 6. Un IDE

Visual Studio Code (https://code.visualstudio.com/) ou Visual Studio suffisent. Extensions utiles côté VS Code : C# Dev Kit, Angular Language Service.

## Installation du projet

### 1. Cloner le dépôt

```
git clone https://github.com/Jikllmp/WebAppExamen.git
cd WebAppExamen
```

### 2. Créer la base de données

Ouvrir SSMS (ou équivalent), se connecter à l'instance SQL Server locale (Windows Authentication), ouvrir une nouvelle requête, coller le contenu du fichier `database/script.sql`, puis l'exécuter (F5).

Ce script crée la base `ImmoSearch`, toutes les tables nécessaires (Utilisateur, Annonce, Region, TypeBien, Commodite, AnnonceCommodite, RendezVous, Favori), et insère les données de référence (régions, types de bien, commodités).

### 3. Configurer la chaîne de connexion

Ouvrir `Api/appsettings.json` et adapter la ligne suivante avec le nom de l'instance SQL Server notée à l'installation :

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=NOM_DE_VOTRE_INSTANCE;Database=ImmoSearch;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Exemple : si l'instance s'appelle `DESKTOP-XXXX\SQLEXPRESS`, la ligne devient :
```json
"DefaultConnection": "Server=DESKTOP-XXXX\\SQLEXPRESS;Database=ImmoSearch;Trusted_Connection=True;TrustServerCertificate=True;"
```

(Le `\\` avec double backslash est nécessaire en JSON.)

### 4. Lancer le backend

Dans un terminal, à la racine du projet :

```
cd Api
dotnet restore
dotnet run
```

L'API démarre sur `http://localhost:5202`.

Pour vérifier que ça fonctionne, ouvrir dans un navigateur : `http://localhost:5202/scalar` — la documentation interactive des endpoints doit s'afficher.

### 5. Lancer le frontend

Dans un **second terminal** (laisser le premier ouvert avec le backend qui tourne), à la racine du projet :

```
cd frontend
npm install
ng serve
```

L'application est accessible sur `http://localhost:4200`.

## Comptes de test

Aucun compte n'est préchargé avec un mot de passe valide. Pour tester l'application, créer un compte via la page d'inscription (`/register`) :

- **Particulier** : nom, prénom, email, mot de passe, téléphone
- **Agence** : mêmes informations + numéro de TVA (le champ apparaît automatiquement quand le rôle Agence est sélectionné)

Conseil de test : créer un compte Agence pour publier des annonces, puis un compte Particulier (dans un autre navigateur ou en navigation privée) pour réserver un rendez-vous et consulter les favoris.

## Structure du projet

```
WebAppExamen/
├── Api/              -> points d'entrée HTTP, configuration JWT/CORS
├── Core/              -> entités, interfaces, logique métier (UseCases)
├── Infrastructure/    -> accès base de données (Dapper), implémentation des contrats du Core
├── database/          -> script SQL de création de la base
├── frontend/          -> application Angular
└── README.md
```

### Flux d'une requête

```
Composant Angular -> Service Angular -> Endpoint API -> UseCase (Core) -> Gateway (Core) -> Repository (Infrastructure) -> Base de données
```

Le Core ne dépend d'aucune autre couche : il définit des interfaces (`IGateways`, `UseCases/Abstractions`) que l'Infrastructure et l'API viennent implémenter ou consommer.

## Problèmes fréquents

**Le backend ne se connecte pas à la base de données**
Vérifier que le nom du serveur dans `appsettings.json` correspond exactement à l'instance SQL Server locale, et que le service SQL Server est démarré.

**Erreur CORS dans la console du navigateur**
Vérifier que le backend tourne bien sur le port 5202 et que le frontend pointe vers cette URL dans les services (`environment` ou directement dans les fichiers de service).

**`ng serve` échoue avec une erreur de dépendances**
Supprimer `node_modules` et `package-lock.json`, puis refaire `npm install`.
