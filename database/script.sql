CREATE DATABASE ImmoSearch;
GO

USE ImmoSearch;
GO

CREATE TABLE Utilisateur (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    Nom         NVARCHAR(100)  NOT NULL,
    Prenom      NVARCHAR(100)  NOT NULL,
    Email       NVARCHAR(255)  NOT NULL UNIQUE,
    MotDePasse  NVARCHAR(255)  NOT NULL,
    Telephone   NVARCHAR(20)   NOT NULL,
    Role        NVARCHAR(20)   NOT NULL CHECK (Role IN ('Particulier', 'Agence')),
    NumeroTVA   NVARCHAR(50)   NULL
);

CREATE TABLE TypeBien (
    Id      INT PRIMARY KEY IDENTITY(1,1),
    Libelle NVARCHAR(100) NOT NULL
);

CREATE TABLE Region (
    Id    INT PRIMARY KEY IDENTITY(1,1),
    Nom   NVARCHAR(100) NOT NULL
);

CREATE TABLE Commodite (
    Id      INT PRIMARY KEY IDENTITY(1,1),
    Libelle NVARCHAR(100) NOT NULL
);

CREATE TABLE Annonce (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    Titre           NVARCHAR(255)   NOT NULL,
    Description     NVARCHAR(MAX)   NOT NULL,
    Prix            DECIMAL(12, 2)  NOT NULL,
    NombrePieces    INT             NOT NULL,
    Superficie      DECIMAL(10, 2)  NOT NULL,
    Jardin          BIT             NOT NULL DEFAULT 0,
    Garage          BIT             NOT NULL DEFAULT 0,
    DatePublication DATETIME        NOT NULL DEFAULT GETDATE(),
    AgenceId        INT             NOT NULL,
    TypeBienId      INT             NOT NULL,
    RegionId        INT             NOT NULL,
    CONSTRAINT FK_Annonce_Agence    FOREIGN KEY (AgenceId)    REFERENCES Utilisateur(Id),
    CONSTRAINT FK_Annonce_TypeBien  FOREIGN KEY (TypeBienId)  REFERENCES TypeBien(Id),
    CONSTRAINT FK_Annonce_Region    FOREIGN KEY (RegionId)    REFERENCES Region(Id)
);

CREATE TABLE AnnonceCommodite (
    AnnonceId   INT NOT NULL,
    CommoditeId INT NOT NULL,
    PRIMARY KEY (AnnonceId, CommoditeId),
    CONSTRAINT FK_AC_Annonce   FOREIGN KEY (AnnonceId)   REFERENCES Annonce(Id),
    CONSTRAINT FK_AC_Commodite FOREIGN KEY (CommoditeId) REFERENCES Commodite(Id)
);

CREATE TABLE RendezVous (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    DateRdv         DATETIME    NOT NULL,
    Statut          NVARCHAR(20) NOT NULL DEFAULT 'EnAttente'
                    CHECK (Statut IN ('EnAttente', 'Confirme', 'Annule')),
    ParticulierId   INT         NOT NULL,
    AnnonceId       INT         NOT NULL,
    CONSTRAINT FK_RDV_Particulier FOREIGN KEY (ParticulierId) REFERENCES Utilisateur(Id),
    CONSTRAINT FK_RDV_Annonce     FOREIGN KEY (AnnonceId)     REFERENCES Annonce(Id)
);

CREATE TABLE Favori (
    UtilisateurId INT NOT NULL,
    AnnonceId     INT NOT NULL,
    DateAjout     DATETIME NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY (UtilisateurId, AnnonceId),
    CONSTRAINT FK_Favori_Utilisateur FOREIGN KEY (UtilisateurId) REFERENCES Utilisateur(Id),
    CONSTRAINT FK_Favori_Annonce     FOREIGN KEY (AnnonceId)     REFERENCES Annonce(Id)
);

INSERT INTO TypeBien (Libelle) VALUES
    ('Maison'), ('Appartement'), ('Terrain'), ('Bureau'), ('Studio');

INSERT INTO Region (Nom) VALUES
    ('Bruxelles'), ('Wallonie'), ('Flandre'), ('Namur'), ('Liège'), ('Charleroi');

INSERT INTO Commodite (Libelle) VALUES
    ('Piscine'), ('Ascenseur'), ('Cave'), ('Grenier'),
    ('Terrasse'), ('Parking'), ('Interphone');