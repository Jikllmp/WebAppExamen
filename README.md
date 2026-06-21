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

## Prérequis

- .NET SDK 10
- Node.js 20+
- Angular CLI 22
- SQL Server Express

## Installation

### Cloner

```
git clone https://github.com/Jikllmp/WebAppExamen.git
cd WebAppExamen
```

### Base de données

Le script SQL est dans `database/script.sql`. L'ouvrir dans SSMS et l'exécuter — ça crée la base `ImmoSearch` avec toutes les tables et insère les régions/types de bien/commodités de base.

### Connection string

Dans `Api/appsettings.json`, changer le nom du serveur pour celui de votre instance SQL Server :

```
"DefaultConnection": "Server=VOTRE_INSTANCE;Database=ImmoSearch;Trusted_Connection=True;TrustServerCertificate=True;"
```

Le nom de l'instance s'affiche à la fin de l'install de SQL Server (genre `DESKTOP-XXXX\SQLEXPRESS`).

### Lancer le backend

```
cd Api
dotnet restore
dotnet run
```

L'API tourne sur `http://localhost:5202`. Pour tester les routes sans passer par le frontend, Scalar est dispo sur `http://localhost:5202/scalar`.

### Lancer le frontend

Dans un autre terminal :

```
cd frontend
npm install
ng serve
```

App accessible sur `http://localhost:4200`.

## Comptes de test

Pas de compte préchargé avec mot de passe valide. Faut s'inscrire via `/register` en choisissant Particulier ou Agence (le champ TVA n'apparaît que pour Agence).

## Structure

```
Api/              -> endpoints, config JWT/CORS
Core/             -> entités, interfaces, logique métier
Infrastructure/   -> Dapper, accès BDD
database/         -> script SQL
frontend/         -> Angular
```

Flux d'une requête : Component Angular -> Service Angular -> Endpoint API -> UseCase -> Gateway -> Repository -> BDD.

Le Core ne dépend de rien d'autre, c'est lui qui définit les interfaces que Infrastructure et Api viennent implémenter/utiliser.
