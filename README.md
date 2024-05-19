# Examen certifiant_BLOC3C#_Leveil_John

Instructions pour Lancer le Projet en Local
1. Prérequis
Avant de commencer, assurez-vous d'avoir les outils suivants installés sur votre machine :

Visual Studio 2019/2022 (ou une version plus récente)
SQL Server (ou SQL Server Express)
Git
2. Cloner le Répertoire du Projet
Ouvrez une ligne de commande ou un terminal et clonez le répertoire du projet à partir du dépôt GitHub :
```
git clone https://github.com/votre-repo/nom-du-projet.git
```
Ou depuis Visual Studio :
Depuis le menu Démarrer de Visual Studio, cliquer sur 'Cloner un dépot', puis coller le lien du dépot, et 'cloner'.

3. Configuration de la Base de Données
Avant de lancer l'application, configurez la base de données.

Ouvrez le fichier appsettings.json dans le répertoire racine du projet.
Modifiez la chaîne de connexion pour pointer vers votre instance locale de SQL Server. Par exemple :
```
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=nom_de_votre_base_de_donnees;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

4. Appliquer les Migrations
Pour créer la base de données et appliquer les migrations, ouvrez le Package Manager Console dans Visual Studio (Outils > Gestionnaire de package NuGet > Console du gestionnaire de package) et exécutez les commandes suivantes :
```
Update-Database
```

5. Lancer l'Application
Vous pouvez maintenant lancer l'application. Dans Visual Studio, cliquez sur le bouton Démarrer (ou appuyez sur F5).

6.Créer un Compte Administrateur
Pour accéder aux fonctionnalités administratives de l'application, vous devrez créer un compte administrateur avec l'adresse e-mail adminjo2024@yopmail.com et lui attribuer le rôle administrateur. Voici les étapes à suivre pour le faire :

Pour créer un compte administrateur, vous pouvez créer le compte adminjo2024@yopmail.com depuis le site en créant un compte.
Une fois le compte créer, ouvrez SQL Server Management Studio (SSMS) ou tout autre outil de gestion de base de données que vous utilisez.
Sélectionnez votre base de données utilisée par l'application.
Noter l'id du compte adminjo2024@yopmail.com que vous trouverez dans la table AspNetUser, colonne Id.
Insérer l'id du compte adminjo2024@yopmail.com dans la colonne UserId de la table AspNetUserRoles et cette id '1793b207-0427-4cf0-be54-2b5a59b37e03' dans la colonne RoleId.
```
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES ('id du compte', '1793b207-0427-4cf0-be54-2b5a59b37e03');
```

6.2 Vérifier la Création du Compte
Lancez l'application et connectez-vous en utilisant les identifiants suivants :

E-mail : adminjo2024@yopmail.com
Mot de passe : Celui que vous avez haché et inséré dans la base de données.
Accédez aux fonctionnalités administratives de l'application pour vérifier que le compte a bien été configuré avec le rôle administrateur.
