# Examen certifiant_BLOC3C#_Leveil_John

Présentation <br />
Bienvenue dans mon application pour la gestion des billets des Jeux Olympiques 2024, développée en ASP.NET Core. Cette application permet aux utilisateurs de parcourir et ajouter des offres à leur panier, et aux administrateurs de gérer les offres. Elle utilise Entity Framework Core pour la gestion des données et inclut des fonctionnalités de sécurité robustes pour garantir la protection des données utilisateur.

Fonctionnalités <br />
Gestion des Offres : Les utilisateurs peuvent consulter les offres disponibles. <br />
Panier d'Achat : Les utilisateurs peuvent ajouter des offres à leur panier. <br />
Authentification et Autorisation : Utilisation d'ASP.NET Core Identity pour gérer l'authentification et l'autorisation. <br />
Gestion des Rôles : Les administrateurs peuvent gérer les offres. <br />

Technologies Utilisées<br />
ASP.NET Core <br />
Entity Framework Core <br />
SQL Server <br />
Bootstrap (pour le design de l'interface utilisateur) <br />
<br />

Instructions pour Lancer le Projet en Local
1. Prérequis :
   
Avant de commencer, assurez-vous d'avoir les outils suivants installés sur votre machine :

Visual Studio 2019/2022 (ou une version plus récente)
SQL Server
Git

2. Cloner le Répertoire du Projet :
   
Ouvrez une ligne de commande ou un terminal et clonez le répertoire du projet à partir du dépôt GitHub :
```
git clone https://github.com/johnlvl/Examen-certifiant_BLOC3C-_Leveil_John.git
```
Ou depuis Visual Studio :

Depuis le menu Démarrer de Visual Studio, cliquer sur 'Cloner un dépot', puis coller le lien du dépot : https://github.com/johnlvl/Examen-certifiant_BLOC3C-_Leveil_John.git, et 'cloner'.

3. Configuration de la Base de Données :
   
Avant de lancer l'application, configurez la base de données.

Ouvrez le fichier appsettings.json dans le répertoire racine du projet.
Modifiez la chaîne de connexion pour pointer vers votre instance locale de SQL Server. Par exemple :
```
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=nom_de_votre_base_de_donnees;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

4. Appliquer les Migrations :

Pour créer la base de données et appliquer les migrations, ouvrez le Package Manager Console dans Visual Studio (Outils > Gestionnaire de package NuGet > Console du gestionnaire de package) coller puis exécutez la commandes suivantes :
```
Update-Database
```

5. Lancer l'Application :
   
Vous pouvez maintenant lancer l'application. Dans Visual Studio, cliquez sur le bouton Démarrer (ou appuyez sur F5).
Des fenêtres (quatres normalement) pour approuver le certificat SSL vont s'ouvrir, cliquer sur "Oui" jusqu'a ne plus en avoir.
Si vôtre navigateur n'ouvre pas la page par exemple sur Chrome, en général il devrait y avoir un message bloquant, vous pouvez lancer l'application dans ce cas sur un autre navigateur :
Cliquer sur le menu déroulant "https" ensuite Naviguer avec... > Choisissez le navigateur > Parcourir

6. Créer un Compte Administrateur :

Pour accéder aux fonctionnalités administratives de l'application, vous devrez créer un compte administrateur avec l'adresse e-mail adminjo2024@yopmail.com et lui attribuer le rôle administrateur. 
Voici les étapes à suivre pour le faire :

Pour créer un compte administrateur, vous pouvez créer le compte adminjo2024@yopmail.com depuis le site en créant un compte.

Une fois le compte créer, ouvrez SQL Server Management Studio (SSMS), pour ce faire cliquer sur "Rechercher" dans Visual Studio, dans la barre de recherche taper "SQL", ouvrez "Explorateur d'objets SQL Server".

Un menu sur la gauche de l'écran devrait s'ouvrir, ensuite, déplier (localdb)\MSSQLLocalDB... puis "Base de données" et ensuite vôtre base de donnée exemple "aspnet-Examen_certifiant_BLOC3_...".

Déplier "Tables", faite un clique droit sur dbo.AspNetUsers > Afficher les données. Vous devriez avoir vôtre compte dans une coloone, noter l'id du compte que vous avez créer, que vous trouverez dans la colonne Id.

Ensuite, faite un clique droit sur dbo.AspNetRoles > Afficher les données. Dans cette table, vous trouverez un id pour les utilisateurs et un id pour les administrateurs, par défaut tout les comptes sont utilisateurs.

Noter l'id pour les administrateurs.

Faite un clique droit sur dbo.AspNetUsersRoles > Afficher les données.

Cliquer dans un champs "NULL" dans la colonne UserId et coller/insérer l'id du compte que vous avez créé puis dans la colonne RoleId, coler/insérer l'id pour les administrateurs.

Exemple de la commande :
```
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES ('id du compte', 'id administrateur');
```

6.2 Vérifier la Création du Compte
Lancez l'application et connectez-vous en utilisant les identifiants suivants :

E-mail : adminjo2024@yopmail.com
Mot de passe : Celui que vous avez inséré.
Accédez aux fonctionnalités administratives de l'application pour vérifier que le compte a bien été configuré avec le rôle administrateur.
