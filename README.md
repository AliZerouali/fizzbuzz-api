# 🚀 FizzBuzz API - Backend

[![.NET 8](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-blue.svg)](https://dotnet.microsoft.com/apps/aspnet)
[![Clean Architecture](https://img.shields.io/badge/Architecture-Clean_Architecture-green.svg)](https://github.com/ardalis/CleanArchitecture)

Une API REST pour générer des séquences FizzBuzz personnalisées, construite avec ASP.NET Core et suivant les principes de Clean Architecture.

## ✨ Fonctionnalités

- ✅ **Génération de séquences FizzBuzz** avec paramètres personnalisables
- ✅ **Statistiques** des requêtes les plus fréquentes
- ✅ **Architecture propre** (Clean Architecture) avec séparation des responsabilités
- ✅ **Validation complète** des entrées
- ✅ **Documentation Swagger/OpenAPI** intégrée
- ✅ **Cache en mémoire** pour les statistiques
- ✅ **Gestion d'erreurs** centralisée
- ✅ **Tests unitaires**

## 🏗 Architecture

Le projet suit une architecture en couches (Clean Architecture) :

```bash
fizzbuzz-webapi/
├── Domain/ # ❤️ Cœur du domaine métier
│ ├── Entities/ # Entités du domaine
│ └── ValueObjects # Objets de valeur
├── Application/ # 🎯 Cas d'utilisation et règles métier
│ ├── Interfaces/ # Contrats d'interface
│ ├── Services/ # Services d'application
│ └── Dtos/ # Objets de transfert de données
├── Infrastructure/ # 🔧 Implémentations externes
│ ├── Persistence/ # Accès aux données
│ └── Services/ # Services d'infrastructure
└── WebApi/ # 🌐 Couche présentation (API)
├── Controllers/ # Contrôleurs API
├── Middleware/ # Middleware personnalisé
└── Program.cs # Point d'entrée
```

## 📋 Prérequis

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- [Postman](https://www.postman.com/) ou [Insomnia](https://insomnia.rest/) (pour tester l'API)

## 🚀 Démarrage rapide

### Méthode 1 :

```bash
# Cloner le dépôt
git clone https://github.com/AliZerouali/FizzBuzz.WebApi.git
cd fizzbuzz-webapi

# L'API sera disponible sur https://localhost:5000
# Swagger UI sur https://localhost:5000/swagger
```

### Méthode 2 : Avec Visual Studio

```bash
Ouvrir FizzBuzzCleanArchitecture.sln

Définir FizzBuzz.WebApi comme projet de démarrage

Appuyer sur F5 ou Ctrl+F5 pour exécuter
```

🌐 Points de terminaison API

1. Générer une séquence FizzBuzz

```bash
GET /api/fizzbuzz/generate
```

Paramètres de requête :

| Paramètre | Type    | Requis | Description                        | Valeur par défaut |
| --------- | ------- | ------ | ---------------------------------- | ----------------- |
| int1      | integer | Oui    | Premier nombre pour les multiples  | 3                 |
| int2      | integer | Oui    | Deuxième nombre pour les multiples | 5                 |
| limit     | integer | Oui    | Limite de la séquence (1 à N)      | 100               |
| str1      | string  | Oui    | Chaîne pour les multiples de int1  | "fizz"            |
| str2      | string  | Oui    | Chaîne pour les multiples de int2  | "buzz"            |

Exemple de requête :

```bash
curl "http://localhost:5000/api/fizzbuzz/generate?int1=3&int2=5&limit=100&str1=fizz&str2=buzz"
```

Réponse :

```bash
{
  "result": [
    "1",
    "2",
    "fizz",
    "4",
    "buzz",
    "fizz",
    "7",
    "8",
    "fizz",
    "buzz",
    "..."
  ]
}
```

2. Obtenir les statistiques

```bash
GET /api/statistics/most-frequent
```

Réponse :

```bash
{
  "mostFrequentRequest": {
    "int1": 3,
    "int2": 5,
    "limit": 100,
    "str1": "fizz",
    "str2": "buzz"
  },
  "hitCount": 42,
  "lastUpdated": "2024-01-15T10:30:00Z"
}
```
